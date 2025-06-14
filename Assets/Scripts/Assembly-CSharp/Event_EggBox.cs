using UnityEngine;

public class Event_EggBox : global::UnityEngine.MonoBehaviour
{
	private float Life_Timer;

	private int State;

	private float State_Timer;

	private float Dist;

	private float Dist_Timer;

	private float Move_Speed;

	public global::UnityEngine.Transform EggBox_Top;

	public global::UnityEngine.Transform EggBox_Bot;

	public global::UnityEngine.SpriteRenderer Glow_Red;

	public global::UnityEngine.Transform Grill;

	public global::UnityEngine.Transform Egg;

	private global::UnityEngine.Vector3 pos_Target;

	private global::UnityEngine.Vector3 pos_Top;

	private global::UnityEngine.Vector3 pos_Bot;

	private global::UnityEngine.Transform Main_Cam;

    GameManager GM => GameManager.instance;

    Player_Control PC => GameManager.instance?.PC;
    GameObject Player => GameManager.instance?.gobj_Player;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//Player = global::UnityEngine.GameObject.Find("Player");
		Main_Cam = UnityEngine.Camera.main.transform;
		pos_Top = EggBox_Top.localPosition;
		pos_Bot = EggBox_Bot.localPosition;
		EggBox_Top.localPosition = new global::UnityEngine.Vector3(pos_Top.x, pos_Top.y + 0.66f, 0f);
		EggBox_Bot.localPosition = new global::UnityEngine.Vector3(pos_Bot.x, pos_Bot.y - 0.76f, 0f);
		pos_Target = new global::UnityEngine.Vector3(base.transform.localPosition.x, -30f, 0f);
		Glow_Red.color = new global::UnityEngine.Color(1f, 0f, 0f, 0f);
		if (!GM.Get_Event(1))
		{
			State = 100;
			base.transform.localPosition = pos_Target;
		}
		else if (GM.Get_Event(8))
		{
			State = 100;
			base.transform.localPosition = pos_Target;
			Grill.localPosition = new global::UnityEngine.Vector3(Grill.localPosition.x, Grill.localPosition.y - 50f, 0f);
			Egg.localPosition = new global::UnityEngine.Vector3(Egg.localPosition.x, Egg.localPosition.y - 50f, 0f);
		}
	}

	private void Update()
	{
		if (State >= 100)
		{
			return;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (State == 0)
		{
			State_Timer += global::UnityEngine.Time.deltaTime;
			if (State_Timer > 2f)
			{
				State = 1;
				State_Timer = 0f;
				GM.Set_Event(8);
			}
		}
		else if (State == 1)
		{
			State_Timer += global::UnityEngine.Time.deltaTime;
			EggBox_Top.localPosition = global::UnityEngine.Vector3.Lerp(EggBox_Top.localPosition, pos_Top, global::UnityEngine.Time.deltaTime * 2f);
			EggBox_Bot.localPosition = global::UnityEngine.Vector3.Lerp(EggBox_Bot.localPosition, pos_Bot, global::UnityEngine.Time.deltaTime * 2f);
			if (State_Timer > 1.8f)
			{
				Glow_Red.color = global::UnityEngine.Color.Lerp(Glow_Red.color, new global::UnityEngine.Color(1f, 0f, 0f, 1f), global::UnityEngine.Time.deltaTime * 5f);
			}
			if (State_Timer > 4f)
			{
				State++;
				Grill.localPosition = new global::UnityEngine.Vector3(Grill.localPosition.x, Grill.localPosition.y - 50f, 0f);
				Egg.localPosition = new global::UnityEngine.Vector3(Egg.localPosition.x, Egg.localPosition.y - 50f, 0f);
			}
		}
		else if (State == 2)
		{
			Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, 3f, global::UnityEngine.Time.deltaTime);
			base.transform.localPosition = global::UnityEngine.Vector3.Lerp(base.transform.localPosition, pos_Target, global::UnityEngine.Time.deltaTime * Move_Speed);
		}
	}
}
