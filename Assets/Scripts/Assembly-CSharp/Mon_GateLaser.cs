public class Mon_GateLaser : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject MonObject;

	public global::UnityEngine.SpriteRenderer Glow_Bar;

	public global::UnityEngine.SpriteRenderer Glow_1;

	public global::UnityEngine.SpriteRenderer Glow_2;

	public global::UnityEngine.SpriteRenderer Glow_3;

	public global::UnityEngine.SpriteRenderer Glow_4;

	public int Damage = 100;

	public float Life_Timer;

	private float Size_Timer;

	private float Glow_Timer;

	private float Size_Y = 1.5f;

	private float Glow_Opacity = 1f;

	private global::UnityEngine.Color glow_color = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private float Damage_Delay;

	private bool onEnd;

    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		Glow_Bar.transform.localScale = new global::UnityEngine.Vector3(0.4f, 1.5f, 1f);
		glow_color = Glow_1.color;
		UnityEngine.Camera.main.GetComponent<Camera_Control>().Set_Shake_Timer(3f, UnityEngine.Camera.main.transform.position);
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
		Size_Timer += global::UnityEngine.Time.deltaTime;
		Glow_Timer += global::UnityEngine.Time.deltaTime;
		if (Damage_Delay > 0f)
		{
			Damage_Delay -= global::UnityEngine.Time.deltaTime;
		}
		if (onEnd || Life_Timer > 5f || MonObject == null)
		{
			Glow_Bar.transform.localScale = global::UnityEngine.Vector3.Lerp(Glow_Bar.transform.localScale, new global::UnityEngine.Vector3(0f, 0f, 1f), global::UnityEngine.Time.deltaTime * 20f);
			Glow_1.transform.localScale = global::UnityEngine.Vector3.Lerp(Glow_1.transform.localScale, new global::UnityEngine.Vector3(Glow_1.transform.localScale.x, 0f, 1f), global::UnityEngine.Time.deltaTime * 20f);
			Glow_2.transform.localScale = global::UnityEngine.Vector3.Lerp(Glow_2.transform.localScale, new global::UnityEngine.Vector3(Glow_2.transform.localScale.x, 0f, 1f), global::UnityEngine.Time.deltaTime * 20f);
			Glow_3.transform.localScale = global::UnityEngine.Vector3.Lerp(Glow_3.transform.localScale, new global::UnityEngine.Vector3(Glow_3.transform.localScale.x, 0f, 1f), global::UnityEngine.Time.deltaTime * 20f);
			Glow_4.transform.localScale = global::UnityEngine.Vector3.Lerp(Glow_4.transform.localScale, new global::UnityEngine.Vector3(Glow_4.transform.localScale.x, 0f, 1f), global::UnityEngine.Time.deltaTime * 20f);
			if (Glow_Bar.transform.localScale.x < 0.05f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			return;
		}
		if (Size_Timer > 0.04f)
		{
			Size_Timer = 0f;
			Size_Y = global::UnityEngine.Random.Range(0.5f, 4f);
		}
		if (Glow_Timer > 0.1f)
		{
			Glow_Timer = 0f;
			Glow_Opacity = 0.7f + (float)global::UnityEngine.Random.Range(0, 30) * 0.01f;
			glow_color = new global::UnityEngine.Color(glow_color.r, glow_color.g, glow_color.b, Glow_Opacity);
		}
		Glow_Bar.transform.localScale = global::UnityEngine.Vector3.Lerp(Glow_Bar.transform.localScale, new global::UnityEngine.Vector3(5.4f, Size_Y, 1f), global::UnityEngine.Time.deltaTime * 10f);
		Glow_1.color = global::UnityEngine.Color.Lerp(Glow_1.color, glow_color, global::UnityEngine.Time.deltaTime * 10f);
		global::UnityEngine.SpriteRenderer glow_ = Glow_4;
		global::UnityEngine.Color color = Glow_1.color;
		Glow_2.color = color;
		color = color;
		Glow_3.color = color;
		glow_.color = color;
	}

	private void End_Laser()
	{
		onEnd = true;
		GetComponent<global::UnityEngine.BoxCollider2D>().enabled = false;
	}

	private void OnTriggerStay2D(global::UnityEngine.Collider2D col)
	{
		if (!GM.Paused && !GM.GameOver)
		{
			if (col.tag == "Magic_Shield")
			{
				Damage_Delay = 0.25f;
				GM.Break_Shield();
				global::UnityEngine.Debug.Log("Break_Shield");
			}
			else if (col.name == "Ani" && !GM.onShield && Damage_Delay <= 0f)
			{
				Damage_Delay = 0.5f;
				int num = ((!(base.transform.position.x > col.transform.position.x)) ? 1 : (-1));
				GM.Damage(Damage, 20 * num, false, 0);
				GameManager.instance.sc_Sound_List.Electric_Dmg(base.transform.position);
			}
		}
	}
}
