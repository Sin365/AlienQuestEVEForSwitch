public class Dust_Rolling : global::UnityEngine.MonoBehaviour
{
	private float Life_Timer;

	private float Speed = 10f;

	private float Size = 0.5f;

	private float Opacity = 1f;

	public global::UnityEngine.Sprite spr_2;

	private global::UnityEngine.SpriteRenderer SR;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		SR = GetComponent<global::UnityEngine.SpriteRenderer>();
		if (global::UnityEngine.Random.Range(0, 20) > 10)
		{
			SR.sprite = spr_2;
		}
		Speed += (float)global::UnityEngine.Random.Range(-100, 100) * 0.1f;
		Size += (float)global::UnityEngine.Random.Range(-10, 10) * 0.01f;
		base.transform.localScale = new global::UnityEngine.Vector3(Size, Size, 1f);
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			Life_Timer += global::UnityEngine.Time.deltaTime;
			Opacity = global::UnityEngine.Mathf.Lerp(Opacity, 0f, global::UnityEngine.Time.deltaTime * 12f);
			GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, Opacity);
			Size = global::UnityEngine.Mathf.Lerp(Size, 0.1f, global::UnityEngine.Time.deltaTime * 5f);
			base.transform.localScale = new global::UnityEngine.Vector3(Size, Size, 1f);
			Speed = global::UnityEngine.Mathf.Lerp(Speed, 0f, global::UnityEngine.Time.deltaTime * 7f);
			base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Speed);
			if (Life_Timer > 1f || Opacity <= 0f)
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
