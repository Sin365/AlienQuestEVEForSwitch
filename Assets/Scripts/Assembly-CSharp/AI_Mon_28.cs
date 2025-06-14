using UnityEngine;

public class AI_Mon_28 : global::UnityEngine.MonoBehaviour
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

	private bool Range_Attack;

	private float Attack_Delay;

	private float Flip_Delay;

	private float rnd_X;

	public global::UnityEngine.GameObject Fire_4;

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
    Player_Control PC => GameManager.instance?.PC;
    GameObject Player => GameManager.instance?.gobj_Player;

    GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//Player = global::UnityEngine.GameObject.Find("Player");
		rnd_X = (float)global::UnityEngine.Random.Range(0, 300) * 0.01f;
		float num = (float)global::UnityEngine.Random.Range(-50, 50) * 0.01f;
		Tr_1_End.position = new global::UnityEngine.Vector3(Tr_1_End.position.x + num, Tr_1_End.position.y, 0f);
		Tr_2_End.position = new global::UnityEngine.Vector3(Tr_2_End.position.x + num, Tr_2_End.position.y, 0f);
		Tr_3_End.position = new global::UnityEngine.Vector3(Tr_3_End.position.x + num, Tr_3_End.position.y, 0f);
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
		else if (Attack_Delay > 0f)
		{
			Attack_Delay -= global::UnityEngine.Time.deltaTime;
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
				Set_Attack();
				return;
			}
			if (isStuck_Front || global::UnityEngine.Mathf.Abs(Player.transform.position.x - base.transform.position.x) < 1f)
			{
				Check_Idle();
				return;
			}
			if (GM.GameOver && GetComponent<Monster>().Gameover_Num == 0 && global::UnityEngine.Mathf.Abs(Player.transform.position.x - base.transform.position.x) < 11f + rnd_X)
			{
				Check_Idle();
				return;
			}
			if (!GetComponent<global::UnityEngine.Animator>().GetBool("onMove"))
			{
				Set_Move();
			}
			base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * 3f * facingRight * GetComponent<Monster>().Move_Speed);
		}
	}

	private void Flip()
	{
		Check_Idle();
		facingRight = -facingRight;
		GetComponent<Monster>().Flip();
		Flip_Delay = 1f + (float)global::UnityEngine.Random.Range(0, 50) * 0.01f;
		Tr_Pos.transform.localScale = new global::UnityEngine.Vector3(-facingRight, 1f, 1f);
		float num = (float)global::UnityEngine.Random.Range(-50, 50) * 0.01f;
		Tr_1_End.position = new global::UnityEngine.Vector3(Tr_1_End.position.x + num, Tr_1_End.position.y, 0f);
		Tr_2_End.position = new global::UnityEngine.Vector3(Tr_2_End.position.x + num, Tr_2_End.position.y, 0f);
		Tr_3_End.position = new global::UnityEngine.Vector3(Tr_3_End.position.x + num, Tr_3_End.position.y, 0f);
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

	private void Set_Attack()
	{
		Attack_Delay = 3f;
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", true);
		GetComponent<global::UnityEngine.Animator>().SetBool("onMove", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
	}

	private void Attack_Force()
	{
		Attack_Delay = 1f;
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Fire_4, new global::UnityEngine.Vector3(base.transform.position.x + 1.36f * (float)facingRight, base.transform.position.y + 2.96f, 0f), base.transform.rotation) as global::UnityEngine.GameObject;
		if (facingRight > 0)
		{
			gameObject.transform.localScale = new global::UnityEngine.Vector3(0f - gameObject.transform.localScale.x, gameObject.transform.localScale.y, 1f);
		}
		GameManager.instance.sc_Sound_List.Mon_Atk_1(base.transform.position);
	}

	private void End_Attack()
	{
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onMove", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
	}

	private void Set_AttackDelay()
	{
		Attack_Delay = 1f;
	}

	private void Sound_Mon_Dmg()
	{
		GameManager.instance.sc_Sound_List.Mon_8_Damage(base.transform.position);
	}

	private void Raycasting()
	{
		global::UnityEngine.Debug.DrawLine(Tr_1_Start.position, Tr_1_End.position, global::UnityEngine.Color.green);
		global::UnityEngine.Debug.DrawLine(Tr_2_Start.position, Tr_2_End.position, global::UnityEngine.Color.yellow);
		global::UnityEngine.Debug.DrawLine(Tr_3_Start.position, Tr_3_End.position, global::UnityEngine.Color.red);
		global::UnityEngine.Debug.DrawLine(Tr_4_Start.position, Tr_4_End.position, new global::UnityEngine.Color(1f, 0.5f, 0.5f));
		global::UnityEngine.Debug.DrawLine(Tr_5_Start.position, Tr_5_End.position, new global::UnityEngine.Color(1f, 0.5f, 0.5f));
		bool flag = global::UnityEngine.Physics2D.Linecast(Tr_1_Start.position, Tr_1_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player"));
		bool flag2 = global::UnityEngine.Physics2D.Linecast(Tr_2_Start.position, Tr_2_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player"));
		bool flag3 = global::UnityEngine.Physics2D.Linecast(Tr_3_Start.position, Tr_3_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player"));
		bool flag4 = global::UnityEngine.Physics2D.Linecast(Tr_4_Start.position, Tr_4_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
		bool flag5 = global::UnityEngine.Physics2D.Linecast(Tr_5_Start.position, Tr_5_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
		distance = global::UnityEngine.Vector3.Distance(new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f, Player.transform.position.z), base.transform.position);
		if (distance > 45f)
		{
			EnemyState = 0;
			return;
		}
		if (flag4)
		{
			isStuck_Front = true;
		}
		else
		{
			isStuck_Front = false;
		}
		if (flag5)
		{
			isStuck_Back = true;
		}
		else
		{
			isStuck_Back = false;
		}
		if (global::UnityEngine.Mathf.Abs(base.transform.position.x - Player.transform.position.x) < 24f)
		{
			Patrol_State = 0;
			Patrol_Idle_Timer = 0f;
			if (base.transform.position.y + 12f > Player.transform.position.y && base.transform.position.y - 12f < Player.transform.position.y)
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
		if (flag || flag2 || flag3)
		{
			Range_Attack = true;
		}
		else
		{
			Range_Attack = false;
		}
	}
}
