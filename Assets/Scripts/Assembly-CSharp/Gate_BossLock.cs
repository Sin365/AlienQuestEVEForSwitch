public class Gate_BossLock : global::UnityEngine.MonoBehaviour
{
	public int Boss_Num;

	public global::UnityEngine.SpriteRenderer Glow_Top;

	public global::UnityEngine.SpriteRenderer Glow_Bot;

	public global::UnityEngine.SpriteRenderer Lock_Icon;

	private bool onEnd;

	private float Life_Timer;

	private float End_Timer = 1.2f;

    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		if (GM.Get_Event(Boss_Num + 10))
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
		global::UnityEngine.SpriteRenderer glow_Top = Glow_Top;
		global::UnityEngine.Color color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
		Lock_Icon.color = color;
		color = color;
		Glow_Bot.color = color;
		glow_Top.color = color;
	}

	public void Set_End_Timer(float timer)
	{
		End_Timer = timer;
	}

	private void Update()
	{
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (onEnd)
		{
			Glow_Top.color = global::UnityEngine.Color.Lerp(Glow_Top.color, new global::UnityEngine.Color(1f, 1f, 1f, 0f), global::UnityEngine.Time.deltaTime * 5f);
			Glow_Bot.color = Glow_Top.color;
			Lock_Icon.color = global::UnityEngine.Color.Lerp(Lock_Icon.color, new global::UnityEngine.Color(1f, 1f, 1f, 0f), global::UnityEngine.Time.deltaTime * 5f);
			if (Glow_Top.color.a < 0.02f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			return;
		}
		if (GM.Get_Event(Boss_Num + 10))
		{
			onEnd = true;
		}
		if (Life_Timer > End_Timer)
		{
			Glow_Top.color = global::UnityEngine.Color.Lerp(Glow_Top.color, new global::UnityEngine.Color(1f, 1f, 1f, 1f), global::UnityEngine.Time.deltaTime * 2.8f);
			Glow_Bot.color = Glow_Top.color;
			Lock_Icon.color = new global::UnityEngine.Color(1f, 1f, 1f, 0.8f + global::UnityEngine.Mathf.Sin(Life_Timer * 4f) * 0.2f);
		}
	}
}
