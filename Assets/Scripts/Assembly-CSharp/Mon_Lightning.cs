public class Mon_Lightning : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.Sprite spr_1;

	public global::UnityEngine.Sprite spr_2;

	public global::UnityEngine.Sprite spr_3;

	public global::UnityEngine.Sprite spr_4;

	public global::UnityEngine.Sprite spr_5;

	public global::UnityEngine.Sprite spr_6;

	public global::UnityEngine.Sprite spr_7;

	public global::UnityEngine.SpriteRenderer SR_Glow;

	public int Mon_Num;

	private int Ani_Index = 1;

	private float Ani_Timer;

	private float Func_Timer;

	private float Life_Timer;

	private float Attack_Delay;

	private int facingRight = 1;

	private global::UnityEngine.SpriteRenderer SR;

	GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		SR = GetComponent<global::UnityEngine.SpriteRenderer>();
		GetComponent<global::UnityEngine.BoxCollider2D>().enabled = false;
		if (base.transform.localScale.x < 0f)
		{
			facingRight = -1;
		}
		SR.sprite = spr_1;
		SR_Glow.color = new global::UnityEngine.Color(SR_Glow.color.r, SR_Glow.color.g, SR_Glow.color.b, 0f);
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Plasma_Atk(base.transform.position);
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (Attack_Delay > 0f)
		{
			Attack_Delay -= global::UnityEngine.Time.deltaTime;
		}
		if (Ani_Timer > 0.02f)
		{
			Ani_Timer = 0f;
			Ani_Index++;
			switch (Ani_Index)
			{
			case 2:
				SR.sprite = spr_2;
				SR_Glow.color = new global::UnityEngine.Color(SR_Glow.color.r, SR_Glow.color.g, SR_Glow.color.b, 0.25f);
				GetComponent<global::UnityEngine.BoxCollider2D>().enabled = true;
				break;
			case 3:
				SR.sprite = spr_3;
				SR_Glow.color = new global::UnityEngine.Color(SR_Glow.color.r, SR_Glow.color.g, SR_Glow.color.b, 1f);
				break;
			case 4:
				SR.sprite = spr_4;
				break;
			case 5:
				SR.sprite = spr_3;
				break;
			case 6:
				SR.sprite = spr_4;
				break;
			case 7:
				SR.sprite = spr_5;
				SR_Glow.color = new global::UnityEngine.Color(SR_Glow.color.r, SR_Glow.color.g, SR_Glow.color.b, 0.5f);
				break;
			case 8:
				SR.sprite = spr_6;
				SR_Glow.color = new global::UnityEngine.Color(SR_Glow.color.r, SR_Glow.color.g, SR_Glow.color.b, 0.1f);
				GetComponent<global::UnityEngine.BoxCollider2D>().enabled = false;
				break;
			case 9:
				SR.sprite = spr_7;
				SR_Glow.color = new global::UnityEngine.Color(SR_Glow.color.r, SR_Glow.color.g, SR_Glow.color.b, 0f);
				break;
			case 10:
				Ani_Index = 1;
				Destroy_Self();
				break;
			}
		}
		else
		{
			Ani_Timer += global::UnityEngine.Time.deltaTime;
		}
		if (Life_Timer > 2.5f)
		{
			Destroy_Self();
		}
	}

	private void Destroy_Self()
	{
		global::UnityEngine.Object.Destroy(base.gameObject);
	}

	private void OnTriggerStay2D(global::UnityEngine.Collider2D col)
	{
		if (!GM.Paused && !GM.GameOver)
		{
			if (col.tag == "Magic_Shield")
			{
				Attack_Delay = 0.5f;
				GetComponent<global::UnityEngine.BoxCollider2D>().enabled = false;
				GM.Break_Shield();
				Destroy_Self();
			}
			else if (col.name == "Ani" && !GM.onShield && Attack_Delay <= 0f && !GM.onEvent && !GM.onHscene)
			{
				Attack_Delay = 0.5f;
				int num = ((!(base.transform.position.x > col.transform.position.x)) ? 1 : (-1));
				GM.Damage(80, 10 * num, true, Mon_Num);
				global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Electric_Dmg(base.transform.position);
			}
		}
	}
}
