using UnityEngine;

public class Player_Control : MonoBehaviour
{
	public enum AniState
	{
		Idle = 0,
		Run = 1,
		Sit = 2,
		Jump = 3,
		Spin = 4,
		Slide = 5,
		BackDash = 6,
		Damage = 7,
		Down = 8,
		Scene = 9,
	}

	public AniState State;
	public int facingRight;
	public bool onAttack;
	public bool grounded_Now;
	public int Jump_Num;
	public bool onRolling;
	public bool onJumpDrop;
	public float Jump_Pos_Y;
	public float Lock_Timer;
	public int Attack_Num;
	public bool onFlicker;
	public bool onHighJump;
	public bool onScrewAttack;
	public float Screw_Opacity;
	public float Speed_X;
	public float Speed_Y;
	public bool[] Lock_Lift;
	public float[] Pos_Lift;
	public Sprite Spr_Effect_Rolling;
	public GameObject[] Effect_Attack;
	public GameObject Effect_Lag;
	public GameObject Effect_BackDash;
	public GameObject Magic_1;
	public GameObject Magic_2;
	public GameObject Magic_3;
	public GameObject Magic_4;
	public GameObject Magic_5;
	public Transform groundedStart;
	public Transform groundedEnd;
	public Transform groundedEndDeath;
	public Transform frontStart;
	public Transform frontEnd;
	public Transform rearStart;
	public Transform rearEnd;
	public Transform posSitLock_1;
	public Transform posSitLock_2;
	public Transform posSitLock_1C;
	public Transform posSitLock_2C;
	public Transform checkFront;
	public Transform checkBack;
	public Transform checkBack2;
	public GameObject player_ani;
	public GameObject Ani_rolling;
	public GameObject Effect_rolling;
	public GameObject Glow_rolling;
	public GameObject Border_rolling;
	public Material Ani_Down_Mat;
	public SpriteRenderer Ani_Down_Foot;
	public SpriteRenderer Ani_Down_Mouth;
	public SpriteRenderer Ani_Eye;
	public BoxCollider2D Col_Top;
	public BoxCollider2D Col_Bot;
	public BoxCollider2D Col_Jump2;
}
