public class Queen_Laser : global::UnityEngine.MonoBehaviour
{
	public int Damage = 30;

	public float DmgForce = 20f;

	public global::UnityEngine.SpriteRenderer Glow_Bar;

	public global::UnityEngine.SpriteRenderer Glow_1;

	public global::UnityEngine.SpriteRenderer Glow_2;

	public global::UnityEngine.SpriteRenderer Glow_3;

	public global::UnityEngine.SpriteRenderer Glow_4;

	public global::UnityEngine.SpriteRenderer Glow_tip;

	public int facingRight = -1;

	public float Rot_Ratio = 1f;

	public float Life_Timer;

	private float Size_Timer;

	private float Glow_Timer;

	private float Size_Y = 1.5f;

	private float Glow_Opacity = 1f;

	private global::UnityEngine.Color glow_color = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private float Damage_Delay;

	private float Rot_Speed;
    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		glow_color = Glow_1.color;
		if (GM.EventState == 200)
		{
			glow_color = new global::UnityEngine.Color(1f, 0f, 0f, 1f);
			Glow_Bar.color = new global::UnityEngine.Color(1f, 0.3f, 0f, 1f);
			Size_Y = 0.3f;
		}
		else
		{
			glow_color = new global::UnityEngine.Color(1f, 0.1f, 0f, 1f);
			Glow_Bar.color = new global::UnityEngine.Color(1f, 0.3f, 0f, 1f);
			global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().Set_Shake_Timer(4.1f, global::UnityEngine.GameObject.Find("Main Camera").transform.position);
		}
		Glow_Bar.transform.localScale = new global::UnityEngine.Vector3(9.5f, 0f, 1f);
		Glow_1.color = new global::UnityEngine.Color(glow_color.r, glow_color.g, glow_color.b, 0f);
		global::UnityEngine.SpriteRenderer glow_ = Glow_4;
		global::UnityEngine.Color color = Glow_1.color;
		Glow_2.color = color;
		color = color;
		Glow_3.color = color;
		glow_.color = color;
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (Life_Timer < 0f)
		{
			return;
		}
		if (GM.EventState != 200 && !GetComponent<global::UnityEngine.BoxCollider2D>().enabled)
		{
			GetComponent<global::UnityEngine.BoxCollider2D>().enabled = true;
		}
		Size_Timer += global::UnityEngine.Time.deltaTime;
		Glow_Timer += global::UnityEngine.Time.deltaTime;
		if (Damage_Delay > 0f)
		{
			Damage_Delay -= global::UnityEngine.Time.deltaTime;
		}
		if (Life_Timer > 3.9f)
		{
			Glow_Bar.transform.localScale = global::UnityEngine.Vector3.Lerp(Glow_Bar.transform.localScale, new global::UnityEngine.Vector3(0f, 0f, 1f), global::UnityEngine.Time.deltaTime * 20f);
			Glow_1.transform.localScale = global::UnityEngine.Vector3.Lerp(Glow_1.transform.localScale, new global::UnityEngine.Vector3(Glow_1.transform.localScale.x, 0f, 1f), global::UnityEngine.Time.deltaTime * 20f);
			Glow_2.transform.localScale = global::UnityEngine.Vector3.Lerp(Glow_2.transform.localScale, new global::UnityEngine.Vector3(Glow_2.transform.localScale.x, 0f, 1f), global::UnityEngine.Time.deltaTime * 20f);
			Glow_3.transform.localScale = global::UnityEngine.Vector3.Lerp(Glow_3.transform.localScale, new global::UnityEngine.Vector3(Glow_3.transform.localScale.x, 0f, 1f), global::UnityEngine.Time.deltaTime * 20f);
			Glow_4.transform.localScale = global::UnityEngine.Vector3.Lerp(Glow_4.transform.localScale, new global::UnityEngine.Vector3(Glow_4.transform.localScale.x, 0f, 1f), global::UnityEngine.Time.deltaTime * 20f);
			Glow_tip.transform.localScale = global::UnityEngine.Vector3.Lerp(Glow_tip.transform.localScale, new global::UnityEngine.Vector3(Glow_tip.transform.localScale.x, 0f, 1f), global::UnityEngine.Time.deltaTime * 20f);
			if (GetComponent<global::UnityEngine.BoxCollider2D>().enabled)
			{
				GetComponent<global::UnityEngine.BoxCollider2D>().enabled = false;
			}
			if (Glow_Bar.transform.localScale.x < 0.05f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
		else
		{
			if (Size_Timer > 0.04f)
			{
				Size_Timer = 0f;
				if (GM.Get_Event(3))
				{
					Size_Y = global::UnityEngine.Random.Range(0.02f, 0.5f);
				}
				else
				{
					Size_Y = global::UnityEngine.Random.Range(0.8f, 1.3f);
				}
			}
			if (Glow_Timer > 0.1f)
			{
				Glow_Timer = 0f;
				Glow_Opacity = 0.7f + (float)global::UnityEngine.Random.Range(0, 30) * 0.01f;
				glow_color = new global::UnityEngine.Color(glow_color.r, glow_color.g, glow_color.b, Glow_Opacity);
			}
			Glow_Bar.transform.localScale = new global::UnityEngine.Vector3(9.5f, Size_Y, 1f);
			Glow_1.color = global::UnityEngine.Color.Lerp(Glow_1.color, glow_color, global::UnityEngine.Time.deltaTime * 10f);
			global::UnityEngine.SpriteRenderer glow_ = Glow_4;
			global::UnityEngine.Color color = Glow_1.color;
			Glow_2.color = color;
			color = color;
			Glow_3.color = color;
			glow_.color = color;
		}
		if (Rot_Ratio > 0f)
		{
			if (Life_Timer < 0.5f)
			{
				Rot_Speed = global::UnityEngine.Mathf.Lerp(Rot_Speed, 20f, global::UnityEngine.Time.deltaTime * 2f);
			}
			else
			{
				Rot_Speed = global::UnityEngine.Mathf.Lerp(Rot_Speed, 35f, global::UnityEngine.Time.deltaTime * 4f);
			}
			base.transform.Rotate(0f, 0f, global::UnityEngine.Time.deltaTime * Rot_Speed * Rot_Ratio * (float)facingRight);
		}
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
			else if (col.name == "Ani" && !GM.onShield && Damage_Delay <= 0f && !GM.onEvent && !GM.onHscene)
			{
				Damage_Delay = 0.5f;
				int num = ((!(base.transform.position.x > col.transform.position.x)) ? 1 : (-1));
				GM.Damage(Damage, DmgForce * (float)num, false, 0);
				GameManager.instance.sc_Sound_List.Electric_Dmg(base.transform.position);
			}
		}
	}
}
