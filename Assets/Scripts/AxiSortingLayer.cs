using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AxiSortingOrder，修复低版本升级高版本的操作
/// unity2017之后重构，SortingOrder，抛弃userID概念，数值传递为hash，且固定按照Layer1,Layer2,Layer3...升序，不再任意数据
/// 则保留旧代码按userID的方式设置SortingOrder，
/// 做一个映射表，userID映射LayerName，在通过SortingLayer.NameToID(layerName)找到Sortting的Hash来设置
/// 达到不改代码的效果
/// </summary>
public static class AxiSortingOrder
{
	// 根据YAML数据构建的userID到名称的映射表
	private static readonly Dictionary<int, string> _userIDToNameMap = new Dictionary<int, string>()
	{
		{0, "Default"},
		{21, "Boss"},
		{23, "Keeper"},
		{26, "Lv_1"},
		{25, "Alien"},
		{6, "Lv_2"},
		{8, "Egg"},
		{5, "Elite"},
		{15, "Queen"},
		{10, "Player"},
		{12, "Lv_3"},
		{2, "Down"},
		{3, "Creep"},
		{13, "Fly_1"},
		{22, "Fly_2"},
		{14, "H_Scene"},
		{19, "Object_Over"},
		{17, "Effect"},
		{18, "Item"},
		{20, "GUI"},
		{1, "Bone"}
	};

	/// <summary>
	/// 根据userID获取Sorting Layer名称
	/// </summary>
	public static string GetNameByUserID(int userID)
	{
		if (_userIDToNameMap.TryGetValue(userID, out string name))
		{
			return name;
		}
		Debug.LogError($"未知的userID: {userID}");
		return "Default"; // 默认回退
	}

	/// <summary>
	/// 根据userID获取Sorting Layer的哈希ID（即Unity的SortingLayerID）
	/// </summary>
	public static int GetHashIDByUserID(int userID)
	{
		if (userID == 0)
			return 0;
		string layerName = GetNameByUserID(userID);
		int hashID = SortingLayer.NameToID(layerName);

		if (hashID == 0) // Unity返回0表示未找到
		{
			Debug.LogError($"SortingLayer名称不存在: {layerName}");
		}
		return hashID;
	}
}
