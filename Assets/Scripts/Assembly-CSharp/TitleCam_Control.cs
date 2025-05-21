public class TitleCam_Control : global::UnityEngine.MonoBehaviour
{
	private float Target_Timer;

	private float size_Target = 6f;

	private global::UnityEngine.Vector3 pos_Target = new global::UnityEngine.Vector3(3.6f, 3f, -100f);

	private float updateInterval = 0.5f;

	private float lastInterval;

	private int frames;

	public float fps;

	private float Fps_Timer;

	private float[] Fps_List;

	private int Fps_Num;

	private void Start()
	{
		lastInterval = global::UnityEngine.Time.realtimeSinceStartup;
		frames = 0;
		Set_60();
		Set_Default();
		Fps_List = new float[8];
	}

	private void Set_85()
	{
		size_Target = 8.5f;
		pos_Target = new global::UnityEngine.Vector3(6f, 0.55f, -100f);
	}

	private void Set_60()
	{
		size_Target = 6f;
		pos_Target = new global::UnityEngine.Vector3(3.6f, 3.8f, -100f);
	}

	private void Set_Default()
	{
		base.GetComponent<UnityEngine.Camera>().orthographicSize = 5f;
		base.transform.position = new global::UnityEngine.Vector3(2f, -6.5f, -100f);
	}

	private void Random_Target()
	{
		size_Target = 6f + (float)global::UnityEngine.Random.Range(-100, 150) * 0.01f;
		pos_Target = new global::UnityEngine.Vector3(global::UnityEngine.Random.Range(3.6f, 6f), global::UnityEngine.Random.Range(1f, 4f), -100f);
	}

	public void Save_Avg_Fps()
	{
		if (Fps_Num > 0)
		{
			float num = 0f;
			string text = string.Empty;
			for (int i = 0; i < Fps_Num; i++)
			{
				text = text + Fps_List[i].ToString("f2") + ", ";
				num += Fps_List[i];
			}
			global::UnityEngine.PlayerPrefs.SetFloat("Avg_Fps", num / (float)Fps_Num);
		}
	}

	private void Update()
	{
		frames++;
		float realtimeSinceStartup = global::UnityEngine.Time.realtimeSinceStartup;
		if (realtimeSinceStartup > lastInterval + updateInterval)
		{
			fps = (float)frames / (realtimeSinceStartup - lastInterval);
			frames = 0;
			lastInterval = realtimeSinceStartup;
		}
		if (Fps_Num < Fps_List.Length)
		{
			Fps_Timer += global::UnityEngine.Time.deltaTime;
			if (Fps_Timer > 0.5f)
			{
				Fps_Timer = 0f;
				Fps_List[Fps_Num] = fps;
				Fps_Num++;
			}
		}
	}

	private void FixedUpdate()
	{
		float num = global::UnityEngine.Vector3.Distance(pos_Target, base.transform.position);
		if (num < 0.2f)
		{
			Target_Timer += global::UnityEngine.Time.deltaTime;
			if (Target_Timer > 5f)
			{
				Target_Timer = 0f;
				Random_Target();
			}
		}
		base.GetComponent<UnityEngine.Camera>().orthographicSize = global::UnityEngine.Mathf.Lerp(base.GetComponent<UnityEngine.Camera>().orthographicSize, size_Target, global::UnityEngine.Time.deltaTime * 0.1f);
		base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, pos_Target, global::UnityEngine.Time.deltaTime * 0.12f);
	}
}
