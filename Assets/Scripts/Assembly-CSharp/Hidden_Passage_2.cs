public class Hidden_Passage_2 : global::UnityEngine.MonoBehaviour
{
	public bool onDust;

	public int dust_Max;

	public global::UnityEngine.GameObject _dust;

	public global::UnityEngine.Transform pos_dust;

	private global::UnityEngine.GameObject[] Dust_List;

	private float[] Dust_Speed;

	public global::UnityEngine.BoxCollider2D Col_Block;

	public global::UnityEngine.GameObject _sound_Force;

	public global::UnityEngine.SpriteRenderer[] SR_List;

	private global::UnityEngine.Color[] color_Orig;

	public global::UnityEngine.Transform pos_Check_1;

	public global::UnityEngine.Transform pos_Check_2;

	public global::UnityEngine.Transform pos_Check_3;

	public global::UnityEngine.Transform pos_Check_4;

	private float Sit_Timer;

	private bool onEnabled;

	private global::UnityEngine.Color color_ON = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_OFF = new global::UnityEngine.Color(0f, 0f, 0f, 0f);

	private Player_Control PC;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		PC = global::UnityEngine.GameObject.Find("Player").GetComponent<Player_Control>();
		color_Orig = new global::UnityEngine.Color[SR_List.Length];
		for (int i = 0; i < SR_List.Length; i++)
		{
			color_Orig[i] = SR_List[i].color;
		}
		if (onDust)
		{
			Dust_List = new global::UnityEngine.GameObject[dust_Max];
			Dust_Speed = new float[dust_Max];
			global::UnityEngine.Vector3 position = new global::UnityEngine.Vector3(pos_dust.position.x, pos_dust.position.y + 3.2f, 0f);
			for (int j = 0; j < dust_Max; j++)
			{
				Dust_List[j] = global::UnityEngine.Object.Instantiate(_dust, position, base.transform.rotation) as global::UnityEngine.GameObject;
				Dust_List[j].transform.parent = base.transform;
				Dust_List[j].GetComponent<global::UnityEngine.SpriteRenderer>().color = color_OFF;
				Dust_Speed[j] = global::UnityEngine.Random.Range(1f, 6f);
			}
		}
		if (!onEnabled && pos_Check_1 != null && pos_Check_2 != null)
		{
			Check_Start_Pos();
		}
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		if (onDust)
		{
			Move_Dust();
		}
		if (onEnabled)
		{
			Show_Passage();
			return;
		}
		if (!onEnabled && pos_Check_1 != null && pos_Check_2 != null)
		{
			Check_Start_Pos();
		}
		if (Sit_Timer > 0.3f)
		{
			onEnabled = true;
			Col_Block.enabled = false;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_sound_Force, PC.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
		}
	}

	private void Move_Dust()
	{
		for (int i = 0; i < dust_Max; i++)
		{
			Dust_List[i].transform.Translate(global::UnityEngine.Vector3.down * global::UnityEngine.Time.deltaTime * Dust_Speed[i]);
			if (!onEnabled)
			{
				Dust_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().color = global::UnityEngine.Color.Lerp(Dust_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().color, color_ON, global::UnityEngine.Time.deltaTime * 1f);
			}
			else
			{
				Dust_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().color = global::UnityEngine.Color.Lerp(Dust_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().color, color_OFF, global::UnityEngine.Time.deltaTime * 1f);
			}
			if (Dust_List[i].transform.position.y <= pos_dust.position.y)
			{
				Dust_List[i].transform.position = new global::UnityEngine.Vector3(pos_dust.position.x, pos_dust.position.y + 3.2f, 0f);
				Dust_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().color = color_OFF;
				Dust_Speed[i] = global::UnityEngine.Random.Range(1f, 6f);
			}
		}
	}

	private void Show_Passage()
	{
		for (int i = 0; i < SR_List.Length; i++)
		{
			SR_List[i].color = global::UnityEngine.Color.Lerp(SR_List[i].color, new global::UnityEngine.Color(color_Orig[i].r, color_Orig[i].g, color_Orig[i].b, 0f), global::UnityEngine.Time.deltaTime * 2f);
		}
	}

	private void Hide_Passage()
	{
		for (int i = 0; i < SR_List.Length; i++)
		{
			SR_List[i].color = global::UnityEngine.Color.Lerp(SR_List[i].color, color_Orig[i], global::UnityEngine.Time.deltaTime * 5f);
		}
	}

	private void Check_Start_Pos()
	{
		if ((bool)global::UnityEngine.Physics2D.Linecast(pos_Check_1.position, pos_Check_2.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player_Dmg")))
		{
			onEnabled = true;
			Col_Block.enabled = false;
		}
		else if (pos_Check_3 != null && pos_Check_4 != null && (bool)global::UnityEngine.Physics2D.Linecast(pos_Check_3.position, pos_Check_4.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player_Dmg")))
		{
			onEnabled = true;
			Col_Block.enabled = false;
		}
	}

	public void Set_Enabled()
	{
		onEnabled = true;
		Col_Block.enabled = false;
	}

	private void OnTriggerStay2D(global::UnityEngine.Collider2D col)
	{
		if (!GM.Paused && col.name == "Ani")
		{
			if (PC.State.ToString() == "Sit" || PC.State.ToString() == "Slide")
			{
				Sit_Timer += global::UnityEngine.Time.deltaTime;
			}
			else
			{
				Sit_Timer = 0f;
			}
		}
	}

	private void OnTriggerExit2D(global::UnityEngine.Collider2D col)
	{
		if (!GM.Paused && col.name == "Ani")
		{
			Sit_Timer = 0f;
		}
	}
}
