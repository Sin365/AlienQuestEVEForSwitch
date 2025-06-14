public class Event_Boss_1 : global::UnityEngine.MonoBehaviour
{
	public int Boss_Num = 1;

	private int state;

	private float Start_Timer;

	private float Release_Timer;

	GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
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
					UnityEngine.Camera.main.GetComponent<Camera_Control>().Event_Cam_Pos(base.transform.position, 1.5f);
				}
				else if (Boss_Num == 2)
				{
					UnityEngine.Camera.main.GetComponent<Camera_Control>().Event_Cam_Pos(base.transform.position, 1.5f);
				}
				else if (Boss_Num == 3)
				{
					UnityEngine.Camera.main.GetComponent<Camera_Control>().Event_Cam_Pos(base.transform.position, 1f);
				}
				else if (Boss_Num == 4)
				{
					UnityEngine.Camera.main.GetComponent<Camera_Control>().targetSize = 11.2f;
					UnityEngine.Camera.main.GetComponent<Camera_Control>().Event_Cam_Pos(base.transform.position, 2f);
				}
				else
				{
					UnityEngine.Camera.main.GetComponent<Camera_Control>().Event_Cam_Pos(base.transform.position, 1.5f);
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
				UnityEngine.Camera.main.GetComponent<Camera_Control>().targetSize = UnityEngine.Camera.main.GetComponent<Camera_Control>().MaxSize;
			}
		}
	}
}
