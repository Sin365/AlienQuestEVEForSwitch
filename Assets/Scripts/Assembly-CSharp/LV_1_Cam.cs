using UnityEngine;

public class LV_1_Cam : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.Transform Orig_Top;

	public global::UnityEngine.Transform Orig_Bot;

	public global::UnityEngine.Transform Orig_Left;

	public global::UnityEngine.Transform Orig_Right;

	public global::UnityEngine.Transform Target_Top;

	public global::UnityEngine.Transform Target_Bot;

	public global::UnityEngine.Transform Target_Left;

	public global::UnityEngine.Transform Target_Right;

	public bool condition_Up;

	public bool condition_Down;

	public bool condition_Left;

	public bool condition_Right;

	public float Orig_Speed = 5f;

	public float Target_Speed = 5f;

	private bool check_Target;

	private bool check_Orig;

	private bool check_Left;

	private bool check_Right;

	private Camera_Control CamCon;
    Player_Control PC => GameManager.instance?.PC;
    GameObject Player => GameManager.instance?.gobj_Player;

    GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//Player = global::UnityEngine.GameObject.Find("Player");
		CamCon = UnityEngine.Camera.main.GetComponent<Camera_Control>();
		if (condition_Up)
		{
			if (Player.transform.position.y > base.transform.position.y)
			{
				Set_Cam_Target();
			}
			else if (Player.transform.position.y < base.transform.position.y)
			{
				Set_Cam_Original();
			}
		}
		else if (condition_Down)
		{
			if (Player.transform.position.y < base.transform.position.y)
			{
				Set_Cam_Target();
			}
			else if (Player.transform.position.y > base.transform.position.y)
			{
				Set_Cam_Original();
			}
		}
		else if (condition_Left)
		{
			if (Player.transform.position.x < base.transform.position.x)
			{
				Set_Cam_Target();
			}
			else if (Player.transform.position.x > base.transform.position.x)
			{
				Set_Cam_Original();
			}
		}
		else if (condition_Right)
		{
			if (Player.transform.position.x > base.transform.position.x)
			{
				Set_Cam_Target();
			}
			else if (Player.transform.position.x < base.transform.position.x)
			{
				Set_Cam_Original();
			}
		}
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		check_Target = false;
		check_Orig = false;
		if (condition_Up)
		{
			if (Player.transform.position.y > base.transform.position.y)
			{
				Set_Cam_Target_Slow();
			}
			else if (Player.transform.position.y < base.transform.position.y)
			{
				Set_Cam_Original_Slow();
			}
		}
		else if (condition_Down)
		{
			if (Player.transform.position.y < base.transform.position.y)
			{
				Set_Cam_Target_Slow();
			}
			else if (Player.transform.position.y > base.transform.position.y)
			{
				Set_Cam_Original_Slow();
			}
		}
		else if (condition_Left)
		{
			if (!check_Left && Player.transform.position.x < base.transform.position.x)
			{
				check_Left = true;
				Set_Cam_Target();
			}
			else if (check_Left && Player.transform.position.x > base.transform.position.x)
			{
				check_Left = false;
				Set_Cam_Original();
			}
		}
		else if (condition_Right)
		{
			if (!check_Right && Player.transform.position.x > base.transform.position.x)
			{
				check_Right = true;
				Set_Cam_Target();
			}
			else if (check_Right && Player.transform.position.x < base.transform.position.x)
			{
				check_Right = false;
				Set_Cam_Original();
			}
		}
	}

	private void Set_Cam_Original()
	{
		if (Orig_Top != null)
		{
			CamCon.Cam_Top = Orig_Top.position.y;
		}
		if (Orig_Bot != null)
		{
			CamCon.Cam_Bot = Orig_Bot.position.y;
		}
		if (Orig_Left != null)
		{
			CamCon.Cam_Left = Orig_Left.position.x;
		}
		if (Orig_Right != null)
		{
			CamCon.Cam_Right = Orig_Right.position.x;
		}
	}

	private void Set_Cam_Target()
	{
		if (Target_Top != null)
		{
			CamCon.Cam_Top = Target_Top.position.y;
		}
		if (Target_Bot != null)
		{
			CamCon.Cam_Bot = Target_Bot.position.y;
		}
		if (Target_Left != null)
		{
			CamCon.Cam_Left = Target_Left.position.x;
		}
		if (Target_Right != null)
		{
			CamCon.Cam_Right = Target_Right.position.x;
		}
	}

	private void Set_Cam_Original_Slow()
	{
		if (Orig_Top != null)
		{
			CamCon.Cam_Top = global::UnityEngine.Mathf.Lerp(CamCon.Cam_Top, Orig_Top.position.y, global::UnityEngine.Time.deltaTime * Orig_Speed);
		}
		if (Orig_Bot != null)
		{
			CamCon.Cam_Bot = global::UnityEngine.Mathf.Lerp(CamCon.Cam_Bot, Orig_Bot.position.y, global::UnityEngine.Time.deltaTime * Orig_Speed);
		}
		if (Orig_Left != null)
		{
			CamCon.Cam_Left = global::UnityEngine.Mathf.Lerp(CamCon.Cam_Left, Orig_Left.position.x, global::UnityEngine.Time.deltaTime * Orig_Speed);
		}
		if (Orig_Right != null)
		{
			CamCon.Cam_Right = global::UnityEngine.Mathf.Lerp(CamCon.Cam_Right, Orig_Right.position.x, global::UnityEngine.Time.deltaTime * Orig_Speed);
		}
	}

	private void Set_Cam_Target_Slow()
	{
		if (Target_Top != null)
		{
			CamCon.Cam_Top = global::UnityEngine.Mathf.Lerp(CamCon.Cam_Top, Target_Top.position.y, global::UnityEngine.Time.deltaTime * Target_Speed);
		}
		if (Target_Bot != null)
		{
			CamCon.Cam_Bot = global::UnityEngine.Mathf.Lerp(CamCon.Cam_Bot, Target_Bot.position.y, global::UnityEngine.Time.deltaTime * Target_Speed);
		}
		if (Target_Left != null)
		{
			CamCon.Cam_Left = global::UnityEngine.Mathf.Lerp(CamCon.Cam_Left, Target_Left.position.x, global::UnityEngine.Time.deltaTime * Target_Speed);
		}
		if (Target_Right != null)
		{
			CamCon.Cam_Right = global::UnityEngine.Mathf.Lerp(CamCon.Cam_Right, Target_Right.position.x, global::UnityEngine.Time.deltaTime * Target_Speed);
		}
	}
}
