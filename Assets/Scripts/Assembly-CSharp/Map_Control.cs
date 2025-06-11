public class Map_Control : global::UnityEngine.MonoBehaviour
{
	private global::UnityEngine.Vector3 pos_Center;

	public global::UnityEngine.Vector2 pos_Cursor;

	private bool cursor_UP;

	private bool cursor_Down;

	private float inputX;

	private float prevX;

	private float inputY;

	private float prevY;

	private float Life_Timer;

	private float SelCursor_Timer;

	private float SelCursor_Size = 1f;

	private global::UnityEngine.GameObject map_Cursor;

	private global::UnityEngine.GameObject minimap_Cursor;

	private global::UnityEngine.GameObject map_CursorBox;

    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		map_Cursor = global::UnityEngine.GameObject.Find("MapPos_Cursor");
		minimap_Cursor = global::UnityEngine.GameObject.Find("MiniMap_Cursor");
		map_CursorBox = global::UnityEngine.GameObject.Find("MapPos_CursorBox");
		pos_Center = new global::UnityEngine.Vector3(0f, 0f, 0f);
		Reset_MiniMap();
		Reset_PosToCursor();
	}

	private void Reset_PosToCursor()
	{
		pos_Center = map_Cursor.GetComponent<global::UnityEngine.RectTransform>().localPosition;
		if (pos_Center.x < -1650f)
		{
			pos_Center.x = 1650f;
		}
		else if (pos_Center.x > 1650f)
		{
			pos_Center.x = -1650f;
		}
		else
		{
			pos_Center.x = 0f - pos_Center.x;
		}
		if (pos_Center.y < -630f)
		{
			pos_Center.y = 630f;
		}
		else if (pos_Center.y > 630f)
		{
			pos_Center.y = -630f;
		}
		else
		{
			pos_Center.y = 0f - pos_Center.y;
		}
	}

	private void Update()
	{
		if (GM.onEvent || GM.GameOver)
		{
			return;
		}
		if (GM.Paused)
		{
			if (GM.onMap)
			{
				Life_Timer += global::UnityEngine.Time.deltaTime;
				inputX = 0f;
				inputY = 0f;
				if (global::UnityEngine.Input.GetKey(global::UnityEngine.KeyCode.RightArrow))
				{
					inputX = 1f;
				}
				else if (global::UnityEngine.Input.GetKey(global::UnityEngine.KeyCode.LeftArrow))
				{
					inputX = -1f;
				}
				if (global::UnityEngine.Input.GetKey(global::UnityEngine.KeyCode.UpArrow))
				{
					inputY = 1f;
				}
				else if (global::UnityEngine.Input.GetKey(global::UnityEngine.KeyCode.DownArrow))
				{
					inputY = -1f;
				}
				if (global::UnityEngine.Input.GetAxis("L_X") != 0f)
				{
					inputX = global::UnityEngine.Input.GetAxis("L_X");
				}
				if (global::UnityEngine.Input.GetAxis("L_Y") != 0f)
				{
					inputY = global::UnityEngine.Input.GetAxis("L_Y");
				}
				if ((inputX > 0f && pos_Center.x > -1650f) || (inputX < 0f && pos_Center.x < 1650f))
				{
					pos_Center.x += 800f * (0f - inputX) * global::UnityEngine.Time.deltaTime;
				}
				if ((inputY > 0f && pos_Center.y > -630f) || (inputY < 0f && pos_Center.y < 630f))
				{
					pos_Center.y += 800f * (0f - inputY) * global::UnityEngine.Time.deltaTime;
				}
				GetComponent<global::UnityEngine.RectTransform>().localPosition = global::UnityEngine.Vector3.Lerp(GetComponent<global::UnityEngine.RectTransform>().localPosition, pos_Center, global::UnityEngine.Time.deltaTime * 3f);
				map_CursorBox.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, 0.6f + global::UnityEngine.Mathf.Sin(Life_Timer * 5f) * 0.4f);
				SelCursor_Timer += global::UnityEngine.Time.deltaTime;
				SelCursor_Size = 1f + (1f + global::UnityEngine.Mathf.Sin(SelCursor_Timer * 10f)) * 0.05f;
				global::UnityEngine.GameObject.Find("Ellen_MapCursor").GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(SelCursor_Size, SelCursor_Size, 1f);
				if (GM.EventState == 200)
				{
					global::UnityEngine.GameObject.Find("Mission_4").GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(-1050f, 170f + global::UnityEngine.Mathf.Sin(SelCursor_Timer * 8f) * 3f, 1f);
				}
				if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.Escape) || global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.X) || global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.Tab) || global::UnityEngine.Input.GetButtonDown("Start") || global::UnityEngine.Input.GetButtonDown("_B") || global::UnityEngine.Input.GetButtonDown("Back"))
				{
					GM.Game_Resume();
					GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(-2500f, 2500f, 0f);
					global::UnityEngine.GameObject.Find("MissionBriefing").SendMessage("Hide_BriefingPos");
				}
			}
		}
		else if (!GM.onMenu && !GM.onGatePass && !GM.onSave)
		{
			Life_Timer += global::UnityEngine.Time.deltaTime;
			minimap_Cursor.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(0.6f, 1f, 1f, 0.6f + global::UnityEngine.Mathf.Sin(Life_Timer * 5f) * 0.4f);
			if (global::UnityEngine.GameObject.Find("MapPos_" + pos_Cursor.x + "_" + pos_Cursor.y) != null)
			{
				map_Cursor.GetComponent<global::UnityEngine.RectTransform>().localPosition = global::UnityEngine.GameObject.Find("MapPos_" + pos_Cursor.x + "_" + pos_Cursor.y).GetComponent<global::UnityEngine.RectTransform>().localPosition;
			}
			if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.Tab) || global::UnityEngine.Input.GetButtonDown("Back"))
			{
				GM.onMap = true;
				cursor_UP = false;
				cursor_Down = false;
				GM.Game_Pause();
				GetComponent<global::UnityEngine.RectTransform>().localPosition = pos_Center;
				Reset_PosToCursor();
				global::UnityEngine.GameObject.Find("MissionBriefing").SendMessage("Set_BriefingPos_Map");
				global::UnityEngine.GameObject.Find("Menu").SendMessage("Sound_MapOn");
			}
		}
	}

	private void Reset_MiniMap()
	{
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				global::UnityEngine.GameObject.Find("MiniMap_" + i + "_" + j).GetComponent<global::UnityEngine.SpriteRenderer>().enabled = false;
				global::UnityEngine.GameObject.Find("MiniMapBorder_" + i + "_" + j).GetComponent<global::UnityEngine.SpriteRenderer>().enabled = false;
			}
		}
	}

	public void Change_MiniMap()
	{
		string text = "--";
		string text2 = text;
		string text3 = text;
		bool flag = false;
		bool flag2 = false;
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				text = "MapPos_" + (pos_Cursor.x - (float)(2 - i)) + "_" + (pos_Cursor.y + (float)(1 - j));
				text2 = "MapBorder_" + (pos_Cursor.x - (float)(2 - i)) + "_" + (pos_Cursor.y + (float)(1 - j));
				text3 = "MiniMapBorder_" + i + "_" + j;
				if (global::UnityEngine.GameObject.Find(text) != null && global::UnityEngine.GameObject.Find(text).GetComponent<global::UnityEngine.SpriteRenderer>().enabled)
				{
					global::UnityEngine.GameObject.Find(text3).GetComponent<global::UnityEngine.SpriteRenderer>().sprite = global::UnityEngine.GameObject.Find(text2).GetComponent<global::UnityEngine.SpriteRenderer>().sprite;
					global::UnityEngine.GameObject.Find(text3).GetComponent<global::UnityEngine.RectTransform>().localRotation = global::UnityEngine.GameObject.Find(text2).GetComponent<global::UnityEngine.RectTransform>().localRotation;
					global::UnityEngine.GameObject.Find(text3).GetComponent<global::UnityEngine.RectTransform>().localScale = global::UnityEngine.GameObject.Find(text2).GetComponent<global::UnityEngine.RectTransform>().localScale;
					global::UnityEngine.GameObject.Find("MiniMap_" + i + "_" + j).GetComponent<global::UnityEngine.SpriteRenderer>().enabled = true;
					global::UnityEngine.GameObject.Find(text3).GetComponent<global::UnityEngine.SpriteRenderer>().enabled = true;
					if (global::UnityEngine.GameObject.Find(text).GetComponent<global::UnityEngine.SpriteRenderer>().sortingOrder == 85)
					{
						global::UnityEngine.GameObject.Find("MiniMap_SaveFont").GetComponent<global::UnityEngine.RectTransform>().localPosition = global::UnityEngine.GameObject.Find("MiniMap_" + i + "_" + j).GetComponent<global::UnityEngine.RectTransform>().localPosition;
						flag = true;
					}
					if (global::UnityEngine.GameObject.Find(text).GetComponent<global::UnityEngine.SpriteRenderer>().sortingOrder == 86)
					{
						global::UnityEngine.GameObject.Find("MiniMap_TeleportFont").GetComponent<global::UnityEngine.RectTransform>().localPosition = global::UnityEngine.GameObject.Find("MiniMap_" + i + "_" + j).GetComponent<global::UnityEngine.RectTransform>().localPosition;
						flag2 = true;
					}
				}
				else
				{
					global::UnityEngine.GameObject.Find("MiniMap_" + i + "_" + j).GetComponent<global::UnityEngine.SpriteRenderer>().enabled = false;
					global::UnityEngine.GameObject.Find(text3).GetComponent<global::UnityEngine.SpriteRenderer>().enabled = false;
				}
			}
		}
		if (flag)
		{
			global::UnityEngine.GameObject.Find("MiniMap_SaveFont").GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(0.5f, 0.5f, 1f);
		}
		else
		{
			global::UnityEngine.GameObject.Find("MiniMap_SaveFont").GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(0f, 0f, 0f);
		}
		if (flag2)
		{
			global::UnityEngine.GameObject.Find("MiniMap_TeleportFont").GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(0.5f, 0.5f, 1f);
		}
		else
		{
			global::UnityEngine.GameObject.Find("MiniMap_TeleportFont").GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(0f, 0f, 0f);
		}
	}
}
