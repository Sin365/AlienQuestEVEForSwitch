public class Save_Block : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject Top_1;

	public global::UnityEngine.GameObject Top_2;

	public global::UnityEngine.GameObject Head;

	public global::UnityEngine.GameObject Inner;

	public global::UnityEngine.GameObject Cover;

	public global::UnityEngine.GameObject Side_L;

	public global::UnityEngine.GameObject Side_R;

	public global::UnityEngine.Transform pos_Top_1_Open;

	public global::UnityEngine.Transform pos_Top_1_Close;

	public global::UnityEngine.Transform pos_Top_2_Open;

	public global::UnityEngine.Transform pos_Top_2_Close;

	public global::UnityEngine.Transform pos_Head_Open;

	public global::UnityEngine.Transform pos_Head_Close;

	public global::UnityEngine.Transform pos_Inner_Open;

	public global::UnityEngine.Transform pos_Inner_Close;

	public global::UnityEngine.Transform pos_Cover_Open;

	public global::UnityEngine.Transform pos_Cover_Close;

	public global::UnityEngine.Transform pos_Side_L_Open;

	public global::UnityEngine.Transform pos_Side_L_Close;

	public global::UnityEngine.Transform pos_Side_R_Open;

	public global::UnityEngine.Transform pos_Side_R_Close;

	public global::UnityEngine.SpriteRenderer Glow_1;

	public global::UnityEngine.SpriteRenderer Glow_2;

	public global::UnityEngine.SpriteRenderer Glow_3;

	public global::UnityEngine.SpriteRenderer Glow_4;

	public global::UnityEngine.SpriteRenderer Glow_4_Top;

	public global::UnityEngine.Transform pos_ActiveGlow_Top;

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
		if (GM.Paused)
		{
			return;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
		Sound_Timer += global::UnityEngine.Time.deltaTime;
		float num = global::UnityEngine.Vector3.Distance(Player.transform.position, pos_Inner_Open.position);
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
					GM.Get_HP(1000);
					global::UnityEngine.GameObject.Find("Menu").SendMessage("Sound_Get_FullHP");
					Glow_2.color = color_On;
				}
				else if (!Change_Cloth && !GM.onCloth)
				{
					Active_Heal = true;
					GM.Get_HP(1000);
					global::UnityEngine.GameObject.Find("Menu").SendMessage("Sound_Get_FullHP");
					Glow_2.color = color_On;
					Change_Cloth = true;
				}
			}
			if (Active_Heal && Heal_Timer < 2f)
			{
				ActiveGlow_Timer += global::UnityEngine.Time.deltaTime;
				if (ActiveGlow_Timer > 0.04f)
				{
					ActiveGlow_Timer = 0f;
					global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(activeGlow, pos_Inner_Open.position, base.transform.rotation) as global::UnityEngine.GameObject;
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
			if (!wasActive && GM.EventState != 200)
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
			Top_1.transform.localPosition = global::UnityEngine.Vector3.Lerp(Top_1.transform.localPosition, pos_Top_1_Open.localPosition, global::UnityEngine.Time.deltaTime * 5f);
			Top_2.transform.localPosition = global::UnityEngine.Vector3.Lerp(Top_2.transform.localPosition, pos_Top_2_Open.localPosition, global::UnityEngine.Time.deltaTime * 5f);
			Head.transform.localPosition = global::UnityEngine.Vector3.Lerp(Head.transform.localPosition, pos_Head_Open.localPosition, global::UnityEngine.Time.deltaTime * 5f);
			Inner.transform.localPosition = global::UnityEngine.Vector3.Lerp(Inner.transform.localPosition, pos_Inner_Open.localPosition, global::UnityEngine.Time.deltaTime * 5f);
			Cover.transform.localPosition = global::UnityEngine.Vector3.Lerp(Cover.transform.localPosition, pos_Cover_Open.localPosition, global::UnityEngine.Time.deltaTime * 5f);
			Side_L.transform.localPosition = global::UnityEngine.Vector3.Lerp(Side_L.transform.localPosition, pos_Side_L_Open.localPosition, global::UnityEngine.Time.deltaTime * 5f);
			Side_R.transform.localPosition = global::UnityEngine.Vector3.Lerp(Side_R.transform.localPosition, pos_Side_R_Open.localPosition, global::UnityEngine.Time.deltaTime * 5f);
			Glow_1.color = global::UnityEngine.Color.Lerp(Glow_1.color, color_On, global::UnityEngine.Time.deltaTime * 0.3f);
			Glow_2.color = global::UnityEngine.Color.Lerp(Glow_2.color, color_Half, global::UnityEngine.Time.deltaTime * 3f);
			Glow_3.color = global::UnityEngine.Color.Lerp(Glow_3.color, color_On, global::UnityEngine.Time.deltaTime * 6f);
			Glow_4.color = global::UnityEngine.Color.Lerp(Glow_4.color, color_On, global::UnityEngine.Time.deltaTime * 6f);
			Glow_4_Top.color = global::UnityEngine.Color.Lerp(Glow_4_Top.color, color_On, global::UnityEngine.Time.deltaTime * 6f);
		}
		else
		{
			Top_1.transform.localPosition = global::UnityEngine.Vector3.Lerp(Top_1.transform.localPosition, pos_Top_1_Close.localPosition, global::UnityEngine.Time.deltaTime * 5f);
			Top_2.transform.localPosition = global::UnityEngine.Vector3.Lerp(Top_2.transform.localPosition, pos_Top_2_Close.localPosition, global::UnityEngine.Time.deltaTime * 5f);
			Head.transform.localPosition = global::UnityEngine.Vector3.Lerp(Head.transform.localPosition, pos_Head_Close.localPosition, global::UnityEngine.Time.deltaTime * 5f);
			Inner.transform.localPosition = global::UnityEngine.Vector3.Lerp(Inner.transform.localPosition, pos_Inner_Close.localPosition, global::UnityEngine.Time.deltaTime * 5f);
			Cover.transform.localPosition = global::UnityEngine.Vector3.Lerp(Cover.transform.localPosition, pos_Cover_Close.localPosition, global::UnityEngine.Time.deltaTime * 5f);
			Side_L.transform.localPosition = global::UnityEngine.Vector3.Lerp(Side_L.transform.localPosition, pos_Side_L_Close.localPosition, global::UnityEngine.Time.deltaTime * 5f);
			Side_R.transform.localPosition = global::UnityEngine.Vector3.Lerp(Side_R.transform.localPosition, pos_Side_R_Close.localPosition, global::UnityEngine.Time.deltaTime * 5f);
			Glow_1.color = global::UnityEngine.Color.Lerp(Glow_1.color, color_Off, global::UnityEngine.Time.deltaTime * 10f);
			Glow_2.color = global::UnityEngine.Color.Lerp(Glow_2.color, color_Off, global::UnityEngine.Time.deltaTime * 3f);
			Glow_3.color = global::UnityEngine.Color.Lerp(Glow_3.color, color_Off, global::UnityEngine.Time.deltaTime * 6f);
			Glow_4.color = global::UnityEngine.Color.Lerp(Glow_4.color, color_Off, global::UnityEngine.Time.deltaTime * 6f);
			Glow_4_Top.color = global::UnityEngine.Color.Lerp(Glow_4_Top.color, color_Off, global::UnityEngine.Time.deltaTime * 6f);
		}
	}

	private void Save()
	{
		Glow_2.color = color_On;
		Heal_Timer = 0.51f;
		Active_Heal = true;
	}
}
