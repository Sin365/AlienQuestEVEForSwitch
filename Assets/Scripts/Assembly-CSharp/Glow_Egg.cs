public class Glow_Egg : global::UnityEngine.MonoBehaviour
{
	private float size_Timer;

	private float color_Timer;

	private global::UnityEngine.Vector3 size_Orig;

	private global::UnityEngine.Vector3 size_Taget;

	private global::UnityEngine.Color color_Orig;

	private global::UnityEngine.Color color_Target;

	private global::UnityEngine.SpriteRenderer SR;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		SR = GetComponent<global::UnityEngine.SpriteRenderer>();
		color_Orig = (color_Target = SR.color);
		size_Orig = (size_Taget = base.transform.localScale);
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			size_Timer += global::UnityEngine.Time.deltaTime;
			color_Timer += global::UnityEngine.Time.deltaTime;
			if (size_Timer > 2.5f)
			{
				size_Timer = 0f;
				size_Taget = new global::UnityEngine.Vector3(size_Orig.x, size_Orig.y * global::UnityEngine.Random.Range(0.5f, 1.5f), 1f);
			}
			if (color_Timer > 1.5f)
			{
				color_Timer = 0f;
				color_Target = new global::UnityEngine.Color(color_Orig.r, color_Orig.g, color_Orig.b, global::UnityEngine.Random.Range(0.2f, 0.5f));
			}
			base.transform.localScale = global::UnityEngine.Vector3.Lerp(base.transform.localScale, size_Taget, global::UnityEngine.Time.deltaTime * 2.5f);
			SR.color = global::UnityEngine.Color.Lerp(SR.color, color_Target, global::UnityEngine.Time.deltaTime * 2f);
		}
	}
}
