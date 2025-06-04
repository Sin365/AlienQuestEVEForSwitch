public class AI_Boss_2 : global::UnityEngine.MonoBehaviour
{
	private bool isDeath;

	private float Death_Timer;

	private int EnemyState;

	private float Life_Timer;

	private int facingRight = -1;

	private float distance;

	private float dist_X;

	private float dist_Y;

	private float Move_Speed = 1f;

	private float Orig_Speed = 1f;

	private bool on_Chase;

	private bool on_BackWalk;

	private bool on_Crouch;

	private bool on_Attack_Range;

	private bool on_Attack_Down_Range;

	private bool on_Tongue_Range;

	private bool on_Tongue_Down_Range;

	private bool on_Dash_Range;

	private float Hit_Delay;

	private float Flip_Delay;

	private float Attack_Delay;

	private float Dash_Delay;

	private float Move_Lock_Timer;

	private float LockHit_Delay;

	private float Invincible_Timer;

	private float Fire_Gravity_Timer;

	private float Fire_Multiple_Timer;

	private float Fire_Multiple_1sec;

	private float JumpDash_Timer;

	private float JumpBack_Timer;

	private float JumpBack_Front_1sec;

	private float Crouch_Timer;

	private int Fire_Triple_Num;

	private float Fire_Triple_Timer;

	private float Escape_JumpDash_Timer;

	private float Escape_JumpFront_Timer;

	private float Escape_JumpBack_Timer;

	private float Screw_JumpBack_Timer;

	private float Escape_BackDash_Timer;

	private float Screw_BackDash_Timer;

	private float rnd_X;

	private bool isStuck_Front;

	private bool isStuck_Back;

	private bool on_Hscene;

	private bool on_Hscene_Range;

	private float H_Timer;

	private float H_Walk_Timer;

	private float H_Pursue_Timer;

	private float PC_Atk_Timer;

	private int Patrol_State;

	private float Patrol_Idle_Timer;

	private float Patrol_Move_Timer;

	private float Patrol_Range;

	public global::UnityEngine.GameObject Explo;

	public global::UnityEngine.Transform[] explo_Pos;

	private float ExploSound_Timer;

	private float[] Explo_Timer = new float[6];

	public global::UnityEngine.GameObject Tr_Pos;

	public global::UnityEngine.Transform Tr_1_Start;

	public global::UnityEngine.Transform Tr_1_End;

	public global::UnityEngine.Transform Tr_2_Start;

	public global::UnityEngine.Transform Tr_2_End;

	public global::UnityEngine.Transform Tr_3_Start;

	public global::UnityEngine.Transform Tr_3_End;

	public global::UnityEngine.Transform Tr_4_Start;

	public global::UnityEngine.Transform Tr_4_Start_H;

	public global::UnityEngine.Transform Tr_4_End;

	public global::UnityEngine.Transform Tr_5_Start;

	public global::UnityEngine.Transform Tr_5_End;

	private global::UnityEngine.RaycastHit2D whatIHit;

	public global::UnityEngine.GameObject _Fire;

	public global::UnityEngine.GameObject _Fire_G;

	public global::UnityEngine.Transform pos_Fire;

	public global::UnityEngine.GameObject[] H_Single;

	public global::UnityEngine.SpriteRenderer CensoredText;

	public global::UnityEngine.SpriteRenderer CensoredBox;

	public global::UnityEngine.SkinnedMeshRenderer Penis;

	public global::UnityEngine.SkinnedMeshRenderer Penis_Censored;

	public global::UnityEngine.SkinnedMeshRenderer Penis_Wet;

	private float Snd_Damage_Timer;

	private float Snd_Growl_Timer;

	private global::UnityEngine.Animator animator;

	private Monster Mon;

	private Player_Control PC;

	private global::UnityEngine.GameObject Player;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		Player = global::UnityEngine.GameObject.Find("Player");
		PC = Player.GetComponent<Player_Control>();
		Mon = GetComponent<Monster>();
		animator = GetComponent<global::UnityEngine.Animator>();
		rnd_X = (float)global::UnityEngine.Random.Range(0, 50) * 0.01f;
		Orig_Speed = (Move_Speed = 20f + rnd_X * 2f);
		Patrol_Range = 15f + global::UnityEngine.Random.Range(0f, 3f);
		if (AxiPlayerPrefs.GetInt("UncensoredPatch") != 1)
		{
			Penis.enabled = false;
			Penis_Wet.enabled = false;
			Penis_Censored.enabled = true;
		}
		else
		{
			Penis.enabled = true;
			Penis_Wet.enabled = false;
			Penis_Censored.enabled = false;
		}
		if (Player.transform.position.x > base.transform.position.x)
		{
			Flip();
		}
	}

	private void Update()
	{
		if (GM.Paused || on_Hscene)
		{
			return;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (AxiPlayerPrefs.GetInt("Censorship") == 1)
		{
			CensoredText.enabled = true;
			CensoredBox.enabled = true;
		}
		else
		{
			CensoredText.enabled = false;
			CensoredBox.enabled = false;
		}
		Fire_Gravity_Timer += global::UnityEngine.Time.deltaTime;
		Fire_Multiple_Timer += global::UnityEngine.Time.deltaTime;
		if (Fire_Triple_Timer > 0f)
		{
			Fire_Triple_Timer -= global::UnityEngine.Time.deltaTime;
		}
		else if (Fire_Triple_Num > 0)
		{
			Fire_Triple_Num = 0;
		}
		JumpDash_Timer += global::UnityEngine.Time.deltaTime;
		JumpBack_Timer += global::UnityEngine.Time.deltaTime;
		if (Escape_JumpDash_Timer > 0f)
		{
			Escape_JumpDash_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Escape_JumpBack_Timer > 0f)
		{
			Escape_JumpBack_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Escape_JumpFront_Timer > 0f)
		{
			Escape_JumpFront_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Escape_BackDash_Timer > 0f)
		{
			Escape_BackDash_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Screw_JumpBack_Timer > 0f)
		{
			Screw_JumpBack_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Screw_BackDash_Timer > 0f)
		{
			Screw_BackDash_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Fire_Multiple_1sec > 0f)
		{
			Fire_Multiple_1sec -= global::UnityEngine.Time.deltaTime;
		}
		if (JumpBack_Front_1sec > 0f)
		{
			JumpBack_Front_1sec -= global::UnityEngine.Time.deltaTime;
		}
		if (Invincible_Timer > 0f)
		{
			Invincible_Timer -= global::UnityEngine.Time.deltaTime;
		}
		else if (Mon.isInvincible)
		{
			Mon.isInvincible = false;
		}
		if (LockHit_Delay > 0f)
		{
			LockHit_Delay -= global::UnityEngine.Time.deltaTime;
		}
		else if (Mon.isLockHit)
		{
			Mon.isLockHit = false;
		}
		if (PC_Atk_Timer > 0f)
		{
			PC_Atk_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Snd_Damage_Timer > 0f)
		{
			Snd_Damage_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Snd_Growl_Timer > 0f)
		{
			Snd_Growl_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Snd_Growl_Timer <= 0f)
		{
			Snd_Growl_Timer = 5f;
			global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_9_Growling(base.transform.position);
		}
		Raycasting();
		if (H_Timer > 0f)
		{
			H_Timer -= global::UnityEngine.Time.deltaTime;
			if (animator.GetBool("onHit"))
			{
				H_Timer = 0f;
				H_Walk_Timer = 0f;
			}
			if ((facingRight > 0 && base.transform.position.x > global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x) || (facingRight < 0 && base.transform.position.x < global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x))
			{
				Flip();
			}
			if (H_Walk_Timer > 0f)
			{
				H_Walk_Timer -= global::UnityEngine.Time.deltaTime;
				if (isStuck_Back)
				{
					H_Walk_Timer = 0f;
					Check_Idle();
				}
				else
				{
					if (!animator.GetBool("onMove"))
					{
						Set_Move();
					}
					Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, Orig_Speed, global::UnityEngine.Time.deltaTime * 12f);
					base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Move_Speed * 0.5f * -facingRight * Mon.Move_Speed);
				}
			}
			else
			{
				Check_Idle();
			}
		}
		else if (distance < 50f && (GM.GameOver || GM.onHscene))
		{
			if (GM.Hscene_Num == 0 && GM.Hscene_Timer <= 0f)
			{
				if (facingRight > 0 && base.transform.position.x > global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x)
				{
					Flip();
				}
				else if (facingRight < 0 && base.transform.position.x < global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x)
				{
					Flip();
				}
				else if (dist_Y < 1f && (global::UnityEngine.Mathf.Abs(base.transform.position.x - global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x) < 1.7f || (isStuck_Front && global::UnityEngine.Mathf.Abs(base.transform.position.x - global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x) < 3.2f)))
				{
					if (facingRight > 0 && Player.transform.localScale.x > 0f)
					{
						Player.transform.position = new global::UnityEngine.Vector3(Player.transform.position.x - 2.1f * Player.transform.localScale.x, Player.transform.position.y, 0f);
						Player.SendMessage("Flip");
					}
					else if (facingRight < 0 && Player.transform.localScale.x < 0f)
					{
						Player.transform.position = new global::UnityEngine.Vector3(Player.transform.position.x - 2.1f * Player.transform.localScale.x, Player.transform.position.y, 0f);
						Player.SendMessage("Flip");
					}
					Start_Hscene();
				}
				else if (isStuck_Front)
				{
					Check_Idle();
				}
				else
				{
					if (!animator.GetBool("onMove"))
					{
						Set_Move();
					}
					Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, Orig_Speed, global::UnityEngine.Time.deltaTime * 12f);
					base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Move_Speed * 1f * facingRight * Mon.Move_Speed);
				}
				H_Pursue_Timer += global::UnityEngine.Time.deltaTime;
			}
			else if (!on_Hscene)
			{
				if (H_Pursue_Timer > 0f)
				{
					Check_Idle();
					Patrol_State = 0;
					Patrol_Idle_Timer = global::UnityEngine.Random.Range(-1f, 1f);
				}
				else if (animator.GetBool("onMove"))
				{
					Patrol_Move_Timer += global::UnityEngine.Time.deltaTime;
					if (isStuck_Front)
					{
						Check_Idle();
						Patrol_State = 1;
						Patrol_Idle_Timer = 2f;
					}
					else if (Patrol_Move_Timer > 1f && global::UnityEngine.Mathf.Abs(global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x - base.transform.position.x) > Patrol_Range)
					{
						Check_Idle();
						Patrol_State = 0;
						Patrol_Idle_Timer = 0f;
					}
					else
					{
						Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, Orig_Speed, global::UnityEngine.Time.deltaTime * 12f);
						base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Move_Speed * 0.8f * facingRight * Mon.Move_Speed);
					}
				}
				else
				{
					Patrol_Idle_Timer += global::UnityEngine.Time.deltaTime;
					if (Patrol_State == 0)
					{
						if (Patrol_Idle_Timer > 3f)
						{
							Patrol_Idle_Timer = 0f;
							Patrol_Move_Timer = 0f;
							Set_Move();
						}
						else if (Patrol_Idle_Timer > 2f)
						{
							if ((facingRight > 0 && base.transform.position.x > global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x) || (facingRight < 0 && base.transform.position.x < global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x))
							{
								Flip();
							}
						}
						else
						{
							Check_Idle();
						}
					}
					else if (Patrol_Idle_Timer > 3f)
					{
						Patrol_Idle_Timer = 0f;
						Patrol_Move_Timer = 0f;
						Flip();
						Set_Move();
					}
					else
					{
						Check_Idle();
					}
				}
				H_Pursue_Timer = 0f;
			}
		}
		else if (isDeath)
		{
			Death_Timer += global::UnityEngine.Time.deltaTime;
			if (Death_Timer < 2.5f)
			{
				DeathExplo();
			}
			else
			{
				global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Explo(base.transform.position);
				for (int i = 0; i < explo_Pos.Length; i++)
				{
					Make_Explo(explo_Pos[i]);
				}
				Mon.Make_Potion(5, 5, 5, 5);
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
		else if (EnemyState < 2)
		{
			Check_Idle();
		}
		else if (animator.GetBool("onHit"))
		{
			Hit_Delay = 0.2f;
			Move_Lock_Timer = 0.2f;
			Attack_Delay = 0f;
			PC_Atk_Timer = 3f;
			if (Flip_Delay <= 0f)
			{
				Check_Flip();
			}
			if (Mon.MagicFire_5_Num > 5)
			{
				Mon.MagicFire_5_Num = 0;
				Hit_Delay = 0f;
				if (JumpDash_Timer > JumpBack_Timer)
				{
					Set_JumpDash();
				}
				else
				{
					Set_JumpBack(false);
				}
			}
			else if (Mon.MagicFire_1_Num > 6)
			{
				Mon.MagicFire_1_Num = 0;
				Hit_Delay = 0f;
				if (Escape_JumpDash_Timer <= 0f)
				{
					Set_JumpDash();
				}
				else
				{
					Set_JumpBack(false);
				}
			}
			else if (Mon.Get_HitCombo() > 3 || GM.onShield)
			{
				Mon.Reset_HitCombo();
				Hit_Delay = 0f;
				if (facingRight > 0 && base.transform.position.x > Player.transform.position.x)
				{
					Flip();
				}
				else if (facingRight < 0 && base.transform.position.x < Player.transform.position.x)
				{
					Flip();
				}
				if (!isStuck_Back)
				{
					if (Escape_BackDash_Timer <= 0f)
					{
						Set_BackDash();
					}
					else
					{
						Set_JumpBack(false);
					}
				}
				else if (!isStuck_Front)
				{
					Set_JumpBack(true);
				}
				else
				{
					Set_JumpDash();
				}
			}
			if (!on_Chase)
			{
				on_Chase = true;
			}
		}
		else if (Hit_Delay > 0f)
		{
			Hit_Delay -= global::UnityEngine.Time.deltaTime;
		}
		else if (Dash_Delay > 0f)
		{
			Dash_Delay -= global::UnityEngine.Time.deltaTime;
			if (Dash_Delay < 0.3f)
			{
				base.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(global::UnityEngine.Mathf.Lerp(base.GetComponent<UnityEngine.Rigidbody2D>().velocity.x, 0f, global::UnityEngine.Time.deltaTime * 5f), base.GetComponent<UnityEngine.Rigidbody2D>().velocity.y);
				if (global::UnityEngine.Mathf.Abs(base.GetComponent<UnityEngine.Rigidbody2D>().velocity.x) < 5f)
				{
					Dash_Delay = 0f;
					Flip_Delay = 0f;
					Attack_Delay = 0f;
				}
			}
		}
		else if (Attack_Delay > 0f)
		{
			Attack_Delay -= global::UnityEngine.Time.deltaTime;
			Move_Lock_Timer = 0.3f;
		}
		else if (Flip_Delay > 0f)
		{
			Flip_Delay -= global::UnityEngine.Time.deltaTime;
			Check_Idle();
		}
		else if (PC_Atk_Timer <= 0f && !GM.onCloth && GM.Option_Int[3] == 1 && PC.grounded_Now)
		{
			Check_Flip();
			if (dist_Y < 1f && dist_X < 1.7f)
			{
				if (!GM.onHscene && GM.Hscene_Timer <= 0f && (PC.State == Player_Control.AniState.Idle || PC.State == Player_Control.AniState.Run || PC.State == Player_Control.AniState.Sit || PC.State == Player_Control.AniState.Down))
				{
					Start_Hscene();
				}
				Check_Idle();
			}
			else if (!isStuck_Front)
			{
				if (!animator.GetBool("onMove"))
				{
					Set_Move();
				}
				Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, Orig_Speed, global::UnityEngine.Time.deltaTime * 15f);
				base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Move_Speed * facingRight * Mon.Move_Speed);
			}
			else
			{
				Check_Idle();
			}
		}
		else
		{
			if (Move_Lock_Timer > 0f)
			{
				Move_Lock_Timer -= global::UnityEngine.Time.deltaTime;
			}
			Check_Flip();
			if (PC.onScrewAttack && dist_X < 7f && Screw_BackDash_Timer <= 0f && !isStuck_Back)
			{
				Screw_BackDash_Timer = 1f;
				Set_BackDash();
			}
			else if (PC.onScrewAttack && dist_X < 7f && Screw_JumpBack_Timer <= 0f)
			{
				Screw_JumpBack_Timer = 3f;
				Set_JumpBack(isStuck_Back);
			}
			else if (on_Tongue_Range)
			{
				Set_Attack();
			}
			else if (on_Tongue_Down_Range)
			{
				Set_Attack_Down();
			}
			else if (on_Dash_Range && Fire_Gravity_Timer > 20f)
			{
				Set_Fire_Gravity();
			}
			else if (on_Dash_Range && JumpDash_Timer > 10f)
			{
				Set_JumpDash();
			}
			else if (on_Dash_Range && Fire_Multiple_Timer > ((!(Mon.HP_Ratio > 0.3f)) ? 5f : 10f))
			{
				Set_Fire_Multiple();
			}
			else if (!on_Attack_Range && Move_Lock_Timer <= 0f && !isStuck_Front)
			{
				if (!animator.GetBool("onMove"))
				{
					Set_Move();
				}
				Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, Orig_Speed, global::UnityEngine.Time.deltaTime * 12f);
				base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Move_Speed * facingRight * Mon.Move_Speed);
			}
			else
			{
				Check_Idle();
			}
		}
		if (Dash_Delay <= 0f && Mon.onCrouch)
		{
			on_Crouch = false;
			Mon.onCrouch = false;
		}
	}

	public void Flip()
	{
		Check_Idle();
		facingRight = -facingRight;
		Mon.Flip();
		Flip_Delay = 0.5f + (float)global::UnityEngine.Random.Range(0, 80) * 0.01f;
		Tr_Pos.transform.localScale = new global::UnityEngine.Vector3(-facingRight, 1f, 1f);
		CensoredText.transform.localScale = new global::UnityEngine.Vector3(1.3f * (float)(-facingRight), 1.5f, 1f);
		GetComponent<global::UnityEngine.BoxCollider2D>().offset = new global::UnityEngine.Vector2(0.8f * (float)(-facingRight), -2.617f);
	}

	private void Check_Flip()
	{
		if (facingRight > 0 && base.transform.position.x > Player.transform.position.x)
		{
			if (global::UnityEngine.Mathf.Abs(Player.transform.position.x - base.transform.position.x) > 1f)
			{
				Flip();
			}
		}
		else if (facingRight < 0 && base.transform.position.x < Player.transform.position.x && global::UnityEngine.Mathf.Abs(Player.transform.position.x - base.transform.position.x) > 1f)
		{
			Flip();
		}
	}

	private void Check_Idle()
	{
		if (animator.GetBool("onAttack") || animator.GetBool("onMove") || animator.GetBool("onHit"))
		{
			Set_Idle();
		}
	}

	private void Set_Idle()
	{
		on_Crouch = false;
		Move_Speed = 0f;
		Crouch_Timer = 0f;
		Mon.onCrouch = false;
		animator.SetBool("onAttack", false);
		animator.SetBool("onAttackDown", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onCrouch", false);
		animator.SetBool("onHit", false);
	}

	private void Set_Move()
	{
		on_Crouch = false;
		Move_Speed = 0f;
		Mon.onCrouch = false;
		animator.SetBool("onAttack", false);
		animator.SetBool("onAttackDown", false);
		animator.SetBool("onMove", true);
		animator.SetBool("onCrouch", false);
		animator.SetBool("onHit", false);
	}

	private void Set_Attack()
	{
		Attack_Delay = 1f;
		Move_Speed = 0f;
		animator.SetBool("onAttack", true);
		animator.SetBool("onAttackDown", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onCrouch", false);
		animator.SetBool("onHit", false);
	}

	private void Set_Attack_Down()
	{
		Attack_Delay = 1f;
		Move_Speed = 0f;
		animator.SetBool("onAttack", false);
		animator.SetBool("onAttackDown", true);
		animator.SetBool("onMove", false);
		animator.SetBool("onCrouch", false);
		animator.SetBool("onHit", false);
	}

	private void End_Attack()
	{
		animator.SetBool("onAttack", false);
		animator.SetBool("onAttackDown", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onCrouch", false);
		animator.SetBool("onHit", false);
	}

	private void Set_Fire_Multiple_Triple()
	{
		Set_Fire_Multiple();
		Fire_Triple_Num = 1;
		Fire_Triple_Timer = 5f;
	}

	private void Set_Fire_Multiple()
	{
		Mon.isLockHit = true;
		LockHit_Delay = 1f;
		Attack_Delay = 1f;
		Move_Speed = 0f;
		Fire_Multiple_Timer = 0f;
		Fire_Multiple_1sec = 1f;
		animator.SetTrigger("FireMulti");
		animator.SetBool("onMove", false);
		animator.SetBool("onHit", false);
	}

	private void Set_Fire_Gravity()
	{
		Mon.isLockHit = true;
		LockHit_Delay = 1.2f;
		Attack_Delay = 1f;
		Move_Speed = 0f;
		Fire_Gravity_Timer = 0f;
		Fire_Multiple_1sec = 0f;
		animator.SetTrigger("FireGravity");
		animator.SetBool("onMove", false);
		animator.SetBool("onHit", false);
	}

	private void Set_JumpDash()
	{
		Mon.onCrouch = true;
		Dash_Delay = 1.2f;
		Mon.isInvincible = true;
		Invincible_Timer = 1f;
		Attack_Delay = 0f;
		Move_Speed = 0f;
		JumpDash_Timer = 0f;
		Escape_JumpDash_Timer = 3f;
		animator.SetTrigger("Dash");
		animator.SetBool("onMove", false);
		animator.SetBool("onHit", false);
	}

	private void Set_JumpBack(bool isFront)
	{
		Mon.onCrouch = true;
		Dash_Delay = 1f;
		Mon.isInvincible = true;
		Invincible_Timer = 0.8f;
		Attack_Delay = 0f;
		Move_Speed = 0f;
		JumpBack_Timer = 0f;
		animator.SetTrigger("BackJump");
		animator.SetBool("onMove", false);
		animator.SetBool("onHit", false);
		if (isFront)
		{
			JumpBack_Front_1sec = 1f;
			Escape_JumpFront_Timer = 3f;
		}
		else
		{
			Escape_JumpBack_Timer = 3f;
		}
	}

	private void Set_BackDash()
	{
		Mon.onCrouch = true;
		Dash_Delay = 0.6f;
		Mon.isInvincible = true;
		Invincible_Timer = 0.5f;
		Attack_Delay = 0f;
		Move_Speed = 0f;
		Escape_BackDash_Timer = 2f;
		animator.SetTrigger("BackDash");
		animator.SetBool("onMove", false);
		animator.SetBool("onHit", false);
		base.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(0f, base.GetComponent<UnityEngine.Rigidbody2D>().velocity.y);
		base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * 40f * -facingRight, global::UnityEngine.ForceMode2D.Impulse);
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Atk_2(base.transform.position);
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_9_Growling(base.transform.position);
	}

	private void JumpDash()
	{
		base.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(0f, base.GetComponent<UnityEngine.Rigidbody2D>().velocity.y);
		base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.up * 28f, global::UnityEngine.ForceMode2D.Impulse);
		base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * 55f * facingRight, global::UnityEngine.ForceMode2D.Impulse);
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Atk_2(base.transform.position);
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_9_Growling(base.transform.position);
	}

	private void JumpBack()
	{
		base.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(0f, base.GetComponent<UnityEngine.Rigidbody2D>().velocity.y);
		base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.up * 25f, global::UnityEngine.ForceMode2D.Impulse);
		if (JumpBack_Front_1sec > 0f)
		{
			base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * 45f * facingRight, global::UnityEngine.ForceMode2D.Impulse);
		}
		else
		{
			base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * 45f * -facingRight, global::UnityEngine.ForceMode2D.Impulse);
		}
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Atk_2(base.transform.position);
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_9_Growling(base.transform.position);
	}

	private void Fire()
	{
		if (Fire_Multiple_1sec > 0f)
		{
			float num = 0f;
			global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f);
			global::UnityEngine.Vector3 position = pos_Fire.transform.position;
			vector.x -= position.x;
			vector.y -= position.y;
			num = global::UnityEngine.Mathf.Atan2(vector.y, vector.x) * 57.29578f + 180f;
			num = Check_Fire_Angle(num);
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, num)) as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, num + 25f)) as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, num - 25f)) as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject gameObject4 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, num + 40f)) as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject gameObject5 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, num - 40f)) as global::UnityEngine.GameObject;
			gameObject.transform.Translate(global::UnityEngine.Vector3.right * -1.5f);
			gameObject2.transform.Translate(global::UnityEngine.Vector3.right * -1f);
			gameObject3.transform.Translate(global::UnityEngine.Vector3.right * -1f);
		}
		else
		{
			global::UnityEngine.GameObject gameObject6 = global::UnityEngine.Object.Instantiate(_Fire_G, pos_Fire.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
			gameObject6.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.up * -10f, global::UnityEngine.ForceMode2D.Impulse);
			gameObject6.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * 30f * facingRight, global::UnityEngine.ForceMode2D.Impulse);
		}
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Boss_4_Fire(pos_Fire.position);
	}

	private float Check_Fire_Angle(float angle)
	{
		if (facingRight > 0)
		{
			if (angle > 220f)
			{
				return 220f;
			}
			if (Player.transform.position.y > base.transform.position.y - 7.02f && angle < 190f)
			{
				return 190f;
			}
			if (angle < 140f)
			{
				return 140f;
			}
			return angle;
		}
		if (angle < 320f && angle > 180f)
		{
			return 320f;
		}
		if ((Player.transform.position.y > base.transform.position.y - 7.02f && angle < 90f) || angle > 350f)
		{
			return 350f;
		}
		if (angle > 40f && angle < 180f)
		{
			return 40f;
		}
		return angle;
	}

	private void Set_AttackDelay()
	{
	}

	private void Sound_Attack()
	{
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Atk_5(base.transform.position);
	}

	private void Sound_Dash()
	{
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Atk_3(base.transform.position);
	}

	private void Sound_Mon_Damage()
	{
		if (Snd_Damage_Timer <= 0f)
		{
			Snd_Damage_Timer = 1f + (float)global::UnityEngine.Random.Range(0, 200) * 0.01f;
			global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_9_Damage(base.transform.position);
		}
	}

	public void Set_Death()
	{
		isDeath = true;
		Death_Timer = 0f;
		animator.SetBool("onDeath", true);
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_9_Damage(base.transform.position);
		DeathExplo();
		global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().Set_Shake_Timer(2.5f, global::UnityEngine.GameObject.Find("Main Camera").transform.position);
	}

	private void DeathExplo()
	{
		if (ExploSound_Timer <= 0f)
		{
			ExploSound_Timer = global::UnityEngine.Random.Range(0.2f, 0.5f);
			global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Explo(base.transform.position);
		}
		else
		{
			ExploSound_Timer -= global::UnityEngine.Time.deltaTime;
		}
		for (int i = 0; i < explo_Pos.Length; i++)
		{
			if (Explo_Timer[i] <= 0f)
			{
				Explo_Timer[i] = global::UnityEngine.Random.Range(0.1f, 0.8f);
				Make_Explo(explo_Pos[i]);
			}
			else
			{
				Explo_Timer[i] -= global::UnityEngine.Time.deltaTime;
			}
		}
	}

	private void Make_Explo(global::UnityEngine.Transform posObj)
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Explo, posObj.position, posObj.transform.rotation) as global::UnityEngine.GameObject;
		gameObject.transform.localScale = posObj.transform.localScale;
	}

	private void Start_Hscene()
	{
		on_Hscene = true;
		GM.onEvent = true;
		GM.onHscene = true;
		GM.Hscene_Timer = 1f;
		GM.Hscene_Num = ((global::UnityEngine.Random.Range(0, 10) <= 5) ? 40 : 39);
		if ((bool)global::UnityEngine.Physics2D.Linecast(Tr_4_End.position, Tr_4_Start_H.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground")))
		{
			Player.transform.position = new global::UnityEngine.Vector3(Player.transform.position.x - 1f * (float)facingRight, Player.transform.position.y, 0f);
		}
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(H_Single[GM.Hscene_Num - 39], Player.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
		gameObject.transform.parent = base.transform.parent;
		if (facingRight > 0)
		{
			gameObject.SendMessage("Flip");
		}
		gameObject.GetComponent<H_Ani>().Mon_Object = base.gameObject;
		animator.speed = 0f;
		GetComponent<Mon_Index>().OnOff_Object(false);
		GetComponent<global::UnityEngine.Rigidbody2D>().Sleep();
		base.transform.position = new global::UnityEngine.Vector3(Player.transform.position.x + 1.2f * (float)(-facingRight), Player.transform.position.y + 5f, 0f);
		Mon.isInvincible = true;
		global::UnityEngine.GameObject.Find("Menu").GetComponent<Menu_Control>().H_Object = gameObject;
		global::UnityEngine.GameObject.Find("Main Camera").SendMessage("Hscene_Zoom");
		if (!GM.GameOver)
		{
			Player.SendMessage("H_Down");
		}
		global::UnityEngine.GameObject.Find("Ani").SendMessage("Start_H_Scene");
		H_Timer = 4f + global::UnityEngine.Random.Range(0f, 2f);
		H_Walk_Timer = 1.3f + global::UnityEngine.Random.Range(0f, 0.2f);
	}

	private void End_Hscene()
	{
		on_Hscene = false;
		GM.onEvent = false;
		GM.onHscene = false;
		GM.Hscene_Num = 0;
		GetComponent<Mon_Index>().OnOff_Object(true);
		animator.speed = 1f;
		GetComponent<global::UnityEngine.Rigidbody2D>().WakeUp();
		Mon.isInvincible = false;
		GM.Down_H_After();
		global::UnityEngine.GameObject.Find("Ani").SendMessage("End_H_Scene");
		if (AxiPlayerPrefs.GetInt("UncensoredPatch") != 1)
		{
			Penis.enabled = false;
			Penis_Wet.enabled = false;
			Penis_Censored.enabled = true;
		}
		else
		{
			Penis.enabled = false;
			Penis_Wet.enabled = true;
			Penis_Censored.enabled = false;
		}
	}

	private void Raycasting()
	{
		on_Tongue_Range = global::UnityEngine.Physics2D.Linecast(Tr_1_Start.position, Tr_1_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player_Dmg"));
		on_Tongue_Down_Range = global::UnityEngine.Physics2D.Linecast(Tr_2_Start.position, Tr_2_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player_Dmg"));
		on_Attack_Down_Range = global::UnityEngine.Physics2D.Linecast(Tr_3_Start.position, Tr_3_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player_Dmg"));
		isStuck_Front = global::UnityEngine.Physics2D.Linecast(Tr_4_Start.position, Tr_4_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
		isStuck_Back = global::UnityEngine.Physics2D.Linecast(Tr_5_Start.position, Tr_5_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
		distance = global::UnityEngine.Vector3.Distance(new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f, Player.transform.position.z), base.transform.position);
		dist_X = global::UnityEngine.Mathf.Abs(base.transform.position.x - Player.transform.position.x);
		dist_Y = global::UnityEngine.Mathf.Abs(Player.transform.position.y - (base.transform.position.y - 5f));
		if (!on_Chase)
		{
			if (dist_X < 40f && Player.transform.position.y < base.transform.position.y + 8f && Player.transform.position.y > base.transform.position.y - 15f)
			{
				EnemyState = 2;
			}
			else
			{
				EnemyState = 0;
			}
		}
		if (EnemyState == 2)
		{
			if (!on_Chase)
			{
				on_Chase = true;
			}
			if (dist_X < 6f + rnd_X)
			{
				on_Attack_Range = true;
			}
			else
			{
				on_Attack_Range = false;
			}
			if (dist_X < 12f + rnd_X)
			{
				on_BackWalk = true;
			}
			else
			{
				on_BackWalk = false;
			}
			if (dist_X < 19f + rnd_X)
			{
				on_Dash_Range = true;
			}
			else
			{
				on_Dash_Range = false;
			}
		}
	}
}
