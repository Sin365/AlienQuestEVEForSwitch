public class AI_Mon_35 : global::UnityEngine.MonoBehaviour
{
	private int EnemyState;

	private float Life_Timer;

	private int facingRight = -1;

	private float distance;

	private float dist_X;

	private float dist_Y;

	private float Move_Speed = 1f;

	private float Orig_Speed = 1f;

	private float Walk_Speed = 1f;

	private bool on_Chase;

	private bool on_BackWalk;

	private bool on_Attack_Range;

	private bool on_Arm_Range;

	private bool on_Fire_Range;

	private float Hit_Delay;

	private float Flip_Delay;

	private float Attack_Delay;

	private float Move_Delay;

	private float Move_Lock_Timer;

	private float LockHit_Delay;

	private float Fire_Timer;

	private float Fire_Charge_Timer;

	private float Dash_Timer;

	private float rnd_X;

	private bool isStuck_Front;

	private bool isStuck_Back;

	private bool isStuck_BackLow;

	private bool on_Hscene;

	private float H_Timer;

	private float H_Walk_Timer;

	private float H_Pursue_Timer;

	private float PC_Atk_Timer;

	private int Patrol_State;

	private float Patrol_Idle_Timer;

	private float Patrol_Move_Timer;

	private float Patrol_Range;

	public global::UnityEngine.GameObject Tr_Pos;

	public global::UnityEngine.Transform Tr_1_Start;

	public global::UnityEngine.Transform Tr_1_End;

	public global::UnityEngine.Transform Tr_Front_Start;

	public global::UnityEngine.Transform Tr_Front_End;

	public global::UnityEngine.Transform Tr_Front_End_H;

	public global::UnityEngine.Transform Tr_Back_Start;

	public global::UnityEngine.Transform Tr_Back_End;

	public global::UnityEngine.Transform Tr_BackLow_End;

	public global::UnityEngine.GameObject _Fire;

	public global::UnityEngine.Transform pos_Fire;

	public global::UnityEngine.GameObject[] H_Single;

	public global::UnityEngine.SpriteRenderer CensoredText;

	public global::UnityEngine.SpriteRenderer CensoredBox;

	public global::UnityEngine.SkinnedMeshRenderer Penis;

	public global::UnityEngine.SkinnedMeshRenderer Penis_Censored;

	private float Snd_Damage_Timer;

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
		Orig_Speed = (Move_Speed = 7.5f + rnd_X);
		Walk_Speed = 5f + rnd_X;
		Patrol_Range = 12f + global::UnityEngine.Random.Range(0f, 4f);
		if (AxiPlayerPrefs.GetInt("UncensoredPatch") != 1)
		{
			Penis.enabled = false;
			Penis_Censored.enabled = true;
		}
		else
		{
			Penis.enabled = true;
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
		if (animator.GetBool("onMove"))
		{
			if (Move_Delay < 1f)
			{
				Move_Delay += global::UnityEngine.Time.deltaTime;
			}
		}
		else if (Move_Delay > 0f)
		{
			Move_Delay -= global::UnityEngine.Time.deltaTime;
		}
		if (Mon.Mon_Num == 36)
		{
			Fire_Timer += global::UnityEngine.Time.deltaTime * 2f;
			Fire_Charge_Timer += global::UnityEngine.Time.deltaTime * 2f;
			Dash_Timer += global::UnityEngine.Time.deltaTime;
		}
		else
		{
			Fire_Timer += global::UnityEngine.Time.deltaTime;
			Fire_Charge_Timer += global::UnityEngine.Time.deltaTime;
			Dash_Timer += global::UnityEngine.Time.deltaTime;
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
		Raycasting();
		if (animator.GetBool("onMove") && (GM.GameOver || GM.onHscene))
		{
			animator.speed = 0.55f;
		}
		else if (animator.GetBool("onMove") && H_Walk_Timer > 0f)
		{
			animator.speed = 0.8f;
		}
		else if (animator.speed < 1f)
		{
			animator.speed = 1f;
		}
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
					return;
				}
				if (!animator.GetBool("onMove"))
				{
					Set_Move();
				}
				Move_Speed = Orig_Speed;
				base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Move_Speed * 0.45f * -facingRight * Mon.Move_Speed);
			}
			else
			{
				Check_Idle();
			}
			return;
		}
		if (distance < 50f && (GM.GameOver || GM.onHscene))
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
				else if (dist_Y < 1f && (global::UnityEngine.Mathf.Abs(base.transform.position.x - global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x) < 2f || (isStuck_Front && global::UnityEngine.Mathf.Abs(base.transform.position.x - global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x) < 3.2f)))
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
					Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, Orig_Speed, global::UnityEngine.Time.deltaTime * 5f);
					base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Move_Speed * facingRight * Mon.Move_Speed);
				}
				H_Pursue_Timer += global::UnityEngine.Time.deltaTime;
			}
			else
			{
				if (on_Hscene)
				{
					return;
				}
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
						Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, Orig_Speed, global::UnityEngine.Time.deltaTime * 5f);
						base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Move_Speed * 0.45f * facingRight * Mon.Move_Speed);
					}
				}
				else
				{
					Patrol_Idle_Timer += global::UnityEngine.Time.deltaTime;
					if (Patrol_State == 0)
					{
						if (Patrol_Idle_Timer > 4.5f)
						{
							Patrol_Idle_Timer = 0f;
							Patrol_Move_Timer = 0f;
							Set_Move();
						}
						else if (Patrol_Idle_Timer > 3f)
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
					else if (Patrol_Idle_Timer > 4.5f)
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
			return;
		}
		if (EnemyState < 2)
		{
			Check_Idle();
			return;
		}
		if (animator.GetBool("onHit"))
		{
			Hit_Delay = 0.2f;
			Move_Lock_Timer = 0.2f;
			Attack_Delay = 0f;
			PC_Atk_Timer = 3f;
			if (Mon.Get_HitCombo() > 2)
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
				if (Fire_Charge_Timer > 10f)
				{
					Set_Attack_Charge();
				}
				else if (!isStuck_Back)
				{
					Set_BackDash();
				}
			}
			if (!on_Chase)
			{
				on_Chase = true;
			}
			Move_Speed = 0f;
			return;
		}
		if (Hit_Delay > 0f)
		{
			Hit_Delay -= global::UnityEngine.Time.deltaTime;
			return;
		}
		if (Attack_Delay > 0f)
		{
			Attack_Delay -= global::UnityEngine.Time.deltaTime;
			Move_Lock_Timer = 0.3f;
			return;
		}
		if (Flip_Delay > 0f)
		{
			Flip_Delay -= global::UnityEngine.Time.deltaTime;
			Move_Speed = 0f;
			Check_Idle();
			return;
		}
		if (PC_Atk_Timer <= 0f && !GM.onCloth && GM.Option_Int[3] == 1 && PC.grounded_Now)
		{
			Check_Flip();
			if (dist_Y < 1f && dist_X < 2.1f)
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
				Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, Orig_Speed, global::UnityEngine.Time.deltaTime * 5f);
				base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Move_Speed * facingRight * Mon.Move_Speed);
			}
			else
			{
				Check_Idle();
			}
			return;
		}
		if (Move_Lock_Timer > 0f)
		{
			Move_Lock_Timer -= global::UnityEngine.Time.deltaTime;
		}
		Check_Flip();
		if (Mon.Mon_Num == 36 && on_Attack_Range && Dash_Timer > 7f + rnd_X && !isStuck_Front)
		{
			Set_Dash();
		}
		else if (on_Arm_Range)
		{
			Set_Attack_Down();
		}
		else if (on_Attack_Range && Fire_Charge_Timer > 20f)
		{
			Set_Attack_Charge();
		}
		else if (on_Attack_Range && Fire_Timer > 5f)
		{
			Set_Attack();
		}
		else if (Move_Lock_Timer <= 0f && !isStuck_Front)
		{
			if (!animator.GetBool("onMove"))
			{
				Set_Move();
			}
			Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, Orig_Speed, global::UnityEngine.Time.deltaTime * 5f);
			base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Move_Speed * facingRight * Mon.Move_Speed);
		}
		else
		{
			Check_Idle();
		}
	}

	public void Flip()
	{
		Check_Idle();
		facingRight = -facingRight;
		GetComponent<Monster>().Flip();
		Flip_Delay = 0.5f + (float)global::UnityEngine.Random.Range(0, 80) * 0.01f;
		Tr_Pos.transform.localScale = new global::UnityEngine.Vector3(-facingRight, 1f, 1f);
		CensoredText.transform.localScale = new global::UnityEngine.Vector3(1.3f * (float)(-facingRight), 1.5f, 1f);
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
		if (animator.GetBool("onAttack") || animator.GetBool("onAttackDown") || animator.GetBool("onAttackCharge") || animator.GetBool("onMove") || animator.GetBool("onHit"))
		{
			Set_Idle();
		}
	}

	private void Set_Idle()
	{
		Move_Speed = 0f;
		Mon.isLockHit = false;
		animator.SetBool("onAttack", false);
		animator.SetBool("onAttackDown", false);
		animator.SetBool("onAttackCharge", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onHit", false);
	}

	private void Set_Move()
	{
		Mon.isLockHit = false;
		animator.SetBool("onAttack", false);
		animator.SetBool("onAttackDown", false);
		animator.SetBool("onAttackCharge", false);
		animator.SetBool("onMove", true);
		animator.SetBool("onHit", false);
	}

	private void Set_Attack()
	{
		Attack_Delay = 1f;
		Fire_Timer = 0f;
		animator.SetBool("onAttack", true);
		animator.SetBool("onAttackDown", false);
		animator.SetBool("onAttackCharge", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onHit", false);
	}

	private void Set_Attack_Down()
	{
		Attack_Delay = 0.7f;
		animator.SetBool("onAttack", false);
		animator.SetBool("onAttackDown", true);
		animator.SetBool("onAttackCharge", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onHit", false);
	}

	private void Set_Attack_Charge()
	{
		Attack_Delay = 1f;
		Flip_Delay = 0.5f;
		Fire_Charge_Timer = 0f;
		Mon.isLockHit = true;
		LockHit_Delay = 1.2f;
		animator.SetBool("onAttack", false);
		animator.SetBool("onAttackDown", false);
		animator.SetBool("onAttackCharge", true);
		animator.SetBool("onMove", false);
		animator.SetBool("onHit", false);
	}

	private void End_Attack()
	{
		Mon.isLockHit = false;
		animator.SetBool("onAttack", false);
		animator.SetBool("onAttackDown", false);
		animator.SetBool("onAttackCharge", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onHit", false);
	}

	private void Set_BackDash()
	{
		Mon.isLockHit = true;
		LockHit_Delay = 0.5f;
		animator.SetBool("onAttack", false);
		animator.SetBool("onAttackTongue", false);
		animator.SetBool("onAttackFire", false);
		animator.SetBool("onAttackUp", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onMoveBack", false);
		animator.SetBool("onHit", false);
		Attack_Delay = 0.7f;
		animator.SetTrigger("On_BackDash");
		base.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(0f, base.GetComponent<UnityEngine.Rigidbody2D>().velocity.y);
		base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * 32f * -facingRight, global::UnityEngine.ForceMode2D.Impulse);
		if (isStuck_BackLow)
		{
			base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.up * 20f, global::UnityEngine.ForceMode2D.Impulse);
		}
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Atk_2(base.transform.position);
		Fire_Charge_Timer -= 5f;
		if (Fire_Charge_Timer > 15f)
		{
			Fire_Charge_Timer = 15f;
		}
	}

	private void Set_Dash()
	{
		Mon.isLockHit = true;
		LockHit_Delay = 0.7f;
		animator.SetBool("onAttack", false);
		animator.SetBool("onAttackTongue", false);
		animator.SetBool("onAttackFire", false);
		animator.SetBool("onAttackUp", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onMoveBack", false);
		animator.SetBool("onHit", false);
		Dash_Timer = 0f;
		Attack_Delay = 1f;
		animator.SetTrigger("On_Dash");
		base.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(0f, base.GetComponent<UnityEngine.Rigidbody2D>().velocity.y);
		base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * 35f * facingRight, global::UnityEngine.ForceMode2D.Impulse);
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Atk_2(base.transform.position);
	}

	private void Set_Fire()
	{
		float num = 0f;
		global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f);
		global::UnityEngine.Vector3 position = pos_Fire.position;
		vector.x -= position.x;
		vector.y -= position.y;
		num = global::UnityEngine.Mathf.Atan2(vector.y, vector.x) * 57.29578f + 180f;
		num = Check_Fire_Angle(num);
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(0f, 0f, num)) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(0f, 0f, num + 15f)) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(0f, 0f, num - 15f)) as global::UnityEngine.GameObject;
		gameObject.transform.Translate(global::UnityEngine.Vector3.right * 0.5f);
		Fire_Charge_Timer -= 5f;
		if (Fire_Charge_Timer > 15f)
		{
			Fire_Charge_Timer = 15f;
		}
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Boss_4_Fire(base.transform.position);
	}

	private void Set_Fire_Charge()
	{
		float num = 0f;
		global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f);
		global::UnityEngine.Vector3 position = pos_Fire.position;
		vector.x -= position.x;
		vector.y -= position.y;
		num = global::UnityEngine.Mathf.Atan2(vector.y, vector.x) * 57.29578f + 180f;
		num = Check_Fire_Angle(num);
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(0f, 0f, num)) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(0f, 0f, num + 20f)) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(0f, 0f, num - 20f)) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject4 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(0f, 0f, num + 40f)) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject5 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(0f, 0f, num - 40f)) as global::UnityEngine.GameObject;
		gameObject.transform.Translate(global::UnityEngine.Vector3.right * 0.5f);
		Fire_Timer = 0f;
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Boss_4_Fire(base.transform.position);
	}

	private float Check_Fire_Angle(float angle)
	{
		if (facingRight > 0)
		{
			if (angle > 220f)
			{
				return 220f;
			}
			if (Player.transform.position.y > base.transform.position.y - 7f && angle < 190f)
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
		if ((Player.transform.position.y > base.transform.position.y - 7f && angle < 90f) || angle > 350f)
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

	private void Sound_Mon_Attack()
	{
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Atk_1(base.transform.position);
	}

	private void Sound_Mon_Damage()
	{
		if (Snd_Damage_Timer <= 0f)
		{
			Snd_Damage_Timer = 1f + (float)global::UnityEngine.Random.Range(0, 200) * 0.01f;
			global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_9_Damage(base.transform.position);
		}
	}

	private void Start_Hscene()
	{
		on_Hscene = true;
		GM.onEvent = true;
		GM.onHscene = true;
		GM.Hscene_Timer = 1f;
		int num = 0;
		if (Mon.Mon_Num == 35)
		{
			GM.Hscene_Num = 33;
		}
		else
		{
			num = ((global::UnityEngine.Random.Range(0, 10) > 5) ? 1 : 0);
			GM.Hscene_Num = 36 + num;
		}
		if ((bool)global::UnityEngine.Physics2D.Linecast(Tr_Front_Start.position, Tr_Front_End_H.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground")))
		{
			Player.transform.position = new global::UnityEngine.Vector3(Player.transform.position.x - 1.2f * (float)facingRight, Player.transform.position.y, 0f);
		}
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(H_Single[num], Player.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
		gameObject.transform.parent = base.transform.parent;
		if (facingRight > 0)
		{
			gameObject.SendMessage("Flip");
		}
		gameObject.GetComponent<H_Ani>().Mon_Object = base.gameObject;
		animator.speed = 0f;
		GetComponent<Mon_Index>().OnOff_Object(false);
		GetComponent<global::UnityEngine.Rigidbody2D>().Sleep();
		base.transform.position = new global::UnityEngine.Vector3(Player.transform.position.x + 1.2f * (float)(-facingRight), Player.transform.position.y + 4.5f, 0f);
		Mon.isInvincible = true;
		global::UnityEngine.GameObject.Find("Menu").GetComponent<Menu_Control>().H_Object = gameObject;
		global::UnityEngine.GameObject.Find("Main Camera").SendMessage("Hscene_Zoom");
		if (!GM.GameOver)
		{
			Player.SendMessage("H_Down");
		}
		global::UnityEngine.GameObject.Find("Ani").SendMessage("Start_H_Scene");
		H_Timer = 4f + global::UnityEngine.Random.Range(0f, 2f);
		H_Walk_Timer = 2.2f + global::UnityEngine.Random.Range(0f, 0.3f);
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
			Penis_Censored.enabled = true;
		}
		else
		{
			Penis.enabled = true;
			Penis_Censored.enabled = false;
		}
	}

	private void Raycasting()
	{
		isStuck_Front = global::UnityEngine.Physics2D.Linecast(Tr_Front_Start.position, Tr_Front_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
		isStuck_Back = global::UnityEngine.Physics2D.Linecast(Tr_Back_Start.position, Tr_Back_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
		isStuck_BackLow = global::UnityEngine.Physics2D.Linecast(Tr_Back_Start.position, Tr_BackLow_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
		on_Arm_Range = global::UnityEngine.Physics2D.Linecast(Tr_1_Start.position, Tr_1_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player_Dmg"));
		distance = global::UnityEngine.Vector3.Distance(new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f, Player.transform.position.z), base.transform.position);
		dist_X = global::UnityEngine.Mathf.Abs(base.transform.position.x - Player.transform.position.x);
		dist_Y = global::UnityEngine.Mathf.Abs(Player.transform.position.y - (base.transform.position.y - 4.5f));
		if (on_Chase)
		{
			if (dist_X > 60f || dist_Y > 30f)
			{
				on_Chase = false;
				EnemyState = 0;
			}
			else
			{
				EnemyState = 2;
			}
		}
		else if (dist_X < 40f && Player.transform.position.y < base.transform.position.y + 8.5f && Player.transform.position.y > base.transform.position.y - 14.5f)
		{
			EnemyState = 2;
		}
		else
		{
			EnemyState = 0;
		}
		if (EnemyState == 2)
		{
			if (!on_Chase)
			{
				on_Chase = true;
			}
			if (dist_X < 15f + rnd_X)
			{
				on_Attack_Range = true;
			}
			else
			{
				on_Attack_Range = false;
			}
		}
	}
}
