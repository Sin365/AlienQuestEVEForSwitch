public class H_Mon31 : global::UnityEngine.MonoBehaviour
{
	public int Index;

	public bool onAutoFlip;

	public bool onFlip;

	public global::UnityEngine.SkinnedMeshRenderer Penis;

	public global::UnityEngine.SkinnedMeshRenderer Penis_Censored;

	public global::UnityEngine.GameObject Mon_18;

	public global::UnityEngine.GameObject Mon_31;

	public global::UnityEngine.Transform pos_18;

	public global::UnityEngine.Transform pos_31;

	private bool onPause;

	private bool Enabled;

	private float Enabled_Timer;

	private float Speed = 1f;

	private bool onEnd;

	private float End_Timer = 3.5f;

	private bool onCumShot;
    GameManager GM => GameManager.instance;
    private void Start()
	{
		if (!(GM != null))
		{
			return;
		}
		Enabled = false;
		GetComponent<global::UnityEngine.Animator>().Play("Start", 0, global::UnityEngine.Random.Range(0f, 2f));
		GetComponent<global::UnityEngine.Animator>().speed = 0f;
		End_Timer = 3.5f + global::UnityEngine.Random.Range(0f, 1f);
		if (onAutoFlip)
		{
			if (global::UnityEngine.GameObject.Find("Player") != null && global::UnityEngine.GameObject.Find("Player").transform.position.x > base.transform.position.x)
			{
				onFlip = true;
				GetComponent<H_Ani>().SendMessage("Flip");
			}
		}
		else if (onFlip)
		{
			GetComponent<H_Ani>().SendMessage("Flip");
		}
	}

	private void Update()
	{
		if (!(global::UnityEngine.GameObject.Find("Player") != null) || !(GM != null))
		{
			return;
		}
		if (Enabled)
		{
			if (GM.Paused && !onPause)
			{
				onPause = true;
				GetComponent<global::UnityEngine.Animator>().speed = 0f;
			}
			else if (!GM.Paused && onPause)
			{
				onPause = false;
				GetComponent<global::UnityEngine.Animator>().speed = Speed;
			}
		}
		if (onPause)
		{
			return;
		}
		if (Enabled_Timer > 0f)
		{
			Enabled_Timer -= global::UnityEngine.Time.deltaTime;
		}
		else if (Enabled)
		{
			Enabled = false;
			GetComponent<global::UnityEngine.Animator>().speed = 0f;
		}
		if (Enabled && onEnd)
		{
			End_Timer -= global::UnityEngine.Time.deltaTime;
			if (End_Timer <= 0f)
			{
				Make_Monster();
			}
		}
	}

	private void Make_Monster()
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Mon_18, pos_18.position, base.transform.rotation) as global::UnityEngine.GameObject;
		gameObject.GetComponent<Mon_Index>().Index = Index;
		gameObject.transform.parent = base.transform.parent;
		gameObject.SendMessage("Set_Penis_Wet");
		global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(Mon_31, pos_31.position, base.transform.rotation) as global::UnityEngine.GameObject;
		gameObject2.GetComponent<Mon_Index>().Index = Index;
		gameObject2.transform.parent = base.transform.parent;
		global::UnityEngine.Object.Destroy(base.gameObject);
	}

	private void H_23Mon_End()
	{
		if (!onEnd)
		{
			onEnd = true;
			End_Timer = 3.5f + global::UnityEngine.Random.Range(0f, 1f);
		}
	}

	private void OnTriggerStay2D(global::UnityEngine.Collider2D col)
	{
		if (GM != null)
		{
			if (col.name == "COL_Cam" && !onPause && !Enabled)
			{
				Enabled = true;
				Enabled_Timer = 1f;
				GetComponent<global::UnityEngine.Animator>().speed = Speed;
			}
			if (!onEnd && (col.tag == "Col_PC_Atk" || col.tag == "Magic_Fire" || col.tag == "Magic_Explo" || col.tag == "Magic_Smog"))
			{
				onEnd = true;
				Make_Monster();
			}
		}
	}
}
