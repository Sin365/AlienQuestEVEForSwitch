using UnityEngine;

public class AI_Mob_1 : global::UnityEngine.MonoBehaviour
{
	private int EnemyState;

	private float Life_Timer;

	private int facingRight = -1;

	private float distance;

	private float Flip_Delay;

	private float rnd_X;

	private int Patrol_State;

	private float Patrol_Idle_Timer;

	private float Patrol_Move_Timer;

	private float Patrol_Pos_X;

	private float Patrol_Range;

	private bool isStuck_Front;

	public global::UnityEngine.GameObject Leg_Pos;

	public global::UnityEngine.Transform Tr_FrontPos;


    Player_Control PC => GameManager.instance?.PC;
    GameObject Player => GameManager.instance?.gobj_Player;

    //private GameManager GM;
    private GameManager GM => GameManager.instance;
    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//Player = global::UnityEngine.GameObject.Find("Player");
		rnd_X = (float)global::UnityEngine.Random.Range(0, 200) * 0.01f;
		Patrol_Range = 8f + global::UnityEngine.Random.Range(0f, 6f);
		if (Player.transform.position.x > base.transform.position.x)
		{
			Flip();
		}
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
		Raycasting();
		if (GM.GameOver && distance < 45f)
		{
			if (Patrol_Pos_X == 0f)
			{
				Patrol_Pos_X = base.transform.position.x;
			}
			if (Patrol_State > 0)
			{
				Patrol_Move_Timer += global::UnityEngine.Time.deltaTime;
				if (Patrol_Move_Timer > 1f && (isStuck_Front || global::UnityEngine.Mathf.Abs(global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x - base.transform.position.x) > Patrol_Range))
				{
					Check_Idle();
					Patrol_State = 0;
					Patrol_Idle_Timer = 0f;
				}
				base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * 2f * facingRight * GetComponent<Monster>().Move_Speed);
			}
			else
			{
				Patrol_Idle_Timer += global::UnityEngine.Time.deltaTime;
				Check_Idle();
				if (Patrol_Idle_Timer > 2f)
				{
					Flip();
					Patrol_State = 1;
					Patrol_Move_Timer = 0f;
				}
			}
		}
		else
		{
			if (EnemyState == 0 || GetComponent<global::UnityEngine.Animator>().GetBool("onHit"))
			{
				return;
			}
			if (Flip_Delay > 0f)
			{
				Flip_Delay -= global::UnityEngine.Time.deltaTime;
				Check_Idle();
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
				else if (GM.GameOver && GetComponent<Monster>().Gameover_Num == 0 && global::UnityEngine.Mathf.Abs(Player.transform.position.x - base.transform.position.x) < 4.5f + rnd_X)
				{
					Check_Idle();
				}
				else
				{
					base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * 2f * facingRight * GetComponent<Monster>().Move_Speed);
				}
			}
		}
	}

	private void Flip()
	{
		facingRight = -facingRight;
		GetComponent<Monster>().Flip();
		Flip_Delay = 0.8f;
		Leg_Pos.transform.localScale = new global::UnityEngine.Vector3(-facingRight, 1f, 1f);
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
	}

	private void Set_AttackDelay()
	{
	}

	private void Raycasting()
	{
		isStuck_Front = global::UnityEngine.Physics2D.Linecast(base.transform.position, Tr_FrontPos.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
		distance = global::UnityEngine.Vector3.Distance(new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f, 0f), new global::UnityEngine.Vector3(base.transform.position.x, base.transform.position.y - 3f, 0f));
		if (distance > 45f)
		{
			EnemyState = 0;
		}
		else if (global::UnityEngine.Mathf.Abs(base.transform.position.x - Player.transform.position.x) < 18f)
		{
			if (base.transform.position.y + 15f > Player.transform.position.y && base.transform.position.y - 9f < Player.transform.position.y)
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
	}
}
