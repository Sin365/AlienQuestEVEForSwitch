public class Event_TentacleToMon7 : global::UnityEngine.MonoBehaviour
{
	public H_Mon7[] Mon_7;

	private float[] Mon7_Timer;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		Mon7_Timer = new float[Mon_7.Length];
		for (int i = 0; i < Mon_7.Length; i++)
		{
			Mon7_Timer[i] = 0f;
			Mon_7[i].GetComponent<global::UnityEngine.Animator>().Play("HG", -1, global::UnityEngine.Random.Range(0f, 0.95f));
		}
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		for (int i = 0; i < Mon_7.Length; i++)
		{
			if (Mon7_Timer[i] > 0f)
			{
				Mon7_Timer[i] -= global::UnityEngine.Time.deltaTime;
			}
		}
	}

	public void Hit(global::UnityEngine.Vector3 pos)
	{
		for (int i = 0; i < Mon_7.Length; i++)
		{
			if (Mon7_Timer[i] <= 0f && global::UnityEngine.Vector3.Distance(Mon_7[i].transform.position, pos) < 5f)
			{
				Mon7_Timer[i] = global::UnityEngine.Random.Range(1f, 2f);
				Mon_7[i].SendMessage("CumShot");
			}
		}
	}
}
