public class Info_MonIcon : global::UnityEngine.MonoBehaviour
{
	private int State = 1;

	private int Pre_State = 1;

	private float Life_Timer;

	private bool onIcon;

	private float distance;

	private float ratio = 1920f / (float)global::UnityEngine.Screen.width;

	private global::UnityEngine.Color color_ON = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_OFF = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	private global::UnityEngine.Vector3 pos_UI;

	private int Range_X;

	private int Range_Y;

	private float d_Top = 1f;

	private float d_Bot = -1f;

	private float d_Left = -1f;

	private float d_Right = 1f;

	private int margin = 35;

	private float pos_X;

	private float pos_Y;

	public int Type;

	public int Mon_Num;

	public global::UnityEngine.UI.Image Dir;

	public global::UnityEngine.UI.Image BG;

	public global::UnityEngine.UI.Image MonIcon;

	public global::UnityEngine.Transform MonCenter;

	private global::UnityEngine.Camera CAM;

    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		CAM = global::UnityEngine.GameObject.Find("Main Camera").GetComponent<global::UnityEngine.Camera>();
		ratio = 1920f / (float)global::UnityEngine.Screen.width;
		if (Type == 3)
		{
			margin = 41 + global::UnityEngine.Random.Range(0, 10);
			GetComponent<global::UnityEngine.RectTransform>().parent = global::UnityEngine.GameObject.Find("List_MonIcon_Boss").GetComponent<global::UnityEngine.RectTransform>();
		}
		else if (Type == 2)
		{
			margin = 38 + global::UnityEngine.Random.Range(0, 10);
			GetComponent<global::UnityEngine.RectTransform>().parent = global::UnityEngine.GameObject.Find("List_MonIcon_Elite").GetComponent<global::UnityEngine.RectTransform>();
		}
		else
		{
			margin = 35 + global::UnityEngine.Random.Range(0, 10);
			GetComponent<global::UnityEngine.RectTransform>().parent = global::UnityEngine.GameObject.Find("List_MonIcon").GetComponent<global::UnityEngine.RectTransform>();
		}
		GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(0f, (float)global::UnityEngine.Screen.height * -0.5f * ratio, 0f);
		GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(1f, 1f, 1f);
		Dir.color = color_OFF;
		BG.color = color_OFF;
		MonIcon.color = color_OFF;
	}

	private void Update()
	{
		if (GM.GameOver || GM.Paused)
		{
			Set_Color_OFF();
			return;
		}
		if (MonCenter == null)
		{
			Set_Color_OFF();
			if (Dir.color.a < 0.1f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			return;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
		distance = global::UnityEngine.Vector3.Distance(new global::UnityEngine.Vector3(CAM.transform.position.x, CAM.transform.position.y, 0f), MonCenter.position);
		if (distance < 40f)
		{
			if (MonCenter.position.x > CAM.transform.position.x + CAM.orthographicSize * (float)global::UnityEngine.Screen.width / (float)global::UnityEngine.Screen.height + d_Right)
			{
				Range_X = 1;
			}
			else if (MonCenter.position.x < CAM.transform.position.x - CAM.orthographicSize * (float)global::UnityEngine.Screen.width / (float)global::UnityEngine.Screen.height + d_Left)
			{
				Range_X = -1;
			}
			else
			{
				Range_X = 0;
			}
			if (MonCenter.position.y > CAM.transform.position.y + CAM.orthographicSize + d_Top)
			{
				Range_Y = 1;
			}
			else if (MonCenter.position.y < CAM.transform.position.y - CAM.orthographicSize + d_Bot)
			{
				Range_Y = -1;
			}
			else
			{
				Range_Y = 0;
			}
			if (Range_X != 0 || Range_Y != 0)
			{
				onIcon = true;
				Move_Icon();
			}
			else
			{
				onIcon = false;
			}
		}
		else
		{
			onIcon = false;
		}
		if (onIcon)
		{
			Set_Color_ON();
		}
		else
		{
			Set_Color_OFF();
		}
	}

	private void Move_Icon()
	{
		pos_UI = global::UnityEngine.GameObject.Find("Main Camera").GetComponent<global::UnityEngine.Camera>().WorldToScreenPoint(MonCenter.position);
		if (Range_X != 0)
		{
			pos_X = 960 * Range_X + margin * -Range_X;
		}
		else
		{
			pos_X = pos_UI.x * ratio - 960f;
		}
		if (Range_Y != 0)
		{
			pos_Y = 540 * Range_Y + margin * -Range_Y;
		}
		else
		{
			pos_Y = pos_UI.y * ratio - 540f;
		}
		if (pos_X > (float)(960 - margin))
		{
			pos_X = 960 - margin;
		}
		else if (pos_X < (float)(-(960 - margin)))
		{
			pos_X = -(960 - margin);
		}
		if (pos_Y > (float)(540 - margin))
		{
			pos_Y = 540 - margin;
		}
		else if (pos_Y < (float)(-(540 - margin)))
		{
			pos_Y = -(540 - margin);
		}
		if (Range_X < 0)
		{
			if (Type == 1)
			{
				Dir.GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(-24.5f, 0f);
			}
			else if (Type == 2)
			{
				Dir.GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(-32.5f, 0f);
			}
			else
			{
				Dir.GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(-40.5f, 0f);
			}
			Dir.GetComponent<global::UnityEngine.RectTransform>().localRotation = global::UnityEngine.Quaternion.Euler(0f, 0f, -90f);
			State = 4;
		}
		else if (Range_X > 0)
		{
			if (Type == 1)
			{
				Dir.GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(24.5f, 0f);
			}
			else if (Type == 2)
			{
				Dir.GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(32.5f, 0f);
			}
			else
			{
				Dir.GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(40.5f, 0f);
			}
			Dir.GetComponent<global::UnityEngine.RectTransform>().localRotation = global::UnityEngine.Quaternion.Euler(0f, 0f, 90f);
			State = 3;
		}
		else if (Range_Y > 0)
		{
			if (Type == 1)
			{
				Dir.GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(0f, 24.5f);
			}
			else if (Type == 2)
			{
				Dir.GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(0f, 32.5f);
			}
			else
			{
				Dir.GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(0f, 40.5f);
			}
			Dir.GetComponent<global::UnityEngine.RectTransform>().localRotation = global::UnityEngine.Quaternion.Euler(0f, 0f, 180f);
			State = 2;
		}
		else if (Range_Y < 0)
		{
			if (Type == 1)
			{
				Dir.GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(0f, -24.5f);
			}
			else if (Type == 2)
			{
				Dir.GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(0f, -32.5f);
			}
			else
			{
				Dir.GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(0f, -40.5f);
			}
			Dir.GetComponent<global::UnityEngine.RectTransform>().localRotation = global::UnityEngine.Quaternion.Euler(0f, 0f, 0f);
			State = 1;
		}
		if (State != Pre_State)
		{
			GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(pos_X, pos_Y, 0f);
		}
		GetComponent<global::UnityEngine.RectTransform>().localPosition = global::UnityEngine.Vector3.Lerp(GetComponent<global::UnityEngine.RectTransform>().localPosition, new global::UnityEngine.Vector3(pos_X, pos_Y, 0f), global::UnityEngine.Time.deltaTime * 5f);
		Pre_State = State;
	}

	public void Set_MonIcon(global::UnityEngine.Sprite spr)
	{
		MonIcon.sprite = spr;
	}

	public void Set_Dist()
	{
		switch (Mon_Num)
		{
		case 1:
			d_Top = 0.5f;
			d_Bot = -0.5f;
			d_Left = -0.8f;
			d_Right = 0.8f;
			break;
		case 2:
			d_Top = 0.5f;
			d_Bot = -2f;
			d_Left = -0.8f;
			d_Right = 0.8f;
			break;
		case 3:
			d_Top = 4f;
			d_Bot = -0.6f;
			d_Left = -0.7f;
			d_Right = 0.7f;
			break;
		case 4:
			d_Top = 4.2f;
			d_Bot = -0.7f;
			d_Left = -1f;
			d_Right = 1f;
			break;
		case 5:
			d_Top = 1f;
			d_Bot = -1f;
			d_Left = -1.8f;
			d_Right = 1.8f;
			break;
		case 6:
			d_Top = 0.9f;
			d_Bot = -1f;
			d_Left = -2f;
			d_Right = 2f;
			break;
		case 7:
			d_Top = 2.1f;
			d_Bot = -2.3f;
			d_Left = -1f;
			d_Right = 1f;
			break;
		case 8:
			d_Top = 2.283f;
			d_Bot = -2.12f;
			d_Left = -1f;
			d_Right = 1f;
			break;
		case 9:
			d_Top = 2.605f;
			d_Bot = -1.25f;
			d_Left = -2.3f;
			d_Right = 2.3f;
			break;
		case 10:
			d_Top = 2.578f;
			d_Bot = -2.3f;
			d_Left = -0.7f;
			d_Right = 0.7f;
			break;
		case 11:
			d_Top = 3.487f;
			d_Bot = -3.2f;
			d_Left = -2.3f;
			d_Right = 2.3f;
			break;
		case 12:
			d_Top = 2.8f;
			d_Bot = -2.8f;
			d_Left = -2.8f;
			d_Right = 2.8f;
			break;
		case 13:
			d_Top = 2.681f;
			d_Bot = -2.45f;
			d_Left = -1.1f;
			d_Right = 1.1f;
			break;
		case 14:
			d_Top = 1.993f;
			d_Bot = -2.4f;
			d_Left = -2f;
			d_Right = 2f;
			break;
		case 15:
			d_Top = 4.65f;
			d_Bot = 2f;
			d_Left = -0.6f;
			d_Right = 0.6f;
			break;
		case 16:
			d_Top = 0.3f;
			d_Bot = -0.2f;
			d_Left = -2.5f;
			d_Right = 1f;
			break;
		case 17:
			d_Top = 0.75f;
			d_Bot = -0.75f;
			d_Left = -1f;
			d_Right = 1f;
			break;
		case 18:
			d_Top = 3.495f;
			d_Bot = -3.84f;
			d_Left = -2.4f;
			d_Right = 2.4f;
			break;
		case 19:
			d_Top = 2.8f;
			d_Bot = -2.8f;
			d_Left = -2.8f;
			d_Right = 2.8f;
			break;
		case 21:
			d_Top = -0.205f;
			d_Bot = 0f;
			d_Left = -0.75f;
			d_Right = 0.75f;
			break;
		case 22:
			d_Top = 1f;
			d_Bot = -0.5f;
			d_Left = -1.1f;
			d_Right = 1.1f;
			break;
		case 24:
			d_Top = 2.105f;
			d_Bot = -2.2f;
			d_Left = -3f;
			d_Right = 3f;
			break;
		case 30:
			d_Top = 3.2f;
			d_Bot = -1.6f;
			d_Left = -2f;
			d_Right = 2f;
			break;
		case 31:
			d_Top = 3.48f;
			d_Bot = -3.4f;
			d_Left = -1.8f;
			d_Right = 1.8f;
			break;
		case 32:
			d_Top = 3.98f;
			d_Bot = -3.8f;
			d_Left = -2.2f;
			d_Right = 2.2f;
			break;
		case 33:
			d_Top = 3.48f;
			d_Bot = -3.4f;
			d_Left = -1.8f;
			d_Right = 1.8f;
			break;
		case 34:
			d_Top = 3.98f;
			d_Bot = -3.8f;
			d_Left = -2.2f;
			d_Right = 2.2f;
			break;
		case 35:
			d_Top = 3.98f;
			d_Bot = -3.3f;
			d_Left = -1.2f;
			d_Right = 1.2f;
			break;
		case 36:
			d_Top = 3.98f;
			d_Bot = -3.3f;
			d_Left = -1.2f;
			d_Right = 1.2f;
			break;
		case 37:
			d_Top = 1.7f;
			d_Bot = -2f;
			d_Left = -1.1f;
			d_Right = 1.1f;
			break;
		case 38:
			d_Top = 4.5f;
			d_Bot = -4.2f;
			d_Left = -3f;
			d_Right = 3f;
			break;
		case 39:
			d_Top = 4.452f;
			d_Bot = -4f;
			d_Left = -3.3f;
			d_Right = 3.3f;
			break;
		case 40:
			d_Top = 2.6f;
			d_Bot = -3.5f;
			d_Left = -4.1f;
			d_Right = 4.1f;
			break;
		case 41:
			d_Top = 7.45f;
			d_Bot = -3.5f;
			d_Left = -5.4f;
			d_Right = 5.4f;
			break;
		case 42:
			d_Top = 8.35f;
			d_Bot = -8f;
			d_Left = -6.9f;
			d_Right = 6.9f;
			break;
		case 51:
			d_Top = 2.8f;
			d_Bot = -3.3f;
			d_Left = -3.8f;
			d_Right = 3.8f;
			break;
		case 52:
			d_Top = 3.3f;
			d_Bot = -4.8f;
			d_Left = -3f;
			d_Right = 3f;
			break;
		case 53:
			d_Top = 1.6f;
			d_Bot = -1.6f;
			d_Left = -1.6f;
			d_Right = 1.6f;
			break;
		case 54:
			d_Top = -0.5f;
			d_Bot = -12.7f;
			d_Left = -7f;
			d_Right = 7f;
			break;
		case 55:
			d_Top = 12.54f;
			d_Bot = -8f;
			d_Left = -3f;
			d_Right = 3f;
			break;
		default:
			d_Top = 2f;
			d_Bot = -2f;
			d_Left = -2f;
			d_Right = 2f;
			break;
		}
	}

	private void Set_Color_OFF()
	{
		Pre_State = 0;
		Dir.color = global::UnityEngine.Color.Lerp(Dir.color, color_OFF, global::UnityEngine.Time.deltaTime * 10f);
		BG.color = Dir.color;
		MonIcon.color = Dir.color;
	}

	private void Set_Color_ON()
	{
		Dir.color = global::UnityEngine.Color.Lerp(Dir.color, color_ON, global::UnityEngine.Time.deltaTime * 10f);
		BG.color = Dir.color;
		MonIcon.color = Dir.color;
	}
}
