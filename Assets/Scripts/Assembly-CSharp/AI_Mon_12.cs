using UnityEngine;

public class AI_Mon_12 : global::UnityEngine.MonoBehaviour
{
	private int EnemyState;

	private float Life_Timer;

	private float State_Timer;

	private float distance;

	private bool wasAttacked;

	private bool Range_Attack;

	private bool onAttack;

	private float Attack_Delay;

	private global::UnityEngine.Vector3 size_Orig;

	public global::UnityEngine.GameObject MonGate_Laser;

	private global::UnityEngine.GameObject Laser;

	public global::UnityEngine.GameObject MonGate_Shield;

	private global::UnityEngine.GameObject Shield;

	public global::UnityEngine.SpriteRenderer EyePupil;

	public global::UnityEngine.SpriteRenderer EyeGlow;

	private float Glow_Timer;

	private global::UnityEngine.Color Glow_Color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	public global::UnityEngine.GameObject Ctrl_Up;

	public global::UnityEngine.GameObject Ctrl_Down;

	public global::UnityEngine.GameObject Ctrl_Pupil;

	private float UpDegree;

	private float DownDegree;

	private float EyeDegree;

	private float EyeTarget;

	private float Eye_Timer;

	public global::UnityEngine.Transform Tr_Pos;

	public global::UnityEngine.Transform Tr_1_Start;

	public global::UnityEngine.Transform Tr_1_End;

	public global::UnityEngine.Transform Tr_2_Start;

	public global::UnityEngine.Transform Tr_2_End;

	public global::UnityEngine.Transform Tr_3_Start;

	public global::UnityEngine.Transform Tr_3_End;

	private global::UnityEngine.RaycastHit2D whatIHit;

    Player_Control PC => GameManager.instance.PC;
    GameObject Player => GameManager.instance.gobj_Player;

    private GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//Player = global::UnityEngine.GameObject.Find("Player");
		Shield = global::UnityEngine.Object.Instantiate(MonGate_Shield, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
		Shield.GetComponent<Mon_Shield>().MonObject = base.gameObject;
		if (Tr_Pos.localScale.x < 0f)
		{
			Shield.transform.localScale = new global::UnityEngine.Vector3(-1f, 1f, 1f);
		}
		size_Orig = new global::UnityEngine.Vector3(1f, 1f, 1f);
		EyeGlow.color = Glow_Color;
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
		State_Timer += global::UnityEngine.Time.deltaTime;
		Raycasting();
		if (base.transform.localScale.x < 1f)
		{
			base.transform.localScale = global::UnityEngine.Vector3.Lerp(base.transform.localScale, size_Orig, global::UnityEngine.Time.deltaTime * 10f);
		}
		if (distance > 45f)
		{
			if (EnemyState > 0)
			{
				EnemyState = 0;
				State_Timer = 0f;
				Set_Idle();
				Set_Close();
				EyePupil.enabled = true;
				EyeGlow.enabled = false;
				if (Laser != null)
				{
					Laser.SendMessage("End_Laser");
				}
			}
			return;
		}
		if (GetComponent<global::UnityEngine.Animator>().GetBool("onHit"))
		{
			if (!wasAttacked)
			{
				wasAttacked = true;
			}
			EyePupil.enabled = true;
			EyeGlow.enabled = false;
			UpDegree = global::UnityEngine.Mathf.Lerp(UpDegree, -20f, global::UnityEngine.Time.deltaTime * 16f);
			Ctrl_Up.transform.localRotation = global::UnityEngine.Quaternion.Euler(0f, 0f, UpDegree);
			DownDegree = global::UnityEngine.Mathf.Lerp(DownDegree, 23f, global::UnityEngine.Time.deltaTime * 16f);
			Ctrl_Down.transform.localRotation = global::UnityEngine.Quaternion.Euler(0f, 0f, DownDegree);
			EyeDegree = global::UnityEngine.Mathf.Lerp(EyeDegree, -18f, global::UnityEngine.Time.deltaTime * 20f);
			Ctrl_Pupil.transform.localRotation = global::UnityEngine.Quaternion.Euler(0f, 0f, EyeDegree);
			if (State_Timer > 3f)
			{
				EnemyState = 2;
				State_Timer = 0f;
				Set_Close();
			}
			return;
		}
		if (EnemyState < 2)
		{
			Ani_Open();
			Eye_Timer += global::UnityEngine.Time.deltaTime;
			if (Eye_Timer > 0.5f && global::UnityEngine.Random.Range(0, 10) > 4)
			{
				Eye_Timer = 0f;
				EyeTarget = global::UnityEngine.Random.Range(-20f, 20f);
			}
			EyeDegree = global::UnityEngine.Mathf.Lerp(EyeDegree, EyeTarget, global::UnityEngine.Time.deltaTime * 6f);
			Ctrl_Pupil.transform.localRotation = global::UnityEngine.Quaternion.Euler(0f, 0f, EyeDegree);
			if ((wasAttacked && State_Timer > 3f) || State_Timer > 5f)
			{
				EnemyState = 2;
				State_Timer = 0f;
				Set_Close();
			}
			return;
		}
		if (EnemyState == 2)
		{
			Ani_Close();
			if (!(State_Timer > 3f))
			{
				return;
			}
			if (wasAttacked && Range_Attack)
			{
				if (global::UnityEngine.Random.Range(0, 10) > 2)
				{
					EnemyState = 3;
					GetComponent<Monster>().isLockHit = true;
					EyePupil.enabled = false;
					EyeGlow.enabled = true;
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
			State_Timer = 0f;
			Set_Open();
			return;
		}
		Ani_Open();
		if (State_Timer > 3.2f)
		{
			EnemyState = 2;
			State_Timer = 0f;
			Set_Idle();
			Set_Close();
			EyePupil.enabled = true;
			EyeGlow.enabled = false;
			if (Laser != null)
			{
				Laser.SendMessage("End_Laser");
			}
		}
		else if (State_Timer > 0.5f && !onAttack)
		{
			Set_Attack();
		}
		Glow_Timer += global::UnityEngine.Time.deltaTime;
		if (Glow_Timer > 0.1f)
		{
			Glow_Timer = 0f;
			Glow_Color = new global::UnityEngine.Color(1f, 1f, 1f, global::UnityEngine.Random.Range(0.25f, 0.5f));
		}
		EyeGlow.color = global::UnityEngine.Color.Lerp(EyeGlow.color, Glow_Color, global::UnityEngine.Time.deltaTime * 10f);
	}

	private void Ani_Open()
	{
		UpDegree = global::UnityEngine.Mathf.Lerp(UpDegree, -30f, global::UnityEngine.Time.deltaTime * 6f);
		Ctrl_Up.transform.localRotation = global::UnityEngine.Quaternion.Euler(0f, 0f, UpDegree);
		DownDegree = global::UnityEngine.Mathf.Lerp(DownDegree, 30f, global::UnityEngine.Time.deltaTime * 6f);
		Ctrl_Down.transform.localRotation = global::UnityEngine.Quaternion.Euler(0f, 0f, DownDegree);
	}

	private void Ani_Close()
	{
		UpDegree = global::UnityEngine.Mathf.Lerp(UpDegree, 0f, global::UnityEngine.Time.deltaTime * 16f);
		Ctrl_Up.transform.localRotation = global::UnityEngine.Quaternion.Euler(0f, 0f, UpDegree);
		DownDegree = global::UnityEngine.Mathf.Lerp(DownDegree, 0f, global::UnityEngine.Time.deltaTime * 16f);
		Ctrl_Down.transform.localRotation = global::UnityEngine.Quaternion.Euler(0f, 0f, DownDegree);
		EyeDegree = global::UnityEngine.Mathf.Lerp(EyeDegree, 0f, global::UnityEngine.Time.deltaTime * 6f);
		Ctrl_Pupil.transform.localRotation = global::UnityEngine.Quaternion.Euler(0f, 0f, EyeDegree);
	}

	private void Set_Close()
	{
		GetComponent<global::UnityEngine.BoxCollider2D>().enabled = false;
		GetComponent<Monster>().isLockHit = true;
		onAttack = false;
	}

	private void Set_Open()
	{
		GetComponent<global::UnityEngine.BoxCollider2D>().enabled = true;
		if (EnemyState < 3)
		{
			GetComponent<Monster>().isLockHit = false;
		}
	}

	private void Set_Idle()
	{
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onMove", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
		GetComponent<Monster>().isLockHit = false;
	}

	private void Set_Attack()
	{
		onAttack = true;
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", true);
		GetComponent<global::UnityEngine.Animator>().SetBool("onMove", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
		GetComponent<Monster>().isLockHit = true;
		Laser = global::UnityEngine.Object.Instantiate(MonGate_Laser, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
		if (GetComponent<Monster>().Mon_Num == 12)
		{
			Laser.GetComponent<Mon_GateLaser>().MonObject = base.gameObject;
		}
		else
		{
			Laser.GetComponent<Mon_GateLaser_2>().MonObject = base.gameObject;
		}
		if (Tr_Pos.localScale.x < 0f)
		{
			Laser.transform.localScale = new global::UnityEngine.Vector3(-1f, 1f, 1f);
		}
	}

	private void End_Attack()
	{
		GetComponent<global::UnityEngine.Animator>().SetBool("onAttack", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onMove", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHit", false);
	}

	private void Set_AttackDelay()
	{
		if (Attack_Delay < 1f)
		{
			Attack_Delay = 1f;
		}
	}

	private void Sound_Mon_Dmg()
	{
		GameManager.instance.sc_Sound_List.Mon_5_Damage(base.transform.position);
	}

	private void Set_Block()
	{
		base.transform.localScale = new global::UnityEngine.Vector3(0.92f, 1f, 1f);
	}

	private void Raycasting()
	{
		bool flag = global::UnityEngine.Physics2D.Linecast(Tr_1_Start.position, Tr_1_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player"));
		bool flag2 = global::UnityEngine.Physics2D.Linecast(Tr_2_Start.position, Tr_2_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player"));
		bool flag3 = global::UnityEngine.Physics2D.Linecast(Tr_3_Start.position, Tr_3_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player"));
		distance = global::UnityEngine.Vector3.Distance(new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f, Player.transform.position.z), base.transform.position);
		if (flag || flag2 || flag3)
		{
			Range_Attack = true;
		}
		else
		{
			Range_Attack = false;
		}
	}
}
