using UnityEngine;

public class AI_Mon_32 : global::UnityEngine.MonoBehaviour
{
	private bool isDeath;

	private float Death_Timer;

	private int EnemyState;

	private float Life_Timer;

	private int facingRight = -1;

	private float distance;

	private float dist_X;

	private float dist_Y;

	private float Move_Speed = 1f;

	private float Orig_Speed = 1f;

	private bool on_Chase;

	private bool on_Attack_Range;

	private bool on_Dash_Range;

	private float Hit_Delay;

	private float Flip_Delay;

	private float Attack_Delay;

	private float Dash_Delay;

	private float Move_Delay;

	private float Move_Push;

	private float Move_Lock_Timer;

	private float Invincible_Timer;

	private float LockHit_Delay;

	private int Attack_Num;

	private float Dash_Timer;

	private float Escape_Dash_Timer;

	private float rnd_X;

	private bool isStuck_Front;

	private bool isStuck_Back;

	public global::UnityEngine.GameObject Explo;

	public global::UnityEngine.Transform[] explo_Pos;

	private float ExploSound_Timer;

	private float[] Explo_Timer = new float[9];

	public global::UnityEngine.GameObject Tr_Pos;

	public global::UnityEngine.Transform Tr_Front_Start;

	public global::UnityEngine.Transform Tr_Front_End;

	public global::UnityEngine.Transform Tr_Back_Start;

	public global::UnityEngine.Transform Tr_Back_End;

	private float Snd_Growl_Timer;

	private float Snd_Flip_Timer;

	private float Snd_Damage_Timer;

	private global::UnityEngine.Animator animator;

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
		animator = GetComponent<global::UnityEngine.Animator>();
		rnd_X = (float)global::UnityEngine.Random.Range(0, 50) * 0.01f;
		Move_Speed = 0f;
		Orig_Speed = 20f + rnd_X;
		Mon.onCrouch = true;
		if (Player.transform.position.x > base.transform.position.x)
		{
			Flip();
		}
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
		Dash_Timer += global::UnityEngine.Time.deltaTime;
		if (Escape_Dash_Timer > 0f)
		{
			Escape_Dash_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Invincible_Timer > 0f)
		{
			Invincible_Timer -= global::UnityEngine.Time.deltaTime;
		}
		else if (Mon.isInvincible)
		{
			Mon.isInvincible = false;
		}
		if (LockHit_Delay > 0f)
		{
			LockHit_Delay -= global::UnityEngine.Time.deltaTime;
		}
		else if (Mon.isLockHit && Move_Speed < 15f)
		{
			Mon.isLockHit = false;
		}
		Raycasting();
		if (Snd_Growl_Timer > 0f)
		{
			Snd_Growl_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Snd_Flip_Timer > 0f)
		{
			Snd_Flip_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Snd_Damage_Timer > 0f)
		{
			Snd_Damage_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (distance > 100f || GM.GameOver || GM.onHscene)
		{
			Check_Idle();
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
			GameManager.instance.sc_Sound_List.Mon_10_Death(base.transform.position);
			for (int i = 0; i < explo_Pos.Length; i++)
			{
				Make_Explo(explo_Pos[i]);
			}
			Mon.Make_Potion(5, 5, 5, 5);
			global::UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		if (EnemyState < 2)
		{
			Check_Idle();
			return;
		}
		if (GetComponent<global::UnityEngine.Animator>().GetBool("onHit"))
		{
			Hit_Delay = 0.2f;
			Move_Lock_Timer = 0.2f;
			Attack_Delay = 0f;
			if (Flip_Delay <= 0f)
			{
				Check_Flip();
			}
			if (Mon.MagicFire_5_Num > 5)
			{
				Mon.MagicFire_5_Num = 0;
				Hit_Delay = 0f;
				Set_Dash();
			}
			else if (Mon.Get_HitCombo() > 3 || GM.onShield)
			{
				Mon.Reset_HitCombo();
				Hit_Delay = 0f;
				if (Escape_Dash_Timer <= 0f)
				{
					Escape_Dash_Timer = 2.5f;
					Set_Dash();
				}
			}
			if (!on_Chase)
			{
				on_Chase = true;
			}
			return;
		}
		if (Hit_Delay > 0f)
		{
			Hit_Delay -= global::UnityEngine.Time.deltaTime;
			return;
		}
		if (Dash_Delay > 0f)
		{
			Dash_Delay -= global::UnityEngine.Time.deltaTime;
			Attack_Delay -= global::UnityEngine.Time.deltaTime;
			if (Dash_Delay < 0.5f)
			{
				base.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(global::UnityEngine.Mathf.Lerp(base.GetComponent<UnityEngine.Rigidbody2D>().velocity.x, 0f, global::UnityEngine.Time.deltaTime * 5f), base.GetComponent<UnityEngine.Rigidbody2D>().velocity.y);
				if (global::UnityEngine.Mathf.Abs(base.GetComponent<UnityEngine.Rigidbody2D>().velocity.x) < 5f)
				{
					Dash_Delay = 0f;
					Flip_Delay = 0f;
					Attack_Delay = 0f;
				}
			}
			return;
		}
		if (Attack_Delay > 0f)
		{
			Attack_Delay -= global::UnityEngine.Time.deltaTime;
			Move_Lock_Timer = 0.3f;
			return;
		}
		if (Flip_Delay > 0f)
		{
			Flip_Delay -= global::UnityEngine.Time.deltaTime;
			Check_Idle();
			return;
		}
		if (Move_Lock_Timer > 0f)
		{
			Move_Lock_Timer -= global::UnityEngine.Time.deltaTime;
		}
		Check_Flip();
		if (Snd_Growl_Timer <= 0f)
		{
			Snd_Growl_Timer = 1f;
			GameManager.instance.sc_Sound_List.Mon_10_Growling(base.transform.position);
		}
		if (on_Dash_Range && Move_Speed > 18f && Dash_Timer > 3f)
		{
			Set_MoveDash();
		}
		else if (on_Dash_Range && Move_Speed < 5f && Dash_Timer > 5f)
		{
			Set_Dash();
		}
		else if (on_Attack_Range)
		{
			Set_Attack();
		}
		else if (dist_X > 7f + rnd_X && Move_Lock_Timer <= 0f && !isStuck_Front)
		{
			if (!animator.GetBool("onMove"))
			{
				Set_Move();
			}
			Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, Orig_Speed, global::UnityEngine.Time.deltaTime * 12f);
			base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Move_Speed * facingRight * Mon.Move_Speed);
		}
		else
		{
			Check_Idle();
		}
	}

	private void Check_Flip()
	{
		if (facingRight > 0 && base.transform.position.x > Player.transform.position.x)
		{
			if (global::UnityEngine.Mathf.Abs(Player.transform.position.x - base.transform.position.x) > 1f)
			{
				Flip();
			}
		}
		else if (facingRight < 0 && base.transform.position.x < Player.transform.position.x && global::UnityEngine.Mathf.Abs(Player.transform.position.x - base.transform.position.x) > 1f)
		{
			Flip();
		}
	}

	private void Flip()
	{
		Check_Idle();
		facingRight = -facingRight;
		GetComponent<Monster>().Flip();
		Flip_Delay = 0.3f + (float)global::UnityEngine.Random.Range(0, 10) * 0.02f;
		Tr_Pos.transform.localScale = new global::UnityEngine.Vector3(-facingRight, 1f, 1f);
		base.transform.position = new global::UnityEngine.Vector3(base.transform.position.x + 1f * (float)facingRight, base.transform.position.y, 0f);
		GameManager.instance.sc_Sound_List.Mon_10_Flip(base.transform.position);
	}

	private void Set_Flip()
	{
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_2", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_3", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onMove", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
		GetComponent<global::UnityEngine.Animator>().SetTrigger("Flip");
	}

	private void Check_Idle()
	{
		if (GetComponent<global::UnityEngine.Animator>().GetBool("onAttack") || GetComponent<global::UnityEngine.Animator>().GetBool("onMove") || GetComponent<global::UnityEngine.Animator>().GetBool("onHit") || GetComponent<global::UnityEngine.Animator>().GetBool("onAttack_2") || GetComponent<global::UnityEngine.Animator>().GetBool("onAttack_3"))
		{
			Set_Idle();
		}
	}

	private void Set_Idle()
	{
		Mon.isLockHit = false;
		LockHit_Delay = 0f;
		Move_Speed = 0f;
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_2", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_3", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onMove", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
	}

	private void Set_Move()
	{
		Mon.isLockHit = false;
		LockHit_Delay = 0f;
		Move_Speed = 0f;
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_2", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_3", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onMove", true);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
	}

	private void Set_Attack()
	{
		Attack_Delay = 0.67f;
		Move_Speed = 0f;
		if (Attack_Num < 2)
		{
			Attack_Num++;
		}
		else
		{
			Attack_Num = 0;
		}
		if (Attack_Num == 0)
		{
			GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", true);
			GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_2", false);
			GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_3", false);
		}
		else if (Attack_Num == 1)
		{
			GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", false);
			GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_2", true);
			GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_3", false);
		}
		else
		{
			GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", false);
			GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_2", false);
			GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_3", true);
		}
		GetComponent<global::UnityEngine.Animator>().SetBool("onMove", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
	}

	private void Set_Dash()
	{
		Mon.isLockHit = true;
		LockHit_Delay = 1f;
		Attack_Delay = 1.5f;
		Move_Speed = 0f;
		GetComponent<global::UnityEngine.Animator>().SetTrigger("Dash");
		End_Attack();
	}

	private void Set_MoveDash()
	{
		Mon.isInvincible = true;
		Invincible_Timer = 0.25f;
		Attack_Delay = 1f;
		Move_Speed = 0f;
		GetComponent<global::UnityEngine.Animator>().SetTrigger("DashMove");
		End_Attack();
		Dash();
	}

	private void End_Attack()
	{
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_2", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack_3", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onMove", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
	}

	private void Attack_Force()
	{
		base.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(0f, base.GetComponent<UnityEngine.Rigidbody2D>().velocity.y);
		base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * 10f * facingRight, global::UnityEngine.ForceMode2D.Impulse);
	}

	private void Dash()
	{
		if (!Mon.isInvincible)
		{
			Mon.isInvincible = true;
			Invincible_Timer = 0.3f;
		}
		Dash_Delay = 1f;
		Dash_Timer = 0f;
		base.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(0f, base.GetComponent<UnityEngine.Rigidbody2D>().velocity.y);
		base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * 45f * facingRight, global::UnityEngine.ForceMode2D.Impulse);
		GameManager.instance.sc_Sound_List.Mon_Atk_2(base.transform.position);
		GameManager.instance.sc_Sound_List.Mon_9_Growling(base.transform.position);
	}

	private void Set_AttackDelay()
	{
	}

	private void Sound_Attack()
	{
		GameManager.instance.sc_Sound_List.Mon_Atk_1(base.transform.position);
	}

	private void Sound_Attack_Tongue()
	{
		GameManager.instance.sc_Sound_List.Mon_Atk_6(base.transform.position);
	}

	private void Sound_Mon_Attack()
	{
		GameManager.instance.sc_Sound_List.Mon_10_Attack(base.transform.position);
	}

	private void Sound_Damage()
	{
		if (Snd_Damage_Timer <= 0f)
		{
			Snd_Damage_Timer = 0.2f;
			if (global::UnityEngine.Random.Range(1, 3) == 1)
			{
				GameManager.instance.sc_Sound_List.Mon_10_Damage1(base.transform.position);
			}
			else
			{
				GameManager.instance.sc_Sound_List.Mon_10_Damage2(base.transform.position);
			}
		}
	}

	private void Sound_Mon_Death()
	{
		GameManager.instance.sc_Sound_List.Mon_10_Death(base.transform.position);
	}

	private void FootStep()
	{
		GameManager.instance.sc_Sound_List.FootStep_Mon(base.transform.position);
	}

	public void Set_Death()
	{
		isDeath = true;
		Death_Timer = 0f;
		animator.SetBool("onDeath", true);
		if (global::UnityEngine.Random.Range(1, 3) == 1)
		{
			GameManager.instance.sc_Sound_List.Mon_10_Damage1(base.transform.position);
		}
		else
		{
			GameManager.instance.sc_Sound_List.Mon_10_Damage2(base.transform.position);
		}
		DeathExplo();
		UnityEngine.Camera.main.GetComponent<Camera_Control>().Set_Shake_Timer(2.5f, UnityEngine.Camera.main.transform.position);
		GetComponent<global::UnityEngine.Animator>().Play("Death", 0, 0f);
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
		for (int i = 0; i < explo_Pos.Length; i++)
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

	private void Raycasting()
	{
		isStuck_Front = global::UnityEngine.Physics2D.Linecast(Tr_Front_Start.position, Tr_Front_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
		isStuck_Back = global::UnityEngine.Physics2D.Linecast(Tr_Back_Start.position, Tr_Back_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
		distance = global::UnityEngine.Vector3.Distance(new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f, Player.transform.position.z), base.transform.position);
		dist_X = global::UnityEngine.Mathf.Abs(base.transform.position.x - Player.transform.position.x);
		dist_Y = global::UnityEngine.Mathf.Abs(Player.transform.position.y - (base.transform.position.y - 8f));
		if (!on_Chase)
		{
			if (dist_X < 45f && Player.transform.position.y < base.transform.position.y + 6.03f && Player.transform.position.y > base.transform.position.y - 17.97f)
			{
				EnemyState = 2;
			}
			else
			{
				EnemyState = 0;
			}
		}
		if (EnemyState == 2 && !on_Chase)
		{
			on_Chase = true;
		}
		if (dist_X < 7f + rnd_X && dist_Y < 9f)
		{
			on_Attack_Range = true;
		}
		else
		{
			on_Attack_Range = false;
		}
		if (dist_X < 15f + rnd_X && dist_Y < 10f)
		{
			on_Dash_Range = true;
		}
		else
		{
			on_Dash_Range = false;
		}
	}
}
