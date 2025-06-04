public class Credits_Control : global::UnityEngine.MonoBehaviour
{
	private int State;

	private float Life_Timer;

	private bool onMouseDrag;

	private float AutoScroll_Timer = 3f;

	private float Scroll_Speed = 0.3f;

	public global::UnityEngine.SpriteRenderer SR_Credit_List;

	public global::UnityEngine.SpriteRenderer SR_Credit_List_2;

	public global::UnityEngine.UI.Image Title_Font_I;

	public global::UnityEngine.UI.Image Title_Font_V;

	public global::UnityEngine.UI.Image Title_Font_E;

	public global::UnityEngine.UI.Image Title_Font_I_a;

	public global::UnityEngine.UI.Image Title_Font_V_a;

	public global::UnityEngine.UI.Image Title_Font_E_a;

	public global::UnityEngine.UI.Text Title_Text;

	public global::UnityEngine.UI.Text GRIMHELM_Text;

	public global::UnityEngine.UI.Image Thanks_BG;

	public global::UnityEngine.UI.Image Thanks_BG_Tail;

	public global::UnityEngine.UI.Text Thanks_Text;

	public global::UnityEngine.SpriteRenderer SR_Glow_LT;

	public global::UnityEngine.SpriteRenderer SR_Glow_RB;

	public global::UnityEngine.SpriteRenderer SR_Glow_RB_2;

	public global::UnityEngine.SpriteRenderer Black_Box;

	private float text_Timer;

	private float textPos_Timer;

	private global::UnityEngine.Vector3[] pos_Font = new global::UnityEngine.Vector3[6];

	private global::UnityEngine.Color color_ON = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_OFF = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	private bool isFadeIn = true;

	private bool isFadeOut;

	private float FadeOpacity = 1f;

	private string FadeOutAction = string.Empty;

	private global::UnityEngine.UI.Image BlackFade;

	private void Start()
	{
		BlackFade = global::UnityEngine.GameObject.Find("BlackFade").GetComponent<global::UnityEngine.UI.Image>();
		BlackFade.enabled = true;
		Black_Box.color = new global::UnityEngine.Color(0f, 0f, 0f, 1f);
		SR_Credit_List.color = color_OFF;
		SR_Credit_List_2.color = color_OFF;
		Title_Font_I.color = color_OFF;
		Title_Font_V.color = color_OFF;
		Title_Font_E.color = color_OFF;
		Title_Font_I_a.color = color_OFF;
		Title_Font_V_a.color = color_OFF;
		Title_Font_E_a.color = color_OFF;
		Title_Text.color = color_OFF;
		GRIMHELM_Text.color = color_OFF;
		global::UnityEngine.UI.Image thanks_BG = Thanks_BG;
		global::UnityEngine.Color color = new global::UnityEngine.Color(Thanks_BG.color.r, Thanks_BG.color.g, Thanks_BG.color.b, 0f);
		Thanks_BG_Tail.color = color;
		thanks_BG.color = color;
		Thanks_Text.color = color_OFF;
		pos_Font[0] = Title_Font_I.GetComponent<global::UnityEngine.RectTransform>().localPosition;
		pos_Font[1] = Title_Font_V.GetComponent<global::UnityEngine.RectTransform>().localPosition;
		pos_Font[2] = Title_Font_E.GetComponent<global::UnityEngine.RectTransform>().localPosition;
		pos_Font[3] = Title_Font_I_a.GetComponent<global::UnityEngine.RectTransform>().localPosition;
		pos_Font[4] = Title_Font_V_a.GetComponent<global::UnityEngine.RectTransform>().localPosition;
		pos_Font[5] = Title_Font_E_a.GetComponent<global::UnityEngine.RectTransform>().localPosition;
		SR_Glow_LT.color = new global::UnityEngine.Color(SR_Glow_LT.color.r, SR_Glow_LT.color.g, SR_Glow_LT.color.b, 0f);
		SR_Glow_RB.color = new global::UnityEngine.Color(SR_Glow_RB.color.r, SR_Glow_RB.color.g, SR_Glow_RB.color.b, 0f);
		SR_Glow_RB_2.color = new global::UnityEngine.Color(SR_Glow_RB_2.color.r, SR_Glow_RB_2.color.g, SR_Glow_RB_2.color.b, 0f);
		if (AxiPlayerPrefs.GetFloat("MusicVolume") < 0.6f)
		{
			global::UnityEngine.GameObject.Find("BGM_Credits").GetComponent<UnityEngine.AudioSource>().volume = 0.6f;
		}
		else
		{
			global::UnityEngine.GameObject.Find("BGM_Credits").GetComponent<UnityEngine.AudioSource>().volume = AxiPlayerPrefs.GetFloat("MusicVolume");
		}
		global::UnityEngine.GameObject.Find("BGM_Credits").GetComponent<UnityEngine.AudioSource>().Play();
	}

	private void Update()
	{
		Life_Timer += global::UnityEngine.Time.deltaTime;
		text_Timer += global::UnityEngine.Time.deltaTime;
		if (text_Timer > 0.1f)
		{
			Title_Font_I.color = global::UnityEngine.Color.Lerp(Title_Font_I.color, new global::UnityEngine.Color(1f, 1f, 1f, 0.3f), global::UnityEngine.Time.deltaTime * 0.5f);
			Title_Font_V.color = global::UnityEngine.Color.Lerp(Title_Font_V.color, new global::UnityEngine.Color(1f, 1f, 1f, 0.3f), global::UnityEngine.Time.deltaTime * 0.25f);
			Title_Font_E.color = global::UnityEngine.Color.Lerp(Title_Font_E.color, new global::UnityEngine.Color(1f, 1f, 1f, 0.3f), global::UnityEngine.Time.deltaTime * 0.1f);
			Title_Font_I_a.color = global::UnityEngine.Color.Lerp(Title_Font_I_a.color, new global::UnityEngine.Color(1f, 1f, 1f, 0.25f), global::UnityEngine.Time.deltaTime * 0.5f);
			Title_Font_V_a.color = global::UnityEngine.Color.Lerp(Title_Font_V_a.color, new global::UnityEngine.Color(1f, 1f, 1f, 0.25f), global::UnityEngine.Time.deltaTime * 0.25f);
			Title_Font_E_a.color = global::UnityEngine.Color.Lerp(Title_Font_E_a.color, new global::UnityEngine.Color(1f, 1f, 1f, 0.25f), global::UnityEngine.Time.deltaTime * 0.1f);
			textPos_Timer += global::UnityEngine.Time.deltaTime;
			if (textPos_Timer > 0.08f)
			{
				textPos_Timer = 0f;
				Title_Font_I_a.GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(pos_Font[3].x + global::UnityEngine.Random.Range(-2.5f, 2.5f), pos_Font[3].y + global::UnityEngine.Random.Range(-2f, 2f), 0f);
				Title_Font_V_a.GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(pos_Font[4].x + global::UnityEngine.Random.Range(-2.5f, 2.5f), pos_Font[4].y + global::UnityEngine.Random.Range(-2f, 2f), 0f);
				Title_Font_E_a.GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(pos_Font[5].x + global::UnityEngine.Random.Range(-2.5f, 2.5f), pos_Font[5].y + global::UnityEngine.Random.Range(-2f, 2f), 0f);
			}
		}
		if (text_Timer > 2f)
		{
			Title_Text.color = global::UnityEngine.Color.Lerp(Title_Text.color, new global::UnityEngine.Color(1f, 1f, 1f, 0.5f), global::UnityEngine.Time.deltaTime * 0.1f);
		}
		if (State == 0)
		{
			if (Life_Timer > 5f)
			{
				State++;
			}
			GRIMHELM_Text.color = global::UnityEngine.Color.Lerp(GRIMHELM_Text.color, color_ON, global::UnityEngine.Time.deltaTime * 0.2f);
			SR_Glow_RB_2.color = global::UnityEngine.Color.Lerp(SR_Glow_RB_2.color, new global::UnityEngine.Color(SR_Glow_RB_2.color.r, SR_Glow_RB_2.color.g, SR_Glow_RB_2.color.b, 0.4f), global::UnityEngine.Time.deltaTime * 0.1f);
		}
		else
		{
			global::UnityEngine.GameObject.Find("Title_AQE").GetComponent<global::UnityEngine.RectTransform>().localPosition = global::UnityEngine.Vector3.Lerp(global::UnityEngine.GameObject.Find("Title_AQE").GetComponent<global::UnityEngine.RectTransform>().localPosition, new global::UnityEngine.Vector3(0f, 440f, 0f), global::UnityEngine.Time.deltaTime * 3f);
			GRIMHELM_Text.color = global::UnityEngine.Color.Lerp(GRIMHELM_Text.color, color_OFF, global::UnityEngine.Time.deltaTime * 10f);
			if (global::UnityEngine.GameObject.Find("Title_AQE").GetComponent<global::UnityEngine.RectTransform>().localPosition.y > 400f)
			{
				Black_Box.color = global::UnityEngine.Color.Lerp(Black_Box.color, new global::UnityEngine.Color(0f, 0f, 0f, 0f), global::UnityEngine.Time.deltaTime * 1f);
				SR_Credit_List.color = global::UnityEngine.Color.Lerp(SR_Credit_List.color, color_ON, global::UnityEngine.Time.deltaTime * 2f);
				SR_Credit_List_2.color = SR_Credit_List.color;
				if (SR_Credit_List.color.a > 0.9999f)
				{
					global::UnityEngine.UI.Image thanks_BG = Thanks_BG;
					global::UnityEngine.Color color = global::UnityEngine.Color.Lerp(Thanks_BG.color, new global::UnityEngine.Color(Thanks_BG.color.r, Thanks_BG.color.g, Thanks_BG.color.b, 0.25f), global::UnityEngine.Time.deltaTime * 1f);
					Thanks_BG_Tail.color = color;
					thanks_BG.color = color;
					Thanks_Text.color = global::UnityEngine.Color.Lerp(Thanks_Text.color, color_ON, global::UnityEngine.Time.deltaTime * 1f);
				}
			}
			SR_Glow_LT.color = global::UnityEngine.Color.Lerp(SR_Glow_LT.color, new global::UnityEngine.Color(SR_Glow_LT.color.r, SR_Glow_LT.color.g, SR_Glow_LT.color.b, 0.05f), global::UnityEngine.Time.deltaTime * 1f);
			SR_Glow_RB_2.color = global::UnityEngine.Color.Lerp(SR_Glow_RB_2.color, new global::UnityEngine.Color(SR_Glow_RB_2.color.r, SR_Glow_RB_2.color.g, SR_Glow_RB_2.color.b, 0.4f), global::UnityEngine.Time.deltaTime * 1f);
			AutoScroll_Timer += global::UnityEngine.Time.deltaTime;
			if ((double)AutoScroll_Timer > 3.0)
			{
				SR_Credit_List.transform.Translate(global::UnityEngine.Vector3.up * global::UnityEngine.Time.deltaTime * Scroll_Speed);
				if (SR_Credit_List.transform.position.y > 48f)
				{
					SR_Credit_List.transform.position = new global::UnityEngine.Vector3(1.8f, -18.5f, 0f);
				}
			}
		}
		if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.Escape))
		{
			global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_DeviceOn");
			Set_FadeOut("Title");
		}
		else if (State == 0)
		{
			if (global::UnityEngine.Input.GetMouseButtonDown(0) || global::UnityEngine.Input.anyKeyDown)
			{
				State = 1;
			}
		}
		else if (global::UnityEngine.Input.GetMouseButton(0) || global::UnityEngine.Input.GetKey(global::UnityEngine.KeyCode.Space) || global::UnityEngine.Input.GetKey(global::UnityEngine.KeyCode.Return) || global::UnityEngine.Input.GetKey(global::UnityEngine.KeyCode.KeypadEnter) || global::UnityEngine.Input.GetKey(global::UnityEngine.KeyCode.Z))
		{
			Scroll_Speed = global::UnityEngine.Mathf.Lerp(Scroll_Speed, 5f, global::UnityEngine.Time.deltaTime * 3f);
		}
		else
		{
			Scroll_Speed = global::UnityEngine.Mathf.Lerp(Scroll_Speed, 0.3f, global::UnityEngine.Time.deltaTime * 3f);
		}
		if (isFadeIn)
		{
			FadeOpacity -= global::UnityEngine.Time.deltaTime * 0.4f;
			if (FadeOpacity <= 0f)
			{
				isFadeIn = false;
				FadeOpacity = 0f;
				BlackFade.enabled = false;
			}
			BlackFade.color = new global::UnityEngine.Color(0f, 0f, 0f, FadeOpacity);
		}
		else if (isFadeOut)
		{
			FadeOpacity += global::UnityEngine.Time.deltaTime * 1f;
			if (FadeOpacity >= 1f)
			{
				isFadeOut = false;
				FadeOpacity = 1f;
				global::UnityEngine.Application.LoadLevel("Title");
			}
			BlackFade.color = new global::UnityEngine.Color(0f, 0f, 0f, FadeOpacity);
			global::UnityEngine.GameObject.Find("BGM_Credits").GetComponent<UnityEngine.AudioSource>().volume = global::UnityEngine.Mathf.Lerp(global::UnityEngine.GameObject.Find("BGM_Credits").GetComponent<UnityEngine.AudioSource>().volume, 0f, global::UnityEngine.Time.deltaTime * 1.6f);
		}
	}

	private void Set_FadeOut(string fadeoutaction)
	{
		isFadeOut = true;
		isFadeIn = false;
		if (!BlackFade.enabled)
		{
			FadeOpacity = 0f;
			BlackFade.enabled = true;
			BlackFade.color = new global::UnityEngine.Color(0f, 0f, 0f, 0f);
		}
		FadeOutAction = fadeoutaction;
	}
}
