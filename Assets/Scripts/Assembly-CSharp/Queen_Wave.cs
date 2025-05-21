public class Queen_Wave : global::UnityEngine.MonoBehaviour
{
	public int Damage = 30;

	public float DmgForce = 10f;

	public global::UnityEngine.SpriteRenderer Glow;

	public global::UnityEngine.SpriteRenderer[] Bar_List;

	public global::UnityEngine.BoxCollider2D Col_Box;

	private float Life_Timer;

	private float Damage_Timer;

	private float Bar_Timer;

	private int Bar_Num;

	private float Bar_Size = 1f;

	private global::UnityEngine.Color Color_Glow_OFF;

	private global::UnityEngine.Color Color_Bar_OFF;

	private global::UnityEngine.Vector3 pos_Orig;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		Color_Glow_OFF = new global::UnityEngine.Color(Glow.color.r, Glow.color.g, Glow.color.b, 0f);
		Color_Bar_OFF = new global::UnityEngine.Color(Bar_List[0].color.r, Bar_List[0].color.g, Bar_List[0].color.b, 0f);
		pos_Orig = base.transform.position;
		Reset_Bar();
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (Damage_Timer > 0f)
		{
			Damage_Timer -= global::UnityEngine.Time.deltaTime;
		}
		Bar_Timer += global::UnityEngine.Time.deltaTime;
		if (Bar_Timer > 0.03f)
		{
			Bar_Timer = 0f;
			Bar_Num++;
			if (Bar_Num < Bar_List.Length)
			{
				if (Bar_Num > 12)
				{
					Bar_List[Bar_Num].transform.localScale = new global::UnityEngine.Vector3(1f, 0.2f + (float)(Bar_List.Length - Bar_Num) * 0.12f, 1f);
				}
				else
				{
					Bar_List[Bar_Num].transform.localScale = new global::UnityEngine.Vector3(1f, 1f, 1f);
				}
				Bar_List[Bar_Num].color = new global::UnityEngine.Color(Color_Bar_OFF.r, Color_Bar_OFF.g, Color_Bar_OFF.b, 1f);
			}
		}
		for (int i = 0; i < Bar_List.Length; i++)
		{
			if (Bar_Num > Bar_List.Length + 5)
			{
				Bar_List[i].transform.localScale = global::UnityEngine.Vector3.Lerp(Bar_List[i].transform.localScale, new global::UnityEngine.Vector3(1f, 0f, 1f), global::UnityEngine.Time.deltaTime * 16f);
				Bar_List[i].color = global::UnityEngine.Color.Lerp(Bar_List[i].color, Color_Bar_OFF, global::UnityEngine.Time.deltaTime * 10f);
			}
			else if (i < Bar_Num - 1)
			{
				Bar_List[i].transform.localScale = global::UnityEngine.Vector3.Lerp(Bar_List[i].transform.localScale, new global::UnityEngine.Vector3(1f, 0f, 1f), global::UnityEngine.Time.deltaTime * 7f);
				Bar_List[i].color = global::UnityEngine.Color.Lerp(Bar_List[i].color, Color_Bar_OFF, global::UnityEngine.Time.deltaTime * 10f);
			}
			else
			{
				Bar_List[i].transform.localScale = global::UnityEngine.Vector3.Lerp(Bar_List[i].transform.localScale, new global::UnityEngine.Vector3(1f, 0f, 1f), global::UnityEngine.Time.deltaTime * 2f);
				Bar_List[i].color = global::UnityEngine.Color.Lerp(Bar_List[i].color, Color_Bar_OFF, global::UnityEngine.Time.deltaTime * 2f);
			}
		}
		if (Bar_Num < Bar_List.Length)
		{
			Col_Box.transform.Translate(global::UnityEngine.Vector3.left * global::UnityEngine.Time.deltaTime * 9f * base.transform.localScale.x);
			base.transform.Translate(global::UnityEngine.Vector3.left * global::UnityEngine.Time.deltaTime * 4f * base.transform.localScale.x);
		}
		else
		{
			base.transform.Translate(global::UnityEngine.Vector3.left * global::UnityEngine.Time.deltaTime * 1f * base.transform.localScale.x);
		}
		if (Col_Box.enabled && Bar_Num > Bar_List.Length - 1)
		{
			Col_Box.enabled = false;
		}
		Glow.transform.localScale = global::UnityEngine.Vector3.Lerp(Glow.transform.localScale, new global::UnityEngine.Vector3(6f, 4f, 0f), global::UnityEngine.Time.deltaTime * 2f);
		if (Bar_Num > 12)
		{
			Glow.color = global::UnityEngine.Color.Lerp(Glow.color, Color_Glow_OFF, global::UnityEngine.Time.deltaTime * 8f);
		}
		if (Glow.color.a < 0.02f)
		{
			Col_Box.enabled = false;
			Glow.color = Color_Glow_OFF;
		}
		if (Life_Timer > 1f)
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void Reset_Bar()
	{
		Life_Timer = 0f;
		Bar_Timer = 0f;
		Bar_Num = 0;
		Bar_List[Bar_Num].transform.localScale = new global::UnityEngine.Vector3(1f, 1f, 1f);
		Bar_List[Bar_Num].color = new global::UnityEngine.Color(Color_Bar_OFF.r, Color_Bar_OFF.g, Color_Bar_OFF.b, 1f);
		for (int i = 1; i < Bar_List.Length; i++)
		{
			Bar_List[i].transform.localScale = new global::UnityEngine.Vector3(1f, 0f, 1f);
		}
		Col_Box.transform.localPosition = new global::UnityEngine.Vector3(0f, 0f, 0f);
		Col_Box.enabled = true;
		base.transform.position = pos_Orig;
		Glow.transform.localScale = new global::UnityEngine.Vector3(2f, 1f, 0f);
		Glow.color = new global::UnityEngine.Color(Color_Glow_OFF.r, Color_Glow_OFF.g, Color_Glow_OFF.b, 1f);
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Boss_4_Fire(base.transform.position);
	}

	private void OnTriggerStay2D(global::UnityEngine.Collider2D col)
	{
		if (!GM.Paused && Damage_Timer <= 0f && col.name == "Ani" && !GM.onShield && GM.Damage_Timer <= 0f && !GM.GameOver && !GM.onEvent && !GM.onHscene && !GM.onDown)
		{
			Damage_Timer = 0.2f;
			GM.Damage(Damage, DmgForce * (0f - base.transform.localScale.x), false, 155);
		}
	}
}
