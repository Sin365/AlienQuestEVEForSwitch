public class H_Ani : global::UnityEngine.MonoBehaviour
{
	public int H_Num;

	private bool onExit;

	public bool isGallery;

	public bool isLoop;

	public float Speed = 1f;

	public global::UnityEngine.GameObject Mon_Object;

	public global::UnityEngine.GameObject Mon_Object_2;

	private int facingRight = -1;

	public global::UnityEngine.GameObject Ctrl_1;

	public global::UnityEngine.GameObject Ctrl_2;

	public global::UnityEngine.GameObject Ctrl_3;

	public global::UnityEngine.GameObject Ctrl_4;

	public global::UnityEngine.GameObject H_Dummy;

	private global::UnityEngine.GameObject H_Dummy_Object;

	public global::UnityEngine.GameObject CensoredBox;

	public global::UnityEngine.GameObject Penis_1;

	public global::UnityEngine.GameObject Penis_1_Censored;

	public global::UnityEngine.GameObject Penis_2;

	public global::UnityEngine.GameObject Penis_2_Censored;

	public global::UnityEngine.Transform pos_Penis_1;

	public global::UnityEngine.Transform pos_Penis_2;

	public global::UnityEngine.Transform pos_Vagina;

	public global::UnityEngine.Transform pos_Mouth;

	public int Cum_Index = 10;

	public float Cum_Size = 1f;

	private float Sound_Timer;

	private float Cum_Rnd_Timer;

	private bool onEnd;

	private float End_Timer;

	private float Life_Timer;

	private float Moan_Timer;

	private float Moan_End_Timer;

	private int Moan_Num;

	private int Moan_End_Num;

	private bool onSoundEnd;

	private bool onSoundAfter;

	public H_Slot Slot;

	private int Slot_Num;

	public int CumShot_Num;

	private H_SoundControl H_Sound;

	private Gallery_Control GC;

    GameManager GM => GameManager.instance;

    private void Start()
	{
		if (GM != null)
		{
			//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
			if (H_Num > 0)
			{
				GM.Check_Hscene(H_Num - 1);
			}
			if (GM.GameOver && H_Num < 50 && H_Num != 13 && H_Num != 23 && global::UnityEngine.GameObject.Find("Menu_GameOver") != null)
			{
				global::UnityEngine.GameObject.Find("Menu_GameOver").GetComponent<Menu_GameOver>().H_Object = base.gameObject;
			}
		}
		else if (global::UnityEngine.GameObject.Find("Gallery_Menu") != null)
		{
			GC = global::UnityEngine.GameObject.Find("Gallery_Menu").GetComponent<Gallery_Control>();
		}
		if (global::UnityEngine.GameObject.Find("Sound_List_H") != null)
		{
			H_Sound = global::UnityEngine.GameObject.Find("Sound_List_H").GetComponent<H_SoundControl>();
		}
		if (AxiPlayerPrefs.GetInt("Censorship") != 1)
		{
			global::UnityEngine.Object.Destroy(CensoredBox.gameObject);
		}
		if (AxiPlayerPrefs.GetInt("UncensoredPatch") != 1)
		{
			if (Penis_1 != null)
			{
				global::UnityEngine.Object.Destroy(Penis_1.gameObject);
			}
			if (Penis_2 != null)
			{
				global::UnityEngine.Object.Destroy(Penis_2.gameObject);
			}
		}
		else
		{
			if (Penis_1_Censored != null)
			{
				global::UnityEngine.Object.Destroy(Penis_1_Censored.gameObject);
			}
			if (Penis_2_Censored != null)
			{
				global::UnityEngine.Object.Destroy(Penis_2_Censored.gameObject);
			}
		}
	}

	private void Update()
	{
		if (Slot_Num == 0 && Slot != null)
		{
			Slot_Num = Slot.Slot_Num;
		}
		if (!onEnd)
		{
			Life_Timer += global::UnityEngine.Time.deltaTime;
		}
		if (isGallery != GetComponent<global::UnityEngine.Animator>().GetBool("isGallery"))
		{
			GetComponent<global::UnityEngine.Animator>().SetBool("isGallery", isGallery);
		}
		if (isLoop != GetComponent<global::UnityEngine.Animator>().GetBool("isLoop"))
		{
			GetComponent<global::UnityEngine.Animator>().SetBool("isLoop", isLoop);
		}
		if (Sound_Timer > 0f)
		{
			Sound_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Cum_Rnd_Timer > 0f)
		{
			Cum_Rnd_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (End_Timer > 0f)
		{
			End_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Moan_Timer > 0f)
		{
			Moan_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Moan_End_Timer > 0f)
		{
			Moan_End_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (isGallery && GetComponent<global::UnityEngine.Animator>().speed != Speed && GetComponent<global::UnityEngine.Animator>().GetInteger("State") >= 0)
		{
			GetComponent<global::UnityEngine.Animator>().speed = global::UnityEngine.Mathf.Lerp(GetComponent<global::UnityEngine.Animator>().speed, Speed, global::UnityEngine.Time.deltaTime * 5f);
			if (H_Num == 2 && H_Dummy_Object != null)
			{
				H_Dummy_Object.GetComponent<global::UnityEngine.Animator>().speed = GetComponent<global::UnityEngine.Animator>().speed;
			}
		}
		if (H_Num > 50 || H_Num == 13 || H_Num == 23 || H_Num == 29)
		{
			return;
		}
		if (!isGallery && !isLoop && onEnd && End_Timer <= 0f)
		{
			H_Exit();
		}
		if (!isGallery && !onExit && GM != null && !GM.GameOver && !GM.onHscene && GM.Hscene_Num == 0)
		{
			if (Mon_Object != null)
			{
				Mon_Object.SendMessage("End_Hscene");
			}
			if (Mon_Object_2 != null)
			{
				Mon_Object_2.SendMessage("End_Hscene");
			}
			if (H_Dummy_Object != null)
			{
				global::UnityEngine.Object.Destroy(H_Dummy_Object.gameObject);
			}
			GM.onDown = false;
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void Flip()
	{
		facingRight = -facingRight;
		bool flag = ((facingRight > 0) ? true : false);
		if (Ctrl_1 != null)
		{
			Ctrl_1.GetComponent<Puppet2D_GlobalControl>().flip = flag;
		}
		if (Ctrl_2 != null)
		{
			if (H_Num == 7 || H_Num == 12)
			{
				Ctrl_2.GetComponent<Puppet2D_GlobalControl>().flip = !flag;
			}
			else
			{
				Ctrl_2.GetComponent<Puppet2D_GlobalControl>().flip = flag;
			}
		}
		if (Ctrl_3 != null)
		{
			if (H_Num == 32)
			{
				Ctrl_3.GetComponent<Puppet2D_GlobalControl>().flip = !flag;
			}
			else
			{
				Ctrl_3.GetComponent<Puppet2D_GlobalControl>().flip = flag;
			}
		}
		if (Ctrl_4 != null)
		{
			Ctrl_4.GetComponent<Puppet2D_GlobalControl>().flip = flag;
		}
		if (H_Num == 1)
		{
			Ctrl_1.transform.localPosition = new global::UnityEngine.Vector3(0.509f * (float)facingRight, 3.167f, 0f);
			if (flag && CensoredBox != null)
			{
				CensoredBox.transform.localPosition = new global::UnityEngine.Vector3(1.2f, 0f, 0f);
			}
		}
		else if (H_Num == 2)
		{
			Ctrl_1.transform.localPosition = new global::UnityEngine.Vector3(0.096f * (float)facingRight, 4.127f, 0f);
			H_Dummy_Object.SendMessage("Flip");
			if (flag && CensoredBox != null)
			{
				CensoredBox.transform.localPosition = new global::UnityEngine.Vector3(1.2f, 0f, 0f);
			}
		}
		else if (H_Num == 3)
		{
			if (flag && CensoredBox != null)
			{
				CensoredBox.transform.localPosition = new global::UnityEngine.Vector3(1.1f, 0f, 0f);
			}
		}
		else if (H_Num == 4)
		{
			if (flag && CensoredBox != null)
			{
				CensoredBox.transform.localPosition = new global::UnityEngine.Vector3(1f, 0f, 0f);
			}
		}
		else if (H_Num == 5)
		{
			Ctrl_1.transform.localPosition = new global::UnityEngine.Vector3(0.69f * (float)facingRight, 0f, 0f);
			Ctrl_2.transform.localPosition = new global::UnityEngine.Vector3(0f, 2.798f, 0f);
			if (flag && CensoredBox != null)
			{
				CensoredBox.transform.localPosition = new global::UnityEngine.Vector3(0.9f, 0f, 0f);
			}
		}
		else if (H_Num == 6)
		{
			if (flag && CensoredBox != null)
			{
				CensoredBox.transform.localPosition = new global::UnityEngine.Vector3(1f, 0f, 0f);
			}
		}
		else if (H_Num == 7)
		{
			Ctrl_1.transform.localPosition = new global::UnityEngine.Vector3(-0.5f * (float)facingRight, 0f, 0f);
			Ctrl_2.transform.localPosition = new global::UnityEngine.Vector3(2.32f * (float)facingRight, 2.798f, 0f);
			Ctrl_3.transform.localPosition = new global::UnityEngine.Vector3(-0.5f * (float)facingRight, 0f, 0f);
			if (flag && CensoredBox != null)
			{
				CensoredBox.transform.localPosition = new global::UnityEngine.Vector3(1f, 0f, 0f);
			}
		}
		else if (H_Num == 10)
		{
			Ctrl_2.transform.localPosition = new global::UnityEngine.Vector3(0.164978f * (float)facingRight, 3.093f, 0f);
		}
		else if (H_Num == 11)
		{
			if (flag && CensoredBox != null)
			{
				CensoredBox.transform.localPosition = new global::UnityEngine.Vector3(1f, 0f, 0f);
			}
		}
		else if (H_Num == 12)
		{
			Ctrl_1.transform.localPosition = new global::UnityEngine.Vector3(-1 * facingRight, 0f, 0f);
			Ctrl_2.transform.localPosition = new global::UnityEngine.Vector3(2.2f * (float)facingRight, 3.1f, 0f);
			Ctrl_3.transform.localPosition = new global::UnityEngine.Vector3(-0.64f * (float)facingRight, 3.1f, 0f);
			if (flag && CensoredBox != null)
			{
				CensoredBox.transform.localPosition = new global::UnityEngine.Vector3(0.8f, 3f, 0f);
			}
		}
		else if (H_Num == 13)
		{
			if (flag && CensoredBox != null)
			{
				CensoredBox.transform.localPosition = new global::UnityEngine.Vector3(1.6f, 0f, 0f);
			}
		}
		else if (H_Num == 14)
		{
			if (flag && CensoredBox != null)
			{
				CensoredBox.transform.localPosition = new global::UnityEngine.Vector3(1.4f, 0f, 0f);
			}
		}
		else if (H_Num == 15)
		{
			if (flag && CensoredBox != null)
			{
				CensoredBox.transform.localPosition = new global::UnityEngine.Vector3(0.8f, 3f, 0f);
			}
		}
		else if (H_Num == 16)
		{
			if (flag && CensoredBox != null)
			{
				CensoredBox.transform.localPosition = new global::UnityEngine.Vector3(1.7f, 2.6f, 0f);
			}
		}
		else if (H_Num == 19)
		{
			Ctrl_1.transform.localPosition = new global::UnityEngine.Vector3(0.8780212f * (float)facingRight, 2.517267f, 0f);
			Ctrl_2.transform.localPosition = Ctrl_1.transform.localPosition;
			Ctrl_3.transform.localPosition = Ctrl_1.transform.localPosition;
			Ctrl_4.transform.localPosition = Ctrl_1.transform.localPosition;
			if (flag && CensoredBox != null)
			{
				CensoredBox.transform.localPosition = new global::UnityEngine.Vector3(1f, 1.65f, 0f);
			}
		}
		else if (H_Num == 20)
		{
			Ctrl_1.transform.localPosition = new global::UnityEngine.Vector3(0.8780212f * (float)facingRight, 2.517267f, 0f);
			Ctrl_2.transform.localPosition = Ctrl_1.transform.localPosition;
			Ctrl_3.transform.localPosition = Ctrl_1.transform.localPosition;
			if (flag && CensoredBox != null)
			{
				CensoredBox.transform.localPosition = new global::UnityEngine.Vector3(1.8f, 1.5f, 0f);
			}
		}
		else if (H_Num == 21)
		{
			if (flag && CensoredBox != null)
			{
				CensoredBox.transform.localPosition = new global::UnityEngine.Vector3(1.4f, 0f, 0f);
			}
		}
		else if (H_Num == 22)
		{
			if (flag && CensoredBox != null)
			{
				CensoredBox.transform.localPosition = new global::UnityEngine.Vector3(0.8f, 0f, 0f);
			}
		}
		else if (H_Num == 23)
		{
			Ctrl_2.transform.localPosition = new global::UnityEngine.Vector3(-0.5f * (float)facingRight, 1.7f, 0f);
		}
		else if (H_Num == 24)
		{
			if (flag && CensoredBox != null)
			{
				CensoredBox.transform.localPosition = new global::UnityEngine.Vector3(1.4f, 0f, 0f);
			}
		}
		else if (H_Num == 29)
		{
			Ctrl_1.transform.localPosition = new global::UnityEngine.Vector3(-0.925f * (float)(-facingRight), 4f, 0f);
			Ctrl_2.transform.localPosition = new global::UnityEngine.Vector3(0.5f * (float)(-facingRight), 4.5f, 0f);
			if (flag && CensoredBox != null)
			{
				CensoredBox.transform.localPosition = new global::UnityEngine.Vector3(1f, 0f, 0f);
			}
		}
		else if (H_Num == 30)
		{
			Ctrl_2.transform.localPosition = new global::UnityEngine.Vector3(0.61f * (float)(-facingRight), 4.52f, 0f);
		}
		else if (H_Num == 31)
		{
			Ctrl_2.transform.localPosition = new global::UnityEngine.Vector3(0.5f * (float)(-facingRight), 4.52f, 0f);
		}
		else if (H_Num == 32)
		{
			Ctrl_2.transform.localPosition = new global::UnityEngine.Vector3(1.4f * (float)(-facingRight), 4.52f, 0f);
			Ctrl_3.transform.localPosition = new global::UnityEngine.Vector3(-2.75f * (float)(-facingRight), 4.52f, 0f);
		}
		else if (H_Num == 33)
		{
			Ctrl_2.transform.localPosition = new global::UnityEngine.Vector3(0.13f * (float)(-facingRight), 4.5f, 0f);
			if (flag && CensoredBox != null)
			{
				CensoredBox.transform.localPosition = new global::UnityEngine.Vector3(0.69f, 0f, 0f);
			}
		}
		else if (H_Num == 35)
		{
			Ctrl_2.transform.localPosition = new global::UnityEngine.Vector3(1.11f * (float)(-facingRight), 5.18f, 0f);
		}
		else if (H_Num == 36 || H_Num == 37)
		{
			Ctrl_2.transform.localPosition = new global::UnityEngine.Vector3(0.13f * (float)(-facingRight), 4.5f, 0f);
		}
		else if (H_Num == 38)
		{
			Ctrl_2.transform.localPosition = new global::UnityEngine.Vector3(-0.25f * (float)(-facingRight), 6.06f, 0f);
		}
		else if (H_Num == 39)
		{
			Ctrl_2.transform.localPosition = new global::UnityEngine.Vector3(-0.8f * (float)(-facingRight), 4.987f, 0f);
		}
		else if (H_Num == 40)
		{
			Ctrl_2.transform.localPosition = new global::UnityEngine.Vector3(0.4f * (float)(-facingRight), 4.98f, 0f);
		}
		else if (H_Num == 41)
		{
			Ctrl_2.transform.localPosition = new global::UnityEngine.Vector3(2.74f * (float)(-facingRight), 6.36f, 0f);
		}
	}

	private void Start_H8_Dummy()
	{
		if (H_Num == 2 && H_Dummy != null)
		{
			H_Dummy_Object = global::UnityEngine.Object.Instantiate(H_Dummy, new global::UnityEngine.Vector3(base.transform.position.x + 2.95f * (float)facingRight, base.transform.position.y + 2.23f, 0f), global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
			H_Dummy_Object.transform.parent = base.transform.parent;
		}
	}

	private void Delete_ToDual()
	{
		if (H_Dummy_Object != null)
		{
			global::UnityEngine.Object.Destroy(H_Dummy_Object.gameObject);
		}
		global::UnityEngine.Object.Destroy(base.gameObject);
	}

	private void H_End()
	{
		if (!isGallery && !isLoop)
		{
			if (H_Num == 14)
			{
				End_Timer = 4.5f;
			}
			else if (H_Num == 15 || H_Num == 21 || H_Num == 22 || H_Num == 25)
			{
				End_Timer = 4f;
			}
			else if (H_Num == 3 || H_Num == 9 || H_Num == 10 || H_Num == 11 || H_Num == 12)
			{
				End_Timer = 3f;
			}
			else
			{
				End_Timer = 2f;
			}
			onEnd = true;
			if (GM != null)
			{
				GM.MP = GM.MP_Max;
			}
		}
	}

	private void H_Exit()
	{
		onExit = true;
		if (Mon_Object != null)
		{
			Mon_Object.SendMessage("End_Hscene");
		}
		if (Mon_Object_2 != null)
		{
			Mon_Object_2.SendMessage("End_Hscene");
		}
		if (H_Dummy_Object != null)
		{
			global::UnityEngine.Object.Destroy(H_Dummy_Object.gameObject);
		}
		global::UnityEngine.Object.Destroy(base.gameObject);
	}

	private void H_Loop()
	{
		isLoop = true;
		onEnd = false;
		End_Timer = 0f;
		GetComponent<global::UnityEngine.Animator>().SetBool("isLoop", isLoop);
	}

	public void Play(int num, float speed)
	{
		GetComponent<global::UnityEngine.Animator>().SetInteger("State", num);
		GetComponent<global::UnityEngine.Animator>().SetTrigger("onPlay");
		GetComponent<global::UnityEngine.Animator>().SetBool("isStopped", false);
		Speed = speed;
		Sound_Timer = 0.2f;
		Moan_Timer = 0f;
		Moan_End_Timer = 0f;
		onSoundEnd = false;
	}

	private void Stop()
	{
		GetComponent<global::UnityEngine.Animator>().SetTrigger("onStop");
		GetComponent<global::UnityEngine.Animator>().SetBool("isStopped", true);
		isLoop = false;
		onEnd = false;
		End_Timer = 0f;
		Sound_Timer = 0.2f;
		Moan_Timer = 0f;
		Moan_End_Timer = 0f;
		onSoundEnd = false;
		onSoundAfter = true;
	}

	private void CumShot()
	{
		GetComponent<global::UnityEngine.Animator>().SetTrigger("onCumShot");
		Moan_Timer = 0f;
		Moan_End_Timer = 0f;
		onSoundEnd = false;
		CumShot_Num++;
	}

	private void After()
	{
		if (Slot != null)
		{
			Slot.SendMessage("Off_PlayButton");
		}
	}

	private void Sound_Piston()
	{
		if (Sound_Timer <= 0f && H_Sound != null)
		{
			if (H_Num == 1 || H_Num == 2)
			{
				H_Sound.SendMessage("Sound_Piston_5");
			}
			else if (H_Num == 3)
			{
				H_Sound.SendMessage("Sound_Piston_5");
			}
			else if (H_Num == 4)
			{
				H_Sound.SendMessage("Sound_Piston_5");
			}
			else if (H_Num == 5)
			{
				H_Sound.SendMessage("Sound_Piston_10");
			}
			else if (H_Num == 8 || H_Num == 9)
			{
				H_Sound.SendMessage("Sound_Piston_5");
			}
			else if (H_Num == 19)
			{
				H_Sound.SendMessage("Sound_Piston_10");
			}
			else if (H_Num > 50)
			{
				H_Sound.SendMessage("Sound_Piston_5");
			}
			else
			{
				H_Sound.SendMessage("Sound_Piston_10");
			}
		}
	}

	private void Sound_Piston_Sub()
	{
		if (Sound_Timer <= 0f && H_Sound != null)
		{
			if (H_Num == 7)
			{
				H_Sound.SendMessage("Sound_Piston_5");
			}
			else if (H_Num == 11)
			{
				H_Sound.SendMessage("Sound_Piston_5");
			}
			else if (H_Num == 12)
			{
				H_Sound.SendMessage("Sound_Piston_5");
			}
			else
			{
				H_Sound.SendMessage("Sound_Piston_5");
			}
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
		if (!(H_Sound != null) || !(Moan_Timer <= 0f))
		{
			return;
		}
		if (Speed < 1.4f || GetComponent<global::UnityEngine.Animator>().speed <= 1f)
		{
			int num = global::UnityEngine.Random.Range(1, 11);
			if (Moan_Num != num && num != 3)
			{
				Moan_Num = num;
				H_Sound.Sound_Moan(num, Slot_Num);
				Moan_Timer = 2.5f;
				if (GC != null && GC.Slot_Sum > 1)
				{
					Moan_Timer += (float)(GC.Slot_Sum - 1) * 0.4f;
				}
			}
			return;
		}
		int num2 = global::UnityEngine.Random.Range(7, 13);
		if (Moan_Num != num2)
		{
			Moan_Num = num2;
			H_Sound.Sound_Moan(num2, Slot_Num);
			Moan_Timer = 1.6f;
			if (GC != null && GC.Slot_Sum > 1)
			{
				Moan_Timer += (float)(GC.Slot_Sum - 1) * 0.4f;
			}
		}
	}

	private void Sound_Moan_Fast()
	{
		if (!(H_Sound != null) || !(Moan_Timer <= 0f))
		{
			return;
		}
		int num = global::UnityEngine.Random.Range(7, 13);
		if (Moan_Num != num)
		{
			Moan_Num = num;
			H_Sound.Sound_Moan(num, Slot_Num);
			Moan_Timer = 1.2f;
			if (GC != null && GC.Slot_Sum > 1)
			{
				Moan_Timer += (float)(GC.Slot_Sum - 1) * 0.4f;
			}
		}
	}

	private void Sound_Moan_End_2()
	{
		onSoundEnd = false;
		Moan_End_Timer = 0f;
		Sound_Moan_End();
	}

	private void Sound_Moan_End()
	{
		if (!(H_Sound != null) || onSoundEnd)
		{
			return;
		}
		onSoundEnd = true;
		onSoundAfter = false;
		if (Moan_End_Timer <= 0f)
		{
			int num = global::UnityEngine.Random.Range(15, 19);
			if (num == Moan_End_Num)
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
					num = 16;
					break;
				}
			}
			Moan_End_Num = num;
			H_Sound.Sound_Moan(num, Slot_Num);
			Moan_Timer = 2f;
			Moan_End_Timer = 1.5f;
			switch (num)
			{
			case 15:
				Moan_Timer = 2.2f;
				break;
			case 16:
				Moan_Timer = 1.5f;
				break;
			case 17:
				Moan_Timer = 3.1f;
				break;
			default:
				Moan_Timer = 1f;
				break;
			}
		}
		else if (Moan_Timer <= 0f)
		{
			int num2 = global::UnityEngine.Random.Range(7, 13);
			if (Moan_Num != num2)
			{
				Moan_Num = num2;
				H_Sound.Sound_Moan(num2, Slot_Num);
				Moan_Timer = 1.2f;
			}
		}
	}

	private void Sound_Moan_After()
	{
		if (H_Sound != null && !onSoundAfter && Moan_Timer <= 0f)
		{
			onSoundAfter = true;
			int num = global::UnityEngine.Random.Range(20, 22);
			H_Sound.Sound_Moan(num, Slot_Num);
			Moan_Timer = 4f;
		}
	}

	private void Sound_Moan_Close()
	{
		if (!(H_Sound != null) || !(Moan_Timer <= 0f))
		{
			return;
		}
		if (Speed < 1.4f || GetComponent<global::UnityEngine.Animator>().speed <= 1f)
		{
			int num = global::UnityEngine.Random.Range(22, 27);
			if (Moan_Num != num)
			{
				Moan_Num = num;
				H_Sound.Sound_Moan(num, Slot_Num);
				Moan_Timer = 2f;
				if (GC != null && GC.Slot_Sum > 1)
				{
					Moan_Timer += (float)(GC.Slot_Sum - 1) * 0.4f;
				}
			}
			return;
		}
		int num2 = global::UnityEngine.Random.Range(24, 27);
		if (Moan_Num != num2)
		{
			Moan_Num = num2;
			H_Sound.Sound_Moan(num2, Slot_Num);
			Moan_Timer = 1.2f;
			if (GC != null && GC.Slot_Sum > 1)
			{
				Moan_Timer += (float)(GC.Slot_Sum - 1) * 0.4f;
			}
		}
	}

	private void Sound_Moan_Close_Fast()
	{
		if (!(H_Sound != null) || !(Moan_Timer <= 0f))
		{
			return;
		}
		int num = global::UnityEngine.Random.Range(24, 27);
		if (Moan_Num != num)
		{
			Moan_Num = num;
			H_Sound.Sound_Moan(num, Slot_Num);
			Moan_Timer = 1.2f;
			if (GC != null && GC.Slot_Sum > 1)
			{
				Moan_Timer += (float)(GC.Slot_Sum - 1) * 0.4f;
			}
		}
	}

	private void Sound_Moan_Close_End()
	{
		if (H_Sound != null && !onSoundEnd)
		{
			onSoundEnd = true;
			onSoundAfter = false;
			H_Sound.Sound_Moan(27, Slot_Num);
			Moan_Timer = 1.5f;
			Moan_End_Timer = 1.5f;
		}
	}

	private void Cum_Random()
	{
		if (!(Life_Timer > 5f) || !(H_Sound != null) || !(Cum_Rnd_Timer <= 0f))
		{
			return;
		}
		if (pos_Vagina != null && global::UnityEngine.Random.Range(0, 10) > 8)
		{
			H_Sound.Cum_DownDirect(pos_Vagina, Cum_Index, Cum_Size);
		}
		if (pos_Mouth != null && global::UnityEngine.Random.Range(0, 10) > 8)
		{
			if (H_Num == 20)
			{
				H_Sound.Cum_DownDirect(pos_Mouth, 39, Cum_Size);
			}
			else
			{
				H_Sound.Cum_DownDirect(pos_Mouth, Cum_Index, Cum_Size);
			}
		}
		Cum_Rnd_Timer = 0.25f;
	}

	private void Cum_End()
	{
		if (!(H_Sound != null) || !(pos_Vagina != null))
		{
			return;
		}
		if (H_Num == 20)
		{
			if (global::UnityEngine.Random.Range(0, 10) > 4)
			{
				H_Sound.Cum_DownDirect(pos_Vagina, Cum_Index, Cum_Size);
			}
			if (global::UnityEngine.Random.Range(0, 10) > 4)
			{
				H_Sound.Cum_DownDirect(pos_Mouth, 39, Cum_Size);
			}
		}
		else
		{
			H_Sound.Cum_DownDirect(pos_Vagina, Cum_Index, Cum_Size);
		}
	}

	private void Cum_End_Mouth()
	{
		if (H_Sound != null && pos_Mouth != null)
		{
			H_Sound.Cum_DownDirect(pos_Mouth, Cum_Index, Cum_Size);
		}
	}

	private void Cum_End_Pee()
	{
		if (H_Sound != null && pos_Vagina != null)
		{
			H_Sound.Cum_Pee(pos_Vagina, Cum_Index, Cum_Size);
		}
		if (H_Num == 13 && H_Sound != null && pos_Penis_1 != null)
		{
			H_Sound.Cum_Pee(pos_Penis_1, Cum_Index, Cum_Size);
		}
	}

	private void Cum_End_After_Grool()
	{
		if (H_Sound != null)
		{
			if (H_Num == 20)
			{
				H_Sound.Cum_DownDrool(pos_Vagina, Cum_Index, Cum_Size);
			}
			else if (pos_Penis_1 != null && global::UnityEngine.Random.Range(0, 10) > 4)
			{
				H_Sound.Cum_DownDrool(pos_Penis_1, Cum_Index, Cum_Size);
			}
			if (pos_Penis_2 != null && global::UnityEngine.Random.Range(0, 10) > 4)
			{
				H_Sound.Cum_DownDrool(pos_Penis_2, Cum_Index, Cum_Size);
			}
		}
	}

	private void Cum_End_After_Grool_32()
	{
		if (H_Sound != null && pos_Penis_1 != null && pos_Penis_2 != null)
		{
			H_Sound.Cum_DownDrool(pos_Penis_1, Cum_Index, Cum_Size);
			H_Sound.Cum_DownDrool(pos_Penis_2, 56, Cum_Size);
		}
	}

	private void Cum_End_Mouth_32()
	{
		if (H_Sound != null && pos_Mouth != null)
		{
			H_Sound.Cum_DownDirect(pos_Mouth, 56, Cum_Size);
		}
	}

	private void Mon_7_End_Dist_5()
	{
		if (global::UnityEngine.GameObject.Find("HG_List") != null)
		{
			global::UnityEngine.GameObject.Find("HG_List").GetComponent<Event_TentacleToMon7>().Hit(base.transform.position);
		}
	}
}
