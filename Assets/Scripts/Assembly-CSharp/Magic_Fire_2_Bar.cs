public class Magic_Fire_2_Bar : global::UnityEngine.MonoBehaviour
{
	private float Life_Timer;

	private float Speed = 12f;

	private global::UnityEngine.Vector3 size_Off = new global::UnityEngine.Vector3(0.2f, 1f, 1f);

	private global::UnityEngine.Color color_Off;

	private global::UnityEngine.SpriteRenderer SR;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		SR = GetComponent<global::UnityEngine.SpriteRenderer>();
		Speed += (float)global::UnityEngine.Random.Range(-100, 100) * 0.1f;
		color_Off = new global::UnityEngine.Color(SR.color.r, SR.color.g, SR.color.b, 0f);
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			Life_Timer += global::UnityEngine.Time.deltaTime;
			SR.color = global::UnityEngine.Color.Lerp(SR.color, color_Off, global::UnityEngine.Time.deltaTime * 7f);
			Speed = global::UnityEngine.Mathf.Lerp(Speed, 0f, global::UnityEngine.Time.deltaTime * 7f);
			base.transform.Translate(global::UnityEngine.Vector3.up * global::UnityEngine.Time.deltaTime * Speed);
			if (Life_Timer > 3f || SR.color.a < 0.005f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}
}
