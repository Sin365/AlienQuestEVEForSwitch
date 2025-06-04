public class Custom_Key : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.KeyCode Up = global::UnityEngine.KeyCode.UpArrow;

	public global::UnityEngine.KeyCode Down = global::UnityEngine.KeyCode.DownArrow;

	public global::UnityEngine.KeyCode Left = global::UnityEngine.KeyCode.LeftArrow;

	public global::UnityEngine.KeyCode Right = global::UnityEngine.KeyCode.RightArrow;

	public global::UnityEngine.KeyCode Jump = global::UnityEngine.KeyCode.Z;

	public global::UnityEngine.KeyCode Attack = global::UnityEngine.KeyCode.X;

	public global::UnityEngine.KeyCode Skill = global::UnityEngine.KeyCode.C;

	public global::UnityEngine.KeyCode Spin = global::UnityEngine.KeyCode.Space;

	public global::UnityEngine.KeyCode LB = global::UnityEngine.KeyCode.A;

	public global::UnityEngine.KeyCode RB = global::UnityEngine.KeyCode.S;

	public global::UnityEngine.KeyCode SkillSwap = global::UnityEngine.KeyCode.V;

	public global::UnityEngine.KeyCode Map = global::UnityEngine.KeyCode.Tab;

	public global::UnityEngine.KeyCode ZoomReset = global::UnityEngine.KeyCode.Home;

	public global::UnityEngine.KeyCode ZoomIn = global::UnityEngine.KeyCode.PageUp;

	public global::UnityEngine.KeyCode ZoomOut = global::UnityEngine.KeyCode.PageDown;

	private int[] User_Key_Num = new int[11]
	{
		59, 60, 61, 62, 41, 42, 43, 54, 29, 30,
		44
	};

	private global::UnityEngine.KeyCode[] User_Key = new global::UnityEngine.KeyCode[11]
	{
		global::UnityEngine.KeyCode.UpArrow,
		global::UnityEngine.KeyCode.DownArrow,
		global::UnityEngine.KeyCode.LeftArrow,
		global::UnityEngine.KeyCode.RightArrow,
		global::UnityEngine.KeyCode.Z,
		global::UnityEngine.KeyCode.X,
		global::UnityEngine.KeyCode.C,
		global::UnityEngine.KeyCode.Space,
		global::UnityEngine.KeyCode.A,
		global::UnityEngine.KeyCode.S,
		global::UnityEngine.KeyCode.V
	};

	private global::UnityEngine.KeyCode[] Key = new global::UnityEngine.KeyCode[63]
	{
		global::UnityEngine.KeyCode.BackQuote,
		global::UnityEngine.KeyCode.None,
		global::UnityEngine.KeyCode.None,
		global::UnityEngine.KeyCode.Alpha3,
		global::UnityEngine.KeyCode.Alpha4,
		global::UnityEngine.KeyCode.Alpha5,
		global::UnityEngine.KeyCode.Alpha6,
		global::UnityEngine.KeyCode.Alpha7,
		global::UnityEngine.KeyCode.Alpha8,
		global::UnityEngine.KeyCode.Alpha9,
		global::UnityEngine.KeyCode.Alpha0,
		global::UnityEngine.KeyCode.Minus,
		global::UnityEngine.KeyCode.Equals,
		global::UnityEngine.KeyCode.Backslash,
		global::UnityEngine.KeyCode.Backspace,
		global::UnityEngine.KeyCode.Q,
		global::UnityEngine.KeyCode.W,
		global::UnityEngine.KeyCode.E,
		global::UnityEngine.KeyCode.R,
		global::UnityEngine.KeyCode.T,
		global::UnityEngine.KeyCode.Y,
		global::UnityEngine.KeyCode.U,
		global::UnityEngine.KeyCode.I,
		global::UnityEngine.KeyCode.O,
		global::UnityEngine.KeyCode.P,
		global::UnityEngine.KeyCode.LeftBracket,
		global::UnityEngine.KeyCode.RightBracket,
		global::UnityEngine.KeyCode.Return,
		global::UnityEngine.KeyCode.CapsLock,
		global::UnityEngine.KeyCode.A,
		global::UnityEngine.KeyCode.S,
		global::UnityEngine.KeyCode.D,
		global::UnityEngine.KeyCode.F,
		global::UnityEngine.KeyCode.G,
		global::UnityEngine.KeyCode.H,
		global::UnityEngine.KeyCode.J,
		global::UnityEngine.KeyCode.K,
		global::UnityEngine.KeyCode.L,
		global::UnityEngine.KeyCode.Semicolon,
		global::UnityEngine.KeyCode.Quote,
		global::UnityEngine.KeyCode.LeftShift,
		global::UnityEngine.KeyCode.Z,
		global::UnityEngine.KeyCode.X,
		global::UnityEngine.KeyCode.C,
		global::UnityEngine.KeyCode.V,
		global::UnityEngine.KeyCode.B,
		global::UnityEngine.KeyCode.N,
		global::UnityEngine.KeyCode.M,
		global::UnityEngine.KeyCode.Comma,
		global::UnityEngine.KeyCode.Period,
		global::UnityEngine.KeyCode.Slash,
		global::UnityEngine.KeyCode.RightShift,
		global::UnityEngine.KeyCode.LeftControl,
		global::UnityEngine.KeyCode.LeftAlt,
		global::UnityEngine.KeyCode.Space,
		global::UnityEngine.KeyCode.RightAlt,
		global::UnityEngine.KeyCode.RightControl,
		global::UnityEngine.KeyCode.Insert,
		global::UnityEngine.KeyCode.Delete,
		global::UnityEngine.KeyCode.UpArrow,
		global::UnityEngine.KeyCode.DownArrow,
		global::UnityEngine.KeyCode.LeftArrow,
		global::UnityEngine.KeyCode.RightArrow
	};

	private int[] iskeyAssigned = new int[11];

	private void Start()
	{
		Load_UserSetting();
	}

	public bool Check_Input(int num)
	{
		bool flag = false;
		int num2 = 0;
		string text = string.Empty;
		for (int i = 0; i < Key.Length; i++)
		{
			if (flag)
			{
				break;
			}
			if (global::UnityEngine.Input.GetKeyDown(Key[i]))
			{
				flag = true;
				Check_Overlap(Key[i]);
				User_Key[num - 1] = Key[i];
				User_Key_Num[num - 1] = i;
				text = KeyToString(i);
			}
		}
		if (flag)
		{
			global::UnityEngine.GameObject.Find("KeyAssigned_" + num).GetComponent<global::UnityEngine.UI.Text>().text = text;
			switch (num)
			{
			case 9:
			{
				global::UnityEngine.GameObject.Find("UI_Name_LBKey").GetComponent<global::UnityEngine.UI.Text>().text = text;
				float x2 = 36f + (float)(text.Length - 1) * 10f;
				global::UnityEngine.GameObject.Find("UI_Guide_LB").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(x2, 36f);
				break;
			}
			case 10:
			{
				global::UnityEngine.GameObject.Find("UI_Name_RBKey").GetComponent<global::UnityEngine.UI.Text>().text = text;
				float x = 36f + (float)(text.Length - 1) * 10f;
				global::UnityEngine.GameObject.Find("UI_Guide_RB").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(x, 36f);
				break;
			}
			}
			Update_Key();
			return true;
		}
		return false;
	}

	private void Check_Overlap(global::UnityEngine.KeyCode test)
	{
		for (int i = 0; i < 11; i++)
		{
			if (User_Key[i] == test)
			{
				global::UnityEngine.GameObject.Find("KeyAssigned_" + (i + 1)).GetComponent<global::UnityEngine.UI.Text>().text = string.Empty;
				User_Key[i] = global::UnityEngine.KeyCode.None;
				User_Key_Num[i] = 0;
				if (i == 8)
				{
					global::UnityEngine.GameObject.Find("UI_Name_LBKey").GetComponent<global::UnityEngine.UI.Text>().text = string.Empty;
					global::UnityEngine.GameObject.Find("UI_Guide_LB").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(36f, 36f);
				}
				if (i == 9)
				{
					global::UnityEngine.GameObject.Find("UI_Name_RBKey").GetComponent<global::UnityEngine.UI.Text>().text = string.Empty;
					global::UnityEngine.GameObject.Find("UI_Guide_RB").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(36f, 36f);
				}
			}
		}
	}

	private string KeyToString(int num)
	{
		if (num == 0)
		{
			return "`";
		}
		if (num < 10)
		{
			return num.ToString();
		}
		switch (num)
		{
		case 10:
			return "0";
		case 11:
			return "-";
		case 12:
			return "=";
		case 25:
			return "[";
		case 26:
			return "]";
		case 27:
			return "Enter";
		case 38:
			return ";";
		case 39:
			return "'";
		case 48:
			return ",";
		case 49:
			return ".";
		case 50:
			return "/";
		case 59:
			return "Up";
		case 60:
			return "Down";
		case 61:
			return "Left";
		case 62:
			return "Right";
		default:
			return Key[num].ToString();
		}
	}

	public string KeyToString_Out(int num)
	{
		return KeyToString(User_Key_Num[num]);
	}

	private bool Check_Changed()
	{
		bool flag = false;
		for (int i = 0; i < 11; i++)
		{
			if (flag)
			{
				break;
			}
			if (iskeyAssigned[i] > 1)
			{
				flag = true;
			}
		}
		if (flag)
		{
			flag = User_Key[0] != global::UnityEngine.KeyCode.UpArrow || User_Key[1] != global::UnityEngine.KeyCode.DownArrow || User_Key[2] != global::UnityEngine.KeyCode.LeftArrow || User_Key[3] != global::UnityEngine.KeyCode.RightArrow || User_Key[4] != global::UnityEngine.KeyCode.Z || User_Key[5] != global::UnityEngine.KeyCode.X || User_Key[6] != global::UnityEngine.KeyCode.C || User_Key[7] != global::UnityEngine.KeyCode.Space || User_Key[8] != global::UnityEngine.KeyCode.A || User_Key[9] != global::UnityEngine.KeyCode.S || ((User_Key[10] != global::UnityEngine.KeyCode.V) ? true : false);
		}
		return flag;
	}

	private void Update_Key()
	{
		Up = User_Key[0];
		Down = User_Key[1];
		Left = User_Key[2];
		Right = User_Key[3];
		Jump = User_Key[4];
		Attack = User_Key[5];
		Skill = User_Key[6];
		Spin = User_Key[7];
		LB = User_Key[8];
		RB = User_Key[9];
		SkillSwap = User_Key[10];
		Save_UserSetting();
	}

	private void Save_UserSetting()
	{
		AxiPlayerPrefs.SetInt("KeyConfig_Saved", 1);
		for (int i = 0; i < 11; i++)
		{
			if (User_Key[i] != global::UnityEngine.KeyCode.None)
			{
				AxiPlayerPrefs.SetInt("KeyConfig_" + i, User_Key_Num[i]);
			}
			else
			{
				AxiPlayerPrefs.SetInt("KeyConfig_" + i, -1);
			}
		}
	}

	private void Load_UserSetting()
	{
		if (AxiPlayerPrefs.GetInt("KeyConfig_Saved") <= 0)
		{
			return;
		}
		for (int i = 0; i < 11; i++)
		{
			if (AxiPlayerPrefs.GetInt("KeyConfig_" + i) < 0)
			{
				User_Key[i] = global::UnityEngine.KeyCode.None;
				User_Key_Num[i] = 0;
			}
			else
			{
				User_Key_Num[i] = AxiPlayerPrefs.GetInt("KeyConfig_" + i);
				User_Key[i] = Key[User_Key_Num[i]];
			}
		}
		Up = User_Key[0];
		Down = User_Key[1];
		Left = User_Key[2];
		Right = User_Key[3];
		Jump = User_Key[4];
		Attack = User_Key[5];
		Skill = User_Key[6];
		Spin = User_Key[7];
		LB = User_Key[8];
		RB = User_Key[9];
		SkillSwap = User_Key[10];
		Show_Text_KeyConfig();
		bool flag = false;
		if (User_Key[0] == global::UnityEngine.KeyCode.UpArrow && User_Key[1] == global::UnityEngine.KeyCode.DownArrow && User_Key[2] == global::UnityEngine.KeyCode.LeftArrow && User_Key[3] == global::UnityEngine.KeyCode.RightArrow && User_Key[4] == global::UnityEngine.KeyCode.Z && User_Key[5] == global::UnityEngine.KeyCode.X && User_Key[6] == global::UnityEngine.KeyCode.C && User_Key[7] == global::UnityEngine.KeyCode.Space && User_Key[8] == global::UnityEngine.KeyCode.A && User_Key[9] == global::UnityEngine.KeyCode.S && (User_Key[10] == global::UnityEngine.KeyCode.V || 1 == 0))
		{
			AxiPlayerPrefs.SetInt("KeyConfig_Saved", 0);
			global::UnityEngine.Debug.Log("Default_Same");
		}
	}

	private void Show_Text_KeyConfig()
	{
		for (int i = 0; i < 11; i++)
		{
			if (User_Key[i] != global::UnityEngine.KeyCode.None)
			{
				global::UnityEngine.GameObject.Find("KeyAssigned_" + (i + 1)).GetComponent<global::UnityEngine.UI.Text>().text = KeyToString(User_Key_Num[i]);
			}
			else
			{
				global::UnityEngine.GameObject.Find("KeyAssigned_" + (i + 1)).GetComponent<global::UnityEngine.UI.Text>().text = string.Empty;
			}
		}
		Size_LR_Button();
	}

	private void Size_LR_Button()
	{
		if (User_Key[8] != global::UnityEngine.KeyCode.None)
		{
			string text = KeyToString(User_Key_Num[8]);
			global::UnityEngine.GameObject.Find("UI_Name_LBKey").GetComponent<global::UnityEngine.UI.Text>().text = text;
			float x = 36f + (float)(text.Length - 1) * 10f;
			global::UnityEngine.GameObject.Find("UI_Guide_LB").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(x, 36f);
		}
		else
		{
			global::UnityEngine.GameObject.Find("UI_Name_LBKey").GetComponent<global::UnityEngine.UI.Text>().text = string.Empty;
			global::UnityEngine.GameObject.Find("UI_Guide_LB").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(36f, 36f);
		}
		if (User_Key[9] != global::UnityEngine.KeyCode.None)
		{
			string text2 = KeyToString(User_Key_Num[9]);
			global::UnityEngine.GameObject.Find("UI_Name_RBKey").GetComponent<global::UnityEngine.UI.Text>().text = text2;
			float x2 = 36f + (float)(text2.Length - 1) * 10f;
			global::UnityEngine.GameObject.Find("UI_Guide_RB").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(x2, 36f);
		}
		else
		{
			global::UnityEngine.GameObject.Find("UI_Name_RBKey").GetComponent<global::UnityEngine.UI.Text>().text = string.Empty;
			global::UnityEngine.GameObject.Find("UI_Guide_RB").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(36f, 36f);
		}
	}

	public void Reset_KeyConfig()
	{
		Up = (User_Key[0] = global::UnityEngine.KeyCode.UpArrow);
		Down = (User_Key[1] = global::UnityEngine.KeyCode.DownArrow);
		Left = (User_Key[2] = global::UnityEngine.KeyCode.LeftArrow);
		Right = (User_Key[3] = global::UnityEngine.KeyCode.RightArrow);
		Jump = (User_Key[4] = global::UnityEngine.KeyCode.Z);
		Attack = (User_Key[5] = global::UnityEngine.KeyCode.X);
		Skill = (User_Key[6] = global::UnityEngine.KeyCode.C);
		Spin = (User_Key[7] = global::UnityEngine.KeyCode.Space);
		LB = (User_Key[8] = global::UnityEngine.KeyCode.A);
		RB = (User_Key[9] = global::UnityEngine.KeyCode.S);
		SkillSwap = (User_Key[10] = global::UnityEngine.KeyCode.V);
		User_Key_Num[0] = 59;
		User_Key_Num[1] = 60;
		User_Key_Num[2] = 61;
		User_Key_Num[3] = 62;
		User_Key_Num[4] = 41;
		User_Key_Num[5] = 42;
		User_Key_Num[6] = 43;
		User_Key_Num[7] = 54;
		User_Key_Num[8] = 29;
		User_Key_Num[9] = 30;
		User_Key_Num[10] = 44;
		global::UnityEngine.GameObject.Find("KeyAssigned_1").GetComponent<global::UnityEngine.UI.Text>().text = "Up";
		global::UnityEngine.GameObject.Find("KeyAssigned_2").GetComponent<global::UnityEngine.UI.Text>().text = "Down";
		global::UnityEngine.GameObject.Find("KeyAssigned_3").GetComponent<global::UnityEngine.UI.Text>().text = "Left";
		global::UnityEngine.GameObject.Find("KeyAssigned_4").GetComponent<global::UnityEngine.UI.Text>().text = "Right";
		for (int i = 4; i < 11; i++)
		{
			global::UnityEngine.GameObject.Find("KeyAssigned_" + (i + 1)).GetComponent<global::UnityEngine.UI.Text>().text = User_Key[i].ToString();
		}
		AxiPlayerPrefs.SetInt("KeyConfig_Saved", 0);
		global::UnityEngine.GameObject.Find("UI_Name_LBKey").GetComponent<global::UnityEngine.UI.Text>().text = "A";
		global::UnityEngine.GameObject.Find("UI_Name_RBKey").GetComponent<global::UnityEngine.UI.Text>().text = "S";
		global::UnityEngine.GameObject.Find("UI_Guide_LB").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(36f, 36f);
		global::UnityEngine.GameObject.Find("UI_Guide_RB").GetComponent<global::UnityEngine.RectTransform>().sizeDelta = new global::UnityEngine.Vector2(36f, 36f);
	}
}
