public class Puppet2D_ParentControl : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject bone;

	public bool IsEnabled;

	public bool Point;

	public bool Orient;

	public bool Scale;

	public bool ConstrianedPosition;

	public bool ConstrianedOrient;

	public bool MaintainOffset;

	public global::UnityEngine.Vector3 OffsetPos;

	public global::UnityEngine.Vector3 OffsetScale = new global::UnityEngine.Vector3(1f, 1f, 1f);

	public global::UnityEngine.Quaternion OffsetOrient;

	public void ParentControlRun()
	{
		if (Orient)
		{
			if (MaintainOffset)
			{
				bone.transform.rotation = base.transform.rotation * OffsetOrient;
			}
			else
			{
				bone.transform.rotation = base.transform.rotation;
			}
		}
		if (Point)
		{
			if (MaintainOffset)
			{
				bone.transform.position = base.transform.TransformPoint(OffsetPos);
			}
			else
			{
				bone.transform.position = base.transform.position;
			}
		}
		if (Scale)
		{
			bone.transform.localScale = new global::UnityEngine.Vector3(base.transform.localScale.x * OffsetScale.x, base.transform.localScale.y * OffsetScale.y, base.transform.localScale.z * OffsetScale.z);
		}
		if (ConstrianedPosition && !Point)
		{
			base.transform.position = bone.transform.position;
		}
	}
}
