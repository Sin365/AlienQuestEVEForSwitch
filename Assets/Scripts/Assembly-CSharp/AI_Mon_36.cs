public class AI_Mon_36 : global::UnityEngine.MonoBehaviour
{
	private int EnemyState;

	private float Life_Timer;

	private int facingRight = -1;

	private float distance;

	private float dist_Y;

	private bool Range_Attack;

	private float Attack_Delay;

	private float Flip_Delay;

	public global::UnityEngine.GameObject Tr_Pos;

	public global::UnityEngine.Transform Tr_1_Start;

	public global::UnityEngine.Transform Tr_1_End;

	public global::UnityEngine.Transform Tr_2_Start;

	public global::UnityEngine.Transform Tr_2_End;

	public global::UnityEngine.Transform Tr_3_Start;

	public global::UnityEngine.Transform Tr_3_End;

	private global::UnityEngine.RaycastHit2D whatIHit;

	private float Fire_Timer = 1f;

	private int Fire_Num;

	private float FireNum_Timer;

	private float LockHit_Delay;

	public global::UnityEngine.GameObject _Fire;

	public global::UnityEngine.Transform pos_Fire;

	public H_Mon7[] Mon_7;

	private bool is_Mon7_Free;

	private float Mon7_Timer;

	private Monster Mon;

	private global::UnityEngine.GameObject Player;

	GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		Player = global::UnityEngine.GameObject.Find("Player");
		Mon = GetComponent<Monster>();
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
		if (Mon7_Timer > 0f)
		{
			Mon7_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (LockHit_Delay > 0f)
		{
			LockHit_Delay -= global::UnityEngine.Time.deltaTime;
		}
		else if (Mon.isLockHit)
		{
			Mon.isLockHit = false;
		}
		Raycasting();
		if (EnemyState == 0)
		{
			return;
		}
		if (GetComponent<global::UnityEngine.Animator>().GetBool("onHit"))
		{
			if (Attack_Delay > 0f)
			{
				Attack_Delay -= global::UnityEngine.Time.deltaTime;
			}
			else if (Range_Attack && !GM.GameOver)
			{
				Set_Attack();
			}
			if (facingRight > 0 && base.transform.position.x > Player.transform.position.x)
			{
				if (global::UnityEngine.Mathf.Abs(Player.transform.position.x - base.transform.position.x) > 1f)
				{
					Flip();
				}
			}
			else if (facingRight < 0 && base.transform.position.x < Player.transform.position.x && global::UnityEngine.Mathf.Abs(Player.transform.position.x - base.transform.position.x) > 1f)
			{
				Flip();
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
			else if (EnemyState == 2 && !GM.GameOver && !GM.onHscene)
			{
				Set_Attack();
			}
		}
	}

	private void Flip()
	{
		Check_Idle();
		facingRight = -facingRight;
		Mon.Flip();
		Flip_Delay = 0.5f;
		Tr_Pos.transform.localScale = new global::UnityEngine.Vector3(-facingRight, 1f, 1f);
	}

	private void Check_Flip()
	{
		if (facingRight > 0 && base.transform.position.x > Player.transform.position.x)
		{
			if (global::UnityEngine.Mathf.Abs(Player.transform.position.x - base.transform.position.x) > 1f)
			{
				Flip();
			}
		}
		else if (facingRight < 0 && base.transform.position.x < Player.transform.position.x && global::UnityEngine.Mathf.Abs(Player.transform.position.x - base.transform.position.x) > 1f)
		{
			Flip();
		}
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
		Attack_Delay = 2.5f;
		Mon.isLockHit = true;
		LockHit_Delay = 0.7f;
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", true);
		GetComponent<global::UnityEngine.Animator>().SetBool("onMove", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
	}

	private void End_Attack()
	{
		Mon.isLockHit = false;
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onMove", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
	}

	private void Set_AttackDelay()
	{
	}

	private void Sound_Mon_Dmg()
	{
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_5_Damage(base.transform.position);
	}

	private void Set_Fire()
	{
		float num = ((facingRight >= 0) ? 180 : 0);
		global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f);
		global::UnityEngine.Vector3 position = pos_Fire.transform.position;
		vector.x -= position.x;
		vector.y -= position.y;
		num = global::UnityEngine.Mathf.Atan2(vector.y, vector.x) * 57.29578f + 180f;
		num = Check_Fire_Angle(num);
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(0f, 0f, num + 15f)) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(0f, 0f, num)) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(0f, 0f, num - 15f)) as global::UnityEngine.GameObject;
		gameObject2.transform.Translate(global::UnityEngine.Vector3.right * -1f);
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Boss_4_Fire(pos_Fire.position);
	}

	private float Check_Fire_Angle(float angle)
	{
		if (facingRight > 0)
		{
			if (angle > 220f)
			{
				return 220f;
			}
			if (angle < 140f)
			{
				return 140f;
			}
			return angle;
		}
		if (angle < 320f && angle > 180f)
		{
			return 320f;
		}
		if (angle > 40f && angle < 180f)
		{
			return 40f;
		}
		return angle;
	}

	private void Mon_7_Free()
	{
		if (!is_Mon7_Free && Mon_7.Length > 0)
		{
			for (int i = 0; i < Mon_7.Length; i++)
			{
				Mon_7[i].GetComponent<H_Mon7>().Set_Index(i + 10);
				Mon_7[i].SendMessage("Get_Free");
			}
			is_Mon7_Free = true;
		}
	}

	private void Mon_7_CumShot()
	{
		if (Mon7_Timer <= 0f && Mon_7.Length > 0)
		{
			for (int i = 0; i < Mon_7.Length; i++)
			{
				Mon_7[i].SendMessage("CumShot");
			}
			Mon7_Timer = 0.7f;
		}
	}

	private void Mon_7_Death()
	{
		if (Mon_7.Length > 0)
		{
			for (int i = 0; i < Mon_7.Length; i++)
			{
				Mon_7[i].GetComponent<global::UnityEngine.Animator>().SetBool("isDeath", true);
			}
		}
	}

	public void Set_Death()
	{
		Mon_7_Free();
	}

	private void Raycasting()
	{
		bool flag = global::UnityEngine.Physics2D.Linecast(Tr_1_Start.position, Tr_1_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player"));
		bool flag2 = global::UnityEngine.Physics2D.Linecast(Tr_2_Start.position, Tr_2_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player"));
		bool flag3 = global::UnityEngine.Physics2D.Linecast(Tr_3_Start.position, Tr_3_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player"));
		distance = global::UnityEngine.Vector3.Distance(new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f, Player.transform.position.z), base.transform.position);
		dist_Y = Player.transform.position.y - base.transform.position.y;
		if (distance > 45f)
		{
			EnemyState = 0;
			return;
		}
		if (global::UnityEngine.Mathf.Abs(base.transform.position.x - Player.transform.position.x) < 25f)
		{
			if (dist_Y > -10f && dist_Y < 10f)
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
