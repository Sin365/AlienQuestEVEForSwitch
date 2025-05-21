public class Fade_MissionText : global::UnityEngine.MonoBehaviour
{
	private int Fade_State = 1;

	private float Fade_Speed = 1.2f;

	private float Life_Timer;

	public global::UnityEngine.UI.Text txtTitle;

	public global::UnityEngine.UI.Text txt_1;

	public global::UnityEngine.UI.Text txt_2;

	public global::UnityEngine.UI.Text txt_3;

	public global::UnityEngine.UI.Image bg_1;

	public global::UnityEngine.UI.Image bg_2;

	public global::UnityEngine.UI.Image bg_3;

	private global::UnityEngine.Color color_On = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_Off = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	private global::UnityEngine.Color brown_On = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Vector3 PosOrig = new global::UnityEngine.Vector3(1500f, -360f, 0f);

	private global::UnityEngine.Vector3 PosTarget = new global::UnityEngine.Vector3(650f, -360f, 0f);

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		brown_On = txt_1.color;
		Language_MenuItem component = global::UnityEngine.GameObject.Find("Menu").GetComponent<Language_MenuItem>();
		if (global::UnityEngine.PlayerPrefs.GetInt("Language_Num") > 0)
		{
			txtTitle.text = component.MissionBriefing(0, 1);
			txt_1.text = component.MissionBriefing(1, 1);
			txt_2.text = component.MissionBriefing(2, 1);
			txt_3.text = component.MissionBriefing(3, 1);
		}
		GetComponent<global::UnityEngine.RectTransform>().parent = global::UnityEngine.GameObject.Find("UI Canvas").GetComponent<global::UnityEngine.RectTransform>();
		GetComponent<global::UnityEngine.RectTransform>().localPosition = PosOrig;
		GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(1f, 1f, 1f);
		global::UnityEngine.GameObject.Find("Menu").SendMessage("Sound_MissionBriefing");
	}

	private void Update()
	{
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (Fade_State == 1)
		{
			txtTitle.color = global::UnityEngine.Color.Lerp(txtTitle.color, color_On, global::UnityEngine.Time.deltaTime * Fade_Speed);
			txt_1.color = global::UnityEngine.Color.Lerp(txt_1.color, brown_On, global::UnityEngine.Time.deltaTime * Fade_Speed);
			bg_1.color = global::UnityEngine.Color.Lerp(bg_1.color, color_On, global::UnityEngine.Time.deltaTime * Fade_Speed * 0.6f);
			if (Life_Timer > 9f)
			{
				Fade_State = 2;
			}
			GetComponent<global::UnityEngine.RectTransform>().localPosition = global::UnityEngine.Vector3.Lerp(GetComponent<global::UnityEngine.RectTransform>().localPosition, PosTarget, global::UnityEngine.Time.deltaTime * 6f);
		}
		else
		{
			txtTitle.color = global::UnityEngine.Color.Lerp(txtTitle.color, color_Off, global::UnityEngine.Time.deltaTime * Fade_Speed);
			txt_1.color = global::UnityEngine.Color.Lerp(txt_1.color, color_Off, global::UnityEngine.Time.deltaTime * Fade_Speed);
			bg_1.color = global::UnityEngine.Color.Lerp(bg_1.color, color_Off, global::UnityEngine.Time.deltaTime * Fade_Speed * 0.4f);
			if (Life_Timer > 13f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			GetComponent<global::UnityEngine.RectTransform>().localPosition = global::UnityEngine.Vector3.Lerp(GetComponent<global::UnityEngine.RectTransform>().localPosition, PosOrig, global::UnityEngine.Time.deltaTime * 3f);
		}
		txt_2.color = txt_1.color;
		txt_3.color = txt_1.color;
		bg_2.color = bg_1.color;
		bg_3.color = bg_1.color;
	}
}
