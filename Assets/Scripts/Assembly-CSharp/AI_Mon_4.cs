using UnityEngine;

public class AI_Mon_4 : global::UnityEngine.MonoBehaviour
{
	private int EnemyState;

	private float Life_Timer;

	private int facingRight = -1;

	private float distance;

	private float Move_Speed = 1f;

	private float Speed_Orig = 1f;

	private bool Range_Attack;

	private float Attack_Delay;

	private float Flip_Delay;

	private global::UnityEngine.Vector3 Pos_Target;

	private global::UnityEngine.Vector2 Rnd_XY;

	private global::UnityEngine.Vector2 GameOver_XY;

	private bool isGameOver;

	public global::UnityEngine.GameObject Tr_Pos;

	public global::UnityEngine.Transform Tr_C;

	public global::UnityEngine.Transform Tr_1;

	public global::UnityEngine.Transform Tr_2;

	public global::UnityEngine.Transform Tr_3;

	public global::UnityEngine.Transform Tr_4;

	public global::UnityEngine.Transform Tr_5;

	public global::UnityEngine.Transform Tr_6;

	public global::UnityEngine.Transform Tr_7;

	private global::UnityEngine.RaycastHit2D whatIHit;

    Player_Control PC => GameManager.instance?.PC;
    GameObject Player => GameManager.instance?.gobj_Player;

    GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//Player = global::UnityEngine.GameObject.Find("Player");
		Pos_Target = new global::UnityEngine.Vector3(Player.transform.position.x + 3f, Player.transform.position.y + 7.3f, 0f);
		Rnd_XY = new global::UnityEngine.Vector2((float)global::UnityEngine.Random.Range(0, 30) * 0.01f, (float)global::UnityEngine.Random.Range(0, 30) * 0.01f);
		GameOver_XY = new global::UnityEngine.Vector2((float)global::UnityEngine.Random.Range(50, 80) * 0.1f, (float)global::UnityEngine.Random.Range(60, 82) * 0.1f);
		Speed_Orig = 4f + global::UnityEngine.Random.Range(0f, 1f);
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
		if (GetComponent<global::UnityEngine.Animator>().GetBool("onHit"))
		{
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
			}
			else if (facingRight < 0 && base.transform.position.x < Player.transform.position.x)
			{
				if (global::UnityEngine.Mathf.Abs(Player.transform.position.x - base.transform.position.x) > 1f)
				{
					Flip();
				}
			}
			else if (Range_Attack && !GM.GameOver && !GM.onHscene)
			{
				Set_Attack();
				Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, 0f, global::UnityEngine.Time.deltaTime * 10f);
				base.transform.position = global::UnityEngine.Vector3.MoveTowards(base.transform.position, Pos_Target, global::UnityEngine.Time.deltaTime * Move_Speed * GetComponent<Monster>().Move_Speed);
			}
			else if (GM.GameOver)
			{
				if (GameManager.instance.eg2d_Player.velocity.y == 0f)
				{
					if (!isGameOver)
					{
						isGameOver = true;
						Check_Idle();
						Pos_Target = new global::UnityEngine.Vector3(Player.transform.position.x + GameOver_XY.x * (float)(-facingRight), Player.transform.position.y + GameOver_XY.y, 0f);
					}
					base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, Pos_Target, global::UnityEngine.Time.deltaTime * (0.5f + (float)GetComponent<Monster>().Gameover_Num * 0.2f));
				}
			}
			else
			{
				if (base.transform.position.x < Player.transform.position.x)
				{
					Pos_Target = new global::UnityEngine.Vector3(Player.transform.position.x - 3f + Rnd_XY.x, Player.transform.position.y + 7.3f + Rnd_XY.y, 0f);
				}
				else
				{
					Pos_Target = new global::UnityEngine.Vector3(Player.transform.position.x + 3f + Rnd_XY.x, Player.transform.position.y + 7.3f + Rnd_XY.y, 0f);
				}
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

	private void Set_Attack()
	{
		Attack_Delay = 0.8f;
		GameManager.instance.sc_Sound_List.Mon_Atk_5(base.transform.position);
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", true);
		GetComponent<global::UnityEngine.Animator>().SetBool("onMove", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
	}

	private void End_Attack()
	{
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onMove", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
	}

	private void Set_AttackDelay()
	{
		if (Attack_Delay < 1f)
		{
			Attack_Delay = 1f;
		}
	}

	private void Raycasting()
	{
		global::UnityEngine.Debug.DrawLine(Tr_1.position, Tr_C.position, global::UnityEngine.Color.green);
		global::UnityEngine.Debug.DrawLine(Tr_2.position, Tr_C.position, global::UnityEngine.Color.green);
		global::UnityEngine.Debug.DrawLine(Tr_3.position, Tr_C.position, global::UnityEngine.Color.green);
		global::UnityEngine.Debug.DrawLine(Tr_4.position, Tr_C.position, global::UnityEngine.Color.green);
		global::UnityEngine.Debug.DrawLine(Tr_5.position, Tr_C.position, global::UnityEngine.Color.green);
		global::UnityEngine.Debug.DrawLine(Tr_6.position, Tr_C.position, global::UnityEngine.Color.green);
		global::UnityEngine.Debug.DrawLine(Tr_7.position, Tr_C.position, global::UnityEngine.Color.green);
		bool flag = global::UnityEngine.Physics2D.Linecast(Tr_1.position, Tr_C.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player_Dmg"));
		bool flag2 = global::UnityEngine.Physics2D.Linecast(Tr_2.position, Tr_C.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player_Dmg"));
		bool flag3 = global::UnityEngine.Physics2D.Linecast(Tr_3.position, Tr_C.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player_Dmg"));
		bool flag4 = global::UnityEngine.Physics2D.Linecast(Tr_4.position, Tr_C.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player_Dmg"));
		bool flag5 = global::UnityEngine.Physics2D.Linecast(Tr_5.position, Tr_C.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player_Dmg"));
		bool flag6 = global::UnityEngine.Physics2D.Linecast(Tr_6.position, Tr_C.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player_Dmg"));
		bool flag7 = global::UnityEngine.Physics2D.Linecast(Tr_7.position, Tr_C.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player_Dmg"));
		distance = global::UnityEngine.Vector3.Distance(new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f, 0f), new global::UnityEngine.Vector3(base.transform.position.x, base.transform.position.y - 3f, 0f));
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
		if (flag || flag2 || flag3 || flag4 || flag5 || flag6 || flag7)
		{
			Range_Attack = true;
		}
		else
		{
			Range_Attack = false;
		}
	}
}
