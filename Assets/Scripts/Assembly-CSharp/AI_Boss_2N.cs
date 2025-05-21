public class AI_Boss_2N : global::UnityEngine.MonoBehaviour
{
	private bool isCleared;

	private bool isDeath;

	private float Death_Timer;

	private float Life_Timer;

	private int facingRight = -1;

	private float distance;

	private bool Range_Attack;

	private int Attack_Num;

	private float Attack_Delay = 2f;

	private float Poison_Delay = 2f;

	private float Flip_Delay;

	public global::UnityEngine.SpriteRenderer SR_Ball_Yeollow;

	public global::UnityEngine.SpriteRenderer SR_Ball_Purple;

	public global::UnityEngine.SpriteRenderer SR_Ball_Glow;

	public global::UnityEngine.SpriteRenderer SR_Glow_Yellow;

	public global::UnityEngine.SpriteRenderer SR_Glow_Purple;

	public global::UnityEngine.SpriteRenderer SR_Glow_Light;

	public global::UnityEngine.GameObject _Fire;

	public global::UnityEngine.GameObject _Fire_Poison;

	public global::UnityEngine.Transform pos_Fire;

	private global::UnityEngine.Color color_OFF = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	private global::UnityEngine.Color color_ON = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	public global::UnityEngine.GameObject Explo;

	public global::UnityEngine.Transform[] explo_Pos;

	private float ExploSound_Timer;

	private float[] Explo_Timer = new float[6];

	public H_Mon7[] Mon_7;

	private bool is_Mon7_Free;

	private float Mon7_Timer;

	public global::UnityEngine.GameObject Clear_Item;

	public global::UnityEngine.GameObject[] Tentacle;

	private UI_Control UC;

	private Monster Mon;

	private Player_Control PC;

	private global::UnityEngine.GameObject Player;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		Player = global::UnityEngine.GameObject.Find("Player");
		PC = Player.GetComponent<Player_Control>();
		Mon = GetComponent<Monster>();
		UC = global::UnityEngine.GameObject.Find("Status").GetComponent<UI_Control>();
		SR_Ball_Yeollow.color = color_OFF;
		SR_Ball_Purple.color = color_OFF;
		SR_Ball_Glow.color = new global::UnityEngine.Color(1f, 0.5f, 0f, 0f);
		SR_Glow_Yellow.color = color_OFF;
		SR_Glow_Purple.color = color_OFF;
		SR_Glow_Light.color = color_OFF;
		if (global::UnityEngine.GameObject.Find("Player").transform.position.x > base.transform.position.x)
		{
			Flip();
		}
		if (GM.Get_Event(12))
		{
			isCleared = true;
			GetComponent<global::UnityEngine.BoxCollider2D>().enabled = false;
			if (!GM.onSkill_3)
			{
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Clear_Item, pos_Fire.position, base.transform.rotation) as global::UnityEngine.GameObject;
				gameObject.transform.parent = base.transform.parent;
				gameObject.GetComponent<Item>().Set_Target(new global::UnityEngine.Vector3(pos_Fire.position.x, pos_Fire.position.y - 4f, 0f));
			}
			if (Mon_7.Length > 0)
			{
				for (int i = 0; i < Mon_7.Length; i++)
				{
					global::UnityEngine.Object.Destroy(Mon_7[i].gameObject);
				}
			}
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
		else
		{
			UC.Boss_Mon = GetComponent<Monster>();
			UC.Set_Boss_Start();
		}
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (Mon7_Timer > 0f)
		{
			Mon7_Timer -= global::UnityEngine.Time.deltaTime;
		}
		distance = global::UnityEngine.Vector3.Distance(new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f, 0f), pos_Fire.position);
		if (isCleared || (GM.onEvent && !GM.onHscene))
		{
			return;
		}
		if (isDeath)
		{
			Death_Timer += global::UnityEngine.Time.deltaTime;
			if (Death_Timer < 2.5f)
			{
				DeathExplo();
				return;
			}
			isCleared = true;
			GetComponent<global::UnityEngine.BoxCollider2D>().enabled = false;
			global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Explo(base.transform.position);
			for (int i = 0; i < explo_Pos.Length; i++)
			{
				Make_Explo(explo_Pos[i]);
			}
			if (!GM.onSkill_3)
			{
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Clear_Item, pos_Fire.position, base.transform.rotation) as global::UnityEngine.GameObject;
				gameObject.transform.parent = base.transform.parent;
				gameObject.GetComponent<Item>().Set_Target(new global::UnityEngine.Vector3(pos_Fire.position.x, pos_Fire.position.y - 4f, 0f));
			}
			Mon_7_Free();
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
		else if (distance > 30f || Life_Timer < 5f)
		{
			Attack_Num = 0;
			Attack_Delay = 1f;
			SR_Ball_Yeollow.color = global::UnityEngine.Color.Lerp(SR_Ball_Yeollow.color, color_OFF, global::UnityEngine.Time.deltaTime * 1f);
			SR_Ball_Purple.color = global::UnityEngine.Color.Lerp(SR_Ball_Purple.color, color_OFF, global::UnityEngine.Time.deltaTime * 1f);
			SR_Ball_Glow.color = global::UnityEngine.Color.Lerp(SR_Ball_Glow.color, new global::UnityEngine.Color(1f, 0.5f, 0f, 0f), global::UnityEngine.Time.deltaTime * 1f);
			SR_Glow_Yellow.color = global::UnityEngine.Color.Lerp(SR_Glow_Yellow.color, color_OFF, global::UnityEngine.Time.deltaTime * 1f);
			SR_Glow_Purple.color = global::UnityEngine.Color.Lerp(SR_Glow_Purple.color, color_OFF, global::UnityEngine.Time.deltaTime * 1f);
			SR_Glow_Light.color = global::UnityEngine.Color.Lerp(SR_Glow_Light.color, color_OFF, global::UnityEngine.Time.deltaTime * 1f);
		}
		else
		{
			if (GetComponent<global::UnityEngine.Animator>().GetBool("onAttack"))
			{
				return;
			}
			if (GetComponent<global::UnityEngine.Animator>().GetBool("onHit"))
			{
				if (Attack_Num == 3)
				{
					Attack_Num = 0;
					Poison_Delay = 2f;
					SR_Ball_Purple.color = color_OFF;
					SR_Ball_Glow.color = new global::UnityEngine.Color(1f, 0.5f, 0f, 0f);
					SR_Glow_Purple.color = color_OFF;
					SR_Glow_Light.color = color_OFF;
				}
			}
			else
			{
				if (GM.GameOver || GM.onHscene)
				{
					return;
				}
				if (facingRight > 0 && base.transform.position.x > Player.transform.position.x)
				{
					Flip();
				}
				else if (facingRight < 0 && base.transform.position.x < Player.transform.position.x)
				{
					Flip();
				}
				if (Mon.HP_Ratio > 0.5f)
				{
					if (Attack_Delay > 0f)
					{
						Attack_Delay -= global::UnityEngine.Time.deltaTime;
					}
					SR_Ball_Yeollow.color = global::UnityEngine.Color.Lerp(SR_Ball_Yeollow.color, color_ON, global::UnityEngine.Time.deltaTime * 0.3f);
					SR_Ball_Glow.color = global::UnityEngine.Color.Lerp(SR_Ball_Glow.color, new global::UnityEngine.Color(1f, 0.5f, 0f, 1f), global::UnityEngine.Time.deltaTime * 0.6f);
					SR_Glow_Yellow.color = SR_Ball_Yeollow.color;
					SR_Glow_Light.color = SR_Ball_Glow.color;
					if (Attack_Delay <= 0f)
					{
						Set_Fire();
					}
				}
				else if (Mon.HP_Ratio > 0.2f)
				{
					if (Attack_Num < 3)
					{
						if (Attack_Delay > 0f)
						{
							Attack_Delay -= global::UnityEngine.Time.deltaTime;
						}
						SR_Ball_Yeollow.color = global::UnityEngine.Color.Lerp(SR_Ball_Yeollow.color, color_ON, global::UnityEngine.Time.deltaTime * 0.3f);
						SR_Ball_Glow.color = global::UnityEngine.Color.Lerp(SR_Ball_Glow.color, new global::UnityEngine.Color(1f, 0.5f, 0f, 1f), global::UnityEngine.Time.deltaTime * 0.6f);
						SR_Glow_Yellow.color = SR_Ball_Yeollow.color;
						SR_Glow_Light.color = SR_Ball_Glow.color;
						if (Attack_Delay <= 0f)
						{
							Set_Fire_360();
						}
						if (Attack_Num == 3)
						{
							Attack_Delay = 2f;
						}
					}
					else
					{
						if (Poison_Delay > 0f)
						{
							Poison_Delay -= global::UnityEngine.Time.deltaTime;
						}
						SR_Ball_Purple.color = global::UnityEngine.Color.Lerp(SR_Ball_Purple.color, color_ON, global::UnityEngine.Time.deltaTime * 1f);
						SR_Ball_Glow.color = global::UnityEngine.Color.Lerp(SR_Ball_Glow.color, new global::UnityEngine.Color(0.2f, 0f, 1f, 1f), global::UnityEngine.Time.deltaTime * 0.1f);
						SR_Glow_Purple.color = SR_Ball_Purple.color;
						SR_Glow_Light.color = global::UnityEngine.Color.Lerp(SR_Glow_Light.color, new global::UnityEngine.Color(0.2f, 0f, 1f, 1f), global::UnityEngine.Time.deltaTime * 0.2f);
						if (Poison_Delay <= 0f)
						{
							Set_Attack();
						}
					}
				}
				else if (Attack_Num < 3)
				{
					if (Attack_Delay > 0f)
					{
						Attack_Delay -= global::UnityEngine.Time.deltaTime;
					}
					SR_Ball_Yeollow.color = global::UnityEngine.Color.Lerp(SR_Ball_Yeollow.color, color_ON, global::UnityEngine.Time.deltaTime * 0.3f);
					SR_Ball_Glow.color = global::UnityEngine.Color.Lerp(SR_Ball_Glow.color, new global::UnityEngine.Color(1f, 0.5f, 0f, 1f), global::UnityEngine.Time.deltaTime * 0.6f);
					SR_Glow_Yellow.color = SR_Ball_Yeollow.color;
					SR_Glow_Light.color = SR_Ball_Glow.color;
					if (Attack_Delay <= 0f)
					{
						Set_Fire_360();
						Attack_Delay = 0.8f;
						Poison_Delay = 1f;
					}
				}
				else
				{
					if (Poison_Delay > 0f)
					{
						Poison_Delay -= global::UnityEngine.Time.deltaTime;
					}
					SR_Ball_Purple.color = global::UnityEngine.Color.Lerp(SR_Ball_Purple.color, color_ON, global::UnityEngine.Time.deltaTime * 1f);
					SR_Ball_Glow.color = global::UnityEngine.Color.Lerp(SR_Ball_Glow.color, new global::UnityEngine.Color(0.2f, 0f, 1f, 1f), global::UnityEngine.Time.deltaTime * 0.1f);
					SR_Glow_Purple.color = SR_Ball_Purple.color;
					SR_Glow_Light.color = global::UnityEngine.Color.Lerp(SR_Glow_Light.color, new global::UnityEngine.Color(0.2f, 0f, 1f, 1f), global::UnityEngine.Time.deltaTime * 0.2f);
					if (Poison_Delay <= 0f)
					{
						Set_Attack();
						Poison_Delay = 1f;
						Attack_Delay = 2f;
					}
				}
			}
		}
	}

	private void Flip()
	{
		facingRight = -facingRight;
		GetComponent<Monster>().Flip_Pos();
	}

	private void Set_Attack()
	{
		Poison_Delay = 2f;
		Attack_Num = 0;
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", true);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
	}

	private void End_Attack()
	{
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
	}

	private void Set_AttackDelay()
	{
	}

	private void Mon_7_Free()
	{
		if (!is_Mon7_Free && Mon_7.Length > 0)
		{
			for (int i = 0; i < Mon_7.Length; i++)
			{
				Mon_7[i].GetComponent<H_Mon7>().Set_Index(i + 10);
				Mon_7[i].SendMessage("Get_Free");
			}
			is_Mon7_Free = true;
		}
	}

	private void Mon_7_CumShot()
	{
		if (!isDeath && Mon7_Timer <= 0f && Mon_7.Length > 0)
		{
			for (int i = 0; i < Mon_7.Length; i++)
			{
				Mon_7[i].SendMessage("CumShot");
			}
			Mon7_Timer = 0.7f;
		}
	}

	private void Mon_7_Death()
	{
		if (Mon_7.Length > 0)
		{
			for (int i = 0; i < Mon_7.Length; i++)
			{
				Mon_7[i].GetComponent<global::UnityEngine.Animator>().SetBool("isDeath", true);
			}
		}
	}

	public void Set_Death()
	{
		isDeath = true;
		Death_Timer = 0f;
		GetComponent<global::UnityEngine.Animator>().SetTrigger("onDeath");
		GetComponent<global::UnityEngine.BoxCollider2D>().enabled = false;
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_9_Damage(base.transform.position);
		UC.Set_Boss_Death();
		Mon_7_Death();
		DeathExplo();
		global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().Set_Shake_Timer(2.5f, global::UnityEngine.GameObject.Find("Main Camera").transform.position);
	}

	private void DeathExplo()
	{
		if (ExploSound_Timer <= 0f)
		{
			ExploSound_Timer = global::UnityEngine.Random.Range(0.2f, 0.5f);
			global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Explo(base.transform.position);
		}
		else
		{
			ExploSound_Timer -= global::UnityEngine.Time.deltaTime;
		}
		for (int i = 0; i < Explo_Timer.Length; i++)
		{
			if (Explo_Timer[i] <= 0f)
			{
				Explo_Timer[i] = global::UnityEngine.Random.Range(0.1f, 0.8f);
				Make_Explo(explo_Pos[i]);
			}
			else
			{
				Explo_Timer[i] -= global::UnityEngine.Time.deltaTime;
			}
		}
	}

	private void Make_Explo(global::UnityEngine.Transform posObj)
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Explo, posObj.position, posObj.transform.rotation) as global::UnityEngine.GameObject;
		gameObject.transform.localScale = posObj.transform.localScale;
	}

	private void Set_Fire()
	{
		float num = 0f;
		Attack_Delay = 2f;
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, base.transform.rotation) as global::UnityEngine.GameObject;
		global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.4f);
		global::UnityEngine.Vector3 position = gameObject.transform.position;
		vector.x -= position.x;
		vector.y -= position.y;
		num = global::UnityEngine.Mathf.Atan2(vector.y, vector.x) * 57.29578f;
		gameObject.transform.rotation = global::UnityEngine.Quaternion.Euler(new global::UnityEngine.Vector3(0f, 0f, num + 180f));
		global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(new global::UnityEngine.Vector3(0f, 0f, num + 200f))) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(new global::UnityEngine.Vector3(0f, 0f, num + 160f))) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Boss_4_Fire(pos_Fire.position);
		SR_Ball_Yeollow.color = color_OFF;
		SR_Ball_Purple.color = color_OFF;
		SR_Ball_Glow.color = new global::UnityEngine.Color(1f, 0.5f, 0f, 0f);
		SR_Glow_Yellow.color = color_OFF;
		SR_Glow_Purple.color = color_OFF;
		SR_Glow_Light.color = color_OFF;
	}

	private void Set_Fire_360()
	{
		float num = 0f;
		Attack_Delay = 1.5f;
		Attack_Num++;
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, base.transform.rotation) as global::UnityEngine.GameObject;
		global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.4f);
		global::UnityEngine.Vector3 position = gameObject.transform.position;
		vector.x -= position.x;
		vector.y -= position.y;
		num = global::UnityEngine.Mathf.Atan2(vector.y, vector.x) * 57.29578f;
		gameObject.transform.rotation = global::UnityEngine.Quaternion.Euler(new global::UnityEngine.Vector3(0f, 0f, num + 180f));
		global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(new global::UnityEngine.Vector3(0f, 0f, num + 180f + 30f))) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(new global::UnityEngine.Vector3(0f, 0f, num + 180f - 30f))) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject4 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(new global::UnityEngine.Vector3(0f, 0f, num + 180f + 60f))) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject5 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(new global::UnityEngine.Vector3(0f, 0f, num + 180f - 60f))) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject6 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(new global::UnityEngine.Vector3(0f, 0f, num + 180f + 90f))) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject7 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(new global::UnityEngine.Vector3(0f, 0f, num + 180f - 90f))) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Boss_4_Fire(pos_Fire.position);
		SR_Ball_Yeollow.color = color_OFF;
		SR_Ball_Purple.color = color_OFF;
		SR_Ball_Glow.color = new global::UnityEngine.Color(1f, 0.5f, 0f, 0f);
		SR_Glow_Yellow.color = color_OFF;
		SR_Glow_Purple.color = color_OFF;
		SR_Glow_Light.color = color_OFF;
	}

	private void Set_Fire_Poison()
	{
		float num = base.transform.position.x - Player.transform.position.x;
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Fire_Poison, base.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
		gameObject.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.up * 20f, global::UnityEngine.ForceMode2D.Impulse);
		gameObject.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * (0f - num), global::UnityEngine.ForceMode2D.Impulse);
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Boss_4_Fire(pos_Fire.position);
		SR_Ball_Yeollow.color = color_OFF;
		SR_Ball_Purple.color = color_OFF;
		SR_Ball_Glow.color = new global::UnityEngine.Color(1f, 0.5f, 0f, 0f);
		SR_Glow_Yellow.color = color_OFF;
		SR_Glow_Purple.color = color_OFF;
		SR_Glow_Light.color = color_OFF;
	}
}
