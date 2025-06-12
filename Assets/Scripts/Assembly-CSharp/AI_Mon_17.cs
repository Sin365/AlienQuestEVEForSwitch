using UnityEngine;

public class AI_Mon_17 : global::UnityEngine.MonoBehaviour
{
	private float Life_Timer;

	private int facingRight = -1;

	private float distance;

	private float Move_Speed = 5f;

	private float Speed_Orig = 1f;

	private bool onAttack;

	private float Attack_Delay;

	private bool onExplo;

	private float Explo_Timer;

	private float pos_Y;

	private float angle_Z;

	private int Patrol_State;

	private float Patrol_Idle_Timer;

	private float Patrol_Move_Timer;

	private float Patrol_Pos_X;

	private float Patrol_Range;

	private global::UnityEngine.Vector3 pos_Orig;

	private global::UnityEngine.Vector3 Pos_Target;

	private global::UnityEngine.Vector2 Rnd_XY;

	public global::UnityEngine.GameObject _Explo;

	public global::UnityEngine.GameObject Sound_Bomb;

	public global::UnityEngine.GameObject Sound_Explo;

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
		Pos_Target = new global::UnityEngine.Vector3(Player.transform.position.x + 3f, Player.transform.position.y + 7.3f, 0f);
		Rnd_XY = new global::UnityEngine.Vector2((float)global::UnityEngine.Random.Range(0, 30) * 0.01f, (float)global::UnityEngine.Random.Range(0, 30) * 0.01f);
		pos_Orig = base.transform.position;
		pos_Y = base.transform.position.y;
		Speed_Orig = 2.5f + global::UnityEngine.Random.Range(0f, 1f);
		Patrol_Range = 12f + global::UnityEngine.Random.Range(0f, 2f);
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
		if (PC.State == Player_Control.AniState.Sit || PC.State == Player_Control.AniState.Slide)
		{
			Pos_Target = new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2f, 0f);
		}
		else
		{
			Pos_Target = new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 4f, 0f);
		}
		distance = global::UnityEngine.Vector3.Distance(base.transform.position, Pos_Target);
		if (onExplo)
		{
			Explo_Timer += global::UnityEngine.Time.deltaTime;
			if (Explo_Timer > 1.25f)
			{
				global::UnityEngine.Vector3 position = new global::UnityEngine.Vector3(base.transform.position.x, base.transform.position.y, 0f);
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Explo, position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
				global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(Sound_Explo, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
		if (distance > 20f)
		{
			Reset_Attack();
			return;
		}
		if (GM.GameOver || GM.onHscene)
		{
			Patrol_Pos_X = global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x;
			distance = global::UnityEngine.Vector3.Distance(base.transform.position, new global::UnityEngine.Vector3(Patrol_Pos_X, Player.transform.position.y + 7f - Rnd_XY.y * 3f, 0f));
			if (Patrol_State == 1)
			{
				Patrol_Move_Timer += global::UnityEngine.Time.deltaTime;
				if (Patrol_Move_Timer > 1f && distance > Patrol_Range)
				{
					Patrol_State = 0;
					Patrol_Idle_Timer = 0f;
				}
				Pos_Target = new global::UnityEngine.Vector3(Patrol_Pos_X + (Patrol_Range + 2f) * (float)facingRight, Player.transform.position.y + 7f - Rnd_XY.y * 3f, 0f);
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
		if (onAttack && distance > 9f)
		{
			Reset_Attack();
		}
		else if (!onExplo && distance < 3f)
		{
			Set_Explo();
		}
		else if (!onAttack && distance < 9f)
		{
			Set_Attack();
		}
		if (GetComponent<global::UnityEngine.Animator>().GetBool("onHit"))
		{
			Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, 0f, global::UnityEngine.Time.deltaTime * 20f);
		}
		else if (onExplo)
		{
			Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, 4f, global::UnityEngine.Time.deltaTime * 1f);
		}
		else if (onAttack)
		{
			Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, 8f, global::UnityEngine.Time.deltaTime * 4f);
		}
		else
		{
			Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, Speed_Orig, global::UnityEngine.Time.deltaTime * 3f);
		}
		base.transform.position = global::UnityEngine.Vector3.MoveTowards(base.transform.position, Pos_Target, global::UnityEngine.Time.deltaTime * Move_Speed * GetComponent<Monster>().Move_Speed);
	}

	private void Flip()
	{
		facingRight = -facingRight;
		GetComponent<Monster>().Flip_Pos();
	}

	private void Set_Attack()
	{
		onAttack = true;
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", true);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onExplo", false);
	}

	private void Set_Explo()
	{
		onExplo = true;
		Explo_Timer = 0f;
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Sound_Bomb, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onExplo", true);
		GetComponent<Monster>().SendMessage("Set_LockHit");
	}

	private void Reset_Attack()
	{
		onAttack = false;
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onExplo", false);
	}

	private void Set_AttackDelay()
	{
	}
}
