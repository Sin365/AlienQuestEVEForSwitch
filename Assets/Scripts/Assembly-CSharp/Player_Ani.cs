using UnityEngine;

public class Player_Ani : global::UnityEngine.MonoBehaviour
{
	private enum AniState
	{
		Idle = 0,
		Run = 1,
		Sit = 2,
		Jump = 3,
		Spin = 4,
		Slide = 5,
		BackDash = 6,
		Damage = 7,
		Down = 8,
		Scene = 9
	}

	private Player_Ani.AniState State;

	private int Ani_Index;

	private float Ani_Timer;

	private float Func_Timer;

	private float Run_Speed = 0.03f;

	private bool onAttack;

	private bool onAttack_2;

	private bool onAttack_3;

	private bool onAttack_4;

	private bool onMagic;

	private bool onEdge;

	private bool onCloth = true;

	private int Num_EyeClose;

	private float EyeClose_Delay;

	private int Num_Turn;

	private int Num_RunStart;

	private int Num_RunStart_Jump;

	private int Num_RunStart_Sit;

	private int Num_RunStop;

	private int Num_RunStop_Sit;

	private int Num_SitUp;

	private int Num_JumpEnd;

	private int Num_JumpShort;

	private int Num_SlideJump;

	private int Num_BackDash;

	private int Num_Attack;

	private int Num_SpinEnd;

	private float SpinEnd_Timer;

	private int Num_Damage;

	private int Num_Down;

	private int Num_GetUp;

	private float Attack_Delay;

	private float Atk3_Timer;

	private float Combo_Delay_2;

	private float Combo_Delay_3;

	private float Combo_Delay_4;

	private float ClothOpacity;

	private float AttackJump_Error;

	private float BoxCol_SizeY = 4.8f;

	private global::UnityEngine.BoxCollider2D BoxCol;

	private float PC_Col_Delay;

	private global::UnityEngine.Sprite[] Spr_Idle;

	private global::UnityEngine.Sprite[] Spr_Idle_E;

	private global::UnityEngine.Sprite[] Spr_Run;

	private global::UnityEngine.Sprite[] Spr_Sit;

	private global::UnityEngine.Sprite[] Spr_Jump;

	private global::UnityEngine.Sprite[] Spr_Attack;

	private global::UnityEngine.Sprite[] Spr_Attack_2;

	private global::UnityEngine.Sprite[] Spr_Attack_3;

	private global::UnityEngine.Sprite[] Spr_Attack_4;

	private global::UnityEngine.Sprite[] Spr_Effect_1;

	private global::UnityEngine.Sprite[] Spr_Naked_1;

	private global::UnityEngine.Sprite[] Spr_Naked_2;

	private global::UnityEngine.Sprite[] Spr_Naked_3;

	private global::UnityEngine.Sprite[] Spr_Naked_4;

	private global::UnityEngine.Sprite[] Spr_Naked_5;

	private global::UnityEngine.Sprite[] Spr_Naked_6;

	private global::UnityEngine.SpriteRenderer SR;

	private global::UnityEngine.SpriteRenderer SR_Eye;

	private global::UnityEngine.SpriteRenderer SR_FaceHugger;

	private global::UnityEngine.SpriteRenderer SR_ClothOff;

	private global::UnityEngine.SpriteRenderer SR_ClothGlow;

	private global::UnityEngine.SpriteRenderer SR_Effect;

	private global::UnityEngine.SpriteRenderer SR_Effect_Spin;

	private global::UnityEngine.SpriteRenderer SR_Effect_SpinGlow;

	private global::UnityEngine.SpriteRenderer SR_Effect_SpinAfter;

	public global::UnityEngine.GameObject Atk_Lag;

	public global::UnityEngine.GameObject Spin_Lag;

	public global::UnityEngine.SpriteRenderer CensoredBreast;

	private float Censored_Timer;

	private bool on_H_Down;

	private bool onFaceHugger;

	private float FaceHugger_Timer;

	public global::UnityEngine.Animator H_Down;

	public global::UnityEngine.GameObject Ctrl_Down;

	public global::UnityEngine.SpriteRenderer Sperm_1;

	public global::UnityEngine.SpriteRenderer Sperm_2;

	public global::UnityEngine.SkinnedMeshRenderer Down_FaceHugger;

	public global::UnityEngine.SkinnedMeshRenderer Down_FaceHugger_L;

	public global::UnityEngine.SkinnedMeshRenderer Down_FaceHugger_R;

	GameManager GM => GameManager.instance;
    Player_Control PC => GameManager.instance.PC;
    GameObject Player => GameManager.instance.gobj_Player;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//PC = global::UnityEngine.GameObject.Find("Player").GetComponent<Player_Control>();
		BoxCol = GetComponent<global::UnityEngine.BoxCollider2D>();
		Spr_Idle = global::UnityEngine.Resources.LoadAll<global::UnityEngine.Sprite>("PC/2048_01_Idle");
		Spr_Idle_E = global::UnityEngine.Resources.LoadAll<global::UnityEngine.Sprite>("PC/2048_02_Idle_E");
		Spr_Run = global::UnityEngine.Resources.LoadAll<global::UnityEngine.Sprite>("PC/2048_03_Run");
		Spr_Sit = global::UnityEngine.Resources.LoadAll<global::UnityEngine.Sprite>("PC/2048_04_Sit");
		Spr_Jump = global::UnityEngine.Resources.LoadAll<global::UnityEngine.Sprite>("PC/2048_05_Jump");
		Spr_Attack = global::UnityEngine.Resources.LoadAll<global::UnityEngine.Sprite>("PC/2048_07_Attack");
		Spr_Attack_2 = global::UnityEngine.Resources.LoadAll<global::UnityEngine.Sprite>("PC/2048_07_Attack2");
		Spr_Attack_3 = global::UnityEngine.Resources.LoadAll<global::UnityEngine.Sprite>("PC/2048_07_Attack3");
		Spr_Attack_4 = global::UnityEngine.Resources.LoadAll<global::UnityEngine.Sprite>("PC/2048_07_Attack4");
		Set_Weapon();
		Spr_Naked_1 = global::UnityEngine.Resources.LoadAll<global::UnityEngine.Sprite>("PC/2048_08_Naked");
		Spr_Naked_2 = global::UnityEngine.Resources.LoadAll<global::UnityEngine.Sprite>("PC/2048_09_Naked");
		Spr_Naked_3 = global::UnityEngine.Resources.LoadAll<global::UnityEngine.Sprite>("PC/2048_10_Naked");
		Spr_Naked_4 = global::UnityEngine.Resources.LoadAll<global::UnityEngine.Sprite>("PC/2048_11_Naked");
		Spr_Naked_5 = global::UnityEngine.Resources.LoadAll<global::UnityEngine.Sprite>("PC/2048_12_Naked");
		Spr_Naked_6 = global::UnityEngine.Resources.LoadAll<global::UnityEngine.Sprite>("PC/2048_13_Naked");
		SR = GetComponent<global::UnityEngine.SpriteRenderer>();
		SR_Eye = global::UnityEngine.GameObject.Find("Ani_Eye").GetComponent<global::UnityEngine.SpriteRenderer>();
		SR_FaceHugger = global::UnityEngine.GameObject.Find("Ani_FaceHugger").GetComponent<global::UnityEngine.SpriteRenderer>();
		SR_ClothOff = global::UnityEngine.GameObject.Find("ClothOff").GetComponent<global::UnityEngine.SpriteRenderer>();
		SR_ClothGlow = global::UnityEngine.GameObject.Find("ClothGlow").GetComponent<global::UnityEngine.SpriteRenderer>();
		SR_Effect = global::UnityEngine.GameObject.Find("Effect_Attack").GetComponent<global::UnityEngine.SpriteRenderer>();
		SR_Effect_Spin = global::UnityEngine.GameObject.Find("Effect_Spin").GetComponent<global::UnityEngine.SpriteRenderer>();
		SR_Effect_SpinGlow = global::UnityEngine.GameObject.Find("Effect_SpinGlow").GetComponent<global::UnityEngine.SpriteRenderer>();
		SR_Effect_SpinAfter = global::UnityEngine.GameObject.Find("Effect_SpinAfter").GetComponent<global::UnityEngine.SpriteRenderer>();
		CensoredBreast.enabled = false;
		CensoredBreast.color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
		H_Down.speed = 0f;
	}

	private void Reset_Num()
	{
		Num_Turn = 0;
		Num_RunStart = 0;
		Num_RunStart_Jump = 0;
		Num_RunStart_Sit = 0;
		Num_RunStop = 0;
		Num_RunStop_Sit = 0;
		Num_SitUp = 0;
		Num_JumpEnd = 0;
		Num_JumpShort = 0;
		Num_SlideJump = 0;
		Num_BackDash = 0;
		Num_Attack = 0;
		Num_SpinEnd = 0;
		SpinEnd_Timer = 0f;
		Num_Damage = 0;
		Num_Down = 0;
		Off_Col_Attack();
		SR_Effect.sprite = Spr_Effect_1[0];
		SR_Effect_Spin.sprite = null;
		SR_Effect_SpinGlow.sprite = null;
		SR_Effect_SpinAfter.sprite = null;
		global::UnityEngine.GameObject.Find("Col_Spin").GetComponent<global::UnityEngine.BoxCollider2D>().enabled = false;
	}

	public void Set_Edge(bool OnEdge)
	{
		onEdge = OnEdge;
	}

	public void Set_Turn()
	{
		Reset_Num();
		Num_Turn = 8;
		Ani_Index = 16;
		Ani_Timer = 1f;
	}

	public void Set_Turn_Sit()
	{
		if (GM.onCloth)
		{
			if (onEdge)
			{
				SR.sprite = Spr_Attack_3[26];
			}
			else
			{
				SR.sprite = Spr_Attack_3[24];
			}
		}
		else
		{
			SR.sprite = Spr_Naked_1[31];
		}
		Reset_Num();
		Num_Turn = 2;
		Ani_Index = 8;
		Ani_Timer = 0f;
	}

	public void Set_Run()
	{
		Reset_Num();
		if (State == Player_Ani.AniState.Jump)
		{
			State = Player_Ani.AniState.Run;
			Num_RunStart_Jump = 4;
		}
		else if (State == Player_Ani.AniState.Sit || (State == Player_Ani.AniState.Idle && Num_SitUp > 4))
		{
			State = Player_Ani.AniState.Run;
			Num_RunStart_Sit = 4;
		}
		else
		{
			State = Player_Ani.AniState.Run;
			Num_RunStart = 4;
		}
		Ani_Index = 2;
		Ani_Timer = 1f;
	}

	public void Set_Sit()
	{
		int num_SitUp = Num_SitUp;
		if (State == Player_Ani.AniState.Run)
		{
			Reset_Num();
			State = Player_Ani.AniState.Sit;
			Num_RunStop_Sit = 2;
			Ani_Index = 0;
			Ani_Timer = 1f;
		}
		else if (State == Player_Ani.AniState.Idle && Num_JumpEnd > 0)
		{
			State = Player_Ani.AniState.Sit;
			switch (Num_JumpEnd)
			{
			case 16:
				Ani_Index = 1;
				break;
			case 15:
				Ani_Index = 2;
				break;
			case 14:
				Ani_Index = 3;
				break;
			case 13:
				Ani_Index = 4;
				break;
			case 12:
				Ani_Index = 5;
				break;
			case 11:
				Ani_Index = 6;
				break;
			case 10:
				Ani_Index = 7;
				break;
			case 9:
				Ani_Index = 8;
				break;
			case 8:
				Ani_Index = 7;
				break;
			case 7:
				Ani_Index = 6;
				break;
			case 6:
				Ani_Index = 5;
				break;
			case 5:
				Ani_Index = 4;
				break;
			case 4:
				Ani_Index = 3;
				break;
			case 3:
				Ani_Index = 2;
				break;
			case 2:
				Ani_Index = 1;
				break;
			case 1:
				Ani_Index = 1;
				break;
			case 0:
				Ani_Index = 1;
				break;
			}
			Reset_Num();
		}
		else
		{
			Reset_Num();
			State = Player_Ani.AniState.Sit;
			if (num_SitUp > 3)
			{
				Ani_Index = num_SitUp;
				Ani_Timer = 1f;
			}
			else
			{
				Ani_Index = 1;
				Ani_Timer = 1f;
			}
		}
	}

	public void Set_Jump()
	{
		Reset_Num();
		State = Player_Ani.AniState.Jump;
		Ani_Index = 0;
		Ani_Timer = 1f;
		if (onAttack_3 || onAttack_4)
		{
			AttackJump_Error = 0.5f;
		}
	}

	public void Set_Jump_Slide()
	{
		Set_Jump();
		Num_SlideJump = 2;
	}

	public void Set_HighJump()
	{
		Reset_Num();
		State = Player_Ani.AniState.Jump;
		Ani_Index = 3;
		Ani_Timer = -0.5f;
		if (GM.onCloth)
		{
			SR.sprite = Spr_Jump[Ani_Index];
		}
		else
		{
			SR.sprite = Spr_Naked_3[Ani_Index];
		}
	}

	public void Set_Attack_2()
	{
		onMagic = false;
		Reset_Num();
		Num_Attack = 8;
		onAttack = true;
		onAttack_2 = true;
		onAttack_3 = false;
		onAttack_4 = false;
		Ani_Index = 14;
		Ani_Timer = 1f;
	}

	public void Set_Attack_3()
	{
		onMagic = false;
		Reset_Num();
		Num_Attack = 22;
		onAttack = true;
		onAttack_2 = true;
		onAttack_3 = true;
		onAttack_4 = false;
		Ani_Index = 22;
		Ani_Timer = 1f;
	}

	public void Set_Attack_4()
	{
		onAttack_4 = true;
		if (onAttack_3 && Num_Attack < 15)
		{
			onAttack_3 = false;
			Num_Attack = 11;
			Off_Col_Attack();
		}
	}

	public void Set_Attack()
	{
		onMagic = false;
		Reset_Num();
		PC.Sound_Attack();
		Num_Attack = 10;
		onAttack = true;
		onAttack_2 = false;
		onAttack_3 = false;
		onAttack_4 = false;
		Ani_Index = 1;
		Ani_Timer = 1f;
	}

	public void Set_Attack_Down()
	{
		Reset_Num();
		Num_Attack = 110;
		Ani_Index = 2;
		onAttack = true;
		onAttack_2 = false;
		onAttack_3 = false;
		onAttack_4 = false;
		onMagic = false;
		Ani_Timer = 1f;
	}

	public void Set_Attack_Down2()
	{
		Reset_Num();
		Num_Attack = 210;
		Ani_Index = 2;
		onAttack = true;
		onAttack_2 = false;
		onAttack_3 = false;
		onAttack_4 = false;
		onMagic = false;
		Ani_Timer = 1f;
	}

	public void Set_Spin()
	{
		Reset_Num();
		State = Player_Ani.AniState.Spin;
		onAttack = true;
		onAttack_2 = false;
		onAttack_3 = false;
		onAttack_4 = false;
		Ani_Index = 0;
		Ani_Timer = 1f;
		SR_Effect_SpinGlow.sprite = Spr_Effect_1[32];
		global::UnityEngine.GameObject.Find("Col_Spin").GetComponent<global::UnityEngine.BoxCollider2D>().enabled = true;
	}

	public void Set_Throw()
	{
		Reset_Num();
		Num_Attack = 9;
		onAttack = true;
		onAttack_2 = false;
		onAttack_3 = false;
		onAttack_4 = false;
		onMagic = true;
		Ani_Index = 1;
		Ani_Timer = 1f;
	}

	public void Set_Idle()
	{
		Reset_Num();
		State = Player_Ani.AniState.Idle;
		Ani_Index = 0;
		Ani_Timer = 1f;
	}

	public void Set_Slide()
	{
		Reset_Num();
		onAttack = false;
		onAttack_2 = false;
		onAttack_3 = false;
		onAttack_4 = false;
		onMagic = false;
		State = Player_Ani.AniState.Slide;
		Ani_Index = 0;
		Ani_Timer = 1f;
	}

	public void Set_Run_Stop()
	{
		State = Player_Ani.AniState.Idle;
		if (Attack_Delay <= 0f)
		{
			Ani_Index = 16;
			Ani_Timer = 1f;
			Num_RunStop = 6;
		}
	}

	public void Set_SitUp()
	{
		State = Player_Ani.AniState.Idle;
		Ani_Index = 15;
		Ani_Timer = 1f;
		Num_SitUp = 9;
	}

	public void Set_Jump_End()
	{
		State = Player_Ani.AniState.Idle;
		Ani_Index = 15;
		Ani_Timer = 1f;
		if (!onAttack)
		{
			Num_JumpEnd = 16;
		}
		else if (Num_Attack > 100)
		{
			End_Attack(0);
		}
		BoxCol_SizeY = 2.2f;
		BoxCol.offset = new global::UnityEngine.Vector2(-0.1f, 1.22f);
		BoxCol.size = new global::UnityEngine.Vector2(1.2f, 2.2f);
	}

	public void Set_Jump_Short()
	{
		State = Player_Ani.AniState.Idle;
		Ani_Index = 15;
		Ani_Timer = 1f;
		if (!onAttack)
		{
			Num_JumpShort = 6;
		}
		else if (Num_Attack > 100)
		{
			End_Attack(0);
		}
	}

	public void Set_Spin_End_Idle()
	{
		Reset_Num();
		onAttack = false;
		onAttack_2 = false;
		onAttack_3 = false;
		onAttack_4 = false;
		State = Player_Ani.AniState.Idle;
		Ani_Index = 15;
		Ani_Timer = 1f;
		Num_JumpShort = 5;
		Num_SpinEnd = 3;
		SpinEnd_Timer = 1f;
	}

	public void Set_Spin_End_Jump()
	{
		Reset_Num();
		onAttack = false;
		onAttack_2 = false;
		onAttack_3 = false;
		onAttack_4 = false;
		State = Player_Ani.AniState.Jump;
		Ani_Index = 0;
		Ani_Timer = 1f;
		Num_SpinEnd = 3;
		SpinEnd_Timer = 1f;
	}

	public void Cancel()
	{
		Reset_Num();
		onAttack = false;
		onAttack_2 = false;
		onAttack_3 = false;
		onAttack_4 = false;
		onMagic = false;
		onEdge = false;
		Ani_Index = 0;
		Ani_Timer = 1f;
	}

	private void End_Attack(int Num)
	{
		SR_Effect.sprite = Spr_Effect_1[0];
		PC.onAttack = false;
		onAttack_2 = false;
		onAttack = false;
		Attack_Delay = 0.5f;
		onAttack_3 = false;
		onAttack_4 = false;
		Ani_Index = Num;
		Ani_Timer = 1f;
	}

	public void Set_Run_Speed(float speed)
	{
		Run_Speed = speed;
	}

	public void Set_Damage()
	{
		Reset_Num();
		onAttack = false;
		onAttack_2 = false;
		onMagic = false;
		onAttack_3 = false;
		onAttack_4 = false;
		State = Player_Ani.AniState.Damage;
		Ani_Index = 9;
		Ani_Timer = 1f;
		EyeClose_Delay = 3f;
		Num_EyeClose = 0;
		SR_Eye.sprite = null;
		Censored_Timer = 0f;
	}

	public void Set_Down()
	{
		Reset_Num();
		State = Player_Ani.AniState.Down;
		Ani_Index = 0;
		Ani_Timer = 1f;
		EyeClose_Delay = 3f;
		Num_EyeClose = 0;
		SR_Eye.sprite = null;
		Censored_Timer = 0f;
		SR_FaceHugger.enabled = GM.onFaceHugger;
	}

	public void OnOff_Cloth()
	{
		if (!GM.onCloth && onCloth)
		{
			ClothOpacity = 1f;
			SR_ClothOff.enabled = true;
			SR_ClothOff.sprite = null;
			SR_ClothGlow.transform.localPosition = new global::UnityEngine.Vector3(-0.1f, 3.795f, 0f);
			SR_ClothGlow.enabled = true;
		}
		else
		{
			ClothOpacity = 0f;
			SR_ClothOff.enabled = false;
			SR_ClothOff.sprite = null;
		}
		if (GM.onCloth)
		{
			global::UnityEngine.GameObject.Find("Ani_Rolling").GetComponent<global::UnityEngine.SpriteRenderer>().sprite = Spr_Sit[27];
			onCloth = true;
		}
		else
		{
			global::UnityEngine.GameObject.Find("Ani_Rolling").GetComponent<global::UnityEngine.SpriteRenderer>().sprite = Spr_Naked_1[33];
			onCloth = false;
		}
	}

	public void End_Slide()
	{
		Ani_Index = 8;
		Ani_Timer = 1f;
		if (!GM.onCloth)
		{
			SR.sprite = Spr_Naked_2[15];
		}
		else if (onEdge)
		{
			SR.sprite = Spr_Sit[15];
		}
		else
		{
			SR.sprite = Spr_Sit[6];
		}
		State = Player_Ani.AniState.Sit;
	}

	public void Set_BackDash()
	{
		Reset_Num();
		State = Player_Ani.AniState.BackDash;
		Ani_Index = 15;
		Ani_Timer = 0f;
		Num_BackDash = 1;
		if (GM.onCloth)
		{
			SR.sprite = Spr_Attack_3[28];
		}
		else
		{
			SR.sprite = Spr_Naked_5[27];
		}
	}

	public void Set_DropAtk()
	{
		State = Player_Ani.AniState.Jump;
		Ani_Index = 1;
	}

	public bool Check_FlipLock()
	{
		if (onAttack_3 || onAttack_4)
		{
			return true;
		}
		return false;
	}

	public void Set_Weapon()
	{
		if (GM.Weapon_Num == 0)
		{
			global::UnityEngine.GameObject.Find("Effect_Spin").transform.localScale = new global::UnityEngine.Vector3(1.3f, 2f, 1f);
			global::UnityEngine.GameObject.Find("Effect_SpinGlow").transform.localScale = new global::UnityEngine.Vector3(1.3f, 2f, 1f);
			global::UnityEngine.GameObject.Find("Col_Spin").GetComponent<global::UnityEngine.BoxCollider2D>().size = new global::UnityEngine.Vector2(9.56f, 0.66f);
			global::UnityEngine.GameObject.Find("Col_Spin").GetComponent<global::UnityEngine.BoxCollider2D>().offset = new global::UnityEngine.Vector2(0f, 4.2f);
		}
		else
		{
			global::UnityEngine.GameObject.Find("Effect_Spin").transform.localScale = new global::UnityEngine.Vector3(2f, 2f, 1f);
			global::UnityEngine.GameObject.Find("Effect_SpinGlow").transform.localScale = new global::UnityEngine.Vector3(2f, 2f, 1f);
			global::UnityEngine.GameObject.Find("Col_Spin").GetComponent<global::UnityEngine.BoxCollider2D>().size = new global::UnityEngine.Vector2(14.8f, 0.66f);
			global::UnityEngine.GameObject.Find("Col_Spin").GetComponent<global::UnityEngine.BoxCollider2D>().offset = new global::UnityEngine.Vector2(0f, 4.2f);
		}
		if (GM.Weapon_Num == 0)
		{
			Spr_Effect_1 = global::UnityEngine.Resources.LoadAll<global::UnityEngine.Sprite>("Effect/2048_Ef_Weapon_0");
			global::UnityEngine.Sprite[] array = global::UnityEngine.Resources.LoadAll<global::UnityEngine.Sprite>("Effect/2048_Ef_Weapon_1");
			for (int i = 0; i < 10; i++)
			{
				Spr_Effect_1[25 + i] = array[25 + i];
			}
		}
		else if (GM.Weapon_Num == 1)
		{
			Spr_Effect_1 = global::UnityEngine.Resources.LoadAll<global::UnityEngine.Sprite>("Effect/2048_Ef_Weapon_1");
		}
		else if (GM.Weapon_Num == 2)
		{
			Spr_Effect_1 = global::UnityEngine.Resources.LoadAll<global::UnityEngine.Sprite>("Effect/2048_Ef_Weapon_2");
			global::UnityEngine.GameObject.Find("Col_Spin").GetComponent<global::UnityEngine.BoxCollider2D>().size = new global::UnityEngine.Vector2(15.6f, 0.66f);
			global::UnityEngine.GameObject.Find("Col_Spin").GetComponent<global::UnityEngine.BoxCollider2D>().offset = new global::UnityEngine.Vector2(0f, 4.2f);
		}
		else if (GM.Weapon_Num == 3)
		{
			Spr_Effect_1 = global::UnityEngine.Resources.LoadAll<global::UnityEngine.Sprite>("Effect/2048_Ef_Weapon_3");
			global::UnityEngine.GameObject.Find("Col_Spin").GetComponent<global::UnityEngine.BoxCollider2D>().size = new global::UnityEngine.Vector2(15.6f, 0.7f);
			global::UnityEngine.GameObject.Find("Col_Spin").GetComponent<global::UnityEngine.BoxCollider2D>().offset = new global::UnityEngine.Vector2(0f, 4.15f);
		}
		else if (GM.Weapon_Num == 4)
		{
			Spr_Effect_1 = global::UnityEngine.Resources.LoadAll<global::UnityEngine.Sprite>("Effect/2048_Ef_Weapon_4");
			global::UnityEngine.GameObject.Find("Col_Spin").GetComponent<global::UnityEngine.BoxCollider2D>().size = new global::UnityEngine.Vector2(18.8f, 1f);
			global::UnityEngine.GameObject.Find("Col_Spin").GetComponent<global::UnityEngine.BoxCollider2D>().offset = new global::UnityEngine.Vector2(0f, 4f);
		}
		else if (GM.Weapon_Num == 5)
		{
			Spr_Effect_1 = global::UnityEngine.Resources.LoadAll<global::UnityEngine.Sprite>("Effect/2048_Ef_Weapon_5");
			global::UnityEngine.GameObject.Find("Col_Spin").GetComponent<global::UnityEngine.BoxCollider2D>().size = new global::UnityEngine.Vector2(18.8f, 1.2f);
			global::UnityEngine.GameObject.Find("Col_Spin").GetComponent<global::UnityEngine.BoxCollider2D>().offset = new global::UnityEngine.Vector2(0f, 4f);
		}
		if (GM.Weapon_Num < 5)
		{
			global::UnityEngine.GameObject.Find("Effect_Attack").GetComponent<global::UnityEngine.SpriteRenderer>().material = global::UnityEngine.Resources.Load("Effect/Mtl_Additive", typeof(global::UnityEngine.Material)) as global::UnityEngine.Material;
			global::UnityEngine.GameObject.Find("Effect_Spin").GetComponent<global::UnityEngine.SpriteRenderer>().material = global::UnityEngine.Resources.Load("Effect/Mtl_Additive", typeof(global::UnityEngine.Material)) as global::UnityEngine.Material;
		}
		else
		{
			global::UnityEngine.GameObject.Find("Effect_Attack").GetComponent<global::UnityEngine.SpriteRenderer>().material = global::UnityEngine.Resources.Load("Effect/Mtl_AddSoft", typeof(global::UnityEngine.Material)) as global::UnityEngine.Material;
			global::UnityEngine.GameObject.Find("Effect_Spin").GetComponent<global::UnityEngine.SpriteRenderer>().material = global::UnityEngine.Resources.Load("Effect/Mtl_AddSoft", typeof(global::UnityEngine.Material)) as global::UnityEngine.Material;
		}
		global::UnityEngine.GameObject.Find("Effect_Rolling").GetComponent<global::UnityEngine.SpriteRenderer>().sprite = Spr_Effect_1[25];
		global::UnityEngine.GameObject.Find("Border_Rolling").GetComponent<global::UnityEngine.SpriteRenderer>().sprite = Spr_Effect_1[33];
		global::UnityEngine.GameObject.Find("Glow_Rolling").GetComponent<global::UnityEngine.SpriteRenderer>().sprite = Spr_Effect_1[34];
	}

	public void Set_GetUp()
	{
		Reset_Num();
		Num_SitUp = 13;
		if (!GM.onCloth)
		{
			SR.sprite = Spr_Naked_6[17];
		}
		else
		{
			SR.sprite = Spr_Attack_3[43];
		}
	}

	private void Update()
	{
		if (GM.Paused || GM.onGatePass)
		{
			CensoredBreast.enabled = false;
			return;
		}
		if (GM.GameOver && AxiPlayerPrefs.GetInt("Censorship") == 1)
		{
			if (Censored_Timer > 1.8f)
			{
				CensoredBreast.enabled = true;
				CensoredBreast.color = global::UnityEngine.Color.Lerp(CensoredBreast.color, new global::UnityEngine.Color(1f, 1f, 1f, 1f), global::UnityEngine.Time.deltaTime * 12f);
			}
		}
		else if (CensoredBreast.enabled)
		{
			CensoredBreast.enabled = false;
			Censored_Timer = 0f;
			CensoredBreast.color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
		}
		if (base.transform.localPosition.x > 0f || base.transform.localPosition.y > 0f)
		{
			base.transform.localPosition = new global::UnityEngine.Vector3(0f, 0f, 0f);
		}
		if (Attack_Delay > 0f)
		{
			Attack_Delay -= global::UnityEngine.Time.deltaTime;
		}
		if (Atk3_Timer > 0f)
		{
			Atk3_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (EyeClose_Delay > 0f)
		{
			EyeClose_Delay -= global::UnityEngine.Time.deltaTime;
		}
		if (PC_Col_Delay > 0f)
		{
			PC_Col_Delay -= global::UnityEngine.Time.deltaTime;
		}
		if (State == Player_Ani.AniState.Sit || State == Player_Ani.AniState.Slide)
		{
			global::UnityEngine.GameObject.Find("Effect_Attack").transform.localPosition = new global::UnityEngine.Vector3(0f, -2.53f, 0f);
			global::UnityEngine.GameObject.Find("Col_Attack_0_1").transform.localPosition = new global::UnityEngine.Vector3(0f, -2.53f, 0f);
			global::UnityEngine.GameObject.Find("Col_Attack_1_1").transform.localPosition = new global::UnityEngine.Vector3(0f, -2.53f, 0f);
			global::UnityEngine.GameObject.Find("Col_Attack_2_1").transform.localPosition = new global::UnityEngine.Vector3(0f, -2.53f, 0f);
			global::UnityEngine.GameObject.Find("Col_Attack_4_1").transform.localPosition = new global::UnityEngine.Vector3(0f, -2.53f, 0f);
			global::UnityEngine.GameObject.Find("Col_Attack_5_1").transform.localPosition = new global::UnityEngine.Vector3(0f, -2.53f, 0f);
		}
		else
		{
			global::UnityEngine.GameObject.Find("Effect_Attack").transform.localPosition = new global::UnityEngine.Vector3(0f, 0f, 0f);
			global::UnityEngine.GameObject.Find("Col_Attack_0_1").transform.localPosition = new global::UnityEngine.Vector3(0f, 0f, 0f);
			global::UnityEngine.GameObject.Find("Col_Attack_1_1").transform.localPosition = new global::UnityEngine.Vector3(0f, 0f, 0f);
			global::UnityEngine.GameObject.Find("Col_Attack_2_1").transform.localPosition = new global::UnityEngine.Vector3(0f, 0f, 0f);
			global::UnityEngine.GameObject.Find("Col_Attack_4_1").transform.localPosition = new global::UnityEngine.Vector3(0f, 0f, 0f);
			global::UnityEngine.GameObject.Find("Col_Attack_5_1").transform.localPosition = new global::UnityEngine.Vector3(0f, 0f, 0f);
		}
		if (ClothOpacity > 0f)
		{
			Ani_ClothOpacity();
		}
		if (PC.onFlicker || State != Player_Ani.AniState.Idle || onAttack || onAttack_2 || onAttack_3 || onAttack_4 || EyeClose_Delay > 0f)
		{
			SR_Eye.sprite = null;
			Num_EyeClose = 0;
		}
		else if (onEdge || !GM.onCloth)
		{
			SR_Eye.transform.localPosition = new global::UnityEngine.Vector3(0.169f, 4.575f, 0f);
		}
		else
		{
			SR_Eye.transform.localPosition = new global::UnityEngine.Vector3(0.1403f, 4.575f, 0f);
		}
		switch (State)
		{
		case Player_Ani.AniState.Down:
			if (GM.GameOver)
			{
				Censored_Timer += global::UnityEngine.Time.deltaTime;
			}
			Ani_Down();
			break;
		case Player_Ani.AniState.Damage:
			if (SR_Eye.sprite != null)
			{
				SR_Eye.sprite = null;
			}
			if (BoxCol_SizeY != 4.8f || BoxCol.size.x != 1.2f)
			{
				BoxCol_SizeY = 4.8f;
				BoxCol.offset = new global::UnityEngine.Vector2(-0.1f, 2.54f);
				BoxCol.size = new global::UnityEngine.Vector2(1.2f, 4.8f);
			}
			Ani_Damage();
			break;
		case Player_Ani.AniState.Slide:
			if (BoxCol_SizeY != 2.2f)
			{
				BoxCol_SizeY = 2.2f;
				BoxCol.offset = new global::UnityEngine.Vector2(-0.1f, 1.22f);
				BoxCol.size = new global::UnityEngine.Vector2(1.2f, 2.2f);
			}
			Ani_Slide();
			break;
		case Player_Ani.AniState.Spin:
			if (BoxCol_SizeY != 4.8f || BoxCol.size.x != 1.2f)
			{
				BoxCol_SizeY = 4.8f;
				BoxCol.offset = new global::UnityEngine.Vector2(-0.1f, 2.54f);
				BoxCol.size = new global::UnityEngine.Vector2(1.2f, 4.8f);
			}
			Ani_Attack_Spin();
			break;
		case Player_Ani.AniState.Idle:
			if (Num_JumpEnd <= 0)
			{
				if (Num_SitUp > 0)
				{
					if (Num_SitUp < 7 && BoxCol_SizeY != 4.8f)
					{
						BoxCol_SizeY = 4.8f;
						BoxCol.offset = new global::UnityEngine.Vector2(-0.1f, 2.54f);
						BoxCol.size = new global::UnityEngine.Vector2(1.2f, 4.8f);
					}
				}
				else if (BoxCol_SizeY != 4.8f || BoxCol.size.x != 1.2f)
				{
					BoxCol_SizeY = 4.8f;
					BoxCol.offset = new global::UnityEngine.Vector2(-0.1f, 2.54f);
					BoxCol.size = new global::UnityEngine.Vector2(1.2f, 4.8f);
				}
			}
			if (onAttack_4 && !onAttack_3)
			{
				Ani_Attack_Idle_4();
			}
			else if (onAttack_3)
			{
				Ani_Attack_Idle_3();
			}
			else if (onAttack_2)
			{
				Ani_Attack_Idle_2();
			}
			else if (onAttack)
			{
				Ani_Attack_Idle_1();
			}
			else if (Num_Turn > 0)
			{
				Ani_Turn();
			}
			else if (Num_RunStop > 0)
			{
				Ani_RunStop();
			}
			else if (Num_SitUp > 0)
			{
				Ani_SitUp();
			}
			else if (Num_BackDash > 0)
			{
				Num_BackDash = 0;
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_5[28];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Attack_3[30];
				}
				else
				{
					SR.sprite = Spr_Attack_3[29];
				}
			}
			else if (Num_JumpEnd > 0)
			{
				Ani_JumpEnd();
			}
			else if (Num_JumpShort > 0)
			{
				Ani_JumpShort();
			}
			else
			{
				Ani_Idle();
			}
			break;
		case Player_Ani.AniState.Run:
			if (BoxCol_SizeY != 4.6f || BoxCol.size.x != 1.6f)
			{
				BoxCol_SizeY = 4.6f;
				BoxCol.offset = new global::UnityEngine.Vector2(-0.1f, 2.44f);
				BoxCol.size = new global::UnityEngine.Vector2(1.6f, 4.6f);
			}
			if (onAttack_4 && !onAttack_3)
			{
				Ani_Attack_Idle_4();
				break;
			}
			if (onAttack_3)
			{
				Ani_Attack_Idle_3();
				break;
			}
			if (onAttack_2)
			{
				Ani_Attack_Idle_2();
				break;
			}
			if (onAttack)
			{
				Ani_Attack_Idle_1();
				break;
			}
			if (Num_RunStart > 0)
			{
				Ani_RunStart();
				break;
			}
			if (Num_RunStart_Jump > 0)
			{
				Ani_RunStart_Jump();
				break;
			}
			if (Num_RunStart_Sit > 0)
			{
				Ani_RunStart_Sit();
				break;
			}
			if (Num_JumpShort > 0)
			{
				Num_JumpShort = 0;
			}
			if (SR_Effect.sprite != Spr_Effect_1[0])
			{
				SR_Effect.sprite = Spr_Effect_1[0];
			}
			Ani_Run();
			break;
		case Player_Ani.AniState.Sit:
			if (BoxCol_SizeY != 2.2f)
			{
				BoxCol_SizeY = 2.2f;
				BoxCol.offset = new global::UnityEngine.Vector2(-0.1f, 1.22f);
				BoxCol.size = new global::UnityEngine.Vector2(1.2f, 2.2f);
			}
			if (onAttack)
			{
				Ani_Attack_Sit();
			}
			else if (Num_Turn > 0)
			{
				Ani_Turn_Sit();
			}
			else if (Ani_Index < 8)
			{
				Ani_Sit();
			}
			break;
		case Player_Ani.AniState.Jump:
			if (PC.onRolling)
			{
				if (BoxCol_SizeY != 2.2f)
				{
					BoxCol_SizeY = 2.2f;
					BoxCol.offset = new global::UnityEngine.Vector2(-0.1f, 2.7f);
					BoxCol.size = new global::UnityEngine.Vector2(2.2f, 2.2f);
				}
			}
			else if (BoxCol_SizeY != 4.8f || BoxCol.size.x != 1.2f)
			{
				BoxCol_SizeY = 4.8f;
				BoxCol.offset = new global::UnityEngine.Vector2(-0.1f, 2.54f);
				BoxCol.size = new global::UnityEngine.Vector2(1.2f, 4.8f);
			}
			if (onAttack_4 && !onAttack_3)
			{
				Ani_Attack_Idle_4();
			}
			else if (onAttack_3)
			{
				Ani_Attack_Idle_3();
			}
			else if (onAttack_2)
			{
				Ani_Attack_Jump_2();
			}
			else if (onAttack)
			{
				if (Num_Attack > 200)
				{
					Ani_Attack_Jump_Down2();
				}
				else if (Num_Attack > 100)
				{
					Ani_Attack_Jump_Down();
				}
				else
				{
					Ani_Attack_Jump_1();
				}
			}
			else if (Num_SlideJump > 0)
			{
				Ani_Slide_To_Jump();
			}
			else if (Num_BackDash > 0)
			{
				Num_BackDash = 0;
				if (GM.onCloth)
				{
					SR.sprite = Spr_Attack_3[31];
				}
				else
				{
					SR.sprite = Spr_Naked_5[29];
				}
			}
			else
			{
				Ani_Jump();
			}
			break;
		}
		if (Num_SpinEnd > 0)
		{
			Ani_Spin_End();
		}
		else if (State != Player_Ani.AniState.Spin && SR_Effect_Spin.sprite != null)
		{
			SR_Effect_Spin.sprite = null;
			SR_Effect_SpinGlow.sprite = null;
			SR_Effect_SpinAfter.sprite = null;
			global::UnityEngine.GameObject.Find("Col_Spin").GetComponent<global::UnityEngine.BoxCollider2D>().enabled = false;
		}
		if ((GM.GameOver || GM.onDown) && State == Player_Ani.AniState.Down && Num_Down >= 20)
		{
			if (GM.Hscene_Num == 0 && !on_H_Down)
			{
				ON_H_Down();
			}
		}
		else if (!GM.GameOver && !GM.onDown && on_H_Down)
		{
			OFF_H_Down();
		}
		if (onFaceHugger)
		{
			FaceHugger_Timer += global::UnityEngine.Time.deltaTime;
			if (FaceHugger_Timer > 6f)
			{
				GM.onFaceHugger = false;
				onFaceHugger = false;
			}
			else if (FaceHugger_Timer > 5f)
			{
				Down_FaceHugger.material.color = global::UnityEngine.Color.Lerp(Down_FaceHugger.material.color, new global::UnityEngine.Color(1f, 1f, 1f, 0f), global::UnityEngine.Time.deltaTime * 3f);
				Down_FaceHugger_L.material.color = Down_FaceHugger.material.color;
				Down_FaceHugger_R.material.color = Down_FaceHugger.material.color;
			}
		}
		if ((GM.onHscene || onFaceHugger) && onAttack)
		{
			End_Attack(0);
		}
	}

	private void Ani_Turn()
	{
		if (Ani_Timer > 0.02f)
		{
			Ani_Timer = 0f;
			Num_Turn--;
			switch (Num_Turn)
			{
			case 8:
				if (GM.onCloth)
				{
					if (onEdge)
					{
						SR.sprite = Spr_Idle[19];
					}
					else
					{
						SR.sprite = Spr_Idle[18];
					}
				}
				else
				{
					SR.sprite = Spr_Naked_1[18];
				}
				PC.FootStep();
				break;
			case 7:
				if (GM.onCloth)
				{
					SR.sprite = Spr_Idle_E[18];
				}
				else
				{
					SR.sprite = Spr_Naked_1[19];
				}
				break;
			case 6:
				if (GM.onCloth)
				{
					SR.sprite = Spr_Idle_E[19];
				}
				else
				{
					SR.sprite = Spr_Naked_1[20];
				}
				break;
			case 5:
				if (GM.onCloth)
				{
					if (onEdge)
					{
						SR.sprite = Spr_Run[10];
					}
					else
					{
						SR.sprite = Spr_Idle_E[20];
					}
				}
				else
				{
					SR.sprite = Spr_Naked_1[21];
				}
				break;
			case 4:
				if (GM.onCloth)
				{
					if (onEdge)
					{
						SR.sprite = Spr_Run[11];
					}
					else
					{
						SR.sprite = Spr_Idle_E[21];
					}
				}
				else
				{
					SR.sprite = Spr_Naked_1[22];
				}
				break;
			case 3:
				if (GM.onCloth)
				{
					if (onEdge)
					{
						SR.sprite = Spr_Run[12];
					}
					else
					{
						SR.sprite = Spr_Idle_E[22];
					}
				}
				else
				{
					SR.sprite = Spr_Naked_1[23];
				}
				break;
			case 2:
				if (GM.onCloth)
				{
					if (onEdge)
					{
						SR.sprite = Spr_Run[13];
					}
					else
					{
						SR.sprite = Spr_Run[23];
					}
				}
				else
				{
					SR.sprite = Spr_Naked_1[24];
				}
				break;
			case 1:
				if (GM.onCloth)
				{
					if (onEdge)
					{
						SR.sprite = Spr_Run[14];
					}
					else
					{
						SR.sprite = Spr_Run[9];
					}
				}
				else
				{
					SR.sprite = Spr_Naked_1[25];
				}
				break;
			case 0:
				Ani_Timer = 1f;
				break;
			}
		}
		else
		{
			Ani_Timer += global::UnityEngine.Time.deltaTime;
		}
	}

	private void Ani_Turn_Sit()
	{
		if (Ani_Timer > 0.03f)
		{
			Ani_Timer = 0f;
			Num_Turn--;
			switch (Num_Turn)
			{
			case 1:
				if (GM.onCloth)
				{
					if (onEdge)
					{
						SR.sprite = Spr_Attack_3[27];
					}
					else
					{
						SR.sprite = Spr_Attack_3[25];
					}
				}
				else
				{
					SR.sprite = Spr_Naked_1[32];
				}
				break;
			case 0:
				if (GM.onCloth)
				{
					if (onEdge)
					{
						SR.sprite = Spr_Sit[15];
					}
					else
					{
						SR.sprite = Spr_Sit[6];
					}
				}
				else
				{
					SR.sprite = Spr_Naked_2[15];
				}
				break;
			}
		}
		else
		{
			Ani_Timer += global::UnityEngine.Time.deltaTime;
		}
	}

	private void Ani_Idle()
	{
		if (Ani_Timer > 0.025f)
		{
			Ani_Timer = 0f;
			if (Ani_Index >= 17)
			{
				Ani_Index = 0;
			}
			else
			{
				Ani_Index++;
			}
			if (Ani_Index == 1)
			{
				if (EyeClose_Delay <= 0f && global::UnityEngine.Random.Range(0, 3) == 1)
				{
					Num_EyeClose = 1;
				}
				else
				{
					Num_EyeClose = 0;
				}
			}
			if (GM.onCloth)
			{
				if (onEdge)
				{
					SR.sprite = Spr_Idle_E[Ani_Index];
				}
				else
				{
					SR.sprite = Spr_Idle[Ani_Index];
				}
			}
			else
			{
				SR.sprite = Spr_Naked_1[Ani_Index];
			}
			if (PC.onFlicker)
			{
				SR_Eye.sprite = null;
			}
			else
			{
				if (Num_EyeClose <= 0 || Ani_Index <= 0 || Ani_Index >= 7)
				{
					return;
				}
				if (GM.onCloth)
				{
					if (onEdge)
					{
						SR_Eye.sprite = Spr_Idle[29 + Ani_Index];
					}
					else
					{
						SR_Eye.sprite = Spr_Idle[23 + Ani_Index];
					}
				}
				else
				{
					SR_Eye.sprite = Spr_Naked_1[33 + Ani_Index];
				}
			}
		}
		else
		{
			Ani_Timer += global::UnityEngine.Time.deltaTime;
		}
	}

	private void Ani_RunStart()
	{
		if (Ani_Timer > 0.015f)
		{
			Ani_Timer = 0f;
			Num_RunStart--;
			SR_Effect.sprite = Spr_Effect_1[0];
			switch (Num_RunStart)
			{
			case 3:
				if (GM.onCloth)
				{
					if (onEdge)
					{
						SR.sprite = Spr_Idle[19];
					}
					else
					{
						SR.sprite = Spr_Idle[18];
					}
				}
				else
				{
					SR.sprite = Spr_Naked_1[18];
				}
				PC.FootStep();
				break;
			case 2:
				if (GM.onCloth)
				{
					SR.sprite = Spr_Idle_E[18];
				}
				else
				{
					SR.sprite = Spr_Naked_1[19];
				}
				break;
			case 1:
				if (GM.onCloth)
				{
					SR.sprite = Spr_Idle_E[19];
				}
				else
				{
					SR.sprite = Spr_Naked_1[20];
				}
				break;
			case 0:
				Ani_Timer = 1f;
				break;
			}
		}
		else
		{
			Ani_Timer += global::UnityEngine.Time.deltaTime;
		}
	}

	private void Ani_RunStart_Jump()
	{
		if (Ani_Timer > 0.02f)
		{
			Ani_Timer = 0f;
			Num_RunStart_Jump--;
			switch (Num_RunStart_Jump)
			{
			case 3:
				if (GM.onCloth)
				{
					SR.sprite = Spr_Jump[15];
				}
				else
				{
					SR.sprite = Spr_Naked_3[18];
				}
				break;
			case 2:
				if (GM.onCloth)
				{
					SR.sprite = Spr_Jump[16];
				}
				else
				{
					SR.sprite = Spr_Naked_3[19];
				}
				break;
			case 1:
				if (GM.onCloth)
				{
					SR.sprite = Spr_Idle_E[19];
				}
				else
				{
					SR.sprite = Spr_Naked_1[20];
				}
				break;
			case 0:
				Ani_Timer = 1f;
				break;
			}
		}
		else
		{
			Ani_Timer += global::UnityEngine.Time.deltaTime;
		}
	}

	private void Ani_RunStart_Sit()
	{
		if (Ani_Timer > 0.015f)
		{
			Ani_Timer = 0f;
			Num_RunStart_Sit--;
			switch (Num_RunStart_Sit)
			{
			case 3:
				if (GM.onCloth)
				{
					if (onEdge)
					{
						SR.sprite = Spr_Run[18];
					}
					else
					{
						SR.sprite = Spr_Run[15];
					}
				}
				else
				{
					SR.sprite = Spr_Naked_1[26];
				}
				break;
			case 2:
				if (GM.onCloth)
				{
					SR.sprite = Spr_Run[16];
				}
				else
				{
					SR.sprite = Spr_Naked_1[27];
				}
				break;
			case 1:
				if (GM.onCloth)
				{
					SR.sprite = Spr_Run[17];
				}
				else
				{
					SR.sprite = Spr_Naked_1[28];
				}
				break;
			case 0:
				Ani_Timer = 1f;
				break;
			}
		}
		else
		{
			Ani_Timer += global::UnityEngine.Time.deltaTime;
		}
	}

	private void Ani_Run()
	{
		if (Ani_Timer > Run_Speed)
		{
			Ani_Timer = 0f;
			if (Ani_Index >= 8)
			{
				Ani_Index = 0;
			}
			else
			{
				Ani_Index++;
			}
			if (GM.onCloth)
			{
				SR.sprite = Spr_Run[Ani_Index];
			}
			else
			{
				SR.sprite = Spr_Naked_2[Ani_Index];
			}
			if (Ani_Index == 2)
			{
				PC.FootStep();
			}
			else if (Ani_Index == 6)
			{
				PC.FootStep();
			}
		}
		else
		{
			Ani_Timer += global::UnityEngine.Time.deltaTime;
		}
	}

	private void Ani_RunStop()
	{
		if (Ani_Timer > 0.025f)
		{
			Ani_Timer = 0f;
			Num_RunStop--;
			switch (Num_RunStop)
			{
			case 5:
				if (GM.onCloth)
				{
					if (onEdge)
					{
						SR.sprite = Spr_Run[10];
					}
					else
					{
						SR.sprite = Spr_Idle_E[20];
					}
				}
				else
				{
					SR.sprite = Spr_Naked_1[21];
				}
				break;
			case 4:
				if (GM.onCloth)
				{
					if (onEdge)
					{
						SR.sprite = Spr_Run[11];
					}
					else
					{
						SR.sprite = Spr_Idle_E[21];
					}
				}
				else
				{
					SR.sprite = Spr_Naked_1[22];
				}
				break;
			case 3:
				if (GM.onCloth)
				{
					if (onEdge)
					{
						SR.sprite = Spr_Run[12];
					}
					else
					{
						SR.sprite = Spr_Idle_E[22];
					}
				}
				else
				{
					SR.sprite = Spr_Naked_1[23];
				}
				break;
			case 2:
				if (GM.onCloth)
				{
					if (onEdge)
					{
						SR.sprite = Spr_Run[13];
					}
					else
					{
						SR.sprite = Spr_Idle_E[23];
					}
				}
				else
				{
					SR.sprite = Spr_Naked_1[24];
				}
				break;
			case 1:
				Ani_Index = 16;
				if (GM.onCloth)
				{
					if (onEdge)
					{
						SR.sprite = Spr_Run[14];
					}
					else
					{
						SR.sprite = Spr_Run[9];
					}
				}
				else
				{
					SR.sprite = Spr_Naked_1[25];
				}
				break;
			case 0:
				Ani_Timer = 1f;
				break;
			}
		}
		else
		{
			Ani_Timer += global::UnityEngine.Time.deltaTime;
		}
	}

	private void Ani_Sit()
	{
		if (Ani_Timer > 0.015f)
		{
			Ani_Timer = 0f;
			Ani_Index++;
			switch (Ani_Index)
			{
			case 1:
				if (Num_RunStop_Sit > 0)
				{
					if (GM.onCloth)
					{
						SR.sprite = Spr_Run[19];
					}
					else
					{
						SR.sprite = Spr_Naked_1[29];
					}
				}
				break;
			case 2:
				if (Num_RunStop_Sit > 0)
				{
					if (GM.onCloth)
					{
						if (onEdge)
						{
							SR.sprite = Spr_Run[21];
						}
						else
						{
							SR.sprite = Spr_Run[20];
						}
					}
					else
					{
						SR.sprite = Spr_Naked_1[30];
					}
				}
				else if (GM.onCloth)
				{
					if (onEdge)
					{
						SR.sprite = Spr_Sit[9];
					}
					else
					{
						SR.sprite = Spr_Sit[0];
					}
				}
				else
				{
					SR.sprite = Spr_Naked_2[9];
				}
				break;
			case 3:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_2[10];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Sit[10];
				}
				else
				{
					SR.sprite = Spr_Sit[1];
				}
				break;
			case 4:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_2[11];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Sit[11];
				}
				else
				{
					SR.sprite = Spr_Sit[2];
				}
				break;
			case 5:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_2[12];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Sit[12];
				}
				else
				{
					SR.sprite = Spr_Sit[3];
				}
				break;
			case 6:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_2[13];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Sit[13];
				}
				else
				{
					SR.sprite = Spr_Sit[4];
				}
				break;
			case 7:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_2[14];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Sit[14];
				}
				else
				{
					SR.sprite = Spr_Sit[5];
				}
				break;
			case 8:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_2[15];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Sit[15];
				}
				else
				{
					SR.sprite = Spr_Sit[6];
				}
				break;
			}
		}
		else
		{
			Ani_Timer += global::UnityEngine.Time.deltaTime;
		}
	}

	private void Ani_SitUp()
	{
		if (Ani_Timer > 0.02f)
		{
			Ani_Timer = 0f;
			Num_SitUp--;
			switch (Num_SitUp)
			{
			case 8:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_2[14];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Sit[14];
				}
				else
				{
					SR.sprite = Spr_Sit[5];
				}
				break;
			case 7:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_2[13];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Sit[13];
				}
				else
				{
					SR.sprite = Spr_Sit[4];
				}
				break;
			case 6:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_2[12];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Sit[12];
				}
				else
				{
					SR.sprite = Spr_Sit[3];
				}
				break;
			case 5:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_2[11];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Sit[11];
				}
				else
				{
					SR.sprite = Spr_Sit[2];
				}
				break;
			case 4:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_2[10];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Sit[10];
				}
				else
				{
					SR.sprite = Spr_Sit[1];
				}
				break;
			case 3:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_2[9];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Sit[9];
				}
				else
				{
					SR.sprite = Spr_Sit[0];
				}
				break;
			case 2:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_2[16];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Sit[16];
				}
				else
				{
					SR.sprite = Spr_Sit[7];
				}
				break;
			case 1:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_2[17];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Sit[17];
				}
				else
				{
					SR.sprite = Spr_Sit[8];
				}
				Ani_Index = 14;
				break;
			case 0:
				Ani_Timer = 1f;
				break;
			}
		}
		else
		{
			Ani_Timer += global::UnityEngine.Time.deltaTime;
		}
	}

	private void Ani_JumpShort()
	{
		if (Ani_Timer > 0.03f)
		{
			Ani_Timer = 0f;
			Num_JumpShort--;
			switch (Num_JumpShort)
			{
			case 5:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_2[18];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Sit[21];
				}
				else
				{
					SR.sprite = Spr_Sit[18];
				}
				break;
			case 4:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_2[19];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Sit[22];
				}
				else
				{
					SR.sprite = Spr_Sit[19];
				}
				break;
			case 3:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_2[9];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Sit[9];
				}
				else
				{
					SR.sprite = Spr_Sit[0];
				}
				SR_Effect.sprite = Spr_Effect_1[0];
				break;
			case 2:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_2[16];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Sit[16];
				}
				else
				{
					SR.sprite = Spr_Sit[7];
				}
				break;
			case 1:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_2[17];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Sit[17];
				}
				else
				{
					SR.sprite = Spr_Sit[8];
				}
				break;
			case 0:
				Ani_Timer = 1f;
				break;
			}
		}
		else
		{
			Ani_Timer += global::UnityEngine.Time.deltaTime;
		}
	}

	private void Ani_JumpEnd()
	{
		if (Ani_Timer > 0.02f)
		{
			Ani_Timer = 0f;
			Num_JumpEnd--;
			switch (Num_JumpEnd)
			{
			case 15:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_2[20];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Sit[23];
				}
				else
				{
					SR.sprite = Spr_Sit[20];
				}
				Num_JumpEnd--;
				break;
			case 13:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_2[11];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Sit[11];
				}
				else
				{
					SR.sprite = Spr_Sit[2];
				}
				break;
			case 12:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_2[12];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Sit[12];
				}
				else
				{
					SR.sprite = Spr_Sit[3];
				}
				break;
			case 11:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_2[13];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Sit[13];
				}
				else
				{
					SR.sprite = Spr_Sit[4];
				}
				break;
			case 10:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_2[14];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Sit[14];
				}
				else
				{
					SR.sprite = Spr_Sit[5];
				}
				break;
			case 9:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_2[15];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Sit[15];
				}
				else
				{
					SR.sprite = Spr_Sit[6];
				}
				break;
			case 8:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_2[14];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Sit[14];
				}
				else
				{
					SR.sprite = Spr_Sit[5];
				}
				break;
			case 7:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_2[13];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Sit[13];
				}
				else
				{
					SR.sprite = Spr_Sit[4];
				}
				break;
			case 6:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_2[12];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Sit[12];
				}
				else
				{
					SR.sprite = Spr_Sit[3];
				}
				break;
			case 5:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_2[11];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Sit[11];
				}
				else
				{
					SR.sprite = Spr_Sit[2];
				}
				break;
			case 4:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_2[10];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Sit[10];
				}
				else
				{
					SR.sprite = Spr_Sit[1];
				}
				break;
			case 3:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_2[9];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Sit[9];
				}
				else
				{
					SR.sprite = Spr_Sit[0];
				}
				break;
			case 2:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_2[16];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Sit[16];
				}
				else
				{
					SR.sprite = Spr_Sit[7];
				}
				break;
			case 1:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_2[17];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Sit[17];
				}
				else
				{
					SR.sprite = Spr_Sit[8];
				}
				break;
			case 0:
				Ani_Timer = 1f;
				break;
			case 14:
				break;
			}
		}
		else
		{
			Ani_Timer += global::UnityEngine.Time.deltaTime;
		}
	}

	private void Ani_Jump()
	{
		if (AttackJump_Error > 0f)
		{
			AttackJump_Error -= global::UnityEngine.Time.deltaTime;
		}
		if (Ani_Timer > 0.02f)
		{
			Ani_Timer = 0f;
			if (AttackJump_Error > 0f)
			{
				Ani_Index = 0;
				AttackJump_Error = 0f;
			}
			if (GM.onCloth)
			{
				if (Ani_Index < 15)
				{
					SR.sprite = Spr_Jump[Ani_Index];
				}
				else
				{
					SR.sprite = Spr_Attack_2[Ani_Index + 6];
				}
			}
			else
			{
				SR.sprite = Spr_Naked_3[Ani_Index];
			}
			if (Ani_Index < 16)
			{
				Ani_Index++;
			}
			else if (Ani_Index < 17)
			{
				Ani_Index++;
			}
			else
			{
				Ani_Index = 16;
			}
		}
		else
		{
			Ani_Timer += global::UnityEngine.Time.deltaTime;
		}
	}

	private void Ani_Attack_Idle_1()
	{
		if (Ani_Timer > 0.02f)
		{
			Ani_Timer = 0f;
			if (GM.Weapon_Num == 5)
			{
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Atk_Lag, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
				gameObject.transform.parent = base.transform;
				gameObject.transform.localScale = new global::UnityEngine.Vector3(2f, 2f, 1f);
				gameObject.GetComponent<global::UnityEngine.SpriteRenderer>().sprite = SR_Effect.sprite;
			}
			Num_Attack--;
			switch (Num_Attack)
			{
			case 9:
				if (!onMagic)
				{
					On_Col_Attack(1);
				}
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[0];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Attack[10];
				}
				else
				{
					SR.sprite = Spr_Attack[0];
				}
				SR_Effect.sprite = Spr_Effect_1[0];
				break;
			case 8:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[1];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Attack[11];
				}
				else
				{
					SR.sprite = Spr_Attack[1];
				}
				if (!onMagic)
				{
					SR_Effect.sprite = Spr_Effect_1[1];
				}
				break;
			case 7:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[2];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Attack[12];
				}
				else
				{
					SR.sprite = Spr_Attack[2];
				}
				if (!onMagic)
				{
					SR_Effect.sprite = Spr_Effect_1[2];
				}
				break;
			case 6:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[3];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Attack[13];
				}
				else
				{
					SR.sprite = Spr_Attack[3];
				}
				if (!onMagic)
				{
					SR_Effect.sprite = Spr_Effect_1[3];
				}
				break;
			case 5:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[4];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Attack[14];
				}
				else
				{
					SR.sprite = Spr_Attack[4];
				}
				if (!onMagic)
				{
					SR_Effect.sprite = Spr_Effect_1[4];
				}
				Off_Col_Attack();
				break;
			case 4:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[5];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Attack[15];
				}
				else
				{
					SR.sprite = Spr_Attack[5];
				}
				if (!onMagic)
				{
					SR_Effect.sprite = Spr_Effect_1[0];
				}
				break;
			case 3:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[6];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Attack[16];
				}
				else
				{
					SR.sprite = Spr_Attack[6];
				}
				break;
			case 2:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[7];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Attack[17];
				}
				else
				{
					SR.sprite = Spr_Attack[7];
				}
				break;
			case 1:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[8];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Attack[18];
				}
				else
				{
					SR.sprite = Spr_Attack[8];
				}
				break;
			case 0:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[9];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Attack[19];
				}
				else
				{
					SR.sprite = Spr_Attack[9];
				}
				break;
			}
			if (Num_Attack < 0)
			{
				SR_Effect.sprite = Spr_Effect_1[0];
				PC.onAttack = false;
				onAttack = false;
				onAttack_2 = false;
				onAttack_3 = false;
				onAttack_4 = false;
				onMagic = false;
				Attack_Delay = 0.5f;
				Ani_Index = 1;
				Ani_Timer = 1f;
				Off_Col_Attack();
			}
		}
		else
		{
			Ani_Timer += global::UnityEngine.Time.deltaTime;
		}
	}

	private void Ani_Attack_Idle_2()
	{
		if (Ani_Timer > 0.02f)
		{
			Ani_Timer = 0f;
			if (GM.Weapon_Num == 5)
			{
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Atk_Lag, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
				gameObject.transform.parent = base.transform;
				gameObject.transform.localScale = new global::UnityEngine.Vector3(2f, 2f, 1f);
				gameObject.GetComponent<global::UnityEngine.SpriteRenderer>().sprite = SR_Effect.sprite;
			}
			Num_Attack--;
			switch (Num_Attack)
			{
			case 7:
				On_Col_Attack(2);
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[10];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Attack_2[7];
				}
				else
				{
					SR.sprite = Spr_Attack_2[0];
				}
				SR_Effect.sprite = Spr_Effect_1[0];
				PC.Sound_Attack();
				break;
			case 6:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[11];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Attack_2[8];
				}
				else
				{
					SR.sprite = Spr_Attack_2[1];
				}
				SR_Effect.sprite = Spr_Effect_1[5];
				break;
			case 5:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[12];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Attack_2[9];
				}
				else
				{
					SR.sprite = Spr_Attack_2[2];
				}
				SR_Effect.sprite = Spr_Effect_1[6];
				break;
			case 4:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[13];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Attack_2[10];
				}
				else
				{
					SR.sprite = Spr_Attack_2[3];
				}
				SR_Effect.sprite = Spr_Effect_1[7];
				break;
			case 3:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[14];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Attack_2[11];
				}
				else
				{
					SR.sprite = Spr_Attack_2[4];
				}
				SR_Effect.sprite = Spr_Effect_1[8];
				break;
			case 2:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[15];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Attack_2[12];
				}
				else
				{
					SR.sprite = Spr_Attack_2[5];
				}
				SR_Effect.sprite = Spr_Effect_1[9];
				Off_Col_Attack();
				break;
			case 1:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[16];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Attack_2[13];
				}
				else
				{
					SR.sprite = Spr_Attack_2[6];
				}
				SR_Effect.sprite = Spr_Effect_1[0];
				break;
			case 0:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_2[17];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Sit[17];
				}
				else
				{
					SR.sprite = Spr_Sit[8];
				}
				break;
			}
			if (Num_Attack < 2 && onAttack_3)
			{
				onAttack_2 = false;
				Num_Attack = 22;
			}
			else if (Num_Attack < 0)
			{
				PC.onAttack = false;
				onAttack = false;
				onAttack_2 = false;
				onAttack_3 = false;
				onAttack_4 = false;
				onMagic = false;
				Attack_Delay = 0.5f;
				Ani_Index = 14;
				Ani_Timer = 1f;
				Off_Col_Attack();
			}
		}
		else
		{
			Ani_Timer += global::UnityEngine.Time.deltaTime;
		}
	}

	private void Ani_Attack_Idle_3()
	{
		if (Ani_Timer > 0.03f)
		{
			Ani_Timer = 0f;
			Num_Attack--;
			switch (Num_Attack)
			{
			case 21:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_5[0];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Attack_4[10];
				}
				else
				{
					SR.sprite = Spr_Attack_4[0];
				}
				SR_Effect.sprite = Spr_Effect_1[10];
				break;
			case 20:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_5[1];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Attack_4[11];
				}
				else
				{
					SR.sprite = Spr_Attack_4[1];
				}
				SR_Effect.sprite = Spr_Effect_1[11];
				break;
			case 19:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_5[2];
				}
				else
				{
					SR.sprite = Spr_Attack_4[2];
				}
				PC.Atk3_Jump();
				PC.Sound_Attack();
				Atk3_Timer = 0.18f;
				SR_Effect.sprite = Spr_Effect_1[12];
				break;
			case 18:
				On_Col_Attack(3);
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_5[3];
				}
				else
				{
					SR.sprite = Spr_Attack_4[3];
				}
				SR_Effect.sprite = Spr_Effect_1[13];
				break;
			case 17:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_5[4];
				}
				else
				{
					SR.sprite = Spr_Attack_4[4];
				}
				SR_Effect.sprite = Spr_Effect_1[14];
				break;
			case 16:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_5[5];
				}
				else
				{
					SR.sprite = Spr_Attack_4[5];
				}
				SR_Effect.sprite = Spr_Effect_1[15];
				break;
			case 15:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_5[6];
				}
				else
				{
					SR.sprite = Spr_Attack_4[6];
				}
				SR_Effect.sprite = Spr_Effect_1[16];
				break;
			case 14:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_5[7];
				}
				else
				{
					SR.sprite = Spr_Attack_4[7];
				}
				SR_Effect.sprite = Spr_Effect_1[17];
				break;
			case 13:
				if (!GM.onCloth)
				{
					if (State != Player_Ani.AniState.Jump)
					{
						SR.sprite = Spr_Naked_5[8];
					}
					else
					{
						SR.sprite = Spr_Naked_5[17];
					}
				}
				else if (State != Player_Ani.AniState.Jump)
				{
					SR.sprite = Spr_Attack_4[19];
				}
				else
				{
					SR.sprite = Spr_Attack_4[8];
				}
				SR_Effect.sprite = Spr_Effect_1[18];
				Off_Col_Attack();
				break;
			case 12:
				if (!GM.onCloth)
				{
					if (State != Player_Ani.AniState.Jump)
					{
						SR.sprite = Spr_Naked_5[9];
					}
					else
					{
						SR.sprite = Spr_Naked_5[18];
					}
				}
				else if (State != Player_Ani.AniState.Jump)
				{
					SR.sprite = Spr_Attack_4[20];
				}
				else
				{
					SR.sprite = Spr_Attack_4[9];
				}
				SR_Effect.sprite = Spr_Effect_1[0];
				break;
			case 11:
				if (!onAttack_4)
				{
					PC.onAttack = false;
					onAttack = false;
					onAttack_2 = false;
					onAttack_3 = false;
					onAttack_4 = false;
					onMagic = false;
					Attack_Delay = 0.5f;
					if (State != Player_Ani.AniState.Jump)
					{
						Ani_Index = 15;
						Ani_Timer = 1f;
						Num_JumpShort = 6;
					}
					else
					{
						Ani_Index = 14;
						Ani_Timer = 1f;
					}
				}
				break;
			}
			if (Num_Attack < 15 && onAttack_4)
			{
				onAttack_3 = false;
				Num_Attack = 10;
				Off_Col_Attack();
			}
			else if (Num_Attack < 11)
			{
				PC.onAttack = false;
				onAttack = false;
				onAttack_2 = false;
				onAttack_3 = false;
				onAttack_4 = false;
				onMagic = false;
				Attack_Delay = 0.5f;
				Ani_Index = 15;
				Ani_Timer = 1f;
				Num_JumpShort = 6;
				Off_Col_Attack();
			}
			if (GM.Weapon_Num == 5)
			{
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Atk_Lag, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
				gameObject.transform.parent = base.transform;
				gameObject.transform.localScale = new global::UnityEngine.Vector3(2f, 2f, 1f);
				gameObject.GetComponent<global::UnityEngine.SpriteRenderer>().sprite = SR_Effect.sprite;
			}
		}
		else
		{
			Ani_Timer += global::UnityEngine.Time.deltaTime;
		}
	}

	private void Ani_Attack_Idle_4()
	{
		if (Ani_Timer > 0.017f)
		{
			Ani_Timer = 0f;
			Num_Attack--;
			switch (Num_Attack)
			{
			case 10:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_5[10];
				}
				else
				{
					SR.sprite = Spr_Attack_4[12];
				}
				if (SR_Effect.sprite != Spr_Effect_1[0])
				{
					SR_Effect.sprite = Spr_Effect_1[17];
				}
				else
				{
					SR_Effect.sprite = Spr_Effect_1[0];
				}
				Off_Col_Attack();
				break;
			case 9:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_5[11];
				}
				else
				{
					SR.sprite = Spr_Attack_4[13];
				}
				PC.Atk4_Force();
				if (SR_Effect.sprite != Spr_Effect_1[0])
				{
					SR_Effect.sprite = Spr_Effect_1[18];
				}
				break;
			case 8:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_5[12];
				}
				else
				{
					SR.sprite = Spr_Attack_4[14];
				}
				SR_Effect.sprite = Spr_Effect_1[19];
				if (Atk3_Timer <= 0f)
				{
					PC.Sound_Attack();
				}
				break;
			case 7:
				On_Col_Attack(4);
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_5[13];
				}
				else
				{
					SR.sprite = Spr_Attack_4[15];
				}
				SR_Effect.sprite = Spr_Effect_1[20];
				break;
			case 6:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_5[14];
				}
				else
				{
					SR.sprite = Spr_Attack_4[16];
				}
				SR_Effect.sprite = Spr_Effect_1[21];
				break;
			case 5:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_5[15];
				}
				else
				{
					SR.sprite = Spr_Attack_4[17];
				}
				SR_Effect.sprite = Spr_Effect_1[22];
				break;
			case 4:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_5[16];
				}
				else
				{
					SR.sprite = Spr_Attack_4[18];
				}
				SR_Effect.sprite = Spr_Effect_1[23];
				break;
			case 3:
				if (State != Player_Ani.AniState.Jump)
				{
					PC.onAttack = false;
					onAttack = false;
					onAttack_2 = false;
					onAttack_3 = false;
					onAttack_4 = false;
					onMagic = false;
					Attack_Delay = 0.5f;
					Ani_Index = 15;
					Ani_Timer = 1f;
					Num_JumpShort = 5;
				}
				Off_Col_Attack();
				SR_Effect.sprite = Spr_Effect_1[24];
				break;
			case 2:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[19];
				}
				else
				{
					SR.sprite = Spr_Attack_4[21];
				}
				SR_Effect.sprite = Spr_Effect_1[0];
				break;
			case 1:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[20];
				}
				else
				{
					SR.sprite = Spr_Attack_4[22];
				}
				Off_Col_Attack();
				break;
			case 0:
				PC.onAttack = false;
				onAttack = false;
				onAttack_2 = false;
				onAttack_3 = false;
				onAttack_4 = false;
				onMagic = false;
				Attack_Delay = 0.5f;
				if (State == Player_Ani.AniState.Jump)
				{
					Ani_Index = 16;
					Ani_Timer = 1f;
				}
				else if (State != Player_Ani.AniState.Idle)
				{
				}
				SR_Effect.sprite = Spr_Effect_1[0];
				Off_Col_Attack();
				break;
			}
			if (Num_Attack < 3 && State != Player_Ani.AniState.Jump)
			{
				PC.onAttack = false;
				onAttack = false;
				onAttack_2 = false;
				onAttack_3 = false;
				onAttack_4 = false;
				onMagic = false;
				Attack_Delay = 0.5f;
				Ani_Index = 15;
				Ani_Timer = 1f;
				Num_JumpShort = 5;
				Off_Col_Attack();
			}
			else if (Num_Attack < 0)
			{
				PC.onAttack = false;
				onAttack = false;
				onAttack_2 = false;
				onAttack_3 = false;
				onAttack_4 = false;
				onMagic = false;
				Attack_Delay = 0.5f;
				Ani_Index = 15;
				Ani_Timer = 1f;
				Num_JumpShort = 6;
			}
			if (GM.Weapon_Num == 5)
			{
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Atk_Lag, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
				gameObject.transform.parent = base.transform;
				gameObject.transform.localScale = new global::UnityEngine.Vector3(2f, 2f, 1f);
				gameObject.GetComponent<global::UnityEngine.SpriteRenderer>().sprite = SR_Effect.sprite;
			}
		}
		else
		{
			Ani_Timer += global::UnityEngine.Time.deltaTime;
		}
	}

	private void Ani_Attack_Jump_1()
	{
		if (Ani_Timer > 0.02f)
		{
			Ani_Timer = 0f;
			if (GM.Weapon_Num == 5)
			{
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Atk_Lag, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
				gameObject.transform.parent = base.transform;
				gameObject.transform.localScale = new global::UnityEngine.Vector3(2f, 2f, 1f);
				gameObject.GetComponent<global::UnityEngine.SpriteRenderer>().sprite = SR_Effect.sprite;
			}
			Num_Attack--;
			switch (Num_Attack)
			{
			case 9:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[0];
				}
				else
				{
					SR.sprite = Spr_Attack[20];
				}
				SR_Effect.sprite = Spr_Effect_1[0];
				break;
			case 8:
				if (!onMagic)
				{
					On_Col_Attack(1);
				}
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[1];
				}
				else
				{
					SR.sprite = Spr_Attack[1];
				}
				if (!onMagic)
				{
					SR_Effect.sprite = Spr_Effect_1[1];
				}
				break;
			case 7:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[2];
				}
				else
				{
					SR.sprite = Spr_Attack[2];
				}
				if (!onMagic)
				{
					SR_Effect.sprite = Spr_Effect_1[2];
				}
				break;
			case 6:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[3];
				}
				else
				{
					SR.sprite = Spr_Attack[3];
				}
				if (!onMagic)
				{
					SR_Effect.sprite = Spr_Effect_1[3];
				}
				break;
			case 5:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[4];
				}
				else
				{
					SR.sprite = Spr_Attack[4];
				}
				if (!onMagic)
				{
					SR_Effect.sprite = Spr_Effect_1[4];
				}
				Off_Col_Attack();
				break;
			case 4:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[5];
				}
				else
				{
					SR.sprite = Spr_Attack[21];
				}
				if (!onMagic)
				{
					SR_Effect.sprite = Spr_Effect_1[0];
				}
				break;
			case 3:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[17];
				}
				else
				{
					SR.sprite = Spr_Attack[22];
				}
				break;
			case 2:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[18];
				}
				else
				{
					SR.sprite = Spr_Attack[23];
				}
				break;
			case 1:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[19];
				}
				else
				{
					SR.sprite = Spr_Attack[24];
				}
				break;
			case 0:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[20];
				}
				else
				{
					SR.sprite = Spr_Attack[25];
				}
				break;
			}
			if (Num_Attack < 0)
			{
				SR_Effect.sprite = Spr_Effect_1[0];
				PC.onAttack = false;
				onAttack_2 = false;
				onAttack = false;
				Attack_Delay = 0.5f;
				Ani_Index = 2;
				Ani_Timer = 1f;
				onMagic = false;
				Off_Col_Attack();
			}
		}
		else
		{
			Ani_Timer += global::UnityEngine.Time.deltaTime;
		}
	}

	private void Ani_Attack_Jump_2()
	{
		if (Ani_Timer > 0.02f)
		{
			Ani_Timer = 0f;
			if (GM.Weapon_Num == 5)
			{
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Atk_Lag, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
				gameObject.transform.parent = base.transform;
				gameObject.transform.localScale = new global::UnityEngine.Vector3(2f, 2f, 1f);
				gameObject.GetComponent<global::UnityEngine.SpriteRenderer>().sprite = SR_Effect.sprite;
			}
			Num_Attack--;
			switch (Num_Attack)
			{
			case 7:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[10];
				}
				else
				{
					SR.sprite = Spr_Attack_2[0];
				}
				SR_Effect.sprite = Spr_Effect_1[0];
				PC.Sound_Attack();
				break;
			case 6:
				On_Col_Attack(2);
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[11];
				}
				else
				{
					SR.sprite = Spr_Attack_2[1];
				}
				SR_Effect.sprite = Spr_Effect_1[5];
				break;
			case 5:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[12];
				}
				else
				{
					SR.sprite = Spr_Attack_2[2];
				}
				SR_Effect.sprite = Spr_Effect_1[6];
				break;
			case 4:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[13];
				}
				else
				{
					SR.sprite = Spr_Attack_2[3];
				}
				SR_Effect.sprite = Spr_Effect_1[7];
				break;
			case 3:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[21];
				}
				else
				{
					SR.sprite = Spr_Attack_2[14];
				}
				SR_Effect.sprite = Spr_Effect_1[8];
				break;
			case 2:
				Off_Col_Attack();
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[22];
				}
				else
				{
					SR.sprite = Spr_Attack_2[15];
				}
				SR_Effect.sprite = Spr_Effect_1[0];
				break;
			case 1:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[23];
				}
				else
				{
					SR.sprite = Spr_Attack_2[16];
				}
				break;
			}
			if (Num_Attack < 1)
			{
				SR_Effect.sprite = Spr_Effect_1[0];
				PC.onAttack = false;
				onAttack_2 = false;
				onAttack = false;
				Attack_Delay = 0.5f;
				Ani_Index = 2;
				Ani_Timer = 1f;
				Off_Col_Attack();
			}
		}
		else
		{
			Ani_Timer += global::UnityEngine.Time.deltaTime;
		}
	}

	private void Ani_Attack_Jump_Down()
	{
		if (Ani_Timer > 0.02f)
		{
			Ani_Timer = 0f;
			Num_Attack--;
			switch (Num_Attack)
			{
			case 109:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[24];
				}
				else
				{
					SR.sprite = Spr_Attack_2[17];
				}
				SR_Effect.sprite = Spr_Effect_1[0];
				break;
			case 108:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[25];
				}
				else
				{
					SR.sprite = Spr_Attack_2[18];
				}
				break;
			case 107:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[26];
				}
				else
				{
					SR.sprite = Spr_Attack_2[19];
				}
				break;
			case 106:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[27];
				}
				else
				{
					SR.sprite = Spr_Attack_2[20];
				}
				break;
			case 105:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[20];
				}
				else
				{
					SR.sprite = Spr_Attack[25];
				}
				break;
			}
			if (Num_Attack < 105)
			{
				PC.onAttack = false;
				onAttack_2 = false;
				onAttack = false;
				Attack_Delay = 0.5f;
				Ani_Index = 2;
				Ani_Timer = 1f;
			}
		}
		else
		{
			Ani_Timer += global::UnityEngine.Time.deltaTime;
		}
	}

	private void Ani_Attack_Jump_Down2()
	{
		if (Ani_Timer > 0.02f)
		{
			Ani_Timer = 0f;
			Num_Attack--;
			switch (Num_Attack)
			{
			case 209:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[28];
				}
				else
				{
					SR.sprite = Spr_Idle[20];
				}
				SR_Effect.sprite = Spr_Effect_1[0];
				break;
			case 208:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[29];
				}
				else
				{
					SR.sprite = Spr_Idle[21];
				}
				break;
			case 207:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[30];
				}
				else
				{
					SR.sprite = Spr_Idle[22];
				}
				break;
			case 206:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_4[31];
				}
				else
				{
					SR.sprite = Spr_Idle[23];
				}
				break;
			}
			if (Num_Attack < 206)
			{
				PC.onAttack = false;
				onAttack_2 = false;
				onAttack = false;
				Attack_Delay = 0.5f;
				Ani_Index = 2;
				Ani_Timer = 1f;
			}
		}
		else
		{
			Ani_Timer += global::UnityEngine.Time.deltaTime;
		}
	}

	private void Ani_Attack_Sit()
	{
		if (Ani_Timer > 0.01f)
		{
			Ani_Timer = 0f;
			Num_Attack--;
			switch (Num_Attack)
			{
			case 9:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_6[0];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Attack_3[10];
				}
				else
				{
					SR.sprite = Spr_Attack_3[0];
				}
				SR_Effect.sprite = Spr_Effect_1[0];
				break;
			case 8:
				if (!onMagic)
				{
					On_Col_Attack(1);
				}
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_6[1];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Attack_3[11];
				}
				else
				{
					SR.sprite = Spr_Attack_3[1];
				}
				if (!onMagic)
				{
					SR_Effect.sprite = Spr_Effect_1[1];
				}
				break;
			case 7:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_6[2];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Attack_3[12];
				}
				else
				{
					SR.sprite = Spr_Attack_3[2];
				}
				if (!onMagic)
				{
					SR_Effect.sprite = Spr_Effect_1[2];
				}
				break;
			case 6:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_6[3];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Attack_3[13];
				}
				else
				{
					SR.sprite = Spr_Attack_3[3];
				}
				if (!onMagic)
				{
					SR_Effect.sprite = Spr_Effect_1[3];
				}
				break;
			case 5:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_6[4];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Attack_3[14];
				}
				else
				{
					SR.sprite = Spr_Attack_3[4];
				}
				if (!onMagic)
				{
					SR_Effect.sprite = Spr_Effect_1[4];
				}
				Off_Col_Attack();
				break;
			case 4:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_6[5];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Attack_3[15];
				}
				else
				{
					SR.sprite = Spr_Attack_3[5];
				}
				if (!onMagic)
				{
					SR_Effect.sprite = Spr_Effect_1[0];
				}
				break;
			case 3:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_6[6];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Attack_3[16];
				}
				else
				{
					SR.sprite = Spr_Attack_3[6];
				}
				break;
			case 2:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_6[7];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Attack_3[17];
				}
				else
				{
					SR.sprite = Spr_Attack_3[7];
				}
				break;
			case 1:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_6[8];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Attack_3[18];
				}
				else
				{
					SR.sprite = Spr_Attack_3[8];
				}
				break;
			case 0:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_6[9];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Attack_3[19];
				}
				else
				{
					SR.sprite = Spr_Attack_3[9];
				}
				break;
			}
			if (Num_Attack < 0)
			{
				SR_Effect.sprite = Spr_Effect_1[0];
				PC.onAttack = false;
				onAttack_2 = false;
				onAttack = false;
				Attack_Delay = 0.5f;
				Ani_Index = 7;
				Ani_Timer = 1f;
				onMagic = false;
				Off_Col_Attack();
			}
		}
		else
		{
			Ani_Timer += global::UnityEngine.Time.deltaTime;
		}
	}

	private void Ani_Attack_Spin()
	{
		if (Ani_Timer > 0.01f)
		{
			Ani_Timer = 0f;
			Ani_Index++;
			switch (Ani_Index)
			{
			case 1:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_2[21];
				}
				else
				{
					SR.sprite = Spr_Attack_3[20];
				}
				SR_Effect_Spin.sprite = Spr_Effect_1[26];
				PC.Sound_Spin();
				SR_Effect_SpinAfter.sprite = Spr_Jump[20];
				global::UnityEngine.GameObject.Find("Effect_Spin").transform.rotation = global::UnityEngine.Quaternion.Euler(0f, 0f, (float)global::UnityEngine.Random.Range(0, 8) * 0.1f);
				global::UnityEngine.GameObject.Find("Effect_SpinGlow").transform.rotation = global::UnityEngine.Quaternion.Euler(0f, 0f, (float)global::UnityEngine.Random.Range(0, 8) * 0.1f);
				if (GM.Weapon_Num == 5)
				{
					global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Spin_Lag, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
				}
				break;
			case 2:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_2[22];
				}
				else
				{
					SR.sprite = Spr_Attack_3[21];
				}
				SR_Effect_Spin.sprite = Spr_Effect_1[27];
				SR_Effect_SpinAfter.sprite = Spr_Jump[21];
				break;
			case 3:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_2[23];
				}
				else
				{
					SR.sprite = Spr_Attack_3[22];
				}
				SR_Effect_Spin.sprite = Spr_Effect_1[28];
				SR_Effect_SpinAfter.sprite = Spr_Jump[22];
				break;
			case 4:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_2[24];
				}
				else
				{
					SR.sprite = Spr_Attack_3[23];
				}
				SR_Effect_Spin.sprite = Spr_Effect_1[29];
				SR_Effect_SpinAfter.sprite = Spr_Jump[23];
				Ani_Index = 0;
				break;
			}
		}
		else
		{
			Ani_Timer += global::UnityEngine.Time.deltaTime;
		}
	}

	private void Ani_Spin_End()
	{
		if (SpinEnd_Timer > 0.015f)
		{
			SpinEnd_Timer = 0f;
			Num_SpinEnd--;
			switch (Num_SpinEnd)
			{
			case 2:
				SR_Effect_SpinGlow.sprite = Spr_Effect_1[30];
				break;
			case 1:
				SR_Effect_SpinGlow.sprite = Spr_Effect_1[31];
				break;
			case 0:
				SR_Effect_SpinGlow.sprite = Spr_Effect_1[0];
				break;
			}
			if (GM.Weapon_Num == 5 && Num_SpinEnd > 0)
			{
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Spin_Lag, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
				gameObject.GetComponent<global::UnityEngine.SpriteRenderer>().sprite = SR_Effect_SpinGlow.sprite;
			}
		}
		else
		{
			SpinEnd_Timer += global::UnityEngine.Time.deltaTime;
		}
	}

	private void Ani_Slide()
	{
		if (Ani_Timer > 0.05f)
		{
			Ani_Timer = 0f;
			Ani_Index++;
			switch (Ani_Index)
			{
			case 1:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_5[21];
				}
				else
				{
					SR.sprite = Spr_Attack_4[23];
				}
				break;
			case 2:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_5[22];
				}
				else
				{
					SR.sprite = Spr_Attack_4[24];
				}
				break;
			case 3:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_5[23];
				}
				else
				{
					SR.sprite = Spr_Attack_4[25];
				}
				break;
			case 4:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_5[24];
				}
				else
				{
					SR.sprite = Spr_Attack_4[26];
				}
				break;
			case 5:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_2[15];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Sit[15];
				}
				else
				{
					SR.sprite = Spr_Sit[6];
				}
				break;
			case 6:
				break;
			}
		}
		else
		{
			Ani_Timer += global::UnityEngine.Time.deltaTime;
		}
	}

	private void Ani_Slide_To_Jump()
	{
		if (Ani_Timer > 0.1f)
		{
			Ani_Timer = 0f;
			Num_SlideJump--;
			switch (Num_SlideJump)
			{
			case 1:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_5[25];
				}
				else
				{
					SR.sprite = Spr_Attack_4[27];
				}
				break;
			case 0:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_5[26];
				}
				else
				{
					SR.sprite = Spr_Attack_4[28];
				}
				break;
			}
		}
		else
		{
			Ani_Timer += global::UnityEngine.Time.deltaTime;
		}
	}

	private void Ani_BackDash()
	{
		if (Ani_Timer > 0.15f)
		{
			Ani_Timer = 0f;
			if (!GM.onCloth)
			{
				SR.sprite = Spr_Naked_5[28];
			}
			else if (onEdge)
			{
				SR.sprite = Spr_Attack_3[30];
			}
			else
			{
				SR.sprite = Spr_Attack_3[29];
			}
		}
		else
		{
			Ani_Timer += global::UnityEngine.Time.deltaTime;
		}
	}

	private void Ani_Damage()
	{
		if (Ani_Timer > 0.03f)
		{
			Ani_Timer = 0f;
			Num_Damage++;
			switch (Num_Damage)
			{
			case 1:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_3[20];
				}
				else
				{
					SR.sprite = Spr_Jump[17];
				}
				if (ClothOpacity > 0f)
				{
					SR_ClothOff.sprite = Spr_Attack_4[29];
				}
				SR_ClothGlow.transform.localPosition = new global::UnityEngine.Vector3(-0.522f, 3.795f, 0f);
				break;
			case 2:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_3[21];
				}
				else
				{
					SR.sprite = Spr_Jump[18];
				}
				if (ClothOpacity > 0f)
				{
					SR_ClothOff.sprite = Spr_Attack_4[30];
				}
				break;
			case 3:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_3[22];
				}
				else
				{
					SR.sprite = Spr_Sit[24];
				}
				if (ClothOpacity > 0f)
				{
					SR_ClothOff.sprite = Spr_Attack_4[31];
				}
				break;
			case 4:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_3[23];
				}
				else
				{
					SR.sprite = Spr_Sit[25];
				}
				if (ClothOpacity > 0f)
				{
					SR_ClothOff.sprite = Spr_Attack_4[32];
				}
				break;
			case 5:
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_3[24];
				}
				else if (onEdge)
				{
					SR.sprite = Spr_Sit[26];
				}
				else
				{
					SR.sprite = Spr_Jump[19];
				}
				if (ClothOpacity > 0f)
				{
					SR_ClothOff.sprite = Spr_Attack_4[33];
					ClothOpacity = 0.1f;
				}
				break;
			case 8:
				break;
			case 6:
			case 7:
				break;
			}
		}
		else
		{
			Ani_Timer += global::UnityEngine.Time.deltaTime;
		}
	}

	private void Ani_Down()
	{
		if (Num_Down < 10)
		{
			if (Ani_Timer > 0.07f)
			{
				Ani_Timer = 0f;
				if (GM.onFaceHugger && !SR_FaceHugger.enabled)
				{
					SR_FaceHugger.enabled = true;
				}
				if (Num_Down < 9)
				{
					Num_Down++;
				}
				switch (Num_Down)
				{
				case 1:
					if (!GM.onCloth)
					{
						SR.sprite = Spr_Naked_6[10];
						SR_FaceHugger.sprite = Spr_Naked_6[18];
					}
					else
					{
						SR.sprite = Spr_Attack_3[36];
					}
					if (ClothOpacity > 0f)
					{
						SR_ClothOff.sprite = Spr_Attack_3[32];
					}
					SR_ClothGlow.transform.localPosition = new global::UnityEngine.Vector3(-1.029f, 3.699f, 0f);
					break;
				case 4:
					if (!GM.onCloth)
					{
						SR.sprite = Spr_Naked_6[11];
						SR_FaceHugger.sprite = Spr_Naked_6[19];
					}
					else
					{
						SR.sprite = Spr_Attack_3[37];
					}
					if (ClothOpacity > 0f)
					{
						SR_ClothOff.sprite = Spr_Attack_3[33];
					}
					SR_ClothGlow.transform.localPosition = new global::UnityEngine.Vector3(-1.405f, 2.922f, 0f);
					break;
				case 9:
					if (!GM.onCloth)
					{
						SR.sprite = Spr_Naked_6[12];
						SR_FaceHugger.sprite = Spr_Naked_6[20];
					}
					else
					{
						SR.sprite = Spr_Attack_3[38];
					}
					if (ClothOpacity > 0f)
					{
						SR_ClothOff.sprite = Spr_Attack_3[34];
					}
					SR_ClothGlow.transform.localPosition = new global::UnityEngine.Vector3(-1.79f, 1.49f, 0f);
					break;
				}
			}
			else
			{
				Ani_Timer += global::UnityEngine.Time.deltaTime;
			}
		}
		else
		{
			if (Num_Down != 10)
			{
				return;
			}
			if (Ani_Timer > 0.08f)
			{
				Ani_Timer = 0f;
				Num_Down++;
				if (!GM.onCloth)
				{
					SR.sprite = Spr_Naked_6[12];
					SR_FaceHugger.sprite = Spr_Naked_6[20];
				}
				else
				{
					SR.sprite = Spr_Attack_3[38];
				}
				if (ClothOpacity > 0f)
				{
					SR_ClothOff.sprite = Spr_Attack_3[34];
				}
				SR_ClothGlow.transform.localPosition = new global::UnityEngine.Vector3(-1.79f, 1.49f, 0f);
			}
			else
			{
				Ani_Timer += global::UnityEngine.Time.deltaTime;
			}
		}
	}

	private void Ani_ClothOpacity()
	{
		ClothOpacity -= global::UnityEngine.Time.deltaTime * 1f;
		if (ClothOpacity < 0f)
		{
			ClothOpacity = 0f;
		}
		SR_ClothOff.color = new global::UnityEngine.Color(SR_ClothOff.color.r, SR_ClothOff.color.g, SR_ClothOff.color.b, ClothOpacity);
		SR_ClothGlow.color = new global::UnityEngine.Color(SR_ClothGlow.color.r, SR_ClothGlow.color.g, SR_ClothGlow.color.b, ClothOpacity * 0.3f);
		if (ClothOpacity <= 0f)
		{
			SR_ClothOff.enabled = false;
			SR_ClothGlow.enabled = false;
		}
	}

	private void ON_H_Down()
	{
		on_H_Down = true;
		bool flip = ((PC.facingRight <= 0) ? true : false);
		Ctrl_Down.GetComponent<Puppet2D_GlobalControl>().flip = flip;
		H_Down.speed = 1f;
		SR.enabled = false;
		SR_Eye.enabled = false;
		SR_FaceHugger.enabled = false;
		if (GM.onFaceHugger)
		{
			Down_FaceHugger.enabled = true;
			Down_FaceHugger_L.enabled = true;
			Down_FaceHugger_R.enabled = true;
		}
		else
		{
			Down_FaceHugger.enabled = false;
			Down_FaceHugger_L.enabled = false;
			Down_FaceHugger_R.enabled = false;
		}
		H_Down.transform.position = PC.transform.position;
	}

	private void OFF_H_Down()
	{
		on_H_Down = false;
		H_Down.speed = 0f;
		SR.enabled = true;
		SR_Eye.enabled = true;
		H_Down.transform.position = new global::UnityEngine.Vector3(-30f, 8f, 0f);
	}

	private void Start_H_Scene()
	{
		H_Down.speed = 0f;
		SR.enabled = false;
		SR_Eye.enabled = false;
		SR_FaceHugger.enabled = false;
		H_Down.transform.position = new global::UnityEngine.Vector3(-30f, 8f, 0f);
		if (State != Player_Ani.AniState.Down)
		{
			Reset_Num();
			State = Player_Ani.AniState.Down;
			Ani_Index = 0;
			Ani_Timer = 1f;
			EyeClose_Delay = 3f;
			Num_EyeClose = 0;
			SR_Eye.sprite = null;
			Censored_Timer = 0f;
			Num_Down = 20;
			SR.sprite = Spr_Naked_6[13];
		}
	}

	private void End_H_Scene()
	{
		on_H_Down = true;
		Sperm_1.enabled = true;
		Sperm_2.enabled = true;
		bool flip = ((PC.facingRight <= 0) ? true : false);
		Ctrl_Down.GetComponent<Puppet2D_GlobalControl>().flip = flip;
		H_Down.speed = 1f;
		H_Down.transform.position = PC.transform.position;
	}

	private void Start_H_Grab()
	{
		H_Down.speed = 0f;
		SR.enabled = false;
		SR_Eye.enabled = false;
		SR_FaceHugger.enabled = false;
		H_Down.transform.position = new global::UnityEngine.Vector3(-30f, 8f, 0f);
	}

	private void Set_FaceHugger()
	{
		onFaceHugger = true;
		FaceHugger_Timer = 0f;
		Down_FaceHugger.enabled = true;
		Down_FaceHugger_L.enabled = true;
		Down_FaceHugger_R.enabled = true;
		Down_FaceHugger.material.color = new global::UnityEngine.Color(1f, 1f, 1f, 1f);
		Down_FaceHugger_L.material.color = Down_FaceHugger.material.color;
		Down_FaceHugger_R.material.color = Down_FaceHugger.material.color;
	}

	private void Reset_FaceHugger()
	{
		onFaceHugger = false;
		FaceHugger_Timer = 0f;
		Down_FaceHugger.enabled = false;
		Down_FaceHugger_L.enabled = false;
		Down_FaceHugger_R.enabled = false;
	}

	public void Set_Down_Bounce()
	{
		if (Num_Down < 20)
		{
			PC.Sound_Down();
			Num_Down = 10;
			Ani_Timer = 0f;
			if (!GM.onCloth)
			{
				SR.sprite = Spr_Naked_6[13];
				SR_FaceHugger.sprite = Spr_Naked_6[21];
			}
			else
			{
				SR.sprite = Spr_Attack_3[39];
			}
			if (ClothOpacity > 0f)
			{
				SR_ClothOff.sprite = Spr_Attack_3[35];
			}
			SR_ClothGlow.transform.localPosition = new global::UnityEngine.Vector3(-1.79f, 0.44f, 0f);
		}
	}

	public void Set_Down_Down()
	{
		if (Num_Down < 20)
		{
			PC.Sound_Down();
			Num_Down = 20;
			if (!GM.onCloth)
			{
				SR.sprite = Spr_Naked_6[13];
				SR_FaceHugger.sprite = Spr_Naked_6[21];
			}
			else
			{
				SR.sprite = Spr_Attack_3[39];
			}
			if (ClothOpacity > 0f)
			{
				SR_ClothOff.sprite = Spr_Attack_3[35];
			}
			SR_ClothGlow.transform.localPosition = new global::UnityEngine.Vector3(-1.79f, 0.44f, 0f);
		}
	}

	private void On_Col_Attack(int Atk_Num)
	{
		if (GM.Weapon_Num == 0)
		{
			global::UnityEngine.GameObject.Find("Col_Attack_0_" + Atk_Num).GetComponent<global::UnityEngine.PolygonCollider2D>().enabled = true;
		}
		else if (GM.Weapon_Num < 5)
		{
			if (Atk_Num == 3 || Atk_Num == 4)
			{
				global::UnityEngine.GameObject.Find("Col_Attack_" + Atk_Num).GetComponent<global::UnityEngine.PolygonCollider2D>().enabled = true;
			}
			else
			{
				global::UnityEngine.GameObject.Find("Col_Attack_" + ((GM.Weapon_Num != 3) ? GM.Weapon_Num : 2) + "_" + Atk_Num).GetComponent<global::UnityEngine.PolygonCollider2D>().enabled = true;
			}
		}
		else
		{
			global::UnityEngine.GameObject.Find("Col_Attack_5_" + Atk_Num).GetComponent<global::UnityEngine.PolygonCollider2D>().enabled = true;
		}
	}

	private void Off_Col_Attack()
	{
		global::UnityEngine.GameObject.Find("Col_Attack_0_1").GetComponent<global::UnityEngine.PolygonCollider2D>().enabled = false;
		global::UnityEngine.GameObject.Find("Col_Attack_0_2").GetComponent<global::UnityEngine.PolygonCollider2D>().enabled = false;
		global::UnityEngine.GameObject.Find("Col_Attack_0_3").GetComponent<global::UnityEngine.PolygonCollider2D>().enabled = false;
		global::UnityEngine.GameObject.Find("Col_Attack_0_4").GetComponent<global::UnityEngine.PolygonCollider2D>().enabled = false;
		global::UnityEngine.GameObject.Find("Col_Attack_1_1").GetComponent<global::UnityEngine.PolygonCollider2D>().enabled = false;
		global::UnityEngine.GameObject.Find("Col_Attack_1_2").GetComponent<global::UnityEngine.PolygonCollider2D>().enabled = false;
		global::UnityEngine.GameObject.Find("Col_Attack_2_1").GetComponent<global::UnityEngine.PolygonCollider2D>().enabled = false;
		global::UnityEngine.GameObject.Find("Col_Attack_2_2").GetComponent<global::UnityEngine.PolygonCollider2D>().enabled = false;
		global::UnityEngine.GameObject.Find("Col_Attack_4_1").GetComponent<global::UnityEngine.PolygonCollider2D>().enabled = false;
		global::UnityEngine.GameObject.Find("Col_Attack_4_2").GetComponent<global::UnityEngine.PolygonCollider2D>().enabled = false;
		global::UnityEngine.GameObject.Find("Col_Attack_3").GetComponent<global::UnityEngine.PolygonCollider2D>().enabled = false;
		global::UnityEngine.GameObject.Find("Col_Attack_4").GetComponent<global::UnityEngine.PolygonCollider2D>().enabled = false;
		global::UnityEngine.GameObject.Find("Col_Attack_5_1").GetComponent<global::UnityEngine.PolygonCollider2D>().enabled = false;
		global::UnityEngine.GameObject.Find("Col_Attack_5_2").GetComponent<global::UnityEngine.PolygonCollider2D>().enabled = false;
		global::UnityEngine.GameObject.Find("Col_Attack_5_3").GetComponent<global::UnityEngine.PolygonCollider2D>().enabled = false;
		global::UnityEngine.GameObject.Find("Col_Attack_5_4").GetComponent<global::UnityEngine.PolygonCollider2D>().enabled = false;
	}
}
