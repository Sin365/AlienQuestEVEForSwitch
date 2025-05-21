using UnityEngine;

public class AI_Boss_3Gun : MonoBehaviour
{
	public int Type;
	public GameObject Clear_Item;
	public Transform Body;
	public Transform Center;
	public Transform Top;
	public Transform Bot;
	public GameObject _Fire;
	public GameObject _Laser;
	public GameObject _Shield;
	public Transform pos_FireCenter;
	public Transform pos_FireTop;
	public Transform pos_FireBot;
	public SpriteRenderer Glow_Center_V;
	public SpriteRenderer Glow_Center_C;
	public SpriteRenderer Glow_Center_B;
	public SpriteRenderer Glow_Top_V;
	public SpriteRenderer Glow_Top_C;
	public SpriteRenderer Glow_Top_B;
	public SpriteRenderer Glow_Bot_V;
	public SpriteRenderer Glow_Bot_C;
	public SpriteRenderer Glow_Bot_B;
	public SpriteRenderer Guide_Laser;
	public GameObject Explo;
	public GameObject Sound_Gun_Fire;
	public GameObject Sound_Explo;
}
