public class Event_General : global::UnityEngine.MonoBehaviour
{
	private bool isDeath;

	public global::UnityEngine.SpriteRenderer SR_Mouth;

	public global::UnityEngine.SpriteRenderer SR_Arm_1;

	public global::UnityEngine.SpriteRenderer SR_Arm_2;

	public global::UnityEngine.SpriteRenderer SR_Arm_2B;

	public global::UnityEngine.SpriteRenderer SR_Leg;

	public global::UnityEngine.GameObject Explo;

	public global::UnityEngine.Transform[] posExplo;

    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	private void Update()
	{
		if (!GM.onEvent)
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void Set_Shocked()
	{
		SR_Mouth.enabled = true;
		SR_Arm_1.enabled = false;
		SR_Arm_2.enabled = true;
		SR_Arm_2B.enabled = true;
		SR_Leg.enabled = true;
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
			GameManager.instance.sc_Sound_List.Mon_Explo(base.transform.position);
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
