using UnityEngine;

public class LV_5_XenoRush : global::UnityEngine.MonoBehaviour
{
	private int Room_Num;

	public int Gen_Count;

	public int Gen_Count_Max = 1;

	public float Gen_Timer = 10f;

	private int Gen_Count_Head;

	public int Index;

	public global::UnityEngine.Transform Distance_Bar;

	public global::UnityEngine.Transform Pos_Y;

	public int Mon_33_Event_Num;

	public int[] Mon_Order;

	public global::UnityEngine.GameObject[] Xeno_List;

	public global::UnityEngine.Transform[] Head_L;

	public global::UnityEngine.Transform[] Head_R;

	private bool On_Head_L;

	private bool On_Head_R;

	private int Mon_Num_L;

	private int Mon_Num_R;

	private float Active_Timer;

	private float dist_X;

	private float dist_Y;

	private float L_Timer;

	private float R_Timer;

	private global::UnityEngine.Vector3 Target_L;

	private global::UnityEngine.Vector3 Target_R;

	private global::UnityEngine.Camera Main_Cam;
    Player_Control PC => GameManager.instance.PC;
    GameObject Player => GameManager.instance.gobj_Player;

    GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//Player = global::UnityEngine.GameObject.Find("Player");
		Main_Cam = global::UnityEngine.GameObject.Find("Main Camera").GetComponent<global::UnityEngine.Camera>();
		Room_Num = GM.Room_Num;
		if (Gen_Count_Max != Mon_Order.Length)
		{
			Gen_Count_Max = Mon_Order.Length;
		}
		for (int i = 0; i < Head_L.Length; i++)
		{
			Head_L[i].position = new global::UnityEngine.Vector3(Head_L[i].position.x, base.transform.position.y - 8f, 0f);
			Head_L[i].transform.localRotation = global::UnityEngine.Quaternion.Euler(0f, 0f, 5f);
			Head_R[i].position = new global::UnityEngine.Vector3(Head_R[i].position.x, base.transform.position.y - 8f, 0f);
			Head_R[i].transform.localRotation = global::UnityEngine.Quaternion.Euler(0f, 0f, 355f);
		}
		L_Timer = global::UnityEngine.Random.Range(0f, 1f);
		R_Timer = global::UnityEngine.Random.Range(0f, 1f);
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		dist_X = global::UnityEngine.Mathf.Abs(Player.transform.position.x - base.transform.position.x);
		dist_Y = global::UnityEngine.Mathf.Abs(Player.transform.position.y - base.transform.position.y);
		if (!GM.GameOver && !GM.onHscene && dist_X < Distance_Bar.localScale.x && dist_Y < 5f)
		{
			Active_Timer += global::UnityEngine.Time.deltaTime;
			L_Timer += global::UnityEngine.Time.deltaTime;
			R_Timer += global::UnityEngine.Time.deltaTime;
		}
		else
		{
			if (L_Timer > 2f)
			{
				L_Timer += global::UnityEngine.Time.deltaTime;
			}
			if (R_Timer > 2f)
			{
				R_Timer += global::UnityEngine.Time.deltaTime;
			}
		}
		if (Gen_Count < Gen_Count_Max)
		{
			if (L_Timer > 3.5f && On_Head_L)
			{
				Make_Monster_L();
			}
			else if (L_Timer > 2f && On_Head_L)
			{
				float y = global::UnityEngine.Mathf.Lerp(Head_L[Mon_Num_L].transform.position.y, Pos_Y.position.y + Target_L.y, global::UnityEngine.Time.deltaTime * 6f);
				Head_L[Mon_Num_L].position = new global::UnityEngine.Vector3(Main_Cam.transform.position.x + (0f - Main_Cam.orthographicSize) * Main_Cam.aspect * Target_L.x, y, 0f);
				Head_L[Mon_Num_L].transform.localRotation = global::UnityEngine.Quaternion.Euler(0f, 0f, global::UnityEngine.Mathf.Lerp(Head_L[Mon_Num_L].transform.localRotation.eulerAngles.z, Target_L.z, global::UnityEngine.Time.deltaTime * 3f));
			}
			else if (!On_Head_L && Gen_Count_Head < Gen_Count_Max)
			{
				On_Head_L = true;
				Mon_Num_L = Mon_Order[Gen_Count_Head] - 31;
				Set_Target_L();
				Gen_Count_Head++;
			}
			if (R_Timer > 3.5f && On_Head_R)
			{
				Make_Monster_R();
			}
			else if (R_Timer > 2f && On_Head_R)
			{
				float y2 = global::UnityEngine.Mathf.Lerp(Head_R[Mon_Num_R].transform.position.y, Pos_Y.position.y + Target_R.y, global::UnityEngine.Time.deltaTime * 6f);
				Head_R[Mon_Num_R].position = new global::UnityEngine.Vector3(Main_Cam.transform.position.x + Main_Cam.orthographicSize * Main_Cam.aspect * Target_R.x, y2, 0f);
				Head_R[Mon_Num_R].transform.localRotation = global::UnityEngine.Quaternion.Euler(0f, 0f, global::UnityEngine.Mathf.Lerp(Head_R[Mon_Num_R].transform.localRotation.eulerAngles.z, Target_R.z, global::UnityEngine.Time.deltaTime * 3f));
			}
			else if (!On_Head_R && Gen_Count_Head < Gen_Count_Max)
			{
				On_Head_R = true;
				Mon_Num_R = Mon_Order[Gen_Count_Head] - 31;
				Set_Target_R();
				Gen_Count_Head++;
			}
		}
	}

	private void Set_Target_L()
	{
		if (Mon_Num_L == 0 || Mon_Num_L == 2)
		{
			Target_L = new global::UnityEngine.Vector3(global::UnityEngine.Random.Range(0.7f, 0.9f), global::UnityEngine.Random.Range(-1.5f, -1f), global::UnityEngine.Random.Range(30, 40));
		}
		else
		{
			Target_L = new global::UnityEngine.Vector3(global::UnityEngine.Random.Range(0.7f, 0.9f), global::UnityEngine.Random.Range(-1.5f, -1f), global::UnityEngine.Random.Range(40, 45));
		}
	}

	private void Set_Target_R()
	{
		if (Mon_Num_R == 0 || Mon_Num_R == 2)
		{
			Target_R = new global::UnityEngine.Vector3(global::UnityEngine.Random.Range(0.7f, 0.9f), global::UnityEngine.Random.Range(-1.5f, -1f), global::UnityEngine.Random.Range(320, 330));
		}
		else
		{
			Target_R = new global::UnityEngine.Vector3(global::UnityEngine.Random.Range(0.7f, 0.9f), global::UnityEngine.Random.Range(-1.5f, -1f), global::UnityEngine.Random.Range(310, 315));
		}
	}

	private void Make_Monster_L()
	{
		if (Mon_Num_L < 0 || Mon_Num_L >= Xeno_List.Length)
		{
			Mon_Num_L = 0;
		}
		global::UnityEngine.Vector3 position = new global::UnityEngine.Vector3(Head_L[Mon_Num_L].position.x + 0.5f, base.transform.position.y - 5.2f, 0f);
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Xeno_List[Mon_Num_L], position, base.transform.rotation) as global::UnityEngine.GameObject;
		gameObject.transform.parent = base.transform.parent;
		gameObject.transform.localScale = new global::UnityEngine.Vector3(3f, 3f, 1f);
		gameObject.GetComponent<Mon_Index>().Index = Index + Gen_Count;
		gameObject.GetComponent<Monster>().onEvent = true;
		if (gameObject.GetComponent<AI_Mon_31>() != null)
		{
			gameObject.GetComponent<AI_Mon_31>().event_Type = AI_Mon_31.Event_Type.Bottom;
		}
		else if (gameObject.GetComponent<AI_Mon_33>() != null)
		{
			gameObject.GetComponent<AI_Mon_33>().event_Type = AI_Mon_33.Event_Type.Bottom;
			if (Mon_33_Event_Num > 0)
			{
				gameObject.GetComponent<Monster>().Event_Num = Mon_33_Event_Num;
			}
		}
		else if (gameObject.GetComponent<AI_Mon_Xeno>() != null)
		{
			gameObject.GetComponent<AI_Mon_Xeno>().event_Type = AI_Mon_Xeno.Event_Type.Bottom;
			gameObject.transform.position = new global::UnityEngine.Vector3(Head_L[Mon_Num_L].position.x + 1f, base.transform.position.y - 6f, 0f);
		}
		Head_L[Mon_Num_L].position = new global::UnityEngine.Vector3(Head_L[Mon_Num_L].position.x, base.transform.position.y - 8f, 0f);
		Head_L[Mon_Num_L].transform.localRotation = global::UnityEngine.Quaternion.Euler(0f, 0f, 5f);
		L_Timer = global::UnityEngine.Random.Range(0f, 1f);
		Gen_Count++;
		L_Timer = 0f - Gen_Timer + 3.5f;
		On_Head_L = false;
	}

	private void Make_Monster_R()
	{
		if (Mon_Num_R < 0 || Mon_Num_R >= Xeno_List.Length)
		{
			Mon_Num_R = 0;
		}
		global::UnityEngine.Vector3 position = new global::UnityEngine.Vector3(Head_R[Mon_Num_R].position.x - 0.5f, base.transform.position.y - 5.2f, 0f);
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Xeno_List[Mon_Num_R], position, base.transform.rotation) as global::UnityEngine.GameObject;
		gameObject.transform.parent = base.transform.parent;
		gameObject.transform.localScale = new global::UnityEngine.Vector3(3f, 3f, 1f);
		gameObject.GetComponent<Mon_Index>().Index = Index + Gen_Count;
		gameObject.GetComponent<Monster>().onEvent = true;
		if (gameObject.GetComponent<AI_Mon_31>() != null)
		{
			gameObject.GetComponent<AI_Mon_31>().event_Type = AI_Mon_31.Event_Type.Bottom;
		}
		else if (gameObject.GetComponent<AI_Mon_33>() != null)
		{
			gameObject.GetComponent<AI_Mon_33>().event_Type = AI_Mon_33.Event_Type.Bottom;
			if (Mon_33_Event_Num > 0)
			{
				gameObject.GetComponent<Monster>().Event_Num = Mon_33_Event_Num;
			}
		}
		else if (gameObject.GetComponent<AI_Mon_Xeno>() != null)
		{
			gameObject.GetComponent<AI_Mon_Xeno>().event_Type = AI_Mon_Xeno.Event_Type.Bottom;
			gameObject.transform.position = new global::UnityEngine.Vector3(Head_R[Mon_Num_R].position.x - 1f, base.transform.position.y - 6f, 0f);
		}
		Head_R[Mon_Num_R].position = new global::UnityEngine.Vector3(Head_R[Mon_Num_R].position.x, base.transform.position.y - 8f, 0f);
		Head_R[Mon_Num_R].transform.localRotation = global::UnityEngine.Quaternion.Euler(0f, 0f, 355f);
		R_Timer = global::UnityEngine.Random.Range(0f, 1f);
		Gen_Count++;
		R_Timer = 0f - Gen_Timer + 3.5f;
		On_Head_R = false;
	}
}
