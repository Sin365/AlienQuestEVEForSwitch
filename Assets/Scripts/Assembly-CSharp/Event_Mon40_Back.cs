public class Event_Mon40_Back : global::UnityEngine.MonoBehaviour
{
	private float Life_Timer;

	private float Speed_L = 5f;

	private float Speed_R = 5f;

	private float Sleep_Timer_L;

	private float Sleep_Timer_R;

	public global::UnityEngine.Transform Mon_40_L;

	public global::UnityEngine.Transform Mon_40_R;

	private global::UnityEngine.Vector3 pos_Target_L;

	private global::UnityEngine.Vector3 pos_Target_R;

	private bool onPause;

    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		if (GM.Get_Event(13))
		{
			Life_Timer = 12f;
		}
		Mon_40_L.localPosition = new global::UnityEngine.Vector3(-32f, global::UnityEngine.Random.Range(-5f, 13f), 0f);
		Mon_40_R.localPosition = new global::UnityEngine.Vector3(32f, global::UnityEngine.Random.Range(-5f, 13f), 0f);
		Speed_L = global::UnityEngine.Random.Range(1f, 10f);
		Sleep_Timer_L = global::UnityEngine.Random.Range(0f, 3f);
		Speed_R = global::UnityEngine.Random.Range(1f, 10f);
		Sleep_Timer_R = global::UnityEngine.Random.Range(0f, 3f);
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			if (onPause)
			{
				onPause = false;
				Mon_40_L.GetComponent<global::UnityEngine.Animator>().speed = 1f;
				Mon_40_R.GetComponent<global::UnityEngine.Animator>().speed = 1f;
			}
			Life_Timer += global::UnityEngine.Time.deltaTime;
			if (Life_Timer > 15f)
			{
				if (Sleep_Timer_L > 0f)
				{
					Sleep_Timer_L -= global::UnityEngine.Time.deltaTime;
				}
				if (Sleep_Timer_R > 0f)
				{
					Sleep_Timer_R -= global::UnityEngine.Time.deltaTime;
				}
				if (Sleep_Timer_L <= 0f)
				{
					Mon_40_L.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Speed_L);
				}
				if (Sleep_Timer_R <= 0f)
				{
					Mon_40_R.Translate(global::UnityEngine.Vector3.left * global::UnityEngine.Time.deltaTime * Speed_R);
				}
				if (Mon_40_L.localPosition.x > 32f)
				{
					Mon_40_L.localPosition = new global::UnityEngine.Vector3(-32f, global::UnityEngine.Random.Range(-5f, 13f), 0f);
					Speed_L = global::UnityEngine.Random.Range(1f, 10f);
					Sleep_Timer_L = global::UnityEngine.Random.Range(3f, 15f);
				}
				if (Mon_40_R.localPosition.x < -32f)
				{
					Mon_40_R.localPosition = new global::UnityEngine.Vector3(32f, global::UnityEngine.Random.Range(-5f, 13f), 0f);
					Speed_R = global::UnityEngine.Random.Range(1f, 10f);
					Sleep_Timer_R = global::UnityEngine.Random.Range(3f, 15f);
				}
			}
		}
		else if (!onPause)
		{
			onPause = true;
			Mon_40_L.GetComponent<global::UnityEngine.Animator>().speed = 0f;
			Mon_40_R.GetComponent<global::UnityEngine.Animator>().speed = 0f;
		}
	}
}
