public class Mother_FireDot : global::UnityEngine.MonoBehaviour
{
	private float Life_Timer;

	private float Size = 2.5f;

	private float Opacity = 1f;

	private float Speed = 2f;

	private global::UnityEngine.SpriteRenderer SR;

	GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		SR = GetComponent<global::UnityEngine.SpriteRenderer>();
		Speed += (float)global::UnityEngine.Random.Range(-10, 10) * 0.1f;
		Size += (float)global::UnityEngine.Random.Range(-20, 20) * 0.01f;
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			Life_Timer += global::UnityEngine.Time.deltaTime;
			base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Speed);
			Opacity = global::UnityEngine.Mathf.Lerp(Opacity, 0f, global::UnityEngine.Time.deltaTime * 8f);
			if (Opacity >= 0f)
			{
				SR.color = new global::UnityEngine.Color(SR.color.r, SR.color.g, SR.color.b, Opacity);
			}
			Size = global::UnityEngine.Mathf.Lerp(Size, 0.2f, global::UnityEngine.Time.deltaTime * 10f);
			base.transform.localScale = new global::UnityEngine.Vector3(Size, Size, 1f);
			if (Life_Timer > 0.5f)
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
