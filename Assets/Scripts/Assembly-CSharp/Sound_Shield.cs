public class Sound_Shield : global::UnityEngine.MonoBehaviour
{
	private float Life_Timer;

	private bool onActive = true;

	private float Volume_Orig = 1f;

	private float Vol_Target = 1f;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		Volume_Orig = base.GetComponent<UnityEngine.AudioSource>().volume;
		base.GetComponent<UnityEngine.AudioSource>().volume = base.GetComponent<UnityEngine.AudioSource>().volume * GM.Option_Volume[0];
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			Life_Timer += global::UnityEngine.Time.deltaTime;
			if (!base.GetComponent<UnityEngine.AudioSource>().isPlaying)
			{
				base.GetComponent<UnityEngine.AudioSource>().volume = Volume_Orig * GM.Option_Volume[0];
				base.GetComponent<UnityEngine.AudioSource>().Play();
			}
			if (!onActive)
			{
				float num = global::UnityEngine.Mathf.Lerp(base.GetComponent<UnityEngine.AudioSource>().volume, 0f, global::UnityEngine.Time.deltaTime * 20f);
				base.GetComponent<UnityEngine.AudioSource>().volume = num;
				Volume_Orig = num;
			}
		}
		else if (base.GetComponent<UnityEngine.AudioSource>().isPlaying)
		{
			base.GetComponent<UnityEngine.AudioSource>().Pause();
		}
	}

	private void Sound_Off()
	{
		onActive = false;
	}
}
