public class Item : global::UnityEngine.MonoBehaviour
{
	public int Item_Num;

	public int Bonus_Index;

	public global::UnityEngine.GameObject Info;

	public global::UnityEngine.GameObject _glowEffect;

	public global::UnityEngine.GameObject Effect_GetItem;

	private global::UnityEngine.GameObject GlowEffect;

	private bool Impact;

	private float Stay_Timer;

	private bool onFlicker;

	private float Flicker_Timer;

	private bool on_Target;

	private global::UnityEngine.Vector3 pos_Target;

	private int Room_Num = -1;

	private float Del_Timer;

	private global::UnityEngine.Shader shaderGUItext;

	private global::UnityEngine.Shader shaderSpritesDefault;

    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		shaderGUItext = global::UnityEngine.Shader.Find("GUI/Text Shader");
		shaderSpritesDefault = global::UnityEngine.Shader.Find("Sprites/Default");
		if (Item_Num <= 100 || Bonus_Index != 0)
		{
			if (Item_Num > 20)
			{
				Check_Bonus();
			}
			else
			{
				Check_Item();
			}
		}
		if (Item_Num == 202 && GM.Get_Event(2))
		{
			if (GlowEffect != null)
			{
				global::UnityEngine.Object.Destroy(GlowEffect.gameObject);
			}
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
		if (_glowEffect != null)
		{
			GlowEffect = global::UnityEngine.Object.Instantiate(_glowEffect, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
			GlowEffect.GetComponent<Item_Glow>().ItemObject = base.gameObject;
			GlowEffect.transform.parent = base.transform;
		}
		Room_Num = GM.Room_Num;
	}

	private void Check_Item()
	{
		switch (Item_Num)
		{
		case 1:
			if (GM.onWeapon_1)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			break;
		case 2:
			if (GM.onWeapon_2)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			break;
		case 3:
			if (GM.onWeapon_3)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			break;
		case 4:
			if (GM.onWeapon_4)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			break;
		case 5:
			if (GM.onWeapon_5)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			break;
		case 7:
			if (GM.onSkill_2)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			break;
		case 8:
			if (GM.onSkill_3)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			break;
		case 9:
			if (GM.onSkill_4)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			break;
		case 10:
			if (GM.onSkill_5)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			break;
		case 11:
			if (GM.onBackDash)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			break;
		case 12:
			if (GM.onDBJump)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			break;
		case 13:
			if (GM.onSpeedUp)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			break;
		case 14:
			if (GM.onHighJump)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			break;
		case 15:
			if (GM.onScrew)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			break;
		case 16:
			if (GM.onCard_1)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			break;
		case 17:
			if (GM.onCard_2)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			break;
		case 18:
			if (GM.onCard_3)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			break;
		case 19:
			if (GM.onCard_4)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			break;
		case 20:
			if (GM.onCard_5)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			break;
		case 6:
			break;
		}
	}

	private void Check_Bonus()
	{
		if (GM.Check_Bonus(Bonus_Index))
		{
			if (GlowEffect != null)
			{
				global::UnityEngine.Object.Destroy(GlowEffect.gameObject);
			}
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void Update()
	{
		if (GM.Paused || GM.onGatePass)
		{
			return;
		}
		Flicker_Timer += global::UnityEngine.Time.deltaTime;
		if (onFlicker)
		{
			if (Flicker_Timer > 0.05f)
			{
				onFlicker = false;
				Flicker_Timer = 0f;
				GetComponent<global::UnityEngine.SpriteRenderer>().material.shader = shaderSpritesDefault;
				GetComponent<global::UnityEngine.SpriteRenderer>().color = global::UnityEngine.Color.white;
			}
		}
		else if (Flicker_Timer > 1.5f)
		{
			onFlicker = true;
			Flicker_Timer = 0f;
			GetComponent<global::UnityEngine.SpriteRenderer>().material.shader = shaderGUItext;
			GetComponent<global::UnityEngine.SpriteRenderer>().color = global::UnityEngine.Color.white;
		}
		if (on_Target)
		{
			if (Item_Num == 202)
			{
				base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, pos_Target, global::UnityEngine.Time.deltaTime * 0.8f);
			}
			else
			{
				base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, pos_Target, global::UnityEngine.Time.deltaTime * 0.7f);
			}
		}
		if (Item_Num <= 100)
		{
			return;
		}
		if (Room_Num != GM.Room_Num)
		{
			Del_Timer += global::UnityEngine.Time.deltaTime;
			if (Del_Timer > 10f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
		else
		{
			Del_Timer = 0f;
		}
	}

	public void Set_Target(global::UnityEngine.Vector3 pos)
	{
		on_Target = true;
		pos_Target = pos;
	}

	private void Check_Col()
	{
		if (Item_Num > 100)
		{
			if (Item_Num == 103 && GM.HP < GM.HP_Max)
			{
				Impact = true;
				global::UnityEngine.GameObject.Find("Menu").SendMessage("Sound_Get_Item");
				global::UnityEngine.GameObject.Find("Menu").SendMessage("Sound_Get_Potion");
				global::UnityEngine.GameObject.Find("Menu").SendMessage("Sound_Get_FullHP");
				GM.Get_HP(500);
				if (GM.onPoison)
				{
					GM.Poison_Timer = 0f;
				}
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			else if (Item_Num == 102 && GM.Potion_MP < 100)
			{
				Impact = true;
				GM.Get_Potion_MP(Bonus_Index);
				global::UnityEngine.GameObject.Find("Potion_Inventory").SendMessage("Get_Potion_MP");
				global::UnityEngine.GameObject.Find("Menu").SendMessage("Sound_Get_Item");
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			else if (Item_Num == 101 && GM.Potion_HP < 100)
			{
				Impact = true;
				GM.Get_Potion_HP(Bonus_Index);
				global::UnityEngine.GameObject.Find("Potion_Inventory").SendMessage("Get_Potion_HP");
				global::UnityEngine.GameObject.Find("Menu").SendMessage("Sound_Get_Item");
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			else if (Item_Num == 202)
			{
				Impact = true;
				if (!GM.Get_Event(2))
				{
					GM.Set_Event(2);
				}
				if (GetComponent<Gen_Effect_1>() != null)
				{
					GetComponent<Gen_Effect_1>().Destroy_Dust();
				}
				if (GlowEffect != null)
				{
					global::UnityEngine.Object.Destroy(GlowEffect.gameObject);
				}
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			return;
		}
		Impact = true;
		global::UnityEngine.GameObject.Find("Menu").SendMessage("Sound_Get_Item");
		if (Item_Num > 0 && Item_Num < 11)
		{
			global::UnityEngine.GameObject.Find("Menu").SendMessage("Sound_Get_Weapon");
		}
		else if (Item_Num > 10 && Item_Num < 16)
		{
			global::UnityEngine.GameObject.Find("Menu").SendMessage("Sound_Get_Ability");
		}
		else if (Item_Num > 20 && Item_Num < 26)
		{
			global::UnityEngine.GameObject.Find("Menu").SendMessage("Sound_Get_Bonus");
		}
		if (Item_Num == 1)
		{
			GM.Get_Weapon_1();
		}
		else if (Item_Num == 2)
		{
			GM.Get_Weapon_2();
		}
		else if (Item_Num == 3)
		{
			GM.Get_Weapon_3();
		}
		else if (Item_Num == 4)
		{
			GM.Get_Weapon_4();
		}
		else if (Item_Num == 5)
		{
			GM.Get_Weapon_5();
		}
		else if (Item_Num == 7)
		{
			GM.Get_Skill_2();
		}
		else if (Item_Num == 8)
		{
			GM.Get_Skill_3();
		}
		else if (Item_Num == 9)
		{
			GM.Get_Skill_4();
		}
		else if (Item_Num == 10)
		{
			GM.Get_Skill_5();
		}
		else if (Item_Num == 11)
		{
			GM.Get_Ability_1();
		}
		else if (Item_Num == 12)
		{
			GM.Get_Ability_2();
		}
		else if (Item_Num == 13)
		{
			GM.Get_Ability_3();
		}
		else if (Item_Num == 14)
		{
			GM.Get_Ability_4();
		}
		else if (Item_Num == 15)
		{
			GM.Get_Ability_5();
		}
		else if (Item_Num == 16)
		{
			GM.Get_Card_1();
		}
		else if (Item_Num == 17)
		{
			GM.Get_Card_2();
		}
		else if (Item_Num == 18)
		{
			GM.Get_Card_3();
		}
		else if (Item_Num == 19)
		{
			GM.Get_Card_4();
		}
		else if (Item_Num == 20)
		{
			GM.Get_Card_5();
		}
		else if (Item_Num == 21)
		{
			GM.Get_Bonus_1(Bonus_Index);
		}
		else if (Item_Num == 22)
		{
			GM.Get_Bonus_2(Bonus_Index);
		}
		else if (Item_Num == 23)
		{
			GM.Get_Bonus_3(Bonus_Index);
		}
		else if (Item_Num == 24)
		{
			GM.Get_Bonus_4(Bonus_Index);
		}
		else if (Item_Num == 25)
		{
			GM.Get_Bonus_5(Bonus_Index);
		}
		if (Info != null)
		{
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Info) as global::UnityEngine.GameObject;
		}
		if (Item_Num == 3 || Item_Num == 4 || Item_Num == 10 || Item_Num == 24 || Item_Num == 25)
		{
			GetComponent<Gen_Effect_1>().Destroy_Dust();
		}
		global::UnityEngine.Object.Destroy(base.gameObject);
	}

	private void OnTriggerEnter2D(global::UnityEngine.Collider2D col)
	{
		if (Item_Num > 0 && !GM.onGatePass && !GM.Paused && !GM.GameOver && !Impact && col.tag == "Player_Col")
		{
			Check_Col();
		}
	}

	private void OnTriggerStay2D(global::UnityEngine.Collider2D col)
	{
		if (Item_Num > 0 && !GM.onGatePass && !GM.Paused && !GM.GameOver && !Impact && col.tag == "Player_Col")
		{
			Stay_Timer += global::UnityEngine.Time.deltaTime;
			if (Stay_Timer > 0.3f)
			{
				Check_Col();
			}
		}
	}
}
