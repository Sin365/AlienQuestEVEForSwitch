using UnityEngine;
using UnityEngine.UI;

public class UI_Control : global::UnityEngine.MonoBehaviour
{
	private float Damage_Timer = 0.6f;

	private float Mana_Timer;

	private float HP_Glow_TImer;

	private float MP_Glow_TImer;

	private float Size_HP = 1f;

	private float Size_MP = 1f;

	private bool onSkill = true;

	private float RotNow;

	private float RotFrom;

	private float RotTarget;

	private float RotOpacity = 1f;

	private float RotSize = 100f;

	private float Rot_Timer;

	private float lvUp_Timer;

	private bool zeroTric;

	private int skill_Total;

	public global::UnityEngine.GameObject Skill_Border;

	public global::UnityEngine.RectTransform[] Pos_Skill_Index;

	public global::UnityEngine.GameObject[] Skill_Img;

	private int[] Pos_skill_Num = new int[5] { 2, 3, 4, 0, 1 };

	private global::UnityEngine.Color[] skill_color = new global::UnityEngine.Color[5];

	private global::UnityEngine.Vector3[] skill_scale = new global::UnityEngine.Vector3[5];

	private int[] sortingOrder = new int[5] { 1003, 1001, 1000, 1000, 1001 };

	private bool on_BossBar;

	private float Boss_Timer;

	public Monster Boss_Mon;

	private global::UnityEngine.Color BossBar_Glow_color;

	private global::UnityEngine.Color BossBar_color;

	private global::UnityEngine.Color BossBar_Red_color;

	private global::UnityEngine.Vector3 pos_BossBar_On;

	private global::UnityEngine.Vector3 pos_BossBar_Off;

	private global::UnityEngine.Vector3 pos_Status_On;

	private global::UnityEngine.Vector3 pos_Status_Off;

	private global::UnityEngine.Vector3 pos_MiniMap_On;

	private global::UnityEngine.Vector3 pos_MiniMap_Off;

	private global::UnityEngine.Vector3 pos_BarTop_On;

	private global::UnityEngine.Vector3 pos_BarTop_Off;

    GameManager GM => GameManager.instance;
    private RectTransform rect_Status;
    private RectTransform rect_MiniMap;
    private RectTransform rect_Bar_Top;
    private Image img_BossBar_Glow;
    private Image img_BossBar;
    private Image img_BossBar_Red_Glow;
    private RectTransform rect_HealthBar_Boss;
    private Image img_BossBar_Red_Vertical;
    private Image img_HP_Bar;
    private Image img_HP_Bar_Glow;
    private Image img_MP_Bar;
    private Image img_MP_Bar_Glow;
    private GameObject gobj_LVSecond;
    private SpriteRenderer sp_UI_Circle_2;
    private GameObject gobj_UI_Circle_2;
    private Text txt_Level_Text;
    private Text txt_HP_Text;
    private Text txt_MP_Text;
    private SpriteRenderer sp_UI_HP_Bar_R;
    private SpriteRenderer sp_UI_MP_Bar_R;
    private RectTransform rect_UI_MP_Edge;
    private RectTransform rect_UI_HP_Edge;
    private Image img_UI_HP_Edge;
    private Image img_UI_MP_Edge;
    private GameObject gobj_Menu;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();

        #region 集中Find，全局只Find一次
        //
        rect_Status = global::UnityEngine.GameObject.Find("Status").GetComponent<global::UnityEngine.RectTransform>();
        rect_MiniMap = global::UnityEngine.GameObject.Find("MiniMap").GetComponent<global::UnityEngine.RectTransform>();
        rect_Bar_Top = global::UnityEngine.GameObject.Find("Bar_Top").GetComponent<global::UnityEngine.RectTransform>();
        img_BossBar_Glow = global::UnityEngine.GameObject.Find("BossBar_Glow").GetComponent<global::UnityEngine.UI.Image>();
        img_BossBar = global::UnityEngine.GameObject.Find("BossBar").GetComponent<global::UnityEngine.UI.Image>();
        img_BossBar_Red_Glow = global::UnityEngine.GameObject.Find("BossBar_Red_Glow").GetComponent<global::UnityEngine.UI.Image>();
        rect_HealthBar_Boss = global::UnityEngine.GameObject.Find("HealthBar_Boss").GetComponent<global::UnityEngine.RectTransform>();
        img_BossBar_Red_Vertical = global::UnityEngine.GameObject.Find("BossBar_Red_Vertical").GetComponent<global::UnityEngine.UI.Image>();

        img_HP_Bar = global::UnityEngine.GameObject.Find("HP_Bar").GetComponent<global::UnityEngine.UI.Image>();
        img_HP_Bar_Glow = global::UnityEngine.GameObject.Find("HP_Bar_Glow").GetComponent<global::UnityEngine.UI.Image>();
        img_MP_Bar = global::UnityEngine.GameObject.Find("MP_Bar").GetComponent<global::UnityEngine.UI.Image>();
        img_MP_Bar_Glow = global::UnityEngine.GameObject.Find("MP_Bar_Glow").GetComponent<global::UnityEngine.UI.Image>();

        gobj_LVSecond = global::UnityEngine.GameObject.Find("LVSecond");
        sp_UI_Circle_2 = global::UnityEngine.GameObject.Find("UI_Circle_2").GetComponent<global::UnityEngine.SpriteRenderer>();
        gobj_UI_Circle_2 = global::UnityEngine.GameObject.Find("UI_Circle_2");

        txt_Level_Text = global::UnityEngine.GameObject.Find("Level_Text").GetComponent<global::UnityEngine.UI.Text>();

        txt_HP_Text = global::UnityEngine.GameObject.Find("HP_Text").GetComponent<global::UnityEngine.UI.Text>();
        txt_MP_Text = global::UnityEngine.GameObject.Find("MP_Text").GetComponent<global::UnityEngine.UI.Text>();

        sp_UI_HP_Bar_R = global::UnityEngine.GameObject.Find("UI_HP_Bar_R").GetComponent<global::UnityEngine.SpriteRenderer>();
        sp_UI_MP_Bar_R = global::UnityEngine.GameObject.Find("UI_MP_Bar_R").GetComponent<global::UnityEngine.SpriteRenderer>();

        rect_UI_MP_Edge = global::UnityEngine.GameObject.Find("UI_MP_Edge").GetComponent<global::UnityEngine.RectTransform>();
        rect_UI_HP_Edge = global::UnityEngine.GameObject.Find("UI_HP_Edge").GetComponent<global::UnityEngine.RectTransform>();

        img_UI_HP_Edge = global::UnityEngine.GameObject.Find("UI_HP_Edge").GetComponent<global::UnityEngine.UI.Image>();
        img_UI_MP_Edge = global::UnityEngine.GameObject.Find("UI_MP_Edge").GetComponent<global::UnityEngine.UI.Image>();

        gobj_Menu = global::UnityEngine.GameObject.Find("Menu");
        #endregion

        skill_scale[0] = new global::UnityEngine.Vector3(100f, 100f, 1f);
		skill_scale[1] = new global::UnityEngine.Vector3(85f, 85f, 1f);
		skill_scale[2] = new global::UnityEngine.Vector3(70f, 70f, 1f);
		skill_scale[3] = new global::UnityEngine.Vector3(70f, 70f, 1f);
		skill_scale[4] = new global::UnityEngine.Vector3(85f, 85f, 1f);
		skill_color[0] = new global::UnityEngine.Color(1f, 1f, 1f, 1f);
		skill_color[1] = new global::UnityEngine.Color(1f, 1f, 1f, 0.1f);
		skill_color[2] = new global::UnityEngine.Color(1f, 1f, 1f, 0.1f);
		skill_color[3] = new global::UnityEngine.Color(1f, 1f, 1f, 0.1f);
		skill_color[4] = new global::UnityEngine.Color(1f, 1f, 1f, 0.1f);
		Skill_Img[1].GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
		Skill_Img[2].GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
		Skill_Img[3].GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
		Skill_Img[4].GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
		if (Skill_Img.Length > 0)
		{
			for (int i = 0; i < 5; i++)
			{
			}
		}
		pos_Status_On = rect_Status.localPosition;
		pos_MiniMap_On = rect_MiniMap.localPosition;
		pos_BarTop_On = rect_Bar_Top.localPosition;
		pos_Status_Off = new global::UnityEngine.Vector3(pos_Status_On.x - 600f, pos_Status_On.y + 300f, 0f);
		pos_MiniMap_Off = new global::UnityEngine.Vector3(pos_MiniMap_On.x + 550f, pos_MiniMap_On.y + 300f, 0f);
		pos_BarTop_Off = new global::UnityEngine.Vector3(pos_BarTop_On.x, pos_BarTop_On.y + 1500f, 0f);
		rect_Status.localPosition = pos_Status_Off;
		rect_MiniMap.localPosition = pos_MiniMap_Off;
		rect_Bar_Top.localPosition = pos_BarTop_Off;
		BossBar_Glow_color = img_BossBar_Glow.color;
		BossBar_color = img_BossBar.color;
		BossBar_Red_color = img_BossBar_Red_Glow.color;
		pos_BossBar_On = rect_HealthBar_Boss.localPosition;
		pos_BossBar_Off = new global::UnityEngine.Vector3(pos_BossBar_On.x + 200f, pos_BossBar_On.y, 0f);
		rect_HealthBar_Boss.localPosition = pos_BossBar_Off;
	}

	public void Set_Boss_Start()
	{
		on_BossBar = true;
		Boss_Timer = 0f;
		img_BossBar_Glow.color = BossBar_Glow_color;
		img_BossBar.color = BossBar_color;
		img_BossBar_Red_Glow.color = BossBar_Red_color;
		img_BossBar_Red_Vertical.color = BossBar_Red_color;
		img_BossBar.fillAmount = 0f;
	}

	public void Set_Boss_Death()
	{
		on_BossBar = false;
	}

	private void Show_Boss_Bar()
	{
		rect_HealthBar_Boss.localPosition = global::UnityEngine.Vector3.Lerp(rect_HealthBar_Boss.localPosition, pos_BossBar_On, global::UnityEngine.Time.deltaTime * 8f);
	}

	private void Hide_Boss_Bar()
	{
		rect_HealthBar_Boss.localPosition = pos_BossBar_Off;
	}

	private void Display_Status()
	{
		rect_Status.localPosition = global::UnityEngine.Vector3.Lerp(rect_Status.localPosition, pos_Status_On, global::UnityEngine.Time.deltaTime * 10f);
		rect_MiniMap.localPosition = global::UnityEngine.Vector3.Lerp(rect_MiniMap.localPosition, pos_MiniMap_On, global::UnityEngine.Time.deltaTime * 10f);
		rect_Bar_Top.localPosition = global::UnityEngine.Vector3.Lerp(rect_Bar_Top.localPosition, pos_BarTop_Off, global::UnityEngine.Time.deltaTime * 12f);
	}

	private void Hide_Status()
	{
		rect_Status.localPosition = global::UnityEngine.Vector3.Lerp(rect_Status.localPosition, pos_Status_Off, global::UnityEngine.Time.deltaTime * 2f);
		rect_MiniMap.localPosition = global::UnityEngine.Vector3.Lerp(rect_MiniMap.localPosition, pos_MiniMap_Off, global::UnityEngine.Time.deltaTime * 2f);
		if (GM.onMenu)
		{
			rect_Bar_Top.localPosition = global::UnityEngine.Vector3.Lerp(rect_Bar_Top.localPosition, pos_BarTop_On, global::UnityEngine.Time.deltaTime * 12f);
		}
	}

	public void HideInst_Status()
	{
		rect_Status.localPosition = pos_Status_Off;
		rect_MiniMap.localPosition = pos_MiniMap_Off;
		rect_Bar_Top.localPosition = pos_BarTop_On;
	}

	private void Update()
	{
		if (GM.GameOver || (GM.onEvent && GM.Hscene_Num != 96))
		{
			Hide_Status();
		}
		else if (!GM.Paused)
		{
			if (Damage_Timer > 0f)
			{
				Damage_Effect();
			}
			else if (Size_HP < 0.25f)
			{
				HP_Low();
			}
			else
			{
				HP_Glow();
			}
			if (Mana_Timer > 0f)
			{
				Mana_Effect();
			}
			else
			{
				MP_Glow();
			}
			Display_Status();
			Check_Skill();
			Size_HP = global::UnityEngine.Mathf.Lerp(Size_HP, (float)GM.HP / (float)GM.HP_Max, global::UnityEngine.Time.deltaTime * 2f);
			Size_MP = global::UnityEngine.Mathf.Lerp(Size_MP, (float)GM.MP / (float)GM.MP_Max, global::UnityEngine.Time.deltaTime * 5f);
			img_HP_Bar.fillAmount = Size_HP;
			img_HP_Bar_Glow.fillAmount = Size_HP;
			img_MP_Bar.fillAmount = Size_MP;
			img_MP_Bar_Glow.fillAmount = Size_MP;
			RotNow = global::UnityEngine.Mathf.Lerp(RotNow, RotTarget, global::UnityEngine.Time.deltaTime * 5f);
			if (zeroTric && RotNow < 0f)
			{
				RotNow = 360f + RotNow;
				RotTarget = 360f - 360f * GM.Get_ExpRatio();
				zeroTric = false;
			}
			gobj_LVSecond.transform.rotation = global::UnityEngine.Quaternion.Euler(0f, 0f, RotNow);
			if (lvUp_Timer > 0f)
			{
				lvUp_Timer -= global::UnityEngine.Time.deltaTime;
				RotOpacity = global::UnityEngine.Mathf.Lerp(RotOpacity, 0.5f, global::UnityEngine.Time.deltaTime * 0.3f);
				RotSize = global::UnityEngine.Mathf.Lerp(RotSize, 100f, global::UnityEngine.Time.deltaTime * 0.1f);
				sp_UI_Circle_2.color = new global::UnityEngine.Color(1f, 1f, 1f, RotOpacity);
				gobj_UI_Circle_2.transform.localScale = new global::UnityEngine.Vector3(RotSize, RotSize, 1f);
			}
			else
			{
				if (RotOpacity > 0.5f)
				{
					RotOpacity = global::UnityEngine.Mathf.Lerp(RotOpacity, 0.5f, global::UnityEngine.Time.deltaTime * 0.3f);
					sp_UI_Circle_2.color = new global::UnityEngine.Color(1f, 1f, 1f, RotOpacity);
				}
				if (RotNow < 50f && RotNow > 0f)
				{
					Rot_Timer += global::UnityEngine.Time.deltaTime;
					RotSize = 100f + (1f + global::UnityEngine.Mathf.Sin(Rot_Timer * 10f)) * 2f;
					gobj_UI_Circle_2.transform.localScale = new global::UnityEngine.Vector3(RotSize, RotSize, 1f);
				}
				else
				{
					RotSize = global::UnityEngine.Mathf.Lerp(RotSize, 100f, global::UnityEngine.Time.deltaTime * 0.5f);
					gobj_UI_Circle_2.transform.localScale = new global::UnityEngine.Vector3(RotSize, RotSize, 1f);
				}
			}
			txt_Level_Text.text = GM.Level.ToString();
			txt_HP_Text.text = GM.HP.ToString();
			txt_MP_Text.text = GM.MP.ToString();
			sp_UI_HP_Bar_R.color = new global::UnityEngine.Color(1f, 1f, 1f, Size_HP);
			sp_UI_MP_Bar_R.color = new global::UnityEngine.Color(1f, 1f, 1f, Size_MP);
			rect_UI_HP_Edge.localPosition = new global::UnityEngine.Vector3(390f * Size_HP - 195f - 3f, 0f, 0f);
			rect_UI_MP_Edge.localPosition = new global::UnityEngine.Vector3(390f * Size_MP - 195f - 3f, 0f, 0f);
			if (on_BossBar)
			{
				if (Boss_Mon != null)
				{
					if (Boss_Timer > 1.5f)
					{
						img_BossBar.fillAmount = global::UnityEngine.Mathf.Lerp(img_BossBar.fillAmount, Boss_Mon.HP_Ratio, global::UnityEngine.Time.deltaTime * 2f);
						if (Boss_Mon.HP_Ratio > 0.5f)
						{
							img_BossBar.color = BossBar_color;
						}
						else if (Boss_Mon.HP_Ratio > 0.2f)
						{
							img_BossBar.color = global::UnityEngine.Color.Lerp(img_BossBar.color, new global::UnityEngine.Color(1f, 1f, 0.5f, 1f), global::UnityEngine.Time.deltaTime * 1f);
						}
						else
						{
							img_BossBar.color = global::UnityEngine.Color.Lerp(img_BossBar.color, new global::UnityEngine.Color(1f, 0f, 0f, 1f), global::UnityEngine.Time.deltaTime * 2f);
							img_BossBar_Red_Glow.color = global::UnityEngine.Color.Lerp(img_BossBar_Red_Glow.color, new global::UnityEngine.Color(1f, 0f, 0f, 0.5f), global::UnityEngine.Time.deltaTime * 1f);
							img_BossBar_Red_Vertical.color = img_BossBar_Red_Glow.color;
						}
						img_BossBar_Glow.color = new global::UnityEngine.Color(BossBar_Glow_color.r, BossBar_Glow_color.g, BossBar_Glow_color.b, 0.2f + Boss_Mon.HP_Ratio * 0.55f);
						Show_Boss_Bar();
					}
					else
					{
						Boss_Timer += global::UnityEngine.Time.deltaTime;
					}
				}
				else
				{
					on_BossBar = false;
					Hide_Boss_Bar();
				}
			}
			else if (Boss_Mon != null)
			{
				Set_Boss_Start();
			}
			else
			{
				Hide_Boss_Bar();
			}
		}
		else if (on_BossBar)
		{
			Hide_Boss_Bar();
		}
	}

	public void Set_LevelUP()
	{
		lvUp_Timer = 2f;
		zeroTric = true;
		RotOpacity = 1f;
		RotSize = 108f;
		sp_UI_Circle_2.color = new global::UnityEngine.Color(1f, 1f, 1f, RotOpacity);
		gobj_UI_Circle_2.transform.localScale = new global::UnityEngine.Vector3(RotSize, RotSize, 1f);
		Set_Damage();
		Set_Mana();
	}

	private void Damage_Effect()
	{
		Damage_Timer -= global::UnityEngine.Time.deltaTime;
		if (Damage_Timer <= 0f)
		{
			Damage_Timer = 0f;
			img_HP_Bar_Glow.color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
		}
		else
		{
			float a = img_HP_Bar_Glow.color.a;
			a = global::UnityEngine.Mathf.Lerp(a, 0f, global::UnityEngine.Time.deltaTime * 3f);
			img_HP_Bar_Glow.color = new global::UnityEngine.Color(1f, 1f, 1f, a);
		}
	}

	private void HP_Glow()
	{
		float num = 0f;
		if ((double)Size_HP > 0.9)
		{
			num = img_UI_HP_Edge.color.a;
			num = global::UnityEngine.Mathf.Lerp(num, 0.2f, global::UnityEngine.Time.deltaTime * 1f);
		}
		else
		{
			HP_Glow_TImer += global::UnityEngine.Time.deltaTime;
			num = 0.3f + (1f + global::UnityEngine.Mathf.Cos(HP_Glow_TImer * 4f)) * 0.25f;
		}
		img_UI_HP_Edge.color = new global::UnityEngine.Color(1f, 1f, 1f, num);
		if (img_HP_Bar_Glow.color.a > 0f)
		{
			img_HP_Bar_Glow.color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
		}
	}

	private void HP_Low()
	{
		HP_Glow_TImer += global::UnityEngine.Time.deltaTime;
		float a = (1f + global::UnityEngine.Mathf.Cos(HP_Glow_TImer * 10f)) * 0.2f;
		img_HP_Bar_Glow.color = new global::UnityEngine.Color(1f, 1f, 1f, a);
	}

	private void Mana_Effect()
	{
		Mana_Timer -= global::UnityEngine.Time.deltaTime;
		if (Mana_Timer <= 0f)
		{
			Mana_Timer = 0f;
			img_MP_Bar_Glow.color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
		}
		else
		{
			float a = img_MP_Bar_Glow.color.a;
			a = global::UnityEngine.Mathf.Lerp(a, 0f, global::UnityEngine.Time.deltaTime * 3f);
			img_MP_Bar_Glow.color = new global::UnityEngine.Color(1f, 1f, 1f, a);
		}
	}

	private void MP_Glow()
	{
		float num = 0f;
		if ((double)Size_MP > 0.9)
		{
			num = img_UI_MP_Edge.color.a;
			num = global::UnityEngine.Mathf.Lerp(num, 0.2f, global::UnityEngine.Time.deltaTime * 1f);
		}
		else
		{
			MP_Glow_TImer += global::UnityEngine.Time.deltaTime;
			num = 0.3f + (1f + global::UnityEngine.Mathf.Cos(MP_Glow_TImer * 4f)) * 0.25f;
		}
		img_UI_MP_Edge.color = new global::UnityEngine.Color(1f, 1f, 1f, num);
	}

	public void Set_Damage()
	{
		Damage_Timer = 0.7f;
		HP_Glow_TImer = 0f;
		img_HP_Bar_Glow.color = new global::UnityEngine.Color(1f, 1f, 1f, 0.7f);
		img_UI_HP_Edge.color = new global::UnityEngine.Color(1f, 1f, 1f, 1f);
	}

	public void Set_Mana()
	{
		Mana_Timer = 1f;
		MP_Glow_TImer = 0f;
		img_MP_Bar_Glow.color = new global::UnityEngine.Color(1f, 1f, 1f, 0.4f);
		img_UI_MP_Edge.color = new global::UnityEngine.Color(1f, 1f, 1f, 1f);
	}

	public void Set_ExpRotate(float expRatio)
	{
		if (zeroTric)
		{
			if (RotNow < 360f - 360f * expRatio)
			{
				RotTarget -= 360f * expRatio;
			}
			else
			{
				RotTarget = 360f - 360f * expRatio;
			}
		}
		else if (expRatio < 1f)
		{
			RotTarget = 360f - 360f * expRatio;
		}
		else if (expRatio > 1f)
		{
			RotTarget = 360f - 360f * expRatio;
			zeroTric = true;
		}
		else
		{
			RotTarget = 0f;
		}
		if (RotNow == 0f)
		{
			RotNow = 359.99f;
		}
		RotFrom = gobj_LVSecond.transform.rotation.eulerAngles.z;
	}

	public void Change_Skill()
	{
		if (global::UnityEngine.Time.timeSinceLevelLoad > 1f)
		{
			global::UnityEngine.GameObject.Find("Menu").SendMessage("Sound_Move_2");
		}
		Skill_Border.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
		for (int i = 0; i < 5; i++)
		{
			int num = GM.onSkill_Index[i];
			if (num >= 0)
			{
				Skill_Img[num].GetComponent<global::UnityEngine.SpriteRenderer>().sortingOrder = sortingOrder[i];
			}
		}
	}

	private void Check_Skill()
	{
		for (int i = 0; i < 5; i++)
		{
			int num = GM.onSkill_Index[i];
			if (num >= 0)
			{
				if (GM.Skill_Total == 3 && i == 2)
				{
					Skill_Img[num].GetComponent<global::UnityEngine.RectTransform>().position = global::UnityEngine.Vector3.Lerp(Skill_Img[num].GetComponent<global::UnityEngine.RectTransform>().position, Pos_Skill_Index[4].position, global::UnityEngine.Time.deltaTime * 5f);
				}
				else if (GM.Skill_Total == 4 && i == 3)
				{
					Skill_Img[num].GetComponent<global::UnityEngine.RectTransform>().position = global::UnityEngine.Vector3.Lerp(Skill_Img[num].GetComponent<global::UnityEngine.RectTransform>().position, Pos_Skill_Index[4].position, global::UnityEngine.Time.deltaTime * 5f);
				}
				else
				{
					Skill_Img[num].GetComponent<global::UnityEngine.RectTransform>().position = global::UnityEngine.Vector3.Lerp(Skill_Img[num].GetComponent<global::UnityEngine.RectTransform>().position, Pos_Skill_Index[i].position, global::UnityEngine.Time.deltaTime * 5f);
				}
				Skill_Img[num].GetComponent<global::UnityEngine.RectTransform>().localScale = global::UnityEngine.Vector3.Lerp(Skill_Img[num].GetComponent<global::UnityEngine.RectTransform>().localScale, skill_scale[i], global::UnityEngine.Time.deltaTime * 5f);
				Skill_Img[num].GetComponent<global::UnityEngine.SpriteRenderer>().color = global::UnityEngine.Color.Lerp(Skill_Img[num].GetComponent<global::UnityEngine.SpriteRenderer>().color, skill_color[i], global::UnityEngine.Time.deltaTime * 4f);
				if (GM.MP >= GM.Skill_MP[num] && skill_color[i].a < 1f)
				{
					skill_color[i] = new global::UnityEngine.Color(1f, 1f, 1f, 1f);
				}
				else if (GM.MP < GM.Skill_MP[num] && skill_color[i].a == 1f)
				{
					skill_color[i] = new global::UnityEngine.Color(1f, 1f, 1f, 0.3f);
				}
			}
		}
		Skill_Border.GetComponent<global::UnityEngine.SpriteRenderer>().color = global::UnityEngine.Color.Lerp(Skill_Border.GetComponent<global::UnityEngine.SpriteRenderer>().color, new global::UnityEngine.Color(1f, 1f, 1f, 1f), global::UnityEngine.Time.deltaTime * 0.4f);
	}
}
