public class Console_Input : global::UnityEngine.MonoBehaviour
{
	public bool Enabled;

	private global::UnityEngine.Vector3 PosConsole;

	private global::UnityEngine.Vector3 PosInfo;

	private int input_Num;

	private string[] input_list;

	private global::UnityEngine.GameObject inputField;

	private GameManager GM;

	private Player_Control PC;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		PC = global::UnityEngine.GameObject.Find("Player").GetComponent<Player_Control>();
		input_list = new string[10];
		inputField = global::UnityEngine.GameObject.Find("InputField");
		PosConsole = global::UnityEngine.GameObject.Find("Console").GetComponent<global::UnityEngine.RectTransform>().localPosition;
		PosInfo = global::UnityEngine.GameObject.Find("Text_Info").GetComponent<global::UnityEngine.RectTransform>().localPosition;
		Console_Off();
	}

	private void Update()
	{
		if (GM.Paused)
		{
			if (Enabled)
			{
				Console_Off();
			}
			return;
		}
		if (Enabled)
		{
			Info_Text();
			Console_Text_List();
			if (global::UnityEngine.GameObject.Find("InputField").GetComponent<global::UnityEngine.UI.InputField>().isFocused)
			{
				if (!GM.onConsole)
				{
					GM.onConsole = true;
					global::UnityEngine.GameObject.Find("InputField").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(0.2f, 0.1f, 1f, 0.6f);
				}
			}
			else if (GM.onConsole)
			{
				GM.onConsole = false;
				global::UnityEngine.GameObject.Find("InputField").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, 0.25f);
			}
		}
		if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.F4))
		{
			if (Enabled)
			{
				Console_Off();
			}
			else
			{
				Console_ON();
			}
		}
	}

	private void Console_ON()
	{
		Enabled = true;
		global::UnityEngine.GameObject.Find("Console_BG").GetComponent<global::UnityEngine.SpriteRenderer>().enabled = true;
		global::UnityEngine.GameObject.Find("Console").GetComponent<global::UnityEngine.RectTransform>().localPosition = PosConsole;
		global::UnityEngine.GameObject.Find("Text_Info").GetComponent<global::UnityEngine.RectTransform>().localPosition = PosInfo;
		global::UnityEngine.GameObject.Find("InputField").GetComponent<global::UnityEngine.UI.InputField>().Select();
		global::UnityEngine.GameObject.Find("InputField").GetComponent<global::UnityEngine.UI.InputField>().ActivateInputField();
		global::UnityEngine.GameObject.Find("InputField").GetComponent<global::UnityEngine.UI.InputField>().text = string.Empty;
	}

	private void Console_Off()
	{
		Enabled = false;
		GM.onConsole = false;
		global::UnityEngine.GameObject.Find("Text_Info").GetComponent<global::UnityEngine.UI.Text>().text = string.Empty;
		global::UnityEngine.GameObject.Find("Console_BG").GetComponent<global::UnityEngine.SpriteRenderer>().enabled = false;
		global::UnityEngine.GameObject.Find("Console").GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(-3500f, 2500f, 0f);
		global::UnityEngine.GameObject.Find("Text_Info").GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(-3500f, 2500f, 0f);
	}

	private void Console_Text_List()
	{
		string text = string.Empty;
		if (input_Num > 0)
		{
			for (int num = input_Num - 1; num >= 0; num--)
			{
				text += input_list[num];
				if (num > 0)
				{
					text += "\n";
				}
			}
		}
		if (global::UnityEngine.GameObject.Find("Text_Console_List") != null)
		{
			global::UnityEngine.GameObject.Find("Text_Console_List").GetComponent<global::UnityEngine.UI.Text>().text = text;
		}
	}

	private void CharacterField(string inputFieldString)
	{
		if (!(inputFieldString != string.Empty))
		{
			return;
		}
		int num = 0;
		if (input_Num < 10)
		{
			input_Num++;
			num = input_Num - 1;
		}
		else
		{
			num = 9;
		}
		if (num > 0)
		{
			for (int num2 = num; num2 > 0; num2--)
			{
				input_list[num2] = input_list[num2 - 1];
			}
		}
		input_list[0] = inputFieldString;
		global::UnityEngine.GameObject.Find("InputField").GetComponent<global::UnityEngine.UI.InputField>().Select();
		global::UnityEngine.GameObject.Find("InputField").GetComponent<global::UnityEngine.UI.InputField>().ActivateInputField();
		global::UnityEngine.GameObject.Find("InputField").GetComponent<global::UnityEngine.UI.InputField>().text = string.Empty;
		Console_Text_List();
		StageManager component = global::UnityEngine.GameObject.Find("StageManager").GetComponent<StageManager>();
		int result = -1;
		if (int.TryParse(inputFieldString, out result))
		{
			global::UnityEngine.Debug.Log(result);
			if (result < component.Room.Length)
			{
				global::UnityEngine.GameObject.Find("Player").GetComponent<Player_Control>().Lock_GameLoad();
				component.Go_Room(result, 0, 0f, 0f, true);
			}
		}
		else
		{
			global::UnityEngine.Debug.Log("Can't Parse to int...");
		}
	}

	private void Info_Text()
	{
		if (global::UnityEngine.GameObject.Find("Player") != null)
		{
			string text = " State:  " + PC.State;
			string text2 = text;
			text = text2 + "\n HP:  " + GM.HP + " / " + GM.HP_Max.ToString();
			text2 = text;
			text = text2 + "\n MP:  " + GM.MP + " / " + GM.MP_Max;
			text = text + "\n\n Vel:  " + GM.Velcocity;
			text = text + "\nrgVel:  " + global::UnityEngine.GameObject.Find("Player").GetComponent<UnityEngine.Rigidbody2D>().velocity;
			text = text + "\n\n Jump:  " + PC.Jump_Num;
			text = text + "\n HJump:  " + PC.onHighJump;
			text = text + "\n Ground: " + PC.grounded_Now;
			text = text + "\n onDrop: " + PC.onJumpDrop;
			text = text + "\n\n Cam:  " + global::UnityEngine.GameObject.Find("Main Camera").GetComponent<UnityEngine.Camera>().orthographicSize.ToString("f2");
			global::UnityEngine.GameObject.Find("Text_Info").GetComponent<global::UnityEngine.UI.Text>().text = text;
		}
	}
}
