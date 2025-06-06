public class Stage_Control_5 : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject guide_T;

	public global::UnityEngine.GameObject guide_L;

	public global::UnityEngine.GameObject guide_R;

	public global::UnityEngine.GameObject guide_B;

	public global::UnityEngine.GameObject Queen;

	private bool inNest;

	private void Start()
	{
	}

	private void Update()
	{
		if (global::UnityEngine.GameObject.Find("Player").transform.position.x > 191f && global::UnityEngine.GameObject.Find("Player").transform.position.y < 26.9f)
		{
			if (!inNest)
			{
				inNest = true;
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().Cam_Top = guide_T.transform.position.y;
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().Cam_Left = guide_L.transform.position.x;
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().Cam_Right = guide_R.transform.position.x;
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().Cam_Bot = guide_B.transform.position.y;
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
			global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().Cam_Top = 40f;
			global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().Cam_Left = 0f;
			global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().Cam_Right = 220f;
			global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().Cam_Bot = -1.5f;
			if (global::UnityEngine.GameObject.Find("Queen") != null)
			{
				global::UnityEngine.GameObject.Find("Queen").SendMessage("Sleep");
			}
		}
	}
}
