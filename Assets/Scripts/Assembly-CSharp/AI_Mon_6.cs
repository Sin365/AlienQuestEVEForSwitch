using UnityEngine;

public class AI_Mon_6 : global::UnityEngine.MonoBehaviour
{
	private int EnemyState;

	private float Life_Timer;

	private int facingRight = -1;

	private float distance;

	private int Patrol_State;

	private float Patrol_Idle_Timer;

	private float Patrol_Move_Timer;

	private float Rush_Timer;

	private bool isStuck_Front;

	private bool isStuck_Back;

	private bool Range_Chase;

	private bool Range_Attack;

	private float Attack_Delay;

	private float Flip_Delay;

	private float rnd_X;

	public global::UnityEngine.GameObject Tr_Pos;

	public global::UnityEngine.Transform Tr_1_Start;

	public global::UnityEngine.Transform Tr_1_End;

	public global::UnityEngine.Transform Tr_2_Start;

	public global::UnityEngine.Transform Tr_2_End;

	public global::UnityEngine.Transform Tr_4_Start;

	public global::UnityEngine.Transform Tr_4_End;

	public global::UnityEngine.Transform Tr_5_Start;

	public global::UnityEngine.Transform Tr_5_End;
    Player_Control PC => GameManager.instance.PC;
    GameObject Player => GameManager.instance.gobj_Player;

    GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//Player = global::UnityEngine.GameObject.Find("Player");
		rnd_X = (float)global::UnityEngine.Random.Range(0, 200) * 0.01f;
		float num = (float)global::UnityEngine.Random.Range(-70, 70) * 0.01f;
		Tr_1_End.position = new global::UnityEngine.Vector3(Tr_1_End.position.x + num, Tr_1_End.position.y, 0f);
		Tr_2_End.position = new global::UnityEngine.Vector3(Tr_2_End.position.x + num, Tr_2_End.position.y, 0f);
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
		if (EnemyState == 0 || GetComponent<global::UnityEngine.Animator>().GetBool("onHit"))
		{
			return;
		}
		if (Flip_Delay > 0f)
		{
			Flip_Delay -= global::UnityEngine.Time.deltaTime;
			Check_Idle();
		}
		else if (Rush_Timer > 0f)
		{
			Rush_Timer -= global::UnityEngine.Time.deltaTime;
			base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * 15f * facingRight * GetComponent<Monster>().Move_Speed);
			if (!GetComponent<global::UnityEngine.Animator>().GetBool("onMove"))
			{
				Set_Move();
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
				Set_Rush();
				return;
			}
			if (isStuck_Front || global::UnityEngine.Mathf.Abs(Player.transform.position.x - base.transform.position.x) < 2f)
			{
				Check_Idle();
				return;
			}
			if (GM.GameOver && GetComponent<Monster>().Gameover_Num == 0 && global::UnityEngine.Mathf.Abs(Player.transform.position.x - base.transform.position.x) < 6f + rnd_X)
			{
				Check_Idle();
				return;
			}
			if (!GetComponent<global::UnityEngine.Animator>().GetBool("onMove"))
			{
				Set_Move();
			}
			base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * 8f * facingRight * GetComponent<Monster>().Move_Speed);
		}
	}

	private void Flip()
	{
		facingRight = -facingRight;
		GetComponent<Monster>().Flip();
		Flip_Delay = 0.8f + (float)global::UnityEngine.Random.Range(0, 80) * 0.01f;
		Tr_Pos.transform.localScale = new global::UnityEngine.Vector3(-facingRight, 1f, 1f);
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

	private void Set_Move()
	{
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onMove", true);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
	}

	private void Set_AttackDelay()
	{
	}

	private void Set_Rush()
	{
		Rush_Timer = 1.2f;
	}

	private void Raycasting()
	{
		bool flag = global::UnityEngine.Physics2D.Linecast(Tr_1_Start.position, Tr_1_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player"));
		bool flag2 = global::UnityEngine.Physics2D.Linecast(Tr_2_Start.position, Tr_2_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player"));
		bool flag3 = global::UnityEngine.Physics2D.Linecast(Tr_4_Start.position, Tr_4_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
		bool flag4 = global::UnityEngine.Physics2D.Linecast(Tr_5_Start.position, Tr_5_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
		distance = global::UnityEngine.Vector3.Distance(new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f, Player.transform.position.z), base.transform.position);
		if (distance > 45f)
		{
			EnemyState = 0;
			return;
		}
		if (flag3)
		{
			isStuck_Front = true;
		}
		else
		{
			isStuck_Front = false;
		}
		if (flag4)
		{
			isStuck_Back = true;
		}
		else
		{
			isStuck_Back = false;
		}
		if (global::UnityEngine.Mathf.Abs(base.transform.position.x - Player.transform.position.x) < 18f)
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
		if (flag || flag2)
		{
			Range_Attack = true;
		}
		else
		{
			Range_Attack = false;
		}
	}
}
