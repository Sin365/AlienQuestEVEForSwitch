using UnityEngine;

public class Event_Reactor : global::UnityEngine.MonoBehaviour
{
	private float Life_Timer;

	private int State;

	private float Dist = 100f;

	private float PushY;

	private float inputY;

	private float prevY;

	private float[] Glow_Timer;

	private float[] Glow_Small_Timer;

	private float Border_Timer;

	private float[] Plug_Timer;

	private float Screen_Tmer;

	public global::UnityEngine.SpriteRenderer[] Glow;

	public global::UnityEngine.SpriteRenderer[] Glow_Blur;

	public global::UnityEngine.SpriteRenderer[] Glow_Small;

	public global::UnityEngine.SpriteRenderer Glow_Border;

	public global::UnityEngine.SpriteRenderer Com_Screen;

	public global::UnityEngine.SpriteRenderer Com_Screen_Glow;

	public Info_Gate info_UpArrow;

	public global::UnityEngine.Transform Plug_Platform;

	public Monster Queen_Mon;

	private global::UnityEngine.Color color_Sphere = new global::UnityEngine.Color(1f, 1f, 1f);

	private global::UnityEngine.Color color_On = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_Off = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	private global::UnityEngine.Color color_Red = new global::UnityEngine.Color(1f, 0f, 0f, 1f);

	private global::UnityEngine.Color color_Blue = new global::UnityEngine.Color(0f, 0.7f, 1f, 0.6f);

    GameManager GM => GameManager.instance;
    Player_Control PC => GameManager.instance.PC;
    GameObject Player => GameManager.instance.gobj_Player;

    private Custom_Key CK => GameManager.instance.CK;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//CK = global::UnityEngine.GameObject.Find("GameManager").GetComponent<Custom_Key>();
		//Player = global::UnityEngine.GameObject.Find("Player");
		Glow_Timer = new float[Glow.Length];
		Glow_Small_Timer = new float[Glow_Small.Length];
		color_Sphere = color_On;
		Reset_Color();
		if (GM.Get_Event(15))
		{
			State = 3;
			info_UpArrow.on_Info = false;
			color_Sphere = color_Red;
			global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().targetSize = 11.2f;
		}
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (State == 0)
		{
			Dist = global::UnityEngine.Vector3.Distance(Plug_Platform.position, Player.transform.position);
			if (Dist < 1.5f)
			{
				inputY = 0f;
				if (global::UnityEngine.Input.GetKeyDown(CK.Up))
				{
					inputY = 1f;
				}
				else if (global::UnityEngine.Input.GetKeyDown(CK.Down))
				{
					inputY = -1f;
				}
				if (inputY == 0f && global::UnityEngine.Input.GetAxis("L_Y") != 0f)
				{
					if (global::UnityEngine.Input.GetAxis("L_Y") > 0f && global::UnityEngine.Input.GetAxis("L_Y") < prevY)
					{
						PushY = prevY;
					}
					else if (global::UnityEngine.Input.GetAxis("L_Y") < 0f && global::UnityEngine.Input.GetAxis("L_Y") > prevY)
					{
						PushY = prevY;
					}
					if (global::UnityEngine.Input.GetAxis("L_Y") > 0f && global::UnityEngine.Input.GetAxis("L_Y") - PushY > 0.3f)
					{
						inputY = 1f;
						PushY = 1f;
					}
					else if (global::UnityEngine.Input.GetAxis("L_Y") < 0f && global::UnityEngine.Input.GetAxis("L_Y") - PushY < -0.3f)
					{
						inputY = -1f;
						PushY = -1f;
					}
				}
				else if (inputY == 0f && global::UnityEngine.Input.GetAxis("L_Y") == 0f)
				{
					PushY = 0f;
				}
				info_UpArrow.on_Info = true;
				if (inputY > 0f)
				{
					State = 1;
					info_UpArrow.on_Info = false;
					GM.Set_Event(5);
				}
			}
			else
			{
				info_UpArrow.on_Info = false;
			}
		}
		else if (State == 1)
		{
			color_Sphere = global::UnityEngine.Color.Lerp(color_Sphere, color_Blue, global::UnityEngine.Time.deltaTime * 0.2f);
			Set_Color_Breathe();
			Glow_Border.transform.localScale = new global::UnityEngine.Vector3(3.5f + global::UnityEngine.Mathf.Sin(Life_Timer) * 0.1f, 3.5f + global::UnityEngine.Mathf.Sin(Life_Timer) * 0.1f, 1f);
			Glow_Border.color = global::UnityEngine.Color.Lerp(Glow_Border.color, new global::UnityEngine.Color(0f, 0.7f, 1f, 0.1f), global::UnityEngine.Time.deltaTime * 0.2f);
			Screen_Tmer += global::UnityEngine.Time.deltaTime;
			if (Screen_Tmer > 0.15f)
			{
				Screen_Tmer = 0f;
				float num = global::UnityEngine.Random.Range(0.6f, 0.8f);
				Com_Screen.color = new global::UnityEngine.Color(num, num, num, 1f);
			}
			Com_Screen.color = global::UnityEngine.Color.Lerp(Com_Screen.color, color_On, global::UnityEngine.Time.deltaTime * 2f);
			Com_Screen_Glow.color = global::UnityEngine.Color.Lerp(Com_Screen_Glow.color, color_On, global::UnityEngine.Time.deltaTime * 2f);
			if (GM.Get_Event(3))
			{
				State = 3;
			}
			else if (Queen_Mon != null && Queen_Mon.HP_Ratio <= 0.2f)
			{
				State = 2;
			}
		}
		else if (State == 2)
		{
			color_Sphere = global::UnityEngine.Color.Lerp(color_Sphere, color_Red, global::UnityEngine.Time.deltaTime * 0.2f);
			Set_Color_Breathe();
			Glow_Border.transform.localScale = new global::UnityEngine.Vector3(3.5f + global::UnityEngine.Mathf.Sin(Life_Timer) * 0.1f, 3.5f + global::UnityEngine.Mathf.Sin(Life_Timer) * 0.1f, 1f);
			Glow_Border.color = global::UnityEngine.Color.Lerp(Glow_Border.color, new global::UnityEngine.Color(1f, 0f, 0f, 0.3f), global::UnityEngine.Time.deltaTime * 0.2f);
			Screen_Tmer += global::UnityEngine.Time.deltaTime;
			if (Screen_Tmer > 0.15f)
			{
				Screen_Tmer = 0f;
				float r = global::UnityEngine.Random.Range(0.6f, 0.8f);
				Com_Screen.color = new global::UnityEngine.Color(r, 0f, 0f, 1f);
			}
			Com_Screen.color = global::UnityEngine.Color.Lerp(Com_Screen.color, color_On, global::UnityEngine.Time.deltaTime * 1f);
			Com_Screen_Glow.color = global::UnityEngine.Color.Lerp(Com_Screen_Glow.color, color_On, global::UnityEngine.Time.deltaTime * 1f);
			if (GM.Get_Event(3))
			{
				State = 3;
			}
		}
		else
		{
			color_Sphere = global::UnityEngine.Color.Lerp(color_Sphere, color_Red, global::UnityEngine.Time.deltaTime * 2f);
			Set_Color_Breathe_Red();
			Glow_Border.transform.localScale = new global::UnityEngine.Vector3(3.5f + global::UnityEngine.Mathf.Sin(Life_Timer * 10f) * 0.1f, 3.5f + global::UnityEngine.Mathf.Sin(Life_Timer * 10f) * 0.1f, 1f);
			Glow_Border.color = global::UnityEngine.Color.Lerp(Glow_Border.color, new global::UnityEngine.Color(1f, 0f, 0f, 0.6f), global::UnityEngine.Time.deltaTime * 5f);
			Com_Screen.color = global::UnityEngine.Color.Lerp(Com_Screen.color, color_Red, global::UnityEngine.Time.deltaTime * 2f);
			Com_Screen_Glow.color = global::UnityEngine.Color.Lerp(Com_Screen_Glow.color, color_Red, global::UnityEngine.Time.deltaTime * 2f);
		}
	}

	private void Set_Color_Breathe()
	{
		for (int i = 0; i < Glow.Length; i++)
		{
			Glow_Timer[i] += global::UnityEngine.Time.deltaTime;
			Glow[i].color = new global::UnityEngine.Color(color_Sphere.r, color_Sphere.g, color_Sphere.b, global::UnityEngine.Mathf.Sin(Glow_Timer[i]));
			Glow_Blur[i].color = Glow[i].color;
		}
		for (int j = 0; j < Glow_Small.Length; j++)
		{
			Glow_Small_Timer[j] += global::UnityEngine.Time.deltaTime;
			Glow_Small[j].color = new global::UnityEngine.Color(color_Sphere.r, color_Sphere.g, color_Sphere.b, global::UnityEngine.Mathf.Sin(Glow_Small_Timer[j]));
		}
	}

	private void Reset_Color()
	{
		for (int i = 0; i < Glow.Length; i++)
		{
			Glow_Timer[i] = global::UnityEngine.Random.Range(0f, 5f);
			Glow[i].color = color_Off;
			Glow_Blur[i].color = color_Off;
		}
		for (int j = 0; j < Glow_Small.Length; j++)
		{
			Glow_Small_Timer[j] = global::UnityEngine.Random.Range(0f, 5f);
			Glow_Small[j].color = color_Off;
		}
		Glow_Border.color = color_Off;
	}

	private void Set_Color_Breathe_Red()
	{
		for (int i = 0; i < Glow.Length; i++)
		{
			Glow_Timer[i] += global::UnityEngine.Time.deltaTime * 5f;
			Glow[i].color = new global::UnityEngine.Color(color_Sphere.r, color_Sphere.g, color_Sphere.b, 0.3f + global::UnityEngine.Mathf.Sin(Glow_Timer[i]) * 0.3f);
			Glow_Blur[i].color = Glow[i].color;
		}
		for (int j = 0; j < Glow_Small.Length; j++)
		{
			Glow_Small_Timer[j] += global::UnityEngine.Time.deltaTime * 5f;
			Glow_Small[j].color = new global::UnityEngine.Color(color_Sphere.r, color_Sphere.g, color_Sphere.b, 0.3f + global::UnityEngine.Mathf.Sin(Glow_Small_Timer[j]) * 0.3f);
		}
	}
}
