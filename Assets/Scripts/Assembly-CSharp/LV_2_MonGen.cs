public class LV_2_MonGen : global::UnityEngine.MonoBehaviour
{
	public int Num;

	public int Extra_Num;

	public int Index;

	public global::UnityEngine.Transform Distance_Bar;

	public global::UnityEngine.GameObject Mon_Objcet;

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
		if (Num == 1 && Extra_Num == 0)
		{
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Mon_Objcet, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
			gameObject.transform.parent = base.transform;
			gameObject.GetComponent<Mon_Index>().Index = Index;
			return;
		}
		if (Extra_Num > 0 && global::UnityEngine.Random.Range(0, 10) > 7)
		{
			Num += Extra_Num;
		}
		distNum = Distance_Bar.localScale.x * 2f / (float)(Num - 1);
		distRnd = distNum * 0.5f;
		for (int i = 0; i < Num; i++)
		{
			pos = new global::UnityEngine.Vector3(base.transform.position.x - Distance_Bar.localScale.x + distNum * (float)i + global::UnityEngine.Random.Range(0f - distRnd, distRnd), base.transform.position.y, 0f);
			global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(Mon_Objcet, pos, base.transform.rotation) as global::UnityEngine.GameObject;
			gameObject2.transform.parent = base.transform;
			gameObject2.GetComponent<Mon_Index>().Index = Index + mon_Count;
			mon_Count++;
		}
	}
}
