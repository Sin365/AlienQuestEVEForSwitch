using UnityEngine;

public class Sound_Inv : AxiSoundBase
{
    private float life_Timer;
    private bool isPlayStarted;
	private float mSrc_volume;
	private AudioSource mAS;

	GameManager GM => GameManager.instance;

    public override void Init()
    {
        life_Timer = default;
        isPlayStarted = default;
        Start();
    }

    public override void ReleaseToPool()
	{
		base.GetComponent<UnityEngine.AudioSource>().Stop();
		base.GetComponent<UnityEngine.AudioSource>().volume = mSrc_volume;
		AxiSoundPool.ReleaseSound(this);
    }
    void OnEnable()
    {
        Debug.Log($"[AxiSoundPool]Sound_Inv Enable");
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
        if (isPlayStarted && life_Timer > 0.4f && !mAS.isPlaying)
        {
            Destroy_Self();
        }
    }

    private void Destroy_Self()
    {
        ReleaseToPool();
        //global::UnityEngine.Object.Destroy(base.gameObject);
    }
}
