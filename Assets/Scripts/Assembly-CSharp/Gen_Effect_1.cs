public class Gen_Effect_1 : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.Sprite spr_Dust;

	public global::UnityEngine.GameObject DustObject;

	private int Item_Num;

	private global::UnityEngine.GameObject[] Dust_List;

	private global::UnityEngine.Vector3[] Orig_Pos;

	private float[] Speed_List;

	private float[] Opacity;

	private int DustNum = 20;

	private void Start()
	{
		Dust_List = new global::UnityEngine.GameObject[DustNum];
		Orig_Pos = new global::UnityEngine.Vector3[DustNum];
		Speed_List = new float[DustNum];
		Opacity = new float[DustNum];
		Item_Num = GetComponent<Item>().Item_Num;
		for (int i = 0; i < DustNum; i++)
		{
			Make_Dust(i);
		}
	}

	private void Update()
	{
		for (int i = 0; i < DustNum; i++)
		{
			if (Item_Num == 10)
			{
				Opacity[i] += global::UnityEngine.Time.deltaTime * 1f;
				Dust_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, Opacity[i]);
				Dust_List[i].transform.position = global::UnityEngine.Vector3.Lerp(Dust_List[i].transform.position, base.transform.position, global::UnityEngine.Time.deltaTime * 2f);
				if (global::UnityEngine.Vector3.Distance(base.transform.position, Dust_List[i].transform.position) < 0.2f)
				{
					Dust_Restart(i);
				}
				continue;
			}
			Opacity[i] -= global::UnityEngine.Time.deltaTime * 1f;
			Dust_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, Opacity[i]);
			Dust_List[i].transform.Translate(global::UnityEngine.Vector3.up * global::UnityEngine.Time.deltaTime * Speed_List[i]);
			if (Item_Num == 25 || Item_Num == 202)
			{
				Dust_List[i].transform.localScale = global::UnityEngine.Vector3.Lerp(Dust_List[i].transform.localScale, new global::UnityEngine.Vector3(0.01f, 50f, 1f), global::UnityEngine.Time.deltaTime * 1f);
			}
			else if (Item_Num == 3)
			{
				Dust_List[i].transform.localScale = global::UnityEngine.Vector3.Lerp(Dust_List[i].transform.localScale, new global::UnityEngine.Vector3(0.001f, 30f, 1f), global::UnityEngine.Time.deltaTime * 1f);
			}
			if (Opacity[i] <= 0f)
			{
				Dust_Restart(i);
			}
		}
	}

	private void Make_Dust(int num)
	{
		Dust_List[num] = (global::UnityEngine.GameObject)global::UnityEngine.Object.Instantiate(DustObject, base.transform.position, base.transform.rotation);
		Dust_List[num].GetComponent<global::UnityEngine.SpriteRenderer>().sprite = spr_Dust;
		Dust_List[num].transform.parent = base.transform;
		float num2 = 0.8f + (float)global::UnityEngine.Random.Range(0, 100) * 0.005f;
		Dust_List[num].transform.localScale = new global::UnityEngine.Vector3(num2, num2, 1f);
		if (Item_Num == 10)
		{
			Orig_Pos[num] = new global::UnityEngine.Vector3(base.transform.position.x + (float)global::UnityEngine.Random.Range(-15, 15) * 0.1f, base.transform.position.y + (float)global::UnityEngine.Random.Range(-15, 15) * 0.1f, 0f);
			Opacity[num] = 0f;
		}
		else
		{
			Orig_Pos[num] = new global::UnityEngine.Vector3(base.transform.position.x + (float)global::UnityEngine.Random.Range(-40, 40) * 0.01f, base.transform.position.y + (float)global::UnityEngine.Random.Range(-20, 10) * 0.01f, 0f);
			Opacity[num] = 0.5f + (float)global::UnityEngine.Random.Range(0, 100) * 0.005f;
		}
		Dust_List[num].transform.position = Orig_Pos[num];
		if (Item_Num == 24)
		{
			Speed_List[num] = 0.4f + (float)global::UnityEngine.Random.Range(0, 100) * 0.005f;
		}
		else
		{
			Speed_List[num] = 0.5f + (float)global::UnityEngine.Random.Range(0, 100) * 0.005f;
		}
		Dust_List[num].GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, Opacity[num]);
	}

	private void Dust_Restart(int num)
	{
		if (Item_Num == 10)
		{
			Opacity[num] = 0f;
			Dust_List[num].transform.position = new global::UnityEngine.Vector3(base.transform.position.x + (float)global::UnityEngine.Random.Range(-15, 15) * 0.1f, base.transform.position.y + (float)global::UnityEngine.Random.Range(-15, 15) * 0.1f, 0f);
		}
		else if (Item_Num == 202)
		{
			Opacity[num] = 0.5f + (float)global::UnityEngine.Random.Range(0, 100) * 0.005f;
			Dust_List[num].transform.position = new global::UnityEngine.Vector3(base.transform.position.x + (float)global::UnityEngine.Random.Range(-25, 25) * 0.01f, base.transform.position.y + (float)global::UnityEngine.Random.Range(-20, 10) * 0.01f, 0f);
		}
		else
		{
			Opacity[num] = 0.5f + (float)global::UnityEngine.Random.Range(0, 100) * 0.005f;
			Dust_List[num].transform.position = new global::UnityEngine.Vector3(base.transform.position.x + (float)global::UnityEngine.Random.Range(-40, 40) * 0.01f, base.transform.position.y + (float)global::UnityEngine.Random.Range(-20, 10) * 0.01f, 0f);
		}
		Dust_List[num].GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, Opacity[num]);
		float num2 = 0.8f + (float)global::UnityEngine.Random.Range(0, 100) * 0.005f;
		Dust_List[num].transform.localScale = new global::UnityEngine.Vector3(num2, num2, 1f);
	}

	public void Destroy_Dust()
	{
		for (int i = 0; i < DustNum; i++)
		{
			global::UnityEngine.Object.Destroy(Dust_List[i].gameObject);
		}
	}
}
