public class Trap_Fang_Group : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject[] Head;

	private float[] Life_Timer;

	private float pos_Y;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		pos_Y = Head[0].transform.position.y;
		Life_Timer = new float[Head.Length];
		for (int i = 0; i < Head.Length; i++)
		{
			Life_Timer[i] = global::UnityEngine.Random.Range(-1.57f, 1.57f);
		}
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			for (int i = 0; i < Head.Length; i++)
			{
				Life_Timer[i] += global::UnityEngine.Time.deltaTime;
				Head[i].transform.position = new global::UnityEngine.Vector3(Head[i].transform.position.x, pos_Y + global::UnityEngine.Mathf.Sin(Life_Timer[i] * 5f) * 0.16f, 0f);
			}
		}
	}
}
