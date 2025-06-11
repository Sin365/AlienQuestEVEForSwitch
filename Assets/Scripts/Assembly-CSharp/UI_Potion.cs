public class UI_Potion : global::UnityEngine.MonoBehaviour
{
	private bool onPotion;

	private bool on_Use_HP;

	private bool on_Get_HP;

	private bool on_Use_MP;

	private bool on_Get_MP;

	public global::UnityEngine.UI.Image BG_Box;

	public global::UnityEngine.UI.Image Border_HP;

	public global::UnityEngine.UI.Image Icon_HP;

	public global::UnityEngine.UI.Image Border_MP;

	public global::UnityEngine.UI.Image Icon_MP;

	public global::UnityEngine.UI.Image Icon_Cursor;

	public global::UnityEngine.UI.Text Text_HP;

	public global::UnityEngine.UI.Text Text_MP;

	private global::UnityEngine.Color color_ON = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_Half = new global::UnityEngine.Color(1f, 1f, 1f, 0.3f);

	private global::UnityEngine.Color color_OFF = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	private global::UnityEngine.Vector3 pos_ON = new global::UnityEngine.Vector3(-938f, -460f, 0f);

	private global::UnityEngine.Vector3 pos_Half = new global::UnityEngine.Vector3(-1038f, -460f, 0f);

	private global::UnityEngine.Vector3 pos_OFF = new global::UnityEngine.Vector3(-1300f, -460f, 0f);

	private global::UnityEngine.Vector3 Icon_Scale_ON = new global::UnityEngine.Vector3(1.4f, 1.4f, 1f);

	private global::UnityEngine.Vector3 Icon_Scale_OFF = new global::UnityEngine.Vector3(0.7f, 0.7f, 1f);

	private global::UnityEngine.Vector3 Text_Scale_ON = new global::UnityEngine.Vector3(2f, 2f, 1f);

	private global::UnityEngine.Vector3 Text_Scale_OFF = new global::UnityEngine.Vector3(1f, 1f, 1f);

    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		GetComponent<global::UnityEngine.RectTransform>().localPosition = pos_OFF;
		Border_HP.color = color_OFF;
		Border_MP.color = color_OFF;
		Icon_Cursor.color = color_OFF;
		Icon_Cursor.rectTransform.localPosition = new global::UnityEngine.Vector3(100f, 100f, 0f);
	}

	private void Update()
	{
		if (GM.Paused)
		{
			GetComponent<global::UnityEngine.RectTransform>().localPosition = pos_OFF;
			return;
		}
		if (GM.Paused || GM.GameOver || GM.onHscene || GM.onEvent || (GM.Potion_HP < 1 && GM.Potion_MP < 1))
		{
			GetComponent<global::UnityEngine.RectTransform>().localPosition = global::UnityEngine.Vector3.Lerp(GetComponent<global::UnityEngine.RectTransform>().localPosition, pos_OFF, global::UnityEngine.Time.deltaTime * 4f);
			return;
		}
		if (GM.Potion_HP > 0)
		{
			GetComponent<global::UnityEngine.RectTransform>().localPosition = global::UnityEngine.Vector3.Lerp(GetComponent<global::UnityEngine.RectTransform>().localPosition, pos_ON, global::UnityEngine.Time.deltaTime * 8f);
		}
		else
		{
			GetComponent<global::UnityEngine.RectTransform>().localPosition = global::UnityEngine.Vector3.Lerp(GetComponent<global::UnityEngine.RectTransform>().localPosition, pos_Half, global::UnityEngine.Time.deltaTime * 8f);
		}
		if (GM.Potion_HP > 0)
		{
			Icon_HP.enabled = true;
			Text_HP.enabled = true;
		}
		else
		{
			Icon_HP.enabled = false;
			Text_HP.enabled = false;
		}
		if (GM.Potion_MP > 0)
		{
			Icon_MP.enabled = true;
			Text_MP.enabled = true;
		}
		else
		{
			Icon_MP.enabled = false;
			Text_MP.enabled = false;
		}
		if (GM.Potion_MP > 0 && global::UnityEngine.Input.GetAxis("L_Trigger") < 0f)
		{
			Icon_Cursor.color = global::UnityEngine.Color.Lerp(Icon_Cursor.color, color_ON, global::UnityEngine.Time.deltaTime * 5f);
			Icon_Cursor.rectTransform.localPosition = global::UnityEngine.Vector3.Lerp(Icon_Cursor.rectTransform.localPosition, new global::UnityEngine.Vector3(100f, 45f, 0f), global::UnityEngine.Time.deltaTime * 5f);
			Border_MP.color = global::UnityEngine.Color.Lerp(Border_MP.color, color_Half, global::UnityEngine.Time.deltaTime * 10f);
		}
		else
		{
			Icon_Cursor.color = global::UnityEngine.Color.Lerp(Icon_Cursor.color, color_OFF, global::UnityEngine.Time.deltaTime * 5f);
			Icon_Cursor.rectTransform.localPosition = global::UnityEngine.Vector3.Lerp(Icon_Cursor.rectTransform.localPosition, new global::UnityEngine.Vector3(100f, 100f, 0f), global::UnityEngine.Time.deltaTime * 5f);
			Border_MP.color = global::UnityEngine.Color.Lerp(Border_MP.color, color_OFF, global::UnityEngine.Time.deltaTime * 10f);
		}
		Border_HP.color = global::UnityEngine.Color.Lerp(Border_HP.color, color_OFF, global::UnityEngine.Time.deltaTime * 10f);
		Icon_HP.rectTransform.localScale = global::UnityEngine.Vector3.Lerp(Icon_HP.rectTransform.localScale, Icon_Scale_OFF, global::UnityEngine.Time.deltaTime * 5f);
		Icon_MP.rectTransform.localScale = global::UnityEngine.Vector3.Lerp(Icon_MP.rectTransform.localScale, Icon_Scale_OFF, global::UnityEngine.Time.deltaTime * 5f);
		Text_HP.rectTransform.localScale = global::UnityEngine.Vector3.Lerp(Text_HP.rectTransform.localScale, Text_Scale_OFF, global::UnityEngine.Time.deltaTime * 5f);
		Text_MP.rectTransform.localScale = global::UnityEngine.Vector3.Lerp(Text_MP.rectTransform.localScale, Text_Scale_OFF, global::UnityEngine.Time.deltaTime * 5f);
	}

	private void Set_Text_HPMP()
	{
		Text_HP.text = "x " + GM.Potion_HP;
		Text_MP.text = "x " + GM.Potion_MP;
	}

	private void Get_Potion_HP()
	{
		Icon_HP.rectTransform.localScale = Icon_Scale_ON;
		Text_HP.rectTransform.localScale = Text_Scale_ON;
		Text_HP.text = "x " + GM.Potion_HP;
	}

	private void Get_Potion_MP()
	{
		Icon_MP.rectTransform.localScale = Icon_Scale_ON;
		Text_MP.rectTransform.localScale = Text_Scale_ON;
		Text_MP.text = "x " + GM.Potion_MP;
	}

	private void Use_Potion_HP()
	{
		Text_HP.text = "x " + GM.Potion_HP;
		Border_HP.color = color_ON;
		Text_HP.rectTransform.localScale = Text_Scale_ON;
	}

	private void Use_Potion_MP()
	{
		Text_MP.text = "x " + GM.Potion_MP;
		Border_MP.color = color_ON;
		Text_MP.rectTransform.localScale = Text_Scale_ON;
	}
}
