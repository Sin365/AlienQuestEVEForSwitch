public class AI_Mon_Xeno : global::UnityEngine.MonoBehaviour
{
	public enum Event_Type
	{
		None = 0,
		Top = 1,
		Bottom = 2
	}

	public AI_Mon_Xeno.Event_Type event_Type;

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

	private bool on_Arm_Range;

	private bool on_Tongue_Range;

	private bool on_Tail_Up_Range;

	private bool on_Tail_Down_Range;

	private bool on_Tongue_Down_Range;

	private bool on_Jump_Range;

	private bool on_Fire_Range;

	private float Hit_Delay;

	private float Flip_Delay;

	private float Attack_Delay;

	private float JumpDash_Delay;

	private float BackDash_Delay;

	private float Move_Delay;

	private float Move_Push;

	private float Move_Lock_Timer;

	private float LockHit_Delay;

	private int Fire_Num;

	private float Fire_Timer;

	private float JumpDash_Timer;

	private float Crouch_Timer;

	private float rnd_X;

	private bool isStuck_Front;

	private bool isStuck_Back;

	private bool isStuck_BackLow;

	private bool on_Hscene;

	private bool on_Hscene_Range;

	private bool DualPlayable;

	private float Dual_Delay;

	private float H_Timer;

	private float H_Walk_Timer;

	private float H_Pursue_Timer;

	private float PC_Atk_Timer;

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
		ChaseJump_Timer = rnd_X * -3f;
		Move_Speed = 20f + rnd_X;
		Walk_Speed = 16f + rnd_X;
		Patrol_Range = 14f + global::UnityEngine.Random.Range(0f, 4f);
		if (global::UnityEngine.PlayerPrefs.GetInt("UncensoredPatch") != 1)
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
		if (event_Type != AI_Mon_Xeno.Event_Type.None)
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
		DualPlayable = true;
	}

	private void Event_Start()
	{
		Mon.onEvent = true;
		Mon.isLockHit = true;
		Flip_Delay = 0f;
		if (event_Type == AI_Mon_Xeno.Event_Type.Top)
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
			animator.Play("JumpUp", 0, 0f);
			Set_Jump(10 * facingRight, 65f);
		}
	}

	private void Event_End()
	{
		if (event_Type != AI_Mon_Xeno.Event_Type.None)
		{
			event_Type = AI_Mon_Xeno.Event_Type.None;
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
		if (global::UnityEngine.PlayerPrefs.GetInt("Censorship") == 1)
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
			Move_Push += global::UnityEngine.Time.deltaTime;
			if (Move_Push > 1f)
			{
				Move_Delay = 0.5f;
			}
		}
		else if (Move_Delay > 0f)
		{
			Move_Push = 0f;
			Move_Delay -= global::UnityEngine.Time.deltaTime;
		}
		if (Mon.Mon_Num == 34)
		{
			Fire_Timer += global::UnityEngine.Time.deltaTime;
			JumpDash_Timer += global::UnityEngine.Time.deltaTime * 2f;
		}
		else
		{
			Fire_Timer += global::UnityEngine.Time.deltaTime;
			JumpDash_Timer += global::UnityEngine.Time.deltaTime;
		}
		if (Crouch_Timer > 0f)
		{
			Crouch_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (LockHit_Delay > 0f)
		{
			LockHit_Delay -= global::UnityEngine.Time.deltaTime;
		}
		else if (event_Type == AI_Mon_Xeno.Event_Type.None && Mon.isLockHit)
		{
			Mon.isLockHit = false;
		}
		Raycasting();
		if (Dual_Delay > 0f)
		{
			Dual_Delay -= global::UnityEngine.Time.deltaTime;
		}
		if (PC_Atk_Timer > 0f)
		{
			PC_Atk_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (H_Walk_Timer > 0f && on_Crouch)
		{
			animator.speed = global::UnityEngine.Mathf.Lerp(animator.speed, 0.7f, global::UnityEngine.Time.deltaTime * 5f);
		}
		else if (event_Type == AI_Mon_Xeno.Event_Type.None && animator.speed < 1f)
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
		else if (event_Type == AI_Mon_Xeno.Event_Type.None && !GetComponent<global::UnityEngine.BoxCollider2D>().enabled)
		{
			GetComponent<global::UnityEngine.BoxCollider2D>().enabled = true;
		}
		if (event_Type != AI_Mon_Xeno.Event_Type.None)
		{
			Mon.isLockHit = true;
			if (event_Type == AI_Mon_Xeno.Event_Type.Top)
			{
				if (event_Timer > 1f)
				{
					animator.speed = 1f;
				}
				else if (Player.transform.position.y < base.transform.position.y && dist_Y < 15.5f && dist_X < 12f)
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
		else if (!onGround && base.rigidbody2D.velocity.y < -1f && (animator.GetBool("onMove") || animator.GetBool("onWalk")))
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
			if (DualPlayable && Dual_Delay <= 0f && (GM.Hscene_Num == 30 || GM.Hscene_Num == 31))
			{
				if (GM.GetComponent<H_Control>().Mon_1.GetComponent<Monster>().Mon_Num == Mon.Mon_Num)
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
					if (dist_Y < 1f && global::UnityEngine.Mathf.Abs(base.transform.position.x - global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x) < ((!on_Crouch) ? 3.2f : 4f) && (GM.Hscene_Num == 30 || GM.Hscene_Num == 31))
					{
						GM.Hscene_Num = 32;
						Start_H_Dual();
					}
				}
				else
				{
					Dual_Delay = 12f;
				}
			}
			else if (GM.Hscene_Num == 0 && GM.Hscene_Timer <= 0f)
			{
				if (facingRight > 0 && base.transform.position.x > global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x)
				{
					Flip();
				}
				else if (facingRight < 0 && base.transform.position.x < global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x)
				{
					Flip();
				}
				else if (dist_Y < 1f && (global::UnityEngine.Mathf.Abs(base.transform.position.x - global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x) < ((!on_Crouch) ? 3.2f : 4f) || (isStuck_Front && global::UnityEngine.Mathf.Abs(base.transform.position.x - global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x) < 5f)))
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
					Start_H_Single();
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
					if (Patrol_Move_Timer > 1f && (isStuck_Front || global::UnityEngine.Mathf.Abs(global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x - base.transform.position.x) > Patrol_Range))
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
					if (Patrol_Idle_Timer > 2.5f)
					{
						Patrol_Idle_Timer = 0f;
						Set_Move();
						Patrol_State = 1;
						Patrol_Move_Timer = 0f;
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
				H_Pursue_Timer = 0f;
			}
		}
		else if (EnemyState < 2)
		{
			Check_Idle();
		}
		else
		{
			Action_Mon_32();
		}
	}

	private void Action_Mon_32()
	{
		if (animator.GetBool("onHit"))
		{
			Hit_Delay = 0.2f;
			Move_Lock_Timer = 0.2f;
			Attack_Delay = 0f;
			PC_Atk_Timer = 3f;
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
					if (!on_Crouch && PC.State == Player_Control.AniState.Spin)
					{
						Set_BackCrouch();
					}
					else if (Fire_Num < 1 && Mon.HP_Ratio <= 0.3f)
					{
						Set_Attack_Fire();
					}
				}
				else if (PC.State != Player_Control.AniState.Spin)
				{
					Set_BackDash();
				}
				else
				{
					Set_BackCrouch();
				}
				Crouch_Timer = 8f + global::UnityEngine.Random.Range(0f, 4f);
			}
			else if (Fire_Timer > 8f)
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
		if (JumpDash_Delay > 0f)
		{
			JumpDash_Delay -= global::UnityEngine.Time.deltaTime;
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
		if (PC_Atk_Timer <= 0f && !GM.onCloth && GM.Option_Int[3] == 1 && PC.grounded_Now)
		{
			Check_Flip();
			if (dist_Y < 1f && dist_X < ((!on_Crouch) ? 3.2f : 4.5f))
			{
				if (!GM.onHscene && GM.Hscene_Timer <= 0f && (PC.State == Player_Control.AniState.Idle || PC.State == Player_Control.AniState.Run || PC.State == Player_Control.AniState.Sit || PC.State == Player_Control.AniState.Down))
				{
					Start_H_Single();
				}
				Check_Idle();
			}
			else if (!isStuck_Front)
			{
				if (!animator.GetBool("onMove"))
				{
					Set_Move();
				}
				else if (on_Crouch)
				{
					base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Walk_Speed * facingRight * Mon.Move_Speed);
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
		else if (on_Crouch)
		{
			if (Fire_Num < 1 && Mon.HP_Ratio < 0.3f)
			{
				Set_Attack_Fire();
			}
			else if (JumpDash_Timer > 3f + rnd_X && on_Jump_Range)
			{
				Set_JumpDash();
			}
			else if (on_Tongue_Down_Range)
			{
				Set_Attack_Tongue();
			}
			else if (on_Tail_Down_Range)
			{
				Set_Attack();
			}
			else if (on_Tail_Up_Range)
			{
				Set_Attack_Up();
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
		else if (Fire_Num < 1 && Mon.HP_Ratio < 0.3f)
		{
			Set_Attack_Fire();
		}
		else if (on_Tongue_Range)
		{
			Set_Attack_Tongue();
		}
		else if (on_Arm_Range)
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

	public void Flip()
	{
		facingRight = -facingRight;
		Mon.Flip();
		Flip_Delay = 0.5f + (float)global::UnityEngine.Random.Range(0, 80) * 0.01f;
		Tr_Pos.transform.localScale = new global::UnityEngine.Vector3(-facingRight, 1f, 1f);
		CensoredText.transform.localScale = new global::UnityEngine.Vector3(1.3f * (float)(-facingRight), 1.5f, 1f);
		GetComponent<global::UnityEngine.BoxCollider2D>().center = new global::UnityEngine.Vector2(0.4f * (float)(-facingRight), -3.5f);
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
		base.rigidbody2D.AddForce(global::UnityEngine.Vector3.right * _X, global::UnityEngine.ForceMode2D.Impulse);
		base.rigidbody2D.AddForce(global::UnityEngine.Vector3.up * _Y, global::UnityEngine.ForceMode2D.Impulse);
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
		Attack_Delay = 0.7f;
		animator.SetBool("onAttack", true);
		animator.SetBool("onAttackTongue", false);
		animator.SetBool("onAttackFire", false);
		animator.SetBool("onAttackUp", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onWalk", false);
		animator.SetBool("onHit", false);
		animator.SetBool("onJump", false);
	}

	private void Set_Attack_Up()
	{
		Attack_Delay = 0.7f;
		animator.SetBool("onAttack", false);
		animator.SetBool("onAttackTongue", false);
		animator.SetBool("onAttackFire", false);
		animator.SetBool("onAttackUp", true);
		animator.SetBool("onMove", false);
		animator.SetBool("onWalk", false);
		animator.SetBool("onHit", false);
		animator.SetBool("onJump", false);
	}

	private void Set_Attack_Tongue()
	{
		Attack_Delay = 0.7f;
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
		Attack_Delay = 1f;
		Flip_Delay = 0.5f;
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
		Crouch_Timer = 20f + global::UnityEngine.Random.Range(0f, 2f);
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
		Crouch_Timer = 20f + global::UnityEngine.Random.Range(0f, 3f);
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
		animator.SetTrigger("On_BackDash");
		animator.SetBool("onCrouch", false);
		on_Crouch = false;
		Mon.onCrouch = false;
		base.rigidbody2D.velocity = new global::UnityEngine.Vector2(0f, base.rigidbody2D.velocity.y);
		base.rigidbody2D.AddForce(global::UnityEngine.Vector3.right * 25f * -facingRight, global::UnityEngine.ForceMode2D.Impulse);
		if (isStuck_BackLow)
		{
			base.rigidbody2D.AddForce(global::UnityEngine.Vector3.up * 20f, global::UnityEngine.ForceMode2D.Impulse);
		}
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Atk_2(base.transform.position);
		Fire_Timer = 10f;
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
		animator.SetTrigger("On_BackSit");
		animator.SetBool("onCrouch", true);
		on_Crouch = true;
		Mon.onCrouch = true;
		base.rigidbody2D.velocity = new global::UnityEngine.Vector2(0f, base.rigidbody2D.velocity.y);
		base.rigidbody2D.AddForce(global::UnityEngine.Vector3.right * 23f * -facingRight, global::UnityEngine.ForceMode2D.Impulse);
		if (isStuck_BackLow)
		{
			base.rigidbody2D.AddForce(global::UnityEngine.Vector3.up * 20f, global::UnityEngine.ForceMode2D.Impulse);
		}
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Atk_2(base.transform.position);
		Fire_Timer = 10f;
	}

	private void Set_JumpDash()
	{
		animator.SetBool("onAttack", false);
		animator.SetBool("onAttackTongue", false);
		animator.SetBool("onAttackFire", false);
		animator.SetBool("onAttackUp", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onWalk", false);
		animator.SetBool("onHit", false);
		JumpDash_Timer = 0f;
		JumpDash_Delay = 1f;
		animator.SetTrigger("On_Jump");
		animator.SetBool("onCrouch", true);
		on_Crouch = true;
		Mon.onCrouch = true;
	}

	private void Jump_Up()
	{
		if (Move_Delay > 0f)
		{
			base.rigidbody2D.AddForce(global::UnityEngine.Vector3.right * 40f * facingRight, global::UnityEngine.ForceMode2D.Impulse);
		}
		else
		{
			base.rigidbody2D.AddForce(global::UnityEngine.Vector3.right * 30f * facingRight, global::UnityEngine.ForceMode2D.Impulse);
		}
		base.rigidbody2D.AddForce(global::UnityEngine.Vector3.up * 30f, global::UnityEngine.ForceMode2D.Impulse);
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_9_Dash(base.transform.position);
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Atk_2(base.transform.position);
	}

	private void Set_Fire()
	{
		float num = 0f;
		Fire_Num++;
		global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f);
		global::UnityEngine.Vector3 position = pos_Fire.transform.position;
		vector.x -= position.x;
		vector.y -= position.y;
		num = global::UnityEngine.Mathf.Atan2(vector.y, vector.x) * 57.29578f + 180f;
		if (Mon.Mon_Num == 34)
		{
			num = Check_Fire_Angle_34(num);
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, num)) as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, num + 20f)) as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, num - 20f)) as global::UnityEngine.GameObject;
		}
		else
		{
			num = Check_Fire_Angle(num);
			global::UnityEngine.GameObject gameObject4 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, num)) as global::UnityEngine.GameObject;
		}
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
		if (angle > 40f && angle < 180f)
		{
			return 40f;
		}
		return angle;
	}

	private float Check_Fire_Angle_34(float angle)
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

	private void Set_Dash()
	{
		if (BackDash_Delay <= 0f)
		{
			if (Move_Delay > 0f)
			{
				base.rigidbody2D.AddForce(global::UnityEngine.Vector3.right * 25f * facingRight, global::UnityEngine.ForceMode2D.Impulse);
			}
			else
			{
				base.rigidbody2D.AddForce(global::UnityEngine.Vector3.right * 15f * facingRight, global::UnityEngine.ForceMode2D.Impulse);
			}
		}
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

	private void Raycasting()
	{
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
		dist_Y = global::UnityEngine.Mathf.Abs(Player.transform.position.y - (base.transform.position.y - 4.53f));
		on_Jump_Range = false;
		if (Player.transform.position.y < base.transform.position.y + 3.5f && Player.transform.position.y > base.transform.position.y - 8f)
		{
			if (Move_Delay > 0f && dist_X < 10f)
			{
				on_Arm_Range = true;
			}
			else if (dist_X < 7.5f)
			{
				on_Arm_Range = true;
			}
			else
			{
				on_Arm_Range = false;
			}
			if (dist_X < 15f)
			{
				on_Jump_Range = true;
			}
		}
		else
		{
			on_Arm_Range = false;
		}
		on_Tongue_Range = global::UnityEngine.Physics2D.Linecast(Tr_1_Start.position, Tr_1_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player_Dmg"));
		on_Tail_Down_Range = global::UnityEngine.Physics2D.Linecast(Tr_2_Start.position, Tr_2_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player_Dmg"));
		on_Tail_Up_Range = global::UnityEngine.Physics2D.Linecast(Tr_3_Start.position, Tr_3_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player_Dmg"));
		on_Tongue_Down_Range = global::UnityEngine.Physics2D.Linecast(Tr_4_Start.position, Tr_4_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player_Dmg"));
		if (on_Chase)
		{
			if (dist_X > 60f || dist_Y > 30f)
			{
				on_Chase = false;
				EnemyState = 0;
			}
			else if (PC.Jump_Num == 0 && Player.transform.position.y < base.transform.position.y + 15.5f && Player.transform.position.y > base.transform.position.y + 4.5f)
			{
				EnemyState = 3;
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
			if (on_Crouch)
			{
				if (dist_X < 14f + rnd_X)
				{
					on_Attack_Range = true;
				}
				else
				{
					on_Attack_Range = false;
				}
				if (dist_X < 10f + rnd_X)
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
				if (dist_X < 7f + rnd_X)
				{
					on_Attack_Range = true;
				}
				else
				{
					on_Attack_Range = false;
				}
				if (dist_X < 5.5f + rnd_X)
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

	private void Start_H_Single()
	{
		on_Hscene = true;
		GM.onEvent = true;
		GM.onHscene = true;
		GM.Hscene_Timer = 1f;
		GM.Hscene_Num = ((global::UnityEngine.Random.Range(0, 10) <= 5) ? 30 : 31);
		if ((bool)global::UnityEngine.Physics2D.Linecast(Tr_Front_Start.position, Tr_Front_End_H.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground")))
		{
			Player.transform.position = new global::UnityEngine.Vector3(Player.transform.position.x - 1.5f * (float)facingRight, Player.transform.position.y, 0f);
		}
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(H_Single[-30 + GM.Hscene_Num], Player.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
		base.transform.position = new global::UnityEngine.Vector3(Player.transform.position.x - 1.5f * (float)facingRight, Player.transform.position.y + 4.53f, 0f);
		if (facingRight > 0)
		{
			gameObject.SendMessage("Flip");
		}
		gameObject.GetComponent<H_Ani>().Mon_Object = base.gameObject;
		global::UnityEngine.GameObject.Find("Menu").GetComponent<Menu_Control>().H_Object = gameObject;
		Start_Hscene();
		GM.GetComponent<H_Control>().facingRight = facingRight;
		GM.GetComponent<H_Control>().H_Object = gameObject;
		GM.GetComponent<H_Control>().Mon_1 = base.gameObject;
		if (PC.facingRight > 0 && base.transform.position.x < Player.transform.position.x)
		{
			Player.SendMessage("Flip");
		}
		else if (PC.facingRight < 0 && base.transform.position.x > Player.transform.position.x)
		{
			Player.SendMessage("Flip");
		}
	}

	private void Start_H_Dual()
	{
		on_Hscene = true;
		GM.onEvent = true;
		GM.onHscene = true;
		GM.Hscene_Num = 32;
		if (GM.GetComponent<H_Control>().H_Object != null)
		{
			GM.GetComponent<H_Control>().H_Object.SendMessage("Delete_ToDual");
		}
		if ((bool)global::UnityEngine.Physics2D.Linecast(Tr_Front_Start.position, Tr_Front_End_H.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground")))
		{
			Player.transform.position = new global::UnityEngine.Vector3(Player.transform.position.x - 3.5f * (float)facingRight, Player.transform.position.y, 0f);
			GM.GetComponent<H_Control>().Mon_1.transform.position = new global::UnityEngine.Vector3(GM.GetComponent<H_Control>().Mon_1.transform.position.x - 3.5f * (float)facingRight, GM.GetComponent<H_Control>().Mon_1.transform.position.y, 0f);
		}
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(H_Single[2], Player.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
		if (GM.GetComponent<H_Control>().facingRight > 0)
		{
			gameObject.SendMessage("Flip");
		}
		gameObject.GetComponent<H_Ani>().Mon_Object = GM.GetComponent<H_Control>().Mon_1;
		gameObject.GetComponent<H_Ani>().Mon_Object_2 = base.gameObject;
		global::UnityEngine.GameObject.Find("Menu").GetComponent<Menu_Control>().H_Object = gameObject;
		Start_Hscene();
		GM.GetComponent<H_Control>().H_Object = gameObject;
		GM.GetComponent<H_Control>().Mon_2 = base.gameObject;
		if ((GM.GetComponent<H_Control>().facingRight < 0 && facingRight < 0) || (GM.GetComponent<H_Control>().facingRight > 0 && facingRight > 0))
		{
			Flip();
		}
		Flip_Delay = 0f;
		base.transform.position = new global::UnityEngine.Vector3(Player.transform.position.x - 3.2f * (float)facingRight, Player.transform.position.y + 4.53f, 0f);
		H_Timer = 7f + global::UnityEngine.Random.Range(0f, 3f);
		H_Walk_Timer = 1.2f + global::UnityEngine.Random.Range(0f, 0.2f);
		Dual_Delay = 12f;
	}

	private void Start_Hscene()
	{
		H_Timer = 4f + global::UnityEngine.Random.Range(0f, 2f);
		H_Walk_Timer = 1f + global::UnityEngine.Random.Range(0f, 0.2f);
		GetComponent<Mon_Index>().OnOff_Object(false);
		animator.speed = 0f;
		GetComponent<global::UnityEngine.Rigidbody2D>().Sleep();
		Mon.isInvincible = true;
		if (GM.Hscene_Num != 32)
		{
			global::UnityEngine.GameObject.Find("Main Camera").SendMessage("Hscene_Zoom");
		}
		if (!GM.GameOver)
		{
			Player.SendMessage("H_Down");
		}
		global::UnityEngine.GameObject.Find("Ani").SendMessage("Start_H_Scene");
		animator.Play("HideHands", 0, 0f);
	}

	private void End_Hscene()
	{
		on_Hscene = false;
		GM.onEvent = false;
		GM.onHscene = false;
		GM.Hscene_Num = 0;
		GM.GetComponent<H_Control>().Reset();
		GetComponent<Mon_Index>().OnOff_Object(true);
		animator.speed = 1f;
		GetComponent<global::UnityEngine.Rigidbody2D>().WakeUp();
		Mon.isInvincible = false;
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
		if (global::UnityEngine.PlayerPrefs.GetInt("UncensoredPatch") != 1)
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
}
