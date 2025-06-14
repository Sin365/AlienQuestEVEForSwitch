using UnityEngine;

public class Event_CheckInput : global::UnityEngine.MonoBehaviour
{
	public int Event_Num;

	private float Dist = 100f;

	private float PushY;

	private float inputY;

	private float prevY;

	public global::UnityEngine.GameObject info_UpArrow;
    Player_Control PC => GameManager.instance?.PC;
    GameObject Player => GameManager.instance?.gobj_Player;

    GameManager GM => GameManager.instance;

	private Custom_Key CK => GameManager.instance.CK;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//CK = global::UnityEngine.GameObject.Find("GameManager").GetComponent<Custom_Key>();
		//Player = global::UnityEngine.GameObject.Find("Player");
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		Dist = global::UnityEngine.Vector3.Distance(base.transform.position, Player.transform.position);
		if (!GM.Get_Event(Event_Num) && Dist < 1.2f)
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
			info_UpArrow.GetComponent<Info_Gate>().on_Info = true;
			if (inputY > 0f)
			{
				GM.Set_Event(Event_Num);
			}
		}
		else
		{
			info_UpArrow.GetComponent<Info_Gate>().on_Info = false;
		}
	}
}
