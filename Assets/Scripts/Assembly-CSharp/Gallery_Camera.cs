public class Gallery_Camera : global::UnityEngine.MonoBehaviour
{
	public float Cam_Top = 20f;

	public float Cam_Bot = -20f;

	public float Cam_Right = 20f;

	public float Cam_Left = -20f;

	public float targetSize = 5f;

	public float MaxSize = 11.2f;

	private global::UnityEngine.Vector3 targetPos;

	private bool onMouseZoom;

	private float MouseZoom_Timer;

	private bool onBlur;

	private float blurTimer;

	private float Blur_Delay;

	private float updateInterval = 0.5f;

	private float lastInterval;

	private int frames;

	private float fps;

	private float FrameCheck_Timer;

	private float inputX;

	private float inputY;

	private global::UnityEngine.Vector3 MousePos;

	private global::UnityEngine.Vector3 MousePos_Prev;

	private global::UnityEngine.Vector3 MouseDownPos;

	private global::UnityEngine.Vector3 MouseMove;

	private global::UnityEngine.Vector3 pos_MouseDown;

	private global::UnityEngine.Vector3 pos_CamDown;

	private Gallery_Control GC;

	private void Start()
	{
		GC = global::UnityEngine.GameObject.Find("Gallery_Menu").GetComponent<Gallery_Control>();
		lastInterval = global::UnityEngine.Time.realtimeSinceStartup;
		frames = 0;
		targetPos = base.transform.position;
		MouseMove = new global::UnityEngine.Vector3(0f, 0f, 0f);
		MousePos = global::UnityEngine.Input.mousePosition;
		MouseDownPos = base.GetComponent<UnityEngine.Camera>().ScreenToWorldPoint(global::UnityEngine.Input.mousePosition);
		if (global::UnityEngine.PlayerPrefs.GetInt("onClockFps") != 1)
		{
			global::UnityEngine.GameObject.Find("Text_Fps").GetComponent<global::UnityEngine.UI.Text>().enabled = false;
		}
	}

	private void Awake()
	{
		if (global::UnityEngine.PlayerPrefs.GetInt("onFrameLimit") == 1)
		{
			global::UnityEngine.QualitySettings.vSyncCount = 0;
			global::UnityEngine.Application.targetFrameRate = 60;
		}
	}

	private void Check_Frame()
	{
	}

	private void Update()
	{
		frames++;
		float realtimeSinceStartup = global::UnityEngine.Time.realtimeSinceStartup;
		if (realtimeSinceStartup > lastInterval + updateInterval)
		{
			fps = (float)frames / (realtimeSinceStartup - lastInterval);
			frames = 0;
			lastInterval = realtimeSinceStartup;
			if (fps > 80f)
			{
				FrameCheck_Timer += 1f;
			}
			else
			{
				FrameCheck_Timer = 0f;
			}
		}
		if (global::UnityEngine.PlayerPrefs.GetInt("onFrameLimit") != 1 && FrameCheck_Timer > 10f)
		{
			global::UnityEngine.Debug.Log("OnFrameLimit!!!!!!!");
		}
		global::UnityEngine.GameObject.Find("Text_Fps").GetComponent<global::UnityEngine.UI.Text>().text = fps.ToString("f1") + "fps";
		if (Blur_Delay > 0f)
		{
			Blur_Delay -= global::UnityEngine.Time.deltaTime;
		}
		inputX = 0f;
		inputY = 0f;
		if (global::UnityEngine.Input.GetKey(global::UnityEngine.KeyCode.RightArrow))
		{
			inputX = 1f;
		}
		else if (global::UnityEngine.Input.GetKey(global::UnityEngine.KeyCode.LeftArrow))
		{
			inputX = -1f;
		}
		if (global::UnityEngine.Input.GetKey(global::UnityEngine.KeyCode.UpArrow))
		{
			inputY = 1f;
		}
		else if (global::UnityEngine.Input.GetKey(global::UnityEngine.KeyCode.DownArrow))
		{
			inputY = -1f;
		}
		if (global::UnityEngine.Input.GetAxis("R_Y") != 0f)
		{
			targetSize += global::UnityEngine.Input.GetAxis("R_Y") * -5f * global::UnityEngine.Time.deltaTime;
		}
		else if (global::UnityEngine.Input.GetKey(global::UnityEngine.KeyCode.PageUp))
		{
			targetSize -= 5f * global::UnityEngine.Time.deltaTime;
		}
		else if (global::UnityEngine.Input.GetKey(global::UnityEngine.KeyCode.PageDown))
		{
			targetSize += 5f * global::UnityEngine.Time.deltaTime;
		}
		if (global::UnityEngine.Input.GetButtonDown("R_A"))
		{
			Reset_Zoom();
		}
		else if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.Home))
		{
			Reset_Zoom();
		}
		if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.RightShift))
		{
			if (global::UnityEngine.PlayerPrefs.GetInt("onClockFps") == 1)
			{
				global::UnityEngine.PlayerPrefs.SetInt("onClockFps", 0);
				global::UnityEngine.GameObject.Find("Text_Fps").GetComponent<global::UnityEngine.UI.Text>().enabled = false;
			}
			else
			{
				global::UnityEngine.PlayerPrefs.SetInt("onClockFps", 1);
				global::UnityEngine.GameObject.Find("Text_Fps").GetComponent<global::UnityEngine.UI.Text>().enabled = true;
			}
		}
		if (global::UnityEngine.Input.mouseScrollDelta.y != 0f)
		{
			onMouseZoom = true;
			MouseZoom_Timer = 0.5f;
		}
		if (MouseZoom_Timer > 0f)
		{
			MouseZoom_Timer -= global::UnityEngine.Time.deltaTime;
			targetSize += (0f - global::UnityEngine.Input.mouseScrollDelta.y) * global::UnityEngine.Time.deltaTime * 40f;
		}
		MousePos = base.GetComponent<UnityEngine.Camera>().ScreenToWorldPoint(global::UnityEngine.Input.mousePosition);
		MouseMove = new global::UnityEngine.Vector3(MousePos.x - MousePos_Prev.x, MousePos.y - MousePos_Prev.y, 0f);
		MousePos_Prev = MousePos;
		if (GC.onMouseDrag && global::UnityEngine.Input.GetMouseButton(0))
		{
			if (MouseMove.x != 0f)
			{
				targetPos.x += (0f - MouseMove.x) * global::UnityEngine.Time.deltaTime * 100f;
			}
			if (MouseMove.y != 0f)
			{
				targetPos.y += (0f - MouseMove.y) * global::UnityEngine.Time.deltaTime * 100f;
			}
		}
		else if (inputX != 0f || inputY != 0f)
		{
			targetPos.x += inputX * global::UnityEngine.Time.deltaTime * 6f;
			targetPos.y += inputY * global::UnityEngine.Time.deltaTime * 6f;
		}
		else if (global::UnityEngine.Input.GetAxis("L_X") != 0f || global::UnityEngine.Input.GetAxis("L_Y") != 0f)
		{
			targetPos.x += global::UnityEngine.Input.GetAxis("L_X") * global::UnityEngine.Time.deltaTime * 8f;
			targetPos.y += global::UnityEngine.Input.GetAxis("L_Y") * global::UnityEngine.Time.deltaTime * 8f;
		}
	}

	private void Release_TargetPos()
	{
		targetPos = base.transform.position;
	}

	private void Reset_Zoom()
	{
		targetSize = 7f;
	}

	public void Set_TargetPos(global::UnityEngine.Vector3 pos)
	{
		targetPos = pos;
	}

	public void Set_Blur(int num)
	{
		onBlur = true;
		GetComponent<BlurEffect>().iterations = num;
		GetComponent<BlurEffect>().enabled = true;
		Blur_Delay = 0.3f;
	}

	private void Effect_Blur()
	{
		if (blurTimer > 0.01f)
		{
			blurTimer = 0f;
			if (GetComponent<BlurEffect>().iterations > 0)
			{
				GetComponent<BlurEffect>().iterations--;
				return;
			}
			onBlur = false;
			GetComponent<BlurEffect>().enabled = false;
		}
		else
		{
			blurTimer += global::UnityEngine.Time.deltaTime;
		}
	}

	private void FixedUpdate()
	{
		if (onBlur && Blur_Delay <= 0f)
		{
			Effect_Blur();
		}
		if (targetSize < 4f)
		{
			targetSize = 4f;
		}
		else if (targetSize > MaxSize)
		{
			targetSize = MaxSize;
		}
		base.GetComponent<UnityEngine.Camera>().orthographicSize = global::UnityEngine.Mathf.Lerp(base.GetComponent<UnityEngine.Camera>().orthographicSize, targetSize, global::UnityEngine.Time.deltaTime * 2f);
		if (targetPos.x - base.GetComponent<UnityEngine.Camera>().orthographicSize * base.GetComponent<UnityEngine.Camera>().aspect <= Cam_Left)
		{
			targetPos.x = Cam_Left + base.GetComponent<UnityEngine.Camera>().orthographicSize * base.GetComponent<UnityEngine.Camera>().aspect;
		}
		else if (targetPos.x + base.GetComponent<UnityEngine.Camera>().orthographicSize * base.GetComponent<UnityEngine.Camera>().aspect >= Cam_Right)
		{
			targetPos.x = Cam_Right - base.GetComponent<UnityEngine.Camera>().orthographicSize * base.GetComponent<UnityEngine.Camera>().aspect;
		}
		if (targetPos.y - base.GetComponent<UnityEngine.Camera>().orthographicSize <= Cam_Bot)
		{
			targetPos.y = Cam_Bot + base.GetComponent<UnityEngine.Camera>().orthographicSize;
		}
		else if (targetPos.y + base.GetComponent<UnityEngine.Camera>().orthographicSize >= Cam_Top)
		{
			targetPos.y = Cam_Top - base.GetComponent<UnityEngine.Camera>().orthographicSize;
		}
		base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, targetPos, global::UnityEngine.Time.deltaTime * 5f);
	}
}
