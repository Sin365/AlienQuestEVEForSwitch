[global::UnityEngine.ExecuteInEditMode]
public class Puppet2D_VertHandler : global::UnityEngine.MonoBehaviour
{
	private global::UnityEngine.Mesh mesh;

	private global::UnityEngine.Vector3[] verts;

	private global::UnityEngine.Vector3 vertPos;

	private global::UnityEngine.GameObject[] handles;

	private void OnEnable()
	{
		mesh = GetComponent<global::UnityEngine.MeshFilter>().sharedMesh;
		verts = mesh.vertices;
		global::UnityEngine.Vector3[] array = verts;
		foreach (global::UnityEngine.Vector3 position in array)
		{
			vertPos = base.transform.TransformPoint(position);
			global::UnityEngine.GameObject gameObject = new global::UnityEngine.GameObject("handle");
			gameObject.transform.position = vertPos;
			gameObject.transform.parent = base.transform;
			gameObject.tag = "handle";
			global::UnityEngine.MonoBehaviour.print(vertPos);
		}
	}

	private void Update()
	{
		handles = global::UnityEngine.GameObject.FindGameObjectsWithTag("handle");
		for (int i = 0; i < verts.Length; i++)
		{
			verts[i] = handles[i].transform.localPosition;
		}
		mesh.vertices = verts;
		mesh.RecalculateBounds();
		mesh.RecalculateNormals();
	}
}
