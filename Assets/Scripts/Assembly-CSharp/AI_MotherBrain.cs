public class AI_MotherBrain : global::UnityEngine.MonoBehaviour
{
	public bool isDeath;

	private float Death_Timer;

	private int facingRight = -1;

	private float Life_Timer;

	private float Flip_Timer;

	private int Attack_Level = 1;

	private float Attack_Delay;

	private float Attack_Timer;

	private bool onAttack_Fire;

	private bool isLaunched;

	private bool onAttack_Laser;

	private bool onAttack_Gravity;

	private bool onAttack_Laser_5;

	private float Fire_Timer = 1f;

	private float Laser_Timer = 1f;

	private float Gravity_Timer = 1f;

	private int Fire_Num;

	private float FireNum_Timer;

	private bool onArmStart;

	private bool onClearItem;

	public bool onEvent = true;

	public int Event_Num;

	private int Ani_Num;

	public global::UnityEngine.GameObject Explo;

	public global::UnityEngine.Transform[] explo_Pos;

	public global::UnityEngine.Transform pos_Text;

	public global::UnityEngine.Transform pos_Text_P;

	public global::UnityEngine.GameObject Blood_Obj;

	private float ExploSound_Timer;

	private float[] Explo_Timer = new float[15];

	private global::UnityEngine.Vector3 pos_Orig;

	private global::UnityEngine.Vector3 pos_Death;

	private int Glow_State;

	private float Glow_Timer;

	public global::UnityEngine.SpriteRenderer Brain_Glow_1;

	public global::UnityEngine.SpriteRenderer Brain_Glow_2;

	public global::UnityEngine.SpriteRenderer Eye_Glow_1;

	public global::UnityEngine.SpriteRenderer Eye_Glow_2;

	public global::UnityEngine.SpriteRenderer Eye_Black;

	public global::UnityEngine.SpriteRenderer[] SR_PipeGlow;

	public global::UnityEngine.SpriteRenderer[] SR_GlowBox;

	private global::UnityEngine.Color colorGlow = new global::UnityEngine.Color(0f, 1f, 0f);

	private global::UnityEngine.Color colorGlowBox = new global::UnityEngine.Color(0.5f, 1f, 0f);

	public global::UnityEngine.GameObject Arm_Obj;

	private global::UnityEngine.GameObject Mother_Arm;

	public global::UnityEngine.GameObject _Fire;

	public global::UnityEngine.GameObject _Laser;

	public global::UnityEngine.GameObject _Gravity;

	public global::UnityEngine.Transform pos_Fire;

	public global::UnityEngine.Transform pos_Gravity;

	public global::UnityEngine.Transform[] pos_Laser;

	public global::UnityEngine.GameObject Clear_Weapon;

	public global::UnityEngine.GameObject Clear_Skill;

	public global::UnityEngine.GameObject Clear_Sample;

	public global::UnityEngine.Transform pos_Weapon;

	public global::UnityEngine.Transform pos_Skill;

	public global::UnityEngine.Transform pos_Sample;

	public global::UnityEngine.Transform pos_BrainGirl;

	public global::UnityEngine.GameObject _SoundLaser1;

	public global::UnityEngine.GameObject _SoundLaser2;

	public global::UnityEngine.GameObject _SoundBeat;

	public global::UnityEngine.GameObject _SoundElvStop;

	private global::UnityEngine.GameObject Sound_Laser_1;

	private global::UnityEngine.GameObject Sound_Laser_2;

	private float SoundBeat_Timer;

	private float SoundBeat_Point = 2.2f;

	private bool onScar;

	private bool onChestBurster;

	public global::UnityEngine.GameObject Scar;

	public global::UnityEngine.GameObject ChestBurster;

	public global::UnityEngine.GameObject _Mon_30;

	private float Mon_30_Timer = -5f;

	private global::UnityEngine.Transform[] Mon_30_List;

	private UI_Control UC;

	private global::UnityEngine.Animator animator;

	private Monster Mon;

	private Player_Control PC;

	private Sound_Control SC;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		SC = global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>();
		PC = global::UnityEngine.GameObject.Find("Player").GetComponent<Player_Control>();
		Mon = GetComponent<Monster>();
		animator = GetComponent<global::UnityEngine.Animator>();
		UC = global::UnityEngine.GameObject.Find("Status").GetComponent<UI_Control>();
		pos_Orig = base.transform.position;
		pos_Death = new global::UnityEngine.Vector3(pos_Orig.x, pos_Orig.y - 30f, 0f);
		global::UnityEngine.SpriteRenderer eye_Glow_ = Eye_Glow_1;
		global::UnityEngine.Color color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
		Eye_Glow_2.color = color;
		eye_Glow_.color = color;
		Attack_Delay = 8f;
		if (onEvent)
		{
			base.transform.position = new global::UnityEngine.Vector3(pos_Orig.x, pos_Orig.y - 20f, 0f);
			Mother_Arm = global::UnityEngine.Object.Instantiate(Arm_Obj, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
			Mother_Arm.GetComponent<AI_MotherArm>().MotherBrain = GetComponent<AI_MotherBrain>();
			Mother_Arm.GetComponent<AI_MotherArm>().onEvent = true;
			animator.SetTrigger("onDisable");
		}
		else
		{
			Mother_Arm = global::UnityEngine.Object.Instantiate(Arm_Obj, new global::UnityEngine.Vector3(pos_Orig.x, pos_Orig.y - 20f, 0f), base.transform.rotation) as global::UnityEngine.GameObject;
			Mother_Arm.GetComponent<AI_MotherArm>().MotherBrain = GetComponent<AI_MotherBrain>();
			Mother_Arm.GetComponent<AI_MotherArm>().onEvent = false;
			Mother_Arm.transform.parent = base.transform.parent;
			colorGlow = new global::UnityEngine.Color(1f, 0f, 0f);
			colorGlowBox = new global::UnityEngine.Color(1f, 0f, 0f);
		}
		global::UnityEngine.SpriteRenderer eye_Glow_2 = Eye_Glow_1;
		color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
		Eye_Black.color = color;
		eye_Glow_2.color = color;
		Eye_Glow_2.color = new global::UnityEngine.Color(0f, 0.25f, 0.25f, 0f);
		Brain_Glow_1.color = new global::UnityEngine.Color(1f, 0f, 0f, 0f);
		Brain_Glow_2.color = new global::UnityEngine.Color(1f, 0f, 0f, 0f);
		Mon_30_List = new global::UnityEngine.Transform[3];
		if (GM.Get_Event(14))
		{
			base.transform.position = new global::UnityEngine.Vector3(pos_Orig.x, pos_Orig.y - 10f, 0f);
			Make_Item();
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
		else
		{
			UC.Boss_Mon = Mon;
			UC.Set_Boss_Start();
		}
		UC.Boss_Mon = Mon;
		UC.Set_Boss_Start();
	}

	private void Show_Scar()
	{
		onScar = true;
		Scar.GetComponent<global::UnityEngine.SkinnedMeshRenderer>().enabled = true;
	}

	private void Show_ChestBurster()
	{
		onChestBurster = true;
		ChestBurster.GetComponent<global::UnityEngine.SkinnedMeshRenderer>().enabled = true;
	}

	private void Update()
	{
		if (onEvent)
		{
			if (Event_Num == 0)
			{
				if (Ani_Num != 0)
				{
					Ani_Num = 0;
					animator.SetTrigger("onDisable");
				}
			}
			else if (Event_Num == 1)
			{
				if (base.transform.position.y < pos_Orig.y)
				{
					base.transform.position = new global::UnityEngine.Vector3(base.transform.position.x, base.transform.position.y + global::UnityEngine.Time.deltaTime * 2.5f, 0f);
					base.GetComponent<UnityEngine.AudioSource>().volume = global::UnityEngine.Mathf.Lerp(base.GetComponent<UnityEngine.AudioSource>().volume, 0.5f, global::UnityEngine.Time.deltaTime);
					if (!base.GetComponent<UnityEngine.AudioSource>().isPlaying)
					{
						base.GetComponent<UnityEngine.AudioSource>().Play();
					}
				}
				else
				{
					Life_Timer += global::UnityEngine.Time.deltaTime;
					if (base.GetComponent<UnityEngine.AudioSource>().volume > 0f)
					{
						base.GetComponent<UnityEngine.AudioSource>().volume = 0f;
						base.GetComponent<UnityEngine.AudioSource>().Stop();
						global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_SoundElvStop, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
					}
					if (Life_Timer > 1f && Ani_Num != 1)
					{
						Ani_Num = 1;
						global::UnityEngine.GameObject.Find("Dialogue").SendMessage("MotherBrainArrived");
					}
				}
			}
			else if (Event_Num == 2)
			{
				if (Ani_Num != 2)
				{
					Ani_Num = 2;
					animator.SetTrigger("onStart");
				}
				Glow_Timer += global::UnityEngine.Time.deltaTime;
				if (Glow_Timer > 5f)
				{
					colorGlow = global::UnityEngine.Color.Lerp(colorGlow, new global::UnityEngine.Color(1f, 0f, 0f), global::UnityEngine.Time.deltaTime);
					colorGlowBox = global::UnityEngine.Color.Lerp(colorGlowBox, new global::UnityEngine.Color(1f, 0f, 0f), global::UnityEngine.Time.deltaTime);
					PipeColor(Glow_Timer);
				}
				else
				{
					SR_PipeGlow[0].color = global::UnityEngine.Color.Lerp(SR_PipeGlow[0].color, new global::UnityEngine.Color(0f, 1f, 0f, 0.2f), global::UnityEngine.Time.deltaTime);
					for (int i = 1; i < SR_PipeGlow.Length; i++)
					{
						SR_PipeGlow[i].color = SR_PipeGlow[0].color;
					}
					SR_GlowBox[0].color = global::UnityEngine.Color.Lerp(SR_GlowBox[0].color, new global::UnityEngine.Color(0f, 1f, 0f, 0.5f), global::UnityEngine.Time.deltaTime);
					for (int j = 1; j < SR_GlowBox.Length; j++)
					{
						SR_GlowBox[j].color = SR_GlowBox[0].color;
					}
				}
			}
			else if (Event_Num != 3)
			{
				if (Event_Num == 4)
				{
					if (Mother_Arm != null)
					{
						Mother_Arm.transform.position = global::UnityEngine.Vector3.Lerp(Mother_Arm.transform.position, pos_Orig, global::UnityEngine.Time.deltaTime * 1f);
						if (Ani_Num != 4)
						{
							Ani_Num = 4;
							Mother_Arm.SendMessage("Hold_Release");
						}
					}
				}
				else if (Event_Num == 5)
				{
					if (Ani_Num != 5)
					{
						Ani_Num = 5;
						Set_Attack_Laser_5();
					}
				}
				else if (Event_Num == 6)
				{
					if (Ani_Num != 6)
					{
						Ani_Num = 6;
						animator.SetTrigger("onRedAlert");
					}
					SoundBeat_Point = global::UnityEngine.Mathf.Lerp(SoundBeat_Point, 0.2f, global::UnityEngine.Time.deltaTime * 2f);
				}
				else if (Event_Num == 7 && Ani_Num != 7)
				{
					Ani_Num = 7;
					Show_ChestBurster();
					global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Explo(base.transform.position);
					global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_9_Damage(base.transform.position);
					for (int k = 0; k < 3; k++)
					{
						Make_Explo(explo_Pos[k]);
					}
				}
			}
			if (Event_Num > 1 && Event_Num < 7)
			{
				SoundBeat_Timer += global::UnityEngine.Time.deltaTime;
				if (SoundBeat_Timer > SoundBeat_Point)
				{
					SoundBeat_Timer = 0f;
					global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(_SoundBeat, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
				}
			}
			if (Event_Num > 2)
			{
				Glow_Timer += global::UnityEngine.Time.deltaTime;
				colorGlow = global::UnityEngine.Color.Lerp(colorGlow, new global::UnityEngine.Color(1f, 0f, 0f), global::UnityEngine.Time.deltaTime);
				colorGlowBox = global::UnityEngine.Color.Lerp(colorGlowBox, new global::UnityEngine.Color(1f, 0f, 0f), global::UnityEngine.Time.deltaTime);
				PipeColor(Glow_Timer);
				EyeGlow_Show_Red();
				BrainGlow_Show();
			}
			if (Event_Num > 1)
			{
				Brain_Glow_1.color = global::UnityEngine.Color.Lerp(Brain_Glow_1.color, new global::UnityEngine.Color(1f, 0f, 0f, 0.25f), global::UnityEngine.Time.deltaTime * 0.4f);
				Brain_Glow_2.color = global::UnityEngine.Color.Lerp(Brain_Glow_2.color, new global::UnityEngine.Color(1f, 0f, 0f, 0.5f), global::UnityEngine.Time.deltaTime * 0.4f);
			}
			if (Attack_Timer > 0f)
			{
				Attack_Timer -= global::UnityEngine.Time.deltaTime;
			}
			if (Attack_Timer < 0.07f)
			{
				if (Sound_Laser_1 != null)
				{
					global::UnityEngine.Object.Destroy(Sound_Laser_1.gameObject);
				}
				if (Sound_Laser_2 != null)
				{
					global::UnityEngine.Object.Destroy(Sound_Laser_2.gameObject);
				}
			}
			if (onAttack_Laser_5)
			{
				Ani_Laser_5();
			}
		}
		else
		{
			if (GM.Paused)
			{
				return;
			}
			Life_Timer += global::UnityEngine.Time.deltaTime;
			PipeColor(Life_Timer);
			if (!onScar)
			{
				Show_Scar();
			}
			if (!onArmStart && Life_Timer > 4f)
			{
				onArmStart = true;
				Mother_Arm.SendMessage("Hold_Release");
			}
			if (Life_Timer > 1.2f)
			{
				Mother_Arm.transform.position = global::UnityEngine.Vector3.Lerp(Mother_Arm.transform.position, pos_Orig, global::UnityEngine.Time.deltaTime * 1f);
			}
			if (Attack_Timer > 0f)
			{
				Attack_Timer -= global::UnityEngine.Time.deltaTime;
			}
			if (Attack_Timer < 0.07f)
			{
				if (Sound_Laser_1 != null)
				{
					global::UnityEngine.Object.Destroy(Sound_Laser_1.gameObject);
				}
				if (Sound_Laser_2 != null)
				{
					global::UnityEngine.Object.Destroy(Sound_Laser_2.gameObject);
				}
			}
			if (isDeath)
			{
				Death_Timer += global::UnityEngine.Time.deltaTime;
				EyeGlow_Show_Red();
				BrainGlow_Show();
				base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, pos_Death, global::UnityEngine.Time.deltaTime * 0.2f);
				if (Mother_Arm != null)
				{
					Mother_Arm.transform.position = base.transform.position;
				}
				if (Death_Timer < 5f)
				{
					DeathExplo();
				}
				else if (global::UnityEngine.Vector3.Distance(base.transform.position, pos_Death) < 3f)
				{
					if (Mother_Arm != null)
					{
						global::UnityEngine.Object.Destroy(Mother_Arm.gameObject);
					}
					global::UnityEngine.Object.Destroy(base.gameObject);
				}
				if (!onClearItem && base.transform.position.y < pos_Orig.y - 10f)
				{
					Make_Item();
				}
				return;
			}
			if (onAttack_Fire)
			{
				Ani_Fire();
			}
			else if (onAttack_Laser)
			{
				Ani_Laser();
			}
			else if (onAttack_Gravity)
			{
				Ani_Gravity();
			}
			else if (onAttack_Laser_5)
			{
				Ani_Laser_5();
			}
			else if (!GM.GameOver)
			{
				Attack_Delay -= global::UnityEngine.Time.deltaTime;
				if (Attack_Level == 1)
				{
					if (onAttack_Fire || Attack_Delay < 0.3f)
					{
						EyeGlow_Show_Red();
					}
					else
					{
						EyeGlow_Hide_Red();
					}
					if (Attack_Delay <= 0f)
					{
						Set_Attack_Fire();
					}
					if (Mon.HP_Ratio < 0.75f)
					{
						Attack_Level = 2;
						Attack_Delay = 1f;
					}
				}
				else if (Attack_Level == 2)
				{
					if (onAttack_Laser || Attack_Delay < 0.4f)
					{
						EyeGlow_Show_Red();
					}
					else
					{
						EyeGlow_Hide_Red();
					}
					if (Attack_Delay <= 0f)
					{
						Set_Attack_Laser();
					}
					if (Mon.HP_Ratio < 0.5f)
					{
						Attack_Level = 3;
						Attack_Delay = 1f;
					}
				}
				else if (Attack_Level == 3)
				{
					Eye_Glow_1.transform.localScale = global::UnityEngine.Vector3.Lerp(Eye_Glow_1.transform.localScale, new global::UnityEngine.Vector3(0.03f, 0.03f, 1f), global::UnityEngine.Time.deltaTime);
					if (onAttack_Gravity || Attack_Delay < 0.9f)
					{
						EyeGlow_Show_Black();
					}
					else
					{
						EyeGlow_Hide_Black();
					}
					if (Attack_Delay <= 0f)
					{
						Set_Attack_Gravity();
					}
					if (Mon.HP_Ratio < 0.2f)
					{
						Attack_Level = 4;
						Attack_Delay = 1f;
					}
				}
				else if (Attack_Level == 4)
				{
					if (!global::UnityEngine.GameObject.Find("RedAlert").GetComponent<global::UnityEngine.SpriteRenderer>().enabled)
					{
						global::UnityEngine.GameObject.Find("Dialogue").SendMessage("ON_RedAlert");
					}
					Eye_Glow_1.transform.localScale = global::UnityEngine.Vector3.Lerp(Eye_Glow_1.transform.localScale, new global::UnityEngine.Vector3(0.06f, 0.06f, 1f), global::UnityEngine.Time.deltaTime);
					EyeGlow_Show_Red();
					if (Attack_Delay < 1f)
					{
						BrainGlow_Show();
					}
					if (Attack_Delay <= 0f)
					{
						Set_Attack_Laser_5();
					}
				}
				if (Attack_Level < 3 || GM.onHscene)
				{
					Mon_30_Timer += global::UnityEngine.Time.deltaTime;
					if (Mon_30_Timer > 10f)
					{
						if (Search_BrainGirl() < 3)
						{
							Make_BrainGirl();
						}
						else
						{
							Mon_30_Timer = 4f;
						}
					}
				}
			}
			else
			{
				Mon_30_Timer += global::UnityEngine.Time.deltaTime;
				if (Mon_30_Timer > 10f)
				{
					if (Search_BrainGirl() < 3)
					{
						Make_BrainGirl();
					}
					else
					{
						Mon_30_Timer = 4f;
					}
				}
			}
			if (facingRight < 0 && PC.transform.position.x > base.transform.position.x)
			{
				Flip_Timer += global::UnityEngine.Time.deltaTime;
				if (Flip_Timer > 2f && !onAttack_Fire && !onAttack_Laser && !onAttack_Gravity && !onAttack_Laser_5)
				{
					Flip();
				}
			}
			else if (facingRight > 0 && PC.transform.position.x < base.transform.position.x)
			{
				Flip_Timer += global::UnityEngine.Time.deltaTime;
				if (Flip_Timer > 2f && !onAttack_Fire && !onAttack_Laser && !onAttack_Gravity && !onAttack_Laser_5)
				{
					Flip();
				}
			}
			else
			{
				Flip_Timer = 0f;
			}
			SoundBeat_Timer += global::UnityEngine.Time.deltaTime;
			if (Mon.HP_Ratio > 0.2f)
			{
				if (SoundBeat_Timer > 1.5f)
				{
					SoundBeat_Timer = 0f;
					global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(_SoundBeat, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
				}
			}
			else if (SoundBeat_Timer > 0.7f)
			{
				SoundBeat_Timer = 0f;
				global::UnityEngine.GameObject gameObject4 = global::UnityEngine.Object.Instantiate(_SoundBeat, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
			}
		}
	}

	private void EyeGlow_Show_Red()
	{
		Eye_Glow_1.color = global::UnityEngine.Color.Lerp(Eye_Glow_1.color, new global::UnityEngine.Color(1f, 1f, 1f, 1f), global::UnityEngine.Time.deltaTime * 4f);
		Eye_Glow_2.color = global::UnityEngine.Color.Lerp(Eye_Glow_2.color, new global::UnityEngine.Color(1f, 0.25f, 0.25f, 1f), global::UnityEngine.Time.deltaTime * 4f);
		Eye_Black.color = global::UnityEngine.Color.Lerp(Eye_Black.color, new global::UnityEngine.Color(1f, 1f, 1f, 0f), global::UnityEngine.Time.deltaTime * 4f);
	}

	private void EyeGlow_Hide_Red()
	{
		Eye_Glow_1.color = global::UnityEngine.Color.Lerp(Eye_Glow_1.color, new global::UnityEngine.Color(1f, 1f, 1f, 0f), global::UnityEngine.Time.deltaTime * 4f);
		Eye_Glow_2.color = global::UnityEngine.Color.Lerp(Eye_Glow_2.color, new global::UnityEngine.Color(1f, 0.25f, 0.25f, 0f), global::UnityEngine.Time.deltaTime * 4f);
		Eye_Black.color = global::UnityEngine.Color.Lerp(Eye_Black.color, new global::UnityEngine.Color(1f, 1f, 1f, 0f), global::UnityEngine.Time.deltaTime * 4f);
	}

	private void EyeGlow_Show_Black()
	{
		Eye_Glow_1.color = global::UnityEngine.Color.Lerp(Eye_Glow_1.color, new global::UnityEngine.Color(1f, 1f, 1f, 1f), global::UnityEngine.Time.deltaTime * 8f);
		Eye_Glow_2.color = global::UnityEngine.Color.Lerp(Eye_Glow_2.color, new global::UnityEngine.Color(0f, 0f, 0f, 1f), global::UnityEngine.Time.deltaTime * 8f);
		Eye_Black.color = global::UnityEngine.Color.Lerp(Eye_Black.color, new global::UnityEngine.Color(1f, 1f, 1f, 1f), global::UnityEngine.Time.deltaTime * 8f);
	}

	private void EyeGlow_Hide_Black()
	{
		Eye_Glow_1.color = global::UnityEngine.Color.Lerp(Eye_Glow_1.color, new global::UnityEngine.Color(0f, 0f, 0f, 0f), global::UnityEngine.Time.deltaTime * 8f);
		Eye_Glow_2.color = global::UnityEngine.Color.Lerp(Eye_Glow_2.color, new global::UnityEngine.Color(0f, 0f, 0f, 0f), global::UnityEngine.Time.deltaTime * 8f);
		Eye_Black.color = global::UnityEngine.Color.Lerp(Eye_Black.color, new global::UnityEngine.Color(1f, 1f, 1f, 0f), global::UnityEngine.Time.deltaTime * 8f);
	}

	private void BrainGlow_Show()
	{
		Brain_Glow_1.color = global::UnityEngine.Color.Lerp(Brain_Glow_1.color, new global::UnityEngine.Color(1f, 0f, 0f, 0.25f), global::UnityEngine.Time.deltaTime * 5f);
		Brain_Glow_2.color = global::UnityEngine.Color.Lerp(Brain_Glow_2.color, new global::UnityEngine.Color(1f, 0f, 0f, 0.5f), global::UnityEngine.Time.deltaTime * 5f);
	}

	private void BrainGlow_Hide()
	{
		Brain_Glow_1.color = global::UnityEngine.Color.Lerp(Brain_Glow_1.color, new global::UnityEngine.Color(1f, 0f, 0f, 0f), global::UnityEngine.Time.deltaTime);
		Brain_Glow_2.color = global::UnityEngine.Color.Lerp(Brain_Glow_2.color, new global::UnityEngine.Color(1f, 0f, 0f, 0f), global::UnityEngine.Time.deltaTime);
	}

	private void PipeColor(float timer)
	{
		SR_PipeGlow[0].color = new global::UnityEngine.Color(colorGlow.r, colorGlow.g, colorGlow.b, 0f + (1f + global::UnityEngine.Mathf.Sin(timer * 5f)) * 0.1f);
		for (int i = 1; i < SR_PipeGlow.Length; i++)
		{
			SR_PipeGlow[i].color = SR_PipeGlow[0].color;
		}
		SR_GlowBox[0].color = new global::UnityEngine.Color(colorGlowBox.r, colorGlowBox.g, colorGlowBox.b, 0.05f + (1f + global::UnityEngine.Mathf.Sin(timer * 5f)) * 0.3f);
		for (int j = 1; j < SR_GlowBox.Length; j++)
		{
			SR_GlowBox[j].color = SR_GlowBox[0].color;
		}
	}

	private void Flip()
	{
		Check_Idle();
		facingRight = -facingRight;
		Mon.Flip();
	}

	private void Check_Idle()
	{
		if (animator.GetBool("onAttack") || animator.GetBool("onHit_Back") || animator.GetBool("onHit"))
		{
			Set_Idle();
		}
	}

	private void Set_Idle()
	{
		Mon.isLockHit = false;
		animator.SetBool("onAttack", false);
		animator.SetBool("onHit", false);
		animator.SetBool("onHit_Back", false);
	}

	private void Set_Attack_Fire()
	{
		onAttack_Fire = true;
		Attack_Timer = 5f;
		isLaunched = false;
		Mon.isLockHit = true;
		animator.SetBool("onAttack", true);
		Fire_Num = 0;
		FireNum_Timer = 0f;
		animator.SetBool("onHit", false);
		animator.SetBool("onHit_Back", false);
	}

	private void Set_Attack_Laser()
	{
		onAttack_Laser = true;
		Attack_Timer = 3f;
		isLaunched = false;
		Mon.isLockHit = true;
		animator.SetBool("onAttack", true);
		animator.SetBool("onHit", false);
		animator.SetBool("onHit_Back", false);
		if (Sound_Laser_1 != null)
		{
			global::UnityEngine.Object.Destroy(Sound_Laser_1.gameObject);
		}
		if (Sound_Laser_2 != null)
		{
			global::UnityEngine.Object.Destroy(Sound_Laser_2.gameObject);
		}
		Sound_Laser_1 = global::UnityEngine.Object.Instantiate(_SoundLaser1, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
		Sound_Laser_2 = global::UnityEngine.Object.Instantiate(_SoundLaser2, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
	}

	private void Set_Attack_Gravity()
	{
		onAttack_Gravity = true;
		Attack_Timer = 0.4f;
		isLaunched = false;
		Mon.isLockHit = true;
		animator.SetBool("onAttack", true);
		animator.SetBool("onHit", false);
		animator.SetBool("onHit_Back", false);
	}

	private void Set_Attack_Laser_5()
	{
		onAttack_Laser_5 = true;
		Attack_Timer = 3f;
		isLaunched = false;
		Mon.isLockHit = true;
		if (animator.GetBool("onHit") || animator.GetBool("onHit_Back"))
		{
			animator.Play("Attack", 0, 0f);
		}
		animator.SetBool("onAttack", true);
		animator.SetBool("onHit", false);
		animator.SetBool("onHit_Back", false);
		if (Sound_Laser_1 != null)
		{
			global::UnityEngine.Object.Destroy(Sound_Laser_1.gameObject);
		}
		if (Sound_Laser_2 != null)
		{
			global::UnityEngine.Object.Destroy(Sound_Laser_2.gameObject);
		}
		Sound_Laser_1 = global::UnityEngine.Object.Instantiate(_SoundLaser1, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
		Sound_Laser_2 = global::UnityEngine.Object.Instantiate(_SoundLaser2, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
	}

	private void Ani_Fire()
	{
		if (Fire_Num < 24)
		{
			FireNum_Timer += global::UnityEngine.Time.deltaTime;
			if (FireNum_Timer > 0.1f)
			{
				Fire_Num++;
				FireNum_Timer = 0f;
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Fire, pos_Fire.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 90 + 15 * Fire_Num * facingRight)) as global::UnityEngine.GameObject;
				SC.Boss_4_Fire(pos_Fire.position);
			}
		}
		else
		{
			End_Attack();
		}
	}

	private void Ani_Laser()
	{
		if (!isLaunched && Attack_Timer < 2.9f)
		{
			isLaunched = true;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Laser, pos_Laser[0].position, global::UnityEngine.Quaternion.Euler(0f, 0f, 80 * -facingRight)) as global::UnityEngine.GameObject;
			gameObject.GetComponent<Mother_Laser>().MotherBrain = GetComponent<AI_MotherBrain>();
			gameObject.transform.localScale = new global::UnityEngine.Vector3(-facingRight, 1f, 1f);
			gameObject.transform.parent = base.transform;
		}
		if (Attack_Timer <= 0f)
		{
			End_Attack();
		}
	}

	private void Ani_Gravity()
	{
		if (!isLaunched && Attack_Timer < 0.25f)
		{
			isLaunched = true;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Gravity, pos_Gravity.position, base.transform.rotation) as global::UnityEngine.GameObject;
		}
		if (Attack_Timer <= 0f)
		{
			End_Attack();
		}
	}

	private void Ani_Laser_5()
	{
		if (!isLaunched && Attack_Timer < 2.9f)
		{
			isLaunched = true;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Laser, pos_Laser[0].position, global::UnityEngine.Quaternion.Euler(0f, 0f, 80 * -facingRight)) as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(_Laser, pos_Laser[1].position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(_Laser, pos_Laser[2].position, global::UnityEngine.Quaternion.Euler(0f, 0f, 300 * -facingRight)) as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject gameObject4 = global::UnityEngine.Object.Instantiate(_Laser, pos_Laser[3].position, global::UnityEngine.Quaternion.Euler(0f, 0f, 240 * -facingRight)) as global::UnityEngine.GameObject;
			global::UnityEngine.GameObject gameObject5 = global::UnityEngine.Object.Instantiate(_Laser, pos_Laser[4].position, global::UnityEngine.Quaternion.Euler(0f, 0f, 180 * -facingRight)) as global::UnityEngine.GameObject;
			gameObject.GetComponent<Mother_Laser>().MotherBrain = GetComponent<AI_MotherBrain>();
			gameObject2.GetComponent<Mother_Laser>().MotherBrain = GetComponent<AI_MotherBrain>();
			gameObject3.GetComponent<Mother_Laser>().MotherBrain = GetComponent<AI_MotherBrain>();
			gameObject4.GetComponent<Mother_Laser>().MotherBrain = GetComponent<AI_MotherBrain>();
			gameObject5.GetComponent<Mother_Laser>().MotherBrain = GetComponent<AI_MotherBrain>();
			gameObject.transform.localScale = new global::UnityEngine.Vector3(-facingRight, 1f, 1f);
			gameObject2.transform.localScale = new global::UnityEngine.Vector3(-facingRight, 1f, 1f);
			gameObject3.transform.localScale = new global::UnityEngine.Vector3(-facingRight, 1f, 1f);
			gameObject4.transform.localScale = new global::UnityEngine.Vector3(-facingRight, 1f, 1f);
			gameObject5.transform.localScale = new global::UnityEngine.Vector3(-facingRight, 1f, 1f);
			gameObject.transform.parent = base.transform;
			gameObject2.transform.parent = base.transform;
			gameObject3.transform.parent = base.transform;
			gameObject4.transform.parent = base.transform;
			gameObject5.transform.parent = base.transform;
		}
		if (Attack_Timer <= 0f)
		{
			End_Attack();
		}
	}

	private void End_Attack()
	{
		onAttack_Fire = false;
		onAttack_Laser = false;
		onAttack_Gravity = false;
		onAttack_Laser_5 = false;
		Mon.isLockHit = false;
		animator.SetBool("onAttack", false);
		animator.SetBool("onHit", false);
		animator.SetBool("onHit_Back", false);
		if (Sound_Laser_1 != null)
		{
			global::UnityEngine.Object.Destroy(Sound_Laser_1.gameObject);
		}
		if (Sound_Laser_2 != null)
		{
			global::UnityEngine.Object.Destroy(Sound_Laser_2.gameObject);
		}
		if (Attack_Level == 1)
		{
			Attack_Delay = 6f;
		}
		else if (Attack_Level == 2)
		{
			Attack_Delay = 4f;
		}
		else if (Attack_Level == 3)
		{
			Attack_Delay = 9f;
		}
		else if (Attack_Level == 4)
		{
			Attack_Delay = 2f;
		}
	}

	private int Search_BrainGirl()
	{
		int num = 0;
		for (int i = 0; i < Mon_30_List.Length; i++)
		{
			if (Mon_30_List[i] != null)
			{
				num++;
			}
		}
		return num;
	}

	private void Make_BrainGirl()
	{
		global::UnityEngine.Vector3 position = new global::UnityEngine.Vector3(pos_BrainGirl.position.x, pos_BrainGirl.position.y, 0f);
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Mon_30, position, base.transform.rotation) as global::UnityEngine.GameObject;
		gameObject.GetComponent<AI_Mon_BrainGirl>().event_Type = AI_Mon_BrainGirl.Event_Type.MotherBrain;
		gameObject.transform.parent = base.transform.parent;
		int index = 0;
		for (int i = 0; i < Mon_30_List.Length; i++)
		{
			if (Mon_30_List[i] == null)
			{
				Mon_30_List[i] = gameObject.transform;
				index = i;
				break;
			}
		}
		gameObject.GetComponent<Mon_Index>().Index = index;
		if (Mon.Get_Poison())
		{
			gameObject.SendMessage("Set_BrainGirl_Poison");
		}
		if (Mon.Get_Slow())
		{
			gameObject.SendMessage("Set_BrainGirl_Slow");
		}
		Mon_30_Timer = 0f;
	}

	public void Set_Death()
	{
		isDeath = true;
		Death_Timer = 0f;
		animator.SetTrigger("onDeath");
		GetComponent<Mon_Index>().Set_UserColor(new global::UnityEngine.Color(1f, 0f, 0f, 1f));
		Mother_Arm.GetComponent<Mon_Index>().Set_UserColor(new global::UnityEngine.Color(1f, 0f, 0f, 1f));
		Mother_Arm.GetComponent<AI_MotherArm>().isDeath = true;
		Mother_Arm.SendMessage("Hold");
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_9_Damage(base.transform.position);
		DeathExplo();
		for (int i = 0; i < Mon_30_List.Length; i++)
		{
			if (Mon_30_List[i] != null)
			{
				Mon_30_List[i].SendMessage("Death");
			}
		}
		if (global::UnityEngine.GameObject.Find("RedAlert").GetComponent<global::UnityEngine.SpriteRenderer>().enabled)
		{
			global::UnityEngine.GameObject.Find("Dialogue").SendMessage("Event_Boss4_Death");
		}
	}

	private void DeathExplo()
	{
		if (ExploSound_Timer <= 0f)
		{
			ExploSound_Timer = global::UnityEngine.Random.Range(0.2f, 0.5f);
			SC.Mon_Explo(base.transform.position);
		}
		else
		{
			ExploSound_Timer -= global::UnityEngine.Time.deltaTime;
		}
		for (int i = 0; i < explo_Pos.Length; i++)
		{
			if (Explo_Timer[i] <= 0f)
			{
				Explo_Timer[i] = global::UnityEngine.Random.Range(0.1f, 0.8f);
				Make_Explo(explo_Pos[i]);
			}
			else
			{
				Explo_Timer[i] -= global::UnityEngine.Time.deltaTime;
			}
		}
	}

	private void Make_Explo(global::UnityEngine.Transform posObj)
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Explo, posObj.position, posObj.transform.rotation) as global::UnityEngine.GameObject;
		gameObject.transform.localScale = posObj.transform.localScale;
	}

	private void MotherBrain_Del()
	{
		if (Sound_Laser_1 != null)
		{
			global::UnityEngine.Object.Destroy(Sound_Laser_1.gameObject);
		}
		if (Sound_Laser_2 != null)
		{
			global::UnityEngine.Object.Destroy(Sound_Laser_2.gameObject);
		}
		if (Mother_Arm != null)
		{
			global::UnityEngine.Object.Destroy(Mother_Arm.gameObject);
		}
		global::UnityEngine.Object.Destroy(base.gameObject);
	}

	private void Make_Item()
	{
		onClearItem = true;
		if (!GM.onWeapon_5)
		{
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Clear_Weapon, pos_Weapon.position, base.transform.rotation) as global::UnityEngine.GameObject;
			gameObject.transform.parent = base.transform.parent;
			gameObject.GetComponent<Item>().Set_Target(new global::UnityEngine.Vector3(pos_Weapon.position.x, pos_Weapon.position.y + 11f, 0f));
		}
		if (!GM.onSkill_5)
		{
			global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(Clear_Skill, pos_Skill.position, base.transform.rotation) as global::UnityEngine.GameObject;
			gameObject2.transform.parent = base.transform.parent;
			gameObject2.GetComponent<Item>().Set_Target(new global::UnityEngine.Vector3(pos_Skill.position.x, pos_Skill.position.y + 10f, 0f));
		}
		if (!GM.Get_Event(2))
		{
			global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(Clear_Sample, pos_Sample.position, base.transform.rotation) as global::UnityEngine.GameObject;
			gameObject3.transform.parent = base.transform.parent;
			gameObject3.GetComponent<Item>().Set_Target(new global::UnityEngine.Vector3(pos_Sample.position.x, pos_Sample.position.y + 13f, 0f));
		}
	}

	private void Set_AttackDelay()
	{
	}
}
