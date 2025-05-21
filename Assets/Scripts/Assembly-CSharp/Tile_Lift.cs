public class Tile_Lift : global::UnityEngine.MonoBehaviour
{
	public int Type;

	public float Speed;

	private bool onEnabled = true;

	private bool Col_ON = true;

	private float Start_Timer;

	private float Life_Timer;

	private float pre_X;

	public float Speed_for_PC;

	public global::UnityEngine.Transform pos_1;

	public global::UnityEngine.Transform pos_2;

	private float Dist_Half = 7f;

	private global::UnityEngine.Vector3 pos_Start;

	private int Event_Num = 4;

	private float Event_Speed;

	private global::UnityEngine.Vector3 pos_Target;

	private GameManager GM;

	private global::UnityEngine.GameObject Player;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		Player = global::UnityEngine.GameObject.Find("Player");
		if (Type == 0)
		{
			Dist_Half = global::UnityEngine.Vector3.Distance(pos_1.position, pos_2.position) * 0.5f;
			float num = (base.transform.position.y - pos_2.position.y) / (pos_1.position.y - pos_2.position.y);
			global::UnityEngine.Vector3 position = new global::UnityEngine.Vector3(base.transform.position.x, (pos_1.position.y + pos_2.position.y) * 0.5f, 0f);
			base.transform.position = position;
			pos_Start = position;
			Life_Timer = (float)global::System.Math.PI / 180f * (-90f + 180f * num);
			base.transform.position = new global::UnityEngine.Vector3(pos_Start.x, pos_Start.y + global::UnityEngine.Mathf.Sin(Life_Timer * Speed) * Dist_Half, 0f);
		}
		else if (Type == 1)
		{
			Dist_Half = global::UnityEngine.Vector3.Distance(pos_1.position, pos_2.position) * 0.5f;
			float num2 = (base.transform.position.x - pos_2.position.x) / (pos_1.position.x - pos_2.position.x);
			global::UnityEngine.Vector3 position2 = new global::UnityEngine.Vector3((pos_1.position.x + pos_2.position.x) * 0.5f, base.transform.position.y, 0f);
			base.transform.position = position2;
			pos_Start = position2;
			pre_X = base.transform.position.x;
			Life_Timer = (float)global::System.Math.PI / 180f * (-90f + 180f * num2);
			base.transform.position = new global::UnityEngine.Vector3(pos_Start.x + global::UnityEngine.Mathf.Sin(Life_Timer * Speed) * Dist_Half, pos_Start.y, 0f);
		}
		else
		{
			pos_Target = new global::UnityEngine.Vector3(base.transform.position.x, base.transform.position.y - 34.92f, 0f);
			if (GM.Get_Event(Event_Num))
			{
				base.transform.position = pos_Target;
				onEnabled = true;
				Lift_Start();
			}
			else
			{
				onEnabled = false;
				Lift_Stop();
			}
		}
	}

	private void FixedUpdate()
	{
		if (GM.Paused || GM.onGatePass)
		{
			return;
		}
		if (Start_Timer < 0.35f)
		{
			Start_Timer += global::UnityEngine.Time.deltaTime;
			return;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (Type == 0)
		{
			base.transform.position = new global::UnityEngine.Vector3(pos_Start.x, pos_Start.y + global::UnityEngine.Mathf.Sin(Life_Timer * Speed) * Dist_Half, 0f);
		}
		else if (Type == 1)
		{
			base.transform.position = new global::UnityEngine.Vector3(pos_Start.x + global::UnityEngine.Mathf.Sin(Life_Timer * Speed) * Dist_Half, pos_Start.y, 0f);
			Speed_for_PC = (base.transform.position.x - pre_X) / global::UnityEngine.Time.deltaTime;
			pre_X = base.transform.position.x;
		}
		else if (onEnabled)
		{
			if (Event_Speed < 1f)
			{
				Event_Speed += global::UnityEngine.Time.deltaTime * 0.1f;
			}
			base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, pos_Target, global::UnityEngine.Time.deltaTime * Event_Speed);
		}
	}

	private void Update()
	{
		if (GM.GameOver)
		{
			if (onEnabled && Col_ON)
			{
				Lift_Stop();
			}
		}
		else if (onEnabled && !Col_ON)
		{
			Lift_Start();
		}
		if (GM.Paused)
		{
		}
	}

	private void Lift_Stop()
	{
		Col_ON = false;
		global::UnityEngine.BoxCollider2D[] components = GetComponents<global::UnityEngine.BoxCollider2D>();
		global::UnityEngine.BoxCollider2D[] array = components;
		foreach (global::UnityEngine.BoxCollider2D boxCollider2D in array)
		{
			boxCollider2D.enabled = false;
		}
	}

	public void Lift_Start()
	{
		Col_ON = true;
		global::UnityEngine.BoxCollider2D[] components = GetComponents<global::UnityEngine.BoxCollider2D>();
		global::UnityEngine.BoxCollider2D[] array = components;
		foreach (global::UnityEngine.BoxCollider2D boxCollider2D in array)
		{
			boxCollider2D.enabled = true;
		}
		if (!onEnabled)
		{
			onEnabled = true;
		}
	}
}
