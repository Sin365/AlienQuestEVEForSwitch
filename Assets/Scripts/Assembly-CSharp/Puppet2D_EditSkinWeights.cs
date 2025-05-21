[global::UnityEngine.ExecuteInEditMode]
public class Puppet2D_EditSkinWeights : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject Bone0;

	public global::UnityEngine.GameObject Bone1;

	public global::UnityEngine.GameObject Bone2;

	public global::UnityEngine.GameObject Bone3;

	public int boneIndex0;

	public int boneIndex1;

	public int boneIndex2;

	public int boneIndex3;

	public float Weight0;

	public float Weight1;

	public float Weight2;

	public float Weight3;

	public global::UnityEngine.Mesh mesh;

	public global::UnityEngine.SkinnedMeshRenderer meshRenderer;

	public int vertNumber;

	private global::UnityEngine.GameObject[] handles;

	public global::UnityEngine.Vector3[] verts;

	private static global::UnityEngine.Mesh skinnedMesh;

	public bool autoUpdate;

	private void Update()
	{
		Refresh();
	}

	public void Refresh()
	{
		if ((bool)base.transform.parent && (bool)base.transform.parent.GetComponent<global::UnityEngine.SkinnedMeshRenderer>())
		{
			meshRenderer = base.transform.parent.GetComponent<global::UnityEngine.SkinnedMeshRenderer>();
		}
		global::UnityEngine.BoneWeight[] boneWeights = mesh.boneWeights;
		if ((bool)Bone0)
		{
			boneWeights[vertNumber].boneIndex0 = boneIndex0;
		}
		if ((bool)Bone1)
		{
			boneWeights[vertNumber].boneIndex1 = boneIndex1;
		}
		if ((bool)Bone2)
		{
			boneWeights[vertNumber].boneIndex2 = boneIndex2;
		}
		if ((bool)Bone3)
		{
			boneWeights[vertNumber].boneIndex3 = boneIndex3;
		}
		boneWeights[vertNumber].weight0 = Weight0;
		boneWeights[vertNumber].weight1 = Weight1;
		boneWeights[vertNumber].weight2 = Weight2;
		boneWeights[vertNumber].weight3 = Weight3;
		mesh.boneWeights = boneWeights;
		if (meshRenderer != null)
		{
			meshRenderer.sharedMesh = mesh;
		}
	}
}
