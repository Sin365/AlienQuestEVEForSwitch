public class Event_Edge_Glow : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.SpriteRenderer Glow_L_1;

	public global::UnityEngine.SpriteRenderer Glow_L_2;

	public global::UnityEngine.SpriteRenderer Glow_R_1;

	public global::UnityEngine.SpriteRenderer Glow_R_2;

	public global::UnityEngine.SpriteRenderer Glow_Bottom;

	private float Glow_L_Timer;

	private float Glow_R_Timer;

	private global::UnityEngine.Color color_L;

	private global::UnityEngine.Color color_R;

	private global::UnityEngine.Color color_Bottom;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		Glow_L_Timer = global::UnityEngine.Random.Range(0f, 5f);
		Glow_R_Timer = global::UnityEngine.Random.Range(0f, 5f);
		color_L = Glow_L_1.color;
		color_R = Glow_R_1.color;
		color_Bottom = Glow_Bottom.color;
		color_Bottom = new global::UnityEngine.Color(color_Bottom.r, color_Bottom.g, color_Bottom.b, 0.25f);
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			Glow_L_Timer += global::UnityEngine.Time.deltaTime * 0.5f;
			Glow_R_Timer += global::UnityEngine.Time.deltaTime * 0.5f;
			Glow_L_1.color = new global::UnityEngine.Color(color_L.r, color_L.g, color_L.b, 0.08f + global::UnityEngine.Mathf.Sin(Glow_L_Timer) * 0.04f);
			Glow_R_1.color = new global::UnityEngine.Color(color_R.r, color_R.g, color_R.b, 0.08f + global::UnityEngine.Mathf.Sin(Glow_R_Timer) * 0.04f);
			Glow_L_2.color = Glow_L_1.color;
			Glow_R_2.color = Glow_R_1.color;
			if (GM.Get_Event(3) && GM.Get_Event(15))
			{
				Glow_Bottom.color = global::UnityEngine.Color.Lerp(Glow_Bottom.color, color_Bottom, global::UnityEngine.Time.deltaTime);
			}
		}
	}
}
