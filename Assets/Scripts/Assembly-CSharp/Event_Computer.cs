public class Event_Computer : global::UnityEngine.MonoBehaviour
{
	private int State;

	public global::UnityEngine.SpriteRenderer Monitor_1;

	public global::UnityEngine.SpriteRenderer Monitor_2;

	public global::UnityEngine.SpriteRenderer Monitor_3;

	public global::UnityEngine.SpriteRenderer Glow_1;

	public global::UnityEngine.SpriteRenderer Glow_2;

	public global::UnityEngine.SpriteRenderer Glow_3;

	private float Life_Timer;

	private float screen_Timer_1;

	private float screen_Timer_2;

	private float screen_Timer_3;

	private float glow_Timer_1;

	private float glow_Timer_2;

	private float glow_Timer_3;

	private global::UnityEngine.Color color_On = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_Off = new global::UnityEngine.Color(0f, 0f, 0f, 1f);

	private global::UnityEngine.Color color_Red = new global::UnityEngine.Color(1f, 0f, 0f, 1f);

	private global::UnityEngine.Color color_Half = new global::UnityEngine.Color(1f, 1f, 1f, 0.5f);

    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		if (GM.Room_Num == 149)
		{
			Set_State(1);
		}
		else if (GM.Room_Num == 92)
		{
			if (!GM.Get_Event(1))
			{
				Set_State(0);
			}
		}
		else
		{
			Set_State(0);
		}
	}

	public void Set_State(int num)
	{
		State = num;
		Life_Timer = 0f;
		if (State == 0)
		{
			Monitor_1.color = color_Off;
			Monitor_2.color = color_Off;
			Monitor_3.color = color_Off;
			Glow_1.color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
			Glow_2.color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
			Glow_3.color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
		}
		else if (State == 1 || State == 3)
		{
			Glow_1.color = color_Half;
			Glow_2.color = color_Half;
			Glow_3.color = color_Half;
		}
		else
		{
			Glow_1.color = color_Red;
			Glow_2.color = color_Red;
			Glow_3.color = color_Red;
		}
	}

	private void Update()
	{
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (GM.onEvent)
		{
			if (GM.EventState != 0)
			{
			}
		}
		else if (!GM.Get_Event(1))
		{
		}
		if (State == 1 || State == 3)
		{
			screen_Timer_1 += global::UnityEngine.Time.deltaTime;
			if (screen_Timer_1 > 0.15f)
			{
				screen_Timer_1 = 0f;
				float num = global::UnityEngine.Random.Range(0.6f, 0.8f);
				Monitor_1.color = new global::UnityEngine.Color(num, num, num, 1f);
				num = global::UnityEngine.Random.Range(0.6f, 0.8f);
				Monitor_2.color = new global::UnityEngine.Color(num, num, num, 1f);
				num = global::UnityEngine.Random.Range(0.6f, 0.8f);
				Monitor_3.color = new global::UnityEngine.Color(num, num, num, 1f);
			}
			Monitor_1.color = global::UnityEngine.Color.Lerp(Monitor_1.color, color_On, global::UnityEngine.Time.deltaTime * 2f);
			Monitor_2.color = global::UnityEngine.Color.Lerp(Monitor_2.color, color_On, global::UnityEngine.Time.deltaTime * 2f);
			Monitor_3.color = global::UnityEngine.Color.Lerp(Monitor_3.color, color_On, global::UnityEngine.Time.deltaTime * 2f);
			if (State == 3 && Life_Timer > 5f)
			{
				Set_State(2);
			}
		}
		else if (State == 2)
		{
			screen_Timer_1 += global::UnityEngine.Time.deltaTime;
			screen_Timer_2 += global::UnityEngine.Time.deltaTime;
			screen_Timer_3 += global::UnityEngine.Time.deltaTime;
			if (screen_Timer_1 > 0.2f)
			{
				screen_Timer_1 = 0f;
				Monitor_1.color = new global::UnityEngine.Color(1f, 0f, 0f, 1f);
			}
			if (screen_Timer_2 > 0.3f)
			{
				screen_Timer_2 = 0f;
				Monitor_2.color = new global::UnityEngine.Color(0.5f, 0f, 0f, 1f);
			}
			if (screen_Timer_3 > 0.15f)
			{
				screen_Timer_3 = 0f;
				Monitor_3.color = new global::UnityEngine.Color(0.75f, 0f, 0f, 1f);
			}
			Monitor_1.color = global::UnityEngine.Color.Lerp(Monitor_1.color, color_Off, global::UnityEngine.Time.deltaTime * 2f);
			Monitor_2.color = global::UnityEngine.Color.Lerp(Monitor_2.color, color_Off, global::UnityEngine.Time.deltaTime * 2f);
			Monitor_3.color = global::UnityEngine.Color.Lerp(Monitor_3.color, color_Off, global::UnityEngine.Time.deltaTime * 2f);
		}
	}
}
