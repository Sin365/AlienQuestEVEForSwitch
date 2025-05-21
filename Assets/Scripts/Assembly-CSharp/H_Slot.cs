using UnityEngine;
using UnityEngine.UI;

public class H_Slot : MonoBehaviour
{
	public int Slot_Num;
	public H_Manager H_manager;
	public GameObject H_Object;
	public Image Icon_Play;
	public Image Icon_Stop;
	public Image Icon_Loop;
	public Image Icon_Loop_Dir;
	public RectTransform Rot_Loop;
	public RectTransform SelBorder;
	public Text[] Txt_State;
	public RectTransform[] Box_State;
	public Text Txt_Speed;
	public Image Speed_Bar;
	public RectTransform Cum_Box;
	public Image Cum_Box_Tail;
	public Text Txt_Cum;
	public GameObject H_Dummy;
}
