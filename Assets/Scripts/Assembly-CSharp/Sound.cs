public class Sound : AxiSoundBase
{
	private float life_Timer;

	private bool isPaused;

	private bool isPlayStarted;

	private float Volume_Orig = 1f;

	private float Dist_Orig = 1f;

	private float Dist_Var = 1f;

	private float distance;

	private bool OnEvent;
	private GameManager GM => GameManager.instance;

    public override string resourceName { get; set; }

	public override void Init()
	{
		life_Timer = default;
		isPaused = default;
		isPlayStarted = default;
		Volume_Orig = 1f;
		Dist_Orig = 1f;
		Dist_Var = 1f;
		distance = default;
		OnEvent = default;
		Start();
    }
	
    public override void ReleaseToPool()
    {
		AxiSoundPool.ReleaseSound(this);
    }

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		Volume_Orig = base.GetComponent<UnityEngine.AudioSource>().volume;
		Dist_Orig = base.GetComponent<UnityEngine.AudioSource>().volume * GM.Option_Volume[0];
		if (!OnEvent)
		{
			Check_Distance();
		}
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			life_Timer += global::UnityEngine.Time.deltaTime;
			if (!isPlayStarted && !base.GetComponent<UnityEngine.AudioSource>().isPlaying)
			{
				base.GetComponent<UnityEngine.AudioSource>().Play();
				isPlayStarted = true;
			}
			if (isPaused)
			{
				Dist_Orig = Volume_Orig * GM.Option_Volume[0];
				base.GetComponent<UnityEngine.AudioSource>().volume = Dist_Orig * Dist_Var;
				isPaused = false;
				base.GetComponent<UnityEngine.AudioSource>().Play();
			}
			else if (isPlayStarted && life_Timer > 0.4f && !base.GetComponent<UnityEngine.AudioSource>().isPlaying)
			{
				Destroy_Self();
			}
			if (!OnEvent)
			{
				Check_Distance();
			}
		}
		else if (base.GetComponent<UnityEngine.AudioSource>().isPlaying)
		{
			base.GetComponent<UnityEngine.AudioSource>().Pause();
			isPaused = true;
		}
	}

	private void Check_Distance()
	{
		global::UnityEngine.Vector3 b = new global::UnityEngine.Vector3(global::UnityEngine.GameObject.Find("Main Camera").transform.position.x, global::UnityEngine.GameObject.Find("Main Camera").transform.position.y, 0f);
		distance = global::UnityEngine.Vector3.Distance(base.transform.position, b);
		if (distance < 20f)
		{
			Dist_Var = 1f;
		}
		else if (distance < 50f)
		{
			Dist_Var = 1f - (distance - 20f) / 30f;
			if (Dist_Var < 0.05f)
			{
				Dist_Var = 0.05f;
			}
		}
		else if (distance < 60f)
		{
			Dist_Var = 0.05f;
		}
		else
		{
			Dist_Var = 0f;
		}
		base.GetComponent<UnityEngine.AudioSource>().volume = Dist_Orig * Dist_Var;
	}

	private void Destroy_Self()
	{
		ReleaseToPool();
        //global::UnityEngine.Object.Destroy(base.gameObject);
    }

}
