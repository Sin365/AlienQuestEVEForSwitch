using UnityEngine;

public class Col_Xeno_Rolling : global::UnityEngine.MonoBehaviour
{
	public bool Enabled;

	private int Mon_Num = 33;

	private int Damage = 50;

	private float DmgForce = 10f;

	private float Dmg_Delay;

	private float Player_Rolling_Delay;

	public Monster Mon;

	private Sound_Control SC;

    Player_Control PC => GameManager.instance.PC;
    GameObject Player => GameManager.instance.gobj_Player;

    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//PC = global::UnityEngine.GameObject.Find("Player").GetComponent<Player_Control>();
		SC = global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>();
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			if (Dmg_Delay > 0f)
			{
				Dmg_Delay -= global::UnityEngine.Time.deltaTime;
			}
			if (Player_Rolling_Delay > 0f)
			{
				Player_Rolling_Delay -= global::UnityEngine.Time.deltaTime;
			}
			if (Mon == null)
			{
				COL_OFF();
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	public void COL_ON()
	{
		GetComponent<global::UnityEngine.CircleCollider2D>().enabled = true;
		Enabled = true;
	}

	public void COL_OFF()
	{
		Enabled = false;
		GetComponent<global::UnityEngine.CircleCollider2D>().enabled = false;
	}

	private void OnTriggerStay2D(global::UnityEngine.Collider2D col)
	{
		if (!Enabled || !(Mon != null) || GM.Paused || GM.GameOver || GM.onEvent || GM.onHscene || GM.onDown || GM.onGatePass || GM.onGameClear || GM.onShield)
		{
			return;
		}
		if (col.name == "Ani" && !(global::UnityEngine.GameObject.Find("Border_Rolling").GetComponent<global::UnityEngine.SpriteRenderer>().color.a > 0.05f) && !GM.GameOver && Dmg_Delay <= 0f && (!(PC.transform.position.y < base.transform.position.y - 3.8f) || (PC.State != Player_Control.AniState.Sit && PC.State != Player_Control.AniState.Slide)))
		{
			int num = ((!(base.transform.position.x > col.transform.position.x)) ? 1 : (-1));
			Dmg_Delay = 0.6f;
			GM.Damage(Damage, DmgForce * (float)num, false, Mon_Num);
		}
		if (col.tag == "Col_PC_Atk" && col.name == "Col_Rolling" && global::UnityEngine.GameObject.Find("Border_Rolling").GetComponent<global::UnityEngine.SpriteRenderer>().color.a > 0.05f && Player_Rolling_Delay <= 0f)
		{
			if (global::UnityEngine.GameObject.Find("Border_Rolling").GetComponent<global::UnityEngine.SpriteRenderer>().color.a > 0.05f)
			{
				Player_Rolling_Delay = 0.1f;
				Dmg_Delay = 0.5f;
				Mon.Damage_Mon33_Rolling(1.5f);
			}
			else
			{
				Player_Rolling_Delay = 0.24f;
				Mon.Damage_Mon33_Rolling(1f);
			}
			SC.Mon_Hit_1(base.transform.position);
		}
	}
}
