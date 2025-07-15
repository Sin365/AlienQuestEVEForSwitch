public class GameOver_Control : global::UnityEngine.MonoBehaviour
{
	public bool onMouseDrag;

	public global::UnityEngine.GameObject[] H_GameOver;

	public global::UnityEngine.RectTransform pos_Text_Shadow_1;

	public global::UnityEngine.RectTransform pos_Text_Shadow_2;

	private int H_GameOver_Num;

	private global::UnityEngine.Vector3 posTarget_1;

	private global::UnityEngine.Vector3 posTarget_2;

	private float posTimer;

	private int Input_Mode;

	private bool isFadeIn = true;

	private bool isFadeOut;

	private float FadeOpacity = 1f;

	private string FadeOutAction = string.Empty;

	private global::UnityEngine.GameObject BlackFade;

	private void Start()
	{
		BlackFade = global::UnityEngine.GameObject.Find("BlackFade");
		BlackFade.GetComponent<global::UnityEngine.SpriteRenderer>().enabled = true;
		global::UnityEngine.Physics2D.IgnoreLayerCollision(25, 25);
		if (AxiPlayerPrefs.GetInt("H_GameOver_Now") > 0)
		{
			H_GameOver_Num = AxiPlayerPrefs.GetInt("H_GameOver_Now");
		}
		if (H_GameOver_Num < 1)
		{
			H_GameOver_Num = 1;
		}
		else if (H_GameOver_Num > 5)
		{
			H_GameOver_Num = 5;
		}
		AxiPlayerPrefs.SetInt("H_GameOver_" + H_GameOver_Num, 1);
		if (global::UnityEngine.GameObject.Find("H_GameOver_1") == null && global::UnityEngine.GameObject.Find("H_GameOver_2") == null && global::UnityEngine.GameObject.Find("H_GameOver_3") == null && global::UnityEngine.GameObject.Find("H_GameOver_4") == null && global::UnityEngine.GameObject.Find("H_GameOver_5") == null)
		{
			Load_GameOver();
		}
		posTarget_1 = new global::UnityEngine.Vector3(global::UnityEngine.Random.Range(-4f, 4f), global::UnityEngine.Random.Range(-4f, 4f), 0f);
		posTarget_2 = new global::UnityEngine.Vector3(global::UnityEngine.Random.Range(-4f, 4f), global::UnityEngine.Random.Range(-4f, 4f), 0f);
		if (AxiPlayerPrefs.GetInt("Input_Mode") == 1)
		{
			global::UnityEngine.GameObject.Find("Exit_Text").GetComponent<global::UnityEngine.UI.Text>().text = "Press  START Button  to Exit";
		}
	}

	private void Load_GameOver()
	{
		global::UnityEngine.GameObject gameObject = AxiObject.Instantiate(H_GameOver[H_GameOver_Num - 1], base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
	}

	private void Update()
	{
		if (AxiInputBridge.GetAxis("L_X") != 0f || AxiInputBridge.GetAxis("L_Y") != 0f || AxiInputBridge.GetAxis("R_X") != 0f || AxiInputBridge.GetAxis("R_Y") != 0f || AxiInputBridge.GetButtonDown("Jump") || AxiInputBridge.GetButtonDown("_B") || AxiInputBridge.GetButtonDown("_X") || AxiInputBridge.GetButtonDown("_Y") || AxiInputBridge.GetButtonDown("L_B") || AxiInputBridge.GetButtonDown("R_B") || AxiInputBridge.GetAxis("L_Trigger") != 0f || AxiInputBridge.GetButtonDown("Start") || AxiInputBridge.GetButtonDown("Back") || AxiInputBridge.GetAxis("DPad_X") != 0f || AxiInputBridge.GetAxis("DPad_Y") != 0f)
		{
			if (Input_Mode != 1)
			{
				Input_Mode = 1;
				global::UnityEngine.GameObject.Find("Exit_Text").GetComponent<global::UnityEngine.UI.Text>().text = "Press  START Button  to Exit";
			}
		}
		else if (AxiInputBridge.anyKeyDown && Input_Mode != 0)
		{
			Input_Mode = 0;
			global::UnityEngine.GameObject.Find("Exit_Text").GetComponent<global::UnityEngine.UI.Text>().text = "Press ESC key to Exit";
		}
		if (AxiInputBridge.mousePosition.x > (float)global::UnityEngine.Screen.width || AxiInputBridge.mousePosition.x < 0f || AxiInputBridge.mousePosition.y > (float)global::UnityEngine.Screen.height || AxiInputBridge.mousePosition.y < 0f)
		{
			if (onMouseDrag)
			{
				onMouseDrag = false;
				UnityEngine.Camera.main.SendMessage("Release_TargetPos");
			}
		}
		else
		{
			Check_Mouse();
		}
		if (posTimer > 0f)
		{
			posTimer -= global::UnityEngine.Time.deltaTime;
		}
		else
		{
			posTimer = 0.1f;
			posTarget_1 = new global::UnityEngine.Vector3(global::UnityEngine.Random.Range(-4f, 4f), global::UnityEngine.Random.Range(-4f, 4f), 0f);
			posTarget_2 = new global::UnityEngine.Vector3(global::UnityEngine.Random.Range(-4f, 4f), global::UnityEngine.Random.Range(-4f, 4f), 0f);
			pos_Text_Shadow_1.localRotation = global::UnityEngine.Quaternion.Euler(0f, 0f, global::UnityEngine.Random.Range(-1f, 1f));
			pos_Text_Shadow_2.localRotation = global::UnityEngine.Quaternion.Euler(0f, 0f, global::UnityEngine.Random.Range(-1f, 1f));
		}
		pos_Text_Shadow_1.localPosition = global::UnityEngine.Vector3.Lerp(pos_Text_Shadow_1.localPosition, posTarget_1, global::UnityEngine.Time.deltaTime * 12f);
		pos_Text_Shadow_2.localPosition = global::UnityEngine.Vector3.Lerp(pos_Text_Shadow_2.localPosition, posTarget_2, global::UnityEngine.Time.deltaTime * 12f);
		if (AxiInputBridge.GetKeyDown(global::UnityEngine.KeyCode.Escape) || AxiInputBridge.GetButtonDown("Start"))
		{
			Exit();
		}
		if (isFadeIn)
		{
			FadeOpacity -= global::UnityEngine.Time.deltaTime * 0.5f;
			if (FadeOpacity <= 0f)
			{
				isFadeIn = false;
				FadeOpacity = 0f;
				BlackFade.GetComponent<global::UnityEngine.SpriteRenderer>().enabled = false;
			}
			BlackFade.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(0f, 0f, 0f, FadeOpacity);
		}
		else
		{
			if (!isFadeOut)
			{
				return;
			}
			FadeOpacity += global::UnityEngine.Time.deltaTime * 1f;
			if (FadeOpacity >= 1f)
			{
				isFadeOut = false;
				FadeOpacity = 1f;
				if (FadeOutAction == "Title")
				{
					Execute_Exit();
				}
				else if (FadeOutAction == "Restart")
				{
					Execute_Restart();
				}
			}
			BlackFade.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(0f, 0f, 0f, FadeOpacity);
		}
	}

	private void Set_FadeIn()
	{
		isFadeIn = true;
		isFadeOut = false;
		FadeOpacity = 1f;
		if (!BlackFade.GetComponent<global::UnityEngine.SpriteRenderer>().enabled)
		{
			BlackFade.GetComponent<global::UnityEngine.SpriteRenderer>().enabled = true;
			BlackFade.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, 1f);
		}
		FadeOutAction = string.Empty;
	}

	public void Set_FadeOut(string fadeoutaction)
	{
		isFadeOut = true;
		isFadeIn = false;
		if (!BlackFade.GetComponent<global::UnityEngine.SpriteRenderer>().enabled)
		{
			FadeOpacity = 0f;
			BlackFade.GetComponent<global::UnityEngine.SpriteRenderer>().enabled = true;
			BlackFade.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
		}
		FadeOutAction = fadeoutaction;
	}

	private void Exit()
	{
		global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_DeviceOn");
		global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_Btn");
		Set_FadeOut("Title");
	}

	private void Restart()
	{
		global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_DeviceOn");
		global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_Btn");
		Set_FadeOut("Restart");
	}

	private void Execute_Exit()
	{
		global::UnityEngine.Application.LoadLevel("Title");
	}

	private void Execute_Restart()
	{
		global::UnityEngine.Application.LoadLevel("Title");
	}

	private void Check_Mouse()
	{
		bool flag = false;
		if (AxiInputBridge.GetMouseButtonDown(0))
		{
			global::UnityEngine.Ray ray = global::UnityEngine.GameObject.Find("UI Camera").GetComponent<UnityEngine.Camera>().ScreenPointToRay(AxiInputBridge.mousePosition);
			global::UnityEngine.RaycastHit2D rayIntersection = global::UnityEngine.Physics2D.GetRayIntersection(ray, float.PositiveInfinity);
			if (rayIntersection.collider != null)
			{
				if (rayIntersection.collider.tag != "UI")
				{
					onMouseDrag = true;
				}
			}
			else
			{
				onMouseDrag = true;
			}
		}
		else if (AxiInputBridge.GetMouseButtonUp(0))
		{
			onMouseDrag = false;
		}
	}
}
