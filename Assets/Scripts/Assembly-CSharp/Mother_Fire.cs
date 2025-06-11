public class Mother_Fire : global::UnityEngine.MonoBehaviour
{
	public int Damage = 80;

	public float DmgForce = 10f;

	public global::UnityEngine.SpriteRenderer Glow_Core;

	public global::UnityEngine.SpriteRenderer Glow_Circle_2;

	public global::UnityEngine.SpriteRenderer Glow_Border;

	public global::UnityEngine.SpriteRenderer Glow_Circle_1;

	public global::UnityEngine.GameObject Dust;

	private bool onExplo;

	private float Life_Timer;

	private float onCam_Timer;

	private float Dust_Timer;

	private float diatance;

	private global::UnityEngine.Vector3 posOrig;

	GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		posOrig = base.transform.position;
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (onCam_Timer > 0f)
		{
			onCam_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (!onExplo)
		{
			float num = 0.18f + (1f + global::UnityEngine.Mathf.Sin(Life_Timer * 20f)) * 0.01f;
			Glow_Core.transform.localScale = new global::UnityEngine.Vector3(num, num, 1f);
			num = 0.3f + (1f + global::UnityEngine.Mathf.Sin(Life_Timer * 20f)) * 0.05f;
			Glow_Circle_2.transform.localScale = new global::UnityEngine.Vector3(num, num, 1f);
			base.transform.Translate(global::UnityEngine.Vector3.left * global::UnityEngine.Time.deltaTime * 12f);
			Dust_Timer += global::UnityEngine.Time.deltaTime;
			if (onCam_Timer > 0f && Dust_Timer > 0.01f)
			{
				Dust_Timer = 0f;
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Dust, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
			}
			diatance = global::UnityEngine.Vector3.Distance(base.transform.position, posOrig);
			if (diatance > 30f || Life_Timer > 10f)
			{
				base.transform.localScale = global::UnityEngine.Vector3.Lerp(base.transform.localScale, new global::UnityEngine.Vector3(0.8f, 0f, 1f), global::UnityEngine.Time.deltaTime * 5f);
				if (onCam_Timer <= 0f)
				{
					global::UnityEngine.Object.Destroy(base.gameObject);
				}
			}
			if (diatance > 40f || Life_Timer > 20f || base.transform.localScale.y < 0.02f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
		else
		{
			Glow_Circle_2.color = global::UnityEngine.Color.Lerp(Glow_Circle_2.color, new global::UnityEngine.Color(Glow_Circle_2.color.r, Glow_Circle_2.color.g, Glow_Circle_2.color.b, 0f), global::UnityEngine.Time.deltaTime * 12f);
			Glow_Border.color = global::UnityEngine.Color.Lerp(Glow_Border.color, new global::UnityEngine.Color(1f, 1f, 1f, 0f), global::UnityEngine.Time.deltaTime * 8f);
			Glow_Border.transform.localScale = global::UnityEngine.Vector3.Lerp(Glow_Border.transform.localScale, new global::UnityEngine.Vector3(1.2f, 1.2f, 1f), global::UnityEngine.Time.deltaTime * 3f);
			if (Life_Timer > 3f || Glow_Border.color.a < 0.02f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	private void Explo()
	{
		Life_Timer = 0f;
		Glow_Core.enabled = false;
		Glow_Circle_1.enabled = false;
		base.transform.localScale = new global::UnityEngine.Vector3(1f, 1f, 1f);
		Glow_Border.transform.localScale = new global::UnityEngine.Vector3(0.6f, 0.6f, 1f);
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Hit_2(base.transform.position);
	}

	private void OnTriggerStay2D(global::UnityEngine.Collider2D col)
	{
		if (col.tag == "Col_Camera")
		{
			onCam_Timer = 0.5f;
		}
		if (!GM.Paused && !onExplo)
		{
			if (col.name == "Ani" && !GM.onShield && GM.Damage_Timer <= 0f && !GM.GameOver && !GM.onEvent && !GM.onHscene && !GM.onDown)
			{
				GetComponent<global::UnityEngine.BoxCollider2D>().enabled = false;
				onExplo = true;
				int num = ((!(base.transform.position.x > col.transform.position.x)) ? 1 : (-1));
				GM.Damage(Damage, DmgForce * (float)num, false, 0);
				Explo();
			}
			else if (col.tag == "Magic_Shield" || col.tag == "Ground" || col.tag == "Gate" || col.tag == "Breakable")
			{
				GetComponent<global::UnityEngine.BoxCollider2D>().enabled = false;
				onExplo = true;
				Explo();
			}
		}
	}
}
