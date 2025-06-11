public class Trap_Laser_Beam : global::UnityEngine.MonoBehaviour
{
	public int Damage;

	public float DmgForce;

	public global::UnityEngine.SpriteRenderer spr_Glow_1;

	public global::UnityEngine.SpriteRenderer spr_Glow_2;

	public global::UnityEngine.SpriteRenderer spr_Yellow;

	private float Life_Timer;

	private float glow_Opacity_1;

	private float glow_Opacity_2;

	private float glow_Opacity_3;

	private float glow_Timer_1;

	private float glow_Timer_2;

	private float glow_Timer_3;

	private float Damage_Delay;

	private Player_Control PC;

    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		PC = global::UnityEngine.GameObject.Find("Player").GetComponent<Player_Control>();
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			Life_Timer += global::UnityEngine.Time.deltaTime;
			if (Damage_Delay > 0f)
			{
				Damage_Delay -= global::UnityEngine.Time.deltaTime;
			}
			if (glow_Timer_1 > 0.12f)
			{
				glow_Timer_1 = 0f;
				glow_Opacity_1 = global::UnityEngine.Random.Range(0.2f, 1f);
			}
			else
			{
				glow_Timer_1 += global::UnityEngine.Time.deltaTime;
			}
			if (glow_Timer_2 > 0.08f)
			{
				glow_Timer_2 = 0f;
				glow_Opacity_2 = global::UnityEngine.Random.Range(0.2f, 1f);
			}
			else
			{
				glow_Timer_2 += global::UnityEngine.Time.deltaTime;
			}
			if (glow_Timer_3 > 0.1f)
			{
				glow_Timer_3 = 0f;
				glow_Opacity_3 = global::UnityEngine.Random.Range(0f, 1f);
			}
			else
			{
				glow_Timer_3 += global::UnityEngine.Time.deltaTime;
			}
			spr_Glow_1.color = new global::UnityEngine.Color(1f, 1f, 1f, glow_Opacity_1);
			spr_Glow_2.color = new global::UnityEngine.Color(1f, 1f, 1f, glow_Opacity_2);
			spr_Yellow.color = new global::UnityEngine.Color(1f, 1f, 1f, glow_Opacity_3);
			if (Life_Timer > 1f)
			{
				GetComponent<global::UnityEngine.BoxCollider2D>().enabled = false;
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	private void OnTriggerStay2D(global::UnityEngine.Collider2D col)
	{
		if (PC.State.ToString() != "Down" && !GM.Paused && !GM.onGameClear && !GM.GameOver && col.name == "Ani" && !GM.onShield && Damage_Delay <= 0f)
		{
			if (base.transform.position.x > col.transform.parent.position.x)
			{
				GM.Damage(Damage, 0f - DmgForce, false, 0);
			}
			else
			{
				GM.Damage(Damage, DmgForce, false, 0);
			}
			Damage_Delay = 1f;
		}
	}
}
