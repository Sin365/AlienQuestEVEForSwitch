public class Player_Grab : global::UnityEngine.MonoBehaviour
{
	private bool onGrab;

	private int State;

	private float Grab_Timer;

	private int End_Count;

	private int facingRight = 1;

	public global::UnityEngine.GameObject Ctrl_1;

	public global::UnityEngine.GameObject Mon_Object;

	public global::UnityEngine.Transform pos_Vagina;

	private int Cum_Index = 400;

	private float Cum_Size = 1f;

	private float Sound_Timer;

	private float Moan_Timer;

	private int Moan_Num;

	private float CumShot_Timer;

	private H_SoundControl H_Sound;

	private Player_Control PC;

	private global::UnityEngine.GameObject Player;

	GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		Player = global::UnityEngine.GameObject.Find("Player");
		PC = Player.GetComponent<Player_Control>();
		if (global::UnityEngine.GameObject.Find("Sound_List_H") != null)
		{
			H_Sound = global::UnityEngine.GameObject.Find("Sound_List_H").GetComponent<H_SoundControl>();
		}
		GetComponent<global::UnityEngine.Animator>().speed = 0f;
	}

	private void Update()
	{
		if (!onGrab)
		{
			return;
		}
		Grab_Timer += global::UnityEngine.Time.deltaTime;
		if (Moan_Timer > 0f)
		{
			Moan_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (GM.Hscene_Num == 96)
		{
			if ((float)GM.HP < (float)GM.HP_Max * 0.4f && State == 0)
			{
				State = 1;
				GetComponent<global::UnityEngine.Animator>().SetInteger("State", State);
			}
		}
		else if (Grab_Timer > 10f && State == 0)
		{
			State = 1;
			GetComponent<global::UnityEngine.Animator>().SetInteger("State", State);
		}
	}

	private void H_Play()
	{
		onGrab = true;
		Grab_Timer = 0f;
		End_Count = 0;
		GetComponent<global::UnityEngine.Animator>().speed = 1f;
		base.transform.position = Player.transform.position;
		if (facingRight != PC.facingRight)
		{
			facingRight = -facingRight;
			bool flip = ((facingRight < 0) ? true : false);
			Ctrl_1.GetComponent<Puppet2D_GlobalControl>().flip = flip;
		}
		GetComponent<global::UnityEngine.Animator>().SetTrigger("onPlay");
	}

	private void H_Exit()
	{
		onGrab = false;
		State = 0;
		GetComponent<global::UnityEngine.Animator>().SetInteger("State", State);
		GetComponent<global::UnityEngine.Animator>().speed = 0f;
		base.transform.position = new global::UnityEngine.Vector3(-30f, 8f, 0f);
		if (Mon_Object != null)
		{
			Mon_Object.SendMessage("End_H_Grab");
			Mon_Object = null;
		}
	}

	private void H_Exit_GameOver()
	{
		onGrab = false;
		State = 0;
		GetComponent<global::UnityEngine.Animator>().SetInteger("State", State);
		GetComponent<global::UnityEngine.Animator>().speed = 0f;
		base.transform.position = new global::UnityEngine.Vector3(-30f, 8f, 0f);
		if (Mon_Object != null)
		{
			Mon_Object.SendMessage("End_H_Grab_GameOver");
			Mon_Object = null;
		}
	}

	private void Cum_Count()
	{
		End_Count++;
		if (End_Count >= 3 && Grab_Timer > 24f && State == 1)
		{
			State = 2;
			GetComponent<global::UnityEngine.Animator>().SetInteger("State", State);
		}
	}

	private void Cum_Reset()
	{
		End_Count = 0;
	}

	private void End_DownGrab()
	{
		GM.onEvent = false;
		GM.onHscene = false;
		GM.Hscene_Num = 0;
		GM.Hscene_Timer = 10f;
		End_Count = 0;
		GM.onFaceHugger = true;
		if (!GM.onChestBurster)
		{
			GM.onChestBurster = true;
		}
		onGrab = false;
		State = 0;
		GetComponent<global::UnityEngine.Animator>().SetInteger("State", State);
		GetComponent<global::UnityEngine.Animator>().speed = 0f;
		base.transform.position = new global::UnityEngine.Vector3(-30f, 8f, 0f);
		GM.Down_H_After();
		global::UnityEngine.GameObject.Find("Ani").SendMessage("End_H_Scene");
		global::UnityEngine.GameObject.Find("Ani").SendMessage("Set_FaceHugger");
	}

	private void Sound_Piston()
	{
		if (H_Sound != null)
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
		if (H_Sound != null && Moan_Timer <= 0f)
		{
			int num = global::UnityEngine.Random.Range(1, 11);
			if (Moan_Num != num && num != 3)
			{
				Moan_Num = num;
				H_Sound.Sound_Moan(num, 1);
				Moan_Timer = 1.5f;
			}
		}
	}

	private void Cum_End()
	{
		if (H_Sound != null && pos_Vagina != null)
		{
			H_Sound.Cum_DownDirect(pos_Vagina, Cum_Index, Cum_Size);
		}
	}

	private void Cum_End_Pee()
	{
		if (H_Sound != null && pos_Vagina != null)
		{
			H_Sound.Cum_Pee(pos_Vagina, Cum_Index, Cum_Size);
		}
	}
}
