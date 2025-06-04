public class GunShip : global::UnityEngine.MonoBehaviour
{
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

	private float[] Opacity_1 = new float[10];

	private float[] Opacity_2 = new float[10];

	private global::UnityEngine.Color[] color_1 = new global::UnityEngine.Color[10];

	private global::UnityEngine.Color[] color_2 = new global::UnityEngine.Color[10];

	private global::UnityEngine.Color color_Off = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	private float Life_Timer;

	private float Glow_Timer;

	private float Engine_Timer = 10f;

	private float Sound_Engine_Volume;

	public global::UnityEngine.AudioSource Sound_FlyBy;

	public global::UnityEngine.AudioSource Sound_Engine;

	private void Start()
	{
		Set_TargetOpacity();
		Off_Opacity();
	}

	private void Update()
	{
		Life_Timer += global::UnityEngine.Time.deltaTime;
		Glow_Timer += global::UnityEngine.Time.deltaTime;
		if (Glow_Timer > 0.06f)
		{
			Glow_Timer = 0f;
			Set_TargetOpacity();
		}
		float t = global::UnityEngine.Time.deltaTime * 10f;
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
		if (!Sound_Engine.isPlaying && Sound_Engine_Volume > 0f)
		{
			Sound_Engine.Play();
		}
		Sound_Engine.volume = global::UnityEngine.Mathf.Lerp(Sound_Engine.volume, Sound_Engine_Volume, global::UnityEngine.Time.deltaTime * 1f);
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

	private void Play_Sound_FlyBy()
	{
		Sound_FlyBy.Play();
	}

	public void Set_Engine_Volume(float vol)
	{
		if (AxiPlayerPrefs.GetFloat("SoundVolume") < 0.4f)
		{
			Sound_Engine_Volume = 0.4f * vol;
		}
		else
		{
			Sound_Engine_Volume = AxiPlayerPrefs.GetFloat("SoundVolume") * vol;
		}
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
}
