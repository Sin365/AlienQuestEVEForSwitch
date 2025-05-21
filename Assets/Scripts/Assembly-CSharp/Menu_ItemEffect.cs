public class Menu_ItemEffect : global::UnityEngine.MonoBehaviour
{
	private float Glow_2_Timer;

	private float Glow_5_Timer;

	private float Glow_10_Timer;

	public global::UnityEngine.Sprite spr_Dust_3;

	public global::UnityEngine.Sprite spr_Dust_4;

	public global::UnityEngine.Sprite spr_Dust_10;

	public global::UnityEngine.GameObject DustObject;

	private bool Menu_Quit = true;

	private bool Menu_Hide = true;

	private bool onItem_2;

	private bool onItem_3;

	private bool onItem_4;

	private bool onItem_5;

	private bool onItem_10;

	private float Opacity;

	private float Size = 1f;

	private global::UnityEngine.GameObject[] D3_List;

	private global::UnityEngine.Vector3[] D3_Pos;

	private float[] D3_Speed;

	private float[] D3_Opacity;

	private global::UnityEngine.GameObject[] D4_List;

	private global::UnityEngine.Vector3[] D4_Pos;

	private float[] D4_Speed;

	private float[] D4_Opacity;

	private global::UnityEngine.GameObject[] D10_List;

	private global::UnityEngine.Vector3[] D10_Pos;

	private float[] D10_Speed;

	private float[] D10_Opacity;

	private int DustNum = 20;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		D3_List = new global::UnityEngine.GameObject[DustNum];
		D3_Pos = new global::UnityEngine.Vector3[DustNum];
		D3_Speed = new float[DustNum];
		D3_Opacity = new float[DustNum];
		D4_List = new global::UnityEngine.GameObject[DustNum];
		D4_Pos = new global::UnityEngine.Vector3[DustNum];
		D4_Speed = new float[DustNum];
		D4_Opacity = new float[DustNum];
		D10_List = new global::UnityEngine.GameObject[DustNum];
		D10_Pos = new global::UnityEngine.Vector3[DustNum];
		D10_Speed = new float[DustNum];
		D10_Opacity = new float[DustNum];
		Menu_Off();
	}

	private void Update()
	{
		if (GM.Paused)
		{
			if (GetComponent<Menu_Control>().Menu_State == 1)
			{
				if (Menu_Quit)
				{
					Menu_Quit = false;
					Menu_On();
				}
				if (Menu_Hide)
				{
					Menu_Hide = false;
					Show_All();
				}
				Opacity = 0f;
				if (onItem_2)
				{
					Glow_2_Timer += global::UnityEngine.Time.deltaTime;
					if ((double)Glow_2_Timer > 0.05)
					{
						Glow_2_Timer = 0f;
						Opacity = (float)global::UnityEngine.Random.Range(0, 600) * 0.001f;
						global::UnityEngine.GameObject.Find("Inv_Weapon_2_Glow").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, Opacity);
					}
				}
				if (onItem_5)
				{
					Glow_5_Timer += global::UnityEngine.Time.deltaTime;
					Opacity = (1f + global::UnityEngine.Mathf.Sin(Glow_5_Timer * 2f)) * 0.5f * 0.1f;
					global::UnityEngine.GameObject.Find("Inv_Weapon_5_Glow").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, Opacity);
				}
				if (onItem_10)
				{
					Glow_10_Timer += global::UnityEngine.Time.deltaTime;
					if ((double)Glow_10_Timer > 0.05)
					{
						Size = 1f + (float)global::UnityEngine.Random.Range(-100, 100) * 0.001f;
						global::UnityEngine.GameObject.Find("Inv_Skill_5_Glow").transform.localScale = new global::UnityEngine.Vector3(Size, Size, 1f);
						Opacity = (float)global::UnityEngine.Random.Range(0, 600) * 0.001f;
						global::UnityEngine.GameObject.Find("Inv_Skill_5_Glow").GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, Opacity);
					}
				}
				if (!onItem_3 && !onItem_4 && !onItem_10)
				{
					return;
				}
				for (int i = 0; i < DustNum; i++)
				{
					if (onItem_3)
					{
						D3_Opacity[i] -= global::UnityEngine.Time.deltaTime * 1f;
						D3_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, D3_Opacity[i]);
						D3_List[i].transform.Translate(global::UnityEngine.Vector3.up * global::UnityEngine.Time.deltaTime * D3_Speed[i]);
						D3_List[i].transform.localScale = global::UnityEngine.Vector3.Lerp(D3_List[i].transform.localScale, new global::UnityEngine.Vector3(0.001f, 30f, 1f), global::UnityEngine.Time.deltaTime * 1f);
						if (D3_Opacity[i] <= 0f)
						{
							Dust_Restart3(i);
						}
					}
					if (onItem_4)
					{
						D4_Opacity[i] -= global::UnityEngine.Time.deltaTime * 1f;
						D4_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, D4_Opacity[i]);
						D4_List[i].transform.Translate(global::UnityEngine.Vector3.up * global::UnityEngine.Time.deltaTime * D4_Speed[i]);
						if (D4_Opacity[i] <= 0f)
						{
							Dust_Restart4(i);
						}
					}
					if (onItem_10)
					{
						D10_Opacity[i] += global::UnityEngine.Time.deltaTime * 1f;
						D10_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, D10_Opacity[i]);
						D10_List[i].transform.position = global::UnityEngine.Vector3.Lerp(D10_List[i].transform.position, global::UnityEngine.GameObject.Find("Inv_Skill_5").transform.position, global::UnityEngine.Time.deltaTime * 2f);
						if (global::UnityEngine.Vector2.Distance(new global::UnityEngine.Vector2(global::UnityEngine.GameObject.Find("Inv_Skill_5").transform.position.x, global::UnityEngine.GameObject.Find("Inv_Skill_5").transform.position.y), new global::UnityEngine.Vector2(D10_List[i].transform.position.x, D10_List[i].transform.position.y)) < 0.17f)
						{
							Dust_Restart10(i);
						}
					}
				}
			}
			else if (!Menu_Hide)
			{
				Menu_Hide = true;
				Hide_All();
			}
		}
		else if (!Menu_Quit)
		{
			Menu_Quit = true;
			Menu_Off();
		}
	}

	private void On_Item_2()
	{
		onItem_2 = true;
	}

	private void On_Item_3()
	{
		onItem_3 = true;
		Dust_Start3();
	}

	private void On_Item_4()
	{
		onItem_4 = true;
		Dust_Start4();
	}

	private void On_Item_5()
	{
		onItem_5 = true;
	}

	private void On_Item_10()
	{
		onItem_10 = true;
		Dust_Start10();
	}

	private void Show_All()
	{
		for (int i = 0; i < DustNum; i++)
		{
			if (onItem_3)
			{
				D3_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().enabled = true;
			}
			if (onItem_4)
			{
				D4_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().enabled = true;
			}
			if (onItem_10)
			{
				D10_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().enabled = true;
			}
		}
	}

	private void Hide_All()
	{
		for (int i = 0; i < DustNum; i++)
		{
			if (onItem_3)
			{
				D3_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().enabled = false;
			}
			if (onItem_4)
			{
				D4_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().enabled = false;
			}
			if (onItem_10)
			{
				D10_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().enabled = false;
			}
		}
	}

	private void Menu_On()
	{
		for (int i = 0; i < DustNum; i++)
		{
			if (onItem_3)
			{
				D3_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().enabled = true;
				D3_List[i].transform.Translate(global::UnityEngine.Vector3.up * -100f);
				Dust_Restart3(i);
			}
			if (onItem_4)
			{
				D4_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().enabled = true;
				D4_List[i].transform.Translate(global::UnityEngine.Vector3.up * -100f);
				Dust_Restart4(i);
			}
			if (onItem_10)
			{
				D10_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().enabled = true;
				D10_List[i].transform.Translate(global::UnityEngine.Vector3.up * -100f);
				Dust_Restart10(i);
			}
		}
	}

	private void Menu_Off()
	{
		for (int i = 0; i < DustNum; i++)
		{
			if (onItem_3)
			{
				D3_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().enabled = false;
				D3_List[i].transform.Translate(global::UnityEngine.Vector3.up * 100f);
			}
			if (onItem_4)
			{
				D4_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().enabled = false;
				D4_List[i].transform.Translate(global::UnityEngine.Vector3.up * 100f);
			}
			if (onItem_10)
			{
				D10_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().enabled = false;
				D10_List[i].transform.Translate(global::UnityEngine.Vector3.up * 100f);
			}
		}
	}

	private void Dust_Start3()
	{
		for (int i = 0; i < DustNum; i++)
		{
			D3_List[i] = (global::UnityEngine.GameObject)global::UnityEngine.Object.Instantiate(DustObject, base.transform.position, base.transform.rotation);
			D3_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().sprite = spr_Dust_3;
			float num = 0.8f + (float)global::UnityEngine.Random.Range(0, 100) * 0.005f;
			D3_List[i].transform.localScale = new global::UnityEngine.Vector3(num, num, 1f);
			D3_Pos[i] = base.transform.position;
			D3_Opacity[i] = 0.5f + (float)global::UnityEngine.Random.Range(0, 100) * 0.005f;
			D3_Speed[i] = 0.5f + (float)global::UnityEngine.Random.Range(0, 100) * 0.005f;
			D3_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, D3_Opacity[i]);
		}
	}

	private void Dust_Start4()
	{
		for (int i = 0; i < DustNum; i++)
		{
			D4_List[i] = (global::UnityEngine.GameObject)global::UnityEngine.Object.Instantiate(DustObject, base.transform.position, base.transform.rotation);
			D4_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().sprite = spr_Dust_4;
			float num = 0.8f + (float)global::UnityEngine.Random.Range(0, 100) * 0.005f;
			D4_List[i].transform.localScale = new global::UnityEngine.Vector3(num, num, 1f);
			D4_Pos[i] = base.transform.position;
			D4_Opacity[i] = 0.5f + (float)global::UnityEngine.Random.Range(0, 100) * 0.005f;
			D4_Speed[i] = 0.5f + (float)global::UnityEngine.Random.Range(0, 100) * 0.005f;
			D4_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, D4_Opacity[i]);
		}
	}

	private void Dust_Start10()
	{
		for (int i = 0; i < DustNum; i++)
		{
			D10_List[i] = (global::UnityEngine.GameObject)global::UnityEngine.Object.Instantiate(DustObject, base.transform.position, base.transform.rotation);
			D10_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().sprite = spr_Dust_10;
			float num = 0.8f + (float)global::UnityEngine.Random.Range(0, 100) * 0.005f;
			D10_List[i].transform.localScale = new global::UnityEngine.Vector3(num, num, 1f);
			D10_Pos[i] = base.transform.position;
			D10_Opacity[i] = 0f;
			D10_Speed[i] = 0.5f + (float)global::UnityEngine.Random.Range(0, 100) * 0.005f;
			D10_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, D10_Opacity[i]);
		}
	}

	private void Dust_Restart3(int num)
	{
		D3_Opacity[num] = 0.5f + (float)global::UnityEngine.Random.Range(0, 100) * 0.005f;
		D3_List[num].transform.position = new global::UnityEngine.Vector3(global::UnityEngine.GameObject.Find("Inv_Weapon_3").transform.position.x + (float)global::UnityEngine.Random.Range(-28, 28) * 0.01f, global::UnityEngine.GameObject.Find("Inv_Weapon_3").transform.position.y + (float)global::UnityEngine.Random.Range(-14, 7) * 0.01f, 0f);
		D3_List[num].GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, D3_Opacity[num]);
		float num2 = (0.8f + (float)global::UnityEngine.Random.Range(0, 100) * 0.005f) * 0.7f;
		D3_List[num].transform.localScale = new global::UnityEngine.Vector3(num2, num2, 1f);
	}

	private void Dust_Restart4(int num)
	{
		D4_Opacity[num] = 0.5f + (float)global::UnityEngine.Random.Range(0, 100) * 0.005f;
		D4_List[num].transform.position = new global::UnityEngine.Vector3(global::UnityEngine.GameObject.Find("Inv_Weapon_4").transform.position.x + (float)global::UnityEngine.Random.Range(-28, 28) * 0.01f, global::UnityEngine.GameObject.Find("Inv_Weapon_4").transform.position.y + (float)global::UnityEngine.Random.Range(-14, 7) * 0.01f, 0f);
		D4_List[num].GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, D4_Opacity[num]);
		float num2 = (0.8f + (float)global::UnityEngine.Random.Range(0, 100) * 0.005f) * 0.7f;
		D4_List[num].transform.localScale = new global::UnityEngine.Vector3(num2, num2, 1f);
	}

	private void Dust_Restart10(int num)
	{
		D10_Opacity[num] = 0f;
		D10_List[num].transform.position = new global::UnityEngine.Vector3(global::UnityEngine.GameObject.Find("Inv_Skill_5").transform.position.x + (float)global::UnityEngine.Random.Range(-11, 11) * 0.1f, global::UnityEngine.GameObject.Find("Inv_Skill_5").transform.position.y + (float)global::UnityEngine.Random.Range(-11, 11) * 0.1f, 0f);
		D10_List[num].GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, D10_Opacity[num]);
		float num2 = (0.8f + (float)global::UnityEngine.Random.Range(0, 100) * 0.005f) * 0.7f;
		D10_List[num].transform.localScale = new global::UnityEngine.Vector3(num2, num2, 1f);
	}
}
