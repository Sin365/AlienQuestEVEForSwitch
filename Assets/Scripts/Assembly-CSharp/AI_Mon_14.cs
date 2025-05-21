public class AI_Mon_14 : global::UnityEngine.MonoBehaviour
{
	private int EnemyState;

	private float Life_Timer;

	private int facingRight = -1;

	private float distance;

	private float dist_X;

	private float dist_Y;

	private float Move_Speed = 8f;

	private bool on_Chase;

	private int Patrol_State;

	private float Patrol_Idle_Timer;

	private float Patrol_Move_Timer;

	private float Patrol_Pos_X;

	private float Patrol_Range;

	private bool isStuck_Front;

	private bool isStuck_Back;

	private bool Range_Attack;

	private bool isRunForce;

	private float Run_Timer;

	private float Attack_Delay;

	private float Lag_Timer;

	private int mon_Index;

	private float Flip_Delay;

	private float rnd_X;

	private bool on_Hscene;

	private float H_Timer;

	private float H_Pursue_Timer;

	private bool on_UncensoredPatch;

	private bool on_PenisWet;

	public global::UnityEngine.GameObject Col_Blade;

	public global::UnityEngine.Transform pos_Blade;

	public global::UnityEngine.GameObject _Lag;

	public global::UnityEngine.GameObject Tr_Pos;

	public global::UnityEngine.Transform Tr_1_Start;

	public global::UnityEngine.Transform Tr_1_End;

	public global::UnityEngine.Transform Tr_2_Start;

	public global::UnityEngine.Transform Tr_2_End;

	public global::UnityEngine.Transform Tr_3_Start;

	public global::UnityEngine.Transform Tr_3_End;

	public global::UnityEngine.Transform Tr_4_Start;

	public global::UnityEngine.Transform Tr_4_End;

	public global::UnityEngine.Transform Tr_5_Start;

	public global::UnityEngine.Transform Tr_5_End;

	private global::UnityEngine.RaycastHit2D whatIHit;

	public global::UnityEngine.GameObject[] H_Single;

	public global::UnityEngine.Sprite spr_Penis;

	public global::UnityEngine.Sprite spr_PenisWet;

	public global::UnityEngine.SpriteRenderer Penis;

	public global::UnityEngine.SpriteRenderer CensoredText;

	public global::UnityEngine.SpriteRenderer CensoredBox;

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
		rnd_X = (float)global::UnityEngine.Random.Range(0, 300) * 0.01f;
		float num = (float)global::UnityEngine.Random.Range(-80, 80) * 0.01f;
		Tr_1_End.position = new global::UnityEngine.Vector3(Tr_1_End.position.x + num, Tr_1_End.position.y, 0f);
		Tr_2_End.position = new global::UnityEngine.Vector3(Tr_2_End.position.x + num, Tr_2_End.position.y, 0f);
		Tr_3_End.position = new global::UnityEngine.Vector3(Tr_3_End.position.x + num, Tr_3_End.position.y, 0f);
		Move_Speed = 10f + global::UnityEngine.Random.Range(0f, 1.5f);
		Patrol_Range = 15f + global::UnityEngine.Random.Range(0f, 6f);
		if (global::UnityEngine.PlayerPrefs.GetInt("UncensoredPatch") == 1)
		{
			Penis.sprite = spr_Penis;
		}
		if (global::UnityEngine.GameObject.Find("Player").transform.position.x > base.transform.position.x)
		{
			Flip();
		}
		mon_Index = GetComponent<Mon_Index>().Index;
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
		if (GetComponent<global::UnityEngine.Animator>().GetBool("onMove"))
		{
			Run_Timer += global::UnityEngine.Time.deltaTime;
		}
		else
		{
			Run_Timer = 0f;
		}
		Raycasting();
		if (H_Timer > 0f)
		{
			H_Timer -= global::UnityEngine.Time.deltaTime;
			if (H_Timer > 3f)
			{
				if (!GetComponent<global::UnityEngine.Animator>().GetBool("onMove"))
				{
					Set_Move();
				}
				base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Move_Speed * 0.4f * -facingRight * Mon.Move_Speed);
			}
			else
			{
				Check_Idle();
			}
		}
		else if (GM.GameOver && distance < 45f)
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
				else if (dist_Y < 1f && (global::UnityEngine.Mathf.Abs(base.transform.position.x - global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x) < 1.3f || (isStuck_Front && global::UnityEngine.Mathf.Abs(base.transform.position.x - global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x) < 2.2f)))
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
					if (!GetComponent<global::UnityEngine.Animator>().GetBool("onMove"))
					{
						Set_Move();
					}
					base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Move_Speed * 1.3f * facingRight * Mon.Move_Speed);
				}
				Patrol_Pos_X = 0f;
				H_Pursue_Timer += global::UnityEngine.Time.deltaTime;
			}
			else
			{
				if (on_Hscene)
				{
					return;
				}
				if (Patrol_Pos_X == 0f)
				{
					Patrol_Pos_X = global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x;
				}
				if (H_Pursue_Timer > 0f)
				{
					Check_Idle();
					Patrol_State = 0;
					Patrol_Idle_Timer = global::UnityEngine.Random.Range(-1f, 1f);
				}
				if (GetComponent<global::UnityEngine.Animator>().GetBool("onMove"))
				{
					Patrol_Move_Timer += global::UnityEngine.Time.deltaTime;
					if (Patrol_Move_Timer > 1f && (isStuck_Front || global::UnityEngine.Mathf.Abs(global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x - base.transform.position.x) > Patrol_Range))
					{
						Check_Idle();
						Patrol_State = 0;
						Patrol_Idle_Timer = 0f;
					}
					base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Move_Speed * 0.5f * facingRight * Mon.Move_Speed);
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
		else
		{
			if (EnemyState == 0)
			{
				return;
			}
			if (GetComponent<global::UnityEngine.Animator>().GetBool("onHit"))
			{
				if (base.GetComponent<UnityEngine.Rigidbody2D>().velocity.x != 0f)
				{
					base.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(0f, base.GetComponent<UnityEngine.Rigidbody2D>().velocity.y);
				}
			}
			else if (Flip_Delay > 0f)
			{
				Flip_Delay -= global::UnityEngine.Time.deltaTime;
				Check_Idle();
			}
			else if (Attack_Delay > 0f)
			{
				Attack_Delay -= global::UnityEngine.Time.deltaTime;
				if (Attack_Delay < 0.92f && Attack_Delay > 0.5f)
				{
					Make_Lag();
				}
			}
			else if (EnemyState == 1)
			{
				Check_Idle();
			}
			else
			{
				if (EnemyState != 2)
				{
					return;
				}
				if (facingRight > 0 && base.transform.position.x > Player.transform.position.x)
				{
					if (global::UnityEngine.Mathf.Abs(Player.transform.position.x - base.transform.position.x) > 1f)
					{
						Flip();
					}
				}
				else if (facingRight < 0 && base.transform.position.x < Player.transform.position.x)
				{
					if (global::UnityEngine.Mathf.Abs(Player.transform.position.x - base.transform.position.x) > 1f)
					{
						Flip();
					}
				}
				else if (Range_Attack)
				{
					if (GM.User_Input_Timer > 1f && !GM.onCloth && GM.Hscene_Num == 0 && GM.Hscene_Timer <= 0f && GM.Option_Int[3] == 1)
					{
						if (dist_X > 2.4f)
						{
							if (!GetComponent<global::UnityEngine.Animator>().GetBool("onMove"))
							{
								Set_Move();
							}
							base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Move_Speed * facingRight * Mon.Move_Speed);
						}
						else
						{
							Attack_Delay = 0.2f;
							if (dist_Y < 1f && PC.grounded_Now && (PC.State.ToString() == "Idle" || PC.State.ToString() == "Run" || PC.State.ToString() == "Sit" || PC.State.ToString() == "Down"))
							{
								Start_Hscene();
							}
							else
							{
								Set_Attack();
							}
						}
					}
					else if (Attack_Delay <= 0f)
					{
						Set_Attack();
					}
				}
				else if (isStuck_Front || global::UnityEngine.Mathf.Abs(Player.transform.position.x - base.transform.position.x) < 2f + rnd_X * 0.5f)
				{
					Check_Idle();
				}
				else
				{
					if (!GetComponent<global::UnityEngine.Animator>().GetBool("onMove"))
					{
						Set_Move();
					}
					base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Move_Speed * facingRight * Mon.Move_Speed);
				}
			}
		}
	}

	private void Flip()
	{
		Check_Idle();
		facingRight = -facingRight;
		Mon.Flip();
		Flip_Delay = 0.5f + (float)global::UnityEngine.Random.Range(0, 80) * 0.01f;
		Tr_Pos.transform.localScale = new global::UnityEngine.Vector3(-facingRight, 1f, 1f);
		CensoredText.transform.localScale = new global::UnityEngine.Vector3(1 * -facingRight, 1f, 1f);
	}

	private void Check_Idle()
	{
		if (GetComponent<global::UnityEngine.Animator>().GetBool("onAttack") || GetComponent<global::UnityEngine.Animator>().GetBool("onAttack_2") || GetComponent<global::UnityEngine.Animator>().GetBool("onMove") || GetComponent<global::UnityEngine.Animator>().GetBool("onHit"))
		{
			Set_Idle();
		}
	}

	private void Set_Idle()
	{
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_2", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onMove", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
	}

	private void Set_Move()
	{
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_2", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onMove", true);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
	}

	private void Set_Attack()
	{
		Attack_Delay = 1f;
		if (Run_Timer > 0.5f)
		{
			isRunForce = true;
		}
		if (global::UnityEngine.Random.Range(0, 10) > 5)
		{
			GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", true);
			GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_2", false);
		}
		else
		{
			GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", false);
			GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_2", true);
		}
		GetComponent<global::UnityEngine.Animator>().SetBool("onMove", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
	}

	private void Attack_Force()
	{
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Atk_3(base.transform.position);
		Col_Blade.GetComponent<global::UnityEngine.BoxCollider2D>().enabled = true;
		if (isRunForce)
		{
			isRunForce = false;
			base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * 35f * facingRight, global::UnityEngine.ForceMode2D.Impulse);
		}
		else
		{
			base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * 20f * facingRight, global::UnityEngine.ForceMode2D.Impulse);
		}
	}

	private void End_Attack()
	{
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_2", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onMove", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
	}

	private void DeActive_Col_Atk()
	{
		Col_Blade.GetComponent<global::UnityEngine.BoxCollider2D>().enabled = false;
	}

	private void Set_AttackDelay()
	{
		if (Attack_Delay < 0.5f)
		{
			Attack_Delay = 0.5f;
		}
	}

	private void Sound_Mon_Dmg()
	{
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_5_Damage(base.transform.position);
	}

	private void Make_Lag()
	{
		Lag_Timer += global::UnityEngine.Time.deltaTime;
		if (Lag_Timer > 0.01f)
		{
			global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3(pos_Blade.position.x, pos_Blade.position.y + 3f, 0f);
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Lag, pos_Blade.position, pos_Blade.rotation) as global::UnityEngine.GameObject;
			gameObject.GetComponent<global::UnityEngine.SpriteRenderer>().sortingOrder = 16 + mon_Index * 20;
			Lag_Timer = 0f;
		}
	}

	private void Start_Hscene()
	{
		on_Hscene = true;
		GM.onEvent = true;
		GM.onHscene = true;
		GM.Hscene_Timer = 1f;
		H_Timer = 4f;
		H_Pursue_Timer = 0f;
		int num = ((global::UnityEngine.Random.Range(0, 10) <= 5) ? 19 : 20);
		GM.Hscene_Num = num;
		global::UnityEngine.Vector3 position = new global::UnityEngine.Vector3(Player.transform.position.x + 1f * (float)(-facingRight), Player.transform.position.y, 0f);
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(H_Single[num - 19], position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
		gameObject.transform.parent = base.transform.parent;
		if (facingRight > 0)
		{
			gameObject.SendMessage("Flip");
		}
		gameObject.GetComponent<H_Ani>().Mon_Object = base.gameObject;
		GetComponent<Mon_Index>().OnOff_Object(false);
		GetComponent<global::UnityEngine.Animator>().speed = 0f;
		GetComponent<global::UnityEngine.Rigidbody2D>().Sleep();
		base.transform.position = new global::UnityEngine.Vector3(Player.transform.position.x + 0.8f * (float)(-facingRight), Player.transform.position.y + 2.528f, 0f);
		GetComponent<Monster>().isInvincible = true;
		global::UnityEngine.GameObject.Find("Menu").GetComponent<Menu_Control>().H_Object = gameObject;
		global::UnityEngine.GameObject.Find("Main Camera").SendMessage("Hscene_Zoom");
		if (!GM.GameOver)
		{
			Player.SendMessage("H_Down");
		}
		global::UnityEngine.GameObject.Find("Ani").SendMessage("Start_H_Scene");
		Check_Idle();
		if (global::UnityEngine.PlayerPrefs.GetInt("UncensoredPatch") == 1)
		{
			Penis.sprite = spr_PenisWet;
		}
	}

	private void End_Hscene()
	{
		on_Hscene = false;
		GM.onEvent = false;
		GM.onHscene = false;
		GM.Hscene_Num = 0;
		GetComponent<Mon_Index>().OnOff_Object(true);
		GetComponent<global::UnityEngine.Animator>().speed = 1f;
		GetComponent<global::UnityEngine.Rigidbody2D>().WakeUp();
		GetComponent<Monster>().isInvincible = false;
		GM.Down_H_After();
		global::UnityEngine.GameObject.Find("Ani").SendMessage("End_H_Scene");
	}

	private void Raycasting()
	{
		bool flag = global::UnityEngine.Physics2D.Linecast(Tr_1_Start.position, Tr_1_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player"));
		bool flag2 = global::UnityEngine.Physics2D.Linecast(Tr_2_Start.position, Tr_2_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player"));
		bool flag3 = global::UnityEngine.Physics2D.Linecast(Tr_3_Start.position, Tr_3_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player"));
		isStuck_Front = global::UnityEngine.Physics2D.Linecast(Tr_4_Start.position, Tr_4_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
		isStuck_Back = global::UnityEngine.Physics2D.Linecast(Tr_5_Start.position, Tr_5_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
		distance = global::UnityEngine.Vector3.Distance(new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f, Player.transform.position.z), base.transform.position);
		dist_X = global::UnityEngine.Mathf.Abs(base.transform.position.x - Player.transform.position.x);
		dist_Y = global::UnityEngine.Mathf.Abs(Player.transform.position.y - (base.transform.position.y - 2.54f));
		if (distance > 45f)
		{
			EnemyState = 0;
		}
		else if (dist_X < 25f)
		{
			if (dist_Y < 6.3f)
			{
				EnemyState = 2;
			}
			else if (on_Chase && global::UnityEngine.Mathf.Abs(base.transform.position.y - Player.transform.position.y) < 13f)
			{
				EnemyState = 2;
			}
			else
			{
				EnemyState = 1;
			}
		}
		else
		{
			EnemyState = 1;
		}
		if (flag || flag2 || flag3 || (dist_X < 5f && EnemyState == 2 && Run_Timer > 0.5f))
		{
			Range_Attack = true;
		}
		else
		{
			Range_Attack = false;
		}
		if (EnemyState == 2)
		{
			on_Chase = true;
		}
	}
}
