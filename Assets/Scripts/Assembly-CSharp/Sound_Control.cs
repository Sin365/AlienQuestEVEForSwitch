public class Sound_Control : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject Sound_Magic_1;

	public global::UnityEngine.GameObject Sound_Magic_2;

	public global::UnityEngine.GameObject Sound_Magic_3_Explo_1;

	public global::UnityEngine.GameObject Sound_Magic_3_Explo_2;

	public global::UnityEngine.GameObject Sound_Magic_3_Explo_3;

	public global::UnityEngine.GameObject Sound_Hit_1;

	public global::UnityEngine.GameObject Sound_Hit_2;

	public global::UnityEngine.GameObject Sound_Hit_3;

	public global::UnityEngine.GameObject Sound_Hit_4;

	public global::UnityEngine.GameObject Sound_Hit_5;

	public global::UnityEngine.GameObject Sound_Hit_6;

	public global::UnityEngine.GameObject Sound_Hit_11;

	public global::UnityEngine.GameObject Sound_Hit_12;

	public global::UnityEngine.GameObject Sound_Hit_Explo;

	public global::UnityEngine.GameObject Sound_Footstep_Mon_1;

	public global::UnityEngine.GameObject Sound_Footstep_Mon_2;

	public global::UnityEngine.GameObject Sound_MonAtk_1;

	public global::UnityEngine.GameObject Sound_MonAtk_2;

	public global::UnityEngine.GameObject Sound_MonAtk_3;

	public global::UnityEngine.GameObject Sound_MonAtk_4;

	public global::UnityEngine.GameObject Sound_MonAtk_5;

	public global::UnityEngine.GameObject Sound_MonAtk_6;

	public global::UnityEngine.GameObject Sound_MonAtk_7;

	public global::UnityEngine.GameObject Sound_Elec;

	public global::UnityEngine.GameObject Sound_Plasma;

	public global::UnityEngine.GameObject Mon_10_Growling_1;

	public global::UnityEngine.GameObject Mon_10_Growling_2;

	public global::UnityEngine.GameObject Mon_10_Growling_3;

	public global::UnityEngine.GameObject Mon_10_Growling_4;

	public global::UnityEngine.GameObject Alien_Growling_1;

	public global::UnityEngine.GameObject Alien_Growling_2;

	public global::UnityEngine.GameObject Alien_Growling_3;

	public global::UnityEngine.GameObject Alien_Growling_4;

	public global::UnityEngine.GameObject Alien_Growling_5;

	public global::UnityEngine.GameObject Alien_Dash_1;

	public global::UnityEngine.GameObject Alien_Dash_2;

	public global::UnityEngine.GameObject Alien_Dash_3;

	public global::UnityEngine.GameObject Alien_Dash_4;

	public global::UnityEngine.GameObject Alien_Dmg_1;

	public global::UnityEngine.GameObject Alien_Dmg_2;

	public global::UnityEngine.GameObject Alien_Death_1;

	public global::UnityEngine.GameObject Alien_Death_2;

	public global::UnityEngine.GameObject Alien_Death_3;

	public global::UnityEngine.GameObject Alien_Death_4;

	public global::UnityEngine.GameObject Alien_Death_5;

	public global::UnityEngine.GameObject Mon_8_Dmg;

	public global::UnityEngine.GameObject Mon_7_Atk;

	public global::UnityEngine.GameObject Mon_7_Dmg;

	public global::UnityEngine.GameObject Mon_6_Dmg;

	public global::UnityEngine.GameObject Mon_5_Dmg;

	public global::UnityEngine.GameObject Mon_4_Dmg;

	public global::UnityEngine.GameObject Mon_3_Dmg;

	public global::UnityEngine.GameObject Mon_2_Dmg;

	public global::UnityEngine.GameObject Mon_1_Dmg;

	public global::UnityEngine.GameObject Mob_Dmg;

	public global::UnityEngine.GameObject Mon_10_Atk;

	public global::UnityEngine.GameObject Mon_10_Dmg1;

	public global::UnityEngine.GameObject Mon_10_Dmg2;

	public global::UnityEngine.GameObject Mon_10_Dmg3;

	public global::UnityEngine.GameObject[] s_List;

	private float hit_Timer;

	private float Magic_Timer_1;

	private float Magic_Timer_3;

	private float Magic_Timer_5;

	private float Mon_Explo_Timer;

	private int Mon_FootStep_Num;

	private float Mon_FootStep_Timer;

	private float Mon_11_Timer;

	private float Mon_10_Timer;

	private float Mon_10_Flip_Timer;

	private float Mon_9_Timer;

	private float Mon_8_Timer;

	private float Mon_7_Timer;

	private float Mon_6_Timer;

	private float Mon_5_Timer;

	private float Mon_4_Timer;

	private float Atk_7_Timer;

	private float Atk_6_Timer;

	private float Atk_5_Timer;

	private float Atk_4_Timer;

	private float Atk_3_Timer;

	private float Atk_2_Timer;

	private float Atk_1_Timer;

	private float Death_9_Timer;

	private float Death_8_Timer;

	private float Death_7_Timer;

	private float Death_6_Timer;

	private float Death_5_Timer;

	private float Death_4_Timer;

	private float Death_3_Timer;

	private float Death_2_Timer;

	private float Death_1_Timer;

	private float Death_0_Timer;

	private float Mon_10_Atk_Timer;

	private float Mon_10_Dmg1_Timer;

	private float Mon_10_Dmg2_Timer;

	private float Mon_10_Dmg3_Timer;

	private float MotherFire_Timer;

	private float HiddenPassage_Timer;

	private float Check_Delay = 0.06f;

	private float Test_Timer;

	private void Update()
	{
		if (hit_Timer > 0f)
		{
			hit_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Magic_Timer_1 > 0f)
		{
			Magic_Timer_1 -= global::UnityEngine.Time.deltaTime;
		}
		if (Magic_Timer_3 > 0f)
		{
			Magic_Timer_3 -= global::UnityEngine.Time.deltaTime;
		}
		if (Magic_Timer_5 > 0f)
		{
			Magic_Timer_5 -= global::UnityEngine.Time.deltaTime;
		}
		if (Mon_Explo_Timer > 0f)
		{
			Mon_Explo_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Mon_FootStep_Timer > 0f)
		{
			Mon_FootStep_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Mon_10_Timer > 0f)
		{
			Mon_10_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Mon_10_Flip_Timer > 0f)
		{
			Mon_10_Flip_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Mon_9_Timer > 0f)
		{
			Mon_9_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Mon_8_Timer > 0f)
		{
			Mon_8_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Mon_7_Timer > 0f)
		{
			Mon_7_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Mon_6_Timer > 0f)
		{
			Mon_6_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Mon_5_Timer > 0f)
		{
			Mon_5_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Mon_4_Timer > 0f)
		{
			Mon_4_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Atk_7_Timer > 0f)
		{
			Atk_7_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Atk_6_Timer > 0f)
		{
			Atk_6_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Atk_5_Timer > 0f)
		{
			Atk_5_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Atk_4_Timer > 0f)
		{
			Atk_4_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Atk_3_Timer > 0f)
		{
			Atk_3_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Atk_2_Timer > 0f)
		{
			Atk_2_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Atk_1_Timer > 0f)
		{
			Atk_1_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Death_9_Timer > 0f)
		{
			Death_9_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Death_8_Timer > 0f)
		{
			Death_8_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Death_7_Timer > 0f)
		{
			Death_7_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Death_6_Timer > 0f)
		{
			Death_6_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Death_5_Timer > 0f)
		{
			Death_5_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Death_4_Timer > 0f)
		{
			Death_4_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Death_3_Timer > 0f)
		{
			Death_3_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Death_2_Timer > 0f)
		{
			Death_2_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Death_1_Timer > 0f)
		{
			Death_1_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Death_0_Timer > 0f)
		{
			Death_0_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Mon_10_Atk_Timer > 0f)
		{
			Mon_10_Atk_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Mon_10_Dmg1_Timer > 0f)
		{
			Mon_10_Dmg1_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Mon_10_Dmg2_Timer > 0f)
		{
			Mon_10_Dmg2_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Mon_10_Dmg3_Timer > 0f)
		{
			Mon_10_Dmg3_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (MotherFire_Timer > 0f)
		{
			MotherFire_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (HiddenPassage_Timer > 0f)
		{
			HiddenPassage_Timer -= global::UnityEngine.Time.deltaTime;
		}
	}

	public void Player_Damage(int Mon_Num, bool onForce, global::UnityEngine.Vector3 pos)
	{
		if (onForce)
		{
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Sound_Hit_6, pos, base.transform.rotation) as global::UnityEngine.GameObject;
			return;
		}
		if (Mon_Num < 3)
		{
			global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(Sound_Hit_3, pos, base.transform.rotation) as global::UnityEngine.GameObject;
			return;
		}
		switch (Mon_Num)
		{
		case 6:
		{
			global::UnityEngine.GameObject gameObject5 = global::UnityEngine.Object.Instantiate(Sound_Hit_4, pos, base.transform.rotation) as global::UnityEngine.GameObject;
			break;
		}
		case 14:
		case 15:
		{
			global::UnityEngine.GameObject gameObject4 = global::UnityEngine.Object.Instantiate(Sound_Hit_11, pos, base.transform.rotation) as global::UnityEngine.GameObject;
			break;
		}
		default:
		{
			global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(Sound_Hit_2, pos, base.transform.rotation) as global::UnityEngine.GameObject;
			break;
		}
		}
	}

	public void FootStep_Mon(global::UnityEngine.Vector3 pos)
	{
		if (Mon_FootStep_Timer <= 0f)
		{
			Mon_FootStep_Timer = 0.1f;
			if (Mon_FootStep_Num == 0)
			{
				Mon_FootStep_Num++;
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Sound_Footstep_Mon_1, pos, base.transform.rotation) as global::UnityEngine.GameObject;
			}
			else
			{
				Mon_FootStep_Num = 0;
				global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(Sound_Footstep_Mon_2, pos, base.transform.rotation) as global::UnityEngine.GameObject;
			}
		}
	}

	public void FootStep_Queen(global::UnityEngine.Vector3 pos)
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Sound_Footstep_Mon_2, pos, base.transform.rotation) as global::UnityEngine.GameObject;
	}

	public void Magic_1(global::UnityEngine.Vector3 pos)
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Sound_Magic_1, pos, base.transform.rotation) as global::UnityEngine.GameObject;
	}

	public void Magic_2(global::UnityEngine.Vector3 pos)
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Sound_Magic_2, pos, base.transform.rotation) as global::UnityEngine.GameObject;
	}

	public void Magic_3_Explo(global::UnityEngine.Vector3 pos)
	{
		if (Magic_Timer_3 <= 0f)
		{
			Magic_Timer_3 = Check_Delay;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Sound_Magic_3_Explo_1, pos, base.transform.rotation) as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(Sound_Magic_3_Explo_2, pos, base.transform.rotation) as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(Sound_Magic_3_Explo_3, pos, base.transform.rotation) as global::UnityEngine.GameObject;
		}
	}

	public void Mon_Hit_1(global::UnityEngine.Vector3 pos)
	{
		if (hit_Timer <= 0f)
		{
			hit_Timer = 0.1f;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Sound_Hit_1, pos, base.transform.rotation) as global::UnityEngine.GameObject;
		}
	}

	public void Mon_Hit_2(global::UnityEngine.Vector3 pos)
	{
		if (Magic_Timer_5 <= 0f)
		{
			Magic_Timer_5 = 0.1f;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Sound_Hit_2, pos, base.transform.rotation) as global::UnityEngine.GameObject;
		}
	}

	public void Mon_Explo(global::UnityEngine.Vector3 pos)
	{
		if (Mon_Explo_Timer <= 0f)
		{
			Mon_Explo_Timer = 0.1f;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Sound_Hit_Explo, pos, base.transform.rotation) as global::UnityEngine.GameObject;
		}
	}

	public void Mon_Atk_1(global::UnityEngine.Vector3 pos)
	{
		if (Atk_1_Timer <= 0f)
		{
			Atk_1_Timer = Check_Delay;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Sound_MonAtk_1, pos, base.transform.rotation) as global::UnityEngine.GameObject;
		}
	}

	public void Mon_Atk_2(global::UnityEngine.Vector3 pos)
	{
		if (Atk_2_Timer <= 0f)
		{
			Atk_2_Timer = Check_Delay;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Sound_MonAtk_2, pos, base.transform.rotation) as global::UnityEngine.GameObject;
		}
	}

	public void Mon_Atk_3(global::UnityEngine.Vector3 pos)
	{
		if (Atk_3_Timer <= 0f)
		{
			Atk_3_Timer = Check_Delay;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Sound_MonAtk_3, pos, base.transform.rotation) as global::UnityEngine.GameObject;
		}
	}

	public void Mon_Atk_4(global::UnityEngine.Vector3 pos)
	{
		if (Atk_4_Timer <= 0f)
		{
			Atk_4_Timer = Check_Delay;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Sound_MonAtk_4, pos, base.transform.rotation) as global::UnityEngine.GameObject;
		}
	}

	public void Mon_Atk_5(global::UnityEngine.Vector3 pos)
	{
		if (Atk_5_Timer <= 0f)
		{
			Atk_5_Timer = Check_Delay;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Sound_MonAtk_5, pos, base.transform.rotation) as global::UnityEngine.GameObject;
		}
	}

	public void Mon_Atk_6(global::UnityEngine.Vector3 pos)
	{
		if (Atk_6_Timer <= 0f)
		{
			Atk_6_Timer = Check_Delay;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Sound_MonAtk_6, pos, base.transform.rotation) as global::UnityEngine.GameObject;
		}
	}

	public void Mon_Atk_7(global::UnityEngine.Vector3 pos)
	{
		if (Atk_7_Timer <= 0f)
		{
			Atk_7_Timer = Check_Delay;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Sound_MonAtk_7, pos, base.transform.rotation) as global::UnityEngine.GameObject;
		}
	}

	public void Electric_Dmg(global::UnityEngine.Vector3 pos)
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Sound_Elec, pos, base.transform.rotation) as global::UnityEngine.GameObject;
	}

	public void Plasma_Atk(global::UnityEngine.Vector3 pos)
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Sound_Plasma, pos, base.transform.rotation) as global::UnityEngine.GameObject;
	}

	public void Mon_11_Growling(global::UnityEngine.Vector3 pos)
	{
		if (Mon_11_Timer <= 0f)
		{
			Mon_11_Timer = 6f + (float)global::UnityEngine.Random.Range(0, 600) * 0.01f;
			if (global::UnityEngine.Random.Range(1, 6) < 3)
			{
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Alien_Growling_4, pos, base.transform.rotation) as global::UnityEngine.GameObject;
			}
			else
			{
				global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(Alien_Growling_5, pos, base.transform.rotation) as global::UnityEngine.GameObject;
			}
		}
	}

	public void Mon_10_Growling(global::UnityEngine.Vector3 pos)
	{
		if (Mon_10_Timer <= 0f)
		{
			Mon_10_Timer = 2f + (float)global::UnityEngine.Random.Range(0, 200) * 0.01f;
			switch (global::UnityEngine.Random.Range(1, 4))
			{
			case 1:
			{
				global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(Mon_10_Growling_2, pos, base.transform.rotation) as global::UnityEngine.GameObject;
				break;
			}
			case 2:
			{
				global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(Mon_10_Growling_3, pos, base.transform.rotation) as global::UnityEngine.GameObject;
				break;
			}
			default:
			{
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Mon_10_Growling_4, pos, base.transform.rotation) as global::UnityEngine.GameObject;
				break;
			}
			}
		}
	}

	public void Mon_10_Flip(global::UnityEngine.Vector3 pos)
	{
		if (Mon_10_Flip_Timer <= 0f)
		{
			Mon_10_Flip_Timer = 3f;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Mon_10_Growling_1, pos, base.transform.rotation) as global::UnityEngine.GameObject;
		}
	}

	public void Mon_40_Dash(global::UnityEngine.Vector3 pos)
	{
		if (Mon_9_Timer <= 0f)
		{
			Mon_9_Timer = 0.1f;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Alien_Growling_1, pos, base.transform.rotation) as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(Alien_Growling_5, pos, base.transform.rotation) as global::UnityEngine.GameObject;
		}
	}

	public void Mon_9_Growling(global::UnityEngine.Vector3 pos)
	{
		if (Mon_9_Timer <= 0f)
		{
			Mon_9_Timer = 2f + (float)global::UnityEngine.Random.Range(0, 200) * 0.01f;
			int num = global::UnityEngine.Random.Range(1, 9);
			if (num < 3)
			{
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Alien_Growling_1, pos, base.transform.rotation) as global::UnityEngine.GameObject;
				return;
			}
			if (num < 5)
			{
				global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(Alien_Growling_2, pos, base.transform.rotation) as global::UnityEngine.GameObject;
				return;
			}
			if (num < 7)
			{
				global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(Alien_Growling_3, pos, base.transform.rotation) as global::UnityEngine.GameObject;
				return;
			}
			Mon_9_Timer = 6f;
			global::UnityEngine.GameObject gameObject4 = global::UnityEngine.Object.Instantiate(Alien_Dash_4, pos, base.transform.rotation) as global::UnityEngine.GameObject;
		}
	}

	public void Mon_9_Dash(global::UnityEngine.Vector3 pos)
	{
		switch (global::UnityEngine.Random.Range(1, 4))
		{
		case 1:
		{
			global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(Alien_Dash_1, pos, base.transform.rotation) as global::UnityEngine.GameObject;
			break;
		}
		case 2:
		{
			global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(Alien_Dash_2, pos, base.transform.rotation) as global::UnityEngine.GameObject;
			break;
		}
		default:
		{
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Alien_Dash_3, pos, base.transform.rotation) as global::UnityEngine.GameObject;
			break;
		}
		}
	}

	public void Mon_9_Damage(global::UnityEngine.Vector3 pos)
	{
		int num = global::UnityEngine.Random.Range(0, 6);
		if (num < 3)
		{
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Alien_Dmg_1, pos, base.transform.rotation) as global::UnityEngine.GameObject;
		}
		else
		{
			global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(Alien_Dmg_2, pos, base.transform.rotation) as global::UnityEngine.GameObject;
		}
	}

	public void Mon_8_Damage(global::UnityEngine.Vector3 pos)
	{
		if (Mon_8_Timer <= 0f)
		{
			Mon_8_Timer = 0.6f + (float)global::UnityEngine.Random.Range(0, 100) * 0.01f;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Mon_8_Dmg, pos, base.transform.rotation) as global::UnityEngine.GameObject;
		}
	}

	public void Mon_7_Attack(global::UnityEngine.Vector3 pos)
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Mon_7_Atk, pos, base.transform.rotation) as global::UnityEngine.GameObject;
	}

	public void Mon_7_Damage(global::UnityEngine.Vector3 pos)
	{
		if (Mon_7_Timer <= 0f)
		{
			Mon_7_Timer = 0.6f + (float)global::UnityEngine.Random.Range(0, 100) * 0.01f;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Mon_7_Dmg, pos, base.transform.rotation) as global::UnityEngine.GameObject;
		}
	}

	public void Mon_6_Damage(global::UnityEngine.Vector3 pos)
	{
		if (Mon_6_Timer <= 0f)
		{
			Mon_6_Timer = 3f + (float)global::UnityEngine.Random.Range(0, 300) * 0.01f;
			Mon_9_Damage(pos);
		}
	}

	public void Mon_5_Damage(global::UnityEngine.Vector3 pos)
	{
		if (Mon_5_Timer <= 0f)
		{
			Mon_5_Timer = 3f + (float)global::UnityEngine.Random.Range(0, 300) * 0.01f;
			Mon_9_Damage(pos);
		}
	}

	public void Mon_9_Death(global::UnityEngine.Vector3 pos)
	{
		if (Death_9_Timer <= 0f)
		{
			Death_9_Timer = 0.2f;
			switch (global::UnityEngine.Random.Range(1, 6))
			{
			case 1:
			{
				global::UnityEngine.GameObject gameObject5 = global::UnityEngine.Object.Instantiate(Alien_Death_1, pos, base.transform.rotation) as global::UnityEngine.GameObject;
				break;
			}
			case 2:
			{
				global::UnityEngine.GameObject gameObject4 = global::UnityEngine.Object.Instantiate(Alien_Death_2, pos, base.transform.rotation) as global::UnityEngine.GameObject;
				break;
			}
			case 3:
			{
				global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(Alien_Death_3, pos, base.transform.rotation) as global::UnityEngine.GameObject;
				break;
			}
			case 4:
			{
				global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(Alien_Death_4, pos, base.transform.rotation) as global::UnityEngine.GameObject;
				break;
			}
			default:
			{
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Alien_Death_5, pos, base.transform.rotation) as global::UnityEngine.GameObject;
				break;
			}
			}
		}
	}

	public void Mon_8_Death(global::UnityEngine.Vector3 pos)
	{
		if (Death_8_Timer <= 0f)
		{
			Death_8_Timer = 0.2f;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Mon_8_Dmg, pos, base.transform.rotation) as global::UnityEngine.GameObject;
		}
	}

	public void Mon_7_Death(global::UnityEngine.Vector3 pos)
	{
		if (Death_7_Timer <= 0f)
		{
			Death_7_Timer = 0.2f;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Mon_7_Dmg, pos, base.transform.rotation) as global::UnityEngine.GameObject;
		}
	}

	public void Mon_6_Death(global::UnityEngine.Vector3 pos)
	{
		if (Death_6_Timer <= 0f)
		{
			Death_6_Timer = 0.2f;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Mon_6_Dmg, pos, base.transform.rotation) as global::UnityEngine.GameObject;
		}
	}

	public void Mon_5_Death(global::UnityEngine.Vector3 pos)
	{
		if (Death_5_Timer <= 0f)
		{
			Death_5_Timer = 0.2f;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Mon_5_Dmg, pos, base.transform.rotation) as global::UnityEngine.GameObject;
		}
	}

	public void Mon_4_Death(global::UnityEngine.Vector3 pos)
	{
		if (Death_4_Timer <= 0f)
		{
			Death_4_Timer = 0.2f;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Mon_4_Dmg, pos, base.transform.rotation) as global::UnityEngine.GameObject;
		}
	}

	public void Mon_3_Death(global::UnityEngine.Vector3 pos)
	{
		if (Death_3_Timer <= 0f)
		{
			Death_3_Timer = 0.2f;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Mon_3_Dmg, pos, base.transform.rotation) as global::UnityEngine.GameObject;
		}
	}

	public void Mon_2_Death(global::UnityEngine.Vector3 pos)
	{
		if (Death_2_Timer <= 0f)
		{
			Death_2_Timer = 0.2f;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Mon_2_Dmg, pos, base.transform.rotation) as global::UnityEngine.GameObject;
		}
	}

	public void Mon_1_Death(global::UnityEngine.Vector3 pos)
	{
		if (Death_1_Timer <= 0f)
		{
			Death_1_Timer = 0.2f;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Mon_1_Dmg, pos, base.transform.rotation) as global::UnityEngine.GameObject;
		}
	}

	public void Mob_Death(global::UnityEngine.Vector3 pos)
	{
		if (Death_0_Timer <= 0f)
		{
			Death_0_Timer = 0.2f;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Mob_Dmg, pos, base.transform.rotation) as global::UnityEngine.GameObject;
		}
	}

	public void Mon_10_Attack(global::UnityEngine.Vector3 pos)
	{
		if (Mon_10_Atk_Timer <= 0f)
		{
			Mon_10_Atk_Timer = 0.1f;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Mon_10_Atk, pos, base.transform.rotation) as global::UnityEngine.GameObject;
		}
	}

	public void Mon_10_Damage1(global::UnityEngine.Vector3 pos)
	{
		if (Mon_10_Dmg1_Timer <= 0f)
		{
			Mon_10_Dmg1_Timer = 0.1f;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Mon_10_Dmg1, pos, base.transform.rotation) as global::UnityEngine.GameObject;
		}
	}

	public void Mon_10_Damage2(global::UnityEngine.Vector3 pos)
	{
		if (Mon_10_Dmg2_Timer <= 0f)
		{
			Mon_10_Dmg2_Timer = 0.1f;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Mon_10_Dmg2, pos, base.transform.rotation) as global::UnityEngine.GameObject;
		}
	}

	public void Mon_10_Death(global::UnityEngine.Vector3 pos)
	{
		if (Mon_10_Dmg3_Timer <= 0f)
		{
			Mon_10_Dmg3_Timer = 0.1f;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Mon_10_Dmg3, pos, base.transform.rotation) as global::UnityEngine.GameObject;
		}
	}

	public void Boss_4_Fire(global::UnityEngine.Vector3 pos)
	{
		if (MotherFire_Timer <= 0f)
		{
			MotherFire_Timer = Check_Delay;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(s_List[0], pos, base.transform.rotation) as global::UnityEngine.GameObject;
		}
	}

	public void HiddenPassage(global::UnityEngine.Vector3 pos)
	{
		if (HiddenPassage_Timer <= 0f)
		{
			HiddenPassage_Timer = 0.1f;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(s_List[1], pos, base.transform.rotation) as global::UnityEngine.GameObject;
		}
	}
}
