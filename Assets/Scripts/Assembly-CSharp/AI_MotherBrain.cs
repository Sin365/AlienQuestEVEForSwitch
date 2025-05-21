using UnityEngine;

public class AI_MotherBrain : MonoBehaviour
{
	public bool isDeath;
	public bool onEvent;
	public int Event_Num;
	public GameObject Explo;
	public Transform[] explo_Pos;
	public Transform pos_Text;
	public Transform pos_Text_P;
	public GameObject Blood_Obj;
	public SpriteRenderer Brain_Glow_1;
	public SpriteRenderer Brain_Glow_2;
	public SpriteRenderer Eye_Glow_1;
	public SpriteRenderer Eye_Glow_2;
	public SpriteRenderer Eye_Black;
	public SpriteRenderer[] SR_PipeGlow;
	public SpriteRenderer[] SR_GlowBox;
	public GameObject Arm_Obj;
	public GameObject _Fire;
	public GameObject _Laser;
	public GameObject _Gravity;
	public Transform pos_Fire;
	public Transform pos_Gravity;
	public Transform[] pos_Laser;
	public GameObject Clear_Weapon;
	public GameObject Clear_Skill;
	public GameObject Clear_Sample;
	public Transform pos_Weapon;
	public Transform pos_Skill;
	public Transform pos_Sample;
	public Transform pos_BrainGirl;
	public GameObject _SoundLaser1;
	public GameObject _SoundLaser2;
	public GameObject _SoundBeat;
	public GameObject _SoundElvStop;
	public GameObject Scar;
	public GameObject ChestBurster;
	public GameObject _Mon_30;
}
