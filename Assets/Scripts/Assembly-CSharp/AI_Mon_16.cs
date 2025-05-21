public class AI_Mon_16 : global::UnityEngine.MonoBehaviour
{
	private float Life_Timer;

	private int facingRight = -1;

	private float Move_Speed = 5f;

	private float pos_Y;

	private float angle_Z;

	private int Ani_Speed;

	private global::UnityEngine.Vector3 pos_Orig;

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
		if (Player.transform.position.x > base.transform.position.x)
		{
			Flip();
		}
		pos_Orig = base.transform.position;
		pos_Y = base.transform.position.y;
		Life_Timer = global::UnityEngine.Random.Range(0f, 5f);
		GetComponent<global::UnityEngine.Animator>().speed = 0f;
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			Life_Timer += global::UnityEngine.Time.deltaTime;
			if (Ani_Speed == 0 && Life_Timer * 0.2f > 1f)
			{
				Ani_Speed = 1;
				GetComponent<global::UnityEngine.Animator>().speed = Ani_Speed;
			}
			pos_Y = global::UnityEngine.Mathf.Lerp(pos_Y, pos_Orig.y + global::UnityEngine.Mathf.Sin(Life_Timer * 2f) * 4f, global::UnityEngine.Time.deltaTime * 500f * Mon.Move_Speed);
			base.transform.position = new global::UnityEngine.Vector3(base.transform.position.x + (float)facingRight * global::UnityEngine.Time.deltaTime * 8f * Mon.Move_Speed, pos_Y, 0f);
			angle_Z = global::UnityEngine.Mathf.Sin(Life_Timer * 2f + 1.6f) * 10f * (float)facingRight;
			base.transform.eulerAngles = new global::UnityEngine.Vector3(0f, 0f, global::UnityEngine.Mathf.Lerp(base.transform.eulerAngles.z, angle_Z, global::UnityEngine.Time.deltaTime * 500f));
			if (global::UnityEngine.Vector3.Distance(base.transform.position, pos_Orig) > 100f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	private void Set_AttackDelay()
	{
	}

	private void Flip()
	{
		facingRight = -facingRight;
		GetComponent<Monster>().Flip();
		GetComponent<global::UnityEngine.BoxCollider2D>().center = new global::UnityEngine.Vector2(0.1f * (float)(-facingRight), 0f);
	}
}
