public class Info_SaveData : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject FontObj;

	private bool Active;

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

	private void Info_Start()
	{
		Active = true;
	}

	private void Info_End()
	{
		Active = false;
	}

	private void Update()
	{
		if (Active)
		{
			Life_Timer += global::UnityEngine.Time.deltaTime;
			if (OnOff > 1)
			{
				FontObj.GetComponent<global::UnityEngine.UI.Text>().color = new global::UnityEngine.Color(FontColor[0].r, FontColor[0].g, FontColor[0].b, 0.5f + (1f + global::UnityEngine.Mathf.Sin(Life_Timer * 5f)) * 0.25f);
			}
			else if (FontObj.GetComponent<global::UnityEngine.UI.Text>().color.a < 0.95f)
			{
				GetComponent<global::UnityEngine.UI.Image>().color = global::UnityEngine.Color.Lerp(GetComponent<global::UnityEngine.UI.Image>().color, BGColor[OnOff], global::UnityEngine.Time.deltaTime * 2f);
				FontObj.GetComponent<global::UnityEngine.UI.Text>().color = global::UnityEngine.Color.Lerp(FontObj.GetComponent<global::UnityEngine.UI.Text>().color, FontColor[OnOff], global::UnityEngine.Time.deltaTime * 2f);
			}
			else
			{
				OnOff = 2;
			}
		}
		else if (Life_Timer > 0f)
		{
			GetComponent<global::UnityEngine.UI.Image>().color = global::UnityEngine.Color.Lerp(GetComponent<global::UnityEngine.UI.Image>().color, BGColor[0], global::UnityEngine.Time.deltaTime * 1.5f);
			FontObj.GetComponent<global::UnityEngine.UI.Text>().color = global::UnityEngine.Color.Lerp(FontObj.GetComponent<global::UnityEngine.UI.Text>().color, FontColor[0], global::UnityEngine.Time.deltaTime * 1.5f);
			if (FontObj.GetComponent<global::UnityEngine.UI.Text>().color.a < 0.01f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}
}
