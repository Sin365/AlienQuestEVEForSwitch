public class Event_Core : global::UnityEngine.MonoBehaviour
{
	private int State;

	public global::UnityEngine.GameObject DustObject;

	public global::UnityEngine.SpriteRenderer Glow_Red;

	public global::UnityEngine.SpriteRenderer Glow_Green;

	public global::UnityEngine.SpriteRenderer Monitor;

	public global::UnityEngine.SpriteRenderer Monitor_Glow;

	public global::UnityEngine.SpriteRenderer BG_Glow_1024;

	private global::UnityEngine.GameObject[] Dust_List;

	private float[] Speed_List;

	private float Life_Timer;

	private float screen_Timer;

	private int DustNum = 30;

	private global::UnityEngine.Color color_On = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_Off = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		Dust_List = new global::UnityEngine.GameObject[DustNum];
		Speed_List = new float[DustNum];
		for (int i = 0; i < DustNum; i++)
		{
			Dust_List[i] = (global::UnityEngine.GameObject)global::UnityEngine.Object.Instantiate(DustObject, new global::UnityEngine.Vector3(base.transform.position.x, base.transform.position.y + global::UnityEngine.Random.Range(-5f, 5f), 0f), base.transform.rotation);
			Dust_List[i].transform.parent = base.transform;
			Reset_Dust(i);
			Dust_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
		}
		Glow_Red.color = color_Off;
		Glow_Green.color = color_Off;
		Monitor.color = new global::UnityEngine.Color(0f, 0f, 0f, 1f);
		Monitor_Glow.color = color_Off;
	}

	public void Set_State(int num)
	{
		State = num;
		Life_Timer = 0f;
		if (State == 0)
		{
			Monitor.color = color_Off;
			Monitor_Glow.color = color_Off;
		}
		else if (State == 1 || State == 3)
		{
			Monitor.color = new global::UnityEngine.Color(1f, 1f, 1f, 0.5f);
			Monitor_Glow.color = new global::UnityEngine.Color(1f, 1f, 1f, 0.5f);
		}
		else
		{
			Monitor_Glow.color = new global::UnityEngine.Color(1f, 0f, 0f, 1f);
		}
	}

	private void Update()
	{
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (State == 0)
		{
			for (int i = 0; i < DustNum; i++)
			{
				Dust_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().color = global::UnityEngine.Color.Lerp(Dust_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().color, color_Off, global::UnityEngine.Time.deltaTime * 3f);
				Dust_List[i].transform.Translate(global::UnityEngine.Vector3.up * global::UnityEngine.Time.deltaTime * Speed_List[i]);
				if (Dust_List[i].transform.localPosition.y > 5.3f)
				{
					Reset_Dust(i);
				}
			}
			Glow_Red.color = global::UnityEngine.Color.Lerp(Glow_Red.color, color_Off, global::UnityEngine.Time.deltaTime * 3f);
			Glow_Green.color = global::UnityEngine.Color.Lerp(Glow_Green.color, color_Off, global::UnityEngine.Time.deltaTime * 3f);
			BG_Glow_1024.color = global::UnityEngine.Color.Lerp(BG_Glow_1024.color, new global::UnityEngine.Color(0f, 0.2f, 0.75f, 0.45f), global::UnityEngine.Time.deltaTime * 3f);
			return;
		}
		screen_Timer += global::UnityEngine.Time.deltaTime;
		for (int j = 0; j < DustNum; j++)
		{
			Dust_List[j].GetComponent<global::UnityEngine.SpriteRenderer>().color = global::UnityEngine.Color.Lerp(Dust_List[j].GetComponent<global::UnityEngine.SpriteRenderer>().color, color_On, global::UnityEngine.Time.deltaTime * 2f);
			Dust_List[j].transform.Translate(global::UnityEngine.Vector3.up * global::UnityEngine.Time.deltaTime * Speed_List[j]);
			if (Dust_List[j].transform.localPosition.y > 5.3f)
			{
				Reset_Dust(j);
			}
		}
		if (State == 1 || State == 3)
		{
			Glow_Red.color = global::UnityEngine.Color.Lerp(Glow_Red.color, color_Off, global::UnityEngine.Time.deltaTime * 3f);
			Glow_Green.color = global::UnityEngine.Color.Lerp(Glow_Green.color, color_On, global::UnityEngine.Time.deltaTime * 2f);
			BG_Glow_1024.color = global::UnityEngine.Color.Lerp(BG_Glow_1024.color, new global::UnityEngine.Color(0f, 0.2f, 0.75f, 0.45f), global::UnityEngine.Time.deltaTime * 3f);
			if (screen_Timer > 0.2f)
			{
				screen_Timer = 0f;
				float num = global::UnityEngine.Random.Range(0.6f, 0.8f);
				Monitor.color = new global::UnityEngine.Color(num, num, num, 1f);
			}
			Monitor.color = global::UnityEngine.Color.Lerp(Monitor.color, color_On, global::UnityEngine.Time.deltaTime * 2f);
			if (State == 3 && Life_Timer > 5f)
			{
				Set_State(2);
			}
		}
		else
		{
			Glow_Red.color = global::UnityEngine.Color.Lerp(Glow_Red.color, color_On, global::UnityEngine.Time.deltaTime * 2f);
			Glow_Green.color = global::UnityEngine.Color.Lerp(Glow_Green.color, color_Off, global::UnityEngine.Time.deltaTime * 3f);
			BG_Glow_1024.color = global::UnityEngine.Color.Lerp(BG_Glow_1024.color, new global::UnityEngine.Color(1f, 0f, 0f, 0.55f), global::UnityEngine.Time.deltaTime * 6f);
			if (screen_Timer > 0.2f)
			{
				screen_Timer = 0f;
				Monitor.color = new global::UnityEngine.Color(1f, 0f, 0f, 1f);
			}
			Monitor.color = global::UnityEngine.Color.Lerp(Monitor.color, new global::UnityEngine.Color(0f, 0f, 0f, 1f), global::UnityEngine.Time.deltaTime * 2f);
		}
	}

	private void Reset_Dust(int num)
	{
		Dust_List[num].transform.position = new global::UnityEngine.Vector3(base.transform.position.x, base.transform.position.y - 5.5f + global::UnityEngine.Random.Range(0f, 1f), 0f);
		Dust_List[num].transform.localScale = new global::UnityEngine.Vector3(2f, 2f, 1f);
		Speed_List[num] = global::UnityEngine.Random.Range(0.1f, 10f);
	}
}
