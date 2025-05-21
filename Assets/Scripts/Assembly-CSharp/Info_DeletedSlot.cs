public class Info_DeletedSlot : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject FontObj;

	private int OnOff = 1;

	private float Life_Timer;

	private global::UnityEngine.Color[] BGColor = new global::UnityEngine.Color[2]
	{
		new global::UnityEngine.Color(1f, 1f, 1f, 0f),
		new global::UnityEngine.Color(1f, 1f, 1f, 1f)
	};

	private global::UnityEngine.Color[] FontColor = new global::UnityEngine.Color[2]
	{
		new global::UnityEngine.Color(1f, 1f, 1f, 0f),
		new global::UnityEngine.Color(1f, 1f, 1f, 1f)
	};

	private void Start()
	{
		BGColor[1] = GetComponent<global::UnityEngine.UI.Image>().color;
		GetComponent<global::UnityEngine.UI.Image>().color = BGColor[0];
		FontColor[1] = FontObj.GetComponent<global::UnityEngine.UI.Text>().color;
		FontColor[0] = new global::UnityEngine.Color(FontColor[1].r, FontColor[1].g, FontColor[1].b, 0f);
		FontObj.GetComponent<global::UnityEngine.UI.Text>().color = FontColor[0];
	}

	public void Set_Start(string text_was_deleted)
	{
		FontObj.GetComponent<global::UnityEngine.UI.Text>().text = text_was_deleted;
	}

	private void Update()
	{
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (OnOff > 0)
		{
			if (FontObj.GetComponent<global::UnityEngine.UI.Text>().color.a > 0.98f)
			{
				OnOff = 0;
			}
		}
		else if (FontObj.GetComponent<global::UnityEngine.UI.Text>().color.a < 0.1f)
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
		GetComponent<global::UnityEngine.UI.Image>().color = global::UnityEngine.Color.Lerp(GetComponent<global::UnityEngine.UI.Image>().color, BGColor[OnOff], global::UnityEngine.Time.deltaTime * 5f);
		FontObj.GetComponent<global::UnityEngine.UI.Text>().color = global::UnityEngine.Color.Lerp(FontObj.GetComponent<global::UnityEngine.UI.Text>().color, FontColor[OnOff], global::UnityEngine.Time.deltaTime * 5f);
	}
}
