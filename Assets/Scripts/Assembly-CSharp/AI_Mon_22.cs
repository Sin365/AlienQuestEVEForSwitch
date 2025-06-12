using UnityEngine;

public class AI_Mon_22 : global::UnityEngine.MonoBehaviour
{
	private float Life_Timer;

	private float onCam_Timer;

	private int facingRight = -1;

	private float distance;

	private float dist_X;

	private float dist_H;

	private float Move_Speed = 5f;

	private float Speed_Orig = 1f;

	private bool onAttack_Range;

	private bool onAttack;

	private float Attack_Timer;

	private float Attack_Delay;

	private bool onHit;

	private float Hit_Timer;

	private int Patrol_State;

	private float Patrol_Idle_Timer;

	private float Patrol_Move_Timer;

	private float Patrol_Range;

	private global::UnityEngine.Vector3 pos_Orig;

	private global::UnityEngine.Vector3 pos_Prev;

	private global::UnityEngine.Vector3 Pos_Target;

	private global::UnityEngine.Vector2 Rnd_XY;

	private global::UnityEngine.Vector2 GameOver_XY;

	private bool on_Hscene;

	private float H_Timer;

	public global::UnityEngine.GameObject H_Single;

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
		Pos_Target = base.transform.position;
		Rnd_XY = new global::UnityEngine.Vector2((float)global::UnityEngine.Random.Range(-15, 15) * 0.01f, (float)global::UnityEngine.Random.Range(-15, 15) * 0.01f);
		GameOver_XY = new global::UnityEngine.Vector2((float)global::UnityEngine.Random.Range(50, 80) * 0.1f, global::UnityEngine.Random.Range(-0.5f, 0.5f));
		pos_Orig = base.transform.position;
		Speed_Orig = 10f + global::UnityEngine.Random.Range(0f, 1f);
		Patrol_Range = 12f + global::UnityEngine.Random.Range(0f, 2f);
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
		if (Attack_Delay > 0f)
		{
			Attack_Delay -= global::UnityEngine.Time.deltaTime;
		}
		if (Hit_Timer > 0f)
		{
			Hit_Timer -= global::UnityEngine.Time.deltaTime;
		}
		else if (onHit)
		{
			onHit = false;
		}
		distance = global::UnityEngine.Vector3.Distance(base.transform.position, new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f, 0f));
		dist_X = global::UnityEngine.Mathf.Abs(base.transform.position.x - Player.transform.position.x);
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
			if ((facingRight > 0 && base.transform.position.x > global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x) || (facingRight < 0 && base.transform.position.x < global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x))
			{
				Flip();
			}
			base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, Pos_Target, global::UnityEngine.Time.deltaTime * (0.5f + (float)GetComponent<Monster>().Gameover_Num * 0.2f));
		}
		else if (distance < 45f && (GM.GameOver || GM.onHscene))
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
				dist_H = global::UnityEngine.Vector3.Distance(base.transform.position, new global::UnityEngine.Vector3(global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x, Player.transform.position.y + 1.8f, 0f));
				if (dist_H < 1.5f)
				{
					Start_Hscene();
					return;
				}
				Pos_Target = new global::UnityEngine.Vector3(global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x, Player.transform.position.y + 1.8f, 0f);
				Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, Speed_Orig, global::UnityEngine.Time.deltaTime * 2f);
				base.transform.position = global::UnityEngine.Vector3.MoveTowards(base.transform.position, Pos_Target, global::UnityEngine.Time.deltaTime * Move_Speed * GetComponent<Monster>().Move_Speed);
			}
			else if (Patrol_State == 1)
			{
				Patrol_Move_Timer += global::UnityEngine.Time.deltaTime;
				if (Patrol_Move_Timer > 1f && global::UnityEngine.Mathf.Abs(global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x - base.transform.position.x) > Patrol_Range + Rnd_XY.x)
				{
					Patrol_State = 0;
					Patrol_Idle_Timer = 0f;
				}
				Pos_Target = new global::UnityEngine.Vector3(global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x + (Patrol_Range + 10f) * (float)facingRight, Player.transform.position.y + 6.5f - Rnd_XY.y * 3f, 0f);
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
		}
		else if (distance > 20f)
		{
			Reset_Attack_Range();
			Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, 0f, global::UnityEngine.Time.deltaTime * 5f);
			base.transform.position = global::UnityEngine.Vector3.MoveTowards(base.transform.position, Pos_Target, global::UnityEngine.Time.deltaTime * Move_Speed * GetComponent<Monster>().Move_Speed);
		}
		else if (onHit)
		{
			base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, Pos_Target, global::UnityEngine.Time.deltaTime * Move_Speed * GetComponent<Monster>().Move_Speed);
		}
		else if (onAttack)
		{
			if (Check_H_Position())
			{
				Start_Hscene();
			}
			else if (PC.State == Player_Control.AniState.Damage || Attack_Delay < 1f)
			{
				onAttack = false;
				Pos_Target = pos_Prev;
				if (Attack_Delay < 1f)
				{
					Pos_Target = base.transform.position;
				}
			}
			base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, Pos_Target, global::UnityEngine.Time.deltaTime * 3f * GetComponent<Monster>().Move_Speed);
		}
		else if (Attack_Delay > 0f)
		{
			Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, 0f, global::UnityEngine.Time.deltaTime * 3f);
			base.transform.position = global::UnityEngine.Vector3.MoveTowards(base.transform.position, Pos_Target, global::UnityEngine.Time.deltaTime * Move_Speed * GetComponent<Monster>().Move_Speed);
		}
		else
		{
			if (facingRight > 0 && base.transform.position.x > Player.transform.position.x)
			{
				Flip();
			}
			else if (facingRight < 0 && base.transform.position.x < Player.transform.position.x)
			{
				Flip();
			}
			if (distance <= 10f && !onAttack_Range)
			{
				Set_Attack_Range();
			}
			else if (distance > 10f && onAttack_Range)
			{
				Reset_Attack_Range();
			}
			Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, Speed_Orig, global::UnityEngine.Time.deltaTime * 1f);
			Pos_Target = new global::UnityEngine.Vector3(Player.transform.position.x + Rnd_XY.x * 10f, Player.transform.position.y + 7.5f, 0f);
			base.transform.position = global::UnityEngine.Vector3.MoveTowards(base.transform.position, Pos_Target, global::UnityEngine.Time.deltaTime * Move_Speed * GetComponent<Monster>().Move_Speed);
			if (base.transform.position.y > Player.transform.position.y + 5f && global::UnityEngine.Vector3.Distance(base.transform.position, Player.transform.position) < 9f)
			{
				Set_Attack();
			}
		}
	}

	private void Flip()
	{
		facingRight = -facingRight;
		GetComponent<Monster>().Flip_Pos();
	}

	private void Set_Attack()
	{
		onAttack = true;
		Attack_Delay = 2f;
		pos_Prev = base.transform.position;
		Pos_Target = new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2f, 0f);
	}

	private void Set_Attack_Range()
	{
		onAttack_Range = true;
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onRange", true);
	}

	private void Reset_Attack_Range()
	{
		onAttack_Range = false;
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onRange", false);
	}

	private bool Check_H_Position()
	{
		if (dist_X < 2.3f && base.transform.position.y < Player.transform.position.y + 6.8f && base.transform.position.y > Player.transform.position.y && !GM.onCloth && GM.Hscene_Num == 0 && GM.Hscene_Timer <= 0f && GM.Option_Int[3] == 1 && PC.grounded_Now && (PC.State.ToString() == "Idle" || PC.State.ToString() == "Run" || PC.State.ToString() == "Sit" || PC.State.ToString() == "Down"))
		{
			return true;
		}
		return false;
	}

	private void Hit_Avoid()
	{
		onHit = true;
		Hit_Timer = 1.2f;
		Move_Speed = 5f;
		float num = ((!(dist_X < 6f)) ? 0f : (6f - dist_X));
		num += 1.5f;
		if (Player.transform.position.x > base.transform.position.x)
		{
			Pos_Target = new global::UnityEngine.Vector3(base.transform.position.x - num, base.transform.position.y, 0f);
		}
		else
		{
			Pos_Target = new global::UnityEngine.Vector3(base.transform.position.x + num, base.transform.position.y, 0f);
		}
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

	private void Start_Hscene()
	{
		GetComponent<global::UnityEngine.CircleCollider2D>().enabled = false;
		on_Hscene = true;
		GM.onEvent = true;
		GM.onHscene = true;
		GM.Hscene_Num = 25;
		GM.Hscene_Timer = 1f;
		H_Timer = 4f;
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(H_Single, Player.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
		gameObject.transform.parent = base.transform.parent;
		if (facingRight > 0)
		{
			gameObject.SendMessage("Flip");
		}
		gameObject.GetComponent<H_Ani>().Mon_Object = base.gameObject;
		GetComponent<Mon_Index>().OnOff_Object(false);
		GetComponent<global::UnityEngine.Animator>().speed = 0f;
		GetComponent<global::UnityEngine.Rigidbody2D>().Sleep();
		base.transform.position = new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 4f, 0f);
		gameObject.transform.position = base.transform.position;
		GetComponent<Monster>().isInvincible = true;
		global::UnityEngine.GameObject.Find("Menu").GetComponent<Menu_Control>().H_Object = gameObject;
		global::UnityEngine.GameObject.Find("Main Camera").SendMessage("Hscene_Zoom");
		if (!GM.GameOver)
		{
			Player.SendMessage("H_Down");
		}
		global::UnityEngine.GameObject.Find("Ani").SendMessage("Start_H_Scene");
	}

	private void End_Hscene()
	{
		GetComponent<global::UnityEngine.CircleCollider2D>().enabled = true;
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
		Pos_Target = new global::UnityEngine.Vector3(Player.transform.position.x + GameOver_XY.x * (float)(-facingRight), base.transform.position.y + GameOver_XY.y, 0f);
	}
}
