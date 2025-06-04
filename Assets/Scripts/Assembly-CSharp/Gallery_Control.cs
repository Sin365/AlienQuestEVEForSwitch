public class Gallery_Control : global::UnityEngine.MonoBehaviour
{
	public int State = 1;

	public int Sel_Index = 1;

	private int Prev_Index = 1;

	private bool onList;

	private int Sel_Slot;

	private int List_Slot;

	public int[] Slot_isOpen = new int[5];

	public int Slot_Sum;

	private int Sel_GameOver = 1;

	private bool onSpeedMouseDown;

	public bool onMouseDrag;

	private bool onButtonDown;

	private bool onHoldStart;

	private float Hold_Timer;

	public global::UnityEngine.RectTransform Ani_List;

	public H_Slot[] Slot;

	public global::UnityEngine.RectTransform Cursor;

	public global::UnityEngine.RectTransform Cursor_BG;

	public global::UnityEngine.RectTransform SelBorder_Main;

	public global::UnityEngine.RectTransform SelBorder_Option;

	public global::UnityEngine.RectTransform SelBorder_Ani;

	public global::UnityEngine.GameObject Cursor_Object;

	public global::UnityEngine.RectTransform GameOver_Menu;

	public global::UnityEngine.RectTransform SelBorder_GameOver;

	public global::UnityEngine.GameObject[] Gallery_BG;

	private float SelBG_Opacity = 1f;

	private float Cursor_Timer;

	private float Cursor_Size = 1f;

	private global::UnityEngine.Vector3 PosTarget;

	private int onAllPlayStop;

	private float DelaySound_Timer = 0.8f;

	private float SoundVolume_Timer = 0.8f;

	private int CursorObject_Num;

	private float CursorObjcet_Timer;

	private global::UnityEngine.Color color_SlotGlowON = new global::UnityEngine.Color(1f, 1f, 1f, 0.2f);

	private global::UnityEngine.Color color_SlotBoxOFF = new global::UnityEngine.Color(1f, 1f, 1f, 0.05f);

	private global::UnityEngine.Color color_ON = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_OFF = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	private int FadeMode;

	private bool isFadeIn = true;

	private bool isFadeOut;

	private float FadeOpacity = 1f;

	private string FadeOutAction = string.Empty;

	private global::UnityEngine.GameObject BlackFade;

	private global::UnityEngine.GameObject BlackFade_Mode;

	private void Start()
	{
		BlackFade = global::UnityEngine.GameObject.Find("BlackFade");
		BlackFade_Mode = global::UnityEngine.GameObject.Find("BlackFade_Mode");
		BlackFade.GetComponent<global::UnityEngine.SpriteRenderer>().enabled = true;
		BlackFade_Mode.GetComponent<global::UnityEngine.UI.Image>().enabled = false;
		global::UnityEngine.Physics2D.IgnoreLayerCollision(25, 25);
		Set_Sound(0, AxiPlayerPrefs.GetFloat("SoundVolume"));
		PosTarget = global::UnityEngine.GameObject.Find("Btn_Gallery").GetComponent<global::UnityEngine.RectTransform>().position;
		Ani_List.localPosition = new global::UnityEngine.Vector3(0f, -1500f, 0f);
		GameOver_Menu.localPosition = new global::UnityEngine.Vector3(0f, -3000f, 0f);
		LoadData();
		if (Slot_isOpen[0] > 0 || Slot_isOpen[4] > 0)
		{
			global::UnityEngine.GameObject.Find("Main Camera").GetComponent<UnityEngine.Camera>().orthographicSize = 5f;
			global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Gallery_Camera>().targetSize = 7.6f;
		}
		AxiPlayerPrefs.SetInt("Gallery_Grayscale", 0);
		AxiPlayerPrefs.SetInt("Gallery_Grayscale_GameOver", 0);
		global::UnityEngine.GameObject.Find("Selected_Grayscale").GetComponent<global::UnityEngine.UI.Image>().enabled = false;
		if (AxiPlayerPrefs.GetInt("Gallery_Option") > 0)
		{
			SelBorder_Option.localPosition = global::UnityEngine.GameObject.Find("Btn_Option_" + AxiPlayerPrefs.GetInt("Gallery_Option")).GetComponent<global::UnityEngine.RectTransform>().localPosition;
		}
		else
		{
			AxiPlayerPrefs.SetInt("Gallery_Option", 1);
		}
		if (AxiPlayerPrefs.GetInt("Gallery_Option_GameOver") == 0)
		{
			AxiPlayerPrefs.SetInt("Gallery_Option_GameOver", 1);
		}
		for (int i = 0; i < 4; i++)
		{
			if (AxiPlayerPrefs.GetInt("Gallery_Option") == i + 1)
			{
				Gallery_BG[i].transform.position = new global::UnityEngine.Vector3(0f, 0f, 0f);
			}
			else
			{
				Gallery_BG[i].transform.position = new global::UnityEngine.Vector3(100f, -100f, 0f);
			}
		}
		Gallery_Option_CamSetting();
	}

	private void LoadData()
	{
		for (int i = 1; i < 6; i++)
		{
			Slot_isOpen[i - 1] = AxiPlayerPrefs.GetInt("G_Slot_isOpen_" + i);
			if (Slot_isOpen[i - 1] > 0)
			{
				Slot_Open(i);
			}
		}
		for (int j = 1; j < 61; j++)
		{
			if (global::UnityEngine.GameObject.Find("Box_Ani_" + j) != null && AxiPlayerPrefs.GetInt("H_" + j) == 1)
			{
				global::UnityEngine.GameObject.Find("Box_Ani_" + j).GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
			}
		}
		for (int k = 1; k < 6; k++)
		{
			if (global::UnityEngine.GameObject.Find("Box_GameOver_" + k) != null && AxiPlayerPrefs.GetInt("H_GameOver_" + k) == 1)
			{
				global::UnityEngine.GameObject.Find("Box_GameOver_" + k).GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
			}
		}
	}

	private void Cheat()
	{
		global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_DeviceOn");
		AxiPlayerPrefs.SetInt("H_1", 1);
		AxiPlayerPrefs.SetInt("H_2", 1);
		AxiPlayerPrefs.SetInt("H_3", 1);
		AxiPlayerPrefs.SetInt("H_4", 1);
		AxiPlayerPrefs.SetInt("H_5", 1);
		AxiPlayerPrefs.SetInt("H_6", 1);
		AxiPlayerPrefs.SetInt("H_7", 1);
		AxiPlayerPrefs.SetInt("H_8", 1);
		AxiPlayerPrefs.SetInt("H_9", 1);
		AxiPlayerPrefs.SetInt("H_10", 1);
		AxiPlayerPrefs.SetInt("H_11", 1);
		AxiPlayerPrefs.SetInt("H_12", 1);
		AxiPlayerPrefs.SetInt("H_13", 1);
		AxiPlayerPrefs.SetInt("H_14", 1);
		AxiPlayerPrefs.SetInt("H_15", 1);
		AxiPlayerPrefs.SetInt("H_16", 1);
		AxiPlayerPrefs.SetInt("H_17", 1);
		AxiPlayerPrefs.SetInt("H_18", 1);
		AxiPlayerPrefs.SetInt("H_19", 1);
		AxiPlayerPrefs.SetInt("H_20", 1);
		AxiPlayerPrefs.SetInt("H_21", 1);
		AxiPlayerPrefs.SetInt("H_22", 1);
		AxiPlayerPrefs.SetInt("H_23", 1);
		AxiPlayerPrefs.SetInt("H_24", 1);
		AxiPlayerPrefs.SetInt("H_25", 1);
		AxiPlayerPrefs.SetInt("H_26", 1);
		AxiPlayerPrefs.SetInt("H_27", 1);
		AxiPlayerPrefs.SetInt("H_28", 1);
		AxiPlayerPrefs.SetInt("H_29", 1);
		AxiPlayerPrefs.SetInt("H_30", 1);
		AxiPlayerPrefs.SetInt("H_31", 1);
		AxiPlayerPrefs.SetInt("H_32", 1);
		AxiPlayerPrefs.SetInt("H_33", 1);
		AxiPlayerPrefs.SetInt("H_34", 1);
		AxiPlayerPrefs.SetInt("H_35", 1);
		AxiPlayerPrefs.SetInt("H_36", 1);
		AxiPlayerPrefs.SetInt("H_37", 1);
		AxiPlayerPrefs.SetInt("H_38", 1);
		AxiPlayerPrefs.SetInt("H_39", 1);
		AxiPlayerPrefs.SetInt("H_40", 1);
		AxiPlayerPrefs.SetInt("H_41", 1);
		AxiPlayerPrefs.SetInt("H_42", 1);
		AxiPlayerPrefs.SetInt("H_43", 1);
		AxiPlayerPrefs.SetInt("H_51", 1);
		AxiPlayerPrefs.SetInt("H_52", 1);
		AxiPlayerPrefs.SetInt("H_53", 1);
		AxiPlayerPrefs.SetInt("H_54", 1);
		AxiPlayerPrefs.SetInt("H_55", 1);
		AxiPlayerPrefs.SetInt("H_GameOver_1", 1);
		AxiPlayerPrefs.SetInt("H_GameOver_2", 1);
		AxiPlayerPrefs.SetInt("H_GameOver_3", 1);
		AxiPlayerPrefs.SetInt("H_GameOver_4", 1);
		AxiPlayerPrefs.SetInt("H_GameOver_5", 1);
		global::UnityEngine.GameObject.Find("Box_Ani_1").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_2").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_3").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_4").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_5").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_6").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_7").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_8").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_9").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_10").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_11").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_12").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_13").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_14").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_15").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_16").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_17").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_18").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_19").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_20").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_21").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_22").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_23").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_24").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_25").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_26").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_27").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_28").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_29").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_30").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_31").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_32").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_33").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_34").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_35").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_36").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_37").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_38").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_39").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_40").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_41").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_42").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_43").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_51").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_52").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_53").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_54").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
		global::UnityEngine.GameObject.Find("Box_Ani_55").GetComponent<global::UnityEngine.UI.Image>().color = color_ON;
	}

	private void Update()
	{
		if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.F1))
		{
			Cheat();
		}
		if (State == 1 && global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.Backspace))
		{
			global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_Tab");
			Delete_All();
		}
		Slot_Sum = Slot_isOpen[0] + Slot_isOpen[1] + Slot_isOpen[2] + Slot_isOpen[3] + Slot_isOpen[4];
		if (DelaySound_Timer > 0f)
		{
			DelaySound_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (SoundVolume_Timer > 0f)
		{
			SoundVolume_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (global::UnityEngine.Input.mousePosition.x > (float)global::UnityEngine.Screen.width || global::UnityEngine.Input.mousePosition.x < 0f || global::UnityEngine.Input.mousePosition.y > (float)global::UnityEngine.Screen.height || global::UnityEngine.Input.mousePosition.y < 0f)
		{
			if (onMouseDrag)
			{
				onMouseDrag = false;
				global::UnityEngine.GameObject.Find("Main Camera").SendMessage("Release_TargetPos");
			}
		}
		else
		{
			if (State > 0 && !isFadeOut)
			{
				Check_Mouse();
			}
			if (Sel_Index >= 0 && Prev_Index != Sel_Index)
			{
				if (DelaySound_Timer <= 0f)
				{
					DelaySound_Timer = 0.05f;
					global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_Move_1");
				}
				Target_Select();
			}
		}
		Cursor_Timer += global::UnityEngine.Time.deltaTime;
		Cursor_Size = 1f + (1f + global::UnityEngine.Mathf.Sin(Cursor_Timer * 10f)) * 0.05f;
		if (Cursor.sizeDelta.x > 150f)
		{
			Cursor_Size = 1f + (1f + global::UnityEngine.Mathf.Sin(Cursor_Timer * 10f)) * 0.015f;
		}
		Cursor.localScale = new global::UnityEngine.Vector3(Cursor_Size, Cursor_Size, 1f);
		Cursor.position = global::UnityEngine.Vector3.Lerp(Cursor.position, PosTarget, global::UnityEngine.Time.deltaTime * 16f);
		if (Sel_Index < 0)
		{
			Cursor.GetComponent<global::UnityEngine.UI.Image>().color = global::UnityEngine.Color.Lerp(Cursor.GetComponent<global::UnityEngine.UI.Image>().color, color_OFF, global::UnityEngine.Time.deltaTime * 5f);
			Cursor_BG.GetComponent<global::UnityEngine.UI.Image>().color = Cursor.GetComponent<global::UnityEngine.UI.Image>().color;
		}
		else
		{
			Cursor.GetComponent<global::UnityEngine.UI.Image>().color = global::UnityEngine.Color.Lerp(Cursor.GetComponent<global::UnityEngine.UI.Image>().color, color_ON, global::UnityEngine.Time.deltaTime * 5f);
			Cursor_BG.GetComponent<global::UnityEngine.UI.Image>().color = Cursor.GetComponent<global::UnityEngine.UI.Image>().color;
		}
		for (int i = 1; i < 6; i++)
		{
			if (Slot_isOpen[i - 1] > 0)
			{
				if (Sel_Slot == i)
				{
					global::UnityEngine.GameObject.Find("Slot_Glow_" + i).GetComponent<global::UnityEngine.UI.Image>().color = global::UnityEngine.Color.Lerp(global::UnityEngine.GameObject.Find("Slot_Glow_" + i).GetComponent<global::UnityEngine.UI.Image>().color, color_SlotGlowON, global::UnityEngine.Time.deltaTime * 5f);
				}
				else
				{
					global::UnityEngine.GameObject.Find("Slot_Glow_" + i).GetComponent<global::UnityEngine.UI.Image>().color = global::UnityEngine.Color.Lerp(global::UnityEngine.GameObject.Find("Slot_Glow_" + i).GetComponent<global::UnityEngine.UI.Image>().color, color_OFF, global::UnityEngine.Time.deltaTime * 2f);
				}
			}
			else if (Sel_Slot == i)
			{
				global::UnityEngine.GameObject.Find("Slot_Empty_" + i).GetComponent<global::UnityEngine.UI.Image>().color = global::UnityEngine.Color.Lerp(global::UnityEngine.GameObject.Find("Slot_Empty_" + i).GetComponent<global::UnityEngine.UI.Image>().color, color_SlotGlowON, global::UnityEngine.Time.deltaTime * 5f);
			}
			else
			{
				global::UnityEngine.GameObject.Find("Slot_Empty_" + i).GetComponent<global::UnityEngine.UI.Image>().color = global::UnityEngine.Color.Lerp(global::UnityEngine.GameObject.Find("Slot_Empty_" + i).GetComponent<global::UnityEngine.UI.Image>().color, color_SlotBoxOFF, global::UnityEngine.Time.deltaTime * 2f);
			}
		}
		if (Sel_Index == Prev_Index && Sel_Slot == CursorObject_Num && List_Slot == 0)
		{
			CursorObjcet_Timer += global::UnityEngine.Time.deltaTime;
		}
		else
		{
			CursorObjcet_Timer = 0f;
		}
		if (CursorObjcet_Timer > 1f)
		{
			Cursor_Object.GetComponent<global::UnityEngine.SpriteRenderer>().color = global::UnityEngine.Color.Lerp(Cursor_Object.GetComponent<global::UnityEngine.SpriteRenderer>().color, color_OFF, global::UnityEngine.Time.deltaTime * 10f);
		}
		else
		{
			Cursor_Object.GetComponent<global::UnityEngine.SpriteRenderer>().color = global::UnityEngine.Color.Lerp(Cursor_Object.GetComponent<global::UnityEngine.SpriteRenderer>().color, color_ON, global::UnityEngine.Time.deltaTime * 5f);
		}
		if (Sel_Slot > 0)
		{
			Cursor_Object.transform.position = global::UnityEngine.Vector3.Lerp(Cursor_Object.transform.position, new global::UnityEngine.Vector3(6 * (Sel_Slot - 3), 4f, 0f), global::UnityEngine.Time.deltaTime * 12f);
		}
		else if (List_Slot > 0)
		{
			Cursor_Object.transform.position = global::UnityEngine.Vector3.Lerp(Cursor_Object.transform.position, new global::UnityEngine.Vector3(6 * (List_Slot - 3), 4f, 0f), global::UnityEngine.Time.deltaTime * 12f);
		}
		CursorObject_Num = Sel_Slot;
		Prev_Index = Sel_Index;
		if (State > 0 && (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.Escape) || global::UnityEngine.Input.GetButtonDown("Start")))
		{
			Exit();
		}
		if (isFadeIn)
		{
			if (FadeMode == 0)
			{
				FadeOpacity -= global::UnityEngine.Time.deltaTime * 0.5f;
			}
			else
			{
				FadeOpacity -= global::UnityEngine.Time.deltaTime * 2.5f;
			}
			if (FadeOpacity <= 0f)
			{
				isFadeIn = false;
				FadeOpacity = 0f;
				BlackFade.GetComponent<global::UnityEngine.SpriteRenderer>().enabled = false;
				BlackFade_Mode.GetComponent<global::UnityEngine.UI.Image>().enabled = false;
			}
			BlackFade.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(0f, 0f, 0f, FadeOpacity);
			BlackFade_Mode.GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(0f, 0f, 0f, FadeOpacity);
		}
		else
		{
			if (!isFadeOut)
			{
				return;
			}
			if (FadeMode == 0)
			{
				FadeOpacity += global::UnityEngine.Time.deltaTime * 1f;
			}
			else
			{
				FadeOpacity += global::UnityEngine.Time.deltaTime * 2.5f;
			}
			if (FadeOpacity >= 1f)
			{
				isFadeOut = false;
				FadeOpacity = 1f;
				if (FadeOutAction == "Title")
				{
					ExitToMain();
				}
				else if (FadeOutAction == "Gallery")
				{
					Start_Gallery();
				}
				else if (FadeOutAction == "GameOver")
				{
					Start_GameOver();
				}
			}
			BlackFade.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(0f, 0f, 0f, FadeOpacity);
			BlackFade_Mode.GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(0f, 0f, 0f, FadeOpacity);
		}
	}

	private void ExitToMain()
	{
		global::UnityEngine.Application.LoadLevel("Title");
	}

	public void Set_FadeOut(int fademode, string fadeoutaction)
	{
		FadeMode = fademode;
		isFadeOut = true;
		isFadeIn = false;
		if (FadeMode == 0)
		{
			if (!BlackFade.GetComponent<global::UnityEngine.SpriteRenderer>().enabled)
			{
				BlackFade.GetComponent<global::UnityEngine.SpriteRenderer>().enabled = true;
			}
			if (BlackFade_Mode.GetComponent<global::UnityEngine.UI.Image>().enabled)
			{
				BlackFade_Mode.GetComponent<global::UnityEngine.UI.Image>().enabled = false;
			}
		}
		else
		{
			if (!BlackFade_Mode.GetComponent<global::UnityEngine.UI.Image>().enabled)
			{
				BlackFade_Mode.GetComponent<global::UnityEngine.UI.Image>().enabled = true;
			}
			if (BlackFade.GetComponent<global::UnityEngine.SpriteRenderer>().enabled)
			{
				BlackFade.GetComponent<global::UnityEngine.SpriteRenderer>().enabled = false;
			}
		}
		FadeOutAction = fadeoutaction;
	}

	private void Start_Gallery()
	{
		isFadeIn = true;
		isFadeOut = false;
		Show_All_Slot();
		global::UnityEngine.GameObject.Find("H_Manager").GetComponent<H_Manager>().Delete_GameOver();
		global::UnityEngine.GameObject.Find("Help_Gallery").SendMessage("Set_Text_Gallery");
		Cursor_Object.GetComponent<global::UnityEngine.SpriteRenderer>().enabled = true;
		global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Gallery_Camera>().Set_TargetPos(new global::UnityEngine.Vector3(0f, 0f, -10f));
		global::UnityEngine.GameObject.Find("Main Camera").transform.position = new global::UnityEngine.Vector3(0f, 0f, -10f);
		GameOver_Menu.localPosition = new global::UnityEngine.Vector3(0f, -3000f, 0f);
		SelBorder_Option.localPosition = global::UnityEngine.GameObject.Find("Btn_Option_" + AxiPlayerPrefs.GetInt("Gallery_Option")).GetComponent<global::UnityEngine.RectTransform>().localPosition;
		global::UnityEngine.GameObject.Find("Main Camera").SendMessage("Set_Vignetting_16");
		global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Gallery_Camera>().Cam_Top = 12f;
		global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Gallery_Camera>().Cam_Bot = -8f;
		global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Gallery_Camera>().Cam_Right = 20f;
		global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Gallery_Camera>().Cam_Left = -20f;
		if (Slot_isOpen[0] > 0 || Slot_isOpen[4] > 0)
		{
			global::UnityEngine.GameObject.Find("Main Camera").GetComponent<UnityEngine.Camera>().orthographicSize = 5f;
			global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Gallery_Camera>().targetSize = 7.6f;
			global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Gallery_Camera>().Set_TargetPos(new global::UnityEngine.Vector3(0f, 2f, -10f));
			global::UnityEngine.GameObject.Find("Main Camera").transform.position = new global::UnityEngine.Vector3(0f, 2f, -10f);
		}
		else
		{
			global::UnityEngine.GameObject.Find("Main Camera").GetComponent<UnityEngine.Camera>().orthographicSize = 7f;
			global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Gallery_Camera>().targetSize = 5f;
			global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Gallery_Camera>().Set_TargetPos(new global::UnityEngine.Vector3(0f, 0f, -10f));
			global::UnityEngine.GameObject.Find("Main Camera").transform.position = new global::UnityEngine.Vector3(0f, 0f, -10f);
		}
		global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Gallery_Camera>().Set_Blur(12);
		Gallery_Option_CamSetting();
		if (AxiPlayerPrefs.GetInt("Gallery_Option") > 0)
		{
			Gallery_BG[AxiPlayerPrefs.GetInt("Gallery_Option") - 1].transform.position = new global::UnityEngine.Vector3(0f, 0f, 0f);
		}
		bool onOff = AxiPlayerPrefs.GetInt("Gallery_Grayscale") == 1;
		Button_Grayscale(onOff);
	}

	private void Start_GameOver()
	{
		isFadeIn = true;
		isFadeOut = false;
		Hide_All_Slot();
		global::UnityEngine.GameObject.Find("H_Manager").GetComponent<H_Manager>().Make_H_GameOver(Sel_GameOver);
		SelBorder_GameOver.localPosition = global::UnityEngine.GameObject.Find("Box_GameOver_" + Sel_GameOver).GetComponent<global::UnityEngine.RectTransform>().localPosition;
		global::UnityEngine.GameObject.Find("Help_Gallery").SendMessage("Set_Text_GameOver");
		Cursor_Object.GetComponent<global::UnityEngine.SpriteRenderer>().enabled = false;
		global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Gallery_Camera>().Set_Blur(12);
		GameOver_Menu.localPosition = new global::UnityEngine.Vector3(0f, 0f, 0f);
		SelBorder_Option.localPosition = global::UnityEngine.GameObject.Find("Btn_Option_" + AxiPlayerPrefs.GetInt("Gallery_Option_GameOver")).GetComponent<global::UnityEngine.RectTransform>().localPosition;
		for (int i = 0; i < 4; i++)
		{
			Gallery_BG[i].transform.position = new global::UnityEngine.Vector3(100f, -100f, 0f);
		}
		bool onOff = AxiPlayerPrefs.GetInt("Gallery_Grayscale_GameOver") == 1;
		Button_Grayscale(onOff);
	}

	private void Target_Select()
	{
		if (Sel_Index >= 0)
		{
			if (Sel_Index < 3)
			{
				Cursor_BG.sizeDelta = new global::UnityEngine.Vector2(212f, 68f);
				Cursor.sizeDelta = new global::UnityEngine.Vector2(224f, 80f);
				if (Sel_Index == 0)
				{
					PosTarget = global::UnityEngine.GameObject.Find("Btn_Exit").GetComponent<global::UnityEngine.RectTransform>().position;
				}
				else if (Sel_Index == 1)
				{
					PosTarget = global::UnityEngine.GameObject.Find("Btn_Gallery").GetComponent<global::UnityEngine.RectTransform>().position;
				}
				else if (Sel_Index == 2)
				{
					PosTarget = global::UnityEngine.GameObject.Find("Btn_GameOver").GetComponent<global::UnityEngine.RectTransform>().position;
				}
			}
			else if (Sel_Index < 4)
			{
				Cursor_BG.sizeDelta = new global::UnityEngine.Vector2(212f, 40f);
				Cursor.sizeDelta = new global::UnityEngine.Vector2(224f, 48f);
				PosTarget = global::UnityEngine.GameObject.Find("Bar_Sound").GetComponent<global::UnityEngine.RectTransform>().position;
			}
			else if (Sel_Index <= 30)
			{
				Cursor_BG.sizeDelta = new global::UnityEngine.Vector2(40f, 40f);
				Cursor.sizeDelta = new global::UnityEngine.Vector2(48f, 48f);
				PosTarget = global::UnityEngine.GameObject.Find("Btn_Option_" + (Sel_Index - 10)).GetComponent<global::UnityEngine.RectTransform>().position;
			}
			else if (Sel_Index <= 100)
			{
				Cursor_BG.sizeDelta = new global::UnityEngine.Vector2(70f, 70f);
				Cursor.sizeDelta = new global::UnityEngine.Vector2(80f, 80f);
			}
			else if (Sel_Index < 180)
			{
				if (Sel_Slot > 0 && Sel_Slot < 6)
				{
					if (Sel_Index % 10 == 9)
					{
						Cursor_BG.sizeDelta = new global::UnityEngine.Vector2(220f, 60f);
						Cursor.sizeDelta = new global::UnityEngine.Vector2(232f, 72f);
					}
					else
					{
						Cursor_BG.sizeDelta = new global::UnityEngine.Vector2(44f, 44f);
						Cursor.sizeDelta = new global::UnityEngine.Vector2(52f, 52f);
					}
				}
			}
			else if (Sel_Index < 200)
			{
				Cursor_BG.sizeDelta = new global::UnityEngine.Vector2(236f, 50f);
				Cursor.sizeDelta = new global::UnityEngine.Vector2(248f, 62f);
			}
			else
			{
				Cursor_BG.sizeDelta = new global::UnityEngine.Vector2(76f, 56f);
				Cursor.sizeDelta = new global::UnityEngine.Vector2(84f, 64f);
			}
		}
		SelBG_Opacity = 0f;
		Cursor_BG.GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, SelBG_Opacity);
		Cursor_BG.position = PosTarget;
	}

	private void Exit()
	{
		if (State > 0 && FadeOpacity < 0.1f)
		{
			State = 0;
			global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_DeviceOn");
			global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_Btn");
			SelBorder_Main.localPosition = global::UnityEngine.GameObject.Find("Btn_Exit").GetComponent<global::UnityEngine.RectTransform>().localPosition;
			Set_FadeOut(0, "Title");
		}
	}

	private void OnGallery()
	{
		if (State > 1)
		{
			State = 1;
			global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_MenuOn");
			SelBorder_Main.localPosition = global::UnityEngine.GameObject.Find("Btn_Gallery").GetComponent<global::UnityEngine.RectTransform>().localPosition;
			Set_FadeOut(1, "Gallery");
		}
	}

	private void OnGameOver()
	{
		if (State == 1)
		{
			State = 2;
			global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_MenuOff");
			SelBorder_Main.localPosition = global::UnityEngine.GameObject.Find("Btn_GameOver").GetComponent<global::UnityEngine.RectTransform>().localPosition;
			Set_FadeOut(1, "GameOver");
		}
	}

	private void Set_Sound(int LR, float posMouse)
	{
		Sel_Index = 3;
		if (LR == 0)
		{
			if (posMouse > 1f)
			{
				posMouse = 1f;
			}
			else if (posMouse < 0f)
			{
				posMouse = 0f;
			}
			AxiPlayerPrefs.SetFloat("SoundVolume", posMouse);
		}
		global::UnityEngine.GameObject.Find("Sound_Bar").GetComponent<global::UnityEngine.UI.Image>().fillAmount = AxiPlayerPrefs.GetFloat("SoundVolume");
		if (SoundVolume_Timer <= 0f)
		{
			global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_Move_2");
			SoundVolume_Timer = 0.15f;
		}
		global::UnityEngine.Debug.Log(AxiPlayerPrefs.GetFloat("SoundVolume"));
	}

	private void Set_Option(int num)
	{
		if (num == 5)
		{
			global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_Tab");
			if (State == 1)
			{
				if (AxiPlayerPrefs.GetInt("Gallery_Grayscale") == 0)
				{
					AxiPlayerPrefs.SetInt("Gallery_Grayscale", 1);
				}
				else
				{
					AxiPlayerPrefs.SetInt("Gallery_Grayscale", 0);
				}
				Gallery_Option_CamSetting();
				bool onOff = AxiPlayerPrefs.GetInt("Gallery_Grayscale") == 1;
				Button_Grayscale(onOff);
			}
			else if (State == 2)
			{
				if (AxiPlayerPrefs.GetInt("Gallery_Grayscale_GameOver") == 0)
				{
					AxiPlayerPrefs.SetInt("Gallery_Grayscale_GameOver", 1);
				}
				else
				{
					AxiPlayerPrefs.SetInt("Gallery_Grayscale_GameOver", 0);
				}
				global::UnityEngine.GameObject.Find("H_Manager").GetComponent<H_Manager>().Set_GameOver_Option(AxiPlayerPrefs.GetInt("Gallery_Option_GameOver"));
				bool onOff2 = AxiPlayerPrefs.GetInt("Gallery_Grayscale_GameOver") == 1;
				Button_Grayscale(onOff2);
			}
		}
		else if (State == 1)
		{
			if (AxiPlayerPrefs.GetInt("Gallery_Option") == num)
			{
				return;
			}
			AxiPlayerPrefs.SetInt("Gallery_Option", num);
			global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_Tab");
			SelBorder_Option.localPosition = global::UnityEngine.GameObject.Find("Btn_Option_" + num).GetComponent<global::UnityEngine.RectTransform>().localPosition;
			for (int i = 0; i < 4; i++)
			{
				if (AxiPlayerPrefs.GetInt("Gallery_Option") == i + 1)
				{
					Gallery_BG[i].transform.position = new global::UnityEngine.Vector3(0f, 0f, 0f);
				}
				else
				{
					Gallery_BG[i].transform.position = new global::UnityEngine.Vector3(100f, -100f, 0f);
				}
			}
			Gallery_Option_CamSetting();
		}
		else if (State == 2 && AxiPlayerPrefs.GetInt("Gallery_Option_GameOver") != num)
		{
			AxiPlayerPrefs.SetInt("Gallery_Option_GameOver", num);
			global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_Tab");
			SelBorder_Option.localPosition = global::UnityEngine.GameObject.Find("Btn_Option_" + num).GetComponent<global::UnityEngine.RectTransform>().localPosition;
			global::UnityEngine.GameObject.Find("H_Manager").GetComponent<H_Manager>().Set_GameOver_Option(num);
		}
	}

	private void Button_Grayscale(bool OnOff)
	{
		if (OnOff)
		{
			global::UnityEngine.GameObject.Find("Selected_Grayscale").GetComponent<global::UnityEngine.UI.Image>().enabled = true;
		}
		else
		{
			global::UnityEngine.GameObject.Find("Selected_Grayscale").GetComponent<global::UnityEngine.UI.Image>().enabled = false;
		}
	}

	private void Gallery_Option_CamSetting()
	{
		if (AxiPlayerPrefs.GetInt("Gallery_Grayscale") == 1)
		{
			if (!global::UnityEngine.GameObject.Find("Main Camera").GetComponent<GrayscaleEffect>().enabled)
			{
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<GrayscaleEffect>().enabled = true;
			}
			if (!global::UnityEngine.GameObject.Find("Main Camera").GetComponent<NoiseEffect>().enabled)
			{
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<NoiseEffect>().enabled = true;
			}
		}
		else
		{
			if (global::UnityEngine.GameObject.Find("Main Camera").GetComponent<GrayscaleEffect>().enabled)
			{
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<GrayscaleEffect>().enabled = false;
			}
			if (global::UnityEngine.GameObject.Find("Main Camera").GetComponent<NoiseEffect>().enabled)
			{
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<NoiseEffect>().enabled = false;
			}
		}
		switch (AxiPlayerPrefs.GetInt("Gallery_Option"))
		{
		case 1:
			if (AxiPlayerPrefs.GetInt("Gallery_Grayscale") == 1)
			{
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<GrayscaleEffect>().rampOffset = 0f;
				global::UnityEngine.GameObject.Find("Main Camera").SendMessage("Set_Bloom_ON_10");
			}
			else
			{
				global::UnityEngine.GameObject.Find("Main Camera").SendMessage("Set_Bloom_ON_20");
			}
			break;
		case 2:
			if (AxiPlayerPrefs.GetInt("Gallery_Grayscale") == 1)
			{
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<GrayscaleEffect>().rampOffset = -0.05f;
				global::UnityEngine.GameObject.Find("Main Camera").SendMessage("Set_Bloom_ON_20");
			}
			else
			{
				global::UnityEngine.GameObject.Find("Main Camera").SendMessage("Set_Bloom_ON_40");
			}
			break;
		case 3:
			if (AxiPlayerPrefs.GetInt("Gallery_Grayscale") == 1)
			{
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<GrayscaleEffect>().rampOffset = 0f;
				global::UnityEngine.GameObject.Find("Main Camera").SendMessage("Set_Bloom_ON_20");
			}
			else
			{
				global::UnityEngine.GameObject.Find("Main Camera").SendMessage("Set_Bloom_ON_20");
			}
			break;
		case 4:
			if (AxiPlayerPrefs.GetInt("Gallery_Grayscale") == 1)
			{
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<GrayscaleEffect>().rampOffset = 0f;
				global::UnityEngine.GameObject.Find("Main Camera").SendMessage("Set_Bloom_ON_40");
			}
			else
			{
				global::UnityEngine.GameObject.Find("Main Camera").SendMessage("Set_Bloom_ON_30");
			}
			break;
		}
	}

	private void Set_Ani(int num)
	{
		if (num == 60)
		{
			if (Slot_isOpen[List_Slot - 1] > 0)
			{
				Slot_isOpen[List_Slot - 1] = 0;
				Slot[List_Slot - 1].Delete_Ani();
				Slot_Close(List_Slot);
			}
			onList = true;
			List_OnOff(0);
			SelBorder_Ani.position = global::UnityEngine.GameObject.Find("Box_Ani_" + num).GetComponent<global::UnityEngine.RectTransform>().position;
		}
		else if (List_Slot > 0 && AxiPlayerPrefs.GetInt("H_" + num) == 1)
		{
			Slot_isOpen[List_Slot - 1] = 1;
			Slot[List_Slot - 1].Load_Ani(num);
			Slot_Open(List_Slot);
			global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_Tab");
			SelBorder_Ani.position = global::UnityEngine.GameObject.Find("Box_Ani_" + num).GetComponent<global::UnityEngine.RectTransform>().position;
		}
	}

	private void Check_Mouse()
	{
		bool flag = false;
		if (global::UnityEngine.Input.GetMouseButtonDown(0))
		{
			global::UnityEngine.Ray ray = global::UnityEngine.GameObject.Find("UI Camera").GetComponent<UnityEngine.Camera>().ScreenPointToRay(global::UnityEngine.Input.mousePosition);
			global::UnityEngine.RaycastHit2D rayIntersection = global::UnityEngine.Physics2D.GetRayIntersection(ray, float.PositiveInfinity);
			if (rayIntersection.collider != null)
			{
				if (rayIntersection.collider.name == "Btn_Exit")
				{
					Exit();
				}
				else if (rayIntersection.collider.name == "Btn_Gallery")
				{
					OnGallery();
				}
				else if (rayIntersection.collider.name == "Btn_GameOver")
				{
					OnGameOver();
				}
				else if (rayIntersection.collider.name == "Bar_Sound")
				{
					float posMouse = (global::UnityEngine.GameObject.Find("UI Camera").GetComponent<UnityEngine.Camera>().ScreenToWorldPoint(global::UnityEngine.Input.mousePosition).x - global::UnityEngine.GameObject.Find("pos_SoundBarMin").GetComponent<global::UnityEngine.RectTransform>().position.x) / (global::UnityEngine.GameObject.Find("pos_SoundBarMax").GetComponent<global::UnityEngine.RectTransform>().position.x - global::UnityEngine.GameObject.Find("pos_SoundBarMin").GetComponent<global::UnityEngine.RectTransform>().position.x);
					Set_Sound(0, posMouse);
				}
				else if (rayIntersection.collider.name == "Btn_Option_1")
				{
					Set_Option(1);
				}
				else if (rayIntersection.collider.name == "Btn_Option_2")
				{
					Set_Option(2);
				}
				else if (rayIntersection.collider.name == "Btn_Option_3")
				{
					Set_Option(3);
				}
				else if (rayIntersection.collider.name == "Btn_Option_4")
				{
					Set_Option(4);
				}
				else if (rayIntersection.collider.name == "Btn_Option_5")
				{
					Set_Option(5);
				}
				else if (rayIntersection.collider.name.Length == 14)
				{
					if (rayIntersection.collider.name.Substring(0, 11) == "Slot_State_")
					{
						int num = int.Parse(rayIntersection.collider.name.Substring(13, 1));
						Sel_Slot = int.Parse(rayIntersection.collider.name.Substring(11, 1));
						if (num < 5)
						{
							Slot[Sel_Slot - 1].Set_State(num);
						}
						else if (num == 8)
						{
							Slot[Sel_Slot - 1].Set_Play();
							onHoldStart = true;
							flag = true;
						}
					}
					else if (rayIntersection.collider.name.Substring(0, 12) == "Box_GameOver")
					{
						int num2 = int.Parse(rayIntersection.collider.name.Substring(13, 1));
						if (Sel_GameOver != num2 && num2 < 6 && (num2 == 1 || AxiPlayerPrefs.GetInt("H_GameOver_" + num2) > 0))
						{
							global::UnityEngine.GameObject.Find("H_Manager").GetComponent<H_Manager>().Make_H_GameOver(num2);
							Sel_GameOver = num2;
							SelBorder_GameOver.localPosition = rayIntersection.collider.GetComponent<global::UnityEngine.RectTransform>().localPosition;
							global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_Tab");
						}
					}
				}
				else if (rayIntersection.collider.name.Length == 12)
				{
					if (rayIntersection.collider.name.Substring(0, 11) == "Slot_Empty_")
					{
						Sel_Slot = int.Parse(rayIntersection.collider.name.Substring(11, 1));
						if (List_Slot != Sel_Slot)
						{
							List_Slot = Sel_Slot;
							List_OnOff(1);
						}
					}
				}
				else if (rayIntersection.collider.name.Length > 8)
				{
					if (rayIntersection.collider.name.Substring(0, 8) == "Box_Ani_")
					{
						int num3 = int.Parse(rayIntersection.collider.name.Substring(8, rayIntersection.collider.name.Length - 8));
						if (AxiPlayerPrefs.GetInt("H_" + num3) == 1 || num3 == 60)
						{
							Set_Ani(num3);
						}
					}
					else if (rayIntersection.collider.name.Substring(0, 8) == "Btn_Cum_")
					{
						int num4 = int.Parse(rayIntersection.collider.name.Substring(8, rayIntersection.collider.name.Length - 8));
						Slot[num4 - 1].Set_Cum();
					}
				}
				if (rayIntersection.collider.tag != "UI")
				{
					onMouseDrag = true;
				}
			}
			else
			{
				onMouseDrag = true;
			}
		}
		else
		{
			global::UnityEngine.Ray ray2 = global::UnityEngine.GameObject.Find("UI Camera").GetComponent<UnityEngine.Camera>().ScreenPointToRay(global::UnityEngine.Input.mousePosition);
			global::UnityEngine.RaycastHit2D rayIntersection2 = global::UnityEngine.Physics2D.GetRayIntersection(ray2, float.PositiveInfinity);
			if (rayIntersection2.collider != null)
			{
				if (rayIntersection2.collider.name == "Btn_Exit")
				{
					Sel_Index = 0;
				}
				else if (rayIntersection2.collider.name == "Btn_Gallery")
				{
					Sel_Index = 1;
				}
				else if (rayIntersection2.collider.name == "Btn_GameOver")
				{
					Sel_Index = 2;
				}
				else if (rayIntersection2.collider.name == "Bar_Sound")
				{
					Sel_Index = 3;
					if (global::UnityEngine.Input.GetMouseButton(0))
					{
						float posMouse2 = (global::UnityEngine.GameObject.Find("UI Camera").GetComponent<UnityEngine.Camera>().ScreenToWorldPoint(global::UnityEngine.Input.mousePosition).x - global::UnityEngine.GameObject.Find("pos_SoundBarMin").GetComponent<global::UnityEngine.RectTransform>().position.x) / (global::UnityEngine.GameObject.Find("pos_SoundBarMax").GetComponent<global::UnityEngine.RectTransform>().position.x - global::UnityEngine.GameObject.Find("pos_SoundBarMin").GetComponent<global::UnityEngine.RectTransform>().position.x);
						Set_Sound(0, posMouse2);
					}
				}
				else if (rayIntersection2.collider.name == "Btn_Option_1")
				{
					Sel_Index = 11;
				}
				else if (rayIntersection2.collider.name == "Btn_Option_2")
				{
					Sel_Index = 12;
				}
				else if (rayIntersection2.collider.name == "Btn_Option_3")
				{
					Sel_Index = 13;
				}
				else if (rayIntersection2.collider.name == "Btn_Option_4")
				{
					Sel_Index = 14;
				}
				else if (rayIntersection2.collider.name == "Btn_Option_5")
				{
					Sel_Index = 15;
				}
				else if (rayIntersection2.collider.name.Length == 14)
				{
					if (rayIntersection2.collider.name.Substring(0, 11) == "Slot_State_")
					{
						int num5 = int.Parse(rayIntersection2.collider.name.Substring(13, 1));
						Sel_Slot = int.Parse(rayIntersection2.collider.name.Substring(11, 1));
						bool flag2 = true;
						if (num5 == 9 && global::UnityEngine.Input.GetMouseButton(0))
						{
							float speed = (global::UnityEngine.GameObject.Find("UI Camera").GetComponent<UnityEngine.Camera>().ScreenToWorldPoint(global::UnityEngine.Input.mousePosition).x - global::UnityEngine.GameObject.Find("Slot_" + Sel_Slot + "_SpeedBarMin").GetComponent<global::UnityEngine.RectTransform>().position.x) / (global::UnityEngine.GameObject.Find("Slot_" + Sel_Slot + "_SpeedBarMax").GetComponent<global::UnityEngine.RectTransform>().position.x - global::UnityEngine.GameObject.Find("Slot_" + Sel_Slot + "_SpeedBarMin").GetComponent<global::UnityEngine.RectTransform>().position.x);
							Slot[Sel_Slot - 1].Set_Speed(speed);
							onSpeedMouseDown = true;
						}
						else if (onHoldStart && num5 == 8 && global::UnityEngine.Input.GetMouseButton(0))
						{
							flag = true;
							Hold_Timer += global::UnityEngine.Time.deltaTime;
							if (Hold_Timer > 0.5f)
							{
								global::UnityEngine.Debug.Log("Hold 0.5f");
								Slot[Sel_Slot - 1].Set_Loop();
								flag = (onHoldStart = false);
								Hold_Timer = 0f;
							}
						}
						else if ((num5 == 2 || num5 == 3) && !rayIntersection2.collider.GetComponent<global::UnityEngine.UI.Image>().enabled)
						{
							flag2 = false;
						}
						if (flag2)
						{
							Sel_Index = 100 + Sel_Slot * 10 + num5;
							PosTarget = global::UnityEngine.GameObject.Find(rayIntersection2.collider.name).GetComponent<global::UnityEngine.RectTransform>().position;
						}
						if (global::UnityEngine.Input.GetMouseButtonDown(1))
						{
							if (List_Slot != Sel_Slot)
							{
								List_Slot = Sel_Slot;
								List_OnOff(1);
							}
							else
							{
								List_OnOff(0);
							}
						}
					}
					else if (rayIntersection2.collider.name.Substring(0, 12) == "Box_GameOver")
					{
						int num6 = int.Parse(rayIntersection2.collider.name.Substring(13, 1));
						Sel_Index = 200 + num6;
						PosTarget = global::UnityEngine.GameObject.Find(rayIntersection2.collider.name).GetComponent<global::UnityEngine.RectTransform>().position;
					}
				}
				else if (rayIntersection2.collider.name.Length == 12)
				{
					if (rayIntersection2.collider.name.Substring(0, 11) == "Slot_Empty_")
					{
						Sel_Slot = int.Parse(rayIntersection2.collider.name.Substring(11, 1));
						Sel_Index = 180 + Sel_Slot;
						PosTarget = global::UnityEngine.GameObject.Find(rayIntersection2.collider.name).GetComponent<global::UnityEngine.RectTransform>().position;
						if (global::UnityEngine.Input.GetMouseButtonDown(1))
						{
							if (List_Slot != Sel_Slot)
							{
								List_Slot = Sel_Slot;
								List_OnOff(1);
							}
							else
							{
								List_OnOff(0);
							}
						}
					}
				}
				else if (rayIntersection2.collider.name.Length > 8)
				{
					if (rayIntersection2.collider.name.Substring(0, 8) == "Box_Ani_")
					{
						int num7 = int.Parse(rayIntersection2.collider.name.Substring(8, rayIntersection2.collider.name.Length - 8));
						Sel_Index = 30 + num7;
						PosTarget = global::UnityEngine.GameObject.Find(rayIntersection2.collider.name).GetComponent<global::UnityEngine.RectTransform>().position;
					}
					else if (rayIntersection2.collider.name.Substring(0, 8) == "Btn_Cum_")
					{
						int num8 = int.Parse(rayIntersection2.collider.name.Substring(8, rayIntersection2.collider.name.Length - 8));
					}
					if (global::UnityEngine.Input.GetMouseButtonDown(1))
					{
						List_OnOff(0);
					}
				}
				if (Sel_Index > 0 && (Sel_Index < 110 || Sel_Index > 190))
				{
					Sel_Slot = 0;
				}
			}
			else if (global::UnityEngine.Input.GetMouseButtonDown(1))
			{
				List_OnOff(2);
			}
			if (global::UnityEngine.Input.GetMouseButtonUp(0))
			{
				onMouseDrag = false;
				if (onSpeedMouseDown)
				{
					onSpeedMouseDown = false;
					Slot[Sel_Slot - 1].Save_Speed();
				}
			}
		}
		if (!flag)
		{
			flag = (onHoldStart = false);
			Hold_Timer = 0f;
		}
	}

	private void List_OnOff(int mode)
	{
		if (State != 1)
		{
			return;
		}
		global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_Btn");
		switch (mode)
		{
		case 0:
			onList = false;
			break;
		case 1:
			onList = true;
			break;
		default:
			Get_Closest_Slot();
			break;
		}
		if (onList)
		{
			if (List_Slot == 1)
			{
				Ani_List.localPosition = new global::UnityEngine.Vector3(-500f, -208f, 0f);
				global::UnityEngine.GameObject.Find("Menu_Animation_Tail").GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(-200f, -191f);
			}
			else if (List_Slot == 5)
			{
				Ani_List.localPosition = new global::UnityEngine.Vector3(500f, -208f, 0f);
				global::UnityEngine.GameObject.Find("Menu_Animation_Tail").GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(200f, -191f);
			}
			else
			{
				Ani_List.localPosition = new global::UnityEngine.Vector3(350 * (List_Slot - 3), -208f, 0f);
				global::UnityEngine.GameObject.Find("Menu_Animation_Tail").GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(0f, -191f);
			}
			if (Slot_isOpen[List_Slot - 1] > 0)
			{
				SelBorder_Ani.position = global::UnityEngine.GameObject.Find("Box_Ani_" + Slot[List_Slot - 1].Get_H_Num()).GetComponent<global::UnityEngine.RectTransform>().position;
			}
			else
			{
				SelBorder_Ani.position = global::UnityEngine.GameObject.Find("Box_Ani_60").GetComponent<global::UnityEngine.RectTransform>().position;
			}
		}
		else
		{
			Sel_Index = -1;
			List_Slot = 0;
			Ani_List.localPosition = new global::UnityEngine.Vector3(Ani_List.localPosition.x, -1500f, 0f);
		}
	}

	private void Get_Closest_Slot()
	{
		int num = 2;
		float num2 = 1000f;
		float num3 = 0f;
		global::UnityEngine.Vector3 b = global::UnityEngine.GameObject.Find("UI Camera").GetComponent<UnityEngine.Camera>().ScreenToWorldPoint(global::UnityEngine.Input.mousePosition);
		for (int i = 0; i < 5; i++)
		{
			num3 = ((Slot_isOpen[i] <= 0) ? global::UnityEngine.Vector3.Distance(global::UnityEngine.GameObject.Find("Slot_Empty_" + (i + 1)).GetComponent<global::UnityEngine.RectTransform>().position, b) : global::UnityEngine.Vector3.Distance(global::UnityEngine.GameObject.Find("Slot_" + (i + 1)).GetComponent<global::UnityEngine.RectTransform>().position, b));
			if (num3 < num2)
			{
				num = i;
				num2 = num3;
			}
		}
		if (num2 < 1000f)
		{
			global::UnityEngine.Vector3 vector = ((Slot_isOpen[num] <= 0) ? global::UnityEngine.GameObject.Find("Slot_Empty_" + (num + 1)).GetComponent<global::UnityEngine.RectTransform>().position : global::UnityEngine.GameObject.Find("Slot_" + (num + 1)).GetComponent<global::UnityEngine.RectTransform>().position);
			if (Slot_isOpen[num] > 0 && b.x < vector.x + 1.2f && b.x > vector.x - 1.2f && b.y < vector.y + 0.2f && b.y > vector.y - 0.2f)
			{
				if (List_Slot == num + 1)
				{
					onList = false;
				}
				else
				{
					onList = true;
				}
				global::UnityEngine.Debug.Log("posMouse.x : " + b.x + ",    posSlot.x : " + vector.x);
			}
			else
			{
				onList = !onList;
			}
		}
		if (onList)
		{
			List_Slot = num + 1;
		}
	}

	private void Slot_Open(int num)
	{
		global::UnityEngine.GameObject.Find("Slot_Empty_" + num).GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(350 * (num - 3), -1500f, 0f);
		global::UnityEngine.GameObject.Find("Slot_" + num).GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(350 * (num - 3), -454f, 0f);
	}

	private void Slot_Close(int num)
	{
		global::UnityEngine.GameObject.Find("Slot_Empty_" + num).GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(350 * (num - 3), -454f, 0f);
		global::UnityEngine.GameObject.Find("Slot_" + num).GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(350 * (num - 3), -1500f, 0f);
	}

	private void Delete_All()
	{
		Slot_isOpen[0] = 0;
		Slot_isOpen[1] = 0;
		Slot_isOpen[2] = 0;
		Slot_isOpen[3] = 0;
		Slot_isOpen[4] = 0;
		Slot[0].Delete_Ani();
		Slot_Close(1);
		Slot[1].Delete_Ani();
		Slot_Close(2);
		Slot[2].Delete_Ani();
		Slot_Close(3);
		Slot[3].Delete_Ani();
		Slot_Close(4);
		Slot[4].Delete_Ani();
		Slot_Close(5);
	}

	private void Hide_All_Slot()
	{
		for (int i = 1; i < 6; i++)
		{
			global::UnityEngine.GameObject.Find("Slot_Empty_" + i).GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(350 * (i - 3), -1500f, 0f);
			global::UnityEngine.GameObject.Find("Slot_" + i).GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(350 * (i - 3), -1500f, 0f);
		}
		Slot[0].Hide_Ani();
		Slot[1].Hide_Ani();
		Slot[2].Hide_Ani();
		Slot[3].Hide_Ani();
		Slot[4].Hide_Ani();
		Sel_Index = -1;
		List_Slot = 0;
		Ani_List.localPosition = new global::UnityEngine.Vector3(Ani_List.localPosition.x, -1500f, 0f);
	}

	private void Show_All_Slot()
	{
		for (int i = 1; i < 6; i++)
		{
			if (Slot_isOpen[i - 1] > 0)
			{
				Slot_Open(i);
			}
			else
			{
				Slot_Close(i);
			}
		}
		Slot[0].Start_Gallery();
		Slot[1].Start_Gallery();
		Slot[2].Start_Gallery();
		Slot[3].Start_Gallery();
		Slot[4].Start_Gallery();
		Sel_Index = -1;
		List_Slot = 0;
		Ani_List.localPosition = new global::UnityEngine.Vector3(Ani_List.localPosition.x, -1500f, 0f);
	}
}
