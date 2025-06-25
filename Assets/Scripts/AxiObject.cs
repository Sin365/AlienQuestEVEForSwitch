using UnityEngine;

public static class AxiObject
{
	public static GameObject Instantiate(GameObject src)
	{

#if UNITY_EDITOR
		Debug.Log($"AxiObject=>{src.name}");
#endif
		return Object.Instantiate(src);
	}

	//当前项目是H_SoundControl音频在调用
	public static GameObject Instantiate(string path, Vector3 position, Quaternion rotation)
	{
		GameObject src = Resources.Load<GameObject>(path);
		return (GameObject)Object.Instantiate((Object)src, position, rotation);
	}

	public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation) where T : Object
	{
		return (T)Object.Instantiate((Object)original, position, rotation);
	}
}
