public class Mon_Fire_Dust : global::UnityEngine.MonoBehaviour
{
	public int Type;

	private float Life_Timer;

	private float Speed = 2f;

	private float Opacity = 1f;

	private global::UnityEngine.Vector3 Size_Orig;

	private global::UnityEngine.Vector3 Size_Target;

	private global::UnityEngine.SpriteRenderer SR;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		SR = GetComponent<global::UnityEngine.SpriteRenderer>();
		if (Type == 10)
		{
			Speed = -1f;
		}
		else
		{
			Speed = global::UnityEngine.Random.Range(-2f, -0.5f);
		}
		float num = global::UnityEngine.Random.Range(0.5f, 1f);
		Size_Orig = new global::UnityEngine.Vector3(base.transform.localScale.x * num, base.transform.localScale.y * num, 1f);
		base.transform.localScale = Size_Orig;
		Size_Target = new global::UnityEngine.Vector3(Size_Orig.x * 0.1f, Size_Orig.y * 0.1f, 1f);
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			Life_Timer += global::UnityEngine.Time.deltaTime;
			base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Speed);
			if (Type == 3)
			{
				Opacity = global::UnityEngine.Mathf.Lerp(Opacity, 0f, global::UnityEngine.Time.deltaTime * 9f);
				base.transform.localScale = global::UnityEngine.Vector3.Lerp(base.transform.localScale, Size_Target, global::UnityEngine.Time.deltaTime * 3f);
			}
			else if (Type == 4)
			{
				Opacity = global::UnityEngine.Mathf.Lerp(Opacity, 0f, global::UnityEngine.Time.deltaTime * 7f);
				base.transform.localScale = global::UnityEngine.Vector3.Lerp(base.transform.localScale, Size_Target, global::UnityEngine.Time.deltaTime * 3f);
			}
			else if (Type == 10)
			{
				Opacity = global::UnityEngine.Mathf.Lerp(Opacity, 0f, global::UnityEngine.Time.deltaTime * 5f);
				base.transform.localScale = global::UnityEngine.Vector3.Lerp(base.transform.localScale, Size_Target, global::UnityEngine.Time.deltaTime * 3f);
			}
			else
			{
				Opacity = global::UnityEngine.Mathf.Lerp(Opacity, 0f, global::UnityEngine.Time.deltaTime * 5f);
				base.transform.localScale = global::UnityEngine.Vector3.Lerp(base.transform.localScale, Size_Target, global::UnityEngine.Time.deltaTime * 2f);
			}
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
