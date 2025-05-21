public class H11_Control : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.Material Mat_Tentacle_1;

	public global::UnityEngine.Material Mat_Tentacle_2;

	public global::UnityEngine.Material Mat_Tentacle_3;

	public global::UnityEngine.Material Mat_Tentacle_4;

	public global::UnityEngine.GameObject Tentacle_1;

	public global::UnityEngine.GameObject Tentacle_2;

	public global::UnityEngine.GameObject Tentacle_3;

	public global::UnityEngine.GameObject Tentacle_4;

	public global::UnityEngine.GameObject Tentacle_7;

	public global::UnityEngine.GameObject Tentacle_5;

	public global::UnityEngine.GameObject Tentacle_6;

	public global::UnityEngine.GameObject Tongue_5;

	public global::UnityEngine.GameObject Tongue_6;

	public global::UnityEngine.Transform pos_Penis_LT;

	public global::UnityEngine.Transform pos_Penis_LB;

	public global::UnityEngine.Transform pos_Penis_RT;

	public global::UnityEngine.Transform pos_Penis_C;

	public global::UnityEngine.Transform pos_Mouth;

	public global::UnityEngine.Transform pos_Penis_RT_Down;

	public global::UnityEngine.Transform pos_Penis_C_Down;

	public global::UnityEngine.GameObject[] CumShot;

	public global::UnityEngine.GameObject[] CumDot;

	public global::UnityEngine.GameObject[] CumShot_N;

	public global::UnityEngine.GameObject[] BG;

	public global::UnityEngine.GameObject CensoredBox;

	private float Life_Timer;

	private int State;

	private int Cum_Num;

	private bool isIdle = true;

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

	private global::UnityEngine.Color color_Cum_3 = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

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

	private float Cum_3_Timer;

	private float Cum_Rnd_Timer;

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
		Speed = 2f;
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
				global::UnityEngine.GameObject.Find("Main Camera").camera.orthographicSize = 4.5f;
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<GameOver_Camera>().targetSize = 7f;
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<GameOver_Camera>().MaxSize = 8.4f;
				global::UnityEngine.GameObject.Find("Main Camera").transform.position = new global::UnityEngine.Vector3(1.3f, 1.3f, -10f);
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<GameOver_Camera>().Set_TargetPos(new global::UnityEngine.Vector3(1.3f, 1.3f, -10f));
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
				global::UnityEngine.GameObject.Find("Main Camera").camera.orthographicSize = 4.5f;
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Gallery_Camera>().targetSize = 7f;
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Gallery_Camera>().MaxSize = 8.4f;
				global::UnityEngine.GameObject.Find("Main Camera").transform.position = new global::UnityEngine.Vector3(1.3f, 1.3f, -10f);
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Gallery_Camera>().Set_TargetPos(new global::UnityEngine.Vector3(1.3f, 1.3f, -10f));
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
			global::UnityEngine.GameObject.Find("Mosaic Camera").GetComponent<Gallery_Mosaic>().Set_Mosaic(1);
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
				Speed = global::UnityEngine.Mathf.Lerp(Speed, 5f, global::UnityEngine.Time.deltaTime * 2f);
				GetComponent<global::UnityEngine.Animator>().speed = Speed;
			}
			else if (CumCombo_Delay > 0f)
			{
				CumCombo_Delay -= global::UnityEngine.Time.deltaTime;
				Speed = global::UnityEngine.Mathf.Lerp(Speed, 3f, global::UnityEngine.Time.deltaTime * 1f);
				GetComponent<global::UnityEngine.Animator>().speed = Speed;
			}
			else if (Press_Timer > 0.1f)
			{
				Speed += global::UnityEngine.Time.deltaTime;
				GetComponent<global::UnityEngine.Animator>().speed = Speed;
				if (Speed >= 6f)
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
					else
					{
						Cum_3();
					}
				}
			}
			else
			{
				Speed = global::UnityEngine.Mathf.Lerp(Speed, 2f, global::UnityEngine.Time.deltaTime * 1f);
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
			if (Speed < 2.01f && Press_Timer > 0f)
			{
				Press_Timer = 0f;
			}
			if (State == 1)
			{
				State = 0;
				GetComponent<global::UnityEngine.Animator>().SetInteger("State", 0);
			}
			else if (State == 0 && isIdle)
			{
				if (Speed < 2.05f && Cum_Num > 0)
				{
					Cum_Num = 0;
				}
				Speed = global::UnityEngine.Mathf.Lerp(Speed, 2f, global::UnityEngine.Time.deltaTime * 8f);
				GetComponent<global::UnityEngine.Animator>().speed = Speed;
			}
			Cum_BG.color = global::UnityEngine.Color.Lerp(Cum_BG.color, color_BG_OFF, global::UnityEngine.Time.deltaTime * 5f);
			Cum_Bar.color = global::UnityEngine.Color.Lerp(Cum_Bar.color, color_BG_OFF, global::UnityEngine.Time.deltaTime * 5f);
			Cum_Bar_After.color = global::UnityEngine.Color.Lerp(Cum_Bar_After.color, color_Blue_OFF, global::UnityEngine.Time.deltaTime * 5f);
			Cum_Cursor.color = global::UnityEngine.Color.Lerp(Cum_Cursor.color, color_OFF, global::UnityEngine.Time.deltaTime * 5f);
			Cum_Cursor_Glow.color = global::UnityEngine.Color.Lerp(Cum_Cursor_Glow.color, color_OFF, global::UnityEngine.Time.deltaTime * 5f);
		}
		if (Cum_Bar != null)
		{
			float num = ((State != 0) ? 1f : ((Speed - 2f) / 4f));
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
		if (Cum_3_Timer > 0f)
		{
			Cum_3_Timer -= global::UnityEngine.Time.deltaTime;
			color_Cum_3 = global::UnityEngine.Color.Lerp(color_Cum_3, color_ON, global::UnityEngine.Time.deltaTime * 2f);
		}
		else
		{
			color_Cum_3 = global::UnityEngine.Color.Lerp(color_Cum_3, color_OFF, global::UnityEngine.Time.deltaTime * 1f);
		}
		Set_Cum_Opacity();
		if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.Backspace))
		{
			Delete_Cum();
		}
	}

	private void NowIdle()
	{
		isIdle = true;
	}

	private void NowCum()
	{
		isIdle = false;
	}

	private void EndCum()
	{
		if (Cum_Num < 2)
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
	}

	private void Cum_Random()
	{
		if (H_Sound != null && Cum_Rnd_Timer <= 0f)
		{
			if (global::UnityEngine.Random.Range(0, 10) > 5)
			{
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(CumShot[global::UnityEngine.Random.Range(1, 6)], pos_Penis_LT.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 90f)) as global::UnityEngine.GameObject;
				gameObject.GetComponent<CumShot_1>().Set_SortingOrder(99, pos_Penis_LT, 0.5f);
			}
			if (global::UnityEngine.Random.Range(0, 10) > 6)
			{
				global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(CumShot[global::UnityEngine.Random.Range(1, 6)], pos_Penis_LB.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 90f)) as global::UnityEngine.GameObject;
				gameObject2.GetComponent<CumShot_1>().Set_SortingOrder(99, pos_Penis_LB, 0.5f);
			}
			if (Cum_1_Timer > 0f && global::UnityEngine.Random.Range(0, 10) > 6)
			{
				global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(CumShot[global::UnityEngine.Random.Range(2, 6)], pos_Mouth.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 90f)) as global::UnityEngine.GameObject;
				gameObject3.GetComponent<CumShot_1>().Set_SortingOrder(118, pos_Mouth, 0.8f);
			}
			Cum_Rnd_Timer = 0.1f;
		}
	}

	private void Cum_End_Direct()
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(CumShot[global::UnityEngine.Random.Range(1, 6)], pos_Penis_LT.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 90f)) as global::UnityEngine.GameObject;
		gameObject.GetComponent<CumShot_1>().Set_SortingOrder(99, pos_Penis_LT, 0.5f);
		global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(CumShot[global::UnityEngine.Random.Range(1, 6)], pos_Penis_LB.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 90f)) as global::UnityEngine.GameObject;
		gameObject2.GetComponent<CumShot_1>().Set_SortingOrder(99, pos_Penis_LB, 0.5f);
		global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(CumShot[global::UnityEngine.Random.Range(1, 6)], pos_Penis_RT_Down.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 90f)) as global::UnityEngine.GameObject;
		gameObject3.SendMessage("Slow_Down");
		global::UnityEngine.GameObject gameObject4 = global::UnityEngine.Object.Instantiate(CumShot[global::UnityEngine.Random.Range(1, 6)], pos_Penis_C_Down.position, pos_Penis_C.rotation) as global::UnityEngine.GameObject;
		gameObject4.SendMessage("Slow_Down");
	}

	private void Cum_End_Direct_2()
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(CumShot[global::UnityEngine.Random.Range(1, 6)], pos_Penis_LT.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 90f)) as global::UnityEngine.GameObject;
		gameObject.GetComponent<CumShot_1>().Set_SortingOrder(99, pos_Penis_LT, 0.5f);
		global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(CumShot[global::UnityEngine.Random.Range(1, 6)], pos_Penis_RT_Down.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 90f)) as global::UnityEngine.GameObject;
		gameObject2.SendMessage("Slow_Down");
	}

	private void Cum_End_Long_C()
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(CumShot[global::UnityEngine.Random.Range(1, 6)], pos_Penis_C.position, pos_Penis_C.rotation) as global::UnityEngine.GameObject;
		gameObject.SendMessage("Start_Long");
		global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(CumDot[0], pos_Penis_C.position, pos_Penis_C.rotation) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(CumDot[1], pos_Penis_C.position, pos_Penis_C.rotation) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject4 = global::UnityEngine.Object.Instantiate(CumDot[2], pos_Penis_C.position, pos_Penis_C.rotation) as global::UnityEngine.GameObject;
	}

	private void Cum_End_Long_RT()
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(CumShot[global::UnityEngine.Random.Range(1, 6)], pos_Penis_RT.position, pos_Penis_RT.rotation) as global::UnityEngine.GameObject;
		gameObject.SendMessage("Start_Long");
		global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(CumDot[0], pos_Penis_RT.position, pos_Penis_RT.rotation) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(CumDot[1], pos_Penis_RT.position, pos_Penis_RT.rotation) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject4 = global::UnityEngine.Object.Instantiate(CumDot[2], pos_Penis_RT.position, pos_Penis_RT.rotation) as global::UnityEngine.GameObject;
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
			Set_Tentacle(Mat_Tentacle_1);
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
			Set_Tentacle(Mat_Tentacle_2);
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
			Set_Tentacle(Mat_Tentacle_3);
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
			Set_Tentacle(Mat_Tentacle_4);
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

	private void Set_Tentacle(global::UnityEngine.Material Mat_Tentacle)
	{
		Tentacle_1.GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material = Mat_Tentacle;
		Tentacle_2.GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material = Mat_Tentacle;
		Tentacle_3.GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material = Mat_Tentacle;
		Tentacle_4.GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material = Mat_Tentacle;
		Tentacle_7.GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material = Mat_Tentacle;
		Tentacle_5.GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material = Mat_Tentacle;
		Tentacle_6.GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material = Mat_Tentacle;
		Tongue_5.GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material = Mat_Tentacle;
		Tongue_6.GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material = Mat_Tentacle;
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

	private void Cum_3()
	{
		Cum_1_Timer = 44f;
		Cum_2_Timer = 42f;
		Cum_3_Timer = 40f;
	}

	private void Delete_Cum()
	{
		Cum_1_Timer = (Cum_2_Timer = (Cum_3_Timer = 0f));
	}

	private void Reset_Cum()
	{
		global::UnityEngine.GameObject.Find("Cum_Face_Temp_1").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_OFF;
		global::UnityEngine.GameObject.Find("Cum_Face_Temp_2").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_OFF;
		global::UnityEngine.GameObject.Find("Cum_Face_1").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_OFF;
		global::UnityEngine.GameObject.Find("Cum_Face_2").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_OFF;
		global::UnityEngine.GameObject.Find("GO_1_Cum_R3").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_OFF;
		global::UnityEngine.GameObject.Find("GO_1_Cum_Mouth_GEO").GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material.color = color_OFF;
		global::UnityEngine.GameObject.Find("GO_1_Cum_TC_GEO").GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material.color = color_OFF;
		global::UnityEngine.GameObject.Find("GO_1_Cum_TL_GEO").GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material.color = color_OFF;
		global::UnityEngine.GameObject.Find("GO_1_Cum_TR_GEO").GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material.color = color_OFF;
		global::UnityEngine.GameObject.Find("GO_1_Cum_R1_GEO").GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material.color = color_OFF;
		global::UnityEngine.GameObject.Find("GO_1_Cum_R2_GEO").GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material.color = color_OFF;
		global::UnityEngine.GameObject.Find("GO_1_Cum_RR_GEO").GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material.color = color_OFF;
		global::UnityEngine.GameObject.Find("Cum_Arm_1").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_OFF;
		global::UnityEngine.GameObject.Find("Cum_Arm_2").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_OFF;
		global::UnityEngine.GameObject.Find("Cum_Arm_3").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_OFF;
		global::UnityEngine.GameObject.Find("Cum_Arm_4").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_OFF;
		global::UnityEngine.GameObject.Find("Cum_Arm_5").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_OFF;
		global::UnityEngine.GameObject.Find("Cum_Arm_6").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_OFF;
		global::UnityEngine.GameObject.Find("Cum_Arm_7").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_OFF;
		global::UnityEngine.GameObject.Find("Cum_Breast_1").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_OFF;
		global::UnityEngine.GameObject.Find("Cum_Breast_2").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_OFF;
		global::UnityEngine.GameObject.Find("Cum_Breast_3").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_OFF;
		global::UnityEngine.GameObject.Find("Cum_Spine_1").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_OFF;
		global::UnityEngine.GameObject.Find("Cum_Spine_2").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_OFF;
		global::UnityEngine.GameObject.Find("Cum_Spine_3").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_OFF;
		global::UnityEngine.GameObject.Find("Cum_Spine_B").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_OFF;
		global::UnityEngine.GameObject.Find("Cum_Spine_4").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_OFF;
		global::UnityEngine.GameObject.Find("Cum_Spine_B2").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_OFF;
		global::UnityEngine.GameObject.Find("Cum_Pelvis").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_OFF;
		global::UnityEngine.GameObject.Find("Cum_Pelvis_2").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_OFF;
		global::UnityEngine.GameObject.Find("Cum_Pelvis_3").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_OFF;
		global::UnityEngine.GameObject.Find("Cum_Leg_1").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_OFF;
		global::UnityEngine.GameObject.Find("Cum_Leg_2").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_OFF;
		global::UnityEngine.GameObject.Find("Cum_Leg_3").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_OFF;
		global::UnityEngine.GameObject.Find("Cum_Leg_4").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_OFF;
		global::UnityEngine.GameObject.Find("Cum_Leg_5").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_OFF;
		global::UnityEngine.GameObject.Find("Cum_Leg_6").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_OFF;
		global::UnityEngine.GameObject.Find("Cum_Leg_7").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_OFF;
	}

	private void Set_Cum_Opacity()
	{
		global::UnityEngine.GameObject.Find("Cum_Face_Temp_1").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Cum_1;
		global::UnityEngine.GameObject.Find("Cum_Face_Temp_2").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Cum_1;
		global::UnityEngine.GameObject.Find("GO_1_Cum_Mouth_GEO").GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material.color = color_Cum_1;
		global::UnityEngine.GameObject.Find("Cum_Arm_1").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Cum_1;
		global::UnityEngine.GameObject.Find("Cum_Spine_1").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Cum_1;
		global::UnityEngine.GameObject.Find("Cum_Spine_3").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Cum_1;
		global::UnityEngine.GameObject.Find("Cum_Leg_1").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Cum_1;
		global::UnityEngine.GameObject.Find("Cum_Leg_5").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Cum_1;
		global::UnityEngine.GameObject.Find("Cum_Face_1").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Cum_2;
		global::UnityEngine.GameObject.Find("GO_1_Cum_TC_GEO").GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material.color = color_Cum_2;
		global::UnityEngine.GameObject.Find("GO_1_Cum_R1_GEO").GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material.color = color_Cum_2;
		global::UnityEngine.GameObject.Find("GO_1_Cum_R2_GEO").GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material.color = color_Cum_2;
		global::UnityEngine.GameObject.Find("Cum_Arm_2").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Cum_2;
		global::UnityEngine.GameObject.Find("Cum_Arm_4").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Cum_2;
		global::UnityEngine.GameObject.Find("Cum_Breast_1").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Cum_2;
		global::UnityEngine.GameObject.Find("Cum_Spine_2").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Cum_2;
		global::UnityEngine.GameObject.Find("Cum_Spine_B").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Cum_2;
		global::UnityEngine.GameObject.Find("Cum_Pelvis").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Cum_2;
		global::UnityEngine.GameObject.Find("Cum_Leg_2").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Cum_2;
		global::UnityEngine.GameObject.Find("Cum_Leg_3").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Cum_2;
		global::UnityEngine.GameObject.Find("Cum_Leg_4").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Cum_2;
		global::UnityEngine.GameObject.Find("Cum_Face_2").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Cum_3;
		global::UnityEngine.GameObject.Find("GO_1_Cum_R3").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Cum_3;
		global::UnityEngine.GameObject.Find("GO_1_Cum_TL_GEO").GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material.color = color_Cum_3;
		global::UnityEngine.GameObject.Find("GO_1_Cum_TR_GEO").GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material.color = color_Cum_3;
		global::UnityEngine.GameObject.Find("GO_1_Cum_RR_GEO").GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material.color = color_Cum_3;
		global::UnityEngine.GameObject.Find("Cum_Arm_3").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Cum_3;
		global::UnityEngine.GameObject.Find("Cum_Breast_2").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Cum_3;
		global::UnityEngine.GameObject.Find("Cum_Breast_3").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Cum_3;
		global::UnityEngine.GameObject.Find("Cum_Arm_5").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Cum_3;
		global::UnityEngine.GameObject.Find("Cum_Arm_6").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Cum_3;
		global::UnityEngine.GameObject.Find("Cum_Arm_7").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Cum_3;
		global::UnityEngine.GameObject.Find("Cum_Spine_4").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Cum_3;
		global::UnityEngine.GameObject.Find("Cum_Spine_B2").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Cum_3;
		global::UnityEngine.GameObject.Find("Cum_Pelvis_2").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Cum_3;
		global::UnityEngine.GameObject.Find("Cum_Pelvis_3").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Cum_3;
		global::UnityEngine.GameObject.Find("Cum_Leg_6").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Cum_3;
		global::UnityEngine.GameObject.Find("Cum_Leg_7").GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Cum_3;
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
		if (H_Sound != null)
		{
			H_Sound.SendMessage("Sound_Piston_4");
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
			Moan_End_Timer = 1.5f;
		}
		else if (Moan_Timer <= 0f)
		{
			int num2 = global::UnityEngine.Random.Range(7, 13);
			if (Moan_Num != num2)
			{
				Moan_Num = num2;
				H_Sound.Sound_Moan(num2, 0);
				Moan_Timer = 1.2f;
			}
		}
	}
}
