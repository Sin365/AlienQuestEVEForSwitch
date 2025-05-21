public class Info_GetItem : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject[] Fonts;

	public int Item_Num;

	private int OnOff = 1;

	private float Life_Timer;

	private global::UnityEngine.Vector3 Pos_Target;

	private global::UnityEngine.Vector3 Pos_Orig;

	private void Start()
	{
		if (Item_Num > 0 && Fonts.Length > 0)
		{
			int language_Num = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>().Language_Num;
			Fonts[0].GetComponent<global::UnityEngine.UI.Text>().text = global::UnityEngine.GameObject.Find("Menu").GetComponent<Language_MenuItem>().ItemName(Item_Num, language_Num);
			if (Fonts.Length > 1)
			{
				if (Item_Num <= 5)
				{
					Fonts[1].GetComponent<global::UnityEngine.UI.Text>().text = "ATK  +" + global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>().Weapon_DMG[Item_Num];
				}
				else if (Item_Num <= 10)
				{
					Fonts[1].GetComponent<global::UnityEngine.UI.Text>().text = "MP  -" + global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>().Skill_MP[Item_Num - 6];
				}
				else if (Item_Num == 11 || Item_Num == 13 || Item_Num == 14)
				{
					Fonts[1].GetComponent<global::UnityEngine.UI.Text>().text = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>().Get_KeyText(Item_Num);
				}
				else
				{
					Fonts[1].GetComponent<global::UnityEngine.UI.Text>().text = global::UnityEngine.GameObject.Find("Menu").GetComponent<Language_MenuItem>().ItemDesc(Item_Num, language_Num);
				}
			}
		}
		Pos_Orig = global::UnityEngine.GameObject.Find("Pos_InfoItem_Orig").GetComponent<global::UnityEngine.RectTransform>().position;
		Pos_Target = global::UnityEngine.GameObject.Find("Pos_InfoItem_Target").GetComponent<global::UnityEngine.RectTransform>().position;
		GetComponent<global::UnityEngine.RectTransform>().parent = global::UnityEngine.GameObject.Find("UI Canvas").GetComponent<global::UnityEngine.RectTransform>();
		GetComponent<global::UnityEngine.RectTransform>().position = Pos_Orig;
		GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(1f, 1f, 1f);
	}

	private void Update()
	{
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (OnOff > 0)
		{
			if (Life_Timer > 4f)
			{
				OnOff = 0;
			}
			GetComponent<global::UnityEngine.RectTransform>().position = global::UnityEngine.Vector3.Lerp(GetComponent<global::UnityEngine.RectTransform>().position, Pos_Target, global::UnityEngine.Time.deltaTime * 20f);
		}
		else
		{
			if (Life_Timer > 6f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			GetComponent<global::UnityEngine.RectTransform>().position = global::UnityEngine.Vector3.Lerp(GetComponent<global::UnityEngine.RectTransform>().position, Pos_Orig, global::UnityEngine.Time.deltaTime * 5f);
		}
	}
}
