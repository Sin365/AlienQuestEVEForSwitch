public class H_Test_Ani : global::UnityEngine.MonoBehaviour
{
	public int Cum_Index = 50;

	public float Cum_Size = 1f;

	public global::UnityEngine.GameObject[] CumShot;

	public global::UnityEngine.GameObject[] CumDot;

	public global::UnityEngine.Transform pos_Mouth_1;

	public global::UnityEngine.Transform pos_Mouth_2;

	public global::UnityEngine.Transform pos_Virgina;

	public global::UnityEngine.Transform pos_Penis;

	private float Life_Timer;

	private global::UnityEngine.Vector3 pos_Orig;

	private void Start()
	{
		pos_Orig = base.transform.position;
	}

	private void Update()
	{
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.J))
		{
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(CumShot[global::UnityEngine.Random.Range(0, 6)], base.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 90f)) as global::UnityEngine.GameObject;
			gameObject.GetComponent<H_CumDown>().pos_Target = base.transform;
			gameObject.GetComponent<H_CumDown>().Set_SortingOrder(Cum_Index);
			gameObject.GetComponent<H_CumDown>().Set_DownDirect(Cum_Size);
		}
		if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.K))
		{
			global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(CumShot[global::UnityEngine.Random.Range(0, 6)], base.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 90f)) as global::UnityEngine.GameObject;
			gameObject2.GetComponent<H_CumDown>().pos_Target = base.transform;
			gameObject2.GetComponent<H_CumDown>().Set_SortingOrder(Cum_Index);
			gameObject2.GetComponent<H_CumDown>().Set_DownDrool(Cum_Size);
		}
		if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.L))
		{
			for (int i = 0; i < 1; i++)
			{
				global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(CumShot[global::UnityEngine.Random.Range(0, 6)], base.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 90f)) as global::UnityEngine.GameObject;
				gameObject3.GetComponent<H_CumDown>().pos_Target = base.transform;
				gameObject3.GetComponent<H_CumDown>().Set_SortingOrder(Cum_Index);
				gameObject3.GetComponent<H_CumDown>().Set_Pee();
			}
			for (int j = 0; j < 8; j++)
			{
				global::UnityEngine.GameObject gameObject4 = global::UnityEngine.Object.Instantiate(CumDot[global::UnityEngine.Random.Range(1, 3)], base.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
			}
		}
		if (pos_Penis == null)
		{
			base.transform.position = new global::UnityEngine.Vector3(pos_Orig.x + global::UnityEngine.Mathf.Sin(Life_Timer * 1f) * 1f, pos_Orig.y, 0f);
		}
		else
		{
			base.transform.position = pos_Penis.position;
		}
	}

	private void Cum_Set_1()
	{
	}

	private void Cum_Penis_Direct()
	{
	}

	private void Cum_Penis_Drool()
	{
	}

	private void Cum_Virgina_Direct()
	{
	}

	private void Cum_Virgina_Drool()
	{
	}

	private void Cum_Virgina_Pee()
	{
	}

	private void Cum_Mouth_Direct()
	{
	}

	private void Cum_Mouth_Drool()
	{
	}
}
