using UnityEngine;

public class Col_Off_Sliding : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.BoxCollider2D Col_Box;

	private global::UnityEngine.Vector3 pos_Orig;

	private global::UnityEngine.Vector3 pos_Target;

	private bool onSlide;

	private float Col_Timer;
    Player_Control PC => GameManager.instance.PC;
    GameObject Player => GameManager.instance.gobj_Player;

    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//PC = global::UnityEngine.GameObject.Find("Player").GetComponent<Player_Control>();
		pos_Orig = Col_Box.transform.position;
		pos_Target = new global::UnityEngine.Vector3(pos_Orig.x, pos_Orig.y + 2f, 0f);
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			if (Col_Timer > 0f)
			{
				Col_Timer -= global::UnityEngine.Time.deltaTime;
			}
			else if (onSlide)
			{
				onSlide = false;
				Col_Box.transform.position = pos_Orig;
			}
		}
	}

	private void OnTriggerStay2D(global::UnityEngine.Collider2D col)
	{
		if (!GM.Paused && !GM.GameOver && (PC.State.ToString() == "Jump" || PC.State.ToString() == "Slide") && (col.name == "Col_Box_Top" || col.name == "Col_Box_Bot"))
		{
			Col_Timer = 0.5f;
			onSlide = true;
			Col_Box.transform.position = pos_Target;
		}
	}
}
