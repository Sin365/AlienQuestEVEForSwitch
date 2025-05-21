public class AI_Queen : global::UnityEngine.MonoBehaviour
{
	private bool isDeath;

	private float Death_Timer;

	private bool on_Flying;

	private bool on_Drop;

	private bool on_Laser;

	private int EnemyState;

	private float Life_Timer;

	private int facingRight = -1;

	private float distance;

	private float dist_X;

	private float dist_Y;

	private float dist_Cam;

	private float Move_Speed = 1f;

	private float Orig_Speed = 1f;

	private int Queen_State;

	private float State_Timer;

	private bool on_Chase;

	private bool on_Attack_Range;

	private bool on_Wave_Range;

	private bool on_Tongue_Range;

	private bool on_Fire_Range;

	private bool on_Shock_Range;

	private bool on_Laser_Range;

	private float Hit_Delay;

	private float Flip_Delay;

	private float Attack_Delay;

	private float Laser_Delay;

	private float Dash_Delay;

	private float Move_Delay;

	private float Move_Push;

	private float Move_Lock_Timer;

	private float Invincible_Timer;

	private float LockHit_Delay;

	private float Tongue_Timer;

	private float Fire_Timer;

	private int Fire_Num;

	private float Shock_Timer;

	private float Wave_Timer;

	private float Laser_Timer;

	private int Laser_Num;

	private float Flying_Timer;

	private float Dash_Timer;

	private float Flying_Delay;

	private float Escape_Dash_Timer;

	private float Escape_BackDash_Timer;

	private float Screw_Dash_Timer;

	private float Screw_BackDash_Timer;

	private float rnd_X;

	private bool isStuck_Front;

	private bool isStuck_Back;

	private bool onGrounded;

	private global::UnityEngine.Vector3 Target_Dash;

	private global::UnityEngine.Vector3 Target_Death;

	private float Flying_Y;

	private float speed_Y = 3f;

	public global::UnityEngine.GameObject Explo;

	public global::UnityEngine.Transform[] explo_Pos;

	private float ExploSound_Timer;

	private float[] Explo_Timer = new float[9];

	public global::UnityEngine.GameObject Tr_Pos;

	public global::UnityEngine.Transform Tr_Front_Start;

	public global::UnityEngine.Transform Tr_Front_End;

	public global::UnityEngine.Transform Tr_Back_Start;

	public global::UnityEngine.Transform Tr_Back_End;

	public global::UnityEngine.Transform Tr_Tongue_Start;

	public global::UnityEngine.Transform Tr_Tongue_End;

	public global::UnityEngine.Transform Tr_Wave_Start;

	public global::UnityEngine.Transform Tr_Wave_End;

	public global::UnityEngine.Transform Tr_Bottom_Start;

	public global::UnityEngine.Transform Tr_Bottom_End;

	public global::UnityEngine.Transform pos_Fire;

	public global::UnityEngine.Transform pos_Glow;

	public global::UnityEngine.Transform pos_Laser;

	public global::UnityEngine.Transform pos_Wave;

	public global::UnityEngine.Transform pos_Sound;

	public global::UnityEngine.GameObject _Fire;

	public global::UnityEngine.GameObject _Fire_2;

	public global::UnityEngine.GameObject _Wave;

	public global::UnityEngine.GameObject _Laser;

	public global::UnityEngine.GameObject _Shock;

	public global::UnityEngine.GameObject _Dot;

	public global::UnityEngine.GameObject[] sound_List;

	public global::UnityEngine.GameObject sound_Atk_1;

	public global::UnityEngine.GameObject sound_Atk_2;

	public global::UnityEngine.GameObject sound_Dmg_1;

	public global::UnityEngine.GameObject sound_Dmg_2;

	public global::UnityEngine.GameObject sound_Death;

	public global::UnityEngine.GameObject sound_Laser_1;

	public global::UnityEngine.GameObject sound_Laser_2;

	public global::UnityEngine.GameObject sound_Shield;

	private global::UnityEngine.GameObject SndObj_Laser_1;

	private global::UnityEngine.GameObject SndObj_Laser_2;

	private global::UnityEngine.GameObject SndObj_Shield;

	private float Snd_Laser_Timer;

	private float Snd_Shield_Timer;

	public global::UnityEngine.SpriteRenderer SR_Glow;

	public global::UnityEngine.SpriteRenderer SR_Border;

	public global::UnityEngine.SpriteRenderer SR_Border_LT;

	public global::UnityEngine.SpriteRenderer SR_Border_RT;

	public global::UnityEngine.SpriteRenderer SR_Border_LB;

	public global::UnityEngine.SpriteRenderer SR_Border_RB;

	public global::UnityEngine.Transform pos_Center;

	public global::UnityEngine.Transform pos_Border;

	public global::UnityEngine.Transform pos_Orbit;

	public global::UnityEngine.Transform pos_Dot;

	public global::UnityEngine.Transform pos_Pelvis;

	public global::UnityEngine.Transform pos_Tit_L;

	public global::UnityEngine.Transform pos_Tit_R;

	public global::UnityEngine.Transform pos_Dash;

	private global::UnityEngine.GameObject[] Dot_List;

	private float[] Dot_Speed;

	private global::UnityEngine.Vector3[] Dot_Pos;

	private float Shield_Opcity = 0.4f;

	private float Shield_Opcity_Timer;

	private global::UnityEngine.Color Color_ON = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color Color_OFF = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	public global::UnityEngine.PolygonCollider2D COL_Shield;

	private bool Growl_Sound_1_Started;

	private bool Growl_Sound_2_Started;

	private float Snd_Growl_Timer;

	private float Snd_Damage_Timer;

	private float Snd_Attack_Timer;

	private global::UnityEngine.Animator animator;

	private global::UnityEngine.GameObject Main_Camera;

	private UI_Control UC;

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
		UC = global::UnityEngine.GameObject.Find("Status").GetComponent<UI_Control>();
		Main_Camera = global::UnityEngine.GameObject.Find("Main Camera");
		animator = GetComponent<global::UnityEngine.Animator>();
		rnd_X = (float)global::UnityEngine.Random.Range(0, 50) * 0.01f;
		Target_Dash = (Target_Death = new global::UnityEngine.Vector3(base.transform.position.x + (float)(40 * facingRight), base.transform.position.y + 12f, 0f));
		Flying_Y = base.transform.position.y + 3.8f;
		Orig_Speed = (Move_Speed = 18f + rnd_X);
		Mon.onCrouch = true;
		if (Player.transform.position.x > base.transform.position.x)
		{
			Flip();
		}
		if (GM.Get_Event(15))
		{
			Set_Death_Already();
		}
		else
		{
			Set_Flying_Start();
		}
		int num = 80;
		Dot_List = new global::UnityEngine.GameObject[num];
		Dot_Speed = new float[num];
		Dot_Pos = new global::UnityEngine.Vector3[num];
		float num2 = 0f;
		float num3 = 1.5f;
		for (int i = 0; i < Dot_List.Length; i++)
		{
			num2 = (float)(Dot_List.Length - 1 - i) * (360f / (float)num);
			pos_Orbit.rotation = global::UnityEngine.Quaternion.Euler(0f, 0f, num2);
			Dot_List[i] = global::UnityEngine.Object.Instantiate(_Dot, pos_Dot.position, base.transform.rotation) as global::UnityEngine.GameObject;
			Dot_List[i].transform.parent = pos_Center;
			Dot_List[i].transform.localRotation = global::UnityEngine.Quaternion.Euler(0f, 0f, num2);
			num3 = global::UnityEngine.Random.Range(1f, 1.6f);
			Dot_List[i].transform.localScale = new global::UnityEngine.Vector3(num3, num3, 1f);
			Dot_Speed[i] = global::UnityEngine.Random.Range(1f, 5f);
			Dot_Pos[i] = Dot_List[i].transform.localPosition;
			Dot_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().color = Color_OFF;
		}
		SR_Glow.color = Color_OFF;
		SR_Border.color = new global::UnityEngine.Color(0.5f, 1f, 0.7f, 0f);
		global::UnityEngine.SpriteRenderer sR_Border_RB = SR_Border_RB;
		global::UnityEngine.Color color_OFF = Color_OFF;
		SR_Border_LT.color = color_OFF;
		color_OFF = color_OFF;
		SR_Border_LB.color = color_OFF;
		color_OFF = color_OFF;
		SR_Border_RT.color = color_OFF;
		sR_Border_RB.color = color_OFF;
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
		Tongue_Timer += global::UnityEngine.Time.deltaTime;
		Fire_Timer += global::UnityEngine.Time.deltaTime;
		Wave_Timer += global::UnityEngine.Time.deltaTime;
		Shock_Timer += global::UnityEngine.Time.deltaTime;
		if (on_Flying)
		{
			Laser_Timer += global::UnityEngine.Time.deltaTime;
		}
		else
		{
			Flying_Timer += global::UnityEngine.Time.deltaTime;
			Dash_Timer += global::UnityEngine.Time.deltaTime;
		}
		if (Escape_Dash_Timer > 0f)
		{
			Escape_Dash_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Escape_BackDash_Timer > 0f)
		{
			Escape_BackDash_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Screw_Dash_Timer > 0f)
		{
			Screw_Dash_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Screw_BackDash_Timer > 0f)
		{
			Screw_BackDash_Timer -= global::UnityEngine.Time.deltaTime;
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
		Raycasting();
		if (on_Chase && !isDeath)
		{
			Snd_Growl_Timer += global::UnityEngine.Time.deltaTime;
			if (Snd_Damage_Timer > 0f)
			{
				Snd_Damage_Timer -= global::UnityEngine.Time.deltaTime;
			}
			if (Snd_Attack_Timer > 0f)
			{
				Snd_Attack_Timer -= global::UnityEngine.Time.deltaTime;
			}
			if (!isDeath && Flip_Delay <= 0f && Hit_Delay <= 0f && Snd_Growl_Timer > 10f)
			{
				Snd_Growl_Timer = 0f;
				switch (global::UnityEngine.Random.Range(1, 4))
				{
				case 3:
					Sound_Q_3();
					break;
				case 2:
					Sound_Q_4();
					break;
				default:
					Sound_Q_5();
					break;
				}
			}
		}
		if (Queen_State < 10)
		{
			if (Queen_State > 0)
			{
				State_Timer += global::UnityEngine.Time.deltaTime;
			}
			if (Queen_State == 0)
			{
				if (GM.Get_Event(5))
				{
					Queen_State = 1;
				}
			}
			else if (Queen_State == 1)
			{
				if (!Growl_Sound_1_Started)
				{
					Growl_Sound_1_Started = true;
					Sound_Q_2();
				}
				if (State_Timer > 1.5f)
				{
					Queen_State = 3;
					global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().Event_Cam_Pos(new global::UnityEngine.Vector3(949f, -288f, -10f), 0.4f);
					base.transform.position = new global::UnityEngine.Vector3(base.transform.position.x, base.transform.position.y + 75f, 0f);
					global::UnityEngine.GameObject.Find("BGM_List").GetComponent<BGM_Control>().Play_Boss(5);
				}
			}
			else if (Queen_State == 3)
			{
				if (State_Timer > 2f)
				{
					base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, new global::UnityEngine.Vector3(base.transform.position.x, Flying_Y, 0f), global::UnityEngine.Time.deltaTime * 0.2f);
				}
				if (State_Timer > 5f && Fire_Num < 2)
				{
					global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().Event_Cam_Pos(new global::UnityEngine.Vector3(949f, -280.5f, -10f), 0.2f);
					Fire_Num = 2;
				}
				if (State_Timer > 8f && !Growl_Sound_2_Started)
				{
					Growl_Sound_2_Started = true;
					Sound_Q_1();
					global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().MaxSize = 13f;
					global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().targetSize = 13f;
				}
				if (State_Timer > 15f)
				{
					GM.onEvent = false;
					Mon.onEvent = false;
					Queen_State = 10;
					GetComponent<global::UnityEngine.BoxCollider2D>().enabled = true;
					COL_Shield.enabled = true;
					animator.speed = 1f;
					UC.Boss_Mon = GetComponent<Monster>();
					Fire_Num = 2;
					global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().MaxSize = 11.2f;
					global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().targetSize = 11.2f;
				}
			}
		}
		else if (isDeath)
		{
			Death_Timer += global::UnityEngine.Time.deltaTime;
			if (Death_Timer > 0f)
			{
				DeathExplo();
				if (!GM.Get_Event(3) || !animator.GetBool("onDeath"))
				{
					animator.SetBool("onDeath", true);
					Main_Camera.GetComponent<Camera_Control>().Set_Queen_Death();
					Main_Camera.GetComponent<Camera_Control>().Set_Queen_Shake();
					Main_Camera.GetComponent<NoiseEffect>().enabled = true;
					global::UnityEngine.GameObject.Find("Dialogue").SendMessage("ON_RedAlert");
					GM.Set_Event(3);
				}
				if (Laser_Timer > 5f)
				{
					Laser_Death();
				}
			}
			if (Death_Timer < 0f)
			{
				base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, new global::UnityEngine.Vector3(base.transform.position.x, Flying_Y, 0f), global::UnityEngine.Time.deltaTime * 0.5f);
			}
			else
			{
				base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, Target_Death, global::UnityEngine.Time.deltaTime * 1f);
			}
		}
		else if (animator.GetBool("onHit"))
		{
			Hit_Delay = 0.2f;
			Move_Lock_Timer = 0.2f;
			Attack_Delay = 0f;
			if (Flip_Delay <= 0f)
			{
				Check_Flip();
			}
			if (Mon.MagicFire_5_Num > 5)
			{
				Mon.MagicFire_5_Num = 0;
				Hit_Delay = 0f;
				if (!on_Flying)
				{
					if (Mon.HP_Ratio > 0.5f)
					{
						if (Flying_Timer > 5f)
						{
							Set_Flying();
						}
						else if (!isStuck_Back)
						{
							Set_BackDash();
							if (GM.onShield)
							{
								Shock_Timer = 20f;
							}
						}
						else
						{
							Set_Flying();
						}
					}
					else if (Mon.HP_Ratio > 0.2f)
					{
						if (Flying_Timer > 5f)
						{
							Set_Dash();
						}
						else if (!isStuck_Back)
						{
							Set_BackDash();
							if (GM.onShield)
							{
								Shock_Timer = 10f;
							}
						}
						else
						{
							Set_Flying();
						}
					}
					else if (Flying_Timer > 3f)
					{
						Set_Dash();
					}
					else if (!isStuck_Back)
					{
						Set_BackDash();
						if (GM.onShield)
						{
							Shock_Timer = 10f;
						}
					}
					else
					{
						Set_Flying();
					}
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
				if (!on_Flying)
				{
					if (Mon.HP_Ratio > 0.5f)
					{
						if (Flying_Timer > 5f)
						{
							Set_Flying();
						}
						else if (!isStuck_Back)
						{
							Set_BackDash();
							Shock_Timer = 20f;
						}
						else
						{
							Set_Flying();
						}
					}
					else if (Mon.HP_Ratio > 0.2f)
					{
						if (Flying_Timer > 5f)
						{
							Set_Dash();
						}
						else if (!isStuck_Back)
						{
							Set_BackDash();
							Shock_Timer = 10f;
						}
						else
						{
							Set_Flying();
						}
					}
					else if (Flying_Timer > 3f)
					{
						Set_Dash();
					}
					else if (!isStuck_Back)
					{
						Set_BackDash();
						Shock_Timer = 10f;
					}
					else
					{
						Set_Flying();
					}
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
			Attack_Delay -= global::UnityEngine.Time.deltaTime;
			if (Dash_Delay < 0.5f)
			{
				Check_Flip();
			}
			base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, Target_Dash, global::UnityEngine.Time.deltaTime * 5f);
		}
		else if (on_Laser)
		{
			Laser_Delay += global::UnityEngine.Time.deltaTime;
			if (Laser_Delay > 4f)
			{
				on_Laser = false;
				Laser_Timer = 0f;
				End_Attack();
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
		else if (on_Drop)
		{
			if (onGrounded)
			{
				End_Drop();
			}
		}
		else if (on_Flying)
		{
			Flying_Delay += global::UnityEngine.Time.deltaTime;
			Check_Flip();
			if (dist_X > 15f + rnd_X && !isStuck_Front)
			{
				Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, Orig_Speed * 0.6f, global::UnityEngine.Time.deltaTime * 5f);
				base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Move_Speed * facingRight * Mon.Move_Speed);
			}
			else
			{
				base.rigidbody2D.velocity = new global::UnityEngine.Vector2(0f, 0f);
			}
			base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, new global::UnityEngine.Vector3(base.transform.position.x, Flying_Y, 0f), global::UnityEngine.Time.deltaTime * speed_Y);
			if (Mon.HP_Ratio > 0.5f)
			{
				if (Fire_Num < 2)
				{
					if (Fire_Timer > 2.5f && on_Fire_Range)
					{
						Set_Attack_Fire();
					}
					else if (Flying_Delay > 15f && !on_Fire_Range)
					{
						Set_Drop();
					}
				}
				else if (Fire_Timer > 1f)
				{
					Set_Drop();
				}
			}
			else if (Mon.HP_Ratio > 0.2f)
			{
				if (Laser_Num < 1)
				{
					if (Fire_Num < 2 && Fire_Timer > 1.5f && on_Fire_Range)
					{
						Set_Attack_Fire();
					}
					else if (Fire_Num == 2 && Fire_Timer > 2f && on_Laser_Range)
					{
						Set_Attack_Laser();
					}
					else if (Flying_Delay > 15f && !on_Laser_Range)
					{
						Set_Drop();
					}
				}
				else if (Laser_Timer > 1f)
				{
					Set_Drop();
				}
			}
			else if (Laser_Num < 2)
			{
				if (Laser_Timer > 2f)
				{
					Set_Attack_Laser();
				}
			}
			else if (Laser_Timer > 1f)
			{
				Set_Drop();
			}
		}
		else if (distance > 100f || GM.GameOver || GM.onHscene)
		{
			Check_Idle();
		}
		else
		{
			if (Move_Lock_Timer > 0f)
			{
				Move_Lock_Timer -= global::UnityEngine.Time.deltaTime;
			}
			Check_Flip();
			if (Mon.HP_Ratio > 0.5f)
			{
				Action_100();
			}
			else if (Mon.HP_Ratio > 0.2f)
			{
				Action_50();
			}
			else
			{
				Action_20();
			}
		}
		if (on_Flying)
		{
			Show_Glow();
		}
		else
		{
			Hide_Glow();
		}
		if (SndObj_Laser_1 != null || SndObj_Laser_2 != null)
		{
			Snd_Laser_Timer += global::UnityEngine.Time.deltaTime;
			if (Snd_Laser_Timer > 4f)
			{
				if (SndObj_Laser_1 != null)
				{
					global::UnityEngine.Object.Destroy(SndObj_Laser_1.gameObject);
				}
				if (SndObj_Laser_2 != null)
				{
					global::UnityEngine.Object.Destroy(SndObj_Laser_2.gameObject);
				}
			}
		}
		if (SndObj_Shield != null)
		{
			Snd_Shield_Timer += global::UnityEngine.Time.deltaTime;
			if (Snd_Shield_Timer > 12f)
			{
				global::UnityEngine.Object.Destroy(SndObj_Shield.gameObject);
			}
		}
	}

	private void Action_100()
	{
		if (PC.onScrewAttack && dist_X < 7f && Screw_BackDash_Timer <= 0f && !isStuck_Back)
		{
			Screw_BackDash_Timer = 1f;
			Set_BackDash();
		}
		else if (on_Fire_Range && Fire_Timer > 10f)
		{
			Set_Attack_Fire();
			if (Shock_Timer > 16f)
			{
				Shock_Timer = 16f;
			}
		}
		else if (on_Shock_Range && Shock_Timer > 20f)
		{
			Set_Attack_Shock();
		}
		else if (on_Wave_Range && Wave_Timer > 3f)
		{
			Set_Attack_Wave();
		}
		else if (on_Tongue_Range && Tongue_Timer > 5f)
		{
			Set_Attack_Tongue();
		}
		else if (on_Attack_Range)
		{
			Set_Attack();
		}
		else if (dist_X > 7f + rnd_X && Move_Lock_Timer <= 0f && !isStuck_Front)
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

	private void Action_50()
	{
		if (Flying_Timer > 10f)
		{
			Set_Flying();
		}
		else if (PC.onScrewAttack && dist_X < 7f && Screw_BackDash_Timer <= 0f && !isStuck_Back)
		{
			Screw_BackDash_Timer = 1f;
			Set_BackDash();
		}
		else if (on_Fire_Range && Fire_Timer > 5f)
		{
			Set_Attack_Fire();
		}
		else if (on_Shock_Range && Shock_Timer > 7f)
		{
			Set_Attack_Shock();
		}
		else if (on_Wave_Range && Wave_Timer > 3f)
		{
			Set_Attack_Wave();
		}
		else if (on_Tongue_Range && Tongue_Timer > 5f)
		{
			Set_Attack_Tongue();
		}
		else if (on_Attack_Range)
		{
			Set_Attack();
		}
		else if (dist_X > 7f + rnd_X && Move_Lock_Timer <= 0f && !isStuck_Front)
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

	private void Action_20()
	{
		if (Flying_Timer > 5f)
		{
			Set_Dash();
		}
		else if (PC.onScrewAttack && dist_X < 7f && Screw_BackDash_Timer <= 0f && !isStuck_Back)
		{
			Screw_BackDash_Timer = 1f;
			Set_BackDash();
		}
		else if (on_Fire_Range && Fire_Timer > 2f)
		{
			Set_Attack_Fire();
		}
		else if (on_Shock_Range && Shock_Timer > 3f)
		{
			Set_Attack_Shock();
		}
		else if (on_Wave_Range && Wave_Timer > 2f)
		{
			Set_Attack_Wave();
		}
		else if (on_Tongue_Range && Tongue_Timer > 5f)
		{
			Set_Attack_Tongue();
		}
		else if (on_Attack_Range)
		{
			Set_Attack();
		}
		else if (dist_X > 7f + rnd_X && Move_Lock_Timer <= 0f && !isStuck_Front)
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

	public void Flip()
	{
		Check_Idle();
		facingRight = -facingRight;
		Mon.Flip();
		Flip_Delay = 0.3f + (float)global::UnityEngine.Random.Range(0, 20) * 0.01f;
		Tr_Pos.transform.localScale = new global::UnityEngine.Vector3(-facingRight, 1f, 1f);
		GetComponent<global::UnityEngine.BoxCollider2D>().center = new global::UnityEngine.Vector2(1.6f * (float)(-facingRight), -5f);
		if (on_Flying)
		{
			base.transform.position = new global::UnityEngine.Vector3(base.transform.position.x + 2.5f * (float)facingRight, base.transform.position.y, 0f);
		}
		else
		{
			base.transform.position = new global::UnityEngine.Vector3(base.transform.position.x + 3.5f * (float)facingRight, base.transform.position.y, 0f);
		}
		if (global::UnityEngine.Random.Range(1, 3) == 1)
		{
			Sound_Q_3();
		}
		else
		{
			Sound_Q_4();
		}
	}

	private void Check_Flip()
	{
		if (facingRight > 0 && base.transform.position.x > Player.transform.position.x)
		{
			if (global::UnityEngine.Mathf.Abs(Player.transform.position.x - base.transform.position.x) > 2f)
			{
				Flip();
			}
		}
		else if (facingRight < 0 && base.transform.position.x < Player.transform.position.x && global::UnityEngine.Mathf.Abs(Player.transform.position.x - base.transform.position.x) > 2f)
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
		Move_Speed = 0f;
		animator.SetBool("onAttack", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onHit", false);
		animator.SetBool("onAttack_Upper", false);
		animator.SetBool("onAttack_Tongue", false);
		animator.SetBool("onAttack_Fire", false);
		animator.SetBool("onAttack_Laser", false);
		animator.SetBool("onAttack_Shock", false);
	}

	private void Set_Move()
	{
		Move_Speed = 0f;
		animator.SetBool("onAttack", false);
		animator.SetBool("onMove", true);
		animator.SetBool("onHit", false);
		animator.SetBool("onFlying", false);
		animator.SetBool("onAttack_Upper", false);
		animator.SetBool("onAttack_Tongue", false);
		animator.SetBool("onAttack_Fire", false);
		animator.SetBool("onAttack_Laser", false);
		animator.SetBool("onAttack_Shock", false);
	}

	private void Set_Attack()
	{
		Attack_Delay = 0.85f;
		Tongue_Timer = 10f;
		animator.SetBool("onAttack", true);
		animator.SetBool("onMove", false);
		animator.SetBool("onHit", false);
		animator.SetBool("onFlying", false);
		animator.SetBool("onAttack_Upper", false);
		animator.SetBool("onAttack_Tongue", false);
		animator.SetBool("onAttack_Fire", false);
		animator.SetBool("onAttack_Laser", false);
		animator.SetBool("onAttack_Shock", false);
	}

	private void Set_Attack_Wave()
	{
		Mon.isLockHit = true;
		LockHit_Delay = 0.7f;
		Attack_Delay = 0.85f;
		Wave_Timer = 0f;
		animator.SetBool("onAttack", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onHit", false);
		animator.SetBool("onFlying", false);
		animator.SetBool("onAttack_Upper", true);
		animator.SetBool("onAttack_Tongue", false);
		animator.SetBool("onAttack_Fire", false);
		animator.SetBool("onAttack_Laser", false);
		animator.SetBool("onAttack_Shock", false);
	}

	private void Set_Attack_Tongue()
	{
		Attack_Delay = 0.85f;
		Tongue_Timer = 0f;
		animator.SetBool("onAttack", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onHit", false);
		animator.SetBool("onFlying", false);
		animator.SetBool("onAttack_Upper", false);
		animator.SetBool("onAttack_Tongue", true);
		animator.SetBool("onAttack_Fire", false);
		animator.SetBool("onAttack_Laser", false);
		animator.SetBool("onAttack_Shock", false);
	}

	private void Set_Attack_Fire()
	{
		Mon.isLockHit = true;
		LockHit_Delay = 1.6f;
		if (on_Flying)
		{
			Attack_Delay = 1f;
		}
		else
		{
			Attack_Delay = 1.7f;
		}
		Fire_Timer = 0f;
		animator.SetBool("onAttack", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onHit", false);
		animator.SetBool("onAttack_Upper", false);
		animator.SetBool("onAttack_Tongue", false);
		animator.SetBool("onAttack_Fire", true);
		animator.SetBool("onAttack_Laser", false);
		animator.SetBool("onAttack_Shock", false);
	}

	private void Set_Attack_Shock()
	{
		Mon.isLockHit = true;
		LockHit_Delay = 2f;
		Attack_Delay = 2f;
		Shock_Timer = 0f;
		animator.SetBool("onAttack", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onHit", false);
		animator.SetBool("onAttack_Upper", false);
		animator.SetBool("onAttack_Tongue", false);
		animator.SetBool("onAttack_Fire", false);
		animator.SetBool("onAttack_Laser", false);
		animator.SetBool("onAttack_Shock", true);
	}

	private void Set_Attack_Laser()
	{
		on_Laser = true;
		Laser();
		Mon.isLockHit = true;
		LockHit_Delay = 4.5f;
		Attack_Delay = 0.1f;
		Laser_Delay = 0f;
		animator.SetBool("onAttack", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onHit", false);
		animator.SetBool("onAttack_Upper", false);
		animator.SetBool("onAttack_Tongue", false);
		animator.SetBool("onAttack_Fire", false);
		animator.SetBool("onAttack_Laser", true);
		animator.SetBool("onAttack_Shock", false);
	}

	private void End_Attack()
	{
		on_Laser = false;
		animator.SetBool("onAttack", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onHit", false);
		animator.SetBool("onAttack_Upper", false);
		animator.SetBool("onAttack_Tongue", false);
		animator.SetBool("onAttack_Fire", false);
		animator.SetBool("onAttack_Laser", false);
		animator.SetBool("onAttack_Shock", false);
		if (!on_Flying)
		{
			Fire_Num = 0;
		}
	}

	private void Set_BackDash()
	{
		Mon.isLockHit = true;
		LockHit_Delay = 0.5f;
		Attack_Delay = 0.4f;
		animator.SetBool("onAttack", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onHit", false);
		animator.SetBool("onFlying", false);
		animator.SetBool("onAttack_Upper", false);
		animator.SetBool("onAttack_Tongue", false);
		animator.SetBool("onAttack_Fire", false);
		animator.SetBool("onAttack_Laser", false);
		animator.SetBool("onAttack_Shock", false);
		animator.SetTrigger("BackDash");
		base.rigidbody2D.velocity = new global::UnityEngine.Vector2(0f, base.rigidbody2D.velocity.y);
		if (PC.onScrewAttack)
		{
			base.rigidbody2D.AddForce(global::UnityEngine.Vector3.right * 4000f * -facingRight, global::UnityEngine.ForceMode2D.Impulse);
		}
		else
		{
			base.rigidbody2D.AddForce(global::UnityEngine.Vector3.right * 3500f * -facingRight, global::UnityEngine.ForceMode2D.Impulse);
		}
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Atk_2(pos_Sound.position);
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_9_Growling(pos_Sound.position);
		if (Snd_Damage_Timer <= 0f)
		{
			Snd_Damage_Timer = 0.5f;
			if (global::UnityEngine.Random.Range(1, 3) == 1)
			{
				Sound_Q_Dmg_1();
			}
			else
			{
				Sound_Q_Dmg_2();
			}
		}
	}

	private void Set_Dash()
	{
		Mon.isLockHit = true;
		LockHit_Delay = 1.5f;
		Attack_Delay = 1.5f;
		animator.SetBool("onAttack", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onHit", false);
		animator.SetBool("onFlying", false);
		animator.SetBool("onAttack_Upper", false);
		animator.SetBool("onAttack_Tongue", false);
		animator.SetBool("onAttack_Fire", false);
		animator.SetBool("onAttack_Laser", false);
		animator.SetBool("onAttack_Shock", false);
		animator.SetTrigger("Dash");
	}

	private void Dash()
	{
		Dash_Delay = 0.8f;
		Mon.Set_QueenDash();
		int num = -1;
		for (int i = 0; i < 6; i++)
		{
			pos_Dash.localPosition = new global::UnityEngine.Vector3(-34 + 5 * i, 10f, 0f);
			if (!global::UnityEngine.Physics2D.Linecast(base.transform.position, pos_Dash.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground")))
			{
				num = i;
				i = 6;
			}
		}
		if (num < 0)
		{
			pos_Dash.localPosition = new global::UnityEngine.Vector3(-5f, 10f, 0f);
		}
		Target_Dash = pos_Dash.position;
		Set_Flying();
		speed_Y = 1f;
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Atk_2(pos_Sound.position);
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_9_Growling(pos_Sound.position);
		pos_Dash.localPosition = new global::UnityEngine.Vector3(-34f, 10f, 0f);
	}

	private void Set_Flying()
	{
		Mon.isInvincible = true;
		Invincible_Timer = 1f;
		on_Flying = true;
		on_Drop = false;
		Move_Speed = 0f;
		Flying_Timer = 0f;
		Flying_Delay = 0f;
		Dash_Timer = 0f;
		Fire_Timer = 0f;
		Laser_Timer = 0f;
		Fire_Num = 0;
		Laser_Num = 0;
		animator.SetBool("onFlying", true);
		Flying_Y = base.transform.position.y + 3.8f;
		speed_Y = 3f;
		Mon.isInvincible = true;
		Invincible_Timer = 1f;
		base.rigidbody2D.velocity = new global::UnityEngine.Vector2(0f, 0f);
		base.rigidbody2D.gravityScale = 0f;
		Shield_Opcity = 0.4f;
		Shield_Opcity_Timer = 0f;
		if (SndObj_Shield != null)
		{
			global::UnityEngine.Object.Destroy(SndObj_Shield.gameObject);
		}
		SndObj_Shield = global::UnityEngine.Object.Instantiate(sound_Shield, pos_Sound.position, base.transform.rotation) as global::UnityEngine.GameObject;
		SndObj_Shield.transform.parent = pos_Sound;
		Snd_Shield_Timer = 0f;
	}

	private void Set_Drop()
	{
		on_Flying = false;
		on_Drop = true;
		Move_Speed = 0f;
		base.rigidbody2D.gravityScale = 10f;
		Set_Idle();
		Fire_Timer = 0f;
		Wave_Timer = 0f;
		Shock_Timer = 0f;
		Fire_Num = 0;
		if (SndObj_Shield != null)
		{
			global::UnityEngine.Object.Destroy(SndObj_Shield.gameObject);
		}
	}

	private void End_Drop()
	{
		on_Flying = false;
		on_Drop = false;
		Attack_Delay = 1f;
		animator.SetBool("onFlying", false);
		FootStep();
		if (SndObj_Shield != null)
		{
			global::UnityEngine.Object.Destroy(SndObj_Shield.gameObject);
		}
	}

	private void Set_Flying_Start()
	{
		Mon.isInvincible = true;
		Mon.onEvent = true;
		Invincible_Timer = 1f;
		on_Flying = true;
		on_Drop = false;
		Move_Speed = 0f;
		Flying_Timer = 0f;
		Flying_Delay = 0f;
		Dash_Timer = 0f;
		Fire_Timer = 0f;
		Laser_Timer = 0f;
		Fire_Num = 0;
		Laser_Num = 0;
		animator.SetBool("onFlying", true);
		Flying_Y = base.transform.position.y + 3.8f;
		speed_Y = 3f;
		Mon.isInvincible = true;
		Invincible_Timer = 1f;
		base.rigidbody2D.velocity = new global::UnityEngine.Vector2(0f, 0f);
		base.rigidbody2D.gravityScale = 0f;
		Shield_Opcity = 0.4f;
		Shield_Opcity_Timer = 0f;
		base.transform.position = new global::UnityEngine.Vector3(base.transform.position.x, base.transform.position.y - 100f, 0f);
		GetComponent<global::UnityEngine.BoxCollider2D>().enabled = false;
		COL_Shield.enabled = false;
		animator.speed = 0.55f;
	}

	private void Show_Glow()
	{
		if (!Mon.isInvincible)
		{
			Mon.isInvincible = true;
			Invincible_Timer = 1f;
		}
		float num = 1.5f;
		for (int i = 0; i < Dot_List.Length; i++)
		{
			if (Dot_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().color.a < 0.01f || global::UnityEngine.Vector3.Distance(Dot_List[i].transform.position, pos_Orbit.position) < 5f)
			{
				Dot_List[i].transform.localPosition = Dot_Pos[i];
				Dot_Speed[i] = global::UnityEngine.Random.Range(1f, 5f);
				num = global::UnityEngine.Random.Range(1f, 1.6f);
				Dot_List[i].transform.localScale = new global::UnityEngine.Vector3(num, num, 1f);
				if (isDeath || Mon.HP_Ratio <= 0.2f || (Fire_Num >= 2 && Mon.HP_Ratio <= 0.5f))
				{
					Dot_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 0.6f, 0f, 1f);
				}
				else
				{
					Dot_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().color = Color_ON;
				}
			}
			else
			{
				Dot_List[i].transform.Translate(global::UnityEngine.Vector3.left * global::UnityEngine.Time.deltaTime * Dot_Speed[i] * 1f);
				if (isDeath || Mon.HP_Ratio <= 0.2f || (Fire_Num >= 2 && Mon.HP_Ratio <= 0.5f))
				{
					Dot_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().color = global::UnityEngine.Color.Lerp(Dot_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().color, new global::UnityEngine.Color(1f, 0f, 0f, 0f), global::UnityEngine.Time.deltaTime * 3f);
				}
				else
				{
					Dot_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().color = global::UnityEngine.Color.Lerp(Dot_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().color, Color_OFF, global::UnityEngine.Time.deltaTime * 3f);
				}
			}
		}
		if (isDeath || Mon.HP_Ratio <= 0.2f || (Fire_Num >= 2 && Mon.HP_Ratio <= 0.5f))
		{
			SR_Glow.color = global::UnityEngine.Color.Lerp(SR_Glow.color, new global::UnityEngine.Color(1f, 0.9f, 0.7f, 0.25f), global::UnityEngine.Time.deltaTime * 10f);
			SR_Border.color = global::UnityEngine.Color.Lerp(SR_Border.color, new global::UnityEngine.Color(1f, 0f, 0f, 0.2f), global::UnityEngine.Time.deltaTime * 10f);
			Shield_Opcity_Timer += global::UnityEngine.Time.deltaTime;
			if (Shield_Opcity_Timer > 0.05f)
			{
				Shield_Opcity_Timer = 0f;
				Shield_Opcity = global::UnityEngine.Random.Range(0.2f, 0.9f);
			}
			SR_Border_LT.color = global::UnityEngine.Color.Lerp(SR_Border_LT.color, new global::UnityEngine.Color(1f, 0f, 0f, Shield_Opcity), global::UnityEngine.Time.deltaTime * 10f);
			global::UnityEngine.SpriteRenderer sR_Border_RB = SR_Border_RB;
			global::UnityEngine.Color color = SR_Border_LT.color;
			SR_Border_LB.color = color;
			color = color;
			SR_Border_RT.color = color;
			sR_Border_RB.color = color;
			if (!isDeath)
			{
				pos_Border.localScale = global::UnityEngine.Vector3.Lerp(pos_Border.localScale, new global::UnityEngine.Vector3(1.575f, 2.1f, 1f), global::UnityEngine.Time.deltaTime * 5f);
			}
		}
		else
		{
			SR_Glow.color = global::UnityEngine.Color.Lerp(SR_Glow.color, new global::UnityEngine.Color(1f, 1f, 1f, 0.25f), global::UnityEngine.Time.deltaTime * 10f);
			SR_Border.color = global::UnityEngine.Color.Lerp(SR_Border.color, new global::UnityEngine.Color(0.5f, 1f, 0.7f, 0.2f), global::UnityEngine.Time.deltaTime * 10f);
			SR_Border_LT.color = new global::UnityEngine.Color(1f, 1f, 1f, 0.35f + global::UnityEngine.Mathf.Sin(Life_Timer * 8f) * 0.1f);
			global::UnityEngine.SpriteRenderer sR_Border_RB2 = SR_Border_RB;
			global::UnityEngine.Color color = SR_Border_LT.color;
			SR_Border_LB.color = color;
			color = color;
			SR_Border_RT.color = color;
			sR_Border_RB2.color = color;
			pos_Border.localScale = global::UnityEngine.Vector3.Lerp(pos_Border.localScale, new global::UnityEngine.Vector3(1.5f, 2f, 1f), global::UnityEngine.Time.deltaTime * 2f);
		}
		if (Queen_State == 10 && !isDeath && !COL_Shield.enabled)
		{
			COL_Shield.enabled = true;
		}
		if (GetComponent<global::UnityEngine.BoxCollider2D>().enabled)
		{
			GetComponent<global::UnityEngine.BoxCollider2D>().enabled = false;
		}
	}

	private void Hide_Glow()
	{
		if (Mon.isInvincible)
		{
			Mon.isInvincible = false;
		}
		for (int i = 0; i < Dot_List.Length; i++)
		{
			if (global::UnityEngine.Vector3.Distance(Dot_List[i].transform.position, pos_Orbit.position) > 2f)
			{
				Dot_List[i].transform.Translate(global::UnityEngine.Vector3.left * global::UnityEngine.Time.deltaTime * Dot_Speed[i] * 1f);
			}
			Dot_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().color = global::UnityEngine.Color.Lerp(Dot_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().color, Color_OFF, global::UnityEngine.Time.deltaTime * 2f);
		}
		SR_Glow.color = global::UnityEngine.Color.Lerp(SR_Glow.color, Color_OFF, global::UnityEngine.Time.deltaTime * 15f);
		SR_Border.color = global::UnityEngine.Color.Lerp(SR_Border.color, new global::UnityEngine.Color(0.5f, 1f, 0.7f, 0f), global::UnityEngine.Time.deltaTime * 14f);
		SR_Border_LT.color = global::UnityEngine.Color.Lerp(SR_Border_LT.color, Color_OFF, global::UnityEngine.Time.deltaTime * 13f);
		global::UnityEngine.SpriteRenderer sR_Border_RB = SR_Border_RB;
		global::UnityEngine.Color color = SR_Border_LT.color;
		SR_Border_LB.color = color;
		color = color;
		SR_Border_RT.color = color;
		sR_Border_RB.color = color;
		if (COL_Shield.enabled)
		{
			COL_Shield.enabled = false;
		}
		if (!GetComponent<global::UnityEngine.BoxCollider2D>().enabled)
		{
			GetComponent<global::UnityEngine.BoxCollider2D>().enabled = true;
		}
	}

	private void Fire()
	{
		float angle = Get_Angle();
		angle = Check_Fire_Angle(angle);
		if (on_Flying)
		{
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(0f, 0f, angle + 25f)) as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(0f, 0f, angle - 25f)) as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(0f, 0f, angle + 50f)) as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject gameObject4 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(0f, 0f, angle - 50f)) as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject gameObject5 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(0f, 0f, angle + 75f)) as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject gameObject6 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(0f, 0f, angle - 75f)) as global::UnityEngine.GameObject;
		}
		else
		{
			global::UnityEngine.GameObject gameObject7 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(0f, 0f, angle + 30f + (float)(Fire_Num * 10))) as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject gameObject8 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(0f, 0f, angle - 30f - (float)(Fire_Num * 10))) as global::UnityEngine.GameObject;
		}
		Fire_Num++;
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Boss_4_Fire(pos_Fire.position);
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

	private void Shock()
	{
		float angle = Get_Angle();
		angle = Check_Shock_Angle(angle);
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Shock, pos_Glow.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, angle)) as global::UnityEngine.GameObject;
	}

	private float Check_Shock_Angle(float angle)
	{
		if (facingRight > 0)
		{
			if (angle > 260f)
			{
				return 260f;
			}
			if (angle < 100f)
			{
				return 100f;
			}
			return angle;
		}
		if (angle < 280f && angle > 180f)
		{
			return 280f;
		}
		if (angle > 80f && angle < 180f)
		{
			return 80f;
		}
		return angle;
	}

	private void Laser()
	{
		float angle = Get_Angle();
		Snd_Laser_Timer = 0f;
		Laser_Num++;
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Laser, pos_Laser.position, global::UnityEngine.Quaternion.Euler(0f, 0f, angle + 9.5f * (float)(-facingRight))) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(_Laser, pos_Laser.position, global::UnityEngine.Quaternion.Euler(0f, 0f, angle + (float)(10 * -facingRight))) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(_Laser, pos_Laser.position, global::UnityEngine.Quaternion.Euler(0f, 0f, angle + 10.5f * (float)(-facingRight))) as global::UnityEngine.GameObject;
		if (facingRight > 0)
		{
			gameObject.GetComponent<Queen_Laser>().facingRight = facingRight;
			gameObject2.GetComponent<Queen_Laser>().facingRight = facingRight;
			gameObject3.GetComponent<Queen_Laser>().facingRight = facingRight;
		}
		gameObject.transform.parent = pos_Center;
		gameObject2.transform.parent = pos_Center;
		gameObject3.transform.parent = pos_Center;
		gameObject.GetComponent<Queen_Laser>().Rot_Ratio = 1.5f;
		gameObject2.GetComponent<Queen_Laser>().Rot_Ratio = 1f;
		gameObject3.GetComponent<Queen_Laser>().Rot_Ratio = 0.5f;
		gameObject2.GetComponent<Queen_Laser>().Life_Timer = -0.1f;
		gameObject3.GetComponent<Queen_Laser>().Life_Timer = -0.2f;
		if (SndObj_Laser_1 != null)
		{
			global::UnityEngine.Object.Destroy(SndObj_Laser_1.gameObject);
		}
		if (SndObj_Laser_2 != null)
		{
			global::UnityEngine.Object.Destroy(SndObj_Laser_2.gameObject);
		}
		SndObj_Laser_1 = global::UnityEngine.Object.Instantiate(sound_Laser_1, pos_Sound.position, base.transform.rotation) as global::UnityEngine.GameObject;
		SndObj_Laser_2 = global::UnityEngine.Object.Instantiate(sound_Laser_2, pos_Sound.position, base.transform.rotation) as global::UnityEngine.GameObject;
		SndObj_Laser_1.transform.parent = pos_Sound;
		SndObj_Laser_2.transform.parent = pos_Sound;
	}

	private void Wave()
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Wave, pos_Wave.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
		gameObject.transform.localScale = new global::UnityEngine.Vector3(-facingRight, 1f, 1f);
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Boss_4_Fire(pos_Sound.position);
	}

	private void Laser_Death()
	{
		Laser_Timer = 0f;
		Snd_Laser_Timer = 0f;
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Laser, pos_Pelvis.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f + global::UnityEngine.Random.Range(0f, 80f))) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(_Laser, pos_Pelvis.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 90f + global::UnityEngine.Random.Range(0f, 80f))) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(_Laser, pos_Pelvis.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 180f + global::UnityEngine.Random.Range(0f, 80f))) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject4 = global::UnityEngine.Object.Instantiate(_Laser, pos_Pelvis.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 270f + global::UnityEngine.Random.Range(0f, 80f))) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject5 = global::UnityEngine.Object.Instantiate(_Laser, pos_Tit_L.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f + global::UnityEngine.Random.Range(230f - (float)(facingRight * 50), 310f - (float)(facingRight * 50)))) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject6 = global::UnityEngine.Object.Instantiate(_Laser, pos_Tit_R.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f + global::UnityEngine.Random.Range(50f + (float)(facingRight * 50), 130f + (float)(facingRight * 50)))) as global::UnityEngine.GameObject;
		gameObject.transform.parent = pos_Center;
		gameObject2.transform.parent = pos_Center;
		gameObject3.transform.parent = pos_Center;
		gameObject4.transform.parent = pos_Center;
		gameObject5.transform.parent = pos_Center;
		gameObject6.transform.parent = pos_Center;
		float num = 0.5f;
		gameObject.transform.Translate(global::UnityEngine.Vector3.left * num);
		gameObject2.transform.Translate(global::UnityEngine.Vector3.left * num);
		gameObject3.transform.Translate(global::UnityEngine.Vector3.left * num);
		gameObject4.transform.Translate(global::UnityEngine.Vector3.left * num);
		gameObject.GetComponent<Queen_Laser>().Rot_Ratio = global::UnityEngine.Random.Range(0.1f, 0.3f);
		gameObject2.GetComponent<Queen_Laser>().Rot_Ratio = 0.1f;
		gameObject3.GetComponent<Queen_Laser>().Rot_Ratio = 0.1f;
		gameObject4.GetComponent<Queen_Laser>().Rot_Ratio = 0.1f;
		gameObject5.GetComponent<Queen_Laser>().Rot_Ratio = global::UnityEngine.Random.Range(0.1f, 0.3f);
		gameObject6.GetComponent<Queen_Laser>().Rot_Ratio = global::UnityEngine.Random.Range(0.1f, 0.3f);
		gameObject2.GetComponent<Queen_Laser>().Life_Timer = -0.1f;
		gameObject3.GetComponent<Queen_Laser>().Life_Timer = -0.2f;
		gameObject4.GetComponent<Queen_Laser>().Life_Timer = -0.3f;
		gameObject5.GetComponent<Queen_Laser>().Life_Timer = -0.1f;
		gameObject6.GetComponent<Queen_Laser>().Life_Timer = -0.2f;
		if (SndObj_Laser_1 != null)
		{
			global::UnityEngine.Object.Destroy(SndObj_Laser_1.gameObject);
		}
		if (SndObj_Laser_2 != null)
		{
			global::UnityEngine.Object.Destroy(SndObj_Laser_2.gameObject);
		}
		SndObj_Laser_1 = global::UnityEngine.Object.Instantiate(sound_Laser_1, pos_Sound.position, base.transform.rotation) as global::UnityEngine.GameObject;
		SndObj_Laser_2 = global::UnityEngine.Object.Instantiate(sound_Laser_2, pos_Sound.position, base.transform.rotation) as global::UnityEngine.GameObject;
		SndObj_Laser_1.transform.parent = pos_Sound;
		SndObj_Laser_2.transform.parent = pos_Sound;
	}

	private void Set_AttackDelay()
	{
	}

	private void Sound_Attack()
	{
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Atk_4(pos_Sound.position);
	}

	private void Sound_Tongue()
	{
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Atk_7(pos_Sound.position);
	}

	private void FootStep()
	{
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().FootStep_Queen(pos_Sound.position);
	}

	private void Sound_Damage()
	{
		if (!isDeath && Mon.Get_HitCombo() == 1 && Snd_Damage_Timer <= 0f)
		{
			Snd_Damage_Timer = 0.5f;
			if (global::UnityEngine.Random.Range(1, 3) == 1)
			{
				Sound_Q_Dmg_1();
			}
			else
			{
				Sound_Q_Dmg_2();
			}
		}
	}

	private void Sound_Q_Attack()
	{
		if (Snd_Attack_Timer <= 0f)
		{
			Snd_Attack_Timer = 0.3f;
			if (global::UnityEngine.Random.Range(1, 3) == 1)
			{
				Sound_Q_Atk_1();
			}
			else
			{
				Sound_Q_Atk_2();
			}
		}
	}

	private void Sound_Q_1()
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(sound_List[0], Player.transform.position, pos_Sound.rotation) as global::UnityEngine.GameObject;
	}

	private void Sound_Q_2()
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(sound_List[1], Player.transform.position, pos_Sound.rotation) as global::UnityEngine.GameObject;
	}

	private void Sound_Q_3()
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(sound_List[2], pos_Sound.position, pos_Sound.rotation) as global::UnityEngine.GameObject;
	}

	private void Sound_Q_4()
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(sound_List[3], pos_Sound.position, pos_Sound.rotation) as global::UnityEngine.GameObject;
	}

	private void Sound_Q_5()
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(sound_List[4], pos_Sound.position, pos_Sound.rotation) as global::UnityEngine.GameObject;
	}

	private void Sound_Q_Dmg_1()
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(sound_Dmg_1, pos_Sound.position, pos_Sound.rotation) as global::UnityEngine.GameObject;
	}

	private void Sound_Q_Dmg_2()
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(sound_Dmg_2, pos_Sound.position, pos_Sound.rotation) as global::UnityEngine.GameObject;
	}

	private void Sound_Q_Atk_1()
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(sound_Atk_1, pos_Sound.position, pos_Sound.rotation) as global::UnityEngine.GameObject;
	}

	private void Sound_Q_Atk_2()
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(sound_Atk_2, pos_Sound.position, pos_Sound.rotation) as global::UnityEngine.GameObject;
	}

	public void Set_Death()
	{
		isDeath = true;
		Death_Timer = 0f;
		COL_Shield.enabled = false;
		UC.Boss_Mon = null;
		Laser_Timer = 10f;
		Mon.onEvent = true;
		if (global::UnityEngine.GameObject.Find("Reactor_Sphere") != null)
		{
			Target_Death = global::UnityEngine.GameObject.Find("Reactor_Sphere").transform.position;
			if (base.transform.position.x < Target_Death.x)
			{
				GM.Set_Event(6);
				Target_Death = new global::UnityEngine.Vector3(Target_Death.x - 9f, Target_Death.y + 1.2f, 0f);
			}
			else
			{
				Target_Death = new global::UnityEngine.Vector3(Target_Death.x + 9f, Target_Death.y + 1.2f, 0f);
			}
			if (facingRight > 0)
			{
				GM.Set_Event(7);
			}
		}
		else
		{
			Target_Death = new global::UnityEngine.Vector3(938.79f, -270.8f, 0f);
		}
		if (on_Flying)
		{
			DeathExplo();
			animator.SetBool("onDeath", true);
			Main_Camera.GetComponent<Camera_Control>().Set_Queen_Death();
			Main_Camera.GetComponent<Camera_Control>().Set_Queen_Shake();
			Main_Camera.GetComponent<NoiseEffect>().enabled = true;
			global::UnityEngine.GameObject.Find("Dialogue").SendMessage("ON_RedAlert");
			if (!GM.Get_Event(3))
			{
				GM.Set_Event(3);
			}
		}
		else
		{
			Death_Timer = -0.6f;
			on_Flying = true;
			on_Drop = false;
			animator.Play("Hit_Death", 0, 0f);
			animator.SetBool("onFlying", true);
			animator.SetBool("onAttack_Upper", false);
			animator.SetBool("onAttack_Tongue", false);
			animator.SetBool("onAttack_Fire", false);
			animator.SetBool("onAttack_Laser", false);
			animator.SetBool("onAttack_Shock", false);
			base.rigidbody2D.velocity = new global::UnityEngine.Vector2(0f, 0f);
			base.rigidbody2D.gravityScale = 0f;
			Shield_Opcity = 0.4f;
			Shield_Opcity_Timer = 0f;
			if (SndObj_Shield != null)
			{
				global::UnityEngine.Object.Destroy(SndObj_Shield.gameObject);
			}
			SndObj_Shield = global::UnityEngine.Object.Instantiate(sound_Shield, pos_Sound.position, pos_Sound.rotation) as global::UnityEngine.GameObject;
			SndObj_Shield.transform.parent = pos_Sound;
			Snd_Shield_Timer = 0f;
		}
		Main_Camera.GetComponent<Camera_Control>().Set_Shake_Timer(2.5f, Main_Camera.transform.position);
		if (Snd_Damage_Timer < 0.1f)
		{
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(sound_Dmg_2, pos_Sound.position, pos_Sound.rotation) as global::UnityEngine.GameObject;
		}
		global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(sound_Death, pos_Sound.position, pos_Sound.rotation) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(sound_Death, pos_Sound.position, pos_Sound.rotation) as global::UnityEngine.GameObject;
	}

	private void Set_Death_Already()
	{
		Queen_State = 10;
		isDeath = true;
		Death_Timer = 0f;
		COL_Shield.enabled = false;
		UC.Boss_Mon = null;
		Laser_Timer = 10f;
		Mon.Set_QueenDeath();
		on_Flying = true;
		on_Drop = false;
		if (global::UnityEngine.GameObject.Find("Reactor_Sphere") != null)
		{
			Target_Death = global::UnityEngine.GameObject.Find("Reactor_Sphere").transform.position;
			if (GM.Get_Event(6))
			{
				Target_Death = new global::UnityEngine.Vector3(Target_Death.x - 9f, Target_Death.y + 1.2f, 0f);
			}
			else
			{
				Target_Death = new global::UnityEngine.Vector3(Target_Death.x + 9f, Target_Death.y + 1.2f, 0f);
			}
			if (GM.Get_Event(7))
			{
				facingRight = -facingRight;
				Mon.Flip();
				Tr_Pos.transform.localScale = new global::UnityEngine.Vector3(-facingRight, 1f, 1f);
				GetComponent<global::UnityEngine.BoxCollider2D>().center = new global::UnityEngine.Vector2(1.6f * (float)(-facingRight), -5f);
			}
		}
		else
		{
			Target_Death = new global::UnityEngine.Vector3(938.79f, -270.8f, 0f);
		}
		base.transform.position = Target_Death;
		animator.SetBool("onDeath", true);
		base.rigidbody2D.velocity = new global::UnityEngine.Vector2(0f, 0f);
		base.rigidbody2D.gravityScale = 0f;
		Shield_Opcity = 0.4f;
		Shield_Opcity_Timer = 0f;
		global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().MaxSize = 11.2f;
		global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().targetSize = 11.2f;
	}

	private void DeathExplo()
	{
		if (ExploSound_Timer <= 0f)
		{
			ExploSound_Timer = global::UnityEngine.Random.Range(0.2f, 0.5f);
			global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Explo(pos_Sound.position);
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

	private void Raycasting()
	{
		isStuck_Front = global::UnityEngine.Physics2D.Linecast(Tr_Front_Start.position, Tr_Front_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
		isStuck_Back = global::UnityEngine.Physics2D.Linecast(Tr_Back_Start.position, Tr_Back_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
		onGrounded = global::UnityEngine.Physics2D.Linecast(Tr_Bottom_Start.position, Tr_Bottom_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
		on_Tongue_Range = global::UnityEngine.Physics2D.Linecast(Tr_Tongue_Start.position, Tr_Tongue_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player_Dmg"));
		on_Wave_Range = global::UnityEngine.Physics2D.Linecast(Tr_Wave_Start.position, Tr_Wave_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player_Dmg"));
		distance = global::UnityEngine.Vector3.Distance(new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f, Player.transform.position.z), base.transform.position);
		dist_X = global::UnityEngine.Mathf.Abs(base.transform.position.x - Player.transform.position.x);
		dist_Y = global::UnityEngine.Mathf.Abs(Player.transform.position.y - (base.transform.position.y - 8.86f));
		dist_Cam = global::UnityEngine.Vector3.Distance(new global::UnityEngine.Vector3(Main_Camera.transform.position.x, Main_Camera.transform.position.y, 0f), base.transform.position);
		if (!on_Chase)
		{
			if (dist_X < 35f && Player.transform.position.y < base.transform.position.y + 7.1596f && Player.transform.position.y > base.transform.position.y - 18.84f)
			{
				EnemyState = 2;
			}
			else
			{
				EnemyState = 0;
			}
		}
		if (EnemyState == 2 && !on_Chase)
		{
			on_Chase = true;
		}
		if (PC.State == Player_Control.AniState.Sit || PC.State == Player_Control.AniState.Slide || PC.State == Player_Control.AniState.Down)
		{
			if (dist_X < 5f + rnd_X && dist_Y < 13f)
			{
				on_Attack_Range = true;
			}
			else
			{
				on_Attack_Range = false;
			}
		}
		else if (dist_X < 8.5f + rnd_X && dist_Y < 13f)
		{
			on_Attack_Range = true;
		}
		else
		{
			on_Attack_Range = false;
		}
		if (dist_Cam < 21f)
		{
			on_Fire_Range = true;
			on_Shock_Range = true;
			on_Laser_Range = true;
		}
		else
		{
			on_Fire_Range = false;
			on_Shock_Range = false;
			on_Laser_Range = false;
		}
	}
}
