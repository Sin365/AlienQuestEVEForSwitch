using UnityEngine;

public class LV_5_MonGen : MonoBehaviour
{
	public enum Event_Type
	{
		None = 0,
		Top = 1,
		Bottom = 2,
	}

	public int Num;
	public int Extra_Num;
	public int Index;
	public int Event_Map_Num;
	public int Event_Bonus_Num;
	public Transform Distance_Bar;
	public GameObject Mon_Objcet;
	public Event_Type M40_Type;
}
