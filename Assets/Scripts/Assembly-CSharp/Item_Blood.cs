public class Item_Blood : global::UnityEngine.MonoBehaviour
{
	public int heal_HP;

	private float Life_Timer;

	private float Size = 1f;

	public bool Impact;

	private float Stay_Timer;

	public bool onChase;

	public bool onChase_Absorb;

	private float Absorb_Change_Speed = 1f;

	private float Absorb_Distance;

	public global::UnityEngine.GameObject Absorb_Target;

	private float rnd_X;

	private float rnd_Y;

	public float distance;

	private float Miss_Timer;

	private float Circle_Timer;

	private global::UnityEngine.Vector3 pos_Target;

	public global::UnityEngine.GameObject Outer;

	public global::UnityEngine.GameObject Circle;

	private global::UnityEngine.BoxCollider2D PlayerColBox;

    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		PlayerColBox = global::UnityEngine.GameObject.Find("Ani").GetComponent<global::UnityEngine.BoxCollider2D>();
		rnd_X = (float)global::UnityEngine.Random.Range(0, 20) * 0.01f;
		rnd_Y = (float)global::UnityEngine.Random.Range(0, 20) * 0.01f;
		pos_Target = new global::UnityEngine.Vector3(PlayerColBox.transform.position.x + rnd_X, PlayerColBox.transform.position.y + ((!(PlayerColBox.size.y > 3f)) ? 1.8f : 3f) + rnd_Y, PlayerColBox.transform.position.z);
		distance = global::UnityEngine.Vector3.Distance(pos_Target, base.transform.position);
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
		Circle_Timer += global::UnityEngine.Time.deltaTime;
		if (Life_Timer > 6f || Impact)
		{
			Size = Outer.transform.localScale.x;
			Size -= global::UnityEngine.Time.deltaTime * ((!Impact) ? 1f : 3f);
			if (Size <= 0f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
				return;
			}
			base.transform.localScale = new global::UnityEngine.Vector3(Size, Size, 1f);
			Outer.transform.localScale = new global::UnityEngine.Vector3(Size, Size, 1f);
			return;
		}
		float num = global::UnityEngine.Mathf.Sin(Life_Timer * 13f) * 0.1f;
		Outer.transform.localScale = new global::UnityEngine.Vector3(0.7f + num, 0.7f + num, 1f);
		if (onChase_Absorb && Absorb_Target != null)
		{
			pos_Target = Absorb_Target.transform.position;
		}
		else
		{
			if (onChase_Absorb)
			{
				onChase_Absorb = false;
				Absorb_Change_Speed = 0f;
			}
			pos_Target = new global::UnityEngine.Vector3(PlayerColBox.transform.position.x + rnd_X, PlayerColBox.transform.position.y + ((!(PlayerColBox.size.y > 3f)) ? 1.8f : 3f) + rnd_Y, PlayerColBox.transform.position.z);
		}
		distance = global::UnityEngine.Vector3.Distance(pos_Target, base.transform.position);
		Absorb_Change_Speed = global::UnityEngine.Mathf.Lerp(Absorb_Change_Speed, 1f, global::UnityEngine.Time.deltaTime);
		if (distance < 10f)
		{
			onChase = true;
			base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, pos_Target, global::UnityEngine.Time.deltaTime * (10f - distance) * 0.5f) * Absorb_Change_Speed;
			if (distance < 3f && onChase_Absorb && Absorb_Target != null)
			{
				Impact = true;
				if (Absorb_Target.GetComponent<Item_Blood_Absorb>() != null)
				{
					Absorb_Target.GetComponent<Item_Blood_Absorb>().Get_Blood(heal_HP * 10);
				}
			}
			else if (distance > 0.1f && Circle_Timer > 0.01f)
			{
				Circle_Timer = 0f;
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Circle, base.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
			}
		}
		else if (onChase)
		{
			onChase = false;
			Miss_Timer = 0.5f;
		}
		else if (Miss_Timer > 0f)
		{
			Miss_Timer -= global::UnityEngine.Time.deltaTime;
			base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, pos_Target, global::UnityEngine.Time.deltaTime * Miss_Timer);
		}
	}

	private void OnTriggerStay2D(global::UnityEngine.Collider2D col)
	{
		if (!Impact && !GM.Paused && !GM.GameOver && col.tag == "Player_Col")
		{
			Stay_Timer += global::UnityEngine.Time.deltaTime;
			if (Stay_Timer > 0.01f)
			{
				Impact = true;
				GM.Get_Blood(heal_HP);
			}
		}
	}
}
