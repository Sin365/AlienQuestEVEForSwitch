public class AI_Mon_13 : global::UnityEngine.MonoBehaviour
{
	private int EnemyState;

	private float Life_Timer;

	private int facingRight = -1;

	private float distance;

	private float dist_X;

	private float dist_Y;

	private float Move_Speed = 1f;

	private bool on_Chase;

	private int Patrol_State;

	private float Patrol_Idle_Timer;

	private float Patrol_Move_Timer;

	private float Patrol_Pos_X;

	private float Patrol_Range;

	private bool isStuck_Front;

	private bool isStuck_Back;

	private bool Range_Attack;

	private bool isRunForce;

	private float Attack_Delay;

	private float Flip_Delay;

	private float rnd_X;

	private bool on_Hscene;

	private float H_Timer;

	private float H_Pursue_Timer;

	private float PC_Atk_Timer;

	public global::UnityEngine.GameObject Tr_Pos;

	public global::UnityEngine.Transform Tr_1_Start;

	public global::UnityEngine.Transform Tr_1_End;

	public global::UnityEngine.Transform Tr_2_Start;

	public global::UnityEngine.Transform Tr_2_End;

	public global::UnityEngine.Transform Tr_3_Start;

	public global::UnityEngine.Transform Tr_3_End;

	public global::UnityEngine.Transform Tr_4_Start;

	public global::UnityEngine.Transform Tr_4_End;

	public global::UnityEngine.Transform Tr_5_Start;

	public global::UnityEngine.Transform Tr_5_End;

	private global::UnityEngine.RaycastHit2D whatIHit;

	public global::UnityEngine.GameObject _Fire;

	public global::UnityEngine.Transform pos_Fire;

	public global::UnityEngine.GameObject[] H_Single;

	public global::UnityEngine.SpriteRenderer CensoredText;

	public global::UnityEngine.SpriteRenderer CensoredBox;

	public global::UnityEngine.SkinnedMeshRenderer Penis;

	public global::UnityEngine.SkinnedMeshRenderer Penis_Censored;

	public global::UnityEngine.SkinnedMeshRenderer Penis_Wet;

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
		rnd_X = global::UnityEngine.Random.Range(0.5f, 1.5f);
		Move_Speed = 3.5f + global::UnityEngine.Random.Range(0f, 1f);
		Patrol_Range = 12f + global::UnityEngine.Random.Range(0f, 6f);
		if (global::UnityEngine.PlayerPrefs.GetInt("UncensoredPatch") != 1)
		{
			Penis.enabled = false;
			Penis_Wet.enabled = false;
			Penis_Censored.enabled = true;
		}
		else
		{
			Penis.enabled = true;
			Penis_Wet.enabled = false;
			Penis_Censored.enabled = false;
		}
		if (Player.transform.position.x > base.transform.position.x)
		{
			Flip();
		}
	}

	private void Update()
	{
		if (GM.Paused || on_Hscene)
		{
			return;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (global::UnityEngine.PlayerPrefs.GetInt("Censorship") == 1)
		{
			CensoredText.enabled = true;
			CensoredBox.enabled = true;
		}
		else
		{
			CensoredText.enabled = false;
			CensoredBox.enabled = false;
		}
		Raycasting();
		if (PC_Atk_Timer > 0f)
		{
			PC_Atk_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (H_Timer > 0f)
		{
			H_Timer -= global::UnityEngine.Time.deltaTime;
			if (H_Timer > 1f)
			{
				if (!GetComponent<global::UnityEngine.Animator>().GetBool("onMove"))
				{
					Set_Move();
				}
				base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Move_Speed * 0.6f * -facingRight * Mon.Move_Speed);
			}
			else
			{
				Check_Idle();
			}
		}
		else if (distance < 45f && (GM.GameOver || GM.onHscene))
		{
			if (GM.Hscene_Num == 0 && GM.Hscene_Timer <= 0f)
			{
				if (facingRight > 0 && base.transform.position.x > global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x)
				{
					Flip();
				}
				else if (facingRight < 0 && base.transform.position.x < global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x)
				{
					Flip();
				}
				else if (dist_Y < 1f && (global::UnityEngine.Mathf.Abs(base.transform.position.x - global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x) < 0.3f || (isStuck_Front && global::UnityEngine.Mathf.Abs(base.transform.position.x - global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x) < 2.2f)))
				{
					if (facingRight > 0 && Player.transform.localScale.x > 0f)
					{
						Player.transform.position = new global::UnityEngine.Vector3(Player.transform.position.x - 2.1f * Player.transform.localScale.x, Player.transform.position.y, 0f);
						Player.SendMessage("Flip");
					}
					else if (facingRight < 0 && Player.transform.localScale.x < 0f)
					{
						Player.transform.position = new global::UnityEngine.Vector3(Player.transform.position.x - 2.1f * Player.transform.localScale.x, Player.transform.position.y, 0f);
						Player.SendMessage("Flip");
					}
					Start_Hscene();
				}
				else if (isStuck_Front)
				{
					Check_Idle();
				}
				else
				{
					if (!GetComponent<global::UnityEngine.Animator>().GetBool("onMove"))
					{
						Set_Move();
					}
					base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Move_Speed * 1.2f * facingRight * Mon.Move_Speed);
				}
				Patrol_Pos_X = 0f;
				H_Pursue_Timer += global::UnityEngine.Time.deltaTime;
				return;
			}
			if (Patrol_Pos_X == 0f)
			{
				Patrol_Pos_X = global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x;
			}
			if (H_Pursue_Timer > 0f)
			{
				Check_Idle();
				Patrol_State = 0;
				Patrol_Idle_Timer = global::UnityEngine.Random.Range(-1f, 1f);
			}
			else if (GetComponent<global::UnityEngine.Animator>().GetBool("onMove"))
			{
				Patrol_Move_Timer += global::UnityEngine.Time.deltaTime;
				if (Patrol_Move_Timer > 1f && (isStuck_Front || global::UnityEngine.Mathf.Abs(global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x - base.transform.position.x) > Patrol_Range))
				{
					Check_Idle();
					Patrol_State = 0;
					Patrol_Idle_Timer = 0f;
				}
				base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Move_Speed * 0.5f * facingRight * Mon.Move_Speed);
			}
			else
			{
				Patrol_Idle_Timer += global::UnityEngine.Time.deltaTime;
				if (Patrol_Idle_Timer > 2.5f)
				{
					Patrol_Idle_Timer = 0f;
					Set_Move();
					Patrol_State = 1;
					Patrol_Move_Timer = 0f;
				}
				else if (Patrol_Idle_Timer > 1.5f)
				{
					if ((facingRight > 0 && base.transform.position.x > global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x) || (facingRight < 0 && base.transform.position.x < global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x))
					{
						Flip();
					}
				}
				else
				{
					Check_Idle();
				}
			}
			H_Pursue_Timer = 0f;
		}
		else
		{
			if (EnemyState == 0)
			{
				return;
			}
			if (GetComponent<global::UnityEngine.Animator>().GetBool("onHit"))
			{
				PC_Atk_Timer = 3f;
			}
			else if (Flip_Delay > 0f)
			{
				Flip_Delay -= global::UnityEngine.Time.deltaTime;
				Check_Idle();
			}
			else if (Attack_Delay > 0f)
			{
				Attack_Delay -= global::UnityEngine.Time.deltaTime;
				if (GetComponent<global::UnityEngine.Animator>().GetBool("onMove"))
				{
					Check_Idle();
				}
			}
			else if (EnemyState == 1)
			{
				Check_Idle();
			}
			else
			{
				if (EnemyState != 2)
				{
					return;
				}
				if (facingRight > 0 && base.transform.position.x > Player.transform.position.x)
				{
					if (global::UnityEngine.Mathf.Abs(Player.transform.position.x - base.transform.position.x) > 1f)
					{
						Flip();
					}
				}
				else if (facingRight < 0 && base.transform.position.x < Player.transform.position.x)
				{
					if (global::UnityEngine.Mathf.Abs(Player.transform.position.x - base.transform.position.x) > 1f)
					{
						Flip();
					}
				}
				else if (Range_Attack && !GM.GameOver)
				{
					if (GM.User_Input_Timer > 1f && !GM.onCloth && GM.Hscene_Num == 0 && GM.Hscene_Timer <= 0f && GM.Option_Int[3] == 1)
					{
						if (dist_X > 1.5f)
						{
							if (!GetComponent<global::UnityEngine.Animator>().GetBool("onMove"))
							{
								Set_Move();
							}
							base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Move_Speed * facingRight * Mon.Move_Speed);
						}
						else
						{
							Attack_Delay = 0.2f;
							if (dist_Y < 1f && PC.grounded_Now && (PC.State.ToString() == "Idle" || PC.State.ToString() == "Run" || PC.State.ToString() == "Sit" || PC.State.ToString() == "Down"))
							{
								Start_Hscene();
							}
							else
							{
								Set_Attack();
							}
						}
					}
					else
					{
						Set_Attack();
					}
				}
				else if (isStuck_Front || global::UnityEngine.Mathf.Abs(Player.transform.position.x - base.transform.position.x) < rnd_X)
				{
					Check_Idle();
				}
				else
				{
					if (!GetComponent<global::UnityEngine.Animator>().GetBool("onMove"))
					{
						Set_Move();
					}
					base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Move_Speed * facingRight * Mon.Move_Speed);
				}
			}
		}
	}

	public void Flip()
	{
		Check_Idle();
		facingRight = -facingRight;
		Mon.Flip();
		Flip_Delay = 0.5f + (float)global::UnityEngine.Random.Range(0, 80) * 0.01f;
		Tr_Pos.transform.localScale = new global::UnityEngine.Vector3(-facingRight, 1f, 1f);
		CensoredText.transform.localScale = new global::UnityEngine.Vector3(1 * -facingRight, 1f, 1f);
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
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onMove", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
	}

	private void Set_Move()
	{
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onMove", true);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
	}

	private void Set_Attack()
	{
		Attack_Delay = 1.5f;
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", true);
		GetComponent<global::UnityEngine.Animator>().SetBool("onMove", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
	}

	private void End_Attack()
	{
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onMove", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
	}

	private void Set_AttackDelay()
	{
		if (Attack_Delay < 0.6f)
		{
			Attack_Delay = 0.6f;
		}
	}

	private void Sound_Mon_Attack()
	{
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Atk_1(base.transform.position);
	}

	private void Set_Fire()
	{
		float num = 0f;
		global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f);
		global::UnityEngine.Vector3 position = pos_Fire.transform.position;
		vector.x -= position.x;
		vector.y -= position.y;
		num = global::UnityEngine.Mathf.Atan2(vector.y, vector.x) * 57.29578f + 180f;
		num = Check_Fire_Angle(num);
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, num)) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Boss_4_Fire(pos_Fire.position);
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

	private void Start_Hscene()
	{
		on_Hscene = true;
		GM.onEvent = true;
		GM.onHscene = true;
		GM.Hscene_Num = 15;
		GM.Hscene_Timer = 1f;
		H_Timer = 4f;
		H_Pursue_Timer = 0f;
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(H_Single[0], Player.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
		gameObject.transform.parent = base.transform.parent;
		if (facingRight > 0)
		{
			gameObject.SendMessage("Flip");
		}
		gameObject.GetComponent<H_Ani>().Mon_Object = base.gameObject;
		GetComponent<Mon_Index>().OnOff_Object(false);
		GetComponent<global::UnityEngine.Animator>().speed = 0f;
		GetComponent<global::UnityEngine.Rigidbody2D>().Sleep();
		base.transform.position = new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 3.23f, 0f);
		Mon.isInvincible = true;
		global::UnityEngine.GameObject.Find("Menu").GetComponent<Menu_Control>().H_Object = gameObject;
		global::UnityEngine.GameObject.Find("Main Camera").SendMessage("Hscene_Zoom");
		if (!GM.GameOver)
		{
			Player.SendMessage("H_Down");
		}
		global::UnityEngine.GameObject.Find("Ani").SendMessage("Start_H_Scene");
		Check_Idle();
	}

	private void End_Hscene()
	{
		on_Hscene = false;
		GM.onEvent = false;
		GM.onHscene = false;
		GM.Hscene_Num = 0;
		GetComponent<Mon_Index>().OnOff_Object(true);
		GetComponent<global::UnityEngine.Animator>().speed = 1f;
		GetComponent<global::UnityEngine.Rigidbody2D>().WakeUp();
		Mon.isInvincible = false;
		GM.Down_H_After();
		global::UnityEngine.GameObject.Find("Ani").SendMessage("End_H_Scene");
		if (global::UnityEngine.PlayerPrefs.GetInt("UncensoredPatch") != 1)
		{
			Penis.enabled = false;
			Penis_Wet.enabled = false;
			Penis_Censored.enabled = true;
		}
		else
		{
			Penis.enabled = false;
			Penis_Wet.enabled = true;
			Penis_Censored.enabled = false;
		}
	}

	private void Raycasting()
	{
		bool flag = global::UnityEngine.Physics2D.Linecast(Tr_1_Start.position, Tr_1_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player"));
		bool flag2 = global::UnityEngine.Physics2D.Linecast(Tr_2_Start.position, Tr_2_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player"));
		bool flag3 = global::UnityEngine.Physics2D.Linecast(Tr_3_Start.position, Tr_3_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player"));
		isStuck_Front = global::UnityEngine.Physics2D.Linecast(Tr_4_Start.position, Tr_4_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
		isStuck_Back = global::UnityEngine.Physics2D.Linecast(Tr_5_Start.position, Tr_5_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
		distance = global::UnityEngine.Vector3.Distance(new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f, Player.transform.position.z), base.transform.position);
		dist_X = global::UnityEngine.Mathf.Abs(base.transform.position.x - Player.transform.position.x);
		dist_Y = global::UnityEngine.Mathf.Abs(Player.transform.position.y - (base.transform.position.y - 3.22f));
		if (distance > 45f)
		{
			EnemyState = 0;
		}
		else if (dist_X < 25f)
		{
			if (dist_Y < 6.3f)
			{
				EnemyState = 2;
			}
			else if (on_Chase && global::UnityEngine.Mathf.Abs(base.transform.position.y - Player.transform.position.y) < 13f)
			{
				EnemyState = 2;
			}
			else
			{
				EnemyState = 1;
			}
		}
		else
		{
			EnemyState = 1;
		}
		if (flag || flag2 || flag3 || (dist_X < 10f && EnemyState == 2))
		{
			Range_Attack = true;
		}
		else
		{
			Range_Attack = false;
		}
		if (dist_X < 2.5f && dist_Y > 7f)
		{
			Range_Attack = false;
		}
		if (EnemyState == 2)
		{
			on_Chase = true;
		}
	}
}
