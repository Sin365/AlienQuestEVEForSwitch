public class LV_5_MonGen : global::UnityEngine.MonoBehaviour
{
	public enum Event_Type
	{
		None = 0,
		Top = 1,
		Bottom = 2
	}

	public int Num;

	public int Extra_Num;

	public int Index;

	public int Event_Map_Num;

	public int Event_Bonus_Num;

	public global::UnityEngine.Transform Distance_Bar;

	public global::UnityEngine.GameObject Mon_Objcet;

	public LV_5_MonGen.Event_Type M40_Type;

	private float distNum;

	private float distRnd;

	private int mon_Count;

	private global::UnityEngine.Vector3 pos;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		if (Num <= 0 || !(Mon_Objcet != null))
		{
			return;
		}
		if (Event_Map_Num > 0)
		{
			if (!GM.Check_EventMonster(Event_Map_Num))
			{
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Mon_Objcet, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
				gameObject.transform.parent = base.transform;
				gameObject.GetComponent<Mon_Index>().Index = Index;
				gameObject.GetComponent<Monster>().Event_Num = Event_Map_Num;
			}
		}
		else if (Num == 1 && Extra_Num == 0)
		{
			global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(Mon_Objcet, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
			gameObject2.transform.parent = base.transform;
			gameObject2.GetComponent<Mon_Index>().Index = Index;
			if (Event_Bonus_Num > 0)
			{
				gameObject2.GetComponent<Monster>().Event_Num = Event_Bonus_Num;
			}
			if (gameObject2.GetComponent<Monster>().Mon_Num == 40)
			{
				if (M40_Type == LV_5_MonGen.Event_Type.Top)
				{
					gameObject2.GetComponent<AI_Mon_40>().event_Type = AI_Mon_40.Event_Type.Top;
				}
				else if (M40_Type == LV_5_MonGen.Event_Type.Bottom)
				{
					gameObject2.GetComponent<AI_Mon_40>().event_Type = AI_Mon_40.Event_Type.Bottom;
				}
			}
		}
		else
		{
			if (Extra_Num > 0 && global::UnityEngine.Random.Range(0, 10) > 7)
			{
				Num += Extra_Num;
			}
			distNum = Distance_Bar.localScale.x * 2f / (float)(Num - 1);
			distRnd = distNum * 0.5f;
			for (int i = 0; i < Num; i++)
			{
				pos = new global::UnityEngine.Vector3(base.transform.position.x - Distance_Bar.localScale.x + distNum * (float)i + global::UnityEngine.Random.Range(0f - distRnd, distRnd), base.transform.position.y, 0f);
				global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(Mon_Objcet, pos, base.transform.rotation) as global::UnityEngine.GameObject;
				gameObject3.transform.parent = base.transform;
				gameObject3.GetComponent<Mon_Index>().Index = Index + mon_Count;
				mon_Count++;
			}
		}
	}
}
