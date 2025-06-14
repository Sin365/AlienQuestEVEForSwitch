using UnityEngine;

public class Sound : AxiSoundBase
{
	private float life_Timer;

	private bool isPaused;

	private bool isPlayStarted;
	private AudioSource mAS;
	private float Volume_Orig = 1f;
	private GameObject mMC => Camera.main.gameObject;
	private float Dist_Orig = 1f;

	private float Dist_Var = 1f;

	private float distance;

	private bool OnEvent;
	private GameManager GM => GameManager.instance;

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
		base.GetComponent<UnityEngine.AudioSource>().Stop();
		base.GetComponent<UnityEngine.AudioSource>().volume = Volume_Orig;
		AxiSoundPool.ReleaseSound(this);
    }

	private void Awake()
	{
		mAS = base.GetComponent<UnityEngine.AudioSource>();
		Volume_Orig = mAS.volume;
	}

	private void Start()
	{
		//¸´Ô­
		mAS.volume = Volume_Orig;
		//mMC = UnityEngine.Camera.main;
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		Dist_Orig = base.GetComponent<UnityEngine.AudioSource>().volume * GM.Option_Volume[0];
		if (!OnEvent)
		{
			Check_Distance();
		}
	}

	private void OnDestroy()
	{
		AxiSoundPool.CheckNeedRemoveFormPool(this);
	}
	private void Update()
	{
		if (!GM.Paused)
		{
			life_Timer += global::UnityEngine.Time.deltaTime;
			if (!isPlayStarted && !mAS.isPlaying)
			{
				mAS.Play();
				isPlayStarted = true;
			}
			if (isPaused)
			{
				Dist_Orig = Volume_Orig * GM.Option_Volume[0];
				mAS.volume = Dist_Orig * Dist_Var;
				isPaused = false;
				mAS.Play();
			}
			else if (isPlayStarted && life_Timer > 0.4f && !mAS.isPlaying)
			{
				Destroy_Self();
			}
			if (!OnEvent)
			{
				Check_Distance();
			}
		}
		else if (mAS.isPlaying)
		{
			mAS.Pause();
			isPaused = true;
		}
	}

	private void Check_Distance()
	{
		global::UnityEngine.Vector3 b = new global::UnityEngine.Vector3(mMC.transform.position.x, mMC.transform.position.y, 0f);
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
		mAS.volume = Dist_Orig * Dist_Var;
	}

	private void Destroy_Self()
	{
		ReleaseToPool();
        //global::UnityEngine.Object.Destroy(base.gameObject);
    }

}
