﻿#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class AxiProjectTools : EditorWindow
{
	static string cachecfgPath = "Assets/AxiComToolCache.asset";
	public static string toolSenceName = "AxiProjectTools";
	public static string outCsDir = Application.dataPath + "/AxiCom/";
	public static Dictionary<string, AxiPrefabCache_Com2GUID> ComType2GUID = new Dictionary<string, AxiPrefabCache_Com2GUID>();
	
	public static void GoTAxiProjectToolsSence()
	{
		string[] sceneGuids = AssetDatabase.FindAssets("t:scene");
		foreach (string guid in sceneGuids)
		{
			string path = AssetDatabase.GUIDToAssetPath(guid);
			if (path.Contains(toolSenceName))
			{
				#if UNITY_4_6
				EditorApplication.OpenScene(path);
				#else
				UnityEditor.SceneManagement.EditorSceneManager.OpenScene(path);
				#endif
				return;
			}
		}
	}
	
	[MenuItem("Axibug移植工具/ToLowVersionUnity/[1]采集所有预制体和场景下的UGUI组件")]
	public static void Part1()
	{
		GoTAxiProjectToolsSence();
		ComType2GUID.Clear();
		string[] sceneGuids = AssetDatabase.FindAssets("t:scene");
		foreach (string guid in sceneGuids)
		{
			string path = AssetDatabase.GUIDToAssetPath(guid);
			if (path.Contains(toolSenceName))
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
				LoopPrefabNode(path, node, 0);
		}
		
		
		string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab");
		foreach (string guid in prefabGuids)
		{
			string path = AssetDatabase.GUIDToAssetPath(guid);
			GetPrefab(path);
		}
		
		AxiPrefabCache cache = ScriptableObject.CreateInstance<AxiPrefabCache>();
		foreach (var data in ComType2GUID)
			cache.caches.Add(data.Value);
		AssetDatabase.CreateAsset(cache, cachecfgPath);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		GoTAxiProjectToolsSence();
		Debug.Log("<Color=#FFF333>处理完毕  [1]采集所有预制体和场景下的UGUI组件</color>");
	}
	
	static void GetPrefab(string path)
	{		
		#if UNITY_4_6
		GameObject prefab = AssetDatabase.LoadAssetAtPath(path,typeof(GameObject)) as GameObject;
		#else
		GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
		#endif
		
		LoopPrefabNode(path, prefab.gameObject, 0);
	}
	static void LoopPrefabNode(string rootPath, GameObject trans, int depth)
	{
		//		#if UNITY_2018_4_OR_NEWER
		string nodename = rootPath + trans.name;
		GameObject prefabRoot = trans.gameObject;
		
		Component[] components = prefabRoot.GetComponents<Component>();
		for (int i = 0; i < components.Length; i++)
		{
			var com = components[i];
			
			if (com == null)
				continue;
			
			MonoBehaviour monoCom = com as MonoBehaviour;
			if (monoCom == null)
				continue;
			Type monoType = monoCom.GetType();
			if (!monoType.Assembly.FullName.Contains("UnityEngine.UI"))
				continue;
			// 获取MonoScript资源
			MonoScript monoScript = MonoScript.FromMonoBehaviour(monoCom);
			if (monoScript != null)
			{
				// 获取MonoScript资源的GUID
				string guid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(monoScript));
				Debug.Log(nodename+"	|	<color=#FFF333>["+monoType.Name+"]</color> <color=#FF0000>"+guid+"</color><color=#00FF00>("+monoType.FullName+")</color>");
				ComType2GUID[monoType.FullName] =
					new AxiPrefabCache_Com2GUID()
				{
					SrcFullName = monoType.FullName,
					SrcName = monoType.Name,
					GUID = guid,
				};
			}
			else
			{
				Debug.LogError("!!!! 没得");
			}
		}
		
		//遍历
		foreach (Transform child in trans.transform)
			LoopPrefabNode(nodename, child.gameObject, depth + 1);
		//#else
		//		Debug.Log("低版本不要执行本函数");
		//#endif
	}
	
	[MenuItem("Axibug移植工具/ToLowVersionUnity/[2]生成中间脚本代码")]
	public static void Part2()
	{
		
		#if UNITY_4_6
		if(System.IO.Directory.Exists(outCsDir))
			System.IO.Directory.Delete(outCsDir);
		#else
		if (UnityEngine.Windows.Directory.Exists(outCsDir))
			UnityEngine.Windows.Directory.Delete(outCsDir);
		#endif
		
		
		Directory.CreateDirectory(outCsDir);
		
		
		
		#if UNITY_4_6
		AxiPrefabCache cache = AssetDatabase.LoadAssetAtPath(cachecfgPath,typeof(AxiPrefabCache)) as AxiPrefabCache;
		#else
		AxiPrefabCache cache = AssetDatabase.LoadAssetAtPath<AxiPrefabCache>(cachecfgPath);
		#endif
		foreach (var data in cache.caches)
		{
			string toName = "Axi" + data.SrcName;
			string toPath = outCsDir + toName + ".cs";
			string codeStr = "namespace AxibugCom { public class " + toName + " : " + data.SrcFullName + " {} }";
			try
			{
				System.IO.File.WriteAllText(toPath, codeStr);
				data.ToName = toName;
				data.ToPATH = toPath;
			}
			catch (Exception ex)
			{
				Debug.LogError("写入失败" + ex.ToString());
			}
		}
		Debug.Log("写入完毕");
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		Debug.Log("<Color=#FFF333>处理完毕  [2]生成中间脚本代码</color>");
	}
	
	[MenuItem("Axibug移植工具/ToLowVersionUnity/[3]收集生成的脚本")]
	public static void Part3()
	{
		#if UNITY_4_6
		AxiPrefabCache cache = AssetDatabase.LoadAssetAtPath(cachecfgPath,typeof(AxiPrefabCache)) as AxiPrefabCache;
		MonoScript[] allMonoScripts = (MonoScript[])Resources.FindObjectsOfTypeAll(typeof(MonoScript));
		#else
		AxiPrefabCache cache = AssetDatabase.LoadAssetAtPath<AxiPrefabCache>(cachecfgPath);
		List<MonoScript> allMonoScripts = FindAllAssetsOfType<MonoScript>();
		#endif
		
		foreach (var data in cache.caches)
		{
			MonoScript monoScript = allMonoScripts.FirstOrDefault(w => w.name == data.ToName);
			if (monoScript == null)
			{
				Debug.LogError("没找到" + data.ToName);
				continue;
			}
			string guid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(monoScript));
			data.ToGUID = guid;
			data.monoScript = monoScript;
		}
		Debug.Log("写入完毕");
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		Debug.Log("<Color=#FFF333>处理完毕  [3]收集生成的脚本</color>");
	}
	
	static List<T> FindAllAssetsOfType<T>() where T : UnityEngine.Object
	{
		List<T> assets = new List<T>();
		
		string[] allGuids = AssetDatabase.FindAssets("");
		foreach (string guid in allGuids)
		{
			string path = AssetDatabase.GUIDToAssetPath(guid);
			if (path.EndsWith(".cs") || path.EndsWith(".js") || path.EndsWith(".boo")) // Unity支持多种脚本语言，但现代Unity主要使用C#
			{
				#if UNITY_4_6
				T asset = AssetDatabase.LoadAssetAtPath(cachecfgPath,typeof(T)) as T;
				#else
				T asset = AssetDatabase.LoadAssetAtPath<T>(path);
				#endif
				
				if (asset != null)
				{
					assets.Add(asset);
				}
			}
		}
		return assets;
	}
	
	
	[MenuItem("Axibug移植工具/ToLowVersionUnity/[4]替换所有预制体和场景中的组件")]
	public static void Part4()
	{
		
		#if UNITY_4_6
		AxiPrefabCache cache = AssetDatabase.LoadAssetAtPath(cachecfgPath,typeof(AxiPrefabCache)) as AxiPrefabCache;
		#else
		AxiPrefabCache cache = AssetDatabase.LoadAssetAtPath<AxiPrefabCache>(cachecfgPath);
		#endif
		
		Dictionary<string, string> tempReplaceDict = new Dictionary<string, string>();
		foreach (var data in cache.caches)
		{
			tempReplaceDict[data.GUID] = data.ToGUID;
		}
		ProcessAllPrefabs("*.prefab", tempReplaceDict);
		ProcessAllPrefabs("*.unity", tempReplaceDict);
		ProcessAllPrefabs("*.anim", tempReplaceDict);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		Debug.Log("<Color=#FFF333>处理完毕  [4]替换所有预制体和场景中的组件</color>");
	}
	
	static void ProcessAllPrefabs(string form, Dictionary<string, string> tempReplaceDict, bool reverse = false)
	{
		List<GameObject> prefabs = new List<GameObject>();
		var resourcesPath = Application.dataPath;
		var absolutePaths = Directory.GetFiles(resourcesPath, form, SearchOption.AllDirectories);
		for (int i = 0; i < absolutePaths.Length; i++)
		{
			Debug.Log("prefab name: " + absolutePaths[i]);
			foreach (var VARIABLE in tempReplaceDict)
			{
				string oldValue = reverse ? VARIABLE.Value : VARIABLE.Key;
				string newValue = reverse ? VARIABLE.Key : VARIABLE.Value;
				ReplaceValue(absolutePaths[i], oldValue, newValue);
			}
			EditorUtility.DisplayProgressBar("处理预制体……", "处理预制体中……", (float)i / absolutePaths.Length);
		}
		EditorUtility.ClearProgressBar();
	}
	
	/// <summary>
	/// 替换值
	/// </summary>
	/// <param name="strFilePath">文件路径</param>
	static void ReplaceValue(string strFilePath, string oldLine, string newLine)
	{
		if (File.Exists(strFilePath))
		{
			string[] lines = File.ReadAllLines(strFilePath);
			for (int i = 0; i < lines.Length; i++)
			{
				lines[i] = lines[i].Replace(oldLine, newLine);
			}
			File.WriteAllLines(strFilePath, lines);
		}
	}
	
	
	[MenuItem("Axibug移植工具/ToLowVersionUnity/[5]UnPack所有嵌套预制体和场景中的预制体")]
	public static void UnpackPrefabs()
	{
		
		#if UNITY_2018_4_OR_NEWER
		GoTAxiProjectToolsSence();
		string[] allAssetPaths = AssetDatabase.GetAllAssetPaths();
		int prefabCount = 0;
		
		foreach (string path in allAssetPaths)
		{
			if (Path.GetExtension(path).Equals(".prefab"))
			{
				Debug.Log($"Unpacking {path}");
				UnpackPrefab(path);
				prefabCount++;
			}
		}
		Debug.Log($"{prefabCount}个预制体Unpack");
		
		string[] sceneGuids = AssetDatabase.FindAssets("t:scene");
		foreach (string guid in sceneGuids)
		{
			string path = AssetDatabase.GUIDToAssetPath(guid);
			if (path.Contains(toolSenceName))
				continue;
			
			UnityEditor.SceneManagement.EditorSceneManager.OpenScene(path); 
			UnityEngine.SceneManagement.Scene currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
			GameObject[] rootObjects = currentScene.GetRootGameObjects();
			foreach (GameObject rootObj in rootObjects)
			{
				// 遍历场景中的所有对象
				TraverseHierarchy(rootObj);
			}
			// Save the scene // 获取当前打开的场景
			currentScene = UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene();
			// 保存场景到文件（默认路径和名称）
			bool success = UnityEditor.SceneManagement.EditorSceneManager.SaveScene(currentScene, currentScene.path);
			
			Debug.Log($"{currentScene.name}场景中 所有物体Unpack");
		}
		
		GoTAxiProjectToolsSence();
		Debug.Log("<Color=#FFF333>处理完毕  [5]UnPack所有预制体</color>");
		#else
		Debug.Log("低版本不要执行本函数");
		#endif
	}
	
	static void UnpackPrefab(string prefabPath)
	{
		#if UNITY_2018_4_OR_NEWER
		GameObject prefabInstance = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
		if (prefabInstance == null)
		{
			Debug.LogError($"Failed to load prefab at path: {prefabPath}");
			return;
		}
		
		var obj = GameObject.Instantiate(prefabInstance, null);
		TraverseHierarchy(obj);
		PrefabUtility.SaveAsPrefabAsset(obj, prefabPath);
		GameObject.DestroyImmediate(obj);
		#else
		Debug.Log("低版本不要执行本函数");
		#endif
	}
	
	static void TraverseHierarchy(GameObject obj)
	{
		#if UNITY_2018_4_OR_NEWER
		// 检查该对象是否是预制体的实例
		if (PrefabUtility.IsPartOfPrefabInstance(obj))
		{
			// 将预制体实例转换为普通游戏对象
			PrefabUtility.UnpackPrefabInstance(obj, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
			Debug.Log("Prefab instance converted to game object: " + obj.name);
		}
		
		// 递归遍历子对象
		for (int i = 0; i < obj.transform.childCount; i++)
		{
			TraverseHierarchy(obj.transform.GetChild(i).gameObject);
		}
		#else
		Debug.Log("低版本不要执行本函数");
		#endif
	}
	
	
	
	
}
#endif