public class LV_5_145_Gen : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject _Mon_40;

	public global::UnityEngine.Transform Pos_L;

	public global::UnityEngine.Transform Pos_R;

    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		if (GM.Get_Event(3) && GM.Get_Event(4) && GM.Check_EventMonster(8))
		{
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Mon_40, Pos_L.position, base.transform.rotation) as global::UnityEngine.GameObject;
			gameObject.transform.parent = base.transform;
			gameObject.GetComponent<Mon_Index>().Index = 0;
			gameObject.GetComponent<AI_Mon_40>().event_Type = AI_Mon_40.Event_Type.Bottom;
			global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(_Mon_40, Pos_R.position, base.transform.rotation) as global::UnityEngine.GameObject;
			gameObject2.transform.parent = base.transform;
			gameObject2.GetComponent<Mon_Index>().Index = 1;
			gameObject2.GetComponent<AI_Mon_40>().event_Type = AI_Mon_40.Event_Type.Bottom;
		}
	}
}
