public class Mon_Fire_Bomb : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject Bomb;

	public global::UnityEngine.GameObject Glow1;

	public global::UnityEngine.GameObject Glow2;

	public global::UnityEngine.GameObject _Explo;

	public global::UnityEngine.GameObject _Sound_Explo;

	private float Life_Timer;

	private bool onExplo;

	private int facingRight = 1;

	private global::UnityEngine.Color color_On = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_Red = new global::UnityEngine.Color(1f, 0f, 0f, 1f);

	private global::UnityEngine.Vector3 Pos_Orig;

	private float rnd_X;

	private float rnd_Y;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		if (base.transform.localScale.x < 0f)
		{
			facingRight = -1;
		}
		Pos_Orig = Bomb.transform.localPosition;
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			Life_Timer += global::UnityEngine.Time.deltaTime;
			if (Life_Timer > 0.25f)
			{
				Glow1.GetComponent<global::UnityEngine.SpriteRenderer>().color = global::UnityEngine.Color.Lerp(Glow1.GetComponent<global::UnityEngine.SpriteRenderer>().color, color_On, global::UnityEngine.Time.deltaTime * 2.5f);
			}
			if (Life_Timer > 0.7f)
			{
				Glow2.GetComponent<global::UnityEngine.SpriteRenderer>().color = global::UnityEngine.Color.Lerp(Glow2.GetComponent<global::UnityEngine.SpriteRenderer>().color, color_Red, global::UnityEngine.Time.deltaTime * 2f);
				rnd_X = (float)global::UnityEngine.Random.Range(-25, 25) * 0.01f * (Life_Timer - 0.7f);
				Bomb.transform.localPosition = new global::UnityEngine.Vector3(rnd_X, 0f, 0f);
			}
			if (Life_Timer > 1.5f && !onExplo)
			{
				Explo();
			}
		}
	}

	private void Explo()
	{
		onExplo = true;
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Explo, base.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(_Sound_Explo, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
		global::UnityEngine.Object.Destroy(base.gameObject);
	}
}
