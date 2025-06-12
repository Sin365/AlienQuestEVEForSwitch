using UnityEngine;

public class AI_Mon_30 : global::UnityEngine.MonoBehaviour
{
	private int EnemyState = 1;

	private float Life_Timer;

	private int facingRight = -1;

	private float distance;

	private int Patrol_State;

	private float Patrol_Idle_Timer;

	private float Patrol_Move_Timer;

	private bool isStuck_Front;

	private bool isStuck_Back;

	private bool Range_Attack_Top;

	private bool Range_Attack_Bot;

	private bool isRunForce;

	private float Attack_Delay;

	private float Flip_Delay;

	private float rnd_X;

	public global::UnityEngine.GameObject Col_Blade;

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

    Player_Control PC => GameManager.instance.PC;
    GameObject Player => GameManager.instance.gobj_Player;

    GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//Player = global::UnityEngine.GameObject.Find("Player");
		rnd_X = (float)global::UnityEngine.Random.Range(0, 300) * 0.01f;
		float num = (float)global::UnityEngine.Random.Range(-80, 80) * 0.01f;
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
		if (EnemyState == 0)
		{
			return;
		}
		if (Flip_Delay > 0f)
		{
			Flip_Delay -= global::UnityEngine.Time.deltaTime;
			Check_Idle();
			if (Attack_Delay > 0f)
			{
				Flip_Delay = 0f;
			}
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
				Flip();
			}
			else if (facingRight < 0 && base.transform.position.x < Player.transform.position.x)
			{
				Flip();
			}
			else if (Range_Attack_Top && !GM.GameOver)
			{
				Set_Attack_Top();
			}
			else if (Range_Attack_Bot && !GM.GameOver)
			{
				Set_Attack_Bot();
			}
			else if (isStuck_Front || global::UnityEngine.Mathf.Abs(Player.transform.position.x - base.transform.position.x) < 2f + rnd_X * 0.5f)
			{
				Check_Idle();
			}
			else if (GM.GameOver && GetComponent<Monster>().Gameover_Num == 0 && global::UnityEngine.Mathf.Abs(Player.transform.position.x - base.transform.position.x) < 8f + rnd_X)
			{
				Check_Idle();
			}
			else if (!GetComponent<global::UnityEngine.Animator>().GetBool("onHit"))
			{
				if (!GetComponent<global::UnityEngine.Animator>().GetBool("onMove"))
				{
					Set_Move();
				}
				base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * 8f * facingRight * GetComponent<Monster>().Move_Speed);
			}
		}
	}

	private void Flip()
	{
		Check_Idle();
		facingRight = -facingRight;
		GetComponent<Monster>().Flip();
		Flip_Delay = 0.5f + (float)global::UnityEngine.Random.Range(0, 80) * 0.01f;
		Tr_Pos.transform.localScale = new global::UnityEngine.Vector3(-facingRight, 1f, 1f);
		float num = (float)global::UnityEngine.Random.Range(-50, 50) * 0.01f;
		Tr_1_End.position = new global::UnityEngine.Vector3(Tr_1_End.position.x + num, Tr_1_End.position.y, 0f);
		Tr_2_End.position = new global::UnityEngine.Vector3(Tr_2_End.position.x + num, Tr_2_End.position.y, 0f);
		Tr_3_End.position = new global::UnityEngine.Vector3(Tr_3_End.position.x + num, Tr_3_End.position.y, 0f);
	}

	private void Check_Idle()
	{
		if (GetComponent<global::UnityEngine.Animator>().GetBool("onAttack_1") || GetComponent<global::UnityEngine.Animator>().GetBool("onAttack_2") || GetComponent<global::UnityEngine.Animator>().GetBool("onMove") || GetComponent<global::UnityEngine.Animator>().GetBool("onHit"))
		{
			Set_Idle();
		}
	}

	private void Set_Idle()
	{
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_1", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_2", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onMove", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
	}

	private void Set_Move()
	{
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_1", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_2", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onMove", true);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
	}

	private void Set_Attack_Top()
	{
		Attack_Delay = 3f;
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_1", true);
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_2", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onMove", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
		GetComponent<Monster>().isLockHit = true;
	}

	private void Set_Attack_Bot()
	{
		Attack_Delay = 3f;
		if (global::UnityEngine.Mathf.Abs(base.transform.position.x - Player.transform.position.x) > 4f)
		{
			base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * 23f * facingRight, global::UnityEngine.ForceMode2D.Impulse);
		}
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_1", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_2", true);
		GetComponent<global::UnityEngine.Animator>().SetBool("onMove", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
		GetComponent<Monster>().isLockHit = true;
	}

	private void End_Attack()
	{
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_1", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_2", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onMove", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
		GetComponent<Monster>().isLockHit = false;
	}

	private void Active_Col_Atk()
	{
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Atk_3(base.transform.position);
		Col_Blade.GetComponent<global::UnityEngine.PolygonCollider2D>().enabled = true;
	}

	private void DeActive_Col_Atk()
	{
		Col_Blade.GetComponent<global::UnityEngine.PolygonCollider2D>().enabled = false;
	}

	private void Set_LockHit()
	{
		GetComponent<Monster>().isLockHit = true;
	}

	private void Reset_LockHit()
	{
		GetComponent<Monster>().isLockHit = false;
	}

	private void Set_AttackDelay()
	{
		Attack_Delay = 1f;
	}

	private void Sound_Mon_Dmg()
	{
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_5_Damage(base.transform.position);
	}

	private void Raycasting()
	{
		global::UnityEngine.Debug.DrawLine(Tr_1_Start.position, Tr_1_End.position, global::UnityEngine.Color.green);
		global::UnityEngine.Debug.DrawLine(Tr_2_Start.position, Tr_2_End.position, global::UnityEngine.Color.yellow);
		global::UnityEngine.Debug.DrawLine(Tr_3_Start.position, Tr_3_End.position, global::UnityEngine.Color.red);
		bool flag = global::UnityEngine.Physics2D.Linecast(Tr_1_Start.position, Tr_1_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player"));
		bool flag2 = global::UnityEngine.Physics2D.Linecast(Tr_2_Start.position, Tr_2_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player"));
		Range_Attack_Bot = global::UnityEngine.Physics2D.Linecast(Tr_3_Start.position, Tr_3_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player"));
		isStuck_Front = global::UnityEngine.Physics2D.Linecast(Tr_4_Start.position, Tr_4_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
		isStuck_Back = global::UnityEngine.Physics2D.Linecast(Tr_5_Start.position, Tr_5_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
		distance = global::UnityEngine.Vector3.Distance(new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f, Player.transform.position.z), base.transform.position);
		if (distance > 50f)
		{
			EnemyState = 0;
			return;
		}
		if (global::UnityEngine.Mathf.Abs(base.transform.position.x - Player.transform.position.x) < 30f)
		{
			Patrol_State = 0;
			Patrol_Idle_Timer = 0f;
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
			Range_Attack_Top = true;
		}
		else
		{
			Range_Attack_Top = false;
		}
	}
}
