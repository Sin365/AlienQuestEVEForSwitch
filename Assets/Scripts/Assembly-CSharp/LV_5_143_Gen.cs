public class LV_5_143_Gen : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject _Mon_30;

	public global::UnityEngine.GameObject _Mon_38;

	public global::UnityEngine.GameObject _Mon_40;

	public global::UnityEngine.GameObject _Mon_41;

	public global::UnityEngine.Transform[] Pos_30_List;

	public global::UnityEngine.Transform[] Pos_38_List;

	public global::UnityEngine.Transform[] Pos_40_List;

	public global::UnityEngine.Transform Pos_41;

	private int Mon_40_Num;

	private float distance;

    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		if ((GM.Get_Event(3) || GM.Get_Event(15) || GM.EventState == 200) && Pos_40_List.Length > 0)
		{
			for (int i = 0; i < Pos_40_List.Length; i++)
			{
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Mon_40, Pos_40_List[i].position, base.transform.rotation) as global::UnityEngine.GameObject;
				gameObject.transform.parent = base.transform;
				gameObject.GetComponent<Mon_Index>().Index = 10 + i;
				Mon_40_Num++;
				gameObject.GetComponent<AI_Mon_40>().event_Type = AI_Mon_40.Event_Type.Bottom;
			}
		}
		if (Pos_30_List.Length > 0)
		{
			for (int j = 0; j < Pos_30_List.Length; j++)
			{
				global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(_Mon_30, Pos_30_List[j].position, base.transform.rotation) as global::UnityEngine.GameObject;
				gameObject2.transform.parent = base.transform;
				gameObject2.GetComponent<Mon_Index>().Index = 5 + j;
			}
		}
		if (Pos_38_List.Length > 0)
		{
			for (int k = 0; k < Pos_38_List.Length; k++)
			{
				global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(_Mon_38, Pos_38_List[k].position, base.transform.rotation) as global::UnityEngine.GameObject;
				gameObject3.transform.parent = base.transform;
				gameObject3.GetComponent<Mon_Index>().Index = k;
			}
		}
		if (Pos_41 != null)
		{
			global::UnityEngine.GameObject gameObject4 = global::UnityEngine.Object.Instantiate(_Mon_41, Pos_41.position, base.transform.rotation) as global::UnityEngine.GameObject;
			gameObject4.transform.parent = base.transform;
			gameObject4.GetComponent<Mon_Index>().Index = 0;
		}
	}
}
