public class H_GameOver_2 : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.SkinnedMeshRenderer EyeOpen;

	public global::UnityEngine.SpriteRenderer[] EyeClose_List;

	private int Eye_State = -1;

	private int Eye_Num = -1;

	private float Eye_Timer;

	private bool Eye_Half;

	public global::UnityEngine.Transform Eye_L;

	public global::UnityEngine.Transform Eye_R;

	public global::UnityEngine.Transform Eye_L_H;

	public global::UnityEngine.Transform Eye_R_H;

	private int Eye_Pos_State;

	private float Eye_Pos_Timer;

	private float Eye_H_Timer;

	private global::UnityEngine.Vector3 pos_Orig = new global::UnityEngine.Vector3(0f, 0f, 0f);

	private global::UnityEngine.Vector3 pos_Eye_Down_L = new global::UnityEngine.Vector3(-0.019f, -0.138f, 0f);

	private global::UnityEngine.Vector3 pos_Eye_Down_R = new global::UnityEngine.Vector3(-0.081f, -0.069f, 0f);

	private global::UnityEngine.Vector3 pos_Eye_Side_L = new global::UnityEngine.Vector3(-0.091f, -0.013f, 0f);

	private global::UnityEngine.Vector3 pos_Eye_Side_R = new global::UnityEngine.Vector3(-0.078f, 0.013f, 0f);

	private global::UnityEngine.Vector3 pos_Eye_Up_L = new global::UnityEngine.Vector3(0.1f, 0.146f, 0f);

	private global::UnityEngine.Vector3 pos_Eye_Up_R = new global::UnityEngine.Vector3(0.076f, 0.149f, 0f);

	public global::UnityEngine.SkinnedMeshRenderer Mouth_1;

	public global::UnityEngine.SkinnedMeshRenderer Mouth_2;

	public global::UnityEngine.Material Mat_Tentacle_1_1;

	public global::UnityEngine.Material Mat_Tentacle_1_2;

	public global::UnityEngine.Material Mat_Tentacle_2_1;

	public global::UnityEngine.Material Mat_Tentacle_2_2;

	public global::UnityEngine.Material Mat_Tentacle_3_1;

	public global::UnityEngine.Material Mat_Tentacle_3_2;

	public global::UnityEngine.GameObject Tentacle_C;

	public global::UnityEngine.GameObject Tentacle_LB;

	public global::UnityEngine.GameObject Tentacle_RB;

	public global::UnityEngine.GameObject Tentacle_LT;

	public global::UnityEngine.GameObject Tentacle_RT;

	public global::UnityEngine.Transform pos_Penis_C;

	public global::UnityEngine.Transform pos_Penis_LB;

	public global::UnityEngine.Transform pos_Penis_RB;

	public global::UnityEngine.Transform pos_Penis_LT;

	public global::UnityEngine.Transform pos_Penis_RT;

	public global::UnityEngine.Transform pos_Mouth;

	public global::UnityEngine.GameObject[] CumShot;

	public global::UnityEngine.GameObject[] CumDot;

	public global::UnityEngine.GameObject[] CumShot_N;

	public global::UnityEngine.GameObject[] BG;

	public global::UnityEngine.GameObject[] Cum_1_List;

	public global::UnityEngine.GameObject[] Cum_2_List;

	public global::UnityEngine.SpriteRenderer Pink_Glow;

	public global::UnityEngine.GameObject CensoredBox;

	private float Life_Timer;

	private int State;

	private int Cum_Num;

	private float Press_Timer;

	private float Cum_Timer;

	private float CumCombo_Delay;

	private float Cursor_Timer;

	private float Speed = 2f;

	private int BG_Num = 1;

	private float Sound_Timer;

	private global::UnityEngine.Color color_ON = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_OFF = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	private global::UnityEngine.Color color_Cum_1 = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	private global::UnityEngine.Color color_Cum_2 = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	private global::UnityEngine.Color color_BG_ON = new global::UnityEngine.Color(1f, 1f, 1f, 0.3f);

	private global::UnityEngine.Color color_BG_OFF = new global::UnityEngine.Color(1f, 1f, 1f, 0.05f);

	private global::UnityEngine.Color color_Blue_ON = new global::UnityEngine.Color(0f, 0f, 1f, 0.25f);

	private global::UnityEngine.Color color_Blue_OFF = new global::UnityEngine.Color(0f, 0f, 1f, 0.05f);

	private global::UnityEngine.UI.Image Cum_BG;

	private global::UnityEngine.UI.Image Cum_Bar;

	private global::UnityEngine.UI.Image Cum_Bar_After;

	private global::UnityEngine.UI.Image Cum_Cursor;

	private global::UnityEngine.UI.Image Cum_Cursor_Glow;

	private float Cum_1_Timer;

	private float Cum_2_Timer;

	private float Cum_Rnd_Timer;

	private float Cum_Penis_C_Timer;

	private float Cum_Penis_LB_Timer;

	private float Cum_Penis_RB_Timer;

	private float Cum_Penis_LT_Timer;

	private float Cum_Penis_RT_Timer;

	private int Moan_Num;

	private float Moan_Timer;

	private float Moan_End_Timer;

	private H_SoundControl H_Sound;

	private void Start()
	{
		if (global::UnityEngine.GameObject.Find("Sound_List_H") != null)
		{
			H_Sound = global::UnityEngine.GameObject.Find("Sound_List_H").GetComponent<H_SoundControl>();
		}
		Reset_Cum();
		Speed = 1f;
		GetComponent<global::UnityEngine.Animator>().speed = Speed;
		if (global::UnityEngine.PlayerPrefs.GetInt("Censorship") != 1)
		{
			global::UnityEngine.Object.Destroy(CensoredBox.gameObject);
		}
		if (global::UnityEngine.GameObject.Find("GameOver_Manager") != null)
		{
			global::UnityEngine.PlayerPrefs.SetInt("Gallery_Grayscale_GameOver", 0);
			BG_Num = global::UnityEngine.Random.Range(1, 5);
			Set_Option(BG_Num);
			if (global::UnityEngine.GameObject.Find("Main Camera").GetComponent<GameOver_Camera>() != null)
			{
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<GameOver_Camera>().Cam_Top = 10f;
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<GameOver_Camera>().Cam_Bot = -10f;
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<GameOver_Camera>().Cam_Right = 20f;
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<GameOver_Camera>().Cam_Left = -20f;
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<UnityEngine.Camera>().orthographicSize = 4.5f;
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<GameOver_Camera>().targetSize = 7f;
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<GameOver_Camera>().MaxSize = 8.4f;
				global::UnityEngine.GameObject.Find("Main Camera").transform.position = new global::UnityEngine.Vector3(0f, 0.8f, -10f);
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<GameOver_Camera>().Set_TargetPos(new global::UnityEngine.Vector3(0f, 0.8f, -10f));
			}
		}
		else
		{
			Set_Option(global::UnityEngine.PlayerPrefs.GetInt("Gallery_Option_GameOver"));
			if (global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Gallery_Camera>() != null)
			{
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Gallery_Camera>().Cam_Top = 10f;
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Gallery_Camera>().Cam_Bot = -10f;
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Gallery_Camera>().Cam_Right = 20f;
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Gallery_Camera>().Cam_Left = -20f;
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<UnityEngine.Camera>().orthographicSize = 4.5f;
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Gallery_Camera>().targetSize = 7f;
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Gallery_Camera>().MaxSize = 8.4f;
				global::UnityEngine.GameObject.Find("Main Camera").transform.position = new global::UnityEngine.Vector3(0f, 0.8f, -10f);
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Gallery_Camera>().Set_TargetPos(new global::UnityEngine.Vector3(0f, 0.8f, -10f));
			}
		}
		Cum_BG = global::UnityEngine.GameObject.Find("GO_Cum_BG").GetComponent<global::UnityEngine.UI.Image>();
		Cum_Bar = global::UnityEngine.GameObject.Find("GO_Cum_Bar").GetComponent<global::UnityEngine.UI.Image>();
		Cum_Bar_After = global::UnityEngine.GameObject.Find("GO_Cum_Bar_After").GetComponent<global::UnityEngine.UI.Image>();
		Cum_Cursor = global::UnityEngine.GameObject.Find("GO_Cum_Cursor").GetComponent<global::UnityEngine.UI.Image>();
		Cum_Cursor_Glow = global::UnityEngine.GameObject.Find("GO_Cum_Cursor_Glow").GetComponent<global::UnityEngine.UI.Image>();
		Cum_BG.color = color_OFF;
		Cum_Bar.fillAmount = 0f;
		Cum_Bar_After.color = color_Blue_OFF;
		Cum_Cursor.color = color_OFF;
		Cum_Cursor_Glow.color = color_OFF;
		if (global::UnityEngine.GameObject.Find("Mosaic Camera") != null)
		{
			global::UnityEngine.GameObject.Find("Mosaic Camera").GetComponent<Gallery_Mosaic>().Set_Mosaic(2);
		}
	}

	private void Update()
	{
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (Sound_Timer > 0f)
		{
			Sound_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Cum_Rnd_Timer > 0f)
		{
			Cum_Rnd_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Cum_Penis_C_Timer > 0f)
		{
			Cum_Penis_C_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Cum_Penis_LB_Timer > 0f)
		{
			Cum_Penis_LB_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Cum_Penis_RB_Timer > 0f)
		{
			Cum_Penis_RB_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Cum_Penis_LT_Timer > 0f)
		{
			Cum_Penis_LT_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Cum_Penis_RT_Timer > 0f)
		{
			Cum_Penis_RT_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Moan_Timer > 0f)
		{
			Moan_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Moan_End_Timer > 0f)
		{
			Moan_End_Timer -= global::UnityEngine.Time.deltaTime;
		}
		Eye_Check();
		if (global::UnityEngine.Input.GetAxis("L_Trigger") != 0f || global::UnityEngine.Input.GetKey(global::UnityEngine.KeyCode.Space) || global::UnityEngine.Input.GetMouseButton(1))
		{
			Press_Timer += global::UnityEngine.Time.deltaTime;
			if (State == 1)
			{
				if (Cum_Num == 2)
				{
					Cum_2();
				}
			}
			else if (CumCombo_Delay > 0f)
			{
				CumCombo_Delay -= global::UnityEngine.Time.deltaTime;
				Speed = global::UnityEngine.Mathf.Lerp(Speed, 1f, global::UnityEngine.Time.deltaTime * 2f);
				GetComponent<global::UnityEngine.Animator>().speed = Speed;
			}
			else if (Press_Timer > 0.1f)
			{
				Speed += global::UnityEngine.Time.deltaTime * 0.5f;
				GetComponent<global::UnityEngine.Animator>().speed = Speed;
				if (Speed >= 4f)
				{
					State = 1;
					GetComponent<global::UnityEngine.Animator>().SetInteger("State", 1);
					if (Cum_Num == 0)
					{
						Cum_1();
					}
					else if (Cum_Num == 1)
					{
						Cum_2();
					}
				}
			}
			else
			{
				Speed = global::UnityEngine.Mathf.Lerp(Speed, 1f, global::UnityEngine.Time.deltaTime * 1f);
				GetComponent<global::UnityEngine.Animator>().speed = Speed;
			}
			if (Cum_Num > 1)
			{
				Cursor_Timer += global::UnityEngine.Time.deltaTime;
				Cum_Cursor_Glow.color = global::UnityEngine.Color.Lerp(Cum_Cursor_Glow.color, new global::UnityEngine.Color(1f, 1f, 1f, (1f + global::UnityEngine.Mathf.Sin(Cursor_Timer * 20f)) * 0.5f), global::UnityEngine.Time.deltaTime * 3f);
			}
			else
			{
				Cum_Cursor_Glow.color = global::UnityEngine.Color.Lerp(Cum_Cursor_Glow.color, color_BG_ON, global::UnityEngine.Time.deltaTime * 1f);
			}
			Cum_BG.color = global::UnityEngine.Color.Lerp(Cum_BG.color, color_BG_ON, global::UnityEngine.Time.deltaTime * 1f);
			Cum_Bar.color = global::UnityEngine.Color.Lerp(Cum_Bar.color, color_BG_ON, global::UnityEngine.Time.deltaTime * 1f);
			Cum_Bar_After.color = global::UnityEngine.Color.Lerp(Cum_Bar_After.color, color_Blue_ON, global::UnityEngine.Time.deltaTime * 1f);
			Cum_Cursor.color = global::UnityEngine.Color.Lerp(Cum_Cursor.color, color_BG_ON, global::UnityEngine.Time.deltaTime * 1f);
		}
		else
		{
			if (Speed < 1.01f && Press_Timer > 0f)
			{
				Press_Timer = 0f;
			}
			if (State == 1)
			{
				State = 0;
				GetComponent<global::UnityEngine.Animator>().SetInteger("State", 0);
			}
			else if (State == 0)
			{
				if (Speed < 1.05f && Cum_Num > 0)
				{
					Cum_Num = 0;
				}
				Speed = global::UnityEngine.Mathf.Lerp(Speed, 1f, global::UnityEngine.Time.deltaTime * 1.5f);
				GetComponent<global::UnityEngine.Animator>().speed = Speed;
			}
			Cum_BG.color = global::UnityEngine.Color.Lerp(Cum_BG.color, color_BG_OFF, global::UnityEngine.Time.deltaTime);
			Cum_Bar.color = global::UnityEngine.Color.Lerp(Cum_Bar.color, color_BG_OFF, global::UnityEngine.Time.deltaTime);
			Cum_Bar_After.color = global::UnityEngine.Color.Lerp(Cum_Bar_After.color, color_Blue_OFF, global::UnityEngine.Time.deltaTime);
			Cum_Cursor.color = global::UnityEngine.Color.Lerp(Cum_Cursor.color, color_OFF, global::UnityEngine.Time.deltaTime);
			Cum_Cursor_Glow.color = global::UnityEngine.Color.Lerp(Cum_Cursor_Glow.color, color_OFF, global::UnityEngine.Time.deltaTime);
		}
		if (Cum_Bar != null)
		{
			float num = ((State != 0) ? 1f : ((Speed - 1f) / 3f));
			Cum_Bar.fillAmount = global::UnityEngine.Mathf.Lerp(Cum_Bar.fillAmount, num, global::UnityEngine.Time.deltaTime * 3f);
			Cum_Cursor.GetComponent<global::UnityEngine.RectTransform>().localPosition = global::UnityEngine.Vector3.Lerp(Cum_Cursor.GetComponent<global::UnityEngine.RectTransform>().localPosition, new global::UnityEngine.Vector3(-187f + 374f * num, 0f, 0f), global::UnityEngine.Time.deltaTime * 3f);
		}
		if (global::UnityEngine.GameObject.Find("GameOver_Manager") != null)
		{
			if (global::UnityEngine.Input.GetButtonDown("L_B") || global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.A))
			{
				if (BG_Num > 1)
				{
					Set_Option(BG_Num - 1);
				}
				else
				{
					Set_Option(4);
				}
			}
			else if (global::UnityEngine.Input.GetButtonDown("R_B") || global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.S))
			{
				if (BG_Num < 4)
				{
					Set_Option(BG_Num + 1);
				}
				else
				{
					Set_Option(1);
				}
			}
		}
		if (Cum_1_Timer > 0f)
		{
			Cum_1_Timer -= global::UnityEngine.Time.deltaTime;
			color_Cum_1 = global::UnityEngine.Color.Lerp(color_Cum_1, color_ON, global::UnityEngine.Time.deltaTime * 2f);
		}
		else
		{
			color_Cum_1 = global::UnityEngine.Color.Lerp(color_Cum_1, color_OFF, global::UnityEngine.Time.deltaTime * 1f);
		}
		if (Cum_2_Timer > 0f)
		{
			Cum_2_Timer -= global::UnityEngine.Time.deltaTime;
			color_Cum_2 = global::UnityEngine.Color.Lerp(color_Cum_2, color_ON, global::UnityEngine.Time.deltaTime * 2f);
		}
		else
		{
			color_Cum_2 = global::UnityEngine.Color.Lerp(color_Cum_2, color_OFF, global::UnityEngine.Time.deltaTime * 1f);
		}
		Set_Cum_Opacity();
		if (State > 0 && Cum_Num > 1)
		{
			if (Mouth_1.enabled)
			{
				Mouth_1.enabled = false;
				Mouth_2.enabled = true;
				Cum_1_List[0].GetComponent<global::UnityEngine.SkinnedMeshRenderer>().enabled = false;
				Cum_1_List[1].GetComponent<global::UnityEngine.SkinnedMeshRenderer>().enabled = true;
			}
		}
		else if (!Mouth_1.enabled)
		{
			Mouth_1.enabled = true;
			Mouth_2.enabled = false;
			Cum_1_List[0].GetComponent<global::UnityEngine.SkinnedMeshRenderer>().enabled = true;
			Cum_1_List[1].GetComponent<global::UnityEngine.SkinnedMeshRenderer>().enabled = false;
		}
		if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.Backspace))
		{
			Delete_Cum();
		}
	}

	private void Eye_Check()
	{
		if (Eye_State > 0)
		{
			if (Eye_Timer <= 0f)
			{
				Eye_Num++;
				Eye_Timer = 0.02f;
				if (Eye_Num == 0)
				{
					EyeClose_List[0].enabled = true;
					EyeOpen.enabled = false;
				}
				else if (Eye_Num == 1)
				{
					EyeClose_List[1].enabled = true;
					EyeClose_List[0].enabled = false;
					if (Eye_Half)
					{
						Eye_State = -1;
						Eye_Timer = 1f;
					}
				}
				else if (Eye_Num == 2)
				{
					EyeClose_List[2].enabled = true;
					EyeClose_List[1].enabled = false;
				}
				else if (Eye_Num == 3)
				{
					EyeClose_List[1].enabled = true;
					EyeClose_List[2].enabled = false;
				}
				else if (Eye_Num == 4)
				{
					EyeClose_List[0].enabled = true;
					EyeClose_List[1].enabled = false;
				}
				else
				{
					EyeOpen.enabled = true;
					EyeClose_List[0].enabled = false;
					EyeClose_List[1].enabled = false;
					EyeClose_List[2].enabled = false;
					Eye_State = -1;
					Eye_Num = -1;
					Eye_Timer = 1f;
				}
			}
			else
			{
				Eye_Timer -= global::UnityEngine.Time.deltaTime;
			}
		}
		else
		{
			Eye_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (State == 0)
		{
			if (Eye_Pos_Timer <= 0f)
			{
				if (global::UnityEngine.Random.Range(0, 10) > 4)
				{
					Eye_Pos_State = global::UnityEngine.Random.Range(0, 3);
					Eye_Pos_Timer = 2f;
				}
			}
			else
			{
				Eye_Pos_Timer -= global::UnityEngine.Time.deltaTime;
			}
			if (Eye_Pos_State == 0)
			{
				Eye_L.localPosition = global::UnityEngine.Vector3.Lerp(Eye_L.localPosition, pos_Orig, global::UnityEngine.Time.deltaTime * 5f);
				Eye_R.localPosition = global::UnityEngine.Vector3.Lerp(Eye_R.localPosition, pos_Orig, global::UnityEngine.Time.deltaTime * 5f);
			}
			else if (Eye_Pos_State == 1)
			{
				Eye_L.localPosition = global::UnityEngine.Vector3.Lerp(Eye_L.localPosition, pos_Eye_Side_L, global::UnityEngine.Time.deltaTime * 5f);
				Eye_R.localPosition = global::UnityEngine.Vector3.Lerp(Eye_R.localPosition, pos_Eye_Side_R, global::UnityEngine.Time.deltaTime * 5f);
			}
			else
			{
				Eye_L.localPosition = global::UnityEngine.Vector3.Lerp(Eye_L.localPosition, pos_Eye_Down_L, global::UnityEngine.Time.deltaTime * 5f);
				Eye_R.localPosition = global::UnityEngine.Vector3.Lerp(Eye_R.localPosition, pos_Eye_Down_R, global::UnityEngine.Time.deltaTime * 5f);
			}
		}
		else
		{
			Eye_Pos_State = 0;
			Eye_Pos_Timer = 2f;
			Eye_L.localPosition = global::UnityEngine.Vector3.Lerp(Eye_L.localPosition, pos_Eye_Up_L, global::UnityEngine.Time.deltaTime * 8f);
			Eye_R.localPosition = global::UnityEngine.Vector3.Lerp(Eye_R.localPosition, pos_Eye_Up_R, global::UnityEngine.Time.deltaTime * 8f);
		}
		Eye_H_Timer += global::UnityEngine.Time.deltaTime;
		if (Eye_H_Timer > 0.06f)
		{
			Eye_H_Timer = 0f;
			float x = (float)global::UnityEngine.Random.Range(0, 150) * 0.0001f;
			float y = (float)global::UnityEngine.Random.Range(0, 150) * 0.0001f;
			Eye_L_H.localPosition = new global::UnityEngine.Vector3(x, y, 0f);
			x = (float)global::UnityEngine.Random.Range(0, 130) * 0.0001f;
			y = (float)global::UnityEngine.Random.Range(0, 130) * 0.0001f;
			Eye_R_H.localPosition = new global::UnityEngine.Vector3(x, y, 0f);
		}
	}

	private void Eye_Close()
	{
		if (Eye_State < 0 && Eye_Timer <= 0f && global::UnityEngine.Random.Range(0, 10) > 4)
		{
			Eye_State = 1;
			Eye_Timer = 0f;
			if (global::UnityEngine.Random.Range(0, 10) > 5)
			{
				Eye_Half = true;
			}
			else
			{
				Eye_Half = false;
			}
		}
	}

	private void Cum_Hold()
	{
		if (color_Cum_1.a > 0f && Cum_Rnd_Timer <= 0f)
		{
			if (global::UnityEngine.Random.Range(0, 10) > 7)
			{
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(CumShot[global::UnityEngine.Random.Range(1, 6)], pos_Penis_C.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 90f)) as global::UnityEngine.GameObject;
				gameObject.GetComponent<CumShot_1>().Set_SortingOrder(131, pos_Penis_C, 0.5f);
			}
			if (Cum_1_Timer > 0f && global::UnityEngine.Random.Range(0, 10) > 5)
			{
				global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(CumShot[global::UnityEngine.Random.Range(2, 6)], pos_Mouth.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 90f)) as global::UnityEngine.GameObject;
				gameObject2.GetComponent<CumShot_1>().Set_SortingOrder(200, pos_Mouth, 0.5f);
			}
			Cum_Rnd_Timer = 0.2f;
		}
	}

	private void Cum_Penis_All()
	{
		if (Cum_Num < 1)
		{
			Cum_Num++;
			State = 0;
			GetComponent<global::UnityEngine.Animator>().SetInteger("State", 0);
			CumCombo_Delay = 1f;
			if (global::UnityEngine.Input.GetAxis("L_Trigger") != 0f || global::UnityEngine.Input.GetKey(global::UnityEngine.KeyCode.Space) || global::UnityEngine.Input.GetMouseButton(1))
			{
				Press_Timer = 1f;
			}
			else
			{
				Press_Timer = 0f;
			}
		}
		else if (Cum_Num == 1)
		{
			Cum_Num = 2;
		}
		Cum_Penis_LB();
		Cum_Penis_RB();
		Cum_Penis_LT();
		Cum_Penis_RT();
	}

	private void Cum_Penis_Total()
	{
		Cum_Penis_LB();
		Cum_Penis_RB();
		Cum_Penis_LT();
		Cum_Penis_RT();
	}

	private void Cum_Penis_LB()
	{
		if (Cum_Penis_LB_Timer <= 0f)
		{
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(CumShot[global::UnityEngine.Random.Range(1, 6)], pos_Penis_LB.position, pos_Penis_LB.rotation) as global::UnityEngine.GameObject;
			gameObject.SendMessage("Start_Long");
			global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(CumDot[0], pos_Penis_LB.position, pos_Penis_LB.rotation) as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(CumDot[1], pos_Penis_LB.position, pos_Penis_LB.rotation) as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject gameObject4 = global::UnityEngine.Object.Instantiate(CumDot[2], pos_Penis_LB.position, pos_Penis_LB.rotation) as global::UnityEngine.GameObject;
			Cum_Penis_LB_Timer = 0.3f;
		}
	}

	private void Cum_Penis_RB()
	{
		if (Cum_Penis_RB_Timer <= 0f)
		{
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(CumShot[global::UnityEngine.Random.Range(1, 6)], pos_Penis_RB.position, pos_Penis_RB.rotation) as global::UnityEngine.GameObject;
			gameObject.SendMessage("Start_Long");
			global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(CumDot[0], pos_Penis_RB.position, pos_Penis_RB.rotation) as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(CumDot[1], pos_Penis_RB.position, pos_Penis_RB.rotation) as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject gameObject4 = global::UnityEngine.Object.Instantiate(CumDot[2], pos_Penis_RB.position, pos_Penis_RB.rotation) as global::UnityEngine.GameObject;
			Cum_Penis_RB_Timer = 0.3f;
		}
	}

	private void Cum_Penis_LT()
	{
		if (Cum_Penis_LT_Timer <= 0f)
		{
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(CumShot[global::UnityEngine.Random.Range(1, 6)], pos_Penis_LT.position, pos_Penis_LT.rotation) as global::UnityEngine.GameObject;
			gameObject.SendMessage("Start_Long");
			global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(CumDot[0], pos_Penis_LT.position, pos_Penis_LT.rotation) as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(CumDot[1], pos_Penis_LT.position, pos_Penis_LT.rotation) as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject gameObject4 = global::UnityEngine.Object.Instantiate(CumDot[2], pos_Penis_LT.position, pos_Penis_LT.rotation) as global::UnityEngine.GameObject;
			Cum_Penis_LT_Timer = 0.3f;
		}
	}

	private void Cum_Penis_RT()
	{
		if (Cum_Penis_RT_Timer <= 0f)
		{
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(CumShot[global::UnityEngine.Random.Range(1, 6)], pos_Penis_RT.position, pos_Penis_RT.rotation) as global::UnityEngine.GameObject;
			gameObject.SendMessage("Start_Long");
			global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(CumDot[0], pos_Penis_RT.position, pos_Penis_RT.rotation) as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(CumDot[1], pos_Penis_RT.position, pos_Penis_RT.rotation) as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject gameObject4 = global::UnityEngine.Object.Instantiate(CumDot[2], pos_Penis_RT.position, pos_Penis_RT.rotation) as global::UnityEngine.GameObject;
			Cum_Penis_RT_Timer = 0.3f;
		}
	}

	public void Set_Option(int num)
	{
		if (num < 1)
		{
			BG_Num = 1;
		}
		else if (num > 5)
		{
			BG_Num = 5;
		}
		else
		{
			BG_Num = num;
		}
		if (BG_Num == 2)
		{
			global::UnityEngine.GameObject.Find("Main Camera").SendMessage("Set_Vignetting_10");
		}
		else
		{
			global::UnityEngine.GameObject.Find("Main Camera").SendMessage("Set_Vignetting_13");
		}
		if (global::UnityEngine.PlayerPrefs.GetInt("Gallery_Grayscale_GameOver") == 1)
		{
			if (!global::UnityEngine.GameObject.Find("Main Camera").GetComponent<GrayscaleEffect>().enabled)
			{
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<GrayscaleEffect>().enabled = true;
			}
			if (global::UnityEngine.GameObject.Find("Main Camera").GetComponent<NoiseEffect>().enabled)
			{
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<NoiseEffect>().enabled = false;
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
		switch (BG_Num)
		{
		case 1:
			Set_BG(0);
			Set_Tentacle(1);
			if (global::UnityEngine.PlayerPrefs.GetInt("Gallery_Grayscale_GameOver") == 1)
			{
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<GrayscaleEffect>().rampOffset = -0.05f;
				global::UnityEngine.GameObject.Find("Main Camera").SendMessage("Set_Bloom_ON_50");
			}
			else
			{
				global::UnityEngine.GameObject.Find("Main Camera").SendMessage("Set_Bloom_ON_40");
			}
			break;
		case 2:
			Set_BG(1);
			Set_Tentacle(1);
			if (global::UnityEngine.PlayerPrefs.GetInt("Gallery_Grayscale_GameOver") == 1)
			{
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<GrayscaleEffect>().rampOffset = -0.1f;
				global::UnityEngine.GameObject.Find("Main Camera").SendMessage("Set_Bloom_ON_40");
			}
			else
			{
				global::UnityEngine.GameObject.Find("Main Camera").SendMessage("Set_Bloom_ON_50");
			}
			break;
		case 3:
			Set_BG(2);
			Set_Tentacle(2);
			if (global::UnityEngine.PlayerPrefs.GetInt("Gallery_Grayscale_GameOver") == 1)
			{
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<GrayscaleEffect>().rampOffset = -0.1f;
				global::UnityEngine.GameObject.Find("Main Camera").SendMessage("Set_Bloom_ON_30");
			}
			else
			{
				global::UnityEngine.GameObject.Find("Main Camera").SendMessage("Set_Bloom_ON_45");
			}
			break;
		case 4:
			Set_BG(3);
			Set_Tentacle(2);
			if (global::UnityEngine.PlayerPrefs.GetInt("Gallery_Grayscale_GameOver") == 1)
			{
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<GrayscaleEffect>().rampOffset = -0.1f;
				global::UnityEngine.GameObject.Find("Main Camera").SendMessage("Set_Bloom_ON_40");
			}
			else
			{
				global::UnityEngine.GameObject.Find("Main Camera").SendMessage("Set_Bloom_ON_60");
			}
			break;
		case 5:
			break;
		}
	}

	private void Set_BG(int num)
	{
		for (int i = 0; i < 5; i++)
		{
			if (i == num)
			{
				BG[i].transform.position = new global::UnityEngine.Vector3(0f, 0f, 0f);
			}
			else
			{
				BG[i].transform.position = new global::UnityEngine.Vector3(100f, -100f, 0f);
			}
		}
	}

	private void Set_Tentacle(int num)
	{
		if (num == 1)
		{
			Tentacle_C.GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material = Mat_Tentacle_1_1;
			Tentacle_LB.GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material = Mat_Tentacle_1_1;
			Tentacle_RB.GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material = Mat_Tentacle_1_1;
			Tentacle_LT.GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material = Mat_Tentacle_2_1;
			Tentacle_RT.GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material = Mat_Tentacle_3_1;
		}
		else
		{
			Tentacle_C.GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material = Mat_Tentacle_1_2;
			Tentacle_LB.GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material = Mat_Tentacle_1_2;
			Tentacle_RB.GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material = Mat_Tentacle_1_2;
			Tentacle_LT.GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material = Mat_Tentacle_2_2;
			Tentacle_RT.GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material = Mat_Tentacle_3_2;
		}
	}

	private void Cum_1()
	{
		Cum_1_Timer = 20f;
	}

	private void Cum_2()
	{
		Cum_1_Timer = 32f;
		Cum_2_Timer = 30f;
	}

	private void Delete_Cum()
	{
		Cum_1_Timer = (Cum_2_Timer = 0f);
	}

	private void Reset_Cum()
	{
		for (int i = 0; i < Cum_1_List.Length; i++)
		{
			Cum_1_List[i].GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material.color = color_OFF;
		}
		for (int j = 0; j < Cum_2_List.Length; j++)
		{
			Cum_2_List[j].GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material.color = color_OFF;
		}
		Pink_Glow.color = color_OFF;
	}

	private void Set_Cum_Opacity()
	{
		for (int i = 0; i < Cum_1_List.Length; i++)
		{
			Cum_1_List[i].GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material.color = color_Cum_1;
		}
		for (int j = 0; j < Cum_2_List.Length; j++)
		{
			Cum_2_List[j].GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material.color = color_Cum_2;
		}
		Pink_Glow.color = color_Cum_1;
	}

	private void Sound_Piston()
	{
		if (Sound_Timer <= 0f && H_Sound != null)
		{
			H_Sound.SendMessage("Sound_Piston_10");
		}
	}

	private void Sound_Piston_Sub()
	{
		if (Sound_Timer <= 0f && H_Sound != null)
		{
			H_Sound.SendMessage("Sound_Piston_5");
		}
	}

	private void Sound_Piston_End()
	{
		if (Sound_Timer <= 0f && H_Sound != null)
		{
			H_Sound.SendMessage("Sound_Piston_4");
			Sound_Timer = 0.3f;
		}
	}

	private void Sound_Moan()
	{
		if (H_Sound != null && Moan_Timer <= 0f && Speed < 5.2f)
		{
			int num = global::UnityEngine.Random.Range(1, 11);
			if (Moan_Num != num && num != 3)
			{
				Moan_Num = num;
				H_Sound.Sound_Moan(num, 0);
				Moan_Timer = 5f / Speed;
			}
		}
	}

	private void Sound_Moan_End()
	{
		if (!(H_Sound != null))
		{
			return;
		}
		if (Moan_End_Timer <= 0f)
		{
			int num = global::UnityEngine.Random.Range(15, 19);
			if (num == Moan_Num)
			{
				switch (num)
				{
				case 15:
					num = 16;
					break;
				case 16:
					num = 17;
					break;
				case 17:
					num = 18;
					break;
				default:
					num = 15;
					break;
				}
			}
			Moan_Num = num;
			H_Sound.Sound_Moan(num, 0);
			Moan_Timer = 2f;
			Moan_End_Timer = 2.5f;
		}
		else if (Moan_Timer <= 0f)
		{
			int num2 = global::UnityEngine.Random.Range(7, 13);
			if (Moan_Num != num2)
			{
				Moan_Num = num2;
				H_Sound.Sound_Moan(num2, 0);
				Moan_Timer = 2f;
			}
		}
	}
}
