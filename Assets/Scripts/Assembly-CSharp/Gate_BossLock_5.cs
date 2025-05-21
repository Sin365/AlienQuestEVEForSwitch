public class Gate_BossLock_5 : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.SpriteRenderer Glow_L_1;

	public global::UnityEngine.SpriteRenderer Glow_L_2;

	public global::UnityEngine.SpriteRenderer Lock_Icon_L;

	public global::UnityEngine.SpriteRenderer Vertical_L;

	public global::UnityEngine.SpriteRenderer Glow_R_1;

	public global::UnityEngine.SpriteRenderer Glow_R_2;

	public global::UnityEngine.SpriteRenderer Lock_Icon_R;

	public global::UnityEngine.SpriteRenderer Vertical_R;

	private int State;

	private float Life_Timer;

	private float End_Timer;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		if (GM.Get_Event(3) || GM.Get_Event(15))
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
		global::UnityEngine.SpriteRenderer glow_L_ = Glow_L_1;
		global::UnityEngine.Color color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
		Vertical_L.color = color;
		color = color;
		Lock_Icon_L.color = color;
		color = color;
		Glow_L_2.color = color;
		glow_L_.color = color;
		global::UnityEngine.SpriteRenderer glow_R_ = Glow_R_1;
		color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
		Vertical_R.color = color;
		color = color;
		Lock_Icon_R.color = color;
		color = color;
		Glow_R_2.color = color;
		glow_R_.color = color;
		GetComponent<global::UnityEngine.BoxCollider2D>().enabled = false;
	}

	private void Update()
	{
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (State == 0)
		{
			if (GM.Get_Event(5))
			{
				State = 1;
				GetComponent<global::UnityEngine.BoxCollider2D>().enabled = true;
			}
		}
		else if (State == 1)
		{
			global::UnityEngine.SpriteRenderer glow_L_ = Glow_L_2;
			global::UnityEngine.Color color = global::UnityEngine.Color.Lerp(Glow_R_1.color, new global::UnityEngine.Color(1f, 1f, 1f, 1f), global::UnityEngine.Time.deltaTime * 2.8f);
			Glow_R_1.color = color;
			color = color;
			Glow_R_2.color = color;
			color = color;
			Glow_L_1.color = color;
			glow_L_.color = color;
			global::UnityEngine.SpriteRenderer lock_Icon_L = Lock_Icon_L;
			color = new global::UnityEngine.Color(1f, 1f, 1f, 0.8f + global::UnityEngine.Mathf.Sin(Life_Timer * 4f) * 0.2f);
			Lock_Icon_R.color = color;
			lock_Icon_L.color = color;
			global::UnityEngine.SpriteRenderer vertical_L = Vertical_L;
			color = global::UnityEngine.Color.Lerp(Vertical_R.color, new global::UnityEngine.Color(1f, 1f, 1f, 0.5f), global::UnityEngine.Time.deltaTime * 2.8f);
			Vertical_R.color = color;
			vertical_L.color = color;
			if (GM.Get_Event(3) || GM.Get_Event(15))
			{
				End_Timer += global::UnityEngine.Time.deltaTime;
				if (End_Timer > 6f)
				{
					State = 2;
					GetComponent<global::UnityEngine.BoxCollider2D>().enabled = false;
				}
			}
		}
		else
		{
			global::UnityEngine.SpriteRenderer glow_L_2 = Glow_L_2;
			global::UnityEngine.Color color = global::UnityEngine.Color.Lerp(Glow_R_1.color, new global::UnityEngine.Color(1f, 1f, 1f, 0f), global::UnityEngine.Time.deltaTime * 5f);
			Glow_R_1.color = color;
			color = color;
			Glow_R_2.color = color;
			color = color;
			Glow_L_1.color = color;
			glow_L_2.color = color;
			global::UnityEngine.SpriteRenderer lock_Icon_L2 = Lock_Icon_L;
			color = global::UnityEngine.Color.Lerp(Lock_Icon_R.color, new global::UnityEngine.Color(1f, 1f, 1f, 0f), global::UnityEngine.Time.deltaTime * 5f);
			Lock_Icon_R.color = color;
			lock_Icon_L2.color = color;
			global::UnityEngine.SpriteRenderer vertical_L2 = Vertical_L;
			color = global::UnityEngine.Color.Lerp(Vertical_R.color, new global::UnityEngine.Color(1f, 1f, 1f, 0f), global::UnityEngine.Time.deltaTime * 5f);
			Vertical_R.color = color;
			vertical_L2.color = color;
			if (Glow_L_1.color.a < 0.01f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}
}
