public class H25_Control : global::UnityEngine.MonoBehaviour
{
	private int State = 1;

	private float Idle_Timer;

	private float Attack_Timer;

	private float Target_Speed = 1f;

	private float Speed = 1f;

	private global::UnityEngine.Vector3 Pos_Target;

	private H_Ani H_ani;

	private global::UnityEngine.Animator H25_Ani;

	public global::UnityEngine.Animator Mon_Ani;

	private void Start()
	{
		H_ani = GetComponent<H_Ani>();
		H25_Ani = GetComponent<global::UnityEngine.Animator>();
		Pos_Target = new global::UnityEngine.Vector3(base.transform.position.x, base.transform.position.y + 3f, 0f);
		Mon_Ani.SetTrigger("on_H_Start");
	}

	private void Update()
	{
		if (Idle_Timer > 0f)
		{
			Idle_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Attack_Timer > 0f)
		{
			Attack_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (H25_Ani.GetBool("isStopped"))
		{
			Target_Speed = 1f;
		}
		if (H25_Ani.GetInteger("State") == 0)
		{
			if (H25_Ani.GetBool("isStopped"))
			{
			}
			if (!H25_Ani.GetBool("isStopped"))
			{
				Target_Speed = H_ani.Speed;
			}
		}
		else if (H25_Ani.GetInteger("State") == 1)
		{
			if (State != 1)
			{
				State = 1;
				Mon_Ani.SetBool("onAttack", false);
			}
			if (!H25_Ani.GetBool("isStopped"))
			{
				Target_Speed = H_ani.Speed;
			}
		}
		else if (H25_Ani.GetInteger("State") == 2)
		{
			if (H25_Ani.GetBool("isStopped"))
			{
				if (State != 1)
				{
					State = 1;
					Mon_Ani.SetBool("onAttack", false);
				}
			}
			else
			{
				if (State != 2)
				{
					State = 2;
					Mon_Ani.SetBool("onAttack", true);
				}
				if (!H25_Ani.GetBool("isStopped"))
				{
					Target_Speed = H_ani.Speed;
				}
			}
		}
		Mon_Ani.speed = global::UnityEngine.Mathf.Lerp(Mon_Ani.speed, Target_Speed, global::UnityEngine.Time.deltaTime * 1f);
		if (!H25_Ani.GetBool("isGallery"))
		{
			base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, Pos_Target, global::UnityEngine.Time.deltaTime * 3f);
			if (H_ani.Mon_Object != null)
			{
				H_ani.Mon_Object.transform.position = base.transform.position;
			}
		}
	}

	private void H25_Speed_1()
	{
		Idle_Timer = 0.5f;
		if (H25_Ani.GetInteger("State") == 0)
		{
			if (State == 3)
			{
				State = 1;
				Mon_Ani.SetBool("onAttack", false);
			}
			else if (State == 2 && (Attack_Timer <= 0f || H25_Ani.GetBool("isStopped")))
			{
				State = 1;
				Mon_Ani.SetBool("onAttack", false);
			}
		}
	}

	private void H25_Speed_2()
	{
		Attack_Timer = 0.5f;
		if (H25_Ani.GetInteger("State") == 0 && Idle_Timer <= 0f && !H25_Ani.GetBool("isStopped") && State != 2)
		{
			State = 2;
			Mon_Ani.SetBool("onAttack", true);
		}
	}

	private void H25_Speed_End()
	{
		Attack_Timer = 0.5f;
		if (H25_Ani.GetInteger("State") == 0 && Idle_Timer <= 0f && !H25_Ani.GetBool("isStopped") && State != 3)
		{
			State = 3;
			Mon_Ani.SetBool("onAttack", true);
		}
	}
}
