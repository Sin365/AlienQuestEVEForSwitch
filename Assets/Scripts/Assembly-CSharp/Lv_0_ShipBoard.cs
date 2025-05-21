public class Lv_0_ShipBoard : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.SpriteRenderer Glow_top;

	public global::UnityEngine.SpriteRenderer Glow_Bottom;

	public global::UnityEngine.GameObject activeGlow;

	public global::UnityEngine.GameObject info_UpArrow;

	private bool SaveOn;

	private bool wasActive;

	private float Life_Timer;

	private float Heal_Timer;

	private float Sound_Timer;

	private bool Active_Heal;

	private float Active_Timer;

	private float ActiveGlow_Timer;

	private bool Change_Cloth;

	private global::UnityEngine.Color color_On = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_Half = new global::UnityEngine.Color(1f, 1f, 1f, 0.5f);

	private global::UnityEngine.Color color_Off = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	private float Opacity_1;

	private float Opacity_2;

	private GameManager GM;

	private SaveMenu_Control SaveMenu;

	private global::UnityEngine.GameObject Player;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		SaveMenu = global::UnityEngine.GameObject.Find("Menu_Save").GetComponent<SaveMenu_Control>();
		Player = global::UnityEngine.GameObject.Find("Player");
		SaveMenu.save_Block = base.gameObject;
	}

	private void Update()
	{
		if (GM.onEvent)
		{
			if (SaveOn)
			{
				SaveOn = false;
				info_UpArrow.GetComponent<Info_Gate>().on_Info = false;
			}
		}
		else
		{
			if (GM.Paused)
			{
				return;
			}
			Life_Timer += global::UnityEngine.Time.deltaTime;
			Sound_Timer += global::UnityEngine.Time.deltaTime;
			float num = global::UnityEngine.Vector3.Distance(Player.transform.position, base.transform.position);
			SaveMenu.Dist = num;
			if (num < 1.2f)
			{
				Heal_Timer += global::UnityEngine.Time.deltaTime;
				SaveMenu.dist_Timer = 0.5f;
				if (Heal_Timer > 0.5f)
				{
					if (GM.HP < GM.HP_Max)
					{
						Active_Heal = true;
						GM.Get_HP(1001);
						global::UnityEngine.GameObject.Find("Menu").SendMessage("Sound_Get_FullHP");
						Glow_top.color = color_On;
					}
					else if (!Change_Cloth && !GM.onCloth)
					{
						Active_Heal = true;
						GM.Get_HP(1001);
						global::UnityEngine.GameObject.Find("Menu").SendMessage("Sound_Get_FullHP");
						Glow_top.color = color_On;
						Change_Cloth = true;
					}
				}
				if (Active_Heal && Heal_Timer < 2f)
				{
					ActiveGlow_Timer += global::UnityEngine.Time.deltaTime;
					if (ActiveGlow_Timer > 0.04f)
					{
						ActiveGlow_Timer = 0f;
						global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(activeGlow, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
						if (Heal_Timer > 1f)
						{
							gameObject.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, 2f - Heal_Timer);
						}
					}
					if (Heal_Timer > 0.8f && !GM.onCloth)
					{
						GM.Get_Cloth();
					}
				}
				if (GM.onPoison)
				{
					GM.Poison_Timer = 0f;
					GM.Poison_DMG = 0;
				}
			}
			else
			{
				Heal_Timer = 0f;
				Active_Heal = false;
				Change_Cloth = false;
				if (GM.onSave)
				{
					GM.onSave = false;
				}
			}
			if (num < 1.8f)
			{
				SaveOn = true;
				if (!wasActive)
				{
					info_UpArrow.GetComponent<Info_Gate>().on_Info = true;
				}
				wasActive = true;
			}
			else
			{
				SaveOn = false;
				info_UpArrow.GetComponent<Info_Gate>().on_Info = false;
			}
			if (SaveOn)
			{
				Glow_top.color = global::UnityEngine.Color.Lerp(Glow_top.color, color_Half, global::UnityEngine.Time.deltaTime * 5f);
				Glow_Bottom.color = global::UnityEngine.Color.Lerp(Glow_Bottom.color, color_Half, global::UnityEngine.Time.deltaTime * 5f);
			}
			else
			{
				Glow_top.color = global::UnityEngine.Color.Lerp(Glow_top.color, color_Off, global::UnityEngine.Time.deltaTime * 2f);
				Glow_Bottom.color = global::UnityEngine.Color.Lerp(Glow_Bottom.color, color_Off, global::UnityEngine.Time.deltaTime * 2f);
			}
		}
	}

	private void Save()
	{
		Glow_top.color = color_On;
		Heal_Timer = 0.51f;
		Active_Heal = true;
	}
}
