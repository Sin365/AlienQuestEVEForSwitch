using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AxiSortingOrder���޸��Ͱ汾�����߰汾�Ĳ���
/// unity2017֮���ع���SortingOrder������userID�����ֵ����Ϊhash���ҹ̶�����Layer1,Layer2,Layer3...���򣬲�����������
/// �����ɴ��밴userID�ķ�ʽ����SortingOrder��
/// ��һ��ӳ���userIDӳ��LayerName����ͨ��SortingLayer.NameToID(layerName)�ҵ�Sortting��Hash������
/// �ﵽ���Ĵ����Ч��
/// </summary>
public static class AxiSortingOrder
{
	// ����YAML���ݹ�����userID�����Ƶ�ӳ���
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
	/// ����userID��ȡSorting Layer����
	/// </summary>
	public static string GetNameByUserID(int userID)
	{
		if (_userIDToNameMap.TryGetValue(userID, out string name))
		{
			return name;
		}
		Debug.LogError($"δ֪��userID: {userID}");
		return "Default"; // Ĭ�ϻ���
	}

	/// <summary>
	/// ����userID��ȡSorting Layer�Ĺ�ϣID����Unity��SortingLayerID��
	/// </summary>
	public static int GetHashIDByUserID(int userID)
	{
		if (userID == 0)
			return 0;
		string layerName = GetNameByUserID(userID);
		int hashID = SortingLayer.NameToID(layerName);

		if (hashID == 0) // Unity����0��ʾδ�ҵ�
		{
			Debug.LogError($"SortingLayer���Ʋ�����: {layerName}");
		}
		return hashID;
	}
}
