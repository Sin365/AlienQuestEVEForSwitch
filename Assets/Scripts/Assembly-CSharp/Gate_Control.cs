public class Gate_Control : global::UnityEngine.MonoBehaviour
{
	public int Card_Num;

	public int targetRoom_Num;

	public int targetPos_Num;

	private bool onPass;

	private float Pass_Timer;

	private bool isLocked;

	private bool isOpened;

	private float Life_Timer;

	private float Sound_Timer;

	private global::UnityEngine.Vector3 pos_Up_Open = new global::UnityEngine.Vector3(-1.83f, 5f, 0f);

	private global::UnityEngine.Vector3 pos_Down_Open = new global::UnityEngine.Vector3(-1.82f, -5f, 0f);

	private global::UnityEngine.Vector3 pos_Up_Close = new global::UnityEngine.Vector3(-1.83f, 1.362f, 0f);

	private global::UnityEngine.Vector3 pos_Down_Close = new global::UnityEngine.Vector3(-1.83f, -1.382f, 0f);

	private global::UnityEngine.Vector3 pos_Glow_Top_Open = new global::UnityEngine.Vector3(-1f, 2.925f, 0f);

	private global::UnityEngine.Vector3 pos_Glow_Bot_Open = new global::UnityEngine.Vector3(-1f, -2.925f, 0f);

	private global::UnityEngine.Vector3 pos_Glow_Top_Close = new global::UnityEngine.Vector3(-1f, 0.09f, 0f);

	private global::UnityEngine.Vector3 pos_Glow_Bot_Close = new global::UnityEngine.Vector3(-1f, 0.04f, 0f);

	public global::UnityEngine.SpriteRenderer Gate_Up;

	public global::UnityEngine.SpriteRenderer Gate_Down;

	public global::UnityEngine.SpriteRenderer Glow_Top;

	public global::UnityEngine.SpriteRenderer Glow_Bot;

	private global::UnityEngine.Color color_Glow_Open = new global::UnityEngine.Color(0f, 0.2f, 1f, 0.5f);

	private global::UnityEngine.Color color_Glow_Close = new global::UnityEngine.Color(0f, 0f, 1f, 0f);

	public global::UnityEngine.SpriteRenderer Body_L;

	public global::UnityEngine.SpriteRenderer Body_R;

	public global::UnityEngine.SpriteRenderer Body_Top_L;

	public global::UnityEngine.SpriteRenderer Body_Top_R;

	public global::UnityEngine.SpriteRenderer Body_Bot_L;

	public global::UnityEngine.SpriteRenderer Body_Bot_R;

	public global::UnityEngine.Transform pos_Top;

	public global::UnityEngine.Transform pos_Bot;

	public global::UnityEngine.BoxCollider2D ColBox;

	public global::UnityEngine.GameObject Info_Card;

	public global::UnityEngine.GameObject Keep_Monster;

	private bool monLocked;

	public global::UnityEngine.GameObject Boss_Lock;

	private bool bossLocked;

	private bool onBossGateSound;

	private float BossGateSound_Timer;

	private GameManager GM;

	private StageManager SM;

	private global::UnityEngine.GameObject Player;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		SM = global::UnityEngine.GameObject.Find("StageManager").GetComponent<StageManager>();
		Player = global::UnityEngine.GameObject.Find("Player");
		if (Keep_Monster != null)
		{
			monLocked = true;
			isLocked = true;
			ColBox.enabled = true;
		}
		if (Boss_Lock != null)
		{
			bossLocked = true;
			isLocked = true;
			ColBox.enabled = true;
			if (global::UnityEngine.Vector3.Distance(pos_Bot.position, Player.transform.position) < 8f)
			{
				Gate_Up.transform.localPosition = pos_Up_Open;
				Gate_Down.transform.localPosition = pos_Down_Open;
				Glow_Top.transform.localPosition = pos_Glow_Top_Open;
				Glow_Bot.transform.localPosition = pos_Glow_Bot_Open;
				Glow_Top.transform.localScale = new global::UnityEngine.Vector3(-0.38f, -0.5f, 1f);
				Glow_Bot.transform.localScale = new global::UnityEngine.Vector3(-0.38f, 0.5f, 1f);
				global::UnityEngine.SpriteRenderer glow_Top = Glow_Top;
				global::UnityEngine.Color color = color_Glow_Open;
				Glow_Bot.color = color;
				glow_Top.color = color;
				BossGateSound_Timer = 0.1f;
			}
		}
		if (Card_Num > 0 && (Card_Num != 1 || !GM.onCard_1) && (Card_Num != 2 || !GM.onCard_2) && (Card_Num != 3 || !GM.onCard_3) && (Card_Num != 4 || !GM.onCard_4) && (Card_Num != 5 || !GM.onCard_5))
		{
			ColBox.enabled = true;
			isLocked = true;
		}
		Glow_Top.transform.localPosition = pos_Glow_Top_Close;
		Glow_Bot.transform.localPosition = pos_Glow_Bot_Close;
		Glow_Top.transform.localScale = new global::UnityEngine.Vector3(-0.5f, -0.1f, 1f);
		Glow_Bot.transform.localScale = new global::UnityEngine.Vector3(-0.7f, 0.1f, 1f);
		Glow_Top.color = color_Glow_Close;
		Glow_Bot.color = color_Glow_Close;
		Set_Start_SortingLayer();
	}

	private void Check_Card()
	{
		if (Card_Num == 0)
		{
			ColBox.enabled = false;
			isLocked = false;
		}
		else if (Card_Num == 1 && GM.onCard_1)
		{
			ColBox.enabled = false;
			isLocked = false;
		}
		else if (Card_Num == 2 && GM.onCard_2)
		{
			ColBox.enabled = false;
			isLocked = false;
		}
		else if (Card_Num == 3 && GM.onCard_3)
		{
			ColBox.enabled = false;
			isLocked = false;
		}
		else if (Card_Num == 4 && GM.onCard_4)
		{
			ColBox.enabled = false;
			isLocked = false;
		}
		else if (Card_Num == 5 && GM.onCard_5)
		{
			ColBox.enabled = false;
			isLocked = false;
		}
		else
		{
			Info_Card.GetComponent<Info_Gate>().on_Info = true;
		}
	}

	private void Set_Open()
	{
		Gate_Up.transform.localPosition = pos_Up_Open;
		Gate_Down.transform.localPosition = pos_Down_Open;
		Glow_Top.transform.localPosition = pos_Glow_Top_Open;
		Glow_Bot.transform.localPosition = pos_Glow_Bot_Open;
		Glow_Top.transform.localScale = new global::UnityEngine.Vector3(-0.38f, -0.5f, 1f);
		Glow_Bot.transform.localScale = new global::UnityEngine.Vector3(-0.38f, 0.5f, 1f);
		Glow_Top.color = color_Glow_Open;
		Glow_Bot.color = color_Glow_Open;
	}

	private void Set_Start_SortingLayer()
	{
		global::UnityEngine.SpriteRenderer glow_Top = Glow_Top;
		int sortingLayerID = 3;
		Glow_Bot.sortingLayerID = sortingLayerID;
		glow_Top.sortingLayerID = sortingLayerID;
		global::UnityEngine.SpriteRenderer glow_Top2 = Glow_Top;
		sortingLayerID = 99;
		Glow_Bot.sortingOrder = sortingLayerID;
		glow_Top2.sortingOrder = sortingLayerID;
		global::UnityEngine.SpriteRenderer body_R = Body_R;
		sortingLayerID = 3;
		Body_L.sortingLayerID = sortingLayerID;
		body_R.sortingLayerID = sortingLayerID;
		global::UnityEngine.SpriteRenderer body_R2 = Body_R;
		sortingLayerID = 100;
		Body_L.sortingOrder = sortingLayerID;
		body_R2.sortingOrder = sortingLayerID;
		global::UnityEngine.SpriteRenderer body_Top_R = Body_Top_R;
		sortingLayerID = 3;
		Body_Bot_L.sortingLayerID = sortingLayerID;
		sortingLayerID = sortingLayerID;
		Body_Bot_R.sortingLayerID = sortingLayerID;
		sortingLayerID = sortingLayerID;
		Body_Top_L.sortingLayerID = sortingLayerID;
		body_Top_R.sortingLayerID = sortingLayerID;
		global::UnityEngine.SpriteRenderer body_Top_R2 = Body_Top_R;
		sortingLayerID = 101;
		Body_Bot_L.sortingOrder = sortingLayerID;
		sortingLayerID = sortingLayerID;
		Body_Bot_R.sortingOrder = sortingLayerID;
		sortingLayerID = sortingLayerID;
		Body_Top_L.sortingOrder = sortingLayerID;
		body_Top_R2.sortingOrder = sortingLayerID;
	}

	private void Set_GatePass()
	{
		if (global::UnityEngine.GameObject.Find("RedAlert") != null)
		{
			global::UnityEngine.GameObject.Find("RedAlert").GetComponent<global::UnityEngine.SpriteRenderer>().sortingLayerID = 20;
			global::UnityEngine.GameObject.Find("RedAlert").GetComponent<global::UnityEngine.SpriteRenderer>().sortingOrder = 2000;
		}
		global::UnityEngine.SpriteRenderer body_R = Body_R;
		int sortingLayerID = 20;
		Body_L.sortingLayerID = sortingLayerID;
		body_R.sortingLayerID = sortingLayerID;
		global::UnityEngine.SpriteRenderer body_R2 = Body_R;
		sortingLayerID = 1600;
		Body_L.sortingOrder = sortingLayerID;
		body_R2.sortingOrder = sortingLayerID;
		global::UnityEngine.SpriteRenderer body_Top_R = Body_Top_R;
		sortingLayerID = 20;
		Body_Bot_L.sortingLayerID = sortingLayerID;
		sortingLayerID = sortingLayerID;
		Body_Bot_R.sortingLayerID = sortingLayerID;
		sortingLayerID = sortingLayerID;
		Body_Top_L.sortingLayerID = sortingLayerID;
		body_Top_R.sortingLayerID = sortingLayerID;
		global::UnityEngine.SpriteRenderer body_Top_R2 = Body_Top_R;
		sortingLayerID = 1601;
		Body_Bot_L.sortingOrder = sortingLayerID;
		sortingLayerID = sortingLayerID;
		Body_Bot_R.sortingOrder = sortingLayerID;
		sortingLayerID = sortingLayerID;
		Body_Top_L.sortingOrder = sortingLayerID;
		body_Top_R2.sortingOrder = sortingLayerID;
	}

	private void Update()
	{
		if (GM.Paused || GM.EventState == 199)
		{
			return;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (BossGateSound_Timer > 0f)
		{
			BossGateSound_Timer += global::UnityEngine.Time.deltaTime;
			if (!onBossGateSound && BossGateSound_Timer > 0.6f)
			{
				onBossGateSound = true;
				BossGateSound_Timer = 0f;
				global::UnityEngine.GameObject.Find("Menu").GetComponent<UI_SoundList>().SendMessage("Sound_Gate_Close");
			}
		}
		if (Sound_Timer > 0f)
		{
			Sound_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (onPass)
		{
			global::UnityEngine.SpriteRenderer body_R = Body_R;
			global::UnityEngine.Color color = new global::UnityEngine.Color(1f, 1f, 1f, global::UnityEngine.Mathf.Lerp(Body_L.color.a, 0f, global::UnityEngine.Time.deltaTime * 3f));
			Body_L.color = color;
			body_R.color = color;
			global::UnityEngine.SpriteRenderer body_Top_R = Body_Top_R;
			color = Body_R.color;
			Body_Bot_L.color = color;
			color = color;
			Body_Bot_R.color = color;
			color = color;
			Body_Top_L.color = color;
			body_Top_R.color = color;
		}
		if (GM.GameOver || GM.onEvent || GM.onHscene)
		{
			isLocked = true;
			ColBox.enabled = true;
			Gate_Up.transform.localPosition = global::UnityEngine.Vector3.Lerp(Gate_Up.transform.localPosition, pos_Up_Close, global::UnityEngine.Time.deltaTime * 5f);
			Gate_Down.transform.localPosition = global::UnityEngine.Vector3.Lerp(Gate_Down.transform.localPosition, pos_Down_Close, global::UnityEngine.Time.deltaTime * 5f);
			Glow_Top.transform.localPosition = global::UnityEngine.Vector3.Lerp(Glow_Top.transform.localPosition, pos_Glow_Top_Close, global::UnityEngine.Time.deltaTime * 2f);
			Glow_Bot.transform.localPosition = global::UnityEngine.Vector3.Lerp(Glow_Bot.transform.localPosition, pos_Glow_Bot_Close, global::UnityEngine.Time.deltaTime * 2f);
			Glow_Top.transform.localScale = global::UnityEngine.Vector3.Lerp(Glow_Top.transform.localScale, new global::UnityEngine.Vector3(-0.5f, -0.1f, 1f), global::UnityEngine.Time.deltaTime * 3f);
			Glow_Bot.transform.localScale = global::UnityEngine.Vector3.Lerp(Glow_Bot.transform.localScale, new global::UnityEngine.Vector3(-0.7f, 0.1f, 1f), global::UnityEngine.Time.deltaTime * 3f);
			Glow_Top.color = global::UnityEngine.Color.Lerp(Glow_Top.color, color_Glow_Close, global::UnityEngine.Time.deltaTime * 1.6f);
			Glow_Bot.color = global::UnityEngine.Color.Lerp(Glow_Bot.color, color_Glow_Close, global::UnityEngine.Time.deltaTime * 1.6f);
			return;
		}
		if (isLocked)
		{
			if (global::UnityEngine.Vector3.Distance(pos_Bot.position, Player.transform.position) < 8f || global::UnityEngine.Vector3.Distance(pos_Top.position, Player.transform.position) < 8f)
			{
				if (monLocked)
				{
					if (Keep_Monster == null)
					{
						monLocked = false;
						if (Card_Num > 0)
						{
							Check_Card();
						}
						else
						{
							isLocked = false;
						}
					}
				}
				else if (bossLocked)
				{
					if (Boss_Lock == null)
					{
						bossLocked = false;
						if (Card_Num > 0)
						{
							Check_Card();
						}
						else
						{
							isLocked = false;
						}
					}
				}
				else
				{
					Check_Card();
					if (isLocked)
					{
						Info_Card.GetComponent<Info_Gate>().on_Info = true;
					}
					else
					{
						Info_Card.GetComponent<Info_Gate>().on_Info = false;
					}
				}
			}
			else
			{
				Info_Card.GetComponent<Info_Gate>().on_Info = false;
			}
			if (bossLocked && Life_Timer > 0.4f)
			{
				Gate_Up.transform.localPosition = global::UnityEngine.Vector3.Lerp(Gate_Up.transform.localPosition, pos_Up_Close, global::UnityEngine.Time.deltaTime * 5f);
				Gate_Down.transform.localPosition = global::UnityEngine.Vector3.Lerp(Gate_Down.transform.localPosition, pos_Down_Close, global::UnityEngine.Time.deltaTime * 5f);
				Glow_Top.transform.localPosition = global::UnityEngine.Vector3.Lerp(Glow_Top.transform.localPosition, pos_Glow_Top_Close, global::UnityEngine.Time.deltaTime * 2f);
				Glow_Bot.transform.localPosition = global::UnityEngine.Vector3.Lerp(Glow_Bot.transform.localPosition, pos_Glow_Bot_Close, global::UnityEngine.Time.deltaTime * 2f);
				Glow_Top.transform.localScale = global::UnityEngine.Vector3.Lerp(Glow_Top.transform.localScale, new global::UnityEngine.Vector3(-0.5f, -0.1f, 1f), global::UnityEngine.Time.deltaTime * 3f);
				Glow_Bot.transform.localScale = global::UnityEngine.Vector3.Lerp(Glow_Bot.transform.localScale, new global::UnityEngine.Vector3(-0.7f, 0.1f, 1f), global::UnityEngine.Time.deltaTime * 3f);
				Glow_Top.color = global::UnityEngine.Color.Lerp(Glow_Top.color, color_Glow_Close, global::UnityEngine.Time.deltaTime * 1.6f);
				Glow_Bot.color = global::UnityEngine.Color.Lerp(Glow_Bot.color, color_Glow_Close, global::UnityEngine.Time.deltaTime * 1.6f);
			}
			return;
		}
		if (isOpened)
		{
			if (global::UnityEngine.Vector3.Distance(pos_Bot.position, Player.transform.position) > 13f || global::UnityEngine.Vector3.Distance(pos_Top.position, Player.transform.position) > 13f)
			{
				isOpened = false;
				ColBox.enabled = true;
				if (Sound_Timer <= 0f)
				{
					global::UnityEngine.GameObject.Find("Menu").GetComponent<UI_SoundList>().SendMessage("Sound_Gate_Close");
				}
			}
			else if (!GM.GameOver && !GM.onEvent && !GM.onHscene && !onPass && Pass_Timer > 0.02f)
			{
				onPass = true;
				Player.GetComponent<Player_Control>().Lock_GatePass();
				SM.Go_Room(targetRoom_Num, targetPos_Num, 0f, Player.transform.position.y - pos_Bot.position.y, false);
				Set_GatePass();
			}
			Gate_Up.transform.localPosition = global::UnityEngine.Vector3.Lerp(Gate_Up.transform.localPosition, pos_Up_Open, global::UnityEngine.Time.deltaTime * 2.5f);
			Gate_Down.transform.localPosition = global::UnityEngine.Vector3.Lerp(Gate_Down.transform.localPosition, pos_Down_Open, global::UnityEngine.Time.deltaTime * 2.5f);
			Glow_Top.transform.localPosition = global::UnityEngine.Vector3.Lerp(Glow_Top.transform.localPosition, pos_Glow_Top_Open, global::UnityEngine.Time.deltaTime * 4.5f);
			Glow_Bot.transform.localPosition = global::UnityEngine.Vector3.Lerp(Glow_Bot.transform.localPosition, pos_Glow_Bot_Open, global::UnityEngine.Time.deltaTime * 4.5f);
			Glow_Top.transform.localScale = global::UnityEngine.Vector3.Lerp(Glow_Top.transform.localScale, new global::UnityEngine.Vector3(-0.38f, -0.5f, 1f), global::UnityEngine.Time.deltaTime * 4f);
			Glow_Bot.transform.localScale = global::UnityEngine.Vector3.Lerp(Glow_Bot.transform.localScale, new global::UnityEngine.Vector3(-0.38f, 0.5f, 1f), global::UnityEngine.Time.deltaTime * 4f);
			Glow_Top.color = global::UnityEngine.Color.Lerp(Glow_Top.color, color_Glow_Open, global::UnityEngine.Time.deltaTime * 2f);
			Glow_Bot.color = global::UnityEngine.Color.Lerp(Glow_Bot.color, color_Glow_Open, global::UnityEngine.Time.deltaTime * 2f);
		}
		else
		{
			if (Player.transform.position.y > pos_Bot.position.y - 2f && Player.transform.position.y < pos_Top.position.y + 1.2f && (global::UnityEngine.Vector3.Distance(pos_Bot.position, Player.transform.position) < 8f || global::UnityEngine.Vector3.Distance(pos_Top.position, Player.transform.position) < 8f))
			{
				isOpened = true;
				if (Life_Timer > 0.5f && Sound_Timer <= 0f)
				{
					Sound_Timer = 0.5f;
					global::UnityEngine.GameObject.Find("Menu").GetComponent<UI_SoundList>().SendMessage("Sound_Gate_Open");
				}
			}
			Gate_Up.transform.localPosition = global::UnityEngine.Vector3.Lerp(Gate_Up.transform.localPosition, pos_Up_Close, global::UnityEngine.Time.deltaTime * 3f);
			Gate_Down.transform.localPosition = global::UnityEngine.Vector3.Lerp(Gate_Down.transform.localPosition, pos_Down_Close, global::UnityEngine.Time.deltaTime * 3f);
			Glow_Top.transform.localPosition = global::UnityEngine.Vector3.Lerp(Glow_Top.transform.localPosition, pos_Glow_Top_Close, global::UnityEngine.Time.deltaTime * 2f);
			Glow_Bot.transform.localPosition = global::UnityEngine.Vector3.Lerp(Glow_Bot.transform.localPosition, pos_Glow_Bot_Close, global::UnityEngine.Time.deltaTime * 2f);
			Glow_Top.transform.localScale = global::UnityEngine.Vector3.Lerp(Glow_Top.transform.localScale, new global::UnityEngine.Vector3(-0.5f, -0.1f, 1f), global::UnityEngine.Time.deltaTime * 3f);
			Glow_Bot.transform.localScale = global::UnityEngine.Vector3.Lerp(Glow_Bot.transform.localScale, new global::UnityEngine.Vector3(-0.7f, 0.1f, 1f), global::UnityEngine.Time.deltaTime * 3f);
			Glow_Top.color = global::UnityEngine.Color.Lerp(Glow_Top.color, color_Glow_Close, global::UnityEngine.Time.deltaTime * 1.6f);
			Glow_Bot.color = global::UnityEngine.Color.Lerp(Glow_Bot.color, color_Glow_Close, global::UnityEngine.Time.deltaTime * 1.6f);
		}
		if (isOpened)
		{
			ColBox.enabled = false;
		}
		else
		{
			ColBox.enabled = true;
		}
	}

	private void OnTriggerStay2D(global::UnityEngine.Collider2D col)
	{
		if (!onPass && !GM.Paused && !GM.onHscene && !GM.onGatePass && !isLocked && col.name == "Ani")
		{
			Pass_Timer += global::UnityEngine.Time.deltaTime;
		}
	}
}
