using UnityEngine;

public class Event_GunShip_Bay : global::UnityEngine.MonoBehaviour
{
	public LV_0_Ship GunShip;

	public global::UnityEngine.GameObject Ship_Board;

	private bool onEvent;

	private bool onLanding;

	private float Landing_Timer;

	private bool onIdle = true;

	private float Idle_Timer;

	private bool onPlayerDown;

	private float Room_0_Pos_X = 256.035f;

	private bool onGameEnding;

	private bool onFadeOut;

	private float GameEnding_Timer;

	private global::UnityEngine.Vector3 pos_ShipBoard_Up;

	private global::UnityEngine.Vector3 pos_ShipBoard_Down;

	private global::UnityEngine.Vector3 pos_Player_Down;

    GameManager GM => GameManager.instance;
    Player_Control PC => GameManager.instance.PC;
    GameObject Player => GameManager.instance.gobj_Player;
    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		pos_ShipBoard_Up = new global::UnityEngine.Vector3(0f, 6.52f, 0f);
		pos_ShipBoard_Down = new global::UnityEngine.Vector3(0f, -0.52f, 0f);
		pos_Player_Down = new global::UnityEngine.Vector3(Room_0_Pos_X, -5.4f, 0f);
		if (GM.EventState == 10)
		{
			GunShip.Sound_On = false;
			Ship_Board.transform.localPosition = pos_ShipBoard_Down;
		}
		else if (GM.Get_Event(3) && GM.EventState == 200)
		{
			Ship_Board.transform.localPosition = pos_ShipBoard_Down;
		}
	}

	private void Update()
	{
		if (GM.onEvent && GM.EventState == 101)
		{
			if (!onEvent)
			{
				return;
			}
			if (onLanding)
			{
				Landing_Timer += global::UnityEngine.Time.deltaTime;
				if (onLanding && Landing_Timer > 1f && GetComponent<global::UnityEngine.Animator>().speed < 1f)
				{
					GetComponent<global::UnityEngine.Animator>().speed = 1f;
				}
			}
			else
			{
				if (!onIdle)
				{
					return;
				}
				Idle_Timer += global::UnityEngine.Time.deltaTime;
				if (!(Idle_Timer > 3.5f))
				{
					return;
				}
				if (!onPlayerDown)
				{
					onPlayerDown = true;
					GunShip.Set_SortingLayer_On();
					GunShip.SendMessage("Play_Sound_DoorOpen");
					Ship_Board.transform.localPosition = pos_ShipBoard_Up;
					Player.transform.position = new global::UnityEngine.Vector3(Room_0_Pos_X, 1.54f, 0f);
					return;
				}
				Ship_Board.transform.localPosition = global::UnityEngine.Vector3.Lerp(Ship_Board.transform.localPosition, pos_ShipBoard_Down, global::UnityEngine.Time.deltaTime * 3.2f);
				Player.transform.position = global::UnityEngine.Vector3.Lerp(Player.transform.position, pos_Player_Down, global::UnityEngine.Time.deltaTime * 3.2f);
				if (global::UnityEngine.Vector3.Distance(Ship_Board.transform.localPosition, pos_ShipBoard_Down) < 0.05f)
				{
					onEvent = false;
					global::UnityEngine.GameObject.Find("Dialogue").GetComponent<Event_Control>().Complete_Event();
					GunShip.Set_SortingLayer_Off();
				}
			}
		}
		else if (GM.onEvent && GM.EventState == 200 && GM.onGameClear)
		{
			GameEnding_Timer += global::UnityEngine.Time.deltaTime;
			if (!onEvent)
			{
				onEvent = true;
				GunShip.Set_SortingLayer_On();
				GunShip.SendMessage("Play_Sound_DoorOpen");
				pos_ShipBoard_Up = new global::UnityEngine.Vector3(0f, 7.1f, 0f);
			}
			Ship_Board.transform.localPosition = global::UnityEngine.Vector3.Lerp(Ship_Board.transform.localPosition, pos_ShipBoard_Up, global::UnityEngine.Time.deltaTime * 0.8f);
			if (!onGameEnding && global::UnityEngine.Vector3.Distance(Ship_Board.transform.localPosition, pos_ShipBoard_Up) < 1.5f)
			{
				onGameEnding = true;
				GetComponent<global::UnityEngine.Animator>().SetTrigger("onEnd");
				GunShip.Sound_On = true;
			}
			if (!onFadeOut && GameEnding_Timer > 6f)
			{
				onFadeOut = true;
				GunShip.Sound_On = false;
				GM.Set_FadeOut("GameEnding");
				AxiPlayerPrefs.SetInt("Escaped", 1);
			}
		}
		else
		{
			if (GM.Paused)
			{
				return;
			}
			if (GunShip.onEngineStart && global::UnityEngine.Vector3.Distance(Player.transform.position, Ship_Board.transform.position) >= 1.8f)
			{
				GunShip.Engine_Stop(1f);
			}
			else if (!GunShip.onEngineStart && global::UnityEngine.Vector3.Distance(Player.transform.position, Ship_Board.transform.position) < 1.8f)
			{
				if (GunShip.Sound_On)
				{
					GunShip.Sound_On = false;
				}
				GunShip.Engine_Start();
			}
		}
	}

	private void ReadyTo_Landing()
	{
		onEvent = true;
		onIdle = false;
		onLanding = true;
		Landing_Timer = 0f;
		GetComponent<global::UnityEngine.Animator>().SetTrigger("onLanding");
		GetComponent<global::UnityEngine.Animator>().speed = 0f;
		Ship_Board.transform.localPosition = new global::UnityEngine.Vector3(0f, -10.52f, 0f);
		GunShip.Engine_Start();
	}

	private void Landing_Complete()
	{
		onIdle = true;
		onLanding = false;
		Idle_Timer = 0f;
		Landing_Timer = 0f;
	}

	private void Engine_Off()
	{
		GunShip.Engine_Stop(2.8f);
		GunShip.SendMessage("Play_Sound_EngineStop");
	}

	private void Engine_Sound_On()
	{
		GunShip.Sound_On = true;
	}

	private void Play_Sound_LandingGear()
	{
		GunShip.SendMessage("Play_Sound_LandingGear");
	}

	private void GameStart_RightNow()
	{
		GetComponent<global::UnityEngine.Animator>().Play("Idle", 0, 0f);
		Ship_Board.transform.localPosition = new global::UnityEngine.Vector3(0f, -0.52f, 0f);
		GunShip.Set_SortingLayer_Off();
		GunShip.Engine_Stop(2.8f);
		GunShip.SendMessage("Play_Sound_EngineStop");
	}
}
