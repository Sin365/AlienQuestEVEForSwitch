using UnityEngine;
using UnityEngine.UI;

public class Sound_Moan : AxiSoundBase
{
	private float life_Timer;

	private bool isPlayStarted;

	private bool onEnd;
	private float mSrc_volume;
	private AudioSource mAS;

	GameManager GM => GameManager.instance;

	public override void Init()
	{
		life_Timer = default;
		isPlayStarted = default;
		onEnd = default;
		mSrc_volume = default;
		Start();
	}

	public override void ReleaseToPool()
	{
		base.GetComponent<UnityEngine.AudioSource>().Stop();
		base.GetComponent<UnityEngine.AudioSource>().volume = mSrc_volume;
		AxiSoundPool.ReleaseSound(this);
	}

	private void Awake()
	{
		mAS = base.GetComponent<UnityEngine.AudioSource>();
		mSrc_volume = mAS.volume;
	}
	private void Start()
	{
		//¸´Ô­
		mAS.volume = mSrc_volume;
		if (GM != null)
		{
			//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
			mAS.volume = mAS.volume * GM.Option_Volume[0];
		}
		else
		{
			mAS.volume = mAS.volume * AxiPlayerPrefs.GetFloat("SoundVolume");
		}
	}

	private void OnDestroy()
	{
		AxiSoundPool.CheckNeedRemoveFormPool(this);
	}
	private void Update()
	{
		life_Timer += global::UnityEngine.Time.deltaTime;
		if (!isPlayStarted && !mAS.isPlaying)
		{
			mAS.Play();
			isPlayStarted = true;
		}
		if (onEnd)
		{
			mAS.volume -= global::UnityEngine.Time.deltaTime * 2f;
			if (mAS.volume <= 0f)
			{
				global::UnityEngine.Debug.Log("Moan Del");
				Destroy_Self();
			}
		}
		if (isPlayStarted && life_Timer > 0.4f && !mAS.isPlaying)
		{
			Destroy_Self();
		}
	}

	private void Set_End()
	{
		onEnd = true;
		Destroy_Self();
	}

	private void Destroy_Self()
	{
		ReleaseToPool();
		//global::UnityEngine.Object.Destroy(base.gameObject);
	}
}
