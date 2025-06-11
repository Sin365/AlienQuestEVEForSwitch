public class Event_Tentacle : global::UnityEngine.MonoBehaviour
{
	private bool on_Hscene;

	private int H_Num = -1;

	private int Tentacle_Num = -1;

	private float H_Timer;

	private bool on_Down;

	private float Down_Timer;

	public global::UnityEngine.Transform pos_L;

	public global::UnityEngine.Transform pos_R;

	public AI_Mon_Tentacle[] Tentacle_List;

	public global::UnityEngine.GameObject[] H_Single;

	private Player_Control PC;

	private global::UnityEngine.GameObject Player;

	GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		Player = global::UnityEngine.GameObject.Find("Player");
		PC = Player.GetComponent<Player_Control>();
		for (int i = 0; i < Tentacle_List.Length; i++)
		{
			Tentacle_List[i].Hide(1f + global::UnityEngine.Random.Range(0f, 1f));
		}
		if (H_Single.Length < 2)
		{
			global::UnityEngine.Debug.Log("Error Tentacle H Length...");
		}
	}

	private void Update()
	{
		if (GM.Paused || on_Hscene || GM.onHscene || !(GM.Hscene_Timer <= 0f))
		{
			return;
		}
		if (H_Timer > 0f)
		{
			H_Timer -= global::UnityEngine.Time.deltaTime;
			return;
		}
		if (!GM.GameOver)
		{
			if (GM.onCloth || !(GM.Damage_Timer <= 0f))
			{
				return;
			}
			for (int i = 0; i < Tentacle_List.Length; i++)
			{
				if (Tentacle_List[i].on_Dist && Tentacle_List[i].Check_Timer > 0.6f)
				{
					Start_Hscene(i);
				}
			}
			return;
		}
		on_Down = global::UnityEngine.Physics2D.Linecast(pos_L.position, pos_R.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player"));
		if (on_Down)
		{
			Down_Timer += global::UnityEngine.Time.deltaTime;
		}
		else
		{
			Down_Timer = 0f;
		}
		if (!on_Down || !(Down_Timer > 2f) || !(H_Timer <= 0f))
		{
			return;
		}
		int num = 0;
		for (int j = 1; j < Tentacle_List.Length; j++)
		{
			if (Tentacle_List[j].distance < Tentacle_List[num].distance)
			{
				num = j;
			}
		}
		if (!GM.onHscene)
		{
			Start_Hscene(num);
		}
	}

	private void Start_Hscene(int num)
	{
		on_Hscene = true;
		GM.onEvent = true;
		GM.onHscene = true;
		GM.Hscene_Timer = 1f;
		H_Timer = 2f;
		Down_Timer = 0f;
		H_Num = global::UnityEngine.Random.Range(0, H_Single.Length);
		if (H_Single.Length > 2 && !GM.Get_Event(12))
		{
			H_Num = global::UnityEngine.Random.Range(0, 2);
		}
		if (H_Num == 0)
		{
			GM.Hscene_Num = 8;
		}
		else if (H_Num == 1)
		{
			GM.Hscene_Num = 9;
		}
		else if (H_Num == 2)
		{
			GM.Hscene_Num = 17;
		}
		else if (H_Num == 3)
		{
			GM.Hscene_Num = 18;
		}
		else
		{
			GM.Hscene_Num = 26;
		}
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(H_Single[H_Num], Player.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
		gameObject.transform.parent = base.transform.parent;
		if (PC.facingRight > 0)
		{
			gameObject.SendMessage("Flip");
		}
		gameObject.GetComponent<H_Ani>().Mon_Object = base.gameObject;
		Tentacle_List[num].Set_Hscene();
		Tentacle_Num = num;
		global::UnityEngine.GameObject.Find("Menu").GetComponent<Menu_Control>().H_Object = gameObject;
		global::UnityEngine.GameObject.Find("Main Camera").SendMessage("Hscene_Zoom");
		if (!GM.GameOver)
		{
			Player.SendMessage("H_Down");
		}
		global::UnityEngine.GameObject.Find("Ani").SendMessage("Start_H_Scene");
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Hit_2(base.transform.position);
	}

	private void End_Hscene()
	{
		on_Hscene = false;
		GM.onEvent = false;
		GM.onHscene = false;
		GM.Hscene_Num = 0;
		if (Tentacle_Num > -1)
		{
			Tentacle_List[Tentacle_Num].End_Hscene();
		}
		Tentacle_Num = -1;
		GM.Down_H_After();
		global::UnityEngine.GameObject.Find("Ani").SendMessage("End_H_Scene");
	}
}
