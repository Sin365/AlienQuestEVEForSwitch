using UnityEngine;

public class Mother_Gravity : global::UnityEngine.MonoBehaviour
{
	private bool onActive = true;

	private float Life_Timer;

	private float Catch_Timer;

	private float Attack_Timer;

	private int Attack_Num;

	private bool onChase;

	private float Miss_Timer;

	private global::UnityEngine.Vector3 pos_Target;

	private float distance;

	private float Black_Timer;

	private float Blue_Timer;

	private float Size = 1f;

	private float Size_Glow = 1f;

	public global::UnityEngine.GameObject Glow;

	public global::UnityEngine.GameObject BlackBar;

	public global::UnityEngine.GameObject InnerBlue;

	public Sound sound_Gravity;

	public global::UnityEngine.Transform Player_Target;

    Player_Control PC => GameManager.instance.PC;
    GameObject Player => GameManager.instance.gobj_Player;

    private global::UnityEngine.BoxCollider2D PlayerColBox;

	GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		PlayerColBox = global::UnityEngine.GameObject.Find("Ani").GetComponent<global::UnityEngine.BoxCollider2D>();
		//Player = global::UnityEngine.GameObject.Find("Player");
		Size = base.transform.localScale.y;
		pos_Target = new global::UnityEngine.Vector3(PlayerColBox.transform.position.x, PlayerColBox.transform.position.y + ((!(PlayerColBox.size.y > 3f)) ? 1.8f : 3f), PlayerColBox.transform.position.z);
		Make_Black();
		global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().Set_Shake_Mother();
	}

	private void Make_Black()
	{
		int num = global::UnityEngine.Random.Range(0, 24);
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(BlackBar, base.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 15f * (float)num + (float)global::UnityEngine.Random.Range(-50, 50) * 0.1f)) as global::UnityEngine.GameObject;
		gameObject.transform.parent = base.transform;
		num = ((num == global::UnityEngine.Random.Range(0, 24)) ? num++ : num);
		global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(BlackBar, base.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 15f * (float)num + (float)global::UnityEngine.Random.Range(-50, 50) * 0.1f)) as global::UnityEngine.GameObject;
		gameObject2.transform.parent = base.transform;
		num = ((num == global::UnityEngine.Random.Range(0, 24)) ? num++ : num);
		global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(BlackBar, base.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 15f * (float)num + (float)global::UnityEngine.Random.Range(-50, 50) * 0.1f)) as global::UnityEngine.GameObject;
		gameObject3.transform.parent = base.transform;
		num = ((num == global::UnityEngine.Random.Range(0, 24)) ? num++ : num);
		global::UnityEngine.GameObject gameObject4 = global::UnityEngine.Object.Instantiate(BlackBar, base.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 15f * (float)num + (float)global::UnityEngine.Random.Range(-50, 50) * 0.1f)) as global::UnityEngine.GameObject;
		gameObject4.transform.parent = base.transform;
		num = ((num == global::UnityEngine.Random.Range(0, 24)) ? num++ : num);
		global::UnityEngine.GameObject gameObject5 = global::UnityEngine.Object.Instantiate(BlackBar, base.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 15f * (float)num + (float)global::UnityEngine.Random.Range(-50, 50) * 0.1f)) as global::UnityEngine.GameObject;
		gameObject5.transform.parent = base.transform;
	}

	private void Make_Blue()
	{
		int num = global::UnityEngine.Random.Range(0, 36);
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(InnerBlue, base.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 10f * (float)num + (float)global::UnityEngine.Random.Range(-50, 50) * 0.1f)) as global::UnityEngine.GameObject;
		gameObject.transform.parent = base.transform;
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (Catch_Timer > 0f)
		{
			Catch_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Attack_Timer > 0f)
		{
			Attack_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (!GM.GameOver && Catch_Timer <= 0f)
		{
			pos_Target = new global::UnityEngine.Vector3(PlayerColBox.transform.position.x, PlayerColBox.transform.position.y + ((!(PlayerColBox.size.y > 3f)) ? 1.8f : 3f), PlayerColBox.transform.position.z);
			distance = global::UnityEngine.Vector3.Distance(pos_Target, base.transform.position);
			if (distance < 50f)
			{
				onChase = true;
				base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, pos_Target, global::UnityEngine.Time.deltaTime * (50f - distance) * 0.04f);
			}
			else if (onChase)
			{
				onChase = false;
				Miss_Timer = 0.5f;
			}
			else if (Miss_Timer > 0f)
			{
				Miss_Timer -= global::UnityEngine.Time.deltaTime;
			}
		}
		if (Life_Timer > 4.45f)
		{
			Size = global::UnityEngine.Mathf.Lerp(Size, 0f, global::UnityEngine.Time.deltaTime * 20f);
			Glow.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 0.8f, 0.5f);
			if (sound_Gravity != null)
			{
				global::UnityEngine.Object.Destroy(sound_Gravity.gameObject);
			}
		}
		else
		{
			Size = 1f + (1f + global::UnityEngine.Mathf.Sin(Life_Timer * 40f)) * 0.1f;
		}
		base.transform.localScale = new global::UnityEngine.Vector3(Size, Size, 1f);
		if (onActive)
		{
			Black_Timer += global::UnityEngine.Time.deltaTime;
			if (Black_Timer > 0.01f && Life_Timer < 4.5f)
			{
				Make_Black();
				Make_Black();
				Black_Timer = 0f;
			}
			if (Life_Timer < 4.1f)
			{
				Blue_Timer += global::UnityEngine.Time.deltaTime;
				if (Blue_Timer > 0.05f && Life_Timer < 4.5f)
				{
					Make_Blue();
					Blue_Timer = 0f;
				}
			}
		}
		if (Life_Timer > 4.5f && onActive)
		{
			onActive = false;
			GetComponent<global::UnityEngine.CircleCollider2D>().enabled = false;
		}
		if (Life_Timer > 5.5f && Size < 0.02f)
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void OnTriggerStay2D(global::UnityEngine.Collider2D col)
	{
		if (!GM.GameOver && !GM.Paused && col.name == "Ani" && !GM.onEvent && !GM.onHscene)
		{
			Catch_Timer = 0.5f;
			Player.transform.position = global::UnityEngine.Vector3.Lerp(Player.transform.position, Player_Target.position, global::UnityEngine.Time.deltaTime * 3f);
			if (Attack_Timer <= 0f && Attack_Num < 10)
			{
				Attack_Timer = 0.1f;
				GM.Damage(12, 0f, false, 154);
			}
		}
	}
}
