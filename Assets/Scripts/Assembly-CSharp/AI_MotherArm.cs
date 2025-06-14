using UnityEngine;

public class AI_MotherArm : global::UnityEngine.MonoBehaviour
{
	public AI_MotherBrain MotherBrain;

	public int HP = 100;

	public int HP_Max = 100;

	public int Damage = 30;

	public float DmgForce = 10f;

	public bool isDeath;

	private bool onHold = true;

	private float Hold_Timer = 200f;

	private float Life_Timer;

	private int facingRight = -1;

	private float Flip_Timer;

	private bool onAttack;

	private float Attack_Timer;

	private float Attack_Delay;

	private float distance;

	public bool onEvent = true;

	public bool Hit_Atk_1;

	public bool Hit_Atk_2;

	public bool Hit_Atk_3;

	public bool Hit_Atk_4;

	public bool Hit_Spin;

	public bool Hit_Rolling;

	public float Hit_Delay;

	public float Hit_Timer;

	public int Hit_Combo;

	public bool onPoisonSkill;

	public bool onPoisonWeapon;

	public bool onSlow;

	public float Gravity_Delay;

	public float Poison_Skill_Timer;

	public float Poison_Weapon_Timer;

	public float Toxic_Timer;

	public float Slow_Timer;

	public global::UnityEngine.GameObject[] Bomb_Object = new global::UnityEngine.GameObject[2];

	public float[] Bomb_Timer = new float[2];

	private bool onPauseGravity;

	public float PC_Col_Delay;

	public float PC_Body_Delay;

	public global::UnityEngine.GameObject Ctrl_1;

	public global::UnityEngine.Transform pos_Text;

	public global::UnityEngine.Transform pos_Text_P;

	public global::UnityEngine.GameObject Blood_Obj;

	public global::UnityEngine.GameObject sound_Awake;

	public global::UnityEngine.GameObject sound_Start;

	public global::UnityEngine.SpriteRenderer[] SR_Event;
    Player_Control PC => GameManager.instance?.PC;
    GameObject Player => GameManager.instance?.gobj_Player;

    GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//PC = global::UnityEngine.GameObject.Find("Player").GetComponent<Player_Control>();
		HP = HP_Max;
	}

	private void Update()
	{
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (Life_Timer > 2f && MotherBrain == null)
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
		if (onEvent)
		{
			return;
		}
		if (!GM.Paused)
		{
			if (onPauseGravity)
			{
				onPauseGravity = false;
				GetComponent<global::UnityEngine.Animator>().speed = 1f;
			}
			if (Hit_Delay > 0f)
			{
				Hit_Delay -= global::UnityEngine.Time.deltaTime;
			}
			if (PC_Col_Delay > 0f)
			{
				PC_Col_Delay -= global::UnityEngine.Time.deltaTime;
			}
			if (PC_Body_Delay > 0f)
			{
				PC_Body_Delay -= global::UnityEngine.Time.deltaTime;
			}
			if (Hit_Delay <= 0f && Hit_Atk_1)
			{
				Hit_Atk_1 = false;
			}
			if (Hit_Delay <= 0f && Hit_Atk_2)
			{
				Hit_Atk_2 = false;
			}
			if (Hit_Delay <= 0f && Hit_Atk_3)
			{
				Hit_Atk_3 = false;
			}
			if (Hit_Delay <= 0f && Hit_Atk_4)
			{
				Hit_Atk_4 = false;
			}
			if (Hit_Timer > 0f)
			{
				Hit_Timer -= global::UnityEngine.Time.deltaTime;
			}
			if (Hit_Timer <= 0f && Hit_Combo > 0)
			{
				Hit_Combo = 0;
			}
			if (Gravity_Delay > 0f)
			{
				Gravity_Delay -= global::UnityEngine.Time.deltaTime;
			}
			if (Toxic_Timer > 0f)
			{
				Toxic_Timer -= global::UnityEngine.Time.deltaTime;
			}
			if (Poison_Skill_Timer > 0f)
			{
				Poison_Skill_Timer -= global::UnityEngine.Time.deltaTime;
			}
			if (Poison_Weapon_Timer > 0f)
			{
				Poison_Weapon_Timer -= global::UnityEngine.Time.deltaTime;
			}
			if (Slow_Timer > 0f)
			{
				Slow_Timer -= global::UnityEngine.Time.deltaTime;
			}
			if (onPoisonSkill && !isDeath)
			{
				if (Poison_Skill_Timer > 0f)
				{
					if (Toxic_Timer <= 0f)
					{
						Check_Poison();
					}
				}
				else
				{
					onPoisonSkill = false;
					if (!onPoisonWeapon && onSlow)
					{
						GetComponent<Mon_Index>().On_MagicSlow();
					}
					else if (!onPoisonWeapon)
					{
						GetComponent<Mon_Index>().Off_Magic();
					}
				}
			}
			else if (onPoisonWeapon && !isDeath)
			{
				if (Poison_Weapon_Timer > 0f)
				{
					if (Toxic_Timer <= 0f)
					{
						Check_Poison();
					}
				}
				else
				{
					onPoisonWeapon = false;
					if (onSlow)
					{
						GetComponent<Mon_Index>().On_MagicSlow();
					}
					else
					{
						GetComponent<Mon_Index>().Off_Magic();
					}
				}
			}
			if (onSlow && !isDeath && Slow_Timer <= 0f)
			{
				onSlow = false;
				if (!onPoisonSkill && !onPoisonWeapon)
				{
					GetComponent<Mon_Index>().Off_Magic();
				}
			}
			if (Bomb_Timer[0] > 0f)
			{
				Bomb_Timer[0] -= global::UnityEngine.Time.deltaTime;
			}
			else if (Bomb_Object[0] != null)
			{
				Bomb_Object[0] = null;
			}
			if (Bomb_Timer[1] > 0f)
			{
				Bomb_Timer[1] -= global::UnityEngine.Time.deltaTime;
			}
			else if (Bomb_Object[1] != null)
			{
				Bomb_Object[1] = null;
			}
			distance = global::UnityEngine.Vector3.Distance(base.transform.position, PC.transform.position);
			if (!onHold && !isDeath && !GM.onHscene)
			{
				if (facingRight < 0 && PC.transform.position.x > base.transform.position.x)
				{
					Flip_Timer += global::UnityEngine.Time.deltaTime;
					if (Flip_Timer > 0.6f && !onAttack)
					{
						Flip();
					}
				}
				else if (facingRight > 0 && PC.transform.position.x < base.transform.position.x)
				{
					Flip_Timer += global::UnityEngine.Time.deltaTime;
					if (Flip_Timer > 0.6f && !onAttack)
					{
						Flip();
					}
				}
				else
				{
					Flip_Timer = 0f;
				}
				if (Attack_Delay > 0f)
				{
					Attack_Delay -= global::UnityEngine.Time.deltaTime;
				}
				else if (!GM.GameOver && distance < 27f)
				{
					if (PC.transform.position.y < base.transform.position.y + 4f)
					{
						Set_Attack();
					}
					else
					{
						Set_Attack_Upper();
					}
				}
			}
			if (onHold && Hold_Timer < 100f)
			{
				Hold_Timer -= global::UnityEngine.Time.deltaTime;
				if (Hold_Timer <= 0f && !MotherBrain.isDeath)
				{
					HP = HP_Max;
					Hold_Release();
				}
			}
		}
		else if (!onPauseGravity)
		{
			onPauseGravity = true;
			GetComponent<global::UnityEngine.Animator>().speed = 0f;
		}
	}

	public void Flip()
	{
		facingRight = -facingRight;
		Flip_Timer = 0f;
		bool flip = ((facingRight > 0) ? true : false);
		if (Ctrl_1 != null)
		{
			Ctrl_1.GetComponent<Puppet2D_GlobalControl>().flip = flip;
		}
	}

	private void Check_Poison()
	{
		global::UnityEngine.Vector3 pos_Font = new global::UnityEngine.Vector3(pos_Text_P.position.x + (float)global::UnityEngine.Random.Range(-50, 50) * 0.01f, pos_Text_P.position.y + (float)global::UnityEngine.Random.Range(0, 100) * 0.01f, base.transform.position.z);
		if (onPoisonSkill)
		{
			Toxic_Timer = 0.1f;
			HP -= GM.Get_PoisonDamage(pos_Font, 1);
		}
		else
		{
			Toxic_Timer = 0.2f;
			HP -= GM.Get_PoisonDamage(pos_Font, 0);
		}
		if (HP > 0)
		{
		}
	}

	public void Damage_Hit(bool onSkill, int skillNum, float ratio)
	{
		if (!onHold && !isDeath)
		{
			global::UnityEngine.Vector3 pos_Font = new global::UnityEngine.Vector3(pos_Text.position.x + (float)global::UnityEngine.Random.Range(-80, 80) * 0.01f, pos_Text.position.y + (float)global::UnityEngine.Random.Range(0, 150) * 0.01f, base.transform.position.z);
			int num = 0;
			num = ((!onSkill) ? GM.Get_AtkDamage(pos_Font, ratio) : GM.Get_MagicDamage(pos_Font, skillNum));
			HP -= num;
			if (HP <= 0)
			{
				Death();
			}
			PC_Body_Delay = 0.5f;
			Hit_Combo++;
			Hit_Timer = 0.5f;
		}
	}

	private void Death()
	{
		isDeath = true;
		onHold = true;
		Hold_Timer = 10f;
		GetComponent<global::UnityEngine.Animator>().SetTrigger("onHit_Hold");
		GameManager.instance.sc_Sound_List.Mon_9_Damage(base.transform.position);
		Make_Blood();
	}

	private void Make_Blood()
	{
		for (int i = 0; i < GM.Bonus_Blood * 3; i++)
		{
			global::UnityEngine.Vector3 position = new global::UnityEngine.Vector3(base.transform.position.x + (float)(10 * facingRight) + (float)global::UnityEngine.Random.Range(-200, 200) * 0.01f, base.transform.position.y + (float)global::UnityEngine.Random.Range(-200, 200) * 0.01f, 0f);
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Blood_Obj, position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
		}
	}

	private void Set_Attack()
	{
		onAttack = false;
		Attack_Delay = 3f;
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", true);
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_2", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit_Hold", false);
		GameManager.instance.sc_Sound_List.Mon_Atk_3(base.transform.position);
	}

	private void Set_Attack_Upper()
	{
		onAttack = false;
		Attack_Delay = 3f;
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_2", true);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit_Hold", false);
		GameManager.instance.sc_Sound_List.Mon_Atk_3(base.transform.position);
	}

	private void End_Attack()
	{
		onAttack = false;
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_2", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit_Hold", false);
	}

	private void Hold_Release()
	{
		isDeath = false;
		onHold = false;
		Hold_Timer = 0f;
		Attack_Delay = 1.5f;
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_2", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit_Hold", false);
		GetComponent<global::UnityEngine.Animator>().SetTrigger("onAwake");
		AxiSoundPool.AddSoundForPosRot(sound_Start, base.transform.position, base.transform.rotation);
		AxiSoundPool.AddSoundForPosRot(sound_Awake, base.transform.position, base.transform.rotation);
	}

	private void Hold()
	{
		onHold = true;
		Hold_Timer = 200f;
		GetComponent<global::UnityEngine.Animator>().SetTrigger("onHit_Hold");
	}

	private void Show_Event()
	{
		for (int i = 0; i < SR_Event.Length; i++)
		{
			SR_Event[i].enabled = true;
		}
	}

	private void Hide_Event()
	{
		for (int i = 0; i < SR_Event.Length; i++)
		{
			SR_Event[i].enabled = false;
		}
	}
}
