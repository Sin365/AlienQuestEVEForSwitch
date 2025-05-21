public class Menu_Control : global::UnityEngine.MonoBehaviour
{
	public bool Enabled;

	private bool GameOver_Enabled;

	public int Menu_State = 1;

	private int Sel_Index = 6;

	private int Prev_Index = 6;

	private global::UnityEngine.Vector3 PosOrig;

	private global::UnityEngine.Vector3 PosTarget;

	private float PushX;

	private float inputX;

	private float prevX;

	private bool lock_Y;

	private float PushY;

	private float inputY;

	private float prevY;

	private float SelBG_Opacity = 1f;

	private float SelCursor_Timer;

	private float SelCursor_Size = 1f;

	private float[] SelArrow_Opacity = new float[2] { 0.1f, 0.1f };

	private int StatSel_Num;

	private float StatPush_Timer;

	private float StatPress_Timer;

	private global::UnityEngine.Vector3 MousePos;

	private global::UnityEngine.Vector3 MousePosPrev;

	private float MouseDist;

	private int Sound_MoveFalse;

	private bool onStatPoints = true;

	private float[] AddText_Timer = new float[4];

	private float[] StatBtn_Timer = new float[4];

	private global::UnityEngine.Vector3[] pos_StatBtn = new global::UnityEngine.Vector3[4];

	private bool onKeyConfigBox;

	private float KeyBox_Opacity;

	private float QuitRedBox_Opacity = 1f;

	private float DelaySound_Timer;

	private float SoundVolume_Timer;

	public global::UnityEngine.GameObject H_Escape;

	public global::UnityEngine.GameObject H_Object;

	public global::UnityEngine.UI.Image H_Bar;

	public global::UnityEngine.UI.Image H_Bar_img_R;

	public global::UnityEngine.UI.Image H_Bar_img_L;

	public global::UnityEngine.UI.Text H_Bar_text_R;

	public global::UnityEngine.UI.Text H_Bar_text_L;

	private float H_Rate;

	private float H_RL;

	private float H_Key_Timer;

	private float H_Minus = 2f;

	private bool onHscene;

	public bool H_Cancel;

	private GameManager GM;

	private Custom_Key CK;

	private Language_MenuItem Lang_MI;

	private UI_SoundList Sound_List;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		CK = global::UnityEngine.GameObject.Find("GameManager").GetComponent<Custom_Key>();
		Lang_MI = GetComponent<Language_MenuItem>();
		Sound_List = GetComponent<UI_SoundList>();
		PosOrig = GetComponent<global::UnityEngine.RectTransform>().localPosition;
		PosTarget = global::UnityEngine.GameObject.Find("pos_B6").transform.localPosition;
		MousePos = global::UnityEngine.Input.mousePosition;
		MousePosPrev = global::UnityEngine.Input.mousePosition;
		pos_StatBtn[0] = global::UnityEngine.GameObject.Find("Stat_Add1").GetComponent<global::UnityEngine.RectTransform>().localPosition;
		pos_StatBtn[1] = global::UnityEngine.GameObject.Find("Stat_Add2").GetComponent<global::UnityEngine.RectTransform>().localPosition;
		pos_StatBtn[2] = global::UnityEngine.GameObject.Find("Stat_Add3").GetComponent<global::UnityEngine.RectTransform>().localPosition;
		pos_StatBtn[3] = global::UnityEngine.GameObject.Find("Stat_Add4").GetComponent<global::UnityEngine.RectTransform>().localPosition;
		if (global::UnityEngine.PlayerPrefs.GetInt("Language_Num") == 1)
		{
			global::UnityEngine.GameObject.Find("Stat_Name").GetComponent<global::UnityEngine.UI.Text>().text = Lang_MI.CharName(1);
			global::UnityEngine.GameObject.Find("Exit_text_Quit").GetComponent<global::UnityEngine.UI.Text>().text = Lang_MI.QuitText(0, 1);
			global::UnityEngine.GameObject.Find("pos_Quit1").GetComponent<global::UnityEngine.UI.Text>().text = Lang_MI.QuitText(1, 1);
			global::UnityEngine.GameObject.Find("pos_Quit2").GetComponent<global::UnityEngine.UI.Text>().text = Lang_MI.QuitText(2, 1);
			global::UnityEngine.GameObject.Find("Exit_text_Info").GetComponent<global::UnityEngine.UI.Text>().text = Lang_MI.QuitText(3, 1);
			global::UnityEngine.GameObject.Find("Text_EllenMap").GetComponent<global::UnityEngine.UI.Text>().text = Lang_MI.CharName(1);
			global::UnityEngine.GameObject.Find("Text_EveCore").GetComponent<global::UnityEngine.UI.Text>().text = Lang_MI.MapText(0, 1);
			global::UnityEngine.GameObject.Find("Text_MotherBrain").GetComponent<global::UnityEngine.UI.Text>().text = Lang_MI.MapText(1, 1);
			global::UnityEngine.GameObject.Find("Text_Reactor").GetComponent<global::UnityEngine.UI.Text>().text = Lang_MI.MapText(2, 1);
			global::UnityEngine.GameObject.Find("Text_EscapeNow").GetComponent<global::UnityEngine.UI.Text>().text = Lang_MI.MapText(3, 1);
			global::UnityEngine.GameObject.Find("Mission_4").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(300f, 60f);
		}
	}

	private void Update()
	{
		if (onHscene && (!GM.onHscene || GM.GameOver))
		{
			onHscene = false;
			H_Escape.GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(0f, 1200f, 0f);
		}
		if (GM.onHscene && !GM.GameOver)
		{
			if (!onHscene)
			{
				onHscene = true;
				H_Cancel = false;
				if (GM.Hscene_Num > 29 && GM.Hscene_Num < 42)
				{
					H_Escape.GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(0f, 450f, 0f);
				}
				else if (GM.Hscene_Num == 42 || GM.Hscene_Num == 43)
				{
					H_Escape.GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(0f, 50f, 0f);
				}
				else
				{
					H_Escape.GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(0f, 350f, 0f);
				}
			}
			inputX = 0f;
			if (global::UnityEngine.Input.GetKeyDown(CK.Right))
			{
				inputX = 1f;
			}
			else if (global::UnityEngine.Input.GetKeyDown(CK.Left))
			{
				inputX = -1f;
			}
			else if (global::UnityEngine.Input.GetAxis("L_Trigger") > 0f)
			{
				inputX = 1f;
			}
			else if (global::UnityEngine.Input.GetAxis("L_Trigger") < 0f)
			{
				inputX = -1f;
			}
			if (inputX != 0f && H_RL != inputX)
			{
				H_RL = inputX;
				H_Rate += 0.1f;
				H_Minus = 0f;
				H_Key_Timer = 5f;
			}
			if (H_Key_Timer > 0f)
			{
				H_Key_Timer -= global::UnityEngine.Time.deltaTime;
			}
			else
			{
				H_RL = 0f;
			}
			H_Minus = global::UnityEngine.Mathf.Lerp(H_Minus, 2f, global::UnityEngine.Time.deltaTime * 2f);
			H_Rate = global::UnityEngine.Mathf.Lerp(H_Rate, 0f, global::UnityEngine.Time.deltaTime * H_Minus);
			H_Bar.fillAmount = global::UnityEngine.Mathf.Lerp(H_Bar.fillAmount, H_Rate, global::UnityEngine.Time.deltaTime * 10f);
			if (H_Rate > 1.05f)
			{
				H_RL = 0f;
				H_Rate = 0f;
				H_Minus = 2f;
				H_Bar.fillAmount = 0f;
				H_Cancel = true;
				global::UnityEngine.GameObject.Find("Player_SoundList").SendMessage("Sound_Down");
				if (H_Object != null)
				{
					H_Object.SendMessage("H_Exit");
				}
			}
		}
		else
		{
			if (GM.onEvent || GM.GameOver)
			{
				return;
			}
			if (GM.Paused)
			{
				if (!GM.onMenu || !Enabled)
				{
					return;
				}
				if (DelaySound_Timer > 0f)
				{
					DelaySound_Timer -= global::UnityEngine.Time.deltaTime;
				}
				if (SoundVolume_Timer > 0f)
				{
					SoundVolume_Timer -= global::UnityEngine.Time.deltaTime;
				}
				if (onKeyConfigBox)
				{
					if (KeyBox_Opacity < 1f)
					{
						KeyBox_Opacity = global::UnityEngine.Mathf.Lerp(KeyBox_Opacity, 1f, global::UnityEngine.Time.deltaTime * 10f);
					}
					global::UnityEngine.GameObject.Find("Select_KeyConfig").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, KeyBox_Opacity);
					global::UnityEngine.GameObject.Find("Text_AssignedKey").GetComponent<global::UnityEngine.UI.Text>().color = new global::UnityEngine.Color(1f, 1f, 1f, KeyBox_Opacity);
					if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.Escape) || global::UnityEngine.Input.GetButtonDown("Start") || global::UnityEngine.Input.GetButtonDown("_B") || global::UnityEngine.Input.GetMouseButtonDown(0) || global::UnityEngine.Input.GetMouseButtonDown(1))
					{
						onKeyConfigBox = false;
					}
					else if (CK.Check_Input(Sel_Index))
					{
						onKeyConfigBox = false;
					}
					return;
				}
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
				MousePos = global::UnityEngine.Input.mousePosition;
				MouseDist = global::UnityEngine.Vector3.Distance(MousePos, MousePosPrev);
				if (MouseDist > 0f || global::UnityEngine.Input.GetMouseButtonDown(0) || global::UnityEngine.Input.GetMouseButtonDown(1) || global::UnityEngine.Input.GetMouseButton(0))
				{
					Check_Mouse();
				}
				else
				{
					StatPush_Timer = 0f;
					Check_LRB();
				}
				if (Menu_State == 0)
				{
					SelCursor_Timer += global::UnityEngine.Time.deltaTime;
					SelCursor_Size = 1f + (1f + global::UnityEngine.Mathf.Sin(SelCursor_Timer * 10f)) * 0.05f;
					global::UnityEngine.GameObject.Find("Ellen_MapCursor").GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(SelCursor_Size, SelCursor_Size, 1f);
					if (GM.EventState == 200)
					{
						global::UnityEngine.GameObject.Find("Mission_4").GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(-1050f, 300f + global::UnityEngine.Mathf.Sin(SelCursor_Timer * 8f) * 5f, 1f);
					}
				}
				else if (Menu_State == 1)
				{
					GM.Status_Update();
					if (inputX < 0f)
					{
						if (Sel_Index >= 0)
						{
							if (Sel_Index == 1 || Sel_Index == 6 || Sel_Index == 11 || Sel_Index == 16 || Sel_Index == 21)
							{
								if (GM.StatPoints > 0)
								{
									if (Sel_Index == 1 || Sel_Index == 6)
									{
										Sel_Index = -1;
									}
									else if (Sel_Index == 11)
									{
										Sel_Index = -2;
									}
									else if (Sel_Index == 16 || Sel_Index == 21)
									{
										Sel_Index = -4;
									}
									lock_Y = true;
								}
							}
							else
							{
								Sel_Index--;
							}
						}
					}
					else if (inputX > 0f)
					{
						if (GM.StatPoints > 0 && Sel_Index < 0)
						{
							if (Sel_Index == -1)
							{
								Sel_Index = 6;
							}
							else if (Sel_Index == -2 || Sel_Index == -3)
							{
								Sel_Index = 11;
							}
							else
							{
								Sel_Index = 16;
							}
						}
						else if (Sel_Index % 5 != 0)
						{
							Sel_Index++;
						}
					}
					if (!lock_Y)
					{
						if (inputY < 0f && Sel_Index <= 20)
						{
							if (GM.StatPoints > 0 && Sel_Index < 0)
							{
								if (Sel_Index > -4)
								{
									Sel_Index--;
								}
							}
							else
							{
								Sel_Index += 5;
							}
						}
						else if (inputY > 0f && (Sel_Index < -1 || Sel_Index > 5))
						{
							if (GM.StatPoints > 0 && Sel_Index < 0)
							{
								Sel_Index++;
							}
							else
							{
								Sel_Index -= 5;
							}
						}
					}
					Check_AddTextColor();
				}
				else if (Menu_State == 2)
				{
					if (inputX < 0f)
					{
						if (Sel_Index == 1)
						{
							Set_Sound(-1, 0f);
						}
						else if (Sel_Index == 2)
						{
							Set_Music(-1, 0f);
						}
						else if (Sel_Index > 3 && Sel_Index < 10)
						{
							Sel_Index--;
						}
						else if (Sel_Index == 11)
						{
							Sel_Index = 10;
						}
						else if (Sel_Index == 13)
						{
							Sel_Index = 12;
						}
						else if (Sel_Index == 15)
						{
							Sel_Index = 14;
						}
						else if (Sel_Index == 17)
						{
							Sel_Index = 16;
						}
						else if (Sel_Index == 16)
						{
							Sel_Index = 18;
						}
					}
					else if (inputX > 0f)
					{
						if (Sel_Index == 1)
						{
							Set_Sound(1, 0f);
						}
						else if (Sel_Index == 2)
						{
							Set_Music(1, 0f);
						}
						else if (Sel_Index < 9)
						{
							Sel_Index++;
						}
						else if (Sel_Index == 10)
						{
							Sel_Index = 11;
						}
						else if (Sel_Index == 12)
						{
							Sel_Index = 13;
						}
						else if (Sel_Index == 14)
						{
							Sel_Index = 15;
						}
						else if (Sel_Index == 16)
						{
							Sel_Index = 17;
						}
						else if (Sel_Index == 18)
						{
							Sel_Index = 16;
						}
					}
					else if (inputY < 0f)
					{
						if (Sel_Index == 1)
						{
							Sel_Index = 2;
						}
						else if (Sel_Index == 2)
						{
							Sel_Index = 3 + GM.Option_Int[0];
						}
						else if (Sel_Index < 10)
						{
							if (GM.Option_Int[1] == 1)
							{
								Sel_Index = 10;
							}
							else
							{
								Sel_Index = 11;
							}
						}
						else if (Sel_Index < 12)
						{
							if (GM.Option_Int[2] == 1)
							{
								Sel_Index = 12;
							}
							else
							{
								Sel_Index = 13;
							}
						}
						else if (Sel_Index < 14)
						{
							if (GM.Option_Int[3] == 1)
							{
								Sel_Index = 14;
							}
							else
							{
								Sel_Index = 15;
							}
						}
						else if (Sel_Index < 16)
						{
							if (GM.Option_Int[4] == 1)
							{
								Sel_Index = 16;
							}
							else
							{
								Sel_Index = 17;
							}
						}
						else
						{
							Sel_Index = 18;
						}
					}
					else if (inputY > 0f)
					{
						if (Sel_Index != 1)
						{
							if (Sel_Index < 4)
							{
								Sel_Index--;
							}
							else if (Sel_Index < 10)
							{
								Sel_Index = 2;
							}
							else if (Sel_Index < 12)
							{
								if (GM.Option_Int[0] >= 0 && GM.Option_Int[0] < 7)
								{
									Sel_Index = 3 + GM.Option_Int[0];
								}
							}
							else if (Sel_Index < 14)
							{
								if (GM.Option_Int[1] == 1)
								{
									Sel_Index = 10;
								}
								else
								{
									Sel_Index = 11;
								}
							}
							else if (Sel_Index < 16)
							{
								if (GM.Option_Int[2] == 1)
								{
									Sel_Index = 12;
								}
								else
								{
									Sel_Index = 13;
								}
							}
							else if (Sel_Index < 18)
							{
								if (GM.Option_Int[3] == 1)
								{
									Sel_Index = 14;
								}
								else
								{
									Sel_Index = 15;
								}
							}
							else if (GM.Option_Int[4] == 1)
							{
								Sel_Index = 16;
							}
							else
							{
								Sel_Index = 17;
							}
						}
					}
					else if (Sel_Index == 1 && (global::UnityEngine.Input.GetKey(global::UnityEngine.KeyCode.LeftArrow) || global::UnityEngine.Input.GetAxis("L_X") < 0f))
					{
						Set_Sound(-1, 0f);
					}
					else if (Sel_Index == 1 && (global::UnityEngine.Input.GetKey(global::UnityEngine.KeyCode.RightArrow) || global::UnityEngine.Input.GetAxis("L_X") > 0f))
					{
						Set_Sound(1, 0f);
					}
					else if (Sel_Index == 2 && (global::UnityEngine.Input.GetKey(global::UnityEngine.KeyCode.LeftArrow) || global::UnityEngine.Input.GetAxis("L_X") < 0f))
					{
						Set_Music(-1, 0f);
					}
					else if (Sel_Index == 2 && (global::UnityEngine.Input.GetKey(global::UnityEngine.KeyCode.RightArrow) || global::UnityEngine.Input.GetAxis("L_X") > 0f))
					{
						Set_Music(1, 0f);
					}
					Check_SoundArrow();
				}
				else if (Menu_State == 3)
				{
					if (inputY < 0f && Sel_Index < 12)
					{
						Sel_Index++;
					}
					else if (inputY > 0f && Sel_Index > 1)
					{
						Sel_Index--;
					}
					if (KeyBox_Opacity > 0f)
					{
						KeyBox_Opacity = global::UnityEngine.Mathf.Lerp(KeyBox_Opacity, 0f, global::UnityEngine.Time.deltaTime * 5f);
					}
					global::UnityEngine.GameObject.Find("Select_KeyConfig").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, KeyBox_Opacity);
					global::UnityEngine.GameObject.Find("Text_AssignedKey").GetComponent<global::UnityEngine.UI.Text>().color = new global::UnityEngine.Color(1f, 1f, 1f, KeyBox_Opacity);
				}
				else if (Menu_State == 4)
				{
					if (inputY < 0f && Sel_Index == 1)
					{
						Sel_Index = 2;
					}
					else if (inputY > 0f && Sel_Index == 2)
					{
						Sel_Index = 1;
					}
					if (Sel_Index == 1)
					{
						QuitRedBox_Opacity = global::UnityEngine.Mathf.Lerp(QuitRedBox_Opacity, 1f, global::UnityEngine.Time.deltaTime * 16f);
					}
					else
					{
						QuitRedBox_Opacity = global::UnityEngine.Mathf.Lerp(QuitRedBox_Opacity, 0f, global::UnityEngine.Time.deltaTime * 8f);
					}
					global::UnityEngine.GameObject.Find("pos_QuitRedBox").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, QuitRedBox_Opacity);
				}
				if (Menu_State > 0)
				{
					if (Prev_Index != Sel_Index)
					{
						if (Sound_MoveFalse > 0)
						{
							Sound_MoveFalse--;
						}
						else if (DelaySound_Timer <= 0f)
						{
							DelaySound_Timer = 0.05f;
							Sound_List.SendMessage("Sound_Move_1");
						}
						Target_Select();
						Show_Item_Text();
					}
					global::UnityEngine.GameObject.Find("Select_Cursor").GetComponent<global::UnityEngine.RectTransform>().position = global::UnityEngine.Vector3.Lerp(global::UnityEngine.GameObject.Find("Select_Cursor").GetComponent<global::UnityEngine.RectTransform>().position, PosTarget, global::UnityEngine.Time.deltaTime * 16f);
					SelCursor_Timer += global::UnityEngine.Time.deltaTime;
					SelCursor_Size = 1f + (1f + global::UnityEngine.Mathf.Sin(SelCursor_Timer * 10f)) * 0.05f;
					if (global::UnityEngine.GameObject.Find("Select_Cursor").GetComponent<global::UnityEngine.RectTransform>().sizeDelta.x > 150f)
					{
						SelCursor_Size = 1f + (1f + global::UnityEngine.Mathf.Sin(SelCursor_Timer * 10f)) * 0.015f;
					}
					global::UnityEngine.GameObject.Find("Select_Cursor").GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(SelCursor_Size, SelCursor_Size, 1f);
					if (SelBG_Opacity < 1f)
					{
						SelBG_Opacity = global::UnityEngine.Mathf.Lerp(SelBG_Opacity, 1f, global::UnityEngine.Time.deltaTime * 2f);
						global::UnityEngine.GameObject.Find("Select_BG").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, SelBG_Opacity);
					}
				}
				if (global::UnityEngine.Input.GetKeyDown(CK.Jump) || global::UnityEngine.Input.GetButtonDown("Jump"))
				{
					if (Menu_State == 1)
					{
						if (Sel_Index == -1 && GM.StatPoints > 0)
						{
							Add_Stat_1();
						}
						else if (Sel_Index == -2 && GM.StatPoints > 0)
						{
							Add_Stat_2();
						}
						else if (Sel_Index == -3 && GM.StatPoints > 0)
						{
							Add_Stat_3();
						}
						else if (Sel_Index == -4 && GM.StatPoints > 0)
						{
							Add_Stat_4();
						}
						else if (Sel_Index == 1 && GM.onWeapon_1 && GM.Weapon_Num != 1)
						{
							Select_Weapon(1);
						}
						else if (Sel_Index == 2 && GM.onWeapon_2 && GM.Weapon_Num != 2)
						{
							Select_Weapon(2);
						}
						else if (Sel_Index == 3 && GM.onWeapon_3 && GM.Weapon_Num != 3)
						{
							Select_Weapon(3);
						}
						else if (Sel_Index == 4 && GM.onWeapon_4 && GM.Weapon_Num != 4)
						{
							Select_Weapon(4);
						}
						else if (Sel_Index == 5 && GM.onWeapon_5 && GM.Weapon_Num != 5)
						{
							Select_Weapon(5);
						}
						else if (Sel_Index == 6 && GM.Skill_Num != 1)
						{
							Select_Skill(1);
						}
						else if (Sel_Index == 7 && GM.onSkill_2 && GM.Skill_Num != 2)
						{
							Select_Skill(2);
						}
						else if (Sel_Index == 8 && GM.onSkill_3 && GM.Skill_Num != 3)
						{
							Select_Skill(3);
						}
						else if (Sel_Index == 9 && GM.onSkill_4 && GM.Skill_Num != 4)
						{
							Select_Skill(4);
						}
						else if (Sel_Index == 10 && GM.onSkill_5 && GM.Skill_Num != 5)
						{
							Select_Skill(5);
						}
					}
					else if (Menu_State == 2)
					{
						if (Sel_Index >= 3)
						{
							if (Sel_Index < 10)
							{
								Select_BGM(Sel_Index - 3);
							}
							else if (Sel_Index < 12)
							{
								Select_WindowMode(Sel_Index - 9);
							}
							else if (Sel_Index < 14)
							{
								Select_Censorship(Sel_Index - 11);
							}
							else if (Sel_Index < 16)
							{
								Select_Hscene(Sel_Index - 13);
							}
							else if (Sel_Index < 18)
							{
								Select_HealthBar(Sel_Index - 15);
							}
							else
							{
								Default_Option();
							}
						}
					}
					else if (Menu_State == 3)
					{
						if (Sel_Index == 12)
						{
							Default_KeyConfig();
						}
						else
						{
							On_KeyBox(Sel_Index);
						}
					}
					else if (Menu_State == 4)
					{
						if (Sel_Index == 1)
						{
							Sound_List.SendMessage("Sound_DeviceOn");
							Sound_List.SendMessage("Sound_Btn");
							Enabled = false;
							GM.Set_FadeOut("Title");
						}
						else
						{
							Off_Menu();
						}
					}
					if (Menu_State == 1)
					{
						StatSel_Num = Sel_Index;
					}
					else
					{
						StatSel_Num = 0;
					}
				}
				else if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.Escape) || global::UnityEngine.Input.GetButtonDown("Start") || global::UnityEngine.Input.GetButtonDown("_B"))
				{
					Off_Menu();
				}
				if (StatSel_Num != 0 && StatSel_Num == Sel_Index && (global::UnityEngine.Input.GetKey(CK.Jump) || global::UnityEngine.Input.GetButton("Jump")))
				{
					StatPress_Timer += global::UnityEngine.Time.deltaTime;
					if (StatPress_Timer > 0.6f)
					{
						StatPress_Timer = 0.5f;
						if (StatSel_Num == -1 && GM.StatPoints > 0)
						{
							Add_Stat_1();
						}
						else if (StatSel_Num == -2 && GM.StatPoints > 0)
						{
							Add_Stat_2();
						}
						else if (StatSel_Num == -3 && GM.StatPoints > 0)
						{
							Add_Stat_3();
						}
						else if (StatSel_Num == -4 && GM.StatPoints > 0)
						{
							Add_Stat_4();
						}
					}
				}
				else
				{
					StatPress_Timer = 0f;
				}
				Prev_Index = Sel_Index;
				prevX = global::UnityEngine.Input.GetAxis("L_X");
				lock_Y = false;
				prevY = global::UnityEngine.Input.GetAxis("L_Y");
				MousePosPrev = MousePos;
			}
			else if (!GM.onSave && !GM.onTeleport && (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.Escape) || global::UnityEngine.Input.GetButtonDown("Start")) && GM.resumeTimer <= 0f)
			{
				On_Menu();
			}
		}
	}

	private void On_Menu()
	{
		GM.onMenu = true;
		Enabled = true;
		Prev_Index = 13;
		Sound_MoveFalse = 1;
		Sound_List.SendMessage("Sound_MenuOn");
		GM.Game_Pause();
		GM.Status_OnMenu();
		Check_Map();
		Check_StatPoint();
		Check_Option();
		Check_KeyConfig();
		GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(332f, 0f, 0f);
		if (GM.Weapon_Num > 0)
		{
			Sel_Index = GM.Weapon_Num;
		}
		else
		{
			Sel_Index = 5 + GM.Skill_Num;
		}
		Target_Select();
	}

	private void Off_Menu()
	{
		global::UnityEngine.PlayerPrefs.SetFloat("SoundVolume", GM.Option_Volume[0]);
		global::UnityEngine.PlayerPrefs.SetFloat("MusicVolume", GM.Option_Volume[1]);
		GM.Game_Resume();
		Enabled = false;
		Sound_List.SendMessage("Sound_MenuOff");
		GetComponent<global::UnityEngine.RectTransform>().localPosition = PosOrig;
		if (Menu_State != 1)
		{
			Menu_State = 1;
			Change_Menu();
		}
	}

	private void Show_Item_Text()
	{
		if (Menu_State == 1)
		{
			string text = string.Empty;
			if (Sel_Index < 0)
			{
				text = Lang_MI.StatText(-Sel_Index, GM.Language_Num);
				if (Sel_Index == -4)
				{
					text = text + " " + Critical_Text();
				}
			}
			else if (Check_Inven(Sel_Index))
			{
				text = Lang_MI.ItemName(Sel_Index, GM.Language_Num);
				if (Sel_Index <= 5)
				{
					text = text + "\nATK  +" + GM.Weapon_DMG[Sel_Index];
				}
				else if (Sel_Index <= 10)
				{
					text = text + "\nMP  -" + GM.Skill_MP[Sel_Index - 6];
				}
				else if (Sel_Index == 11 || Sel_Index == 14)
				{
					text = text + "\n" + GM.Get_KeyText(Sel_Index);
				}
				else if (Sel_Index == 13)
				{
					string text2 = text;
					text = text2 + " : " + Lang_MI.ItemDesc(Sel_Index, GM.Language_Num) + "\n" + GM.Get_KeyText(Sel_Index);
				}
				else
				{
					text = text + "\n" + Lang_MI.ItemDesc(Sel_Index, GM.Language_Num);
				}
			}
			global::UnityEngine.GameObject.Find("Text_ItemDesc").GetComponent<global::UnityEngine.UI.Text>().text = text;
		}
		else
		{
			global::UnityEngine.GameObject.Find("Text_ItemDesc").GetComponent<global::UnityEngine.UI.Text>().text = string.Empty;
		}
	}

	private void Target_Select()
	{
		if (Menu_State != 0)
		{
			if (Menu_State == 1)
			{
				if (Sel_Index < 0)
				{
					PosTarget = global::UnityEngine.GameObject.Find("Stat_Add" + -Sel_Index).GetComponent<global::UnityEngine.RectTransform>().position;
					global::UnityEngine.GameObject.Find("Select_Cursor").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(70f, 70f);
					global::UnityEngine.GameObject.Find("Select_BG").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(366f, 60f);
					global::UnityEngine.GameObject.Find("Select_BG").GetComponent<global::UnityEngine.RectTransform>().position = PosTarget;
					global::UnityEngine.GameObject.Find("Select_BG").GetComponent<global::UnityEngine.RectTransform>().position = new global::UnityEngine.Vector3(PosTarget.x - 1.65f, PosTarget.y, PosTarget.z);
				}
				else if (Sel_Index > 0)
				{
					PosTarget = global::UnityEngine.GameObject.Find("pos_B" + Sel_Index).GetComponent<global::UnityEngine.RectTransform>().position;
					global::UnityEngine.GameObject.Find("Select_Cursor").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(92f, 92f);
					global::UnityEngine.GameObject.Find("Select_BG").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(76f, 76f);
					global::UnityEngine.GameObject.Find("Select_BG").GetComponent<global::UnityEngine.RectTransform>().position = PosTarget;
				}
			}
			else if (Menu_State == 2)
			{
				if (Sel_Index < 0)
				{
					Sel_Index = 1;
				}
				if (Sel_Index <= 2)
				{
					PosTarget = global::UnityEngine.GameObject.Find("pos_Opt" + Sel_Index).GetComponent<global::UnityEngine.RectTransform>().position;
					global::UnityEngine.GameObject.Find("Select_Cursor").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(420f, 90f);
					global::UnityEngine.GameObject.Find("Select_BG").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(404f, 76f);
				}
				else if (Sel_Index < 10)
				{
					PosTarget = global::UnityEngine.GameObject.Find("pos_Opt" + Sel_Index).GetComponent<global::UnityEngine.RectTransform>().position;
					global::UnityEngine.GameObject.Find("Select_Cursor").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(92f, 92f);
					global::UnityEngine.GameObject.Find("Select_BG").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(76f, 76f);
				}
				else if (Sel_Index < 12)
				{
					PosTarget = global::UnityEngine.GameObject.Find("pos_Opt" + Sel_Index).GetComponent<global::UnityEngine.RectTransform>().position;
					global::UnityEngine.GameObject.Find("Select_Cursor").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(290f, 92f);
					global::UnityEngine.GameObject.Find("Select_BG").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(275f, 76f);
				}
				else if (Sel_Index < 18)
				{
					PosTarget = global::UnityEngine.GameObject.Find("pos_Opt" + Sel_Index).GetComponent<global::UnityEngine.RectTransform>().position;
					global::UnityEngine.GameObject.Find("Select_Cursor").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(100f, 70f);
					global::UnityEngine.GameObject.Find("Select_BG").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(78f, 48f);
				}
				else
				{
					PosTarget = global::UnityEngine.GameObject.Find("pos_Opt18").GetComponent<global::UnityEngine.RectTransform>().position;
					global::UnityEngine.GameObject.Find("Select_Cursor").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(157f, 70f);
					global::UnityEngine.GameObject.Find("Select_BG").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(130f, 42f);
				}
				global::UnityEngine.GameObject.Find("Select_BG").GetComponent<global::UnityEngine.RectTransform>().position = PosTarget;
			}
			else if (Menu_State == 3)
			{
				if (Sel_Index < 0)
				{
					Sel_Index = 1;
				}
				if (Sel_Index < 9 || Sel_Index == 11)
				{
					PosTarget = global::UnityEngine.GameObject.Find("pos_Kcfg" + Sel_Index).GetComponent<global::UnityEngine.RectTransform>().position;
					global::UnityEngine.GameObject.Find("Select_Cursor").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(450f, 60f);
					global::UnityEngine.GameObject.Find("Select_BG").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(426f, 36f);
				}
				else if (Sel_Index < 11)
				{
					PosTarget = global::UnityEngine.GameObject.Find("pos_Kcfg" + Sel_Index).GetComponent<global::UnityEngine.RectTransform>().position;
					global::UnityEngine.GameObject.Find("Select_Cursor").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(450f, 80f);
					global::UnityEngine.GameObject.Find("Select_BG").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(426f, 56f);
				}
				else
				{
					PosTarget = global::UnityEngine.GameObject.Find("pos_Kcfg12").GetComponent<global::UnityEngine.RectTransform>().position;
					global::UnityEngine.GameObject.Find("Select_Cursor").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(157f, 70f);
					global::UnityEngine.GameObject.Find("Select_BG").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(130f, 42f);
				}
				global::UnityEngine.GameObject.Find("Select_BG").GetComponent<global::UnityEngine.RectTransform>().position = PosTarget;
			}
			else if (Menu_State == 4)
			{
				if (Sel_Index < 0)
				{
					Sel_Index = 1;
				}
				PosTarget = global::UnityEngine.GameObject.Find("pos_Quit" + Sel_Index).GetComponent<global::UnityEngine.RectTransform>().position;
				global::UnityEngine.GameObject.Find("Select_Cursor").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(196f, 66f);
				global::UnityEngine.GameObject.Find("Select_BG").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(180f, 50f);
				global::UnityEngine.GameObject.Find("Select_BG").GetComponent<global::UnityEngine.RectTransform>().position = PosTarget;
			}
		}
		SelBG_Opacity = 0f;
		global::UnityEngine.GameObject.Find("Select_BG").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, SelBG_Opacity);
	}

	private void Change_Menu()
	{
		if (Enabled)
		{
			Sound_List.SendMessage("Sound_Btn");
		}
		global::UnityEngine.GameObject.Find("Pos_Menu_0").GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(0f, 2000f, 0f);
		global::UnityEngine.GameObject.Find("Pos_Menu_1").GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(0f, 2000f, 0f);
		global::UnityEngine.GameObject.Find("Pos_Menu_2").GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(0f, 2000f, 0f);
		global::UnityEngine.GameObject.Find("Pos_Menu_3").GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(0f, 2000f, 0f);
		global::UnityEngine.GameObject.Find("Pos_Menu_4").GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(0f, 2000f, 0f);
		global::UnityEngine.GameObject.Find("Btn_Menu_0").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
		global::UnityEngine.GameObject.Find("Btn_Menu_1").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
		global::UnityEngine.GameObject.Find("Btn_Menu_2").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
		global::UnityEngine.GameObject.Find("Btn_Menu_3").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
		global::UnityEngine.GameObject.Find("Btn_Menu_4").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
		global::UnityEngine.GameObject.Find("MenuText_0").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, 1f);
		global::UnityEngine.GameObject.Find("MenuText_1").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, 1f);
		global::UnityEngine.GameObject.Find("MenuText_2").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, 1f);
		global::UnityEngine.GameObject.Find("MenuText_3").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, 1f);
		global::UnityEngine.GameObject.Find("Pos_Menu_" + Menu_State).GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(0f, 0f, 0f);
		global::UnityEngine.GameObject.Find("Btn_Menu_" + Menu_State).GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, 1f);
		if (Menu_State < 4)
		{
			global::UnityEngine.GameObject.Find("MenuText_" + Menu_State).GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(0.15f, 0.33f, 0.43f, 1f);
		}
		else
		{
			QuitRedBox_Opacity = 1f;
		}
		if (Menu_State == 1)
		{
			if (GM.Weapon_Num > 0)
			{
				Sel_Index = GM.Weapon_Num;
			}
			else
			{
				Sel_Index = 5 + GM.Skill_Num;
			}
		}
		else
		{
			Sel_Index = 1;
		}
		Target_Select();
		global::UnityEngine.GameObject.Find("Text_ItemDesc").GetComponent<global::UnityEngine.UI.Text>().text = string.Empty;
		if (Menu_State == 0)
		{
			global::UnityEngine.GameObject.Find("Select_Cursor").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
			global::UnityEngine.GameObject.Find("Select_BG").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
			global::UnityEngine.Debug.Log("Menu-----Map");
			global::UnityEngine.GameObject.Find("Menu_Map").GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(global::UnityEngine.GameObject.Find("Pos_Menu_0").GetComponent<global::UnityEngine.RectTransform>().localPosition.x + 325f, global::UnityEngine.GameObject.Find("Pos_Menu_0").GetComponent<global::UnityEngine.RectTransform>().localPosition.y, 0f);
			global::UnityEngine.GameObject.Find("Menu_Map").GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(0.22f, 0.22f, 1f);
			global::UnityEngine.GameObject.Find("Map_Grid").GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(8000f, 0f, 0f);
			global::UnityEngine.GameObject.Find("MapPos_Cursor").GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(3f, 3f, 1f);
			global::UnityEngine.GameObject.Find("Mission_1").GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(2.5f, 2.5f, 1f);
			global::UnityEngine.GameObject.Find("Mission_2").GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(2.5f, 2.5f, 1f);
			global::UnityEngine.GameObject.Find("Mission_3").GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(2.5f, 2.5f, 1f);
			if (GM.EventState < 200)
			{
				global::UnityEngine.GameObject.Find("Mission_4").GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(0f, 0f, 0f);
			}
			else
			{
				global::UnityEngine.GameObject.Find("Mission_4").GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(3f, 3f, 1f);
			}
			global::UnityEngine.GameObject.Find("Mission_4").GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(-1050f, 300f, 1f);
			if (GM.Room_Num > 0)
			{
				global::UnityEngine.GameObject.Find("Map_GunShip").GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(2f, 2f, 1f);
			}
			else
			{
				global::UnityEngine.GameObject.Find("Map_GunShip").GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(0f, 0f, 0f);
			}
			global::UnityEngine.GameObject.Find("MissionBriefing").SendMessage("Set_BriefingPos_Menu");
		}
		else
		{
			global::UnityEngine.GameObject.Find("Select_Cursor").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, 1f);
			global::UnityEngine.GameObject.Find("Select_BG").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, 1f);
			global::UnityEngine.GameObject.Find("Menu_Map").GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(-2500f, 2500f, 0f);
			global::UnityEngine.GameObject.Find("Menu_Map").GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(1f, 1f, 1f);
			global::UnityEngine.GameObject.Find("Map_Grid").GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(0f, 0f, 0f);
			global::UnityEngine.GameObject.Find("MapPos_Cursor").GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(1f, 1f, 1f);
			global::UnityEngine.GameObject.Find("Mission_1").GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(1f, 1f, 1f);
			global::UnityEngine.GameObject.Find("Mission_2").GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(1f, 1f, 1f);
			global::UnityEngine.GameObject.Find("Mission_3").GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(1f, 1f, 1f);
			if (GM.EventState < 200)
			{
				global::UnityEngine.GameObject.Find("Mission_4").GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(0f, 0f, 0f);
			}
			else
			{
				global::UnityEngine.GameObject.Find("Mission_4").GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(1f, 1f, 1f);
			}
			global::UnityEngine.GameObject.Find("Mission_4").GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(-1050f, 170f, 1f);
			global::UnityEngine.GameObject.Find("Map_GunShip").GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(1f, 1f, 1f);
			global::UnityEngine.GameObject.Find("MissionBriefing").SendMessage("Hide_BriefingPos");
		}
	}

	private void Check_LRB()
	{
		if ((global::UnityEngine.Input.GetButtonDown("L_B") || global::UnityEngine.Input.GetKeyDown(CK.LB)) && Menu_State > 0)
		{
			Menu_State--;
			Change_Menu();
		}
		else if ((global::UnityEngine.Input.GetButtonDown("R_B") || global::UnityEngine.Input.GetKeyDown(CK.RB)) && Menu_State < 4)
		{
			Menu_State++;
			Change_Menu();
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
				if (rayIntersection.collider.name == "Btn_Menu_0" && Menu_State != 0)
				{
					Menu_State = 0;
					Change_Menu();
				}
				else if (rayIntersection.collider.name == "Btn_Menu_1" && Menu_State != 1)
				{
					Menu_State = 1;
					Change_Menu();
				}
				else if (rayIntersection.collider.name == "Btn_Menu_2" && Menu_State != 2)
				{
					Menu_State = 2;
					Change_Menu();
				}
				else if (rayIntersection.collider.name == "Btn_Menu_3" && Menu_State != 3)
				{
					Menu_State = 3;
					Change_Menu();
				}
				else if (rayIntersection.collider.name == "Btn_Menu_4" && Menu_State != 4)
				{
					Menu_State = 4;
					Change_Menu();
				}
				else if (Menu_State == 1)
				{
					if (rayIntersection.collider.name == "pos_B1" && GM.onWeapon_1 && GM.Weapon_Num != 1)
					{
						Select_Weapon(1);
					}
					else if (rayIntersection.collider.name == "pos_B2" && GM.onWeapon_2 && GM.Weapon_Num != 2)
					{
						Select_Weapon(2);
					}
					else if (rayIntersection.collider.name == "pos_B3" && GM.onWeapon_3 && GM.Weapon_Num != 3)
					{
						Select_Weapon(3);
					}
					else if (rayIntersection.collider.name == "pos_B4" && GM.onWeapon_4 && GM.Weapon_Num != 4)
					{
						Select_Weapon(4);
					}
					else if (rayIntersection.collider.name == "pos_B5" && GM.onWeapon_5 && GM.Weapon_Num != 5)
					{
						Select_Weapon(5);
					}
					else if (rayIntersection.collider.name == "pos_B6" && GM.Skill_Num != 1)
					{
						Select_Skill(1);
					}
					else if (rayIntersection.collider.name == "pos_B7" && GM.onSkill_2 && GM.Skill_Num != 2)
					{
						Select_Skill(2);
					}
					else if (rayIntersection.collider.name == "pos_B8" && GM.onSkill_3 && GM.Skill_Num != 3)
					{
						Select_Skill(3);
					}
					else if (rayIntersection.collider.name == "pos_B9" && GM.onSkill_4 && GM.Skill_Num != 4)
					{
						Select_Skill(4);
					}
					else if (rayIntersection.collider.name == "pos_B10" && GM.onSkill_5 && GM.Skill_Num != 5)
					{
						Select_Skill(5);
					}
					else if (rayIntersection.collider.name == "Stat_Add1" && GM.StatPoints > 0)
					{
						flag = true;
						Add_Stat_1();
					}
					else if (rayIntersection.collider.name == "Stat_Add2" && GM.StatPoints > 0)
					{
						flag = true;
						Add_Stat_2();
					}
					else if (rayIntersection.collider.name == "Stat_Add3" && GM.StatPoints > 0)
					{
						flag = true;
						Add_Stat_3();
					}
					else if (rayIntersection.collider.name == "Stat_Add4" && GM.StatPoints > 0)
					{
						flag = true;
						Add_Stat_4();
					}
				}
				else if (Menu_State == 2)
				{
					if (rayIntersection.collider.name == "pos_Opt1")
					{
						float posMouse = (global::UnityEngine.GameObject.Find("UI Camera").GetComponent<UnityEngine.Camera>().ScreenToWorldPoint(global::UnityEngine.Input.mousePosition).x - global::UnityEngine.GameObject.Find("pos_SoundBarMin").GetComponent<global::UnityEngine.RectTransform>().position.x) / (global::UnityEngine.GameObject.Find("pos_SoundBarMax").GetComponent<global::UnityEngine.RectTransform>().position.x - global::UnityEngine.GameObject.Find("pos_SoundBarMin").GetComponent<global::UnityEngine.RectTransform>().position.x);
						Set_Sound(0, posMouse);
					}
					else if (rayIntersection.collider.name == "pos_Opt2")
					{
						float posMouse2 = (global::UnityEngine.GameObject.Find("UI Camera").GetComponent<UnityEngine.Camera>().ScreenToWorldPoint(global::UnityEngine.Input.mousePosition).x - global::UnityEngine.GameObject.Find("pos_MusicBarMin").GetComponent<global::UnityEngine.RectTransform>().position.x) / (global::UnityEngine.GameObject.Find("pos_MusicBarMax").GetComponent<global::UnityEngine.RectTransform>().position.x - global::UnityEngine.GameObject.Find("pos_MusicBarMin").GetComponent<global::UnityEngine.RectTransform>().position.x);
						Set_Music(0, posMouse2);
					}
					else if (rayIntersection.collider.name == "pos_Opt3")
					{
						Select_BGM(0);
					}
					else if (rayIntersection.collider.name == "pos_Opt4")
					{
						Select_BGM(1);
					}
					else if (rayIntersection.collider.name == "pos_Opt5")
					{
						Select_BGM(2);
					}
					else if (rayIntersection.collider.name == "pos_Opt6")
					{
						Select_BGM(3);
					}
					else if (rayIntersection.collider.name == "pos_Opt7")
					{
						Select_BGM(4);
					}
					else if (rayIntersection.collider.name == "pos_Opt8")
					{
						Select_BGM(5);
					}
					else if (rayIntersection.collider.name == "pos_Opt9")
					{
						Select_BGM(6);
					}
					else if (rayIntersection.collider.name == "pos_Opt10")
					{
						Select_WindowMode(1);
					}
					else if (rayIntersection.collider.name == "pos_Opt11")
					{
						Select_WindowMode(2);
					}
					else if (rayIntersection.collider.name == "pos_Opt12")
					{
						Select_Censorship(1);
					}
					else if (rayIntersection.collider.name == "pos_Opt13")
					{
						Select_Censorship(2);
					}
					else if (rayIntersection.collider.name == "pos_Opt14")
					{
						Select_Hscene(1);
					}
					else if (rayIntersection.collider.name == "pos_Opt15")
					{
						Select_Hscene(2);
					}
					else if (rayIntersection.collider.name == "pos_Opt16")
					{
						Select_HealthBar(1);
					}
					else if (rayIntersection.collider.name == "pos_Opt17")
					{
						Select_HealthBar(2);
					}
					else if (rayIntersection.collider.name == "pos_Opt18")
					{
						Default_Option();
					}
				}
				else if (Menu_State == 3)
				{
					if (rayIntersection.collider.name == "pos_Kcfg1")
					{
						On_KeyBox(1);
					}
					else if (rayIntersection.collider.name == "pos_Kcfg2")
					{
						On_KeyBox(2);
					}
					else if (rayIntersection.collider.name == "pos_Kcfg3")
					{
						On_KeyBox(3);
					}
					else if (rayIntersection.collider.name == "pos_Kcfg4")
					{
						On_KeyBox(4);
					}
					else if (rayIntersection.collider.name == "pos_Kcfg5")
					{
						On_KeyBox(5);
					}
					else if (rayIntersection.collider.name == "pos_Kcfg6")
					{
						On_KeyBox(6);
					}
					else if (rayIntersection.collider.name == "pos_Kcfg7")
					{
						On_KeyBox(7);
					}
					else if (rayIntersection.collider.name == "pos_Kcfg8")
					{
						On_KeyBox(8);
					}
					else if (rayIntersection.collider.name == "pos_Kcfg9")
					{
						On_KeyBox(9);
					}
					else if (rayIntersection.collider.name == "pos_Kcfg10")
					{
						On_KeyBox(10);
					}
					else if (rayIntersection.collider.name == "pos_Kcfg11")
					{
						On_KeyBox(11);
					}
					else if (rayIntersection.collider.name == "pos_Kcfg12")
					{
						Default_KeyConfig();
					}
				}
				else if (Menu_State == 4)
				{
					if (rayIntersection.collider.name == "pos_Quit1")
					{
						Sound_List.SendMessage("Sound_DeviceOn");
						Sound_List.SendMessage("Sound_Btn");
						GM.Set_FadeOut("Title");
					}
					else if (rayIntersection.collider.name == "pos_Quit2")
					{
						Sound_MoveFalse = 1;
						Off_Menu();
					}
				}
			}
		}
		else
		{
			global::UnityEngine.Ray ray2 = global::UnityEngine.GameObject.Find("UI Camera").GetComponent<UnityEngine.Camera>().ScreenPointToRay(global::UnityEngine.Input.mousePosition);
			global::UnityEngine.RaycastHit2D rayIntersection2 = global::UnityEngine.Physics2D.GetRayIntersection(ray2, float.PositiveInfinity);
			if (rayIntersection2.collider != null)
			{
				if (Menu_State == 1)
				{
					if (rayIntersection2.collider.name.Substring(0, 5) == "pos_B")
					{
						int num = int.Parse(rayIntersection2.collider.name.Substring(5, rayIntersection2.collider.name.Length - 5));
						if (num != Prev_Index && Check_Inven(num))
						{
							Sel_Index = num;
						}
					}
					else if (rayIntersection2.collider.name.Substring(0, 8) == "Stat_Add" && GM.StatPoints > 0)
					{
						if (global::UnityEngine.Input.GetMouseButton(0))
						{
							flag = true;
							StatPush_Timer += global::UnityEngine.Time.deltaTime;
							if (StatPush_Timer > 0.6f)
							{
								StatPush_Timer = 0.5f;
								if (rayIntersection2.collider.name == "Stat_Add1" && GM.StatPoints > 0)
								{
									Add_Stat_1();
								}
								else if (rayIntersection2.collider.name == "Stat_Add2" && GM.StatPoints > 0)
								{
									Add_Stat_2();
								}
								else if (rayIntersection2.collider.name == "Stat_Add3" && GM.StatPoints > 0)
								{
									Add_Stat_3();
								}
								else if (rayIntersection2.collider.name == "Stat_Add4" && GM.StatPoints > 0)
								{
									Add_Stat_4();
								}
							}
						}
						else
						{
							StatPush_Timer = 0f;
						}
						Sel_Index = int.Parse(rayIntersection2.collider.name.Substring(8, 1)) * -1;
					}
				}
				else if (Menu_State == 2)
				{
					if (global::UnityEngine.Input.GetMouseButton(0) && rayIntersection2.collider.name == "pos_Opt1")
					{
						if (Sel_Index == 1)
						{
							Sel_Index = 1;
						}
						float posMouse3 = (global::UnityEngine.GameObject.Find("UI Camera").GetComponent<UnityEngine.Camera>().ScreenToWorldPoint(global::UnityEngine.Input.mousePosition).x - global::UnityEngine.GameObject.Find("pos_SoundBarMin").GetComponent<global::UnityEngine.RectTransform>().position.x) / (global::UnityEngine.GameObject.Find("pos_SoundBarMax").GetComponent<global::UnityEngine.RectTransform>().position.x - global::UnityEngine.GameObject.Find("pos_SoundBarMin").GetComponent<global::UnityEngine.RectTransform>().position.x);
						Set_Sound(0, posMouse3);
					}
					else if (global::UnityEngine.Input.GetMouseButton(0) && rayIntersection2.collider.name == "pos_Opt2")
					{
						if (Sel_Index == 1)
						{
							Sel_Index = 1;
						}
						float posMouse4 = (global::UnityEngine.GameObject.Find("UI Camera").GetComponent<UnityEngine.Camera>().ScreenToWorldPoint(global::UnityEngine.Input.mousePosition).x - global::UnityEngine.GameObject.Find("pos_MusicBarMin").GetComponent<global::UnityEngine.RectTransform>().position.x) / (global::UnityEngine.GameObject.Find("pos_MusicBarMax").GetComponent<global::UnityEngine.RectTransform>().position.x - global::UnityEngine.GameObject.Find("pos_MusicBarMin").GetComponent<global::UnityEngine.RectTransform>().position.x);
						Set_Music(0, posMouse4);
					}
					else if (rayIntersection2.collider.name.Substring(0, 7) == "pos_Opt")
					{
						int num2 = int.Parse(rayIntersection2.collider.name.Substring(7, rayIntersection2.collider.name.Length - 7));
						if (num2 != Sel_Index)
						{
							Sel_Index = num2;
						}
					}
				}
				else if (Menu_State == 3)
				{
					if (rayIntersection2.collider.name.Substring(0, 8) == "pos_Kcfg")
					{
						int num3 = int.Parse(rayIntersection2.collider.name.Substring(8, rayIntersection2.collider.name.Length - 8));
						if (num3 != Sel_Index)
						{
							Sel_Index = num3;
						}
					}
				}
				else if (Menu_State == 4 && rayIntersection2.collider.name.Substring(0, 8) == "pos_Quit")
				{
					int num4 = int.Parse(rayIntersection2.collider.name.Substring(8, 1));
					if (num4 != Sel_Index)
					{
						Sel_Index = num4;
					}
				}
			}
		}
		if (!flag || global::UnityEngine.Input.GetMouseButtonUp(0))
		{
			StatPush_Timer = 0f;
		}
	}

	private bool Check_Inven(int num)
	{
		switch (num)
		{
		case 1:
			return GM.onWeapon_1;
		case 2:
			return GM.onWeapon_2;
		case 3:
			return GM.onWeapon_3;
		case 4:
			return GM.onWeapon_4;
		case 5:
			return GM.onWeapon_5;
		case 6:
			return true;
		case 7:
			return GM.onSkill_2;
		case 8:
			return GM.onSkill_3;
		case 9:
			return GM.onSkill_4;
		case 10:
			return GM.onSkill_5;
		case 11:
			return GM.onBackDash;
		case 12:
			return GM.onDBJump;
		case 13:
			return GM.onSpeedUp;
		case 14:
			return GM.onHighJump;
		case 15:
			return GM.onScrew;
		case 16:
			return GM.onCard_1;
		case 17:
			return GM.onCard_2;
		case 18:
			return GM.onCard_3;
		case 19:
			return GM.onCard_4;
		case 20:
			return GM.onCard_5;
		case 21:
			if (GM.Bonus_HP > 0)
			{
				return true;
			}
			break;
		}
		if (num == 22 && GM.Bonus_MP > 0)
		{
			return true;
		}
		if (num == 23 && GM.Bonus_ATK > 0)
		{
			return true;
		}
		if (num == 24 && GM.Bonus_Regen > 0)
		{
			return true;
		}
		if (num == 25 && GM.Bonus_Blood > 0)
		{
			return true;
		}
		return false;
	}

	private void Add_Stat_1()
	{
		GM.StatAdd_STR();
		StatBtn_Timer[0] = 1f;
		AddText_Timer[2] = 1.2f;
		global::UnityEngine.GameObject.Find("Stat_Add1").GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(global::UnityEngine.GameObject.Find("Stat_Add1").GetComponent<global::UnityEngine.RectTransform>().localPosition.x, global::UnityEngine.GameObject.Find("Stat_Add1").GetComponent<global::UnityEngine.RectTransform>().localPosition.y - 3f, global::UnityEngine.GameObject.Find("Stat_Add1").GetComponent<global::UnityEngine.RectTransform>().localPosition.z);
		global::UnityEngine.GameObject.Find("ATK_Add").GetComponent<global::UnityEngine.UI.Text>().text = "+" + GM.Atk_Per_Str;
		Check_StatPoint();
		GM.Status_Update();
	}

	private void Add_Stat_2()
	{
		GM.StatAdd_CON();
		StatBtn_Timer[1] = 1f;
		AddText_Timer[0] = 1.2f;
		AddText_Timer[3] = 1.2f;
		global::UnityEngine.GameObject.Find("Stat_Add2").GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(global::UnityEngine.GameObject.Find("Stat_Add2").GetComponent<global::UnityEngine.RectTransform>().localPosition.x, global::UnityEngine.GameObject.Find("Stat_Add2").GetComponent<global::UnityEngine.RectTransform>().localPosition.y - 3f, global::UnityEngine.GameObject.Find("Stat_Add2").GetComponent<global::UnityEngine.RectTransform>().localPosition.z);
		global::UnityEngine.GameObject.Find("HP_Add").GetComponent<global::UnityEngine.UI.Text>().text = "+" + GM.HP_Per_Con;
		global::UnityEngine.GameObject.Find("DEF_Add").GetComponent<global::UnityEngine.UI.Text>().text = "+" + GM.Def_Per_Con;
		Check_StatPoint();
		GM.Status_Update();
	}

	private void Add_Stat_3()
	{
		GM.StatAdd_INT();
		StatBtn_Timer[2] = 1f;
		AddText_Timer[1] = 1.2f;
		global::UnityEngine.GameObject.Find("Stat_Add3").GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(global::UnityEngine.GameObject.Find("Stat_Add3").GetComponent<global::UnityEngine.RectTransform>().localPosition.x, global::UnityEngine.GameObject.Find("Stat_Add3").GetComponent<global::UnityEngine.RectTransform>().localPosition.y - 3f, global::UnityEngine.GameObject.Find("Stat_Add3").GetComponent<global::UnityEngine.RectTransform>().localPosition.z);
		global::UnityEngine.GameObject.Find("MP_Add").GetComponent<global::UnityEngine.UI.Text>().text = "+" + GM.MP_Per_Int;
		Check_StatPoint();
		GM.Status_Update();
	}

	private void Add_Stat_4()
	{
		GM.StatAdd_LCK();
		StatBtn_Timer[3] = 1f;
		global::UnityEngine.GameObject.Find("Stat_Add4").GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(global::UnityEngine.GameObject.Find("Stat_Add4").GetComponent<global::UnityEngine.RectTransform>().localPosition.x, global::UnityEngine.GameObject.Find("Stat_Add4").GetComponent<global::UnityEngine.RectTransform>().localPosition.y - 3f, global::UnityEngine.GameObject.Find("Stat_Add4").GetComponent<global::UnityEngine.RectTransform>().localPosition.z);
		Check_StatPoint();
		GM.Status_Update();
		string text = Lang_MI.StatText(4, GM.Language_Num) + " " + Critical_Text();
		global::UnityEngine.GameObject.Find("Text_ItemDesc").GetComponent<global::UnityEngine.UI.Text>().text = text;
		float num = (float)int.Parse(global::UnityEngine.GameObject.Find("LCK_Num").GetComponent<global::UnityEngine.UI.Text>().text) * 0.5f;
		if (num > 100f)
		{
			num = 100f;
		}
		global::UnityEngine.GameObject.Find("Critical_Num").GetComponent<global::UnityEngine.UI.Text>().text = "CRITICAL  " + num.ToString("f1") + "%";
	}

	private void Check_StatPoint()
	{
		if (GM.StatPoints > 0 && !onStatPoints)
		{
			onStatPoints = true;
			global::UnityEngine.GameObject.Find("StatPointsBox").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, 1f);
			global::UnityEngine.GameObject.Find("Stat_Add1").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, 1f);
			global::UnityEngine.GameObject.Find("Stat_Add2").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, 1f);
			global::UnityEngine.GameObject.Find("Stat_Add3").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, 1f);
			global::UnityEngine.GameObject.Find("Stat_Add4").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, 1f);
			global::UnityEngine.GameObject.Find("StatPoints").GetComponent<global::UnityEngine.UI.Text>().color = new global::UnityEngine.Color(1f, 0.98f, 0.9f, 1f);
			global::UnityEngine.GameObject.Find("StatPoints_Num").GetComponent<global::UnityEngine.UI.Text>().color = new global::UnityEngine.Color(1f, 0.98f, 0.9f, 1f);
		}
		else if (GM.StatPoints == 0 && onStatPoints)
		{
			onStatPoints = false;
			global::UnityEngine.GameObject.Find("StatPointsBox").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
			global::UnityEngine.GameObject.Find("Stat_Add1").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
			global::UnityEngine.GameObject.Find("Stat_Add2").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
			global::UnityEngine.GameObject.Find("Stat_Add3").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
			global::UnityEngine.GameObject.Find("Stat_Add4").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
			global::UnityEngine.GameObject.Find("StatPoints").GetComponent<global::UnityEngine.UI.Text>().color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
			global::UnityEngine.GameObject.Find("StatPoints_Num").GetComponent<global::UnityEngine.UI.Text>().color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
			if (Sel_Index < 0)
			{
				Sel_Index = 6;
			}
		}
	}

	private void Check_AddTextColor()
	{
		if (StatBtn_Timer[0] > 0f)
		{
			StatBtn_Timer[0] -= global::UnityEngine.Time.deltaTime;
			global::UnityEngine.GameObject.Find("Stat_Add1").GetComponent<global::UnityEngine.RectTransform>().localPosition = global::UnityEngine.Vector3.Lerp(global::UnityEngine.GameObject.Find("Stat_Add1").GetComponent<global::UnityEngine.RectTransform>().localPosition, pos_StatBtn[0], global::UnityEngine.Time.deltaTime * 10f);
		}
		if (StatBtn_Timer[1] > 0f)
		{
			StatBtn_Timer[1] -= global::UnityEngine.Time.deltaTime;
			global::UnityEngine.GameObject.Find("Stat_Add2").GetComponent<global::UnityEngine.RectTransform>().localPosition = global::UnityEngine.Vector3.Lerp(global::UnityEngine.GameObject.Find("Stat_Add2").GetComponent<global::UnityEngine.RectTransform>().localPosition, pos_StatBtn[1], global::UnityEngine.Time.deltaTime * 10f);
		}
		if (StatBtn_Timer[2] > 0f)
		{
			StatBtn_Timer[2] -= global::UnityEngine.Time.deltaTime;
			global::UnityEngine.GameObject.Find("Stat_Add3").GetComponent<global::UnityEngine.RectTransform>().localPosition = global::UnityEngine.Vector3.Lerp(global::UnityEngine.GameObject.Find("Stat_Add3").GetComponent<global::UnityEngine.RectTransform>().localPosition, pos_StatBtn[2], global::UnityEngine.Time.deltaTime * 10f);
		}
		if (StatBtn_Timer[3] > 0f)
		{
			StatBtn_Timer[3] -= global::UnityEngine.Time.deltaTime;
			global::UnityEngine.GameObject.Find("Stat_Add4").GetComponent<global::UnityEngine.RectTransform>().localPosition = global::UnityEngine.Vector3.Lerp(global::UnityEngine.GameObject.Find("Stat_Add4").GetComponent<global::UnityEngine.RectTransform>().localPosition, pos_StatBtn[3], global::UnityEngine.Time.deltaTime * 10f);
		}
		if (AddText_Timer[0] > 0f && AddText_Timer[0] < 100f)
		{
			AddText_Timer[0] -= global::UnityEngine.Time.deltaTime;
			global::UnityEngine.GameObject.Find("HP_Add").GetComponent<global::UnityEngine.UI.Text>().color = new global::UnityEngine.Color(0.5f, 1f, 0.9f, AddText_Timer[0]);
			global::UnityEngine.GameObject.Find("DEF_Add").GetComponent<global::UnityEngine.UI.Text>().color = new global::UnityEngine.Color(0.5f, 1f, 0.9f, AddText_Timer[0]);
		}
		if (AddText_Timer[1] > 0f && AddText_Timer[1] < 100f)
		{
			AddText_Timer[1] -= global::UnityEngine.Time.deltaTime;
			global::UnityEngine.GameObject.Find("MP_Add").GetComponent<global::UnityEngine.UI.Text>().color = new global::UnityEngine.Color(0.5f, 1f, 0.9f, AddText_Timer[1]);
		}
		if (AddText_Timer[2] > 0f && AddText_Timer[2] < 100f)
		{
			AddText_Timer[2] -= global::UnityEngine.Time.deltaTime;
			global::UnityEngine.GameObject.Find("ATK_Add").GetComponent<global::UnityEngine.UI.Text>().color = new global::UnityEngine.Color(0.5f, 1f, 0.9f, AddText_Timer[2]);
		}
		if (AddText_Timer[3] > 0f && AddText_Timer[3] < 100f)
		{
			AddText_Timer[3] -= global::UnityEngine.Time.deltaTime;
			global::UnityEngine.GameObject.Find("DEF_Add").GetComponent<global::UnityEngine.UI.Text>().color = new global::UnityEngine.Color(0.5f, 1f, 0.9f, AddText_Timer[3]);
		}
	}

	private void Select_Weapon(int num)
	{
		global::UnityEngine.GameObject.Find("ATK_Add").GetComponent<global::UnityEngine.UI.Text>().text = "+" + GM.Weapon_DMG[num];
		AddText_Timer[2] = 1.2f;
		GM.Change_Weapon(num);
		Sound_List.SendMessage("Sound_Tab");
		Sound_List.SendMessage("Sound_DeviceOn");
		global::UnityEngine.GameObject.Find("SelBorder_Aug").GetComponent<global::UnityEngine.RectTransform>().localPosition = global::UnityEngine.GameObject.Find("pos_B" + num).GetComponent<global::UnityEngine.RectTransform>().localPosition;
	}

	private void Select_Skill(int num)
	{
		GM.Select_Skill(num);
		Sound_List.SendMessage("Sound_Tab");
		global::UnityEngine.GameObject.Find("SelBorder_Skill").GetComponent<global::UnityEngine.RectTransform>().localPosition = global::UnityEngine.GameObject.Find("pos_B" + (num + 5)).GetComponent<global::UnityEngine.RectTransform>().localPosition;
	}

	private void Check_Map()
	{
	}

	private void Check_Option()
	{
		global::UnityEngine.GameObject.Find("Sound_Bar").GetComponent<global::UnityEngine.UI.Image>().fillAmount = GM.Option_Volume[0];
		global::UnityEngine.GameObject.Find("Music_Bar").GetComponent<global::UnityEngine.UI.Image>().fillAmount = GM.Option_Volume[1];
		global::UnityEngine.GameObject.Find("SelBorder_BGM").GetComponent<global::UnityEngine.RectTransform>().localPosition = global::UnityEngine.GameObject.Find("pos_Opt" + (3 + GM.Option_Int[0])).GetComponent<global::UnityEngine.RectTransform>().localPosition;
		global::UnityEngine.GameObject.Find("SelBorder_Window").GetComponent<global::UnityEngine.RectTransform>().localPosition = global::UnityEngine.GameObject.Find("pos_Opt" + (9 + GM.Option_Int[1])).GetComponent<global::UnityEngine.RectTransform>().localPosition;
		global::UnityEngine.GameObject.Find("SelBorder_Censorship").GetComponent<global::UnityEngine.RectTransform>().localPosition = global::UnityEngine.GameObject.Find("pos_Opt" + (11 + GM.Option_Int[2])).GetComponent<global::UnityEngine.RectTransform>().localPosition;
		global::UnityEngine.GameObject.Find("SelBorder_Hscene").GetComponent<global::UnityEngine.RectTransform>().localPosition = global::UnityEngine.GameObject.Find("pos_Opt" + (13 + GM.Option_Int[3])).GetComponent<global::UnityEngine.RectTransform>().localPosition;
		global::UnityEngine.GameObject.Find("SelBorder_HealthBar").GetComponent<global::UnityEngine.RectTransform>().localPosition = global::UnityEngine.GameObject.Find("pos_Opt" + (15 + GM.Option_Int[4])).GetComponent<global::UnityEngine.RectTransform>().localPosition;
	}

	private void Check_SoundArrow()
	{
		SelArrow_Opacity[0] = global::UnityEngine.Mathf.Lerp(SelArrow_Opacity[0], (Sel_Index != 1) ? 0.1f : 1f, global::UnityEngine.Time.deltaTime * 5f);
		global::UnityEngine.GameObject.Find("Opt_SoundArrowL").GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, SelArrow_Opacity[0]);
		global::UnityEngine.GameObject.Find("Opt_SoundArrowR").GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, SelArrow_Opacity[0]);
		SelArrow_Opacity[1] = global::UnityEngine.Mathf.Lerp(SelArrow_Opacity[1], (Sel_Index != 2) ? 0.1f : 1f, global::UnityEngine.Time.deltaTime * 5f);
		global::UnityEngine.GameObject.Find("Opt_MusicArrowL").GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, SelArrow_Opacity[1]);
		global::UnityEngine.GameObject.Find("Opt_MusicArrowR").GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, SelArrow_Opacity[1]);
	}

	private void Set_Sound(int LR, float posMouse)
	{
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
			GM.Option_Volume[0] = posMouse;
		}
		else
		{
			float num = GM.Option_Volume[0];
			num += (float)LR * global::UnityEngine.Time.deltaTime * 0.4f;
			if (num > 1f)
			{
				GM.Option_Volume[0] = 1f;
			}
			else if (num < 0f)
			{
				GM.Option_Volume[0] = 0f;
			}
			else
			{
				GM.Option_Volume[0] = num;
			}
		}
		global::UnityEngine.GameObject.Find("Sound_Bar").GetComponent<global::UnityEngine.UI.Image>().fillAmount = GM.Option_Volume[0];
		if (SoundVolume_Timer <= 0f)
		{
			Sound_List.SendMessage("Sound_Move_2");
			SoundVolume_Timer = 0.15f;
		}
	}

	private void Set_Music(int LR, float posMouse)
	{
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
			GM.Option_Volume[1] = posMouse;
		}
		else
		{
			float num = GM.Option_Volume[1];
			num += (float)LR * global::UnityEngine.Time.deltaTime * 0.4f;
			if (num > 1f)
			{
				GM.Option_Volume[1] = 1f;
			}
			else if (num < 0f)
			{
				GM.Option_Volume[1] = 0f;
			}
			else
			{
				GM.Option_Volume[1] = num;
			}
		}
		global::UnityEngine.GameObject.Find("Music_Bar").GetComponent<global::UnityEngine.UI.Image>().fillAmount = GM.Option_Volume[1];
		if (SoundVolume_Timer <= 0f)
		{
			Sound_List.SendMessage("Sound_Move_2");
			SoundVolume_Timer = 0.15f;
		}
	}

	private void Select_BGM(int num)
	{
		GM.Option_Int[0] = num;
		Sound_List.SendMessage("Sound_Tab");
		global::UnityEngine.GameObject.Find("SelBorder_BGM").GetComponent<global::UnityEngine.RectTransform>().localPosition = global::UnityEngine.GameObject.Find("pos_Opt" + (3 + GM.Option_Int[0])).GetComponent<global::UnityEngine.RectTransform>().localPosition;
		global::UnityEngine.PlayerPrefs.SetInt("SelBGM", num);
		global::UnityEngine.GameObject.Find("BGM_List").GetComponent<BGM_Control>().Play(num);
	}

	private void Select_WindowMode(int num)
	{
		GM.Option_Int[1] = num;
		Sound_List.SendMessage("Sound_Tab");
		global::UnityEngine.GameObject.Find("SelBorder_Window").GetComponent<global::UnityEngine.RectTransform>().localPosition = global::UnityEngine.GameObject.Find("pos_Opt" + (9 + GM.Option_Int[1])).GetComponent<global::UnityEngine.RectTransform>().localPosition;
		if (num == 2 && global::UnityEngine.PlayerPrefs.GetInt("WindowSize") != 1920)
		{
			global::UnityEngine.PlayerPrefs.SetInt("WindowSize", 1920);
			global::UnityEngine.Screen.SetResolution(1920, 1080, true);
		}
		else if (num == 1 && global::UnityEngine.PlayerPrefs.GetInt("WindowSize") != 1280)
		{
			global::UnityEngine.PlayerPrefs.SetInt("WindowSize", 1280);
			global::UnityEngine.Screen.SetResolution(1280, 720, false);
		}
	}

	private void Select_Censorship(int num)
	{
		GM.Option_Int[2] = num;
		Sound_List.SendMessage("Sound_Tab");
		global::UnityEngine.GameObject.Find("SelBorder_Censorship").GetComponent<global::UnityEngine.RectTransform>().localPosition = global::UnityEngine.GameObject.Find("pos_Opt" + (11 + GM.Option_Int[2])).GetComponent<global::UnityEngine.RectTransform>().localPosition;
		global::UnityEngine.PlayerPrefs.SetInt("Censorship", num);
	}

	private void Select_Hscene(int num)
	{
		GM.Option_Int[3] = num;
		Sound_List.SendMessage("Sound_Tab");
		global::UnityEngine.GameObject.Find("SelBorder_Hscene").GetComponent<global::UnityEngine.RectTransform>().localPosition = global::UnityEngine.GameObject.Find("pos_Opt" + (13 + num)).GetComponent<global::UnityEngine.RectTransform>().localPosition;
		global::UnityEngine.PlayerPrefs.SetInt("On_Hscene", num);
		global::UnityEngine.Debug.Log("Select_Hscene : " + num);
	}

	private void Select_HealthBar(int num)
	{
		GM.Option_Int[4] = num;
		Sound_List.SendMessage("Sound_Tab");
		global::UnityEngine.GameObject.Find("SelBorder_HealthBar").GetComponent<global::UnityEngine.RectTransform>().localPosition = global::UnityEngine.GameObject.Find("pos_Opt" + (15 + num)).GetComponent<global::UnityEngine.RectTransform>().localPosition;
		global::UnityEngine.PlayerPrefs.SetInt("On_HealthBar", num);
		global::UnityEngine.Debug.Log("Select_HealthBar : " + num);
	}

	private void Default_Option()
	{
		GM.Reset_Option();
		Sound_List.SendMessage("Sound_Tab");
		Check_Option();
	}

	private void On_KeyBox(int num)
	{
		onKeyConfigBox = true;
		KeyBox_Opacity = 0f;
		Sound_List.SendMessage("Sound_Tab");
		global::UnityEngine.GameObject.Find("Select_KeyConfig").GetComponent<global::UnityEngine.RectTransform>().position = global::UnityEngine.GameObject.Find("pos_Kcfg" + num).GetComponent<global::UnityEngine.RectTransform>().position;
		global::UnityEngine.GameObject.Find("Text_AssignedKey").GetComponent<global::UnityEngine.UI.Text>().text = global::UnityEngine.GameObject.Find("KeyAssigned_" + num).GetComponent<global::UnityEngine.UI.Text>().text;
	}

	private void Check_KeyConfig()
	{
		global::UnityEngine.GameObject.Find("Select_KeyConfig").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
		global::UnityEngine.GameObject.Find("Text_AssignedKey").GetComponent<global::UnityEngine.UI.Text>().color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
	}

	private void Default_KeyConfig()
	{
		Sound_List.SendMessage("Sound_Tab");
		CK.Reset_KeyConfig();
	}

	private string Critical_Text()
	{
		float num = (float)int.Parse(global::UnityEngine.GameObject.Find("LCK_Num").GetComponent<global::UnityEngine.UI.Text>().text) * 0.5f;
		if (num > 100f)
		{
			num = 100f;
		}
		return "+ 0.5%  ( " + num.ToString("f1") + "% )";
	}

	private void Hscene_Bar_KB()
	{
		H_Bar_img_R.enabled = true;
		H_Bar_img_L.enabled = true;
		H_Bar_text_R.enabled = false;
		H_Bar_text_L.enabled = false;
	}

	private void Hscene_Bar_Pad()
	{
		H_Bar_img_R.enabled = false;
		H_Bar_img_L.enabled = false;
		H_Bar_text_R.enabled = true;
		H_Bar_text_L.enabled = true;
	}
}
