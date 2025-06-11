public class Sound_Gravity : global::UnityEngine.MonoBehaviour
{
	private float Life_Timer;

	private float Volume_Orig = 1f;

	private float Dist_Orig = 1f;

	private float Dist_Var = 1f;

	private float Distance;

    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		Volume_Orig = base.GetComponent<UnityEngine.AudioSource>().volume;
		Dist_Orig = base.GetComponent<UnityEngine.AudioSource>().volume * GM.Option_Volume[0];
		Check_Distance();
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			Life_Timer += global::UnityEngine.Time.deltaTime;
			if (!base.GetComponent<UnityEngine.AudioSource>().isPlaying)
			{
				Dist_Orig = Volume_Orig * GM.Option_Volume[0];
				base.GetComponent<UnityEngine.AudioSource>().volume = Dist_Orig * Dist_Var;
				base.GetComponent<UnityEngine.AudioSource>().Play();
			}
			if (Life_Timer > 3.5f)
			{
				base.GetComponent<UnityEngine.AudioSource>().volume = global::UnityEngine.Mathf.Lerp(base.GetComponent<UnityEngine.AudioSource>().volume, 0f, global::UnityEngine.Time.deltaTime * 20f);
			}
			else
			{
				Check_Distance();
			}
		}
		else if (base.GetComponent<UnityEngine.AudioSource>().isPlaying)
		{
			base.GetComponent<UnityEngine.AudioSource>().Pause();
		}
	}

	private void Check_Distance()
	{
		Distance = global::UnityEngine.Vector3.Distance(base.transform.position, global::UnityEngine.GameObject.Find("Main Camera").transform.position);
		if (Distance < 20f)
		{
			Dist_Var = 1f;
		}
		else if (Distance < 40f)
		{
			Dist_Var = 1f - (Distance - 20f) / 20f;
			if (Dist_Var < 0.05f)
			{
				Dist_Var = 0.05f;
			}
		}
		else if (Distance < 50f)
		{
			Dist_Var = 0.05f;
		}
		else
		{
			Dist_Var = 0f;
		}
		base.GetComponent<UnityEngine.AudioSource>().volume = Dist_Orig * Dist_Var;
	}
}
