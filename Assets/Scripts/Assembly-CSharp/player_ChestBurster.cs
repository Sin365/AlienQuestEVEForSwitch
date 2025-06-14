using UnityEngine;

public class player_ChestBurster : global::UnityEngine.MonoBehaviour
{
	private bool onBurst;

	private int facingRight = 1;

	private float Moan_Timer;

	private int Moan_Num;

	public global::UnityEngine.GameObject Ctrl_1;

	public global::UnityEngine.GameObject Blood_Explo;

	public global::UnityEngine.Transform pos_1;

	public global::UnityEngine.Transform pos_2;

	public global::UnityEngine.Transform pos_3;

	private H_SoundControl H_Sound;

    Player_Control PC => GameManager.instance?.PC;
    GameObject Player => GameManager.instance?.gobj_Player;

    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//Player = global::UnityEngine.GameObject.Find("Player");
		//PC = Player.GetComponent<Player_Control>();
		if (global::UnityEngine.GameObject.Find("Sound_List_H") != null)
		{
			H_Sound = global::UnityEngine.GameObject.Find("Sound_List_H").GetComponent<H_SoundControl>();
		}
		GetComponent<global::UnityEngine.Animator>().speed = 0f;
	}

	private void Update()
	{
		if (onBurst && Moan_Timer > 0f)
		{
			Moan_Timer -= global::UnityEngine.Time.deltaTime;
		}
	}

	private void Play()
	{
		onBurst = true;
		GetComponent<global::UnityEngine.Animator>().speed = 1f;
		GetComponent<global::UnityEngine.Animator>().SetTrigger("onPlay");
		base.transform.position = Player.transform.position;
		global::UnityEngine.GameObject.Find("Player_Down").transform.position = new global::UnityEngine.Vector3(-30f, 8f, 0f);
		if (facingRight != PC.facingRight)
		{
			facingRight = -facingRight;
			bool flip = ((facingRight < 0) ? true : false);
			Ctrl_1.GetComponent<Puppet2D_GlobalControl>().flip = flip;
		}
		UnityEngine.Camera.main.SendMessage("Hscene_Zoom");
	}

	private void Stop()
	{
		onBurst = false;
		GetComponent<global::UnityEngine.Animator>().speed = 0f;
		GetComponent<global::UnityEngine.Animator>().SetTrigger("onStop");
		base.transform.position = new global::UnityEngine.Vector3(-30f, 8f, 0f);
	}

	private void Burst()
	{
		GameManager.instance.sc_Sound_List.Mon_Explo(base.transform.position);
		GameManager.instance.sc_Sound_List.Mon_1_Death(base.transform.position);
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Blood_Explo, pos_1.position, pos_1.rotation) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(Blood_Explo, pos_2.position, pos_2.rotation) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(Blood_Explo, pos_3.position, pos_3.rotation) as global::UnityEngine.GameObject;
		gameObject3.transform.localScale = pos_3.localScale;
	}

	private void Sound_Moan()
	{
		if (H_Sound != null && Moan_Timer <= 0f)
		{
			int num = global::UnityEngine.Random.Range(1, 11);
			if (Moan_Num != num && num != 3)
			{
				Moan_Num = num;
				H_Sound.Sound_Moan(num, 1);
				Moan_Timer = 1.5f;
			}
		}
	}
}
