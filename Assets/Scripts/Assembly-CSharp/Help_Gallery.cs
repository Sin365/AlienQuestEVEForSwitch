public class Help_Gallery : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.UI.Image BG_Black;

	public global::UnityEngine.UI.Image BG_Border;

	public global::UnityEngine.UI.Image BG_Bar_1;

	public global::UnityEngine.UI.Image BG_Bar_2;

	public global::UnityEngine.UI.Image BG_Bar_3;

	public global::UnityEngine.UI.Image BG_Bar_4;

	public global::UnityEngine.UI.Image BG_Bar_5;

	public global::UnityEngine.UI.Image MouseGlow_L;

	public global::UnityEngine.UI.Image MouseGlow_R;

	public global::UnityEngine.UI.Image[] Img_List;

	public global::UnityEngine.UI.Text[] Txt_List;

	public bool onHelp = true;

	private global::UnityEngine.Color color_Img = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_ON = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_OFF = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	private global::UnityEngine.Color color_BG_Black = new global::UnityEngine.Color(0f, 0f, 0f, 1f);

	private global::UnityEngine.Color color_BG_Black_ON = new global::UnityEngine.Color(0f, 0f, 0f, 1f);

	private global::UnityEngine.Color color_BG_Black_OFF = new global::UnityEngine.Color(0f, 0f, 0f, 0f);

	private global::UnityEngine.Color color_BG_Border = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_BG_Border_ON = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_BG_Bar_1 = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_BG_Bar_1_ON = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_BG_Bar_2 = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_BG_Bar_2_ON = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_MouseGlow = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_MouseGlow_ON = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_Font = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_Font_ON = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_Font_OFF = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	private float Life_Timer;

	private Gallery_Control GC;

	private void Start()
	{
		GC = global::UnityEngine.GameObject.Find("Gallery_Menu").GetComponent<Gallery_Control>();
		color_BG_Black = (color_BG_Black_ON = BG_Black.color);
		color_BG_Border = (color_BG_Border_ON = BG_Border.color);
		color_BG_Bar_1 = (color_BG_Bar_1_ON = BG_Bar_1.color);
		color_BG_Bar_2 = (color_BG_Bar_2_ON = BG_Bar_2.color);
		color_MouseGlow = (color_MouseGlow_ON = MouseGlow_L.color);
		color_Font = (color_Font_ON = Txt_List[0].color);
		color_Font_OFF = new global::UnityEngine.Color(color_Font.r, color_Font.g, color_Font.b, 0f);
	}

	private void Update()
	{
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (onHelp)
		{
			color_Img = global::UnityEngine.Color.Lerp(color_Img, color_ON, global::UnityEngine.Time.deltaTime * 2f);
			color_BG_Black = global::UnityEngine.Color.Lerp(color_BG_Black, color_BG_Black_ON, global::UnityEngine.Time.deltaTime * 2f);
			color_BG_Border = global::UnityEngine.Color.Lerp(color_BG_Border, color_BG_Border_ON, global::UnityEngine.Time.deltaTime * 2f);
			color_BG_Bar_1 = global::UnityEngine.Color.Lerp(color_BG_Bar_1, color_BG_Bar_1_ON, global::UnityEngine.Time.deltaTime * 2f);
			color_BG_Bar_2 = global::UnityEngine.Color.Lerp(color_BG_Bar_2, color_BG_Bar_2_ON, global::UnityEngine.Time.deltaTime * 2f);
			color_MouseGlow = global::UnityEngine.Color.Lerp(color_MouseGlow, color_MouseGlow_ON, global::UnityEngine.Time.deltaTime * 2.5f);
			color_Font = global::UnityEngine.Color.Lerp(color_Font, color_Font_ON, global::UnityEngine.Time.deltaTime * 2f);
			BG_Black.color = color_BG_Black;
			BG_Border.color = color_BG_Border;
			global::UnityEngine.UI.Image bG_Bar_ = BG_Bar_1;
			global::UnityEngine.Color color = color_BG_Bar_1;
			BG_Bar_3.color = color;
			bG_Bar_.color = color;
			BG_Bar_2.color = color_BG_Bar_2;
			MouseGlow_L.color = color_MouseGlow;
			MouseGlow_R.color = color_MouseGlow;
			for (int i = 0; i < Img_List.Length; i++)
			{
				Img_List[i].color = color_Img;
			}
			Txt_List[0].color = color_Font;
			Txt_List[1].color = color_Font;
			Txt_List[2].color = color_Font;
			Txt_List[3].color = color_Font;
			Txt_List[4].color = color_Font;
			Txt_List[5].color = color_Font;
			Txt_List[6].color = color_Font;
			if (GC.State == 1)
			{
				Txt_List[7].color = color_Font;
				Txt_List[8].color = color_Font;
				Txt_List[9].color = color_Font;
				Txt_List[10].color = color_Font;
				BG_Bar_4.color = color_BG_Bar_2;
				BG_Bar_5.color = color_BG_Bar_1;
			}
			else
			{
				Txt_List[7].color = global::UnityEngine.Color.Lerp(Txt_List[7].color, color_Font_OFF, global::UnityEngine.Time.deltaTime * 2f);
				Txt_List[8].color = Txt_List[7].color;
				Txt_List[9].color = Txt_List[7].color;
				Txt_List[10].color = Txt_List[7].color;
				BG_Bar_4.color = global::UnityEngine.Color.Lerp(BG_Bar_4.color, color_OFF, global::UnityEngine.Time.deltaTime * 2f);
				BG_Bar_5.color = global::UnityEngine.Color.Lerp(BG_Bar_5.color, color_OFF, global::UnityEngine.Time.deltaTime * 2f);
			}
			if (Life_Timer > 10f)
			{
				onHelp = false;
				Life_Timer = 0f;
			}
		}
		else
		{
			color_Img = global::UnityEngine.Color.Lerp(color_Img, color_OFF, global::UnityEngine.Time.deltaTime * 2f);
			color_BG_Black = global::UnityEngine.Color.Lerp(color_BG_Black, color_BG_Black_OFF, global::UnityEngine.Time.deltaTime * 2f);
			color_BG_Border = global::UnityEngine.Color.Lerp(color_BG_Border, color_OFF, global::UnityEngine.Time.deltaTime * 2f);
			color_BG_Bar_1 = global::UnityEngine.Color.Lerp(color_BG_Bar_1, color_OFF, global::UnityEngine.Time.deltaTime * 2f);
			color_BG_Bar_2 = global::UnityEngine.Color.Lerp(color_BG_Bar_2, color_OFF, global::UnityEngine.Time.deltaTime * 2f);
			color_MouseGlow = global::UnityEngine.Color.Lerp(color_MouseGlow, color_OFF, global::UnityEngine.Time.deltaTime * 2.5f);
			color_Font = global::UnityEngine.Color.Lerp(color_Font, color_Font_OFF, global::UnityEngine.Time.deltaTime * 2f);
			BG_Black.color = color_BG_Black;
			BG_Border.color = color_BG_Border;
			global::UnityEngine.UI.Image bG_Bar_2 = BG_Bar_1;
			global::UnityEngine.Color color = color_BG_Bar_1;
			BG_Bar_3.color = color;
			bG_Bar_2.color = color;
			BG_Bar_2.color = color_BG_Bar_2;
			MouseGlow_L.color = color_MouseGlow;
			MouseGlow_R.color = color_MouseGlow;
			for (int j = 0; j < Img_List.Length; j++)
			{
				Img_List[j].color = color_Img;
			}
			Txt_List[0].color = color_Font;
			Txt_List[1].color = color_Font;
			Txt_List[2].color = color_Font;
			Txt_List[3].color = color_Font;
			Txt_List[4].color = color_Font;
			Txt_List[5].color = color_Font;
			Txt_List[6].color = color_Font;
			if (GC.State == 1)
			{
				Txt_List[7].color = color_Font;
				Txt_List[8].color = color_Font;
				Txt_List[9].color = color_Font;
				Txt_List[10].color = color_Font;
				BG_Bar_4.color = color_BG_Bar_2;
				BG_Bar_5.color = color_BG_Bar_1;
			}
			else
			{
				Txt_List[7].color = global::UnityEngine.Color.Lerp(Txt_List[7].color, color_Font_OFF, global::UnityEngine.Time.deltaTime * 2f);
				Txt_List[8].color = Txt_List[7].color;
				Txt_List[9].color = Txt_List[7].color;
				Txt_List[10].color = Txt_List[7].color;
				BG_Bar_4.color = global::UnityEngine.Color.Lerp(BG_Bar_4.color, color_OFF, global::UnityEngine.Time.deltaTime * 2f);
				BG_Bar_5.color = global::UnityEngine.Color.Lerp(BG_Bar_5.color, color_OFF, global::UnityEngine.Time.deltaTime * 2f);
			}
			if (color_Font.a < 0.05f || Life_Timer > 10f)
			{
				GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(-614f, -1500f);
			}
		}
	}

	private void Show_Menu()
	{
		onHelp = true;
		Life_Timer = 0f;
		GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(-614f, 30f);
	}

	private void Hide_Menu()
	{
		onHelp = false;
		Life_Timer = 0f;
	}

	private void Set_Text_Gallery()
	{
		Txt_List[5].text = "Animation List :";
		Txt_List[6].text = "Click";
	}

	private void Set_Text_GameOver()
	{
		Txt_List[5].text = "Charge to Cum :";
		Txt_List[6].text = "Hold";
	}
}
