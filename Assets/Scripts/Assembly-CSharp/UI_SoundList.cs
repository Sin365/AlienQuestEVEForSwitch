public class UI_SoundList : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject levelUP;

	public global::UnityEngine.GameObject MenuOn;

	public global::UnityEngine.GameObject MenuOff;

	public global::UnityEngine.GameObject Select;

	public global::UnityEngine.GameObject Tab;

	public global::UnityEngine.GameObject Btn;

	public global::UnityEngine.GameObject DeviceOn;

	public global::UnityEngine.GameObject Move_1;

	public global::UnityEngine.GameObject Move_2;

	public global::UnityEngine.GameObject Get_Item;

	public global::UnityEngine.GameObject Get_Weapon;

	public global::UnityEngine.GameObject Get_Ability;

	public global::UnityEngine.GameObject Get_Bonus;

	public global::UnityEngine.GameObject Get_Potion;

	public global::UnityEngine.GameObject Full_HP;

	public global::UnityEngine.GameObject MissionBriefing;

	public global::UnityEngine.GameObject MapOn;

	public global::UnityEngine.GameObject Gate_Open;

	public global::UnityEngine.GameObject Gate_Close;

	public global::UnityEngine.GameObject Save_On;

	public global::UnityEngine.GameObject Save_Off;

	public global::UnityEngine.GameObject Trap_Laser;

	private float Item_Timer;

	private float Weapon_Timer;

	private float Ability_Timer;

	private float Bonus_Timer;

	private float Potion_Timer;

	private float FullHP_Timer;

	private float Laser_Timer;

	private void Update()
	{
		if (Item_Timer > 0f)
		{
			Item_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Weapon_Timer > 0f)
		{
			Weapon_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Ability_Timer > 0f)
		{
			Ability_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Bonus_Timer > 0f)
		{
			Bonus_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Potion_Timer > 0f)
		{
			Potion_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (FullHP_Timer > 0f)
		{
			FullHP_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Laser_Timer > 0f)
		{
			Laser_Timer -= global::UnityEngine.Time.deltaTime;
		}
	}

	private void Sound_Get_Item()
	{
		if (Item_Timer <= 0f)
		{
			Item_Timer = 0.01f;
			//global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Get_Item) as global::UnityEngine.GameObject;
			AxiSoundPool.AddSoundForTrans(Get_Item);
		}
	}

	private void Sound_Get_Weapon()
	{
		if (Weapon_Timer <= 0f)
		{
			Weapon_Timer = 0.01f;
			//global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Get_Weapon) as global::UnityEngine.GameObject;
            AxiSoundPool.AddSoundForTrans(Get_Weapon);
        }
	}

	private void Sound_Get_Ability()
	{
		if (Ability_Timer <= 0f)
		{
			Ability_Timer = 0.01f;
			//global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Get_Ability) as global::UnityEngine.GameObject;
            AxiSoundPool.AddSoundForTrans(Get_Ability);
        }
	}

	private void Sound_Get_Bonus()
	{
		if (Bonus_Timer <= 0f)
		{
			Bonus_Timer = 0.01f;
            //global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Get_Bonus) as global::UnityEngine.GameObject;
            AxiSoundPool.AddSoundForTrans(Get_Bonus);
        }
	}

	private void Sound_Get_Potion()
	{
		if (Potion_Timer <= 0f)
		{
			Potion_Timer = 0.01f;
			//global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Get_Potion) as global::UnityEngine.GameObject;
            AxiSoundPool.AddSoundForTrans(Get_Potion);
        }
	}

	private void Sound_Get_FullHP()
	{
		if (FullHP_Timer <= 0f)
		{
			FullHP_Timer = 0.01f;
			//global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Full_HP) as global::UnityEngine.GameObject;
            AxiSoundPool.AddSoundForTrans(Full_HP);
        }
	}

	private void Sound_Laser()
	{
		if (Laser_Timer <= 0f)
		{
			Laser_Timer = 0.01f;
            //global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Trap_Laser) as global::UnityEngine.GameObject;
            AxiSoundPool.AddSoundForTrans(Trap_Laser);
        }
	}

	private void Sound_LevelUp()
	{
        //global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(levelUP) as global::UnityEngine.GameObject;
        AxiSoundPool.AddSoundForTrans(levelUP);
    }

	private void Sound_MenuOn()
	{
        //global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(MenuOn) as global::UnityEngine.GameObject;
        AxiSoundPool.AddSoundForTrans(MenuOn);
    }

	private void Sound_MenuOff()
	{
		//global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(MenuOff) as global::UnityEngine.GameObject;
        AxiSoundPool.AddSoundForTrans(MenuOff);
    }

	private void Sound_Select()
	{
        //global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Select) as global::UnityEngine.GameObject;
        AxiSoundPool.AddSoundForTrans(Select);
    }

	private void Sound_Tab()
	{
        //global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Tab) as global::UnityEngine.GameObject;
        AxiSoundPool.AddSoundForTrans(Tab);
    }

	private void Sound_Btn()
	{
		//global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Btn) as global::UnityEngine.GameObject;
        AxiSoundPool.AddSoundForTrans(Btn);
    }

	private void Sound_DeviceOn()
	{
		//global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(DeviceOn) as global::UnityEngine.GameObject;
        AxiSoundPool.AddSoundForTrans(DeviceOn);
    }

	private void Sound_Move_1()
	{
        //global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Move_1) as global::UnityEngine.GameObject;
        AxiSoundPool.AddSoundForTrans(Move_1);
    }

	private void Sound_Move_2()
	{
        //global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Move_2) as global::UnityEngine.GameObject;
        AxiSoundPool.AddSoundForTrans(Move_2);
    }

	public void Sound_Gate_Open()
	{
		//global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Gate_Open) as global::UnityEngine.GameObject;
        AxiSoundPool.AddSoundForTrans(Gate_Open);
    }

	private void Sound_Gate_Close()
	{
		//global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Gate_Close) as global::UnityEngine.GameObject;
        AxiSoundPool.AddSoundForTrans(Gate_Close);
    }

	private void Sound_Save_On()
	{
		//global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Save_On) as global::UnityEngine.GameObject;
        AxiSoundPool.AddSoundForTrans(Save_On);
    }

	private void Sound_Save_Off()
	{
		//global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Save_Off) as global::UnityEngine.GameObject;
        AxiSoundPool.AddSoundForTrans(Save_Off);
    }

	private void Sound_MissionBriefing()
	{
		//global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(MissionBriefing) as global::UnityEngine.GameObject;
        AxiSoundPool.AddSoundForTrans(MissionBriefing);
    }

	private void Sound_MapOn()
	{
        //global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(MapOn) as global::UnityEngine.GameObject;
        AxiSoundPool.AddSoundForTrans(MapOn);
    }
}
