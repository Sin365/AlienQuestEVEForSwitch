public class Event_Scientist : global::UnityEngine.MonoBehaviour
{
	private bool isDeath;

	public global::UnityEngine.SpriteRenderer SR_Body_Front;

	public global::UnityEngine.SpriteRenderer SR_Head_Front;

	public global::UnityEngine.SpriteRenderer SR_Body_Back_1;

	public global::UnityEngine.SpriteRenderer SR_Head_Back_1;

	public global::UnityEngine.SpriteRenderer SR_Body_Back_2;

	public global::UnityEngine.SpriteRenderer SR_Head_Back_2;

	public global::UnityEngine.GameObject Explo;

	public global::UnityEngine.Transform[] posExplo;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	private void Update()
	{
		if (!GM.onEvent)
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public void Set_Front(int dir)
	{
		SR_Body_Back_1.enabled = false;
		SR_Head_Back_1.enabled = false;
		SR_Body_Back_2.enabled = false;
		SR_Head_Back_2.enabled = false;
		SR_Body_Front.enabled = true;
		SR_Head_Front.enabled = true;
		if (base.transform.localScale.x > 0f && dir < 0)
		{
			base.transform.localScale = new global::UnityEngine.Vector3(-1f, 1f, 1f);
		}
		else if (base.transform.localScale.x < 0f && dir > 0)
		{
			base.transform.localScale = new global::UnityEngine.Vector3(1f, 1f, 1f);
		}
	}

	private void OnTriggerStay2D(global::UnityEngine.Collider2D col)
	{
		if (!isDeath && col.tag == "Mon_Fire")
		{
			isDeath = true;
			for (int i = 0; i < posExplo.Length; i++)
			{
				global::UnityEngine.GameObject gameObject = (global::UnityEngine.GameObject)global::UnityEngine.Object.Instantiate(Explo, posExplo[i].position, posExplo[i].rotation);
				gameObject.transform.localScale = posExplo[i].transform.localScale;
			}
			global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Explo(base.transform.position);
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
