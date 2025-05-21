public class LV_2_MonGenEgg : global::UnityEngine.MonoBehaviour
{
	public int Num;

	public int Index;

	public global::UnityEngine.Transform Distance_Bar;

	public global::UnityEngine.GameObject[] _Mon_List;

	private float distNum;

	private float distRnd;

	private int Egg_Count;

	private int rnd;

	private global::UnityEngine.Vector3 pos;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		if (GM.Get_Event(12))
		{
			if (Num > 0 && _Mon_List.Length > 0)
			{
				if (global::UnityEngine.Random.Range(0, 10) > 7)
				{
					Num++;
				}
				distNum = Distance_Bar.localScale.x * 2f / (float)(Num - 1);
				distRnd = distNum * 0.5f;
				for (int i = 0; i < Num; i++)
				{
					pos = new global::UnityEngine.Vector3(base.transform.position.x - Distance_Bar.localScale.x + distNum * (float)i + global::UnityEngine.Random.Range(0f - distRnd, distRnd), base.transform.position.y, 0f);
					rnd = global::UnityEngine.Random.Range(0, _Mon_List.Length);
					global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Mon_List[rnd], pos, base.transform.rotation) as global::UnityEngine.GameObject;
					gameObject.transform.parent = base.transform;
					gameObject.GetComponent<AI_Mon_Egg>().Index = Index + Egg_Count;
					Egg_Count++;
				}
			}
		}
		else
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
