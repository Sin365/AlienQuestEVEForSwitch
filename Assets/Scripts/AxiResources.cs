using UnityEngine;
using System.Collections.Generic;

public static class AxiResources
{
	static Dictionary<string, Sprite[]> dictSpriteArrCache = new Dictionary<string, Sprite[]>();
	public static Sprite[] LoadAllSprite(string Name)
	{
		if (!dictSpriteArrCache.ContainsKey(Name))
		{
			dictSpriteArrCache[Name] = Resources.LoadAll<global::UnityEngine.Sprite>(Name);
		}
		return dictSpriteArrCache[Name];
	}
}
