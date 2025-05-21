public class LV_3_XenoList : global::UnityEngine.MonoBehaviour
{
	private int Mon_Count;

	private int Mon_Count_Max;

	public int Index;

	public global::UnityEngine.Transform Distance_Bar;

	public global::UnityEngine.Transform[] Head_List;

	public global::UnityEngine.GameObject[] _MonObject;

	public float Horizontal_Ratio;

	public float Vertical_Ratio;

	public bool Random_Delete;

	private float[] Timer;

	private global::UnityEngine.Vector3 pos_Orig;

	private global::UnityEngine.Camera Main_Cam;

	private global::UnityEngine.GameObject Player;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		Player = global::UnityEngine.GameObject.Find("Player");
		Main_Cam = global::UnityEngine.GameObject.Find("Main Camera").GetComponent<global::UnityEngine.Camera>();
		Mon_Count_Max = 0;
		if (Head_List.Length > 0)
		{
			Timer = new float[Head_List.Length];
			for (int i = 0; i < Head_List.Length; i++)
			{
				Timer[i] = 0f;
				if (Random_Delete && global::UnityEngine.Random.Range(0, 10) > 5)
				{
					Head_List[i] = null;
				}
				else
				{
					Mon_Count_Max++;
				}
			}
		}
		pos_Orig = base.transform.position;
	}

	private void FixedUpdate()
	{
		float num = Main_Cam.transform.position.x - pos_Orig.x;
		float num2 = Main_Cam.transform.position.y - pos_Orig.y;
		base.transform.position = new global::UnityEngine.Vector3(pos_Orig.x + num * Horizontal_Ratio, pos_Orig.y + num2 * Vertical_Ratio, 0f);
	}

	private void Update()
	{
		if (GM.Paused || Mon_Count_Max <= 0)
		{
			return;
		}
		for (int i = 0; i < Head_List.Length; i++)
		{
			if (Head_List[i] != null && (global::UnityEngine.Vector3.Distance(Player.transform.position, Head_List[i].transform.position) < 5f || Timer[i] > 0f))
			{
				Active_Head(i);
			}
		}
	}

	private void Active_Head(int num)
	{
		Timer[num] += global::UnityEngine.Time.deltaTime;
		if (Timer[num] > 2f)
		{
			int num2 = 0;
			float num3 = -0.5f;
			float num4 = -5.2f;
			if (!(Head_List[num].name == "Mon_31"))
			{
				num3 = -1f;
				num4 = -6f;
				if (Head_List[num].name == "Mon_32")
				{
					num2 = 1;
				}
				else if (Head_List[num].name == "Mon_34")
				{
					num2 = 2;
				}
			}
			if (Head_List[num].transform.localScale.x < 0f)
			{
				num3 = 0f - num3;
			}
			global::UnityEngine.Vector3 position = new global::UnityEngine.Vector3(Head_List[num].position.x + num3, base.transform.position.y + num4, 0f);
			if (_MonObject[num2] != null)
			{
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_MonObject[num2], position, base.transform.rotation) as global::UnityEngine.GameObject;
				gameObject.transform.parent = base.transform.parent;
				gameObject.transform.localScale = new global::UnityEngine.Vector3(3f, 3f, 1f);
				gameObject.GetComponent<Mon_Index>().Index = Index + Mon_Count;
				gameObject.GetComponent<Monster>().onEvent = true;
				if (gameObject.GetComponent<AI_Mon_31>() != null)
				{
					gameObject.GetComponent<AI_Mon_31>().event_Type = AI_Mon_31.Event_Type.Bottom;
				}
				else if (gameObject.GetComponent<AI_Mon_Xeno>() != null)
				{
					gameObject.GetComponent<AI_Mon_Xeno>().event_Type = AI_Mon_Xeno.Event_Type.Bottom;
				}
			}
			Mon_Count++;
			global::UnityEngine.Object.Destroy(Head_List[num].gameObject);
			Mon_Count_Max--;
			Timer[num] = 0f;
		}
		else
		{
			if (!(Timer[num] > 1.5f))
			{
				return;
			}
			Head_List[num].transform.localPosition = global::UnityEngine.Vector3.Lerp(Head_List[num].transform.localPosition, new global::UnityEngine.Vector3(Head_List[num].transform.localPosition.x, 0.8f, 0f), global::UnityEngine.Time.deltaTime * 6f);
			if (Head_List[num].name == "Mon_31")
			{
				if (Head_List[num].transform.localScale.x > 0f)
				{
					Head_List[num].transform.localRotation = global::UnityEngine.Quaternion.Euler(0f, 0f, global::UnityEngine.Mathf.Lerp(Head_List[num].transform.localRotation.eulerAngles.z, 325f, global::UnityEngine.Time.deltaTime * 3f));
				}
				else
				{
					Head_List[num].transform.localRotation = global::UnityEngine.Quaternion.Euler(0f, 0f, global::UnityEngine.Mathf.Lerp(Head_List[num].transform.localRotation.eulerAngles.z, 35f, global::UnityEngine.Time.deltaTime * 3f));
				}
			}
			else if (Head_List[num].transform.localScale.x > 0f)
			{
				Head_List[num].transform.localRotation = global::UnityEngine.Quaternion.Euler(0f, 0f, global::UnityEngine.Mathf.Lerp(Head_List[num].transform.localRotation.eulerAngles.z, 315f, global::UnityEngine.Time.deltaTime * 3f));
			}
			else
			{
				Head_List[num].transform.localRotation = global::UnityEngine.Quaternion.Euler(0f, 0f, global::UnityEngine.Mathf.Lerp(Head_List[num].transform.localRotation.eulerAngles.z, 45f, global::UnityEngine.Time.deltaTime * 3f));
			}
		}
	}
}
