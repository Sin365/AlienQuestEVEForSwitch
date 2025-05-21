public class Effect_Atk_Lag : global::UnityEngine.MonoBehaviour
{
	private float Life_Timer;

	private int Room_Num;

	private global::UnityEngine.Color color_Target;

	private global::UnityEngine.SpriteRenderer SR;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		SR = GetComponent<global::UnityEngine.SpriteRenderer>();
		Room_Num = GM.Room_Num;
		color_Target = new global::UnityEngine.Color(SR.color.r, SR.color.g, SR.color.b, 0f);
	}

	private void Update()
	{
		if (Room_Num != GM.Room_Num)
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
		if (!GM.Paused && !GM.onGatePass)
		{
			Life_Timer += global::UnityEngine.Time.deltaTime;
			SR.color = global::UnityEngine.Color.Lerp(SR.color, color_Target, global::UnityEngine.Time.deltaTime * 10f);
			if (SR.color.a < 0.05f || Life_Timer > 3f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}
}
