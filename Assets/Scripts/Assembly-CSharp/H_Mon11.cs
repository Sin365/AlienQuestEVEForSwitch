using UnityEngine;

public class H_Mon11 : global::UnityEngine.MonoBehaviour
{
	public int Index;

	public bool onAutoFlip;

	public bool onFlip;

	public global::UnityEngine.SkinnedMeshRenderer Penis;

	public global::UnityEngine.SkinnedMeshRenderer Penis_Wet;

	public global::UnityEngine.SkinnedMeshRenderer Penis_Censored;

	public global::UnityEngine.SkinnedMeshRenderer Hair;

	public global::UnityEngine.SkinnedMeshRenderer Hair_Cum;

	public global::UnityEngine.SkinnedMeshRenderer Face;

	public global::UnityEngine.SkinnedMeshRenderer Face_Cum;

	public global::UnityEngine.SkinnedMeshRenderer Tongue;

	public global::UnityEngine.SkinnedMeshRenderer Tongue_Cum;

	public global::UnityEngine.GameObject Mon_11;

	public global::UnityEngine.GameObject Mon_7;

	public global::UnityEngine.GameObject Mon_7_Down;

	public global::UnityEngine.Transform pos_11;

	public global::UnityEngine.Transform pos_7_Stand;

	public global::UnityEngine.Transform pos_7_Down;

	private bool onPause;

	private float dist_X;

	private float dist_Y;

	private float dist_Timer;

	private bool Enabled;

	private float Enabled_Timer;

	private bool onCumShot;

	private bool onEnd;

	private float End_Timer;

	private float Speed = 1f;

	private bool isEndMode;
    GameManager GM => GameManager.instance;
    Player_Control PC => GameManager.instance?.PC;
    GameObject Player => GameManager.instance?.gobj_Player;
    private void Start()
	{
		if (!(GM != null))
		{
			return;
		}
		Enabled = false;
		GetComponent<global::UnityEngine.Animator>().Play("H_13N", 0, global::UnityEngine.Random.Range(0f, 2f));
		GetComponent<global::UnityEngine.Animator>().speed = 0f;
		if (onAutoFlip)
		{
			if (Player != null && Player.transform.position.x > base.transform.position.x)
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
		if (!(Player != null) || !(GM != null))
		{
			return;
		}
		if (Enabled)
		{
			if (GM.GetComponent<GameManager>().Paused && !onPause)
			{
				onPause = true;
				GetComponent<global::UnityEngine.Animator>().speed = 0f;
			}
			else if (!GM.GetComponent<GameManager>().Paused && onPause)
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
		if (Enabled)
		{
			dist_Timer += global::UnityEngine.Time.deltaTime;
			if (dist_Timer > 3f && !isEndMode)
			{
				isEndMode = true;
				GetComponent<global::UnityEngine.Animator>().SetBool("isEndMode", true);
			}
		}
		if (onCumShot)
		{
			End_Timer += global::UnityEngine.Time.deltaTime;
			if (End_Timer > 9.7f)
			{
				End();
			}
			else if (End_Timer > 6f)
			{
				Speed = global::UnityEngine.Mathf.Lerp(Speed, 0.1f, global::UnityEngine.Time.deltaTime * 0.8f);
				GetComponent<global::UnityEngine.Animator>().speed = Speed;
			}
		}
	}

	private void End()
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Mon_11, pos_11.position, base.transform.rotation) as global::UnityEngine.GameObject;
		gameObject.GetComponent<Mon_Index>().Index = Index;
		gameObject.transform.parent = base.transform.parent;
		gameObject.SendMessage("Set_Penis_Wet");
		global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(Mon_7, pos_7_Stand.position, base.transform.rotation) as global::UnityEngine.GameObject;
		gameObject2.GetComponent<Mon_Index>().Index = Index;
		gameObject2.transform.parent = base.transform.parent;
		gameObject2.SendMessage("Set_AttackDelay");
		global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(Mon_7_Down, pos_7_Down.position, base.transform.rotation) as global::UnityEngine.GameObject;
		gameObject3.GetComponent<AI_Mon_7_Down>().Set_Index(Index + 1);
		if (!onFlip)
		{
			gameObject3.SendMessage("Flip");
		}
		gameObject3.transform.parent = base.transform.parent;
		global::UnityEngine.Object.Destroy(base.gameObject);
	}

	private void Set_CumShot()
	{
		if (!onCumShot)
		{
			onCumShot = true;
			if (AxiPlayerPrefs.GetInt("UncensoredPatch") == 1)
			{
				Penis.enabled = false;
				Penis_Wet.enabled = true;
			}
			Hair.enabled = false;
			Hair_Cum.enabled = true;
			Face.enabled = false;
			Face_Cum.enabled = true;
			Tongue.enabled = false;
			Tongue_Cum.enabled = true;
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
				End();
			}
		}
	}
}
