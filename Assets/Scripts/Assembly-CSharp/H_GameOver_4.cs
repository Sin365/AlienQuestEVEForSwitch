public class H_GameOver_4 : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.SkinnedMeshRenderer EyeOpen;

	public global::UnityEngine.SkinnedMeshRenderer Mouth;

	public global::UnityEngine.SkinnedMeshRenderer Tooth;

	public global::UnityEngine.SkinnedMeshRenderer Mouth_In;

	public global::UnityEngine.SkinnedMeshRenderer Mouth_Small;

	public global::UnityEngine.Sprite[] Spr_List;

	public global::UnityEngine.SpriteRenderer EyeClose;

	public global::UnityEngine.SpriteRenderer EyeHL_L;

	public global::UnityEngine.SpriteRenderer EyeHL_R;

	public global::UnityEngine.SpriteRenderer Pink_Glow;

	private int Eye_State = -1;

	private int Eye_Num = -1;

	private float Eye_Timer;

	private float Ani_Timer;

	private float EyeHL_Opacity = 1f;

	private float Mouth_Timer;

	private global::UnityEngine.Color color_Mouth = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	public global::UnityEngine.Transform pos_Penis_Center;

	public global::UnityEngine.Transform pos_Penis_LT;

	public global::UnityEngine.Transform pos_Penis_RT;

	public global::UnityEngine.Material[] Mat_Tentacle;

	public global::UnityEngine.GameObject[] Tentacle;

	public global::UnityEngine.GameObject[] BG;

	public global::UnityEngine.GameObject[] CumShot;

	public global::UnityEngine.GameObject[] CumDot;

	public global::UnityEngine.GameObject[] Cum_1_List;

	public global::UnityEngine.GameObject[] Cum_2_List;

	public global::UnityEngine.GameObject CensoredBox;

	private float Life_Timer;

	private int State;

	private int Cum_Num;

	private float Press_Timer;

	private float Cum_Timer;

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
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<GameOver_Camera>().Cam_Top = 11f;
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<GameOver_Camera>().Cam_Bot = -9f;
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<GameOver_Camera>().Cam_Right = 20f;
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<GameOver_Camera>().Cam_Left = -20f;
				global::UnityEngine.GameObject.Find("Main Camera").camera.orthographicSize = 4.5f;
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<GameOver_Camera>().targetSize = 7f;
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<GameOver_Camera>().MaxSize = 8.4f;
				global::UnityEngine.GameObject.Find("Main Camera").transform.position = new global::UnityEngine.Vector3(-0.4f, -0.4f, -10f);
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<GameOver_Camera>().Set_TargetPos(new global::UnityEngine.Vector3(-0.4f, -0.4f, -10f));
			}
		}
		else
		{
			Set_Option(global::UnityEngine.PlayerPrefs.GetInt("Gallery_Option_GameOver"));
			if (global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Gallery_Camera>() != null)
			{
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Gallery_Camera>().Cam_Top = 11f;
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Gallery_Camera>().Cam_Bot = -9f;
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Gallery_Camera>().Cam_Right = 20f;
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Gallery_Camera>().Cam_Left = -20f;
				global::UnityEngine.GameObject.Find("Main Camera").camera.orthographicSize = 4.5f;
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Gallery_Camera>().targetSize = 7f;
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Gallery_Camera>().MaxSize = 8.4f;
				global::UnityEngine.GameObject.Find("Main Camera").transform.position = new global::UnityEngine.Vector3(-0.4f, -0.4f, -10f);
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Gallery_Camera>().Set_TargetPos(new global::UnityEngine.Vector3(-0.4f, -0.4f, -10f));
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
		Eye_Timer = global::UnityEngine.Random.Range(1.5f, 5f);
		Mouth_Timer = global::UnityEngine.Random.Range(5f, 10f);
		color_Mouth = color_OFF;
		Mouth.material.color = color_Mouth;
		Tooth.material.color = color_Mouth;
		Mouth_In.material.color = color_Mouth;
		Pink_Glow.color = color_OFF;
		if (global::UnityEngine.GameObject.Find("Mosaic Camera") != null)
		{
			global::UnityEngine.GameObject.Find("Mosaic Camera").GetComponent<Gallery_Mosaic>().Set_Mosaic(4);
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
					Speed = 1f;
					GetComponent<global::UnityEngine.Animator>().speed = Speed;
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
			if (Mouth_Timer > 0f)
			{
				Mouth_Timer -= global::UnityEngine.Time.deltaTime;
			}
			else
			{
				color_Mouth = global::UnityEngine.Color.Lerp(color_Mouth, color_OFF, global::UnityEngine.Time.deltaTime * 5f);
				Mouth.material.color = color_Mouth;
				Tooth.material.color = color_Mouth;
				Mouth_In.material.color = color_Mouth;
			}
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
		if (Life_Timer > 4f)
		{
			Pink_Glow.color = global::UnityEngine.Color.Lerp(Pink_Glow.color, color_ON, global::UnityEngine.Time.deltaTime * 1f);
		}
		Eye_Check();
		if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.Backspace))
		{
			Delete_Cum();
		}
	}

	private void Eye_Check()
	{
		Eye_Timer -= global::UnityEngine.Time.deltaTime;
		if (Eye_State > 0)
		{
			Ani_Timer += global::UnityEngine.Time.deltaTime;
			if (Ani_Timer > 0.03f)
			{
				Eye_Num++;
				Ani_Timer = 0f;
				switch (Eye_Num)
				{
				case 1:
					EyeClose.sprite = Spr_List[1];
					break;
				case 2:
					EyeClose.sprite = Spr_List[2];
					break;
				case 3:
					EyeClose.sprite = Spr_List[3];
					break;
				case 4:
					EyeClose.sprite = Spr_List[4];
					if (Cum_Num == 2 && State > 0)
					{
						if (global::UnityEngine.Random.Range(0, 10) > 5)
						{
							Ani_Timer -= 0.5f;
						}
						else
						{
							Ani_Timer -= 0.05f;
						}
					}
					else if (global::UnityEngine.Random.Range(0, 10) > 6)
					{
						Ani_Timer -= 0.2f;
					}
					else
					{
						Ani_Timer -= 0.05f;
					}
					break;
				case 5:
					EyeClose.sprite = Spr_List[3];
					break;
				case 6:
					EyeClose.sprite = Spr_List[2];
					break;
				case 7:
					EyeClose.sprite = Spr_List[1];
					break;
				case 8:
				{
					Eye_State = 0;
					EyeClose.enabled = false;
					EyeOpen.enabled = true;
					global::UnityEngine.SpriteRenderer eyeHL_L = EyeHL_L;
					global::UnityEngine.Color color = color_ON;
					EyeHL_R.color = color;
					eyeHL_L.color = color;
					break;
				}
				}
			}
			if (Eye_Num < 5)
			{
				global::UnityEngine.SpriteRenderer eyeHL_L2 = EyeHL_L;
				global::UnityEngine.Color color = global::UnityEngine.Color.Lerp(EyeHL_R.color, color_OFF, global::UnityEngine.Time.deltaTime * 15f);
				EyeHL_R.color = color;
				eyeHL_L2.color = color;
			}
			else
			{
				global::UnityEngine.SpriteRenderer eyeHL_L3 = EyeHL_L;
				global::UnityEngine.Color color = global::UnityEngine.Color.Lerp(EyeHL_R.color, color_ON, global::UnityEngine.Time.deltaTime * 15f);
				EyeHL_R.color = color;
				eyeHL_L3.color = color;
			}
		}
		else if (Cum_Num < 2 && State == 0 && Eye_Timer < 0f)
		{
			Eye_State = 1;
			Eye_Num = 0;
			Eye_Timer = global::UnityEngine.Random.Range(1f, 5f);
			Ani_Timer = 0f;
			EyeClose.sprite = Spr_List[Eye_Num];
			EyeClose.enabled = true;
			EyeOpen.enabled = false;
		}
	}

	private void Eye_Close_Cum()
	{
		if (Eye_State == 0)
		{
			Eye_State = 1;
			Eye_Num = 0;
			Eye_Timer = global::UnityEngine.Random.Range(1f, 5f);
			Ani_Timer = 0f;
			EyeClose.sprite = Spr_List[Eye_Num];
			EyeClose.enabled = true;
			EyeOpen.enabled = false;
		}
	}

	private void Mouth_Open()
	{
		Mouth_Timer = global::UnityEngine.Random.Range(5f, 10f);
		if (color_Mouth.a < 1f)
		{
			color_Mouth = color_ON;
			Mouth.material.color = color_Mouth;
			Tooth.material.color = color_Mouth;
			Mouth_In.material.color = color_Mouth;
		}
	}

	private void Cum_Hold()
	{
		if (!(color_Cum_1.a > 0.01f) || !(Cum_Rnd_Timer <= 0f))
		{
			return;
		}
		if (State > 0 && Cum_Num == 2)
		{
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(CumShot[global::UnityEngine.Random.Range(1, 6)], pos_Penis_Center.position, pos_Penis_Center.rotation) as global::UnityEngine.GameObject;
			int num = global::UnityEngine.Random.Range(0, 10);
			if (num > 5)
			{
				gameObject.GetComponent<CumShot_1>().Set_SortingOrder(160, pos_Penis_Center, 0.7f);
			}
			else
			{
				gameObject.GetComponent<CumShot_1>().Set_SortingOrder(146, pos_Penis_Center, 0.7f);
			}
			num = global::UnityEngine.Random.Range(0, 10);
			if (num > 7)
			{
				global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(CumShot[global::UnityEngine.Random.Range(1, 6)], pos_Penis_Center.position, pos_Penis_Center.rotation) as global::UnityEngine.GameObject;
				gameObject2.GetComponent<CumShot_1>().Set_SortingOrder(160, pos_Penis_Center, 0.5f);
				gameObject2.transform.position = new global::UnityEngine.Vector3(gameObject2.transform.position.x + global::UnityEngine.Random.Range(0.3f, 0.6f), gameObject2.transform.position.y + global::UnityEngine.Random.Range(-0.3f, 0.3f), 0f);
			}
		}
		else if (Cum_Num == 2)
		{
			global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(CumShot[global::UnityEngine.Random.Range(1, 6)], pos_Penis_Center.position, pos_Penis_Center.rotation) as global::UnityEngine.GameObject;
			int num2 = global::UnityEngine.Random.Range(0, 10);
			if (num2 > 4)
			{
				gameObject3.GetComponent<CumShot_1>().Set_SortingOrder(160, pos_Penis_Center, 0.7f);
			}
			else
			{
				gameObject3.GetComponent<CumShot_1>().Set_SortingOrder(146, pos_Penis_Center, 0.7f);
			}
		}
		else if (global::UnityEngine.Random.Range(0, 10) > 5)
		{
			global::UnityEngine.GameObject gameObject4 = global::UnityEngine.Object.Instantiate(CumShot[global::UnityEngine.Random.Range(1, 6)], pos_Penis_Center.position, pos_Penis_Center.rotation) as global::UnityEngine.GameObject;
			gameObject4.GetComponent<CumShot_1>().Set_SortingOrder(146, pos_Penis_Center, 0.7f);
		}
		Cum_Rnd_Timer = 0.2f;
	}

	private void Cum_Penis_All()
	{
		if (Cum_Num < 1)
		{
			Cum_Num++;
			State = 0;
			GetComponent<global::UnityEngine.Animator>().SetInteger("State", 0);
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
		Cum_Penis_LT();
		Cum_Penis_RT();
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
			Set_Tentacle(0);
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
			Set_Tentacle(3);
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
				BG[i].transform.position = new global::UnityEngine.Vector3(0f, 1f, 0f);
			}
			else
			{
				BG[i].transform.position = new global::UnityEngine.Vector3(100f, -100f, 0f);
			}
		}
	}

	private void Set_Tentacle(int num)
	{
		for (int i = 0; i < 5; i++)
		{
			Tentacle[i].GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material = Mat_Tentacle[num];
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
