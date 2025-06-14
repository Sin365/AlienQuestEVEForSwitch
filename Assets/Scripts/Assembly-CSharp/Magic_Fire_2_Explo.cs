public class Magic_Fire_2_Explo : global::UnityEngine.MonoBehaviour
{
	private float Life_Timer;

	private bool onCollision = true;

	private bool onColPlayer;

	private global::UnityEngine.Vector3 CircleScale = new global::UnityEngine.Vector3(3f, 3f, 1f);

	private global::UnityEngine.Vector3 CircleScale_2 = new global::UnityEngine.Vector3(2.5f, 2.5f, 1f);

	private float Brd_Size_X = 0.6f;

	private float Brd_SizeSpeed = 3f;

	private global::UnityEngine.Color color_Brd_OFF;

	private global::UnityEngine.Color color_Circle_OFF;

	public global::UnityEngine.GameObject Circle;

	public global::UnityEngine.GameObject Border;

	public global::UnityEngine.SpriteRenderer[] Border_Q;

	public global::UnityEngine.GameObject Dust;

	GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		float num = 10f + (float)global::UnityEngine.Random.Range(-20, 20) * 0.03f;
		for (int i = 1; i < 72; i++)
		{
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Dust, base.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, num * (float)i)) as global::UnityEngine.GameObject;
		}
		color_Brd_OFF = new global::UnityEngine.Color(Border_Q[0].color.r, Border_Q[0].color.g, Border_Q[0].color.b, 0f);
		global::UnityEngine.Color color = Circle.GetComponent<global::UnityEngine.SpriteRenderer>().color;
		color_Circle_OFF = new global::UnityEngine.Color(color.r, color.g, color.b, 0f);
		UnityEngine.Camera.main.GetComponent<Camera_Control>().Set_Shake_Timer(0.8f, base.transform.position);
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			Life_Timer += global::UnityEngine.Time.deltaTime;
			Brd_SizeSpeed = global::UnityEngine.Mathf.Lerp(Brd_SizeSpeed, 0.1f, global::UnityEngine.Time.deltaTime * 5f);
			Brd_Size_X += global::UnityEngine.Time.deltaTime * Brd_SizeSpeed;
			Border.transform.localScale = new global::UnityEngine.Vector3(Brd_Size_X, Brd_Size_X, 1f);
			global::UnityEngine.Color color;
			if (Life_Timer < 0.3f)
			{
				Circle.transform.localScale = global::UnityEngine.Vector3.Lerp(Circle.transform.localScale, CircleScale, global::UnityEngine.Time.deltaTime * 9f);
			}
			else
			{
				Circle.transform.localScale = global::UnityEngine.Vector3.Lerp(Circle.transform.localScale, CircleScale_2, global::UnityEngine.Time.deltaTime * 5f);
				Border_Q[8].color = global::UnityEngine.Color.Lerp(Border_Q[8].color, new global::UnityEngine.Color(1f, 1f, 1f, 0f), global::UnityEngine.Time.deltaTime * 22f);
				global::UnityEngine.SpriteRenderer obj = Border_Q[11];
				color = Border_Q[8].color;
				Border_Q[9].color = color;
				color = color;
				Border_Q[10].color = color;
				obj.color = color;
			}
			Border_Q[0].color = global::UnityEngine.Color.Lerp(Border_Q[0].color, color_Brd_OFF, global::UnityEngine.Time.deltaTime * 5f);
			global::UnityEngine.SpriteRenderer obj2 = Border_Q[3];
			color = Border_Q[0].color;
			Border_Q[1].color = color;
			color = color;
			Border_Q[2].color = color;
			obj2.color = color;
			Border_Q[4].color = global::UnityEngine.Color.Lerp(Border_Q[4].color, new global::UnityEngine.Color(1f, 1f, 1f, 0f), global::UnityEngine.Time.deltaTime * 8f);
			global::UnityEngine.SpriteRenderer obj3 = Border_Q[7];
			color = Border_Q[4].color;
			Border_Q[5].color = color;
			color = color;
			Border_Q[6].color = color;
			obj3.color = color;
			Circle.GetComponent<global::UnityEngine.SpriteRenderer>().color = global::UnityEngine.Color.Lerp(Circle.GetComponent<global::UnityEngine.SpriteRenderer>().color, color_Circle_OFF, global::UnityEngine.Time.deltaTime * 15f);
			if (Life_Timer > 2f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			else if (Life_Timer > 0.15f && onCollision)
			{
				onCollision = false;
				GetComponent<global::UnityEngine.CircleCollider2D>().enabled = false;
			}
		}
	}

	private void OnTriggerStay2D(global::UnityEngine.Collider2D col)
	{
		if (GM.Paused || GM.onSave || GM.onGatePass)
		{
			return;
		}
		if (!GM.GameOver && !GM.onHscene && !GM.onDown && GM.Damage_Timer <= 0f && !onColPlayer && col.name == "Ani")
		{
			onColPlayer = true;
			GM.Damage((int)((float)GM.HP_Max * 0.5f), 0f, true, 0);
			GM.eg2d_Player.velocity = new global::UnityEngine.Vector2(0f, 0f);
			int num = ((!(base.transform.position.x > col.transform.position.x)) ? 1 : (-1));
			GM.eg2d_Player.AddForce(global::UnityEngine.Vector3.right * num * 20f, global::UnityEngine.ForceMode2D.Impulse);
			if (col.transform.position.y > base.transform.position.y)
			{
				GM.eg2d_Player.AddForce(global::UnityEngine.Vector3.up * 20f, global::UnityEngine.ForceMode2D.Impulse);
			}
		}
		else if (col.tag == "Breakable")
		{
			col.gameObject.SendMessage("Break");
		}
	}
}
