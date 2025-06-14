using UnityEngine;

public class LV_4_Cam_102 : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.Transform Bottom_Orig;

	public global::UnityEngine.Transform Bottom_Left;

	public global::UnityEngine.Transform Bottom_Right;

	private float Speed = 5f;

	private Camera_Control CamCon;
    Player_Control PC => GameManager.instance?.PC;
    GameObject Player => GameManager.instance?.gobj_Player;

    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//Player = global::UnityEngine.GameObject.Find("Player");
		CamCon = UnityEngine.Camera.main.GetComponent<Camera_Control>();
		if (Player.transform.position.x < 1077f)
		{
			CamCon.Cam_Bot = Bottom_Left.position.y;
		}
		else if (Player.transform.position.x > 1129f)
		{
			CamCon.Cam_Bot = Bottom_Right.position.y;
		}
		else
		{
			CamCon.Cam_Bot = Bottom_Orig.position.y;
		}
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			if (Player.transform.position.x < 1077f)
			{
				CamCon.Cam_Bot = global::UnityEngine.Mathf.Lerp(CamCon.Cam_Bot, Bottom_Left.position.y, global::UnityEngine.Time.deltaTime * Speed);
			}
			else if (Player.transform.position.x > 1129f)
			{
				CamCon.Cam_Bot = global::UnityEngine.Mathf.Lerp(CamCon.Cam_Bot, Bottom_Right.position.y, global::UnityEngine.Time.deltaTime * Speed);
			}
			else
			{
				CamCon.Cam_Bot = global::UnityEngine.Mathf.Lerp(CamCon.Cam_Bot, Bottom_Orig.position.y, global::UnityEngine.Time.deltaTime * Speed);
			}
		}
	}
}
