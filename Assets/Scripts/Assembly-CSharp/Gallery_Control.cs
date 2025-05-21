using UnityEngine;

public class Gallery_Control : MonoBehaviour
{
	public int State;
	public int Sel_Index;
	public int[] Slot_isOpen;
	public int Slot_Sum;
	public bool onMouseDrag;
	public RectTransform Ani_List;
	public H_Slot[] Slot;
	public RectTransform Cursor;
	public RectTransform Cursor_BG;
	public RectTransform SelBorder_Main;
	public RectTransform SelBorder_Option;
	public RectTransform SelBorder_Ani;
	public GameObject Cursor_Object;
	public RectTransform GameOver_Menu;
	public RectTransform SelBorder_GameOver;
	public GameObject[] Gallery_BG;
}
