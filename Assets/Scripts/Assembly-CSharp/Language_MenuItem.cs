public class Language_MenuItem : global::UnityEngine.MonoBehaviour
{
	private int Lang_Num;

	private string[] Char_Name = new string[3] { "Ellen", "エレン", "爱莲娜" };

	private string[,] Item_Name = new string[28, 3]
	{
		{ "Plasma Star", "プラズマスター", "等离子体" },
	{ "Thunder Blade", "サンダーブレード", "雷霆剑" },
	{ "Poison Skull", "毒の頭蓋骨", "剧毒骷髅" },
	{ "Frozen Blade", "凍った刃", "冰洁之刃" },
	{ "Dark Shard", "ダークシャード", "暗影碎片" },
	{ "Energy Bolt", "エネルギーボルト", "能量箭" },
	{ "Power Bomb", "パワーボム", "重力炸弹" },
	{ "Poison Wave", "毒波", "毒波" },
	{ "Shield", "シールド", "神盾" },
	{ "Gravity Orb", "重力オーブ", "重力球" },
	{ "Back Dash", "バックダッシュ", "后跳" },
	{ "Double Jump", "ダブルジャンプ", "二段跳" },
	{ "Speed Up", "スピードアップ", "神速" },
	{ "High Jump", "高跳び", "高跳" },
	{ "Screw Attack", "スクリューアタック", "旋转攻击" },
	{ "Yellow Card", "イエローカード", "黄色卡片" },
	{ "Blue Card", "ブルーカード", "蓝色卡片" },
	{ "Green Card", "グリーンカード", "绿色卡片" },
	{ "Orange Card", "オレンジ色のカード", "橙色卡片" },
	{ "Violet Card", "バイオレットカード", "紫色卡片" },
	{ "Max HP +50", "Max HP +50", "Max HP +50" },
	{ "Max MP +40", "Max MP +40", "Max MP +40" },
	{ "ATK +20", "ATK +20", "ATK +20" },
	{ "Mana Orb", "マナオーブ", "玛娜之球" },
	{ "Blood Skull", "血の頭蓋骨", "鲜血骷髅" },
	{ "Healing Potion", "ヒーリングポーション", "恢复药剂" },
	{ "Medi Kit", "メディキット", "メディキット" },
	{ "Mana Potion", "マナポーション", "玛娜药剂" },
	};

	private string[,] Item_Desc = new string[28, 3]
	{
		{ " ", "  " , "  " },
		{ " ", "  " , "  " },
		{ " ", "  " , "  " },
		{ " ", "  " , "  " },
		{ " ", "  " , "  " },
		{ " ", "  " , "  " },
		{ " ", "  " , "  " },
		{ " ", "  " , "  " },
		{ " ", "  " , "  " },
		{ " ", "  " , "  " },
		{ "( press  LB )", "( press  LB )" , "( press  LB )" },
		{ " ", "  " , "  "},
		{ "50% Faster Movement speed", "50％高速移動速度", "50％高速移動速度" },
		{ "UP + (LB or ", "UP + (LB or ", "UP + (LB or " },
		{ "(on Double Jump) hold Jump Button", "（ダブルジャンプ時）ホールドジャンプボタン" , "（二段跳时）按住跳跃按钮" },
		{ " ", " " , " " },
		{ " ", " " , " " },
		{ " ", " " , " " },
		{ " ", " " , " " },
		{ " ", " " , " " },
		{ " ", " " , " " },
		{ " ", " " , " " },
		{ " ", " " , " " },
		{ "Regenerate MP", "MP再生成する", "迅速再生MP" },
		{ "Absorbs HP from enemies death", "HPを敵の死から吸収する" , "从死亡敌人身上吸收HP" },
		{ "+100 HP", "+100 HP" , "+100 HP" },
		{ "100% HP", "100% HP", "100% HP" },
		{ "+100 MP", "+100 MP" , "+100 MP" }
	};

	private string[,] Quit_Text = new string[4, 3]
	{
		{ "Quit Game?", "ゲームを終了しますか？" , "确定要结束游戏吗？" },
		{ "Yes", "はい " , "是 " },
		{ "No", "いいえ ", "否 " },
		{ "Quit to the Main Menu. \nAny unsaved progress will be lost.", "メインメニューに終了します。\n保存されていない進行状況が失われます。" , "现在退出的话。\n未保存的进度将会丢失。" }
	};

	private string[,] Stat_Text = new string[4, 3]
	{
		{ "Strength\nATK  +2", "Strength\nATK  +2" , "力量解放\nATK  +2" },
		{ "Constitution\nHP  +5,  DEF +1", "Constitution\nHP  +5,  DEF +1" , "素质解放\nHP  +5,  DEF +1" },
		{ "Intelligence\nMP  +4", "Intelligence\nMP  +4" , "源能解放\nMP  +4" },
		{ "Luck\nCritical  ", "Luck\nクリティカル  " , "幸运强化\n会心一击  " }
	};

	private string[,] Title_Menu = new string[9, 3]
	{
		{ "NEW GAME", "はじめから" , "新的探索" },
		{ "CONTINUE", "つづきから" , "继续探索" },
		{ "GALLERY", "鑑賞モード" , "鉴赏模式" },
		{ "English", "日本語" , "中文" },
		{ "EXIT", "ゲーム終了" , "游戏结束" },
		{ " has been deleted.", " が削除されました。" , " 删掉了。" },
		{ "SaveData Load Failed!", "SaveDataの読み込みに失敗しました！" , "SaveData读取失败！" },
		{ "Would you like to save?", "保存しますか？" , "要保存吗？" },
		{ "Save Completed.", "保存完了。" , "保存完了。" }
	};

	private string[,] Map_Text = new string[4, 3]
	{
		{ "EVE Core", "EVE コア" , "EVE 核心区" },
		{ "Mother Brain", "マザーブレイン" , "主脑" },
		{ "Reactor", "リアクター" , "反应堆" },
		{ "Escape Now!!", "今すぐエスケープ！" , "迅速离开此地！" }
	};

	private string[,] Eve_Info = new string[2, 3]
	{
		{ "MEDICAL RESEARCH VESSEL", "医療調査船" , "医療調査船" },
		{ "UNITED SYSTEMS MILITARY", "ユナイテッドシステムズミリタリー" , "ユナイテッドシステムズミリタリー" }
	};

	private string[,] Mission_Briefing = new string[7, 3]
	{

		{ "MISSION BRIEFING", "ミッション・ブリーフィング", "任务目标" },
		{ "1. Copy the experiment data from EVE Core.", "1. EVEコアから実験データをコピーします。" , "1. 从EVE核心区复制实验数据。" },
		{ "2. Get the sample of MOTHER BRAIN.", "2. マザーブレインの生体標本を回収する。" , "2. 回收主脑生物标本。" },
		{ "3. Activate the emergency destruction system.", "3. 緊急破壊システムを有効にしてください。" , "3. 启用紧急破坏系统。" },
		{ "Data copy completed.", "數據拷貝完了", "数据拷贝成功" },
		{ "Sampling completed.", "採樣完了" , "采样成功" },
		{ "The self destruct system has been activated", "自毀系統已啟動" , "自毁系统已启动" }
	};

	private string[,] Info_Dialogue = new string[3, 3]
	{
		{ "something disgusting..", "気持ち悪い。" , "恶心的东西。" },
		{ "not enough mana!", "マナが不足！" , "玛娜不足！" },
		{ "   ", " ", " " }
	};

	public string CharName(int lang_num)
	{
		return Char_Name[lang_num];
	}

	public string QuitText(int num, int lang_num)
	{
		return Quit_Text[num, lang_num];
	}

	public string ItemName(int num, int lang_num)
	{
		return Item_Name[num - 1, lang_num];
	}

	public string ItemDesc(int num, int lang_num)
	{
		return Item_Desc[num - 1, lang_num];
	}

	public string StatText(int num, int lang_num)
	{
		return Stat_Text[num - 1, lang_num];
	}

	public string TitleMenu(int num, int lang_num)
	{
		return Title_Menu[num, lang_num];
	}

	public string EveInfo(int num, int lang_num)
	{
		return Eve_Info[num, lang_num];
	}

	public string MapText(int num, int lang_num)
	{
		return Map_Text[num, lang_num];
	}

	public string MissionBriefing(int num, int lang_num)
	{
		return Mission_Briefing[num, lang_num];
	}

	public string InfoDialogue(int num, int lang_num)
	{
		return Info_Dialogue[num, lang_num];
	}
}
