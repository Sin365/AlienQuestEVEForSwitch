public class Teleport : global::UnityEngine.MonoBehaviour
{
	public int Teleport_Num;

	public bool onStartTeleport;

	public global::UnityEngine.GameObject DustObject;

	public global::UnityEngine.SpriteRenderer Font_1;

	public global::UnityEngine.SpriteRenderer Font_2;

	public global::UnityEngine.SpriteRenderer Font_Glow;

	public global::UnityEngine.SpriteRenderer Vertical_Bar;

	public global::UnityEngine.SpriteRenderer Vertical_Bar_Glow;

	public global::UnityEngine.SpriteRenderer Bottom_Bar;

	public global::UnityEngine.SpriteRenderer Bottom_Bar_Glow;

	public global::UnityEngine.SpriteRenderer BG_Glow;

	public global::UnityEngine.Transform pos_Teleport;

	public global::UnityEngine.GameObject info_UpArrow;

	private bool onEnabled;

	private bool wasActive;

	private global::UnityEngine.GameObject[] Dust_List;

	private float[] Speed_List;

	private float Life_Timer;

	private float Col_Timer;

	private int DustNum = 60;

	private global::UnityEngine.Color color_Dot_ON = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_Dot_OFF = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	private global::UnityEngine.Color color_Font_1;

	private global::UnityEngine.Color color_Font_2;

	private global::UnityEngine.Color color_Font_Glow;

	private global::UnityEngine.Color color_Vertical_Bar;

	private global::UnityEngine.Color color_Vertical_Bar_Glow;

	private global::UnityEngine.Color color_Bottom_Bar;

	private global::UnityEngine.Color color_Bottom_Bar_Glow;

	private global::UnityEngine.Color color_BG_Glow;

    GameManager GM => GameManager.instance;

    private TeleMenu_Control TeleMenu;

	private global::UnityEngine.GameObject Player;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		TeleMenu = global::UnityEngine.GameObject.Find("Menu_Teleport").GetComponent<TeleMenu_Control>();
		Player = global::UnityEngine.GameObject.Find("Player");
		TeleMenu.Teleport_Num = Teleport_Num;
		TeleMenu.teleport_Block = base.gameObject;
		Dust_List = new global::UnityEngine.GameObject[DustNum];
		Speed_List = new float[DustNum];
		switch (Teleport_Num)
		{
		case 1:
			color_Dot_ON = new global::UnityEngine.Color(1f, 1f, 0.5f, 1f);
			color_Dot_OFF = new global::UnityEngine.Color(1f, 1f, 0f, 0f);
			break;
		case 2:
			color_Dot_ON = new global::UnityEngine.Color(0.8f, 0.9f, 1f, 1f);
			color_Dot_OFF = new global::UnityEngine.Color(0.8f, 0.9f, 1f, 0f);
			break;
		case 3:
			color_Dot_ON = new global::UnityEngine.Color(0.9f, 1f, 0.5f, 1f);
			color_Dot_OFF = new global::UnityEngine.Color(0.9f, 1f, 0.5f, 0f);
			break;
		case 4:
			color_Dot_ON = new global::UnityEngine.Color(1f, 0.37f, 0f, 1f);
			color_Dot_OFF = new global::UnityEngine.Color(1f, 0.37f, 0f, 0f);
			break;
		}
		for (int i = 0; i < DustNum; i++)
		{
			Dust_List[i] = (global::UnityEngine.GameObject)global::UnityEngine.Object.Instantiate(DustObject, new global::UnityEngine.Vector3(base.transform.position.x, base.transform.position.y + global::UnityEngine.Random.Range(-1.6f, 2.2f), 0f), base.transform.rotation);
			Dust_List[i].transform.parent = base.transform;
			Reset_Dust(i);
			Dust_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Dot_OFF;
		}
		color_Font_1 = Font_1.color;
		color_Font_2 = Font_2.color;
		color_Font_Glow = Font_Glow.color;
		color_Vertical_Bar = Vertical_Bar.color;
		color_Vertical_Bar_Glow = Vertical_Bar_Glow.color;
		color_Bottom_Bar = Bottom_Bar.color;
		color_Bottom_Bar_Glow = Bottom_Bar_Glow.color;
		color_BG_Glow = BG_Glow.color;
		float num = global::UnityEngine.Vector3.Distance(Player.transform.position, pos_Teleport.position);
		if (!(num > 1.2f))
		{
			BG_Glow.color = new global::UnityEngine.Color(color_BG_Glow.r, color_BG_Glow.g, color_BG_Glow.b, 1f);
		}
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
		float num = global::UnityEngine.Vector3.Distance(Player.transform.position, pos_Teleport.position);
		TeleMenu.Dist = num;
		if (Col_Timer > 0f)
		{
			Col_Timer -= global::UnityEngine.Time.deltaTime;
			if (!onEnabled && !GM.Check_Teleport_On(Teleport_Num))
			{
				GM.Set_Teleport_On(Teleport_Num);
			}
			onEnabled = true;
		}
		else
		{
			onEnabled = false;
		}
		if (onEnabled)
		{
			TeleMenu.dist_Timer = 0.5f;
			for (int i = 0; i < DustNum; i++)
			{
				if (Dust_List[i].transform.localPosition.y < 0f)
				{
					Dust_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().color = global::UnityEngine.Color.Lerp(Dust_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().color, color_Dot_ON, global::UnityEngine.Time.deltaTime * 3f);
				}
				else
				{
					Dust_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().color = global::UnityEngine.Color.Lerp(Dust_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().color, color_Dot_OFF, global::UnityEngine.Time.deltaTime * Speed_List[i]);
				}
				Dust_List[i].transform.Translate(global::UnityEngine.Vector3.up * global::UnityEngine.Time.deltaTime * Speed_List[i]);
				Dust_List[i].transform.localScale = global::UnityEngine.Vector3.Lerp(Dust_List[i].transform.localScale, new global::UnityEngine.Vector3(0.3f, 0.3f, 1f), global::UnityEngine.Time.deltaTime * 1f);
				if (Dust_List[i].transform.localPosition.y > 0f && Dust_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().color.a < 0.01f)
				{
					Reset_Dust(i);
				}
			}
			Font_1.color = global::UnityEngine.Color.Lerp(Font_1.color, color_Font_1, global::UnityEngine.Time.deltaTime * 3f);
			Font_2.color = global::UnityEngine.Color.Lerp(Font_2.color, color_Font_2, global::UnityEngine.Time.deltaTime * 3f);
			Font_Glow.color = global::UnityEngine.Color.Lerp(Font_Glow.color, color_Font_Glow, global::UnityEngine.Time.deltaTime * 3f);
			Vertical_Bar.color = global::UnityEngine.Color.Lerp(Vertical_Bar.color, color_Vertical_Bar, global::UnityEngine.Time.deltaTime * 3f);
			Vertical_Bar_Glow.color = global::UnityEngine.Color.Lerp(Vertical_Bar_Glow.color, color_Vertical_Bar_Glow, global::UnityEngine.Time.deltaTime * 3f);
			Bottom_Bar.color = global::UnityEngine.Color.Lerp(Bottom_Bar.color, color_Bottom_Bar, global::UnityEngine.Time.deltaTime * 3f);
			Bottom_Bar_Glow.color = global::UnityEngine.Color.Lerp(Bottom_Bar_Glow.color, color_Bottom_Bar_Glow, global::UnityEngine.Time.deltaTime * 3f);
			if (onStartTeleport)
			{
				BG_Glow.color = global::UnityEngine.Color.Lerp(BG_Glow.color, new global::UnityEngine.Color(color_BG_Glow.r, color_BG_Glow.g, color_BG_Glow.b, 0.6f), global::UnityEngine.Time.deltaTime * 6f);
			}
			else
			{
				BG_Glow.color = global::UnityEngine.Color.Lerp(BG_Glow.color, color_BG_Glow, global::UnityEngine.Time.deltaTime * 3f);
			}
		}
		else
		{
			for (int j = 0; j < DustNum; j++)
			{
				Dust_List[j].GetComponent<global::UnityEngine.SpriteRenderer>().color = global::UnityEngine.Color.Lerp(Dust_List[j].GetComponent<global::UnityEngine.SpriteRenderer>().color, color_Dot_OFF, global::UnityEngine.Time.deltaTime * 3f);
				Dust_List[j].transform.Translate(global::UnityEngine.Vector3.up * global::UnityEngine.Time.deltaTime * Speed_List[j]);
				if (Dust_List[j].transform.localPosition.y > 1.8f)
				{
					Reset_Dust(j);
				}
			}
			Font_1.color = global::UnityEngine.Color.Lerp(Font_1.color, new global::UnityEngine.Color(color_Font_1.r, color_Font_1.g, color_Font_1.b, 0f), global::UnityEngine.Time.deltaTime * 3f);
			Font_2.color = global::UnityEngine.Color.Lerp(Font_2.color, new global::UnityEngine.Color(color_Font_1.r, color_Font_1.g, color_Font_1.b, 0f), global::UnityEngine.Time.deltaTime * 3f);
			Font_Glow.color = global::UnityEngine.Color.Lerp(Font_Glow.color, new global::UnityEngine.Color(color_Font_1.r, color_Font_1.g, color_Font_1.b, 0f), global::UnityEngine.Time.deltaTime * 3f);
			Vertical_Bar.color = global::UnityEngine.Color.Lerp(Vertical_Bar.color, new global::UnityEngine.Color(color_Vertical_Bar.r, color_Vertical_Bar.g, color_Vertical_Bar.b, 0f), global::UnityEngine.Time.deltaTime * 3f);
			Vertical_Bar_Glow.color = global::UnityEngine.Color.Lerp(Vertical_Bar_Glow.color, new global::UnityEngine.Color(color_Vertical_Bar_Glow.r, color_Vertical_Bar_Glow.g, color_Vertical_Bar_Glow.b, 0f), global::UnityEngine.Time.deltaTime * 3f);
			Bottom_Bar.color = global::UnityEngine.Color.Lerp(Bottom_Bar.color, new global::UnityEngine.Color(color_Bottom_Bar.r, color_Bottom_Bar.g, color_Bottom_Bar.b, 0f), global::UnityEngine.Time.deltaTime * 3f);
			Bottom_Bar_Glow.color = global::UnityEngine.Color.Lerp(Bottom_Bar_Glow.color, new global::UnityEngine.Color(color_Bottom_Bar_Glow.r, color_Bottom_Bar_Glow.g, color_Bottom_Bar_Glow.b, 0f), global::UnityEngine.Time.deltaTime * 3f);
			BG_Glow.color = global::UnityEngine.Color.Lerp(BG_Glow.color, new global::UnityEngine.Color(color_BG_Glow.r, color_BG_Glow.g, color_BG_Glow.b, 0f), global::UnityEngine.Time.deltaTime * 3f);
		}
		if (onEnabled && num < 1.8f && GM.EventState != 200)
		{
			if (!wasActive)
			{
				info_UpArrow.GetComponent<Info_Gate>().on_Info = true;
			}
			wasActive = true;
		}
		else
		{
			info_UpArrow.GetComponent<Info_Gate>().on_Info = false;
		}
	}

	private void Reset_Dust(int num)
	{
		Dust_List[num].transform.position = new global::UnityEngine.Vector3(base.transform.position.x + global::UnityEngine.Random.Range(-1.5f, 1.5f), base.transform.position.y - 2.5f - global::UnityEngine.Random.Range(0f, 0.4f), 0f);
		Dust_List[num].transform.localScale = new global::UnityEngine.Vector3(0.6f, 0.6f, 1f);
		Speed_List[num] = global::UnityEngine.Random.Range(0.1f, 10f);
		Dust_List[num].GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Dot_OFF;
	}

	private void OnTriggerStay2D(global::UnityEngine.Collider2D col)
	{
		if (!GM.Paused && col.name == "Ani")
		{
			Col_Timer = 0.2f;
		}
	}
}
