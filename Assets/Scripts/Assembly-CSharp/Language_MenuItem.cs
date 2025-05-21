public class Language_MenuItem : global::UnityEngine.MonoBehaviour
{
	private int Lang_Num;

	private string[] Char_Name = new string[2] { "Ellen", "エレン" };

	private string[,] Item_Name = new string[28, 2]
	{
		{ "Plasma Star", "プラズマスター" },
		{ "Thunder Blade", "サンダーブレード" },
		{ "Poison Skull", "毒の頭蓋骨" },
		{ "Frozen Blade", "凍った刃" },
		{ "Dark Shard", "ダークシャード" },
		{ "Energy Bolt", "エネルギーボルト" },
		{ "Power Bomb", "パワーボム" },
		{ "Poison Wave", "毒波" },
		{ "Shield", "シールド" },
		{ "Gravity Orb", "重力オーブ" },
		{ "Back Dash", "バックダッシュ" },
		{ "Double Jump", "ダブルジャンプ" },
		{ "Speed Up", "スピードアップ" },
		{ "High Jump", "高跳び" },
		{ "Screw Attack", "スクリューアタック" },
		{ "Yellow Card", "イエローカード" },
		{ "Blue Card", "ブルーカード" },
		{ "Green Card", "グリーンカード" },
		{ "Orange Card", "オレンジ色のカード" },
		{ "Violet Card", "バイオレットカード" },
		{ "Max HP +50", "Max HP +50" },
		{ "Max MP +40", "Max MP +40" },
		{ "ATK +20", "ATK +20" },
		{ "Mana Orb", "マナオーブ" },
		{ "Blood Skull", "血の頭蓋骨" },
		{ "Healing Potion", "ヒーリングポーション" },
		{ "Medi Kit", "メディキット" },
		{ "Mana Potion", "マナポーション" }
	};

	private string[,] Item_Desc = new string[28, 2]
	{
		{ " ", "  " },
		{ " ", "  " },
		{ " ", "  " },
		{ " ", "  " },
		{ " ", "  " },
		{ " ", "  " },
		{ " ", "  " },
		{ " ", "  " },
		{ " ", "  " },
		{ " ", "  " },
		{ "( press  LB )", "( press  LB )" },
		{ " ", "  " },
		{ "50% Faster Movement speed", "50％高速移動速度" },
		{ "UP + (LB or ", "UP + (LB or " },
		{ "(on Double Jump) hold Jump Button", "（ダブルジャンプ時）ホールドジャンプボタン" },
		{ " ", " " },
		{ " ", " " },
		{ " ", " " },
		{ " ", " " },
		{ " ", " " },
		{ " ", " " },
		{ " ", " " },
		{ " ", " " },
		{ "Regenerate MP", "MP再生成する" },
		{ "Absorbs HP from enemies death", "HPを敵の死から吸収する" },
		{ "+100 HP", "+100 HP" },
		{ "100% HP", "100% HP" },
		{ "+100 MP", "+100 MP" }
	};

	private string[,] Quit_Text = new string[4, 2]
	{
		{ "Quit Game?", "ゲームを終了しますか？" },
		{ "Yes", "はい " },
		{ "No", "いいえ " },
		{ "Quit to the Main Menu. \nAny unsaved progress will be lost.", "メインメニューに終了します。\n保存されていない進行状況が失われます。" }
	};

	private string[,] Stat_Text = new string[4, 2]
	{
		{ "Strength\nATK  +2", "Strength\nATK  +2" },
		{ "Constitution\nHP  +5,  DEF +1", "Constitution\nHP  +5,  DEF +1" },
		{ "Intelligence\nMP  +4", "Intelligence\nMP  +4" },
		{ "Luck\nCritical  ", "Luck\nクリティカル  " }
	};

	private string[,] Title_Menu = new string[9, 2]
	{
		{ "NEW GAME", "はじめから" },
		{ "CONTINUE", "つづきから" },
		{ "GALLERY", "鑑賞モード" },
		{ "English", "日本語" },
		{ "EXIT", "ゲーム終了" },
		{ " has been deleted.", " が削除されました。" },
		{ "SaveData Load Failed!", "SaveDataの読み込みに失敗しました！" },
		{ "Would you like to save?", "保存しますか？" },
		{ "Save Completed.", "保存完了。" }
	};

	private string[,] Map_Text = new string[4, 2]
	{
		{ "EVE Core", "EVE コア" },
		{ "Mother Brain", "マザーブレイン" },
		{ "Reactor", "リアクター" },
		{ "Escape Now!!", "今すぐエスケープ！" }
	};

	private string[,] Eve_Info = new string[2, 2]
	{
		{ "MEDICAL RESEARCH VESSEL", "医療調査船" },
		{ "UNITED SYSTEMS MILITARY", "ユナイテッドシステムズミリタリー" }
	};

	private string[,] Mission_Briefing = new string[7, 2]
	{
		{ "MISSION BRIEFING", "ミッション・ブリーフィング" },
		{ "1. Copy the experiment data from EVE Core.", "1. EVEコアから実験データをコピーします。" },
		{ "2. Get the sample of MOTHER BRAIN.", "2. マザーブレインの生体標本を回収する。" },
		{ "3. Activate the emergency destruction system.", "3. 緊急破壊システムを有効にしてください。" },
		{ "Data copy completed.", "マナが不足" },
		{ "Sampling completed.", "マナが不足" },
		{ "The self destruct system has been activated", "マナが不足" }
	};

	private string[,] Info_Dialogue = new string[3, 2]
	{
		{ "something disgusting..", "気持ち悪い。" },
		{ "not enough mana!", "マナが不足！" },
		{ "   ", " " }
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
