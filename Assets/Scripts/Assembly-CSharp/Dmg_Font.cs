public class Dmg_Font : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject[] font_Obj;

	private float Life_Timer;

	private int ColorType;

	private bool onCritical;

	private float Opacity = 1f;

	private global::UnityEngine.Color ColorTarget = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	public void Set_Number(int Num, int type)
	{
		global::UnityEngine.Sprite[] array = global::UnityEngine.Resources.LoadAll<global::UnityEngine.Sprite>("UI/256_Font");
		int num = Num;
		if (Num > 9999)
		{
			Num = 9999;
		}
		if (Num >= 1000)
		{
			font_Obj[3].GetComponent<global::UnityEngine.SpriteRenderer>().sprite = array[Num / 1000];
			Num %= 1000;
		}
		if (Num >= 100)
		{
			font_Obj[2].GetComponent<global::UnityEngine.SpriteRenderer>().sprite = array[Num / 100];
			Num %= 100;
		}
		else if (num >= 1000)
		{
			font_Obj[2].GetComponent<global::UnityEngine.SpriteRenderer>().sprite = array[0];
		}
		if (Num >= 10)
		{
			font_Obj[1].GetComponent<global::UnityEngine.SpriteRenderer>().sprite = array[Num / 10];
			Num %= 10;
		}
		else if (num >= 100)
		{
			font_Obj[1].GetComponent<global::UnityEngine.SpriteRenderer>().sprite = array[0];
		}
		font_Obj[0].GetComponent<global::UnityEngine.SpriteRenderer>().sprite = array[Num];
		switch (type)
		{
		case 2:
			base.transform.Translate(global::UnityEngine.Vector3.up * 1.2f);
			break;
		case 3:
			ColorTarget = new global::UnityEngine.Color(0.7f, 1f, 0f, 1f);
			break;
		case 4:
			ColorTarget = new global::UnityEngine.Color(1f, 0f, 0f, 1f);
			break;
		case 8:
			ColorTarget = new global::UnityEngine.Color(1f, 0f, 1f, 1f);
			break;
		case 32:
			base.transform.Translate(global::UnityEngine.Vector3.up * 1.2f);
			ColorTarget = new global::UnityEngine.Color(0.7f, 1f, 0f, 1f);
			break;
		}
		font_Obj[0].GetComponent<global::UnityEngine.SpriteRenderer>().color = ColorTarget;
		font_Obj[1].GetComponent<global::UnityEngine.SpriteRenderer>().color = ColorTarget;
		font_Obj[2].GetComponent<global::UnityEngine.SpriteRenderer>().color = ColorTarget;
		font_Obj[3].GetComponent<global::UnityEngine.SpriteRenderer>().color = ColorTarget;
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
		base.transform.Translate(global::UnityEngine.Vector3.up * global::UnityEngine.Time.deltaTime * 2f);
		if (Life_Timer > 0.2f)
		{
			Opacity -= global::UnityEngine.Time.deltaTime * 3f;
			if (Opacity < 0f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			global::UnityEngine.Color color = new global::UnityEngine.Color(ColorTarget.r, ColorTarget.g, ColorTarget.b, Opacity);
			font_Obj[0].GetComponent<global::UnityEngine.SpriteRenderer>().color = color;
			font_Obj[1].GetComponent<global::UnityEngine.SpriteRenderer>().color = color;
			font_Obj[2].GetComponent<global::UnityEngine.SpriteRenderer>().color = color;
			font_Obj[3].GetComponent<global::UnityEngine.SpriteRenderer>().color = color;
		}
	}

	private void Set_Opacity()
	{
	}
}
