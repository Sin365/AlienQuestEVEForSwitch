public class LV_1_MonGenerator : global::UnityEngine.MonoBehaviour
{
	public int Num;

	public int Num_Max;

	public int Index;

	public global::UnityEngine.Transform Distance_Bar;

	public global::UnityEngine.GameObject[] _Mon_List;

	private int mon_Count;

	private int mon_Max;

	private int rnd;

	private global::UnityEngine.Vector3 pos;

	GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		if (Num_Max <= 0 || _Mon_List.Length <= 0)
		{
			return;
		}
		if (Num > 0)
		{
			mon_Max = global::UnityEngine.Random.Range(Num, Num_Max);
		}
		else
		{
			mon_Max = global::UnityEngine.Random.Range(-2, 2);
			if (mon_Max < 0)
			{
				mon_Max = 0;
			}
		}
		for (int i = 0; i < mon_Max; i++)
		{
			pos = new global::UnityEngine.Vector3(base.transform.position.x + global::UnityEngine.Random.Range(0f - Distance_Bar.localScale.x, Distance_Bar.localScale.x), base.transform.position.y, 0f);
			rnd = global::UnityEngine.Random.Range(0, _Mon_List.Length);
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Mon_List[rnd], pos, base.transform.rotation) as global::UnityEngine.GameObject;
			gameObject.transform.parent = base.transform;
			gameObject.GetComponent<Mon_Index>().Index = Index + mon_Count;
			mon_Count++;
		}
	}
}
