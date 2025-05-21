using UnityEngine;

public class AI_Mon_31 : MonoBehaviour
{
	public enum Event_Type
	{
		None = 0,
		Top = 1,
		Bottom = 2,
	}

	public Event_Type event_Type;
	public GameObject Tr_Pos;
	public Transform Tr_1_Start;
	public Transform Tr_1_End;
	public Transform Tr_2_Start;
	public Transform Tr_2_End;
	public Transform Tr_3_Start;
	public Transform Tr_3_End;
	public Transform Tr_4_Start;
	public Transform Tr_4_End;
	public Transform Tr_Front_Start;
	public Transform Tr_Front_End;
	public Transform Tr_Front_End_H;
	public Transform Tr_Back_Start;
	public Transform Tr_Back_End;
	public Transform Tr_BackLow_End;
	public Transform Tr_Ground_R;
	public Transform Tr_Ground_L;
	public GameObject _Fire;
	public Transform pos_Fire;
	public GameObject[] H_Single;
}
