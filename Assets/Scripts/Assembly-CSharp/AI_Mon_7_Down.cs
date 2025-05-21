public class AI_Mon_7_Down : global::UnityEngine.MonoBehaviour
{
	private int facingRight = -1;

	public global::UnityEngine.GameObject Mon_7_Obejct;

	public global::UnityEngine.GameObject Ctrl_1;

	private int mon_Index;

	private float Life_Timer;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		Life_Timer += global::UnityEngine.Random.Range(0f, 1.5f);
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			Life_Timer += global::UnityEngine.Time.deltaTime;
			if (Life_Timer > 5f)
			{
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Mon_7_Obejct, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
				gameObject.GetComponent<Mon_Index>().Index = mon_Index;
				gameObject.SendMessage("Set_DownDelay");
				gameObject.transform.parent = base.transform.parent;
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	public void Set_Index(int num)
	{
		mon_Index = num;
	}

	private void Flip()
	{
		facingRight = 1;
		if (Ctrl_1 != null)
		{
			Ctrl_1.GetComponent<Puppet2D_GlobalControl>().flip = true;
		}
	}
}
