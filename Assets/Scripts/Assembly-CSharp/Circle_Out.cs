public class Circle_Out : global::UnityEngine.MonoBehaviour
{
	private float Dust_Timer;

	public global::UnityEngine.GameObject Dust;

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
		if (Dust_Timer > 0.1f)
		{
			Dust_Timer = 0f;
			float num = (float)global::UnityEngine.Random.Range(-20, 20) * 0.1f;
			num += 10f;
			for (int i = 1; i < 40; i++)
			{
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Dust, base.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, num * (float)i)) as global::UnityEngine.GameObject;
				gameObject.transform.Translate(global::UnityEngine.Vector3.right * 1f);
				gameObject.transform.Translate(global::UnityEngine.Vector3.down * 1f);
			}
		}
		else
		{
			Dust_Timer += global::UnityEngine.Time.deltaTime;
		}
	}
}
