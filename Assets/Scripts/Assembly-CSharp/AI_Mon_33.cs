using UnityEngine;

public class AI_Mon_33 : global::UnityEngine.MonoBehaviour
{
	public enum Event_Type
	{
		None = 0,
		Top = 1,
		Bottom = 2
	}

	public AI_Mon_33.Event_Type event_Type;

	private float event_Timer;

	private bool on_Event_Rotate;

	private bool on_Event_Scale;

	private int EnemyState;

	private float Life_Timer;

	private int facingRight = -1;

	private float distance;

	private float dist_X;

	private float dist_Y;

	private float Move_Speed = 1f;

	private float Walk_Speed = 1f;

	private bool on_Chase;

	private bool on_Crouch;

	private bool on_BackWalk;

	private bool on_Attack_Range;

	private bool on_Tail_Range;

	private bool on_Tongue_Range;

	private bool on_Tail_Down_Range;

	private bool on_Tongue_Down_Range;

	private bool on_Fire_Range;

	private bool on_Rolling;

	private float Rolling_Delay;

	private float Hit_Delay;

	private float Flip_Delay;

	private float Attack_Delay;

	private float BackDash_Delay;

	private float Move_Delay;

	private float Move_Lock_Timer;

	private float LockHit_Delay;

	private float Fire_Timer;

	private float Rolling_Timer;

	private float Crouch_Timer;

	private float rnd_X;

	private bool isStuck_Front;

	private bool isStuck_Back;

	private bool isStuck_BackLow;

	private float HP_Recover_Timer;

	private bool on_Hscene;

	private bool on_Hscene_Range;

	private float H_Timer;

	private float H_Walk_Timer;

	private float H_Pursue_Timer;

	private int Patrol_State;

	private float Patrol_Idle_Timer;

	private float Patrol_Move_Timer;

	private float Patrol_Range;

	private bool onJump;

	private float Jump_Delay;

	private bool onGround;

	private float Ground_Timer;

	private float ChaseJump_Timer;

	private float ColBox_Timer;

	public global::UnityEngine.GameObject Tr_Pos;

	public global::UnityEngine.Transform Tr_1_Start;

	public global::UnityEngine.Transform Tr_1_End;

	public global::UnityEngine.Transform Tr_2_Start;

	public global::UnityEngine.Transform Tr_2_End;

	public global::UnityEngine.Transform Tr_3_Start;

	public global::UnityEngine.Transform Tr_3_End;

	public global::UnityEngine.Transform Tr_4_Start;

	public global::UnityEngine.Transform Tr_4_End;

	public global::UnityEngine.Transform Tr_Front_Start;

	public global::UnityEngine.Transform Tr_Front_End;

	public global::UnityEngine.Transform Tr_Front_End_H;

	public global::UnityEngine.Transform Tr_Back_Start;

	public global::UnityEngine.Transform Tr_Back_End;

	public global::UnityEngine.Transform Tr_BackLow_End;

	public global::UnityEngine.Transform Tr_Ground_R;

	public global::UnityEngine.Transform Tr_Ground_L;

	public global::UnityEngine.Transform Mon_Ctrl;

	public global::UnityEngine.GameObject _Fire;

	public global::UnityEngine.Transform pos_Fire;

	public global::UnityEngine.GameObject _SoundRolling;

	private float sound_Rolling_Timer;

	public global::UnityEngine.GameObject _col_rolling;

	public Col_Xeno_Rolling COL_Rolling;

	public global::UnityEngine.GameObject _blood_absorb;

	public global::UnityEngine.Transform pos_Absorb;

	private Item_Blood_Absorb Blood_Absorb;

	public global::UnityEngine.GameObject[] H_Single;

	private float Snd_Damage_Timer;

	private global::UnityEngine.Animator animator;

	private global::UnityEngine.AudioSource sound_Base;

	private Monster Mon;
    Player_Control PC => GameManager.instance.PC;
    GameObject Player => GameManager.instance.gobj_Player;

    GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//Player = global::UnityEngine.GameObject.Find("Player");
		//PC = Player.GetComponent<Player_Control>();
		Mon = GetComponent<Monster>();
		animator = GetComponent<global::UnityEngine.Animator>();
		sound_Base = GetComponent<global::UnityEngine.AudioSource>();
		rnd_X = (float)global::UnityEngine.Random.Range(0, 50) * 0.01f;
		ChaseJump_Timer = rnd_X * -3f;
		Move_Speed = 18f + rnd_X;
		Walk_Speed = 12f + rnd_X;
		Patrol_Range = 14f + global::UnityEngine.Random.Range(0f, 4f);
		if (Player.transform.position.x > base.transform.position.x)
		{
			Flip();
		}
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_col_rolling, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
		COL_Rolling = gameObject.GetComponent<Col_Xeno_Rolling>();
		COL_Rolling.Mon = Mon;
		global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(_blood_absorb, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
		Blood_Absorb = gameObject2.GetComponent<Item_Blood_Absorb>();
		Blood_Absorb.Mon = Mon;
		if (event_Type != AI_Mon_33.Event_Type.None)
		{
			Event_Start();
		}
		else if (global::UnityEngine.Random.Range(0, 10) > 5)
		{
			Crouch_Timer = 20f + global::UnityEngine.Random.Range(0f, 3f);
			Set_Crouch();
		}
		else
		{
			Crouch_Timer = 15f + global::UnityEngine.Random.Range(0f, 3f);
		}
	}

	private void Event_Start()
	{
		Mon.onEvent = true;
		Mon.isLockHit = true;
		Flip_Delay = 0f;
		if (event_Type == AI_Mon_33.Event_Type.Top)
		{
			GetComponent<Mon_Index>().Xenomorph_Layer(0);
			animator.Play("Ambush", 0, 0f);
			animator.speed = 0f;
			if (facingRight < 0)
			{
				base.transform.localRotation = global::UnityEngine.Quaternion.Euler(0f, 0f, 120f);
			}
			else
			{
				base.transform.localRotation = global::UnityEngine.Quaternion.Euler(0f, 0f, 240f);
				Mon.XenoEvent_Set_FacingRight();
			}
			GetComponent<global::UnityEngine.Rigidbody2D>().Sleep();
			GetComponent<global::UnityEngine.BoxCollider2D>().enabled = false;
		}
		else
		{
			on_Event_Scale = true;
			GetComponent<global::UnityEngine.BoxCollider2D>().enabled = false;
			GetComponent<global::UnityEngine.Rigidbody2D>().WakeUp();
			Mon.onEvent = false;
			Mon.Event_Timer = 1f;
			animator.Play("Jump", 0, 0f);
			Set_Jump(6 * facingRight, 65f);
		}
	}

	private void Event_End()
	{
		if (event_Type != AI_Mon_33.Event_Type.None)
		{
			event_Type = AI_Mon_33.Event_Type.None;
			GetComponent<global::UnityEngine.BoxCollider2D>().enabled = true;
		}
	}

	private void Top_Drop()
	{
		if (!on_Event_Rotate)
		{
			on_Event_Rotate = true;
			GetComponent<global::UnityEngine.Rigidbody2D>().WakeUp();
			Mon.onEvent = false;
			Mon.Event_Timer = 0.5f;
			onJump = true;
			Jump_Delay = 0.2f;
			global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_9_Dash(base.transform.position);
			global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Atk_2(base.transform.position);
			if (global::UnityEngine.Random.Range(0, 10) > 5)
			{
				animator.SetBool("onCrouch", true);
				on_Crouch = true;
				Mon.onCrouch = true;
			}
		}
	}

	private void Update()
	{
		if (GM.Paused || on_Hscene)
		{
			return;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
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
		Fire_Timer += global::UnityEngine.Time.deltaTime;
		Rolling_Timer += global::UnityEngine.Time.deltaTime;
		if (Crouch_Timer > 0f)
		{
			Crouch_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (LockHit_Delay > 0f)
		{
			LockHit_Delay -= global::UnityEngine.Time.deltaTime;
		}
		else if (event_Type == AI_Mon_33.Event_Type.None && Mon.isLockHit)
		{
			Mon.isLockHit = false;
		}
		Raycasting();
		if (H_Walk_Timer > 0f && on_Crouch)
		{
			animator.speed = global::UnityEngine.Mathf.Lerp(animator.speed, 0.7f, global::UnityEngine.Time.deltaTime * 5f);
		}
		else if (event_Type == AI_Mon_33.Event_Type.None && animator.speed < 1f)
		{
			animator.speed = 1f;
		}
		if (on_Event_Rotate)
		{
			base.transform.localRotation = global::UnityEngine.Quaternion.Lerp(base.transform.localRotation, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f), global::UnityEngine.Time.deltaTime * 10f);
			if (base.transform.localRotation.eulerAngles.z < 1f)
			{
				on_Event_Rotate = false;
				base.transform.localRotation = global::UnityEngine.Quaternion.Euler(0f, 0f, 0f);
				if (facingRight > 0)
				{
					Mon.XenoEvent_Set_FacingRight();
				}
				Mon.Make_Icon();
				GetComponent<Mon_Index>().Xenomorph_Layer(25);
			}
		}
		else if (on_Event_Scale)
		{
			base.transform.localScale = global::UnityEngine.Vector3.Lerp(base.transform.localScale, new global::UnityEngine.Vector3(1f, 1f, 1f), global::UnityEngine.Time.deltaTime * 5f);
			if (base.transform.localScale.x < 1.01f)
			{
				on_Event_Scale = false;
				base.transform.localScale = new global::UnityEngine.Vector3(1f, 1f, 1f);
				Mon.Make_Icon();
				GetComponent<Mon_Index>().Xenomorph_Layer(25);
			}
		}
		if (ColBox_Timer > 0f)
		{
			ColBox_Timer -= global::UnityEngine.Time.deltaTime;
		}
		else if (event_Type == AI_Mon_33.Event_Type.None && !GetComponent<global::UnityEngine.BoxCollider2D>().enabled)
		{
			GetComponent<global::UnityEngine.BoxCollider2D>().enabled = true;
		}
		if (event_Type != AI_Mon_33.Event_Type.None)
		{
			Mon.isLockHit = true;
			if (event_Type == AI_Mon_33.Event_Type.Top)
			{
				if (event_Timer > 1f)
				{
					animator.speed = 1f;
				}
				else if (Player.transform.position.y < base.transform.position.y && dist_Y < 15f && dist_X < 12f)
				{
					event_Timer += global::UnityEngine.Time.deltaTime;
				}
				else
				{
					event_Timer = 0f;
				}
			}
		}
		else if (onJump)
		{
			Jump_Delay += global::UnityEngine.Time.deltaTime;
			if (Jump_Delay > 0.3f && onGround)
			{
				onJump = false;
				Attack_Delay = 0.6f;
				Set_Idle();
			}
		}
		else if (!onGround && base.GetComponent<UnityEngine.Rigidbody2D>().velocity.y < -1f && (animator.GetBool("onMove") || animator.GetBool("onWalk")))
		{
			onJump = true;
			Jump_Delay = 0.5f;
			animator.SetTrigger("On_Drop");
			animator.SetBool("onJump", true);
		}
		else if (ChaseJump_Timer > 0.6f)
		{
			ChaseJump_Timer = -5f + rnd_X * 2f;
			Set_Jump(10 * facingRight, 60f);
		}
		else if (H_Timer > 0f)
		{
			if (on_Rolling)
			{
				End_Rolling();
			}
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
				else if (on_Crouch)
				{
					if (!animator.GetBool("onMove"))
					{
						Set_Move();
					}
					base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Walk_Speed * 0.5f * -facingRight * Mon.Move_Speed);
				}
				else
				{
					if (!animator.GetBool("onWalk"))
					{
						Set_Walk();
					}
					base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Walk_Speed * 0.5f * -facingRight * Mon.Move_Speed);
				}
			}
			else
			{
				Check_Idle();
			}
		}
		else if (distance < 50f && (GM.GameOver || GM.onHscene))
		{
			if (on_Rolling)
			{
				End_Rolling();
			}
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
					if (on_Crouch)
					{
						base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Walk_Speed * 1f * facingRight * Mon.Move_Speed);
					}
					else
					{
						base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Move_Speed * 1f * facingRight * Mon.Move_Speed);
					}
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
					else if (on_Crouch)
					{
						base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Walk_Speed * 0.9f * facingRight * Mon.Move_Speed);
					}
					else
					{
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
							if (Crouch_Timer <= 0f)
							{
								if (on_Crouch)
								{
									Reset_Crouch();
								}
								else
								{
									Set_Crouch();
								}
							}
						}
						else if (Patrol_Idle_Timer > 1.5f)
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
		else if (EnemyState < 2)
		{
			if (on_Rolling)
			{
				End_Rolling();
			}
			Check_Idle();
		}
		else
		{
			Action_Mon_33();
		}
		if (on_Rolling)
		{
			if (!sound_Base.isPlaying)
			{
				sound_Base.Play();
			}
			sound_Base.volume = global::UnityEngine.Mathf.Lerp(sound_Base.volume, 0.3f, global::UnityEngine.Time.deltaTime * 1f);
			if (!Mon.isPass)
			{
				Mon.isPass = true;
			}
			if (!COL_Rolling.Enabled)
			{
				COL_Rolling.COL_ON();
			}
		}
		else
		{
			if (sound_Base.isPlaying)
			{
				sound_Base.Stop();
				sound_Base.volume = 0f;
			}
			if (Mon.isPass)
			{
				Mon.isPass = false;
			}
			if (COL_Rolling.Enabled)
			{
				COL_Rolling.COL_OFF();
			}
		}
		COL_Rolling.transform.position = base.transform.position;
		if (pos_Absorb != null && Blood_Absorb != null)
		{
			Blood_Absorb.transform.position = pos_Absorb.position;
		}
	}

	private void Action_Mon_33()
	{
		if (on_Rolling)
		{
			Rolling_Delay -= global::UnityEngine.Time.deltaTime;
			if (Rolling_Delay > 0f)
			{
				Mon_Ctrl.Rotate(new global::UnityEngine.Vector3(0f, 0f, 1800f * global::UnityEngine.Time.deltaTime));
			}
			else
			{
				End_Rolling();
			}
			sound_Rolling_Timer -= global::UnityEngine.Time.deltaTime;
			if (sound_Rolling_Timer <= 0f)
			{
				sound_Rolling_Timer = 0.12f;
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_SoundRolling, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
			}
			return;
		}
		if (animator.GetBool("onHit"))
		{
			Hit_Delay = 0.2f;
			Move_Lock_Timer = 0.2f;
			Attack_Delay = 0f;
			if (Flip_Delay <= 0f)
			{
				Check_Flip();
			}
			if (Mon.MagicFire_1_Num > 3)
			{
				if (!on_Crouch)
				{
					Set_Crouch();
				}
				Crouch_Timer = 10f;
			}
			else if (Mon.Get_HitCombo() > 2)
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
				if (isStuck_Back)
				{
					Set_Rolling();
				}
				else
				{
					Set_BackDash();
				}
				Crouch_Timer = 5f + global::UnityEngine.Random.Range(0f, 4f);
			}
			else if (Fire_Timer > 4f)
			{
				if (on_Crouch)
				{
					Set_BackDash();
				}
				else
				{
					Set_BackCrouch();
				}
			}
			if (!on_Chase)
			{
				on_Chase = true;
			}
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
		if (BackDash_Delay > 0f)
		{
			BackDash_Delay -= global::UnityEngine.Time.deltaTime;
			return;
		}
		if (Flip_Delay > 0f)
		{
			Flip_Delay -= global::UnityEngine.Time.deltaTime;
			Check_Idle();
			return;
		}
		if (Move_Lock_Timer > 0f)
		{
			Move_Lock_Timer -= global::UnityEngine.Time.deltaTime;
		}
		Check_Flip();
		if (dist_X > 10f && Crouch_Timer <= 0f)
		{
			if (on_Crouch)
			{
				Reset_Crouch();
			}
			else
			{
				Set_Crouch();
			}
		}
		else if (!isStuck_Front && GM.Option_Int[3] == 1 && !GM.onCloth && GM.User_Input_Timer > 1f && GM.Hscene_Num == 0 && GM.Hscene_Timer <= 0f)
		{
			Rush_Hscene();
		}
		else if (on_Crouch)
		{
			if (on_Tongue_Down_Range)
			{
				Set_Attack_Tongue();
			}
			else if (on_Tail_Down_Range)
			{
				Set_Attack();
			}
			else if (on_BackWalk && Move_Lock_Timer <= 0f && !isStuck_Back)
			{
				if (!animator.GetBool("onMove"))
				{
					Set_Move();
				}
				base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Walk_Speed * -facingRight * Mon.Move_Speed);
			}
			else if (!on_Attack_Range && Move_Lock_Timer <= 0f && !isStuck_Front)
			{
				if (!animator.GetBool("onMove"))
				{
					Set_Move();
				}
				else
				{
					base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Walk_Speed * facingRight * Mon.Move_Speed);
				}
			}
			else
			{
				Check_Idle();
			}
		}
		else if (Fire_Timer > 4f + rnd_X)
		{
			Set_Attack_Fire();
		}
		else if (Rolling_Timer > 7f + rnd_X)
		{
			Set_Rolling();
		}
		else if (on_Tongue_Range)
		{
			Set_Attack_Tongue();
		}
		else if (on_Tail_Range)
		{
			Set_Attack();
		}
		else if (on_BackWalk && Move_Lock_Timer <= 0f && !isStuck_Back)
		{
			if (!animator.GetBool("onWalk"))
			{
				Set_Walk();
			}
			base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Walk_Speed * -facingRight * Mon.Move_Speed);
		}
		else if (!on_Attack_Range && Move_Lock_Timer <= 0f && !isStuck_Front)
		{
			if (!animator.GetBool("onMove"))
			{
				Set_Move();
			}
			else
			{
				base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Move_Speed * facingRight * Mon.Move_Speed);
			}
		}
		else
		{
			Check_Idle();
		}
	}

	private void Rush_Hscene()
	{
		if (on_Hscene_Range)
		{
			if (PC.grounded_Now && (PC.State == Player_Control.AniState.Idle || PC.State == Player_Control.AniState.Run || PC.State == Player_Control.AniState.Sit || PC.State == Player_Control.AniState.Down))
			{
				Start_Hscene();
			}
			else
			{
				Set_Attack_Tongue();
			}
			return;
		}
		if (!animator.GetBool("onMove"))
		{
			Set_Move();
		}
		if (on_Crouch)
		{
			base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Walk_Speed * facingRight * Mon.Move_Speed);
		}
		else
		{
			base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Move_Speed * facingRight * Mon.Move_Speed);
		}
	}

	public void Flip()
	{
		facingRight = -facingRight;
		Mon.Flip();
		Flip_Delay = 0.5f + (float)global::UnityEngine.Random.Range(0, 80) * 0.01f;
		Tr_Pos.transform.localScale = new global::UnityEngine.Vector3(-facingRight, 1f, 1f);
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

	private void Set_Jump(float _X, float _Y)
	{
		animator.SetBool("onAttack", false);
		animator.SetBool("onAttackTongue", false);
		animator.SetBool("onAttackFire", false);
		animator.SetBool("onAttackUp", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onWalk", false);
		animator.SetBool("onHit", false);
		animator.SetBool("onJump", true);
		if (!on_Crouch && global::UnityEngine.Random.Range(0, 10) > 5)
		{
			animator.SetBool("onCrouch", true);
			on_Crouch = true;
			Mon.onCrouch = true;
			Crouch_Timer = 1f + global::UnityEngine.Random.Range(0f, 1f);
		}
		onJump = true;
		Jump_Delay = 0f;
		base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * _X, global::UnityEngine.ForceMode2D.Impulse);
		base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.up * _Y, global::UnityEngine.ForceMode2D.Impulse);
		ColBox_Timer = 0.6f;
		GetComponent<global::UnityEngine.BoxCollider2D>().enabled = false;
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_9_Dash(base.transform.position);
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Atk_2(base.transform.position);
	}

	private void Check_Idle()
	{
		if (animator.GetBool("onAttack") || animator.GetBool("onAttackTongue") || animator.GetBool("onAttackFire") || animator.GetBool("onAttackUp") || animator.GetBool("onMove") || animator.GetBool("onHit") || animator.GetBool("onWalk"))
		{
			Set_Idle();
		}
	}

	private void Set_Idle()
	{
		Mon.isLockHit = false;
		animator.SetBool("onAttack", false);
		animator.SetBool("onAttackTongue", false);
		animator.SetBool("onAttackFire", false);
		animator.SetBool("onAttackUp", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onWalk", false);
		animator.SetBool("onHit", false);
		animator.SetBool("onJump", false);
	}

	private void Set_Move()
	{
		Mon.isLockHit = false;
		animator.SetBool("onAttack", false);
		animator.SetBool("onAttackTongue", false);
		animator.SetBool("onAttackFire", false);
		animator.SetBool("onAttackUp", false);
		animator.SetBool("onMove", true);
		animator.SetBool("onWalk", false);
		animator.SetBool("onHit", false);
		animator.SetBool("onJump", false);
	}

	private void Set_Walk()
	{
		Mon.isLockHit = false;
		animator.SetBool("onAttack", false);
		animator.SetBool("onAttackTongue", false);
		animator.SetBool("onAttackFire", false);
		animator.SetBool("onAttackUp", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onWalk", true);
		animator.SetBool("onHit", false);
		animator.SetBool("onJump", false);
	}

	private void Set_Attack()
	{
		Attack_Delay = 0.8f;
		animator.SetBool("onAttack", true);
		animator.SetBool("onAttackTongue", false);
		animator.SetBool("onAttackFire", false);
		animator.SetBool("onAttackUp", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onWalk", false);
		animator.SetBool("onHit", false);
		animator.SetBool("onJump", false);
	}

	private void Set_Attack_Tongue()
	{
		Attack_Delay = 0.8f;
		animator.SetBool("onAttack", false);
		animator.SetBool("onAttackTongue", true);
		animator.SetBool("onAttackFire", false);
		animator.SetBool("onAttackUp", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onWalk", false);
		animator.SetBool("onHit", false);
		animator.SetBool("onJump", false);
	}

	private void Set_Attack_Fire()
	{
		Attack_Delay = 2f;
		Fire_Timer = 0f;
		Mon.isLockHit = true;
		LockHit_Delay = 1.2f;
		animator.SetBool("onAttack", false);
		animator.SetBool("onAttackTongue", false);
		animator.SetBool("onAttackFire", true);
		animator.SetBool("onAttackUp", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onWalk", false);
		animator.SetBool("onHit", false);
		animator.SetBool("onJump", false);
	}

	private void End_Attack()
	{
		Mon.isLockHit = false;
		animator.SetBool("onAttack", false);
		animator.SetBool("onAttackTongue", false);
		animator.SetBool("onAttackFire", false);
		animator.SetBool("onAttackUp", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onWalk", false);
		animator.SetBool("onHit", false);
		animator.SetBool("onJump", false);
	}

	private void Set_Crouch()
	{
		if (!animator.GetBool("onMove"))
		{
			Set_Idle();
		}
		animator.SetBool("onCrouch", true);
		on_Crouch = true;
		Mon.onCrouch = true;
		Crouch_Timer = 8f + global::UnityEngine.Random.Range(0f, 2f);
		Flip_Delay = 0.7f;
	}

	private void Reset_Crouch()
	{
		if (!animator.GetBool("onMove"))
		{
			Set_Idle();
		}
		animator.SetBool("onCrouch", false);
		on_Crouch = false;
		Mon.onCrouch = false;
		Crouch_Timer = 15f + global::UnityEngine.Random.Range(0f, 3f);
		Flip_Delay = 0.7f;
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
		animator.SetBool("onWalk", false);
		animator.SetBool("onHit", false);
		BackDash_Delay = 0.7f;
		Flip_Delay = 0f;
		Attack_Delay = 0f;
		animator.SetTrigger("On_BackDash");
		animator.SetBool("onCrouch", false);
		on_Crouch = false;
		Mon.onCrouch = false;
		base.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(0f, base.GetComponent<UnityEngine.Rigidbody2D>().velocity.y);
		base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * 25f * -facingRight, global::UnityEngine.ForceMode2D.Impulse);
		if (isStuck_BackLow)
		{
			base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.up * 20f, global::UnityEngine.ForceMode2D.Impulse);
		}
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Atk_2(base.transform.position);
		Fire_Timer = 10f;
		Rolling_Timer = 10f;
	}

	private void Set_BackCrouch()
	{
		Mon.isLockHit = true;
		LockHit_Delay = 0.5f;
		animator.SetBool("onAttack", false);
		animator.SetBool("onAttackTongue", false);
		animator.SetBool("onAttackFire", false);
		animator.SetBool("onAttackUp", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onWalk", false);
		animator.SetBool("onHit", false);
		BackDash_Delay = 0.7f;
		Flip_Delay = 0f;
		Attack_Delay = 0f;
		animator.SetTrigger("On_BackSit");
		animator.SetBool("onCrouch", true);
		on_Crouch = true;
		Mon.onCrouch = true;
		base.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(0f, base.GetComponent<UnityEngine.Rigidbody2D>().velocity.y);
		base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * 23f * -facingRight, global::UnityEngine.ForceMode2D.Impulse);
		if (isStuck_BackLow)
		{
			base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.up * 20f, global::UnityEngine.ForceMode2D.Impulse);
		}
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Atk_2(base.transform.position);
	}

	private void Set_Rolling()
	{
		Mon.isLockHit = true;
		LockHit_Delay = 0.5f;
		Hit_Delay = 2f;
		Rolling_Timer = 0f;
		animator.SetTrigger("On_RollingStart");
	}

	private void Start_Rolling()
	{
		Mon.isLockHit = true;
		LockHit_Delay = 0.5f;
		animator.SetBool("onAttack", false);
		animator.SetBool("onAttackTongue", false);
		animator.SetBool("onAttackFire", false);
		animator.SetBool("onAttackUp", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onWalk", false);
		animator.SetBool("onHit", false);
		Rolling_Delay = 0.7f;
		sound_Rolling_Timer = 0f;
		animator.SetBool("onRolling", true);
		on_Rolling = true;
		base.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(0f, base.GetComponent<UnityEngine.Rigidbody2D>().velocity.y);
		base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * 50f * facingRight, global::UnityEngine.ForceMode2D.Impulse);
	}

	private void End_Rolling()
	{
		Mon.isLockHit = false;
		Hit_Delay = 0f;
		Flip_Delay = 0f;
		Attack_Delay = 0.3f;
		animator.SetBool("onRolling", false);
		on_Rolling = false;
		if (facingRight > 0)
		{
			Mon_Ctrl.rotation = global::UnityEngine.Quaternion.Euler(0f, 180f, 0f);
		}
		else
		{
			Mon_Ctrl.rotation = global::UnityEngine.Quaternion.Euler(0f, 0f, 0f);
		}
	}

	private void Set_Fire()
	{
		float num = 0f;
		global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f);
		global::UnityEngine.Vector3 position = pos_Fire.transform.position;
		vector.x -= position.x;
		vector.y -= position.y;
		num = global::UnityEngine.Mathf.Atan2(vector.y, vector.x) * 57.29578f + 180f;
		num = Check_Fire_Angle(num);
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, num)) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(0f, 0f, num + 16f)) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(0f, 0f, num - 16f)) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject4 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(0f, 0f, num + 35f)) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject5 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(0f, 0f, num - 35f)) as global::UnityEngine.GameObject;
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
			if (Player.transform.position.y > base.transform.position.y - 6.02f && angle < 190f)
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
		if ((Player.transform.position.y > base.transform.position.y - 6.02f && angle < 90f) || angle > 350f)
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

	private void Sound_Attack_Arm()
	{
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Atk_1(base.transform.position);
	}

	private void Sound_Attack_Tongue()
	{
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Atk_6(base.transform.position);
	}

	private void Sound_Attack_Tail()
	{
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Atk_7(base.transform.position);
	}

	private void Sound_Mon_Damage()
	{
		if (Snd_Damage_Timer <= 0f)
		{
			Snd_Damage_Timer = 1f + (float)global::UnityEngine.Random.Range(0, 200) * 0.01f;
			global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_9_Damage(base.transform.position);
		}
	}

	public void Delete_Col_Rolling()
	{
		global::UnityEngine.Object.Destroy(COL_Rolling.gameObject);
	}

	private void Start_Hscene()
	{
		on_Hscene = true;
		GM.onEvent = true;
		GM.onHscene = true;
		GM.Hscene_Num = 24;
		GM.Hscene_Timer = 1f;
		if ((bool)global::UnityEngine.Physics2D.Linecast(Tr_Front_Start.position, Tr_Front_End_H.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground")))
		{
			Player.transform.position = new global::UnityEngine.Vector3(Player.transform.position.x - 2.5f * (float)facingRight, Player.transform.position.y, 0f);
		}
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(H_Single[0], Player.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
		gameObject.transform.parent = base.transform.parent;
		if (facingRight > 0)
		{
			gameObject.SendMessage("Flip");
		}
		gameObject.GetComponent<H_Ani>().Mon_Object = base.gameObject;
		animator.speed = 0f;
		GetComponent<Mon_Index>().OnOff_Object(false);
		GetComponent<global::UnityEngine.Rigidbody2D>().Sleep();
		base.transform.position = new global::UnityEngine.Vector3(Player.transform.position.x + 0.2f * (float)(-facingRight), Player.transform.position.y + 4.02f, 0f);
		GetComponent<Monster>().isInvincible = true;
		global::UnityEngine.GameObject.Find("Menu").GetComponent<Menu_Control>().H_Object = gameObject;
		global::UnityEngine.GameObject.Find("Main Camera").SendMessage("Hscene_Zoom");
		if (!GM.GameOver)
		{
			Player.SendMessage("H_Down");
		}
		global::UnityEngine.GameObject.Find("Ani").SendMessage("Start_H_Scene");
		animator.Play("HideHands", 0, 0f);
		H_Timer = 4f + global::UnityEngine.Random.Range(0f, 2f);
		H_Walk_Timer = 1f + global::UnityEngine.Random.Range(0f, 0.2f);
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
		GetComponent<Monster>().isInvincible = false;
		GM.Down_H_After();
		global::UnityEngine.GameObject.Find("Ani").SendMessage("End_H_Scene");
		if (on_Crouch)
		{
			animator.Play("Sit", 0, 0f);
		}
		else
		{
			animator.Play("Idle", 0, 0f);
		}
	}

	private void Raycasting()
	{
		on_Tail_Range = global::UnityEngine.Physics2D.Linecast(Tr_1_Start.position, Tr_1_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player_Dmg"));
		on_Tongue_Range = global::UnityEngine.Physics2D.Linecast(Tr_2_Start.position, Tr_2_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player_Dmg"));
		on_Tail_Down_Range = global::UnityEngine.Physics2D.Linecast(Tr_3_Start.position, Tr_3_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player_Dmg"));
		on_Tongue_Down_Range = global::UnityEngine.Physics2D.Linecast(Tr_4_Start.position, Tr_4_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player_Dmg"));
		if (Move_Delay > 0f && Move_Delay < 0.3f)
		{
			on_Tail_Range = (on_Tongue_Range = (on_Tail_Down_Range = (on_Tongue_Down_Range = false)));
		}
		isStuck_Front = global::UnityEngine.Physics2D.Linecast(Tr_Front_Start.position, Tr_Front_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
		isStuck_Back = global::UnityEngine.Physics2D.Linecast(Tr_Back_Start.position, Tr_Back_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
		isStuck_BackLow = global::UnityEngine.Physics2D.Linecast(Tr_Back_Start.position, Tr_BackLow_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
		onGround = global::UnityEngine.Physics2D.Linecast(Tr_Ground_R.position, Tr_Ground_L.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
		if (onGround)
		{
			Ground_Timer += global::UnityEngine.Time.deltaTime;
		}
		else
		{
			Ground_Timer = 0f;
		}
		distance = global::UnityEngine.Vector3.Distance(new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f, Player.transform.position.z), base.transform.position);
		dist_X = global::UnityEngine.Mathf.Abs(base.transform.position.x - Player.transform.position.x);
		dist_Y = global::UnityEngine.Mathf.Abs(Player.transform.position.y - (base.transform.position.y - 4f));
		if (on_Chase)
		{
			if (dist_X > 60f || dist_Y > 30f)
			{
				on_Chase = false;
				EnemyState = 0;
			}
			else if (PC.Jump_Num == 0 && Player.transform.position.y < base.transform.position.y + 16f && Player.transform.position.y > base.transform.position.y + 4f)
			{
				EnemyState = 3;
			}
			else
			{
				EnemyState = 2;
			}
		}
		else if (dist_X < 40f && Player.transform.position.y < base.transform.position.y + 9f && Player.transform.position.y > base.transform.position.y - 14f)
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
			if (on_Crouch)
			{
				if (dist_X < 10f + rnd_X)
				{
					on_Attack_Range = true;
				}
				else
				{
					on_Attack_Range = false;
				}
				if (dist_X < 7.8f + rnd_X)
				{
					on_BackWalk = true;
				}
				else
				{
					on_BackWalk = false;
				}
			}
			else
			{
				if (dist_X < 8f + rnd_X)
				{
					on_Attack_Range = true;
				}
				else
				{
					on_Attack_Range = false;
				}
				if (dist_X < 7.5f + rnd_X && PC.State == Player_Control.AniState.Sit)
				{
					on_BackWalk = true;
				}
				else if (dist_X < 5.7f + rnd_X)
				{
					on_BackWalk = true;
				}
				else
				{
					on_BackWalk = false;
				}
			}
			if (dist_X < 2f && dist_Y < 1f)
			{
				on_Hscene_Range = true;
			}
			else
			{
				on_Hscene_Range = false;
			}
		}
		else
		{
			on_Hscene_Range = false;
		}
		if (EnemyState == 3)
		{
			ChaseJump_Timer += global::UnityEngine.Time.deltaTime;
		}
		else if (ChaseJump_Timer > 0f)
		{
			ChaseJump_Timer = 0f;
		}
	}
}
