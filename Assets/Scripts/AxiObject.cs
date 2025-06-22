using UnityEngine;

public static class AxiObject
{
	public static GameObject Instantiate(GameObject src)
	{
		Debug.Log($"AxiObject=>{src.name}");
		return Object.Instantiate(src);
	}
	public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation) where T : Object
	{
		return (T)Object.Instantiate((Object)original, position, rotation);
	}
}
