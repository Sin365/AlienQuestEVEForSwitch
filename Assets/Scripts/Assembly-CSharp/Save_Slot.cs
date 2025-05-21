public class Save_Slot : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject Name_Time;

	public global::UnityEngine.GameObject Text_Time;

	public global::UnityEngine.GameObject Name_Level;

	public global::UnityEngine.GameObject Text_Level;

	public global::UnityEngine.GameObject Name_HP;

	public global::UnityEngine.GameObject Text_HP;

	public global::UnityEngine.GameObject Name_MP;

	public global::UnityEngine.GameObject Text_MP;

	public global::UnityEngine.GameObject Name_Rate;

	public global::UnityEngine.GameObject Text_Rate;

	public global::UnityEngine.UI.Image Death_Icon;

	private bool Active;

	private bool isDeleted;

	private global::UnityEngine.Color FontColor = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	private void Start()
	{
		Set_Opacity();
	}

	public void Set_Slot(int num)
	{
		Save_Control component = global::UnityEngine.GameObject.Find("Title_Menu").GetComponent<Save_Control>();
		Text_Time.GetComponent<global::UnityEngine.UI.Text>().text = Check_Time(component.SaveData.PlayTime[num]);
		Text_Level.GetComponent<global::UnityEngine.UI.Text>().text = component.SaveData.Level[num].ToString();
		Text_HP.GetComponent<global::UnityEngine.UI.Text>().text = component.SaveData.HP[num] + " / " + component.SaveData.HP_Max[num];
		Text_MP.GetComponent<global::UnityEngine.UI.Text>().text = component.SaveData.MP[num] + " / " + component.SaveData.MP_Max[num];
		Text_Rate.GetComponent<global::UnityEngine.UI.Text>().text = string.Empty;
		if (component.SaveData.Bonus_Life[num] > 0)
		{
			Death_Icon.enabled = true;
		}
		else
		{
			Death_Icon.enabled = false;
		}
	}

	private string Check_Time(float time)
	{
		string empty = string.Empty;
		int num = (int)(time / 3600f);
		int num2 = (int)((time - (float)(3600 * num)) / 60f);
		int num3 = (int)(time % 60f);
		empty = ((num <= 0) ? (empty + "00:") : (num + ":"));
		empty = ((num2 <= 9) ? (empty + "0" + num2 + ":") : (empty + num2 + ":"));
		if (num3 > 9)
		{
			return empty + num3;
		}
		return empty + "0" + num3;
	}

	public bool Delete_Slot()
	{
		if (FontColor.a > 0.3f)
		{
			Active = false;
			isDeleted = true;
			return true;
		}
		return false;
	}

	private void On_Slot()
	{
		Active = true;
	}

	private void Off_Slot()
	{
		Active = false;
	}

	private void Update()
	{
		if (Active)
		{
			FontColor.a = global::UnityEngine.Mathf.Lerp(FontColor.a, 1f, global::UnityEngine.Time.deltaTime * 3f);
			if (FontColor.a < 1f)
			{
				Set_Opacity();
			}
			return;
		}
		if (FontColor.a > 0f)
		{
			FontColor.a = global::UnityEngine.Mathf.Lerp(FontColor.a, 0f, global::UnityEngine.Time.deltaTime * 16f);
			if (FontColor.a < 0.1f)
			{
				FontColor.a = 0f;
			}
			Set_Opacity();
		}
		if (FontColor.a == 0f && isDeleted)
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void Set_Opacity()
	{
		Name_Time.GetComponent<global::UnityEngine.UI.Text>().color = FontColor;
		Text_Time.GetComponent<global::UnityEngine.UI.Text>().color = FontColor;
		Name_Level.GetComponent<global::UnityEngine.UI.Text>().color = FontColor;
		Text_Level.GetComponent<global::UnityEngine.UI.Text>().color = FontColor;
		Name_HP.GetComponent<global::UnityEngine.UI.Text>().color = FontColor;
		Text_HP.GetComponent<global::UnityEngine.UI.Text>().color = FontColor;
		Name_MP.GetComponent<global::UnityEngine.UI.Text>().color = FontColor;
		Text_MP.GetComponent<global::UnityEngine.UI.Text>().color = FontColor;
		Name_Rate.GetComponent<global::UnityEngine.UI.Text>().color = FontColor;
		Text_Rate.GetComponent<global::UnityEngine.UI.Text>().color = FontColor;
		Death_Icon.color = new global::UnityEngine.Color(1f, 1f, 1f, FontColor.a * 0.5f);
	}
}
