public class AI_Mon_42 : global::UnityEngine.MonoBehaviour
{
	private bool isDeath;

	private float Death_Timer;

	private int EnemyState;

	private float Life_Timer;

	private int facingRight = -1;

	private float distance;

	private float dist_X;

	private float dist_Y;

	private float dist_Cam;

	private float Move_Speed = 1f;

	private float Orig_Speed = 1f;

	private bool on_Chase;

	private bool on_Attack_Range;

	private bool on_AttackUpper_Range;

	private bool on_Tongue_Range;

	private bool on_Tail_Range;

	private bool on_Fire_Range;

	private bool on_Laser_Range;

	private float Hit_Delay;

	private float Flip_Delay;

	private float Attack_Delay;

	private float Dash_Delay;

	private float Move_Delay;

	private float Move_Push;

	private float Move_Lock_Timer;

	private float Invincible_Timer;

	private float LockHit_Delay;

	private float Fire_Timer;

	private float Laser_Timer;

	private float Dash_Timer;

	private float Tail_Timer;

	private float Escape_Dash_Timer;

	private float Escape_BackDash_Timer;

	private float Screw_Dash_Timer;

	private float Screw_BackDash_Timer;

	private float rnd_X;

	private bool isStuck_Front;

	private bool isStuck_Back;

	private int Patrol_State;

	private float Patrol_Idle_Timer;

	private float Patrol_Move_Timer;

	private float Patrol_Pos_X;

	private float Patrol_Range;

	public global::UnityEngine.GameObject Explo;

	public global::UnityEngine.Transform[] explo_Pos;

	private float ExploSound_Timer;

	private float[] Explo_Timer = new float[9];

	public global::UnityEngine.GameObject Tr_Pos;

	public global::UnityEngine.Transform Tr_Tail_Start;

	public global::UnityEngine.Transform Tr_Tail_End;

	public global::UnityEngine.Transform Tr_Front_Start;

	public global::UnityEngine.Transform Tr_Front_End;

	public global::UnityEngine.Transform Tr_Back_Start;

	public global::UnityEngine.Transform Tr_Back_End;

	public global::UnityEngine.Transform pos_Fire;

	public global::UnityEngine.Transform pos_Glow;

	public global::UnityEngine.GameObject _Fire;

	public global::UnityEngine.GameObject _Laser;

	private float Snd_Growl_Timer;

	private float Snd_Damage_Timer;

	private global::UnityEngine.Animator animator;

	private global::UnityEngine.GameObject Main_Camera;

	private Monster Mon;

	private Player_Control PC;

	private global::UnityEngine.GameObject Player;

	GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		Player = global::UnityEngine.GameObject.Find("Player");
		PC = Player.GetComponent<Player_Control>();
		Mon = GetComponent<Monster>();
		Main_Camera = global::UnityEngine.GameObject.Find("Main Camera");
		animator = GetComponent<global::UnityEngine.Animator>();
		rnd_X = (float)global::UnityEngine.Random.Range(0, 50) * 0.01f;
		Orig_Speed = (Move_Speed = 18f + rnd_X);
		Patrol_Range = 20f + global::UnityEngine.Random.Range(0f, 6f);
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
		Fire_Timer += global::UnityEngine.Time.deltaTime;
		Laser_Timer += global::UnityEngine.Time.deltaTime;
		Dash_Timer += global::UnityEngine.Time.deltaTime;
		Tail_Timer += global::UnityEngine.Time.deltaTime;
		if (Escape_Dash_Timer > 0f)
		{
			Escape_Dash_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Escape_BackDash_Timer > 0f)
		{
			Escape_BackDash_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Screw_Dash_Timer > 0f)
		{
			Screw_Dash_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Screw_BackDash_Timer > 0f)
		{
			Screw_BackDash_Timer -= global::UnityEngine.Time.deltaTime;
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
		else if (Mon.isLockHit)
		{
			Mon.isLockHit = false;
		}
		if (Snd_Damage_Timer > 0f)
		{
			Snd_Damage_Timer -= global::UnityEngine.Time.deltaTime;
		}
		Raycasting();
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
			global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Explo(base.transform.position);
			for (int i = 0; i < explo_Pos.Length; i++)
			{
				Make_Explo(explo_Pos[i]);
			}
			Mon.Make_Potion(5, 5, 5, 5);
			global::UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		if (animator.GetBool("onHit"))
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
				Set_Attack_Tongue();
			}
			else if (Mon.Get_HitCombo() > 3 || GM.onShield)
			{
				Mon.Reset_HitCombo();
				Hit_Delay = 0f;
				if (facingRight > 0 && base.transform.position.x > Player.transform.position.x)
				{
					Flip();
				}
				else if (facingRight < 0 && base.transform.position.x < Player.transform.position.x)
				{
					Flip();
				}
				if (!isStuck_Back && Escape_BackDash_Timer <= 0f)
				{
					Set_BackDash();
				}
				else if (!isStuck_Front && Escape_Dash_Timer <= 0f)
				{
					Set_Attack_Tongue();
				}
				else if (GM.onShield)
				{
					Set_Attack_Shock();
				}
				else
				{
					Set_Attack_Fire();
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
		if (!on_Chase)
		{
			Check_Idle();
			return;
		}
		if (Move_Lock_Timer > 0f)
		{
			Move_Lock_Timer -= global::UnityEngine.Time.deltaTime;
		}
		Check_Flip();
		if ((PC.onScrewAttack || PC.State == Player_Control.AniState.Sit) && dist_X < 7f && Screw_BackDash_Timer <= 0f && !isStuck_Back)
		{
			Screw_BackDash_Timer = 1f;
			Laser_Timer += 8f;
			Fire_Timer += 2f;
			Set_BackDash();
			global::UnityEngine.Debug.Log("Set_BackDash");
		}
		else if ((PC.onScrewAttack || PC.State == Player_Control.AniState.Sit) && dist_X < 7f && Screw_Dash_Timer <= 0f)
		{
			Screw_Dash_Timer = 3f;
			Set_Attack_Tongue();
		}
		else if (on_Laser_Range && Laser_Timer > 8f)
		{
			Set_Attack_Shock();
		}
		else if (on_Fire_Range && Fire_Timer > 6f)
		{
			Set_Attack_Fire();
		}
		else if (on_Tongue_Range && Dash_Timer > 5f)
		{
			Set_Attack_Tongue();
		}
		else if (!on_Attack_Range && on_AttackUpper_Range)
		{
			Set_Attack_Upper();
		}
		else if (on_Attack_Range)
		{
			if (global::UnityEngine.Random.Range(0, 10) < 6)
			{
				Set_Attack();
			}
			else
			{
				Set_Attack_Upper();
			}
		}
		else if (on_Tail_Range && Tail_Timer > 5f)
		{
			Set_Attack_Tail();
		}
		else if (dist_X > 10f + rnd_X && Move_Lock_Timer <= 0f && !isStuck_Front)
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

	public void Flip()
	{
		Check_Idle();
		facingRight = -facingRight;
		Mon.Flip();
		Flip_Delay = 0.5f + (float)global::UnityEngine.Random.Range(0, 30) * 0.01f;
		Tr_Pos.transform.localScale = new global::UnityEngine.Vector3(-facingRight, 1f, 1f);
		GetComponent<global::UnityEngine.BoxCollider2D>().offset = new global::UnityEngine.Vector2(0.5f * (float)(-facingRight), -3.567f);
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

	private void Check_Idle()
	{
		if (animator.GetBool("onAttack") || animator.GetBool("onMove") || animator.GetBool("onHit"))
		{
			Set_Idle();
		}
	}

	private void Set_Idle()
	{
		Move_Speed = 0f;
		animator.SetBool("onAttack", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onHit", false);
		animator.SetBool("onAttack_Upper", false);
		animator.SetBool("onAttack_Tongue", false);
		animator.SetBool("onAttack_Fire", false);
		animator.SetBool("onAttack_Tail", false);
		animator.SetBool("onAttack_Shock", false);
	}

	private void Set_Move()
	{
		Move_Speed = 0f;
		animator.SetBool("onAttack", false);
		animator.SetBool("onMove", true);
		animator.SetBool("onHit", false);
		animator.SetBool("onAttack_Upper", false);
		animator.SetBool("onAttack_Tongue", false);
		animator.SetBool("onAttack_Fire", false);
		animator.SetBool("onAttack_Tail", false);
		animator.SetBool("onAttack_Shock", false);
	}

	private void Set_Attack()
	{
		Mon.isLockHit = true;
		LockHit_Delay = 0.6f;
		Attack_Delay = 0.85f;
		animator.SetBool("onAttack", true);
		animator.SetBool("onMove", false);
		animator.SetBool("onHit", false);
		animator.SetBool("onAttack_Upper", false);
		animator.SetBool("onAttack_Tongue", false);
		animator.SetBool("onAttack_Fire", false);
		animator.SetBool("onAttack_Tail", false);
		animator.SetBool("onAttack_Shock", false);
	}

	private void Set_Attack_Upper()
	{
		Mon.isLockHit = true;
		LockHit_Delay = 0.6f;
		Attack_Delay = 0.85f;
		animator.SetBool("onAttack", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onHit", false);
		animator.SetBool("onAttack_Upper", true);
		animator.SetBool("onAttack_Tongue", false);
		animator.SetBool("onAttack_Fire", false);
		animator.SetBool("onAttack_Tail", false);
		animator.SetBool("onAttack_Shock", false);
	}

	private void Set_Attack_Tongue()
	{
		Mon.isLockHit = true;
		LockHit_Delay = 0.75f;
		Attack_Delay = 1f;
		Dash_Timer = 0f;
		animator.SetBool("onAttack", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onHit", false);
		animator.SetBool("onAttack_Upper", false);
		animator.SetBool("onAttack_Tongue", true);
		animator.SetBool("onAttack_Fire", false);
		animator.SetBool("onAttack_Tail", false);
		animator.SetBool("onAttack_Shock", false);
	}

	private void Set_Attack_Fire()
	{
		Mon.isLockHit = true;
		LockHit_Delay = 0.8f;
		Attack_Delay = 1f;
		Fire_Timer = 0f;
		animator.SetBool("onAttack", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onHit", false);
		animator.SetBool("onAttack_Upper", false);
		animator.SetBool("onAttack_Tongue", false);
		animator.SetBool("onAttack_Fire", true);
		animator.SetBool("onAttack_Tail", false);
		animator.SetBool("onAttack_Shock", false);
	}

	private void Set_Attack_Tail()
	{
		Mon.isLockHit = true;
		LockHit_Delay = 1.3f;
		Attack_Delay = 1.67f;
		Tail_Timer = 0f;
		animator.SetBool("onAttack", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onHit", false);
		animator.SetBool("onAttack_Upper", false);
		animator.SetBool("onAttack_Tongue", false);
		animator.SetBool("onAttack_Fire", false);
		animator.SetBool("onAttack_Tail", true);
		animator.SetBool("onAttack_Shock", false);
	}

	private void Set_Attack_Shock()
	{
		Mon.isLockHit = true;
		LockHit_Delay = 1.6f;
		Attack_Delay = 2f;
		Laser_Timer = 0f;
		animator.SetBool("onAttack", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onHit", false);
		animator.SetBool("onAttack_Upper", false);
		animator.SetBool("onAttack_Tongue", false);
		animator.SetBool("onAttack_Fire", false);
		animator.SetBool("onAttack_Tail", false);
		animator.SetBool("onAttack_Shock", true);
	}

	private void End_Attack()
	{
		animator.SetBool("onAttack", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onHit", false);
		animator.SetBool("onAttack_Upper", false);
		animator.SetBool("onAttack_Tongue", false);
		animator.SetBool("onAttack_Fire", false);
		animator.SetBool("onAttack_Tail", false);
		animator.SetBool("onAttack_Shock", false);
	}

	private void Set_BackDash()
	{
		Mon.isLockHit = true;
		LockHit_Delay = 0.6f;
		Attack_Delay = 0.8f;
		animator.SetBool("onAttack", false);
		animator.SetBool("onMove", false);
		animator.SetBool("onHit", false);
		animator.SetBool("onAttack_Upper", false);
		animator.SetBool("onAttack_Tongue", false);
		animator.SetBool("onAttack_Fire", false);
		animator.SetBool("onAttack_Tail", false);
		animator.SetBool("onAttack_Shock", false);
		animator.SetTrigger("BackDash");
		base.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(0f, base.GetComponent<UnityEngine.Rigidbody2D>().velocity.y);
		base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * 40f * -facingRight, global::UnityEngine.ForceMode2D.Impulse);
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Atk_2(base.transform.position);
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_9_Growling(base.transform.position);
	}

	private void Dash_Short()
	{
		Mon.isInvincible = true;
		Invincible_Timer = 0.25f;
		base.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(0f, base.GetComponent<UnityEngine.Rigidbody2D>().velocity.y);
		base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * 15f * facingRight, global::UnityEngine.ForceMode2D.Impulse);
	}

	private void Dash()
	{
		Mon.isInvincible = true;
		Invincible_Timer = 0.25f;
		Dash_Delay = 1f;
		base.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(0f, base.GetComponent<UnityEngine.Rigidbody2D>().velocity.y);
		base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * 45f * facingRight, global::UnityEngine.ForceMode2D.Impulse);
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Atk_2(base.transform.position);
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_9_Growling(base.transform.position);
	}

	private void Fire()
	{
		float angle = Get_Angle();
		angle = ((facingRight <= 0) ? pos_Fire.transform.rotation.z : (180f + pos_Fire.transform.rotation.z));
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, angle + 20f)) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, angle - 20f)) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Boss_4_Fire(pos_Fire.position);
	}

	private float Get_Angle()
	{
		float num = 0f;
		global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f);
		global::UnityEngine.Vector3 position = pos_Fire.position;
		vector.x -= position.x;
		vector.y -= position.y;
		return global::UnityEngine.Mathf.Atan2(vector.y, vector.x) * 57.29578f + 180f;
	}

	private float Check_Fire_Angle(float angle)
	{
		if (facingRight > 0)
		{
			if (angle > 220f)
			{
				return 220f;
			}
			if (angle < 140f)
			{
				return 140f;
			}
			return angle;
		}
		if (angle < 320f && angle > 180f)
		{
			return 320f;
		}
		if (angle > 40f && angle < 180f)
		{
			return 40f;
		}
		return angle;
	}

	private void Laser()
	{
		float angle = Get_Angle();
		angle = Check_Shock_Angle(angle);
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Laser, pos_Fire.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, angle)) as global::UnityEngine.GameObject;
	}

	private float Check_Shock_Angle(float angle)
	{
		if (facingRight > 0)
		{
			if (angle > 260f)
			{
				return 260f;
			}
			if (angle < 100f)
			{
				return 100f;
			}
			return angle;
		}
		if (angle < 280f && angle > 180f)
		{
			return 280f;
		}
		if (angle > 80f && angle < 180f)
		{
			return 80f;
		}
		return angle;
	}

	public void Set_Death()
	{
		isDeath = true;
		Death_Timer = 0f;
		animator.SetBool("onDeath", true);
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_9_Damage(base.transform.position);
		DeathExplo();
		Main_Camera.GetComponent<Camera_Control>().Set_Shake_Timer(2.5f, Main_Camera.transform.position);
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

	private void Sound_Attack()
	{
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Atk_5(base.transform.position);
	}

	private void Sound_Dash()
	{
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Atk_3(base.transform.position);
	}

	private void Sound_Attack_Arm()
	{
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Atk_1(base.transform.position);
	}

	private void Sound_Attack_Tongue()
	{
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Atk_6(base.transform.position);
	}

	private void Sound_Attack_Tail()
	{
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Atk_7(base.transform.position);
	}

	private void Sound_Mon_Damage()
	{
		if (Snd_Damage_Timer <= 0f)
		{
			Snd_Damage_Timer = 1f + (float)global::UnityEngine.Random.Range(0, 200) * 0.01f;
			global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_9_Damage(base.transform.position);
		}
	}

	private void FootStep()
	{
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().FootStep_Queen(base.transform.position);
	}

	private void Set_AttackDelay()
	{
	}

	private void Raycasting()
	{
		on_Tail_Range = global::UnityEngine.Physics2D.Linecast(Tr_Tail_Start.position, Tr_Tail_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player_Dmg"));
		isStuck_Front = global::UnityEngine.Physics2D.Linecast(Tr_Front_Start.position, Tr_Front_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
		isStuck_Back = global::UnityEngine.Physics2D.Linecast(Tr_Back_Start.position, Tr_Back_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
		distance = global::UnityEngine.Vector3.Distance(new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f, Player.transform.position.z), base.transform.position);
		dist_X = global::UnityEngine.Mathf.Abs(base.transform.position.x - Player.transform.position.x);
		dist_Y = global::UnityEngine.Mathf.Abs(Player.transform.position.y - (base.transform.position.y - 8.86f));
		dist_Cam = global::UnityEngine.Vector3.Distance(new global::UnityEngine.Vector3(Main_Camera.transform.position.x, Main_Camera.transform.position.y, 0f), base.transform.position);
		if (!on_Chase)
		{
			if (dist_X < 35f && Player.transform.position.y < base.transform.position.y + 7.1596f && Player.transform.position.y > base.transform.position.y - 18.84f)
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
		if (dist_X < 12f + rnd_X && dist_Y < 9f)
		{
			on_Attack_Range = true;
		}
		else
		{
			on_Attack_Range = false;
		}
		if (dist_X < 12f + rnd_X && dist_Y < 20f && Player.transform.position.y > base.transform.position.y - 10f)
		{
			on_AttackUpper_Range = true;
		}
		else
		{
			on_AttackUpper_Range = false;
		}
		if (dist_X < 20f + rnd_X && dist_Y < 10f)
		{
			on_Tongue_Range = true;
		}
		else
		{
			on_Tongue_Range = false;
		}
		if (dist_Cam < 21f)
		{
			on_Fire_Range = true;
			on_Laser_Range = true;
		}
		else
		{
			on_Fire_Range = false;
			on_Laser_Range = false;
		}
	}
}
