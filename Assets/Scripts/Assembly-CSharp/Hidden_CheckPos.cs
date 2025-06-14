using UnityEngine;

public class Hidden_CheckPos : global::UnityEngine.MonoBehaviour
{
	private bool onEnabled;

	public Hidden_Passage_2 hidden_passage;
    Player_Control PC => GameManager.instance?.PC;
    GameObject Player => GameManager.instance?.gobj_Player;

    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//PC = global::UnityEngine.GameObject.Find("Player").GetComponent<Player_Control>();
	}

	private void Update()
	{
		if (GM.Paused)
		{
		}
	}

	private void OnTriggerStay2D(global::UnityEngine.Collider2D col)
	{
		if (!GM.Paused && !onEnabled && col.name == "Ani" && hidden_passage != null)
		{
			onEnabled = true;
			hidden_passage.Set_Enabled();
		}
	}
}
