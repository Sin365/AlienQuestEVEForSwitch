using UnityEngine;

public class AI_MotherArm : MonoBehaviour
{
	public AI_MotherBrain MotherBrain;
	public int HP;
	public int HP_Max;
	public int Damage;
	public float DmgForce;
	public bool isDeath;
	public bool onEvent;
	public bool Hit_Atk_1;
	public bool Hit_Atk_2;
	public bool Hit_Atk_3;
	public bool Hit_Atk_4;
	public bool Hit_Spin;
	public bool Hit_Rolling;
	public float Hit_Delay;
	public float Hit_Timer;
	public int Hit_Combo;
	public bool onPoisonSkill;
	public bool onPoisonWeapon;
	public bool onSlow;
	public float Gravity_Delay;
	public float Poison_Skill_Timer;
	public float Poison_Weapon_Timer;
	public float Toxic_Timer;
	public float Slow_Timer;
	public GameObject[] Bomb_Object;
	public float[] Bomb_Timer;
	public float PC_Col_Delay;
	public float PC_Body_Delay;
	public GameObject Ctrl_1;
	public Transform pos_Text;
	public Transform pos_Text_P;
	public GameObject Blood_Obj;
	public GameObject sound_Awake;
	public GameObject sound_Start;
	public SpriteRenderer[] SR_Event;
}
