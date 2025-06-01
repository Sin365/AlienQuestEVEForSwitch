#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class AxiProjectToolsStatistics : EditorWindow
{
    static string cachecfgPath = "Assets/AxiStatisticsDatas.asset";
    static Dictionary<string, AxiStatisticsDatas> dictTempData = new Dictionary<string, AxiStatisticsDatas>();

    static void ClearTempData()
    {
        dictTempData.Clear();
    }
    static string GetRootTempKey(int type, string rootName)
    {
        return type + "_" + rootName;
    }

    static void AddComponentData(int _type, string _rootPath, AxiStatistics_Node_Component _com, string _nodepath)
    {
        string rootKey = GetRootTempKey(_type, _rootPath);

        if (!dictTempData.ContainsKey(rootKey))
        {
            dictTempData[rootKey] = new AxiStatisticsDatas() { type = _type, FullPath = _rootPath, nodes = new List<AxiStatistics_Node>() };
        }

        AxiStatisticsDatas rootData = dictTempData[rootKey];

        AxiStatistics_Node nodeData = rootData.nodes.Where(w => w.NodeFullPath == _nodepath).FirstOrDefault();
        if (nodeData == null)
        {
            nodeData = new AxiStatistics_Node();
            nodeData.Name = Path.GetFileName(_nodepath);
            nodeData.NodeFullPath = _nodepath;
            nodeData.components = new List<AxiStatistics_Node_Component>();
            rootData.nodes.Add(nodeData);
        }

        nodeData.components.Add(_com);
    }


    static bool CheckCom(int _type, string _rootPath, Component com, string nodepath)
    {
        if (com is BoxCollider2D)
        {
            BoxCollider2D bc = com as BoxCollider2D;
#if UNITY_2017_1_OR_NEWER
            Debug.Log(nodepath + "BoxCollider2D->center=>(" + bc.offset.x + "," + bc.offset.y + ") size=>(" + bc.size.x + "," + bc.size.y + "");
#else
			Debug.Log(nodepath +"BoxCollider2D->center=>("+ bc.center.x+","+bc.center.y+") size=>("+ bc.size.x+","+bc.size.y+"");
#endif
            AxiStatistics_Node_Component _com = new AxiStatistics_Node_Component();
            _com.type = typeof(BoxCollider2D).ToString();


#if UNITY_2017_1_OR_NEWER
            _com.center = bc.offset;
#else
            _com.center = bc.center;
#endif

            _com.size = bc.size;
            AddComponentData(_type, _rootPath, _com, nodepath);
        }
        if (com is Rigidbody2D)
        {
            Rigidbody2D rig2d = com as Rigidbody2D;
            Debug.Log(_rootPath + "Rigidbody2D->simulated=>(" + rig2d.simulated + ")");
            Debug.Log(_rootPath + "Rigidbody2D->IsSleeping=>(" + rig2d.isKinematic.ToString() + ")");

            AxiStatistics_Node_Component _com = new AxiStatistics_Node_Component();
            _com.type = typeof(Rigidbody2D).ToString();
            _com.isKinematic = rig2d.isKinematic;
            _com.simulated = rig2d.simulated;
            AddComponentData(_type, _rootPath, _com, nodepath);
        }
        return true;
    }

    [MenuItem("Axibug移植工具/Statistics/[1]统计所有预制体和场景下的Collider和RigBody")]

    public static void StatisticsCollider()
    {
        ClearTempData();
        StatisticsCollider<BoxCollider2D>(CheckCom);
        StatisticsCollider<Rigidbody2D>(CheckCom);

        AxiStatisticsCache cache = ScriptableObject.CreateInstance<AxiStatisticsCache>();
        foreach (var data in dictTempData)
            cache.caches.Add(data.Value);
        AssetDatabase.CreateAsset(cache, cachecfgPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }


    public static void StatisticsCollider<T>(Func<int, string, T, string, bool> motion) where T : Component
    {
        AxiProjectTools.GoTAxiProjectToolsSence();
        //ComType2GUID.Clear();
        string[] sceneGuids = AssetDatabase.FindAssets("t:scene");
        foreach (string guid in sceneGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            if (path.Contains(AxiProjectTools.toolSenceName))
                continue;

#if UNITY_4_6
			EditorApplication.OpenScene(path);
#else
            UnityEditor.SceneManagement.EditorSceneManager.OpenScene(path);
#endif

            // 创建一个列表来存储根节点
            List<GameObject> rootNodes = new List<GameObject>();

            // 遍历场景中的所有对象
            GameObject[] allObjects = FindObjectsOfType<GameObject>();
            foreach (GameObject obj in allObjects)
            {
                // 检查对象是否有父对象
                if (obj.transform.parent == null)
                {
                    // 如果没有父对象，则它是一个根节点
                    rootNodes.Add(obj);
                }
            }

            foreach (var node in rootNodes)
                LoopPrefabNode<T>(0, path, path, node, 0, motion);
        }


        string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab");
        foreach (string guid in prefabGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GetPrefab<T>(path, motion);
        }

        AxiProjectTools.GoTAxiProjectToolsSence();
        Debug.Log("<Color=#FFF333>处理完毕  统计所有预制体和场景下的" + typeof(T).FullName + "</color>");
    }


    static void GetPrefab<T>(string path, Func<int, string, T, string, bool> motion) where T : Component
    {
#if UNITY_4_6
		GameObject prefab = AssetDatabase.LoadAssetAtPath(path,typeof(GameObject)) as GameObject;
#else
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
#endif

        LoopPrefabNode<T>(1, path, path, prefab.gameObject, 0, motion);
    }

    static void LoopPrefabNode<T>(int _type, string _rootPath, string noderootPath, GameObject trans, int depth, Func<int, string, T, string, bool> motion) where T : Component
    {
        //		#if UNITY_2018_4_OR_NEWER
        string nodename = noderootPath + "/" + trans.name;
        GameObject prefabRoot = trans.gameObject;

        Component[] components = prefabRoot.GetComponents<Component>();
        for (int i = 0; i < components.Length; i++)
        {
            var com = components[i];

            if (com == null)
                continue;

            T comobj = com as T;
            if (comobj == null)
                continue;
            if (!motion.Invoke(_type, _rootPath, comobj, nodename))
                continue;
        }

        //遍历
        foreach (Transform child in trans.transform)
            LoopPrefabNode<T>(_type, _rootPath, nodename, child.gameObject, depth + 1, motion);
        //#else
        //		Debug.Log("低版本不要执行本函数");
        //#endif
    }



    [MenuItem("Axibug移植工具/Statistics/[2]通过记录，对Rigbody进行修补")]

    public static void RepairRigBodyByStatistics()
    {
        string CurrScenePath = string.Empty;
        AxiProjectTools.GoTAxiProjectToolsSence();
#if UNITY_4_6
		AxiStatisticsCache data = AssetDatabase.LoadAssetAtPath(cachecfgPath,typeof(AxiStatisticsCache)) as AxiStatisticsCache;
#else
        AxiStatisticsCache data = AssetDatabase.LoadAssetAtPath<AxiStatisticsCache>(cachecfgPath);
#endif
        string[] sceneGuids = AssetDatabase.FindAssets("t:scene");
        List<string> ScenePath = new List<string>();
        List<string> SceneName = new List<string>();
        foreach (string guid in sceneGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            if (path.Contains(AxiProjectTools.toolSenceName))
                continue;
            ScenePath.Add(path);
            SceneName.Add(Path.GetFileName(path));
        }

        string[] prefabGuids = AssetDatabase.FindAssets("t:prefab");
        List<string> prefabPath = new List<string>();
        List<string> prefabName = new List<string>();
        foreach (string guid in prefabGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            prefabPath.Add(path);
            prefabName.Add(Path.GetFileName(path));
        }

        foreach (var cache in data.caches.OrderBy(w => w.type))
        {
            //场景
            if (cache.type == 0)
            {
                #region 场景加载
                string targetName = Path.GetFileName(cache.FullPath);
                int Idx = SceneName.IndexOf(targetName);
                if (Idx < 0)
                {
                    Debug.LogError(targetName + "[Repair]找不到对应资源");
                    continue;
                }
                string targetpath = ScenePath[Idx];

                //保证场景切换
                if (!string.Equals(CurrScenePath, targetpath))
                {
#if UNITY_4_6
					EditorApplication.OpenScene(targetpath);
#else
                    UnityEditor.SceneManagement.EditorSceneManager.OpenScene(targetpath);
#endif
                }
                CurrScenePath = targetpath;
                #endregion

                int DirtyCount = 0;
                foreach (var node in cache.nodes)
                {
                    string targetNodePath = node.NodeFullPath.Substring(cache.FullPath.Length, node.NodeFullPath.Length - cache.FullPath.Length);

                    GameObject targetNodePathObj = GameObject.Find(targetNodePath);
                    if (targetNodePathObj == null)
                    {
                        Debug.LogError("[Repair]" + targetNodePath + "找不到对应节点");
                        continue;
                    }

                    foreach (var com in node.components)
                    {
                        if (RepairComponent(node.NodeFullPath, targetNodePathObj, com))
                        {
                            DirtyCount++;
                        }
                    }
                }
                Debug.Log($"[Repair][场景处理]{cache.FullPath}共{DirtyCount}个需要处理");
                if (DirtyCount > 0)
                {

                }
            }
            else if (cache.type == 1)
            {
                //string targetName = Path.GetFileName(cache.FullPath);
                //int Idx = SceneName.IndexOf(targetName);
                //if (Idx < 0)
                //{
                //    Debug.LogError(targetName + "[Repair]找不到对应资源");
                //    continue;
                //}
                string targetpath = cache.FullPath;

                //来到空场景
                if (!string.IsNullOrEmpty(CurrScenePath))
                { 
                    AxiProjectTools.GoTAxiProjectToolsSence();
                    CurrScenePath = string.Empty;
                }


                GameObject prefabInstance = AssetDatabase.LoadAssetAtPath<GameObject>(targetpath);
                if (prefabInstance == null)
                {
                    Debug.LogError($"[Repair]Failed to load prefab at path: {prefabPath}");
                    return;
                }

                var obj = GameObject.Instantiate(prefabInstance, null);


                int DirtyCount = 0;
                foreach (var node in cache.nodes)
                {
                    GameObject targetNodePathObj = null;
                    if (node.NodeFullPath == targetpath + "/" + Path.GetFileNameWithoutExtension(targetpath))
                    {
                        //预制体自己就是目标
                        targetNodePathObj = obj;
                    }
                    else
                    {
                        string targetNodePath = node.NodeFullPath.Substring(cache.FullPath.Length + prefabInstance.name.Length + 2, node.NodeFullPath.Length - cache.FullPath.Length - prefabInstance.name.Length - 2);
                        targetNodePathObj = obj.transform.Find(targetNodePath)?.gameObject;

                        if (targetNodePathObj == null)
                        {
                            Debug.LogError("[Repair]" + targetNodePath + "找不到对应节点");
                            continue;
                        }
                    }


                    foreach (var com in node.components)
                    {
                        if (RepairComponent(node.NodeFullPath, targetNodePathObj, com))
                        {
                            DirtyCount++;
                        }
                    }
                }

                Debug.Log($"[Repair][预制体处理]{targetpath}共{DirtyCount}个需要处理");
                if (DirtyCount > 0)
                {
                    //PrefabUtility.SaveAsPrefabAsset(obj, targetpath);
                }

                GameObject.DestroyImmediate(obj);

            }

        }

        AxiProjectTools.GoTAxiProjectToolsSence();
    }

    static bool RepairComponent(string NodePath, GameObject targetNodePathObj, AxiStatistics_Node_Component comdata)
    {
        bool Dirty = false;
        if (comdata.type == typeof(Rigidbody2D).ToString())
        {
            Rigidbody2D rg2d = targetNodePathObj.GetComponent<Rigidbody2D>();
            if (rg2d.simulated != comdata.simulated)
            {
                Debug.Log($"[Repair]{NodePath}=> Rigidbody2D simulated:{rg2d.simulated} != :{comdata.simulated}  rg2d.bodyType => {rg2d.bodyType} ");
                Dirty = true;
            }
        }
        else if (comdata.type == typeof(BoxCollider2D).ToString())
        {
            BoxCollider2D bc = targetNodePathObj.GetComponent<BoxCollider2D>();
            if (bc.size != comdata.size)
            {
                Debug.Log($"[Repair]{NodePath} BoxCollider2D => size:{bc.size} != {comdata.size} ");
                Dirty = true;
            }
            if (bc.offset != comdata.center)
            {
                Debug.Log($"[Repair]{NodePath} BoxCollider2D => offset:{bc.offset} != center{comdata.center} ");
                Dirty = true;
            }
        }


        return Dirty;
    }
}
#endif