public class Room_Control : global::UnityEngine.MonoBehaviour
{
	public int Room_Num;

	public global::UnityEngine.Transform cam_Top;

	public global::UnityEngine.Transform cam_Bot;

	public global::UnityEngine.Transform cam_Left;

	public global::UnityEngine.Transform cam_Right;

	public global::UnityEngine.Transform[] targetPos;

	public global::UnityEngine.Transform Save_Pos;

	public float MaxCam_Size;

	public bool Start_MaxCam;

	private GameManager GM;

	private StageManager SM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		SM = global::UnityEngine.GameObject.Find("StageManager").GetComponent<StageManager>();
		if (Room_Num == 0 || !GM.onEvent)
		{
			global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().Cam_Top = cam_Top.position.y;
			global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().Cam_Bot = cam_Bot.position.y;
			global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().Cam_Left = cam_Left.position.x;
			global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().Cam_Right = cam_Right.position.x;
			if (Room_Num < 150)
			{
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().MaxSize = MaxCam_Size;
			}
			if (global::UnityEngine.GameObject.Find("Main Camera").GetComponent<UnityEngine.Camera>().orthographicSize > MaxCam_Size || Start_MaxCam)
			{
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().targetSize = MaxCam_Size;
			}
			GM.Room_Num = Room_Num;
			SM.Current_Room = base.gameObject;
			if (Room_Num == 92 && GM.Get_Event(1))
			{
				if (global::UnityEngine.GameObject.Find("EVE_Core") != null)
				{
					global::UnityEngine.GameObject.Find("EVE_Core").GetComponent<Event_Core>().Set_State(2);
				}
				if (global::UnityEngine.GameObject.Find("Computer_1") != null)
				{
					global::UnityEngine.GameObject.Find("Computer_1").GetComponent<Event_Computer>().Set_State(2);
				}
				if (global::UnityEngine.GameObject.Find("Computer_2") != null)
				{
					global::UnityEngine.GameObject.Find("Computer_2").GetComponent<Event_Computer>().Set_State(2);
				}
				if (global::UnityEngine.GameObject.Find("Computer_3") != null)
				{
					global::UnityEngine.GameObject.Find("Computer_3").GetComponent<Event_Computer>().Set_State(2);
				}
			}
		}
		else
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
