public class Dust_Control : global::UnityEngine.MonoBehaviour
{
	public int Dust_Max;

	public global::UnityEngine.Vector2 dist_XY;

	public global::UnityEngine.GameObject _dust;

	private global::UnityEngine.GameObject[] Dust_List;

	private float[] startTimer;

	private float[] opacity;

	private float[] lifeTimer;

	private float[] killTimer;

	private float[] speed;

	private float[] rotationZ;

	public bool onEnabled = true;

	public int facingRight = -1;

	private float Dust_Timer;

	private int Dust_Num;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		Dust_List = new global::UnityEngine.GameObject[Dust_Max];
		startTimer = new float[Dust_Max];
		opacity = new float[Dust_Max];
		lifeTimer = new float[Dust_Max];
		killTimer = new float[Dust_Max];
		speed = new float[Dust_Max];
		rotationZ = new float[Dust_Max];
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		Dust_Timer += global::UnityEngine.Time.deltaTime;
		for (int i = 0; i < Dust_Max; i++)
		{
			if (Dust_List[i] != null)
			{
				Move_Dust(i);
			}
			else if (Dust_Timer > 0.02f)
			{
				Dust_Timer = 0f;
				Dust_Num++;
				Dust_List[i] = global::UnityEngine.Object.Instantiate(_dust) as global::UnityEngine.GameObject;
				Dust_List[i].transform.parent = base.transform;
				Make_Dust(i);
			}
		}
		if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.Return))
		{
			if (onEnabled)
			{
				Dust_Off();
			}
			else
			{
				Dust_On();
			}
		}
	}

	private void Turn_Right()
	{
		facingRight = 1;
	}

	private void Dust_On()
	{
		onEnabled = true;
	}

	private void Dust_Off()
	{
		onEnabled = false;
		for (int i = 0; i < Dust_Max; i++)
		{
			lifeTimer[i] = 0f;
		}
	}

	private void Move_Dust(int num)
	{
		if (startTimer[num] > 0f)
		{
			opacity[num] += global::UnityEngine.Time.deltaTime;
			if (opacity[num] > 1f)
			{
				opacity[num] = 1f;
			}
			Dust_List[num].GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, opacity[num]);
			startTimer[num] -= global::UnityEngine.Time.deltaTime;
		}
		if (lifeTimer[num] > 0f)
		{
			Dust_List[num].transform.Translate(global::UnityEngine.Vector3.right * facingRight * global::UnityEngine.Time.deltaTime * speed[num]);
			Dust_List[num].transform.rotation = global::UnityEngine.Quaternion.Euler(0f, 0f, rotationZ[num]);
			lifeTimer[num] -= global::UnityEngine.Time.deltaTime;
		}
		else if (killTimer[num] > 0f)
		{
			Dust_List[num].transform.Translate(global::UnityEngine.Vector3.right * facingRight * global::UnityEngine.Time.deltaTime * speed[num]);
			Dust_List[num].transform.rotation = global::UnityEngine.Quaternion.Euler(0f, 0f, rotationZ[num]);
			opacity[num] -= global::UnityEngine.Time.deltaTime;
			if (opacity[num] >= 0f)
			{
				Dust_List[num].GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, opacity[num]);
			}
			killTimer[num] -= global::UnityEngine.Time.deltaTime;
		}
		else if (onEnabled)
		{
			Make_Dust(num);
		}
	}

	private void Make_Dust(int num)
	{
		startTimer[num] = 1f;
		speed[num] = (float)global::UnityEngine.Random.Range(10, 50) * 0.01f;
		lifeTimer[num] = (float)global::UnityEngine.Random.Range(20, 200) * 0.1f;
		killTimer[num] = 1f;
		float num2 = global::UnityEngine.Random.Range(0f - dist_XY.x, dist_XY.x);
		float num3 = global::UnityEngine.Random.Range(0f - dist_XY.y, dist_XY.y);
		Dust_List[num].transform.position = new global::UnityEngine.Vector3(base.transform.position.x + num2, base.transform.position.y + num3, 0f);
		if (facingRight > 0)
		{
			rotationZ[num] = (float)global::UnityEngine.Random.Range(-400, -90) * 0.1f;
		}
		else
		{
			rotationZ[num] = (float)global::UnityEngine.Random.Range(90, 400) * 0.1f;
		}
		Dust_List[num].GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
	}

	private void Delete_All()
	{
		for (int i = 0; i < Dust_Max; i++)
		{
			if (Dust_List[i] != null)
			{
				global::UnityEngine.Object.Destroy(Dust_List[i].gameObject);
			}
		}
	}
}
