using UnityEngine;

public class Stage_Control_5 : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject guide_T;

	public global::UnityEngine.GameObject guide_L;

	public global::UnityEngine.GameObject guide_R;

	public global::UnityEngine.GameObject guide_B;

	public global::UnityEngine.GameObject Queen;

	private bool inNest;
    Player_Control PC => GameManager.instance?.PC;
    GameObject Player => GameManager.instance?.gobj_Player;
    private void Start()
	{
	}

	private void Update()
	{
		if (PC.transform.position.x > 191f && PC.transform.position.y < 26.9f)
		{
			if (!inNest)
			{
				inNest = true;
				UnityEngine.Camera.main.GetComponent<Camera_Control>().Cam_Top = guide_T.transform.position.y;
				UnityEngine.Camera.main.GetComponent<Camera_Control>().Cam_Left = guide_L.transform.position.x;
				UnityEngine.Camera.main.GetComponent<Camera_Control>().Cam_Right = guide_R.transform.position.x;
				UnityEngine.Camera.main.GetComponent<Camera_Control>().Cam_Bot = guide_B.transform.position.y;
				if (global::UnityEngine.GameObject.Find("Queen") != null)
				{
					global::UnityEngine.GameObject.Find("Queen").SendMessage("WakeUp");
					return;
				}
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Queen, new global::UnityEngine.Vector3(224f, 14f, 0f), base.transform.rotation) as global::UnityEngine.GameObject;
				gameObject.name = "Queen";
				gameObject.SendMessage("WakeUp");
			}
		}
		else if (inNest)
		{
			inNest = false;
			UnityEngine.Camera.main.GetComponent<Camera_Control>().Cam_Top = 40f;
			UnityEngine.Camera.main.GetComponent<Camera_Control>().Cam_Left = 0f;
			UnityEngine.Camera.main.GetComponent<Camera_Control>().Cam_Right = 220f;
			UnityEngine.Camera.main.GetComponent<Camera_Control>().Cam_Bot = -1.5f;
			if (global::UnityEngine.GameObject.Find("Queen") != null)
			{
				global::UnityEngine.GameObject.Find("Queen").SendMessage("Sleep");
			}
		}
	}
}
