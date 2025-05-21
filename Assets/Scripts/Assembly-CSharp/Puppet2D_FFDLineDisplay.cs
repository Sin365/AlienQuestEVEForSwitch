using UnityEngine;
using System.Collections.Generic;

public class Puppet2D_FFDLineDisplay : MonoBehaviour
{
	public Transform target;
	public Transform target2;
	public SkinnedMeshRenderer skinnedMesh;
	public SkinnedMeshRenderer outputSkinnedMesh;
	public int vertNumber;
	public List<Transform> bones;
	public List<float> weight;
	public List<Vector3> delta;
}
