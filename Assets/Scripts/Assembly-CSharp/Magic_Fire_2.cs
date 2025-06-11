public class Magic_Fire_2 : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject Glow1;

	public global::UnityEngine.GameObject Glow2;

	public global::UnityEngine.GameObject Ring;

	public global::UnityEngine.GameObject Sound_Explo;

	private float Life_Timer;

	private bool onExplo;

	private int facingRight = 1;

	private global::UnityEngine.Color color_Target = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Vector3 Pos_Orig;

	private float rnd_X;

	private float rnd_Y;

	GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		if (base.transform.localScale.x < 0f)
		{
			facingRight = -1;
		}
		Pos_Orig = base.transform.position;
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			Life_Timer += global::UnityEngine.Time.deltaTime;
			if (Life_Timer > 0.25f)
			{
				Glow1.GetComponent<global::UnityEngine.SpriteRenderer>().color = global::UnityEngine.Color.Lerp(Glow1.GetComponent<global::UnityEngine.SpriteRenderer>().color, color_Target, global::UnityEngine.Time.deltaTime * 2f);
			}
			if (Life_Timer > 0.7f)
			{
				Glow2.GetComponent<global::UnityEngine.SpriteRenderer>().color = global::UnityEngine.Color.Lerp(Glow2.GetComponent<global::UnityEngine.SpriteRenderer>().color, color_Target, global::UnityEngine.Time.deltaTime * 1.2f);
				rnd_X = (float)global::UnityEngine.Random.Range(-25, 25) * 0.01f * (Life_Timer - 0.7f);
				rnd_Y = (float)global::UnityEngine.Random.Range(-15, 15) * 0.01f * (Life_Timer - 0.7f);
				base.transform.position = new global::UnityEngine.Vector3(Pos_Orig.x + rnd_X, Pos_Orig.y + rnd_Y, 0f);
			}
			if (Life_Timer > 1.2f && !onExplo)
			{
				Explo();
			}
		}
	}

	private void Explo()
	{
		onExplo = true;
		global::UnityEngine.Vector3 position = new global::UnityEngine.Vector3(base.transform.position.x, base.transform.position.y, 0f);
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Ring, position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(Sound_Explo, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
		global::UnityEngine.Object.Destroy(base.gameObject);
	}
}
