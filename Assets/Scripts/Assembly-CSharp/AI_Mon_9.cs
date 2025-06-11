public class AI_Mon_9 : global::UnityEngine.MonoBehaviour
{
	private int EnemyState;

	private float Life_Timer;

	private int facingRight = -1;

	private float distance;

	private float Attack_Delay;

	private float Flip_Delay;

	private float Jump_Timer;

	public global::UnityEngine.GameObject Tr_Pos;

	public global::UnityEngine.Transform Tr_1_Start;

	public global::UnityEngine.Transform Tr_1_End;

	public global::UnityEngine.Transform Tr_2_Start;

	public global::UnityEngine.Transform Tr_2_End;

	private global::UnityEngine.RaycastHit2D whatIHit;

	private global::UnityEngine.GameObject Player;

	GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		Player = global::UnityEngine.GameObject.Find("Player");
		if (global::UnityEngine.GameObject.Find("Player").transform.position.x > base.transform.position.x)
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
		if (Jump_Timer > 0f)
		{
			Jump_Timer -= global::UnityEngine.Time.deltaTime;
		}
		Raycasting();
		if (EnemyState == 0)
		{
			Check_Idle();
		}
		else if (GetComponent<global::UnityEngine.Animator>().GetBool("onHit"))
		{
			if (GetComponent<global::UnityEngine.Animator>().GetInteger("JumpState") > 0)
			{
				Hit_Force();
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
			if (Attack_Delay > 0f)
			{
				Attack_Delay -= global::UnityEngine.Time.deltaTime;
			}
			if (Attack_Delay <= 0f && !GM.GameOver)
			{
				Set_Attack();
			}
		}
	}

	private void Flip()
	{
		facingRight = -facingRight;
		GetComponent<Monster>().Flip();
		Flip_Delay = 0.3f;
		Tr_Pos.transform.localScale = new global::UnityEngine.Vector3(-facingRight, 1f, 1f);
	}

	private void Check_Idle()
	{
		if (GetComponent<global::UnityEngine.Animator>().GetInteger("JumpState") <= 1)
		{
			Set_Idle();
		}
	}

	private void Set_Idle()
	{
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
		GetComponent<global::UnityEngine.Animator>().SetInteger("JumpState", 0);
	}

	private void Set_Attack()
	{
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
		GetComponent<global::UnityEngine.Animator>().SetInteger("JumpState", 1);
	}

	private void Set_Jump()
	{
		Jump_Timer = 0.3f;
		Attack_Delay = 2f;
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Atk_1(base.transform.position);
		base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.up * 42f * GetComponent<Monster>().Move_Speed, global::UnityEngine.ForceMode2D.Impulse);
		base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * 15f * facingRight * GetComponent<Monster>().Move_Speed, global::UnityEngine.ForceMode2D.Impulse);
		GetComponent<global::UnityEngine.Animator>().SetInteger("JumpState", 2);
	}

	private void Set_AttackDelay()
	{
		Attack_Delay = 1f + (float)global::UnityEngine.Random.Range(-10, 60) * 0.01f;
	}

	private void End_Jump()
	{
		Attack_Delay = 1f + (float)global::UnityEngine.Random.Range(-50, 50) * 0.01f;
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
		GetComponent<global::UnityEngine.Animator>().SetInteger("JumpState", 3);
	}

	private void Hit_Force()
	{
		GetComponent<global::UnityEngine.Animator>().SetInteger("JumpState", 0);
		base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * 12f * -facingRight, global::UnityEngine.ForceMode2D.Impulse);
	}

	private void FreezeJump()
	{
	}

	private void Sound_Mon_Dmg()
	{
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_9_Damage(base.transform.position);
	}

	private void Raycasting()
	{
		global::UnityEngine.Debug.DrawLine(Tr_1_Start.position, Tr_1_End.position, global::UnityEngine.Color.green);
		global::UnityEngine.Debug.DrawLine(Tr_2_Start.position, Tr_2_End.position, global::UnityEngine.Color.yellow);
		bool flag = global::UnityEngine.Physics2D.Linecast(Tr_1_Start.position, Tr_1_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
		bool flag2 = global::UnityEngine.Physics2D.Linecast(Tr_2_Start.position, Tr_2_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
		distance = global::UnityEngine.Vector3.Distance(new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f, Player.transform.position.z), base.transform.position);
		if (distance > 45f)
		{
			EnemyState = 0;
		}
		else if (Jump_Timer <= 0f && GetComponent<global::UnityEngine.Animator>().GetInteger("JumpState") == 2 && (flag || flag2))
		{
			End_Jump();
		}
		else if (global::UnityEngine.Mathf.Abs(base.transform.position.x - Player.transform.position.x) < 25f)
		{
			if (base.transform.position.y + 15f > Player.transform.position.y && base.transform.position.y - 15f < Player.transform.position.y)
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
