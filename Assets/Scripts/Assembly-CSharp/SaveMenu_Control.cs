public class SaveMenu_Control : global::UnityEngine.MonoBehaviour
{
	private float Life_Timer;

	private float Delay_Timer;

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

	public global::UnityEngine.SpriteRenderer save_BG_1;

	public global::UnityEngine.SpriteRenderer save_BG_2;

	public global::UnityEngine.UI.Text text_Question;

	public global::UnityEngine.UI.Text text_Yes;

	public global::UnityEngine.UI.Text text_No;

	public global::UnityEngine.SpriteRenderer Sel_Cursor;

	public global::UnityEngine.Transform pos_Yes;

	public global::UnityEngine.Transform pos_No;

	public global::UnityEngine.GameObject info_SaveComplete;

	public global::UnityEngine.GameObject save_Block;

	private global::UnityEngine.Color color_BG = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	private float opacity;

	private global::UnityEngine.Vector3 pos_Target;

	private GameManager GM;

	private Custom_Key CK;

	private Language_MenuItem Lang_MI;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		CK = global::UnityEngine.GameObject.Find("GameManager").GetComponent<Custom_Key>();
		Lang_MI = global::UnityEngine.GameObject.Find("Menu").GetComponent<Language_MenuItem>();
		GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(0f, 2000f, 0f);
		color_BG = save_BG_1.color;
		save_BG_1.color = new global::UnityEngine.Color(color_BG.r, color_BG.g, color_BG.b, 0f);
		save_BG_2.color = save_BG_1.color;
		Sel_Cursor.color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
		global::UnityEngine.UI.Text text = text_Question;
		global::UnityEngine.Color color = Sel_Cursor.color;
		text_No.color = color;
		color = color;
		text_Yes.color = color;
		text.color = color;
		pos_Target = pos_Yes.position;
		if (AxiPlayerPrefs.GetInt("Language_Num") == 1)
		{
			text_Question.text = Lang_MI.TitleMenu(7, 1);
			text_Yes.text = Lang_MI.QuitText(1, 1);
			text_No.text = Lang_MI.QuitText(2, 1);
		}
	}

	private void Update()
	{
		if (GM.GameOver || GM.Paused || GM.onEvent || GM.onHscene || GM.onDown)
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
			if (GM.onSave)
			{
				Life_Timer += global::UnityEngine.Time.deltaTime;
				if (opacity < 1f)
				{
					opacity = global::UnityEngine.Mathf.Lerp(opacity, 1f, global::UnityEngine.Time.deltaTime * 5f);
					Change_Color();
				}
				float num = 120f + global::UnityEngine.Mathf.Sin(Life_Timer * 5f) * 10f;
				Sel_Cursor.transform.localScale = new global::UnityEngine.Vector3(num, num, 1f);
				if (inputX > 0f && Sel_Index == 0)
				{
					Sel_Index = 1;
					pos_Target = pos_No.position;
					global::UnityEngine.GameObject.Find("Menu").SendMessage("Sound_Move_1");
				}
				else if (inputX < 0f && Sel_Index == 1)
				{
					Sel_Index = 0;
					pos_Target = pos_Yes.position;
					global::UnityEngine.GameObject.Find("Menu").SendMessage("Sound_Move_1");
				}
				Sel_Cursor.transform.position = global::UnityEngine.Vector3.Lerp(Sel_Cursor.transform.position, pos_Target, global::UnityEngine.Time.deltaTime * 12f);
				if (Sel_Index == 0)
				{
					text_Yes.GetComponent<global::UnityEngine.RectTransform>().localScale = global::UnityEngine.Vector3.Lerp(text_Yes.GetComponent<global::UnityEngine.RectTransform>().localScale, new global::UnityEngine.Vector3(1.1f, 1.1f, 1f), global::UnityEngine.Time.deltaTime * 6f);
					text_No.GetComponent<global::UnityEngine.RectTransform>().localScale = global::UnityEngine.Vector3.Lerp(text_No.GetComponent<global::UnityEngine.RectTransform>().localScale, new global::UnityEngine.Vector3(1f, 1f, 1f), global::UnityEngine.Time.deltaTime * 6f);
				}
				else
				{
					text_Yes.GetComponent<global::UnityEngine.RectTransform>().localScale = global::UnityEngine.Vector3.Lerp(text_Yes.GetComponent<global::UnityEngine.RectTransform>().localScale, new global::UnityEngine.Vector3(1f, 1f, 1f), global::UnityEngine.Time.deltaTime * 6f);
					text_No.GetComponent<global::UnityEngine.RectTransform>().localScale = global::UnityEngine.Vector3.Lerp(text_No.GetComponent<global::UnityEngine.RectTransform>().localScale, new global::UnityEngine.Vector3(1.1f, 1.1f, 1f), global::UnityEngine.Time.deltaTime * 6f);
				}
				if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.Escape) || global::UnityEngine.Input.GetButtonDown("Start") || global::UnityEngine.Input.GetButtonDown("_B"))
				{
					global::UnityEngine.GameObject.Find("Player").GetComponent<Player_Control>().Lock_Timer = 0.2f;
					Off_SaveMenu();
				}
				else if (global::UnityEngine.Input.GetButtonDown("Jump") || global::UnityEngine.Input.GetKeyDown(CK.Jump))
				{
					global::UnityEngine.GameObject.Find("Player").GetComponent<Player_Control>().Lock_Timer = 0.2f;
					if (Sel_Index == 0)
					{
						if (GM.EventState != 200)
						{
							SaveGame();
						}
						Off_SaveMenu();
					}
					else
					{
						Off_SaveMenu();
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
					if (GM.EventState != 200)
					{
						On_SaveMenu();
					}
					else if (GM.Room_Num == 0 && GM.EventState == 200 && GM.Get_Event(3))
					{
						GM.onEvent = true;
						GM.onGameClear = true;
						global::UnityEngine.GameObject.Find("Menu").SendMessage("Sound_DeviceOn");
						global::UnityEngine.GameObject.Find("Menu").SendMessage("Sound_Get_FullHP");
						global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().targetSize = 11.2f;
						global::UnityEngine.Debug.Log("Game Ending Start!!!!!!!!!!!!!!!!!");
					}
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
		}
	}

	private void SaveGame()
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(info_SaveComplete) as global::UnityEngine.GameObject;
		if (AxiPlayerPrefs.GetInt("Language_Num") == 1)
		{
			string start = Lang_MI.TitleMenu(8, 1);
			gameObject.GetComponent<Info_Save_Completed>().Set_Start(start);
		}
		GM.Save_Data();
		if (save_Block != null)
		{
			save_Block.SendMessage("Save");
		}
		global::UnityEngine.GameObject.Find("Menu").SendMessage("Sound_DeviceOn");
		global::UnityEngine.GameObject.Find("Menu").SendMessage("Sound_Get_FullHP");
	}

	private void Change_Color()
	{
		save_BG_1.color = new global::UnityEngine.Color(color_BG.r, color_BG.g, color_BG.b, opacity);
		save_BG_2.color = new global::UnityEngine.Color(color_BG.r, color_BG.g, color_BG.b, opacity * 0.75f);
		Sel_Cursor.color = new global::UnityEngine.Color(1f, 1f, 1f, opacity);
		global::UnityEngine.UI.Text text = text_Question;
		global::UnityEngine.Color color = Sel_Cursor.color;
		text_No.color = color;
		color = color;
		text_Yes.color = color;
		text.color = color;
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

	private void On_SaveMenu()
	{
		GM.onSave = true;
		GM.resumeTimer = 0.5f;
		Sel_Index = 0;
		GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(0f, 138f, 0f);
		pos_Target = pos_Yes.position;
		Sel_Cursor.transform.position = pos_Yes.position;
		global::UnityEngine.GameObject.Find("Menu").SendMessage("Sound_MenuOn");
		global::UnityEngine.GameObject.Find("EscapeTimer").SendMessage("Pause_Timer");
	}

	private void Off_SaveMenu()
	{
		GM.onSave = false;
		GM.resumeTimer = 0.5f;
		Sel_Index = 0;
		global::UnityEngine.GameObject.Find("Menu").SendMessage("Sound_MenuOff");
		global::UnityEngine.GameObject.Find("EscapeTimer").SendMessage("Set_Timer");
	}
}
