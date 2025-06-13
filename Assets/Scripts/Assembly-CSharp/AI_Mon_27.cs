using UnityEngine;

public class AI_Mon_27 : global::UnityEngine.MonoBehaviour
{
	private float Life_Timer;

	private float onCam_Timer;

	private int facingRight = -1;

	private float distance;

	private float dist_X;

	private float dist_Y;

	private float Move_Speed = 5f;

	private float Speed_Orig = 1f;

	private bool on_Chase;

	private bool onAttack_Range;

	private bool onAttack;

	private float Hit_Delay;

	private float Flip_Delay;

	private float Move_Delay;

	private float LockHit_Delay;

	private float Laser_Timer;

	private bool on_LaserTarget;

	private float Laser_Angle;

	private float Target_Timer;

	private float Back_Timer;

	private float Pos_Timer;

	private global::UnityEngine.Vector3 Pos_Orig;

	private global::UnityEngine.Vector3 Pos_Target;

	private global::UnityEngine.Vector2 Rnd_XY;

	private global::UnityEngine.Vector2 GameOver_XY;

	private bool isGameOver;

	public global::UnityEngine.SpriteRenderer SR_Glow_Laser;

	public global::UnityEngine.SpriteRenderer SR_Glow_LVertical;

	public global::UnityEngine.Transform pos_Fire;

	public global::UnityEngine.GameObject _Laser;

	private global::UnityEngine.Color color_Laser;

	private global::UnityEngine.Color color_LVertical;

	private float Snd_Growl_Timer;

	private float Snd_Damage_Timer;

	private float H_Timer;

	private global::UnityEngine.Animator animator;

	private Monster Mon;
    Player_Control PC => GameManager.instance.PC;
    GameObject Player => GameManager.instance.gobj_Player;

    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//Player = global::UnityEngine.GameObject.Find("Player");
		//PC = Player.GetComponent<Player_Control>();
		Mon = GetComponent<Monster>();
		animator = GetComponent<global::UnityEngine.Animator>();
		Pos_Orig = base.transform.position;
		Pos_Target = new global::UnityEngine.Vector3(base.transform.position.x + 10f * (float)(-facingRight), base.transform.position.y + 3f, 0f);
		Rnd_XY = new global::UnityEngine.Vector2((float)global::UnityEngine.Random.Range(0, 50) * 0.01f, (float)global::UnityEngine.Random.Range(0, 50) * 0.01f);
		GameOver_XY = new global::UnityEngine.Vector2((float)global::UnityEngine.Random.Range(40, 120) * 0.1f, (float)global::UnityEngine.Random.Range(-10, 120) * 0.1f);
		Speed_Orig = 18f + global::UnityEngine.Random.Range(0f, 1f);
		if (Player.transform.position.x > base.transform.position.x)
		{
			Flip();
		}
		color_Laser = SR_Glow_Laser.color;
		color_LVertical = SR_Glow_LVertical.color;
		SR_Glow_Laser.color = new global::UnityEngine.Color(color_Laser.r, color_Laser.g, color_Laser.b, 0f);
		SR_Glow_LVertical.color = new global::UnityEngine.Color(color_LVertical.r, color_LVertical.g, color_LVertical.b, 0f);
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
		if (Target_Timer > 0f)
		{
			Target_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Back_Timer > 0f)
		{
			Back_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Pos_Timer > 0f)
		{
			Pos_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Snd_Growl_Timer > 0f)
		{
			Snd_Growl_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Snd_Damage_Timer > 0f)
		{
			Snd_Damage_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (!GM.GameOver && isGameOver)
		{
			isGameOver = false;
		}
		if (LockHit_Delay > 0f)
		{
			LockHit_Delay -= global::UnityEngine.Time.deltaTime;
		}
		else if (Mon.isLockHit)
		{
			Mon.isLockHit = false;
		}
		distance = global::UnityEngine.Vector3.Distance(new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f, 0f), base.transform.position);
		dist_X = global::UnityEngine.Mathf.Abs(base.transform.position.x - Player.transform.position.x);
		dist_Y = global::UnityEngine.Mathf.Abs(Player.transform.position.y - base.transform.position.y);
		if (on_Chase)
		{
			if (dist_X > 60f || dist_Y > 30f)
			{
				on_Chase = false;
			}
			else if (onCam_Timer <= 0f)
			{
				on_Chase = false;
			}
		}
		else if (!on_Chase)
		{
			if (dist_X < 30f && dist_Y < 17f)
			{
				on_Chase = true;
			}
			else if (onCam_Timer > 0f)
			{
				on_Chase = false;
			}
		}
		if (Move_Delay > 0f)
		{
			Move_Delay -= global::UnityEngine.Time.deltaTime;
		}
		else if (Flip_Delay > 0f)
		{
			Flip_Delay -= global::UnityEngine.Time.deltaTime;
		}
		else
		{
			Check_Flip();
		}
		if (H_Timer > 0f)
		{
			return;
		}
		if (distance < 50f && (GM.GameOver || GM.onHscene))
		{
			if (GM.GameOver)
			{
				if (GameManager.instance.eg2d_Player.velocity.y != 0f)
				{
					return;
				}
				if (!isGameOver)
				{
					isGameOver = true;
					Check_Idle();
					if (GetComponent<Monster>().Gameover_Num == 1)
					{
						Pos_Target = new global::UnityEngine.Vector3(Player.transform.position.x + (float)(3 * -facingRight), Player.transform.position.y + 3f, 0f);
					}
					else
					{
						Pos_Target = new global::UnityEngine.Vector3(Player.transform.position.x + GameOver_XY.x * (float)(-facingRight), Player.transform.position.y + GameOver_XY.y, 0f);
					}
				}
				base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, Pos_Target, global::UnityEngine.Time.deltaTime * (0.3f + (float)GetComponent<Monster>().Gameover_Num * 0.2f));
			}
			else
			{
				Check_Idle();
			}
			return;
		}
		if (!on_Chase)
		{
			Check_Idle();
			Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, 0f, global::UnityEngine.Time.deltaTime * 5f);
			return;
		}
		if (Target_Timer <= 0f && GetComponent<global::UnityEngine.Animator>().GetBool("onHit"))
		{
			Hit_Delay = 0.2f;
			if (GetComponent<Monster>().Get_HitCombo() > 2)
			{
				GetComponent<Monster>().Reset_HitCombo();
				Mon.isLockHit = true;
				LockHit_Delay = 1f;
				Hit_Delay = 1f;
				Back_Timer = 1f;
				Move_Speed = 0f;
				Move_Delay = 0f;
				Pos_Target = new global::UnityEngine.Vector3(base.transform.position.x - 10f * (float)facingRight, base.transform.position.y + global::UnityEngine.Random.Range(-2f, 2f), 0f);
				Target_Timer = 2f;
			}
			Laser_Timer = 2f;
			if (onAttack)
			{
				onAttack = false;
				SR_Glow_Laser.color = new global::UnityEngine.Color(color_Laser.r, color_Laser.g, color_Laser.b, 0f);
				SR_Glow_LVertical.color = new global::UnityEngine.Color(color_LVertical.r, color_LVertical.g, color_LVertical.b, 0f);
			}
			if (!on_Chase)
			{
				on_Chase = true;
			}
		}
		else if (Hit_Delay > 0f)
		{
			Hit_Delay -= global::UnityEngine.Time.deltaTime;
		}
		else
		{
			if (Snd_Growl_Timer <= 0f)
			{
				Snd_Growl_Timer = 1f;
				GameManager.instance.sc_Sound_List.Mon_11_Growling(base.transform.position);
			}
			if (distance < 15f || Laser_Timer > 0f)
			{
				Laser_Timer += global::UnityEngine.Time.deltaTime;
				if (Laser_Timer > 2.9f)
				{
					onAttack = false;
					Laser_Timer = 0f;
					SR_Glow_Laser.color = new global::UnityEngine.Color(color_Laser.r, color_Laser.g, color_Laser.b, 0f);
					SR_Glow_LVertical.color = new global::UnityEngine.Color(color_LVertical.r, color_LVertical.g, color_LVertical.b, 0f);
				}
				else if (Laser_Timer > 2f)
				{
					SR_Glow_Laser.color = global::UnityEngine.Color.Lerp(SR_Glow_Laser.color, color_Laser, global::UnityEngine.Time.deltaTime * 5f);
					SR_Glow_LVertical.color = global::UnityEngine.Color.Lerp(SR_Glow_LVertical.color, color_LVertical, global::UnityEngine.Time.deltaTime * 5f);
					if (!onAttack)
					{
						Set_Attack();
					}
				}
			}
			else if (onAttack)
			{
				onAttack = false;
				Laser_Timer = 0f;
				SR_Glow_Laser.color = new global::UnityEngine.Color(color_Laser.r, color_Laser.g, color_Laser.b, 0f);
				SR_Glow_LVertical.color = new global::UnityEngine.Color(color_LVertical.r, color_LVertical.g, color_LVertical.b, 0f);
			}
			if (Pos_Timer <= 0f && PC.Jump_Num == 0 && PC.State != Player_Control.AniState.Down)
			{
				Pos_Target = new global::UnityEngine.Vector3(Player.transform.position.x - (9f + Rnd_XY.x * 2f) * (float)facingRight, Player.transform.position.y + 7f + Rnd_XY.y * 2f, 0f);
				Pos_Timer = 0.3f;
			}
			else if (PC.Screw_Opacity > 0.4f)
			{
				Pos_Target = new global::UnityEngine.Vector3(Player.transform.position.x - (9f + Rnd_XY.x * 2f) * (float)facingRight, Player.transform.position.y + Rnd_XY.y * 2f, 0f);
			}
		}
		if (Back_Timer <= 0f)
		{
			if (onAttack || Move_Delay > 0f)
			{
				Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, 0f, global::UnityEngine.Time.deltaTime * 10f);
			}
			else
			{
				Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, Speed_Orig, global::UnityEngine.Time.deltaTime * 1f);
			}
		}
	}

	private void FixedUpdate()
	{
		if (!GM.Paused)
		{
			if (Back_Timer > 0f)
			{
				base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, Pos_Target, global::UnityEngine.Time.deltaTime * 5f);
			}
			else
			{
				base.transform.position = global::UnityEngine.Vector3.MoveTowards(base.transform.position, Pos_Target, global::UnityEngine.Time.deltaTime * Move_Speed * GetComponent<Monster>().Move_Speed);
			}
		}
	}

	private void Flip()
	{
		facingRight = -facingRight;
		GetComponent<Monster>().Flip();
		Flip_Delay = 0.2f;
		Laser_Angle = Get_Angle();
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
		if (GetComponent<global::UnityEngine.Animator>().GetBool("onAttack") || GetComponent<global::UnityEngine.Animator>().GetBool("onMove") || GetComponent<global::UnityEngine.Animator>().GetBool("onHit"))
		{
			Set_Idle();
		}
	}

	private void Set_Idle()
	{
		onAttack = false;
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onMove", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
	}

	private void Set_Attack()
	{
		onAttack = true;
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", true);
		GetComponent<global::UnityEngine.Animator>().SetBool("onMove", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
	}

	private void End_Attack()
	{
		onAttack = false;
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onMove", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
	}

	private void Set_On_Laser()
	{
		on_LaserTarget = true;
		Laser_Angle = Get_Angle();
	}

	private void Set_Fire_Laser()
	{
		float laser_Angle = Laser_Angle;
		Laser_Timer = 0f;
		Move_Delay = 0.8f;
		on_LaserTarget = false;
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Laser, pos_Fire.position, global::UnityEngine.Quaternion.Euler(0f, 0f, laser_Angle)) as global::UnityEngine.GameObject;
		SR_Glow_Laser.color = new global::UnityEngine.Color(color_Laser.r, color_Laser.g, color_Laser.b, 0f);
		SR_Glow_LVertical.color = new global::UnityEngine.Color(color_LVertical.r, color_LVertical.g, color_LVertical.b, 0f);
	}

	private float Get_Angle()
	{
		global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f);
		global::UnityEngine.Vector3 position = pos_Fire.position;
		vector.x -= position.x;
		vector.y -= position.y;
		return global::UnityEngine.Mathf.Atan2(vector.y, vector.x) * 57.29578f + 180f;
	}

	private void Set_AttackDelay()
	{
	}

	private void Sound_Mon_Damage()
	{
		if (Snd_Damage_Timer <= 0f)
		{
			Snd_Damage_Timer = 1f + (float)global::UnityEngine.Random.Range(0, 200) * 0.01f;
			GameManager.instance.sc_Sound_List.Mon_9_Damage(base.transform.position);
		}
	}

	private void OnTriggerStay2D(global::UnityEngine.Collider2D col)
	{
		if (col.tag == "Col_Camera")
		{
			onCam_Timer = 0.5f;
		}
	}
}
