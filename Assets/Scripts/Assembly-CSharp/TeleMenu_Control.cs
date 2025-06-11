public class TeleMenu_Control : global::UnityEngine.MonoBehaviour
{
	private float Life_Timer;

	private float Delay_Timer;

	public int Teleport_Num;

	public float Dist = 100f;

	public float dist_Timer;

	private int Sel_Index;

	private int Prev_Index;

	private float PushX;

	private float inputX;

	private float prevX;

	private float PushY;

	private float inputY;

	private float prevY;

	public global::UnityEngine.UI.Image Img_Box_Now;

	public global::UnityEngine.UI.Image Img_Box_Target;

	public global::UnityEngine.SpriteRenderer save_BG_1;

	public global::UnityEngine.SpriteRenderer save_BG_2;

	public global::UnityEngine.UI.Text text_Title;

	public global::UnityEngine.UI.Text text_Center;

	public global::UnityEngine.UI.Text text_Center_Shadow;

	public global::UnityEngine.UI.Image[] Img_Num;

	public global::UnityEngine.UI.Image[] Img_Icon;

	public global::UnityEngine.SpriteRenderer Sel_Cursor;

	public global::UnityEngine.GameObject teleport_Block;

	private bool[] Img_ON;

	private global::UnityEngine.Color color_BG = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	private float opacity;

	private global::UnityEngine.Vector3 pos_Target_Box;

	private global::UnityEngine.Vector3 pos_Target_Cursor;

    GameManager GM => GameManager.instance;
    private Custom_Key CK => GameManager.instance.CK;

	private StageManager SM;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//CK = global::UnityEngine.GameObject.Find("GameManager").GetComponent<Custom_Key>();
		SM = global::UnityEngine.GameObject.Find("StageManager").GetComponent<StageManager>();
		GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(0f, 2000f, 0f);
		Img_ON = new bool[6];
		Img_ON[0] = true;
		for (int i = 1; i < 6; i++)
		{
			Img_ON[i] = false;
		}
		color_BG = save_BG_1.color;
		Change_Color();
		Img_Box_Now.rectTransform.localPosition = new global::UnityEngine.Vector3(-300 + Teleport_Num * 120, -30f, 0f);
		pos_Target_Box = new global::UnityEngine.Vector3(-300f, -30f, 0f);
		pos_Target_Cursor = new global::UnityEngine.Vector3(-300f, -112f, 0f);
	}

	private void Update()
	{
		if (GM.GameOver || GM.Paused || GM.onEvent || GM.onHscene || GM.onDown || GM.EventState == 200)
		{
			if (opacity > 0f)
			{
				opacity = 0f;
				Change_Color();
				GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(0f, 2000f, 0f);
			}
		}
		else if (Dist < 1.2f && dist_Timer > 0f)
		{
			dist_Timer -= global::UnityEngine.Time.deltaTime;
			inputX = 0f;
			inputY = 0f;
			if (global::UnityEngine.Input.GetKeyDown(CK.Right))
			{
				inputX = 1f;
			}
			else if (global::UnityEngine.Input.GetKeyDown(CK.Left))
			{
				inputX = -1f;
			}
			if (global::UnityEngine.Input.GetKeyDown(CK.Up))
			{
				inputY = 1f;
			}
			else if (global::UnityEngine.Input.GetKeyDown(CK.Down))
			{
				inputY = -1f;
			}
			if (inputX == 0f && global::UnityEngine.Input.GetAxis("L_X") != 0f)
			{
				if (global::UnityEngine.Input.GetAxis("L_X") > 0f && global::UnityEngine.Input.GetAxis("L_X") < prevX)
				{
					PushX = prevX;
				}
				else if (global::UnityEngine.Input.GetAxis("L_X") < 0f && global::UnityEngine.Input.GetAxis("L_X") > prevX)
				{
					PushX = prevX;
				}
				if (global::UnityEngine.Input.GetAxis("L_X") > 0f && global::UnityEngine.Input.GetAxis("L_X") - PushX > 0.3f)
				{
					inputX = 1f;
					PushX = 1f;
				}
				else if (global::UnityEngine.Input.GetAxis("L_X") < 0f && global::UnityEngine.Input.GetAxis("L_X") - PushX < -0.3f)
				{
					inputX = -1f;
					PushX = -1f;
				}
			}
			else if (inputX == 0f && global::UnityEngine.Input.GetAxis("L_X") == 0f)
			{
				PushX = 0f;
			}
			if (inputY == 0f && global::UnityEngine.Input.GetAxis("L_Y") != 0f)
			{
				if (global::UnityEngine.Input.GetAxis("L_Y") > 0f && global::UnityEngine.Input.GetAxis("L_Y") < prevY)
				{
					PushY = prevY;
				}
				else if (global::UnityEngine.Input.GetAxis("L_Y") < 0f && global::UnityEngine.Input.GetAxis("L_Y") > prevY)
				{
					PushY = prevY;
				}
				if (global::UnityEngine.Input.GetAxis("L_Y") > 0f && global::UnityEngine.Input.GetAxis("L_Y") - PushY > 0.3f)
				{
					inputY = 1f;
					PushY = 1f;
				}
				else if (global::UnityEngine.Input.GetAxis("L_Y") < 0f && global::UnityEngine.Input.GetAxis("L_Y") - PushY < -0.3f)
				{
					inputY = -1f;
					PushY = -1f;
				}
			}
			else if (inputY == 0f && global::UnityEngine.Input.GetAxis("L_Y") == 0f)
			{
				PushY = 0f;
			}
			if (GM.onTeleport)
			{
				Life_Timer += global::UnityEngine.Time.deltaTime;
				if (opacity < 1f)
				{
					opacity = global::UnityEngine.Mathf.Lerp(opacity, 1f, global::UnityEngine.Time.deltaTime * 5f);
					Change_Color();
				}
				float num = 120f + global::UnityEngine.Mathf.Sin(Life_Timer * 5f) * 10f;
				Sel_Cursor.transform.localScale = new global::UnityEngine.Vector3(num, 0f - num, 1f);
				if (inputX > 0f && Sel_Index < 5)
				{
					Sel_Index++;
					global::UnityEngine.GameObject.Find("Menu").SendMessage("Sound_Move_1");
				}
				else if (inputX < 0f && Sel_Index > 0)
				{
					Sel_Index--;
					global::UnityEngine.GameObject.Find("Menu").SendMessage("Sound_Move_1");
				}
				pos_Target_Box = new global::UnityEngine.Vector3(-300 + Sel_Index * 120, -30f, 0f);
				pos_Target_Cursor = new global::UnityEngine.Vector3(-300 + Sel_Index * 120, -112f, 0f);
				Img_Box_Target.rectTransform.localPosition = global::UnityEngine.Vector3.Lerp(Img_Box_Target.rectTransform.localPosition, pos_Target_Box, global::UnityEngine.Time.deltaTime * 12f);
				Sel_Cursor.transform.localPosition = global::UnityEngine.Vector3.Lerp(Sel_Cursor.transform.localPosition, pos_Target_Cursor, global::UnityEngine.Time.deltaTime * 12f);
				for (int i = 0; i < Img_Num.Length; i++)
				{
					if (i == Sel_Index)
					{
						if (Img_Icon[i] != null)
						{
							Img_Icon[i].rectTransform.localScale = global::UnityEngine.Vector3.Lerp(Img_Icon[i].rectTransform.localScale, new global::UnityEngine.Vector3(1.8f, 1.8f, 1f), global::UnityEngine.Time.deltaTime * 6f);
						}
					}
					else if (Img_Icon[i] != null)
					{
						Img_Icon[i].rectTransform.localScale = global::UnityEngine.Vector3.Lerp(Img_Icon[i].rectTransform.localScale, new global::UnityEngine.Vector3(1.2f, 1.2f, 1f), global::UnityEngine.Time.deltaTime * 6f);
					}
				}
				if (Sel_Index == 0)
				{
					text_Center.GetComponent<global::UnityEngine.RectTransform>().localScale = global::UnityEngine.Vector3.Lerp(text_Center.GetComponent<global::UnityEngine.RectTransform>().localScale, new global::UnityEngine.Vector3(1.1f, 1.1f, 1f), global::UnityEngine.Time.deltaTime * 6f);
				}
				else
				{
					text_Center.GetComponent<global::UnityEngine.RectTransform>().localScale = global::UnityEngine.Vector3.Lerp(text_Center.GetComponent<global::UnityEngine.RectTransform>().localScale, new global::UnityEngine.Vector3(1f, 1f, 1f), global::UnityEngine.Time.deltaTime * 6f);
				}
				text_Center_Shadow.GetComponent<global::UnityEngine.RectTransform>().localScale = text_Center.GetComponent<global::UnityEngine.RectTransform>().localScale;
				if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.Escape) || global::UnityEngine.Input.GetButtonDown("Start") || global::UnityEngine.Input.GetButtonDown("_B"))
				{
					global::UnityEngine.GameObject.Find("Player").GetComponent<Player_Control>().Lock_Timer = 0.2f;
					Off_Menu();
				}
				else if (global::UnityEngine.Input.GetButtonDown("Jump") || global::UnityEngine.Input.GetKeyDown(CK.Jump))
				{
					global::UnityEngine.GameObject.Find("Player").GetComponent<Player_Control>().Lock_Timer = 0.2f;
					if (Sel_Index != Teleport_Num)
					{
						if (Sel_Index == 0 || GM.Check_Teleport_On(Sel_Index))
						{
							Teleport();
							Off_Menu();
						}
					}
					else
					{
						Off_Menu();
					}
				}
			}
			else
			{
				if (opacity > 0f)
				{
					opacity = global::UnityEngine.Mathf.Lerp(opacity, 0f, global::UnityEngine.Time.deltaTime * 5f);
					Change_Color();
					Opacity_Off();
				}
				if (Delay_Timer > 0f)
				{
					Delay_Timer -= global::UnityEngine.Time.deltaTime;
				}
				if ((double)Dist < 1.2 && dist_Timer > 0f && inputY > 0f && Delay_Timer <= 0f)
				{
					On_Menu();
				}
			}
			Prev_Index = Sel_Index;
			prevX = global::UnityEngine.Input.GetAxis("L_X");
			prevY = global::UnityEngine.Input.GetAxis("L_Y");
		}
		else
		{
			if (dist_Timer > 0f)
			{
				dist_Timer -= global::UnityEngine.Time.deltaTime;
			}
			if (opacity > 0f)
			{
				opacity = global::UnityEngine.Mathf.Lerp(opacity, 0f, global::UnityEngine.Time.deltaTime * 5f);
				Change_Color();
				Opacity_Off();
			}
			if (GM.onTeleport)
			{
				Off_Menu();
			}
		}
	}

	private void Teleport()
	{
		Teleport_Num = Sel_Index;
		Img_Box_Now.rectTransform.localPosition = new global::UnityEngine.Vector3(-300 + Teleport_Num * 120, -30f, 0f);
		if (teleport_Block != null)
		{
			teleport_Block.GetComponent<Teleport>().onStartTeleport = true;
			GM.Level_Up_Effect(teleport_Block.GetComponent<Teleport>().pos_Teleport.position, 0f);
		}
		int num = 60;
		if (Teleport_Num == 1)
		{
			num = 24;
		}
		else if (Teleport_Num == 2)
		{
			num = 44;
		}
		else if (Teleport_Num == 3)
		{
			num = 90;
		}
		else if (Teleport_Num == 4)
		{
			num = 125;
		}
		else if (Teleport_Num == 5)
		{
			num = 142;
		}
		global::UnityEngine.GameObject.Find("Player").GetComponent<Player_Control>().Lock_GameLoad();
		if (num == 60)
		{
			SM.Go_Room(num, 5, 0f, 0f, true);
		}
		else
		{
			SM.Go_Room(num, 0, 0f, 0f, true);
		}
		global::UnityEngine.GameObject.Find("Menu").SendMessage("Sound_DeviceOn");
	}

	private void Change_Color()
	{
		save_BG_1.color = new global::UnityEngine.Color(color_BG.r, color_BG.g, color_BG.b, opacity);
		save_BG_2.color = new global::UnityEngine.Color(color_BG.r, color_BG.g, color_BG.b, opacity * 0.75f);
		Sel_Cursor.color = new global::UnityEngine.Color(1f, 1f, 1f, opacity);
		text_Title.color = Sel_Cursor.color;
		for (int i = 0; i < Img_Num.Length; i++)
		{
			if (Img_ON[i])
			{
				Img_Num[i].color = Sel_Cursor.color;
				if (Img_Icon[i] != null)
				{
					Img_Icon[i].color = Sel_Cursor.color;
				}
			}
			else
			{
				Img_Num[i].color = new global::UnityEngine.Color(1f, 1f, 1f, opacity * 0.25f);
				if (Img_Icon[i] != null)
				{
					Img_Icon[i].color = Img_Num[i].color;
				}
			}
		}
		Img_Box_Now.color = new global::UnityEngine.Color(0f, 0f, 0f, opacity * 0.15f);
		Img_Box_Target.color = new global::UnityEngine.Color(1f, 1f, 1f, opacity * 0.6f);
		text_Center.color = Sel_Cursor.color;
		text_Center_Shadow.color = new global::UnityEngine.Color(0f, 0f, 0f, opacity * 0.5f);
	}

	private void Opacity_Off()
	{
		if (opacity < 0.02f)
		{
			opacity = 0f;
			Change_Color();
			GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(0f, 2000f, 0f);
		}
	}

	private void On_Menu()
	{
		GM.onTeleport = true;
		GM.resumeTimer = 0.5f;
		Sel_Index = 0;
		GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(0f, 240f, 0f);
		for (int i = 1; i < 6; i++)
		{
			Img_ON[i] = GM.Check_Teleport_On(i);
		}
		Img_Box_Now.rectTransform.localPosition = new global::UnityEngine.Vector3(-300 + Teleport_Num * 120, -30f, 0f);
		Img_Box_Target.rectTransform.localPosition = (pos_Target_Box = new global::UnityEngine.Vector3(-300f, -30f, 0f));
		Sel_Cursor.transform.localPosition = (pos_Target_Cursor = new global::UnityEngine.Vector3(-300f, -112f, 0f));
		global::UnityEngine.GameObject.Find("Menu").SendMessage("Sound_MenuOn");
		global::UnityEngine.GameObject.Find("EscapeTimer").SendMessage("Pause_Timer");
	}

	private void Off_Menu()
	{
		GM.onTeleport = false;
		GM.resumeTimer = 0.5f;
		Sel_Index = 0;
		global::UnityEngine.GameObject.Find("Menu").SendMessage("Sound_MenuOff");
		global::UnityEngine.GameObject.Find("EscapeTimer").SendMessage("Set_Timer");
	}
}
