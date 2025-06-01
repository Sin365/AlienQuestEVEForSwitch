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
	static Dictionary<string,AxiStatisticsDatas> dictTempData = new Dictionary<string,AxiStatisticsDatas>();

	static void ClearTempData()
	{
		dictTempData.Clear();
	}
	static string GetRootTempKey(int type,string rootName)
	{
		return type + "_" + rootName;
	}
	
	static void AddComponentData(int _type,string _rootPath,AxiStatistics_Node_Component _com,string _nodepath)
	{
		string rootKey = GetRootTempKey(_type, _rootPath);
		
		if (!dictTempData.ContainsKey(rootKey)) {
			dictTempData[rootKey] = new AxiStatisticsDatas(){type = _type,FullPath = _rootPath,nodes = new List<AxiStatistics_Node>()};
		}

		AxiStatisticsDatas rootData = dictTempData[rootKey];

		AxiStatistics_Node nodeData = rootData.nodes.Where(w => w.NodeFullPath == _nodepath).FirstOrDefault();
		if (nodeData == null) {
			nodeData = new AxiStatistics_Node();
			nodeData.Name = Path.GetFileName(_nodepath);
			nodeData.NodeFullPath = _nodepath;
			nodeData.components = new List<AxiStatistics_Node_Component>();
			rootData.nodes.Add(nodeData);
		}

		nodeData.components.Add(_com);
	}

	
	static bool CheckCom(int _type,string _rootPath,Component com,string nodepath)
	{
		if (com is BoxCollider2D) {
			BoxCollider2D bc = com as BoxCollider2D;
#if UNITY_2017_1_OR_NEWER
			Debug.Log(nodepath +"BoxCollider2D->center=>("+ bc.offset.x+","+bc.offset.y+") size=>("+ bc.size.x+","+bc.size.y+"");
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
			AddComponentData(_type,_rootPath,_com,nodepath);
		}
		if (com is Rigidbody2D) {
			Rigidbody2D rig2d = com as Rigidbody2D;
			Debug.Log(_rootPath +"Rigidbody2D->simulated=>("+ rig2d.simulated+")");
			Debug.Log(_rootPath +"Rigidbody2D->IsSleeping=>("+ rig2d.isKinematic.ToString()+")");
			
			AxiStatistics_Node_Component _com = new AxiStatistics_Node_Component();
			_com.type = typeof(Rigidbody2D).ToString();
			_com.isKinematic = rig2d.isKinematic;
			_com.simulated = rig2d.simulated;
			AddComponentData(_type,_rootPath,_com,nodepath);
		}
		return true;
	}

	[MenuItem("Axibug移植工具/Statistics/统计所有预制体和场景下的Collider")]

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


	public static void StatisticsCollider<T>(Func<int,string,T,string,bool> motion) where T : Component
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
				LoopPrefabNode<T>(0,path,path, node, 0,motion);
		}
		
		
		string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab");
		foreach (string guid in prefabGuids)
		{
			string path = AssetDatabase.GUIDToAssetPath(guid);
			GetPrefab<T>(path,motion);
		}

		AxiProjectTools.GoTAxiProjectToolsSence();
		Debug.Log("<Color=#FFF333>处理完毕  统计所有预制体和场景下的"+typeof(T).FullName+"</color>");
	}


	static void GetPrefab<T>(string path,Func<int,string,T,string,bool> motion) where T : Component
	{		
#if UNITY_4_6
		GameObject prefab = AssetDatabase.LoadAssetAtPath(path,typeof(GameObject)) as GameObject;
#else
		GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
#endif

		LoopPrefabNode<T>(1,path,path, prefab.gameObject, 0,motion);
	}

	static void LoopPrefabNode<T>(int _type,string _rootPath,string noderootPath, GameObject trans, int depth,Func<int,string,T,string,bool> motion) where T : Component
	{
		//		#if UNITY_2018_4_OR_NEWER
		string nodename = noderootPath +"/"+ trans.name;
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
				if(!motion.Invoke(_type,_rootPath,comobj,nodename))
					continue;
			}
			
			//遍历
			foreach (Transform child in trans.transform)
				LoopPrefabNode<T>(_type,_rootPath,nodename, child.gameObject, depth + 1,motion);
			//#else
			//		Debug.Log("低版本不要执行本函数");
			//#endif
		}

}
#endif