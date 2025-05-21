[global::UnityEngine.ExecuteInEditMode]
public class Puppet2D_Bakedmesh : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.SkinnedMeshRenderer skin;

	private void Start()
	{
		skin = base.transform.GetComponent<global::UnityEngine.SkinnedMeshRenderer>();
	}

	private void Update()
	{
		if (!skin)
		{
			return;
		}
		global::UnityEngine.Mesh mesh = new global::UnityEngine.Mesh();
		skin.BakeMesh(mesh);
		int num = 0;
		foreach (global::UnityEngine.Transform item in base.transform)
		{
			if (!float.IsNaN(mesh.vertices[num].x))
			{
				item.localPosition = mesh.vertices[num];
			}
			else
			{
				global::UnityEngine.Debug.LogWarning("vertex " + num + " is corrupted");
			}
			num++;
		}
		global::UnityEngine.Object.DestroyImmediate(mesh);
	}
}
