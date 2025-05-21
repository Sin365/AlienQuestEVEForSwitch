public class Help_GameOver : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.UI.Image BG_Black;

	public global::UnityEngine.UI.Image BG_Border;

	public global::UnityEngine.UI.Image BG_Bar_1;

	public global::UnityEngine.UI.Image BG_Bar_2;

	public global::UnityEngine.UI.Image BG_Bar_3;

	public global::UnityEngine.UI.Image MouseGlow_L;

	public global::UnityEngine.UI.Image MouseGlow_R;

	public global::UnityEngine.UI.Image[] Img_List;

	public global::UnityEngine.UI.Text[] Txt_List;

	private global::UnityEngine.Color color_Img = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_OFF = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	private global::UnityEngine.Color color_BG_Black = new global::UnityEngine.Color(0f, 0f, 0f, 1f);

	private global::UnityEngine.Color color_BG_Black_OFF = new global::UnityEngine.Color(0f, 0f, 0f, 0f);

	private global::UnityEngine.Color color_BG_Border = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_BG_Bar_1 = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_BG_Bar_2 = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_MouseGlow = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_Font = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_Font_OFF = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	private float Life_Timer;

	private void Start()
	{
		color_BG_Black = BG_Black.color;
		color_BG_Border = BG_Border.color;
		color_BG_Bar_1 = BG_Bar_1.color;
		color_BG_Bar_2 = BG_Bar_2.color;
		color_MouseGlow = MouseGlow_L.color;
		color_Font = Txt_List[0].color;
		color_Font_OFF = new global::UnityEngine.Color(color_Font.r, color_Font.g, color_Font.b, 0f);
	}

	private void Update()
	{
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (Life_Timer > 3f)
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
			for (int j = 0; j < Txt_List.Length; j++)
			{
				Txt_List[j].color = color_Font;
			}
			if (color_Font.a < 0.05f || Life_Timer > 10f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}
}
