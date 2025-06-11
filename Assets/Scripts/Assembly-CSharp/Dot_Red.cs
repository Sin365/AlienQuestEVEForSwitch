public class Dot_Red : global::UnityEngine.MonoBehaviour
{
	private float Life_Timer;

	private float Size = 2.5f;

	private float Opacity = 1f;

	private float Speed = 1f;

	private int facingRight = 1;

	private global::UnityEngine.SpriteRenderer SR;

	GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		SR = GetComponent<global::UnityEngine.SpriteRenderer>();
		if (base.transform.localScale.x < 0f)
		{
			facingRight = -1;
		}
		Speed += (float)global::UnityEngine.Random.Range(-10, 10) * 0.1f;
		Size += (float)global::UnityEngine.Random.Range(-20, 20) * 0.01f;
		base.transform.localScale = new global::UnityEngine.Vector3(2f, 2f, 1f);
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			Life_Timer += global::UnityEngine.Time.deltaTime;
			base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Speed * facingRight);
			Opacity = global::UnityEngine.Mathf.Lerp(Opacity, 0f, global::UnityEngine.Time.deltaTime * 2f);
			if (Opacity >= 0f)
			{
				SR.color = new global::UnityEngine.Color(1f, 1f, 1f, Opacity);
			}
			if (Life_Timer > 3f || Opacity <= 0f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}
}
