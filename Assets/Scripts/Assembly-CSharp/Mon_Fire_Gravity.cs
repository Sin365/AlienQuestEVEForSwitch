public class Mon_Fire_Gravity : global::UnityEngine.MonoBehaviour
{
	public int Type;

	public int Damage = 50;

	public float DmgForce = 10f;

	public global::UnityEngine.SpriteRenderer SR_Glow;

	public global::UnityEngine.GameObject _Dust;

	public global::UnityEngine.GameObject _Explo;

	private bool onExplo;

	private float Life_Timer;

	private float onCam_Timer;

	private float Life_Max = 4f;

	private float Dust_Timer;

	private float diatance;

	private float Glow_Timer;

	private float Glow_Opacity;

	private float Glow_Opacity_Max = 1f;

	private global::UnityEngine.Vector3 posOrig;

	private global::UnityEngine.Vector2 Velocity;

	private Player_Control PC;

	private global::UnityEngine.GameObject Player;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		Player = global::UnityEngine.GameObject.Find("Player");
		PC = Player.GetComponent<Player_Control>();
		Glow_Opacity_Max = SR_Glow.color.a;
		posOrig = base.transform.position;
		Velocity = base.GetComponent<UnityEngine.Rigidbody2D>().velocity;
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			if (GetComponent<global::UnityEngine.Rigidbody2D>().IsSleeping())
			{
				GetComponent<global::UnityEngine.Rigidbody2D>().WakeUp();
				base.GetComponent<UnityEngine.Rigidbody2D>().velocity = Velocity;
				global::UnityEngine.Debug.Log("WakeUp");
			}
			Life_Timer += global::UnityEngine.Time.deltaTime;
			Dust_Timer += global::UnityEngine.Time.deltaTime;
			Glow_Timer += global::UnityEngine.Time.deltaTime;
			if (onCam_Timer > 0f)
			{
				onCam_Timer -= global::UnityEngine.Time.deltaTime;
			}
			diatance = global::UnityEngine.Vector3.Distance(base.transform.position, posOrig);
			if (diatance > 40f || Life_Timer > Life_Max)
			{
				if (onCam_Timer > 0f)
				{
					Explo();
				}
				else
				{
					global::UnityEngine.Object.Destroy(base.gameObject);
				}
			}
			if (Type == 0)
			{
				if (Glow_Timer > 0.01f)
				{
					Glow_Timer = 0f;
					Glow_Opacity = global::UnityEngine.Random.Range(0.1f, Glow_Opacity_Max);
					SR_Glow.color = new global::UnityEngine.Color(SR_Glow.color.r, SR_Glow.color.g, SR_Glow.color.b, Glow_Opacity);
				}
				if (Dust_Timer > 0.01f)
				{
					Dust_Timer = 0f;
					global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Dust, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
					global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(_Dust, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
					global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(_Dust, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
					gameObject2.transform.Translate(global::UnityEngine.Vector3.up * global::UnityEngine.Random.Range(-0.3f, 0.3f));
					gameObject3.transform.Translate(global::UnityEngine.Vector3.up * global::UnityEngine.Random.Range(-0.3f, 0.3f));
				}
			}
		}
		else if (GetComponent<global::UnityEngine.Rigidbody2D>().IsAwake())
		{
			Velocity = base.GetComponent<UnityEngine.Rigidbody2D>().velocity;
			GetComponent<global::UnityEngine.Rigidbody2D>().Sleep();
		}
	}

	private void Explo()
	{
		onExplo = true;
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Explo, new global::UnityEngine.Vector3(base.transform.position.x, base.transform.position.y + 0.8f, 0f), global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Magic_3_Explo(base.transform.position);
		global::UnityEngine.Object.Destroy(base.gameObject);
	}

	private void OnTriggerEnter2D(global::UnityEngine.Collider2D col)
	{
		if (col.tag == "Col_Camera")
		{
			onCam_Timer = 0.5f;
		}
		if (GM.Paused || onExplo)
		{
			return;
		}
		if (col.name == "Ani" && !GM.onShield && !GM.GameOver && !GM.onEvent && !GM.onHscene && !GM.onDown)
		{
			onExplo = true;
			int num = ((!(base.transform.position.x > col.transform.position.x)) ? 1 : (-1));
			GM.Poison_Timer = 3f;
			if (GM.Poison_DMG < 10)
			{
				GM.Poison_DMG = 10;
			}
			GM.Damage_Timer = 0f;
			GM.Damage(Damage, DmgForce * (float)num, false, 0);
			Explo();
		}
		else if (col.tag == "Magic_Shield" || col.tag == "Ground" || col.tag == "Gate" || col.tag == "Breakable")
		{
			onExplo = true;
			Explo();
		}
	}
}
