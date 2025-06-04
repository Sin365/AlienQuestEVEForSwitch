public class Mission_Briefing : global::UnityEngine.MonoBehaviour
{
	private void Start()
	{
		Hide_BriefingPos();
		Language_MenuItem component = global::UnityEngine.GameObject.Find("Menu").GetComponent<Language_MenuItem>();
		if (AxiPlayerPrefs.GetInt("Language_Num") == 1)
		{
			global::UnityEngine.GameObject.Find("Text_MissionBr_Title").GetComponent<global::UnityEngine.UI.Text>().text = component.MissionBriefing(0, 1);
			global::UnityEngine.GameObject.Find("Text_MissionBr_1").GetComponent<global::UnityEngine.UI.Text>().text = component.MissionBriefing(1, 1);
			global::UnityEngine.GameObject.Find("Text_MissionBr_2").GetComponent<global::UnityEngine.UI.Text>().text = component.MissionBriefing(2, 1);
			global::UnityEngine.GameObject.Find("Text_MissionBr_3").GetComponent<global::UnityEngine.UI.Text>().text = component.MissionBriefing(3, 1);
			global::UnityEngine.GameObject.Find("Text_MissionBr_4").GetComponent<global::UnityEngine.UI.Text>().text = component.MapText(3, 1);
		}
	}

	private void Hide_BriefingPos()
	{
		GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(-690f, 2000f, 0f);
	}

	private void Set_BriefingPos_Map()
	{
		GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(-690f, 440f, 0f);
		GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(0.8f, 0.8f, 1f);
	}

	private void Set_BriefingPos_Menu()
	{
		GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(98f, 368f, 0f);
		GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(0.7f, 0.7f, 1f);
	}

	private void Complete_Mission_1()
	{
		global::UnityEngine.GameObject.Find("MB_Check_1").GetComponent<global::UnityEngine.UI.Image>().enabled = true;
		global::UnityEngine.GameObject.Find("MB_ListBox_1").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, 0.2f);
		global::UnityEngine.GameObject.Find("Text_MissionBr_1").GetComponent<global::UnityEngine.UI.Text>().color = new global::UnityEngine.Color(0f, 1f, 1f, 0.7f);
	}

	private void Complete_Mission_2()
	{
		global::UnityEngine.GameObject.Find("MB_Check_2").GetComponent<global::UnityEngine.UI.Image>().enabled = true;
		global::UnityEngine.GameObject.Find("MB_ListBox_2").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, 0.2f);
		global::UnityEngine.GameObject.Find("Text_MissionBr_2").GetComponent<global::UnityEngine.UI.Text>().color = new global::UnityEngine.Color(0f, 1f, 1f, 0.7f);
	}

	private void Complete_Mission_3()
	{
		global::UnityEngine.GameObject.Find("MB_Check_3").GetComponent<global::UnityEngine.UI.Image>().enabled = true;
		global::UnityEngine.GameObject.Find("MB_ListBox_3").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, 0.2f);
		global::UnityEngine.GameObject.Find("Text_MissionBr_3").GetComponent<global::UnityEngine.UI.Text>().color = new global::UnityEngine.Color(0f, 1f, 1f, 0.7f);
		global::UnityEngine.GameObject.Find("Text_MissionBr_4").GetComponent<global::UnityEngine.UI.Text>().enabled = true;
		global::UnityEngine.GameObject.Find("MB_ListBox_4").GetComponent<global::UnityEngine.UI.Image>().enabled = true;
	}
}
