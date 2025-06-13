public class LV_0_InfoDoor : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject info_Dialog;

	private float Info_Timer;

	private float Stay_Timer;

	private string text_Eng = "It's locked.\nI need to find another way.";

	private string text_Jpn = "ロックされている。\n別の道を見つける必要がして。";

	private string text_Cn = "锁住了。\n我需要寻找别的道路。";

	GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		if (Info_Timer > 0f)
		{
			Info_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (!GM.onCard_1 && Stay_Timer > 1f && Info_Timer <= 0f)
		{
			Stay_Timer = 0f;
			Info_Timer = 6f;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(info_Dialog) as global::UnityEngine.GameObject;
			if (GM.Language_Num == 1)
			{
				gameObject.GetComponent<Info_Dialog>().Set_Size(300f);
				gameObject.GetComponent<Info_Dialog>().Set_Text(text_Jpn);
			}
			if (GM.Language_Num == 2)
			{
				gameObject.GetComponent<Info_Dialog>().Set_Size(300f);
				gameObject.GetComponent<Info_Dialog>().Set_Text(text_Cn);
			}
			else
			{
				gameObject.GetComponent<Info_Dialog>().Set_Size(280f);
				gameObject.GetComponent<Info_Dialog>().Set_Text(text_Eng);
			}
			gameObject.GetComponent<Info_Dialog>().Set_Timer(4f);
		}
	}

	private void OnTriggerStay2D(global::UnityEngine.Collider2D col)
	{
		if (!GM.Paused && col.name == "Ani")
		{
			Stay_Timer += global::UnityEngine.Time.deltaTime;
		}
	}

	private void OnTriggerExit2D(global::UnityEngine.Collider2D col)
	{
		if (col.name == "Ani")
		{
			Stay_Timer = 0f;
		}
	}
}
