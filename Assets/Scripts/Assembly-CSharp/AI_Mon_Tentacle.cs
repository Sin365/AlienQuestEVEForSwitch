using UnityEngine;

public class AI_Mon_Tentacle : global::UnityEngine.MonoBehaviour
{
	private int Idle_Num = 1;

	private bool isHide;

	private bool onHscene;

	private float H_Timer;

	private float Life_Timer;

	private float Idle_Timer;

	private float Hide_Timer;

	public bool on_Dist;

	public float distance;

	public float Check_Timer;

	public global::UnityEngine.Transform pos_L;

	public global::UnityEngine.Transform pos_R;

	private Sound_Control SC;
    Player_Control PC => GameManager.instance.PC;
    GameObject Player => GameManager.instance.gobj_Player;

    GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		SC = global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>();
		//Player = global::UnityEngine.GameObject.Find("Player");
		if (global::UnityEngine.Random.Range(0, 10) > 7)
		{
			Idle_Num = 2;
			GetComponent<global::UnityEngine.Animator>().SetInteger("Idle_Num", Idle_Num);
		}
		Idle_Timer = global::UnityEngine.Random.Range(0f, 5f);
		GetComponent<global::UnityEngine.Animator>().speed = 1f + (float)global::UnityEngine.Random.Range(-10, 10) * 0.01f;
	}

	private void Update()
	{
		if (GM.Paused || GM.onEvent)
		{
			return;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (H_Timer > 0f)
		{
			H_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (!GM.GameOver)
		{
			distance = global::UnityEngine.Vector3.Distance(base.transform.position, Player.transform.position);
		}
		else
		{
			distance = global::UnityEngine.Vector3.Distance(base.transform.position, global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position);
		}
		on_Dist = global::UnityEngine.Physics2D.Linecast(pos_L.position, pos_R.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player"));
		if (on_Dist)
		{
			if (!isHide && H_Timer <= 0f)
			{
				Check_Timer += global::UnityEngine.Time.deltaTime;
			}
		}
		else
		{
			Check_Timer = 0f;
		}
		if (onHscene)
		{
			return;
		}
		if (isHide)
		{
			Hide_Timer -= global::UnityEngine.Time.deltaTime;
			if (Hide_Timer <= 0f)
			{
				Show();
			}
			return;
		}
		Idle_Timer += global::UnityEngine.Time.deltaTime;
		if (Idle_Timer > 10f)
		{
			Idle_Num = ((global::UnityEngine.Random.Range(0, 10) <= 5) ? 1 : 2);
			GetComponent<global::UnityEngine.Animator>().SetInteger("Idle_Num", Idle_Num);
			Idle_Timer = 0f;
		}
	}

	public void Set_Hscene()
	{
		onHscene = true;
		Hide(3f);
	}

	public void End_Hscene()
	{
		onHscene = false;
		Hide_Timer = 2f;
		H_Timer = 3f;
	}

	public void Hide(float timer)
	{
		isHide = true;
		Hide_Timer = timer;
		GetComponent<global::UnityEngine.Animator>().SetTrigger("onHide");
		if (Life_Timer > 1f && global::UnityEngine.GameObject.Find("HG_List") != null)
		{
			global::UnityEngine.GameObject.Find("HG_List").GetComponent<Event_TentacleToMon7>().Hit(base.transform.position);
		}
	}

	private void Show()
	{
		isHide = false;
		GetComponent<global::UnityEngine.Animator>().SetTrigger("onShow");
		global::UnityEngine.GameObject.Find("Sound_List_H").GetComponent<H_SoundControl>().SendMessage("Sound_Piston_10");
	}

	private void OnTriggerStay2D(global::UnityEngine.Collider2D col)
	{
		if (!GM.Paused && !GM.onEvent && !GM.onHscene && !GM.onDown && !GM.onGatePass && !GM.onGameClear && !isHide)
		{
			if (col.tag == "Magic_Explo")
			{
				SC.Mon_Hit_2(base.transform.position);
				Hide(5f);
			}
			else if (col.tag == "Magic_Smog")
			{
				Hide(5f);
			}
			else if (col.tag == "Magic_Fire")
			{
				SC.Mon_Hit_1(base.transform.position);
				Hide(5f);
			}
			if (col.tag == "Col_PC_Atk" && col.name != "Col_Rolling")
			{
				SC.Mon_Hit_1(base.transform.position);
				Hide(5f);
			}
		}
	}
}
