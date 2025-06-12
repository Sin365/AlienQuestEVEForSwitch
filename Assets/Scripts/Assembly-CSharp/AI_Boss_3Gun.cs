using UnityEngine;

public class AI_Boss_3Gun : global::UnityEngine.MonoBehaviour
{
	private bool isDeath;

	private float Death_Timer;

	private int EnemyState;

	private float Life_Timer;

	private int facingRight = -1;

	private float distance;

	private float dist_X;

	private float dist_Y;

	private bool on_Chase;

	private bool on_Laser;

	private bool on_Shield;

	private int Position_Level;

	private float Fire_Timer;

	private float Combo_Timer;

	private float Laser_Timer;

	private float LaserOFF_Timer;

	private float Shield_Timer;

	private float Attack_Delay;

	private float Slow_Timer;

	private float Glow_Combo_Timer;

	private float Glow_Laser_Timer;

	public int Type;

	public global::UnityEngine.GameObject Clear_Item;

	public global::UnityEngine.Transform Body;

	public global::UnityEngine.Transform Center;

	public global::UnityEngine.Transform Top;

	public global::UnityEngine.Transform Bot;

	public global::UnityEngine.GameObject _Fire;

	public global::UnityEngine.GameObject _Laser;

	public global::UnityEngine.GameObject _Shield;

	public global::UnityEngine.Transform pos_FireCenter;

	public global::UnityEngine.Transform pos_FireTop;

	public global::UnityEngine.Transform pos_FireBot;

	private global::UnityEngine.GameObject Laser;

	public global::UnityEngine.SpriteRenderer Glow_Center_V;

	public global::UnityEngine.SpriteRenderer Glow_Center_C;

	public global::UnityEngine.SpriteRenderer Glow_Center_B;

	public global::UnityEngine.SpriteRenderer Glow_Top_V;

	public global::UnityEngine.SpriteRenderer Glow_Top_C;

	public global::UnityEngine.SpriteRenderer Glow_Top_B;

	public global::UnityEngine.SpriteRenderer Glow_Bot_V;

	public global::UnityEngine.SpriteRenderer Glow_Bot_C;

	public global::UnityEngine.SpriteRenderer Glow_Bot_B;

	public global::UnityEngine.SpriteRenderer Guide_Laser;

	private float Glow_Timer;

	private global::UnityEngine.Color Color_Center_V;

	private global::UnityEngine.Color Color_Center_C;

	private global::UnityEngine.Color Color_Center_B;

	private global::UnityEngine.Color Color_Top_V;

	private global::UnityEngine.Color Color_Top_C;

	private global::UnityEngine.Color Color_Top_B;

	public global::UnityEngine.GameObject Explo;

	private global::UnityEngine.Vector3 pos_Explo;

	private float Explo_Timer;

	private float ExploSound_Timer;

	private global::UnityEngine.Vector3 pos_Orig;

	private global::UnityEngine.Vector3 pos_Target;

	public global::UnityEngine.GameObject Sound_Gun_Fire;

	public global::UnityEngine.GameObject Sound_Explo;

	private global::UnityEngine.Animator animator;

	private UI_Control UC;

	private Monster Mon;

    Player_Control PC => GameManager.instance.PC;
    GameObject Player => GameManager.instance.gobj_Player;


    private GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//Player = global::UnityEngine.GameObject.Find("Player");
		//PC = Player.GetComponent<Player_Control>();
		Mon = GetComponent<Monster>();
		UC = global::UnityEngine.GameObject.Find("Status").GetComponent<UI_Control>();
		animator = GetComponent<global::UnityEngine.Animator>();
		pos_Orig = (pos_Target = base.transform.position);
		Color_Center_V = Glow_Center_V.color;
		Color_Center_C = Glow_Center_C.color;
		Color_Center_B = Glow_Center_B.color;
		Color_Top_V = Glow_Top_V.color;
		Color_Top_C = Glow_Top_C.color;
		Color_Top_B = Glow_Top_B.color;
		if (GM.Get_Event(13))
		{
			GetComponent<global::UnityEngine.CircleCollider2D>().enabled = false;
			if (!GM.onSkill_4)
			{
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Clear_Item, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
				gameObject.transform.parent = base.transform.parent;
				gameObject.GetComponent<Item>().Set_Target(pos_Orig);
			}
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
		else
		{
			UC.Boss_Mon = GetComponent<Monster>();
			UC.Set_Boss_Start();
		}
		Body.transform.rotation = global::UnityEngine.Quaternion.Euler(0f, 0f, 90f);
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
		base.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(0f, 0f);
		distance = global::UnityEngine.Vector3.Distance(new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f, 0f), base.transform.position);
		dist_X = global::UnityEngine.Mathf.Abs(base.transform.position.x - Player.transform.position.x);
		dist_Y = global::UnityEngine.Mathf.Abs(base.transform.position.y - (Player.transform.position.y + 2.8f));
		if (!on_Chase && dist_X < 25f && dist_Y < 15f)
		{
			on_Chase = true;
		}
		Shield_Timer += global::UnityEngine.Time.deltaTime;
		if (on_Shield && Shield_Timer > 5f)
		{
			on_Shield = false;
			Mon.isPass = false;
			Mon.isInvincible = false;
		}
		if (Life_Timer < 7f)
		{
			Look_Player(true);
		}
		else if (!isDeath)
		{
			if (Slow_Timer > 0f)
			{
				Slow_Timer -= global::UnityEngine.Time.deltaTime;
			}
			if (Slow_Timer > 0f)
			{
				Look_Player(true);
			}
			else
			{
				Look_Player(false);
			}
		}
		if (isDeath)
		{
			Death_Timer += global::UnityEngine.Time.deltaTime;
			if (Death_Timer < 2.5f)
			{
				DeathExplo();
			}
			else
			{
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Sound_Explo, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
				pos_Explo = new global::UnityEngine.Vector3(base.transform.position.x + global::UnityEngine.Random.Range(-2.3f, 2.3f), base.transform.position.y + global::UnityEngine.Random.Range(-2.3f, 2.3f), 0f);
				Make_Explo(pos_Explo);
				if (!GM.onSkill_4)
				{
					global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(Clear_Item, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
					gameObject2.transform.parent = base.transform.parent;
					gameObject2.GetComponent<Item>().Set_Target(pos_Orig);
				}
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
		else if (!GM.onEvent && !GM.GameOver && !GM.onHscene && !(distance > 35f) && !(dist_Y > 20f) && !(Life_Timer < 5f))
		{
			if (!on_Shield && Mon.Get_HitCombo() >= 3 && Shield_Timer > 8f)
			{
				Mon.Reset_HitCombo();
				Set_Shield();
				if (Position_Level == 0)
				{
					Position_Level = 1;
				}
			}
			else if (Attack_Delay > 0f)
			{
				Attack_Delay -= global::UnityEngine.Time.deltaTime;
			}
			else
			{
				Fire_Timer += global::UnityEngine.Time.deltaTime;
				Combo_Timer += global::UnityEngine.Time.deltaTime;
				Laser_Timer += global::UnityEngine.Time.deltaTime;
				if (Mon.HP_Ratio > 0.5f)
				{
					if (Combo_Timer > 6f)
					{
						Set_Attack();
					}
					else if (Fire_Timer > 2f)
					{
						Fire_Timer = 0f;
						Set_Fire();
					}
				}
				else if (Mon.HP_Ratio > 0.2f)
				{
					if (Laser_Timer > 7.5f)
					{
						Set_Attack_Laser();
					}
					else if (Combo_Timer > 2f)
					{
						Set_Attack();
					}
				}
				else if (Shield_Timer > 8f)
				{
					Set_Shield();
				}
				else if (!on_Shield && Laser_Timer > 1.5f)
				{
					Set_Attack_Laser();
				}
				else if (Combo_Timer > 1f)
				{
					Set_Attack();
				}
			}
		}
		if (Glow_Combo_Timer > 0f)
		{
			Glow_Combo_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Glow_Laser_Timer > 0f)
		{
			Glow_Laser_Timer -= global::UnityEngine.Time.deltaTime;
		}
		else
		{
			Guide_Laser.color = new global::UnityEngine.Color(0f, 1f, 1f, 0f);
		}
		Glow_Timer += global::UnityEngine.Time.deltaTime;
		Glow_Check();
		if (Position_Level == 2)
		{
			base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, new global::UnityEngine.Vector3(pos_Orig.x, pos_Orig.y + 6.5f, 0f), global::UnityEngine.Time.deltaTime * 2f);
		}
		else if (Position_Level == 1)
		{
			base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, new global::UnityEngine.Vector3(pos_Orig.x, pos_Orig.y + 3f, 0f), global::UnityEngine.Time.deltaTime * 2f);
			if (Mon.HP_Ratio <= 0.33f)
			{
				Position_Level++;
			}
		}
		else
		{
			base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, pos_Orig, global::UnityEngine.Time.deltaTime * 2f);
			if (Mon.HP_Ratio <= 0.5f)
			{
				Position_Level++;
			}
		}
		if (Laser != null)
		{
			LaserOFF_Timer += global::UnityEngine.Time.deltaTime;
			if (isDeath || LaserOFF_Timer > 3.8f)
			{
				Laser.SendMessage("End_Laser");
			}
		}
	}

	private void Look_Player(bool isSlow)
	{
		float num = 0f;
		global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f);
		global::UnityEngine.Vector3 position = base.transform.position;
		vector.x -= position.x;
		vector.y -= position.y;
		num = global::UnityEngine.Mathf.Atan2(vector.y, vector.x) * 57.29578f + 180f;
		if (isSlow)
		{
			Body.transform.rotation = global::UnityEngine.Quaternion.Lerp(Body.transform.rotation, global::UnityEngine.Quaternion.Euler(new global::UnityEngine.Vector3(0f, 0f, num)), global::UnityEngine.Time.deltaTime * 0.3f);
		}
		else
		{
			Body.transform.rotation = global::UnityEngine.Quaternion.Lerp(Body.transform.rotation, global::UnityEngine.Quaternion.Euler(new global::UnityEngine.Vector3(0f, 0f, num)), global::UnityEngine.Time.deltaTime * 3f);
		}
	}

	private void Set_Attack()
	{
		Attack_Delay = 2f;
		Fire_Timer = 0f;
		Combo_Timer = 0f;
		Glow_Combo_Timer = 1.3f;
		animator.SetBool("onAttack", true);
		animator.SetBool("onLaser", false);
		animator.SetBool("onHit", false);
	}

	private void Set_Attack_Laser()
	{
		Attack_Delay = 5f;
		Fire_Timer = 0f;
		Laser_Timer = 0f;
		Glow_Laser_Timer = 4.8f;
		animator.SetBool("onAttack", false);
		animator.SetBool("onLaser", true);
		animator.SetBool("onHit", false);
	}

	private void End_Attack()
	{
		on_Laser = false;
		animator.SetBool("onAttack", false);
		animator.SetBool("onLaser", false);
		animator.SetBool("onHit", false);
	}

	private void Set_Fire()
	{
		Slow_Timer = 0.5f;
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Fire, pos_FireTop.position, pos_FireTop.rotation) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(_Fire, pos_FireBot.position, pos_FireBot.rotation) as global::UnityEngine.GameObject;
		if (Mon.HP_Ratio <= 0.5f)
		{
			float num = 0f;
			global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f);
			global::UnityEngine.Vector3 position = pos_FireCenter.position;
			vector.x -= position.x;
			vector.y -= position.y;
			num = global::UnityEngine.Mathf.Atan2(vector.y, vector.x) * 57.29578f + 180f;
			global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(_Fire, pos_FireCenter.position, pos_FireCenter.rotation) as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject gameObject4 = global::UnityEngine.Object.Instantiate(_Fire, pos_FireCenter.position, pos_FireCenter.rotation) as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject gameObject5 = global::UnityEngine.Object.Instantiate(_Fire, pos_FireCenter.position, pos_FireCenter.rotation) as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject gameObject6 = global::UnityEngine.Object.Instantiate(_Fire, pos_FireCenter.position, pos_FireCenter.rotation) as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject gameObject7 = global::UnityEngine.Object.Instantiate(_Fire, pos_FireCenter.position, pos_FireCenter.rotation) as global::UnityEngine.GameObject;
			gameObject4.transform.Rotate(0f, 0f, 20f);
			gameObject5.transform.Rotate(0f, 0f, -20f);
			gameObject6.transform.Rotate(0f, 0f, 40f);
			gameObject7.transform.Rotate(0f, 0f, -40f);
		}
		global::UnityEngine.GameObject gameObject8 = global::UnityEngine.Object.Instantiate(Sound_Gun_Fire, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
	}

	private void Set_Laser()
	{
		on_Laser = true;
		Slow_Timer = 6f;
		LaserOFF_Timer = 0f;
		Laser = global::UnityEngine.Object.Instantiate(_Laser, pos_FireCenter.position, pos_FireCenter.rotation) as global::UnityEngine.GameObject;
		Laser.transform.parent = Body.transform;
		Laser.GetComponent<Mon_GateLaser>().MonObject = base.gameObject;
	}

	private void Set_Shield()
	{
		on_Shield = true;
		Shield_Timer = 0f;
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Shield, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
		gameObject.transform.parent = base.transform;
		gameObject.transform.localScale = new global::UnityEngine.Vector3(1.1f, 1.1f, 1f);
		Mon.isPass = true;
		Mon.isInvincible = true;
	}

	private void Glow_Check()
	{
		if (Glow_Timer > 0.1f)
		{
			Glow_Timer = 0f;
			if (Glow_Laser_Timer > 0f)
			{
				Color_Center_V = new global::UnityEngine.Color(Color_Center_V.r, Color_Center_V.g, Color_Center_V.b, global::UnityEngine.Random.Range(0.4f, 1f));
				Color_Center_C = new global::UnityEngine.Color(Color_Center_C.r, Color_Center_C.g, Color_Center_C.b, global::UnityEngine.Random.Range(0.5f, 1f));
				Color_Top_V = new global::UnityEngine.Color(Color_Top_V.r, Color_Top_V.g, Color_Top_V.b, global::UnityEngine.Random.Range(0.1f, 0.3f));
				Color_Top_C = new global::UnityEngine.Color(Color_Top_C.r, Color_Top_C.g, Color_Top_C.b, global::UnityEngine.Random.Range(0.2f, 0.4f));
				Guide_Laser.color = new global::UnityEngine.Color(0f, 1f, 1f, global::UnityEngine.Random.Range(0.1f, 0.5f));
			}
			else if (Glow_Combo_Timer > 0f)
			{
				if (Mon.HP_Ratio <= 0.5f)
				{
					Color_Center_V = new global::UnityEngine.Color(Color_Center_V.r, Color_Center_V.g, Color_Center_V.b, global::UnityEngine.Random.Range(0.4f, 1f));
					Color_Center_C = new global::UnityEngine.Color(Color_Center_C.r, Color_Center_C.g, Color_Center_C.b, global::UnityEngine.Random.Range(0.5f, 1f));
				}
				else
				{
					Color_Center_V = new global::UnityEngine.Color(Color_Center_V.r, Color_Center_V.g, Color_Center_V.b, global::UnityEngine.Random.Range(0.01f, 0.1f));
					Color_Center_C = new global::UnityEngine.Color(Color_Center_C.r, Color_Center_C.g, Color_Center_C.b, global::UnityEngine.Random.Range(0.1f, 0.2f));
				}
				Color_Top_V = new global::UnityEngine.Color(Color_Top_V.r, Color_Top_V.g, Color_Top_V.b, global::UnityEngine.Random.Range(0.4f, 1f));
				Color_Top_C = new global::UnityEngine.Color(Color_Top_C.r, Color_Top_C.g, Color_Top_C.b, global::UnityEngine.Random.Range(0.5f, 1f));
			}
			else
			{
				Color_Center_V = new global::UnityEngine.Color(Color_Center_V.r, Color_Center_V.g, Color_Center_V.b, global::UnityEngine.Random.Range(0.01f, 0.1f));
				Color_Center_C = new global::UnityEngine.Color(Color_Center_C.r, Color_Center_C.g, Color_Center_C.b, global::UnityEngine.Random.Range(0.1f, 0.2f));
				if (Fire_Timer > 1.4f && Mon.HP_Ratio > 0.5f)
				{
					Color_Top_V = new global::UnityEngine.Color(Color_Top_V.r, Color_Top_V.g, Color_Top_V.b, global::UnityEngine.Random.Range(0.4f, 1f));
					Color_Top_C = new global::UnityEngine.Color(Color_Top_C.r, Color_Top_C.g, Color_Top_C.b, global::UnityEngine.Random.Range(0.5f, 1f));
				}
				else
				{
					Color_Top_V = new global::UnityEngine.Color(Color_Top_V.r, Color_Top_V.g, Color_Top_V.b, global::UnityEngine.Random.Range(0.01f, 0.1f));
					Color_Top_C = new global::UnityEngine.Color(Color_Top_C.r, Color_Top_C.g, Color_Top_C.b, global::UnityEngine.Random.Range(0.1f, 0.2f));
				}
			}
		}
		Glow_Center_V.color = global::UnityEngine.Color.Lerp(Glow_Center_V.color, Color_Center_V, global::UnityEngine.Time.deltaTime * 10f);
		Glow_Center_C.color = global::UnityEngine.Color.Lerp(Glow_Center_C.color, Color_Center_C, global::UnityEngine.Time.deltaTime * 10f);
		global::UnityEngine.SpriteRenderer glow_Bot_V = Glow_Bot_V;
		global::UnityEngine.Color color = global::UnityEngine.Color.Lerp(Glow_Top_V.color, Color_Top_V, global::UnityEngine.Time.deltaTime * 10f);
		Glow_Top_V.color = color;
		glow_Bot_V.color = color;
		global::UnityEngine.SpriteRenderer glow_Bot_C = Glow_Bot_C;
		color = global::UnityEngine.Color.Lerp(Glow_Top_C.color, Color_Top_C, global::UnityEngine.Time.deltaTime * 10f);
		Glow_Top_C.color = color;
		glow_Bot_C.color = color;
		if (Glow_Laser_Timer > 0f || Glow_Combo_Timer > 0f)
		{
			global::UnityEngine.SpriteRenderer glow_Center_B = Glow_Center_B;
			color = global::UnityEngine.Color.Lerp(Glow_Top_B.color, new global::UnityEngine.Color(1f, 1f, 1f, 1f), global::UnityEngine.Time.deltaTime);
			Glow_Top_B.color = color;
			color = color;
			Glow_Bot_B.color = color;
			glow_Center_B.color = color;
		}
		else
		{
			global::UnityEngine.SpriteRenderer glow_Center_B2 = Glow_Center_B;
			color = global::UnityEngine.Color.Lerp(Glow_Top_B.color, new global::UnityEngine.Color(1f, 1f, 1f, 0.5f), global::UnityEngine.Time.deltaTime);
			Glow_Top_B.color = color;
			color = color;
			Glow_Bot_B.color = color;
			glow_Center_B2.color = color;
		}
	}

	private void Set_AttackDelay()
	{
	}

	private void Position_Reset()
	{
		Position_Level = 0;
	}

	public void Set_Death()
	{
		isDeath = true;
		Death_Timer = 0f;
		animator.SetBool("onDeath", true);
		GetComponent<global::UnityEngine.CircleCollider2D>().enabled = false;
		UC.Set_Boss_Death();
		DeathExplo();
		global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().Set_Shake_Timer(2.5f, global::UnityEngine.GameObject.Find("Main Camera").transform.position);
	}

	private void DeathExplo()
	{
		if (ExploSound_Timer <= 0f)
		{
			ExploSound_Timer = global::UnityEngine.Random.Range(0.2f, 0.5f);
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Sound_Explo, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
		}
		else
		{
			ExploSound_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Explo_Timer <= 0f)
		{
			Explo_Timer = 0.01f;
			pos_Explo = new global::UnityEngine.Vector3(base.transform.position.x + global::UnityEngine.Random.Range(-2.3f, 2.3f), base.transform.position.y + global::UnityEngine.Random.Range(-2.3f, 2.3f), 0f);
			Make_Explo(pos_Explo);
		}
		else
		{
			Explo_Timer -= global::UnityEngine.Time.deltaTime;
		}
	}

	private void Make_Explo(global::UnityEngine.Vector3 pos)
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Explo, pos, base.transform.rotation) as global::UnityEngine.GameObject;
	}
}
