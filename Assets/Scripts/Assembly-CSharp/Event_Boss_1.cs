public class Event_Boss_1 : global::UnityEngine.MonoBehaviour
{
	public int Boss_Num = 1;

	private int state;

	private float Start_Timer;

	private float Release_Timer;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		if (GM.Get_Event(10 + Boss_Num))
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
		else if (state == 0)
		{
			Start_Timer += global::UnityEngine.Time.deltaTime;
			if (!GM.onEvent && Start_Timer > 0.8f)
			{
				GM.onEvent = true;
				global::UnityEngine.GameObject.Find("BGM_List").GetComponent<BGM_Control>().Play_Boss(Boss_Num);
			}
			if (Start_Timer > 1.8f)
			{
				state = 1;
				GM.onEvent = true;
				if (Boss_Num == 1)
				{
					global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().Event_Cam_Pos(base.transform.position, 1.5f);
				}
				else if (Boss_Num == 2)
				{
					global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().Event_Cam_Pos(base.transform.position, 1.5f);
				}
				else if (Boss_Num == 3)
				{
					global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().Event_Cam_Pos(base.transform.position, 1f);
				}
				else if (Boss_Num == 4)
				{
					global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().targetSize = 11.2f;
					global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().Event_Cam_Pos(base.transform.position, 2f);
				}
				else
				{
					global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().Event_Cam_Pos(base.transform.position, 1.5f);
				}
			}
		}
		else if (state == 1)
		{
			Release_Timer += global::UnityEngine.Time.deltaTime;
			if (Release_Timer > 3.6f)
			{
				state = 2;
				GM.onEvent = false;
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().targetSize = global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().MaxSize;
			}
		}
	}
}
