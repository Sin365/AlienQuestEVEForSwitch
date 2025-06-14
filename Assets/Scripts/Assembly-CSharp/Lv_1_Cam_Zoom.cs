using UnityEngine;

public class Lv_1_Cam_Zoom : global::UnityEngine.MonoBehaviour
{
	private bool onEnabled;

	private float Col_Timer;

	private float Original_CamMax = 11.2f;

	private float Current_CamSize = 8f;

	public float Target_CamMax = 6.5f;

	public global::UnityEngine.Transform Orig_Top;

	public global::UnityEngine.Transform Orig_Bot;

	public global::UnityEngine.Transform Orig_Left;

	public global::UnityEngine.Transform Orig_Right;

	public global::UnityEngine.Transform Target_Top;

	public global::UnityEngine.Transform Target_Bot;

	public global::UnityEngine.Transform Target_Left;

	public global::UnityEngine.Transform Target_Right;

	public global::UnityEngine.SpriteRenderer[] SR_List;

	private global::UnityEngine.Color[] color_Orig;

	private global::UnityEngine.Color color_OFF = new global::UnityEngine.Color(0f, 0f, 0f, 0f);

	private Camera_Control CamCon;
    Player_Control PC => GameManager.instance?.PC;
    GameObject Player => GameManager.instance?.gobj_Player;

    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//PC = global::UnityEngine.GameObject.Find("Player").GetComponent<Player_Control>();
		CamCon = UnityEngine.Camera.main.GetComponent<Camera_Control>();
		color_Orig = new global::UnityEngine.Color[SR_List.Length];
		for (int i = 0; i < SR_List.Length; i++)
		{
			color_Orig[i] = SR_List[i].color;
			SR_List[i].color = new global::UnityEngine.Color(SR_List[i].color.r, SR_List[i].color.g, SR_List[i].color.b, 0f);
		}
		Original_CamMax = CamCon.MaxSize;
		Current_CamSize = CamCon.GetComponent<UnityEngine.Camera>().orthographicSize;
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		if (Col_Timer > 0.05f)
		{
			if (!onEnabled)
			{
				onEnabled = true;
				Current_CamSize = CamCon.GetComponent<UnityEngine.Camera>().orthographicSize;
			}
			CamCon.MaxSize = Target_CamMax;
			if (Col_Timer > 0.8f)
			{
				if (Target_Top != null)
				{
					CamCon.Cam_Top = global::UnityEngine.Mathf.Lerp(CamCon.Cam_Top, Target_Top.position.y, global::UnityEngine.Time.deltaTime * 3f);
				}
				if (Target_Bot != null)
				{
					CamCon.Cam_Bot = global::UnityEngine.Mathf.Lerp(CamCon.Cam_Bot, Target_Bot.position.y, global::UnityEngine.Time.deltaTime * 3f);
				}
				if (Target_Left != null)
				{
					CamCon.Cam_Left = global::UnityEngine.Mathf.Lerp(CamCon.Cam_Left, Target_Left.position.x, global::UnityEngine.Time.deltaTime * 3f);
				}
				if (Target_Right != null)
				{
					CamCon.Cam_Right = global::UnityEngine.Mathf.Lerp(CamCon.Cam_Right, Target_Right.position.x, global::UnityEngine.Time.deltaTime * 3f);
				}
			}
			for (int i = 0; i < SR_List.Length; i++)
			{
				SR_List[i].color = global::UnityEngine.Color.Lerp(SR_List[i].color, color_Orig[i], global::UnityEngine.Time.deltaTime * 2f);
			}
		}
		else
		{
			if (onEnabled)
			{
				onEnabled = false;
				CamCon.targetSize = Current_CamSize;
			}
			CamCon.MaxSize = Original_CamMax;
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
			for (int j = 0; j < SR_List.Length; j++)
			{
				SR_List[j].color = global::UnityEngine.Color.Lerp(SR_List[j].color, new global::UnityEngine.Color(SR_List[j].color.r, SR_List[j].color.g, SR_List[j].color.b, 0f), global::UnityEngine.Time.deltaTime * 2f);
			}
		}
	}

	private void OnTriggerStay2D(global::UnityEngine.Collider2D col)
	{
		if (!GM.Paused && col.name == "Ani")
		{
			Col_Timer += global::UnityEngine.Time.deltaTime;
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
