public class Dust_HealCross : global::UnityEngine.MonoBehaviour
{
	private float Life_Timer;

	private float Speed = 1f;

	private global::UnityEngine.Color color_Off = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	private global::UnityEngine.SpriteRenderer SR;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		SR = GetComponent<global::UnityEngine.SpriteRenderer>();
		Speed += (float)global::UnityEngine.Random.Range(-50, 50) * 0.01f;
		color_Off = new global::UnityEngine.Color(color_Off.r, color_Off.g, color_Off.b, 0f);
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			Life_Timer += global::UnityEngine.Time.deltaTime;
			base.transform.Translate(global::UnityEngine.Vector3.up * global::UnityEngine.Time.deltaTime * Speed);
			if (Life_Timer > 3.5f || SR.color.a < 0.02f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			else if (Life_Timer > 0.7f)
			{
				SR.color = global::UnityEngine.Color.Lerp(SR.color, color_Off, global::UnityEngine.Time.deltaTime * 3f);
			}
		}
	}
}
