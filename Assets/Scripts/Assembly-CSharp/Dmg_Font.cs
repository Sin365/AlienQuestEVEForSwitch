using System.Collections.Generic;
using UnityEngine;

public class Dmg_Font : global::UnityEngine.MonoBehaviour
{
	#region
	static Queue<Dmg_Font> mQueue_Obj = new Queue<Dmg_Font>();
    private static Color srcColor1;
    private static Color srcColor2;
    private static Color srcColor3;
    private static Color srcColor4;

    public static Dmg_Font ShowDmg(Vector3 position, Quaternion rotation,int num, int type)
	{
		Dmg_Font dmg;
		if (mQueue_Obj.Count > 0)
		{
			dmg = mQueue_Obj.Dequeue();
			dmg.transform.position = position;
			dmg.transform.rotation = rotation;
		}
		else
		{ 
			dmg = AxiObject.Instantiate(GameManager.instance.Damage_Font, position, rotation).GetComponent<Dmg_Font>();
			srcColor1 = dmg.sr_font_Obj[0].color;
            srcColor2 = dmg.sr_font_Obj[1].color;
            srcColor3 = dmg.sr_font_Obj[2].color;
            srcColor4 = dmg.sr_font_Obj[3].color;
        }

        dmg.sr_font_Obj[0].color = srcColor1;
        dmg.sr_font_Obj[1].color = srcColor2;
        dmg.sr_font_Obj[2].color = srcColor3;
        dmg.sr_font_Obj[3].color = srcColor4;

        dmg.transform.localScale = Vector3.one;
		dmg.Set_Number(num, type);
		dmg.Opacity = 1f;
		dmg.gameObject.SetActive(true);
		GameObject.DontDestroyOnLoad(dmg);
		return dmg;
	}
	static void ReleaseDmg(Dmg_Font dmg)
	{
		dmg.gameObject.SetActive(false);
		mQueue_Obj.Enqueue(dmg);
	}
	#endregion

	public global::UnityEngine.GameObject[] font_Obj;
	private SpriteRenderer[] sr_font_Obj;

	private float Life_Timer;

	private int ColorType;

	private bool onCritical;

	private float Opacity = 1f;

	private global::UnityEngine.Color ColorTarget = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

    GameManager GM => GameManager.instance;

	private void Awake()
	{
		sr_font_Obj = new SpriteRenderer[font_Obj.Length];
		for (int i = 0; i < font_Obj.Length; i++)
		{
			sr_font_Obj[i] = font_Obj[i].GetComponent<SpriteRenderer>();
		}
	}

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	public void Set_Number(int Num, int type)
	{
		//global::UnityEngine.Sprite[] array = global::UnityEngine.Resources.LoadAll<global::UnityEngine.Sprite>("UI/256_Font");
		global::UnityEngine.Sprite[] array = AxiResources.LoadAllSprite("UI/256_Font");
		int num = Num;
		if (Num > 9999)
		{
			Num = 9999;
		}
		if (Num >= 1000)
		{
			sr_font_Obj[3].sprite = array[Num / 1000];
			Num %= 1000;
		}
		if (Num >= 100)
		{
			sr_font_Obj[2].sprite = array[Num / 100];
			Num %= 100;
		}
		else if (num >= 1000)
		{
			sr_font_Obj[2].sprite = array[0];
		}
		if (Num >= 10)
		{
			sr_font_Obj[1].sprite = array[Num / 10];
			Num %= 10;
		}
		else if (num >= 100)
		{
			sr_font_Obj[1].sprite = array[0];
		}
		sr_font_Obj[0].sprite = array[Num];
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
		sr_font_Obj[0].color = ColorTarget;
		sr_font_Obj[1].color = ColorTarget;
		sr_font_Obj[2].color = ColorTarget;
		sr_font_Obj[3].color = ColorTarget;
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
				//global::UnityEngine.Object.Destroy(base.gameObject);
				ReleaseDmg(this);
			}
			global::UnityEngine.Color color = new global::UnityEngine.Color(ColorTarget.r, ColorTarget.g, ColorTarget.b, Opacity);
			sr_font_Obj[0].color = color;
			sr_font_Obj[1].color = color;
			sr_font_Obj[2].color = color;
			sr_font_Obj[3].color = color;
		}
	}

	private void Set_Opacity()
	{
	}
}
