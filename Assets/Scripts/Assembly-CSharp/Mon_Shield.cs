public class Mon_Shield : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject MonObject;

	public global::UnityEngine.GameObject Sound_Block;

	private float Sound_Timer;

	private float Damage_Delay;

	private bool Hit_Atk_1;

	private bool Hit_Atk_2;

	private bool Hit_Atk_3;

	private bool Hit_Atk_4;

	private bool Hit_Spin;

	private bool Hit_Rolling;

	private float Hit_Delay;

	private float Hit_Timer;

	private int Hit_Combo;

    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			if (Sound_Timer > 0f)
			{
				Sound_Timer -= global::UnityEngine.Time.deltaTime;
			}
			if (Damage_Delay > 0f)
			{
				Damage_Delay -= global::UnityEngine.Time.deltaTime;
			}
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
		}
		if (MonObject == null)
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void Block()
	{
		if (MonObject != null)
		{
			MonObject.SendMessage("Set_Block");
		}
		AxiSoundPool.AddSoundForPosRot(Sound_Block, base.transform.position, base.transform.rotation);
	}

	private void OnTriggerStay2D(global::UnityEngine.Collider2D col)
	{
		if (GM.Paused || GM.GameOver || GM.onGameClear || !(MonObject != null))
		{
			return;
		}
		if (!(global::UnityEngine.GameObject.Find("Border_Rolling").GetComponent<global::UnityEngine.SpriteRenderer>().color.a > 0.05f) && !(col.tag == "Magic_Shield") && !GM.onShield && col.name == "Ani" && GM.Damage_Timer <= 0f && Damage_Delay <= 0f)
		{
			if (base.transform.position.x > col.transform.parent.position.x)
			{
				GM.Damage(45, -10f, false, 0);
			}
			else
			{
				GM.Damage(45, 10f, false, 0);
			}
			Damage_Delay = 0.5f;
			global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Player_Damage(6, false, base.transform.position);
		}
		if (col.tag == "Col_PC_Atk")
		{
			if (col.name == "Col_Attack_0_1" || col.name == "Col_Attack_1_1" || col.name == "Col_Attack_2_1" || col.name == "Col_Attack_4_1" || col.name == "Col_Attack_5_1")
			{
				if (!Hit_Atk_1)
				{
					Hit_Atk_2 = false;
					Hit_Atk_1 = true;
					Hit_Delay = 0.3f;
					Block();
				}
			}
			else if (col.name == "Col_Attack_0_2" || col.name == "Col_Attack_1_2" || col.name == "Col_Attack_2_2" || col.name == "Col_Attack_4_2" || col.name == "Col_Attack_5_2")
			{
				if (!Hit_Atk_2)
				{
					Hit_Atk_1 = false;
					Hit_Atk_2 = true;
					Hit_Delay = 0.3f;
					Block();
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
					Block();
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
					Block();
				}
			}
			else if (col.name == "Col_Spin")
			{
				if (Hit_Delay <= 0f)
				{
					Hit_Delay = 0.1f;
					Block();
				}
			}
			else if (col.name == "Col_Rolling")
			{
				if (Hit_Delay <= 0f)
				{
					if (global::UnityEngine.GameObject.Find("Border_Rolling").GetComponent<global::UnityEngine.SpriteRenderer>().color.a > 0.05f)
					{
						Hit_Delay = 0.1f;
					}
					else
					{
						Hit_Delay = 0.24f;
					}
					Block();
				}
			}
			else if (!Hit_Atk_1)
			{
				Hit_Atk_1 = true;
				Hit_Delay = 0.3f;
				Block();
			}
		}
		else if (col.tag == "Magic_Fire" && Sound_Timer <= 0f)
		{
			Sound_Timer = 0.1f;
			Block();
		}
	}
}
