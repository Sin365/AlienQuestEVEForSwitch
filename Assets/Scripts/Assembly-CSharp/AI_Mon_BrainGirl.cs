public class AI_Mon_BrainGirl : global::UnityEngine.MonoBehaviour
{
	public enum Event_Type
	{
		None = 0,
		MotherBrain = 1
	}

	public AI_Mon_BrainGirl.Event_Type event_Type;

	private float Life_Timer;

	private int facingRight = -1;

	private float distance;

	private float dist_X;

	private float dist_H;

	private bool onExplo;

	private bool onAttack;

	private float Attack_Timer;

	private float Move_Speed = 5f;

	private float Speed_Orig = 1f;

	private bool on_Start_Poison;

	private bool on_Start_Slow;

	private int Patrol_State;

	private float Patrol_Idle_Timer;

	private float Patrol_Move_Timer;

	private float Patrol_Range;

	private float pos_Y = 4.5f;

	private global::UnityEngine.Vector3 pos_Orig;

	private global::UnityEngine.Vector3 pos_Prev;

	private global::UnityEngine.Vector3 Pos_Target;

	private global::UnityEngine.Vector3 Pos_Attack;

	private global::UnityEngine.Vector2 Rnd_XY;

	private global::UnityEngine.Vector2 GameOver_XY;

	public global::UnityEngine.GameObject _Fire;

	private bool on_Hscene;

	private float H_Timer;

	private float Dual_Timer;

	private float H_Pursue_Timer;

	public global::UnityEngine.GameObject H_Single;

	public global::UnityEngine.GameObject H_Dual;

	public global::UnityEngine.PolygonCollider2D Col_Body;

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
		Pos_Target = base.transform.position;
		Rnd_XY = new global::UnityEngine.Vector2((float)global::UnityEngine.Random.Range(-15, 15) * 0.01f, (float)global::UnityEngine.Random.Range(-15, 15) * 0.01f);
		GameOver_XY = new global::UnityEngine.Vector2((float)global::UnityEngine.Random.Range(50, 80) * 0.1f, global::UnityEngine.Random.Range(1f, 2.5f));
		pos_Orig = base.transform.position;
		Pos_Attack = Player.transform.position;
		Speed_Orig = 10f + global::UnityEngine.Random.Range(0f, 1f);
		Patrol_Range = 12f + global::UnityEngine.Random.Range(0f, 2f);
		if (Player.transform.position.x > base.transform.position.x)
		{
			Flip();
		}
		if (event_Type == AI_Mon_BrainGirl.Event_Type.MotherBrain)
		{
			global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Boss_4_Fire(base.transform.position);
			Pos_Target = new global::UnityEngine.Vector3(Pos_Target.x + global::UnityEngine.Random.Range(0f, 2f) * (float)facingRight, Pos_Target.y + global::UnityEngine.Random.Range(5f, 5.5f), 0f);
			Life_Timer = -0.5f;
		}
	}

	private void Set_BrainGirl_Poison()
	{
		on_Start_Poison = true;
	}

	private void Set_BrainGirl_Slow()
	{
		on_Start_Slow = true;
	}

	private void Update()
	{
		if (on_Start_Poison)
		{
			on_Start_Poison = false;
			Mon.GetComponent<Monster>().Set_Poison(1f);
		}
		if (on_Start_Slow)
		{
			on_Start_Slow = false;
			Mon.GetComponent<Monster>().Set_Slow(1.5f);
		}
		if (GM.Paused || onExplo || on_Hscene)
		{
			return;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (PC.State == Player_Control.AniState.Sit || PC.State == Player_Control.AniState.Slide)
		{
			pos_Y = global::UnityEngine.Mathf.Lerp(pos_Y, 2.5f, global::UnityEngine.Time.deltaTime * 0.5f);
		}
		else if (PC.State == Player_Control.AniState.Damage)
		{
			pos_Y = pos_Y;
		}
		else
		{
			pos_Y = global::UnityEngine.Mathf.Lerp(pos_Y, 5f, global::UnityEngine.Time.deltaTime * 0.8f);
		}
		distance = global::UnityEngine.Vector3.Distance(base.transform.position, new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + pos_Y, 0f));
		dist_X = global::UnityEngine.Mathf.Abs(base.transform.position.x - Player.transform.position.x);
		if (Life_Timer < 0f)
		{
			Move_Speed = Speed_Orig;
			base.transform.position = global::UnityEngine.Vector3.MoveTowards(base.transform.position, Pos_Target, global::UnityEngine.Time.deltaTime * 10f);
		}
		else if (H_Timer > 0f)
		{
			H_Timer -= global::UnityEngine.Time.deltaTime;
			if (GetComponent<global::UnityEngine.Animator>().GetBool("onHit"))
			{
				H_Timer = 0f;
			}
			if ((facingRight > 0 && base.transform.position.x > global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x) || (facingRight < 0 && base.transform.position.x < global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x))
			{
				Flip();
			}
			base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, Pos_Target, global::UnityEngine.Time.deltaTime * (0.5f + (float)GetComponent<Monster>().Gameover_Num * 0.2f));
		}
		else if (distance < 80f && (GM.GameOver || GM.onHscene))
		{
			if (GM.Hscene_Num == 34 || (GM.Hscene_Num == 0 && GM.Hscene_Timer <= 0f))
			{
				if (facingRight > 0 && base.transform.position.x > global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x)
				{
					Flip();
				}
				else if (facingRight < 0 && base.transform.position.x < global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x)
				{
					Flip();
				}
				dist_H = global::UnityEngine.Vector3.Distance(base.transform.position, new global::UnityEngine.Vector3(global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x, Player.transform.position.y + 5.2f, 0f));
				if (dist_H < 2f)
				{
					if (GM.Hscene_Num == 34)
					{
						GM.Hscene_Num = 35;
						Start_H_Dual();
					}
					else
					{
						Start_H_Single();
					}
				}
				else
				{
					Pos_Target = new global::UnityEngine.Vector3(global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x, Player.transform.position.y + 5.2f, 0f);
					Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, Speed_Orig, global::UnityEngine.Time.deltaTime * 2f);
					base.transform.position = global::UnityEngine.Vector3.MoveTowards(base.transform.position, Pos_Target, global::UnityEngine.Time.deltaTime * Move_Speed * GetComponent<Monster>().Move_Speed);
				}
			}
			else if (Patrol_State == 1)
			{
				Patrol_Move_Timer += global::UnityEngine.Time.deltaTime;
				if (Patrol_Move_Timer > 1f && global::UnityEngine.Mathf.Abs(global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x - base.transform.position.x) > Patrol_Range + Rnd_XY.x)
				{
					Patrol_State = 0;
					Patrol_Idle_Timer = 0f;
				}
				Pos_Target = new global::UnityEngine.Vector3(global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x + (Patrol_Range + 10f) * (float)facingRight, Player.transform.position.y + 6.5f - Rnd_XY.y * 3f, 0f);
				Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, Speed_Orig, global::UnityEngine.Time.deltaTime * 2f);
				base.transform.position = global::UnityEngine.Vector3.MoveTowards(base.transform.position, Pos_Target, global::UnityEngine.Time.deltaTime * Move_Speed * 0.25f * GetComponent<Monster>().Move_Speed);
			}
			else
			{
				Patrol_Idle_Timer += global::UnityEngine.Time.deltaTime;
				if (Patrol_Idle_Timer > 2.5f)
				{
					Patrol_Idle_Timer = 0f;
					Patrol_State = 1;
					Patrol_Move_Timer = 0f;
				}
				else if (Patrol_Idle_Timer > 1.5f && ((facingRight > 0 && base.transform.position.x > global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x) || (facingRight < 0 && base.transform.position.x < global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position.x)))
				{
					Flip();
				}
			}
		}
		else
		{
			if (!(distance < 60f))
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
			Pos_Target = new global::UnityEngine.Vector3(Player.transform.position.x - 1f * (float)facingRight + Rnd_XY.x, Player.transform.position.y + Rnd_XY.y + pos_Y + 0.2f, 0f);
			if (onAttack)
			{
				Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, 0f, global::UnityEngine.Time.deltaTime * 2.5f);
			}
			else if (GetComponent<global::UnityEngine.Animator>().GetBool("onHit"))
			{
				Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, 0f, global::UnityEngine.Time.deltaTime * 20f);
			}
			else if (distance > 15f || GM.Hscene_Timer > 0f)
			{
				Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, 0f, global::UnityEngine.Time.deltaTime * 1f);
			}
			else
			{
				Move_Speed = global::UnityEngine.Mathf.Lerp(Move_Speed, Speed_Orig, global::UnityEngine.Time.deltaTime * 1.2f);
			}
			base.transform.position = global::UnityEngine.Vector3.MoveTowards(base.transform.position, Pos_Target, global::UnityEngine.Time.deltaTime * Move_Speed * GetComponent<Monster>().Move_Speed);
			if (onAttack)
			{
				Attack_Timer += global::UnityEngine.Time.deltaTime;
				if (Attack_Timer > 0.25f)
				{
					onExplo = true;
					Mon.SendMessage("Death");
				}
			}
			else if (!GM.onCloth && GM.Option_Int[3] == 1 && PC.grounded_Now)
			{
				if (distance < 2.5f && GM.Hscene_Timer <= 0f && (PC.State == Player_Control.AniState.Idle || PC.State == Player_Control.AniState.Run || PC.State == Player_Control.AniState.Sit || PC.State == Player_Control.AniState.Down))
				{
					Start_H_Single();
				}
			}
			else if (distance < 5f)
			{
				Set_Attack();
			}
		}
	}

	private void Flip()
	{
		facingRight = -facingRight;
		Mon.Flip();
	}

	private void Set_Attack()
	{
		onAttack = true;
		Attack_Timer = 0f;
		Mon.SendMessage("Set_LockHit");
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", true);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
	}

	private void Set_Idle()
	{
		onAttack = false;
		Attack_Timer = 0f;
		Mon.SendMessage("End_LockHit");
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
	}

	public void FireExplo()
	{
		onExplo = true;
		float num = 60f;
		global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f);
		global::UnityEngine.Vector3 position = base.transform.position;
		vector.x -= position.x;
		vector.y -= position.y;
		num = global::UnityEngine.Mathf.Atan2(vector.y, vector.x) * 57.29578f + 180f;
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Fire, base.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, num)) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(_Fire, base.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, num + (float)global::UnityEngine.Random.Range(75, 85))) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(_Fire, base.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, num - (float)global::UnityEngine.Random.Range(75, 85))) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Boss_4_Fire(base.transform.position);
	}

	private void Set_AttackDelay()
	{
	}

	private void Start_H_Single()
	{
		Col_Body.enabled = false;
		on_Hscene = true;
		GM.onEvent = true;
		GM.onHscene = true;
		GM.Hscene_Num = 34;
		GM.Hscene_Timer = 1f;
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(H_Single, Player.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
		base.transform.position = new global::UnityEngine.Vector3(Player.transform.position.x + 2f * (float)(-facingRight), Player.transform.position.y + 5.2f, 0f);
		if (facingRight > 0)
		{
			gameObject.SendMessage("Flip");
		}
		gameObject.GetComponent<H_Ani>().Mon_Object = base.gameObject;
		global::UnityEngine.GameObject.Find("Menu").GetComponent<Menu_Control>().H_Object = gameObject;
		Start_Hscene();
		GM.GetComponent<H_Control>().facingRight = facingRight;
		GM.GetComponent<H_Control>().H_Object = gameObject;
		GM.GetComponent<H_Control>().Mon_1 = base.gameObject;
		if (PC.facingRight > 0 && base.transform.position.x < Player.transform.position.x)
		{
			Player.SendMessage("Flip");
		}
		else if (PC.facingRight < 0 && base.transform.position.x > Player.transform.position.x)
		{
			Player.SendMessage("Flip");
		}
	}

	private void Start_H_Dual()
	{
		Col_Body.enabled = false;
		on_Hscene = true;
		GM.onEvent = true;
		GM.onHscene = true;
		GM.Hscene_Num = 35;
		if (GM.GetComponent<H_Control>().H_Object != null)
		{
			GM.GetComponent<H_Control>().H_Object.SendMessage("Delete_ToDual");
		}
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(H_Dual, Player.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
		if (GM.GetComponent<H_Control>().facingRight > 0)
		{
			gameObject.SendMessage("Flip");
		}
		gameObject.GetComponent<H_Ani>().Mon_Object = GM.GetComponent<H_Control>().Mon_1;
		gameObject.GetComponent<H_Ani>().Mon_Object_2 = base.gameObject;
		global::UnityEngine.GameObject.Find("Menu").GetComponent<Menu_Control>().H_Object = gameObject;
		Start_Hscene();
		GM.GetComponent<H_Control>().H_Object = gameObject;
		GM.GetComponent<H_Control>().Mon_2 = base.gameObject;
		if ((GM.GetComponent<H_Control>().facingRight < 0 && facingRight < 0) || (GM.GetComponent<H_Control>().facingRight > 0 && facingRight > 0))
		{
			Flip();
		}
		base.transform.position = new global::UnityEngine.Vector3(Player.transform.position.x + 3f * (float)(-facingRight), Player.transform.position.y + 5.2f, 0f);
		H_Timer = 9f;
	}

	private void Start_Hscene()
	{
		H_Timer = 6f;
		Dual_Timer = 0f;
		GetComponent<Mon_Index>().OnOff_Object(false);
		GetComponent<global::UnityEngine.Animator>().speed = 0f;
		GetComponent<global::UnityEngine.Rigidbody2D>().Sleep();
		Mon.isInvincible = true;
		if (GM.Hscene_Num != 35)
		{
			global::UnityEngine.GameObject.Find("Main Camera").SendMessage("Hscene_Zoom");
		}
		if (!GM.GameOver)
		{
			Player.SendMessage("H_Down");
		}
		global::UnityEngine.GameObject.Find("Ani").SendMessage("Start_H_Scene");
		Set_Idle();
	}

	private void End_Hscene()
	{
		on_Hscene = false;
		GM.onEvent = false;
		GM.onHscene = false;
		GM.Hscene_Num = 0;
		Dual_Timer = 0f;
		GM.GetComponent<H_Control>().Reset();
		GetComponent<Mon_Index>().OnOff_Object(true);
		GetComponent<global::UnityEngine.Animator>().speed = 1f;
		GetComponent<global::UnityEngine.Rigidbody2D>().WakeUp();
		Col_Body.enabled = true;
		Mon.isInvincible = false;
		GM.Down_H_After();
		global::UnityEngine.GameObject.Find("Ani").SendMessage("End_H_Scene");
		Pos_Target = new global::UnityEngine.Vector3(base.transform.position.x + GameOver_XY.x * (float)(-facingRight), base.transform.position.y + GameOver_XY.y, 0f);
	}
}
