using UnityEngine;

public class Event_Lift : global::UnityEngine.MonoBehaviour
{
	private int Event_Num = 4;

	private bool onEnabled;

	public global::UnityEngine.BoxCollider2D Ground_Cover;

	private float Life_Timer;

	private float Dist = 100f;

	private float PushY;

	private float inputY;

	private float prevY;

	public Tile_Lift tile_Lift;

	public Info_Gate info_UpArrow;

	public Event_Computer event_Com;

	GameManager GM => GameManager.instance;
    Player_Control PC => GameManager.instance?.PC;
    GameObject Player => GameManager.instance?.gobj_Player;

    private Custom_Key CK => GameManager.instance.CK;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//CK = global::UnityEngine.GameObject.Find("GameManager").GetComponent<Custom_Key>();
		//Player = global::UnityEngine.GameObject.Find("Player");
		if (GM.Get_Event(Event_Num))
		{
			onEnabled = true;
			Ground_Cover.enabled = false;
		}
		else
		{
			onEnabled = false;
			Ground_Cover.enabled = true;
		}
	}

	private void Update()
	{
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (!GM.Paused && !GM.GameOver && !GM.Get_Event(Event_Num) && GM.Check_EventMonster(8))
		{
			Dist = global::UnityEngine.Vector3.Distance(base.transform.position, Player.transform.position);
			if (Dist < 1.5f)
			{
				inputY = 0f;
				if (global::UnityEngine.Input.GetKeyDown(CK.Up))
				{
					inputY = 1f;
				}
				else if (global::UnityEngine.Input.GetKeyDown(CK.Down))
				{
					inputY = -1f;
				}
				if (inputY == 0f && global::UnityEngine.Input.GetAxis("L_Y") != 0f)
				{
					if (global::UnityEngine.Input.GetAxis("L_Y") > 0f && global::UnityEngine.Input.GetAxis("L_Y") < prevY)
					{
						PushY = prevY;
					}
					else if (global::UnityEngine.Input.GetAxis("L_Y") < 0f && global::UnityEngine.Input.GetAxis("L_Y") > prevY)
					{
						PushY = prevY;
					}
					if (global::UnityEngine.Input.GetAxis("L_Y") > 0f && global::UnityEngine.Input.GetAxis("L_Y") - PushY > 0.3f)
					{
						inputY = 1f;
						PushY = 1f;
					}
					else if (global::UnityEngine.Input.GetAxis("L_Y") < 0f && global::UnityEngine.Input.GetAxis("L_Y") - PushY < -0.3f)
					{
						inputY = -1f;
						PushY = -1f;
					}
				}
				else if (inputY == 0f && global::UnityEngine.Input.GetAxis("L_Y") == 0f)
				{
					PushY = 0f;
				}
				info_UpArrow.on_Info = true;
				if (inputY > 0f)
				{
					GM.Set_Event(Event_Num);
					onEnabled = true;
					Ground_Cover.enabled = false;
					tile_Lift.Lift_Start();
					event_Com.Set_State(1);
				}
			}
			else
			{
				info_UpArrow.on_Info = false;
			}
		}
		else
		{
			info_UpArrow.on_Info = false;
		}
	}
}
