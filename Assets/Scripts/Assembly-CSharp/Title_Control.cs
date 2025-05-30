public class Title_Control : global::UnityEngine.MonoBehaviour
{
	private int Menu_State;

	private int Bar_State;

	private bool isGameStart;

	private bool isNewGame;

	private bool isSaved;

	private int CopySlot_Num;

	private int Sel_Index = 1;

	private int Prev_Index = 1;

	private int Window_Size = 1920;

	private int Language_Num;

	private global::UnityEngine.Vector3 PosOrig;

	private global::UnityEngine.Vector3 PosTarget;

	private global::UnityEngine.Vector3 Pos_Open_Top;

	private global::UnityEngine.Vector3 Pos_Open_Bot;

	private global::UnityEngine.Vector3 Pos_Close_Top;

	private global::UnityEngine.Vector3 Pos_Close_Bot;

	private global::UnityEngine.Vector3 Pos_Close_Orig;

	private float PushX;

	private float inputX;

	private float prevX;

	private bool lock_Y;

	private float PushY;

	private float inputY;

	private float prevY;

	private float SelBG_Opacity = 1f;

	private float SelGlow_Opacity;

	private float SelCursor_Timer;

	private float SelCursor_Size = 1f;

	private float SelCursor_Opacity = 1f;

	private float[] SelArrow_Opacity = new float[2] { 0.1f, 0.1f };

	private float Menu_Size = 1f;

	private float Menu_Opacity = 1f;

	private float Box_Opacity = 0.04f;

	private float Slot_Opacity;

	private global::UnityEngine.Color FontColor1 = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color FontColor2 = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color CopyFontColor = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color CopyBoxColor = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Vector3 MousePos;

	private global::UnityEngine.Vector3 MousePosPrev;

	private float MouseDist;

	private int Sound_MoveFalse;

	private float Start_Timer;

	private float Load_Timer;

	private bool DataLoaded;

	private int Input_Mode;

	private float Input_Delay;

	private float Delay_Timer;

	private float DelaySound_Timer;

	private bool isLoaded;

	public global::UnityEngine.GameObject Slot_Obj;

	public global::UnityEngine.GameObject Save_Slot_1;

	public global::UnityEngine.GameObject Save_Slot_2;

	public global::UnityEngine.GameObject Save_Slot_3;

	public global::UnityEngine.GameObject InfoDel_Obj;

	public global::UnityEngine.UI.Image Title_Font_I;

	public global::UnityEngine.UI.Image Title_Font_V;

	public global::UnityEngine.UI.Image Title_Font_E;

	public global::UnityEngine.UI.Image Title_Font_I_a;

	public global::UnityEngine.UI.Image Title_Font_V_a;

	public global::UnityEngine.UI.Image Title_Font_E_a;

	public global::UnityEngine.UI.Text Title_Text;

	public global::UnityEngine.UI.Text Version_Text;

	public global::UnityEngine.GameObject Tail_1;

	public global::UnityEngine.GameObject Tail_2;

	public global::UnityEngine.GameObject Tail_3;

	public global::UnityEngine.GameObject Tail_4;

	public global::UnityEngine.SpriteRenderer SR_Glow_1;

	public global::UnityEngine.SpriteRenderer SR_Glow_2;

	private float text_Timer;

	private float textPos_Timer;

	private global::UnityEngine.Vector3[] pos_Font = new global::UnityEngine.Vector3[6];

	private global::UnityEngine.Color color_OFF = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	private bool isFadeIn = true;

	private bool isFadeOut;

	private float FadeOpacity = 1f;

	private string FadeOutAction = string.Empty;

	private global::UnityEngine.UI.Image BlackFade;

	private void Start()
	{
		BlackFade = global::UnityEngine.GameObject.Find("BlackFade").GetComponent<global::UnityEngine.UI.Image>();
		BlackFade.enabled = true;
		global::UnityEngine.GameObject.Find("pos_Loading_Bar").GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(0f, -2000f, 0f);
		global::UnityEngine.GameObject.Find("Loading_Bar").GetComponent<global::UnityEngine.UI.Image>().fillAmount = 0f;
		PosOrig = GetComponent<global::UnityEngine.RectTransform>().localPosition;
		PosTarget = global::UnityEngine.GameObject.Find("Pos_CloseOrigin").GetComponent<global::UnityEngine.RectTransform>().position;
		MousePos = global::UnityEngine.Input.mousePosition;
		MousePosPrev = global::UnityEngine.Input.mousePosition;
		PosTarget = global::UnityEngine.GameObject.Find("Pos_Button_1").GetComponent<global::UnityEngine.RectTransform>().position;
		Target_Select();
		FontColor1 = global::UnityEngine.GameObject.Find("Name_NewGame").GetComponent<global::UnityEngine.UI.Text>().color;
		FontColor2 = global::UnityEngine.GameObject.Find("Text_Slot_1").GetComponent<global::UnityEngine.UI.Text>().color;
		CopyFontColor = global::UnityEngine.GameObject.Find("Text_Copy").GetComponent<global::UnityEngine.UI.Text>().color;
		CopyBoxColor = global::UnityEngine.GameObject.Find("BG_Copy").GetComponent<global::UnityEngine.UI.Image>().color;
		Pos_Open_Top = global::UnityEngine.GameObject.Find("Pos_OpenTop").GetComponent<global::UnityEngine.RectTransform>().position;
		Pos_Open_Bot = global::UnityEngine.GameObject.Find("Pos_OpenBot").GetComponent<global::UnityEngine.RectTransform>().position;
		Pos_Close_Top = global::UnityEngine.GameObject.Find("Pos_CloseTop").GetComponent<global::UnityEngine.RectTransform>().position;
		Pos_Close_Bot = global::UnityEngine.GameObject.Find("Pos_CloseBot").GetComponent<global::UnityEngine.RectTransform>().position;
		Pos_Close_Orig = global::UnityEngine.GameObject.Find("Pos_CloseOrigin").GetComponent<global::UnityEngine.RectTransform>().position;
		global::UnityEngine.GameObject.Find("Dlg_Top").GetComponent<global::UnityEngine.RectTransform>().position = Pos_Close_Top;
		global::UnityEngine.GameObject.Find("Dlg_Bottom").GetComponent<global::UnityEngine.RectTransform>().position = Pos_Close_Bot;
		global::UnityEngine.GameObject.Find("Select_BG").GetComponent<global::UnityEngine.RectTransform>().position = Pos_Close_Orig;
		global::UnityEngine.GameObject.Find("Select_BG_Glow").GetComponent<global::UnityEngine.RectTransform>().position = Pos_Close_Orig;
		Menu_Opacity = 0f;
		SelArrow_Opacity[0] = 0f;
		SelArrow_Opacity[1] = 0f;
		SelCursor_Opacity = 0f;
		Box_Opacity = 0f;
		FontColor1.a = 0f;
		FontColor2.a = 0f;
		for (int i = 1; i < 8; i++)
		{
			global::UnityEngine.GameObject.Find("Pos_Button_" + i).GetComponent<global::UnityEngine.BoxCollider2D>().enabled = false;
		}
		for (int j = 1; j < 4; j++)
		{
			global::UnityEngine.GameObject.Find("Pos_Slot_" + j).GetComponent<global::UnityEngine.BoxCollider2D>().enabled = false;
		}
		Ani_Opacity();
		if (global::UnityEngine.PlayerPrefs.GetInt("Game_Setting") > 0)
		{
			Window_Size = global::UnityEngine.PlayerPrefs.GetInt("WindowSize");
			Language_Num = global::UnityEngine.PlayerPrefs.GetInt("Language_Num");
		}
		else
		{
			global::UnityEngine.PlayerPrefs.SetInt("WindowSize", Window_Size);
			global::UnityEngine.PlayerPrefs.SetInt("Language_Num", Language_Num);
			global::UnityEngine.PlayerPrefs.SetFloat("SoundVolume", 0.7f);
			global::UnityEngine.PlayerPrefs.SetFloat("MusicVolume", 0.7f);
			global::UnityEngine.PlayerPrefs.SetInt("SelBGM", 0);
			global::UnityEngine.PlayerPrefs.SetInt("Censorship", 0);
			global::UnityEngine.PlayerPrefs.SetInt("On_Hscene", 0);
			global::UnityEngine.PlayerPrefs.SetInt("On_HealthBar", 0);
			global::UnityEngine.PlayerPrefs.SetInt("UncensoredPatch", 0);
			global::UnityEngine.PlayerPrefs.SetInt("onClockFps", 1);
			global::UnityEngine.PlayerPrefs.SetInt("Gallery_Option", 1);
			global::UnityEngine.PlayerPrefs.SetInt("Gallery_Option_GameOver", 1);
		}
		global::UnityEngine.PlayerPrefs.SetInt("Game_Setting", 1);
		global::UnityEngine.PlayerPrefs.SetInt("H_GameOver_Now", 0);
		global::UnityEngine.PlayerPrefs.SetInt("Escaped", 0);
		if (Window_Size != global::UnityEngine.Screen.width)
		{
			if (Window_Size == 1280)
			{
				global::UnityEngine.Screen.SetResolution(1280, 720, false);
			}
			else
			{
				global::UnityEngine.Screen.SetResolution(1920, 1080, true);
			}
		}
		if (Window_Size != 1920)
		{
			Text_WindowMode();
		}
		if (Language_Num != 0)
		{
			Text_Language();
		}
		Title_Font_I.color = color_OFF;
		Title_Font_V.color = color_OFF;
		Title_Font_E.color = color_OFF;
		Title_Font_I_a.color = color_OFF;
		Title_Font_V_a.color = color_OFF;
		Title_Font_E_a.color = color_OFF;
		Title_Text.color = color_OFF;
		Version_Text.color = color_OFF;
		pos_Font[0] = Title_Font_I.GetComponent<global::UnityEngine.RectTransform>().localPosition;
		pos_Font[1] = Title_Font_V.GetComponent<global::UnityEngine.RectTransform>().localPosition;
		pos_Font[2] = Title_Font_E.GetComponent<global::UnityEngine.RectTransform>().localPosition;
		pos_Font[3] = Title_Font_I_a.GetComponent<global::UnityEngine.RectTransform>().localPosition;
		pos_Font[4] = Title_Font_V_a.GetComponent<global::UnityEngine.RectTransform>().localPosition;
		pos_Font[5] = Title_Font_E_a.GetComponent<global::UnityEngine.RectTransform>().localPosition;
		SR_Glow_2.color = new global::UnityEngine.Color(SR_Glow_2.color.r, SR_Glow_2.color.g, SR_Glow_2.color.b, 0.05f);
		global::UnityEngine.GameObject.Find("BGM_Title").GetComponent<UnityEngine.AudioSource>().volume = global::UnityEngine.PlayerPrefs.GetFloat("MusicVolume");
		global::UnityEngine.GameObject.Find("BGM_Title").GetComponent<UnityEngine.AudioSource>().Play();
	}

	private void Title_Font()
	{
		text_Timer += global::UnityEngine.Time.deltaTime;
		if (text_Timer > 3.5f)
		{
			Title_Font_I.color = global::UnityEngine.Color.Lerp(Title_Font_I.color, new global::UnityEngine.Color(1f, 1f, 1f, 0.3f), global::UnityEngine.Time.deltaTime * 0.5f);
			Title_Font_V.color = global::UnityEngine.Color.Lerp(Title_Font_V.color, new global::UnityEngine.Color(1f, 1f, 1f, 0.3f), global::UnityEngine.Time.deltaTime * 0.25f);
			Title_Font_E.color = global::UnityEngine.Color.Lerp(Title_Font_E.color, new global::UnityEngine.Color(1f, 1f, 1f, 0.3f), global::UnityEngine.Time.deltaTime * 0.1f);
			Title_Font_I_a.color = global::UnityEngine.Color.Lerp(Title_Font_I_a.color, new global::UnityEngine.Color(1f, 1f, 1f, 0.25f), global::UnityEngine.Time.deltaTime * 0.5f);
			Title_Font_V_a.color = global::UnityEngine.Color.Lerp(Title_Font_V_a.color, new global::UnityEngine.Color(1f, 1f, 1f, 0.25f), global::UnityEngine.Time.deltaTime * 0.25f);
			Title_Font_E_a.color = global::UnityEngine.Color.Lerp(Title_Font_E_a.color, new global::UnityEngine.Color(1f, 1f, 1f, 0.25f), global::UnityEngine.Time.deltaTime * 0.1f);
			textPos_Timer += global::UnityEngine.Time.deltaTime;
			if (textPos_Timer > 0.08f)
			{
				textPos_Timer = 0f;
				Title_Font_I_a.GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(pos_Font[3].x + global::UnityEngine.Random.Range(-2.5f, 2.5f), pos_Font[3].y + global::UnityEngine.Random.Range(-2f, 2f), 0f);
				Title_Font_V_a.GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(pos_Font[4].x + global::UnityEngine.Random.Range(-2.5f, 2.5f), pos_Font[4].y + global::UnityEngine.Random.Range(-2f, 2f), 0f);
				Title_Font_E_a.GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(pos_Font[5].x + global::UnityEngine.Random.Range(-2.5f, 2.5f), pos_Font[5].y + global::UnityEngine.Random.Range(-2f, 2f), 0f);
			}
		}
		if (text_Timer > 5f)
		{
			Title_Text.color = global::UnityEngine.Color.Lerp(Title_Text.color, new global::UnityEngine.Color(1f, 1f, 1f, 0.5f), global::UnityEngine.Time.deltaTime * 0.1f);
		}
		if (text_Timer > 8f)
		{
			Version_Text.color = global::UnityEngine.Color.Lerp(Version_Text.color, new global::UnityEngine.Color(1f, 1f, 1f, 0.5f), global::UnityEngine.Time.deltaTime * 0.1f);
		}
		Tail_1.transform.localScale = new global::UnityEngine.Vector3(1.4f + global::UnityEngine.Mathf.Sin(text_Timer * 1f) * 0.01f, 1.4f + global::UnityEngine.Mathf.Sin(text_Timer * 1f) * 0.02f, 1f);
		Tail_2.transform.localScale = new global::UnityEngine.Vector3(1.5f + global::UnityEngine.Mathf.Sin(text_Timer * 0.7f) * 0.01f, 1.5f + global::UnityEngine.Mathf.Sin(text_Timer * 0.7f) * 0.02f, 1f);
		Tail_3.transform.localScale = new global::UnityEngine.Vector3(1.6f + global::UnityEngine.Mathf.Sin(text_Timer * 0.5f) * 0.01f, 1.6f + global::UnityEngine.Mathf.Sin(text_Timer * 0.5f) * 0.02f, 1f);
		Tail_4.transform.localScale = new global::UnityEngine.Vector3(1.4f + global::UnityEngine.Mathf.Sin(text_Timer * 1.2f) * 0.01f, 1.4f + global::UnityEngine.Mathf.Sin(text_Timer * 1.2f) * 0.02f, 1f);
		SR_Glow_2.color = global::UnityEngine.Color.Lerp(SR_Glow_2.color, new global::UnityEngine.Color(SR_Glow_2.color.r, SR_Glow_2.color.g, SR_Glow_2.color.b, 0.5f), global::UnityEngine.Time.deltaTime * 0.1f);
	}

	private void Load_Data()
	{
		GetComponent<Save_Control>().Load_Game();
		Create_Slot();
		if (GetComponent<Save_Control>().SaveData.isSaved[0] || GetComponent<Save_Control>().SaveData.isSaved[1] || GetComponent<Save_Control>().SaveData.isSaved[2])
		{
			isSaved = true;
		}
		else
		{
			isSaved = false;
		}
		isLoaded = true;
		if (GetComponent<Save_Control>().SaveData.H_scene.Length > 60)
		{
			for (int i = 0; i < 30; i++)
			{
				if (GetComponent<Save_Control>().SaveData.H_scene[i] > 0)
				{
					global::UnityEngine.PlayerPrefs.SetInt("H_" + (i + 1), 1);
				}
			}
			for (int j = 50; j < 60; j++)
			{
				if (GetComponent<Save_Control>().SaveData.H_scene[j] > 0)
				{
					global::UnityEngine.PlayerPrefs.SetInt("H_" + (j + 1), 1);
				}
			}
		}
		for (int k = 0; k < 5; k++)
		{
			if (GetComponent<Save_Control>().SaveData.H_Over[k] > 0)
			{
				global::UnityEngine.PlayerPrefs.SetInt("H_GameOver_" + (k + 1), 1);
			}
		}
	}

	private void Save_Data()
	{
		bool[] array = GetComponent<Save_Control>().SaveData.isSaved;
		bool flag;
		GetComponent<Save_Control>().SaveData.isSaved[1] = (flag = (GetComponent<Save_Control>().SaveData.isSaved[2] = true));
		array[0] = flag;
		GetComponent<Save_Control>().Save_Game();
	}

	private void Reset_All()
	{
		global::UnityEngine.PlayerPrefs.DeleteAll();
		global::UnityEngine.Debug.Log("Reset_All");
	}

	private void Update()
	{
		Title_Font();
		if (!DataLoaded)
		{
			Load_Timer += global::UnityEngine.Time.deltaTime;
			if (Load_Timer > 0.2f)
			{
				DataLoaded = true;
				Load_Data();
			}
		}
		if (DelaySound_Timer > 0f)
		{
			DelaySound_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Menu_State >= 0)
		{
			if (Menu_State == 0)
			{
				Start_Timer += global::UnityEngine.Time.deltaTime;
				SelGlow_Opacity = 0.3f + (1f + global::UnityEngine.Mathf.Sin(Start_Timer * 3f)) * 0.2f;
				global::UnityEngine.GameObject.Find("Select_BG_Glow").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, SelGlow_Opacity);
				if (!global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.LeftAlt) && !global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.RightAlt) && !global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.Escape) && !global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.Tab) && global::UnityEngine.Input.anyKeyDown)
				{
					On_Menu();
					global::UnityEngine.GameObject.Find("Title_Info_Pad_1").SendMessage("On");
					global::UnityEngine.GameObject.Find("Title_Info_Key_1").SendMessage("On");
				}
			}
			else
			{
				if (Input_Delay > 0f)
				{
					Input_Delay -= global::UnityEngine.Time.deltaTime;
				}
				if (Bar_State == 1)
				{
					Ani_Close();
				}
				else
				{
					inputX = 0f;
					inputY = 0f;
					if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.RightArrow))
					{
						inputX = 1f;
					}
					else if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.LeftArrow))
					{
						inputX = -1f;
					}
					if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.UpArrow))
					{
						inputY = 1f;
					}
					else if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.DownArrow))
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
					if (inputY < 0f && Sel_Index < ((Menu_State != 1) ? 3 : 7))
					{
						if (Menu_State == 1 && Sel_Index == 1 && !isSaved)
						{
							Sel_Index = 3;
						}
						else
						{
							Sel_Index++;
						}
					}
					else if (inputY > 0f && Sel_Index > 1)
					{
						if (Menu_State == 1 && Sel_Index == 3 && !isSaved)
						{
							Sel_Index = 1;
						}
						else
						{
							Sel_Index--;
						}
					}
					else if (Sel_Index == 4)
					{
						if (inputX < 0f || inputX > 0f)
						{
							global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_Btn");
							Set_WindowMode();
						}
					}
					else if (Sel_Index == 5 && (inputX < 0f || inputX > 0f))
					{
						global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_Btn");
						Set_Language();
					}
					MousePos = global::UnityEngine.Input.mousePosition;
					MouseDist = global::UnityEngine.Vector3.Distance(MousePos, MousePosPrev);
					if (Input_Delay <= 0f && (MouseDist > 0f || global::UnityEngine.Input.GetMouseButtonDown(0)))
					{
						Check_Mouse();
					}
					if (Prev_Index != Sel_Index)
					{
						if (Sound_MoveFalse > 0)
						{
							Sound_MoveFalse--;
						}
						else if (DelaySound_Timer <= 0f)
						{
							DelaySound_Timer = 0.05f;
							global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_Move_1");
						}
						Target_Select();
					}
					global::UnityEngine.GameObject.Find("Select_Cursor").GetComponent<global::UnityEngine.RectTransform>().position = global::UnityEngine.Vector3.Lerp(global::UnityEngine.GameObject.Find("Select_Cursor").GetComponent<global::UnityEngine.RectTransform>().position, PosTarget, global::UnityEngine.Time.deltaTime * 16f);
					global::UnityEngine.GameObject.Find("Select_BG").GetComponent<global::UnityEngine.RectTransform>().position = global::UnityEngine.Vector3.Lerp(global::UnityEngine.GameObject.Find("Select_BG").GetComponent<global::UnityEngine.RectTransform>().position, PosTarget, global::UnityEngine.Time.deltaTime * 16f);
					global::UnityEngine.GameObject.Find("Select_BG_Glow").GetComponent<global::UnityEngine.RectTransform>().position = global::UnityEngine.Vector3.Lerp(global::UnityEngine.GameObject.Find("Select_BG_Glow").GetComponent<global::UnityEngine.RectTransform>().position, PosTarget, global::UnityEngine.Time.deltaTime * 16f);
					Cursor_Size();
					Ani_Open();
					if (SelBG_Opacity < 1f)
					{
						SelBG_Opacity = global::UnityEngine.Mathf.Lerp(SelBG_Opacity, 1f, global::UnityEngine.Time.deltaTime * 2f);
						global::UnityEngine.GameObject.Find("Select_BG").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, SelBG_Opacity);
					}
					if (SelGlow_Opacity > 0f)
					{
						SelGlow_Opacity = global::UnityEngine.Mathf.Lerp(SelGlow_Opacity, 0f, global::UnityEngine.Time.deltaTime * 10f);
						global::UnityEngine.GameObject.Find("Select_BG_Glow").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, SelGlow_Opacity);
					}
					if (Menu_State == 1)
					{
						if (Sel_Index == 4)
						{
							SelArrow_Opacity[0] = global::UnityEngine.Mathf.Lerp(SelArrow_Opacity[0], 0.9f, global::UnityEngine.Time.deltaTime * 5f);
							global::UnityEngine.GameObject.Find("Arrow_L_4").GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, SelArrow_Opacity[0]);
							global::UnityEngine.GameObject.Find("Arrow_R_4").GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, SelArrow_Opacity[0]);
						}
						else if (SelArrow_Opacity[0] > 0.1f)
						{
							SelArrow_Opacity[0] = global::UnityEngine.Mathf.Lerp(SelArrow_Opacity[0], 0.1f, global::UnityEngine.Time.deltaTime * 5f);
							global::UnityEngine.GameObject.Find("Arrow_L_4").GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, SelArrow_Opacity[0]);
							global::UnityEngine.GameObject.Find("Arrow_R_4").GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, SelArrow_Opacity[0]);
						}
						if (Sel_Index == 5)
						{
							SelArrow_Opacity[1] = global::UnityEngine.Mathf.Lerp(SelArrow_Opacity[1], 0.9f, global::UnityEngine.Time.deltaTime * 5f);
							global::UnityEngine.GameObject.Find("Arrow_L_5").GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, SelArrow_Opacity[1]);
							global::UnityEngine.GameObject.Find("Arrow_R_5").GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, SelArrow_Opacity[1]);
						}
						else if (SelArrow_Opacity[1] > 0.1f)
						{
							SelArrow_Opacity[1] = global::UnityEngine.Mathf.Lerp(SelArrow_Opacity[1], 0.1f, global::UnityEngine.Time.deltaTime * 5f);
							global::UnityEngine.GameObject.Find("Arrow_L_5").GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, SelArrow_Opacity[1]);
							global::UnityEngine.GameObject.Find("Arrow_R_5").GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, SelArrow_Opacity[1]);
						}
					}
					if (global::UnityEngine.Input.GetAxis("L_X") != 0f || global::UnityEngine.Input.GetAxis("L_Y") != 0f || global::UnityEngine.Input.GetButtonDown("Jump") || global::UnityEngine.Input.GetButtonDown("_B") || global::UnityEngine.Input.GetButtonDown("_X") || global::UnityEngine.Input.GetButtonDown("_Y") || global::UnityEngine.Input.GetButtonDown("L_B") || global::UnityEngine.Input.GetButtonDown("R_B") || global::UnityEngine.Input.GetAxis("L_Trigger") != 0f || global::UnityEngine.Input.GetButtonDown("Start") || global::UnityEngine.Input.GetButtonDown("Back") || global::UnityEngine.Input.GetAxis("DPad_X") != 0f || global::UnityEngine.Input.GetAxis("DPad_Y") != 0f)
					{
						Info_Pad_On();
					}
					else if (global::UnityEngine.Input.anyKeyDown)
					{
						Info_Key_On();
					}
					if (Input_Delay <= 0f)
					{
						if (Menu_State == 1)
						{
							if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.Z) || global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.Return) || global::UnityEngine.Input.GetButtonDown("Jump"))
							{
								Select_Button(Sel_Index);
							}
						}
						else if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.Z) || global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.Return) || global::UnityEngine.Input.GetButtonDown("Jump"))
						{
							Select_Slot(Sel_Index);
						}
						else if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.X) || global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.Escape) || global::UnityEngine.Input.GetButtonDown("_B") || global::UnityEngine.Input.GetMouseButtonDown(1))
						{
							if (CopySlot_Num == 0)
							{
								global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_MenuOff");
								if (Save_Slot_1 != null)
								{
									Save_Slot_1.SendMessage("Off_Slot");
								}
								if (Save_Slot_2 != null)
								{
									Save_Slot_2.SendMessage("Off_Slot");
								}
								if (Save_Slot_3 != null)
								{
									Save_Slot_3.SendMessage("Off_Slot");
								}
								Set_Close();
								isNewGame = false;
							}
							else
							{
								Copy_Slot_Close();
							}
						}
						else if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.C) || global::UnityEngine.Input.GetButtonDown("_X"))
						{
							if (CopySlot_Num == Sel_Index)
							{
								Copy_Slot_Close();
							}
							else
							{
								Copy_Slot_Open(Sel_Index);
							}
						}
						else if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.V) || global::UnityEngine.Input.GetButtonDown("_Y"))
						{
							if (CopySlot_Num > 0)
							{
								Copy_Slot_Close();
							}
							else
							{
								Delete_Slot(Sel_Index);
							}
						}
					}
					Prev_Index = Sel_Index;
					prevX = global::UnityEngine.Input.GetAxis("L_X");
					lock_Y = false;
					prevY = global::UnityEngine.Input.GetAxis("L_Y");
					MousePosPrev = MousePos;
				}
			}
		}
		if (isFadeIn)
		{
			FadeOpacity -= global::UnityEngine.Time.deltaTime * 0.4f;
			if (FadeOpacity <= 0f)
			{
				isFadeIn = false;
				FadeOpacity = 0f;
				BlackFade.enabled = false;
				if (!isLoaded)
				{
					global::UnityEngine.GameObject.Find("Title_Info_SaveData").SendMessage("Info_Start");
				}
			}
			BlackFade.color = new global::UnityEngine.Color(0f, 0f, 0f, FadeOpacity);
		}
		else
		{
			if (!isFadeOut)
			{
				return;
			}
			FadeOpacity += global::UnityEngine.Time.deltaTime * 1f;
			if (FadeOpacity >= 1f)
			{
				isFadeOut = false;
				FadeOpacity = 1f;
				if (FadeOutAction == "New_Game")
				{
					New_Game();
				}
				else if (FadeOutAction == "Continue")
				{
					Continue();
				}
				else if (FadeOutAction == "Gallery")
				{
					Gallery();
				}
				else if (FadeOutAction == "GameOver")
				{
					GameOver();
				}
				else if (FadeOutAction == "Credit")
				{
					Credit();
				}
				else if (FadeOutAction == "Exit")
				{
					Exit();
				}
			}
			BlackFade.color = new global::UnityEngine.Color(0f, 0f, 0f, FadeOpacity);
			global::UnityEngine.GameObject.Find("BGM_Title").GetComponent<UnityEngine.AudioSource>().volume = global::UnityEngine.Mathf.Lerp(global::UnityEngine.GameObject.Find("BGM_Title").GetComponent<UnityEngine.AudioSource>().volume, 0f, global::UnityEngine.Time.deltaTime * 1.6f);
		}
	}

	private void Cursor_Size()
	{
		SelCursor_Timer += global::UnityEngine.Time.deltaTime;
		SelCursor_Size = 1f + (1f + global::UnityEngine.Mathf.Sin(SelCursor_Timer * 10f)) * 0.05f;
		if (global::UnityEngine.GameObject.Find("Select_Cursor").GetComponent<global::UnityEngine.RectTransform>().sizeDelta.x > 150f)
		{
			SelCursor_Size = 1f + (1f + global::UnityEngine.Mathf.Sin(SelCursor_Timer * 10f)) * 0.015f;
		}
		global::UnityEngine.GameObject.Find("Select_Cursor").GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(SelCursor_Size, SelCursor_Size, 1f);
	}

	private void Ani_Open()
	{
		global::UnityEngine.GameObject.Find("Dlg_Top").GetComponent<global::UnityEngine.RectTransform>().position = global::UnityEngine.Vector3.Lerp(global::UnityEngine.GameObject.Find("Dlg_Top").GetComponent<global::UnityEngine.RectTransform>().position, Pos_Open_Top, global::UnityEngine.Time.deltaTime * 12f);
		global::UnityEngine.GameObject.Find("Dlg_Bottom").GetComponent<global::UnityEngine.RectTransform>().position = global::UnityEngine.Vector3.Lerp(global::UnityEngine.GameObject.Find("Dlg_Bottom").GetComponent<global::UnityEngine.RectTransform>().position, Pos_Open_Bot, global::UnityEngine.Time.deltaTime * 12f);
		Menu_Opacity = global::UnityEngine.Mathf.Lerp(Menu_Opacity, 0.73f, global::UnityEngine.Time.deltaTime * 1f);
		SelCursor_Opacity = global::UnityEngine.Mathf.Lerp(SelCursor_Opacity, 1f, global::UnityEngine.Time.deltaTime * 5f);
		if (Menu_State == 1)
		{
			if (SelArrow_Opacity[0] < 0.1f)
			{
				SelArrow_Opacity[0] = global::UnityEngine.Mathf.Lerp(SelArrow_Opacity[0], 0.1f, global::UnityEngine.Time.deltaTime * 2f);
			}
			if (SelArrow_Opacity[1] < 0.1f)
			{
				SelArrow_Opacity[1] = global::UnityEngine.Mathf.Lerp(SelArrow_Opacity[1], 0.1f, global::UnityEngine.Time.deltaTime * 2f);
			}
			Box_Opacity = global::UnityEngine.Mathf.Lerp(Box_Opacity, 0.04f, global::UnityEngine.Time.deltaTime * 1f);
			FontColor1.a = global::UnityEngine.Mathf.Lerp(FontColor1.a, 1f, global::UnityEngine.Time.deltaTime * 3f);
		}
		else if (Menu_State == 2)
		{
			Slot_Opacity = global::UnityEngine.Mathf.Lerp(Slot_Opacity, 0.04f, global::UnityEngine.Time.deltaTime * 1f);
			FontColor2.a = global::UnityEngine.Mathf.Lerp(FontColor2.a, 1f, global::UnityEngine.Time.deltaTime * 3f);
		}
		if (global::UnityEngine.GameObject.Find("Name_Start").GetComponent<global::UnityEngine.UI.Text>().color.a > 0f)
		{
			float num = global::UnityEngine.Mathf.Lerp(global::UnityEngine.GameObject.Find("Name_Start").GetComponent<global::UnityEngine.UI.Text>().color.a, 0f, global::UnityEngine.Time.deltaTime * 20f);
			if (num < 0.02f)
			{
				num = 0f;
			}
			global::UnityEngine.GameObject.Find("Name_Start").GetComponent<global::UnityEngine.UI.Text>().color = new global::UnityEngine.Color(FontColor1.r, FontColor1.g, FontColor1.b, num);
		}
		Ani_Opacity();
	}

	private void Ani_Close()
	{
		Delay_Timer += global::UnityEngine.Time.deltaTime;
		if (Delay_Timer > 0f)
		{
			global::UnityEngine.GameObject.Find("Dlg_Top").GetComponent<global::UnityEngine.RectTransform>().position = global::UnityEngine.Vector3.Lerp(global::UnityEngine.GameObject.Find("Dlg_Top").GetComponent<global::UnityEngine.RectTransform>().position, Pos_Close_Top, global::UnityEngine.Time.deltaTime * 8f);
			global::UnityEngine.GameObject.Find("Dlg_Bottom").GetComponent<global::UnityEngine.RectTransform>().position = global::UnityEngine.Vector3.Lerp(global::UnityEngine.GameObject.Find("Dlg_Bottom").GetComponent<global::UnityEngine.RectTransform>().position, Pos_Close_Bot, global::UnityEngine.Time.deltaTime * 8f);
			global::UnityEngine.GameObject.Find("Select_BG").GetComponent<global::UnityEngine.RectTransform>().position = global::UnityEngine.Vector3.Lerp(global::UnityEngine.GameObject.Find("Select_BG").GetComponent<global::UnityEngine.RectTransform>().position, Pos_Close_Orig, global::UnityEngine.Time.deltaTime * 8f);
			global::UnityEngine.GameObject.Find("Select_BG_Glow").GetComponent<global::UnityEngine.RectTransform>().position = global::UnityEngine.Vector3.Lerp(global::UnityEngine.GameObject.Find("Select_BG_Glow").GetComponent<global::UnityEngine.RectTransform>().position, Pos_Close_Orig, global::UnityEngine.Time.deltaTime * 8f);
			global::UnityEngine.GameObject.Find("Select_Cursor").GetComponent<global::UnityEngine.RectTransform>().position = global::UnityEngine.Vector3.Lerp(global::UnityEngine.GameObject.Find("Select_Cursor").GetComponent<global::UnityEngine.RectTransform>().position, Pos_Close_Orig, global::UnityEngine.Time.deltaTime * 8f);
			global::UnityEngine.GameObject.Find("Select_BG").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(344f, 74f);
			global::UnityEngine.GameObject.Find("Select_BG_Glow").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(358f, 90f);
			Menu_Opacity = global::UnityEngine.Mathf.Lerp(Menu_Opacity, 0f, global::UnityEngine.Time.deltaTime * 12f);
			SelCursor_Opacity = global::UnityEngine.Mathf.Lerp(SelCursor_Opacity, 0f, global::UnityEngine.Time.deltaTime * 12f);
			SelArrow_Opacity[0] = global::UnityEngine.Mathf.Lerp(SelArrow_Opacity[0], 0f, global::UnityEngine.Time.deltaTime * 12f);
			SelArrow_Opacity[1] = SelArrow_Opacity[0];
			Box_Opacity = global::UnityEngine.Mathf.Lerp(Box_Opacity, 0f, global::UnityEngine.Time.deltaTime * 16f);
			FontColor1.a = global::UnityEngine.Mathf.Lerp(FontColor1.a, 0f, global::UnityEngine.Time.deltaTime * 12f);
			Slot_Opacity = global::UnityEngine.Mathf.Lerp(Slot_Opacity, 0f, global::UnityEngine.Time.deltaTime * 16f);
			FontColor2.a = global::UnityEngine.Mathf.Lerp(FontColor2.a, 0f, global::UnityEngine.Time.deltaTime * 12f);
			CopyFontColor.a = global::UnityEngine.Mathf.Lerp(CopyFontColor.a, 0f, global::UnityEngine.Time.deltaTime * 12f);
			CopyBoxColor.a = global::UnityEngine.Mathf.Lerp(CopyBoxColor.a, 0f, global::UnityEngine.Time.deltaTime * 16f);
			float num = global::UnityEngine.Vector3.Distance(global::UnityEngine.GameObject.Find("Dlg_Top").GetComponent<global::UnityEngine.RectTransform>().position, Pos_Close_Top);
			if (num < 0.02f)
			{
				if (Menu_State == 1)
				{
					if (Sel_Index < 3)
					{
						On_Slot();
					}
					if (Input_Mode == 0)
					{
						Info_Key_On();
					}
					else
					{
						Info_Pad_On();
					}
				}
				else if (Menu_State == 2 && !isGameStart)
				{
					On_Menu();
					if (Input_Mode == 0)
					{
						Info_Key_On();
					}
					else
					{
						Info_Pad_On();
					}
				}
				Input_Delay = 0.2f;
			}
			Ani_Opacity();
		}
		SelGlow_Opacity = global::UnityEngine.Mathf.Lerp(SelGlow_Opacity, 0.3f, global::UnityEngine.Time.deltaTime * 2f);
		global::UnityEngine.GameObject.Find("Select_BG_Glow").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, SelGlow_Opacity);
	}

	private void Ani_Opacity()
	{
		global::UnityEngine.GameObject.Find("Dlg_BG").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, Menu_Opacity);
		global::UnityEngine.GameObject.Find("Select_Cursor").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, SelCursor_Opacity);
		for (int i = 1; i < 8; i++)
		{
			global::UnityEngine.GameObject.Find("Pos_Button_" + i).GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, Box_Opacity);
		}
		for (int j = 1; j < 4; j++)
		{
			global::UnityEngine.GameObject.Find("Pos_Slot_" + j).GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, Slot_Opacity);
		}
		global::UnityEngine.GameObject.Find("Name_NewGame").GetComponent<global::UnityEngine.UI.Text>().color = FontColor1;
		if (isSaved)
		{
			global::UnityEngine.GameObject.Find("Name_Continue").GetComponent<global::UnityEngine.UI.Text>().color = FontColor1;
		}
		else
		{
			global::UnityEngine.GameObject.Find("Name_Continue").GetComponent<global::UnityEngine.UI.Text>().color = new global::UnityEngine.Color(FontColor1.r, FontColor1.g, FontColor1.b, FontColor1.a * 0.25f);
		}
		global::UnityEngine.GameObject.Find("Name_Gallery").GetComponent<global::UnityEngine.UI.Text>().color = FontColor1;
		global::UnityEngine.GameObject.Find("Name_Window").GetComponent<global::UnityEngine.UI.Text>().color = FontColor1;
		global::UnityEngine.GameObject.Find("Name_Language").GetComponent<global::UnityEngine.UI.Text>().color = FontColor1;
		global::UnityEngine.GameObject.Find("Name_Credit").GetComponent<global::UnityEngine.UI.Text>().color = FontColor1;
		global::UnityEngine.GameObject.Find("Name_Exit").GetComponent<global::UnityEngine.UI.Text>().color = FontColor1;
		global::UnityEngine.GameObject.Find("Text_Window_Res").GetComponent<global::UnityEngine.UI.Text>().color = new global::UnityEngine.Color(FontColor1.r, FontColor1.g, FontColor1.b, FontColor1.a * 0.6f);
		global::UnityEngine.GameObject.Find("Text_Language").GetComponent<global::UnityEngine.UI.Text>().color = new global::UnityEngine.Color(FontColor1.r, FontColor1.g, FontColor1.b, FontColor1.a * 0.6f);
		global::UnityEngine.GameObject.Find("Arrow_L_4").GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, SelArrow_Opacity[0]);
		global::UnityEngine.GameObject.Find("Arrow_R_4").GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, SelArrow_Opacity[0]);
		global::UnityEngine.GameObject.Find("Arrow_L_5").GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, SelArrow_Opacity[1]);
		global::UnityEngine.GameObject.Find("Arrow_R_5").GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, SelArrow_Opacity[1]);
		global::UnityEngine.GameObject.Find("BG_Copy").GetComponent<global::UnityEngine.UI.Image>().color = CopyBoxColor;
		global::UnityEngine.GameObject.Find("Text_Copy").GetComponent<global::UnityEngine.UI.Text>().color = CopyFontColor;
		global::UnityEngine.GameObject.Find("Text_SlotInfo").GetComponent<global::UnityEngine.UI.Text>().color = FontColor2;
		for (int k = 1; k < 4; k++)
		{
			global::UnityEngine.GameObject.Find("Text_Slot_" + k).GetComponent<global::UnityEngine.UI.Text>().color = FontColor2;
			global::UnityEngine.GameObject.Find("Text_Empty_" + k).GetComponent<global::UnityEngine.UI.Text>().color = FontColor2;
		}
	}

	private void On_Menu()
	{
		Menu_State = 1;
		Sel_Index = 1;
		Prev_Index = 1;
		Bar_State = 2;
		for (int i = 1; i < 8; i++)
		{
			global::UnityEngine.GameObject.Find("Pos_Button_" + i).GetComponent<global::UnityEngine.BoxCollider2D>().enabled = true;
		}
		global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_MenuOn");
		Target_Select();
	}

	private void On_Slot()
	{
		if (Sel_Index == 1)
		{
			global::UnityEngine.GameObject.Find("Text_SlotInfo").GetComponent<global::UnityEngine.UI.Text>().text = "New Game";
		}
		else if (Sel_Index == 2)
		{
			global::UnityEngine.GameObject.Find("Text_SlotInfo").GetComponent<global::UnityEngine.UI.Text>().text = "Continue";
		}
		Menu_State = 2;
		Sel_Index = 1;
		Prev_Index = 1;
		Bar_State = 2;
		for (int i = 1; i < 4; i++)
		{
			global::UnityEngine.GameObject.Find("Pos_Slot_" + i).GetComponent<global::UnityEngine.BoxCollider2D>().enabled = true;
		}
		global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_MenuOn");
		Target_Select();
		if (Save_Slot_1 != null)
		{
			Save_Slot_1.SendMessage("On_Slot");
		}
		if (Save_Slot_2 != null)
		{
			Save_Slot_2.SendMessage("On_Slot");
		}
		if (Save_Slot_3 != null)
		{
			Save_Slot_3.SendMessage("On_Slot");
		}
	}

	private void Set_Close()
	{
		Bar_State = 1;
		Delay_Timer = 0f;
		for (int i = 1; i < 8; i++)
		{
			global::UnityEngine.GameObject.Find("Pos_Button_" + i).GetComponent<global::UnityEngine.BoxCollider2D>().enabled = false;
		}
		for (int j = 1; j < 4; j++)
		{
			global::UnityEngine.GameObject.Find("Pos_Slot_" + j).GetComponent<global::UnityEngine.BoxCollider2D>().enabled = false;
		}
	}

	private void Target_Select()
	{
		if (Menu_State == 1)
		{
			PosTarget = global::UnityEngine.GameObject.Find("Pos_Button_" + Sel_Index).GetComponent<global::UnityEngine.RectTransform>().position;
			global::UnityEngine.GameObject.Find("Select_Cursor").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(358f, 89f);
			global::UnityEngine.GameObject.Find("Select_BG").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(344f, 74f);
			global::UnityEngine.GameObject.Find("Select_BG_Glow").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(358f, 90f);
		}
		else if (Menu_State == 2)
		{
			PosTarget = global::UnityEngine.GameObject.Find("Pos_Slot_" + Sel_Index).GetComponent<global::UnityEngine.RectTransform>().position;
			global::UnityEngine.GameObject.Find("Select_Cursor").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(358f, 178f);
			global::UnityEngine.GameObject.Find("Select_BG").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(344f, 164f);
			global::UnityEngine.GameObject.Find("Select_BG_Glow").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(358f, 178f);
		}
		SelBG_Opacity = 0f;
		global::UnityEngine.GameObject.Find("Select_BG").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, SelBG_Opacity);
	}

	private void Check_Mouse()
	{
		if (global::UnityEngine.Input.GetMouseButtonDown(0))
		{
			global::UnityEngine.Ray ray = global::UnityEngine.GameObject.Find("UI Camera").GetComponent<UnityEngine.Camera>().ScreenPointToRay(global::UnityEngine.Input.mousePosition);
			global::UnityEngine.RaycastHit2D rayIntersection = global::UnityEngine.Physics2D.GetRayIntersection(ray, float.PositiveInfinity);
			if (rayIntersection.collider != null)
			{
				if (Menu_State == 1)
				{
					if (rayIntersection.collider.name == "Pos_Button_1")
					{
						Select_Button(1);
					}
					else if (rayIntersection.collider.name == "Pos_Button_2" && isSaved)
					{
						Select_Button(2);
					}
					else if (rayIntersection.collider.name == "Pos_Button_3")
					{
						Select_Button(3);
					}
					else if (rayIntersection.collider.name == "Pos_Button_4")
					{
						Select_Button(4);
					}
					else if (rayIntersection.collider.name == "Pos_Button_5")
					{
						Select_Button(5);
					}
					else if (rayIntersection.collider.name == "Pos_Button_6")
					{
						Select_Button(6);
					}
					else if (rayIntersection.collider.name == "Pos_Button_7")
					{
						Select_Button(7);
					}
				}
				else if (Menu_State == 2)
				{
					if (rayIntersection.collider.name == "Pos_Slot_1" && CopySlot_Num != 1)
					{
						Select_Slot(1);
					}
					else if (rayIntersection.collider.name == "Pos_Slot_2" && CopySlot_Num != 2)
					{
						Select_Slot(2);
					}
					else if (rayIntersection.collider.name == "Pos_Slot_3" && CopySlot_Num != 3)
					{
						Select_Slot(3);
					}
				}
			}
			global::UnityEngine.Ray ray2 = global::UnityEngine.GameObject.Find("Main Camera").GetComponent<UnityEngine.Camera>().ScreenPointToRay(global::UnityEngine.Input.mousePosition);
			global::UnityEngine.RaycastHit2D rayIntersection2 = global::UnityEngine.Physics2D.GetRayIntersection(ray2, float.PositiveInfinity);
			if (rayIntersection2.collider != null && rayIntersection2.collider.name == "Col_ClothOnOff")
			{
				global::UnityEngine.GameObject.Find("Title_Girl").SendMessage("OnOff_Cloth");
			}
			return;
		}
		global::UnityEngine.Ray ray3 = global::UnityEngine.GameObject.Find("UI Camera").GetComponent<UnityEngine.Camera>().ScreenPointToRay(global::UnityEngine.Input.mousePosition);
		global::UnityEngine.RaycastHit2D rayIntersection3 = global::UnityEngine.Physics2D.GetRayIntersection(ray3, float.PositiveInfinity);
		if (!(rayIntersection3.collider != null))
		{
			return;
		}
		if (Menu_State == 1)
		{
			if (rayIntersection3.collider.name.Length > 11 && rayIntersection3.collider.name.Substring(0, 11) == "Pos_Button_")
			{
				int num = int.Parse(rayIntersection3.collider.name.Substring(11, 1));
				if (num != Prev_Index && (num != 2 || (num == 2 && isSaved)))
				{
					Sel_Index = num;
					Info_Key_On();
				}
			}
		}
		else if (Menu_State == 2 && rayIntersection3.collider.name.Length > 9 && rayIntersection3.collider.name.Substring(0, 9) == "Pos_Slot_")
		{
			int num2 = int.Parse(rayIntersection3.collider.name.Substring(9, 1));
			if (num2 != Prev_Index)
			{
				Sel_Index = num2;
				Info_Key_On();
			}
		}
	}

	private void Select_Button(int num)
	{
		if (Sel_Index != num)
		{
			Sel_Index = num;
		}
		global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_Btn");
		if (Sel_Index != 4 && Sel_Index != 5)
		{
			Set_Close();
		}
		if (Sel_Index == 1)
		{
			isNewGame = true;
		}
		else if (Sel_Index == 2)
		{
			isNewGame = false;
		}
		else if (Sel_Index == 3)
		{
			Set_FadeOut("Gallery");
		}
		else if (Sel_Index == 4)
		{
			Set_WindowMode();
		}
		else if (Sel_Index == 5)
		{
			Set_Language();
		}
		else if (Sel_Index == 6)
		{
			Set_FadeOut("Credit");
		}
		else
		{
			Set_FadeOut("Exit");
		}
		SelGlow_Opacity = 1f;
	}

	private void Select_Slot(int num)
	{
		if (Sel_Index != num)
		{
			Sel_Index = num;
		}
		if (Sel_Index > 3)
		{
			Sel_Index = 3;
		}
		if (CopySlot_Num == 0)
		{
			global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_DeviceOn");
			global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_Btn");
			global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_MenuOn");
			if (isNewGame)
			{
				isGameStart = true;
				Set_Close();
				Set_FadeOut("New_Game");
				SelGlow_Opacity = 1f;
				if (Save_Slot_1 != null)
				{
					Save_Slot_1.SendMessage("Off_Slot");
				}
				if (Save_Slot_2 != null)
				{
					Save_Slot_2.SendMessage("Off_Slot");
				}
				if (Save_Slot_3 != null)
				{
					Save_Slot_3.SendMessage("Off_Slot");
				}
				GetComponent<Save_Control>().Delete_Data(Sel_Index - 1, true);
			}
			else if (GetComponent<Save_Control>().SaveData.isSaved[Sel_Index - 1])
			{
				isGameStart = true;
				Set_Close();
				Set_FadeOut("Continue");
				SelGlow_Opacity = 1f;
				if (Save_Slot_1 != null)
				{
					Save_Slot_1.SendMessage("Off_Slot");
				}
				if (Save_Slot_2 != null)
				{
					Save_Slot_2.SendMessage("Off_Slot");
				}
				if (Save_Slot_3 != null)
				{
					Save_Slot_3.SendMessage("Off_Slot");
				}
			}
			else
			{
				SelGlow_Opacity = 0.4f;
			}
		}
		else if (CopySlot_Num != Sel_Index)
		{
			GetComponent<Save_Control>().Copy_Data(CopySlot_Num - 1, Sel_Index - 1);
			Create_Slot();
			if (Sel_Index == 1)
			{
				Save_Slot_1.SendMessage("On_Slot");
			}
			else if (Sel_Index == 2)
			{
				Save_Slot_2.SendMessage("On_Slot");
			}
			else if (Sel_Index == 3)
			{
				Save_Slot_3.SendMessage("On_Slot");
			}
			global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_Tab");
			CopySlot_Num = 0;
			CopyFontColor.a = 0f;
			CopyBoxColor.a = 0f;
			global::UnityEngine.Debug.Log("Copy");
		}
		else
		{
			Copy_Slot_Close();
		}
	}

	private void Copy_Slot_Open(int num)
	{
		if (Sel_Index != num)
		{
			Sel_Index = num;
		}
		if (Sel_Index > 3)
		{
			Sel_Index = 3;
		}
		if (GetComponent<Save_Control>().SaveData.isSaved[Sel_Index - 1])
		{
			CopySlot_Num = Sel_Index;
			global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_Tab");
			CopyFontColor.a = 1f;
			CopyBoxColor.a = 1f;
			global::UnityEngine.GameObject.Find("Slot_Copy").GetComponent<global::UnityEngine.RectTransform>().position = global::UnityEngine.GameObject.Find("Pos_Slot_" + Sel_Index).GetComponent<global::UnityEngine.RectTransform>().position;
		}
	}

	private void Copy_Slot_Close()
	{
		global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_MenuOff");
		CopySlot_Num = 0;
		CopyFontColor.a = 0f;
		CopyBoxColor.a = 0f;
	}

	private void Create_Slot()
	{
		if (Save_Slot_1 == null && GetComponent<Save_Control>().SaveData.isSaved[0])
		{
			Save_Slot_1 = global::UnityEngine.Object.Instantiate(Slot_Obj) as global::UnityEngine.GameObject;
            //Save_Slot_1.GetComponent<global::UnityEngine.RectTransform>().parent = GetComponent<global::UnityEngine.RectTransform>();
            Save_Slot_1.GetComponent<global::UnityEngine.RectTransform>().SetParent(GetComponent<global::UnityEngine.RectTransform>(),false);
            Save_Slot_1.GetComponent<global::UnityEngine.RectTransform>().position = global::UnityEngine.GameObject.Find("Pos_Slot_1").GetComponent<global::UnityEngine.RectTransform>().position;
			Save_Slot_1.GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(1f, 1f, 1f);
			Save_Slot_1.GetComponent<Save_Slot>().Set_Slot(0);
			global::UnityEngine.GameObject.Find("Text_Empty_1").GetComponent<global::UnityEngine.UI.Text>().enabled = false;
		}
		if (Save_Slot_2 == null && GetComponent<Save_Control>().SaveData.isSaved[1])
		{
			Save_Slot_2 = global::UnityEngine.Object.Instantiate(Slot_Obj) as global::UnityEngine.GameObject;
            //Save_Slot_2.GetComponent<global::UnityEngine.RectTransform>().parent = GetComponent<global::UnityEngine.RectTransform>();
            Save_Slot_2.GetComponent<global::UnityEngine.RectTransform>().SetParent(GetComponent<global::UnityEngine.RectTransform>(),false);
            Save_Slot_2.GetComponent<global::UnityEngine.RectTransform>().position = global::UnityEngine.GameObject.Find("Pos_Slot_2").GetComponent<global::UnityEngine.RectTransform>().position;
			Save_Slot_2.GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(1f, 1f, 1f);
			Save_Slot_2.GetComponent<Save_Slot>().Set_Slot(1);
			global::UnityEngine.GameObject.Find("Text_Empty_2").GetComponent<global::UnityEngine.UI.Text>().enabled = false;
		}
		if (Save_Slot_3 == null && GetComponent<Save_Control>().SaveData.isSaved[2])
		{
			Save_Slot_3 = global::UnityEngine.Object.Instantiate(Slot_Obj) as global::UnityEngine.GameObject;
            //Save_Slot_3.GetComponent<global::UnityEngine.RectTransform>().parent = GetComponent<global::UnityEngine.RectTransform>();
            Save_Slot_3.GetComponent<global::UnityEngine.RectTransform>().SetParent(GetComponent<global::UnityEngine.RectTransform>(),false);
            Save_Slot_3.GetComponent<global::UnityEngine.RectTransform>().position = global::UnityEngine.GameObject.Find("Pos_Slot_3").GetComponent<global::UnityEngine.RectTransform>().position;
			Save_Slot_3.GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(1f, 1f, 1f);
			Save_Slot_3.GetComponent<Save_Slot>().Set_Slot(2);
			global::UnityEngine.GameObject.Find("Text_Empty_3").GetComponent<global::UnityEngine.UI.Text>().enabled = false;
		}
	}

	private void Delete_Slot(int num)
	{
		bool flag = false;
		if (num == 1 && GetComponent<Save_Control>().SaveData.isSaved[0])
		{
			if (Save_Slot_1.GetComponent<Save_Slot>().Delete_Slot())
			{
				global::UnityEngine.GameObject.Find("Text_Empty_1").GetComponent<global::UnityEngine.UI.Text>().enabled = true;
				GetComponent<Save_Control>().Delete_Data(num - 1, false);
				global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_Tab");
				flag = true;
			}
		}
		else if (num == 2 && GetComponent<Save_Control>().SaveData.isSaved[1])
		{
			if (Save_Slot_2.GetComponent<Save_Slot>().Delete_Slot())
			{
				global::UnityEngine.GameObject.Find("Text_Empty_2").GetComponent<global::UnityEngine.UI.Text>().enabled = true;
				GetComponent<Save_Control>().Delete_Data(num - 1, false);
				global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_Tab");
				flag = true;
			}
		}
		else if (num == 3 && GetComponent<Save_Control>().SaveData.isSaved[2] && Save_Slot_3.GetComponent<Save_Slot>().Delete_Slot())
		{
			global::UnityEngine.GameObject.Find("Text_Empty_3").GetComponent<global::UnityEngine.UI.Text>().enabled = true;
			GetComponent<Save_Control>().Delete_Data(num - 1, false);
			global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_Tab");
			flag = true;
		}
		if (flag)
		{
			if (GetComponent<Save_Control>().SaveData.isSaved[0] || GetComponent<Save_Control>().SaveData.isSaved[1] || GetComponent<Save_Control>().SaveData.isSaved[2])
			{
				isSaved = true;
			}
			else
			{
				isSaved = false;
			}
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(InfoDel_Obj) as global::UnityEngine.GameObject;
            //gameObject.GetComponent<global::UnityEngine.RectTransform>().parent = GetComponent<global::UnityEngine.RectTransform>();
            gameObject.GetComponent<global::UnityEngine.RectTransform>().SetParent(GetComponent<global::UnityEngine.RectTransform>(),false);
            gameObject.GetComponent<global::UnityEngine.RectTransform>().position = global::UnityEngine.GameObject.Find("Pos_InfoSlot" + num).GetComponent<global::UnityEngine.RectTransform>().position;
			gameObject.GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(1f, 1f, 1f);
			gameObject.GetComponent<Info_DeletedSlot>().Set_Start("Slot " + num + GetComponent<Language_MenuItem>().TitleMenu(5, Language_Num));
		}
	}

	private void New_Game()
	{
		global::UnityEngine.GameObject.Find("Main Camera").GetComponent<TitleCam_Control>().Save_Avg_Fps();
		global::UnityEngine.PlayerPrefs.SetInt("SelBGM", 0);
		global::UnityEngine.PlayerPrefs.SetInt("Slot_Num", Sel_Index - 1);
		StartCoroutine("Load_Main");
	}

	private void Continue()
	{
		global::UnityEngine.GameObject.Find("Main Camera").GetComponent<TitleCam_Control>().Save_Avg_Fps();
		global::UnityEngine.PlayerPrefs.SetInt("Slot_Num", Sel_Index - 1);
		StartCoroutine("Load_Main");
	}

	private void Gallery()
	{
		StartCoroutine("Load_Gallery");
	}

	private void GameOver()
	{
		global::UnityEngine.Application.LoadLevel("GameOver");
	}

	private void Credit()
	{
		global::UnityEngine.Application.LoadLevel("Credits");
	}

	private void Set_WindowMode()
	{
		if (Window_Size == 1920)
		{
			Window_Size = 1280;
			global::UnityEngine.Screen.SetResolution(1280, 720, false);
		}
		else
		{
			Window_Size = 1920;
			global::UnityEngine.Screen.SetResolution(1920, 1080, true);
		}
		Text_WindowMode();
		global::UnityEngine.PlayerPrefs.SetInt("WindowSize", Window_Size);
	}

	private void Set_Language()
	{
		if (Language_Num == 0)
		{
			Language_Num = 1;
		}
		else
		{
			Language_Num = 0;
		}
		Text_Language();
		global::UnityEngine.PlayerPrefs.SetInt("Language_Num", Language_Num);
	}

	private void Exit()
	{
		global::UnityEngine.Application.Quit();
	}

	private void Text_WindowMode()
	{
		if (Window_Size == 1920)
		{
			global::UnityEngine.GameObject.Find("Name_Window").GetComponent<global::UnityEngine.UI.Text>().text = "1920 x 1080   Full Screen";
		}
		else
		{
			global::UnityEngine.GameObject.Find("Name_Window").GetComponent<global::UnityEngine.UI.Text>().text = "1280 x 720    Window Mode";
		}
	}

	private void Text_Language()
	{
		global::UnityEngine.GameObject.Find("Name_NewGame").GetComponent<global::UnityEngine.UI.Text>().text = GetComponent<Language_MenuItem>().TitleMenu(0, Language_Num);
		global::UnityEngine.GameObject.Find("Name_Continue").GetComponent<global::UnityEngine.UI.Text>().text = GetComponent<Language_MenuItem>().TitleMenu(1, Language_Num);
		global::UnityEngine.GameObject.Find("Name_Gallery").GetComponent<global::UnityEngine.UI.Text>().text = GetComponent<Language_MenuItem>().TitleMenu(2, Language_Num);
		global::UnityEngine.GameObject.Find("Name_Language").GetComponent<global::UnityEngine.UI.Text>().text = GetComponent<Language_MenuItem>().TitleMenu(3, Language_Num);
		global::UnityEngine.GameObject.Find("Name_Exit").GetComponent<global::UnityEngine.UI.Text>().text = GetComponent<Language_MenuItem>().TitleMenu(4, Language_Num);
	}

	private void Info_Pad_On()
	{
		Input_Mode = 1;
		if (Menu_State == 1)
		{
			global::UnityEngine.GameObject.Find("Title_Info_Pad_1").SendMessage("On");
			global::UnityEngine.GameObject.Find("Title_Info_Key_1").SendMessage("Off");
			global::UnityEngine.GameObject.Find("Title_Info_Pad_2").SendMessage("Off");
			global::UnityEngine.GameObject.Find("Title_Info_Key_2").SendMessage("Off");
		}
		else
		{
			global::UnityEngine.GameObject.Find("Title_Info_Pad_1").SendMessage("Off");
			global::UnityEngine.GameObject.Find("Title_Info_Key_1").SendMessage("Off");
			global::UnityEngine.GameObject.Find("Title_Info_Pad_2").SendMessage("On");
			global::UnityEngine.GameObject.Find("Title_Info_Key_2").SendMessage("Off");
		}
	}

	private void Info_Key_On()
	{
		Input_Mode = 0;
		if (Menu_State == 1)
		{
			global::UnityEngine.GameObject.Find("Title_Info_Pad_1").SendMessage("Off");
			global::UnityEngine.GameObject.Find("Title_Info_Key_1").SendMessage("On");
			global::UnityEngine.GameObject.Find("Title_Info_Key_1").SendMessage("Reset_Pos");
			global::UnityEngine.GameObject.Find("Title_Info_Pad_2").SendMessage("Off");
			global::UnityEngine.GameObject.Find("Title_Info_Key_2").SendMessage("Off");
		}
		else
		{
			global::UnityEngine.GameObject.Find("Title_Info_Pad_1").SendMessage("Off");
			global::UnityEngine.GameObject.Find("Title_Info_Key_1").SendMessage("Off");
			global::UnityEngine.GameObject.Find("Title_Info_Pad_2").SendMessage("Off");
			global::UnityEngine.GameObject.Find("Title_Info_Key_2").SendMessage("On");
		}
	}

	private global::System.Collections.IEnumerator Load_Main()
	{
		global::UnityEngine.AsyncOperation async = global::UnityEngine.Application.LoadLevelAsync("Main");
		global::UnityEngine.GameObject.Find("pos_Loading_Bar").GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(0f, 0f, 0f);
		global::UnityEngine.UI.Image Loading_Bar = global::UnityEngine.GameObject.Find("Loading_Bar").GetComponent<global::UnityEngine.UI.Image>();
		while (!async.isDone)
		{
			Loading_Bar.fillAmount = async.progress;
			yield return true;
		}
	}

	private global::System.Collections.IEnumerator Load_Gallery()
	{
		global::UnityEngine.AsyncOperation async = global::UnityEngine.Application.LoadLevelAsync("Gallery");
		global::UnityEngine.GameObject.Find("pos_Loading_Bar").GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(0f, 0f, 0f);
		while (!async.isDone)
		{
			global::UnityEngine.GameObject.Find("Loading_Bar").GetComponent<global::UnityEngine.UI.Image>().fillAmount = async.progress;
			yield return true;
		}
	}

	private global::System.Collections.IEnumerator Load_GameOver()
	{
		global::UnityEngine.AsyncOperation async = global::UnityEngine.Application.LoadLevelAsync("GameOver");
		global::UnityEngine.GameObject.Find("pos_Loading_Bar").GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(0f, 0f, 0f);
		while (!async.isDone)
		{
			global::UnityEngine.GameObject.Find("Loading_Bar").GetComponent<global::UnityEngine.UI.Image>().fillAmount = async.progress;
			yield return true;
		}
	}

	private void Set_FadeIn()
	{
		isFadeIn = true;
		isFadeOut = false;
		FadeOpacity = 1f;
		if (!BlackFade.enabled)
		{
			BlackFade.enabled = true;
			BlackFade.color = new global::UnityEngine.Color(0f, 0f, 0f, 1f);
		}
		FadeOutAction = string.Empty;
	}

	public void Set_FadeOut(string fadeoutaction)
	{
		isFadeOut = true;
		isFadeIn = false;
		if (!BlackFade.enabled)
		{
			FadeOpacity = 0f;
			BlackFade.enabled = true;
			BlackFade.color = new global::UnityEngine.Color(0f, 0f, 0f, 0f);
		}
		FadeOutAction = fadeoutaction;
	}
}
