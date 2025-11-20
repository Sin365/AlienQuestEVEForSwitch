#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public partial class AxiProjectTools : EditorWindow
{
    private enum ESteps
    {
        None,
        Prepare,
        CollectAllMaterial,
        CollectVariants,
        CollectSleeping,
        WaitingDone,
    }
    private static ESteps _steps = ESteps.None;
    private static List<string> _allMaterials;
    private static int _processMaxNum = 1000;
    private const float WaitMilliseconds = 1000f;
    private const float SleepMilliseconds = 100f;
    private static Stopwatch _elapsedTime;
    private static string savePath
    {
        get
        {
            if (bIsForReSource)
                return "Assets/Resources/Axibug/Shaders/ShaderCollection/unity_collected.shadervariants";
            else
                return "Assets/GameAssets/Axibug/Shaders/ShaderCollection/unity_collected.shadervariants";
        }
    }
    private static List<GameObject> _allSpheres = new List<GameObject>(1000);
    private static Action _completedCallback;
    private static bool bIsForReSource;
    [MenuItem("Axibug移植工具/Shader变体收集(Resource)")]

    static void ShaderVariantCollectorByResources()
    {
        ShaderVariantCollector(true, null);
    }

    static void ShaderVariantCollector(bool forResource,Action completedCallback)
    {
        if (_steps != ESteps.None)
            return;

        //if (Path.HasExtension(savePath) == false)
        //    savePath = $"{savePath}.shadervariants";
        if (Path.GetExtension(savePath) != ".shadervariants")
            throw new System.Exception("Shader variant file extension is invalid.");

        bIsForReSource = forResource;
        // 注意：先删除再保存，否则ShaderVariantCollection内容将无法及时刷新
        AssetDatabase.DeleteAsset(savePath);
        EditorTools.CreateFileDirectory(savePath);

        _completedCallback = completedCallback;
        // 聚焦到游戏窗口
        EditorTools.FocusUnityGameWindow();
        //创建临时创景
        EditorTools.CreateTempScene();

        _steps = ESteps.Prepare;
        EditorApplication.update += EditorUpdate;
    }

    private static void EditorUpdate()
    {
        if (_steps == ESteps.None)
            return;

        if (_steps == ESteps.Prepare)
        {
            ShaderVariantCollectionHelper.ClearCurrentShaderVariantCollection();
            _steps = ESteps.CollectAllMaterial;
            return; //等待一帧
        }

        if (_steps == ESteps.CollectAllMaterial)
        {
            if (bIsForReSource)
            {
                _allMaterials = GetAllMaterialsByResourceAndDep();
            }
            else
            {
                //未实现
                _steps = ESteps.None;
                throw new NotImplementedException();
            }
            _steps = ESteps.CollectVariants;
            return; //等待一帧
        }

        if (_steps == ESteps.CollectVariants)
        {
            int count = Mathf.Min(_processMaxNum, _allMaterials.Count);
            List<string> range = _allMaterials.GetRange(0, count);
            _allMaterials.RemoveRange(0, count);
            CollectVariants(range);

            if (_allMaterials.Count > 0)
            {
                _elapsedTime = Stopwatch.StartNew();
                _steps = ESteps.CollectSleeping;
            }
            else
            {
                _elapsedTime = Stopwatch.StartNew();
                _steps = ESteps.WaitingDone;
            }
        }


        if (_steps == ESteps.CollectSleeping)
        {
            if (_elapsedTime.ElapsedMilliseconds > SleepMilliseconds)
            {
                DestroyAllSpheres();
                _elapsedTime.Stop();
                _steps = ESteps.CollectVariants;
            }
        }

        if (_steps == ESteps.WaitingDone)
        {
            // 注意：一定要延迟保存才会起效
            if (_elapsedTime.ElapsedMilliseconds > WaitMilliseconds)
            {
                _elapsedTime.Stop();
                _steps = ESteps.None;

                ShaderVariantCollection svc = AssetDatabase.LoadAssetAtPath<ShaderVariantCollection>(savePath);
                if (svc == null) EditorTools.CreateFileDirectory(savePath);
                // 保存结果并创建清单
                ShaderVariantCollectionHelper.SaveCurrentShaderVariantCollection(savePath);

                UnityEngine.Debug.Log($"搜集SVC完毕！收集到shader {ShaderVariantCollectionHelper.GetCurrentShaderVariantCollectionShaderCount()}  变体 {ShaderVariantCollectionHelper.GetCurrentShaderVariantCollectionVariantCount()}");
                EditorApplication.update -= EditorUpdate;

                //如果有Assets地方需要引用挂，把这个赋过去
                //AssetDatabase.LoadAssetAtPath<ShaderVariantCollection>(savePath);

                _completedCallback?.Invoke();
            }
        }
    }

    private static List<string> GetAllMaterialsByResourceAndDep()
    {
        int progressValue = 0;
        EditorTools.ClearProgressBar();
        // 搜集所有材质球
        progressValue = 0;
        List<string> allMaterial = new List<string>(1000);


        ///////////////////

        string[] allResGuid = AssetDatabase.FindAssets("", new[] { "Assets/Resources/" });
        HashSet<string> needCheckAssetPaths = new HashSet<string>();
        int totalDependencies = 0;
        foreach (string guid in allResGuid)
        {
            EditorTools.DisplayProgressBar("收集resource中的所有资源", ++progressValue, allResGuid.Count());
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            // 跳过meta文件和非资源文件
            if (assetPath.EndsWith(".meta") || IsNonResourceFile(assetPath))
                continue;

            if (!needCheckAssetPaths.Contains(assetPath)) 
                needCheckAssetPaths.Add(assetPath);

            //所有依赖
            string[] deparr = AssetDatabase.GetDependencies(assetPath);
            List<string> vaildDep = new List<string>();
            foreach (string dep in deparr)
            {
                if (dep == assetPath || dep.Contains("Assets/Resources") || dep.EndsWith(".meta") || IsNonResourceFile(assetPath))
                    continue;
                //TODO 也要跳过本来就是resource里的

                if (!needCheckAssetPaths.Contains(dep)) 
                    needCheckAssetPaths.Add(dep);
            }
        }
        EditorTools.ClearProgressBar();
        ///////////////////


        // 搜集所有材质球
        progressValue = 0;
        foreach (var assetPath in needCheckAssetPaths)
        {
            System.Type assetType = AssetDatabase.GetMainAssetTypeAtPath(assetPath);
            if (assetType == typeof(UnityEngine.Material))
            {
                allMaterial.Add(assetPath);
            }
            EditorTools.DisplayProgressBar("搜集所有材质球", ++progressValue, needCheckAssetPaths.Count);
        }
        EditorTools.ClearProgressBar();

        // 返回结果
        return allMaterial;
    }
    private static bool IsNonResourceFile(string path)
    {
        return path.EndsWith(".cs") || path.EndsWith(".js") || path.EndsWith(".shader");
    }


    private static void CollectVariants(List<string> materials)
    {
        Camera camera = Camera.main;
        if (camera == null)
            throw new System.Exception("Not found main camera.");

        // 设置主相机
        float aspect = camera.aspect;
        int totalMaterials = materials.Count;
        float height = Mathf.Sqrt(totalMaterials / aspect) + 1;
        float width = Mathf.Sqrt(totalMaterials / aspect) * aspect + 1;
        float halfHeight = Mathf.CeilToInt(height / 2f);
        float halfWidth = Mathf.CeilToInt(width / 2f);
        camera.orthographic = true;
        camera.orthographicSize = halfHeight;
        camera.transform.position = new Vector3(0f, 0f, -10f);

        // 创建测试球体
        int xMax = (int)(width - 1);
        int x = 0, y = 0;
        int progressValue = 0;
        for (int i = 0; i < materials.Count; i++)
        {
            var material = materials[i];
            var position = new Vector3(x - halfWidth + 1f, y - halfHeight + 1f, 0f);
            var go = CreateSphere(material, position, i);
            if (go != null)
                _allSpheres.Add(go);
            if (x == xMax)
            {
                x = 0;
                y++;
            }
            else
            {
                x++;
            }
            EditorTools.DisplayProgressBar("照射所有材质球", ++progressValue, materials.Count);
        }
        EditorTools.ClearProgressBar();
    }

    private static GameObject CreateSphere(string assetPath, Vector3 position, int index)
    {
        var material = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<Material>(assetPath));
        var shader = material.shader;
        if (shader == null)
            return null;
        //设置材质单面渲染
        if (material.HasProperty("_Cull") && material.GetInt("_Cull") == (int)UnityEngine.Rendering.CullMode.Off)
        {
            // 将 Cull 参数设置为 Off
            //material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Back);
            UnityEngine.Debug.LogError($"材质启用了双面渲染，请美术同学注意 {material.name}");
            //EditorUtility.SetDirty(material);
            //AssetDatabase.SaveAssets();
        }
        var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        go.GetComponent<Renderer>().material = material;
        go.transform.position = position;
        go.name = $"Sphere_{index} | {material.name}";
        if (shader.name.Contains("RoleShader"))
        {
            material.EnableKeyword("_ALPHATEST_ON");
        }
        return go;
    }

    private static void DestroyAllSpheres()
    {
        foreach (var go in _allSpheres)
        {
            GameObject.DestroyImmediate(go);
        }
        _allSpheres.Clear();

        // 尝试释放编辑器加载的资源
        EditorUtility.UnloadUnusedAssetsImmediate(true);
    }
}

public static class ShaderVariantCollectionHelper
{
    public static void ClearCurrentShaderVariantCollection()
    {
        EditorTools.InvokeNonPublicStaticMethod(typeof(ShaderUtil), "ClearCurrentShaderVariantCollection");
    }
    public static void SaveCurrentShaderVariantCollection(string savePath)
    {
        EditorTools.InvokeNonPublicStaticMethod(typeof(ShaderUtil), "SaveCurrentShaderVariantCollection", savePath);
    }
    public static int GetCurrentShaderVariantCollectionShaderCount()
    {
        return (int)EditorTools.InvokeNonPublicStaticMethod(typeof(ShaderUtil), "GetCurrentShaderVariantCollectionShaderCount");
    }
    public static int GetCurrentShaderVariantCollectionVariantCount()
    {
        return (int)EditorTools.InvokeNonPublicStaticMethod(typeof(ShaderUtil), "GetCurrentShaderVariantCollectionVariantCount");
    }

    /// <summary>
    /// 获取着色器的变种总数量
    /// </summary>
    public static string GetShaderVariantCount(string assetPath)
    {
        Shader shader = AssetDatabase.LoadAssetAtPath<Shader>(assetPath);
        var variantCount = EditorTools.InvokeNonPublicStaticMethod(typeof(ShaderUtil), "GetVariantCount", shader, true);
        return variantCount.ToString();
    }
}
#endif