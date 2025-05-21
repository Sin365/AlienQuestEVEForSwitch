public class AI_Mon_5 : global::UnityEngine.MonoBehaviour
{
	private int EnemyState;

	private float Life_Timer;

	private int facingRight = -1;

	private float distance;

	private float Move_Speed = 1f;

	private float Speed_Orig = 1f;

	private int Patrol_State;

	private float Patrol_Idle_Timer;

	private float Patrol_Move_Timer;

	private float Patrol_Range;

	private bool Range_Attack;

	private float Attack_Delay;

	private float Flip_Delay;

	private float pos_Y = 4f;

	private global::UnityEngine.Vector3 Pos_Target;

	private global::UnityEngine.Vector2 Rnd_XY;

	private global::UnityEngine.Vector2 GameOver_XY;

	private bool on_Hscene;

	private float H_Timer;

	private float Dual_Timer;

	private bool DualPlayable;

	private float PC_Atk_Timer;

	public global::UnityEngine.GameObject Tr_Pos;

	public global::UnityEngine.Transform Tr_1;

	public global::UnityEngine.Transform Tr_1_End;

	public global::UnityEngine.Transform Tr_2;

	public global::UnityEngine.Transform Tr_2_End;

	public global::UnityEngine.Transform Tr_Top;

	public global::UnityEngine.Transform Tr_Bot;

	public global::UnityEngine.GameObject H_Single;

	public global::UnityEngine.GameObject H_Dual;

	private Player_Control PC;

	private global::UnityEngine.GameObject Player;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		Player = global::UnityEngine.GameObject.Find("Player");
		PC = Player.GetComponent<Player_Control>();
		Pos_Target = new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 4f, 0f);
		Rnd_XY = new global::UnityEngine.Vector2((float)global::UnityEngine.Random.Range(0, 30) * 0.01f, (float)global::UnityEngine.Random.Range(0, 30) * -0.01f);
		GameOver_XY = new global::UnityEngine.Vector2((float)global::UnityEngine.Random.Range(50, 80) * 0.1f, (float)global::UnityEngine.Random.Range(60, 82) * 0.1f);
		GetComponent<global::UnityEngine.Animator>().SetBool("isDummy", false);
		Speed_Orig = 6f + global::UnityEngine.Random.Range(0f, 1.5f);
		Patrol_Range = 12f + global::UnityEngine.Random.Range(0f, 6f);
		if (Player.transform.position.x > base.transform.position.x)
		{
			Flip();
		}
		if (global::UnityEngine.Random.Range(0, 10) > 7)
		{
			DualPlayable = true;
		}
	}

	private void Update()
	{
		if (GM.Paused || on_Hscene)
		{
			return;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
		Raycasting();
		if (PC_Atk_Timer > 0f)
		{
			PC_Atk_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (H_Timer > 0f)
		{
			H_Timer -= global::UnityEngine.Time.deltaTime;
			if (GetComponent<global::UnityEngine.Animator>().GetBool("onHit"))
			{
				H_Timer = 0f;
			}
			if ((facingRight > 0 && base.transform.position.x > global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x) || (facingRight < 0 && base.transform.position.x < global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x))
			{
				Flip();
			}
			Pos_Target = new global::UnityEngine.Vector3(Player.transform.position.x + GameOver_XY.x * (float)(-facingRight), Player.transform.position.y + GameOver_XY.y, 0f);
			Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, Speed_Orig, global::UnityEngine.Time.deltaTime * 2f);
			base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, Pos_Target, global::UnityEngine.Time.deltaTime * (0.5f + (float)GetComponent<Monster>().Gameover_Num * 0.2f));
		}
		else if (distance < 45f && (GM.GameOver || GM.onHscene))
		{
			if (DualPlayable && GM.Hscene_Num == 1)
			{
				Pos_Target = new global::UnityEngine.Vector3(global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x, Player.transform.position.y + 2.2f, 0f);
				Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, Speed_Orig, global::UnityEngine.Time.deltaTime * 2f);
				base.transform.position = global::UnityEngine.Vector3.MoveTowards(base.transform.position, Pos_Target, global::UnityEngine.Time.deltaTime * Move_Speed * GetComponent<Monster>().Move_Speed);
				if (distance < 4.2f)
				{
					Dual_Timer += global::UnityEngine.Time.deltaTime;
					if (Dual_Timer > 0.5f && GM.Hscene_Num == 1)
					{
						GM.Hscene_Num = 2;
						Start_H_Dual();
					}
				}
			}
			else if (GM.Hscene_Num == 0 && GM.Hscene_Timer <= 0f)
			{
				Dual_Timer = 0f;
				if (facingRight > 0 && base.transform.position.x > global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x)
				{
					Flip();
				}
				else if (facingRight < 0 && base.transform.position.x < global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x)
				{
					Flip();
				}
				else if (distance < 3f)
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
				else
				{
					Pos_Target = new global::UnityEngine.Vector3(global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x, Player.transform.position.y + 2.2f, 0f);
					Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, Speed_Orig, global::UnityEngine.Time.deltaTime * 2f);
					base.transform.position = global::UnityEngine.Vector3.MoveTowards(base.transform.position, Pos_Target, global::UnityEngine.Time.deltaTime * Move_Speed * GetComponent<Monster>().Move_Speed);
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
				Pos_Target = new global::UnityEngine.Vector3(global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x + (Patrol_Range + 10f) * (float)facingRight, Player.transform.position.y + 5.5f - Rnd_XY.y * 3f, 0f);
				Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, Speed_Orig, global::UnityEngine.Time.deltaTime * 2f);
				base.transform.position = global::UnityEngine.Vector3.MoveTowards(base.transform.position, Pos_Target, global::UnityEngine.Time.deltaTime * Move_Speed * 0.5f * GetComponent<Monster>().Move_Speed);
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
		}
		else
		{
			if (EnemyState == 0)
			{
				return;
			}
			if (GetComponent<global::UnityEngine.Animator>().GetBool("onHit"))
			{
				PC_Atk_Timer = 3f;
				Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, 0f, global::UnityEngine.Time.deltaTime * 20f);
			}
			else if (Flip_Delay > 0f)
			{
				Flip_Delay -= global::UnityEngine.Time.deltaTime;
				Check_Idle();
			}
			else if (Attack_Delay > 0f)
			{
				Attack_Delay -= global::UnityEngine.Time.deltaTime;
				Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, 0f, global::UnityEngine.Time.deltaTime * 10f);
				base.transform.position = global::UnityEngine.Vector3.MoveTowards(base.transform.position, Pos_Target, global::UnityEngine.Time.deltaTime * Move_Speed * GetComponent<Monster>().Move_Speed);
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
					return;
				}
				if (facingRight < 0 && base.transform.position.x < Player.transform.position.x)
				{
					if (global::UnityEngine.Mathf.Abs(Player.transform.position.x - base.transform.position.x) > 1f)
					{
						Flip();
					}
					return;
				}
				if (Range_Attack && !GM.GameOver)
				{
					if (PC_Atk_Timer <= 0f && !GM.onCloth && GM.Hscene_Num == 0 && GM.Hscene_Timer <= 0f && GM.Option_Int[3] == 1)
					{
						Attack_Delay = 0.2f;
						if (PC.grounded_Now && (PC.State.ToString() == "Idle" || PC.State.ToString() == "Run" || PC.State.ToString() == "Sit" || PC.State.ToString() == "Down"))
						{
							Start_H_Single();
						}
						else
						{
							Set_Attack();
						}
					}
					else
					{
						Set_Attack();
					}
				}
				if (PC.State == Player_Control.AniState.Sit || PC.State == Player_Control.AniState.Slide)
				{
					pos_Y = global::UnityEngine.Mathf.Lerp(pos_Y, 1.6f, global::UnityEngine.Time.deltaTime * 1f);
				}
				else
				{
					pos_Y = global::UnityEngine.Mathf.Lerp(pos_Y, 4f, global::UnityEngine.Time.deltaTime * 1f);
				}
				Pos_Target = new global::UnityEngine.Vector3(Player.transform.position.x + Rnd_XY.x, Player.transform.position.y + pos_Y + Rnd_XY.y, 0f);
				Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, Speed_Orig, global::UnityEngine.Time.deltaTime * 2f);
				base.transform.position = global::UnityEngine.Vector3.MoveTowards(base.transform.position, Pos_Target, global::UnityEngine.Time.deltaTime * Move_Speed * GetComponent<Monster>().Move_Speed);
			}
		}
	}

	private void Flip()
	{
		Check_Idle();
		facingRight = -facingRight;
		GetComponent<Monster>().Flip();
		Flip_Delay = 0.3f + (float)global::UnityEngine.Random.Range(0, 50) * 0.01f;
		Tr_Pos.transform.localScale = new global::UnityEngine.Vector3(-facingRight, 1f, 1f);
		GetComponent<global::UnityEngine.CircleCollider2D>().offset = new global::UnityEngine.Vector2(0.15f * (float)facingRight, 0.04f);
	}

	private void Check_Idle()
	{
		if (GetComponent<global::UnityEngine.Animator>().GetBool("onAttack") || GetComponent<global::UnityEngine.Animator>().GetBool("onMove") || GetComponent<global::UnityEngine.Animator>().GetBool("onHit"))
		{
			Set_Idle();
		}
	}

	private void Set_Idle()
	{
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onMove", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("isDummy", false);
	}

	private void Set_Attack()
	{
		Attack_Delay = 0.7f;
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", true);
		GetComponent<global::UnityEngine.Animator>().SetBool("onMove", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
	}

	private void Sound_Attack()
	{
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Atk_1(base.transform.position);
	}

	private void End_Attack()
	{
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onMove", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
	}

	private void Set_AttackDelay()
	{
		Attack_Delay = 0.7f;
	}

	private void Start_H_Single()
	{
		on_Hscene = true;
		GM.onEvent = true;
		GM.onHscene = true;
		GM.Hscene_Num = 1;
		GM.Hscene_Timer = 1f;
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(H_Single, Player.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
		gameObject.transform.parent = base.transform.parent;
		if (facingRight > 0)
		{
			gameObject.SendMessage("Flip");
		}
		gameObject.GetComponent<H_Ani>().Mon_Object = base.gameObject;
		global::UnityEngine.GameObject.Find("Menu").GetComponent<Menu_Control>().H_Object = gameObject;
		Start_Hscene();
		base.transform.position = new global::UnityEngine.Vector3(Player.transform.position.x + 0.4f * (float)facingRight, Player.transform.position.y + 3.2f, 0f);
		GM.GetComponent<H_Control>().facingRight = facingRight;
		GM.GetComponent<H_Control>().H_Object = gameObject;
		GM.GetComponent<H_Control>().Mon_1 = base.gameObject;
	}

	private void Start_H_Dual()
	{
		on_Hscene = true;
		GM.Hscene_Num = 2;
		if (GM.GetComponent<H_Control>().H_Object != null)
		{
			GM.GetComponent<H_Control>().H_Object.SendMessage("Delete_ToDual");
		}
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(H_Dual, Player.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
		gameObject.transform.parent = base.transform.parent;
		gameObject.SendMessage("Start_H8_Dummy");
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
		gameObject.GetComponent<H_Ani>().Mon_Object.transform.position = new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 4.2f, 0f);
		if ((GM.GetComponent<H_Control>().facingRight < 0 && facingRight < 0) || (GM.GetComponent<H_Control>().facingRight > 0 && facingRight > 0))
		{
			Flip();
		}
		Flip_Delay = 0f;
		base.transform.position = new global::UnityEngine.Vector3(Player.transform.position.x - 2.95f * (float)facingRight, Player.transform.position.y + 2.23f, 0f);
	}

	private void Start_Hscene()
	{
		GetComponent<Mon_Index>().OnOff_Object(false);
		H_Timer = 6f;
		Dual_Timer = 0f;
		GetComponent<global::UnityEngine.Animator>().speed = 0f;
		GetComponent<global::UnityEngine.Rigidbody2D>().Sleep();
		GetComponent<Monster>().isInvincible = true;
		if (GM.Hscene_Num != 2)
		{
			global::UnityEngine.GameObject.Find("Main Camera").SendMessage("Hscene_Zoom");
		}
		if (!GM.GameOver)
		{
			Player.SendMessage("H_Down");
		}
		global::UnityEngine.GameObject.Find("Ani").SendMessage("Start_H_Scene");
		Check_Idle();
	}

	private void End_Hscene()
	{
		on_Hscene = false;
		GM.onEvent = false;
		GM.onHscene = false;
		GM.Hscene_Num = 0;
		Dual_Timer = 0f;
		GM.GetComponent<H_Control>().Reset();
		GetComponent<Mon_Index>().OnOff_Object(true);
		GetComponent<global::UnityEngine.Animator>().speed = 1f;
		GetComponent<global::UnityEngine.Rigidbody2D>().WakeUp();
		GetComponent<Monster>().isInvincible = false;
		GM.Down_H_After();
		global::UnityEngine.GameObject.Find("Ani").SendMessage("End_H_Scene");
	}

	private void Raycasting()
	{
		bool flag = global::UnityEngine.Physics2D.Linecast(Tr_1.position, Tr_1_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player_Dmg"));
		bool flag2 = global::UnityEngine.Physics2D.Linecast(Tr_2.position, Tr_2_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player_Dmg"));
		bool flag3 = global::UnityEngine.Physics2D.Linecast(Tr_Top.position, Tr_1_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player_Dmg"));
		bool flag4 = global::UnityEngine.Physics2D.Linecast(Tr_Bot.position, Tr_2_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player_Dmg"));
		if (GM.GameOver)
		{
			distance = global::UnityEngine.Vector3.Distance(new global::UnityEngine.Vector3(global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x, global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.y + 2.2f, 0f), base.transform.position);
		}
		else
		{
			distance = global::UnityEngine.Vector3.Distance(new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 3.2f, 0f), base.transform.position);
		}
		if (distance > 45f)
		{
			EnemyState = 0;
			return;
		}
		if (global::UnityEngine.Mathf.Abs(base.transform.position.x - Player.transform.position.x) < 18f)
		{
			if (base.transform.position.y + 10f > Player.transform.position.y && base.transform.position.y - 18f < Player.transform.position.y)
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
		if (flag || flag2 || flag3 || flag4)
		{
			Range_Attack = true;
		}
		else
		{
			Range_Attack = false;
		}
	}
}
