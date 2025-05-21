public class H_Slot : global::UnityEngine.MonoBehaviour
{
	public int Slot_Num;

	public H_Manager H_manager;

	public global::UnityEngine.GameObject H_Object;

	public global::UnityEngine.UI.Image Icon_Play;

	public global::UnityEngine.UI.Image Icon_Stop;

	public global::UnityEngine.UI.Image Icon_Loop;

	public global::UnityEngine.UI.Image Icon_Loop_Dir;

	public global::UnityEngine.RectTransform Rot_Loop;

	public global::UnityEngine.RectTransform SelBorder;

	public global::UnityEngine.UI.Text[] Txt_State;

	public global::UnityEngine.RectTransform[] Box_State;

	public global::UnityEngine.UI.Text Txt_Speed;

	public global::UnityEngine.UI.Image Speed_Bar;

	public global::UnityEngine.RectTransform Cum_Box;

	public global::UnityEngine.UI.Image Cum_Box_Tail;

	public global::UnityEngine.UI.Text Txt_Cum;

	public global::UnityEngine.GameObject H_Dummy;

	private int isOpen;

	private int H_Num;

	private int isPlaying;

	private int isLoop;

	private int State;

	private int StateMax = 3;

	private float[] Speed = new float[4] { 0.5f, 0.5f, 0.5f, 0.5f };

	private int isFlip;

	private float Playing_Timer;

	private float After_Timer;

	private float PlayStop_Timer;

	private float Cum_Timer;

	private float Off_PlayButton_Timer;

	private bool onCumBox;

	private float CumBox_Timer;

	private global::UnityEngine.Vector3 Scale_1 = new global::UnityEngine.Vector3(1f, 1f, 1f);

	private global::UnityEngine.Color color_CumBox_ON = new global::UnityEngine.Color(1f, 0f, 0f, 1f);

	private global::UnityEngine.Color color_CumBox_OFF = new global::UnityEngine.Color(1f, 0f, 0f, 0f);

	private global::UnityEngine.Color color_CumText_ON = new global::UnityEngine.Color(1f, 1f, 0.67f, 1f);

	private global::UnityEngine.Color color_CumText_OFF = new global::UnityEngine.Color(1f, 1f, 0.67f, 0f);

	private global::UnityEngine.Color color_ON = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_OFF = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	private Gallery_Control GC;

	private void Start()
	{
		GC = global::UnityEngine.GameObject.Find("Gallery_Menu").GetComponent<Gallery_Control>();
		Start_Gallery();
	}

	public void Start_Gallery()
	{
		if (global::UnityEngine.PlayerPrefs.GetInt("G_Slot_isOpen_" + Slot_Num) == 1 && global::UnityEngine.PlayerPrefs.GetInt("G_Slot_H_Num_" + Slot_Num) > 0)
		{
			isOpen = 1;
			GC.Slot_isOpen[Slot_Num - 1] = 1;
			H_Num = global::UnityEngine.PlayerPrefs.GetInt("G_Slot_H_Num_" + Slot_Num);
			isPlaying = 1;
			isLoop = global::UnityEngine.PlayerPrefs.GetInt("G_Slot_isLoop_" + Slot_Num);
			State = global::UnityEngine.PlayerPrefs.GetInt("G_Slot_State_" + Slot_Num);
			StateMax = global::UnityEngine.PlayerPrefs.GetInt("G_Slot_StateMax_" + Slot_Num);
			Speed[0] = global::UnityEngine.PlayerPrefs.GetFloat("G_Slot_Speed_" + Slot_Num + "_0");
			Speed[1] = global::UnityEngine.PlayerPrefs.GetFloat("G_Slot_Speed_" + Slot_Num + "_1");
			Speed[2] = global::UnityEngine.PlayerPrefs.GetFloat("G_Slot_Speed_" + Slot_Num + "_2");
			Speed[3] = global::UnityEngine.PlayerPrefs.GetFloat("G_Slot_Speed_" + Slot_Num + "_3");
			isFlip = global::UnityEngine.PlayerPrefs.GetInt("G_Slot_isFlip_" + Slot_Num);
			Icon_Play.enabled = false;
			Icon_Stop.enabled = true;
			if (isLoop > 0)
			{
				Icon_Loop.enabled = true;
				Icon_Loop_Dir.enabled = true;
			}
			else
			{
				Icon_Loop.enabled = false;
				Icon_Loop_Dir.enabled = false;
			}
			SelBorder.position = Box_State[State].position;
			if (StateMax < 2)
			{
				Box_State[2].GetComponent<global::UnityEngine.UI.Image>().enabled = false;
				Txt_State[2].enabled = false;
			}
			if (StateMax < 3)
			{
				Box_State[3].GetComponent<global::UnityEngine.UI.Image>().enabled = false;
				Txt_State[3].enabled = false;
			}
			for (int i = 0; i < 4; i++)
			{
				if (i == State)
				{
					Txt_State[i].color = color_ON;
				}
				else
				{
					Txt_State[i].color = new global::UnityEngine.Color(0.557f, 0.33f, 0.082f);
				}
			}
			H_Object = H_manager.Make_H(Slot_Num, H_Num - 1, isFlip);
			if (isFlip > 0)
			{
				H_Object.SendMessage("Flip");
			}
			H_Object.GetComponent<H_Ani>().Slot = GetComponent<H_Slot>();
			H_Object.GetComponent<H_Ani>().isGallery = true;
			if (State > 0)
			{
				H_Object.GetComponent<H_Ani>().Play(State, Speed[State] * 2f);
			}
			if (isLoop > 0)
			{
				H_Object.GetComponent<H_Ani>().isLoop = true;
			}
			Set_Speed(Speed[State]);
			if (H_Num < 50 && H_Num != 13)
			{
				for (int j = 0; j < 5; j++)
				{
					Box_State[j].GetComponent<global::UnityEngine.BoxCollider2D>().enabled = true;
					if (j < 2)
					{
						Txt_State[j].enabled = true;
					}
				}
				SelBorder.GetComponent<global::UnityEngine.UI.Image>().enabled = true;
			}
			else
			{
				for (int k = 0; k < 5; k++)
				{
					Box_State[k].GetComponent<global::UnityEngine.BoxCollider2D>().enabled = false;
					if (k < 4)
					{
						Txt_State[k].enabled = false;
					}
				}
				Icon_Play.enabled = false;
				Icon_Stop.enabled = false;
				Icon_Loop.enabled = false;
				Icon_Loop_Dir.enabled = false;
				SelBorder.GetComponent<global::UnityEngine.UI.Image>().enabled = false;
			}
		}
		CumBoxHide();
	}

	public void Load_Ani(int num)
	{
		if (H_Object != null)
		{
			H_Object.SendMessage("Delete_ToDual");
			H_Object = null;
		}
		int h_Num = H_Num;
		int num2 = isFlip;
		isOpen = 1;
		H_Num = num;
		isPlaying = 1;
		isLoop = 0;
		State = 0;
		Speed = new float[4] { 0.5f, 0.5f, 0.5f, 0.5f };
		isFlip = 0;
		switch (H_Num)
		{
		case 1:
			StateMax = 2;
			break;
		case 2:
			StateMax = 1;
			break;
		case 3:
			StateMax = 3;
			break;
		case 4:
			StateMax = 1;
			break;
		case 5:
			StateMax = 3;
			break;
		case 6:
			StateMax = 3;
			break;
		case 7:
			StateMax = 2;
			break;
		case 8:
			StateMax = 2;
			break;
		case 9:
			StateMax = 1;
			break;
		case 10:
			StateMax = 2;
			break;
		case 11:
			StateMax = 3;
			break;
		case 12:
			StateMax = 2;
			break;
		case 13:
			StateMax = 1;
			break;
		case 14:
			StateMax = 2;
			break;
		case 15:
			StateMax = 2;
			break;
		case 16:
			StateMax = 3;
			break;
		case 17:
			StateMax = 1;
			break;
		case 18:
			StateMax = 2;
			break;
		case 19:
			StateMax = 3;
			break;
		case 20:
			StateMax = 2;
			break;
		case 21:
			StateMax = 3;
			break;
		case 22:
			StateMax = 3;
			break;
		case 23:
			StateMax = 3;
			break;
		case 24:
			StateMax = 2;
			break;
		case 25:
			StateMax = 2;
			break;
		case 26:
			StateMax = 2;
			break;
		case 27:
			StateMax = 3;
			break;
		case 28:
			StateMax = 2;
			break;
		case 29:
			StateMax = 3;
			break;
		case 30:
			StateMax = 3;
			break;
		case 31:
			StateMax = 2;
			break;
		case 32:
			StateMax = 2;
			break;
		case 33:
			StateMax = 2;
			break;
		case 34:
			StateMax = 2;
			break;
		case 35:
			StateMax = 3;
			break;
		case 36:
			StateMax = 2;
			break;
		case 37:
			StateMax = 2;
			break;
		case 38:
			StateMax = 2;
			break;
		case 39:
			StateMax = 2;
			break;
		case 40:
			StateMax = 2;
			break;
		case 41:
			StateMax = 3;
			break;
		case 42:
			StateMax = 3;
			break;
		case 43:
			StateMax = 3;
			break;
		default:
			StateMax = 1;
			break;
		}
		global::UnityEngine.PlayerPrefs.SetInt("G_Slot_isOpen_" + Slot_Num, 1);
		global::UnityEngine.PlayerPrefs.SetInt("G_Slot_H_Num_" + Slot_Num, num);
		global::UnityEngine.PlayerPrefs.SetInt("G_Slot_isLoop_" + Slot_Num, 0);
		global::UnityEngine.PlayerPrefs.SetInt("G_Slot_State_" + Slot_Num, 0);
		global::UnityEngine.PlayerPrefs.SetInt("G_Slot_StateMax_" + Slot_Num, StateMax);
		global::UnityEngine.PlayerPrefs.SetFloat("G_Slot_Speed_" + Slot_Num + "_0", 0.5f);
		global::UnityEngine.PlayerPrefs.SetFloat("G_Slot_Speed_" + Slot_Num + "_1", 0.5f);
		global::UnityEngine.PlayerPrefs.SetFloat("G_Slot_Speed_" + Slot_Num + "_2", 0.5f);
		global::UnityEngine.PlayerPrefs.SetFloat("G_Slot_Speed_" + Slot_Num + "_3", 0.5f);
		if (h_Num > 0 && h_Num == H_Num)
		{
			if (num2 == 0)
			{
				isFlip = 1;
			}
		}
		else
		{
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			for (int i = 0; i < 5; i++)
			{
				if (Slot_Num != i + 1)
				{
					num3 = GC.Slot[i].Check_HnumFlip(H_Num);
					if (num3 > 0)
					{
						num4++;
					}
					else if (num3 < 0)
					{
						num5++;
					}
				}
			}
			if (num5 > 0 && num4 > 0)
			{
				isFlip = ((global::UnityEngine.Random.Range(0, 10) > 5) ? 1 : 0);
			}
			else if (num4 > 0)
			{
				isFlip = 1;
			}
		}
		global::UnityEngine.PlayerPrefs.SetInt("G_Slot_isFlip_" + Slot_Num, isFlip);
		Icon_Play.enabled = false;
		Icon_Stop.enabled = true;
		Icon_Loop.enabled = false;
		Icon_Loop_Dir.enabled = false;
		SelBorder.position = Box_State[0].position;
		for (int j = 2; j < 4; j++)
		{
			if (StateMax >= j)
			{
				Box_State[j].GetComponent<global::UnityEngine.UI.Image>().enabled = true;
				Txt_State[j].enabled = true;
			}
			else
			{
				Box_State[j].GetComponent<global::UnityEngine.UI.Image>().enabled = false;
				Txt_State[j].enabled = false;
			}
		}
		Txt_State[0].color = color_ON;
		global::UnityEngine.UI.Text obj = Txt_State[1];
		global::UnityEngine.Color color = new global::UnityEngine.Color(0.557f, 0.33f, 0.082f);
		Txt_State[3].color = color;
		color = color;
		Txt_State[2].color = color;
		obj.color = color;
		H_Object = H_manager.Make_H(Slot_Num, H_Num - 1, isFlip);
		if (isFlip > 0)
		{
			H_Object.SendMessage("Flip");
		}
		H_Object.GetComponent<H_Ani>().Slot = GetComponent<H_Slot>();
		H_Object.GetComponent<H_Ani>().isGallery = true;
		Set_Speed(Speed[State]);
		CumBoxHide();
		Playing_Timer = 0f;
		After_Timer = 0f;
		onCumBox = false;
		CumBoxHide();
		if (H_Num < 50 && H_Num != 13)
		{
			for (int k = 0; k < 5; k++)
			{
				Box_State[k].GetComponent<global::UnityEngine.BoxCollider2D>().enabled = true;
				if (k < 2)
				{
					Txt_State[k].enabled = true;
				}
			}
			SelBorder.GetComponent<global::UnityEngine.UI.Image>().enabled = true;
			return;
		}
		for (int l = 0; l < 5; l++)
		{
			Box_State[l].GetComponent<global::UnityEngine.BoxCollider2D>().enabled = false;
			if (l < 4)
			{
				Txt_State[l].enabled = false;
			}
		}
		Icon_Play.enabled = false;
		Icon_Stop.enabled = false;
		Icon_Loop.enabled = false;
		Icon_Loop_Dir.enabled = false;
		SelBorder.GetComponent<global::UnityEngine.UI.Image>().enabled = false;
	}

	public void Delete_Ani()
	{
		isOpen = 0;
		H_Num = 0;
		isPlaying = 0;
		isLoop = 0;
		State = 0;
		StateMax = 3;
		Speed = new float[4] { 0.5f, 0.5f, 0.5f, 0.5f };
		isFlip = 0;
		global::UnityEngine.PlayerPrefs.SetInt("G_Slot_isOpen_" + Slot_Num, 0);
		global::UnityEngine.PlayerPrefs.SetInt("G_Slot_H_Num_" + Slot_Num, 0);
		global::UnityEngine.PlayerPrefs.SetInt("G_Slot_isLoop_" + Slot_Num, 0);
		global::UnityEngine.PlayerPrefs.SetInt("G_Slot_State_" + Slot_Num, 0);
		global::UnityEngine.PlayerPrefs.SetInt("G_Slot_StateMax_" + Slot_Num, 0);
		global::UnityEngine.PlayerPrefs.SetFloat("G_Slot_Speed_" + Slot_Num + "_0", 0.5f);
		global::UnityEngine.PlayerPrefs.SetFloat("G_Slot_Speed_" + Slot_Num + "_1", 0.5f);
		global::UnityEngine.PlayerPrefs.SetFloat("G_Slot_Speed_" + Slot_Num + "_2", 0.5f);
		global::UnityEngine.PlayerPrefs.SetFloat("G_Slot_Speed_" + Slot_Num + "_3", 0.5f);
		global::UnityEngine.PlayerPrefs.SetInt("G_Slot_isFlip_" + Slot_Num, 0);
		if (H_Object != null)
		{
			H_Object.SendMessage("Delete_ToDual");
			H_Object = null;
		}
		Playing_Timer = 0f;
		After_Timer = 0f;
		onCumBox = false;
		CumBoxHide();
	}

	public void Hide_Ani()
	{
		isOpen = 0;
		H_Num = 0;
		isPlaying = 0;
		isLoop = 0;
		State = 0;
		StateMax = 3;
		Speed = new float[4] { 0.5f, 0.5f, 0.5f, 0.5f };
		isFlip = 0;
		if (H_Object != null)
		{
			H_Object.SendMessage("Delete_ToDual");
			H_Object = null;
		}
		Playing_Timer = 0f;
		After_Timer = 0f;
		onCumBox = false;
		CumBoxHide();
	}

	private void Update()
	{
		if (PlayStop_Timer > 0f)
		{
			PlayStop_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Cum_Timer > 0f)
		{
			Cum_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Off_PlayButton_Timer > 0f)
		{
			Off_PlayButton_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (isLoop > 0)
		{
			Rot_Loop.Rotate(0f, 0f, global::UnityEngine.Time.deltaTime * 100f);
		}
		if (isPlaying > 0)
		{
			Playing_Timer += global::UnityEngine.Time.deltaTime;
			After_Timer = 0f;
			if (Playing_Timer > 5f && !onCumBox)
			{
				onCumBox = true;
			}
		}
		else
		{
			Playing_Timer = 0f;
			After_Timer += global::UnityEngine.Time.deltaTime;
			if (After_Timer > 3f && onCumBox)
			{
				onCumBox = false;
			}
		}
		if (onCumBox)
		{
			if (Cum_Timer <= 0.5f)
			{
				Txt_Cum.color = global::UnityEngine.Color.Lerp(Txt_Cum.color, color_CumText_ON, global::UnityEngine.Time.deltaTime * 2f);
				Cum_Box_Tail.color = global::UnityEngine.Color.Lerp(Cum_Box_Tail.color, color_CumBox_ON, global::UnityEngine.Time.deltaTime * 2f);
				Cum_Box.GetComponent<global::UnityEngine.UI.Image>().color = Cum_Box_Tail.color;
				Cum_Box.localScale = global::UnityEngine.Vector3.Lerp(Cum_Box.localScale, Scale_1, global::UnityEngine.Time.deltaTime * 12f);
				if (!Cum_Box.GetComponent<global::UnityEngine.BoxCollider2D>().enabled)
				{
					Cum_Box.GetComponent<global::UnityEngine.BoxCollider2D>().enabled = true;
				}
			}
		}
		else
		{
			Txt_Cum.color = global::UnityEngine.Color.Lerp(Txt_Cum.color, color_CumText_OFF, global::UnityEngine.Time.deltaTime * 12f);
			Cum_Box_Tail.color = global::UnityEngine.Color.Lerp(Cum_Box_Tail.color, color_CumBox_OFF, global::UnityEngine.Time.deltaTime * 12f);
			Cum_Box.GetComponent<global::UnityEngine.UI.Image>().color = Cum_Box_Tail.color;
			Cum_Box.localScale = global::UnityEngine.Vector3.Lerp(Cum_Box.localScale, new global::UnityEngine.Vector3(0.2f, 0.2f, 1f), global::UnityEngine.Time.deltaTime * 12f);
		}
	}

	private void CumBoxHide()
	{
		Cum_Box.GetComponent<global::UnityEngine.BoxCollider2D>().enabled = false;
		Txt_Cum.color = color_CumText_OFF;
		Cum_Box_Tail.color = color_CumBox_OFF;
		Cum_Box.GetComponent<global::UnityEngine.UI.Image>().color = color_CumBox_OFF;
		Cum_Box.localScale = new global::UnityEngine.Vector3(0.5f, 0.5f, 1f);
	}

	public void Set_Loop()
	{
		global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_Tab");
		if (isLoop == 0)
		{
			isLoop = 1;
			Icon_Loop.enabled = true;
			Icon_Loop_Dir.enabled = true;
			if (H_Object != null)
			{
				H_Object.GetComponent<H_Ani>().isLoop = true;
			}
			global::UnityEngine.PlayerPrefs.SetInt("G_Slot_isLoop_" + Slot_Num, 1);
		}
		else
		{
			isLoop = 0;
			Icon_Loop.enabled = false;
			Icon_Loop_Dir.enabled = false;
			if (H_Object != null)
			{
				H_Object.GetComponent<H_Ani>().isLoop = false;
			}
			global::UnityEngine.PlayerPrefs.SetInt("G_Slot_isLoop_" + Slot_Num, 0);
		}
	}

	public void Set_Play()
	{
		if (!(PlayStop_Timer <= 0f))
		{
			return;
		}
		global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_Tab");
		Off_PlayButton_Timer = 0.5f;
		if (isPlaying > 0)
		{
			isPlaying = 0;
			PlayStop_Timer = 0.3f;
			Icon_Play.enabled = true;
			Icon_Stop.enabled = false;
			if (H_Object != null)
			{
				H_Object.SendMessage("Stop");
			}
			return;
		}
		isPlaying = 1;
		PlayStop_Timer = 0.5f;
		Icon_Play.enabled = false;
		Icon_Stop.enabled = true;
		if (H_Object != null)
		{
			H_Object.GetComponent<H_Ani>().Play(State, Speed[State] * 2f);
			if (isLoop > 0)
			{
				H_Object.SendMessage("H_Loop");
			}
		}
	}

	public void Set_Cum()
	{
		if (Cum_Timer <= 0f)
		{
			Cum_Timer = 2f;
			if (H_Object != null)
			{
				H_Object.SendMessage("CumShot");
			}
			CumBoxHide();
		}
	}

	private void Off_PlayButton()
	{
		if (State == 0 && Off_PlayButton_Timer <= 0f && isPlaying > 0 && isLoop == 0)
		{
			isPlaying = 0;
			Icon_Play.enabled = true;
			Icon_Stop.enabled = false;
		}
	}

	public void Set_State(int num)
	{
		if (num > StateMax || num == State)
		{
			return;
		}
		Off_PlayButton_Timer = 0.5f;
		if (isPlaying == 0)
		{
			isPlaying = 1;
			Icon_Play.enabled = false;
			Icon_Stop.enabled = true;
		}
		State = num;
		SelBorder.position = Box_State[num].position;
		for (int i = 0; i < 4; i++)
		{
			if (i == num)
			{
				Txt_State[i].color = color_ON;
			}
			else
			{
				Txt_State[i].color = new global::UnityEngine.Color(0.557f, 0.33f, 0.082f);
			}
		}
		global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_Tab");
		global::UnityEngine.PlayerPrefs.SetInt("G_Slot_State_" + Slot_Num, num);
		if (H_Object != null)
		{
			H_Object.GetComponent<H_Ani>().Play(State, Speed[State] * 2f);
		}
		Set_Speed(Speed[State]);
	}

	public void Set_Speed(float ratio)
	{
		if (ratio < 0f)
		{
			ratio = 0f;
		}
		else if (ratio > 1f)
		{
			ratio = 1f;
		}
		Speed_Bar.fillAmount = ratio;
		Txt_Speed.text = "Speed : " + (ratio * 2f).ToString("f2");
		Speed[State] = ratio;
		if (H_Object != null)
		{
			H_Object.GetComponent<H_Ani>().Speed = ratio * 2f;
		}
	}

	public void Save_Speed()
	{
		global::UnityEngine.PlayerPrefs.SetFloat("G_Slot_Speed_" + Slot_Num + "_" + State, Speed[State]);
	}

	public int Get_H_Num()
	{
		return H_Num;
	}

	public int Check_HnumFlip(int num)
	{
		if (H_Num == num)
		{
			if (isFlip > 0)
			{
				return -1;
			}
			return 1;
		}
		return 0;
	}

	public int Get_isPlaying()
	{
		return isPlaying;
	}
}
