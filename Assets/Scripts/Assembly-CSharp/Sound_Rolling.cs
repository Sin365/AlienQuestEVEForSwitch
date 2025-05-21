public class Sound_Rolling : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject Effect_Spin;

	public global::UnityEngine.GameObject Glow_rolling;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			if (Glow_rolling.GetComponent<global::UnityEngine.SpriteRenderer>().color.a > 0f)
			{
				if (!base.GetComponent<UnityEngine.AudioSource>().isPlaying)
				{
					base.GetComponent<UnityEngine.AudioSource>().Play();
				}
				base.GetComponent<UnityEngine.AudioSource>().volume = Glow_rolling.GetComponent<global::UnityEngine.SpriteRenderer>().color.a * GM.Option_Volume[0];
			}
			else if (Effect_Spin.GetComponent<global::UnityEngine.SpriteRenderer>().sprite != null)
			{
				if (!base.GetComponent<UnityEngine.AudioSource>().isPlaying)
				{
					base.GetComponent<UnityEngine.AudioSource>().Play();
				}
				base.GetComponent<UnityEngine.AudioSource>().volume = global::UnityEngine.Mathf.Lerp(base.GetComponent<UnityEngine.AudioSource>().volume, 0.6f * GM.Option_Volume[0], global::UnityEngine.Time.deltaTime * 2f);
			}
			else if (base.GetComponent<UnityEngine.AudioSource>().isPlaying)
			{
				if (base.GetComponent<UnityEngine.AudioSource>().volume > 0f)
				{
					base.GetComponent<UnityEngine.AudioSource>().volume -= global::UnityEngine.Time.deltaTime * 2f;
				}
				else
				{
					base.GetComponent<UnityEngine.AudioSource>().Pause();
				}
			}
			else
			{
				base.GetComponent<UnityEngine.AudioSource>().volume = 0f;
			}
		}
		else
		{
			base.GetComponent<UnityEngine.AudioSource>().Pause();
		}
	}
}
