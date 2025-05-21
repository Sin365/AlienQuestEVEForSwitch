using UnityEngine;

public class AI_Mon_BrainGirl : MonoBehaviour
{
	public enum Event_Type
	{
		None = 0,
		MotherBrain = 1,
	}

	public Event_Type event_Type;
	public GameObject _Fire;
	public GameObject H_Single;
	public GameObject H_Dual;
	public PolygonCollider2D Col_Body;
}
