using UnityEngine;

public class LV_1_Breakable_Cam : global::UnityEngine.MonoBehaviour
{
	public Breakable_Block Block;

	public global::UnityEngine.Transform cam_Top;

	public global::UnityEngine.Transform cam_Bot;

	public global::UnityEngine.Transform cam_Left;

	public global::UnityEngine.Transform cam_Right;

	public global::UnityEngine.SpriteRenderer SR_Glow_1;

	public global::UnityEngine.SpriteRenderer SR_Glow_2;

	private global::UnityEngine.Color color_Glow_1 = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_Glow_2 = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private bool onBreak;

	private int Glow_Dir;
    Player_Control PC => GameManager.instance?.PC;
    GameObject Player => GameManager.instance?.gobj_Player;

    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//Player = global::UnityEngine.GameObject.Find("Player");
		if (SR_Glow_1 != null)
		{
			color_Glow_1 = SR_Glow_1.color;
		}
		if (SR_Glow_2 != null)
		{
			color_Glow_2 = SR_Glow_2.color;
		}
		if (cam_Bot != null && Player.transform.position.y < base.transform.position.y)
		{
			onBreak = true;
			UnityEngine.Camera.main.GetComponent<Camera_Control>().Cam_Bot = cam_Bot.position.y;
		}
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		if (!onBreak && Block.isBroken)
		{
			onBreak = true;
			if (cam_Top != null)
			{
				UnityEngine.Camera.main.GetComponent<Camera_Control>().Cam_Top = cam_Top.position.y;
			}
			if (cam_Bot != null)
			{
				UnityEngine.Camera.main.GetComponent<Camera_Control>().Cam_Bot = cam_Bot.position.y;
			}
			if (cam_Left != null)
			{
				UnityEngine.Camera.main.GetComponent<Camera_Control>().Cam_Left = cam_Left.position.x;
			}
			if (cam_Right != null)
			{
				UnityEngine.Camera.main.GetComponent<Camera_Control>().Cam_Right = cam_Right.position.x;
			}
		}
		if (!(SR_Glow_1 != null))
		{
			return;
		}
		if (SR_Glow_1.transform.position.y < base.transform.position.y - 1f)
		{
			if (Player.transform.position.y < base.transform.position.y)
			{
				Fog_Off();
			}
			else
			{
				Fog_On();
			}
		}
		else if (SR_Glow_1.transform.position.y > base.transform.position.y + 1f)
		{
			if (Player.transform.position.y > base.transform.position.y)
			{
				Fog_Off();
			}
			else
			{
				Fog_On();
			}
		}
		else if (SR_Glow_1.transform.position.x < base.transform.position.x - 1f)
		{
			if (Player.transform.position.x < base.transform.position.x)
			{
				Fog_Off();
			}
			else
			{
				Fog_On();
			}
		}
		else if (SR_Glow_1.transform.position.x > base.transform.position.x + 1f)
		{
			if (Player.transform.position.x > base.transform.position.x)
			{
				Fog_Off();
			}
			else
			{
				Fog_On();
			}
		}
	}

	private void Fog_On()
	{
		SR_Glow_1.color = global::UnityEngine.Color.Lerp(SR_Glow_1.color, color_Glow_1, global::UnityEngine.Time.deltaTime * 0.8f);
		if (SR_Glow_2 != null)
		{
			SR_Glow_2.color = global::UnityEngine.Color.Lerp(SR_Glow_2.color, color_Glow_2, global::UnityEngine.Time.deltaTime * 0.8f);
		}
	}

	private void Fog_Off()
	{
		SR_Glow_1.color = global::UnityEngine.Color.Lerp(SR_Glow_1.color, new global::UnityEngine.Color(SR_Glow_1.color.r, SR_Glow_1.color.g, SR_Glow_1.color.b, 0.01f), global::UnityEngine.Time.deltaTime * 2f);
		if (SR_Glow_2 != null)
		{
			SR_Glow_2.color = global::UnityEngine.Color.Lerp(SR_Glow_2.color, new global::UnityEngine.Color(SR_Glow_2.color.r, SR_Glow_2.color.g, SR_Glow_2.color.b, 0.01f), global::UnityEngine.Time.deltaTime * 2f);
		}
	}
}
