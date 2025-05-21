public class Circle_Dust : global::UnityEngine.MonoBehaviour
{
	private float Life_Timer;

	private float Speed = 20f;

	private global::UnityEngine.Vector3 size_Off = new global::UnityEngine.Vector3(0.01f, 1.3f, 1f);

	private global::UnityEngine.Color color_Off = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	public global::UnityEngine.Sprite spr_2;

	private global::UnityEngine.SpriteRenderer SR;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		SR = GetComponent<global::UnityEngine.SpriteRenderer>();
		Speed += (float)global::UnityEngine.Random.Range(-100, 100) * 0.1f;
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			Life_Timer += global::UnityEngine.Time.deltaTime;
			GetComponent<global::UnityEngine.SpriteRenderer>().color = global::UnityEngine.Color.Lerp(GetComponent<global::UnityEngine.SpriteRenderer>().color, color_Off, global::UnityEngine.Time.deltaTime * 2f);
			base.transform.localScale = global::UnityEngine.Vector3.Lerp(base.transform.localScale, size_Off, global::UnityEngine.Time.deltaTime * 1f);
			Speed = global::UnityEngine.Mathf.Lerp(Speed, 5f, global::UnityEngine.Time.deltaTime * 7f);
			base.transform.Translate(global::UnityEngine.Vector3.up * global::UnityEngine.Time.deltaTime * Speed);
			if (Life_Timer > 2f || GetComponent<global::UnityEngine.SpriteRenderer>().color.a < 0.01f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}
}
