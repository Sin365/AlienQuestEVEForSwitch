public class LV_3_XenoAmbush : global::UnityEngine.MonoBehaviour
{
	public int Num;

	public int Index;

	public global::UnityEngine.Transform Distance_Bar;

	public global::UnityEngine.GameObject[] _XenoList;

	private float distNum;

	private float distRnd;

	private int Mon_Count;

	private int rnd;

	private global::UnityEngine.Vector3 pos;

    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		if (Num <= 0 || _XenoList.Length <= 0)
		{
			return;
		}
		distNum = Distance_Bar.localScale.x * 2f / (float)(Num - 1);
		distRnd = distNum * 0.5f;
		for (int i = 0; i < Num; i++)
		{
			if (Num > 1)
			{
				pos = new global::UnityEngine.Vector3(base.transform.position.x + 1.5f - Distance_Bar.localScale.x + distNum * (float)i, base.transform.position.y + 2.62f, 0f);
			}
			else
			{
				pos = new global::UnityEngine.Vector3(base.transform.position.x + 1.5f, base.transform.position.y + 2.62f, 0f);
			}
			if (_XenoList.Length > 1)
			{
				rnd = global::UnityEngine.Random.Range(0, 2);
			}
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_XenoList[rnd], pos, base.transform.rotation) as global::UnityEngine.GameObject;
			gameObject.transform.parent = base.transform;
			gameObject.GetComponent<Mon_Index>().Index = Index + Mon_Count;
			gameObject.GetComponent<Monster>().onEvent = true;
			if (gameObject.GetComponent<AI_Mon_31>() != null)
			{
				gameObject.GetComponent<AI_Mon_31>().event_Type = AI_Mon_31.Event_Type.Top;
			}
			else if (gameObject.GetComponent<AI_Mon_33>() != null)
			{
				gameObject.GetComponent<AI_Mon_33>().event_Type = AI_Mon_33.Event_Type.Top;
			}
			else if (gameObject.GetComponent<AI_Mon_Xeno>() != null)
			{
				gameObject.GetComponent<AI_Mon_Xeno>().event_Type = AI_Mon_Xeno.Event_Type.Top;
			}
			if (gameObject.GetComponent<AI_Mon_Xeno>() == null)
			{
				gameObject.transform.position = new global::UnityEngine.Vector3(gameObject.transform.position.x, base.transform.position.y + 2.52f, 0f);
			}
			Mon_Count++;
		}
	}
}
