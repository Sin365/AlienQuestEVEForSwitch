using UnityEngine;

public class AI_Mon_FaceHugger : MonoBehaviour
{
	public enum AniState
	{
		Idle = 0,
		Move = 1,
		Attack = 2,
	}

	public AniState State;
	public int HP;
	public int HP_Max;
	public float HP_Ratio;
	public int Damage;
	public float DmgForce;
	public GameObject Ctrl_1;
	public SpriteRenderer HP_Bar_BG;
	public SpriteRenderer HP_Bar;
	public GameObject Explo;
	public GameObject Pos_Root;
	public GameObject pos_explo_1;
	public GameObject pos_explo_2;
	public GameObject pos_explo_3;
	public Transform pos_Text;
	public Transform pos_Text_P;
	public Transform pos_L;
	public Transform pos_R;
	public Transform pos_Bottom_L;
	public Transform pos_Bottom_R;
	public Transform pos_Stand_1;
	public Transform pos_Stand_2;
	public Transform pos_Down;
	public Transform pos_Front;
	public GameObject Blood_Obj;
	public GameObject _Icon;
	public Sprite Icon_Spr;
	public int Index;
	public GameObject[] Object_Body;
	public GameObject[] Object_Fly;
}
