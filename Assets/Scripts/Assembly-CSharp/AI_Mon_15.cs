public class AI_Mon_15 : global::UnityEngine.MonoBehaviour
{
	private float Life_Timer;

	private int facingRight = -1;

	private float distance;

	private bool Range_Attack;

	private float Attack_Delay;

	private float Flip_Delay;

	private float Blink_Timer;

	private float IdleFire_Timer;

	private float Fire_Angle;

	private int Fire_Num;

	public global::UnityEngine.GameObject _Fire;

	public global::UnityEngine.Transform pos_Fire;

	private Monster Mon;

	private Player_Control PC;

	private global::UnityEngine.GameObject Player;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		Player = global::UnityEngine.GameObject.Find("Player");
		PC = Player.GetComponent<Player_Control>();
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
		if (Attack_Delay > 0f)
		{
			Attack_Delay -= global::UnityEngine.Time.deltaTime;
		}
		distance = global::UnityEngine.Vector3.Distance(new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f, 0f), new global::UnityEngine.Vector3(base.transform.position.x, base.transform.position.y - 4.392f, 0f));
		if (distance > 17f || GM.GameOver || GM.onHscene || GetComponent<global::UnityEngine.Animator>().GetBool("onAttack") || GetComponent<global::UnityEngine.Animator>().GetBool("onHit"))
		{
			return;
		}
		if (Flip_Delay > 0f)
		{
			Flip_Delay -= global::UnityEngine.Time.deltaTime;
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
		if (IdleFire_Timer > 0f)
		{
			IdleFire_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Blink_Timer > 0f)
		{
			Blink_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Mon.HP_Ratio < 0.5f)
		{
			if (Attack_Delay <= 0f)
			{
				Set_Attack();
			}
			else if (Blink_Timer <= 0f)
			{
				Set_Blink();
			}
		}
		else if (Blink_Timer <= 0f)
		{
			Set_Blink();
		}
		else if (IdleFire_Timer <= 0f)
		{
			Set_IdleFire();
		}
	}

	private void Flip()
	{
		facingRight = -facingRight;
		GetComponent<Monster>().Flip();
	}

	private void Set_Attack()
	{
		Attack_Delay = 3.5f;
		Blink_Timer = 0.5f;
		Fire_Num = 0;
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", true);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
	}

	private void End_Attack()
	{
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
	}

	private void Set_AttackDelay()
	{
		Attack_Delay = 1.5f;
	}

	private void Set_IdleFire()
	{
		IdleFire_Timer = 1f;
		Fire_Num = 0;
		GetComponent<global::UnityEngine.Animator>().SetTrigger("OnFire");
	}

	private void Set_Blink()
	{
		Blink_Timer = 4f;
		GetComponent<global::UnityEngine.Animator>().SetTrigger("OnBlink");
	}

	private void Set_Fire()
	{
		float num = 0f;
		if (Fire_Num == 0)
		{
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, base.transform.rotation) as global::UnityEngine.GameObject;
			global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f);
			global::UnityEngine.Vector3 position = gameObject.transform.position;
			vector.x -= position.x;
			vector.y -= position.y;
			num = global::UnityEngine.Mathf.Atan2(vector.y, vector.x) * 57.29578f;
			gameObject.transform.rotation = global::UnityEngine.Quaternion.Euler(new global::UnityEngine.Vector3(0f, 0f, num + 180f));
			Fire_Num++;
		}
		else
		{
			global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, base.transform.rotation) as global::UnityEngine.GameObject;
			global::UnityEngine.Vector3 vector2 = new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f);
			global::UnityEngine.Vector3 position2 = gameObject2.transform.position;
			vector2.x -= position2.x;
			vector2.y -= position2.y;
			num = global::UnityEngine.Mathf.Atan2(vector2.y, vector2.x) * 57.29578f;
			gameObject2.transform.rotation = global::UnityEngine.Quaternion.Euler(new global::UnityEngine.Vector3(0f, 0f, num + 180f + (float)global::UnityEngine.Random.Range(-10, 10)));
		}
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Boss_4_Fire(pos_Fire.position);
	}
}
