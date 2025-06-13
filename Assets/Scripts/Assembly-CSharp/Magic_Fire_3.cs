public class Magic_Fire_3 : global::UnityEngine.MonoBehaviour
{
	private bool OnExplo;

	private float Life_Timer;

	private int facingRight = 1;

	private global::UnityEngine.Color color_Target = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Vector2 Velocity;

	public global::UnityEngine.GameObject Glow;

	public global::UnityEngine.GameObject Glow_Add;

	public global::UnityEngine.GameObject Obj_Explo;

	GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		if (base.transform.localScale.x < 0f)
		{
			facingRight = -1;
			base.transform.rotation = global::UnityEngine.Quaternion.Euler(0f, 0f, 200f);
		}
		Velocity = base.GetComponent<UnityEngine.Rigidbody2D>().velocity;
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			if (GetComponent<global::UnityEngine.Rigidbody2D>().IsSleeping())
			{
				GetComponent<global::UnityEngine.Rigidbody2D>().WakeUp();
				base.GetComponent<UnityEngine.Rigidbody2D>().velocity = Velocity;
				global::UnityEngine.Debug.Log("WakeUp");
			}
			Life_Timer += global::UnityEngine.Time.deltaTime;
			base.transform.Rotate(0f, 0f, (float)(facingRight * -800) * global::UnityEngine.Time.deltaTime);
			Glow.GetComponent<global::UnityEngine.SpriteRenderer>().color = global::UnityEngine.Color.Lerp(Glow.GetComponent<global::UnityEngine.SpriteRenderer>().color, color_Target, global::UnityEngine.Time.deltaTime * 5f);
			Glow_Add.GetComponent<global::UnityEngine.SpriteRenderer>().color = global::UnityEngine.Color.Lerp(Glow_Add.GetComponent<global::UnityEngine.SpriteRenderer>().color, color_Target, global::UnityEngine.Time.deltaTime * 1.5f);
			if (Life_Timer > 3f)
			{
				Destroy_Self();
			}
		}
		else if (GetComponent<global::UnityEngine.Rigidbody2D>().IsAwake())
		{
			Velocity = base.GetComponent<UnityEngine.Rigidbody2D>().velocity;
			GetComponent<global::UnityEngine.Rigidbody2D>().Sleep();
			global::UnityEngine.Debug.Log("Sleep");
		}
	}

	private void Destroy_Self()
	{
		global::UnityEngine.Object.Destroy(base.gameObject);
	}

	private void Explo()
	{
		if (!OnExplo)
		{
			OnExplo = true;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Obj_Explo, new global::UnityEngine.Vector3(base.transform.position.x, base.transform.position.y + 0.8f, 0f), global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
			GameManager.instance.sc_Sound_List.Magic_3_Explo(base.transform.position);
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void OnTriggerEnter2D(global::UnityEngine.Collider2D col)
	{
		if (!GM.Paused && (col.tag == "Ground" || col.tag == "Gate" || col.tag == "Breakable" || col.tag == "Mon_Shield"))
		{
			Explo();
		}
	}
}
