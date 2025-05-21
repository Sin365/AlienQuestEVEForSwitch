using UnityEngine;
using UnityEngine.UI;

public class TeleMenu_Control : MonoBehaviour
{
	public int Teleport_Num;
	public float Dist;
	public float dist_Timer;
	public Image Img_Box_Now;
	public Image Img_Box_Target;
	public SpriteRenderer save_BG_1;
	public SpriteRenderer save_BG_2;
	public Text text_Title;
	public Text text_Center;
	public Text text_Center_Shadow;
	public Image[] Img_Num;
	public Image[] Img_Icon;
	public SpriteRenderer Sel_Cursor;
	public GameObject teleport_Block;
}
