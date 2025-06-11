public class Info_Dialog : global::UnityEngine.MonoBehaviour
{
	private float Life_Timer;

	private int OnOff = 1;

	private float Speed = 5f;

	private float Visible_Timer;

	private float ratio = 1920f / (float)global::UnityEngine.Screen.width;

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

	private global::UnityEngine.Vector3 pos_Player;

	private global::UnityEngine.Vector3 pos_UI;

	public global::UnityEngine.UI.Text FontObj;

	public global::UnityEngine.SpriteRenderer spr_Tail;

	GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		GetComponent<global::UnityEngine.RectTransform>().parent = global::UnityEngine.GameObject.Find("UI Canvas").GetComponent<global::UnityEngine.RectTransform>();
		GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(1f, 1f, 1f);
		BGColor[1] = GetComponent<global::UnityEngine.UI.Image>().color;
		BGColor[0] = new global::UnityEngine.Color(BGColor[1].r, BGColor[1].g, BGColor[1].b, 0f);
		global::UnityEngine.UI.Image component = GetComponent<global::UnityEngine.UI.Image>();
		global::UnityEngine.Color color = BGColor[0];
		spr_Tail.color = color;
		component.color = color;
		FontColor[1] = FontObj.GetComponent<global::UnityEngine.UI.Text>().color;
		FontColor[0] = new global::UnityEngine.Color(FontColor[1].r, FontColor[1].g, FontColor[1].b, 0f);
		FontObj.color = FontColor[0];
		ratio = 1920f / (float)global::UnityEngine.Screen.width;
		pos_Player = new global::UnityEngine.Vector3(global::UnityEngine.GameObject.Find("Player").transform.position.x, global::UnityEngine.GameObject.Find("Player").transform.position.y + 5.7f, 0f);
		pos_UI = global::UnityEngine.GameObject.Find("Main Camera").GetComponent<global::UnityEngine.Camera>().WorldToScreenPoint(pos_Player);
		GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(pos_UI.x * ratio - 960f, pos_UI.y * ratio - 540f, 0f);
	}

	public void Set_Size(float size_X)
	{
		GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(size_X, GetComponent<global::UnityEngine.RectTransform>().sizeDelta.y);
	}

	public void Set_Timer(float timer)
	{
		Visible_Timer = timer;
	}

	public void Set_Text(string text)
	{
		FontObj.text = text;
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			Life_Timer += global::UnityEngine.Time.deltaTime;
			ratio = 1920f / (float)global::UnityEngine.Screen.width;
			pos_Player = new global::UnityEngine.Vector3(global::UnityEngine.GameObject.Find("Player").transform.position.x, global::UnityEngine.GameObject.Find("Player").transform.position.y + 5.7f, 0f);
			pos_UI = global::UnityEngine.GameObject.Find("Main Camera").GetComponent<global::UnityEngine.Camera>().WorldToScreenPoint(pos_Player);
			GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(pos_UI.x * ratio - 960f, pos_UI.y * ratio - 540f, 0f);
			if (OnOff > 0)
			{
				Visible_Timer -= global::UnityEngine.Time.deltaTime;
				if (FontObj.color.a > 0.98f && Visible_Timer <= 0f)
				{
					OnOff = 0;
				}
			}
			else if (FontObj.color.a < 0.01f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			global::UnityEngine.SpriteRenderer spriteRenderer = spr_Tail;
			global::UnityEngine.Color color = global::UnityEngine.Color.Lerp(GetComponent<global::UnityEngine.UI.Image>().color, BGColor[OnOff], global::UnityEngine.Time.deltaTime * Speed);
			GetComponent<global::UnityEngine.UI.Image>().color = color;
			spriteRenderer.color = color;
			FontObj.color = global::UnityEngine.Color.Lerp(FontObj.color, FontColor[OnOff], global::UnityEngine.Time.deltaTime * Speed);
		}
		else
		{
			global::UnityEngine.SpriteRenderer spriteRenderer2 = spr_Tail;
			global::UnityEngine.Color color = global::UnityEngine.Color.Lerp(GetComponent<global::UnityEngine.UI.Image>().color, BGColor[0], global::UnityEngine.Time.deltaTime * 7f);
			GetComponent<global::UnityEngine.UI.Image>().color = color;
			spriteRenderer2.color = color;
			FontObj.color = global::UnityEngine.Color.Lerp(FontObj.color, FontColor[0], global::UnityEngine.Time.deltaTime * 7f);
		}
	}
}
