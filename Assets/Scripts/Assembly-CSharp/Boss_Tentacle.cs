public class Boss_Tentacle : global::UnityEngine.MonoBehaviour
{
	public AI_Mon_30N Boss;

	private bool onHit;

	private bool onPause;

	private float Attack_Delay;

	private global::UnityEngine.GameObject Player;

	GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		Player = global::UnityEngine.GameObject.Find("Player");
		Attack_Delay = 1f;
		if (global::UnityEngine.Random.Range(0, 10) > 5)
		{
			GetComponent<global::UnityEngine.Animator>().SetTrigger("onAttack_1");
		}
		else
		{
			GetComponent<global::UnityEngine.Animator>().SetTrigger("onAttack_2");
		}
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Atk_3(base.transform.position);
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			if (onPause)
			{
				onPause = false;
				GetComponent<global::UnityEngine.Animator>().speed = 1f;
			}
		}
		else if (!onPause)
		{
			onPause = true;
			GetComponent<global::UnityEngine.Animator>().speed = 0f;
		}
	}

	private void End_Attack()
	{
		global::UnityEngine.Object.Destroy(base.gameObject);
	}

	private void OnTriggerStay2D(global::UnityEngine.Collider2D col)
	{
		if (GM.Paused || GM.onHscene || onHit || GM.onShield || !(GM.Damage_Timer <= 0f) || GM.GameOver || !(col.name == "Ani"))
		{
			return;
		}
		onHit = true;
		if (Boss != null && GM.Option_Int[3] == 1 && !GM.onCloth && GM.Hscene_Timer <= 0f)
		{
			Player_Control component = Player.GetComponent<Player_Control>();
			if (component.grounded_Now && (component.State.ToString() == "Idle" || component.State.ToString() == "Run" || component.State.ToString() == "Sit" || component.State.ToString() == "Down"))
			{
				GM.onHscene = true;
				Boss.Start_Hscene_Tentacle(base.transform.position);
			}
			else
			{
				int num = ((!(base.transform.position.x > col.transform.position.x)) ? 1 : (-1));
				GM.Damage(40, 10 * num, false, 0);
			}
		}
		else
		{
			int num2 = ((!(base.transform.position.x > col.transform.position.x)) ? 1 : (-1));
			GM.Damage(40, 10 * num2, false, 0);
		}
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Hit_2(base.transform.position);
	}
}
