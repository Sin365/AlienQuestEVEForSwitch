using UnityEngine;

public class AI_Mon_30N : global::UnityEngine.MonoBehaviour
{
	private bool isCleared;

	private bool isDeath;

	private float Death_Timer;

	private float Life_Timer;

	private int facingRight = -1;

	private float distance;

	private float dist_X;

	private bool Range_Attack;

	private int Attack_Mode;

	private bool on_Hscene;

	private float Tentacle_Delay = 2f;

	private float Bomb_Delay;

	private float Flip_Delay;

	private float Size_HP;

	public global::UnityEngine.GameObject Tr_Pos;

	public global::UnityEngine.Transform Tr_1_Start;

	public global::UnityEngine.Transform Tr_1_End;

	public global::UnityEngine.Transform Tr_2_Start;

	public global::UnityEngine.Transform Tr_2_End;

	public global::UnityEngine.Transform Tr_3_Start;

	public global::UnityEngine.Transform Tr_3_End;

	private global::UnityEngine.RaycastHit2D whatIHit;

	private global::UnityEngine.Vector3 pos_Orig;

	public global::UnityEngine.PolygonCollider2D Col_Body;

	public global::UnityEngine.GameObject _Fire;

	public global::UnityEngine.Transform pos_Fire;

	private int Fire_Num;

	public global::UnityEngine.GameObject _Tentacle;

	public global::UnityEngine.Transform pos_Tentacle;

	private int Tentacle_Num;

	private float Tentacle_Timer;

	public global::UnityEngine.GameObject Explo;

	public global::UnityEngine.Transform[] explo_Pos;

	private float ExploSound_Timer;

	private float[] Explo_Timer = new float[5];

	public H_Mon7[] Mon_7;

	private bool is_Mon7_Free;

	private float Mon7_Timer;

	public global::UnityEngine.Transform pos_Item;

	public global::UnityEngine.GameObject Clear_Item;

	public global::UnityEngine.GameObject[] H_Single;

	private float Snd_Growl_Timer;

	private float Snd_Damage_Timer;

	private UI_Control UC;

	private Monster Mon;
    Player_Control PC => GameManager.instance?.PC;
    GameObject Player => GameManager.instance?.gobj_Player;

    GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//Player = global::UnityEngine.GameObject.Find("Player");
		//PC = Player.GetComponent<Player_Control>();
		Mon = GetComponent<Monster>();
		UC = global::UnityEngine.GameObject.Find("Status").GetComponent<UI_Control>();
		pos_Orig = base.transform.localPosition;
		if (global::UnityEngine.GameObject.Find("Player").transform.position.x > base.transform.position.x)
		{
			Flip();
		}
		if (GM.Get_Event(11))
		{
			if (!GM.onSkill_2)
			{
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Clear_Item, pos_Item.position, base.transform.rotation) as global::UnityEngine.GameObject;
				gameObject.transform.parent = base.transform.parent;
				gameObject.GetComponent<Item>().Set_Target(new global::UnityEngine.Vector3(pos_Item.position.x, pos_Item.position.y + 2f, 0f));
			}
			Set_Clear();
			if (Mon_7.Length > 0)
			{
				for (int i = 0; i < Mon_7.Length; i++)
				{
					global::UnityEngine.Object.Destroy(Mon_7[i].gameObject);
				}
			}
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
		if (Tentacle_Delay > 0f)
		{
			Tentacle_Delay -= global::UnityEngine.Time.deltaTime;
		}
		if (Bomb_Delay > 0f)
		{
			Bomb_Delay -= global::UnityEngine.Time.deltaTime;
		}
		if (Mon7_Timer > 0f)
		{
			Mon7_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Snd_Growl_Timer > 0f)
		{
			Snd_Growl_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Snd_Damage_Timer > 0f)
		{
			Snd_Damage_Timer -= global::UnityEngine.Time.deltaTime;
		}
		dist_X = global::UnityEngine.Mathf.Abs(base.transform.position.x - Player.transform.position.x);
		if (dist_X < 25f && global::UnityEngine.Mathf.Abs(base.transform.position.y - Player.transform.position.y) < 12f)
		{
			Range_Attack = true;
		}
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
			GameManager.instance.sc_Sound_List.Mon_Explo(base.transform.position);
			for (int i = 0; i < explo_Pos.Length; i++)
			{
				Make_Explo(explo_Pos[i]);
			}
			if (!GM.onSkill_2)
			{
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Clear_Item, pos_Item.position, base.transform.rotation) as global::UnityEngine.GameObject;
				gameObject.transform.parent = base.transform.parent;
				gameObject.GetComponent<Item>().Set_Target(new global::UnityEngine.Vector3(pos_Item.position.x, pos_Item.position.y + 2f, 0f));
			}
			Set_Clear();
			Mon_7_Free();
		}
		else if (GetComponent<global::UnityEngine.Animator>().GetBool("onAttackDown"))
		{
			if (Attack_Mode == 2 && Tentacle_Num < 7)
			{
				if (Tentacle_Timer <= 0f)
				{
					Set_Tentacle();
				}
				else
				{
					Tentacle_Timer -= global::UnityEngine.Time.deltaTime;
				}
			}
		}
		else
		{
			if (GetComponent<global::UnityEngine.Animator>().GetBool("onAttack") || !Range_Attack || on_Hscene)
			{
				return;
			}
			if (facingRight > 0 && base.transform.position.x > Player.transform.position.x)
			{
				Flip_Delay += global::UnityEngine.Time.deltaTime;
				if (Flip_Delay > 0.75f)
				{
					Flip();
				}
			}
			else if (facingRight < 0 && base.transform.position.x < Player.transform.position.x)
			{
				Flip_Delay += global::UnityEngine.Time.deltaTime;
				if (Flip_Delay > 0.75f)
				{
					Flip();
				}
			}
			if (Mon.HP_Ratio > 0.5f || GM.GameOver)
			{
				if (Tentacle_Delay <= 0f)
				{
					Set_Attack_Tentacle();
				}
			}
			else if (Mon.HP_Ratio > 0.2f)
			{
				if (Bomb_Delay <= 0f)
				{
					Set_Attack();
				}
				else if (Tentacle_Delay <= 0f)
				{
					Set_Attack_Tentacle();
				}
			}
			else if (Bomb_Delay <= 0f)
			{
				Set_Attack();
			}
			else if (Tentacle_Delay <= 0f)
			{
				Set_Attack_Tentacle();
			}
		}
	}

	private void Flip()
	{
		Check_Idle();
		facingRight = -facingRight;
		GetComponent<Monster>().Flip();
		Flip_Delay = 0f;
		Tr_Pos.transform.localScale = new global::UnityEngine.Vector3(-facingRight, 1f, 1f);
		if (facingRight < 0)
		{
			base.transform.localPosition = pos_Orig;
		}
		else
		{
			base.transform.localPosition = new global::UnityEngine.Vector3(pos_Orig.x - 0.7f, pos_Orig.y, 0f);
		}
	}

	private void Check_Idle()
	{
		if (GetComponent<global::UnityEngine.Animator>().GetBool("onAttack") || GetComponent<global::UnityEngine.Animator>().GetBool("onMove") || GetComponent<global::UnityEngine.Animator>().GetBool("onHit"))
		{
			Set_Idle();
		}
	}

	private void Set_Idle()
	{
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttackDown", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onMove", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
	}

	private void Set_Attack()
	{
		Attack_Mode = 1;
		Bomb_Delay = 3f;
		Fire_Num = 1;
		if (Mon.HP_Ratio < 0.2f)
		{
			Fire_Num = 3;
		}
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", true);
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttackDown", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onMove", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
	}

	private void Set_Attack_Tentacle()
	{
		Attack_Mode = 2;
		Tentacle_Delay = 5f;
		Tentacle_Num = 0;
		Tentacle_Timer = 0.3f;
		GetComponent<Monster>().isInvincible = true;
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttackDown", true);
		GetComponent<global::UnityEngine.Animator>().SetBool("onMove", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
	}

	private void End_Attack()
	{
		if (Attack_Mode == 2 || (Attack_Mode == 1 && Fire_Num == 0))
		{
			GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", false);
			GetComponent<global::UnityEngine.Animator>().SetBool("onAttackDown", false);
			GetComponent<global::UnityEngine.Animator>().SetBool("onMove", false);
			GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
			Attack_Mode = 0;
			GetComponent<Monster>().isInvincible = false;
			Flip_Delay = -1f;
		}
	}

	private void Set_AttackDelay()
	{
		Bomb_Delay = 0.5f;
	}

	private void Sound_Mon_Dmg()
	{
		GameManager.instance.sc_Sound_List.Mon_5_Damage(base.transform.position);
	}

	private void Set_Fire()
	{
		float num = ((facingRight >= 0) ? 180 : 0);
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(0f, 0f, num + 15f)) as global::UnityEngine.GameObject;
		gameObject.GetComponent<UnityEngine.Rigidbody2D>().AddForce(new global::UnityEngine.Vector2(dist_X * 40f * (float)facingRight, dist_X * 10f));
		GameManager.instance.sc_Sound_List.Boss_4_Fire(pos_Fire.position);
		Fire_Num--;
	}

	private void Set_Tentacle()
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Tentacle, new global::UnityEngine.Vector3(pos_Tentacle.position.x + 3f * (float)Tentacle_Num * (float)facingRight, pos_Tentacle.position.y, 0f), base.transform.rotation) as global::UnityEngine.GameObject;
		gameObject.transform.parent = base.transform.parent;
		gameObject.GetComponent<Boss_Tentacle>().Boss = GetComponent<AI_Mon_30N>();
		Tentacle_Timer = 0.1f;
		Tentacle_Num++;
		if (Tentacle_Num == 2 && !on_Hscene && GM.GameOver && !GM.onHscene && GM.Hscene_Num == 0 && GM.Hscene_Timer <= 0f)
		{
			Start_Hscene_GameOver();
			GameManager.instance.sc_Sound_List.Mon_Hit_2(base.transform.position);
		}
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

	private void Sound_Mon_Damage()
	{
		if (Snd_Damage_Timer <= 0f)
		{
			Snd_Damage_Timer = 1f + (float)global::UnityEngine.Random.Range(0, 200) * 0.01f;
			GameManager.instance.sc_Sound_List.Mon_9_Damage(base.transform.position);
		}
	}

	public void Set_Death()
	{
		isDeath = true;
		Death_Timer = 0f;
		GetComponent<global::UnityEngine.Animator>().SetTrigger("onDeath");
		Col_Body.enabled = false;
		GameManager.instance.sc_Sound_List.Mon_9_Damage(base.transform.position);
		UC.Set_Boss_Death();
		Mon_7_Death();
		DeathExplo();
		UnityEngine.Camera.main.GetComponent<Camera_Control>().Set_Shake_Timer(2.5f, UnityEngine.Camera.main.transform.position);
	}

	private void DeathExplo()
	{
		if (ExploSound_Timer <= 0f)
		{
			ExploSound_Timer = global::UnityEngine.Random.Range(0.2f, 0.5f);
			GameManager.instance.sc_Sound_List.Mon_Explo(base.transform.position);
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

	private void Set_Clear()
	{
		isCleared = true;
		GetComponent<global::UnityEngine.Animator>().SetTrigger("onClear");
		Col_Body.enabled = false;
	}

	public void Start_Hscene_Tentacle(global::UnityEngine.Vector3 Pos_T)
	{
		on_Hscene = true;
		GM.onEvent = true;
		GM.onHscene = true;
		GM.Hscene_Timer = 1f;
		int num = ((global::UnityEngine.Random.Range(0, 10) >= 5) ? 8 : 9);
		GM.Hscene_Num = num;
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(H_Single[num - 8], new global::UnityEngine.Vector3(Pos_T.x, Pos_T.y + 0.34f, 0f), global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
		Player.transform.position = new global::UnityEngine.Vector3(Pos_T.x, Pos_T.y + 0.34f, 0f);
		gameObject.transform.parent = base.transform.parent;
		if (PC.facingRight > 0)
		{
			gameObject.SendMessage("Flip");
		}
		gameObject.GetComponent<H_Ani>().Mon_Object = base.gameObject;
		GetComponent<Monster>().isInvincible = true;
		global::UnityEngine.GameObject.Find("Menu").GetComponent<Menu_Control>().H_Object = gameObject;
		UnityEngine.Camera.main.SendMessage("Hscene_Zoom");
		if (!GM.GameOver)
		{
			Player.SendMessage("H_Down");
		}
		global::UnityEngine.GameObject.Find("Ani").SendMessage("Start_H_Scene");
	}

	private void Start_Hscene_GameOver()
	{
		on_Hscene = true;
		GM.onEvent = true;
		GM.onHscene = true;
		GM.Hscene_Timer = 1f;
		int num = ((global::UnityEngine.Random.Range(0, 10) >= 5) ? 8 : 9);
		GM.Hscene_Num = num;
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(H_Single[num - 8], new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y, 0f), global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
		gameObject.transform.parent = base.transform.parent;
		if (PC.facingRight > 0)
		{
			gameObject.SendMessage("Flip");
		}
		gameObject.GetComponent<H_Ani>().Mon_Object = base.gameObject;
		GetComponent<Monster>().isInvincible = true;
		global::UnityEngine.GameObject.Find("Menu").GetComponent<Menu_Control>().H_Object = gameObject;
		UnityEngine.Camera.main.SendMessage("Hscene_Zoom");
		if (!GM.GameOver)
		{
			Player.SendMessage("H_Down");
		}
		global::UnityEngine.GameObject.Find("Ani").SendMessage("Start_H_Scene");
	}

	private void End_Hscene()
	{
		on_Hscene = false;
		GM.onEvent = false;
		GM.onHscene = false;
		GM.Hscene_Num = 0;
		GetComponent<Monster>().isInvincible = false;
		GM.Down_H_After();
		global::UnityEngine.GameObject.Find("Ani").SendMessage("End_H_Scene");
	}
}
