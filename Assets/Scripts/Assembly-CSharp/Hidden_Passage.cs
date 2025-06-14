using UnityEngine;

public class Hidden_Passage : global::UnityEngine.MonoBehaviour
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

	private float Sit_Timer;

	private bool onEnabled;

	private float Hint_Timer;

	private global::UnityEngine.Color color_ON = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_OFF = new global::UnityEngine.Color(0f, 0f, 0f, 0f);

    Player_Control PC => GameManager.instance?.PC;
    GameObject Player => GameManager.instance?.gobj_Player;

    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//PC = global::UnityEngine.GameObject.Find("Player").GetComponent<Player_Control>();
		color_Orig = new global::UnityEngine.Color[SR_List.Length];
		for (int i = 0; i < SR_List.Length; i++)
		{
			color_Orig[i] = SR_List[i].color;
		}
		if (PC.transform.position.y < base.transform.position.y)
		{
			onEnabled = true;
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
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		if (Hint_Timer > 0f)
		{
			Hint_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (onDust)
		{
			Move_Dust();
		}
		if (onEnabled)
		{
			Show_Passage();
			if (Sit_Timer > 0.3f && Hint_Timer <= 0f)
			{
				Hint_Timer = 5f;
				GM.Info_Dialog("' ↓ + JUMP '   to SLIDE");
			}
		}
		else if (Sit_Timer > 0.3f)
		{
			onEnabled = true;
			Col_Block.enabled = false;
			AxiSoundPool.AddSoundForPosRot(_sound_Force, PC.transform.position, base.transform.rotation);
			Hint_Timer = 5f;
			GM.Info_Dialog("' ↓ + JUMP '   to SLIDE");
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
			SR_List[i].color = global::UnityEngine.Color.Lerp(SR_List[i].color, color_OFF, global::UnityEngine.Time.deltaTime * 2f);
		}
	}

	private void Hide_Passage()
	{
		for (int i = 0; i < SR_List.Length; i++)
		{
			SR_List[i].color = global::UnityEngine.Color.Lerp(SR_List[i].color, color_Orig[i], global::UnityEngine.Time.deltaTime * 5f);
		}
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
