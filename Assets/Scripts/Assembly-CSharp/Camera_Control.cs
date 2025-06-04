public class Camera_Control : global::UnityEngine.MonoBehaviour
{
	public float Cam_Top = 10f;

	public float Cam_Bot = -10f;

	public float Cam_Right = 10f;

	public float Cam_Left = -10f;

	public float targetSize = 10f;

	public float MaxSize = 11.2f;

	private float orignalSize = 8f;

	private float currentSize = 10f;

	private float playingSize = 8f;

	private float camsizeSpeed = 2f;

	private float moveSpeed = 5f;

	private global::UnityEngine.Vector3 currentPos;

	private global::UnityEngine.Vector3 targetPos;

	public global::UnityEngine.Vector2 RoomMax;

	public global::UnityEngine.Vector2 RoomMin;

	public bool Room_Changed;

	private int RoomChange_CutIn;

	private float moveLerp = 1f;

	private float camHeight = 4f;

	private float camHeightTarget = 4f;

	private float GroundAirDrop_Timer;

	private bool onZoom;

	private float zoomLerp = 0.03f;

	private bool onShake;

	private float shakeTimer = -100f;

	private float shakeDeg = 0.01f;

	private float shakeDelay;

	private global::UnityEngine.Vector2 shake_R = new global::UnityEngine.Vector2(0f, 0f);

	private bool onBlur;

	private float blurTimer;

	private float Contrast_Timer;

	private bool onQueenDeath;

	private float Move_Delay;

	private float Move_Distance_X;

	private float Move_Distance_Y;

	private float EventCam_Speed = 1f;

	private float Lerp_Speed = 5f;

	private bool on_H_Zoom;

	private float updateInterval = 0.5f;

	private float lastInterval;

	private int frames;

	public float fps;

	private global::UnityEngine.BoxCollider2D Col_Cam;

	private GameManager GM;

	private global::UnityEngine.GameObject Player;

	private Player_Control PC;

	private void Start()
	{
		lastInterval = global::UnityEngine.Time.realtimeSinceStartup;
		frames = 0;
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		Player = global::UnityEngine.GameObject.Find("Player");
		PC = Player.GetComponent<Player_Control>();
		Col_Cam = global::UnityEngine.GameObject.Find("COL_Cam").GetComponent<global::UnityEngine.BoxCollider2D>();
		currentSize = orignalSize;
		targetSize = orignalSize;
		currentPos = (targetPos = base.transform.position);
	}

	private void Awake()
	{
		if (AxiPlayerPrefs.GetFloat("Avg_Fps") > 65f)
		{
			global::UnityEngine.QualitySettings.vSyncCount = 0;
			global::UnityEngine.Application.targetFrameRate = 60;
		}
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
		}
	}

	private void Reset_Zoom_Button()
	{
		targetSize = orignalSize;
		on_H_Zoom = false;
	}

	private void Reset_Zoom()
	{
		targetSize = playingSize;
		on_H_Zoom = false;
	}

	private void Hscene_Zoom()
	{
		if (!GM.GameOver)
		{
			playingSize = base.GetComponent<UnityEngine.Camera>().orthographicSize;
		}
		targetSize = 5f;
	}

	public void Set_Blur_Pause()
	{
		onBlur = false;
		GetComponent<BlurEffect>().iterations = 12;
		GetComponent<BlurEffect>().enabled = true;
	}

	public void Set_Blur(int num)
	{
		onBlur = true;
		GetComponent<BlurEffect>().iterations = num;
		GetComponent<BlurEffect>().enabled = true;
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

	public void Set_Shake()
	{
		if (shakeTimer < 1000f)
		{
			onShake = true;
			shakeTimer = 3.6f;
			shakeDeg = 0.004f;
		}
		GetComponent<ContrastStretchEffect>().limitMaximum = 0.85f;
		GetComponent<ContrastStretchEffect>().enabled = true;
		Contrast_Timer = 3.6f;
	}

	public void Set_Shake_Mother()
	{
		if (shakeTimer < 1000f)
		{
			onShake = true;
			shakeTimer = 6.6f;
			shakeDeg = 0.004f;
		}
		GetComponent<ContrastStretchEffect>().limitMaximum = 0.85f;
		GetComponent<ContrastStretchEffect>().enabled = true;
		Contrast_Timer = 6.6f;
	}

	public void Set_Shake_Timer(float timer, global::UnityEngine.Vector3 pos)
	{
		float num = global::UnityEngine.Vector3.Distance(base.transform.position, new global::UnityEngine.Vector3(pos.x, pos.y, -10f));
		if (num < base.GetComponent<UnityEngine.Camera>().orthographicSize * 3f)
		{
			if (!onShake)
			{
				onShake = true;
				shakeTimer = timer;
				shakeDeg = 0.003f;
			}
			else if (shakeTimer < timer)
			{
				shakeTimer = timer;
			}
		}
	}

	public void Test_Shake()
	{
		onShake = true;
		shakeTimer = 1.5f;
		shakeDeg = 0.004f;
	}

	public void Set_Queen_Death()
	{
		onQueenDeath = true;
		Move_Delay = 5f;
	}

	public void Set_Queen_Shake()
	{
		onShake = true;
		shakeTimer = 1500f;
		shakeDeg = 0.004f;
		Lerp_Speed = 0f;
	}

	private void Set_Camera_Col()
	{
		Col_Cam.size = new global::UnityEngine.Vector2(base.GetComponent<UnityEngine.Camera>().orthographicSize * 3.55f + 0.5f, base.GetComponent<UnityEngine.Camera>().orthographicSize * 2f + 0.5f);
	}

	public void Event_Cam_Pos(global::UnityEngine.Vector3 pos, float speed)
	{
		targetPos = pos;
		EventCam_Speed = speed;
		if (GM.Get_Event(5) && !GM.Get_Event(3))
		{
			moveSpeed = 0f;
		}
	}

	public void Set_Start_Cam_Size(float size)
	{
		currentSize = size;
	}

	private void FixedUpdate()
	{
		if (onBlur)
		{
			Effect_Blur();
		}
		if (Contrast_Timer > 0f)
		{
			Contrast_Timer -= global::UnityEngine.Time.deltaTime;
		}
		else if (GetComponent<ContrastStretchEffect>().enabled)
		{
			if (GetComponent<ContrastStretchEffect>().limitMaximum < 16f)
			{
				GetComponent<ContrastStretchEffect>().limitMaximum = global::UnityEngine.Mathf.Lerp(GetComponent<ContrastStretchEffect>().limitMaximum, 20f, 0.08f);
			}
			else
			{
				GetComponent<ContrastStretchEffect>().enabled = false;
			}
		}
		if (GM.GameOver && !GM.onGatePass)
		{
			if (targetSize < 5f)
			{
				targetSize = 5f;
			}
			else if (targetSize > 8f)
			{
				targetSize = 8f;
			}
			currentSize = global::UnityEngine.Mathf.Lerp(currentSize, targetSize, global::UnityEngine.Time.deltaTime * 1f);
			base.GetComponent<UnityEngine.Camera>().orthographicSize = currentSize;
			global::UnityEngine.Vector3 position = global::UnityEngine.GameObject.Find("Pos_Down_Center").transform.position;
			if (position.x - base.GetComponent<UnityEngine.Camera>().orthographicSize * base.GetComponent<UnityEngine.Camera>().aspect <= Cam_Left)
			{
				position.x = Cam_Left + base.GetComponent<UnityEngine.Camera>().orthographicSize * base.GetComponent<UnityEngine.Camera>().aspect;
			}
			else if (position.x + base.GetComponent<UnityEngine.Camera>().orthographicSize * base.GetComponent<UnityEngine.Camera>().aspect >= Cam_Right)
			{
				position.x = Cam_Right - base.GetComponent<UnityEngine.Camera>().orthographicSize * base.GetComponent<UnityEngine.Camera>().aspect;
			}
			float num = 3f;
			float num2 = ((!PC.Lock_Lift[1]) ? Player.transform.position.y : PC.Pos_Lift[1]);
			num = ((Player.transform.position.y + 3f - base.GetComponent<UnityEngine.Camera>().orthographicSize <= Cam_Bot) ? (Cam_Bot + base.GetComponent<UnityEngine.Camera>().orthographicSize) : ((!(Player.transform.position.y + 3f + base.GetComponent<UnityEngine.Camera>().orthographicSize >= Cam_Top)) ? (Player.transform.position.y + 3f) : (Cam_Top - base.GetComponent<UnityEngine.Camera>().orthographicSize)));
			if (GM.onHscene)
			{
				num = ((GM.Hscene_Num != 3 && GM.Hscene_Num != 11 && GM.Hscene_Num != 17 && GM.Hscene_Num != 18 && GM.Hscene_Num != 19 && GM.Hscene_Num != 20 && GM.Hscene_Num != 27 && GM.Hscene_Num != 28) ? (num + 0.5f) : num);
			}
			targetPos = new global::UnityEngine.Vector3(position.x, num, -10f);
			base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, targetPos, global::UnityEngine.Time.deltaTime * 1f);
			if (onShake)
			{
				onShake = false;
				shakeTimer = 0f;
			}
		}
		else if (GM.onEvent && !GM.Paused && !GM.onHscene && !GM.onGameClear)
		{
			base.GetComponent<UnityEngine.Camera>().orthographicSize = global::UnityEngine.Mathf.Lerp(base.GetComponent<UnityEngine.Camera>().orthographicSize, targetSize, global::UnityEngine.Time.deltaTime * EventCam_Speed);
			currentSize = base.GetComponent<UnityEngine.Camera>().orthographicSize;
			base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, targetPos, global::UnityEngine.Time.deltaTime * EventCam_Speed);
		}
		else
		{
			if (GM.Paused)
			{
				return;
			}
			Set_Camera_Col();
			if (targetSize < 5f)
			{
				targetSize = 5f;
			}
			else if (targetSize > MaxSize)
			{
				targetSize = MaxSize;
			}
			if (currentSize != targetSize)
			{
				if (GM.onGatePass)
				{
					currentSize = global::UnityEngine.Mathf.Lerp(currentSize, targetSize, global::UnityEngine.Time.deltaTime * 12f);
				}
				else
				{
					currentSize = global::UnityEngine.Mathf.Lerp(currentSize, targetSize, global::UnityEngine.Time.deltaTime * 2f);
				}
			}
			base.GetComponent<UnityEngine.Camera>().orthographicSize = currentSize;
			if (!GM.onHscene && (PC.Speed_X >= 1f || PC.Speed_X <= -1f))
			{
				if (GM.Velcocity.x > 1f)
				{
					Move_Distance_X = global::UnityEngine.Mathf.Lerp(Move_Distance_X, (2.8f + GM.Velcocity.x) * (float)PC.facingRight, global::UnityEngine.Time.deltaTime * 3f);
				}
				else
				{
					Move_Distance_X = global::UnityEngine.Mathf.Lerp(Move_Distance_X, 2.8f * (float)PC.facingRight, global::UnityEngine.Time.deltaTime * 3f);
				}
			}
			else
			{
				Move_Distance_X = global::UnityEngine.Mathf.Lerp(Move_Distance_X, 0f, global::UnityEngine.Time.deltaTime * 0.8f);
			}
			if (PC.onHighJump && GM.Velcocity.y > 0f)
			{
				Move_Distance_Y = global::UnityEngine.Mathf.Lerp(Move_Distance_Y, 5f, global::UnityEngine.Time.deltaTime * 5f);
			}
			else
			{
				Move_Distance_Y = global::UnityEngine.Mathf.Lerp(Move_Distance_Y, 0f, global::UnityEngine.Time.deltaTime * 5f);
			}
			float num3 = 0f;
			float num4 = ((!PC.Lock_Lift[0]) ? Player.transform.position.x : PC.Pos_Lift[0]);
			num3 = ((num4 + Move_Distance_X - base.GetComponent<UnityEngine.Camera>().orthographicSize * base.GetComponent<UnityEngine.Camera>().aspect <= Cam_Left) ? (Cam_Left + base.GetComponent<UnityEngine.Camera>().orthographicSize * base.GetComponent<UnityEngine.Camera>().aspect) : ((!(num4 + Move_Distance_X + base.GetComponent<UnityEngine.Camera>().orthographicSize * base.GetComponent<UnityEngine.Camera>().aspect >= Cam_Right)) ? (num4 + Move_Distance_X) : (Cam_Right - base.GetComponent<UnityEngine.Camera>().orthographicSize * base.GetComponent<UnityEngine.Camera>().aspect)));
			float num5 = 4.5f;
			float num6 = ((!PC.Lock_Lift[1]) ? Player.transform.position.y : PC.Pos_Lift[1]);
			num5 = ((num6 + 4.5f + Move_Distance_Y - base.GetComponent<UnityEngine.Camera>().orthographicSize <= Cam_Bot) ? (Cam_Bot + base.GetComponent<UnityEngine.Camera>().orthographicSize) : ((!(num6 + 4.5f + Move_Distance_Y + base.GetComponent<UnityEngine.Camera>().orthographicSize >= Cam_Top)) ? (num6 + 4.5f + Move_Distance_Y) : (Cam_Top - base.GetComponent<UnityEngine.Camera>().orthographicSize)));
			if (GM.onHscene)
			{
				num5 = ((GM.Hscene_Num != 3 && GM.Hscene_Num != 11 && GM.Hscene_Num != 17 && GM.Hscene_Num != 18 && GM.Hscene_Num != 19 && GM.Hscene_Num != 20 && GM.Hscene_Num != 27 && GM.Hscene_Num != 28) ? (num5 - 0.5f) : (num5 - 1.2f));
			}
			targetPos = new global::UnityEngine.Vector3(num3, num5, -10f);
			if (onQueenDeath)
			{
				Move_Delay -= global::UnityEngine.Time.deltaTime;
				if (Move_Delay <= 0f)
				{
					onQueenDeath = false;
				}
				if (global::UnityEngine.GameObject.Find("Pos_CamToQueen") != null)
				{
					targetPos = global::UnityEngine.Vector3.Lerp(base.transform.position, global::UnityEngine.GameObject.Find("Pos_CamToQueen").transform.position, global::UnityEngine.Time.deltaTime * 1f);
					base.transform.position = targetPos;
				}
			}
			if (moveSpeed < 5f)
			{
				moveSpeed = global::UnityEngine.Mathf.Lerp(moveSpeed, 5f, global::UnityEngine.Time.deltaTime);
				if (moveSpeed > 4.99f)
				{
					moveSpeed = 5f;
				}
			}
			if (onShake)
			{
				if (shakeTimer < 1000f)
				{
					shakeTimer -= global::UnityEngine.Time.deltaTime;
				}
				if (shakeTimer <= 0f)
				{
					onShake = false;
				}
				else if (shakeTimer < 0.4f)
				{
					shakeDeg = global::UnityEngine.Mathf.Lerp(shakeDeg, 0f, global::UnityEngine.Time.deltaTime * 3f);
				}
				if (!onQueenDeath && Lerp_Speed < 5f)
				{
					Lerp_Speed = global::UnityEngine.Mathf.Lerp(Lerp_Speed, 5f, global::UnityEngine.Time.deltaTime * 2f);
				}
				shake_R.x = (float)global::UnityEngine.Random.Range(-50, 50) * shakeDeg;
				shake_R.y = (float)global::UnityEngine.Random.Range(-50, 50) * shakeDeg;
				targetPos = global::UnityEngine.Vector3.Lerp(base.transform.position, targetPos, global::UnityEngine.Time.deltaTime * Lerp_Speed);
				base.transform.position = new global::UnityEngine.Vector3(targetPos.x + shake_R.x, targetPos.y + shake_R.y, -10f);
			}
			else
			{
				base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, targetPos, global::UnityEngine.Time.deltaTime * moveSpeed);
			}
		}
	}
}
