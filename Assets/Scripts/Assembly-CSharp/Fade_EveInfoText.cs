public class Fade_EveInfoText : global::UnityEngine.MonoBehaviour
{
	private int Fade_State = 1;

	private float Fade_Speed = 1f;

	private float Life_Timer;

	public global::UnityEngine.UI.Text txt_1;

	public global::UnityEngine.UI.Text txt_2;

	public global::UnityEngine.UI.Text txt_3;

	private global::UnityEngine.Color color_On = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_75 = new global::UnityEngine.Color(1f, 1f, 1f, 0.75f);

	private global::UnityEngine.Color color_Off = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		global::UnityEngine.UI.Text text = txt_3;
		global::UnityEngine.Color color = color_Off;
		txt_1.color = color;
		color = color;
		txt_2.color = color;
		text.color = color;
	}

	private void Update()
	{
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (Life_Timer > 1f && GM.EventState < 100)
		{
			Fade_State = 2;
			Fade_Speed = 5f;
		}
		if (Fade_State == 1)
		{
			txt_1.color = global::UnityEngine.Color.Lerp(txt_1.color, color_On, global::UnityEngine.Time.deltaTime * 0.6f);
			txt_3.color = global::UnityEngine.Color.Lerp(txt_3.color, color_75, global::UnityEngine.Time.deltaTime * 0.6f);
			if (Life_Timer > 4f)
			{
				Fade_State = 2;
			}
		}
		else
		{
			txt_1.color = global::UnityEngine.Color.Lerp(txt_1.color, color_Off, global::UnityEngine.Time.deltaTime * Fade_Speed);
			txt_3.color = global::UnityEngine.Color.Lerp(txt_3.color, color_Off, global::UnityEngine.Time.deltaTime * Fade_Speed);
			if (Life_Timer > 8f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
		txt_2.color = txt_1.color;
	}
}
