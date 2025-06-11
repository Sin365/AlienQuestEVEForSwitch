using UnityEngine;

public class Sound_Inv : AxiSoundBase
{
    private float life_Timer;
    private bool isPlayStarted;

    GameManager GM => GameManager.instance;
    public override string resourceName { get; set; }

    public override void Init()
    {
        life_Timer = default;
        isPlayStarted = default;
        Start();
    }

    public override void ReleaseToPool()
    {
        base.GetComponent<UnityEngine.AudioSource>().Stop();
        AxiSoundPool.ReleaseSound(this);
    }
    void OnEnable()
    {
        Debug.Log($"[AxiSoundPool]Sound_Inv Enable");
    }
    private void Start()
    {
        if (GM != null)
        {
            //GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
            base.GetComponent<UnityEngine.AudioSource>().volume = base.GetComponent<UnityEngine.AudioSource>().volume * GM.Option_Volume[0];
        }
        else
        {
            base.GetComponent<UnityEngine.AudioSource>().volume = base.GetComponent<UnityEngine.AudioSource>().volume * AxiPlayerPrefs.GetFloat("SoundVolume");
        }
    }


    private void Update()
    {
        life_Timer += global::UnityEngine.Time.deltaTime;
        if (!isPlayStarted && !base.GetComponent<UnityEngine.AudioSource>().isPlaying)
        {   
            base.GetComponent<UnityEngine.AudioSource>().Play();
            isPlayStarted = true;
        }
        if (isPlayStarted && life_Timer > 0.4f && !base.GetComponent<UnityEngine.AudioSource>().isPlaying)
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
