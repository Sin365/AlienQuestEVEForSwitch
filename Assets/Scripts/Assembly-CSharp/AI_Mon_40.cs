public class AI_Mon_40 : global::UnityEngine.MonoBehaviour
{
	public enum Event_Type
	{
		None = 0,
		Top = 1,
		Bottom = 2
	}

	public AI_Mon_40.Event_Type event_Type;

	private float event_Timer;

	private bool on_Event_Start;

	private bool onEvent_Sound;

	private float Life_Timer;

	private float onCam_Timer;

	private int facingRight = -1;

	private float distance;

	private float dist_X;

	private float dist_Y;

	private float Move_Speed = 5f;

	private float Speed_Orig = 1f;

	private int State;

	private float State_Timer;

	private int Pos_State;

	private int Fire_State;

	private bool onAttack_Range;

	private bool onAttack;

	private bool onDash;

	private float Hit_Delay;

	private float Flip_Delay;

	private float Attack_Delay;

	private float Dash_Delay;

	private float Fire_Timer;

	private float Dash_Timer;

	private float Dash_Target_Timer;

	private float Position_Timer;

	private float LockHit_Delay;

	private float Invincible_Delay;

	private bool on_Hscene;

	private bool on_Hscene_Range;

	private float H_Timer;

	private float H_Pursue_Timer;

	private float PC_Atk_Timer;

	private int Patrol_State;

	private float Patrol_Idle_Timer;

	private float Patrol_Move_Timer;

	private float Patrol_Range;

	private global::UnityEngine.Vector3 pos_Orig;

	private global::UnityEngine.Vector3 pos_Prev;

	private global::UnityEngine.Vector3 Pos_Target;

	private global::UnityEngine.Vector3 Pos_Attack;

	private global::UnityEngine.Vector2 Rnd_XY;

	private global::UnityEngine.Vector2 GameOver_XY;

	private bool isStuck_Front;

	private bool isStuck_Back;

	public global::UnityEngine.Transform pos_Fire;

	public global::UnityEngine.GameObject _Fire;

	public global::UnityEngine.Transform Mon_Ctrl;

	public global::UnityEngine.Transform pos_Dash;

	public global::UnityEngine.GameObject H_Single;

	public global::UnityEngine.Transform Tr_Front_Start;

	public global::UnityEngine.Transform Tr_Front_End;

	public global::UnityEngine.Transform Tr_Back_Start;

	public global::UnityEngine.Transform Tr_Back_End;

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
		pos_Orig = base.transform.position;
		Pos_Target = base.transform.position;
		Rnd_XY = new global::UnityEngine.Vector2((float)global::UnityEngine.Random.Range(-15, 15) * 0.01f, (float)global::UnityEngine.Random.Range(-15, 15) * 0.01f);
		GameOver_XY = new global::UnityEngine.Vector2((float)global::UnityEngine.Random.Range(50, 80) * 0.1f, global::UnityEngine.Random.Range(1f, 2.5f));
		Pos_Attack = Player.transform.position;
		Speed_Orig = 5f + global::UnityEngine.Random.Range(0f, 1f);
		Patrol_Range = 25f + global::UnityEngine.Random.Range(0f, 2f);
		if (Player.transform.position.x > base.transform.position.x)
		{
			Flip();
		}
		if (event_Type != AI_Mon_40.Event_Type.None)
		{
			Set_Event_Position();
		}
	}

	private void Update()
	{
		if (GM.Paused || on_Hscene)
		{
			return;
		}
		if (event_Type != AI_Mon_40.Event_Type.None)
		{
			distance = global::UnityEngine.Vector3.Distance(new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.7f, 0f), pos_Orig);
			if (!(distance < 20f) && !(event_Timer > 0f))
			{
				return;
			}
			event_Timer += global::UnityEngine.Time.deltaTime;
			if (!on_Event_Start)
			{
				on_Event_Start = true;
				Check_Flip();
				Set_Event_Position();
				Set_Move();
				return;
			}
			if (event_Timer > 0.6f)
			{
				Check_Idle();
			}
			else if (event_Timer > 0.2f && !onEvent_Sound)
			{
				onEvent_Sound = true;
				global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_40_Dash(pos_Orig);
			}
			if (event_Type == AI_Mon_40.Event_Type.Top)
			{
				if (global::UnityEngine.Vector3.Distance(base.transform.position, pos_Orig) < 15f)
				{
					if (facingRight > 0)
					{
						Mon_Ctrl.rotation = global::UnityEngine.Quaternion.Euler(0f, 180f, global::UnityEngine.Mathf.Lerp(Mon_Ctrl.rotation.eulerAngles.z, 0f, global::UnityEngine.Time.deltaTime * 2.5f));
					}
					else
					{
						Mon_Ctrl.rotation = global::UnityEngine.Quaternion.Euler(0f, 0f, global::UnityEngine.Mathf.Lerp(Mon_Ctrl.rotation.eulerAngles.z, 0f, global::UnityEngine.Time.deltaTime * 2.5f));
					}
				}
				base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, Pos_Target, global::UnityEngine.Time.deltaTime * 2.5f);
				if (Mon.isInvincible && Mon_Ctrl.rotation.eulerAngles.z < 10f)
				{
					End_Event();
				}
				return;
			}
			if (global::UnityEngine.Vector3.Distance(base.transform.position, pos_Orig) < 15f && Mon_Ctrl.rotation.eulerAngles.z < 360f)
			{
				if (facingRight > 0)
				{
					Mon_Ctrl.rotation = global::UnityEngine.Quaternion.Euler(0f, 180f, global::UnityEngine.Mathf.Lerp(Mon_Ctrl.rotation.eulerAngles.z, 359.99f, global::UnityEngine.Time.deltaTime * 2.5f));
				}
				else
				{
					Mon_Ctrl.rotation = global::UnityEngine.Quaternion.Euler(0f, 0f, global::UnityEngine.Mathf.Lerp(Mon_Ctrl.rotation.eulerAngles.z, 359.99f, global::UnityEngine.Time.deltaTime * 2.5f));
				}
			}
			base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, Pos_Target, global::UnityEngine.Time.deltaTime * 2f);
			if (Mon.isInvincible && Mon_Ctrl.rotation.eulerAngles.z > 350f)
			{
				End_Event();
			}
			return;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (onCam_Timer > 0f)
		{
			onCam_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Fire_Timer > 0f)
		{
			Fire_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Dash_Timer > 0f)
		{
			Dash_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Dash_Delay > 0f)
		{
			Dash_Delay -= global::UnityEngine.Time.deltaTime;
		}
		if (Invincible_Delay > 0f)
		{
			Invincible_Delay -= global::UnityEngine.Time.deltaTime;
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
		distance = global::UnityEngine.Vector3.Distance(new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f, 0f), base.transform.position);
		dist_X = global::UnityEngine.Mathf.Abs(Player.transform.position.x - base.transform.position.x);
		dist_Y = global::UnityEngine.Mathf.Abs(Player.transform.position.y - (base.transform.position.y - 6.4f));
		if (H_Timer > 0f)
		{
			H_Timer -= global::UnityEngine.Time.deltaTime;
			if (animator.GetBool("onHit"))
			{
				H_Timer = 0f;
			}
			if ((facingRight > 0 && base.transform.position.x > global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x) || (facingRight < 0 && base.transform.position.x < global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x))
			{
				Just_Flip();
			}
			base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, Pos_Target, global::UnityEngine.Time.deltaTime * (0.5f + (float)GetComponent<Monster>().Gameover_Num * 0.2f));
			return;
		}
		if (distance < 50f && (GM.GameOver || GM.onHscene))
		{
			if (GM.Hscene_Num == 0 && GM.Hscene_Timer <= 0f)
			{
				if (facingRight > 0 && base.transform.position.x > global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x)
				{
					Just_Flip();
				}
				else if (facingRight < 0 && base.transform.position.x < global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x)
				{
					Just_Flip();
				}
				dist_X = global::UnityEngine.Mathf.Abs(global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x - base.transform.position.x);
				if (dist_Y < 0.5f && dist_X < 4f)
				{
					if ((facingRight > 0 && Player.transform.localScale.x > 0f) || (facingRight < 0 && Player.transform.localScale.x < 0f))
					{
						Player.transform.position = new global::UnityEngine.Vector3(Player.transform.position.x - 2.1f * Player.transform.localScale.x, Player.transform.position.y, 0f);
						Player.SendMessage("Flip");
					}
					Start_Hscene();
				}
				else
				{
					Pos_Target = new global::UnityEngine.Vector3(global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x, Player.transform.position.y + 6.4f, 0f);
					base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, Pos_Target, global::UnityEngine.Time.deltaTime * 3f);
				}
			}
			else if (Patrol_State == 1)
			{
				Patrol_Move_Timer += global::UnityEngine.Time.deltaTime;
				if (Patrol_Move_Timer > 1f && distance > Patrol_Range + Rnd_XY.x)
				{
					Patrol_State = 0;
					Patrol_Idle_Timer = 0f;
					global::UnityEngine.Debug.Log("Set to Idle");
				}
				Pos_Target = new global::UnityEngine.Vector3(global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x + (Patrol_Range + 10f) * (float)facingRight, Player.transform.position.y + 7.5f - Rnd_XY.y * 3f, 0f);
				Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, Speed_Orig, global::UnityEngine.Time.deltaTime * 5f);
				base.transform.position = global::UnityEngine.Vector3.MoveTowards(base.transform.position, Pos_Target, global::UnityEngine.Time.deltaTime * Move_Speed * 0.4f * GetComponent<Monster>().Move_Speed);
			}
			else
			{
				Patrol_Idle_Timer += global::UnityEngine.Time.deltaTime;
				if (Patrol_Idle_Timer > 2.5f)
				{
					Patrol_Idle_Timer = 0f;
					Patrol_State = 1;
					Patrol_Move_Timer = 0f;
				}
				else if (Patrol_Idle_Timer > 1.5f && ((facingRight > 0 && base.transform.position.x > global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x) || (facingRight < 0 && base.transform.position.x < global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x)))
				{
					Just_Flip();
				}
			}
			return;
		}
		if (GetComponent<global::UnityEngine.Animator>().GetBool("onHit"))
		{
			Hit_Delay = 0.2f;
			PC_Atk_Timer = 3f;
			if (Dash_Delay <= 0f && GetComponent<Monster>().Get_HitCombo() > 4)
			{
				GetComponent<Monster>().Reset_HitCombo();
				Set_Dash();
				State = 0;
				State_Timer = 0f;
				Pos_State = 0;
				Fire_State = 0;
			}
			return;
		}
		if (Hit_Delay > 0f)
		{
			Hit_Delay -= global::UnityEngine.Time.deltaTime;
			return;
		}
		if (PC_Atk_Timer <= 0f && GM.User_Input_Timer > 1.5f && !GM.onCloth && GM.Option_Int[3] == 1 && PC.grounded_Now)
		{
			Check_Flip();
			if (dist_Y < 0.5f && dist_X < 3f && !GM.onHscene && GM.Hscene_Timer <= 0f && (PC.State == Player_Control.AniState.Idle || PC.State == Player_Control.AniState.Run || PC.State == Player_Control.AniState.Sit || PC.State == Player_Control.AniState.Down))
			{
				Start_Hscene();
			}
			Pos_Target = new global::UnityEngine.Vector3(Player.transform.position.x + 1.5f * (float)(-facingRight), Player.transform.position.y + 6.4f, 0f);
			base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, Pos_Target, global::UnityEngine.Time.deltaTime * 3f);
			return;
		}
		if (Flip_Delay > 0f)
		{
			Flip_Delay -= global::UnityEngine.Time.deltaTime;
		}
		else
		{
			Check_Flip();
		}
		if (State == 0)
		{
			State_Timer += global::UnityEngine.Time.deltaTime;
			if (State_Timer > 1f && !GM.GameOver)
			{
				State = 1;
				State_Timer = 0f;
				Pos_State = 0;
			}
		}
		else if (State == 1)
		{
			State_Timer += global::UnityEngine.Time.deltaTime;
			if (Pos_State < 1)
			{
				Pos_State = 1;
				Random_Position();
			}
			else if (State_Timer > 1f && Fire_State < 1)
			{
				Fire_State = 1;
				Set_Attack();
			}
			else if (State_Timer > 2.5f)
			{
				State = 2;
				State_Timer = 0f;
			}
		}
		else if (State == 2)
		{
			State_Timer += global::UnityEngine.Time.deltaTime;
			if (Pos_State < 2)
			{
				Pos_State = 2;
				Random_Position();
			}
			else if (State_Timer > 1f && Fire_State < 2)
			{
				Fire_State = 2;
				Set_Attack();
			}
			else if (State_Timer > 2.5f)
			{
				State = 3;
				State_Timer = 0f;
			}
		}
		else if (State == 3)
		{
			State_Timer += global::UnityEngine.Time.deltaTime;
			if (Pos_State < 3)
			{
				Pos_State = 3;
				Random_Position();
			}
			else if (State_Timer > 1f && Fire_State < 3)
			{
				Fire_State = 3;
				Set_Attack();
			}
			else if (State_Timer > 2.5f)
			{
				State = 4;
				State_Timer = 0f;
			}
		}
		else if (State == 4)
		{
			State_Timer += global::UnityEngine.Time.deltaTime;
			if (State_Timer > 1f && Fire_State < 4)
			{
				Fire_State = 4;
				Set_Dash();
			}
			else if (State_Timer > 3f)
			{
				State = 0;
				State_Timer = 0f;
				Pos_State = 0;
				Fire_State = 0;
			}
		}
		if (onDash)
		{
			if (Dash_Target_Timer > 0f)
			{
				Dash_Target_Timer -= global::UnityEngine.Time.deltaTime;
				if (facingRight > 0)
				{
					Mon_Ctrl.rotation = global::UnityEngine.Quaternion.Lerp(Mon_Ctrl.rotation, global::UnityEngine.Quaternion.Euler(0f, 180f, 180f - Get_DashAngle()), global::UnityEngine.Time.deltaTime * 5f);
				}
				else
				{
					Mon_Ctrl.rotation = global::UnityEngine.Quaternion.Lerp(Mon_Ctrl.rotation, global::UnityEngine.Quaternion.Euler(0f, 0f, Get_DashAngle()), global::UnityEngine.Time.deltaTime * 5f);
				}
			}
			if (Dash_Timer <= 0f)
			{
				onDash = false;
			}
			base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, Pos_Target, global::UnityEngine.Time.deltaTime * 3f);
		}
		else
		{
			if (facingRight > 0)
			{
				Mon_Ctrl.rotation = global::UnityEngine.Quaternion.Lerp(Mon_Ctrl.rotation, global::UnityEngine.Quaternion.Euler(0f, 180f, 0f), global::UnityEngine.Time.deltaTime * 2f);
			}
			else
			{
				Mon_Ctrl.rotation = global::UnityEngine.Quaternion.Lerp(Mon_Ctrl.rotation, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f), global::UnityEngine.Time.deltaTime * 2f);
			}
			base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, Pos_Target, global::UnityEngine.Time.deltaTime * 3f);
		}
	}

	private void Random_Position()
	{
		Check_Flip();
		Pos_Target = global::UnityEngine.GameObject.Find("Main Camera").transform.position;
		Pos_Target = new global::UnityEngine.Vector3(Pos_Target.x + (float)(14 * -facingRight) + Rnd_XY.x * 2f, Pos_Target.y + (float)global::UnityEngine.Random.Range(-3, 3) + Rnd_XY.y * 2f, 0f);
	}

	private void Reset_Event()
	{
		event_Type = ((global::UnityEngine.Random.Range(0, 10) > 4) ? AI_Mon_40.Event_Type.Top : AI_Mon_40.Event_Type.Bottom);
		event_Timer = 0f;
		on_Event_Start = false;
		onEvent_Sound = false;
		Pos_Target = pos_Orig;
		Set_Event_Position();
	}

	private void End_Event()
	{
		event_Type = AI_Mon_40.Event_Type.None;
		Mon.isInvincible = false;
		Flip_Delay = global::UnityEngine.Random.Range(0.1f, 0.2f);
	}

	private void Just_Flip()
	{
		facingRight = -facingRight;
		Mon.Flip();
		if (facingRight > 0)
		{
			Mon_Ctrl.rotation = global::UnityEngine.Quaternion.Euler(0f, 180f, 0f);
		}
		else
		{
			Mon_Ctrl.rotation = global::UnityEngine.Quaternion.Euler(0f, 0f, 0f);
		}
	}

	private void Flip()
	{
		facingRight = -facingRight;
		Mon.Flip();
		Flip_Delay = global::UnityEngine.Random.Range(0.1f, 0.3f);
		if (facingRight > 0)
		{
			Mon_Ctrl.rotation = global::UnityEngine.Quaternion.Euler(0f, 180f, 0f);
		}
		else
		{
			Mon_Ctrl.rotation = global::UnityEngine.Quaternion.Euler(0f, 0f, 0f);
		}
		Pos_Target = new global::UnityEngine.Vector3(base.transform.position.x - 1f * (float)facingRight, base.transform.position.y, 0f);
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

	private void Set_Event_Position()
	{
		Mon.isInvincible = true;
		Invincible_Delay = 60f;
		State = 1;
		State_Timer = 0f;
		Pos_State = 1;
		Fire_State = 0;
		if (event_Type == AI_Mon_40.Event_Type.Top)
		{
			base.transform.position = new global::UnityEngine.Vector3(pos_Orig.x + (float)(8 * -facingRight), pos_Orig.y + 40f, 0f);
			if (facingRight > 0)
			{
				Mon_Ctrl.rotation = global::UnityEngine.Quaternion.Euler(0f, 180f, 90f);
			}
			else
			{
				Mon_Ctrl.rotation = global::UnityEngine.Quaternion.Euler(0f, 0f, 90f);
			}
		}
		else
		{
			base.transform.position = new global::UnityEngine.Vector3(pos_Orig.x + (float)(8 * -facingRight), pos_Orig.y - 40f, 0f);
			if (facingRight > 0)
			{
				Mon_Ctrl.rotation = global::UnityEngine.Quaternion.Euler(0f, 180f, 270f);
			}
			else
			{
				Mon_Ctrl.rotation = global::UnityEngine.Quaternion.Euler(0f, 0f, 270f);
			}
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
		onAttack = false;
		onDash = false;
		animator.SetBool("onAttack", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onHit", false);
	}

	private void Set_Move()
	{
		animator.SetBool("onAttack", false);
		animator.SetBool("onMove", true);
		animator.SetBool("onHit", false);
	}

	private void Set_Dash()
	{
		State = 4;
		Check_Flip();
		onDash = true;
		Dash_Timer = 1f;
		Dash_Target_Timer = 0.3f;
		Flip_Delay = 2f;
		Mon.isInvincible = true;
		Invincible_Delay = 0.7f;
		animator.SetTrigger("Dash");
		animator.SetBool("onAttack", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onHit", false);
		Pos_Target = new global::UnityEngine.Vector3(base.transform.position.x - 2.5f * (float)facingRight, base.transform.position.y, 0f);
	}

	private void Set_Attack()
	{
		onAttack = true;
		Attack_Delay = 1.5f;
		Mon.isLockHit = true;
		LockHit_Delay = 0.8f;
		Flip_Delay = 1f;
		animator.SetBool("onAttack", true);
		animator.SetBool("onMove", false);
		animator.SetBool("onHit", false);
	}

	private void End_Attack()
	{
		onAttack = false;
		animator.SetBool("onAttack", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onHit", false);
	}

	private void Dash()
	{
		Flip_Delay = 1.8f;
		Mon.isInvincible = true;
		Invincible_Delay = 0.25f;
		Dash_Delay = 3f;
		if (onCam_Timer <= 0f)
		{
			Pos_Target = new global::UnityEngine.Vector3(Player.transform.position.x + 10f * (float)facingRight, Player.transform.position.y + 5f, 0f);
			global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Atk_2(Pos_Target);
			global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_9_Growling(Pos_Target);
			global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().HiddenPassage(Pos_Target);
		}
		else
		{
			Pos_Target = pos_Dash.position;
			global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Atk_2(base.transform.position);
			global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_9_Growling(base.transform.position);
			global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().HiddenPassage(base.transform.position);
		}
	}

	private void Set_Fire()
	{
		float angle = Get_Angle();
		Fire_Timer = 0f;
		angle = Check_Fire_Angle(angle);
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(0f, 0f, angle)) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(0f, 0f, angle + 20f)) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(0f, 0f, angle - 20f)) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject4 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(0f, 0f, angle + 40f)) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject5 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(0f, 0f, angle - 40f)) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Boss_4_Fire(base.transform.position);
	}

	private float Get_Angle()
	{
		float num = 0f;
		global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f);
		global::UnityEngine.Vector3 position = pos_Fire.position;
		vector.x -= position.x;
		vector.y -= position.y;
		return global::UnityEngine.Mathf.Atan2(vector.y, vector.x) * 57.29578f + 180f;
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

	private float Get_DashAngle()
	{
		float num = 0f;
		global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 5f);
		global::UnityEngine.Vector3 position = base.transform.position;
		vector.x -= position.x;
		vector.y -= position.y;
		return global::UnityEngine.Mathf.Atan2(vector.y, vector.x) * 57.29578f + 180f;
	}

	private void Set_AttackDelay()
	{
	}

	private void Sound_Mon_Damage()
	{
		if (Snd_Damage_Timer <= 0f)
		{
			Snd_Damage_Timer = 1f + (float)global::UnityEngine.Random.Range(0, 200) * 0.01f;
			global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_9_Damage(base.transform.position);
		}
	}

	private void OnTriggerStay2D(global::UnityEngine.Collider2D col)
	{
		if (col.tag == "Col_Camera")
		{
			onCam_Timer = 0.5f;
		}
	}

	private void Start_Hscene()
	{
		on_Hscene = true;
		GM.onEvent = true;
		GM.onHscene = true;
		GM.Hscene_Num = 41;
		GM.Hscene_Timer = 1f;
		isStuck_Front = global::UnityEngine.Physics2D.Linecast(Tr_Front_Start.position, Tr_Front_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
		isStuck_Back = global::UnityEngine.Physics2D.Linecast(Tr_Back_Start.position, Tr_Back_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
		if (isStuck_Front && !isStuck_Back)
		{
			Player.transform.position = new global::UnityEngine.Vector3(Player.transform.position.x - 1.5f * (float)facingRight, Player.transform.position.y, 0f);
		}
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(H_Single, Player.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
		gameObject.transform.parent = base.transform.parent;
		if (facingRight > 0)
		{
			gameObject.SendMessage("Flip");
		}
		gameObject.GetComponent<H_Ani>().Mon_Object = base.gameObject;
		animator.speed = 0f;
		GetComponent<Mon_Index>().OnOff_Object(false);
		GetComponent<global::UnityEngine.Rigidbody2D>().Sleep();
		base.transform.position = new global::UnityEngine.Vector3(Player.transform.position.x + 3f * (float)(-facingRight), Player.transform.position.y + 6.4f, 0f);
		Mon.isInvincible = true;
		global::UnityEngine.GameObject.Find("Menu").GetComponent<Menu_Control>().H_Object = gameObject;
		global::UnityEngine.GameObject.Find("Main Camera").SendMessage("Hscene_Zoom");
		if (!GM.GameOver)
		{
			Player.SendMessage("H_Down");
		}
		global::UnityEngine.GameObject.Find("Ani").SendMessage("Start_H_Scene");
		H_Timer = 6f + global::UnityEngine.Random.Range(0f, 1f);
		State = 0;
		State_Timer = 0f;
		Pos_State = 0;
		Fire_State = 0;
		onDash = false;
		Dash_Timer = 0f;
		Dash_Target_Timer = 0f;
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
		Pos_Target = new global::UnityEngine.Vector3(base.transform.position.x + GameOver_XY.x * (float)(-facingRight), base.transform.position.y + GameOver_XY.y, 0f);
	}
}
