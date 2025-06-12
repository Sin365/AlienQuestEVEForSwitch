using UnityEngine;

public class Tile_Lift_Bottom : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.Transform pos_Start;

	public global::UnityEngine.Transform pos_End;

	private bool onDown;

	private bool onGroundNear;

	private float pre_Y;
    Player_Control PC => GameManager.instance.PC;
    GameObject Player => GameManager.instance.gobj_Player;
    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//PC = global::UnityEngine.GameObject.Find("Player").GetComponent<Player_Control>();
	}

	private void Update()
	{
		if (!GM.Paused && !GM.GameOver)
		{
			onGroundNear = global::UnityEngine.Physics2D.Linecast(pos_Start.position, pos_End.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
			if (base.transform.position.y - pre_Y < 0f)
			{
				onDown = true;
			}
			else
			{
				onDown = false;
			}
			pre_Y = base.transform.position.y;
		}
	}

	private void OnTriggerStay2D(global::UnityEngine.Collider2D col)
	{
		if (GM.Paused || GM.GameOver || !onDown || !onGroundNear)
		{
			return;
		}
		if (col.name == "Ani")
		{
			if (PC.transform.position.y < base.transform.position.y)
			{
				if (PC.transform.position.x > base.transform.position.x)
				{
					PC.GetComponent<UnityEngine.Rigidbody2D>().AddForce(new global::UnityEngine.Vector2(8000f * global::UnityEngine.Time.deltaTime, 0f));
				}
				else
				{
					PC.GetComponent<UnityEngine.Rigidbody2D>().AddForce(new global::UnityEngine.Vector2(-8000f * global::UnityEngine.Time.deltaTime, 0f));
				}
			}
		}
		else if (col.tag == "Mon" && col.GetComponent<global::UnityEngine.Rigidbody2D>() != null && col.transform.position.y < base.transform.position.y)
		{
			if (col.transform.position.x > base.transform.position.x)
			{
				col.GetComponent<UnityEngine.Rigidbody2D>().AddForce(new global::UnityEngine.Vector2(8000f * global::UnityEngine.Time.deltaTime, 0f));
			}
			else
			{
				col.GetComponent<UnityEngine.Rigidbody2D>().AddForce(new global::UnityEngine.Vector2(-8000f * global::UnityEngine.Time.deltaTime, 0f));
			}
		}
	}
}
