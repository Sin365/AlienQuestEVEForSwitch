public class H_Manager : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject[] H_Play;

	public global::UnityEngine.GameObject[] H_GameOver;

	private int GameOver_Num;

	private global::UnityEngine.GameObject GameOver_Now;

	public global::UnityEngine.GameObject Make_H(int Slot_num, int H_num, int isFlip)
	{
		if (H_num < 0 || H_num >= H_Play.Length)
		{
			H_num = 0;
		}
		if (H_Play[H_num] == null)
		{
			H_num = 0;
		}
		global::UnityEngine.Vector3 position = global::UnityEngine.GameObject.Find("pos_R_" + Slot_num).transform.position;
		if (H_num == 7 || H_num == 8 || H_num == 9 || H_num == 16 || H_num == 17 || H_num >= 50)
		{
			position = ((isFlip <= 0) ? new global::UnityEngine.Vector3(position.x - 1.04f, position.y, 0f) : new global::UnityEngine.Vector3(position.x - 0.96f, position.y, 0f));
		}
		else
		{
			switch (H_num)
			{
			case 20:
			case 21:
			case 33:
			case 34:
				position = new global::UnityEngine.Vector3(position.x - 1.04f, position.y, 0f);
				break;
			case 24:
				position = new global::UnityEngine.Vector3(position.x - 1.04f, position.y + 7f, 0f);
				break;
			case 26:
			case 27:
				position = new global::UnityEngine.Vector3(position.x - 1.04f, position.y + 0.15f, 0f);
				break;
			case 31:
				position = ((isFlip <= 0) ? new global::UnityEngine.Vector3(position.x - 0.4f, position.y, 0f) : new global::UnityEngine.Vector3(position.x - 1.74f, position.y, 0f));
				break;
			default:
				if (isFlip > 0)
				{
					position = new global::UnityEngine.Vector3(position.x - 2f, position.y, 0f);
				}
				break;
			}
		}
		if (H_num == 12)
		{
			position = new global::UnityEngine.Vector3(position.x, position.y - 0.03f, 0f);
		}
		if ((H_num == 22 || H_num == 23 || H_num == 28 || H_num == 29 || H_num == 30 || H_num == 31) && global::UnityEngine.Random.Range(0, 10) > 5)
		{
			switch (H_num)
			{
			case 22:
				H_num = 55;
				break;
			case 23:
				H_num = 56;
				break;
			case 28:
				H_num = ((global::UnityEngine.Random.Range(0, 10) <= 5) ? 58 : 57);
				break;
			case 29:
				H_num = 59;
				break;
			case 30:
				H_num = 60;
				break;
			case 31:
				H_num = 61;
				break;
			}
		}
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(H_Play[H_num], position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
		if (H_num == 1)
		{
			gameObject.SendMessage("Start_H8_Dummy");
		}
		return gameObject;
	}

	public void Make_H_GameOver(int num)
	{
		if (GameOver_Now != null)
		{
			global::UnityEngine.Object.Destroy(GameOver_Now.gameObject);
		}
		GameOver_Num = num;
		GameOver_Now = global::UnityEngine.Object.Instantiate(H_GameOver[num - 1], base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
	}

	public void Delete_GameOver()
	{
		GameOver_Num = 0;
		if (GameOver_Now != null)
		{
			global::UnityEngine.Object.Destroy(GameOver_Now.gameObject);
		}
	}

	public void Set_GameOver_Option(int num)
	{
		if (GameOver_Num > 0 && GameOver_Now != null)
		{
			if (GameOver_Num == 1)
			{
				GameOver_Now.GetComponent<H11_Control>().Set_Option(num);
			}
			else if (GameOver_Num == 2)
			{
				GameOver_Now.GetComponent<H_GameOver_2>().Set_Option(num);
			}
			else if (GameOver_Num == 3)
			{
				GameOver_Now.GetComponent<H_GameOver_3>().Set_Option(num);
			}
			else if (GameOver_Num == 4)
			{
				GameOver_Now.GetComponent<H_GameOver_4>().Set_Option(num);
			}
			else if (GameOver_Num == 5)
			{
				GameOver_Now.GetComponent<H_GameOver_5>().Set_Option(num);
			}
		}
		global::UnityEngine.Debug.Log(GameOver_Num + " , " + num);
	}
}
