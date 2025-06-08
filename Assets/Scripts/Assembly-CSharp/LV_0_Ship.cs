public class LV_0_Ship : global::UnityEngine.MonoBehaviour
{
	private int State;

	public global::UnityEngine.Transform Arm_L;

	public global::UnityEngine.Transform Arm_R;

	public global::UnityEngine.Transform Joint_1_L;

	public global::UnityEngine.Transform Joint_1_R;

	public global::UnityEngine.Transform Joint_2_L;

	public global::UnityEngine.Transform Joint_2_R;

	public global::UnityEngine.Transform Engine_L;

	public global::UnityEngine.Transform Engine_R;

	public global::UnityEngine.Transform Wing_L;

	public global::UnityEngine.Transform Wing_R;

	public global::UnityEngine.Transform E_Side_L;

	public global::UnityEngine.Transform E_Side_R;

	public global::UnityEngine.SpriteRenderer glow_Side_L1;

	public global::UnityEngine.SpriteRenderer glow_Side_R1;

	public global::UnityEngine.SpriteRenderer glow_Side_L2;

	public global::UnityEngine.SpriteRenderer glow_Side_R2;

	public global::UnityEngine.SpriteRenderer glow_Engine_L1;

	public global::UnityEngine.SpriteRenderer glow_Engine_R1;

	public global::UnityEngine.SpriteRenderer glow_Engine_L2;

	public global::UnityEngine.SpriteRenderer glow_Engine_R2;

	public global::UnityEngine.SpriteRenderer glow_A_L1;

	public global::UnityEngine.SpriteRenderer glow_A_R1;

	public global::UnityEngine.SpriteRenderer glow_A_L2;

	public global::UnityEngine.SpriteRenderer glow_A_R2;

	public global::UnityEngine.SpriteRenderer glow_B_L1;

	public global::UnityEngine.SpriteRenderer glow_B_R1;

	public global::UnityEngine.SpriteRenderer glow_B_L2;

	public global::UnityEngine.SpriteRenderer glow_B_R2;

	public global::UnityEngine.SpriteRenderer glow_C_L1;

	public global::UnityEngine.SpriteRenderer glow_C_R1;

	public global::UnityEngine.SpriteRenderer glow_C_L2;

	public global::UnityEngine.SpriteRenderer glow_C_R2;

	public global::UnityEngine.SpriteRenderer Body_L;

	public global::UnityEngine.SpriteRenderer Body_R;

	public global::UnityEngine.SpriteRenderer Highlight;

	private float[] Opacity_1 = new float[10];

	private float[] Opacity_2 = new float[10];

	private global::UnityEngine.Color[] color_1 = new global::UnityEngine.Color[10];

	private global::UnityEngine.Color[] color_2 = new global::UnityEngine.Color[10];

	private global::UnityEngine.Color color_Off = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	public bool onEngineStart;

	private bool onEngineSide;

	private float Engine_Timer = 10f;

	private float Life_Timer;

	private float EngineStop_Timer = -10f;

	private float Off_Speed = 0.1f;

	private float Glow_Timer;

	public bool Sound_On;

	public global::UnityEngine.AudioSource Sound_Engine;

	public global::UnityEngine.AudioSource Sound_EngineStop;

	public global::UnityEngine.AudioSource Sound_LandingGear;

	public global::UnityEngine.AudioSource Sound_DoorOpen;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		Set_TargetOpacity();
		Off_Opacity();
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (EngineStop_Timer > 0f)
		{
			EngineStop_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (onEngineStart || EngineStop_Timer >= 0f)
		{
			Glow_Timer += global::UnityEngine.Time.deltaTime;
			if (Glow_Timer > 0.06f)
			{
				Glow_Timer = 0f;
				if (onEngineStart)
				{
					Set_TargetOpacity();
				}
				else
				{
					Set_TargetOpacity_EngineOff();
				}
			}
			float t = global::UnityEngine.Time.deltaTime * 10f;
			On_Side();
			glow_Side_L1.color = global::UnityEngine.Color.Lerp(glow_Side_L1.color, color_1[0], t);
			glow_Side_L2.color = global::UnityEngine.Color.Lerp(glow_Side_L2.color, color_2[0], t);
			glow_Side_R1.color = global::UnityEngine.Color.Lerp(glow_Side_R1.color, color_1[1], t);
			glow_Side_R2.color = global::UnityEngine.Color.Lerp(glow_Side_R2.color, color_2[1], t);
			Engine_Timer += global::UnityEngine.Time.deltaTime;
			if (Engine_Timer > 1f)
			{
				glow_Engine_L1.color = global::UnityEngine.Color.Lerp(glow_Engine_L1.color, color_1[2], t);
				glow_Engine_L2.color = global::UnityEngine.Color.Lerp(glow_Engine_L2.color, color_2[2], t);
				glow_Engine_R1.color = global::UnityEngine.Color.Lerp(glow_Engine_R1.color, color_1[3], t);
				glow_Engine_R2.color = global::UnityEngine.Color.Lerp(glow_Engine_R2.color, color_2[3], t);
				glow_A_L1.color = global::UnityEngine.Color.Lerp(glow_A_L1.color, color_1[4], t);
				glow_A_L2.color = global::UnityEngine.Color.Lerp(glow_A_L2.color, color_2[4], t);
				glow_A_R1.color = global::UnityEngine.Color.Lerp(glow_A_R1.color, color_1[5], t);
				glow_A_R2.color = global::UnityEngine.Color.Lerp(glow_A_R2.color, color_2[5], t);
				glow_B_L1.color = global::UnityEngine.Color.Lerp(glow_B_L1.color, color_1[6], t);
				glow_B_L2.color = global::UnityEngine.Color.Lerp(glow_B_L2.color, color_2[6], t);
				glow_B_R1.color = global::UnityEngine.Color.Lerp(glow_B_R1.color, color_1[7], t);
				glow_B_R2.color = global::UnityEngine.Color.Lerp(glow_B_R2.color, color_2[7], t);
				glow_C_L1.color = global::UnityEngine.Color.Lerp(glow_C_L1.color, color_1[8], t);
				glow_C_L2.color = global::UnityEngine.Color.Lerp(glow_C_L2.color, color_2[8], t);
				glow_C_R1.color = global::UnityEngine.Color.Lerp(glow_C_R1.color, color_1[9], t);
				glow_C_R2.color = global::UnityEngine.Color.Lerp(glow_C_R2.color, color_2[9], t);
			}
			if (Sound_On)
			{
				if (Sound_EngineStop.isPlaying)
				{
					Sound_Engine.volume = global::UnityEngine.Mathf.Lerp(Sound_Engine.volume, 0f, global::UnityEngine.Time.deltaTime * 0.8f);
				}
				else
				{
					Sound_Engine.volume = global::UnityEngine.Mathf.Lerp(Sound_Engine.volume, AxiPlayerPrefs.GetFloat("SoundVolume"), global::UnityEngine.Time.deltaTime * 0.8f);
				}
				if (!Sound_Engine.isPlaying)
				{
					Sound_Engine.Play();
				}
			}
			else if (GM.onGameClear)
			{
				Sound_Engine.volume = (1f - global::UnityEngine.GameObject.Find("BlackFade").GetComponent<global::UnityEngine.SpriteRenderer>().color.a) * AxiPlayerPrefs.GetFloat("SoundVolume") * 0.5f;
			}
			return;
		}
		if (EngineStop_Timer != -10f)
		{
			Engine_Timer = 0f;
			EngineStop_Timer = -10f;
		}
		if (onEngineSide)
		{
			Glow_Timer += global::UnityEngine.Time.deltaTime;
			if (Glow_Timer > 0.1f)
			{
				Glow_Timer = 0f;
				Set_TargetOpacity_EngineOff();
			}
			float t2 = global::UnityEngine.Time.deltaTime * 10f;
			glow_Side_L1.color = global::UnityEngine.Color.Lerp(glow_Side_L1.color, color_1[0], t2);
			glow_Side_L2.color = global::UnityEngine.Color.Lerp(glow_Side_L2.color, color_2[0], t2);
			glow_Side_R1.color = global::UnityEngine.Color.Lerp(glow_Side_R1.color, color_1[1], t2);
			glow_Side_R2.color = global::UnityEngine.Color.Lerp(glow_Side_R2.color, color_2[1], t2);
			Engine_Timer += global::UnityEngine.Time.deltaTime;
			if (Engine_Timer > 1f)
			{
				onEngineSide = false;
			}
			Sound_Engine.volume = global::UnityEngine.Mathf.Lerp(Sound_Engine.volume, 0f, global::UnityEngine.Time.deltaTime * 2f);
		}
		else
		{
			Off_Side();
			glow_Side_L1.color = global::UnityEngine.Color.Lerp(glow_Side_L1.color, color_Off, Off_Speed);
			glow_Side_L2.color = global::UnityEngine.Color.Lerp(glow_Side_L2.color, color_Off, Off_Speed);
			glow_Side_R1.color = global::UnityEngine.Color.Lerp(glow_Side_R1.color, color_Off, Off_Speed);
			glow_Side_R2.color = global::UnityEngine.Color.Lerp(glow_Side_R2.color, color_Off, Off_Speed);
			Sound_Engine.volume = global::UnityEngine.Mathf.Lerp(Sound_Engine.volume, 0f, global::UnityEngine.Time.deltaTime * 5f);
		}
		glow_Engine_L1.color = global::UnityEngine.Color.Lerp(glow_Engine_L1.color, color_Off, Off_Speed);
		glow_Engine_L2.color = global::UnityEngine.Color.Lerp(glow_Engine_L2.color, color_Off, Off_Speed);
		glow_Engine_R1.color = global::UnityEngine.Color.Lerp(glow_Engine_R1.color, color_Off, Off_Speed);
		glow_Engine_R2.color = global::UnityEngine.Color.Lerp(glow_Engine_R2.color, color_Off, Off_Speed);
		glow_A_L1.color = global::UnityEngine.Color.Lerp(glow_A_L1.color, color_Off, Off_Speed);
		glow_A_L2.color = global::UnityEngine.Color.Lerp(glow_A_L2.color, color_Off, Off_Speed);
		glow_A_R1.color = global::UnityEngine.Color.Lerp(glow_A_R1.color, color_Off, Off_Speed);
		glow_A_R2.color = global::UnityEngine.Color.Lerp(glow_A_R2.color, color_Off, Off_Speed);
		glow_B_L1.color = global::UnityEngine.Color.Lerp(glow_B_L1.color, color_Off, Off_Speed);
		glow_B_L2.color = global::UnityEngine.Color.Lerp(glow_B_L2.color, color_Off, Off_Speed);
		glow_B_R1.color = global::UnityEngine.Color.Lerp(glow_B_R1.color, color_Off, Off_Speed);
		glow_B_R2.color = global::UnityEngine.Color.Lerp(glow_B_R2.color, color_Off, Off_Speed);
		glow_C_L1.color = global::UnityEngine.Color.Lerp(glow_C_L1.color, color_Off, Off_Speed);
		glow_C_L2.color = global::UnityEngine.Color.Lerp(glow_C_L2.color, color_Off, Off_Speed);
		glow_C_R1.color = global::UnityEngine.Color.Lerp(glow_C_R1.color, color_Off, Off_Speed);
		glow_C_R2.color = global::UnityEngine.Color.Lerp(glow_C_R2.color, color_Off, Off_Speed);
	}

	private void Set_TargetOpacity()
	{
		color_1[0] = new global::UnityEngine.Color(1f, 1f, 1f, (float)global::UnityEngine.Random.Range(60, 100) * 0.01f);
		color_2[0] = new global::UnityEngine.Color(1f, 1f, 1f, (float)global::UnityEngine.Random.Range(10, 100) * 0.01f);
		color_1[1] = new global::UnityEngine.Color(1f, 1f, 1f, (float)global::UnityEngine.Random.Range(60, 100) * 0.01f);
		color_2[1] = new global::UnityEngine.Color(1f, 1f, 1f, (float)global::UnityEngine.Random.Range(10, 100) * 0.01f);
		for (int i = 2; i < 10; i++)
		{
			color_1[i] = new global::UnityEngine.Color(1f, 1f, 1f, (float)global::UnityEngine.Random.Range(10, 100) * 0.01f);
		}
		for (int j = 2; j < 10; j++)
		{
			color_2[j] = new global::UnityEngine.Color(1f, 1f, 1f, (float)global::UnityEngine.Random.Range(5, 100) * 0.01f);
		}
	}

	private void Set_TargetOpacity_EngineOff()
	{
		color_1[0] = new global::UnityEngine.Color(1f, 1f, 1f, (float)global::UnityEngine.Random.Range(5, 60) * 0.01f);
		color_2[0] = new global::UnityEngine.Color(1f, 1f, 1f, (float)global::UnityEngine.Random.Range(5, 60) * 0.01f);
		color_1[1] = new global::UnityEngine.Color(1f, 1f, 1f, (float)global::UnityEngine.Random.Range(5, 60) * 0.01f);
		color_2[1] = new global::UnityEngine.Color(1f, 1f, 1f, (float)global::UnityEngine.Random.Range(5, 60) * 0.01f);
		for (int i = 2; i < 10; i++)
		{
			color_1[i] = new global::UnityEngine.Color(1f, 1f, 1f, (float)global::UnityEngine.Random.Range(5, 60) * 0.01f);
		}
		for (int j = 2; j < 10; j++)
		{
			color_2[j] = new global::UnityEngine.Color(1f, 1f, 1f, (float)global::UnityEngine.Random.Range(5, 60) * 0.01f);
		}
	}

	public void Engine_Start()
	{
		onEngineStart = true;
		onEngineSide = true;
		Engine_Timer = 0f;
		EngineStop_Timer = -10f;
	}

	public void Engine_Stop(float timer)
	{
		onEngineStart = false;
		onEngineSide = true;
		EngineStop_Timer = timer;
	}

	public void Set_SortingLayer_On()
	{
		Body_L.sortingLayerID = AxiSortingOrder.GetHashIDByUserID(19);
		Body_R.sortingLayerID = AxiSortingOrder.GetHashIDByUserID(19);
		Highlight.sortingLayerID = AxiSortingOrder.GetHashIDByUserID(19);
	}

	public void Set_SortingLayer_Off()
	{
		Body_L.sortingLayerID = AxiSortingOrder.GetHashIDByUserID(0);
		Body_R.sortingLayerID = AxiSortingOrder.GetHashIDByUserID(0);
		Highlight.sortingLayerID = AxiSortingOrder.GetHashIDByUserID(0);
	}

	private void Play_Sound_LandingGear()
	{
		Sound_LandingGear.volume = AxiPlayerPrefs.GetFloat("SoundVolume");
		Sound_LandingGear.Play();
	}

	private void Play_Sound_EngineStop()
	{
		Sound_EngineStop.volume = AxiPlayerPrefs.GetFloat("SoundVolume") * 0.7f;
		Sound_EngineStop.Play();
	}

	private void Play_Sound_DoorOpen()
	{
		Sound_DoorOpen.volume = AxiPlayerPrefs.GetFloat("SoundVolume") * 0.85f;
		Sound_DoorOpen.Play();
	}

	private void On_Side()
	{
		E_Side_L.localPosition = global::UnityEngine.Vector3.Lerp(E_Side_L.localPosition, new global::UnityEngine.Vector3(-1.63f, -0.374f, 0f), global::UnityEngine.Time.deltaTime * 5f);
		E_Side_R.localPosition = E_Side_L.localPosition;
		Engine_L.localPosition = global::UnityEngine.Vector3.Lerp(Engine_L.localPosition, new global::UnityEngine.Vector3(-3.157f, -0.023f, 0f), global::UnityEngine.Time.deltaTime * 5f);
		Engine_R.localPosition = Engine_L.localPosition;
	}

	private void Off_Side()
	{
		E_Side_L.localPosition = global::UnityEngine.Vector3.Lerp(E_Side_L.localPosition, new global::UnityEngine.Vector3(-1.36f, -0.374f, 0f), global::UnityEngine.Time.deltaTime * 0.5f);
		E_Side_R.localPosition = E_Side_L.localPosition;
		Engine_L.localPosition = global::UnityEngine.Vector3.Lerp(Engine_L.localPosition, new global::UnityEngine.Vector3(-3f, -0.023f, 0f), global::UnityEngine.Time.deltaTime * 0.5f);
		Engine_R.localPosition = Engine_L.localPosition;
	}

	private void Off_Opacity()
	{
		glow_Side_L1.color = color_Off;
		glow_Side_L2.color = color_Off;
		glow_Side_R1.color = color_Off;
		glow_Side_R2.color = color_Off;
		glow_Engine_L1.color = color_Off;
		glow_Engine_L2.color = color_Off;
		glow_Engine_R1.color = color_Off;
		glow_Engine_R2.color = color_Off;
		glow_A_L1.color = color_Off;
		glow_A_L2.color = color_Off;
		glow_A_R1.color = color_Off;
		glow_A_R2.color = color_Off;
		glow_B_L1.color = color_Off;
		glow_B_L2.color = color_Off;
		glow_B_R1.color = color_Off;
		glow_B_R2.color = color_Off;
		glow_C_L1.color = color_Off;
		glow_C_L2.color = color_Off;
		glow_C_R1.color = color_Off;
		glow_C_R2.color = color_Off;
	}

	private void Set_SortingLayer(int back)
	{
		Body_L.sortingOrder = 30 - back;
		Body_R.sortingOrder = 30 - back;
		Highlight.sortingOrder = 31 - back;
		Arm_L.GetComponent<global::UnityEngine.SpriteRenderer>().sortingOrder = 28 - back;
		Arm_R.GetComponent<global::UnityEngine.SpriteRenderer>().sortingOrder = 28 - back;
		Joint_1_L.GetComponent<global::UnityEngine.SpriteRenderer>().sortingOrder = 29 - back;
		Joint_1_R.GetComponent<global::UnityEngine.SpriteRenderer>().sortingOrder = 29 - back;
		Joint_2_L.GetComponent<global::UnityEngine.SpriteRenderer>().sortingOrder = 27 - back;
		Joint_2_R.GetComponent<global::UnityEngine.SpriteRenderer>().sortingOrder = 27 - back;
		Engine_L.GetComponent<global::UnityEngine.SpriteRenderer>().sortingOrder = 30 - back;
		Engine_R.GetComponent<global::UnityEngine.SpriteRenderer>().sortingOrder = 30 - back;
		Wing_L.GetComponent<global::UnityEngine.SpriteRenderer>().sortingOrder = 29 - back;
		Wing_R.GetComponent<global::UnityEngine.SpriteRenderer>().sortingOrder = 29 - back;
		E_Side_L.GetComponent<global::UnityEngine.SpriteRenderer>().sortingOrder = 29 - back;
		E_Side_R.GetComponent<global::UnityEngine.SpriteRenderer>().sortingOrder = 29 - back;
		glow_Side_L1.sortingOrder = 31 - back;
		glow_Side_L2.sortingOrder = 32 - back;
		glow_Side_R1.sortingOrder = 31 - back;
		glow_Side_R2.sortingOrder = 32 - back;
		glow_Engine_L1.sortingOrder = 28 - back;
		glow_Engine_L2.sortingOrder = 29 - back;
		glow_Engine_R1.sortingOrder = 28 - back;
		glow_Engine_R2.sortingOrder = 29 - back;
		glow_A_L1.sortingOrder = 35 - back;
		glow_A_L2.sortingOrder = 36 - back;
		glow_A_R1.sortingOrder = 35 - back;
		glow_A_R2.sortingOrder = 36 - back;
		glow_B_L1.sortingOrder = 35 - back;
		glow_B_L2.sortingOrder = 36 - back;
		glow_B_R1.sortingOrder = 35 - back;
		glow_B_R2.sortingOrder = 36 - back;
		glow_C_L1.sortingOrder = 35 - back;
		glow_C_L2.sortingOrder = 36 - back;
		glow_C_R1.sortingOrder = 35 - back;
		glow_C_R2.sortingOrder = 36 - back;
	}
}
