public class Magic_Fire_1 : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject BlueTail;

	public global::UnityEngine.GameObject Explo;

	public global::UnityEngine.Transform Pos_Explo;

	private float Life_Timer;

	private int facingRight = 1;

	private global::UnityEngine.Color Tail_Color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		if (base.transform.localScale.x < 0f)
		{
			facingRight = -1;
		}
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			Life_Timer += global::UnityEngine.Time.deltaTime;
			base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * 65f * facingRight);
			BlueTail.transform.localScale = new global::UnityEngine.Vector3(BlueTail.transform.localScale.x + global::UnityEngine.Time.deltaTime * 8f, 1f, 1f);
			BlueTail.GetComponent<global::UnityEngine.SpriteRenderer>().color = global::UnityEngine.Color.Lerp(BlueTail.GetComponent<global::UnityEngine.SpriteRenderer>().color, Tail_Color, global::UnityEngine.Time.deltaTime * 2f);
			if (Life_Timer > 0.6f)
			{
				Make_Explo();
			}
		}
	}

	private void Make_Explo()
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Explo, Pos_Explo.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
		global::UnityEngine.Object.Destroy(base.gameObject);
	}

	private void Set_Short()
	{
	}

	private void OnTriggerExit2D(global::UnityEngine.Collider2D col)
	{
		if (!GM.Paused && col.tag == "Col_Camera")
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void OnTriggerEnter2D(global::UnityEngine.Collider2D col)
	{
		if (!GM.Paused && (col.tag == "Ground" || col.tag == "Gate" || col.tag == "Breakable" || col.tag == "Mon_Shield"))
		{
			Make_Explo();
		}
	}
}
