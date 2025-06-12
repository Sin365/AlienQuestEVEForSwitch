using UnityEngine;

public class AI_Mon_Egg : global::UnityEngine.MonoBehaviour
{
	public enum AniState
	{
		Normal = 0,
		Open = 1
	}

	public AI_Mon_Egg.AniState State;

	public int HP = 100;

	public int HP_Max = 100;

	public float HP_Ratio = 1f;

	private bool isDeath;

	private float Life_Timer;

	private float distance;

	private float rnd_X;

	private bool Hit_Atk_1;

	private bool Hit_Atk_2;

	private bool Hit_Atk_3;

	private bool Hit_Atk_4;

	private bool Hit_Spin;

	private bool Hit_Rolling;

	private float Hit_Delay;

	private float Hit_Timer;

	private int Hit_Combo;

	private global::UnityEngine.GameObject[] Bomb_Object = new global::UnityEngine.GameObject[2];

	private float[] Bomb_Timer = new float[2];

	private float MagicFire_3_Timer;

	private float Gravity_Delay;

	private int Ready_Num;

	private float Ready_Timer;

	private float rnd_Limit;

	public global::UnityEngine.GameObject FaceHugger;

	public global::UnityEngine.GameObject Explo;

	public global::UnityEngine.Transform pos_Text;

	public global::UnityEngine.Transform explo_Pos_1;

	public global::UnityEngine.Transform explo_Pos_2;

	public global::UnityEngine.Transform explo_Pos_3;

	public global::UnityEngine.Transform pos_FaceHugger;

	public global::UnityEngine.GameObject Blood_Obj;

	public int Index;

	public global::UnityEngine.SkinnedMeshRenderer Egg_Body;

	public global::UnityEngine.SkinnedMeshRenderer Egg_Wing_L;

	public global::UnityEngine.SkinnedMeshRenderer Egg_Wing_R;

	public global::UnityEngine.SpriteRenderer Egg_Cover;

	private global::UnityEngine.Color color_Orig = new global::UnityEngine.Color(1f, 1f, 1f);

	private Sound_Control SC;
    Player_Control PC => GameManager.instance.PC;
    GameObject Player => GameManager.instance.gobj_Player;

    GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		SC = global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>();
		//Player = global::UnityEngine.GameObject.Find("Player");
		rnd_X = global::UnityEngine.Random.Range(0.3f, 0.8f);
		rnd_Limit = global::UnityEngine.Random.Range(0.9f, 1.2f);
		if (HP_Max < HP)
		{
			HP_Max = HP;
		}
		else if (HP < HP_Max)
		{
			HP = HP_Max;
		}
		Egg_Body.sortingOrder += 20 * Index;
		Egg_Wing_L.sortingOrder += 20 * Index;
		Egg_Wing_R.sortingOrder += 20 * Index;
		if (Egg_Cover != null)
		{
			Egg_Cover.sortingOrder += 20 * Index;
		}
		if (State.ToString() == "Open")
		{
			isDeath = true;
			GetComponent<global::UnityEngine.Animator>().SetTrigger("onDeath");
		}
	}

	private void Update()
	{
		if (isDeath)
		{
			return;
		}
		if (!GM.Paused)
		{
			if (GetComponent<global::UnityEngine.Animator>().speed < 1f)
			{
				GetComponent<global::UnityEngine.Animator>().speed = 1f;
			}
			Life_Timer += global::UnityEngine.Time.deltaTime;
			if (Hit_Delay > 0f)
			{
				Hit_Delay -= global::UnityEngine.Time.deltaTime;
			}
			if (Hit_Delay <= 0f && Hit_Atk_1)
			{
				Hit_Atk_1 = false;
			}
			if (Hit_Delay <= 0f && Hit_Atk_2)
			{
				Hit_Atk_2 = false;
			}
			if (Hit_Delay <= 0f && Hit_Atk_3)
			{
				Hit_Atk_3 = false;
			}
			if (Hit_Delay <= 0f && Hit_Atk_4)
			{
				Hit_Atk_4 = false;
			}
			if (Hit_Timer > 0f)
			{
				Hit_Timer -= global::UnityEngine.Time.deltaTime;
			}
			if (Hit_Timer <= 0f && Hit_Combo > 0)
			{
				Hit_Combo = 0;
			}
			if (Gravity_Delay > 0f)
			{
				Gravity_Delay -= global::UnityEngine.Time.deltaTime;
			}
			if (MagicFire_3_Timer > 0f)
			{
				MagicFire_3_Timer -= global::UnityEngine.Time.deltaTime;
			}
			distance = global::UnityEngine.Vector3.Distance(new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.8f, Player.transform.position.z), base.transform.position);
			if (distance < 5f)
			{
				Ready_Timer += global::UnityEngine.Time.deltaTime;
				if (Ready_Timer > rnd_Limit)
				{
					GetComponent<global::UnityEngine.Animator>().SetBool("isReady", true);
				}
			}
			else
			{
				Ready_Timer = 0f;
			}
			if (Bomb_Timer[0] > 0f)
			{
				Bomb_Timer[0] -= global::UnityEngine.Time.deltaTime;
			}
			else if (Bomb_Object[0] != null)
			{
				Bomb_Object[0] = null;
			}
			if (Bomb_Timer[1] > 0f)
			{
				Bomb_Timer[1] -= global::UnityEngine.Time.deltaTime;
			}
			else if (Bomb_Object[1] != null)
			{
				Bomb_Object[1] = null;
			}
		}
		else
		{
			GetComponent<global::UnityEngine.Animator>().speed = 0f;
		}
	}

	private void Set_Open()
	{
		GetComponent<global::UnityEngine.Animator>().SetBool("isOpened", true);
	}

	private void Set_Ready()
	{
		Ready_Num++;
		if (Ready_Num > 2)
		{
			GetComponent<global::UnityEngine.Animator>().SetBool("isOpened", true);
		}
	}

	private void End_Hit()
	{
		GetComponent<global::UnityEngine.Animator>().SetBool("isHit", false);
	}

	private void Set_Fire()
	{
		if (!isDeath)
		{
			isDeath = true;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(FaceHugger, pos_FaceHugger.position, pos_FaceHugger.rotation) as global::UnityEngine.GameObject;
			gameObject.GetComponent<AI_Mon_FaceHugger>().State = AI_Mon_FaceHugger.AniState.Attack;
			gameObject.GetComponent<AI_Mon_FaceHugger>().Index = Index;
			gameObject.transform.parent = base.transform.parent;
		}
	}

	private void Damage_Hit(bool onSkill, int skillNum, float ratio)
	{
		global::UnityEngine.Vector3 pos_Font = new global::UnityEngine.Vector3(pos_Text.position.x + (float)global::UnityEngine.Random.Range(-80, 80) * 0.01f, pos_Text.position.y + (float)global::UnityEngine.Random.Range(0, 150) * 0.01f, base.transform.position.z);
		int num = 0;
		num = ((!onSkill) ? GM.Get_AtkDamage(pos_Font, ratio) : GM.Get_MagicDamage(pos_Font, skillNum));
		HP -= num;
		if (HP <= 0)
		{
			HP_Ratio = 0f;
			if (!isDeath)
			{
				Death();
			}
		}
		else
		{
			HP_Ratio = (float)HP / (float)HP_Max;
			GetComponent<global::UnityEngine.Animator>().SetTrigger("isHit");
		}
	}

	private void Death()
	{
		isDeath = true;
		GetComponent<global::UnityEngine.Animator>().SetTrigger("onFire");
		if (GM.Bonus_Blood > 0)
		{
			Make_Blood();
		}
		GM.Monster_Kill(20);
		SC.Mon_Explo(base.transform.position);
		if (explo_Pos_1 != null)
		{
			Make_Explo(explo_Pos_1, false);
		}
		if (explo_Pos_2 != null)
		{
			Make_Explo(explo_Pos_2, false);
		}
		if (explo_Pos_3 != null)
		{
			Make_Explo(explo_Pos_3, false);
		}
	}

	private void Make_Explo(global::UnityEngine.Transform posObj, bool isFlip)
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Explo, posObj.position, posObj.rotation) as global::UnityEngine.GameObject;
		if (isFlip)
		{
			gameObject.transform.localScale = new global::UnityEngine.Vector3(posObj.localScale.x * -1f, posObj.localScale.y, 1f);
			gameObject.transform.rotation = global::UnityEngine.Quaternion.Euler(0f, 0f, 0f - gameObject.transform.rotation.eulerAngles.z);
		}
		else
		{
			gameObject.transform.localScale = posObj.localScale;
		}
	}

	private void Make_Blood()
	{
		for (int i = 0; i < GM.Bonus_Blood * 3; i++)
		{
			global::UnityEngine.Vector3 position = new global::UnityEngine.Vector3(base.transform.position.x + (float)global::UnityEngine.Random.Range(-200, 200) * 0.01f, base.transform.position.y + (float)global::UnityEngine.Random.Range(-200, 200) * 0.01f, 0f);
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Blood_Obj, position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
		}
	}

	private void OnTriggerStay2D(global::UnityEngine.Collider2D col)
	{
		if (GM.Paused || GM.onEvent || GM.onHscene || GM.onDown || GM.onGatePass || GM.onGameClear || isDeath)
		{
			return;
		}
		if (col.tag == "Magic_Explo")
		{
			if (HP > 0 && col.gameObject != Bomb_Object[0] && col.gameObject != Bomb_Object[1])
			{
				if (Bomb_Object[0] == null)
				{
					Bomb_Object[0] = col.gameObject;
					Bomb_Timer[0] = 0.18f;
				}
				else
				{
					Bomb_Object[1] = Bomb_Object[0];
					Bomb_Timer[1] = Bomb_Timer[0];
					Bomb_Object[0] = col.gameObject;
					Bomb_Timer[0] = 0.18f;
				}
				Damage_Hit(true, 1, 0f);
				SC.Mon_Hit_2(base.transform.position);
			}
		}
		else if (col.tag == "Magic_Fire")
		{
			switch (col.name.Substring(0, 11))
			{
			case "MagicFire_5":
				if (Gravity_Delay <= 0f)
				{
					Gravity_Delay = 0.1f;
					SC.Mon_Hit_1(base.transform.position);
					Damage_Hit(true, 4, 0f);
				}
				break;
			case "MagicFire_3":
				if (MagicFire_3_Timer <= 0f)
				{
					MagicFire_3_Timer = 0.1f;
					col.gameObject.SendMessage("Explo");
					Damage_Hit(true, 2, 0f);
				}
				break;
			case "MagicFire_1":
				if (HP > 0)
				{
					float x = col.transform.localScale.x;
					col.gameObject.SendMessage("Make_Explo");
					Damage_Hit(true, 0, 0f);
					SC.Mon_Hit_1(base.transform.position);
				}
				break;
			}
		}
		else
		{
			if (!(col.tag == "Col_PC_Atk"))
			{
				return;
			}
			if (col.name == "Col_Attack_0_1" || col.name == "Col_Attack_1_1" || col.name == "Col_Attack_2_1" || col.name == "Col_Attack_4_1" || col.name == "Col_Attack_5_1")
			{
				if (!Hit_Atk_1)
				{
					Hit_Atk_2 = false;
					Hit_Atk_1 = true;
					Hit_Delay = 0.3f;
					SC.Mon_Hit_1(base.transform.position);
					Damage_Hit(false, 0, 1f);
				}
			}
			else if (col.name == "Col_Attack_0_2" || col.name == "Col_Attack_1_2" || col.name == "Col_Attack_2_2" || col.name == "Col_Attack_4_2" || col.name == "Col_Attack_5_2")
			{
				if (!Hit_Atk_2)
				{
					Hit_Atk_1 = false;
					Hit_Atk_2 = true;
					Hit_Delay = 0.3f;
					SC.Mon_Hit_1(base.transform.position);
					Damage_Hit(false, 0, 1f);
				}
			}
			else if (col.name == "Col_Attack_3" || col.name == "Col_Attack_5_3")
			{
				if (!Hit_Atk_3)
				{
					Hit_Atk_1 = false;
					Hit_Atk_2 = false;
					Hit_Atk_3 = true;
					Hit_Atk_4 = false;
					Hit_Delay = 0.3f;
					SC.Mon_Hit_1(base.transform.position);
					Damage_Hit(false, 0, 1.2f);
				}
			}
			else if (col.name == "Col_Attack_4" || col.name == "Col_Attack_5_4")
			{
				if (!Hit_Atk_4)
				{
					Hit_Atk_1 = false;
					Hit_Atk_2 = false;
					Hit_Atk_3 = false;
					Hit_Atk_4 = true;
					Hit_Delay = 0.3f;
					SC.Mon_Hit_1(base.transform.position);
					Damage_Hit(false, 0, 1.5f);
				}
			}
			else if (col.name == "Col_Spin")
			{
				if (Hit_Delay <= 0f)
				{
					Hit_Delay = 0.1f;
					SC.Mon_Hit_1(base.transform.position);
					Damage_Hit(false, 0, 1.2f);
				}
			}
			else if (col.name == "Col_Rolling")
			{
				if (Hit_Delay <= 0f)
				{
					if (global::UnityEngine.GameObject.Find("Border_Rolling").GetComponent<global::UnityEngine.SpriteRenderer>().color.a > 0.05f)
					{
						Hit_Delay = 0.1f;
						Damage_Hit(false, 0, 1.5f);
					}
					else
					{
						Hit_Delay = 0.24f;
						Damage_Hit(false, 0, 1f);
					}
					SC.Mon_Hit_1(base.transform.position);
				}
			}
			else if (!Hit_Atk_1)
			{
				Hit_Atk_1 = true;
				Hit_Delay = 0.3f;
				SC.Mon_Hit_1(base.transform.position);
				Damage_Hit(false, 0, 1f);
			}
		}
	}

	private void Set_Color()
	{
		float num = global::UnityEngine.Random.Range(0.8f, 1f);
		Egg_Body.material.color = new global::UnityEngine.Color(num, num, num, 1f);
		Egg_Wing_L.material.color = Egg_Body.material.color;
		Egg_Wing_R.material.color = Egg_Body.material.color;
		if (Egg_Cover != null)
		{
			Egg_Cover.color = Egg_Body.material.color;
		}
	}

	private void Set_Color_Death()
	{
	}
}
