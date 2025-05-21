using UnityEngine;

public class Room_Control : MonoBehaviour
{
	public int Room_Num;
	public Transform cam_Top;
	public Transform cam_Bot;
	public Transform cam_Left;
	public Transform cam_Right;
	public Transform[] targetPos;
	public Transform Save_Pos;
	public float MaxCam_Size;
	public bool Start_MaxCam;
}
