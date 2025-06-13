using UnityEngine;

public class AI_Mon_FaceHugger : global::UnityEngine.MonoBehaviour
{
	public enum AniState
	{
		Idle = 0,
		Move = 1,
		Attack = 2
	}

	public AI_Mon_FaceHugger.AniState State;

	public int HP = 100;

	public int HP_Max = 100;

	public float HP_Ratio = 1f;

	public int Damage = 30;

	public float DmgForce = 30f;

	private bool isDeath;

	private float Life_Timer;

	private int facingRight = -1;

	private float distance;

	private float dist_DownCenter;

	private float dist_X;

	private float dist_Y;

	private float Move_Speed = 1f;

	private bool onGrounded;

	private bool grounded_L_R;

	private float Grounded_Timer;

	private bool on_Chase;

	private int Patrol_State;

	private float Patrol_Idle_Timer;

	private float Patrol_Move_Timer;

	private float Patrol_Pos_X;

	private float Patrol_Range;

	private bool isStuck_Front;

	private bool Range_Attack;

	private bool Range_Grab;

	private float Attack_Delay;

	private float Flip_Delay;

	private float rnd_X;

	private bool on_Hscene;

	private bool on_Grab_Down;

	private float H_Timer;

	private float H_Pursue_Timer;

	private bool onPoisonSkill;

	private float Poison_Skill_Timer;

	private float Poison_Smog_Timer;

	private float Toxic_Timer;

	private bool onTail_Up;

	private bool onPauseGravity;

	private global::UnityEngine.Vector2 Mon_Velocity = new global::UnityEngine.Vector2(0f, 0f);

	private float PC_Body_Delay;

	public global::UnityEngine.GameObject Ctrl_1;

	public global::UnityEngine.SpriteRenderer HP_Bar_BG;

	public global::UnityEngine.SpriteRenderer HP_Bar;

	private bool onHpBar;

	public global::UnityEngine.GameObject Explo;

	public global::UnityEngine.GameObject Pos_Root;

	public global::UnityEngine.GameObject pos_explo_1;

	public global::UnityEngine.GameObject pos_explo_2;

	public global::UnityEngine.GameObject pos_explo_3;

	public global::UnityEngine.Transform pos_Text;

	public global::UnityEngine.Transform pos_Text_P;

	public global::UnityEngine.Transform pos_L;

	public global::UnityEngine.Transform pos_R;

	public global::UnityEngine.Transform pos_Bottom_L;

	public global::UnityEngine.Transform pos_Bottom_R;

	public global::UnityEngine.Transform pos_Stand_1;

	public global::UnityEngine.Transform pos_Stand_2;

	public global::UnityEngine.Transform pos_Down;

	public global::UnityEngine.Transform pos_Front;

	public global::UnityEngine.GameObject Blood_Obj;

	public global::UnityEngine.GameObject _Icon;

	public global::UnityEngine.Sprite Icon_Spr;

	public int Index;

	public global::UnityEngine.GameObject[] Object_Body;

	public global::UnityEngine.GameObject[] Object_Fly;

	private global::UnityEngine.Color color_Orig = new global::UnityEngine.Color(1f, 1f, 1f);

	private global::UnityEngine.Color color_Poison = new global::UnityEngine.Color(0.75f, 1f, 0f);

	private global::UnityEngine.Color color_Slow = new global::UnityEngine.Color(0f, 0.92f, 1f);

	private Sound_Control SC;
    Player_Control PC => GameManager.instance.PC;
    GameObject Player => GameManager.instance.gobj_Player;

    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//Player = global::UnityEngine.GameObject.Find("Player");
		//PC = Player.GetComponent<Player_Control>();
		SC = GameManager.instance.sc_Sound_List;
		rnd_X = global::UnityEngine.Random.Range(0.3f, 1f);
		Move_Speed = 7f + global::UnityEngine.Random.Range(0f, 0.5f);
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
			if (GM.GameOver || GM.onHscene || GM.Option_Int[4] != 1)
			{
				Off_HP_Bar();
			}
		}
		if (Object_Body.Length > 0)
		{
			for (int i = 0; i < Object_Body.Length; i++)
			{
				Object_Body[i].GetComponent<global::UnityEngine.SkinnedMeshRenderer>().sortingOrder += 20 * Index;
			}
		}
		if (Object_Fly.Length > 0)
		{
			for (int j = 0; j < Object_Fly.Length; j++)
			{
				Object_Fly[j].GetComponent<global::UnityEngine.SkinnedMeshRenderer>().sortingOrder += 20 * Index;
			}
		}
		if (global::UnityEngine.Random.Range(0, 10) > 5)
		{
			onTail_Up = true;
			Object_Body[0].GetComponent<global::UnityEngine.SkinnedMeshRenderer>().enabled = false;
			Object_Body[1].GetComponent<global::UnityEngine.SkinnedMeshRenderer>().enabled = true;
		}
		else
		{
			Object_Body[1].GetComponent<global::UnityEngine.SkinnedMeshRenderer>().enabled = false;
		}
		if (Player.transform.position.x > base.transform.position.x)
		{
			Flip();
		}
		if (State == AI_Mon_FaceHugger.AniState.Attack)
		{
			Set_Attack();
		}
		else
		{
			OnOff_Attack(false);
		}
		if (_Icon != null)
		{
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Icon, new global::UnityEngine.Vector3(0f, 0f, 0f), global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
			gameObject.GetComponent<Info_MonIcon>().Mon_Num = 21;
			gameObject.GetComponent<Info_MonIcon>().MonCenter = base.transform;
			gameObject.GetComponent<Info_MonIcon>().Set_MonIcon(Icon_Spr);
			gameObject.GetComponent<Info_MonIcon>().Set_Dist();
		}
	}

	private void Update()
	{
		if (!GM.Paused && !on_Hscene)
		{
			if (onPauseGravity)
			{
				onPauseGravity = false;
				GetComponent<global::UnityEngine.Animator>().speed = 1f;
				GetComponent<global::UnityEngine.Rigidbody2D>().WakeUp();
				base.GetComponent<UnityEngine.Rigidbody2D>().velocity = Mon_Velocity;
			}
			Life_Timer += global::UnityEngine.Time.deltaTime;
			Grounded_Timer += global::UnityEngine.Time.deltaTime;
			if (PC_Body_Delay > 0f)
			{
				PC_Body_Delay -= global::UnityEngine.Time.deltaTime;
			}
			if (Toxic_Timer > 0f)
			{
				Toxic_Timer -= global::UnityEngine.Time.deltaTime;
			}
			if (Poison_Skill_Timer > 0f)
			{
				Poison_Skill_Timer -= global::UnityEngine.Time.deltaTime;
			}
			if (Poison_Smog_Timer > 0f)
			{
				Poison_Smog_Timer -= global::UnityEngine.Time.deltaTime;
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
					Off_Magic();
				}
			}
			Raycasting();
			if (H_Timer > 0f)
			{
				H_Timer -= global::UnityEngine.Time.deltaTime;
			}
			else if (!(distance > 45f))
			{
				if (GM.GameOver || GM.onHscene)
				{
					if (!GM.onChestBurster && GM.Hscene_Num == 0 && GM.Hscene_Timer <= 0f)
					{
						if (facingRight > 0 && base.transform.position.x > global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x)
						{
							Flip();
						}
						else if (facingRight < 0 && base.transform.position.x < global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x)
						{
							Flip();
						}
						else if (dist_Y < 0.8f && dist_DownCenter < 1.5f && PC.grounded_Now)
						{
							on_Grab_Down = true;
							Start_H_Grab();
						}
						else if (Attack_Delay > 0f)
						{
							Attack_Delay -= global::UnityEngine.Time.deltaTime;
							if (Attack_Delay < 1.4f && State == AI_Mon_FaceHugger.AniState.Attack && onGrounded)
							{
								Set_Idle();
								Grounded_Timer = 0f;
							}
						}
						else if (Range_Grab)
						{
							Set_JumpGrab();
						}
						else if (isStuck_Front)
						{
							if (State != AI_Mon_FaceHugger.AniState.Idle)
							{
								Set_Idle();
							}
						}
						else
						{
							if (State != AI_Mon_FaceHugger.AniState.Move)
							{
								Set_Move();
							}
							base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * facingRight * Move_Speed);
						}
						Patrol_Pos_X = 0f;
						H_Pursue_Timer += global::UnityEngine.Time.deltaTime;
					}
					if (Attack_Delay > 0f)
					{
						Attack_Delay -= global::UnityEngine.Time.deltaTime;
						if (Attack_Delay < 1.4f && State == AI_Mon_FaceHugger.AniState.Attack && onGrounded)
						{
							Set_Idle();
							Grounded_Timer = 0f;
						}
					}
					else
					{
						if (Patrol_Pos_X == 0f)
						{
							Patrol_Pos_X = global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x;
						}
						if (H_Pursue_Timer > 0f)
						{
							if (State != AI_Mon_FaceHugger.AniState.Idle)
							{
								Set_Idle();
							}
							Patrol_State = 0;
							Patrol_Idle_Timer = global::UnityEngine.Random.Range(-1f, 1f);
						}
						else if (State == AI_Mon_FaceHugger.AniState.Move)
						{
							Patrol_Move_Timer += global::UnityEngine.Time.deltaTime;
							if (Patrol_Move_Timer > 1f && (isStuck_Front || global::UnityEngine.Mathf.Abs(global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x - base.transform.position.x) > Patrol_Range))
							{
								if (State != AI_Mon_FaceHugger.AniState.Idle)
								{
									Set_Idle();
								}
								Patrol_State = 0;
								Patrol_Idle_Timer = 0f;
							}
							base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * facingRight * Move_Speed);
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
							else if (State != AI_Mon_FaceHugger.AniState.Idle)
							{
								Set_Idle();
							}
						}
						H_Pursue_Timer = 0f;
					}
				}
				else if (Flip_Delay > 0f)
				{
					Flip_Delay -= global::UnityEngine.Time.deltaTime;
				}
				else if (Attack_Delay > 0f)
				{
					Attack_Delay -= global::UnityEngine.Time.deltaTime;
					if (!GM.onChestBurster && GM.Hscene_Num == 0 && GM.Hscene_Timer <= 0f && !GM.onCloth && dist_X < 1.2f && PC.grounded_Now && Check_PC_State() && ((PC.State != Player_Control.AniState.Sit && base.transform.position.y > Player.transform.position.y + 3.5f && base.transform.position.y < Player.transform.position.y + 5.5f) || (PC.State == Player_Control.AniState.Sit && base.transform.position.y > Player.transform.position.y && base.transform.position.y < Player.transform.position.y + 2.5f)))
					{
						on_Grab_Down = false;
						Start_H_Grab();
					}
					else if (Attack_Delay < 1.4f && State == AI_Mon_FaceHugger.AniState.Attack && onGrounded)
					{
						Set_Idle();
						Grounded_Timer = 0f;
					}
				}
				else if (facingRight > 0 && base.transform.position.x > Player.transform.position.x)
				{
					if (global::UnityEngine.Mathf.Abs(Player.transform.position.x - base.transform.position.x) > 0.4f)
					{
						Flip();
					}
				}
				else if (facingRight < 0 && base.transform.position.x < Player.transform.position.x)
				{
					if (global::UnityEngine.Mathf.Abs(Player.transform.position.x - base.transform.position.x) > 0.4f)
					{
						Flip();
					}
				}
				else if (Range_Attack)
				{
					Set_Attack();
				}
				else if (isStuck_Front || global::UnityEngine.Mathf.Abs(Player.transform.position.x - base.transform.position.x) < rnd_X)
				{
					if (State != AI_Mon_FaceHugger.AniState.Idle)
					{
						Set_Idle();
					}
				}
				else
				{
					if (State != AI_Mon_FaceHugger.AniState.Move)
					{
						Set_Move();
					}
					base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * facingRight * Move_Speed);
				}
			}
			if (HP_Bar != null)
			{
				if (GM.GameOver || GM.onHscene)
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

	private void Flip()
	{
		facingRight = -facingRight;
		Flip_Delay = 0.2f;
		Pos_Root.transform.localScale = new global::UnityEngine.Vector3(-facingRight, 1f, 1f);
		bool flip = ((facingRight > 0) ? true : false);
		if (Ctrl_1 != null)
		{
			Ctrl_1.GetComponent<Puppet2D_GlobalControl>().flip = flip;
		}
		GetComponent<global::UnityEngine.BoxCollider2D>().offset = new global::UnityEngine.Vector2(0.12f * (float)facingRight, 0.69f);
	}

	private bool Check_PC_State()
	{
		if (PC.State == Player_Control.AniState.Idle || PC.State == Player_Control.AniState.Run || PC.State == Player_Control.AniState.Sit || PC.State == Player_Control.AniState.Down || PC.State == Player_Control.AniState.Spin)
		{
			return true;
		}
		return false;
	}

	private void Set_Idle()
	{
		if (State == AI_Mon_FaceHugger.AniState.Attack)
		{
			OnOff_Attack(false);
		}
		State = AI_Mon_FaceHugger.AniState.Idle;
		GetComponent<global::UnityEngine.Animator>().SetTrigger("onIdle");
	}

	private void Set_Move()
	{
		if (State == AI_Mon_FaceHugger.AniState.Attack)
		{
			OnOff_Attack(false);
		}
		State = AI_Mon_FaceHugger.AniState.Move;
		GetComponent<global::UnityEngine.Animator>().SetTrigger("onMove");
	}

	private void Set_Attack()
	{
		State = AI_Mon_FaceHugger.AniState.Attack;
		GetComponent<global::UnityEngine.Animator>().SetTrigger("onAttack");
		OnOff_Attack(true);
		Attack_Delay = 1.5f;
		GameManager.instance.sc_Sound_List.Mon_Atk_1(base.transform.position);
		if (PC.State == Player_Control.AniState.Sit)
		{
			base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.up * (20f + global::UnityEngine.Random.Range(0f, 1f)), global::UnityEngine.ForceMode2D.Impulse);
		}
		else
		{
			base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.up * (30f + global::UnityEngine.Random.Range(0f, 1f)), global::UnityEngine.ForceMode2D.Impulse);
		}
		base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * 13f * facingRight, global::UnityEngine.ForceMode2D.Impulse);
	}

	private void Set_JumpBack()
	{
		State = AI_Mon_FaceHugger.AniState.Attack;
		GetComponent<global::UnityEngine.Animator>().SetTrigger("onAttack");
		OnOff_Attack(true);
		Attack_Delay = 1f;
		base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.up * 20f, global::UnityEngine.ForceMode2D.Impulse);
		base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * 10f * -facingRight, global::UnityEngine.ForceMode2D.Impulse);
	}

	private void Set_JumpGrab()
	{
		State = AI_Mon_FaceHugger.AniState.Attack;
		GetComponent<global::UnityEngine.Animator>().SetTrigger("onAttack");
		OnOff_Attack(true);
		Attack_Delay = 0.8f;
		GameManager.instance.sc_Sound_List.Mon_Atk_1(base.transform.position);
		base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.up * 15f, global::UnityEngine.ForceMode2D.Impulse);
		base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * 15f * facingRight, global::UnityEngine.ForceMode2D.Impulse);
	}

	private void Check_Poison()
	{
		if (isDeath || HP <= 0)
		{
			return;
		}
		global::UnityEngine.Vector3 pos_Font = new global::UnityEngine.Vector3(pos_Text_P.position.x + (float)global::UnityEngine.Random.Range(-50, 50) * 0.01f, pos_Text_P.position.y + (float)global::UnityEngine.Random.Range(0, 100) * 0.01f, 0f);
		if (Poison_Smog_Timer > 0f)
		{
			Toxic_Timer = 0.1f;
		}
		else
		{
			Toxic_Timer = 0.2f;
		}
		HP -= GM.Get_PoisonDamage(pos_Font, 1);
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

	private void Start_H_Grab()
	{
		on_Hscene = true;
		GM.onEvent = true;
		GM.onHscene = true;
		GM.Hscene_Timer = 2f;
		H_Timer = 4f;
		H_Pursue_Timer = 0f;
		GM.onFaceHugger = true;
		OnOff_Object(false);
		global::UnityEngine.GameObject.Find("Ani").SendMessage("Start_H_Grab");
		if (on_Grab_Down)
		{
			GM.Hscene_Num = 97;
			global::UnityEngine.GameObject.Find("Player_DownGrab").GetComponent<Player_Grab>().Mon_Object = base.gameObject;
			global::UnityEngine.GameObject.Find("Player_DownGrab").SendMessage("H_Play");
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
		else
		{
			GM.Hscene_Num = 96;
			Player.SendMessage("Off_Effect");
			if (PC.facingRight > 0 && base.transform.position.x < Player.transform.position.x)
			{
				Player.SendMessage("Flip");
			}
			else if (PC.facingRight < 0 && base.transform.position.x > Player.transform.position.x)
			{
				Player.SendMessage("Flip");
			}
			base.transform.position = new global::UnityEngine.Vector3(Player.transform.position.x + 0.8f * (float)(-facingRight), Player.transform.position.y + 4f, 0f);
			global::UnityEngine.GameObject.Find("Player_Grab").GetComponent<Player_Grab>().Mon_Object = base.gameObject;
			global::UnityEngine.GameObject.Find("Menu").GetComponent<Menu_Control>().H_Object = global::UnityEngine.GameObject.Find("Player_Grab");
			global::UnityEngine.GameObject.Find("Player_Grab").SendMessage("H_Play");
		}
		global::UnityEngine.GameObject.Find("Main Camera").SendMessage("Hscene_Zoom");
	}

	private void End_H_Grab()
	{
		on_Hscene = false;
		GM.onEvent = false;
		GM.onHscene = false;
		GM.Hscene_Num = 0;
		GM.onFaceHugger = false;
		H_Timer = 0f;
		OnOff_Object(true);
		if (!on_Grab_Down)
		{
			onPauseGravity = false;
			GetComponent<global::UnityEngine.Animator>().speed = 1f;
			GetComponent<global::UnityEngine.Rigidbody2D>().WakeUp();
			Set_JumpBack();
			GM.Damage_Timer = 2f;
			PC.Down(-8 * PC.facingRight);
			global::UnityEngine.GameObject.Find("Main Camera").SendMessage("Reset_Zoom");
			global::UnityEngine.GameObject.Find("Ani").SendMessage("OFF_H_Down");
		}
		on_Grab_Down = false;
	}

	private void End_H_Grab_GameOver()
	{
		OnOff_Object(true);
		Set_JumpBack();
		PC.Down(-15 * PC.facingRight);
		global::UnityEngine.GameObject.Find("Ani").SendMessage("OFF_H_Down");
		on_Grab_Down = false;
		global::UnityEngine.Object.Destroy(base.gameObject);
	}

	private void Death()
	{
		isDeath = true;
		if (GM.Bonus_Blood > 0)
		{
			Make_Blood();
		}
		GM.Monster_Kill(21);
		SC.Mon_Explo(base.transform.position);
		bool isFlip = false;
		if (base.transform.position.x < Player.transform.position.x)
		{
			isFlip = true;
			Pos_Root.transform.localScale = new global::UnityEngine.Vector3(-1f, 1f, 1f);
		}
		if (pos_explo_1 != null)
		{
			Make_Explo(pos_explo_1, isFlip);
		}
		if (pos_explo_2 != null)
		{
			Make_Explo(pos_explo_2, isFlip);
		}
		if (pos_explo_3 != null)
		{
			Make_Explo(pos_explo_3, isFlip);
		}
		SC.Mon_1_Death(base.transform.position);
		global::UnityEngine.Object.Destroy(base.gameObject);
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

	private void Make_Blood()
	{
		for (int i = 0; i < GM.Bonus_Blood * 3; i++)
		{
			global::UnityEngine.Vector3 position = new global::UnityEngine.Vector3(base.transform.position.x + (float)global::UnityEngine.Random.Range(-200, 200) * 0.01f, base.transform.position.y + (float)global::UnityEngine.Random.Range(-200, 200) * 0.01f, 0f);
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Blood_Obj, position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
		}
	}

	private void Set_AttackDelay()
	{
		Attack_Delay = 1f;
	}

	private void OnTriggerStay2D(global::UnityEngine.Collider2D col)
	{
		if (GM.Paused || GM.onEvent || GM.onHscene || GM.onDown || GM.onGatePass || GM.onGameClear || isDeath)
		{
			return;
		}
		if (State == AI_Mon_FaceHugger.AniState.Attack && !GM.onShield && col.name == "Ani" && !(global::UnityEngine.GameObject.Find("Border_Rolling").GetComponent<global::UnityEngine.SpriteRenderer>().color.a > 0.05f))
		{
			if (PC.Jump_Num == 2)
			{
				Death();
			}
			else if (!GM.GameOver && PC_Body_Delay <= 0f)
			{
				PC_Body_Delay = 1f;
				if (GM.onCloth)
				{
					GM.onCloth = false;
					PC.OnOff_Cloth();
				}
				if (base.transform.position.x > col.transform.parent.position.x)
				{
					GM.Damage(Damage, 0f - DmgForce, false, 21);
				}
				else
				{
					GM.Damage(Damage, DmgForce, false, 21);
				}
				Set_AttackDelay();
			}
		}
		if (col.tag == "Magic_Explo")
		{
			Death();
		}
		else if (col.tag == "Magic_Smog")
		{
			Poison_Smog_Timer = 0.1f;
			Poison_Skill_Timer = 0.2f;
			if (!onPoisonSkill)
			{
				onPoisonSkill = true;
				On_MagicPoison();
			}
		}
		else if (col.tag == "Magic_Shield")
		{
			Death();
		}
		else if (col.tag == "Magic_Fire")
		{
			switch (col.name.Substring(0, 11))
			{
			case "MagicFire_1":
			case "MagicFire_5":
				Death();
				break;
			case "MagicFire_3":
				col.gameObject.SendMessage("Explo");
				Death();
				break;
			}
		}
		else if (col.tag == "Col_PC_Atk" || col.tag == "Trap_Laser")
		{
			Death();
		}
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

	private void OnOff_Object(bool isOnOff)
	{
		if (isOnOff)
		{
			for (int i = 0; i < Object_Body.Length; i++)
			{
				Object_Body[i].GetComponent<global::UnityEngine.SkinnedMeshRenderer>().enabled = false;
			}
			for (int j = 0; j < Object_Fly.Length; j++)
			{
				Object_Fly[j].GetComponent<global::UnityEngine.SkinnedMeshRenderer>().enabled = true;
			}
		}
		else
		{
			for (int k = 0; k < Object_Body.Length; k++)
			{
				Object_Body[k].GetComponent<global::UnityEngine.SkinnedMeshRenderer>().enabled = false;
			}
			for (int l = 0; l < Object_Fly.Length; l++)
			{
				Object_Fly[l].GetComponent<global::UnityEngine.SkinnedMeshRenderer>().enabled = false;
			}
		}
	}

	private void OnOff_Attack(bool isOnOff)
	{
		if (Object_Body.Length > 0)
		{
			if (!isOnOff)
			{
				Object_Body[0].GetComponent<global::UnityEngine.SkinnedMeshRenderer>().enabled = !onTail_Up;
				Object_Body[1].GetComponent<global::UnityEngine.SkinnedMeshRenderer>().enabled = onTail_Up;
			}
			else
			{
				Object_Body[0].GetComponent<global::UnityEngine.SkinnedMeshRenderer>().enabled = !isOnOff;
				Object_Body[1].GetComponent<global::UnityEngine.SkinnedMeshRenderer>().enabled = !isOnOff;
			}
			for (int i = 2; i < Object_Body.Length; i++)
			{
				Object_Body[i].GetComponent<global::UnityEngine.SkinnedMeshRenderer>().enabled = !isOnOff;
			}
		}
		if (Object_Fly.Length > 0)
		{
			for (int j = 0; j < Object_Fly.Length; j++)
			{
				Object_Fly[j].GetComponent<global::UnityEngine.SkinnedMeshRenderer>().enabled = isOnOff;
			}
		}
	}

	private void On_MagicSlow()
	{
		if (Object_Body.Length > 0)
		{
			for (int i = 0; i < Object_Body.Length; i++)
			{
				Object_Body[i].GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material.color = color_Slow;
			}
		}
		if (Object_Fly.Length > 0)
		{
			for (int j = 0; j < Object_Fly.Length; j++)
			{
				Object_Fly[j].GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material.color = color_Slow;
			}
		}
	}

	private void On_MagicPoison()
	{
		if (Object_Body.Length > 0)
		{
			for (int i = 0; i < Object_Body.Length; i++)
			{
				Object_Body[i].GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material.color = color_Poison;
			}
		}
		if (Object_Fly.Length > 0)
		{
			for (int j = 0; j < Object_Fly.Length; j++)
			{
				Object_Fly[j].GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material.color = color_Poison;
			}
		}
	}

	private void Off_Magic()
	{
		if (Object_Body.Length > 0)
		{
			for (int i = 0; i < Object_Body.Length; i++)
			{
				Object_Body[i].GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material.color = color_Orig;
			}
		}
		if (Object_Fly.Length > 0)
		{
			for (int j = 0; j < Object_Fly.Length; j++)
			{
				Object_Fly[j].GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material.color = color_Orig;
			}
		}
	}

	private void Raycasting()
	{
		Range_Attack = (((bool)global::UnityEngine.Physics2D.Linecast(pos_L.position, pos_Stand_1.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player")) || (bool)global::UnityEngine.Physics2D.Linecast(pos_L.position, pos_Stand_2.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player"))) ? true : false);
		Range_Grab = global::UnityEngine.Physics2D.Linecast(pos_L.position, pos_Down.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player"));
		isStuck_Front = global::UnityEngine.Physics2D.Linecast(pos_L.position, pos_Front.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
		onGrounded = (((bool)global::UnityEngine.Physics2D.Linecast(pos_L.position, pos_Bottom_L.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground")) || (bool)global::UnityEngine.Physics2D.Linecast(pos_R.position, pos_Bottom_R.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"))) ? true : false);
		distance = global::UnityEngine.Vector3.Distance(new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f, 0f), base.transform.position);
		dist_DownCenter = global::UnityEngine.Vector3.Distance(global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position, base.transform.position);
		dist_X = global::UnityEngine.Mathf.Abs(Player.transform.position.x - base.transform.position.x);
		dist_Y = global::UnityEngine.Mathf.Abs(Player.transform.position.y - (base.transform.position.y - 0.329f));
	}
}
