using UnityEngine;

public class AI_Mon_38 : global::UnityEngine.MonoBehaviour
{
	private float Life_Timer;

	private float onCam_Timer;

	private int facingRight = -1;

	private float distance;

	private float dist_X;

	private float dist_Y;

	private float dist_H;

	private float Move_Speed = 5f;

	private float Speed_Orig = 1f;

	private bool on_Chase;

	private bool onAttack_Range;

	private float Hit_Delay;

	private float Flip_Delay;

	private float Move_Delay;

	private int Fire_Num;

	private int Laser_Num;

	private float Fire_Timer;

	private float Laser_Timer;

	private bool on_LaserTarget;

	private float Laser_Angle;

	private float Target_Timer;

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

	private global::UnityEngine.Vector3 Pos_Target;

	private global::UnityEngine.Vector2 Rnd_XY;

	private global::UnityEngine.Vector2 GameOver_XY;

	private bool isStuck_Front;

	private bool isStuck_Back;

	public global::UnityEngine.SpriteRenderer SR_Glow_Eye;

	public global::UnityEngine.SpriteRenderer SR_Glow_Eye_2;

	public global::UnityEngine.SpriteRenderer SR_Glow_Laser;

	public global::UnityEngine.SpriteRenderer SR_Glow_LVertical;

	public global::UnityEngine.Transform pos_Fire;

	public global::UnityEngine.GameObject _Fire;

	public global::UnityEngine.GameObject _Laser;

	public global::UnityEngine.GameObject H_Single;

	public global::UnityEngine.Transform Tr_Front_Start;

	public global::UnityEngine.Transform Tr_Front_End;

	public global::UnityEngine.Transform Tr_Back_Start;

	public global::UnityEngine.Transform Tr_Back_End;

	private global::UnityEngine.Color color_Eye;

	private global::UnityEngine.Color color_Eye_2;

	private global::UnityEngine.Color color_Laser;

	private global::UnityEngine.Color color_LVertical;

	private float Snd_Damage_Timer;

	private global::UnityEngine.Animator animator;

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
		pos_Orig = base.transform.position;
		Pos_Target = base.transform.position;
		Rnd_XY = new global::UnityEngine.Vector2((float)global::UnityEngine.Random.Range(-15, 15) * 0.01f, (float)global::UnityEngine.Random.Range(-15, 15) * 0.01f);
		GameOver_XY = new global::UnityEngine.Vector2((float)global::UnityEngine.Random.Range(50, 80) * 0.1f, global::UnityEngine.Random.Range(1f, 2.5f));
		Speed_Orig = 6f + global::UnityEngine.Random.Range(0f, 1f);
		Patrol_Range = 7f + global::UnityEngine.Random.Range(0f, 3f);
		if (Player.transform.position.x > base.transform.position.x)
		{
			Flip();
		}
		color_Eye = SR_Glow_Eye.color;
		color_Eye_2 = SR_Glow_Eye_2.color;
		color_Laser = SR_Glow_Laser.color;
		color_LVertical = SR_Glow_LVertical.color;
		SR_Glow_Eye.color = new global::UnityEngine.Color(color_Eye.r, color_Eye.g, color_Eye.b, 0f);
		SR_Glow_Eye_2.color = new global::UnityEngine.Color(color_Eye_2.r, color_Eye_2.g, color_Eye_2.b, 0f);
		SR_Glow_Laser.color = new global::UnityEngine.Color(color_Laser.r, color_Laser.g, color_Laser.b, 0f);
		SR_Glow_LVertical.color = new global::UnityEngine.Color(color_LVertical.r, color_LVertical.g, color_LVertical.b, 0f);
	}

	private void Update()
	{
		if (on_Hscene)
		{
			Color_Set_Off();
			Reset_Attack();
		}
		else
		{
			if (GM.Paused)
			{
				return;
			}
			Life_Timer += global::UnityEngine.Time.deltaTime;
			if (onCam_Timer > 0f)
			{
				onCam_Timer -= global::UnityEngine.Time.deltaTime;
			}
			if (Target_Timer > 0f)
			{
				Target_Timer -= global::UnityEngine.Time.deltaTime;
			}
			if (PC_Atk_Timer > 0f)
			{
				PC_Atk_Timer -= global::UnityEngine.Time.deltaTime;
			}
			distance = global::UnityEngine.Vector3.Distance(new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f, 0f), new global::UnityEngine.Vector3(base.transform.position.x, base.transform.position.y - 3.6f, 0f));
			dist_X = global::UnityEngine.Mathf.Abs(base.transform.position.x - Player.transform.position.x);
			dist_Y = global::UnityEngine.Mathf.Abs(Player.transform.position.y - (base.transform.position.y - 3.6f));
			if (H_Timer > 0f)
			{
				H_Timer -= global::UnityEngine.Time.deltaTime;
				if (animator.GetBool("onHit"))
				{
					H_Timer = 0f;
				}
				if ((facingRight > 0 && base.transform.position.x > global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x) || (facingRight < 0 && base.transform.position.x < global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x))
				{
					Flip();
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
						Flip();
					}
					else if (facingRight < 0 && base.transform.position.x < global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x)
					{
						Flip();
					}
					dist_H = global::UnityEngine.Vector3.Distance(base.transform.position, new global::UnityEngine.Vector3(global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x, Player.transform.position.y + 3.6f, 0f));
					if (dist_H < 1.5f)
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
						Pos_Target = new global::UnityEngine.Vector3(global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x, Player.transform.position.y + 3.6f, 0f);
						Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, Speed_Orig, global::UnityEngine.Time.deltaTime * 2f);
						base.transform.position = global::UnityEngine.Vector3.MoveTowards(base.transform.position, Pos_Target, global::UnityEngine.Time.deltaTime * Move_Speed * 1.2f * GetComponent<Monster>().Move_Speed);
					}
				}
				else if (Patrol_State == 1)
				{
					Patrol_Move_Timer += global::UnityEngine.Time.deltaTime;
					if (Patrol_Move_Timer > 1f && global::UnityEngine.Mathf.Abs(global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x - base.transform.position.x) > Patrol_Range + Rnd_XY.x)
					{
						Patrol_State = 0;
						Patrol_Idle_Timer = 0f;
					}
					Pos_Target = new global::UnityEngine.Vector3(global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x + (Patrol_Range + 10f) * (float)facingRight, Player.transform.position.y + 4.5f - Rnd_XY.y * 3f, 0f);
					Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, Speed_Orig, global::UnityEngine.Time.deltaTime * 2f);
					base.transform.position = global::UnityEngine.Vector3.MoveTowards(base.transform.position, Pos_Target, global::UnityEngine.Time.deltaTime * Move_Speed * 0.25f * GetComponent<Monster>().Move_Speed);
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
						Flip();
					}
				}
				Color_Set_Off();
				Reset_Attack();
				return;
			}
			if (distance > 50f)
			{
				Pos_Target = pos_Orig;
				base.transform.position = global::UnityEngine.Vector3.MoveTowards(base.transform.position, Pos_Target, global::UnityEngine.Time.deltaTime * Move_Speed * 0.3f * GetComponent<Monster>().Move_Speed);
				Color_Set_Off();
				Reset_Attack();
				return;
			}
			if (animator.GetBool("onHit"))
			{
				if (Mon.Get_HitCombo() > 3)
				{
					Mon.Reset_HitCombo();
					Pos_Target = new global::UnityEngine.Vector3(base.transform.position.x - 12f * (float)facingRight, pos_Orig.y + global::UnityEngine.Random.Range(-5f, 2f), 0f);
					Move_Speed = Speed_Orig;
					Move_Delay = 0f;
					Target_Timer = 1f;
				}
				if (!on_Chase)
				{
					on_Chase = true;
				}
				PC_Atk_Timer = 1f;
				base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, Pos_Target, global::UnityEngine.Time.deltaTime * Move_Speed * GetComponent<Monster>().Move_Speed);
				return;
			}
			if (PC_Atk_Timer <= 0f && GM.User_Input_Timer > 1f && !GM.onCloth && GM.Option_Int[3] == 1 && PC.grounded_Now)
			{
				Check_Flip();
				if (dist_Y < 0.5f && dist_X < 1.5f && !GM.onHscene && GM.Hscene_Timer <= 0f && (PC.State == Player_Control.AniState.Idle || PC.State == Player_Control.AniState.Run || PC.State == Player_Control.AniState.Sit || PC.State == Player_Control.AniState.Down))
				{
					Start_Hscene();
				}
				Pos_Target = new global::UnityEngine.Vector3(Player.transform.position.x + 1.2f * (float)(-facingRight), Player.transform.position.y + 3.6f, 0f);
				Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, Speed_Orig, global::UnityEngine.Time.deltaTime * 10f);
				base.transform.position = global::UnityEngine.Vector3.MoveTowards(base.transform.position, Pos_Target, global::UnityEngine.Time.deltaTime * Move_Speed * 1.2f * GetComponent<Monster>().Move_Speed);
				Color_Set_Off();
				Reset_Attack();
				return;
			}
			if (on_Chase)
			{
				if (dist_X > 60f || dist_Y > 30f)
				{
					on_Chase = false;
				}
			}
			else if (!on_Chase && dist_X < 20f && dist_Y < 20f)
			{
				on_Chase = true;
			}
			Check_Flip();
			if (!on_Chase)
			{
				return;
			}
			if (Flip_Delay > 0f)
			{
				Flip_Delay -= global::UnityEngine.Time.deltaTime;
			}
			if (Fire_Num < 0)
			{
				Fire_Timer += global::UnityEngine.Time.deltaTime;
				if (Fire_Timer > 1f)
				{
					SR_Glow_Eye.color = global::UnityEngine.Color.Lerp(SR_Glow_Eye.color, color_Eye, global::UnityEngine.Time.deltaTime * 5f);
					SR_Glow_Eye_2.color = global::UnityEngine.Color.Lerp(SR_Glow_Eye_2.color, color_Eye_2, global::UnityEngine.Time.deltaTime * 5f);
					if (Fire_Timer > 2f)
					{
						Set_Fire();
					}
				}
				else if (GM.onShield)
				{
					Fire_Num = 3;
					Fire_Timer = 0f;
				}
			}
			else
			{
				Laser_Timer += global::UnityEngine.Time.deltaTime;
				if (Laser_Timer > 1.5f)
				{
					SR_Glow_Laser.color = global::UnityEngine.Color.Lerp(SR_Glow_Laser.color, color_Laser, global::UnityEngine.Time.deltaTime * 5f);
					SR_Glow_LVertical.color = global::UnityEngine.Color.Lerp(SR_Glow_LVertical.color, color_LVertical, global::UnityEngine.Time.deltaTime * 5f);
					if (Laser_Timer > 2.8f)
					{
						if (Laser_Num == 1 && Target_Timer <= 0f)
						{
							Pos_Target = new global::UnityEngine.Vector3(base.transform.position.x + (float)(global::UnityEngine.Random.Range(5, 10) * facingRight), pos_Orig.y + global::UnityEngine.Random.Range(-5f, 2f), 0f);
							Move_Speed = 0f;
							Target_Timer = 2f;
						}
						Set_Fire_Laser();
					}
					else if (!on_LaserTarget && Laser_Timer > 2.5f)
					{
						on_LaserTarget = true;
						Laser_Angle = Get_Angle();
					}
				}
			}
			if (Move_Delay > 0f)
			{
				Move_Delay -= global::UnityEngine.Time.deltaTime;
				Move_Speed = 0f;
			}
			else
			{
				Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, Speed_Orig, global::UnityEngine.Time.deltaTime * 1f);
			}
			base.transform.position = global::UnityEngine.Vector3.MoveTowards(base.transform.position, Pos_Target, global::UnityEngine.Time.deltaTime * Move_Speed * GetComponent<Monster>().Move_Speed);
		}
	}

	private void Flip()
	{
		facingRight = -facingRight;
		Mon.Flip();
		Flip_Delay = 0.2f;
		Laser_Angle = Get_Angle();
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

	private void Color_Set_Off()
	{
		SR_Glow_Eye.color = global::UnityEngine.Color.Lerp(SR_Glow_Eye.color, new global::UnityEngine.Color(color_Eye.r, color_Eye.g, color_Eye.b, 0f), global::UnityEngine.Time.deltaTime * 2f);
		SR_Glow_Eye_2.color = global::UnityEngine.Color.Lerp(SR_Glow_Eye_2.color, new global::UnityEngine.Color(color_Eye_2.r, color_Eye_2.g, color_Eye_2.b, 0f), global::UnityEngine.Time.deltaTime * 2f);
		SR_Glow_Laser.color = global::UnityEngine.Color.Lerp(SR_Glow_Laser.color, new global::UnityEngine.Color(color_Laser.r, color_Laser.g, color_Laser.b, 0f), global::UnityEngine.Time.deltaTime * 2f);
		SR_Glow_LVertical.color = global::UnityEngine.Color.Lerp(SR_Glow_LVertical.color, new global::UnityEngine.Color(color_LVertical.r, color_LVertical.g, color_LVertical.b, 0f), global::UnityEngine.Time.deltaTime * 2f);
	}

	private void Set_AttackDelay()
	{
	}

	private void OnTriggerStay2D(global::UnityEngine.Collider2D col)
	{
		if (col.tag == "Col_Camera")
		{
			onCam_Timer = 0.5f;
		}
	}

	private void Set_Fire()
	{
		float angle = Get_Angle();
		Fire_Timer = 0f;
		Fire_Num++;
		Move_Delay = 0f;
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(0f, 0f, angle)) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(0f, 0f, angle + 22f)) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(0f, 0f, angle - 22f)) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject4 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(0f, 0f, angle + 45f)) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject5 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(0f, 0f, angle - 45f)) as global::UnityEngine.GameObject;
		gameObject.transform.Translate(global::UnityEngine.Vector3.left * 1f);
		gameObject2.transform.Translate(global::UnityEngine.Vector3.left * 1f);
		gameObject3.transform.Translate(global::UnityEngine.Vector3.left * 1f);
		gameObject4.transform.Translate(global::UnityEngine.Vector3.left * 1f);
		gameObject5.transform.Translate(global::UnityEngine.Vector3.left * 1f);
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Boss_4_Fire(base.transform.position);
		SR_Glow_Eye.color = new global::UnityEngine.Color(color_Eye.r, color_Eye.g, color_Eye.b, 0f);
		SR_Glow_Eye_2.color = new global::UnityEngine.Color(color_Eye_2.r, color_Eye_2.g, color_Eye_2.b, 0f);
	}

	private void Set_Fire_Laser()
	{
		float laser_Angle = Laser_Angle;
		Fire_Num = 0;
		on_LaserTarget = false;
		if (Laser_Num == 0)
		{
			Laser_Num++;
			Laser_Timer = 2.4f;
		}
		else
		{
			Laser_Num = 0;
			Laser_Timer = 0f;
			Move_Delay = 1f;
		}
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Laser, pos_Fire.position, global::UnityEngine.Quaternion.Euler(0f, 0f, laser_Angle)) as global::UnityEngine.GameObject;
		SR_Glow_Laser.color = new global::UnityEngine.Color(color_Laser.r, color_Laser.g, color_Laser.b, 0f);
		SR_Glow_LVertical.color = new global::UnityEngine.Color(color_LVertical.r, color_LVertical.g, color_LVertical.b, 0f);
	}

	private float Get_Angle()
	{
		global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f);
		global::UnityEngine.Vector3 position = pos_Fire.position;
		vector.x -= position.x;
		vector.y -= position.y;
		return global::UnityEngine.Mathf.Atan2(vector.y, vector.x) * 57.29578f + 180f;
	}

	private void Sound_Mon_Damage()
	{
		if (Snd_Damage_Timer <= 0f)
		{
			Snd_Damage_Timer = 1f + (float)global::UnityEngine.Random.Range(0, 200) * 0.01f;
			global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_9_Damage(base.transform.position);
		}
	}

	private void Reset_Attack()
	{
		Fire_Num = 0;
		Fire_Timer = 0f;
		Laser_Num = 0;
		on_LaserTarget = false;
		Laser_Timer = 0f;
		Move_Delay = 5f;
	}

	private void Start_Hscene()
	{
		on_Hscene = true;
		GM.onEvent = true;
		GM.onHscene = true;
		GM.Hscene_Num = 38;
		GM.Hscene_Timer = 1f;
		isStuck_Front = global::UnityEngine.Physics2D.Linecast(Tr_Front_Start.position, Tr_Front_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
		isStuck_Back = global::UnityEngine.Physics2D.Linecast(Tr_Back_Start.position, Tr_Back_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
		if (isStuck_Front && !isStuck_Back)
		{
			Player.transform.position = new global::UnityEngine.Vector3(Player.transform.position.x - 2f * (float)facingRight, Player.transform.position.y, 0f);
		}
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(H_Single, new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y - 2.5f, 0f), global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
		gameObject.transform.parent = base.transform.parent;
		if (facingRight > 0)
		{
			gameObject.SendMessage("Flip");
		}
		gameObject.GetComponent<H_Ani>().Mon_Object = base.gameObject;
		animator.speed = 0f;
		GetComponent<Mon_Index>().OnOff_Object(false);
		GetComponent<global::UnityEngine.Rigidbody2D>().Sleep();
		base.transform.position = new global::UnityEngine.Vector3(Player.transform.position.x + 0.5f * (float)(-facingRight), Player.transform.position.y + 3.8f, 0f);
		Mon.isInvincible = true;
		global::UnityEngine.GameObject.Find("Menu").GetComponent<Menu_Control>().H_Object = gameObject;
		global::UnityEngine.GameObject.Find("Main Camera").SendMessage("Hscene_Zoom");
		if (!GM.GameOver)
		{
			Player.SendMessage("H_Down");
		}
		global::UnityEngine.GameObject.Find("Ani").SendMessage("Start_H_Scene");
		H_Timer = 6f + global::UnityEngine.Random.Range(0f, 1f);
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
		Pos_Target = new global::UnityEngine.Vector3(base.transform.position.x + GameOver_XY.x * (float)(-facingRight), Player.transform.position.y + 4.5f + GameOver_XY.y, 0f);
		Reset_Attack();
	}
}
