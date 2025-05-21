public class Info_Input : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject[] Icon;

	public global::UnityEngine.GameObject[] Fonts;

	public global::UnityEngine.GameObject[] Pos;

	private int OnOff;

	private int PosNum = 1;

	private global::UnityEngine.Color[] BGColor = new global::UnityEngine.Color[2]
	{
		new global::UnityEngine.Color(1f, 1f, 1f, 0f),
		new global::UnityEngine.Color(1f, 1f, 1f, 1f)
	};

	private global::UnityEngine.Color[] IconColor = new global::UnityEngine.Color[2]
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
		if (Icon.Length > 0)
		{
			for (int i = 0; i < Icon.Length; i++)
			{
				Icon[i].GetComponent<global::UnityEngine.UI.Image>().color = IconColor[0];
			}
		}
		if (Fonts.Length > 0)
		{
			FontColor[1] = Fonts[0].GetComponent<global::UnityEngine.UI.Text>().color;
			FontColor[0] = new global::UnityEngine.Color(FontColor[1].r, FontColor[1].g, FontColor[1].b, 0f);
			for (int j = 0; j < Fonts.Length; j++)
			{
				Fonts[j].GetComponent<global::UnityEngine.UI.Text>().color = FontColor[0];
			}
		}
	}

	private void On()
	{
		OnOff = 1;
	}

	private void Off()
	{
		OnOff = 0;
		PosNum = 0;
	}

	private void Reset_Pos()
	{
		PosNum = 0;
	}

	private void MoveUp()
	{
		if (Pos.Length <= 0)
		{
		}
	}

	private void MoveDown()
	{
		if (Pos.Length <= 0)
		{
		}
	}

	private void Update()
	{
		if (Pos.Length > 1 && PosNum == 0)
		{
			GetComponent<global::UnityEngine.RectTransform>().position = global::UnityEngine.Vector3.Lerp(GetComponent<global::UnityEngine.RectTransform>().position, Pos[0].GetComponent<global::UnityEngine.RectTransform>().position, global::UnityEngine.Time.deltaTime * 5f);
		}
		Set_Opacity();
	}

	private void Set_Opacity()
	{
		GetComponent<global::UnityEngine.UI.Image>().color = global::UnityEngine.Color.Lerp(GetComponent<global::UnityEngine.UI.Image>().color, BGColor[OnOff], global::UnityEngine.Time.deltaTime * 5f);
		if (Icon.Length > 0)
		{
			for (int i = 0; i < Icon.Length; i++)
			{
				Icon[i].GetComponent<global::UnityEngine.UI.Image>().color = global::UnityEngine.Color.Lerp(Icon[i].GetComponent<global::UnityEngine.UI.Image>().color, IconColor[OnOff], global::UnityEngine.Time.deltaTime * 5f);
			}
		}
		if (Fonts.Length > 0)
		{
			for (int j = 0; j < Fonts.Length; j++)
			{
				Fonts[j].GetComponent<global::UnityEngine.UI.Text>().color = global::UnityEngine.Color.Lerp(Fonts[j].GetComponent<global::UnityEngine.UI.Text>().color, FontColor[OnOff], global::UnityEngine.Time.deltaTime * 5f);
			}
		}
	}
}
