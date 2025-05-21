public class Menu_GameOver : global::UnityEngine.MonoBehaviour
{
	private bool Enabled;

	private bool onExit;

	private float GameOver_Timer;

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

	private float SelCursor_Timer;

	private float SelCursor_Size = 1f;

	private global::UnityEngine.Vector3 MousePos;

	private global::UnityEngine.Vector3 MousePosPrev;

	private float MouseDist;

	private int Sound_MoveFalse;

	private float DelaySound_Timer;

	private float SoundVolume_Timer;

	private global::UnityEngine.Color color_ON = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_OFF = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	private global::UnityEngine.Color color_CumBox_ON = new global::UnityEngine.Color(1f, 0.1f, 0f, 1f);

	private global::UnityEngine.Color color_CumBox_OFF = new global::UnityEngine.Color(1f, 0.1f, 0f, 0f);

	private global::UnityEngine.Color color_CumText_ON = new global::UnityEngine.Color(1f, 1f, 0.67f, 1f);

	private global::UnityEngine.Color color_CumText_OFF = new global::UnityEngine.Color(1f, 1f, 0.67f, 0f);

	private global::UnityEngine.Color color_Glow_ON = new global::UnityEngine.Color(0.35f, 0f, 1f, 0.25f);

	private global::UnityEngine.Color color_Glow_OFF = new global::UnityEngine.Color(0.35f, 0f, 1f, 0f);

	public global::UnityEngine.GameObject H_Object;

	private bool on_CumBox_Info = true;

	private float H_Timer;

	private float Cum_Timer;

	private bool onLTrigger;

	private GameManager GM;

	private Custom_Key CK;

	private UI_SoundList Sound_List;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		CK = global::UnityEngine.GameObject.Find("GameManager").GetComponent<Custom_Key>();
		Sound_List = global::UnityEngine.GameObject.Find("Menu").GetComponent<UI_SoundList>();
		GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(0f, -1000f, 0f);
	}

	private void Update()
	{
		if (GM.GameOver)
		{
			if (!Enabled)
			{
				On_GameOver();
			}
			GameOver_Timer += global::UnityEngine.Time.deltaTime;
			if (DelaySound_Timer > 0f)
			{
				DelaySound_Timer -= global::UnityEngine.Time.deltaTime;
			}
			if (SoundVolume_Timer > 0f)
			{
				SoundVolume_Timer -= global::UnityEngine.Time.deltaTime;
			}
			if (Cum_Timer > 0f)
			{
				Cum_Timer -= global::UnityEngine.Time.deltaTime;
			}
			if (GameOver_Timer < 1.5f)
			{
				return;
			}
			GetComponent<global::UnityEngine.RectTransform>().localPosition = global::UnityEngine.Vector3.Lerp(GetComponent<global::UnityEngine.RectTransform>().localPosition, new global::UnityEngine.Vector3(0f, -500f, 0f), global::UnityEngine.Time.deltaTime * 5f);
			global::UnityEngine.GameObject.Find("GameOver_Select_BG").GetComponent<global::UnityEngine.UI.Image>().color = global::UnityEngine.Color.Lerp(global::UnityEngine.GameObject.Find("GameOver_Select_BG").GetComponent<global::UnityEngine.UI.Image>().color, color_ON, global::UnityEngine.Time.deltaTime * 3f);
			inputX = 0f;
			if (global::UnityEngine.Input.GetKeyDown(CK.Right))
			{
				inputX = 1f;
			}
			else if (global::UnityEngine.Input.GetKeyDown(CK.Left))
			{
				inputX = -1f;
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
			MousePos = global::UnityEngine.Input.mousePosition;
			MouseDist = global::UnityEngine.Vector3.Distance(MousePos, MousePosPrev);
			if (MouseDist > 0f || global::UnityEngine.Input.GetMouseButtonDown(0) || global::UnityEngine.Input.GetMouseButtonDown(1) || global::UnityEngine.Input.GetMouseButton(0))
			{
				Check_Mouse();
			}
			if (inputX > 0f && Sel_Index < 2)
			{
				Sel_Index++;
			}
			else if (inputX < 0f && Sel_Index > 0)
			{
				Sel_Index--;
			}
			if (!onExit && Prev_Index != Sel_Index)
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
				PosTarget = global::UnityEngine.GameObject.Find("Pos_GameOver_" + Sel_Index).GetComponent<global::UnityEngine.RectTransform>().localPosition;
				global::UnityEngine.GameObject.Find("GameOver_Select_BG").GetComponent<global::UnityEngine.RectTransform>().localPosition = PosTarget;
				global::UnityEngine.GameObject.Find("GameOver_Select_BG").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
			}
			global::UnityEngine.GameObject.Find("GameOver_Select_Cursor").GetComponent<global::UnityEngine.RectTransform>().localPosition = global::UnityEngine.Vector3.Lerp(global::UnityEngine.GameObject.Find("GameOver_Select_Cursor").GetComponent<global::UnityEngine.RectTransform>().localPosition, PosTarget, global::UnityEngine.Time.deltaTime * 16f);
			SelCursor_Timer += global::UnityEngine.Time.deltaTime;
			SelCursor_Size = 1f + (1f + global::UnityEngine.Mathf.Sin(SelCursor_Timer * 10f)) * 0.015f;
			global::UnityEngine.GameObject.Find("GameOver_Select_Cursor").GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(SelCursor_Size, SelCursor_Size, 1f);
			if (H_Object != null)
			{
				H_Timer += global::UnityEngine.Time.deltaTime;
				if (H_Timer > 1.5f)
				{
					global::UnityEngine.GameObject.Find("Cum_Icon").GetComponent<global::UnityEngine.UI.Image>().color = global::UnityEngine.Color.Lerp(global::UnityEngine.GameObject.Find("Cum_Icon").GetComponent<global::UnityEngine.UI.Image>().color, color_CumBox_ON, global::UnityEngine.Time.deltaTime * 5f);
					global::UnityEngine.GameObject.Find("Cum_Icon_tail").GetComponent<global::UnityEngine.UI.Image>().color = global::UnityEngine.GameObject.Find("Cum_Icon").GetComponent<global::UnityEngine.UI.Image>().color;
					global::UnityEngine.GameObject.Find("Cum_Text").GetComponent<global::UnityEngine.UI.Text>().color = global::UnityEngine.Color.Lerp(global::UnityEngine.GameObject.Find("Cum_Text").GetComponent<global::UnityEngine.UI.Text>().color, color_CumText_ON, global::UnityEngine.Time.deltaTime * 5f);
					if (H_Timer > 8f && on_CumBox_Info)
					{
						on_CumBox_Info = false;
					}
					if (global::UnityEngine.Input.GetKeyDown(CK.Spin) && Cum_Timer <= 0f)
					{
						CumShot();
					}
					else if (global::UnityEngine.Input.GetAxis("L_Trigger") > 0.3f && Cum_Timer <= 0f)
					{
						CumShot();
					}
				}
			}
			else
			{
				if (H_Timer > 0f && on_CumBox_Info)
				{
					on_CumBox_Info = false;
				}
				H_Timer = 0f;
				global::UnityEngine.GameObject.Find("Cum_Icon").GetComponent<global::UnityEngine.UI.Image>().color = global::UnityEngine.Color.Lerp(global::UnityEngine.GameObject.Find("Cum_Icon").GetComponent<global::UnityEngine.UI.Image>().color, color_CumBox_OFF, global::UnityEngine.Time.deltaTime * 5f);
				global::UnityEngine.GameObject.Find("Cum_Icon_tail").GetComponent<global::UnityEngine.UI.Image>().color = global::UnityEngine.GameObject.Find("Cum_Icon").GetComponent<global::UnityEngine.UI.Image>().color;
				global::UnityEngine.GameObject.Find("Cum_Text").GetComponent<global::UnityEngine.UI.Text>().color = global::UnityEngine.Color.Lerp(global::UnityEngine.GameObject.Find("Cum_Text").GetComponent<global::UnityEngine.UI.Text>().color, color_CumText_OFF, global::UnityEngine.Time.deltaTime * 5f);
			}
			global::UnityEngine.GameObject.Find("Cum_Icon").GetComponent<global::UnityEngine.RectTransform>().localScale = global::UnityEngine.Vector3.Lerp(global::UnityEngine.GameObject.Find("Cum_Icon").GetComponent<global::UnityEngine.RectTransform>().localScale, new global::UnityEngine.Vector3(1f, 1f, 1f), global::UnityEngine.Time.deltaTime * 5f);
			global::UnityEngine.GameObject.Find("Cum_Icon").GetComponent<global::UnityEngine.RectTransform>().localPosition = global::UnityEngine.Vector3.Lerp(global::UnityEngine.GameObject.Find("Cum_Icon").GetComponent<global::UnityEngine.RectTransform>().localPosition, new global::UnityEngine.Vector3(460f, 0f, 1f), global::UnityEngine.Time.deltaTime * 5f);
			if (H_Timer > 1.5f && on_CumBox_Info)
			{
				global::UnityEngine.GameObject.Find("Info_Glow_Cum").GetComponent<global::UnityEngine.UI.Image>().color = global::UnityEngine.Color.Lerp(global::UnityEngine.GameObject.Find("Info_Glow_Cum").GetComponent<global::UnityEngine.UI.Image>().color, color_Glow_ON, global::UnityEngine.Time.deltaTime * 5f);
				global::UnityEngine.GameObject.Find("Info_Text_Cum").GetComponent<global::UnityEngine.UI.Text>().color = global::UnityEngine.Color.Lerp(global::UnityEngine.GameObject.Find("Info_Text_Cum").GetComponent<global::UnityEngine.UI.Text>().color, color_ON, global::UnityEngine.Time.deltaTime * 5f);
			}
			else
			{
				global::UnityEngine.GameObject.Find("Info_Glow_Cum").GetComponent<global::UnityEngine.UI.Image>().color = global::UnityEngine.Color.Lerp(global::UnityEngine.GameObject.Find("Info_Glow_Cum").GetComponent<global::UnityEngine.UI.Image>().color, color_Glow_OFF, global::UnityEngine.Time.deltaTime * 5f);
				global::UnityEngine.GameObject.Find("Info_Text_Cum").GetComponent<global::UnityEngine.UI.Text>().color = global::UnityEngine.Color.Lerp(global::UnityEngine.GameObject.Find("Info_Text_Cum").GetComponent<global::UnityEngine.UI.Text>().color, color_OFF, global::UnityEngine.Time.deltaTime * 5f);
			}
			if (global::UnityEngine.Input.GetKeyDown(CK.Jump))
			{
				global::UnityEngine.PlayerPrefs.SetInt("Input_Mode", 0);
				if (Sel_Index == 0)
				{
					GM.Resurrect();
				}
				else if (Sel_Index == 1)
				{
					Restart();
				}
				else if (Sel_Index == 2)
				{
					Exit();
				}
			}
			else if (global::UnityEngine.Input.GetButtonDown("Jump"))
			{
				global::UnityEngine.PlayerPrefs.SetInt("Input_Mode", 1);
				if (Sel_Index == 0)
				{
					GM.Resurrect();
				}
				else if (Sel_Index == 1)
				{
					Restart();
				}
				else if (Sel_Index == 2)
				{
					Exit();
				}
			}
			Prev_Index = Sel_Index;
			MousePosPrev = MousePos;
		}
		else if (Enabled)
		{
			Off_GameOver();
		}
	}

	private void On_GameOver()
	{
		Enabled = true;
		GameOver_Timer = 0f;
		Sel_Index = 1;
		Prev_Index = 1;
		PosTarget = global::UnityEngine.GameObject.Find("Pos_GameOver_1").GetComponent<global::UnityEngine.RectTransform>().localPosition;
		global::UnityEngine.GameObject.Find("GameOver_Select_BG").GetComponent<global::UnityEngine.RectTransform>().localPosition = PosTarget;
		global::UnityEngine.GameObject.Find("Info_Glow_Cum").GetComponent<global::UnityEngine.UI.Image>().color = color_Glow_OFF;
		global::UnityEngine.GameObject.Find("Info_Text_Cum").GetComponent<global::UnityEngine.UI.Text>().color = color_OFF;
		global::UnityEngine.GameObject.Find("Cum_Icon").GetComponent<global::UnityEngine.UI.Image>().color = color_CumBox_OFF;
		global::UnityEngine.GameObject.Find("Cum_Icon_tail").GetComponent<global::UnityEngine.UI.Image>().color = color_CumBox_OFF;
		global::UnityEngine.GameObject.Find("Cum_Text").GetComponent<global::UnityEngine.UI.Text>().color = color_CumText_OFF;
		GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(0f, -1000f, 0f);
	}

	private void Off_GameOver()
	{
		Enabled = false;
		GameOver_Timer = 0f;
		Sound_List.SendMessage("Sound_MenuOff");
		GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(0f, -1000f, 0f);
	}

	private void Restart()
	{
		onExit = true;
		Sound_List.SendMessage("Sound_DeviceOn");
		Sound_List.SendMessage("Sound_Btn");
		GM.Set_FadeOut("Main");
	}

	private void Exit()
	{
		onExit = true;
		Sound_List.SendMessage("Sound_DeviceOn");
		Sound_List.SendMessage("Sound_Btn");
		if (!GM.onChestBurster_Over)
		{
			GM.Set_FadeOut("GameOver");
		}
		else
		{
			GM.Set_FadeOut("Title");
		}
		if (GM.Get_Event(14))
		{
			global::UnityEngine.PlayerPrefs.SetInt("H_GameOver_Now", 5);
		}
		else if (GM.Get_Event(13))
		{
			global::UnityEngine.PlayerPrefs.SetInt("H_GameOver_Now", 4);
		}
		else if (GM.Get_Event(12))
		{
			global::UnityEngine.PlayerPrefs.SetInt("H_GameOver_Now", 3);
		}
		else if (GM.Get_Event(11))
		{
			global::UnityEngine.PlayerPrefs.SetInt("H_GameOver_Now", 2);
		}
		else
		{
			global::UnityEngine.PlayerPrefs.SetInt("H_GameOver_Now", 1);
		}
	}

	private void Check_Mouse()
	{
		if (global::UnityEngine.Input.GetMouseButtonDown(0))
		{
			global::UnityEngine.Ray ray = global::UnityEngine.GameObject.Find("UI Camera").camera.ScreenPointToRay(global::UnityEngine.Input.mousePosition);
			global::UnityEngine.RaycastHit2D rayIntersection = global::UnityEngine.Physics2D.GetRayIntersection(ray, float.PositiveInfinity);
			if (rayIntersection.collider != null)
			{
				if (rayIntersection.collider.name == "Pos_GameOver_0")
				{
					Sel_Index = 0;
					global::UnityEngine.PlayerPrefs.SetInt("Input_Mode", 0);
					GM.Resurrect();
				}
				else if (rayIntersection.collider.name == "Pos_GameOver_1")
				{
					Sel_Index = 1;
					global::UnityEngine.PlayerPrefs.SetInt("Input_Mode", 0);
					Restart();
				}
				else if (rayIntersection.collider.name == "Pos_GameOver_2")
				{
					Sel_Index = 2;
					global::UnityEngine.PlayerPrefs.SetInt("Input_Mode", 0);
					Exit();
				}
				else if (rayIntersection.collider.name == "Cum_Icon" && H_Timer > 1.5f && Cum_Timer <= 0f)
				{
					CumShot();
				}
			}
			return;
		}
		global::UnityEngine.Ray ray2 = global::UnityEngine.GameObject.Find("UI Camera").camera.ScreenPointToRay(global::UnityEngine.Input.mousePosition);
		global::UnityEngine.RaycastHit2D rayIntersection2 = global::UnityEngine.Physics2D.GetRayIntersection(ray2, float.PositiveInfinity);
		if (rayIntersection2.collider != null)
		{
			if (rayIntersection2.collider.name == "Pos_GameOver_0")
			{
				Sel_Index = 0;
			}
			else if (rayIntersection2.collider.name == "Pos_GameOver_1")
			{
				Sel_Index = 1;
			}
			else if (rayIntersection2.collider.name == "Pos_GameOver_2")
			{
				Sel_Index = 2;
			}
		}
	}

	private void CumShot()
	{
		if (H_Object != null)
		{
			Cum_Timer = 0.2f;
			H_Object.SendMessage("CumShot");
			global::UnityEngine.GameObject.Find("Cum_Icon").GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(0.7f, 0.7f, 1f);
			global::UnityEngine.GameObject.Find("Cum_Icon").GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(460f, -10f, 1f);
		}
	}
}
