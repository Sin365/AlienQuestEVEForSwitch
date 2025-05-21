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
				if (!base.audio.isPlaying)
				{
					base.audio.Play();
				}
				base.audio.volume = Glow_rolling.GetComponent<global::UnityEngine.SpriteRenderer>().color.a * GM.Option_Volume[0];
			}
			else if (Effect_Spin.GetComponent<global::UnityEngine.SpriteRenderer>().sprite != null)
			{
				if (!base.audio.isPlaying)
				{
					base.audio.Play();
				}
				base.audio.volume = global::UnityEngine.Mathf.Lerp(base.audio.volume, 0.6f * GM.Option_Volume[0], global::UnityEngine.Time.deltaTime * 2f);
			}
			else if (base.audio.isPlaying)
			{
				if (base.audio.volume > 0f)
				{
					base.audio.volume -= global::UnityEngine.Time.deltaTime * 2f;
				}
				else
				{
					base.audio.Pause();
				}
			}
			else
			{
				base.audio.volume = 0f;
			}
		}
		else
		{
			base.audio.Pause();
		}
	}
}
