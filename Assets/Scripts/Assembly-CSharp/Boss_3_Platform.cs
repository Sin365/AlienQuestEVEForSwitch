using UnityEngine;

public class Boss_3_Platform : global::UnityEngine.MonoBehaviour
{
	private float Life_Timer;

	private float distance;

	private float dist_X;

	private float dist_Y;

	private float dist_Timer;

	private float out_Timer;

	private bool on_Active;

	private float Action_Timer;

	private int Attack_Num;

	private float Attack_Delay;

	private bool on_Check;

	private bool check_1;

	private bool check_2;

	public global::UnityEngine.Transform TR_1_L;

	public global::UnityEngine.Transform TR_1_R;

	public global::UnityEngine.Transform TR_2_L;

	public global::UnityEngine.Transform TR_2_R;

	private bool on_Hscene;

	private float H_Timer;

	public global::UnityEngine.GameObject[] H_Single;

	public global::UnityEngine.GameObject[] Object_List;

	private bool onPause;

	private global::UnityEngine.Animator animator;
    Player_Control PC => GameManager.instance.PC;
    GameObject Player => GameManager.instance.gobj_Player;

    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//Player = global::UnityEngine.GameObject.Find("Player");
		//PC = Player.GetComponent<Player_Control>();
		animator = GetComponent<global::UnityEngine.Animator>();
	}

	private void Update()
	{
		if (GM.Paused)
		{
			if (!onPause)
			{
				onPause = true;
				GetComponent<global::UnityEngine.Animator>().speed = 0f;
			}
		}
		else if (onPause)
		{
			onPause = false;
			GetComponent<global::UnityEngine.Animator>().speed = 1f;
		}
		if (GM.onEvent || on_Hscene)
		{
			if (GM.onHscene && !on_Hscene && on_Active)
			{
				Set_Hide();
				dist_Timer = 0f;
				out_Timer = 0f;
			}
		}
		else
		{
			if (GM.Paused)
			{
				return;
			}
			Life_Timer += global::UnityEngine.Time.deltaTime;
			dist_X = global::UnityEngine.Mathf.Abs(base.transform.position.x - Player.transform.position.x);
			dist_Y = global::UnityEngine.Mathf.Abs(base.transform.position.y - Player.transform.position.y);
			if (Player.transform.position.y > base.transform.position.y && dist_Y < 0.8f && dist_X < 2.5f)
			{
				dist_Timer += global::UnityEngine.Time.deltaTime;
				out_Timer = 0f;
			}
			else
			{
				dist_Timer = 0f;
				out_Timer += global::UnityEngine.Time.deltaTime;
			}
			if (on_Active && on_Check)
			{
				check_1 = global::UnityEngine.Physics2D.Linecast(TR_1_L.position, TR_1_R.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player_Dmg"));
				check_2 = global::UnityEngine.Physics2D.Linecast(TR_2_L.position, TR_2_R.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Player_Dmg"));
			}
			else
			{
				check_1 = false;
				check_2 = false;
			}
			if (H_Timer > 0f)
			{
				H_Timer -= global::UnityEngine.Time.deltaTime;
			}
			else if (check_1 || check_2)
			{
				on_Check = false;
				if (GM.GameOver && GM.Hscene_Num == 0 && GM.Hscene_Timer <= 0f)
				{
					Start_Hscene();
				}
				else
				{
					if (GM.onShield)
					{
						return;
					}
					if (GM.onCloth)
					{
						GM.Damage(1, 0f, true, 10);
						GM.onCloth = false;
						PC.OnOff_Cloth();
					}
					else if (GM.Hscene_Num == 0 && GM.Hscene_Timer <= 0f)
					{
						if (PC.grounded_Now && (PC.State == Player_Control.AniState.Idle || PC.State == Player_Control.AniState.Run || PC.State == Player_Control.AniState.Sit || PC.State == Player_Control.AniState.Down))
						{
							Start_Hscene();
						}
						else
						{
							GM.Damage(1, 0f, true, 10);
						}
					}
					else
					{
						GM.Damage(1, 0f, true, 10);
					}
				}
			}
			else if (Attack_Delay > 0f)
			{
				Attack_Delay -= global::UnityEngine.Time.deltaTime;
			}
			else if (on_Active)
			{
				Action_Timer += global::UnityEngine.Time.deltaTime;
				if (out_Timer > 2f)
				{
					Set_Hide();
				}
				else
				{
					Set_Attack();
				}
			}
			else
			{
				Action_Timer += global::UnityEngine.Time.deltaTime;
				if (dist_Timer > 0.01f && Action_Timer > 1f)
				{
					Set_Idle();
				}
			}
		}
	}

	private void Set_Idle()
	{
		on_Active = true;
		Attack_Num = 0;
		Action_Timer = 0f;
		animator.SetBool("onShow", true);
		animator.SetBool("onAttack", false);
		animator.SetBool("onHide", false);
	}

	private void Set_Hide()
	{
		on_Active = false;
		Attack_Num = 0;
		Action_Timer = 0f;
		animator.SetBool("onShow", false);
		animator.SetBool("onAttack", false);
		animator.SetBool("onHide", true);
	}

	private void Set_Attack()
	{
		Attack_Delay = 2f;
		Attack_Num++;
		animator.SetBool("onAttack", true);
	}

	private void End_Attack()
	{
		on_Check = false;
		animator.SetBool("onAttack", false);
	}

	private void Check_Start()
	{
		on_Check = true;
	}

	private void Check_End()
	{
		on_Check = false;
	}

	private void Start_Hscene()
	{
		on_Hscene = true;
		GM.onEvent = true;
		GM.onHscene = true;
		GM.Hscene_Timer = 1f;
		H_Timer = 0.1f;
		int num = ((global::UnityEngine.Random.Range(0, 10) <= 5) ? 28 : 27);
		GM.Hscene_Num = num;
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(H_Single[num - 27], base.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
		gameObject.transform.parent = base.transform.parent;
		gameObject.GetComponent<H_Ani>().Mon_Object = base.gameObject;
		OnOff_Object(false);
		global::UnityEngine.GameObject.Find("Menu").GetComponent<Menu_Control>().H_Object = gameObject;
		global::UnityEngine.GameObject.Find("Main Camera").SendMessage("Hscene_Zoom");
		if (!GM.GameOver)
		{
			Player.SendMessage("H_Down");
		}
		global::UnityEngine.GameObject.Find("Ani").SendMessage("Start_H_Scene");
		GameManager.instance.sc_Sound_List.Player_Damage(30, false, base.transform.position);
	}

	private void End_Hscene()
	{
		on_Hscene = false;
		GM.onEvent = false;
		GM.onHscene = false;
		GM.Hscene_Num = 0;
		OnOff_Object(true);
		GM.Down_H_After();
		global::UnityEngine.GameObject.Find("Ani").SendMessage("End_H_Scene");
		Set_Hide();
	}

	public void OnOff_Object(bool isOnOff)
	{
		if (Object_List.Length <= 0)
		{
			return;
		}
		for (int i = 0; i < Object_List.Length; i++)
		{
			if (Object_List[i].GetComponent<global::UnityEngine.SpriteRenderer>() != null)
			{
				Object_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().enabled = isOnOff;
			}
			else
			{
				Object_List[i].GetComponent<global::UnityEngine.SkinnedMeshRenderer>().enabled = isOnOff;
			}
		}
	}
}
