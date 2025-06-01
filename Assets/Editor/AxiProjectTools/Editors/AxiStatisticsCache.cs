#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class AxiStatisticsCache : ScriptableObject
{
	public List<AxiStatisticsDatas> caches = new List<AxiStatisticsDatas>();
}

[Serializable]
public class AxiStatisticsDatas
{
	/// <summary>
	/// [0]Sence [1]Prefab
	/// </summary>
	public int type;
	public string FullPath;
	public List<AxiStatistics_Node> nodes = new List<AxiStatistics_Node>();
}

[Serializable]
public class AxiStatistics_Node
{
	public string Name;
	public string NodeFullPath;
	public List<AxiStatistics_Node_Component> components = new List<AxiStatistics_Node_Component>();
}

[Serializable]
public class AxiStatistics_Node_Component
{
    public string type;
	//Rigboody
	public bool simulated;
	public bool isKinematic;
	//BoxCollider2D
	public Vector2 center;
	public Vector2 size;
}

#endif