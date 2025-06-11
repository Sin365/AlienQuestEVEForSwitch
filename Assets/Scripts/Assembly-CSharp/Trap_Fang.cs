public class Trap_Fang : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject Head;

	public int Damage;

	public float DmgForce;

	private float pos_Y;

	private float Life_Timer;

    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		pos_Y = Head.transform.position.y;
		Life_Timer = global::UnityEngine.Random.Range(-1.57f, 1.57f);
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			Life_Timer += global::UnityEngine.Time.deltaTime;
			Head.transform.position = new global::UnityEngine.Vector3(Head.transform.position.x, pos_Y + global::UnityEngine.Mathf.Sin(Life_Timer * 5f) * 0.16f, 0f);
		}
	}
}
