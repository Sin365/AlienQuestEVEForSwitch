using UnityEngine;

public class Puppet2D_EditSkinWeights : MonoBehaviour
{
	public GameObject Bone0;
	public GameObject Bone1;
	public GameObject Bone2;
	public GameObject Bone3;
	public int boneIndex0;
	public int boneIndex1;
	public int boneIndex2;
	public int boneIndex3;
	public float Weight0;
	public float Weight1;
	public float Weight2;
	public float Weight3;
	public Mesh mesh;
	public SkinnedMeshRenderer meshRenderer;
	public int vertNumber;
	public Vector3[] verts;
	public bool autoUpdate;
}
