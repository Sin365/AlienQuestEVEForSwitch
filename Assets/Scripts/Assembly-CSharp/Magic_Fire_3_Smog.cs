public class Magic_Fire_3_Smog : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject bar_Obj;

	private global::UnityEngine.GameObject Bar_C;

	private global::UnityEngine.GameObject[] Bar_L = new global::UnityEngine.GameObject[40];

	private global::UnityEngine.GameObject[] Bar_R = new global::UnityEngine.GameObject[40];

	private float Life_Timer;

	private int Bar_Num;

	private int Bar_Max = 40;

	private float Bar_Timer;

	private global::UnityEngine.Color color_TrCenter = new global::UnityEngine.Color(1f, 1f, 1f, 2f);

	private global::UnityEngine.Color[] color_Target = new global::UnityEngine.Color[40];

	private float size_Target = 1.5f;

	GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		for (int i = 0; i < color_Target.Length; i++)
		{
			color_Target[i] = new global::UnityEngine.Color(1f, 1f, 1f, 2f);
		}
		Bar_C = global::UnityEngine.Object.Instantiate(bar_Obj, new global::UnityEngine.Vector3(base.transform.position.x, base.transform.position.y, 0f), base.transform.rotation) as global::UnityEngine.GameObject;
		Bar_C.transform.localScale = new global::UnityEngine.Vector3(1f, 0.07f, 1f);
		Bar_C.transform.parent = base.transform;
		if (bar_Obj != null)
		{
			size_Target = bar_Obj.transform.localScale.y * 1.5f;
		}
	}

	private void Make_Bar()
	{
		Bar_L[Bar_Num] = global::UnityEngine.Object.Instantiate(bar_Obj, new global::UnityEngine.Vector3(base.transform.position.x - 0.3f - 0.3f * (float)Bar_Num, base.transform.position.y, 0f), base.transform.rotation) as global::UnityEngine.GameObject;
		Bar_R[Bar_Num] = global::UnityEngine.Object.Instantiate(bar_Obj, new global::UnityEngine.Vector3(base.transform.position.x + 0.3f + 0.3f * (float)Bar_Num, base.transform.position.y, 0f), base.transform.rotation) as global::UnityEngine.GameObject;
		global::UnityEngine.Transform obj = Bar_L[Bar_Num].transform;
		global::UnityEngine.Vector3 localScale = new global::UnityEngine.Vector3(1f, 0.07f, 1f);
		Bar_R[Bar_Num].transform.localScale = localScale;
		obj.localScale = localScale;
		Bar_L[Bar_Num].transform.parent = base.transform;
		Bar_R[Bar_Num].transform.parent = base.transform;
		Bar_Num++;
		Bar_Timer = 0f;
	}

	private void Set_Bar()
	{
		for (int i = 0; i < Bar_Num; i++)
		{
			if (color_Target[i].a > 0f)
			{
				Bar_L[i].GetComponent<global::UnityEngine.SpriteRenderer>().color = global::UnityEngine.Color.Lerp(Bar_L[i].GetComponent<global::UnityEngine.SpriteRenderer>().color, color_Target[i], global::UnityEngine.Time.deltaTime * 2f);
				Bar_R[i].GetComponent<global::UnityEngine.SpriteRenderer>().color = global::UnityEngine.Color.Lerp(Bar_R[i].GetComponent<global::UnityEngine.SpriteRenderer>().color, color_Target[i], global::UnityEngine.Time.deltaTime * 2f);
			}
			else
			{
				Bar_L[i].GetComponent<global::UnityEngine.SpriteRenderer>().color = global::UnityEngine.Color.Lerp(Bar_L[i].GetComponent<global::UnityEngine.SpriteRenderer>().color, color_Target[i], global::UnityEngine.Time.deltaTime * 3f);
				Bar_R[i].GetComponent<global::UnityEngine.SpriteRenderer>().color = global::UnityEngine.Color.Lerp(Bar_R[i].GetComponent<global::UnityEngine.SpriteRenderer>().color, color_Target[i], global::UnityEngine.Time.deltaTime * 3f);
			}
			if (Bar_L[i].GetComponent<global::UnityEngine.SpriteRenderer>().color.a > 0.95f)
			{
				color_Target[i] = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
			}
			Bar_L[i].transform.localScale = new global::UnityEngine.Vector3(1f, global::UnityEngine.Mathf.Lerp(Bar_L[i].transform.localScale.y, size_Target, global::UnityEngine.Time.deltaTime * 0.8f), 1f);
			Bar_R[i].transform.localScale = new global::UnityEngine.Vector3(1f, global::UnityEngine.Mathf.Lerp(Bar_R[i].transform.localScale.y, size_Target, global::UnityEngine.Time.deltaTime * 0.8f), 1f);
		}
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			Life_Timer += global::UnityEngine.Time.deltaTime;
			Bar_Timer += global::UnityEngine.Time.deltaTime;
			if (Bar_Timer > 0.025f && Bar_Num < Bar_Max)
			{
				Make_Bar();
			}
			if (Bar_Num > 0)
			{
				Set_Bar();
			}
			if (color_TrCenter.a > 0f)
			{
				Bar_C.GetComponent<global::UnityEngine.SpriteRenderer>().color = global::UnityEngine.Color.Lerp(Bar_C.GetComponent<global::UnityEngine.SpriteRenderer>().color, color_TrCenter, global::UnityEngine.Time.deltaTime * 2f);
			}
			else
			{
				Bar_C.GetComponent<global::UnityEngine.SpriteRenderer>().color = global::UnityEngine.Color.Lerp(Bar_C.GetComponent<global::UnityEngine.SpriteRenderer>().color, color_TrCenter, global::UnityEngine.Time.deltaTime * 3f);
			}
			if (Bar_C.GetComponent<global::UnityEngine.SpriteRenderer>().color.a > 0.95f)
			{
				color_TrCenter = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
			}
			Bar_C.transform.localScale = new global::UnityEngine.Vector3(1f, global::UnityEngine.Mathf.Lerp(Bar_C.transform.localScale.y, size_Target, global::UnityEngine.Time.deltaTime * 0.8f), 1f);
			if (Bar_Num == Bar_Max && Bar_L[Bar_Max - 1].GetComponent<global::UnityEngine.SpriteRenderer>().color.a < 0.01f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			else if (Life_Timer > 3.5f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}
}
