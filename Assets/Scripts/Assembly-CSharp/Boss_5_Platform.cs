public class Boss_5_Platform : global::UnityEngine.MonoBehaviour
{
	private float Life_Timer;

	private float distance;

	private float dist_X;

	private float dist_Y;

	private float dist_Timer;

	private bool check_1;

	private bool check_2;

	private bool check_Queen;

	public global::UnityEngine.Transform Tr_L_2;

	public global::UnityEngine.Transform Tr_R_2;

	public global::UnityEngine.Transform Tr_L_1;

	public global::UnityEngine.Transform Tr_R_1;

	private bool on_Hscene;

	private float H_Timer;

	public global::UnityEngine.GameObject[] H_Single;

	public global::UnityEngine.Transform[] Pos_H;

	public global::UnityEngine.Transform[] Pos_H_Flip;

	public Monster Queen_Mon;

	private Player_Control PC;

	private global::UnityEngine.GameObject Player;

	GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		Player = global::UnityEngine.GameObject.Find("Player");
		PC = Player.GetComponent<Player_Control>();
		if (Queen_Mon != null)
		{
			check_Queen = true;
		}
	}

	private void Update()
	{
		if (GM.Paused || GM.onEvent || on_Hscene)
		{
			return;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
		check_1 = global::UnityEngine.Physics2D.Linecast(Tr_L_2.position, Tr_R_2.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player_Dmg"));
		check_2 = global::UnityEngine.Physics2D.Linecast(Tr_L_1.position, Tr_R_1.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player_Dmg"));
		if (Queen_Mon != null)
		{
			if (!GM.Get_Event(3) && Queen_Mon.onEvent)
			{
				check_Queen = false;
			}
			else
			{
				check_Queen = true;
			}
		}
		else
		{
			check_Queen = true;
		}
		if (H_Timer > 0f)
		{
			H_Timer -= global::UnityEngine.Time.deltaTime;
		}
		else if (check_1 && check_2 && check_Queen && !GM.onHscene && !GM.onCloth && GM.Option_Int[3] == 1 && PC.grounded_Now)
		{
			dist_Timer += global::UnityEngine.Time.deltaTime;
			if (dist_Timer > 0.6f && !GM.onHscene && GM.Hscene_Timer <= 0f && (PC.State == Player_Control.AniState.Idle || PC.State == Player_Control.AniState.Run || PC.State == Player_Control.AniState.Sit || PC.State == Player_Control.AniState.Down))
			{
				if (GM.onShield)
				{
					GM.Break_Shield();
				}
				Start_Hscene();
			}
		}
		else
		{
			dist_Timer = 0f;
		}
	}

	private void Start_Hscene()
	{
		on_Hscene = true;
		GM.onEvent = true;
		GM.onHscene = true;
		GM.Hscene_Timer = 1f;
		int num = 0;
		num = ((global::UnityEngine.Random.Range(0, 10) > 5) ? 1 : 0);
		GM.Hscene_Num = 42 + num;
		bool flag = ((Player.transform.position.x > base.transform.position.x) ? true : false);
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(H_Single[num], (!flag) ? Pos_H[num].position : Pos_H_Flip[num].position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
		gameObject.transform.parent = base.transform.parent;
		if (flag)
		{
			gameObject.SendMessage("Flip");
		}
		gameObject.GetComponent<H_Ani>().Mon_Object = base.gameObject;
		global::UnityEngine.GameObject.Find("Menu").GetComponent<Menu_Control>().H_Object = gameObject;
		global::UnityEngine.GameObject.Find("Main Camera").SendMessage("Hscene_Zoom");
		if (flag)
		{
			Player.transform.position = new global::UnityEngine.Vector3(Pos_H_Flip[0].position.x, Player.transform.position.y, 0f);
		}
		else
		{
			Player.transform.position = new global::UnityEngine.Vector3(Pos_H[0].position.x, Player.transform.position.y, 0f);
		}
		if (PC.facingRight > 0 && flag)
		{
			Player.SendMessage("Flip");
		}
		else if (PC.facingRight < 0 && !flag)
		{
			Player.SendMessage("Flip");
		}
		if (!GM.GameOver)
		{
			Player.SendMessage("H_Down");
		}
		global::UnityEngine.GameObject.Find("Ani").SendMessage("Start_H_Scene");
		H_Timer = 2f;
		dist_Timer = 0f;
	}

	private void End_Hscene()
	{
		on_Hscene = false;
		GM.onEvent = false;
		GM.onHscene = false;
		GM.Hscene_Num = 0;
		GM.Down_H_After();
		global::UnityEngine.GameObject.Find("Ani").SendMessage("End_H_Scene");
	}
}
