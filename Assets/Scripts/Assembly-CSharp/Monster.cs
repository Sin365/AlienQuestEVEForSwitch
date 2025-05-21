public class Monster : global::UnityEngine.MonoBehaviour
{
	public int Mon_Num = 1;

	public bool onEvent;

	public float Event_Timer;

	public int Event_Num;

	public int HP = 100;

	public int HP_Max = 100;

	public float HP_Ratio = 1f;

	public int Damage = 30;

	public float DmgForce = 10f;

	public bool isInvincible;

	public bool isLockHit;

	public bool isPass;

	public bool onCrouch;

	public int Gameover_Num;

	private float Gameover_Timer;

	public float Move_Speed = 1f;

	public int MagicFire_1_Num;

	private float MagicFire_1_Num_Timer;

	public int MagicFire_5_Num;

	private float MagicFire_5_Num_Timer;

	private bool isDeath;

	private int facingRight = -1;

	private bool Hit_Atk_1;

	private bool Hit_Atk_2;

	private bool Hit_Atk_3;

	private bool Hit_Atk_4;

	private bool Hit_Spin;

	private bool Hit_Rolling;

	private float Hit_Delay;

	private float Hit_Timer;

	private int Hit_Combo;

	private bool onKnockback;

	private float knockback_Timer;

	private bool onPoisonSkill;

	private bool onPoisonWeapon;

	private bool onSlow;

	private float Gravity_Delay;

	private float Poison_Skill_Timer;

	private float Poison_Weapon_Timer;

	private float Toxic_Timer;

	private float Slow_Timer;

	private float Poison_Smog_Timer;

	private global::UnityEngine.GameObject[] Bomb_Object = new global::UnityEngine.GameObject[2];

	private float[] Bomb_Timer = new float[2];

	private float MagicFire_1_Timer;

	private float MagicFire_3_Timer;

	private bool onShield_Lock;

	private float Shield_Force_Timer;

	private float Trap_Fang_Timer;

	private float Trap_Laser_Timer;

	private bool onPauseGravity;

	private global::UnityEngine.Vector2 Mon_Velocity = new global::UnityEngine.Vector2(0f, 0f);

	private float PC_Col_Delay;

	private float PC_Body_Delay;

	private float Invincible_Delay;

	private float QueenShield_Delay;

	private float QueenDash_Delay;

	public global::UnityEngine.GameObject Ctrl_1;

	public global::UnityEngine.GameObject Ctrl_2;

	public global::UnityEngine.GameObject Ctrl_3;

	public global::UnityEngine.SpriteRenderer HP_Bar_BG;

	public global::UnityEngine.SpriteRenderer HP_Bar;

	private bool onHpBar = true;

	public global::UnityEngine.GameObject Explo;

	public global::UnityEngine.GameObject explo_Pos_Root;

	public global::UnityEngine.GameObject explo_Pos_1;

	public global::UnityEngine.GameObject explo_Pos_2;

	public global::UnityEngine.GameObject explo_Pos_3;

	public global::UnityEngine.GameObject explo_Pos_4;

	public global::UnityEngine.GameObject explo_Pos_5;

	public global::UnityEngine.GameObject explo_Pos_6;

	public global::UnityEngine.Transform pos_Text;

	public global::UnityEngine.Transform pos_Text_P;

	public global::UnityEngine.GameObject Blood_Obj;

	public global::UnityEngine.GameObject _Icon;

	public global::UnityEngine.Sprite Icon_Spr;

	public global::UnityEngine.GameObject _Item_Potion_HP;

	public global::UnityEngine.GameObject _Item_Potion_MP;

	private global::UnityEngine.GameObject Player;

	private Sound_Control SC;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		SC = global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>();
		Player = global::UnityEngine.GameObject.Find("Player");
		if (Mon_Num != 33 && Event_Num > 0 && GM.Check_EventMonster(Event_Num))
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
		if (HP_Max < HP)
		{
			HP_Max = HP;
		}
		else if (HP < HP_Max)
		{
			HP = HP_Max;
		}
		if (HP_Bar_BG != null)
		{
			HP_Bar_BG.transform.localPosition = new global::UnityEngine.Vector3(HP_Bar_BG.transform.localPosition.x, HP_Bar_BG.transform.localPosition.y + global::UnityEngine.Random.Range(-0.1f, 0.12f), 0f);
			if (GM.Option_Int[4] != 1 || onEvent)
			{
				Off_HP_Bar();
			}
		}
		if (!onEvent)
		{
			Make_Icon();
		}
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			if (onPauseGravity)
			{
				onPauseGravity = false;
				if (!onEvent)
				{
					GetComponent<global::UnityEngine.Animator>().speed = 1f;
					GetComponent<global::UnityEngine.Rigidbody2D>().WakeUp();
					base.GetComponent<UnityEngine.Rigidbody2D>().velocity = Mon_Velocity;
				}
			}
			if (onEvent)
			{
				Event_Timer = 1.5f;
			}
			else if (Event_Timer > 0f)
			{
				Event_Timer -= global::UnityEngine.Time.deltaTime;
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
			if (Invincible_Delay > 0f)
			{
				Invincible_Delay -= global::UnityEngine.Time.deltaTime;
			}
			if (QueenShield_Delay > 0f)
			{
				QueenShield_Delay -= global::UnityEngine.Time.deltaTime;
			}
			if (QueenDash_Delay > 0f)
			{
				QueenDash_Delay -= global::UnityEngine.Time.deltaTime;
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
			if (knockback_Timer > 0f)
			{
				knockback_Timer -= global::UnityEngine.Time.deltaTime;
			}
			if (onKnockback && knockback_Timer <= 0f)
			{
				onKnockback = false;
				base.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(0f, 0f);
				knockback_Timer = 0f;
			}
			if (MagicFire_1_Num_Timer > 0f)
			{
				MagicFire_1_Num_Timer -= global::UnityEngine.Time.deltaTime;
			}
			else if (MagicFire_1_Num > 0)
			{
				MagicFire_1_Num = 0;
			}
			if (MagicFire_5_Num_Timer > 0f)
			{
				MagicFire_5_Num_Timer -= global::UnityEngine.Time.deltaTime;
			}
			else if (MagicFire_5_Num > 0)
			{
				MagicFire_5_Num = 0;
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
			if (Poison_Smog_Timer > 0f)
			{
				Poison_Smog_Timer -= global::UnityEngine.Time.deltaTime;
			}
			if (Slow_Timer > 0f)
			{
				Slow_Timer -= global::UnityEngine.Time.deltaTime;
			}
			if (MagicFire_1_Timer > 0f)
			{
				MagicFire_1_Timer -= global::UnityEngine.Time.deltaTime;
			}
			if (MagicFire_3_Timer > 0f)
			{
				MagicFire_3_Timer -= global::UnityEngine.Time.deltaTime;
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
				Move_Speed = 1f;
				if (!onPoisonSkill && !onPoisonWeapon)
				{
					GetComponent<Mon_Index>().Off_Magic();
				}
			}
			if (Trap_Fang_Timer > 0f)
			{
				Trap_Fang_Timer -= global::UnityEngine.Time.deltaTime;
			}
			if (Trap_Laser_Timer > 0f)
			{
				Trap_Laser_Timer -= global::UnityEngine.Time.deltaTime;
			}
			if (Shield_Force_Timer > 0f)
			{
				Shield_Force_Timer -= global::UnityEngine.Time.deltaTime;
			}
			else if (onShield_Lock)
			{
				onShield_Lock = false;
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
			if (Mon_Num == 55)
			{
				if (Player.transform.position.y > base.transform.position.y - 3f)
				{
					pos_Text.position = new global::UnityEngine.Vector3(pos_Text.position.x, base.transform.position.y + 1f, 0f);
					pos_Text_P.position = new global::UnityEngine.Vector3(pos_Text_P.position.x, base.transform.position.y + 1f, 0f);
				}
				else
				{
					pos_Text.position = new global::UnityEngine.Vector3(pos_Text.position.x, base.transform.position.y - 5.2f, 0f);
					pos_Text_P.position = new global::UnityEngine.Vector3(pos_Text_P.position.x, base.transform.position.y - 5.2f, 0f);
				}
			}
			if (HP_Bar != null)
			{
				if (GM.GameOver || GM.onHscene || Event_Timer > 0f)
				{
					if (onHpBar)
					{
						onHpBar = false;
						HP_Bar.enabled = false;
						HP_Bar_BG.enabled = false;
					}
				}
				else if (onHpBar)
				{
					HP_Bar.transform.localScale = new global::UnityEngine.Vector3(HP_Ratio, 1f, 1f);
					if (GM.Option_Int[4] != 1)
					{
						onHpBar = false;
						HP_Bar.enabled = false;
						HP_Bar_BG.enabled = false;
					}
				}
				else if (GM.Option_Int[4] == 1)
				{
					onHpBar = true;
					HP_Bar.enabled = true;
					HP_Bar_BG.enabled = true;
				}
			}
			if (base.transform.position.y < -500f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
		else if (!onPauseGravity)
		{
			onPauseGravity = true;
			GetComponent<global::UnityEngine.Animator>().speed = 0f;
			Mon_Velocity = base.GetComponent<UnityEngine.Rigidbody2D>().velocity;
			GetComponent<global::UnityEngine.Rigidbody2D>().Sleep();
		}
	}

	public void Flip()
	{
		facingRight = -facingRight;
		bool flip = ((facingRight > 0) ? true : false);
		if (Ctrl_1 != null)
		{
			Ctrl_1.GetComponent<Puppet2D_GlobalControl>().flip = flip;
		}
		if (Ctrl_2 != null)
		{
			Ctrl_2.GetComponent<Puppet2D_GlobalControl>().flip = flip;
		}
		if (Ctrl_3 != null)
		{
			Ctrl_3.GetComponent<Puppet2D_GlobalControl>().flip = flip;
		}
		explo_Pos_Root.transform.localScale = new global::UnityEngine.Vector3(-facingRight, 1f, 1f);
		if (HP_Bar_BG != null)
		{
			HP_Bar_BG.transform.localPosition = new global::UnityEngine.Vector3(HP_Bar_BG.transform.localPosition.x * -1f, HP_Bar_BG.transform.localPosition.y, 0f);
		}
	}

	public void XenoEvent_Set_FacingRight()
	{
		if (Ctrl_1 != null)
		{
			Ctrl_1.transform.rotation = global::UnityEngine.Quaternion.Euler(0f, 180f, 0f);
		}
	}

	public void Flip_Pos()
	{
		facingRight = -facingRight;
		explo_Pos_Root.transform.localScale = new global::UnityEngine.Vector3(-facingRight, 1f, 1f);
		if (HP_Bar_BG != null)
		{
			HP_Bar_BG.transform.localPosition = new global::UnityEngine.Vector3(HP_Bar_BG.transform.localPosition.x * -1f, HP_Bar_BG.transform.localPosition.y, 0f);
		}
	}

	private void End_Hit()
	{
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onMove", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
		if (Mon_Num == 54)
		{
			GetComponent<global::UnityEngine.Animator>().SetBool("onHit_Back", false);
		}
	}

	private void Check_Poison()
	{
		if (isDeath || HP <= 0 || isInvincible)
		{
			return;
		}
		global::UnityEngine.Vector3 pos_Font = new global::UnityEngine.Vector3(pos_Text_P.position.x + (float)global::UnityEngine.Random.Range(-50, 50) * 0.01f, pos_Text_P.position.y + (float)global::UnityEngine.Random.Range(0, 100) * 0.01f, 0f);
		if (onPoisonSkill)
		{
			if (Poison_Smog_Timer > 0f)
			{
				Toxic_Timer = 0.1f;
			}
			else
			{
				Toxic_Timer = 0.2f;
			}
			if (Mon_Num == 39 || Mon_Num == 53)
			{
				HP -= GM.Get_PoisonDamage(pos_Font, 39);
			}
			else
			{
				HP -= GM.Get_PoisonDamage(pos_Font, 1);
			}
		}
		else
		{
			Toxic_Timer = 0.2f;
			if (Mon_Num == 39 || Mon_Num == 53)
			{
				HP -= GM.Get_PoisonDamage(pos_Font, 39);
			}
			else
			{
				HP -= GM.Get_PoisonDamage(pos_Font, 0);
			}
		}
		if (HP <= 0)
		{
			if (!isDeath)
			{
				Death();
			}
			HP_Ratio = 0f;
		}
		else
		{
			HP_Ratio = (float)HP / (float)HP_Max;
		}
	}

	private void Damage_Hit(bool onSkill, int skillNum, float ratio)
	{
		if (isInvincible)
		{
			return;
		}
		if (skillNum < 100)
		{
			global::UnityEngine.Vector3 pos_Font = new global::UnityEngine.Vector3(pos_Text.position.x + (float)global::UnityEngine.Random.Range(-80, 80) * 0.01f, pos_Text.position.y + (float)global::UnityEngine.Random.Range(0, 150) * 0.01f, base.transform.position.z);
			int num = 0;
			num = ((!onSkill) ? GM.Get_AtkDamage(pos_Font, ratio) : GM.Get_MagicDamage(pos_Font, skillNum));
			HP -= num;
		}
		if (HP <= 0)
		{
			HP_Ratio = 0f;
			if (!isDeath)
			{
				Death();
			}
		}
		else
		{
			HP_Ratio = (float)HP / (float)HP_Max;
			if (!isLockHit)
			{
				if (Mon_Num == 54)
				{
					bool flag = false;
					if (facingRight > 0 && Player.transform.position.x < base.transform.position.x + 2f)
					{
						flag = true;
					}
					else if (facingRight < 0 && Player.transform.position.x > base.transform.position.x - 2f)
					{
						flag = true;
					}
					if (flag)
					{
						if (!GetComponent<global::UnityEngine.Animator>().GetBool("onHit_Back"))
						{
							GetComponent<global::UnityEngine.Animator>().SetBool("onHit_Back", true);
						}
						GetComponent<global::UnityEngine.Animator>().Play("Hit_Back", 0, 0f);
					}
					else
					{
						if (!GetComponent<global::UnityEngine.Animator>().GetBool("onHit"))
						{
							GetComponent<global::UnityEngine.Animator>().SetBool("onHit", true);
						}
						GetComponent<global::UnityEngine.Animator>().Play("Hit", 0, 0f);
					}
					GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", false);
				}
				else
				{
					if (!GetComponent<global::UnityEngine.Animator>().GetBool("onHit"))
					{
						GetComponent<global::UnityEngine.Animator>().SetBool("onHit", true);
					}
					GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", false);
					GetComponent<global::UnityEngine.Animator>().SetBool("onMove", false);
					if (Mon_Num > 30 && Mon_Num < 35)
					{
						if (GetComponent<global::UnityEngine.Animator>().GetBool("onCrouch") && !GetComponent<global::UnityEngine.Animator>().GetBool("onJump"))
						{
							GetComponent<global::UnityEngine.Animator>().Play("Sit_Hit", 0, 0f);
						}
						else
						{
							GetComponent<global::UnityEngine.Animator>().Play("Hit", 0, 0f);
						}
						GetComponent<global::UnityEngine.Animator>().SetBool("onAttackTongue", false);
						GetComponent<global::UnityEngine.Animator>().SetBool("onAttackFire", false);
						GetComponent<global::UnityEngine.Animator>().SetBool("onAttackUp", false);
						GetComponent<global::UnityEngine.Animator>().SetBool("onWalk", false);
					}
					else if (Mon_Num == 42)
					{
						GetComponent<global::UnityEngine.Animator>().Play("Hit", 0, 0f);
						GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_Upper", false);
						GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_Tongue", false);
						GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_Fire", false);
						GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_Tail", false);
						GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_Shock", false);
					}
					else if (Mon_Num == 55)
					{
						GetComponent<global::UnityEngine.Animator>().Play("Hit", 0, 0f);
						GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_Upper", false);
						GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_Tongue", false);
						GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_Fire", false);
						GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_Laser", false);
						GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_Shock", false);
					}
					else
					{
						GetComponent<global::UnityEngine.Animator>().Play("Hit", 0, 0f);
					}
					if (Mon_Num == 24)
					{
						GetComponent<AI_Mon_36>().SendMessage("Mon_7_CumShot");
					}
				}
			}
		}
		PC_Body_Delay = 0.5f;
		if (!onSkill)
		{
			Hit_Combo++;
			Hit_Timer = 0.8f;
		}
	}

	private void Death()
	{
		isDeath = true;
		if (HP <= 0)
		{
			if (GM.Bonus_Blood > 0 && Mon_Num != 53)
			{
				Make_Blood();
			}
			GM.Monster_Kill(Mon_Num);
		}
		if (Mon_Num != 33 && Event_Num > 0)
		{
			GM.Set_EventMonster(Event_Num);
		}
		if (Mon_Num < 39 || Mon_Num == 40)
		{
			SC.Mon_Explo(base.transform.position);
			bool isFlip = false;
			if (base.transform.position.x < Player.transform.position.x)
			{
				isFlip = true;
				explo_Pos_Root.transform.localScale = new global::UnityEngine.Vector3(-1f, 1f, 1f);
			}
			if (Mon_Num == 31 || Mon_Num == 33)
			{
				if (GetComponent<global::UnityEngine.Animator>().GetBool("onCrouch"))
				{
					explo_Pos_1.transform.localPosition = new global::UnityEngine.Vector3(-0.2f, -2.1f, 0f);
					explo_Pos_2.transform.localPosition = new global::UnityEngine.Vector3(-0.72f, -1.19f, 0f);
					explo_Pos_3.transform.localPosition = new global::UnityEngine.Vector3(0.9f, -3.28f, 0f);
					explo_Pos_4.transform.localPosition = new global::UnityEngine.Vector3(-2.2f, -1.32f, 0f);
					explo_Pos_5.transform.localPosition = new global::UnityEngine.Vector3(-2f, -2.45f, 0f);
					explo_Pos_6.transform.localPosition = new global::UnityEngine.Vector3(-1.76f, -3.27f, 0f);
				}
				if (Mon_Num == 33)
				{
					GetComponent<AI_Mon_33>().Delete_Col_Rolling();
				}
			}
			else if ((Mon_Num == 32 || Mon_Num == 34) && GetComponent<global::UnityEngine.Animator>().GetBool("onCrouch"))
			{
				explo_Pos_1.transform.localPosition = new global::UnityEngine.Vector3(-0.25f, -2.1f, 0f);
				explo_Pos_2.transform.localPosition = new global::UnityEngine.Vector3(-1.46f, -1.19f, 0f);
				explo_Pos_3.transform.localPosition = new global::UnityEngine.Vector3(-0.2f, -3.28f, 0f);
				explo_Pos_4.transform.localPosition = new global::UnityEngine.Vector3(-3.08f, -1.32f, 0f);
				explo_Pos_5.transform.localPosition = new global::UnityEngine.Vector3(-3.86f, -2.65f, 0f);
				explo_Pos_6.transform.localPosition = new global::UnityEngine.Vector3(-2.19f, -3.27f, 0f);
			}
			if (explo_Pos_1 != null)
			{
				Make_Explo(explo_Pos_1, isFlip);
			}
			else
			{
				Make_Explo(base.gameObject, isFlip);
			}
			if (explo_Pos_2 != null)
			{
				Make_Explo(explo_Pos_2, isFlip);
			}
			if (explo_Pos_3 != null)
			{
				Make_Explo(explo_Pos_3, isFlip);
			}
			if (explo_Pos_4 != null)
			{
				Make_Explo(explo_Pos_4, isFlip);
			}
			if (explo_Pos_5 != null)
			{
				Make_Explo(explo_Pos_5, isFlip);
			}
			if (explo_Pos_6 != null)
			{
				Make_Explo(explo_Pos_6, isFlip);
			}
			if (Mon_Num <= 2)
			{
				SC.Mob_Death(base.transform.position);
			}
			else if (Mon_Num == 3 || Mon_Num == 4)
			{
				SC.Mon_3_Death(base.transform.position);
			}
			else if (Mon_Num == 5 || Mon_Num == 16)
			{
				SC.Mon_1_Death(base.transform.position);
			}
			else if (Mon_Num == 6)
			{
				SC.Mon_2_Death(base.transform.position);
			}
			else if (Mon_Num == 9)
			{
				SC.Mon_4_Death(base.transform.position);
			}
			else if (Mon_Num == 13 || Mon_Num == 14)
			{
				SC.Mon_5_Death(base.transform.position);
			}
			else if (Mon_Num == 11 || Mon_Num == 18)
			{
				SC.Mon_7_Death(base.transform.position);
				if (GM.Level <= Mon_Num + 8)
				{
					Make_Potion();
				}
			}
			else if (Mon_Num == 12 || Mon_Num == 19)
			{
				SC.Mon_6_Death(base.transform.position);
				Make_Potion();
			}
			else if (Mon_Num == 24)
			{
				SC.Mon_6_Death(base.transform.position);
				GetComponent<AI_Mon_36>().Set_Death();
				if (GM.Level <= 31)
				{
					Make_Potion();
				}
			}
			else if (Mon_Num >= 30 && Mon_Num < 38)
			{
				if (Mon_Num == 31 && GM.Level <= 32)
				{
					Make_Potion();
				}
				else if (Mon_Num == 33)
				{
					if (Event_Num > 26 && Event_Num <= 30 && !GM.Check_Bonus(Event_Num))
					{
						Make_BloodSkull();
					}
					Make_Potion(0, 0, 4, 4);
				}
				else if (Mon_Num == 32 && GM.Level <= 35)
				{
					Make_Potion(1, 1, 0, 1);
				}
				else if (Mon_Num == 34 && GM.Level <= 40)
				{
					Make_Potion(1, 1, 0, 1);
				}
				else if (Mon_Num == 35 && GM.Level <= 42)
				{
					Make_Potion(1, 2, 0, 2);
				}
				else if (Mon_Num == 36 && GM.Level <= 44)
				{
					Make_Potion(1, 2, 0, 2);
				}
				SC.Mon_9_Death(base.transform.position);
			}
			else if (Mon_Num == 38)
			{
				SC.Mon_8_Death(base.transform.position);
				Make_Potion(2, 2, 0, 2);
			}
			else if (Mon_Num == 40)
			{
				SC.Mon_6_Death(base.transform.position);
				Make_Potion(3, 3, 0, 3);
			}
			if (Mon_Num == 30)
			{
				GetComponent<AI_Mon_BrainGirl>().FireExplo();
			}
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
		else if (Mon_Num == 39)
		{
			Del_HP_Bar();
			GetComponent<AI_Boss_2>().Set_Death();
		}
		else if (Mon_Num == 41)
		{
			GetComponent<AI_Mon_32>().Set_Death();
		}
		else if (Mon_Num == 42 || Mon_Num == 43)
		{
			GM.Set_EventMonster(Event_Num);
			GetComponent<AI_Mon_42>().Set_Death();
		}
		else if (Mon_Num == 51)
		{
			GM.Set_Event(11);
			GetComponent<AI_Mon_30N>().Set_Death();
		}
		else if (Mon_Num == 52)
		{
			GM.Set_Event(12);
			GetComponent<AI_Boss_2N>().Set_Death();
		}
		else if (Mon_Num == 53)
		{
			GM.Set_Event(13);
			GetComponent<AI_Boss_3Gun>().Set_Death();
		}
		else if (Mon_Num == 54)
		{
			GM.Set_Event(14);
			GetComponent<AI_MotherBrain>().Set_Death();
		}
		else if (Mon_Num == 55)
		{
			GM.Set_Event(15);
			GetComponent<AI_Queen>().Set_Death();
		}
	}

	private void Make_Explo(global::UnityEngine.GameObject posObj, bool isFlip)
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Explo, posObj.transform.position, posObj.transform.rotation) as global::UnityEngine.GameObject;
		if (isFlip)
		{
			gameObject.transform.localScale = new global::UnityEngine.Vector3(posObj.transform.localScale.x * -1f, posObj.transform.localScale.y, 1f);
			gameObject.transform.rotation = global::UnityEngine.Quaternion.Euler(0f, 0f, 0f - gameObject.transform.rotation.eulerAngles.z);
		}
		else
		{
			gameObject.transform.localScale = posObj.transform.localScale;
		}
	}

	private void Make_Potion()
	{
		bool flag = false;
		if (_Item_Potion_HP != null && global::UnityEngine.Random.Range(0, 10) > 7)
		{
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Item_Potion_HP, new global::UnityEngine.Vector3(base.transform.position.x + global::UnityEngine.Random.Range(-1f, 1f), base.transform.position.y + global::UnityEngine.Random.Range(-1f, 1f), 0f), base.transform.rotation) as global::UnityEngine.GameObject;
			flag = true;
		}
		if (!flag && _Item_Potion_MP != null && global::UnityEngine.Random.Range(0, 10) > 7)
		{
			global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(_Item_Potion_MP, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
		}
	}

	private void Make_BloodSkull()
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Item_Potion_HP, new global::UnityEngine.Vector3(base.transform.position.x, base.transform.position.y + 2f, 0f), base.transform.rotation) as global::UnityEngine.GameObject;
		gameObject.GetComponent<Item>().Bonus_Index = Event_Num;
	}

	private void Make_HP_Potion()
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Item_Potion_HP, new global::UnityEngine.Vector3(base.transform.position.x + global::UnityEngine.Random.Range(-1.5f, 1.5f), base.transform.position.y + global::UnityEngine.Random.Range(-1.5f, 1.5f), 0f), base.transform.rotation) as global::UnityEngine.GameObject;
	}

	private void Make_MP_Potion()
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Item_Potion_MP, new global::UnityEngine.Vector3(base.transform.position.x + global::UnityEngine.Random.Range(-1.5f, 1.5f), base.transform.position.y + global::UnityEngine.Random.Range(-1.5f, 1.5f), 0f), base.transform.rotation) as global::UnityEngine.GameObject;
	}

	public void Make_Potion(int HP_min, int HP_max, int MP_min, int MP_max)
	{
		if (HP_max > HP_min)
		{
			HP_max = global::UnityEngine.Random.Range(HP_min, HP_max + 1);
		}
		for (int i = 0; i < HP_max; i++)
		{
			Make_HP_Potion();
		}
		if (MP_max > MP_min)
		{
			MP_max = global::UnityEngine.Random.Range(MP_min, MP_max + 1);
		}
		for (int j = 0; j < MP_max; j++)
		{
			Make_MP_Potion();
		}
	}

	private void Make_Blood()
	{
		int num = ((GM.Bonus_Blood <= 5 && Mon_Num != 33) ? (GM.Bonus_Blood * 3) : 15);
		for (int i = 0; i < num; i++)
		{
			global::UnityEngine.Vector3 position = new global::UnityEngine.Vector3(base.transform.position.x + (float)global::UnityEngine.Random.Range(-200, 200) * 0.01f, base.transform.position.y + (float)global::UnityEngine.Random.Range(-200, 200) * 0.01f, 0f);
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Blood_Obj, position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
		}
	}

	public int Get_HitCombo()
	{
		return Hit_Combo;
	}

	public void Reset_HitCombo()
	{
		Hit_Combo = 0;
		Hit_Timer = 0.8f;
	}

	public void Set_HitMotion()
	{
		if (!isLockHit && !isInvincible)
		{
			if (GetComponent<global::UnityEngine.Animator>().GetBool("onHit"))
			{
				GetComponent<global::UnityEngine.Animator>().Play("Hit", 0, 0f);
			}
			else
			{
				GetComponent<global::UnityEngine.Animator>().SetBool("onHit", true);
				GetComponent<global::UnityEngine.Animator>().Play("Hit", 0, 0f);
			}
			GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", false);
			GetComponent<global::UnityEngine.Animator>().SetBool("onMove", false);
		}
	}

	private void OnTriggerStay2D(global::UnityEngine.Collider2D col)
	{
		if (onEvent || !(Event_Timer <= 0f) || GM.Paused || GM.onEvent || GM.onHscene || GM.onDown || GM.onGatePass || GM.onGameClear || isDeath)
		{
			return;
		}
		if (Mon_Num == 55 && base.GetComponent<UnityEngine.Rigidbody2D>().gravityScale == 0f && col.name == "Ani" && QueenShield_Delay <= 0f)
		{
			QueenShield_Delay = 0.5f;
			PC_Col_Delay = 0.2f;
			bool flag = false;
			bool flag2 = false;
			if (Player.transform.position.y < base.transform.position.y - 10f && global::UnityEngine.Mathf.Abs(Player.transform.position.x - (base.transform.position.x + 1.44f * (float)(-facingRight))) < 4f)
			{
				flag = true;
			}
			else if (Player.transform.position.y < base.transform.position.y + 7f && Player.transform.position.y > base.transform.position.y - 11.37f && global::UnityEngine.Mathf.Abs(Player.transform.position.x - (base.transform.position.x + 1.44f * (float)(-facingRight))) < 6f)
			{
				flag2 = true;
			}
			Player.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(0f, Player.GetComponent<UnityEngine.Rigidbody2D>().velocity.y);
			if (Player.transform.position.x < base.transform.position.x + 1.44f * (float)(-facingRight))
			{
				if (flag || flag2)
				{
					Player.transform.position = new global::UnityEngine.Vector3(Player.transform.position.x - 4f, Player.transform.position.y, 0f);
				}
				if (GM.onShield)
				{
					Player.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * -40f, global::UnityEngine.ForceMode2D.Impulse);
				}
				else if (QueenDash_Delay > 0f)
				{
					GM.Damage(250, -45f, true, Mon_Num);
				}
				else
				{
					GM.Damage(70, -35f, false, Mon_Num);
				}
			}
			else
			{
				if (flag || flag2)
				{
					Player.transform.position = new global::UnityEngine.Vector3(Player.transform.position.x + 4f, Player.transform.position.y, 0f);
				}
				if (GM.onShield)
				{
					Player.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * 40f, global::UnityEngine.ForceMode2D.Impulse);
				}
				else if (QueenDash_Delay > 0f)
				{
					GM.Damage(250, 45f, true, Mon_Num);
				}
				else
				{
					GM.Damage(70, 35f, false, Mon_Num);
				}
			}
		}
		else if (!GM.onShield && col.name == "Ani")
		{
			if ((isInvincible || (!(global::UnityEngine.GameObject.Find("Border_Rolling").GetComponent<global::UnityEngine.SpriteRenderer>().color.a > 0.05f) && !global::UnityEngine.GameObject.Find("Ani_Rolling").GetComponent<global::UnityEngine.SpriteRenderer>().enabled)) && !isPass)
			{
				if (!GM.GameOver && Mon_Num == 41 && isInvincible && Invincible_Delay <= 0f)
				{
					PC_Col_Delay = 0.3f;
					PC_Body_Delay = 0.3f;
					Invincible_Delay = 0.3f;
					if (base.transform.position.x > col.transform.parent.position.x)
					{
						GM.Damage(200, -50f, true, Mon_Num);
					}
					else
					{
						GM.Damage(200, 50f, true, Mon_Num);
					}
				}
				else if (!GM.GameOver && PC_Col_Delay <= 0f && PC_Body_Delay <= 0f)
				{
					PC_Col_Delay = 1f;
					PC_Body_Delay = 1f;
					base.gameObject.SendMessage("Set_AttackDelay");
					if (Mon_Num == 40 && isInvincible && Invincible_Delay <= 0f)
					{
						if (base.transform.position.x > col.transform.parent.position.x)
						{
							GM.Damage(Damage, -45f, true, Mon_Num);
						}
						else
						{
							GM.Damage(Damage, 45f, true, Mon_Num);
						}
					}
					else if (base.transform.position.x > col.transform.parent.position.x)
					{
						GM.Damage(Damage, 0f - DmgForce, false, Mon_Num);
					}
					else
					{
						GM.Damage(Damage, DmgForce, false, Mon_Num);
					}
				}
			}
		}
		else if (Mon_Num == 41 && GM.onShield && col.name == "Ani" && !GM.GameOver && Invincible_Delay <= 0f)
		{
			PC_Col_Delay = 0.3f;
			PC_Body_Delay = 0.3f;
			Invincible_Delay = 0.3f;
			Player.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(0f, Player.GetComponent<UnityEngine.Rigidbody2D>().velocity.y);
			Player.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * 40f * facingRight, global::UnityEngine.ForceMode2D.Impulse);
			if (isInvincible)
			{
				global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Player_Damage(Mon_Num, true, Player.transform.position);
			}
			else
			{
				global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Player_Damage(42, false, Player.transform.position);
			}
		}
		if (col.tag == "Magic_Explo" && !isInvincible)
		{
			if (Mon_Num != 51 && HP > 0 && col.gameObject != Bomb_Object[0] && col.gameObject != Bomb_Object[1])
			{
				if (Bomb_Object[0] == null)
				{
					Bomb_Object[0] = col.gameObject;
					Bomb_Timer[0] = 0.18f;
				}
				else
				{
					Bomb_Object[1] = Bomb_Object[0];
					Bomb_Timer[1] = Bomb_Timer[0];
					Bomb_Object[0] = col.gameObject;
					Bomb_Timer[0] = 0.18f;
				}
				Damage_Hit(true, 1, 0f);
				SC.Mon_Hit_2(base.transform.position);
				PC_Col_Delay = 0.5f;
			}
		}
		else if (col.tag == "Magic_Smog" && !isInvincible)
		{
			Poison_Smog_Timer = 0.1f;
			Poison_Skill_Timer = 0.2f;
			if (!onPoisonSkill && Mon_Num != 53)
			{
				onPoisonSkill = true;
				if (!onPoisonWeapon)
				{
					GetComponent<Mon_Index>().On_MagicPoison();
				}
			}
		}
		else if (col.tag == "Magic_Shield")
		{
			if (Mon_Num == 12 || Mon_Num == 19)
			{
				GM.Break_Shield();
			}
			else if (Mon_Num <= 36 && Mon_Num != 12 && Mon_Num != 15 && Mon_Num != 19 && Mon_Num != 20 && Mon_Num != 24)
			{
				int num = ((col.transform.position.x < base.transform.position.x) ? 1 : (-1));
				base.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(0f, 0f);
				if (base.GetComponent<UnityEngine.Rigidbody2D>().gravityScale > 0f)
				{
					base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * 20f * num, global::UnityEngine.ForceMode2D.Impulse);
				}
				else
				{
					base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * 10f * num, global::UnityEngine.ForceMode2D.Impulse);
				}
				onKnockback = true;
				knockback_Timer = 0.2f;
				if (Shield_Force_Timer <= 0f)
				{
					onShield_Lock = true;
					Shield_Force_Timer = 0.2f;
					Hit_Delay = 0.2f;
					Set_HitMotion();
				}
			}
		}
		else if (col.tag == "Magic_Fire")
		{
			string text = col.name.Substring(0, 11);
			if (text == "MagicFire_5")
			{
				if (Mon_Num != 12 && Mon_Num != 15 && Mon_Num != 19 && Mon_Num != 20 && Mon_Num != 24 && Mon_Num < 50)
				{
					base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, col.transform.position, 1.3f * global::UnityEngine.Time.deltaTime);
				}
				if (Gravity_Delay <= 0f)
				{
					Gravity_Delay = 0.1f;
					SC.Mon_Hit_1(base.transform.position);
					PC_Col_Delay = 0.5f;
					MagicFire_5_Num++;
					MagicFire_5_Num_Timer = 3f;
					Damage_Hit(true, 4, 0f);
				}
			}
			else if (text == "MagicFire_3" && !isInvincible)
			{
				if (!(MagicFire_3_Timer <= 0f))
				{
					return;
				}
				MagicFire_3_Timer = 0.1f;
				col.gameObject.SendMessage("Explo");
				if (!onPoisonSkill && Mon_Num != 53)
				{
					onPoisonSkill = true;
					Poison_Skill_Timer = 3f;
					if (!onPoisonWeapon)
					{
						GetComponent<Mon_Index>().On_MagicPoison();
					}
				}
				Damage_Hit(true, 2, 0f);
			}
			else
			{
				if (!(text == "MagicFire_1") || isInvincible || !(MagicFire_1_Timer <= 0f))
				{
					return;
				}
				MagicFire_1_Num++;
				MagicFire_1_Num_Timer = 3f;
				if (onCrouch)
				{
					MagicFire_1_Timer = 1f;
					Damage_Hit(true, 0, 0f);
					PC_Col_Delay = 0.1f;
					SC.Mon_Hit_1(base.transform.position);
					return;
				}
				MagicFire_1_Timer = 0.05f;
				float num2 = ((!(base.transform.position.x > Player.transform.position.x)) ? (-15f) : 15f);
				col.gameObject.SendMessage("Make_Explo");
				Damage_Hit(true, 0, 0f);
				PC_Col_Delay = 0.5f;
				SC.Mon_Hit_1(base.transform.position);
				if (Mon_Num != 12 && Mon_Num != 15 && Mon_Num != 19 && Mon_Num != 20 && Mon_Num != 24 && Mon_Num != 51 && Mon_Num != 52 && Mon_Num != 53 && Mon_Num != 54)
				{
					if (Mon_Num > 30)
					{
						MagicFire_1_Timer = 0.1f;
					}
					base.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(0f, 0f);
					base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * num2, global::UnityEngine.ForceMode2D.Impulse);
					onKnockback = true;
					knockback_Timer = 0.2f;
				}
			}
		}
		else if (col.tag == "Col_PC_Atk" && !isInvincible)
		{
			if (Mon_Num != 53)
			{
				if (GM.Weapon_Num == 3)
				{
					if (!onPoisonSkill && !onPoisonWeapon)
					{
						GetComponent<Mon_Index>().On_MagicPoison();
					}
					onPoisonWeapon = true;
					Poison_Weapon_Timer = 3f;
				}
				else if (GM.Weapon_Num == 4)
				{
					if (!onPoisonSkill && !onPoisonWeapon && !onSlow)
					{
						GetComponent<Mon_Index>().On_MagicSlow();
					}
					onSlow = true;
					Slow_Timer = 3f;
					if (Mon_Num == 41 || Mon_Num == 55)
					{
						Move_Speed = 0.8f;
					}
					else if (Mon_Num == 42)
					{
						Move_Speed = 0.5f;
					}
					else
					{
						Move_Speed = 0.3f;
					}
				}
			}
			if (col.name == "Col_Attack_0_1" || col.name == "Col_Attack_1_1" || col.name == "Col_Attack_2_1" || col.name == "Col_Attack_4_1" || col.name == "Col_Attack_5_1")
			{
				if (!Hit_Atk_1)
				{
					Hit_Atk_2 = false;
					Hit_Atk_1 = true;
					Hit_Delay = 0.3f;
					SC.Mon_Hit_1(base.transform.position);
					Damage_Hit(false, 0, 1f);
				}
			}
			else if (col.name == "Col_Attack_0_2" || col.name == "Col_Attack_1_2" || col.name == "Col_Attack_2_2" || col.name == "Col_Attack_4_2" || col.name == "Col_Attack_5_2")
			{
				if (!Hit_Atk_2)
				{
					Hit_Atk_1 = false;
					Hit_Atk_2 = true;
					Hit_Delay = 0.3f;
					SC.Mon_Hit_1(base.transform.position);
					Damage_Hit(false, 0, 1f);
				}
			}
			else if (col.name == "Col_Attack_3" || col.name == "Col_Attack_5_3")
			{
				if (!Hit_Atk_3)
				{
					Hit_Atk_1 = false;
					Hit_Atk_2 = false;
					Hit_Atk_3 = true;
					Hit_Atk_4 = false;
					Hit_Delay = 0.3f;
					SC.Mon_Hit_1(base.transform.position);
					Damage_Hit(false, 0, 1.2f);
					KnockBack_Atk34(1f);
				}
			}
			else if (col.name == "Col_Attack_4" || col.name == "Col_Attack_5_4")
			{
				if (!Hit_Atk_4)
				{
					Hit_Atk_1 = false;
					Hit_Atk_2 = false;
					Hit_Atk_3 = false;
					Hit_Atk_4 = true;
					Hit_Delay = 0.3f;
					SC.Mon_Hit_1(base.transform.position);
					Damage_Hit(false, 0, 1.5f);
					KnockBack_Atk34(2f);
				}
			}
			else if (col.name == "Col_Spin")
			{
				if (Hit_Delay <= 0f)
				{
					Hit_Delay = 0.1f;
					PC_Col_Delay = 0.5f;
					SC.Mon_Hit_1(base.transform.position);
					Damage_Hit(false, 0, 1.2f);
				}
			}
			else if (col.name == "Col_Rolling")
			{
				if (Hit_Delay <= 0f)
				{
					if (isLockHit && (Mon_Num == 11 || Mon_Num == 18) && col.transform.position.y > base.transform.position.y + 3f)
					{
						isLockHit = false;
					}
					else if (isLockHit && Mon_Num == 24 && col.transform.position.y > base.transform.position.y + 4.4f)
					{
						isLockHit = false;
					}
					if (col.transform.position.y > base.transform.position.y)
					{
						PC_Col_Delay = 0.5f;
						PC_Body_Delay = 0.5f;
					}
					if (global::UnityEngine.GameObject.Find("Border_Rolling").GetComponent<global::UnityEngine.SpriteRenderer>().color.a > 0.05f)
					{
						Hit_Delay = 0.1f;
						PC_Col_Delay = 0.5f;
						Damage_Hit(false, 0, 1.5f);
					}
					else
					{
						Hit_Delay = 0.24f;
						Damage_Hit(false, 0, 1f);
					}
					SC.Mon_Hit_1(base.transform.position);
				}
			}
			else if (!Hit_Atk_1)
			{
				Hit_Atk_1 = true;
				Hit_Delay = 0.3f;
				SC.Mon_Hit_1(base.transform.position);
				Damage_Hit(false, 0, 1f);
			}
			if (Mon_Num == 41)
			{
				PC_Col_Delay = 0f;
				PC_Body_Delay = 0f;
			}
		}
		else if (col.tag == "Col_PC_Atk" && isInvincible && Mon_Num == 55 && base.GetComponent<UnityEngine.Rigidbody2D>().gravityScale == 0f)
		{
			if (PC_Col_Delay <= 0f)
			{
				PC_Col_Delay = 0.5f;
				if (base.transform.position.x < Player.transform.position.x)
				{
					Player.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * 20f, global::UnityEngine.ForceMode2D.Impulse);
				}
				else
				{
					Player.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * -20f, global::UnityEngine.ForceMode2D.Impulse);
				}
				global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Electric_Dmg(Player.transform.position);
			}
		}
		else if (col.tag == "Trap_Fang" && !isInvincible && Trap_Fang_Timer <= 0f)
		{
			int dmg = (int)((float)GM.HP_Max * 0.1f);
			float num3 = 15f;
			global::UnityEngine.Vector3 pos_Font = new global::UnityEngine.Vector3(pos_Text.position.x + (float)global::UnityEngine.Random.Range(-80, 80) * 0.01f, pos_Text.position.y + (float)global::UnityEngine.Random.Range(0, 150) * 0.01f, base.transform.position.z);
			if (base.transform.position.x < col.transform.position.x)
			{
				Damage_Trap(dmg, 0f - num3);
			}
			else
			{
				Damage_Trap(dmg, num3);
			}
			GM.Get_MonTrapDmg(dmg, pos_Font);
			Trap_Fang_Timer = 1f;
			SC.Mon_Hit_1(base.transform.position);
		}
		else if (col.tag == "Trap_Laser" && !isInvincible && Trap_Laser_Timer <= 0f)
		{
			int dmg2 = (int)((float)HP_Max * 0.25f);
			float num4 = 20f;
			global::UnityEngine.Vector3 pos_Font2 = new global::UnityEngine.Vector3(pos_Text.position.x + (float)global::UnityEngine.Random.Range(-80, 80) * 0.01f, pos_Text.position.y + (float)global::UnityEngine.Random.Range(0, 150) * 0.01f, base.transform.position.z);
			if (base.transform.position.x < col.transform.position.x)
			{
				Damage_Trap(dmg2, 0f - num4);
			}
			else
			{
				Damage_Trap(dmg2, num4);
			}
			GM.Get_MonTrapDmg(dmg2, pos_Font2);
			Trap_Laser_Timer = 1f;
			SC.Mon_Hit_1(base.transform.position);
		}
	}

	private void Damage_Trap(int dmg, float dmgForce)
	{
		HP -= dmg;
		if (HP <= 0)
		{
			HP_Ratio = 0f;
			if (!isDeath)
			{
				Death();
			}
			return;
		}
		HP_Ratio = (float)HP / (float)HP_Max;
		if (GetComponent<global::UnityEngine.Animator>().GetBool("onHit"))
		{
			GetComponent<global::UnityEngine.Animator>().Play("Hit", 0, 0f);
		}
		else
		{
			GetComponent<global::UnityEngine.Animator>().Play("Hit", 0, 0f);
			GetComponent<global::UnityEngine.Animator>().SetBool("onHit", true);
		}
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onMove", false);
	}

	private void KnockBack_Atk34(float force)
	{
		if (Mon_Num == 12 || Mon_Num == 15 || Mon_Num == 19 || Mon_Num == 20 || Mon_Num == 24 || Mon_Num == 51 || Mon_Num == 52 || Mon_Num == 54)
		{
			return;
		}
		int num = ((base.transform.position.x > Player.transform.position.x) ? 1 : (-1));
		base.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(0f, base.GetComponent<UnityEngine.Rigidbody2D>().velocity.y);
		if (base.GetComponent<UnityEngine.Rigidbody2D>().gravityScale > 0f)
		{
			if (Mon_Num != 55)
			{
				base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * 8f * force * num, global::UnityEngine.ForceMode2D.Impulse);
			}
			else
			{
				base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * 800f * force * num, global::UnityEngine.ForceMode2D.Impulse);
			}
		}
		else
		{
			base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * 3f * force * num, global::UnityEngine.ForceMode2D.Impulse);
		}
		onKnockback = true;
		knockback_Timer = 0.35f;
	}

	private void Set_Invincible()
	{
		isInvincible = true;
	}

	private void End_Invincible()
	{
		isInvincible = false;
	}

	private void Set_LockHit()
	{
		isLockHit = true;
	}

	private void End_LockHit()
	{
		isLockHit = false;
	}

	private void Off_HP_Bar()
	{
		onHpBar = false;
		HP_Bar.enabled = false;
		HP_Bar_BG.enabled = false;
	}

	private void Del_HP_Bar()
	{
		global::UnityEngine.Object.Destroy(HP_Bar_BG.gameObject);
	}

	public bool Get_Poison()
	{
		if ((onPoisonSkill && Poison_Skill_Timer > 0f) || (onPoisonWeapon && Poison_Weapon_Timer > 0f))
		{
			return true;
		}
		return false;
	}

	public void Set_Poison(float timer)
	{
		onPoisonWeapon = true;
		Poison_Weapon_Timer = timer;
		GetComponent<Mon_Index>().On_MagicPoison();
	}

	public bool Get_Slow()
	{
		if (onSlow && Slow_Timer > 0f)
		{
			return true;
		}
		return false;
	}

	public void Set_Slow(float timer)
	{
		onSlow = true;
		Slow_Timer = timer;
		GetComponent<Mon_Index>().On_MagicSlow();
		if (Mon_Num == 41 || Mon_Num == 55)
		{
			Move_Speed = 0.8f;
		}
		else if (Mon_Num == 42)
		{
			Move_Speed = 0.5f;
		}
		else
		{
			Move_Speed = 0.3f;
		}
	}

	public void Damage_Mon33_Rolling(float ratio)
	{
		Damage_Hit(false, 0, ratio);
	}

	public void Make_Icon()
	{
		if (_Icon != null)
		{
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Icon, new global::UnityEngine.Vector3(0f, 0f, 0f), global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
			gameObject.GetComponent<Info_MonIcon>().Mon_Num = Mon_Num;
			gameObject.GetComponent<Info_MonIcon>().MonCenter = base.transform;
			gameObject.GetComponent<Info_MonIcon>().Set_MonIcon(Icon_Spr);
			gameObject.GetComponent<Info_MonIcon>().Set_Dist();
		}
	}

	public void Set_QueenDash()
	{
		QueenDash_Delay = 0.5f;
	}

	public void Set_QueenDeath()
	{
		isDeath = true;
		onEvent = true;
	}
}
