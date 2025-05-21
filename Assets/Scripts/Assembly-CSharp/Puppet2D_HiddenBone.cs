[global::UnityEngine.ExecuteInEditMode]
public class Puppet2D_HiddenBone : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.Transform boneToAimAt;

	public bool InEditBoneMode;

	public global::UnityEngine.GameObject[] _newSelection;

	private void LateUpdate()
	{
		if (!GetComponent<global::UnityEngine.Renderer>().enabled)
		{
			return;
		}
		if ((bool)boneToAimAt && (bool)base.transform.parent)
		{
			global::UnityEngine.Transform parent = base.transform.parent;
			base.transform.parent = null;
			float num = global::UnityEngine.Vector3.Distance(boneToAimAt.position, base.transform.position);
			if (num > 0f)
			{
				base.transform.rotation = global::UnityEngine.Quaternion.LookRotation(boneToAimAt.position - base.transform.position, global::UnityEngine.Vector3.forward) * global::UnityEngine.Quaternion.AngleAxis(90f, global::UnityEngine.Vector3.right);
			}
			float magnitude = (boneToAimAt.position - base.transform.position).magnitude;
			base.transform.localScale = new global::UnityEngine.Vector3(magnitude, magnitude, magnitude);
			if ((bool)parent)
			{
				base.transform.parent = parent;
				base.transform.position = parent.position;
				if ((bool)parent.GetComponent<global::UnityEngine.SpriteRenderer>())
				{
					base.transform.GetComponent<global::UnityEngine.SpriteRenderer>().sortingLayerName = parent.GetComponent<global::UnityEngine.SpriteRenderer>().sortingLayerName;
				}
			}
			base.transform.hideFlags = global::UnityEngine.HideFlags.HideInHierarchy | global::UnityEngine.HideFlags.NotEditable;
		}
		else
		{
			global::UnityEngine.Object.DestroyImmediate(base.gameObject);
		}
	}

	public void Refresh()
	{
		if (!boneToAimAt)
		{
			return;
		}
		global::UnityEngine.Transform parent = base.transform.parent;
		base.transform.parent = null;
		float num = global::UnityEngine.Vector3.Distance(boneToAimAt.position, base.transform.position);
		if (num > 0f)
		{
			base.transform.rotation = global::UnityEngine.Quaternion.LookRotation(boneToAimAt.position - base.transform.position, global::UnityEngine.Vector3.forward) * global::UnityEngine.Quaternion.AngleAxis(90f, global::UnityEngine.Vector3.right);
		}
		float magnitude = (boneToAimAt.position - base.transform.position).magnitude;
		base.transform.localScale = new global::UnityEngine.Vector3(magnitude, magnitude, magnitude);
		if ((bool)parent)
		{
			base.transform.parent = parent;
			base.transform.position = parent.position;
			if ((bool)parent.GetComponent<global::UnityEngine.SpriteRenderer>())
			{
				base.transform.GetComponent<global::UnityEngine.SpriteRenderer>().sortingLayerName = parent.GetComponent<global::UnityEngine.SpriteRenderer>().sortingLayerName;
			}
		}
	}
}
