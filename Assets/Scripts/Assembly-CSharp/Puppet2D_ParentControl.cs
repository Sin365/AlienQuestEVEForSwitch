using UnityEngine;

public class Puppet2D_ParentControl : MonoBehaviour
{
	public GameObject bone;
	public bool IsEnabled;
	public bool Point;
	public bool Orient;
	public bool Scale;
	public bool ConstrianedPosition;
	public bool ConstrianedOrient;
	public bool MaintainOffset;
	public Vector3 OffsetPos;
	public Vector3 OffsetScale;
	public Quaternion OffsetOrient;
}
