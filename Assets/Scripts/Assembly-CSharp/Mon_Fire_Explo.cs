public class Mon_Fire_Explo : global::UnityEngine.MonoBehaviour
{
	private float Life_Timer;

	private float Size = 1f;

	private float Target_Size = 4f;

	private float Opacity = 1f;

	private global::UnityEngine.Color color_Target;

	private global::UnityEngine.Color color_Back;

	public global::UnityEngine.SpriteRenderer Spr_Back;

	private global::UnityEngine.SpriteRenderer Spr;

	GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		Spr = GetComponent<global::UnityEngine.SpriteRenderer>();
		Size = base.transform.localScale.x;
		Target_Size = Size * 3f;
		color_Target = new global::UnityEngine.Color(Spr.color.r, Spr.color.g, Spr.color.b, 0f);
		color_Back = new global::UnityEngine.Color(Spr_Back.color.r, Spr_Back.color.g, Spr_Back.color.b, 0f);
	}

	public void Random_Size(float size_Min, float sie_Max)
	{
		Size = global::UnityEngine.Random.Range(size_Min, sie_Max);
		Target_Size = Size * 3f;
	}

	public void Random_Pos(float pos)
	{
		base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Random.Range(0f - pos, pos));
		base.transform.Translate(global::UnityEngine.Vector3.up * global::UnityEngine.Random.Range(0f - pos, pos));
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			Life_Timer += global::UnityEngine.Time.deltaTime;
			Size = global::UnityEngine.Mathf.Lerp(Size, Target_Size, global::UnityEngine.Time.deltaTime * 3f);
			base.transform.localScale = new global::UnityEngine.Vector3(Size, Size, 1f);
			if (Life_Timer > 0.1f)
			{
				Spr.color = global::UnityEngine.Color.Lerp(Spr.color, color_Target, global::UnityEngine.Time.deltaTime * 8f);
				Spr_Back.color = global::UnityEngine.Color.Lerp(Spr_Back.color, color_Back, global::UnityEngine.Time.deltaTime * 5f);
			}
			if (Spr.color.a <= 0.01f || Life_Timer > 1f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}
}
