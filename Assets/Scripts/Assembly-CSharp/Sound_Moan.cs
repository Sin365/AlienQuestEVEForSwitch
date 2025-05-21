public class Sound_Moan : global::UnityEngine.MonoBehaviour
{
	private float life_Timer;

	private bool isPlayStarted;

	private bool onEnd;

	private GameManager GM;

	private void Start()
	{
		if (global::UnityEngine.GameObject.Find("GameManager") != null)
		{
			GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
			base.GetComponent<UnityEngine.AudioSource>().volume = base.GetComponent<UnityEngine.AudioSource>().volume * GM.Option_Volume[0];
		}
		else
		{
			base.GetComponent<UnityEngine.AudioSource>().volume = base.GetComponent<UnityEngine.AudioSource>().volume * global::UnityEngine.PlayerPrefs.GetFloat("SoundVolume");
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
		if (onEnd)
		{
			base.GetComponent<UnityEngine.AudioSource>().volume -= global::UnityEngine.Time.deltaTime * 2f;
			if (base.GetComponent<UnityEngine.AudioSource>().volume <= 0f)
			{
				global::UnityEngine.Debug.Log("Moan Del");
				Destroy_Self();
			}
		}
		if (isPlayStarted && life_Timer > 0.4f && !base.GetComponent<UnityEngine.AudioSource>().isPlaying)
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
		global::UnityEngine.Object.Destroy(base.gameObject);
	}
}
