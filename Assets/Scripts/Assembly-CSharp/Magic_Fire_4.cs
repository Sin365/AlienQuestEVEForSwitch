using UnityEngine;

public class Magic_Fire_4 : global::UnityEngine.MonoBehaviour
{
	public enum Shield_Type
	{
		Player = 0,
		Moster = 1
	}

	public Magic_Fire_4.Shield_Type Type;

	private float Life_Timer;

	private bool onActive = true;

	private float Rot_Speed = -60f;

	private float Rot_Target = -15f;

	private int facingRight = 1;

	private bool isStarted;

	private bool isBroken;

	private float End_Timer;

	public global::UnityEngine.GameObject Circle_Inner;

	public global::UnityEngine.GameObject Circle_Outer;

	public global::UnityEngine.GameObject[] Inner_Q;

	public global::UnityEngine.GameObject[] Outer_Q;

	public global::UnityEngine.GameObject Head;

	private global::UnityEngine.GameObject[] Heads = new global::UnityEngine.GameObject[36];

	private bool[] onShock = new bool[36];

	private float[] shock_Timer = new float[36];

	private float[] end_Timer = new float[36];

	private global::UnityEngine.Vector3 scale_Orig = new global::UnityEngine.Vector3(0.8f, 0.8f, 1f);

	private global::UnityEngine.Vector3 scale_Target = new global::UnityEngine.Vector3(0.8f, 0.55f, 1f);

	private global::UnityEngine.Vector3 scale_Off = new global::UnityEngine.Vector3(0f, 0f, 1f);

	private global::UnityEngine.Color color_On = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_Off = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	public global::UnityEngine.GameObject soundShield;

	//private Player_Control PC;

	GameManager GM => GameManager.instance;
    Player_Control PC => GameManager.instance?.PC;
    GameObject Player => GameManager.instance?.gobj_Player;
    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//PC = global::UnityEngine.GameObject.Find("Player").GetComponent<Player_Control>();
		if (base.transform.localScale.x < 0f)
		{
			facingRight = -1;
		}
		Circle_Inner.transform.localScale = new global::UnityEngine.Vector3(0.1f, 0.1f, 1f);
		Circle_Outer.transform.localScale = new global::UnityEngine.Vector3(0.1f, 0.1f, 1f);
		if (Inner_Q.Length > 0)
		{
			for (int i = 0; i < Inner_Q.Length; i++)
			{
				Inner_Q[i].GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, 0.5f);
			}
		}
		for (int j = 0; j < 36; j++)
		{
			Heads[j] = global::UnityEngine.Object.Instantiate(Head, base.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 10 * j)) as global::UnityEngine.GameObject;
			Heads[j].transform.parent = base.transform;
			Heads[j].transform.localScale = new global::UnityEngine.Vector3(0.12f, 0.12f, 0.12f);
			onShock[j] = false;
			shock_Timer[j] = (float)global::UnityEngine.Random.Range(50, 300) * 0.01f;
			if (j < 18)
			{
				end_Timer[j] = ((j >= 9) ? ((float)(17 - j) * 0.04f) : ((float)j * 0.04f));
			}
			else
			{
				end_Timer[j] = ((j >= 27) ? ((float)(35 - j) * 0.02f) : ((float)j * 0.02f));
			}
		}
		if (Type == Magic_Fire_4.Shield_Type.Player)
		{
			GM.onShield = true;
			GM.Shield_Timer = 5f;
			global::UnityEngine.GameObject.Find("Shield_On").GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(4f, -190f, 0f);
		}
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (Type == Magic_Fire_4.Shield_Type.Player)
		{
			if (GM.onShield && GM.Hscene_Num != 0)
			{
				Set_Broken();
			}
			base.transform.position = new global::UnityEngine.Vector3(PC.transform.position.x, PC.transform.position.y + 2.7f, 0f);
		}
		if (!isStarted)
		{
			for (int i = 0; i < 36; i++)
			{
				Heads[i].transform.localScale = global::UnityEngine.Vector3.Lerp(Heads[i].transform.localScale, scale_Orig, global::UnityEngine.Time.deltaTime * 10f);
				Heads[i].GetComponent<global::UnityEngine.SpriteRenderer>().color = global::UnityEngine.Color.Lerp(Heads[i].GetComponent<global::UnityEngine.SpriteRenderer>().color, color_On, global::UnityEngine.Time.deltaTime * 8f);
			}
			Circle_Inner.transform.localScale = global::UnityEngine.Vector3.Lerp(Circle_Inner.transform.localScale, new global::UnityEngine.Vector3(0.776f, 0.776f, 1f), global::UnityEngine.Time.deltaTime * 6f);
			Circle_Outer.transform.localScale = global::UnityEngine.Vector3.Lerp(Circle_Outer.transform.localScale, new global::UnityEngine.Vector3(0.8f, 0.8f, 1f), global::UnityEngine.Time.deltaTime * 10f);
			base.transform.Rotate(0f, 0f, global::UnityEngine.Time.deltaTime * Rot_Speed * (float)facingRight);
			if (Circle_Inner.transform.localScale.x > 0.77f)
			{
				isStarted = true;
			}
		}
		else if (onActive)
		{
			for (int j = 0; j < 36; j++)
			{
				if (onShock[j])
				{
					Heads[j].transform.localScale = global::UnityEngine.Vector3.Lerp(Heads[j].transform.localScale, scale_Target, global::UnityEngine.Time.deltaTime * 20f);
					if (Heads[j].transform.localScale.y < 0.6f)
					{
						onShock[j] = false;
					}
				}
				else
				{
					shock_Timer[j] -= global::UnityEngine.Time.deltaTime;
					if (shock_Timer[j] <= 0f && global::UnityEngine.Random.Range(0, 10) > 8)
					{
						onShock[j] = true;
						shock_Timer[j] += 3f;
					}
				}
				Heads[j].transform.localScale = global::UnityEngine.Vector3.Lerp(Heads[j].transform.localScale, scale_Orig, global::UnityEngine.Time.deltaTime * 5f);
			}
			if (Type == Magic_Fire_4.Shield_Type.Player)
			{
				if (PC.Jump_Num > 0)
				{
					Rot_Target = -50f;
				}
				else
				{
					Rot_Target = -5f;
				}
			}
			else
			{
				Rot_Target = -5f;
			}
			Rot_Speed = global::UnityEngine.Mathf.Lerp(Rot_Speed, Rot_Target, global::UnityEngine.Time.deltaTime * 3f);
			base.transform.Rotate(0f, 0f, global::UnityEngine.Time.deltaTime * Rot_Speed * (float)facingRight);
		}
		else if (isBroken)
		{
			for (int k = 0; k < 36; k++)
			{
				Heads[k].transform.localScale = global::UnityEngine.Vector3.Lerp(Heads[k].transform.localScale, new global::UnityEngine.Vector3(0f, 1f, 1f), global::UnityEngine.Time.deltaTime * 15f);
				Heads[k].GetComponent<global::UnityEngine.SpriteRenderer>().color = global::UnityEngine.Color.Lerp(Heads[k].GetComponent<global::UnityEngine.SpriteRenderer>().color, color_Off, global::UnityEngine.Time.deltaTime * 8f);
			}
			Circle_Inner.transform.localScale = global::UnityEngine.Vector3.Lerp(Circle_Inner.transform.localScale, scale_Off, global::UnityEngine.Time.deltaTime * 10f);
			Circle_Outer.transform.localScale = global::UnityEngine.Vector3.Lerp(Circle_Outer.transform.localScale, scale_Off, global::UnityEngine.Time.deltaTime * 10f);
			if (Circle_Inner.transform.localScale.x < 0.1f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
		else
		{
			End_Timer += global::UnityEngine.Time.deltaTime;
			if (Inner_Q.Length > 0)
			{
				for (int l = 0; l < Inner_Q.Length; l++)
				{
					Inner_Q[l].GetComponent<global::UnityEngine.SpriteRenderer>().color = global::UnityEngine.Color.Lerp(Inner_Q[l].GetComponent<global::UnityEngine.SpriteRenderer>().color, color_Off, global::UnityEngine.Time.deltaTime * 8f);
					Outer_Q[l].GetComponent<global::UnityEngine.SpriteRenderer>().color = global::UnityEngine.Color.Lerp(Outer_Q[l].GetComponent<global::UnityEngine.SpriteRenderer>().color, color_Off, global::UnityEngine.Time.deltaTime * 8f);
				}
			}
			for (int m = 0; m < 36; m++)
			{
				end_Timer[m] -= global::UnityEngine.Time.deltaTime;
				if (end_Timer[m] <= 0f)
				{
					Heads[m].transform.localScale = global::UnityEngine.Vector3.Lerp(Heads[m].transform.localScale, scale_Off, global::UnityEngine.Time.deltaTime * 10f);
				}
			}
			Circle_Inner.transform.localScale = global::UnityEngine.Vector3.Lerp(Circle_Inner.transform.localScale, scale_Off, global::UnityEngine.Time.deltaTime * 1.5f);
			Circle_Outer.transform.localScale = global::UnityEngine.Vector3.Lerp(Circle_Outer.transform.localScale, scale_Off, global::UnityEngine.Time.deltaTime * 1f);
			Rot_Speed = global::UnityEngine.Mathf.Lerp(Rot_Speed, -100f, global::UnityEngine.Time.deltaTime * 3f);
			base.transform.Rotate(0f, 0f, global::UnityEngine.Time.deltaTime * Rot_Speed * (float)facingRight);
			if (Heads[8].transform.localScale.x < 0.1f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
		if (onActive && Life_Timer > 5f)
		{
			if (Type == Magic_Fire_4.Shield_Type.Player)
			{
				GM.onShield = false;
				GM.Shield_Timer = 0f;
				GM.Shield_Object = null;
				global::UnityEngine.GameObject.Find("Shield_On").GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(-600f, -190f, 0f);
			}
			onActive = false;
			soundShield.SendMessage("Sound_Off");
			isBroken = true;
		}
	}

	private void Set_Broken()
	{
		if (Type == Magic_Fire_4.Shield_Type.Player)
		{
			GM.onShield = false;
			GM.Shield_Timer = 0f;
			GM.Shield_Object = null;
			global::UnityEngine.GameObject.Find("Shield_On").GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(-600f, -190f, 0f);
		}
		GameManager.instance.sc_Sound_List.Electric_Dmg(base.transform.position);
		isStarted = true;
		onActive = false;
		soundShield.SendMessage("Sound_Off");
		isBroken = true;
	}
}
