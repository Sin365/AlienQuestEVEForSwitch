public class Magic_Fire_1B_Dust : global::UnityEngine.MonoBehaviour
{
	private float Life_Timer;

	private float Speed = 2f;

	private float Opacity = 1f;

	private global::UnityEngine.Vector3 Size_Target;

	private global::UnityEngine.SpriteRenderer SR;

	GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		SR = GetComponent<global::UnityEngine.SpriteRenderer>();
		Speed = global::UnityEngine.Random.Range(-2f, -0.5f) * base.transform.localScale.x;
		float num = global::UnityEngine.Random.Range(0.6f, 1f);
		base.transform.localScale = new global::UnityEngine.Vector3(base.transform.localScale.x * num, base.transform.localScale.y * num, 1f);
		Size_Target = new global::UnityEngine.Vector3(base.transform.localScale.x * 0.1f, base.transform.localScale.x * 0.1f, 1f);
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			Life_Timer += global::UnityEngine.Time.deltaTime;
			base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Speed);
			base.transform.localScale = global::UnityEngine.Vector3.Lerp(base.transform.localScale, Size_Target, global::UnityEngine.Time.deltaTime * 1f);
			Opacity = global::UnityEngine.Mathf.Lerp(Opacity, 0f, global::UnityEngine.Time.deltaTime * 10f);
			if (Opacity >= 0f)
			{
				SR.color = new global::UnityEngine.Color(SR.color.r, SR.color.g, SR.color.b, Opacity);
			}
			if (Opacity < 0.02f || Life_Timer > 0.8f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}
}
