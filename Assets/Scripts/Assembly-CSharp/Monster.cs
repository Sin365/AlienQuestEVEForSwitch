using UnityEngine;

public class Monster : MonoBehaviour
{
	public int Mon_Num;
	public bool onEvent;
	public float Event_Timer;
	public int Event_Num;
	public int HP;
	public int HP_Max;
	public float HP_Ratio;
	public int Damage;
	public float DmgForce;
	public bool isInvincible;
	public bool isLockHit;
	public bool isPass;
	public bool onCrouch;
	public int Gameover_Num;
	public float Move_Speed;
	public int MagicFire_1_Num;
	public int MagicFire_5_Num;
	public GameObject Ctrl_1;
	public GameObject Ctrl_2;
	public GameObject Ctrl_3;
	public SpriteRenderer HP_Bar_BG;
	public SpriteRenderer HP_Bar;
	public GameObject Explo;
	public GameObject explo_Pos_Root;
	public GameObject explo_Pos_1;
	public GameObject explo_Pos_2;
	public GameObject explo_Pos_3;
	public GameObject explo_Pos_4;
	public GameObject explo_Pos_5;
	public GameObject explo_Pos_6;
	public Transform pos_Text;
	public Transform pos_Text_P;
	public GameObject Blood_Obj;
	public GameObject _Icon;
	public Sprite Icon_Spr;
	public GameObject _Item_Potion_HP;
	public GameObject _Item_Potion_MP;
}
