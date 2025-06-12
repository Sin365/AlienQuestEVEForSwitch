using UnityEngine;

public class Gate_Passage : global::UnityEngine.MonoBehaviour
{
	public int targetRoom_Num;

	public int targetPos_Num;

	public bool onJumpDrop;

	private bool onPass;

	private float Pass_Timer;

	private float Life_Timer;

    GameManager GM => GameManager.instance;
    StageManager SM => GameManager.instance.sm_StageManager;

    Player_Control PC => GameManager.instance.PC;
    GameObject Player => GameManager.instance.gobj_Player;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//SM = global::UnityEngine.GameObject.Find("StageManager").GetComponent<StageManager>();
		//Player = global::UnityEngine.GameObject.Find("Player");
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (!onPass && Pass_Timer > 0.01f && Life_Timer > 1f)
		{
			onPass = true;
			Player.GetComponent<Player_Control>().Lock_GatePass();
			if (onJumpDrop)
			{
				SM.Go_Room(targetRoom_Num, targetPos_Num, Player.transform.position.x - base.transform.position.x, 0f, false);
			}
			else
			{
				SM.Go_Room(targetRoom_Num, targetPos_Num, 0f, Player.transform.position.y - base.transform.position.y, false);
			}
		}
	}

	private void OnTriggerStay2D(global::UnityEngine.Collider2D col)
	{
		if (!onPass && !GM.Paused && !GM.onGatePass && col.name == "Ani")
		{
			Pass_Timer += global::UnityEngine.Time.deltaTime;
		}
	}
}
