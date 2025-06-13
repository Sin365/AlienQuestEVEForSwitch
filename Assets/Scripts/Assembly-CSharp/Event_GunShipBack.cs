public class Event_GunShipBack : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.SpriteRenderer[] Glows;

	public global::UnityEngine.GameObject Sound_Flyby;

	private bool onSound;

	private float Life_Timer;

	private void Start()
	{
		GetComponent<global::UnityEngine.Animator>().speed = 0f;
	}

	private void Update()
	{
		Life_Timer += global::UnityEngine.Time.deltaTime;
		Glows[0].color = new global::UnityEngine.Color(1f, 1f, 1f, 0.8f + (1f + global::UnityEngine.Mathf.Sin(Life_Timer * 32f)) * 0.1f);
		for (int i = 1; i < Glows.Length; i++)
		{
			Glows[i].color = Glows[0].color;
		}
		if (Life_Timer > 2.5f)
		{
			GetComponent<global::UnityEngine.Animator>().speed = 1.2f;
			if (!onSound)
			{
				onSound = true;
				AxiSoundPool.AddSoundForPosRot(Sound_Flyby, base.transform.position, base.transform.rotation);
			}
		}
	}

	private void End_Fly()
	{
		global::UnityEngine.Object.Destroy(base.gameObject);
	}
}
