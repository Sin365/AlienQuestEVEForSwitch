public class Col_MotherArm : global::UnityEngine.MonoBehaviour
{
	public AI_MotherArm MotherArm;

	private Sound_Control SC;

    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		SC = global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>();
	}

	private void Update()
	{
		if (GM.Paused)
		{
		}
	}

	private void OnTriggerStay2D(global::UnityEngine.Collider2D col)
	{
		if (!(MotherArm != null) || GM.Paused || GM.onGatePass || GM.onGameClear || MotherArm.isDeath)
		{
			return;
		}
		if (!GM.onShield && col.name == "Ani" && !(global::UnityEngine.GameObject.Find("Border_Rolling").GetComponent<global::UnityEngine.SpriteRenderer>().color.a > 0.05f) && !GM.GameOver && MotherArm.PC_Col_Delay <= 0f && MotherArm.PC_Body_Delay <= 0f)
		{
			MotherArm.PC_Col_Delay = 1f;
			MotherArm.PC_Body_Delay = 1f;
			if (base.transform.position.x > col.transform.parent.position.x)
			{
				GM.Damage(MotherArm.Damage, 0f - MotherArm.DmgForce, false, 14);
			}
			else
			{
				GM.Damage(MotherArm.Damage, MotherArm.DmgForce, false, 14);
			}
		}
		if (col.tag == "Magic_Explo")
		{
			if (MotherArm.HP > 0 && col.gameObject != MotherArm.Bomb_Object[0] && col.gameObject != MotherArm.Bomb_Object[1])
			{
				if (MotherArm.Bomb_Object[0] == null)
				{
					MotherArm.Bomb_Object[0] = col.gameObject;
					MotherArm.Bomb_Timer[0] = 0.18f;
				}
				else
				{
					MotherArm.Bomb_Object[1] = MotherArm.Bomb_Object[0];
					MotherArm.Bomb_Timer[1] = MotherArm.Bomb_Timer[0];
					MotherArm.Bomb_Object[0] = col.gameObject;
					MotherArm.Bomb_Timer[0] = 0.18f;
				}
				MotherArm.Damage_Hit(true, 1, 0f);
				SC.Mon_Hit_2(base.transform.position);
				MotherArm.PC_Col_Delay = 0.5f;
			}
		}
		else if (col.tag == "Magic_Smog")
		{
			if (!MotherArm.onPoisonSkill)
			{
				MotherArm.onPoisonSkill = true;
				MotherArm.Poison_Skill_Timer = 1f;
				if (!MotherArm.onPoisonWeapon)
				{
					MotherArm.GetComponent<Mon_Index>().On_MagicPoison();
				}
			}
			else
			{
				MotherArm.Poison_Skill_Timer = 1.25f;
			}
		}
		else if (col.tag == "Magic_Shield")
		{
			if (GM.onShield)
			{
				GM.Break_Shield();
			}
		}
		else if (col.tag == "Magic_Fire")
		{
			switch (col.name.Substring(0, 11))
			{
			case "MagicFire_5":
				if (MotherArm.Gravity_Delay <= 0f)
				{
					MotherArm.Gravity_Delay = 0.1f;
					SC.Mon_Hit_1(base.transform.position);
					MotherArm.PC_Col_Delay = 0.5f;
					MotherArm.Damage_Hit(true, 4, 0f);
				}
				break;
			case "MagicFire_3":
				col.gameObject.SendMessage("Explo");
				if (!MotherArm.onPoisonSkill)
				{
					MotherArm.onPoisonSkill = true;
					MotherArm.Poison_Skill_Timer = 3f;
					if (!MotherArm.onPoisonWeapon)
					{
						MotherArm.GetComponent<Mon_Index>().On_MagicPoison();
					}
				}
				MotherArm.Damage_Hit(true, 2, 0f);
				break;
			case "MagicFire_1":
				if (MotherArm.HP > 0)
				{
					MotherArm.Damage_Hit(true, 0, 0f);
					MotherArm.PC_Col_Delay = 0.5f;
					col.gameObject.SendMessage("Make_Explo");
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
			if (GM.Weapon_Num == 3)
			{
				if (!MotherArm.onPoisonSkill && !MotherArm.onPoisonWeapon)
				{
					MotherArm.GetComponent<Mon_Index>().On_MagicPoison();
				}
				MotherArm.onPoisonWeapon = true;
				MotherArm.Poison_Weapon_Timer = 3f;
			}
			else if (GM.Weapon_Num == 4)
			{
				if (!MotherArm.onPoisonSkill && !MotherArm.onPoisonWeapon && !MotherArm.onSlow)
				{
					MotherArm.GetComponent<Mon_Index>().On_MagicSlow();
				}
				MotherArm.onSlow = true;
				MotherArm.Slow_Timer = 3f;
			}
			if (col.name == "Col_Attack_0_1" || col.name == "Col_Attack_1_1" || col.name == "Col_Attack_2_1" || col.name == "Col_Attack_4_1" || col.name == "Col_Attack_5_1")
			{
				if (!MotherArm.Hit_Atk_1)
				{
					MotherArm.Hit_Atk_2 = false;
					MotherArm.Hit_Atk_1 = true;
					MotherArm.Hit_Delay = 0.3f;
					SC.Mon_Hit_1(base.transform.position);
					MotherArm.Damage_Hit(false, 0, 1f);
				}
			}
			else if (col.name == "Col_Attack_0_2" || col.name == "Col_Attack_1_2" || col.name == "Col_Attack_2_2" || col.name == "Col_Attack_4_2" || col.name == "Col_Attack_5_2")
			{
				if (!MotherArm.Hit_Atk_2)
				{
					MotherArm.Hit_Atk_1 = false;
					MotherArm.Hit_Atk_2 = true;
					MotherArm.Hit_Delay = 0.3f;
					SC.Mon_Hit_1(base.transform.position);
					MotherArm.Damage_Hit(false, 0, 1f);
				}
			}
			else if (col.name == "Col_Attack_3" || col.name == "Col_Attack_5_3")
			{
				if (!MotherArm.Hit_Atk_3)
				{
					MotherArm.Hit_Atk_1 = false;
					MotherArm.Hit_Atk_2 = false;
					MotherArm.Hit_Atk_3 = true;
					MotherArm.Hit_Atk_4 = false;
					MotherArm.Hit_Delay = 0.3f;
					SC.Mon_Hit_1(base.transform.position);
					MotherArm.Damage_Hit(false, 0, 1.2f);
				}
			}
			else if (col.name == "Col_Attack_4" || col.name == "Col_Attack_5_4")
			{
				if (!MotherArm.Hit_Atk_4)
				{
					MotherArm.Hit_Atk_1 = false;
					MotherArm.Hit_Atk_2 = false;
					MotherArm.Hit_Atk_3 = false;
					MotherArm.Hit_Atk_4 = true;
					MotherArm.Hit_Delay = 0.3f;
					SC.Mon_Hit_1(base.transform.position);
					MotherArm.Damage_Hit(false, 0, 1.5f);
				}
			}
			else if (col.name == "Col_Spin")
			{
				if (MotherArm.Hit_Delay <= 0f)
				{
					MotherArm.Hit_Delay = 0.1f;
					MotherArm.PC_Col_Delay = 0.5f;
					SC.Mon_Hit_1(base.transform.position);
					MotherArm.Damage_Hit(false, 0, 1.2f);
				}
			}
			else if (col.name == "Col_Rolling")
			{
				if (MotherArm.Hit_Delay <= 0f)
				{
					if (global::UnityEngine.GameObject.Find("Border_Rolling").GetComponent<global::UnityEngine.SpriteRenderer>().color.a > 0.05f)
					{
						MotherArm.Hit_Delay = 0.1f;
						MotherArm.PC_Col_Delay = 0.5f;
						MotherArm.Damage_Hit(false, 0, 1.5f);
					}
					else
					{
						MotherArm.Hit_Delay = 0.24f;
						MotherArm.Damage_Hit(false, 0, 1f);
					}
					SC.Mon_Hit_1(base.transform.position);
				}
			}
			else if (!MotherArm.Hit_Atk_1)
			{
				MotherArm.Hit_Atk_1 = true;
				MotherArm.Hit_Delay = 0.3f;
				SC.Mon_Hit_1(base.transform.position);
				MotherArm.Damage_Hit(false, 0, 1f);
			}
		}
	}
}
