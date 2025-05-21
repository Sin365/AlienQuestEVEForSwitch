public class LV_1_Cam_2 : global::UnityEngine.MonoBehaviour
{
	private bool onEnabled;

	private float Col_Timer;

	public global::UnityEngine.Transform Orig_Top;

	public global::UnityEngine.Transform Orig_Bot;

	public global::UnityEngine.Transform Orig_Left;

	public global::UnityEngine.Transform Orig_Right;

	public global::UnityEngine.Transform Target_Top;

	public global::UnityEngine.Transform Target_Bot;

	public global::UnityEngine.Transform Target_Left;

	public global::UnityEngine.Transform Target_Right;

	private Camera_Control CamCon;

	private Player_Control PC;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		PC = global::UnityEngine.GameObject.Find("Player").GetComponent<Player_Control>();
		CamCon = global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>();
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		if (Col_Timer > 0f)
		{
			Col_Timer -= global::UnityEngine.Time.deltaTime;
			if (Target_Top != null)
			{
				CamCon.Cam_Top = global::UnityEngine.Mathf.Lerp(CamCon.Cam_Top, Target_Top.position.y, global::UnityEngine.Time.deltaTime * 5f);
			}
			if (Target_Bot != null)
			{
				CamCon.Cam_Bot = global::UnityEngine.Mathf.Lerp(CamCon.Cam_Bot, Target_Bot.position.y, global::UnityEngine.Time.deltaTime * 5f);
			}
			if (Target_Left != null)
			{
				CamCon.Cam_Left = global::UnityEngine.Mathf.Lerp(CamCon.Cam_Left, Target_Left.position.x, global::UnityEngine.Time.deltaTime * 5f);
			}
			if (Target_Right != null)
			{
				CamCon.Cam_Right = global::UnityEngine.Mathf.Lerp(CamCon.Cam_Right, Target_Right.position.x, global::UnityEngine.Time.deltaTime * 5f);
			}
		}
		else
		{
			if (Orig_Top != null)
			{
				CamCon.Cam_Top = global::UnityEngine.Mathf.Lerp(CamCon.Cam_Top, Orig_Top.position.y, global::UnityEngine.Time.deltaTime * 2f);
			}
			if (Orig_Bot != null)
			{
				CamCon.Cam_Bot = global::UnityEngine.Mathf.Lerp(CamCon.Cam_Bot, Orig_Bot.position.y, global::UnityEngine.Time.deltaTime * 2f);
			}
			if (Orig_Left != null)
			{
				CamCon.Cam_Left = global::UnityEngine.Mathf.Lerp(CamCon.Cam_Left, Orig_Left.position.x, global::UnityEngine.Time.deltaTime * 2f);
			}
			if (Orig_Right != null)
			{
				CamCon.Cam_Right = global::UnityEngine.Mathf.Lerp(CamCon.Cam_Right, Orig_Right.position.x, global::UnityEngine.Time.deltaTime * 2f);
			}
		}
	}

	private void OnTriggerStay2D(global::UnityEngine.Collider2D col)
	{
		if (!GM.Paused && col.name == "Ani")
		{
			Col_Timer = 0.5f;
		}
	}

	private void OnTriggerExit2D(global::UnityEngine.Collider2D col)
	{
		if (!GM.Paused && col.name == "Ani")
		{
			Col_Timer = 0f;
		}
	}
}
