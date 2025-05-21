using UnityEngine;

public class Gate_Control : MonoBehaviour
{
	public int Card_Num;
	public int targetRoom_Num;
	public int targetPos_Num;
	public SpriteRenderer Gate_Up;
	public SpriteRenderer Gate_Down;
	public SpriteRenderer Glow_Top;
	public SpriteRenderer Glow_Bot;
	public SpriteRenderer Body_L;
	public SpriteRenderer Body_R;
	public SpriteRenderer Body_Top_L;
	public SpriteRenderer Body_Top_R;
	public SpriteRenderer Body_Bot_L;
	public SpriteRenderer Body_Bot_R;
	public Transform pos_Top;
	public Transform pos_Bot;
	public BoxCollider2D ColBox;
	public GameObject Info_Card;
	public GameObject Keep_Monster;
	public GameObject Boss_Lock;
}
