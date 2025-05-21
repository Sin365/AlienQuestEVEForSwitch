public class Info_Save_Completed : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject FontObj;

	private int OnOff = 1;

	private float Life_Timer;

	private global::UnityEngine.Vector3 Pos_Target;

	private global::UnityEngine.Vector3 Pos_Orig;

	private void Start()
	{
		Pos_Orig = global::UnityEngine.GameObject.Find("Pos_InfoItem_Orig").GetComponent<global::UnityEngine.RectTransform>().position;
		Pos_Target = global::UnityEngine.GameObject.Find("Pos_InfoItem_Target").GetComponent<global::UnityEngine.RectTransform>().position;
		GetComponent<global::UnityEngine.RectTransform>().parent = global::UnityEngine.GameObject.Find("UI Canvas").GetComponent<global::UnityEngine.RectTransform>();
		GetComponent<global::UnityEngine.RectTransform>().position = Pos_Orig;
		GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(1f, 1f, 1f);
	}

	public void Set_Start(string text_saved)
	{
		FontObj.GetComponent<global::UnityEngine.UI.Text>().text = text_saved;
	}

	private void Update()
	{
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (OnOff > 0)
		{
			if (Life_Timer > 2f)
			{
				OnOff = 0;
			}
			GetComponent<global::UnityEngine.RectTransform>().position = global::UnityEngine.Vector3.Lerp(GetComponent<global::UnityEngine.RectTransform>().position, Pos_Target, global::UnityEngine.Time.deltaTime * 20f);
		}
		else
		{
			if (Life_Timer > 4f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			GetComponent<global::UnityEngine.RectTransform>().position = global::UnityEngine.Vector3.Lerp(GetComponent<global::UnityEngine.RectTransform>().position, Pos_Orig, global::UnityEngine.Time.deltaTime * 5f);
		}
	}
}
