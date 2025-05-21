public class Dust_Death : global::UnityEngine.MonoBehaviour
{
	private float Life_Timer;

	private float Size = 1f;

	private float Opacity = 1f;

	private float Speed = 20f;

	private global::UnityEngine.SpriteRenderer SR;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		SR = GetComponent<global::UnityEngine.SpriteRenderer>();
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
		Speed = global::UnityEngine.Mathf.Lerp(Speed, 0f, global::UnityEngine.Time.deltaTime * 2f);
		base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Speed);
		Size = global::UnityEngine.Mathf.Lerp(Size, 0.5f, global::UnityEngine.Time.deltaTime * 2f);
		base.transform.localScale = new global::UnityEngine.Vector3(Size, Size, 1f);
		if (Life_Timer > 0.2f)
		{
			Opacity = global::UnityEngine.Mathf.Lerp(Opacity, 0f, global::UnityEngine.Time.deltaTime * 5f);
			if (Opacity >= 0f)
			{
				SR.color = new global::UnityEngine.Color(1f, 1f, 1f, Opacity);
			}
		}
		if (Life_Timer > 1f)
		{
			Destroy_Self();
		}
	}

	private void Destroy_Self()
	{
		global::UnityEngine.Object.Destroy(base.gameObject);
	}
}
