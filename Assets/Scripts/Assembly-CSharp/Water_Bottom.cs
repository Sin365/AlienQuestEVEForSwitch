public class Water_Bottom : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.Transform[] pos_Center;

	public global::UnityEngine.GameObject _water_Dust;

	public global::UnityEngine.GameObject _water_Dust_2;

	public global::UnityEngine.GameObject _water_Highlight;

	public global::UnityEngine.GameObject[] _sound_WaterWalk;

	public global::UnityEngine.GameObject[] _sound_WaterImpact;

	public int FlowRight = -1;

	private float Hightlight_Timer;

	private float Player_Walk_Timer;

	private float Player_Walk_Timer_2;

	private float Mon_Walk_Timer;

	private float Dust_1_Timer;

	private float Dust_2_Timer;

	private global::UnityEngine.Vector3 pos_Dust;

	private int sound_walk_index;

	private global::UnityEngine.Color color_ON = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_OFF = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	private Player_Control PC;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		PC = global::UnityEngine.GameObject.Find("Player").GetComponent<Player_Control>();
		pos_Dust = new global::UnityEngine.Vector3(PC.transform.position.x, PC.transform.position.y + 1.5f, 0f);
		if (pos_Center.Length > 0)
		{
			for (int i = 0; i < pos_Center.Length; i++)
			{
				Make_Highlight(i);
			}
		}
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		if (Player_Walk_Timer > 0f)
		{
			Player_Walk_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Player_Walk_Timer_2 > 0f)
		{
			Player_Walk_Timer_2 -= global::UnityEngine.Time.deltaTime;
		}
		if (Mon_Walk_Timer > 0f)
		{
			Mon_Walk_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Dust_1_Timer > 0f)
		{
			Dust_1_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Dust_2_Timer > 0f)
		{
			Dust_2_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Hightlight_Timer > 0f)
		{
			Hightlight_Timer -= global::UnityEngine.Time.deltaTime;
		}
		else if (pos_Center.Length > 0)
		{
			for (int i = 0; i < pos_Center.Length; i++)
			{
				Make_Highlight(i);
			}
		}
	}

	private void Sound_Player_Walk()
	{
		if (Player_Walk_Timer <= 0f)
		{
			Player_Walk_Timer = 0.31f;
			sound_walk_index++;
			if (sound_walk_index > 2)
			{
				sound_walk_index = 0;
			}
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_sound_WaterWalk[sound_walk_index], PC.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
		}
		if (Player_Walk_Timer_2 <= 0f)
		{
			Player_Walk_Timer_2 = 1.2f;
			global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(_sound_WaterWalk[3], PC.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
		}
	}

	private void Make_Highlight(int num)
	{
		Hightlight_Timer = global::UnityEngine.Random.Range(0.025f, 0.1f);
		for (int i = 0; i < 2; i++)
		{
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_water_Highlight, new global::UnityEngine.Vector3(pos_Center[num].position.x + global::UnityEngine.Random.Range(-8f, 8f), base.transform.position.y - 0.08f, 0f), base.transform.rotation) as global::UnityEngine.GameObject;
		}
	}

	private void Make_Dust_1_Small()
	{
		if (Dust_1_Timer <= 0f)
		{
			Dust_1_Timer = 0.1f;
			pos_Dust = new global::UnityEngine.Vector3(PC.transform.position.x, base.transform.position.y, 0f);
			for (int i = 0; i < 5; i++)
			{
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_water_Dust, new global::UnityEngine.Vector3(pos_Dust.x + global::UnityEngine.Random.Range(-1f, 1f), pos_Dust.y + global::UnityEngine.Random.Range(0f, 0.1f), 0f), base.transform.rotation) as global::UnityEngine.GameObject;
				gameObject.rigidbody2D.AddForce(new global::UnityEngine.Vector2(global::UnityEngine.Random.Range(-70, 70) * PC.facingRight, global::UnityEngine.Random.Range(150, 220)));
			}
		}
	}

	private void Make_Dust_1_Move()
	{
		if (Dust_1_Timer <= 0f)
		{
			Dust_1_Timer = 0.05f;
			pos_Dust = new global::UnityEngine.Vector3(PC.transform.position.x, base.transform.position.y, 0f);
			for (int i = 0; i < 10; i++)
			{
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_water_Dust, new global::UnityEngine.Vector3(pos_Dust.x + global::UnityEngine.Random.Range(-1f, 1f), pos_Dust.y + global::UnityEngine.Random.Range(0f, 0.1f), 0f), base.transform.rotation) as global::UnityEngine.GameObject;
				gameObject.rigidbody2D.AddForce(new global::UnityEngine.Vector2(global::UnityEngine.Random.Range(-50, 200) * PC.facingRight, global::UnityEngine.Random.Range(150, 300)));
			}
		}
	}

	private void Make_Dust_1_Jump_Begin()
	{
		if (Dust_1_Timer <= 0f)
		{
			Dust_1_Timer = 0.05f;
			pos_Dust = new global::UnityEngine.Vector3(PC.transform.position.x, base.transform.position.y, 0f);
			for (int i = 0; i < 50; i++)
			{
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_water_Dust, new global::UnityEngine.Vector3(pos_Dust.x + global::UnityEngine.Random.Range(-0.5f, 0.5f), pos_Dust.y + global::UnityEngine.Random.Range(0f, 0.8f), 0f), base.transform.rotation) as global::UnityEngine.GameObject;
				gameObject.rigidbody2D.AddForce(new global::UnityEngine.Vector2(global::UnityEngine.Random.Range(-80, 80), global::UnityEngine.Random.Range(150, 500)));
			}
		}
	}

	private void Make_Dust_1_Jump_End()
	{
		if (Dust_1_Timer <= 0f)
		{
			Dust_1_Timer = 0.05f;
			pos_Dust = new global::UnityEngine.Vector3(PC.transform.position.x, base.transform.position.y, 0f);
			for (int i = 0; i < 80; i++)
			{
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_water_Dust, new global::UnityEngine.Vector3(pos_Dust.x + global::UnityEngine.Random.Range(-1f, 1f), pos_Dust.y + global::UnityEngine.Random.Range(0f, 0.1f), 0f), base.transform.rotation) as global::UnityEngine.GameObject;
				gameObject.rigidbody2D.AddForce(new global::UnityEngine.Vector2(global::UnityEngine.Random.Range(-200, 200), global::UnityEngine.Random.Range(150, 500)));
			}
		}
	}

	private void Make_Dust_2_Move()
	{
		if (Dust_2_Timer <= 0f)
		{
			Dust_2_Timer = 0.2f;
			for (int i = 0; i < 4; i++)
			{
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_water_Dust_2, new global::UnityEngine.Vector3(pos_Dust.x + global::UnityEngine.Random.Range(-1.5f, 1.5f), base.transform.position.y - global::UnityEngine.Random.Range(0.5f, 0.6f), 0f), base.transform.rotation) as global::UnityEngine.GameObject;
				gameObject.rigidbody2D.AddForce(new global::UnityEngine.Vector2(global::UnityEngine.Random.Range(-20, 20) * PC.facingRight, global::UnityEngine.Random.Range(0, 50)));
			}
		}
	}

	private void Make_Dust_2_Jump()
	{
		if (Dust_2_Timer <= 0f)
		{
			Dust_2_Timer = 0.1f;
			for (int i = 0; i < 7; i++)
			{
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_water_Dust_2, new global::UnityEngine.Vector3(pos_Dust.x + global::UnityEngine.Random.Range(-1.5f, 1.5f), base.transform.position.y - global::UnityEngine.Random.Range(0.5f, 0.6f), 0f), base.transform.rotation) as global::UnityEngine.GameObject;
				gameObject.rigidbody2D.AddForce(new global::UnityEngine.Vector2(global::UnityEngine.Random.Range(-20, 20) * PC.facingRight, global::UnityEngine.Random.Range(0, 50)));
			}
		}
	}

	private void OnTriggerExit2D(global::UnityEngine.Collider2D col)
	{
		if (!GM.Paused && col.name == "Ani" && (PC.State.ToString() == "Jump" || PC.State.ToString() == "Down"))
		{
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_sound_WaterImpact[0], PC.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
			Make_Dust_1_Jump_Begin();
			Make_Dust_2_Jump();
		}
	}

	private void OnTriggerEnter2D(global::UnityEngine.Collider2D col)
	{
		if (!GM.Paused && col.name == "Ani" && (PC.State.ToString() == "Jump" || PC.State.ToString() == "Down"))
		{
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_sound_WaterImpact[1], PC.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
			Make_Dust_1_Jump_End();
			Make_Dust_2_Jump();
		}
	}

	private void OnTriggerStay2D(global::UnityEngine.Collider2D col)
	{
		if (GM.Paused || !(col.name == "Ani"))
		{
			return;
		}
		if (PC.State.ToString() == "Run" || PC.State.ToString() == "Spin" || PC.State.ToString() == "Slide" || PC.State.ToString() == "BackDash" || PC.State.ToString() == "Damage")
		{
			Sound_Player_Walk();
			Make_Dust_1_Move();
			if (PC.State.ToString() == "Slide")
			{
				Make_Dust_2_Move();
			}
		}
		else if (PC.onAttack && PC.Attack_Num > 2)
		{
			Sound_Player_Walk();
			Make_Dust_1_Move();
			Make_Dust_2_Move();
		}
		else if (PC.onAttack && PC.State.ToString() == "Sit")
		{
			Sound_Player_Walk();
			Make_Dust_1_Small();
		}
	}
}
