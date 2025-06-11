public class LV_3_Hidden : global::UnityEngine.MonoBehaviour
{
	private bool onEnabled;

	public global::UnityEngine.Transform Orig_Top;

	public global::UnityEngine.Transform Orig_Bot;

	public global::UnityEngine.Transform Orig_Left;

	public global::UnityEngine.Transform Orig_Right;

	public global::UnityEngine.Transform Target_Top;

	public global::UnityEngine.Transform Target_Bot;

	public global::UnityEngine.Transform Target_Left;

	public global::UnityEngine.Transform Target_Right;

	public global::UnityEngine.Transform Pos_Check;

	public global::UnityEngine.SpriteRenderer[] SR_List;

	private global::UnityEngine.Color[] color_Orig;

	private Camera_Control CamCon;

	private Player_Control PC;

	GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		PC = global::UnityEngine.GameObject.Find("Player").GetComponent<Player_Control>();
		CamCon = global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>();
		color_Orig = new global::UnityEngine.Color[SR_List.Length];
		for (int i = 0; i < SR_List.Length; i++)
		{
			color_Orig[i] = SR_List[i].color;
			SR_List[i].color = new global::UnityEngine.Color(SR_List[i].color.r, SR_List[i].color.g, SR_List[i].color.b, 0f);
		}
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		if (PC.transform.position.y > Pos_Check.transform.position.y)
		{
			if (Target_Top != null)
			{
				CamCon.Cam_Top = global::UnityEngine.Mathf.Lerp(CamCon.Cam_Top, Target_Top.position.y, global::UnityEngine.Time.deltaTime * 10f);
			}
			if (Target_Bot != null)
			{
				CamCon.Cam_Bot = global::UnityEngine.Mathf.Lerp(CamCon.Cam_Bot, Target_Bot.position.y, global::UnityEngine.Time.deltaTime * 10f);
			}
			if (Target_Left != null)
			{
				CamCon.Cam_Left = global::UnityEngine.Mathf.Lerp(CamCon.Cam_Left, Target_Left.position.x, global::UnityEngine.Time.deltaTime * 10f);
			}
			if (Target_Right != null)
			{
				CamCon.Cam_Right = global::UnityEngine.Mathf.Lerp(CamCon.Cam_Right, Target_Right.position.x, global::UnityEngine.Time.deltaTime * 10f);
			}
			for (int i = 0; i < SR_List.Length; i++)
			{
				SR_List[i].color = global::UnityEngine.Color.Lerp(SR_List[i].color, new global::UnityEngine.Color(SR_List[i].color.r, SR_List[i].color.g, SR_List[i].color.b, 0f), global::UnityEngine.Time.deltaTime * 10f);
			}
		}
		else
		{
			if (Orig_Top != null)
			{
				CamCon.Cam_Top = global::UnityEngine.Mathf.Lerp(CamCon.Cam_Top, Orig_Top.position.y, global::UnityEngine.Time.deltaTime * 5f);
			}
			if (Orig_Bot != null)
			{
				CamCon.Cam_Bot = global::UnityEngine.Mathf.Lerp(CamCon.Cam_Bot, Orig_Bot.position.y, global::UnityEngine.Time.deltaTime * 5f);
			}
			if (Orig_Left != null)
			{
				CamCon.Cam_Left = global::UnityEngine.Mathf.Lerp(CamCon.Cam_Left, Orig_Left.position.x, global::UnityEngine.Time.deltaTime * 5f);
			}
			if (Orig_Right != null)
			{
				CamCon.Cam_Right = global::UnityEngine.Mathf.Lerp(CamCon.Cam_Right, Orig_Right.position.x, global::UnityEngine.Time.deltaTime * 5f);
			}
			for (int j = 0; j < SR_List.Length; j++)
			{
				SR_List[j].color = global::UnityEngine.Color.Lerp(SR_List[j].color, color_Orig[j], global::UnityEngine.Time.deltaTime * 5f);
			}
		}
	}
}
