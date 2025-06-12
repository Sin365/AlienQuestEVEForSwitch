using UnityEngine;

public class Trap_Laser_Group : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject Laser_Beam;

	public global::UnityEngine.SpriteRenderer[] Alert_Glow;

	public global::UnityEngine.Transform LengthObj;

	private int State = 1;

	private int Laser_Num;

	private float[] Life_Timer;

	private float LengthSize = 2f;

	public int facingRight = -1;

	private global::UnityEngine.Color color_On = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_Off = new global::UnityEngine.Color(1f, 1f, 1f, 0f);


    Player_Control PC => GameManager.instance.PC;
    GameObject Player => GameManager.instance.gobj_Player;

    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//Player = global::UnityEngine.GameObject.Find("Player");
		Laser_Num = Alert_Glow.Length;
		Life_Timer = new float[Laser_Num];
		LengthSize = LengthObj.localScale.y * 2f;
		Reset();
	}

	private void Reset()
	{
		for (int i = 0; i < Laser_Num; i++)
		{
			Alert_Glow[i].color = color_Off;
			Life_Timer[i] = 0f + (float)i * -0.01f;
		}
	}

	private void Set_Flip()
	{
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		if (global::UnityEngine.Mathf.Abs(Player.transform.position.x - base.transform.position.x) > 30f || global::UnityEngine.Mathf.Abs(Player.transform.position.y - base.transform.position.y) > 15f)
		{
			if (State > 0)
			{
				State = 0;
				Reset();
			}
			return;
		}
		State = 1;
		for (int i = 0; i < Laser_Num; i++)
		{
			Life_Timer[i] += global::UnityEngine.Time.deltaTime;
			if (Life_Timer[i] > 1f)
			{
				Life_Timer[i] = -1f;
				Make_Laser(i);
			}
			if (Life_Timer[i] > 0f)
			{
				Alert_Glow[i].color = global::UnityEngine.Color.Lerp(Alert_Glow[i].color, color_On, global::UnityEngine.Time.deltaTime * 5f);
			}
			else if (Life_Timer[i] < 0f)
			{
				Alert_Glow[i].color = global::UnityEngine.Color.Lerp(Alert_Glow[i].color, color_Off, global::UnityEngine.Time.deltaTime * 5f);
			}
		}
	}

	private void Make_Laser(int num)
	{
		if (facingRight < 0)
		{
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Laser_Beam, new global::UnityEngine.Vector3(base.transform.position.x - 9.5f + (float)num, base.transform.position.y + 0.58f, 0f), base.transform.rotation) as global::UnityEngine.GameObject;
			gameObject.transform.localScale = new global::UnityEngine.Vector3(2f, LengthSize, 1f);
		}
		else
		{
			global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(Laser_Beam, new global::UnityEngine.Vector3(base.transform.position.x + 9.5f - (float)num, base.transform.position.y + 0.58f, 0f), base.transform.rotation) as global::UnityEngine.GameObject;
			gameObject2.transform.localScale = new global::UnityEngine.Vector3(2f, LengthSize, 1f);
		}
		if (num == 0 || num == 10 || num == 19)
		{
			global::UnityEngine.GameObject.Find("Menu").SendMessage("Sound_Laser");
		}
		if (num == 19)
		{
			if (facingRight < 0 && base.transform.position.x < Player.transform.position.x)
			{
				facingRight = 1;
			}
			else if (facingRight > 0 && base.transform.position.x > Player.transform.position.x)
			{
				facingRight = -1;
			}
		}
	}
}
