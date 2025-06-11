public class Fade_MissionComplete : global::UnityEngine.MonoBehaviour
{
	private int Fade_State;

	private float Fade_Speed = 10f;

	private float Life_Timer;

	private bool onRed;

	public global::UnityEngine.UI.Text txtTitle;

	public global::UnityEngine.UI.Text txt_1;

	public global::UnityEngine.UI.Text txt_2;

	public global::UnityEngine.UI.Image img_1;

	public global::UnityEngine.UI.Image bg_1;

	public global::UnityEngine.UI.Image bg_2;

	private global::UnityEngine.Color color_On = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_Off = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		txtTitle.color = color_Off;
		txt_1.color = color_Off;
		txt_2.color = color_Off;
		img_1.color = color_Off;
		bg_1.color = color_Off;
		bg_2.color = color_Off;
		GetComponent<global::UnityEngine.RectTransform>().parent = global::UnityEngine.GameObject.Find("UI Canvas").GetComponent<global::UnityEngine.RectTransform>();
		GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(-704f, 126f, 0f);
		GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(0.8f, 0.8f, 1f);
	}

	public void Set_Text(string text_1, string text_2, string text_3)
	{
		txtTitle.text = text_1;
		txt_1.text = text_2;
		txt_2.text = text_3;
		Fade_State = 1;
		if (text_3 != string.Empty)
		{
			onRed = true;
		}
	}

	private void Update()
	{
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (Fade_State == 1)
		{
			txtTitle.color = global::UnityEngine.Color.Lerp(txtTitle.color, color_On, global::UnityEngine.Time.deltaTime * Fade_Speed);
			txt_1.color = txtTitle.color;
			img_1.color = txtTitle.color;
			bg_1.color = global::UnityEngine.Color.Lerp(bg_1.color, new global::UnityEngine.Color(1f, 1f, 1f, 0.2f), global::UnityEngine.Time.deltaTime * Fade_Speed);
			if (onRed)
			{
				txt_2.color = global::UnityEngine.Color.Lerp(txt_2.color, new global::UnityEngine.Color(0.95f, 1f, 0f, 1f), global::UnityEngine.Time.deltaTime * Fade_Speed);
				bg_2.color = global::UnityEngine.Color.Lerp(bg_2.color, new global::UnityEngine.Color(1f, 0f, 0f, 1f), global::UnityEngine.Time.deltaTime * Fade_Speed);
			}
			if (Life_Timer > 2.5f)
			{
				Fade_State = 2;
				Fade_Speed = 3f;
			}
		}
		else if (Fade_State > 1)
		{
			txtTitle.color = global::UnityEngine.Color.Lerp(txtTitle.color, color_Off, global::UnityEngine.Time.deltaTime * Fade_Speed);
			txt_1.color = txtTitle.color;
			img_1.color = txtTitle.color;
			bg_1.color = global::UnityEngine.Color.Lerp(bg_1.color, new global::UnityEngine.Color(1f, 1f, 1f, 0f), global::UnityEngine.Time.deltaTime * Fade_Speed);
			if (onRed)
			{
				txt_2.color = global::UnityEngine.Color.Lerp(txt_2.color, new global::UnityEngine.Color(0.95f, 1f, 0f, 0f), global::UnityEngine.Time.deltaTime * Fade_Speed);
				bg_2.color = global::UnityEngine.Color.Lerp(bg_2.color, new global::UnityEngine.Color(1f, 0f, 0f, 0f), global::UnityEngine.Time.deltaTime * Fade_Speed);
			}
			if (txtTitle.color.a < 0.04f || Life_Timer > 5f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}
}
