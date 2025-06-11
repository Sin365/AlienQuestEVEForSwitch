public class Dust_Diamond : global::UnityEngine.MonoBehaviour
{
	private float Life_Timer;

	private float Speed = 20f;

	private float Rnd_Spd = 1f;

	private float Size = 1f;

	private float Rot;

	private float Opacity = 1f;

	private float Rnd_Opacity = 12f;

	public global::UnityEngine.Sprite spr_2;

	GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		Speed += (float)global::UnityEngine.Random.Range(-100, 100) * 0.05f;
		Rnd_Spd = 7f + (float)global::UnityEngine.Random.Range(-100, 100) * 0.01f;
		Rnd_Opacity += (float)global::UnityEngine.Random.Range(-100, 100) * 0.05f;
		Size = 0.3f + (float)global::UnityEngine.Random.Range(-10, 10) * 0.01f;
		base.transform.localScale = new global::UnityEngine.Vector3(Size, Size, 1f);
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			Life_Timer += global::UnityEngine.Time.deltaTime;
			if (Life_Timer > 0.3f)
			{
				Opacity = global::UnityEngine.Mathf.Lerp(Opacity, 0f, global::UnityEngine.Time.deltaTime * Rnd_Opacity);
				GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, Opacity);
				Speed = global::UnityEngine.Mathf.Lerp(Speed, 0.1f, global::UnityEngine.Time.deltaTime * Rnd_Spd);
				Size = global::UnityEngine.Mathf.Lerp(Speed, 0.1f, global::UnityEngine.Time.deltaTime * 7f);
			}
			base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Speed);
			if (Life_Timer > 0.6f)
			{
				Destroy_Self();
			}
		}
	}

	private void Destroy_Self()
	{
		global::UnityEngine.Object.Destroy(base.gameObject);
	}
}
