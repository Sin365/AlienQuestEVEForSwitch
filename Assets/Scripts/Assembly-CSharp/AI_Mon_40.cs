using UnityEngine;

public class AI_Mon_40 : MonoBehaviour
{
	public enum Event_Type
	{
		None = 0,
		Top = 1,
		Bottom = 2,
	}

	public Event_Type event_Type;
	public Transform pos_Fire;
	public GameObject _Fire;
	public Transform Mon_Ctrl;
	public Transform pos_Dash;
	public GameObject H_Single;
	public Transform Tr_Front_Start;
	public Transform Tr_Front_End;
	public Transform Tr_Back_Start;
	public Transform Tr_Back_End;
}
