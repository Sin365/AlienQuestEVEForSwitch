using UnityEngine;
using UnityEngine.UI;

public class GameManager : global::UnityEngine.MonoBehaviour
{
    private int Slot_Num;

    public int EventState;

    public bool GameOver;

    private float GameOver_Timer;

    public bool Paused;

    public bool onEvent = true;

    public bool onHscene;

    public bool onDown;

    private float Down_Timer;

    public bool onFaceHugger;

    private float FaceHugger_Timer;

    private float FaceHugger_DMG_Timer;

    public bool onChestBurster;

    private float ChestBurster_Timer;

    public bool onChestBurster_Over;

    public bool onGatePass;

    public bool onMenu;

    public bool onMap;

    public bool onSave;

    public bool onTeleport;

    public bool onConsole;

    public bool onGameClear;

    public int Room_Num;

    public float Damage_Timer;

    public int Level = 1;

    public int HP = 92;

    public int HP_Max = 92;

    public int MP = 60;

    public int MP_Max = 60;

    public bool onCloth = true;

    private int ATK = 9;

    private int DEF = 8;

    private int STR = 12;

    private int CON = 18;

    private int INT = 16;

    private int LCK = 7;

    private int ExpNow;

    private int ExpNext;

    private float ExpRatio;

    private int Kills;

    private float Rate;

    public int StatPoints;

    public int Weapon_Num;

    public bool onWeapon_1;

    public bool onWeapon_2;

    public bool onWeapon_3;

    public bool onWeapon_4;

    public bool onWeapon_5;

    public int[] Weapon_DMG = new int[0];

    public int Skill_Num = 1;

    public bool onSkill_2;

    public bool onSkill_3;

    public bool onSkill_4;

    public bool onSkill_5;

    public int[] Skill_MP = new int[0];

    public int[] Skill_DMG = new int[0];

    public int Skill_Total = 1;

    private bool[] onSkill_List = new bool[5] { true, false, false, false, false };

    public int[] onSkill_Index = new int[5] { 0, -1, -1, -1, -1 };

    public bool onScrew;

    public bool onHighJump;

    public bool onSpeedUp;

    public bool onDBJump;

    public bool onBackDash;

    public bool onCard_1;

    public bool onCard_2;

    public bool onCard_3;

    public bool onCard_4;

    public bool onCard_5;

    public int Bonus_HP;

    public int Bonus_MP;

    public int Bonus_ATK;

    public int Bonus_Regen;

    private float MPRegen_Timer;

    public int Bonus_Blood;

    public int Bonus_Life;

    public int Potion_HP;

    public int Potion_MP;

    private int[] E_scene = new int[20];

    private int[] Map = new int[200]
    {
        1, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0
    };

    private int[] BonusItems = new int[50];

    private int[] H_scene = new int[70];

    private int[] H_Over = new int[20];

    public int Atk_Per_Str = 2;

    public int HP_Per_Con = 2;

    public int MP_Per_Int = 2;

    public int Hp_Per_BonusHp = 100;

    public int Mp_Per_BonusMp = 80;

    public int Atk_Per_BonusAtk = 20;

    public int Def_Per_Con = 1;

    private float screw_Timer = 1f;

    private float spin_Timer = 1f;

    private float speedUp_Timer = 1f;

    public int Hscene_Num;

    public float Hscene_Timer;

    public float[] Option_Volume = new float[2] { 1f, 1f };

    public int[] Option_Int = new int[5] { 0, 2, 2, 2, 2 };

    public int Language_Num;

    public global::UnityEngine.Vector2 Velcocity = new global::UnityEngine.Vector2(0f, 0f);

    private float Vel_Timer;

    private global::UnityEngine.Vector2 player_Velocity;

    public float resumeTimer;

    public float escapeTimer;

    private float PlayTime;

    private float Load_Timer;

    private bool DataLoaded;

    private int ChangeTimeScale;

    private int Input_Mode;

    public float User_Input_Timer;

    private bool isPressedDpad_R;

    private bool isPressedDpad_L;

    public bool onShield;

    public float Shield_Timer;

    public bool onPoison;

    public float Poison_Timer;

    public int Poison_DMG;

    private float Poison_DMG_Timer;

    public float PoisonSmog_Timer;

    public global::UnityEngine.Color color_Player = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

    private float color_Timer;

    public global::UnityEngine.GameObject Heal_Cross;

    public global::UnityEngine.GameObject Heal_Cross_Red;

    public global::UnityEngine.GameObject Heal_Cross_Blue;

    private float Potion_Timer;

    private float PotionRed_Timer;

    private float PotionBlue_Timer;

    private float Cross_Timer;

    private float CrossRed_Timer;

    private float CrossBlue_Timer;

    public global::UnityEngine.GameObject Damage_Font;

    public global::UnityEngine.GameObject info_Dialog;

    public global::UnityEngine.GameObject info_LevelUp;

    public global::UnityEngine.GameObject info_Mission;

    public global::UnityEngine.GameObject effect_LevelUp;

    public global::UnityEngine.GameObject effect_UsePotionHP;

    public global::UnityEngine.GameObject effect_UsePotionMP;

    private bool isFadeIn;

    private bool isFadeOut;

    private float FadeOpacity = 1f;

    private string FadeOutAction = string.Empty;

    private global::UnityEngine.GameObject BlackFade;

    public global::UnityEngine.GameObject Shield_Object;

    private Player_Control PC;

    private UI_Control UC;
    private Camera_Control cc_Main_Camera;
    private Rigidbody2D eg2d_Player;
    private GameObject gobj_Player;
    private GameObject gobj_EscapeTimer;
    private GameObject gobj_Main_Camera;
    private GameObject gobj_Player_DownGrab;
    private GameObject gobj_Player_Grab;
    private GameObject gobj_Ani;
    private Text txt_Text_TimePlay;
    private GameObject gobj_Text_TimePlay;
    private Text txt_Text_Fps;
    private GameObject gobj_Poison_On;
    private GameObject gobj_Pos_PlayerPoisonDmg;
    private Text txt_Poison_Text;
    private SpriteRenderer sr_Clock_TimePlay;
    private GameObject gobj_BGM_List;
    private SpriteRenderer sr_BlackPause;
    private Menu_Control mc_Menu;
    private GameObject gobj_Menu;
    private GameObject gobj_Pos_SitLock_1_C;
    private GameObject gobj_Player_ChestBurster;
    private SpriteRenderer sr_Ani_FaceHugger;
    private RectTransform sr_SelBorder_Skill;
    private Image img_SelBorder_Aug;
    private RectTransform rect_SelBorder_Aug;
    private Image img_Inv_Weapon_1;
    private Image img_Inv_Weapon_2;
    private Image img_Inv_Weapon_2_Glow;
    private Image img_Inv_Weapon_3;
    private Image img_Inv_Weapon_4;
    private Image img_Inv_Weapon_5;
    private Image img_Inv_Weapon_5_Glow;
    private Image img_Inv_Skill_1;
    private Image img_Inv_Skill_2;
    private Image img_Inv_Skill_3;
    private Image img_Inv_Skill_4;
    private Image img_Inv_Skill_5;
    private Image img_Inv_Skill_5_Glow;
    private Image img_Inv_Ability_1;
    private Image img_Inv_Ability_2;
    private Image img_Inv_Ability_3;
    private Image img_Inv_Ability_4;
    private Image img_Inv_Ability_5;
    private Image img_Inv_Card_1;
    private Image img_Inv_Card_2;
    private Image img_Inv_Card_3;
    private Image img_Inv_Card_4;
    private Image img_Inv_Card_5;
    private Image img_Inv_Bonus_1;
    private Image img_Inv_Bonus_2;
    private Image img_Inv_Bonus_3;
    private Image img_Inv_Bonus_4;
    private Image img_Inv_Bonus_5;
    private GameObject gobj_Potion_Inventory;
    private Text txt_Inv_Bonus_Num_1;
    private Text txt_Inv_Bonus_Num_2;
    private Text txt_Inv_Bonus_Num_3;
    private Text txt_Inv_Bonus_Num_4;
    private Text txt_Inv_Bonus_Num_5;
    private Text txt_HP_Add;
    private Text txt_MP_Add;
    private Text txt_ATK_Add;
    private Text txt_DEF_Add;
    private Text txt_EXP_Num;
    private Text txt_NEXT_Num;
    private Text txt_Text_Time;
    private Text txt_HP_Num;
    private Text txt_MP_Num;
    private Text txt_ATK_Num;
    private Text txt_DEF_Num;
    private Text txt_STR_Num;
    private Text txt_CON_Num;
    private Text txt_INT_Num;
    private Text txt_LCK_Num;
    private Text txt_Critical_Num;
    private Image txt_Death_Icon;
    private Text txt_Stat_DEATH;
    private Text txt_Death_Num;
    private Text txt_Level_Num;
    private Text txt_StatPoints_Num;
    private GameObject gobj_MissionBriefing;
    private RectTransform rect_Mission_1;
    private RectTransform rect_Mission_2;
    private RectTransform rect_Mission_3;
    private RectTransform rect_Mission_4;
    private RectTransform rect_pos_B1;
    private RectTransform rect_pos_B2;
    private RectTransform rect_pos_B3;
    private RectTransform rect_pos_B4;
    private RectTransform rect_pos_B5;
    private RectTransform rect_pos_B6;
    private RectTransform rect_pos_B7;
    private RectTransform rect_pos_B8;
    private RectTransform rect_pos_B9;
    private RectTransform rect_pos_B10;
    private RectTransform rect_pos_B11;
    private RectTransform rect_pos_B12;
    private RectTransform rect_pos_B13;
    private RectTransform rect_pos_B14;
    private RectTransform rect_pos_B15;
    private RectTransform rect_pos_B16;
    private RectTransform rect_pos_B17;
    private RectTransform rect_pos_B18;
    private RectTransform rect_pos_B19;
    private RectTransform rect_pos_B20;
    private RectTransform rect_pos_B21;
    private RectTransform rect_pos_B22;
    private RectTransform rect_pos_B23;
    private RectTransform rect_pos_B24;
    private RectTransform rect_pos_B25;
    private StageManager sm_StageManager;
    private Event_Control eventctrl_Dialogue;
    private Text txt_Shield_Text;
    private Image img_Shield_Bar;
    private Sound_Control sc_Sound_List;
    private GameObject gobj_Player_SoundList;
    private Text txt_KILLS_Num;
    private Text txt_Info_Text;
    public Custom_Key CK;

    public EXP_Table Exp_Table;

    private void Start()
    {
        PC = global::UnityEngine.GameObject.Find("Player").GetComponent<Player_Control>();
        UC = global::UnityEngine.GameObject.Find("Status").GetComponent<UI_Control>();
        #region 改为单次Find
        cc_Main_Camera = global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>();
        eg2d_Player = global::UnityEngine.GameObject.Find("Player").GetComponent<UnityEngine.Rigidbody2D>();
        gobj_Player = global::UnityEngine.GameObject.Find("Player");

        gobj_EscapeTimer = global::UnityEngine.GameObject.Find("EscapeTimer");
        gobj_Main_Camera = global::UnityEngine.GameObject.Find("Main Camera");
        gobj_Player_DownGrab = global::UnityEngine.GameObject.Find("Player_DownGrab");
        gobj_Player_Grab = global::UnityEngine.GameObject.Find("Player_Grab");
        gobj_Ani = global::UnityEngine.GameObject.Find("Ani");

        txt_Text_TimePlay = global::UnityEngine.GameObject.Find("Text_TimePlay").GetComponent<global::UnityEngine.UI.Text>();
        gobj_Text_TimePlay = global::UnityEngine.GameObject.Find("Text_TimePlay");

        txt_Text_Fps = global::UnityEngine.GameObject.Find("Text_Fps").GetComponent<global::UnityEngine.UI.Text>();

        gobj_Poison_On = global::UnityEngine.GameObject.Find("Poison_On");
        gobj_Pos_PlayerPoisonDmg = global::UnityEngine.GameObject.Find("Pos_PlayerPoisonDmg");
        txt_Poison_Text = global::UnityEngine.GameObject.Find("Poison_Text").GetComponent<global::UnityEngine.UI.Text>();
        sr_Clock_TimePlay = global::UnityEngine.GameObject.Find("Clock_TimePlay").GetComponent<global::UnityEngine.SpriteRenderer>();

        gobj_BGM_List = global::UnityEngine.GameObject.Find("BGM_List");
        sr_BlackPause = global::UnityEngine.GameObject.Find("BlackPause").GetComponent<global::UnityEngine.SpriteRenderer>();


        mc_Menu = global::UnityEngine.GameObject.Find("Menu").GetComponent<Menu_Control>();
        gobj_Menu = global::UnityEngine.GameObject.Find("Menu");

        gobj_Pos_SitLock_1_C = global::UnityEngine.GameObject.Find("Pos_SitLock_1_C");

        gobj_Player_ChestBurster = global::UnityEngine.GameObject.Find("Player_ChestBurster");

        sr_Ani_FaceHugger = global::UnityEngine.GameObject.Find("Ani_FaceHugger").GetComponent<global::UnityEngine.SpriteRenderer>();

        sr_SelBorder_Skill = global::UnityEngine.GameObject.Find("SelBorder_Skill").GetComponent<global::UnityEngine.RectTransform>();
        img_SelBorder_Aug = global::UnityEngine.GameObject.Find("SelBorder_Aug").GetComponent<global::UnityEngine.UI.Image>();
        rect_SelBorder_Aug = global::UnityEngine.GameObject.Find("SelBorder_Aug").GetComponent<global::UnityEngine.RectTransform>();

        img_Inv_Weapon_1 = global::UnityEngine.GameObject.Find("Inv_Weapon_1").GetComponent<global::UnityEngine.UI.Image>();
        img_Inv_Weapon_2 = global::UnityEngine.GameObject.Find("Inv_Weapon_2").GetComponent<global::UnityEngine.UI.Image>();
        img_Inv_Weapon_2_Glow = global::UnityEngine.GameObject.Find("Inv_Weapon_2_Glow").GetComponent<global::UnityEngine.UI.Image>();
        img_Inv_Weapon_3 = global::UnityEngine.GameObject.Find("Inv_Weapon_3").GetComponent<global::UnityEngine.UI.Image>();
        img_Inv_Weapon_4 = global::UnityEngine.GameObject.Find("Inv_Weapon_4").GetComponent<global::UnityEngine.UI.Image>();
        img_Inv_Weapon_5 = global::UnityEngine.GameObject.Find("Inv_Weapon_5").GetComponent<global::UnityEngine.UI.Image>();
        img_Inv_Weapon_5_Glow = global::UnityEngine.GameObject.Find("Inv_Weapon_5_Glow").GetComponent<global::UnityEngine.UI.Image>();

        img_Inv_Skill_1 = global::UnityEngine.GameObject.Find("Inv_Skill_1").GetComponent<global::UnityEngine.UI.Image>();
        img_Inv_Skill_2 = global::UnityEngine.GameObject.Find("Inv_Skill_2").GetComponent<global::UnityEngine.UI.Image>();
        img_Inv_Skill_3 = global::UnityEngine.GameObject.Find("Inv_Skill_3").GetComponent<global::UnityEngine.UI.Image>();
        img_Inv_Skill_4 = global::UnityEngine.GameObject.Find("Inv_Skill_4").GetComponent<global::UnityEngine.UI.Image>();
        img_Inv_Skill_5 = global::UnityEngine.GameObject.Find("Inv_Skill_5").GetComponent<global::UnityEngine.UI.Image>();
        img_Inv_Skill_5_Glow = global::UnityEngine.GameObject.Find("Inv_Skill_5_Glow").GetComponent<global::UnityEngine.UI.Image>();

        img_Inv_Ability_1 = global::UnityEngine.GameObject.Find("Inv_Ability_1").GetComponent<global::UnityEngine.UI.Image>();
        img_Inv_Ability_2 = global::UnityEngine.GameObject.Find("Inv_Ability_2").GetComponent<global::UnityEngine.UI.Image>();
        img_Inv_Ability_3 = global::UnityEngine.GameObject.Find("Inv_Ability_3").GetComponent<global::UnityEngine.UI.Image>();
        img_Inv_Ability_4 = global::UnityEngine.GameObject.Find("Inv_Ability_4").GetComponent<global::UnityEngine.UI.Image>();
        img_Inv_Ability_5 = global::UnityEngine.GameObject.Find("Inv_Ability_5").GetComponent<global::UnityEngine.UI.Image>();

        img_Inv_Card_1 = global::UnityEngine.GameObject.Find("Inv_Card_1").GetComponent<global::UnityEngine.UI.Image>();
        img_Inv_Card_2 = global::UnityEngine.GameObject.Find("Inv_Card_2").GetComponent<global::UnityEngine.UI.Image>();
        img_Inv_Card_3 = global::UnityEngine.GameObject.Find("Inv_Card_3").GetComponent<global::UnityEngine.UI.Image>();
        img_Inv_Card_4 = global::UnityEngine.GameObject.Find("Inv_Card_4").GetComponent<global::UnityEngine.UI.Image>();
        img_Inv_Card_5 = global::UnityEngine.GameObject.Find("Inv_Card_5").GetComponent<global::UnityEngine.UI.Image>();

        img_Inv_Bonus_1 = global::UnityEngine.GameObject.Find("Inv_Bonus_1").GetComponent<global::UnityEngine.UI.Image>();
        img_Inv_Bonus_2 = global::UnityEngine.GameObject.Find("Inv_Bonus_2").GetComponent<global::UnityEngine.UI.Image>();
        img_Inv_Bonus_3 = global::UnityEngine.GameObject.Find("Inv_Bonus_3").GetComponent<global::UnityEngine.UI.Image>();
        img_Inv_Bonus_4 = global::UnityEngine.GameObject.Find("Inv_Bonus_4").GetComponent<global::UnityEngine.UI.Image>();
        img_Inv_Bonus_5 = global::UnityEngine.GameObject.Find("Inv_Bonus_5").GetComponent<global::UnityEngine.UI.Image>();

        gobj_Potion_Inventory = global::UnityEngine.GameObject.Find("Potion_Inventory");



        txt_Inv_Bonus_Num_1 = global::UnityEngine.GameObject.Find("Inv_Bonus_Num_1").GetComponent<global::UnityEngine.UI.Text>();
        txt_Inv_Bonus_Num_2 = global::UnityEngine.GameObject.Find("Inv_Bonus_Num_2").GetComponent<global::UnityEngine.UI.Text>();
        txt_Inv_Bonus_Num_3 = global::UnityEngine.GameObject.Find("Inv_Bonus_Num_3").GetComponent<global::UnityEngine.UI.Text>();
        txt_Inv_Bonus_Num_4 = global::UnityEngine.GameObject.Find("Inv_Bonus_Num_4").GetComponent<global::UnityEngine.UI.Text>();
        txt_Inv_Bonus_Num_5 = global::UnityEngine.GameObject.Find("Inv_Bonus_Num_5").GetComponent<global::UnityEngine.UI.Text>();

        txt_HP_Add = global::UnityEngine.GameObject.Find("HP_Add").GetComponent<global::UnityEngine.UI.Text>();
        txt_MP_Add = global::UnityEngine.GameObject.Find("MP_Add").GetComponent<global::UnityEngine.UI.Text>();
        txt_ATK_Add = global::UnityEngine.GameObject.Find("ATK_Add").GetComponent<global::UnityEngine.UI.Text>();
        txt_DEF_Add = global::UnityEngine.GameObject.Find("DEF_Add").GetComponent<global::UnityEngine.UI.Text>();

        txt_EXP_Num = global::UnityEngine.GameObject.Find("EXP_Num").GetComponent<global::UnityEngine.UI.Text>();

        txt_NEXT_Num = global::UnityEngine.GameObject.Find("NEXT_Num").GetComponent<global::UnityEngine.UI.Text>();

        txt_Text_Time = global::UnityEngine.GameObject.Find("Text_Time").GetComponent<global::UnityEngine.UI.Text>();


        txt_HP_Num = global::UnityEngine.GameObject.Find("HP_Num").GetComponent<global::UnityEngine.UI.Text>();
        txt_MP_Num = global::UnityEngine.GameObject.Find("MP_Num").GetComponent<global::UnityEngine.UI.Text>();
        txt_ATK_Num = global::UnityEngine.GameObject.Find("ATK_Num").GetComponent<global::UnityEngine.UI.Text>();
        txt_DEF_Num = global::UnityEngine.GameObject.Find("DEF_Num").GetComponent<global::UnityEngine.UI.Text>();
        txt_STR_Num = global::UnityEngine.GameObject.Find("STR_Num").GetComponent<global::UnityEngine.UI.Text>();
        txt_CON_Num = global::UnityEngine.GameObject.Find("CON_Num").GetComponent<global::UnityEngine.UI.Text>();
        txt_INT_Num = global::UnityEngine.GameObject.Find("INT_Num").GetComponent<global::UnityEngine.UI.Text>();
        txt_LCK_Num = global::UnityEngine.GameObject.Find("LCK_Num").GetComponent<global::UnityEngine.UI.Text>();
        txt_Critical_Num = global::UnityEngine.GameObject.Find("Critical_Num").GetComponent<global::UnityEngine.UI.Text>();

        txt_Death_Icon = global::UnityEngine.GameObject.Find("Death_Icon").GetComponent<global::UnityEngine.UI.Image>();

        txt_Stat_DEATH = global::UnityEngine.GameObject.Find("Stat_DEATH").GetComponent<global::UnityEngine.UI.Text>();


        txt_Death_Num = global::UnityEngine.GameObject.Find("Death_Num").GetComponent<global::UnityEngine.UI.Text>();

        txt_Level_Num = global::UnityEngine.GameObject.Find("Level_Num").GetComponent<global::UnityEngine.UI.Text>();

        txt_StatPoints_Num = global::UnityEngine.GameObject.Find("StatPoints_Num").GetComponent<global::UnityEngine.UI.Text>();

        gobj_MissionBriefing = global::UnityEngine.GameObject.Find("MissionBriefing");

        rect_Mission_1 = global::UnityEngine.GameObject.Find("Mission_1").GetComponent<global::UnityEngine.RectTransform>();
        rect_Mission_2 = global::UnityEngine.GameObject.Find("Mission_2").GetComponent<global::UnityEngine.RectTransform>();
        rect_Mission_3 = global::UnityEngine.GameObject.Find("Mission_3").GetComponent<global::UnityEngine.RectTransform>();
        rect_Mission_4 = global::UnityEngine.GameObject.Find("Mission_4").GetComponent<global::UnityEngine.RectTransform>();



        rect_pos_B1 = global::UnityEngine.GameObject.Find("pos_B1").GetComponent<global::UnityEngine.RectTransform>();
        rect_pos_B2 = global::UnityEngine.GameObject.Find("pos_B2").GetComponent<global::UnityEngine.RectTransform>();
        rect_pos_B3 = global::UnityEngine.GameObject.Find("pos_B3").GetComponent<global::UnityEngine.RectTransform>();
        rect_pos_B4 = global::UnityEngine.GameObject.Find("pos_B4").GetComponent<global::UnityEngine.RectTransform>();
        rect_pos_B5 = global::UnityEngine.GameObject.Find("pos_B5").GetComponent<global::UnityEngine.RectTransform>();
        rect_pos_B6 = global::UnityEngine.GameObject.Find("pos_B6").GetComponent<global::UnityEngine.RectTransform>();
        rect_pos_B7 = global::UnityEngine.GameObject.Find("pos_B7").GetComponent<global::UnityEngine.RectTransform>();
        rect_pos_B8 = global::UnityEngine.GameObject.Find("pos_B8").GetComponent<global::UnityEngine.RectTransform>();
        rect_pos_B9 = global::UnityEngine.GameObject.Find("pos_B9").GetComponent<global::UnityEngine.RectTransform>();
        rect_pos_B10 = global::UnityEngine.GameObject.Find("pos_B10").GetComponent<global::UnityEngine.RectTransform>();
        rect_pos_B11 = global::UnityEngine.GameObject.Find("pos_B11").GetComponent<global::UnityEngine.RectTransform>();
        rect_pos_B12 = global::UnityEngine.GameObject.Find("pos_B12").GetComponent<global::UnityEngine.RectTransform>();
        rect_pos_B13 = global::UnityEngine.GameObject.Find("pos_B13").GetComponent<global::UnityEngine.RectTransform>();
        rect_pos_B14 = global::UnityEngine.GameObject.Find("pos_B14").GetComponent<global::UnityEngine.RectTransform>();
        rect_pos_B15 = global::UnityEngine.GameObject.Find("pos_B15").GetComponent<global::UnityEngine.RectTransform>();
        rect_pos_B16 = global::UnityEngine.GameObject.Find("pos_B16").GetComponent<global::UnityEngine.RectTransform>();
        rect_pos_B17 = global::UnityEngine.GameObject.Find("pos_B17").GetComponent<global::UnityEngine.RectTransform>();
        rect_pos_B18 = global::UnityEngine.GameObject.Find("pos_B18").GetComponent<global::UnityEngine.RectTransform>();
        rect_pos_B19 = global::UnityEngine.GameObject.Find("pos_B19").GetComponent<global::UnityEngine.RectTransform>();
        rect_pos_B20 = global::UnityEngine.GameObject.Find("pos_B20").GetComponent<global::UnityEngine.RectTransform>();
        rect_pos_B21 = global::UnityEngine.GameObject.Find("pos_B21").GetComponent<global::UnityEngine.RectTransform>();
        rect_pos_B22 = global::UnityEngine.GameObject.Find("pos_B22").GetComponent<global::UnityEngine.RectTransform>();
        rect_pos_B23 = global::UnityEngine.GameObject.Find("pos_B23").GetComponent<global::UnityEngine.RectTransform>();
        rect_pos_B24 = global::UnityEngine.GameObject.Find("pos_B24").GetComponent<global::UnityEngine.RectTransform>();
        rect_pos_B25 = global::UnityEngine.GameObject.Find("pos_B25").GetComponent<global::UnityEngine.RectTransform>();

        sm_StageManager = global::UnityEngine.GameObject.Find("StageManager").GetComponent<StageManager>();
        eventctrl_Dialogue = global::UnityEngine.GameObject.Find("Dialogue").GetComponent<Event_Control>();


        txt_Shield_Text = global::UnityEngine.GameObject.Find("Shield_Text").GetComponent<global::UnityEngine.UI.Text>();
        img_Shield_Bar = global::UnityEngine.GameObject.Find("Shield_Bar").GetComponent<global::UnityEngine.UI.Image>();
        sc_Sound_List = global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>();
        gobj_Player_SoundList = global::UnityEngine.GameObject.Find("Player_SoundList");
        txt_KILLS_Num = global::UnityEngine.GameObject.Find("KILLS_Num").GetComponent<global::UnityEngine.UI.Text>();
        txt_Info_Text = global::UnityEngine.GameObject.Find("Info_Text").GetComponent<global::UnityEngine.UI.Text>();
        #endregion
        BlackFade = global::UnityEngine.GameObject.Find("BlackFade");
        BlackFade.GetComponent<global::UnityEngine.SpriteRenderer>().enabled = true;
        Reset_Inventory();
        //这里批量设置了碰撞忽略关系
        global::UnityEngine.Physics2D.IgnoreLayerCollision(25, 25);
        global::UnityEngine.Physics2D.IgnoreLayerCollision(25, 30);
        global::UnityEngine.Physics2D.IgnoreLayerCollision(25, 20);
        global::UnityEngine.Physics2D.IgnoreLayerCollision(25, 19);
        global::UnityEngine.Physics2D.IgnoreLayerCollision(20, 30);
        global::UnityEngine.Physics2D.IgnoreLayerCollision(20, 20);
        global::UnityEngine.Physics2D.IgnoreLayerCollision(19, 30);
        global::UnityEngine.Physics2D.IgnoreLayerCollision(19, 29);
        global::UnityEngine.Physics2D.IgnoreLayerCollision(19, 20);
        global::UnityEngine.Physics2D.IgnoreLayerCollision(19, 19);
        global::UnityEngine.Physics2D.IgnoreLayerCollision(20, 16);
        global::UnityEngine.Physics2D.IgnoreLayerCollision(20, 22);
        global::UnityEngine.Physics2D.IgnoreLayerCollision(20, 23);
        global::UnityEngine.Physics2D.IgnoreLayerCollision(28, 5);
        global::UnityEngine.Physics2D.IgnoreLayerCollision(28, 16);
        global::UnityEngine.Physics2D.IgnoreLayerCollision(28, 28);
        global::UnityEngine.Physics2D.IgnoreLayerCollision(27, 30);
        global::UnityEngine.Physics2D.IgnoreLayerCollision(27, 29);
        global::UnityEngine.Physics2D.IgnoreLayerCollision(27, 28);
        global::UnityEngine.Physics2D.IgnoreLayerCollision(27, 16);
        global::UnityEngine.Physics2D.IgnoreLayerCollision(18, 16);
        global::UnityEngine.Physics2D.IgnoreLayerCollision(18, 27);
        global::UnityEngine.Physics2D.IgnoreLayerCollision(18, 20);
        global::UnityEngine.Physics2D.IgnoreLayerCollision(18, 19);
        global::UnityEngine.Physics2D.IgnoreLayerCollision(18, 10);
        global::UnityEngine.Physics2D.IgnoreLayerCollision(18, 15);
        global::UnityEngine.Physics2D.IgnoreLayerCollision(10, 29);
        global::UnityEngine.Physics2D.IgnoreLayerCollision(10, 5);
        Language_Num = AxiPlayerPrefs.GetInt("Language_Num");
        AxiPlayerPrefs.SetInt("Input_Mode", 0);
        if (AxiPlayerPrefs.GetInt("onClockFps") != 1)
        {
            txt_Text_TimePlay.enabled = false;
            txt_Text_Fps.enabled = false;
            sr_Clock_TimePlay.enabled = false;
        }
    }

    private void Check_Input()
    {
        User_Input_Timer += global::UnityEngine.Time.deltaTime;
        if (Input_Mode == 0)
        {
            if (global::UnityEngine.Input.GetKeyDown(CK.Left) || global::UnityEngine.Input.GetKeyDown(CK.Right) || global::UnityEngine.Input.GetKeyDown(CK.Jump) || global::UnityEngine.Input.GetKeyDown(CK.Attack) || global::UnityEngine.Input.GetKeyDown(CK.Spin) || global::UnityEngine.Input.GetKeyDown(CK.Skill) || global::UnityEngine.Input.GetKeyDown(CK.LB) || global::UnityEngine.Input.GetKeyDown(CK.RB))
            {
                User_Input_Timer = 0f;
            }
        }
        else if (global::UnityEngine.Input.GetAxis("L_X") != 0f || global::UnityEngine.Input.GetAxis("L_Y") != 0f || global::UnityEngine.Input.GetButtonDown("Jump") || global::UnityEngine.Input.GetButtonDown("_B") || global::UnityEngine.Input.GetButtonDown("_X") || global::UnityEngine.Input.GetButtonDown("_Y") || global::UnityEngine.Input.GetButtonDown("L_B") || global::UnityEngine.Input.GetButtonDown("R_B") || global::UnityEngine.Input.GetAxis("L_Trigger") != 0f)
        {
            User_Input_Timer = 0f;
        }
    }

    private void Update()
    {
        if (!DataLoaded)
        {
            Load_Timer += global::UnityEngine.Time.deltaTime;
            if (Load_Timer > 0.2f)
            {
                DataLoaded = true;
                Load_Data();
                ExpNext = Exp_Table.Need_Exp[Level] - ExpNow;
                ExpRatio = Exp_Table.Need_Exp[Level - 1] - ExpNow / Exp_Table.Need_Exp[Level] - Exp_Table.Need_Exp[Level - 1];
                Status_Update();
                isFadeIn = true;
            }
        }
        if (Input_Mode == 0)
        {
            if (global::UnityEngine.Input.GetAxis("L_X") != 0f || global::UnityEngine.Input.GetAxis("L_Y") != 0f || global::UnityEngine.Input.GetAxis("R_X") != 0f || global::UnityEngine.Input.GetAxis("R_Y") != 0f || global::UnityEngine.Input.GetButtonDown("Jump") || global::UnityEngine.Input.GetButtonDown("_B") || global::UnityEngine.Input.GetButtonDown("_X") || global::UnityEngine.Input.GetButtonDown("_Y") || global::UnityEngine.Input.GetButtonDown("L_B") || global::UnityEngine.Input.GetButtonDown("R_B") || global::UnityEngine.Input.GetAxis("L_Trigger") != 0f || global::UnityEngine.Input.GetButtonDown("Start") || global::UnityEngine.Input.GetButtonDown("Back") || global::UnityEngine.Input.GetAxis("DPad_X") != 0f || global::UnityEngine.Input.GetAxis("DPad_Y") != 0f)
            {
                Input_Mode = 1;
                gobj_Menu.SendMessage("Hscene_Bar_Pad");
            }
        }
        else if (Input_Mode == 1 && global::UnityEngine.Input.anyKeyDown)
        {
            Input_Mode = 0;
            gobj_Menu.SendMessage("Hscene_Bar_KB");
        }
        Check_Input();
        if (Hscene_Num == 0 && Hscene_Timer > 0f)
        {
            Hscene_Timer -= global::UnityEngine.Time.deltaTime;
        }
        if (GameOver)
        {
            GameOver_Timer += global::UnityEngine.Time.deltaTime;
            color_Player = global::UnityEngine.Color.Lerp(color_Player, new global::UnityEngine.Color(1f, 1f, 1f, 1f), global::UnityEngine.Time.deltaTime * 0.5f);
        }
        else
        {
            GameOver_Timer = 0f;
        }
        if (!Paused)
        {
            if (!GameOver && !onEvent)
            {
                Check_Time();
            }
            if (!GameOver && onDown && !onHscene && Hscene_Num == 0)
            {
                if (Down_Timer <= 0f)
                {
                    onDown = false;
                    PC.GetUp();
                }
                else
                {
                    Down_Timer -= global::UnityEngine.Time.deltaTime;
                }
            }
            if (resumeTimer > 0f)
            {
                resumeTimer -= global::UnityEngine.Time.deltaTime;
            }
            if (Damage_Timer > 0f)
            {
                Damage_Timer -= global::UnityEngine.Time.deltaTime;
            }
            Mana_Regen();
            if (Potion_Timer > 0f)
            {
                Make_PotionHeal();
            }
            if (PotionRed_Timer > 0f)
            {
                Make_RedHeal();
            }
            if (PotionBlue_Timer > 0f)
            {
                Make_BlueHeal();
            }
            if (PC.State.ToString() != "Spin" && spin_Timer > 0f)
            {
                spin_Timer = 1f;
            }
            if (!PC.onScrewAttack && screw_Timer > 0f)
            {
                screw_Timer = 1f;
            }
            if (!onEvent || onHscene)
            {
                if (global::UnityEngine.Input.GetAxis("R_Y") != 0f)
                {
                    cc_Main_Camera.targetSize += global::UnityEngine.Input.GetAxis("R_Y") * -5f * global::UnityEngine.Time.deltaTime;
                }
                else if (global::UnityEngine.Input.GetKey(CK.ZoomIn))
                {
                    cc_Main_Camera.targetSize -= 5f * global::UnityEngine.Time.deltaTime;
                }
                else if (global::UnityEngine.Input.GetKey(CK.ZoomOut))
                {
                    cc_Main_Camera.targetSize += 5f * global::UnityEngine.Time.deltaTime;
                }
                if (global::UnityEngine.Input.GetButtonDown("R_A"))
                {
                    gobj_Main_Camera.SendMessage("Reset_Zoom_Button");
                }
                else if (global::UnityEngine.Input.GetKeyDown(CK.ZoomReset))
                {
                    gobj_Main_Camera.SendMessage("Reset_Zoom_Button");
                }
            }
            if (!onSave && !onEvent && !GameOver)
            {
                if (global::UnityEngine.Input.GetKeyDown(CK.SkillSwap))
                {
                    Change_Skill(1);
                }
                else if (!isPressedDpad_R && global::UnityEngine.Input.GetAxis("DPad_X") > 0f)
                {
                    isPressedDpad_R = true;
                    Change_Skill(1);
                }
                else if (!isPressedDpad_L && global::UnityEngine.Input.GetAxis("DPad_X") < 0f)
                {
                    isPressedDpad_L = true;
                    Change_Skill(-1);
                }
                else if (global::UnityEngine.Input.GetAxis("DPad_X") == 0f)
                {
                    isPressedDpad_R = (isPressedDpad_L = false);
                }
                if (!onGatePass)
                {
                    if (Potion_HP > 0 && global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.Alpha1))
                    {
                        Use_Potion_HP();
                    }
                    else if (Potion_MP > 0 && global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.Alpha2))
                    {
                        Use_Potion_MP();
                    }
                    if (Potion_HP > 0 && global::UnityEngine.Input.GetAxis("L_Trigger") >= 0f && global::UnityEngine.Input.GetButtonDown("_B"))
                    {
                        Use_Potion_HP();
                    }
                    else if (Potion_MP > 0 && global::UnityEngine.Input.GetAxis("L_Trigger") < 0f && global::UnityEngine.Input.GetButtonDown("_B"))
                    {
                        Use_Potion_MP();
                    }
                }
            }
            if (Shield_Timer > 0f)
            {
                Shield_Timer -= global::UnityEngine.Time.deltaTime;
            }
            if (onShield)
            {
                txt_Shield_Text.text = "SHIELD : " + Shield_Timer.ToString("f1");
                img_Shield_Bar.fillAmount = Shield_Timer / 5f;
            }
            if (Poison_Timer > 0f)
            {
                Poison_Timer -= global::UnityEngine.Time.deltaTime;
            }
            if (PoisonSmog_Timer > 0f)
            {
                PoisonSmog_Timer -= global::UnityEngine.Time.deltaTime;
            }
            if (onChestBurster)
            {
                ChestBurster_Timer += global::UnityEngine.Time.deltaTime;
                if (!onChestBurster_Over && !onHscene && Hscene_Num != 98 && ChestBurster_Timer > 15f && Hscene_Timer < 1f)
                {
                    GameOver = true;
                    onEvent = true;
                    onHscene = true;
                    Hscene_Num = 98;
                    onChestBurster_Over = true;
                    gobj_Player_ChestBurster.SendMessage("Play");
                }
            }
            if (onHscene)
            {
                if (onPoison || Poison_Timer > 0f)
                {
                    onPoison = false;
                    Poison_Timer = 0f;
                    PoisonSmog_Timer = 0f;
                    Poison_DMG_Timer = 0f;
                    gobj_Poison_On.GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(565f, 537f, 0f);
                    color_Player = new global::UnityEngine.Color(1f, 1f, 1f);
                }
                if (onCloth)
                {
                    onCloth = false;
                    PC.OnOff_Cloth();
                }
                if (onShield)
                {
                    Break_Shield();
                }
                if (Hscene_Num == 96)
                {
                    FaceHugger_DMG_Timer += global::UnityEngine.Time.deltaTime;
                    if (FaceHugger_DMG_Timer >= 1f)
                    {
                        FaceHugger_DMG_Timer = 0f;
                        if (HP - 10 >= 0)
                        {
                            HP -= 10;
                        }
                        else
                        {
                            HP = 0;
                            GameOver = true;
                            onEvent = false;
                            onHscene = false;
                            Hscene_Num = 0;
                            Hscene_Timer = 10f;
                            onChestBurster = true;
                            cc_Main_Camera.targetSize = 5f;
                            gobj_Main_Camera.SendMessage("Set_Vignetting_13");
                            gobj_Player_Grab.SendMessage("H_Exit_GameOver");
                            gobj_Ani.SendMessage("Set_FaceHugger");
                        }
                        global::UnityEngine.Vector3 position = new global::UnityEngine.Vector3(PC.transform.position.x, PC.transform.position.y + 5.5f, 0f);
                        global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Damage_Font, position, base.transform.rotation) as global::UnityEngine.GameObject;
                        gameObject.GetComponent<Dmg_Font>().Set_Number(10, 4);
                        UC.Set_Damage();
                    }
                }
            }
            else if (PoisonSmog_Timer > 0f || Poison_Timer > 0f)
            {
                if (!onPoison)
                {
                    onPoison = true;
                    if (PoisonSmog_Timer > 0f)
                    {
                        Poison_DMG_Timer = 0.125f;
                    }
                    else
                    {
                        Poison_DMG_Timer = 0.25f;
                    }
                    gobj_Poison_On.GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(565f, 37f, 0f);
                }
                Poison_DMG_Timer -= global::UnityEngine.Time.deltaTime;
                if (Poison_DMG_Timer <= 0f)
                {
                    if (PoisonSmog_Timer > 0f)
                    {
                        Poison_DMG_Timer = 0.125f;
                    }
                    else
                    {
                        Poison_DMG_Timer = 0.25f;
                    }
                    if (HP > 1)
                    {
                        if (HP < Poison_DMG + 1)
                        {
                            global::UnityEngine.Vector3 position2 = gobj_Pos_PlayerPoisonDmg.transform.position;
                            global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(position: new global::UnityEngine.Vector3(position2.x + (float)global::UnityEngine.Random.Range(-40, 40) * 0.01f, position2.y + (float)global::UnityEngine.Random.Range(0, 100) * 0.01f, 0f), original: Damage_Font, rotation: base.transform.rotation) as global::UnityEngine.GameObject;
                            gameObject2.GetComponent<Dmg_Font>().Set_Number(HP - 1, 8);
                            HP = 1;
                            UC.Set_Damage();
                        }
                        else
                        {
                            global::UnityEngine.Vector3 position3 = gobj_Pos_PlayerPoisonDmg.transform.position;
                            global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(position: new global::UnityEngine.Vector3(position3.x + (float)global::UnityEngine.Random.Range(-40, 40) * 0.01f, position3.y + (float)global::UnityEngine.Random.Range(0, 100) * 0.01f, 0f), original: Damage_Font, rotation: base.transform.rotation) as global::UnityEngine.GameObject;
                            gameObject3.GetComponent<Dmg_Font>().Set_Number(Poison_DMG, 8);
                            HP -= Poison_DMG;
                            UC.Set_Damage();
                        }
                    }
                }
                if (Poison_Timer > 0f)
                {
                    txt_Poison_Text.text = "POISON : " + Poison_Timer.ToString("f1");
                }
                else
                {
                    txt_Poison_Text.text = "POISON   ";
                }
                color_Timer += global::UnityEngine.Time.deltaTime;
                color_Player = new global::UnityEngine.Color(0.9f + global::UnityEngine.Mathf.Sin(color_Timer * 5f) * 0.1f, 0.75f, 1f, 1f);
            }
            else if (onPoison)
            {
                onPoison = false;
                Poison_Timer = 0f;
                Poison_DMG_Timer = 0f;
                Poison_DMG = 0;
                gobj_Poison_On.GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(565f, 537f, 0f);
                color_Player = new global::UnityEngine.Color(1f, 1f, 1f);
            }
            if (FaceHugger_DMG_Timer > 0f && Hscene_Num == 0)
            {
                FaceHugger_DMG_Timer = 0f;
            }
            if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.RightShift))
            {
                if (AxiPlayerPrefs.GetInt("onClockFps") == 1)
                {
                    AxiPlayerPrefs.SetInt("onClockFps", 0);
                    txt_Text_TimePlay.enabled = false;
                    txt_Text_Fps.enabled = false;
                    sr_Clock_TimePlay.enabled = false;
                    AxiPlayerPrefs.SetInt("On_HealthBar", 2);
                }
                else
                {
                    AxiPlayerPrefs.SetInt("onClockFps", 1);
                    txt_Text_TimePlay.enabled = true;
                    txt_Text_Fps.enabled = true;
                    sr_Clock_TimePlay.enabled = true;
                    AxiPlayerPrefs.SetInt("On_HealthBar", 1);
                }
            }
            else if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.F12))
            {
                User_Death();
            }
        }
        if (isFadeIn)
        {
            FadeOpacity -= global::UnityEngine.Time.deltaTime * 0.5f;
            if (FadeOpacity <= 0f)
            {
                isFadeIn = false;
                FadeOpacity = 0f;
                BlackFade.GetComponent<global::UnityEngine.SpriteRenderer>().enabled = false;
            }
            BlackFade.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(0f, 0f, 0f, FadeOpacity);
        }
        else if (isFadeOut)
        {
            FadeOpacity += global::UnityEngine.Time.deltaTime * 1f;
            if (FadeOpacity >= 1f)
            {
                isFadeOut = false;
                FadeOpacity = 1f;
                if (FadeOutAction == "Title")
                {
                    global::UnityEngine.Application.LoadLevel("Title");
                }
                else if (FadeOutAction == "Main")
                {
                    global::UnityEngine.Application.LoadLevel("Main");
                }
                else if (FadeOutAction == "GameOver")
                {
                    global::UnityEngine.Application.LoadLevel("GameOver");
                }
                else if (FadeOutAction == "GameEnding")
                {
                    global::UnityEngine.Application.LoadLevel("GameEnding");
                }
            }
            BlackFade.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(0f, 0f, 0f, FadeOpacity);
        }
        txt_Text_Fps.text = cc_Main_Camera.fps.ToString("f1") + "fps";
    }

    private void Set_FadeIn()
    {
        isFadeIn = true;
        isFadeOut = false;
        FadeOpacity = 1f;
        if (!BlackFade.GetComponent<global::UnityEngine.SpriteRenderer>().enabled)
        {
            BlackFade.GetComponent<global::UnityEngine.SpriteRenderer>().enabled = true;
            BlackFade.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, 1f);
        }
        FadeOutAction = string.Empty;
    }

    public void Set_FadeOut(string fadeoutaction)
    {
        isFadeOut = true;
        isFadeIn = false;
        if (!BlackFade.GetComponent<global::UnityEngine.SpriteRenderer>().enabled)
        {
            FadeOpacity = 0f;
            BlackFade.GetComponent<global::UnityEngine.SpriteRenderer>().enabled = true;
            BlackFade.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
        }
        FadeOutAction = fadeoutaction;
        gobj_BGM_List.SendMessage("OnExit");
    }

    public void Game_Pause()
    {
        Paused = true;
        onConsole = false;
        cc_Main_Camera.Set_Blur_Pause();
        sr_BlackPause.enabled = true;
        sr_BlackPause.color = new global::UnityEngine.Color(1f, 1f, 1f, 0.4f);
        if (gobj_Player != null)
        {
            player_Velocity = eg2d_Player.velocity;
            gobj_Player.GetComponent<global::UnityEngine.Rigidbody2D>().Sleep();
        }
        UC.HideInst_Status();
        gobj_EscapeTimer.SendMessage("Pause_Timer");
    }

    public void Game_Resume()
    {
        Paused = false;
        PC.Lock_Timer = 0.1f;
        resumeTimer = 0.1f;
        onMenu = false;
        onMap = false;
        onConsole = false;
        cc_Main_Camera.Set_Blur(10);
        sr_BlackPause.enabled = false;
        sr_BlackPause.color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
        if (gobj_Player != null)
        {
            gobj_Player.GetComponent<global::UnityEngine.Rigidbody2D>().WakeUp();
            gobj_Player.GetComponent<global::UnityEngine.Rigidbody2D>().velocity = player_Velocity;
        }
    }

    public void Event_Resume()
    {
        onEvent = false;
        onMenu = false;
        onMap = false;
        onConsole = false;
        PC.Lock_Timer = 0.1f;
        resumeTimer = 0.1f;
    }

    public void Down_H_After()
    {
        onDown = true;
        Down_Timer = 3f;
        Damage_Timer = 2f;
        if (mc_Menu.H_Cancel)
        {
            Down_Timer = 0.6f;
        }
        gobj_Main_Camera.SendMessage("Reset_Zoom");
    }

    private void User_Death()
    {
        if (!onHscene && HP > 0 && Damage_Timer <= 0f)
        {
            HP = 0;
            UC.Set_Damage();
            GameOver = true;
            onCloth = false;
            PC.OnOff_Cloth();
            Hscene_Timer = 2f;
            PC.Down(20f * (float)(-PC.facingRight));
            if (onShield)
            {
                Break_Shield();
            }
            gobj_Main_Camera.SendMessage("Hscene_Zoom");
            gobj_Main_Camera.SendMessage("Set_Vignetting_13");
        }
    }

    public void Damage(int dmg, float force, bool isDown, int Mon_Num)
    {
        if (onHscene || HP <= 0 || !(Damage_Timer <= 0f))
        {
            return;
        }
        Damage_Timer = 0.4f;
        if (Mon_Num == 155)
        {
            Damage_Timer = 0.1f;
            Mon_Num = 10;
        }
        else if (Mon_Num > 100)
        {
            Damage_Timer = 0.1f;
            Mon_Num = 14;
        }
        dmg = ((DEF >= 500) ? 1 : (dmg - dmg * DEF / 500));
        if (HP - dmg < 0)
        {
            HP = 0;
        }
        else
        {
            HP -= dmg;
        }
        UC.Set_Damage();
        global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Damage_Font, gobj_Pos_SitLock_1_C.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
        gameObject.GetComponent<Dmg_Font>().Set_Number(dmg, 4);
        gameObject.transform.localScale = new global::UnityEngine.Vector3(1.2f, 1.2f, 1f);
        if (HP > 0)
        {
            if (onCloth && (float)HP / (float)HP_Max < 0.6f)
            {
                onCloth = false;
                PC.OnOff_Cloth();
            }
            if (isDown)
            {
                PC.Down(force);
            }
            else
            {
                PC.Set_Damage(force);
            }
        }
        else
        {
            gobj_Main_Camera.SendMessage("Hscene_Zoom");
            GameOver = true;
            onCloth = false;
            PC.OnOff_Cloth();
            Hscene_Timer = 4f;
            PC.Down(force);
            if (onShield)
            {
                Break_Shield();
            }
            gobj_Main_Camera.SendMessage("Set_Vignetting_13");
        }
        if (Mon_Num > 0 && Mon_Num < 100)
        {
            if (global::UnityEngine.Mathf.Abs(force) >= 30f)
            {
                sc_Sound_List.Player_Damage(Mon_Num, true, PC.transform.position);
            }
            else
            {
                sc_Sound_List.Player_Damage(Mon_Num, false, PC.transform.position);
            }
        }
    }

    public void Break_Shield()
    {
        onShield = false;
        if (Shield_Object != null)
        {
            Shield_Object.SendMessage("Set_Broken");
        }
        else
        {
            Shield_Object = null;
        }
        gobj_Player_SoundList.SendMessage("Sound_Slide");
    }

    private void Mana_Regen()
    {
        if (PC.State.ToString() == "Idle" || PC.State.ToString() == "Sit" || PC.State.ToString() == "Run" || PC.State.ToString() == "Jump")
        {
            MPRegen_Timer += global::UnityEngine.Time.deltaTime * 4.5f * (float)(1 + 2 * Bonus_Regen);
            while (MPRegen_Timer > 1f)
            {
                MPRegen_Timer -= 1f;
                MP = ((MP + 1 <= MP_Max) ? (MP + 1) : MP_Max);
            }
        }
        else
        {
            MPRegen_Timer = 0f;
        }
    }

    public bool Check_Skill()
    {
        if (MP > Skill_MP[Skill_Num - 1])
        {
            return true;
        }
        return false;
    }

    public void MP_Skill()
    {
        MP -= Skill_MP[Skill_Num - 1];
        UC.Set_Mana();
    }

    public void MP_Spin()
    {
        spin_Timer += global::UnityEngine.Time.deltaTime * 90f;
        while (spin_Timer > 1f)
        {
            spin_Timer -= 1f;
            MP--;
            UC.Set_Mana();
        }
    }

    public void MP_Screw(float Screw_Opacity)
    {
        screw_Timer += global::UnityEngine.Time.deltaTime * 180f * Screw_Opacity;
        while (screw_Timer > 1f)
        {
            screw_Timer -= 1f;
            MP--;
            UC.Set_Mana();
        }
    }

    public void MP_SpeedUp()
    {
        speedUp_Timer += global::UnityEngine.Time.deltaTime * 30f;
        while (speedUp_Timer > 1f)
        {
            speedUp_Timer -= 1f;
            MP--;
            UC.Set_Mana();
        }
    }

    private void Reset_SpinScrew()
    {
        spin_Timer = 0f;
        screw_Timer = 0f;
    }

    private void Check_Time()
    {
        PlayTime += global::UnityEngine.Time.deltaTime;
        if (gobj_Text_TimePlay != null)
        {
            string text = string.Empty;
            int num = (int)(PlayTime / 3600f);
            int num2 = (int)((PlayTime - (float)(3600 * num)) / 60f);
            int num3 = (int)(PlayTime % 60f);
            if (num > 0)
            {
                text = num + ":";
            }
            text = ((num2 <= 9) ? (text + "0" + num2 + ":") : (text + num2 + ":"));
            text = ((num3 <= 9) ? (text + "0" + num3) : (text + num3));
            txt_Text_TimePlay.text = text;
        }
    }

    public void Monster_Kill(int mon_Num)
    {
        Kills++;
        if (Level < 50 && Hscene_Num == 0 && !GameOver)
        {
            int num = Exp_Table.Mon_Exp[mon_Num];
            if (Exp_Table.Mon_Lvl[mon_Num] < Level)
            {
                num = ((Level - Exp_Table.Mon_Lvl[mon_Num] > 10) ? 1 : (num / (Level - Exp_Table.Mon_Lvl[mon_Num] + 1)));
            }
            if (Exp_Table.Need_Exp[Level + 1] < ExpNow + num)
            {
                num = Exp_Table.Need_Exp[Level + 1] - 15 - ExpNow;
            }
            ExpNow += num;
            ExpNext = Exp_Table.Need_Exp[Level] - ExpNow;
            if (ExpNext <= 0)
            {
                Level_Up();
            }
            else if (mon_Num > 50)
            {
                ExpNow = Exp_Table.Need_Exp[Level];
                ExpNext = 0;
                Level_Up();
            }
            ExpRatio = (float)(ExpNow - Exp_Table.Need_Exp[Level - 1]) / (float)(Exp_Table.Need_Exp[Level] - Exp_Table.Need_Exp[Level - 1]);
            UC.Set_ExpRotate(ExpRatio);
        }
    }

    public float Get_ExpRatio()
    {
        ExpRatio = (float)(ExpNow - Exp_Table.Need_Exp[Level - 1]) / (float)(Exp_Table.Need_Exp[Level] - Exp_Table.Need_Exp[Level - 1]);
        return ExpRatio;
    }

    public void Info_Dialog(string text)
    {
        global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(info_Dialog) as global::UnityEngine.GameObject;
        gameObject.GetComponent<Info_Dialog>().Set_Timer(3f);
        gameObject.GetComponent<Info_Dialog>().Set_Text(text);
    }

    private void Level_Up()
    {
        Level++;
        HP_Max += 3;
        HP = HP_Max;
        MP_Max += 2;
        MP = MP_Max;
        StatPoints += 5;
        if (Level < 50)
        {
            ExpNext = Exp_Table.Need_Exp[Level] - ExpNow;
        }
        else
        {
            ExpNext = 0;
        }
        UC.Set_LevelUP();
        gobj_Menu.SendMessage("Sound_LevelUp");
        global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(info_LevelUp) as global::UnityEngine.GameObject;
        global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(effect_LevelUp) as global::UnityEngine.GameObject;
        gameObject2.transform.position = new global::UnityEngine.Vector3(PC.transform.position.x, PC.transform.position.y + 3f, 0f);
        gameObject2.transform.parent = PC.transform;
    }

    public void Level_Up_Effect(global::UnityEngine.Vector3 pos, float delay)
    {
        global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(effect_LevelUp) as global::UnityEngine.GameObject;
        gameObject.transform.position = pos;
        gameObject.GetComponent<LevelUp>().Life_Timer = 0f - delay;
    }

    public void Resurrect()
    {
        gobj_Ani.SendMessage("Set_Idle");
        onCloth = true;
        Bonus_Life++;
        HP_Max = ((HP_Max - 50 <= 10) ? 10 : (HP_Max - 50));
        HP = HP_Max;
        MP = MP_Max;
        Damage_Timer = 3f;
        PC.SendMessage("Set_Flicker");
        if (onHscene)
        {
            if (Hscene_Num == 96)
            {
                gobj_Player_Grab.SendMessage("H_Exit");
            }
            else if (Hscene_Num == 97)
            {
                gobj_Player_DownGrab.SendMessage("H_Exit");
            }
            else if (Hscene_Num == 98)
            {
                gobj_Player_ChestBurster.SendMessage("Stop");
            }
        }
        GameOver = false;
        onEvent = false;
        onHscene = false;
        Hscene_Num = 0;
        Hscene_Timer = 4f;
        onDown = false;
        Down_Timer = 0f;
        if (onFaceHugger)
        {
            onFaceHugger = false;
            sr_Ani_FaceHugger.enabled = false;
            gobj_Ani.SendMessage("Reset_FaceHugger");
        }
        if (onChestBurster)
        {
            onChestBurster = false;
            onChestBurster_Over = false;
            ChestBurster_Timer = 0f;
        }
        gobj_Main_Camera.SendMessage("Reset_Zoom");
        gobj_Main_Camera.SendMessage("Set_Vignetting_Main");
        gobj_Menu.SendMessage("Sound_LevelUp");
        global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(info_LevelUp) as global::UnityEngine.GameObject;
        global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(effect_LevelUp) as global::UnityEngine.GameObject;
        gameObject2.transform.position = new global::UnityEngine.Vector3(PC.transform.position.x, PC.transform.position.y + 3f, 0f);
        gameObject2.transform.parent = PC.transform;
    }

    public void StatAdd_STR()
    {
        gobj_Menu.SendMessage("Sound_Tab");
        StatPoints--;
        STR++;
        ATK += Atk_Per_Str;
    }

    public void StatAdd_CON()
    {
        gobj_Menu.SendMessage("Sound_Tab");
        StatPoints--;
        CON++;
        HP_Max += HP_Per_Con;
        DEF++;
    }

    public void StatAdd_INT()
    {
        gobj_Menu.SendMessage("Sound_Tab");
        StatPoints--;
        INT++;
        MP_Max += MP_Per_Int;
    }

    public void StatAdd_LCK()
    {
        gobj_Menu.SendMessage("Sound_Tab");
        StatPoints--;
        LCK++;
    }

    public void Change_Weapon(int num)
    {
        Weapon_Num = num;
        PC.Set_Weapon();
    }

    public void Change_Skill(int num)
    {
        if (Skill_Total <= 1)
        {
            return;
        }
        if (num > 0)
        {
            int num2 = onSkill_Index[0];
            int num3 = 1;
            for (int i = 0; i < 4; i++)
            {
                onSkill_Index[i] = onSkill_Index[i + 1];
                if (onSkill_Index[i + 1] >= 0)
                {
                    num3 = i + 1;
                }
            }
            onSkill_Index[num3] = num2;
        }
        else
        {
            int num4 = 1;
            for (int j = 0; j < 4; j++)
            {
                if (onSkill_Index[j + 1] >= 0)
                {
                    num4 = j + 1;
                }
            }
            int num5 = onSkill_Index[num4];
            for (int num6 = num4; num6 > 0; num6--)
            {
                onSkill_Index[num6] = onSkill_Index[num6 - 1];
            }
            onSkill_Index[0] = num5;
        }
        Skill_Num = onSkill_Index[0] + 1;
        UC.Change_Skill();
        sr_SelBorder_Skill.position = global::UnityEngine.GameObject.Find("pos_B" + (Skill_Num + 5)).GetComponent<global::UnityEngine.RectTransform>().position;
    }

    public void Select_Skill(int num)
    {
        Skill_Num = num;
        num--;
        int num2 = 0;
        for (int i = 0; i < 5; i++)
        {
            if (onSkill_Index[i] == num)
            {
                num2 = i;
                i = 5;
            }
        }
        int num3 = 0;
        switch (num2)
        {
            case 1:
                {
                    int num13 = onSkill_Index[0];
                    onSkill_Index[0] = onSkill_Index[1];
                    if (onSkill_Index[2] >= 0)
                    {
                        onSkill_Index[1] = onSkill_Index[2];
                        num3++;
                    }
                    else
                    {
                        onSkill_Index[1] = -1;
                    }
                    if (onSkill_Index[3] >= 0)
                    {
                        onSkill_Index[2] = onSkill_Index[3];
                        num3++;
                    }
                    else
                    {
                        onSkill_Index[2] = -1;
                    }
                    if (onSkill_Index[4] >= 0)
                    {
                        onSkill_Index[3] = onSkill_Index[4];
                        num3++;
                    }
                    else
                    {
                        onSkill_Index[3] = -1;
                    }
                    onSkill_Index[1 + num3] = num13;
                    break;
                }
            case 2:
                {
                    int num11 = onSkill_Index[0];
                    int num12 = onSkill_Index[1];
                    onSkill_Index[0] = onSkill_Index[2];
                    if (onSkill_Index[3] >= 0)
                    {
                        onSkill_Index[1] = onSkill_Index[3];
                        num3++;
                    }
                    else
                    {
                        onSkill_Index[1] = -1;
                    }
                    if (onSkill_Index[4] >= 0)
                    {
                        onSkill_Index[2] = onSkill_Index[4];
                        num3++;
                    }
                    else
                    {
                        onSkill_Index[2] = -1;
                    }
                    onSkill_Index[1 + num3] = num11;
                    onSkill_Index[2 + num3] = num12;
                    break;
                }
            case 3:
                {
                    int num8 = onSkill_Index[0];
                    int num9 = onSkill_Index[1];
                    int num10 = onSkill_Index[2];
                    onSkill_Index[0] = onSkill_Index[3];
                    if (onSkill_Index[4] >= 0)
                    {
                        onSkill_Index[1] = onSkill_Index[4];
                        num3++;
                    }
                    else
                    {
                        onSkill_Index[4] = -1;
                    }
                    onSkill_Index[1 + num3] = num8;
                    onSkill_Index[2 + num3] = num9;
                    onSkill_Index[3 + num3] = num10;
                    break;
                }
            case 4:
                {
                    int num4 = onSkill_Index[0];
                    int num5 = onSkill_Index[1];
                    int num6 = onSkill_Index[2];
                    int num7 = onSkill_Index[3];
                    onSkill_Index[0] = onSkill_Index[4];
                    onSkill_Index[1] = num4;
                    onSkill_Index[2] = num5;
                    onSkill_Index[3] = num6;
                    onSkill_Index[4] = num7;
                    break;
                }
        }
        UC.Change_Skill();
    }

    private void Update_Skill_List()
    {
        Skill_Total = 1;
        onSkill_Index[0] = Skill_Num - 1;
        int num = Skill_Num - 1;
        int num2 = 1;
        for (int i = 0; i < 4; i++)
        {
            num = ((num != 4) ? (num + 1) : 0);
            if (onSkill_List[num])
            {
                onSkill_Index[num2] = num;
                num2++;
                Skill_Total++;
            }
        }
        UC.Change_Skill();
    }

    public int Get_PoisonDamage(global::UnityEngine.Vector3 pos_Font, int type)
    {
        global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Damage_Font, pos_Font, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
        int num = 0;
        switch (type)
        {
            case 1:
                num = Skill_DMG[2] + global::UnityEngine.Random.Range(-5, 10);
                gameObject.GetComponent<Dmg_Font>().Set_Number(num, 3);
                return num;
            case 39:
                gameObject.GetComponent<Dmg_Font>().Set_Number(0, 3);
                return 0;
            default:
                num = global::UnityEngine.Random.Range(8, 16);
                gameObject.GetComponent<Dmg_Font>().Set_Number(num, 3);
                gameObject.transform.localScale = new global::UnityEngine.Vector3(0.7f, 0.7f, 1f);
                return num;
        }
    }

    public int Get_MagicDamage(global::UnityEngine.Vector3 pos_Font, int num)
    {
        global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Damage_Font, pos_Font, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
        int num2 = Skill_DMG[num] + global::UnityEngine.Random.Range(0, 10);
        switch (num)
        {
            case 1:
                gameObject.GetComponent<Dmg_Font>().Set_Number(num2, 2);
                gameObject.transform.localScale = new global::UnityEngine.Vector3(1.6f, 1.6f, 1f);
                break;
            case 2:
                num2 *= 3;
                gameObject.GetComponent<Dmg_Font>().Set_Number(num2, 32);
                gameObject.transform.localScale = new global::UnityEngine.Vector3(1.6f, 1.6f, 1f);
                break;
            default:
                gameObject.GetComponent<Dmg_Font>().Set_Number(num2, 1);
                break;
        }
        return num2;
    }

    public int Get_AtkDamage(global::UnityEngine.Vector3 pos_Font, float ratio)
    {
        int num = 1;
        if (LCK >= 200 || global::UnityEngine.Random.Range(0, 200) < LCK)
        {
            num = 2;
        }
        int num2 = 0;
        num2 = ((Weapon_Num == 0) ? ((ATK + Weapon_DMG[Weapon_Num] + global::UnityEngine.Random.Range(-1, 2)) * num) : ((Weapon_Num != 1) ? ((ATK + Weapon_DMG[Weapon_Num] + global::UnityEngine.Random.Range(-12, 23)) * num) : ((ATK + Weapon_DMG[Weapon_Num] + global::UnityEngine.Random.Range(-3, 6)) * num)));
        num2 = (int)((float)num2 * ratio);
        global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Damage_Font, pos_Font, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
        gameObject.GetComponent<Dmg_Font>().Set_Number(num2, num);
        if (num > 1)
        {
            gameObject.transform.localScale = new global::UnityEngine.Vector3(1.6f, 1.6f, 1f);
        }
        return num2;
    }

    public void Get_MonTrapDmg(int dmg, global::UnityEngine.Vector3 pos_Font)
    {
        global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Damage_Font, pos_Font, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
        gameObject.GetComponent<Dmg_Font>().Set_Number(dmg, 1);
    }

    public void Get_Weapon_1()
    {
        onWeapon_1 = true;
        Check_FirstWeapon(1);
        img_Inv_Weapon_1.enabled = true;
    }

    public void Get_Weapon_2()
    {
        onWeapon_2 = true;
        Check_FirstWeapon(2);
        gobj_Menu.SendMessage("On_Item_2");
        img_Inv_Weapon_2.enabled = true;
        img_Inv_Weapon_2_Glow.enabled = true;
    }

    public void Get_Weapon_3()
    {
        onWeapon_3 = true;
        Check_FirstWeapon(3);
        gobj_Menu.SendMessage("On_Item_3");
        img_Inv_Weapon_3.enabled = true;
    }

    public void Get_Weapon_4()
    {
        onWeapon_4 = true;
        Check_FirstWeapon(4);
        gobj_Menu.SendMessage("On_Item_4");
        img_Inv_Weapon_4.enabled = true;
    }

    public void Get_Weapon_5()
    {
        onWeapon_5 = true;
        Check_FirstWeapon(5);
        gobj_Menu.SendMessage("On_Item_5");
        img_Inv_Weapon_5.enabled = true;
        img_Inv_Weapon_5_Glow.enabled = true;
    }

    public void Get_Skill_2()
    {
        onSkill_List[1] = (onSkill_2 = true);
        Update_Skill_List();
        img_Inv_Skill_2.enabled = true;
    }

    public void Get_Skill_3()
    {
        onSkill_List[2] = (onSkill_3 = true);
        Update_Skill_List();
        img_Inv_Skill_3.enabled = true;
    }

    public void Get_Skill_4()
    {
        onSkill_List[3] = (onSkill_4 = true);
        Update_Skill_List();
        img_Inv_Skill_4.enabled = true;
    }

    public void Get_Skill_5()
    {
        onSkill_List[4] = (onSkill_5 = true);
        Update_Skill_List();
        gobj_Menu.SendMessage("On_Item_10");
        img_Inv_Skill_5.enabled = true;
        img_Inv_Skill_5_Glow.enabled = true;
    }

    public void Get_Ability_1()
    {
        onBackDash = true;
        img_Inv_Ability_1.enabled = true;
    }

    public void Get_Ability_2()
    {
        onDBJump = true;
        img_Inv_Ability_2.enabled = true;
    }

    public void Get_Ability_3()
    {
        onSpeedUp = true;
        img_Inv_Ability_3.enabled = true;
    }

    public void Get_Ability_4()
    {
        onHighJump = true;
        img_Inv_Ability_4.enabled = true;
    }

    public void Get_Ability_5()
    {
        onScrew = true;
        img_Inv_Ability_5.enabled = true;
    }

    public void Get_Card_1()
    {
        onCard_1 = true;
       img_Inv_Card_1.enabled = true;
    }

    public void Get_Card_2()
    {
        onCard_2 = true;
       img_Inv_Card_2.enabled = true;
    }

    public void Get_Card_3()
    {
        onCard_3 = true;
       img_Inv_Card_3.enabled = true;
    }

    public void Get_Card_4()
    {
        onCard_4 = true;
       img_Inv_Card_4.enabled = true;
    }

    public void Get_Card_5()
    {
        onCard_5 = true;
       img_Inv_Card_5.enabled = true;
    }

    public void Get_Bonus_1(int num)
    {
        if (HP_Max + Hp_Per_BonusHp < 10000)
        {
            HP_Max += Hp_Per_BonusHp;
            HP += Hp_Per_BonusHp;
        }
        else
        {
            HP_Max = 9999;
            if (HP + Hp_Per_BonusHp > HP_Max)
            {
                HP = HP_Max;
            }
        }
        BonusItems[num] = 1;
        Bonus_HP++;
        img_Inv_Bonus_1.enabled = true;
    }

    public void Get_Bonus_2(int num)
    {
        if (MP_Max + Mp_Per_BonusMp < 10000)
        {
            MP_Max += Mp_Per_BonusMp;
            MP += Mp_Per_BonusMp;
        }
        else
        {
            MP_Max = 9999;
            if (MP + Mp_Per_BonusMp > MP_Max)
            {
                MP = MP_Max;
            }
        }
        BonusItems[num] = 1;
        Bonus_MP++;
        img_Inv_Bonus_2.enabled = true;
    }

    public void Get_Bonus_3(int num)
    {
        if (ATK + Atk_Per_BonusAtk < 10000)
        {
            ATK += Atk_Per_BonusAtk;
        }
        else
        {
            ATK = 9999;
        }
        BonusItems[num] = 1;
        Bonus_ATK++;
        img_Inv_Bonus_3.enabled = true;
    }

    public void Get_Bonus_4(int num)
    {
        BonusItems[num] = 1;
        Bonus_Regen++;
        img_Inv_Bonus_4.enabled = true;
    }

    public void Get_Bonus_5(int num)
    {
        if (Bonus_Blood < 5 && BonusItems[num] == 0)
        {
            BonusItems[num] = 1;
            Bonus_Blood++;
            img_Inv_Bonus_5.enabled = true;
        }
    }

    public void Get_Potion_HP(int num)
    {
        if (num > 0)
        {
            BonusItems[num] = 1;
        }
        Potion_HP++;
    }

    public void Get_Potion_MP(int num)
    {
        if (num > 0)
        {
            BonusItems[num] = 1;
        }
        Potion_MP++;
    }

    private void Make_PotionHeal()
    {
        Potion_Timer -= global::UnityEngine.Time.deltaTime;
        if (Cross_Timer <= 0f)
        {
            Cross_Timer = 0.1f;
            global::UnityEngine.Vector3 position = new global::UnityEngine.Vector3(PC.transform.position.x + (float)global::UnityEngine.Random.Range(-80, 80) * 0.01f, PC.transform.position.y + 2.4f + (float)global::UnityEngine.Random.Range(0, 20) * 0.1f, 0f);
            global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Heal_Cross, position, base.transform.rotation) as global::UnityEngine.GameObject;
        }
        else
        {
            Cross_Timer -= global::UnityEngine.Time.deltaTime;
        }
    }

    private void Make_RedHeal()
    {
        PotionRed_Timer -= global::UnityEngine.Time.deltaTime;
        if (CrossRed_Timer <= 0f)
        {
            CrossRed_Timer = 0.1f;
            global::UnityEngine.Vector3 position = new global::UnityEngine.Vector3(PC.transform.position.x + (float)global::UnityEngine.Random.Range(-80, 80) * 0.01f, PC.transform.position.y + 2.4f + (float)global::UnityEngine.Random.Range(0, 20) * 0.1f, 0f);
            global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Heal_Cross_Red, position, base.transform.rotation) as global::UnityEngine.GameObject;
        }
        else
        {
            CrossRed_Timer -= global::UnityEngine.Time.deltaTime;
        }
    }

    private void Make_BlueHeal()
    {
        PotionBlue_Timer -= global::UnityEngine.Time.deltaTime;
        if (CrossBlue_Timer <= 0f)
        {
            CrossBlue_Timer = 0.1f;
            global::UnityEngine.Vector3 position = new global::UnityEngine.Vector3(PC.transform.position.x + (float)global::UnityEngine.Random.Range(-80, 80) * 0.01f, PC.transform.position.y + 2.4f + (float)global::UnityEngine.Random.Range(0, 20) * 0.1f, 0f);
            global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Heal_Cross_Blue, position, base.transform.rotation) as global::UnityEngine.GameObject;
        }
        else
        {
            CrossBlue_Timer -= global::UnityEngine.Time.deltaTime;
        }
    }

    public void Get_Cloth()
    {
        onCloth = true;
        PC.OnOff_Cloth();
    }

    public void Get_HP(int Num)
    {
        if (Num == 1000)
        {
            HP = HP_Max;
            UC.Set_Damage();
            Potion_Timer = 2f;
            Cross_Timer = 0f;
        }
        else
        {
            HP = HP_Max;
            UC.Set_Damage();
            Potion_Timer = 2f;
            Cross_Timer = 0f;
        }
    }

    public void Get_Blood(int Num)
    {
        if (HP < HP_Max)
        {
            HP = ((HP + Num >= HP_Max) ? HP_Max : (HP += Num));
            UC.Set_Damage();
            PotionRed_Timer = 1.5f;
            CrossRed_Timer = 0f;
        }
    }

    private void Use_Potion_HP()
    {
        Potion_HP--;
        HP = ((HP + 100 >= HP_Max) ? HP_Max : (HP += 100));
        UC.Set_Damage();
        PotionRed_Timer = 1.5f;
        CrossRed_Timer = 0f;
        global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(effect_UsePotionHP) as global::UnityEngine.GameObject;
        gameObject.transform.position = new global::UnityEngine.Vector3(PC.transform.position.x, PC.transform.position.y + 3f, 0f);
        gameObject.transform.parent = PC.transform;
        gobj_Potion_Inventory.SendMessage("Use_Potion_HP");
        gobj_Menu.SendMessage("Sound_Get_FullHP");
        gobj_Menu.SendMessage("Sound_Get_Potion");
    }

    private void Use_Potion_MP()
    {
        Potion_MP--;
        MP = ((MP + 100 >= MP_Max) ? MP_Max : (MP += 100));
        UC.Set_Mana();
        PotionBlue_Timer = 1.5f;
        CrossBlue_Timer = 0f;
        global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(effect_UsePotionMP) as global::UnityEngine.GameObject;
        gameObject.transform.position = new global::UnityEngine.Vector3(PC.transform.position.x, PC.transform.position.y + 3f, 0f);
        gameObject.transform.parent = PC.transform;
        gobj_Potion_Inventory.SendMessage("Use_Potion_MP");
        gobj_Menu.SendMessage("Sound_Get_FullHP");
        gobj_Menu.SendMessage("Sound_Get_Potion");
    }

    private void Reset_Inventory()
    {
        //for (int i = 1; i < 6; i++)
        //{
        //    global::UnityEngine.GameObject.Find("Inv_Weapon_" + i).GetComponent<global::UnityEngine.UI.Image>().enabled = false;
        //    if (i > 1)
        //    {
        //        global::UnityEngine.GameObject.Find("Inv_Skill_" + i).GetComponent<global::UnityEngine.UI.Image>().enabled = false;
        //    }
        //    global::UnityEngine.GameObject.Find("Inv_Ability_" + i).GetComponent<global::UnityEngine.UI.Image>().enabled = false;
        //    global::UnityEngine.GameObject.Find("Inv_Card_" + i).GetComponent<global::UnityEngine.UI.Image>().enabled = false;
        //    global::UnityEngine.GameObject.Find("Inv_Bonus_" + i).GetComponent<global::UnityEngine.UI.Image>().enabled = false;
        //}

        img_Inv_Weapon_1.enabled = false;
        img_Inv_Weapon_2.enabled = false;
        img_Inv_Weapon_3.enabled = false;
        img_Inv_Weapon_4.enabled = false;
        img_Inv_Weapon_5.enabled = false;

        img_Inv_Skill_2.enabled = false;
        img_Inv_Skill_3.enabled = false;
        img_Inv_Skill_4.enabled = false;
        img_Inv_Skill_5.enabled = false;


        img_Inv_Ability_1.enabled = false;
        img_Inv_Ability_2.enabled = false;
        img_Inv_Ability_3.enabled = false;
        img_Inv_Ability_4.enabled = false;
        img_Inv_Ability_5.enabled = false;

        img_Inv_Card_1.enabled = false;
        img_Inv_Card_2.enabled = false;
        img_Inv_Card_3.enabled = false;
        img_Inv_Card_4.enabled = false;
        img_Inv_Card_5.enabled = false;

        img_Inv_Bonus_1.enabled = false;
        img_Inv_Bonus_2.enabled = false;
        img_Inv_Bonus_3.enabled = false;
        img_Inv_Bonus_4.enabled = false;
        img_Inv_Bonus_5.enabled = false;
        ///////////////

        img_Inv_Weapon_2_Glow.enabled = false;
        img_Inv_Weapon_5_Glow.enabled = false;
        img_Inv_Skill_5_Glow.enabled = false;
        img_SelBorder_Aug.enabled = false;
        sr_SelBorder_Skill.position = global::UnityEngine.GameObject.Find("pos_B6").GetComponent<global::UnityEngine.RectTransform>().position;
        txt_Inv_Bonus_Num_1.text = string.Empty;
        txt_Inv_Bonus_Num_2.text = string.Empty;
        txt_Inv_Bonus_Num_3.text = string.Empty;
        txt_Inv_Bonus_Num_4.text = string.Empty;
        txt_Inv_Bonus_Num_5.text = string.Empty;
    }

    public void Status_OnMenu()
    {
       txt_HP_Add.text = string.Empty;
        txt_MP_Add.text = string.Empty;
        txt_ATK_Add.text = string.Empty;
        txt_DEF_Add.text = string.Empty;
       txt_HP_Add.color = new global::UnityEngine.Color(0.5f, 1f, 0.9f, 0f);
        txt_MP_Add.color = new global::UnityEngine.Color(0.5f, 1f, 0.9f, 0f);
        txt_ATK_Add.color = new global::UnityEngine.Color(0.5f, 1f, 0.9f, 0f);
        txt_DEF_Add.color = new global::UnityEngine.Color(0.5f, 1f, 0.9f, 0f);
        txt_Inv_Bonus_Num_1.text = ((Bonus_HP <= 0) ? string.Empty : ("+" + Bonus_HP));
        txt_Inv_Bonus_Num_2.text = ((Bonus_MP <= 0) ? string.Empty : ("+" + Bonus_MP));
        txt_Inv_Bonus_Num_3.text = ((Bonus_ATK <= 0) ? string.Empty : ("+" + Bonus_ATK));
        txt_Inv_Bonus_Num_4.text = ((Bonus_Regen <= 0) ? string.Empty : ("+" + Bonus_Regen));
        txt_Inv_Bonus_Num_5.text = ((Bonus_Blood <= 0) ? string.Empty : ("+" + Bonus_Blood));
        txt_EXP_Num.text = ExpNow.ToString();
        txt_NEXT_Num.text = ExpNext.ToString();
        txt_KILLS_Num.text = Kills.ToString();
        txt_Text_Time.text = txt_Text_TimePlay.text;
        if (Bonus_Life > 0)
        {
            txt_HP_Num.fontStyle = global::UnityEngine.FontStyle.Bold;
            txt_HP_Num.color = new global::UnityEngine.Color(1f, 0.102f, 0f);
            txt_Death_Icon.enabled = true;
            txt_Stat_DEATH.enabled = true;
            txt_Death_Num.text = Bonus_Life.ToString();
        }
        else
        {
            txt_HP_Num.fontStyle = global::UnityEngine.FontStyle.Normal;
            txt_HP_Num.color = new global::UnityEngine.Color(1f, 1f, 1f);
            txt_Death_Icon.enabled = false;
            txt_Stat_DEATH.enabled = false;
            txt_Death_Num.text = string.Empty;
        }
    }

    public void Status_Update()
    {
        txt_Level_Num.text = Level.ToString();
        txt_StatPoints_Num.text = StatPoints.ToString();
        txt_HP_Num.text = HP + " / " + HP_Max;
        txt_MP_Num.text = MP + " / " + MP_Max;
        txt_ATK_Num.text = (ATK + Weapon_DMG[Weapon_Num]).ToString();
        txt_DEF_Num.text = DEF.ToString();
        txt_STR_Num.text = STR.ToString();
        txt_CON_Num.text = CON.ToString();
        txt_INT_Num.text = INT.ToString();
        txt_LCK_Num.text = LCK.ToString();
        float num = (float)LCK * 0.5f;
        if (num > 100f)
        {
            num = 100f;
        }
        txt_Critical_Num.text = "CRITICAL  " + num.ToString("f1") + "%";
    }

    private void Check_FirstWeapon(int num)
    {
        if (Weapon_Num == 0)
        {
            Change_Weapon(num);
            img_SelBorder_Aug.enabled = true;
            rect_SelBorder_Aug.localPosition = global::UnityEngine.GameObject.Find("pos_B" + num).GetComponent<global::UnityEngine.RectTransform>().localPosition;
            gobj_Menu.SendMessage("Sound_DeviceOn");
        }
    }

    public void Reset_Option()
    {
        Option_Volume[0] = 0.7f;
        Option_Volume[1] = 0.7f;
        Option_Int[0] = 0;
        Option_Int[2] = 2;
        Option_Int[3] = 2;
        Option_Int[4] = 2;
        AxiPlayerPrefs.SetFloat("SoundVolume", 0.7f);
        AxiPlayerPrefs.SetFloat("MusicVolume", 0.7f);
        AxiPlayerPrefs.SetInt("SelBGM", 0);
        AxiPlayerPrefs.SetInt("Censorship", 2);
        AxiPlayerPrefs.SetInt("On_Hscene", 2);
        AxiPlayerPrefs.SetInt("On_HealthBar", 2);
    }

    public bool Check_Bonus(int num)
    {
        if (BonusItems[num] == 1)
        {
            return true;
        }
        return false;
    }

    public void Check_Hscene(int num)
    {
        if (H_scene[num] == 0)
        {
            H_scene[num] = 1;
            AxiPlayerPrefs.SetInt("H_" + (num + 1), 1);
        }
    }

    public bool Check_WatchMap()
    {
        if (Map[Room_Num] == 0)
        {
            Map[Room_Num] = 1;
            return false;
        }
        return true;
    }

    public bool Check_EventMonster(int num)
    {
        if (Map[180 + num] > 0)
        {
            return true;
        }
        return false;
    }

    public void Set_EventMonster(int num)
    {
        Map[180 + num] = 1;
    }

    public bool Check_Teleport_On(int num)
    {
        if (Map[190 + num] > 0)
        {
            return true;
        }
        return false;
    }

    public void Set_Teleport_On(int num)
    {
        Map[190 + num] = 1;
    }

    public bool Get_Event(int num)
    {
        if (E_scene[num] > 0)
        {
            return true;
        }
        return false;
    }

    public void Set_Event(int num)
    {
        E_scene[num] = 1;
        if (num > 0 && num < 4)
        {
            Info_Mission_Complete(num);
            gobj_MissionBriefing.SendMessage("Complete_Mission_" + num);
            gobj_Menu.SendMessage("Sound_DeviceOn");
            gobj_Menu.SendMessage("Sound_Get_FullHP");
            switch (num)
            {
                case 1:
                    //EVE_Core  Computer_X 貌似动态
                    if (global::UnityEngine.GameObject.Find("EVE_Core") != null)
                    {
                        global::UnityEngine.GameObject.Find("EVE_Core").GetComponent<Event_Core>().Set_State(3);
                    }
                    if (global::UnityEngine.GameObject.Find("Computer_1") != null)
                    {
                        global::UnityEngine.GameObject.Find("Computer_1").GetComponent<Event_Computer>().Set_State(3);
                    }
                    if (global::UnityEngine.GameObject.Find("Computer_2") != null)
                    {
                        global::UnityEngine.GameObject.Find("Computer_2").GetComponent<Event_Computer>().Set_State(3);
                    }
                    if (global::UnityEngine.GameObject.Find("Computer_3") != null)
                    {
                        global::UnityEngine.GameObject.Find("Computer_3").GetComponent<Event_Computer>().Set_State(3);
                    }
                    cc_Main_Camera.targetSize = 11.2f;
                    break;
                case 3:
                    EventState = 200;
                    escapeTimer = 90f;
                    gobj_EscapeTimer.SendMessage("Set_Timer");
                    rect_Mission_4.localScale = new global::UnityEngine.Vector3(1f, 1f, 1f);
                    break;
            }
        }
        else
        {
            switch (num)
            {
                case 4:
                    gobj_Menu.SendMessage("Sound_DeviceOn");
                    gobj_Menu.SendMessage("Sound_Get_FullHP");
                    break;
                case 5:
                    onEvent = true;
                    gobj_Menu.SendMessage("Sound_DeviceOn");
                    gobj_Menu.SendMessage("Sound_Get_FullHP");
                    break;
            }
        }
    }

    private void Info_Mission_Complete(int num)
    {
        global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(info_Mission) as global::UnityEngine.GameObject;
        int lang_num = AxiPlayerPrefs.GetInt("Language_Num");
        Language_MenuItem component = gobj_Menu.GetComponent<Language_MenuItem>();
        if (num < 3)
        {
            gameObject.GetComponent<Fade_MissionComplete>().Set_Text(component.MissionBriefing(0, lang_num), component.MissionBriefing(num, lang_num), string.Empty);
        }
        else
        {
            gameObject.GetComponent<Fade_MissionComplete>().Set_Text(component.MissionBriefing(0, lang_num), component.MissionBriefing(3, lang_num), component.MapText(3, lang_num));
        }
    }

    public void Check_Event()
    {
        if (E_scene[1] > 0)
        {
            //EVE_Core  Computer_X 貌似动态
            gobj_MissionBriefing.SendMessage("Complete_Mission_1");
            if (global::UnityEngine.GameObject.Find("EVE_Core") != null)
            {
                global::UnityEngine.GameObject.Find("EVE_Core").GetComponent<Event_Core>().Set_State(2);
            }
            if (global::UnityEngine.GameObject.Find("Computer_1") != null)
            {
                global::UnityEngine.GameObject.Find("Computer_1").GetComponent<Event_Computer>().Set_State(2);
            }
            if (global::UnityEngine.GameObject.Find("Computer_2") != null)
            {
                global::UnityEngine.GameObject.Find("Computer_2").GetComponent<Event_Computer>().Set_State(2);
            }
            if (global::UnityEngine.GameObject.Find("Computer_3") != null)
            {
                global::UnityEngine.GameObject.Find("Computer_3").GetComponent<Event_Computer>().Set_State(2);
            }
        }
        if (E_scene[2] > 0)
        {
            gobj_MissionBriefing.SendMessage("Complete_Mission_2");
        }
        if (E_scene[3] > 0)
        {
            EventState = 200;
            escapeTimer = 90f;
            gobj_EscapeTimer.SendMessage("Set_Timer");
            global::UnityEngine.Debug.Log("E_scene[3] > 0");
            gobj_MissionBriefing.SendMessage("Complete_Mission_3");
        }
        else
        {
            rect_Mission_4.localScale = new global::UnityEngine.Vector3(0f, 0f, 0f);
        }
        Potion_HP = E_scene[9];
        Potion_MP = E_scene[10];
    }

    private void Load_Data()
    {
        Slot_Num = AxiPlayerPrefs.GetInt("Slot_Num");
        global::UnityEngine.Debug.Log("Slot " + (Slot_Num + 1) + " : Loaded!!");
        GetComponent<Save_Control>().Load_Game();
        Room_Num = GetComponent<Save_Control>().SaveData.Room_Num[Slot_Num];
        Level = GetComponent<Save_Control>().SaveData.Level[Slot_Num];
        HP = GetComponent<Save_Control>().SaveData.HP[Slot_Num];
        HP_Max = GetComponent<Save_Control>().SaveData.HP_Max[Slot_Num];
        MP = GetComponent<Save_Control>().SaveData.MP[Slot_Num];
        MP_Max = GetComponent<Save_Control>().SaveData.MP_Max[Slot_Num];
        onCloth = GetComponent<Save_Control>().SaveData.onCloth[Slot_Num];
        if (!onCloth)
        {
            PC.OnOff_Cloth();
        }
        ATK = GetComponent<Save_Control>().SaveData.ATK[Slot_Num];
        DEF = GetComponent<Save_Control>().SaveData.DEF[Slot_Num];
        STR = GetComponent<Save_Control>().SaveData.STR[Slot_Num];
        CON = GetComponent<Save_Control>().SaveData.CON[Slot_Num];
        INT = GetComponent<Save_Control>().SaveData.INT[Slot_Num];
        LCK = GetComponent<Save_Control>().SaveData.LCK[Slot_Num];
        ExpNow = GetComponent<Save_Control>().SaveData.ExpNow[Slot_Num];
        Kills = GetComponent<Save_Control>().SaveData.Kills[Slot_Num];
        Rate = GetComponent<Save_Control>().SaveData.Rate[Slot_Num];
        PlayTime = GetComponent<Save_Control>().SaveData.PlayTime[Slot_Num];
        StatPoints = GetComponent<Save_Control>().SaveData.StatPoints[Slot_Num];
        Weapon_Num = GetComponent<Save_Control>().SaveData.Weapon_Num[Slot_Num];
        Skill_Num = GetComponent<Save_Control>().SaveData.Skill_Num[Slot_Num];
        onWeapon_1 = GetComponent<Save_Control>().SaveData.onWeapon_1[Slot_Num];
        onWeapon_2 = GetComponent<Save_Control>().SaveData.onWeapon_2[Slot_Num];
        onWeapon_3 = GetComponent<Save_Control>().SaveData.onWeapon_3[Slot_Num];
        onWeapon_4 = GetComponent<Save_Control>().SaveData.onWeapon_4[Slot_Num];
        onWeapon_5 = GetComponent<Save_Control>().SaveData.onWeapon_5[Slot_Num];
        bool[] array = onSkill_List;
        bool num = GetComponent<Save_Control>().SaveData.onSkill_2[Slot_Num];
        bool flag = num;
        onSkill_2 = num;
        array[1] = flag;
        bool[] array2 = onSkill_List;
        bool num2 = GetComponent<Save_Control>().SaveData.onSkill_3[Slot_Num];
        flag = num2;
        onSkill_3 = num2;
        array2[2] = flag;
        bool[] array3 = onSkill_List;
        bool num3 = GetComponent<Save_Control>().SaveData.onSkill_4[Slot_Num];
        flag = num3;
        onSkill_4 = num3;
        array3[3] = flag;
        bool[] array4 = onSkill_List;
        bool num4 = GetComponent<Save_Control>().SaveData.onSkill_5[Slot_Num];
        flag = num4;
        onSkill_5 = num4;
        array4[4] = flag;
        Update_Skill_List();
        onBackDash = GetComponent<Save_Control>().SaveData.onBackDash[Slot_Num];
        onDBJump = GetComponent<Save_Control>().SaveData.onDBJump[Slot_Num];
        onSpeedUp = GetComponent<Save_Control>().SaveData.onSpeedUp[Slot_Num];
        onHighJump = GetComponent<Save_Control>().SaveData.onHighJump[Slot_Num];
        onScrew = GetComponent<Save_Control>().SaveData.onScrew[Slot_Num];
        onCard_1 = GetComponent<Save_Control>().SaveData.onCard_1[Slot_Num];
        onCard_2 = GetComponent<Save_Control>().SaveData.onCard_2[Slot_Num];
        onCard_3 = GetComponent<Save_Control>().SaveData.onCard_3[Slot_Num];
        onCard_4 = GetComponent<Save_Control>().SaveData.onCard_4[Slot_Num];
        onCard_5 = GetComponent<Save_Control>().SaveData.onCard_5[Slot_Num];
        Bonus_HP = GetComponent<Save_Control>().SaveData.Bonus_HP[Slot_Num];
        Bonus_MP = GetComponent<Save_Control>().SaveData.Bonus_MP[Slot_Num];
        Bonus_ATK = GetComponent<Save_Control>().SaveData.Bonus_ATK[Slot_Num];
        Bonus_Regen = GetComponent<Save_Control>().SaveData.Bonus_Regen[Slot_Num];
        Bonus_Blood = GetComponent<Save_Control>().SaveData.Bonus_Blood[Slot_Num];
        Bonus_Life = GetComponent<Save_Control>().SaveData.Bonus_Life[Slot_Num];
        if (onWeapon_1)
        {
            img_Inv_Weapon_1.enabled = true;
        }
        if (onWeapon_2)
        {
            gobj_Menu.SendMessage("On_Item_2");
            img_Inv_Weapon_2.enabled = true;
            img_Inv_Weapon_2_Glow.enabled = true;
        }
        if (onWeapon_3)
        {
            gobj_Menu.SendMessage("On_Item_3");
            img_Inv_Weapon_3.enabled = true;
        }
        if (onWeapon_4)
        {
            gobj_Menu.SendMessage("On_Item_4");
            img_Inv_Weapon_4.enabled = true;
        }
        if (onWeapon_5)
        {
            gobj_Menu.SendMessage("On_Item_5");
            img_Inv_Weapon_5.enabled = true;
            img_Inv_Weapon_5_Glow.enabled = true;
        }
        if (onWeapon_1 || onWeapon_2 || onWeapon_3 || onWeapon_4 || onWeapon_5)
        {
            img_SelBorder_Aug.enabled = true;
            rect_SelBorder_Aug.localPosition = global::UnityEngine.GameObject.Find("pos_B" + Weapon_Num).GetComponent<global::UnityEngine.RectTransform>().localPosition;
            PC.Set_Weapon();
        }
        if (onSkill_2)
        {
            img_Inv_Skill_2.enabled = true;
        }
        if (onSkill_3)
        {
            img_Inv_Skill_3.enabled = true;
        }
        if (onSkill_4)
        {
            img_Inv_Skill_4.enabled = true;
        }
        if (onSkill_5)
        {
            gobj_Menu.SendMessage("On_Item_10");
            img_Inv_Skill_5.enabled = true;
            img_Inv_Skill_5_Glow.enabled = true;
        }
        if (onSkill_2 || onSkill_3 || onSkill_4 || onSkill_5)
        {
            sr_SelBorder_Skill.localPosition = global::UnityEngine.GameObject.Find("pos_B" + (5 + Skill_Num)).GetComponent<global::UnityEngine.RectTransform>().localPosition;
        }
        if (onBackDash)
        {
            img_Inv_Ability_1.enabled = true;
        }
        if (onDBJump)
        {
            img_Inv_Ability_2.enabled = true;
        }
        if (onSpeedUp)
        {
            img_Inv_Ability_3.enabled = true;
        }
        if (onHighJump)
        {
            img_Inv_Ability_4.enabled = true;
        }
        if (onScrew)
        {
            img_Inv_Ability_5.enabled = true;
        }
        if (onCard_1)
        {
           img_Inv_Card_1.enabled = true;
        }
        if (onCard_2)
        {
           img_Inv_Card_2.enabled = true;
        }
        if (onCard_3)
        {
           img_Inv_Card_3.enabled = true;
        }
        if (onCard_4)
        {
           img_Inv_Card_4.enabled = true;
        }
        if (onCard_5)
        {
           img_Inv_Card_5.enabled = true;
        }
        if (Bonus_HP > 0)
        {
            img_Inv_Bonus_1.enabled = true;
        }
        if (Bonus_MP > 0)
        {
            img_Inv_Bonus_2.enabled = true;
        }
        if (Bonus_ATK > 0)
        {
            img_Inv_Bonus_3.enabled = true;
        }
        if (Bonus_Regen > 0)
        {
            img_Inv_Bonus_4.enabled = true;
        }
        if (Bonus_Blood > 0)
        {
            img_Inv_Bonus_5.enabled = true;
        }
        if (Slot_Num == 0)
        {
            E_scene = GetComponent<Save_Control>().SaveData.E_scene_1;
            Map = GetComponent<Save_Control>().SaveData.Map_1;
            BonusItems = GetComponent<Save_Control>().SaveData.BonusItems_1;
        }
        else if (Slot_Num == 1)
        {
            E_scene = GetComponent<Save_Control>().SaveData.E_scene_2;
            Map = GetComponent<Save_Control>().SaveData.Map_2;
            BonusItems = GetComponent<Save_Control>().SaveData.BonusItems_2;
        }
        else if (Slot_Num == 2)
        {
            E_scene = GetComponent<Save_Control>().SaveData.E_scene_3;
            Map = GetComponent<Save_Control>().SaveData.Map_3;
            BonusItems = GetComponent<Save_Control>().SaveData.BonusItems_3;
        }
        if (GetComponent<Save_Control>().SaveData.H_scene.Length < H_scene.Length)
        {
            int num5 = GetComponent<Save_Control>().SaveData.H_scene.Length;
            for (int i = 0; i < num5; i++)
            {
                H_scene[i] = GetComponent<Save_Control>().SaveData.H_scene[i];
            }
        }
        else
        {
            H_scene = GetComponent<Save_Control>().SaveData.H_scene;
        }
        H_Over = GetComponent<Save_Control>().SaveData.H_Over;
        StageManager component = sm_StageManager;
        component.Hide_UnsightMap(Map);
        if (PlayTime > 1f && E_scene[0] > 0)
        {
            onEvent = false;
            EventState = 10;
            component.Load_Game();
            Check_Event();
        }
        else
        {
            EventState = 100;
            component.First_Game();
            Potion_HP = 3;
            Potion_MP = 3;
            eventctrl_Dialogue.Start_OpeningEvent();
            rect_Mission_4.localScale = new global::UnityEngine.Vector3(0f, 0f, 0f);
        }
        gobj_Potion_Inventory.SendMessage("Set_Text_HPMP");
        Option_Volume[0] = AxiPlayerPrefs.GetFloat("SoundVolume");
        Option_Volume[1] = AxiPlayerPrefs.GetFloat("MusicVolume");
        Option_Int[0] = AxiPlayerPrefs.GetInt("SelBGM");
        if (AxiPlayerPrefs.GetInt("WindowSize") == 1280)
        {
            Option_Int[1] = 1;
        }
        else
        {
            Option_Int[1] = 2;
        }
        if (AxiPlayerPrefs.GetInt("Censorship") == 1)
        {
            Option_Int[2] = 1;
        }
        else
        {
            Option_Int[2] = 2;
        }
        if (AxiPlayerPrefs.GetInt("On_Hscene") == 1)
        {
            Option_Int[3] = 1;
        }
        else
        {
            Option_Int[3] = 2;
        }
        if (AxiPlayerPrefs.GetInt("On_HealthBar") == 1)
        {
            Option_Int[4] = 1;
        }
        else
        {
            Option_Int[4] = 2;
        }
    }

    private void InfoText_Status()
    {
        if (txt_Info_Text.text != string.Empty)
        {
            txt_Info_Text.text = string.Empty;
            return;
        }
        string empty = string.Empty;
        string text = empty;
        empty = text + " Version \t:   " + GetComponent<Save_Control>().SaveData.version + "\n";
        empty += " IsCleared \t:";
        text = empty;
        empty = text + "  [  " + GetComponent<Save_Control>().SaveData.E_scene_1[19] + "  ]  ";
        text = empty;
        empty = text + "  [  " + GetComponent<Save_Control>().SaveData.E_scene_2[19] + "  ]  ";
        text = empty;
        empty = text + "  [  " + GetComponent<Save_Control>().SaveData.E_scene_3[19] + "  ]  \n";
        text = empty;
        empty = text + " IsSaved \t:  [  " + GetComponent<Save_Control>().SaveData.isSaved[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " Room_Num \t:  [  " + GetComponent<Save_Control>().SaveData.Room_Num[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " Level \t:  [  " + GetComponent<Save_Control>().SaveData.Level[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " HP \t:  [  " + GetComponent<Save_Control>().SaveData.HP[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " HP_Max \t:  [  " + GetComponent<Save_Control>().SaveData.HP_Max[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " MP \t:  [  " + GetComponent<Save_Control>().SaveData.MP[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " MP_Max \t:  [  " + GetComponent<Save_Control>().SaveData.MP_Max[Slot_Num] + "  ] \n";
        empty += "\n";
        text = empty;
        empty = text + " onCloth  :  [  " + GetComponent<Save_Control>().SaveData.onCloth[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " ATK  :  [  " + GetComponent<Save_Control>().SaveData.ATK[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " DEF  :  [  " + GetComponent<Save_Control>().SaveData.DEF[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " STR  :  [  " + GetComponent<Save_Control>().SaveData.STR[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " CON  :  [  " + GetComponent<Save_Control>().SaveData.CON[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " INT  :  [  " + GetComponent<Save_Control>().SaveData.INT[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " LCK  :  [  " + GetComponent<Save_Control>().SaveData.LCK[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " ExpNow  :  [  " + GetComponent<Save_Control>().SaveData.ExpNow[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " Kills  :  [  " + GetComponent<Save_Control>().SaveData.Kills[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " Rate  :  [  " + GetComponent<Save_Control>().SaveData.Rate[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " PlayTime  :  [  " + (int)GetComponent<Save_Control>().SaveData.PlayTime[Slot_Num] + "sec  ] \n";
        empty += "\n";
        text = empty;
        empty = text + " StatPoints  :  [  " + GetComponent<Save_Control>().SaveData.StatPoints[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " Weapon_Num  :  [  " + GetComponent<Save_Control>().SaveData.Weapon_Num[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " Skill_Num  :  [  " + GetComponent<Save_Control>().SaveData.Skill_Num[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " onWeapon_1  :  [  " + GetComponent<Save_Control>().SaveData.onWeapon_1[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " onWeapon_2  :  [  " + GetComponent<Save_Control>().SaveData.onWeapon_2[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " onWeapon_3  :  [  " + GetComponent<Save_Control>().SaveData.onWeapon_3[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " onWeapon_4  :  [  " + GetComponent<Save_Control>().SaveData.onWeapon_4[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " onWeapon_5  :  [  " + GetComponent<Save_Control>().SaveData.onWeapon_5[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " onSkill_2  :  [  " + GetComponent<Save_Control>().SaveData.onSkill_2[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " onSkill_3  :  [  " + GetComponent<Save_Control>().SaveData.onSkill_3[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " onSkill_4  :  [  " + GetComponent<Save_Control>().SaveData.onSkill_4[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " onSkill_5  :  [  " + GetComponent<Save_Control>().SaveData.onSkill_5[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " onBackDash  :  [  " + GetComponent<Save_Control>().SaveData.onBackDash[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " onDBJump  :  [  " + GetComponent<Save_Control>().SaveData.onDBJump[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " onSpeedUp  :  [  " + GetComponent<Save_Control>().SaveData.onSpeedUp[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " onHighJump  :  [  " + GetComponent<Save_Control>().SaveData.onHighJump[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " onScrew  :  [  " + GetComponent<Save_Control>().SaveData.onScrew[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " onCard_1  :  [  " + GetComponent<Save_Control>().SaveData.onCard_1[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " onCard_2  :  [  " + GetComponent<Save_Control>().SaveData.onCard_2[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " onCard_3  :  [  " + GetComponent<Save_Control>().SaveData.onCard_3[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " onCard_4  :  [  " + GetComponent<Save_Control>().SaveData.onCard_4[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " onCard_5  :  [  " + GetComponent<Save_Control>().SaveData.onCard_5[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " Bonus_HP  :  [  " + GetComponent<Save_Control>().SaveData.Bonus_HP[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " Bonus_MP  :  [  " + GetComponent<Save_Control>().SaveData.Bonus_MP[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " Bonus_ATK  :  [  " + GetComponent<Save_Control>().SaveData.Bonus_ATK[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " Bonus_Regen  :  [  " + GetComponent<Save_Control>().SaveData.Bonus_Regen[Slot_Num] + "  ] \n";
        text = empty;
        empty = text + " Bonus_Blood  :  [  " + GetComponent<Save_Control>().SaveData.Bonus_Blood[Slot_Num] + "  ] \n";
        txt_Info_Text.text = empty;
    }

    private void InfoText_EventMap()
    {
        if (txt_Info_Text.text != string.Empty)
        {
            txt_Info_Text.text = string.Empty;
            return;
        }
        string empty = string.Empty;
        empty = empty + " Event  Slot " + (Slot_Num + 1) + ": ------------------- \n";
        for (int i = 0; i < 20; i++)
        {
            empty = empty + "  " + E_scene[i];
        }
        empty += "\n\n\n\n\n";
        empty = empty + " Map  Slot " + (Slot_Num + 1) + ": ------------------- \n";
        for (int j = 0; j < 10; j++)
        {
            empty = empty + "  " + Map[j];
        }
        empty += "\t,  ";
        for (int k = 10; k < 20; k++)
        {
            empty = empty + "  " + Map[k];
        }
        empty += "\n";
        for (int l = 20; l < 30; l++)
        {
            empty = empty + "  " + Map[l];
        }
        empty += "\t,  ";
        for (int m = 30; m < 40; m++)
        {
            empty = empty + "  " + Map[m];
        }
        empty += "\n";
        for (int n = 40; n < 50; n++)
        {
            empty = empty + "  " + Map[n];
        }
        empty += "\t,  ";
        for (int num = 50; num < 60; num++)
        {
            empty = empty + "  " + Map[num];
        }
        empty += "\n";
        for (int num2 = 60; num2 < 70; num2++)
        {
            empty = empty + "  " + Map[num2];
        }
        empty += "\t,  ";
        for (int num3 = 70; num3 < 80; num3++)
        {
            empty = empty + "  " + Map[num3];
        }
        empty += "\n";
        for (int num4 = 80; num4 < 90; num4++)
        {
            empty = empty + "  " + Map[num4];
        }
        empty += "\t,  ";
        for (int num5 = 90; num5 < 100; num5++)
        {
            empty = empty + "  " + Map[num5];
        }
        empty += "\n";
        for (int num6 = 100; num6 < 110; num6++)
        {
            empty = empty + "  " + Map[num6];
        }
        empty += "\t,  ";
        for (int num7 = 110; num7 < 120; num7++)
        {
            empty = empty + "  " + Map[num7];
        }
        empty += "\n";
        for (int num8 = 120; num8 < 130; num8++)
        {
            empty = empty + "  " + Map[num8];
        }
        empty += "\t,  ";
        for (int num9 = 130; num9 < 140; num9++)
        {
            empty = empty + "  " + Map[num9];
        }
        empty += "\n";
        for (int num10 = 140; num10 < 150; num10++)
        {
            empty = empty + "  " + Map[num10];
        }
        empty += "\t,  ";
        for (int num11 = 150; num11 < 160; num11++)
        {
            empty = empty + "  " + Map[num11];
        }
        empty += "\n";
        for (int num12 = 160; num12 < 170; num12++)
        {
            empty = empty + "  " + Map[num12];
        }
        empty += "\t,  ";
        for (int num13 = 170; num13 < 180; num13++)
        {
            empty = empty + "  " + Map[num13];
        }
        empty += "\n";
        for (int num14 = 180; num14 < 190; num14++)
        {
            empty = empty + "  " + Map[num14];
        }
        empty += "\t,  ";
        for (int num15 = 190; num15 < 200; num15++)
        {
            empty = empty + "  " + Map[num15];
        }
        empty += "\n";
        empty += "\n\n\n\n\n";
        empty += " H_scene: ------------------- \n";
        for (int num16 = 0; num16 < 40; num16++)
        {
            empty = empty + " " + H_scene[num16];
        }
        empty += "\n\n\n";
        empty += " H_Over: ------------------- \n";
        for (int num17 = 0; num17 < 20; num17++)
        {
            empty = empty + " " + H_Over[num17];
        }
        txt_Info_Text.text = empty;
    }

    private void Info_LoadingError()
    {
        txt_Info_Text.text = "Loading Data Error!!!! Wrong SaveData";
    }

    private void Reset_Slot()
    {
        global::UnityEngine.Debug.Log("Slot " + (Slot_Num + 1) + " :  Reset!!----");
        GetComponent<Save_Control>().Delete_Data(Slot_Num, false);
    }

    public void Save_Data()
    {
        gobj_Menu.SendMessage("Sound_DeviceOn");
        global::UnityEngine.Debug.Log("Slot " + (Slot_Num + 1) + " :  Saved!!");
        GetComponent<Save_Control>().SaveData.version = 1f;
        GetComponent<Save_Control>().SaveData.isSaved[Slot_Num] = true;
        GetComponent<Save_Control>().SaveData.Room_Num[Slot_Num] = Room_Num;
        GetComponent<Save_Control>().SaveData.Level[Slot_Num] = Level;
        GetComponent<Save_Control>().SaveData.HP[Slot_Num] = HP;
        GetComponent<Save_Control>().SaveData.HP_Max[Slot_Num] = HP_Max;
        GetComponent<Save_Control>().SaveData.MP[Slot_Num] = MP;
        GetComponent<Save_Control>().SaveData.MP_Max[Slot_Num] = MP_Max;
        GetComponent<Save_Control>().SaveData.onCloth[Slot_Num] = onCloth;
        GetComponent<Save_Control>().SaveData.ATK[Slot_Num] = ATK;
        GetComponent<Save_Control>().SaveData.DEF[Slot_Num] = DEF;
        GetComponent<Save_Control>().SaveData.STR[Slot_Num] = STR;
        GetComponent<Save_Control>().SaveData.CON[Slot_Num] = CON;
        GetComponent<Save_Control>().SaveData.INT[Slot_Num] = INT;
        GetComponent<Save_Control>().SaveData.LCK[Slot_Num] = LCK;
        GetComponent<Save_Control>().SaveData.ExpNow[Slot_Num] = ExpNow;
        GetComponent<Save_Control>().SaveData.Kills[Slot_Num] = Kills;
        GetComponent<Save_Control>().SaveData.Rate[Slot_Num] = Rate;
        GetComponent<Save_Control>().SaveData.PlayTime[Slot_Num] = PlayTime;
        GetComponent<Save_Control>().SaveData.StatPoints[Slot_Num] = StatPoints;
        GetComponent<Save_Control>().SaveData.Weapon_Num[Slot_Num] = Weapon_Num;
        GetComponent<Save_Control>().SaveData.Skill_Num[Slot_Num] = Skill_Num;
        GetComponent<Save_Control>().SaveData.onWeapon_1[Slot_Num] = onWeapon_1;
        GetComponent<Save_Control>().SaveData.onWeapon_2[Slot_Num] = onWeapon_2;
        GetComponent<Save_Control>().SaveData.onWeapon_3[Slot_Num] = onWeapon_3;
        GetComponent<Save_Control>().SaveData.onWeapon_4[Slot_Num] = onWeapon_4;
        GetComponent<Save_Control>().SaveData.onWeapon_5[Slot_Num] = onWeapon_5;
        GetComponent<Save_Control>().SaveData.onSkill_2[Slot_Num] = onSkill_2;
        GetComponent<Save_Control>().SaveData.onSkill_3[Slot_Num] = onSkill_3;
        GetComponent<Save_Control>().SaveData.onSkill_4[Slot_Num] = onSkill_4;
        GetComponent<Save_Control>().SaveData.onSkill_5[Slot_Num] = onSkill_5;
        GetComponent<Save_Control>().SaveData.onScrew[Slot_Num] = onScrew;
        GetComponent<Save_Control>().SaveData.onHighJump[Slot_Num] = onHighJump;
        GetComponent<Save_Control>().SaveData.onSpeedUp[Slot_Num] = onSpeedUp;
        GetComponent<Save_Control>().SaveData.onDBJump[Slot_Num] = onDBJump;
        GetComponent<Save_Control>().SaveData.onBackDash[Slot_Num] = onBackDash;
        GetComponent<Save_Control>().SaveData.onCard_1[Slot_Num] = onCard_1;
        GetComponent<Save_Control>().SaveData.onCard_2[Slot_Num] = onCard_2;
        GetComponent<Save_Control>().SaveData.onCard_3[Slot_Num] = onCard_3;
        GetComponent<Save_Control>().SaveData.onCard_4[Slot_Num] = onCard_4;
        GetComponent<Save_Control>().SaveData.onCard_5[Slot_Num] = onCard_5;
        GetComponent<Save_Control>().SaveData.Bonus_HP[Slot_Num] = Bonus_HP;
        GetComponent<Save_Control>().SaveData.Bonus_MP[Slot_Num] = Bonus_MP;
        GetComponent<Save_Control>().SaveData.Bonus_ATK[Slot_Num] = Bonus_ATK;
        GetComponent<Save_Control>().SaveData.Bonus_Regen[Slot_Num] = Bonus_Regen;
        GetComponent<Save_Control>().SaveData.Bonus_Blood[Slot_Num] = Bonus_Blood;
        GetComponent<Save_Control>().SaveData.Bonus_Life[Slot_Num] = Bonus_Life;
        E_scene[9] = Potion_HP;
        E_scene[10] = Potion_MP;
        if (Slot_Num == 0)
        {
            GetComponent<Save_Control>().SaveData.E_scene_1 = E_scene;
            GetComponent<Save_Control>().SaveData.Map_1 = Map;
            GetComponent<Save_Control>().SaveData.BonusItems_1 = BonusItems;
        }
        else if (Slot_Num == 1)
        {
            GetComponent<Save_Control>().SaveData.E_scene_2 = E_scene;
            GetComponent<Save_Control>().SaveData.Map_2 = Map;
            GetComponent<Save_Control>().SaveData.BonusItems_2 = BonusItems;
        }
        else if (Slot_Num == 2)
        {
            GetComponent<Save_Control>().SaveData.E_scene_3 = E_scene;
            GetComponent<Save_Control>().SaveData.Map_3 = Map;
            GetComponent<Save_Control>().SaveData.BonusItems_3 = BonusItems;
        }
        GetComponent<Save_Control>().SaveData.H_scene = H_scene;
        GetComponent<Save_Control>().SaveData.H_Over = H_Over;
        GetComponent<Save_Control>().Save_Game();
    }

    public string Get_KeyText(int item_Num)
    {
        switch (item_Num)
        {
            case 11:
                if (Input_Mode == 1)
                {
                    return "( press  LB )";
                }
                return "( press  '" + CK.KeyToString_Out(8) + "' )";
            case 13:
                if (Input_Mode == 1)
                {
                    return "( hold  RB )";
                }
                return "( hold  '" + CK.KeyToString_Out(9) + "' )";
            case 14:
                if (Input_Mode == 1)
                {
                    return "( UP  +  LB )";
                }
                return "( UP  +  '" + CK.KeyToString_Out(8) + "' )";
            default:
                return string.Empty;
        }
    }
}
