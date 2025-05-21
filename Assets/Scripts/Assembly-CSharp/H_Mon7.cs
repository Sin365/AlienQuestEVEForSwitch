public class H_Mon7 : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject Mon_7_Down;

	public bool onFlip;

	public global::UnityEngine.Transform pos_Mon;

	public global::UnityEngine.GameObject Explo;

	public global::UnityEngine.Transform[] explo_Pos;

	private bool onPause;

	private int mon_Index;

	private float Life_Timer;

	private void Start()
	{
	}

	private void Update()
	{
		if (global::UnityEngine.GameObject.Find("GameManager") != null)
		{
			if (global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>().Paused && !onPause)
			{
				onPause = true;
				GetComponent<global::UnityEngine.Animator>().speed = 0f;
			}
			else if (!global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>().Paused && onPause)
			{
				onPause = false;
				GetComponent<global::UnityEngine.Animator>().speed = 1f;
			}
			Life_Timer += global::UnityEngine.Time.deltaTime;
			if (GetComponent<H_Ani>() != null && Life_Timer > 15f && GetComponent<H_Ani>().CumShot_Num != 0)
			{
			}
		}
	}

	public void Set_Index(int num)
	{
		mon_Index = num;
	}

	private void Get_Free()
	{
		for (int i = 0; i < explo_Pos.Length; i++)
		{
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Explo, explo_Pos[i].position, explo_Pos[i].transform.rotation) as global::UnityEngine.GameObject;
			gameObject.transform.localScale = explo_Pos[i].transform.localScale;
		}
		global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(Mon_7_Down, pos_Mon.position, base.transform.rotation) as global::UnityEngine.GameObject;
		gameObject2.GetComponent<AI_Mon_7_Down>().Set_Index(mon_Index);
		if (onFlip)
		{
			gameObject2.SendMessage("Flip");
		}
		gameObject2.transform.parent = base.transform.parent;
		global::UnityEngine.Object.Destroy(base.gameObject);
	}
}
