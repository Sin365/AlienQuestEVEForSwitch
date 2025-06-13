public class Sound_Shield : AxiSoundBase
{
    private float Life_Timer;
    private bool onActive = true;
    private float Volume_Orig = 1f;
    private float Vol_Target = 1f;

    GameManager GM => GameManager.instance;
	UnityEngine.AudioSource mAS;

    public override void Init()
    {
        Life_Timer = default;
        onActive = true;
        Volume_Orig = 1f;
        Vol_Target = 1f;
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
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		mAS.volume = mAS.volume * GM.Option_Volume[0];
    }

	private void OnDestroy()
	{
        AxiSoundPool.CheckNeedRemoveFormPool(this);
	}

	private void Update()
    {
        if (!GM.Paused)
        {
            Life_Timer += global::UnityEngine.Time.deltaTime;
            if (!mAS.isPlaying)
            {
                mAS.volume = Volume_Orig * GM.Option_Volume[0];
				mAS.Play();
            }
            if (!onActive)
            {
                float num = global::UnityEngine.Mathf.Lerp(mAS.volume, 0f, global::UnityEngine.Time.deltaTime * 20f);
                mAS.volume = num;
                Volume_Orig = num;
            }
        }
        else if (mAS.isPlaying)
        {
            mAS.Pause();
        }
    }

    private void Sound_Off()
    {
        onActive = false;
        ReleaseToPool();
    }
}
