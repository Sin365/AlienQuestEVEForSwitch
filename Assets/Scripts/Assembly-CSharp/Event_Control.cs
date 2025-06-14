using UnityEngine;

public class Event_Control : global::UnityEngine.MonoBehaviour
{
	private int Event_Num = 1;

	private int Ending_Num;

	private int Index;

	private int text_Num;

	private int[] event_nameNum = new int[16]
	{
		0, 0, 0, 0, 0, 0, 0, 1, 0, 1,
		0, 1, 0, 1, 0, 0
	};

	private string[,] Event_Dialogue = new string[16, 3]
	{
		{ "General", "しょうぐん" ,"将军" },
		{ "Science Officer", "研究員" ,"研究員" },
		{ "Oh, That's perfect!", "すごい！ それは完璧です！" ,"完美！如此完美！！！" },
		{ "MOTHER BRAIN! The clone of ancient alien!", "これがまさにマザーブレイン！ \n古代エイリアンの脳を複製こなすなんてすごい！" ,"这就是主脑！这就是主脑啊！ \n我们终于成功复制古代外星人的大脑了！" },
		{ "Until now, we made bioweapon from just a genetic \ninformation of alien gene.", "今まで私たちは、エイリアンの遺伝情報だけで生物兵器を作った。" ,"在这之前，我们只是像猴子一样，用外星人的遗传信息制作生物武器。" },
		{ "But from now on, we'll get the knowledge and \ninformation from this gigantic brain.", "しかし、これからはその巨大な脳に込められた古代の知識をすべて吸収することができる！" ,"但是，现在我们终于可以摆脱那种浅薄的运用！直接吸收这些大脑中蕴含的古代知识了！" },
		{ "It's time to wake up MOTHER BRAIN.", "今、マザーブレインを眠りから目覚めさせる時間である。" ,"今天！就是我们崛起的时刻。" },
		{ "Yes sir!", "かしこまりました。" ,"是！将军！" },
		{ "What's going on?", "おい、どうした？" ,"嗯？怎么了？" },
		{ "Sir! The MOTHER BRAIN is out of control!! She connect to the Core and try to dominate the system!!", "マザーブレインが暴走しています！ \nコアに接続して、システム全体を掌握していきます！" ,"主脑暴走了！参数异常！参数异常！ \n核心！核心脱离了！不好！整个系统都失去控制了！" },
		{ "W,What??  It's just a dead brain!!", "何？ そんなはずはない... これだけ死んだ脳に過ぎず、！" ,"什么？！怎、怎么可能！区区一个死去几万年的大脑！！" },
		{ "Oh no... We must cut the power of this ship immediately!!", "ああダメ！ \nすぐにマザーブレインを停止させなければします！" ,"啊啊、不行了！ \n必须快点关闭它！它要暴走了！" },
		{ "Bull shit!! This is a Military resource! We can't lose this \nbrain!!", "黙れ！ これ貴重な軍事資源である！ \nこのように放棄することはできない！" ,"开什么玩笑！这是宝贵的军事资源！ \n不能就这么放弃！" },
		{ "H, Help!!", "うわあああ！" ,"哇！哇啊啊啊！" },
		{ "W, What the..?", "..な, なに？" ,"..这、这是？" },
		{ "W, What the..?", "..な, なに？","..什么？" }

	};

	private float Event_Timer;

	private float Dlg_Timer;

	private bool Text_Dlg_On;

	private int T_index;

	private float Text_Timer;

	private float Word_Timer = 0.01f;

	private float Text_Start_Timer;

	private bool isTextEnd;

	private bool isSkip;

	private bool isSkipLock;

	private string contents = string.Empty;

	private float Arrow_Timer;

	private float Arrow_PosY;

	private bool onRedAlert;

	private float RedAlert_Timer;

	private float RedAlertGlow_Timer;

	public global::UnityEngine.SpriteRenderer RedAlert;

	private global::UnityEngine.Vector3 PosTarget;

	private global::UnityEngine.Vector3 PosHide;

	private float Room_0_Pos_X = 256.035f;

	public global::UnityEngine.UI.Image spr_Name_L;

	public global::UnityEngine.UI.Text text_Name_L;

	public global::UnityEngine.UI.Text Text_Contents;

	public global::UnityEngine.UI.Image Arrow;

	public global::UnityEngine.GameObject _openingShip;

	public global::UnityEngine.GameObject _room_MotherBrain;

	public global::UnityEngine.GameObject _room_Eve;

	public global::UnityEngine.GameObject _motherBrain;

	public global::UnityEngine.GameObject _gunshipBack;

	private global::UnityEngine.GameObject Opening_Ship;

	private global::UnityEngine.GameObject Room_MotherBrain;

	private global::UnityEngine.GameObject Room_Eve;

	private global::UnityEngine.GameObject MotherBrain;

	public global::UnityEngine.GameObject _info_Eve;

	public global::UnityEngine.GameObject _info_EveCore;

	public global::UnityEngine.GameObject _info_6months;

	public global::UnityEngine.GameObject _info_Mission;

	private bool isFadeOut;

	private float FadeOpacity;

	private global::UnityEngine.Vector3 CamPos;

	private global::UnityEngine.Vector3 CamPos_Target;

	private float CamSize = 9f;

	private float CamSize_Target = 8f;

	private int FadeInfo_Num;

	private global::UnityEngine.SpriteRenderer GateFade;

    GameManager GM => GameManager.instance;

	private Custom_Key CK => GameManager.instance.CK;
    Player_Control PC => GameManager.instance?.PC;
    GameObject Player => GameManager.instance?.gobj_Player;
    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//CK = global::UnityEngine.GameObject.Find("GameManager").GetComponent<Custom_Key>();
		GateFade = global::UnityEngine.GameObject.Find("GateFade").GetComponent<global::UnityEngine.SpriteRenderer>();
		Arrow_PosY = Arrow.GetComponent<global::UnityEngine.RectTransform>().localPosition.y;
		Reset_Text();
		Player.GetComponent<Player_Control>().Lock_Timer = 0.2f;
		Player.transform.position = new global::UnityEngine.Vector3(Room_0_Pos_X, -5.4f, 0f);
		PosTarget = new global::UnityEngine.Vector3(0f, -380f, 0f);
		PosHide = new global::UnityEngine.Vector3(0f, -900f, 0f);
		GetComponent<global::UnityEngine.RectTransform>().localPosition = PosHide;
	}

	private void Update()
	{
		if (onRedAlert)
		{
			if (GM.Paused)
			{
				if (GetComponent<global::UnityEngine.AudioSource>().isPlaying)
				{
					GetComponent<global::UnityEngine.AudioSource>().Stop();
				}
			}
			else
			{
				RedAlertGlow_Timer += global::UnityEngine.Time.deltaTime;
				if (onRedAlert && !GetComponent<global::UnityEngine.AudioSource>().isPlaying)
				{
					if (Event_Num > 0)
					{
						GetComponent<global::UnityEngine.AudioSource>().volume = AxiPlayerPrefs.GetFloat("SoundVolume") * 0.5f;
					}
					else
					{
						GetComponent<global::UnityEngine.AudioSource>().volume = AxiPlayerPrefs.GetFloat("SoundVolume");
					}
					GetComponent<global::UnityEngine.AudioSource>().Play();
				}
				RedAlert.color = new global::UnityEngine.Color(1f, 0f, 0f, 0.5f + (1f + global::UnityEngine.Mathf.Sin(RedAlertGlow_Timer * 7f)) * 0.25f);
				if (!GM.onGatePass && RedAlert.sortingLayerID != 17)
				{
					RedAlert.sortingLayerID = AxiSortingOrder.GetHashIDByUserID(17);
					RedAlert.sortingOrder = -10;
				}
				if (GM.onGameClear)
				{
					GetComponent<global::UnityEngine.AudioSource>().volume = (1f - global::UnityEngine.GameObject.Find("BlackFade").GetComponent<global::UnityEngine.SpriteRenderer>().color.a) * AxiPlayerPrefs.GetFloat("SoundVolume") * 0.5f;
				}
				else if (GM.GameOver)
				{
					GetComponent<global::UnityEngine.AudioSource>().volume = global::UnityEngine.Mathf.Lerp(GetComponent<global::UnityEngine.AudioSource>().volume, AxiPlayerPrefs.GetFloat("SoundVolume") * 0.2f, global::UnityEngine.Time.deltaTime);
				}
				else
				{
					GetComponent<global::UnityEngine.AudioSource>().volume = global::UnityEngine.Mathf.Lerp(GetComponent<global::UnityEngine.AudioSource>().volume, AxiPlayerPrefs.GetFloat("SoundVolume") * 0.5f, global::UnityEngine.Time.deltaTime);
				}
			}
		}
		else if (RedAlert.enabled)
		{
			if (RedAlert_Timer > 0f)
			{
				RedAlert_Timer -= global::UnityEngine.Time.deltaTime;
				RedAlertGlow_Timer += global::UnityEngine.Time.deltaTime;
				RedAlert.color = new global::UnityEngine.Color(1f, 0f, 0f, 0.5f + (1f + global::UnityEngine.Mathf.Sin(RedAlertGlow_Timer * 7f)) * 0.25f);
			}
			else
			{
				GetComponent<global::UnityEngine.AudioSource>().volume = global::UnityEngine.Mathf.Lerp(GetComponent<global::UnityEngine.AudioSource>().volume, 0f, global::UnityEngine.Time.deltaTime * 2f);
				RedAlert.color = global::UnityEngine.Color.Lerp(RedAlert.color, new global::UnityEngine.Color(1f, 0f, 0f, 0f), global::UnityEngine.Time.deltaTime * 2f);
				if (GetComponent<global::UnityEngine.AudioSource>().volume < 0.02f && RedAlert.color.a < 0.02f)
				{
					GetComponent<global::UnityEngine.AudioSource>().Stop();
					RedAlert.enabled = false;
				}
			}
		}
		if (isSkipLock && (global::UnityEngine.Input.GetKeyUp(CK.Jump) || global::UnityEngine.Input.GetButtonUp("Jump")))
		{
			isSkipLock = false;
		}
		if (GM.onEvent && GM.EventState > 100 && GM.EventState < 200)
		{
			if (Event_Timer > 0f)
			{
				Event_Timer -= global::UnityEngine.Time.deltaTime;
			}
			if (isFadeOut)
			{
				FadeOpacity += global::UnityEngine.Time.deltaTime * 1f;
				if (FadeOpacity >= 1f)
				{
					isFadeOut = false;
					FadeOpacity = 1f;
					if (Index < 14)
					{
						UnityEngine.Camera.main.GetComponent<Camera_Control>().Event_Cam_Pos(CamPos_Target, 0.1f);
					}
					else
					{
						UnityEngine.Camera.main.GetComponent<Camera_Control>().Event_Cam_Pos(CamPos_Target, 0.4f);
					}
					UnityEngine.Camera.main.transform.position = CamPos;
					UnityEngine.Camera.main.GetComponent<Camera_Control>().targetSize = CamSize_Target;
					UnityEngine.Camera.main.GetComponent<UnityEngine.Camera>().orthographicSize = CamSize;
					if (FadeInfo_Num > 0)
					{
						Make_Info(FadeInfo_Num);
					}
				}
				GateFade.color = new global::UnityEngine.Color(0f, 0f, 0f, FadeOpacity);
				if (!GateFade.enabled)
				{
					GateFade.enabled = true;
				}
			}
			else if (GateFade.enabled)
			{
				FadeOpacity -= global::UnityEngine.Time.deltaTime * 1f;
				if (FadeOpacity <= 0f)
				{
					FadeOpacity = 0f;
					GateFade.enabled = false;
				}
				GateFade.color = new global::UnityEngine.Color(0f, 0f, 0f, FadeOpacity);
			}
			if (!GateFade.enabled)
			{
				switch (Event_Num)
				{
					case 1:
						Event_1_Index();
						break;
				}
				if (Text_Dlg_On)
				{
					Dlg_Timer += global::UnityEngine.Time.deltaTime;
					GetComponent<global::UnityEngine.RectTransform>().localPosition = global::UnityEngine.Vector3.Lerp(GetComponent<global::UnityEngine.RectTransform>().localPosition, PosTarget, global::UnityEngine.Time.deltaTime * 8f);
					Text_Start_Timer += global::UnityEngine.Time.deltaTime;
					if (Text_Start_Timer > 0.5f)
					{
						if (!isSkipLock && (global::UnityEngine.Input.GetKey(CK.Jump) || global::UnityEngine.Input.GetButton("Jump")))
						{
							isSkip = true;
						}
						else
						{
							isSkip = false;
						}
						Print_Text();
						if (!isTextEnd && T_index >= contents.Length)
						{
							isTextEnd = true;
							Arrow.enabled = true;
						}
						if (isTextEnd)
						{
							Arrow_Timer += global::UnityEngine.Time.deltaTime;
							float y = Arrow_PosY + global::UnityEngine.Mathf.Sin(Arrow_Timer * 8f) * 2f;
							Arrow.GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(0f, y, 0f);
						}
					}
					if (isTextEnd && (global::UnityEngine.Input.GetKeyDown(CK.Jump) || global::UnityEngine.Input.GetButtonDown("Jump")))
					{
						isSkipLock = true;
						if (Event_Num == 1 && text_Num < 14)
						{
							text_Num++;
							if (text_Num == 7)
							{
								Text_Dlg_On = false;
							}
							else if (text_Num == 8)
							{
								Text_Dlg_On = false;
							}
							else if (text_Num == 12)
							{
								Text_Dlg_On = false;
							}
							else if (text_Num == 14)
							{
								Text_Dlg_On = false;
							}
							else
							{
								Reset_Text();
								contents = Event_Dialogue[text_Num + 1, AxiPlayerPrefs.GetInt("Language_Num")];
								text_Name_L.text = Event_Dialogue[event_nameNum[text_Num + 1], AxiPlayerPrefs.GetInt("Language_Num")];
							}
						}
					}
				}
				else
				{
					GetComponent<global::UnityEngine.RectTransform>().localPosition = global::UnityEngine.Vector3.Lerp(GetComponent<global::UnityEngine.RectTransform>().localPosition, PosHide, global::UnityEngine.Time.deltaTime * 8f);
				}
			}
			else
			{
				GetComponent<global::UnityEngine.RectTransform>().localPosition = global::UnityEngine.Vector3.Lerp(GetComponent<global::UnityEngine.RectTransform>().localPosition, PosHide, global::UnityEngine.Time.deltaTime * 8f);
			}
			if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.Escape) || global::UnityEngine.Input.GetButtonDown("Start"))
			{
				End_Event();
			}
		}
		else
		{
			GetComponent<global::UnityEngine.RectTransform>().localPosition = global::UnityEngine.Vector3.Lerp(GetComponent<global::UnityEngine.RectTransform>().localPosition, PosHide, global::UnityEngine.Time.deltaTime * 3f);
		}
	}

	private void MotherBrainArrived()
	{
		Event_Timer = 0f;
		Index = 3;
		UnityEngine.Camera.main.GetComponent<Camera_Control>().Event_Cam_Pos(new global::UnityEngine.Vector3(-121f, -101f, -10f), 0.35f);
		UnityEngine.Camera.main.GetComponent<Camera_Control>().targetSize = 8f;
	}

	private void Event_1_Index()
	{
		if (Index == 0)
		{
			Index++;
			Event_Timer = 5f;
			Make_Info(1);
			global::UnityEngine.GameObject.Find("BGM_List").SendMessage("Play_Intro");
		}
		else if (Index == 1)
		{
			if (Event_Timer <= 0f)
			{
				Index++;
				Event_Timer = 5f;
				isFadeOut = true;
				CamPos = new global::UnityEngine.Vector3(-105.5f, -106f, -10f);
				CamPos_Target = new global::UnityEngine.Vector3(-105.5f, -100f, -10f);
				CamSize = 8f;
				CamSize_Target = 12f;
				if (global::UnityEngine.GameObject.Find("EVE_Core") != null)
				{
					global::UnityEngine.GameObject.Find("EVE_Core").GetComponent<Event_Core>().Set_State(1);
				}
				if (global::UnityEngine.GameObject.Find("Computer_1") != null)
				{
					global::UnityEngine.GameObject.Find("Computer_1").GetComponent<Event_Computer>().Set_State(1);
				}
				if (global::UnityEngine.GameObject.Find("Computer_2") != null)
				{
					global::UnityEngine.GameObject.Find("Computer_2").GetComponent<Event_Computer>().Set_State(1);
				}
				if (global::UnityEngine.GameObject.Find("Computer_3") != null)
				{
					global::UnityEngine.GameObject.Find("Computer_3").GetComponent<Event_Computer>().Set_State(1);
				}
			}
		}
		else if (Index == 2)
		{
			if (MotherBrain.GetComponent<AI_MotherBrain>().Event_Num < 1)
			{
				MotherBrain.GetComponent<AI_MotherBrain>().Event_Num = 1;
			}
		}
		else if (Index == 3)
		{
			if (Event_Timer <= 0f)
			{
				if (!Text_Dlg_On && text_Num == 0)
				{
					Text_Dlg_On = true;
					text_Num = 1;
					contents = Event_Dialogue[text_Num + 1, AxiPlayerPrefs.GetInt("Language_Num")];
					text_Name_L.text = Event_Dialogue[0, AxiPlayerPrefs.GetInt("Language_Num")];
				}
				else if (!Text_Dlg_On && text_Num > 0)
				{
					GameManager.instance.sc_Sound_List.Magic_2(MotherBrain.transform.position);
					MotherBrain.GetComponent<AI_MotherBrain>().Event_Num = 2;
					Index++;
					Event_Timer = 9f;
					UnityEngine.Camera.main.GetComponent<Camera_Control>().Event_Cam_Pos(new global::UnityEngine.Vector3(-107.5f, -102f, -10f), 1f);
					UnityEngine.Camera.main.GetComponent<Camera_Control>().targetSize = 10f;
				}
			}
		}
		else if (Index == 4)
		{
			if (Event_Timer <= 0f)
			{
				Index++;
				Event_Timer = 6f;
				isFadeOut = true;
				CamPos = new global::UnityEngine.Vector3(-100f, -192f, -10f);
				CamPos_Target = new global::UnityEngine.Vector3(-100f, -195f, -10f);
				CamSize = 8f;
				CamSize_Target = 12f;
				FadeInfo_Num = 2;
			}
		}
		else if (Index == 5)
		{
			if (!Room_Eve.GetComponent<global::UnityEngine.AudioSource>().isPlaying)
			{
				Room_Eve.GetComponent<global::UnityEngine.AudioSource>().Play();
			}
			Room_Eve.GetComponent<global::UnityEngine.AudioSource>().volume = global::UnityEngine.Mathf.Lerp(Room_Eve.GetComponent<global::UnityEngine.AudioSource>().volume, 1f, global::UnityEngine.Time.deltaTime * 4f);
			if (Event_Timer <= 0f)
			{
				Index++;
				Event_Timer = 4f;
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
		else if (Index == 6)
		{
			if (Event_Timer <= 0f)
			{
				Index++;
				Event_Timer = 3f;
				isFadeOut = true;
				CamPos = new global::UnityEngine.Vector3(-105f, -102f, -10f);
				CamPos_Target = new global::UnityEngine.Vector3(-120f, -102f, -10f);
				CamSize = 8f;
				CamSize_Target = 8f;
				MotherBrain.GetComponent<AI_MotherBrain>().Event_Num = 3;
				if (global::UnityEngine.GameObject.Find("Event_General") != null)
				{
					global::UnityEngine.GameObject.Find("Event_General").SendMessage("Set_Shocked");
				}
			}
			if (Event_Timer < 1.5f && !onRedAlert)
			{
				ON_RedAlert();
			}
		}
		else if (Index == 7)
		{
			Room_Eve.GetComponent<global::UnityEngine.AudioSource>().volume = global::UnityEngine.Mathf.Lerp(Room_Eve.GetComponent<global::UnityEngine.AudioSource>().volume, 0f, global::UnityEngine.Time.deltaTime * 3f);
			if (Event_Timer <= 0f)
			{
				UnityEngine.Camera.main.GetComponent<Camera_Control>().Event_Cam_Pos(new global::UnityEngine.Vector3(-121f, -102f, -10f), 0.5f);
				if (!Text_Dlg_On && text_Num == 7)
				{
					Index++;
					Event_Timer = 2f;
					Reset_Text();
					Text_Dlg_On = true;
					contents = Event_Dialogue[text_Num + 1, AxiPlayerPrefs.GetInt("Language_Num")];
					text_Name_L.text = Event_Dialogue[event_nameNum[text_Num + 1], AxiPlayerPrefs.GetInt("Language_Num")];
				}
			}
		}
		else if (Index == 8)
		{
			if (!(Event_Timer <= 0f))
			{
				return;
			}
			if (!Text_Dlg_On && text_Num == 12)
			{
				Index++;
				Event_Timer = 3f;
				if (text_Num == 12 && MotherBrain.GetComponent<AI_MotherBrain>().Event_Num < 4)
				{
					MotherBrain.GetComponent<AI_MotherBrain>().Event_Num = 4;
				}
				if (global::UnityEngine.GameObject.Find("Event_Scientist_1") != null)
				{
					global::UnityEngine.GameObject.Find("Event_Scientist_1").GetComponent<Event_Scientist>().Set_Front(1);
				}
			}
			else if (!Text_Dlg_On && text_Num == 8)
			{
				Reset_Text();
				Text_Dlg_On = true;
				contents = Event_Dialogue[text_Num + 1, AxiPlayerPrefs.GetInt("Language_Num")];
				text_Name_L.text = Event_Dialogue[event_nameNum[text_Num + 1], AxiPlayerPrefs.GetInt("Language_Num")];
			}
			if (text_Num == 9 || text_Num == 11)
			{
				Cam_Focus_1();
			}
			else if (text_Num == 8 || text_Num == 10)
			{
				Cam_Focus_2();
				if (global::UnityEngine.GameObject.Find("Event_Scientist_1") != null)
				{
					global::UnityEngine.GameObject.Find("Event_Scientist_1").GetComponent<Event_Scientist>().Set_Front(-1);
				}
			}
		}
		else if (Index == 9)
		{
			if (Event_Timer <= 0f)
			{
				if (!Text_Dlg_On && text_Num == 14)
				{
					Index++;
					Event_Timer = 7f;
					if (MotherBrain.GetComponent<AI_MotherBrain>().Event_Num < 5)
					{
						MotherBrain.GetComponent<AI_MotherBrain>().Event_Num = 5;
					}
				}
				else if (!Text_Dlg_On && text_Num == 12)
				{
					Reset_Text();
					Text_Dlg_On = true;
					contents = Event_Dialogue[text_Num + 1, AxiPlayerPrefs.GetInt("Language_Num")];
					text_Name_L.text = Event_Dialogue[event_nameNum[text_Num + 1], AxiPlayerPrefs.GetInt("Language_Num")];
					if (global::UnityEngine.GameObject.Find("Event_Scientist_2") != null)
					{
						global::UnityEngine.GameObject.Find("Event_Scientist_2").GetComponent<Event_Scientist>().Set_Front(1);
					}
					if (global::UnityEngine.GameObject.Find("Event_Scientist_3") != null)
					{
						global::UnityEngine.GameObject.Find("Event_Scientist_3").GetComponent<Event_Scientist>().Set_Front(1);
					}
				}
			}
			UnityEngine.Camera.main.GetComponent<Camera_Control>().Event_Cam_Pos(new global::UnityEngine.Vector3(-114.5f, -102f, -10f), 2f);
			UnityEngine.Camera.main.GetComponent<Camera_Control>().targetSize = 11f;
		}
		else if (Index == 10)
		{
			if (Event_Timer <= 0f)
			{
				Index++;
				Event_Timer = 5f;
				if (MotherBrain.GetComponent<AI_MotherBrain>().Event_Num < 6)
				{
					MotherBrain.GetComponent<AI_MotherBrain>().Event_Num = 6;
				}
				UnityEngine.Camera.main.GetComponent<Camera_Control>().Event_Cam_Pos(new global::UnityEngine.Vector3(-106f, -100f, -10f), 0.2f);
				UnityEngine.Camera.main.GetComponent<Camera_Control>().targetSize = 8f;
			}
		}
		else if (Index == 11)
		{
			if (Event_Timer <= 0f)
			{
				Index++;
				Event_Timer = 2f;
				if (MotherBrain.GetComponent<AI_MotherBrain>().Event_Num < 7)
				{
					MotherBrain.GetComponent<AI_MotherBrain>().Event_Num = 7;
				}
			}
		}
		else if (Index == 12)
		{
			if (Event_Timer <= 0f)
			{
				Index++;
				Event_Timer = 5f;
				isFadeOut = true;
				CamPos = new global::UnityEngine.Vector3(-200f, -101f, -10f);
				CamPos_Target = new global::UnityEngine.Vector3(-200f, -98f, -10f);
				CamSize = 8f;
				CamSize_Target = 7f;
				if (onRedAlert)
				{
					OFF_RedAlert();
				}
				FadeInfo_Num = 3;
				Opening_Ship.SendMessage("Ship_Off");
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_gunshipBack, Opening_Ship.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
			}
		}
		else if (Index == 13)
		{
			if (Event_Timer <= 0f)
			{
				Index++;
				Event_Timer = 0f;
				isFadeOut = true;
				CamPos = new global::UnityEngine.Vector3(Room_0_Pos_X, 2f, -10f);
				CamPos_Target = new global::UnityEngine.Vector3(Room_0_Pos_X, 2f, -10f);
				CamSize = 4f;
				CamSize_Target = 9f;
				global::UnityEngine.GameObject.Find("Bay_Stand_Ship").SendMessage("ReadyTo_Landing");
			}
		}
		else if (Index != 14)
		{
		}
	}

	private void Cam_Focus_1()
	{
		UnityEngine.Camera.main.GetComponent<Camera_Control>().Event_Cam_Pos(new global::UnityEngine.Vector3(-121f, -101f, -10f), 0.5f);
		UnityEngine.Camera.main.GetComponent<Camera_Control>().targetSize = 8f;
	}

	private void Cam_Focus_2()
	{
		UnityEngine.Camera.main.GetComponent<Camera_Control>().Event_Cam_Pos(new global::UnityEngine.Vector3(-118f, -106f, -10f), 0.5f);
		UnityEngine.Camera.main.GetComponent<Camera_Control>().targetSize = 8f;
	}

	private void Cam_Focus_All()
	{
		UnityEngine.Camera.main.GetComponent<Camera_Control>().Event_Cam_Pos(new global::UnityEngine.Vector3(-113.5f, -101f, -10f), 0.5f);
		UnityEngine.Camera.main.GetComponent<Camera_Control>().targetSize = 12f;
	}

	private void Make_Info(int num)
	{
		switch (num)
		{
			case 1:
				{
					global::UnityEngine.GameObject gameObject4 = global::UnityEngine.Object.Instantiate(_info_Eve, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
					gameObject4.GetComponent<global::UnityEngine.RectTransform>().parent = global::UnityEngine.GameObject.Find("UI Canvas").GetComponent<global::UnityEngine.RectTransform>();
					gameObject4.GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(626f, -105f, 0f);
					gameObject4.GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(1f, 1f, 1f);
					break;
				}
			case 2:
				{
					global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(_info_EveCore, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
					gameObject3.GetComponent<global::UnityEngine.RectTransform>().parent = global::UnityEngine.GameObject.Find("UI Canvas").GetComponent<global::UnityEngine.RectTransform>();
					gameObject3.GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(626f, -105f, 0f);
					gameObject3.GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(1f, 1f, 1f);
					break;
				}
			case 3:
				{
					global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(_info_6months, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
					gameObject2.GetComponent<global::UnityEngine.RectTransform>().parent = global::UnityEngine.GameObject.Find("UI Canvas").GetComponent<global::UnityEngine.RectTransform>();
					gameObject2.GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(626f, -200f, 0f);
					gameObject2.GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(1f, 1f, 1f);
					break;
				}
			case 4:
				{
					global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_info_Mission, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
					gameObject.GetComponent<global::UnityEngine.RectTransform>().parent = global::UnityEngine.GameObject.Find("UI Canvas").GetComponent<global::UnityEngine.RectTransform>();
					gameObject.GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(1500f, -360f, 0f);
					gameObject.GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(1f, 1f, 1f);
					break;
				}
		}
		FadeInfo_Num = 0;
	}

	public void Start_OpeningEvent()
	{
		GM.onEvent = true;
		GM.EventState = 101;
		Event_Num = 1;
		Event_Timer = 0f;
		UnityEngine.Camera.main.GetComponent<Camera_Control>().Event_Cam_Pos(new global::UnityEngine.Vector3(-200f, -98f, -10f), 0.1f);
		UnityEngine.Camera.main.transform.position = new global::UnityEngine.Vector3(-200f, -101f, -10f);
		UnityEngine.Camera.main.GetComponent<Camera_Control>().targetSize = CamSize_Target;
		UnityEngine.Camera.main.GetComponent<UnityEngine.Camera>().orthographicSize = CamSize;
		Opening_Ship = global::UnityEngine.Object.Instantiate(_openingShip, new global::UnityEngine.Vector3(-200f, -100f, 0f), base.transform.rotation) as global::UnityEngine.GameObject;
		Opening_Ship.transform.localScale = new global::UnityEngine.Vector3(1.2f, 1.2f, 1f);
		Room_MotherBrain = global::UnityEngine.Object.Instantiate(_room_MotherBrain, new global::UnityEngine.Vector3(-100f, -100f, 0f), base.transform.rotation) as global::UnityEngine.GameObject;
		Room_Eve = global::UnityEngine.Object.Instantiate(_room_Eve, new global::UnityEngine.Vector3(-100f, -200f, 0f), base.transform.rotation) as global::UnityEngine.GameObject;
		MotherBrain = global::UnityEngine.Object.Instantiate(_motherBrain, new global::UnityEngine.Vector3(-100f, -106.63f, 0f), base.transform.rotation) as global::UnityEngine.GameObject;
		MotherBrain.GetComponent<AI_MotherBrain>().onEvent = true;
		MotherBrain.GetComponent<Monster>().onEvent = true;
		global::UnityEngine.GameObject.Find("Text_TimePlay").GetComponent<global::UnityEngine.UI.Text>().enabled = false;
		global::UnityEngine.GameObject.Find("Clock_TimePlay").GetComponent<global::UnityEngine.SpriteRenderer>().enabled = false;
	}

	private void End_Event()
	{
		GM.Set_Event(0);
		GM.Event_Resume();
		if (GM.EventState < 200)
		{
			GM.EventState = 10;
		}
		Event_Num = 0;
		Ending_Num = 0;
		Event_Timer = 0f;
		Index = 0;
		text_Num = 0;
		Text_Dlg_On = false;
		Text_Start_Timer = 0f;
		Reset_Text();
		Player.GetComponent<Player_Control>().Lock_Timer = 0.2f;
		Player.transform.position = new global::UnityEngine.Vector3(Room_0_Pos_X, -5.4f, 0f);
		if (Opening_Ship != null)
		{
			global::UnityEngine.Object.Destroy(Opening_Ship.gameObject);
		}
		if (Room_MotherBrain != null)
		{
			global::UnityEngine.Object.Destroy(Room_MotherBrain.gameObject);
		}
		if (Room_Eve != null)
		{
			global::UnityEngine.Object.Destroy(Room_Eve.gameObject);
		}
		if (MotherBrain != null)
		{
			MotherBrain.SendMessage("MotherBrain_Del");
		}
		if (onRedAlert)
		{
			OFF_RedAlert();
		}
		if (GateFade.enabled)
		{
			GateFade.color = new global::UnityEngine.Color(0f, 0f, 0f, 0f);
			GateFade.enabled = false;
			isFadeOut = false;
		}
		UnityEngine.Camera.main.GetComponent<Camera_Control>().Event_Cam_Pos(new global::UnityEngine.Vector3(Room_0_Pos_X, 0.36f, -10f), 0.1f);
		UnityEngine.Camera.main.transform.position = new global::UnityEngine.Vector3(Room_0_Pos_X, 3f, -10f);
		UnityEngine.Camera.main.GetComponent<Camera_Control>().targetSize = 8f;
		UnityEngine.Camera.main.GetComponent<UnityEngine.Camera>().orthographicSize = 9f;
		if (AxiPlayerPrefs.GetInt("onClockFps") == 1)
		{
			global::UnityEngine.GameObject.Find("Text_TimePlay").GetComponent<global::UnityEngine.UI.Text>().enabled = true;
			global::UnityEngine.GameObject.Find("Text_Fps").GetComponent<global::UnityEngine.UI.Text>().enabled = true;
			global::UnityEngine.GameObject.Find("Clock_TimePlay").GetComponent<global::UnityEngine.SpriteRenderer>().enabled = true;
		}
		global::UnityEngine.GameObject.Find("Bay_Stand_Ship").SendMessage("GameStart_RightNow");
		Make_Info(4);
	}

	public void Complete_Event()
	{
		GM.Set_Event(0);
		GM.Event_Resume();
		if (GM.EventState < 200)
		{
			GM.EventState = 10;
		}
		Event_Num = 0;
		Ending_Num = 0;
		Event_Timer = 0f;
		Index = 0;
		text_Num = 0;
		Text_Dlg_On = false;
		Text_Start_Timer = 0f;
		Reset_Text();
		if (Opening_Ship != null)
		{
			global::UnityEngine.Object.Destroy(Opening_Ship.gameObject);
		}
		if (Room_MotherBrain != null)
		{
			global::UnityEngine.Object.Destroy(Room_MotherBrain.gameObject);
		}
		if (Room_Eve != null)
		{
			global::UnityEngine.Object.Destroy(Room_Eve.gameObject);
		}
		if (MotherBrain != null)
		{
			MotherBrain.SendMessage("MotherBrain_Del");
		}
		if (onRedAlert)
		{
			OFF_RedAlert();
		}
		if (GateFade.enabled)
		{
			GateFade.color = new global::UnityEngine.Color(0f, 0f, 0f, 0f);
			GateFade.enabled = false;
			isFadeOut = false;
		}
		UnityEngine.Camera.main.GetComponent<Camera_Control>().targetSize = 8f;
		if (AxiPlayerPrefs.GetInt("onClockFps") == 1)
		{
			global::UnityEngine.GameObject.Find("Text_TimePlay").GetComponent<global::UnityEngine.UI.Text>().enabled = true;
			global::UnityEngine.GameObject.Find("Text_Fps").GetComponent<global::UnityEngine.UI.Text>().enabled = true;
			global::UnityEngine.GameObject.Find("Clock_TimePlay").GetComponent<global::UnityEngine.SpriteRenderer>().enabled = true;
		}
		Make_Info(4);
	}

	private void OnOff_RedAlert()
	{
		if (onRedAlert)
		{
			OFF_RedAlert();
		}
		else
		{
			ON_RedAlert();
		}
	}

	private void ON_RedAlert()
	{
		onRedAlert = true;
		RedAlert_Timer = 0f;
		RedAlertGlow_Timer = 0f;
		if (Event_Num > 0)
		{
			GetComponent<global::UnityEngine.AudioSource>().volume = AxiPlayerPrefs.GetFloat("SoundVolume") * 0.5f;
		}
		else
		{
			GetComponent<global::UnityEngine.AudioSource>().volume = AxiPlayerPrefs.GetFloat("SoundVolume");
		}
		GetComponent<global::UnityEngine.AudioSource>().Play();
		RedAlert.color = new global::UnityEngine.Color(1f, 0f, 0f, 0f);
		RedAlert.enabled = true;
	}

	private void OFF_RedAlert()
	{
		onRedAlert = false;
		RedAlert_Timer = 0f;
		RedAlertGlow_Timer = 0f;
	}

	private void Event_Boss4_Death()
	{
		onRedAlert = false;
		RedAlert_Timer = 7f;
	}

	private void Print_Text()
	{
		if (T_index < contents.Length && Text_Timer > Word_Timer)
		{
			Text_Timer = 0f;
			if (isSkip)
			{
				int num = ((T_index + 10 >= contents.Length) ? contents.Length : (T_index + 10));
				for (int i = T_index; i < num; i++)
				{
					Text_Contents.text += contents[i];
					T_index++;
				}
			}
			else
			{
				Text_Contents.text += contents[T_index];
				T_index++;
			}
		}
		else
		{
			Text_Timer += global::UnityEngine.Time.deltaTime;
		}
	}

	private void Reset_Text()
	{
		T_index = 0;
		Text_Timer = 0f;
		Word_Timer = 0.01f;
		isTextEnd = false;
		Arrow.enabled = false;
		Dlg_Timer = 0f;
		Text_Contents.text = string.Empty;
		contents = string.Empty;
	}
}
