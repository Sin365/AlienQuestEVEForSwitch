public class BGM_Control : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.AudioSource[] BGM_List;

	private int Mode = 1;

	private float Life_Timer;

	private float Sleep_Timer;

	private float Start_Timer;

	private int BGM_Num;

	private int BGM_Max;

	private bool onBoss_Play;

	private int Boss_Num;

	private int Prev_Num;

	private float[] Volume;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		BGM_Max = BGM_List.Length;
		if (BGM_Max > 0)
		{
			Volume = new float[BGM_List.Length];
			for (int i = 0; i < BGM_Max; i++)
			{
				Volume[i] = 0f;
			}
		}
		if (AxiPlayerPrefs.GetInt("SelBGM") > 0)
		{
			BGM_Num = AxiPlayerPrefs.GetInt("SelBGM") - 1;
			Sleep_Timer = 0f;
			BGM_List[BGM_Num].GetComponent<UnityEngine.AudioSource>().Play();
			BGM_List[BGM_Num].GetComponent<UnityEngine.AudioSource>().loop = true;
		}
		Sleep_Timer = 7f;
	}

	private void Play_Intro()
	{
		BGM_Num = 1;
		Sleep_Timer = 0f;
		BGM_List[BGM_Num].GetComponent<UnityEngine.AudioSource>().Play();
	}

	public void Play(int num)
	{
		if (onBoss_Play)
		{
			onBoss_Play = false;
			Boss_Num = 0;
		}
		if (num == 0)
		{
			BGM_List[BGM_Num].GetComponent<UnityEngine.AudioSource>().loop = false;
			return;
		}
		BGM_Num = num - 1;
		Sleep_Timer = 0f;
		for (int i = 0; i < BGM_Max; i++)
		{
			if (i == BGM_Num)
			{
				BGM_List[i].GetComponent<UnityEngine.AudioSource>().Play();
				BGM_List[i].GetComponent<UnityEngine.AudioSource>().loop = true;
			}
			else
			{
				BGM_List[i].GetComponent<UnityEngine.AudioSource>().loop = false;
			}
		}
	}

	public void Play_Boss(int num)
	{
		onBoss_Play = true;
		Boss_Num = num;
		Prev_Num = BGM_Num;
		BGM_Num = 4;
		Sleep_Timer = 0f;
		for (int i = 0; i < BGM_Max; i++)
		{
			if (i == BGM_Num)
			{
				BGM_List[i].GetComponent<UnityEngine.AudioSource>().Play();
				BGM_List[i].GetComponent<UnityEngine.AudioSource>().loop = true;
			}
			else
			{
				BGM_List[i].GetComponent<UnityEngine.AudioSource>().loop = false;
			}
		}
	}

	private void OnExit()
	{
		Mode = 0;
	}

	private void Update()
	{
		if (onBoss_Play)
		{
			if (GM.Get_Event(10 + Boss_Num))
			{
				BGM_List[4].GetComponent<UnityEngine.AudioSource>().loop = false;
				for (int i = 0; i < BGM_Max; i++)
				{
					BGM_List[i].GetComponent<UnityEngine.AudioSource>().volume = global::UnityEngine.Mathf.Lerp(BGM_List[i].GetComponent<UnityEngine.AudioSource>().volume, 0f, global::UnityEngine.Time.deltaTime * 3f);
				}
				if (BGM_List[4].GetComponent<UnityEngine.AudioSource>().volume < 0.01f)
				{
					onBoss_Play = false;
					Boss_Num = 0;
					if (Prev_Num == BGM_Num)
					{
						BGM_Num = global::UnityEngine.Random.Range(1, 4);
					}
					else
					{
						BGM_Num = Prev_Num;
					}
					BGM_List[BGM_Num].GetComponent<UnityEngine.AudioSource>().Play();
				}
				return;
			}
			for (int j = 0; j < BGM_Max; j++)
			{
				if (j == BGM_Num)
				{
					BGM_List[j].GetComponent<UnityEngine.AudioSource>().volume = global::UnityEngine.Mathf.Lerp(BGM_List[j].GetComponent<UnityEngine.AudioSource>().volume, GM.Option_Volume[1] * 2f, global::UnityEngine.Time.deltaTime * 10f);
				}
				else
				{
					BGM_List[j].GetComponent<UnityEngine.AudioSource>().volume = global::UnityEngine.Mathf.Lerp(BGM_List[j].GetComponent<UnityEngine.AudioSource>().volume, 0f, global::UnityEngine.Time.deltaTime * 2f);
				}
			}
		}
		else if (Mode == 1)
		{
			if (GM.Room_Num == 150 && !GM.Get_Event(3))
			{
				for (int k = 0; k < BGM_Max; k++)
				{
					BGM_List[k].GetComponent<UnityEngine.AudioSource>().volume = global::UnityEngine.Mathf.Lerp(BGM_List[k].GetComponent<UnityEngine.AudioSource>().volume, 0f, global::UnityEngine.Time.deltaTime * 1f);
				}
				return;
			}
			if (!BGM_List[BGM_Num].GetComponent<UnityEngine.AudioSource>().isPlaying)
			{
				Sleep_Timer += global::UnityEngine.Time.deltaTime;
				if (Sleep_Timer > 10f)
				{
					if (BGM_Num < 3)
					{
						BGM_Num++;
					}
					else
					{
						BGM_Num = 0;
					}
					BGM_List[BGM_Num].GetComponent<UnityEngine.AudioSource>().Play();
					Sleep_Timer = 0f;
				}
			}
			for (int l = 0; l < BGM_Max; l++)
			{
				if (l == BGM_Num)
				{
					BGM_List[l].GetComponent<UnityEngine.AudioSource>().volume = global::UnityEngine.Mathf.Lerp(BGM_List[l].GetComponent<UnityEngine.AudioSource>().volume, GM.Option_Volume[1], global::UnityEngine.Time.deltaTime * 3f);
				}
				else
				{
					BGM_List[l].GetComponent<UnityEngine.AudioSource>().volume = global::UnityEngine.Mathf.Lerp(BGM_List[l].GetComponent<UnityEngine.AudioSource>().volume, 0f, global::UnityEngine.Time.deltaTime * 1f);
				}
			}
		}
		else
		{
			for (int m = 0; m < BGM_Max; m++)
			{
				BGM_List[m].GetComponent<UnityEngine.AudioSource>().volume = global::UnityEngine.Mathf.Lerp(BGM_List[m].GetComponent<UnityEngine.AudioSource>().volume, 0f, global::UnityEngine.Time.deltaTime * 2f);
			}
		}
	}
}
