public class Ending_Control : global::UnityEngine.MonoBehaviour
{
	private int State;

	private float Life_Timer;

	private bool GameOver;

	private bool Escaped;

	public global::UnityEngine.AudioSource BGM_Ending;

	public Ending_Cam_Control Ending_Cam;

	public global::UnityEngine.Transform Opening_Ship;

	public GunShip Gun_Ship;

	public Ending_Ship EndingShip;

	public global::UnityEngine.SpriteRenderer Spr_Explo;

	public global::UnityEngine.SpriteRenderer Ellen_1;

	public global::UnityEngine.SpriteRenderer Ellen_2;

	public global::UnityEngine.SpriteRenderer Ellen_3;

	public global::UnityEngine.SpriteRenderer Ellen_BG;

	private int Ellen_Num = 1;

	private float Ellen_Scroll_Speed;

	public global::UnityEngine.SpriteRenderer Glow_Speed;

	public global::UnityEngine.SpriteRenderer Glow_Speed_Border;

	public global::UnityEngine.GameObject _Dot_Speed;

	private global::UnityEngine.GameObject[] Dot_List;

	public Title_AQText AlienQuet_Text;

	public global::UnityEngine.UI.Image Title_Font_I;

	public global::UnityEngine.UI.Image Title_Font_V;

	public global::UnityEngine.UI.Image Title_Font_E;

	public global::UnityEngine.UI.Image Title_Font_I_a;

	public global::UnityEngine.UI.Image Title_Font_V_a;

	public global::UnityEngine.UI.Image Title_Font_E_a;

	public global::UnityEngine.UI.Text Title_Text;

	public global::UnityEngine.UI.Text GRIMHELM_Text;

	private float text_Timer;

	private float textPos_Timer;

	private global::UnityEngine.Vector3[] pos_Font = new global::UnityEngine.Vector3[6];

	private bool Bgm_Played;

	private int Slot_Num = -1;

	private float Load_Timer;

	private bool DataLoaded;

	private global::UnityEngine.Color color_ON = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_OFF = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	private bool isFadeIn = true;

	private bool isFadeOut;

	private float FadeOpacity = 1f;

	private string FadeOutAction = string.Empty;

	private global::UnityEngine.UI.Image BlackFade;

	private void Start()
	{
		BlackFade = global::UnityEngine.GameObject.Find("BlackFade").GetComponent<global::UnityEngine.UI.Image>();
		BlackFade.enabled = true;
		Glow_Speed.enabled = false;
		Glow_Speed_Border.enabled = false;
		Spr_Explo.color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
		Opening_Ship.transform.position = new global::UnityEngine.Vector3(0f, 0f, 0f);
		EndingShip.transform.position = new global::UnityEngine.Vector3(100f, 0.1f, 0f);
		Ellen_1.transform.position = new global::UnityEngine.Vector3(100f, -24f, 0f);
		Ellen_2.transform.position = new global::UnityEngine.Vector3(100f, -24f, 0f);
		Ellen_3.transform.position = new global::UnityEngine.Vector3(100f, -24f, 0f);
		Ellen_BG.transform.position = new global::UnityEngine.Vector3(100f, 0f, 0f);
		global::UnityEngine.SpriteRenderer ellen_ = Ellen_1;
		global::UnityEngine.Color color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
		Ellen_3.color = color;
		color = color;
		Ellen_2.color = color;
		ellen_.color = color;
		Ellen_BG.color = new global::UnityEngine.Color(0f, 0f, 0f, 0f);
		if (global::UnityEngine.PlayerPrefs.GetInt("Escaped") > 0)
		{
			for (int i = 1; i < 44; i++)
			{
				global::UnityEngine.PlayerPrefs.SetInt("H_" + i, 1);
			}
			global::UnityEngine.PlayerPrefs.SetInt("H_51", 1);
			global::UnityEngine.PlayerPrefs.SetInt("H_52", 1);
			global::UnityEngine.PlayerPrefs.SetInt("H_53", 1);
			global::UnityEngine.PlayerPrefs.SetInt("H_54", 1);
			global::UnityEngine.PlayerPrefs.SetInt("H_55", 1);
			global::UnityEngine.PlayerPrefs.SetInt("H_GameOver_1", 1);
			global::UnityEngine.PlayerPrefs.SetInt("H_GameOver_2", 1);
			global::UnityEngine.PlayerPrefs.SetInt("H_GameOver_3", 1);
			global::UnityEngine.PlayerPrefs.SetInt("H_GameOver_4", 1);
			global::UnityEngine.PlayerPrefs.SetInt("H_GameOver_5", 1);
			Slot_Num = global::UnityEngine.PlayerPrefs.GetInt("Slot_Num");
		}
		Title_Font_I.color = color_OFF;
		Title_Font_V.color = color_OFF;
		Title_Font_E.color = color_OFF;
		Title_Font_I_a.color = color_OFF;
		Title_Font_V_a.color = color_OFF;
		Title_Font_E_a.color = color_OFF;
		Title_Text.color = color_OFF;
		GRIMHELM_Text.color = color_OFF;
		pos_Font[0] = Title_Font_I.GetComponent<global::UnityEngine.RectTransform>().localPosition;
		pos_Font[1] = Title_Font_V.GetComponent<global::UnityEngine.RectTransform>().localPosition;
		pos_Font[2] = Title_Font_E.GetComponent<global::UnityEngine.RectTransform>().localPosition;
		pos_Font[3] = Title_Font_I_a.GetComponent<global::UnityEngine.RectTransform>().localPosition;
		pos_Font[4] = Title_Font_V_a.GetComponent<global::UnityEngine.RectTransform>().localPosition;
		pos_Font[5] = Title_Font_E_a.GetComponent<global::UnityEngine.RectTransform>().localPosition;
		if (global::UnityEngine.PlayerPrefs.GetFloat("MusicVolume") < 0.6f)
		{
			BGM_Ending.volume = 0.6f;
		}
		else
		{
			BGM_Ending.volume = global::UnityEngine.PlayerPrefs.GetFloat("MusicVolume");
		}
	}

	private void Ani_Text()
	{
		text_Timer += global::UnityEngine.Time.deltaTime;
		if (text_Timer > 0.1f)
		{
			Title_Font_I.color = global::UnityEngine.Color.Lerp(Title_Font_I.color, new global::UnityEngine.Color(1f, 1f, 1f, 0.3f), global::UnityEngine.Time.deltaTime * 0.5f);
			Title_Font_V.color = global::UnityEngine.Color.Lerp(Title_Font_V.color, new global::UnityEngine.Color(1f, 1f, 1f, 0.3f), global::UnityEngine.Time.deltaTime * 0.25f);
			Title_Font_E.color = global::UnityEngine.Color.Lerp(Title_Font_E.color, new global::UnityEngine.Color(1f, 1f, 1f, 0.3f), global::UnityEngine.Time.deltaTime * 0.1f);
			Title_Font_I_a.color = global::UnityEngine.Color.Lerp(Title_Font_I_a.color, new global::UnityEngine.Color(1f, 1f, 1f, 0.25f), global::UnityEngine.Time.deltaTime * 0.5f);
			Title_Font_V_a.color = global::UnityEngine.Color.Lerp(Title_Font_V_a.color, new global::UnityEngine.Color(1f, 1f, 1f, 0.25f), global::UnityEngine.Time.deltaTime * 0.25f);
			Title_Font_E_a.color = global::UnityEngine.Color.Lerp(Title_Font_E_a.color, new global::UnityEngine.Color(1f, 1f, 1f, 0.25f), global::UnityEngine.Time.deltaTime * 0.1f);
			textPos_Timer += global::UnityEngine.Time.deltaTime;
			if (textPos_Timer > 0.08f)
			{
				textPos_Timer = 0f;
				Title_Font_I_a.GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(pos_Font[3].x + global::UnityEngine.Random.Range(-2.5f, 2.5f), pos_Font[3].y + global::UnityEngine.Random.Range(-2f, 2f), 0f);
				Title_Font_V_a.GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(pos_Font[4].x + global::UnityEngine.Random.Range(-2.5f, 2.5f), pos_Font[4].y + global::UnityEngine.Random.Range(-2f, 2f), 0f);
				Title_Font_E_a.GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(pos_Font[5].x + global::UnityEngine.Random.Range(-2.5f, 2.5f), pos_Font[5].y + global::UnityEngine.Random.Range(-2f, 2f), 0f);
			}
		}
		if (text_Timer > 2f)
		{
			Title_Text.color = global::UnityEngine.Color.Lerp(Title_Text.color, new global::UnityEngine.Color(1f, 1f, 1f, 0.5f), global::UnityEngine.Time.deltaTime * 0.1f);
		}
		GRIMHELM_Text.color = global::UnityEngine.Color.Lerp(GRIMHELM_Text.color, color_ON, global::UnityEngine.Time.deltaTime * 0.2f);
	}

	private void Update()
	{
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (base.GetComponent<UnityEngine.AudioSource>().isPlaying && Life_Timer > 10f)
		{
			base.GetComponent<UnityEngine.AudioSource>().volume = global::UnityEngine.Mathf.Lerp(base.GetComponent<UnityEngine.AudioSource>().volume, 0f, global::UnityEngine.Time.deltaTime * 0.5f);
		}
		if (Slot_Num > -1 && !DataLoaded)
		{
			Load_Timer += global::UnityEngine.Time.deltaTime;
			if (Load_Timer > 0.2f)
			{
				DataLoaded = true;
				GetComponent<Save_Control>().Load_Game();
			}
		}
		if (State == 0)
		{
			if (global::UnityEngine.PlayerPrefs.GetInt("Escaped") > 0 && !Escaped && Life_Timer > 2.8f)
			{
				Gun_Ship.GetComponent<global::UnityEngine.Animator>().SetTrigger("onEscape");
				Escaped = true;
			}
			if (Life_Timer > 3f)
			{
				Ending_Cam.Set_Shake();
				base.GetComponent<UnityEngine.AudioSource>().Play();
				State++;
			}
		}
		else if (State == 1)
		{
			Spr_Explo.color = global::UnityEngine.Color.Lerp(Spr_Explo.color, new global::UnityEngine.Color(1f, 1f, 1f, 1f), global::UnityEngine.Time.deltaTime * 2f);
			Spr_Explo.transform.localScale = global::UnityEngine.Vector3.Lerp(Spr_Explo.transform.localScale, new global::UnityEngine.Vector3(12f, 4f, 1f), global::UnityEngine.Time.deltaTime);
			if (BlackFade.enabled)
			{
				BlackFade.color = global::UnityEngine.Color.Lerp(BlackFade.color, Spr_Explo.color, global::UnityEngine.Time.deltaTime * 3f);
			}
			else if (Life_Timer > 5f)
			{
				BlackFade.enabled = true;
				BlackFade.color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
			}
			if (Escaped && Life_Timer > 10f)
			{
				Opening_Ship.position = new global::UnityEngine.Vector3(100f, 0f, 0f);
				BlackFade.color = new global::UnityEngine.Color(0f, 0f, 0f, 0f);
				Glow_Speed.enabled = true;
				Glow_Speed_Border.enabled = true;
				Dot_Make();
				Gun_Ship.Set_Engine_Volume(1f);
				Ending_Cam.target_Size = 5f;
				Gun_Ship.GetComponent<global::UnityEngine.Animator>().SetTrigger("onFly");
				State++;
			}
			else if (!Escaped && Life_Timer > 10f && !isFadeOut)
			{
				Set_FadeOut(string.Empty);
			}
		}
		else if (State == 2)
		{
			Spr_Explo.color = global::UnityEngine.Color.Lerp(Spr_Explo.color, new global::UnityEngine.Color(0f, 0f, 0f, 0f), global::UnityEngine.Time.deltaTime * 0.5f);
			Dot_Move();
			if (Life_Timer > 18f)
			{
				BlackFade.color = global::UnityEngine.Color.Lerp(BlackFade.color, new global::UnityEngine.Color(0f, 0f, 0f, 1f), global::UnityEngine.Time.deltaTime * 2f);
			}
			else
			{
				BlackFade.color = global::UnityEngine.Color.Lerp(BlackFade.color, new global::UnityEngine.Color(0f, 0f, 0f, 0f), global::UnityEngine.Time.deltaTime * 1f);
			}
			if (Life_Timer > 18f)
			{
				Gun_Ship.Set_Engine_Volume(0f);
			}
			if (Life_Timer > 20f && BlackFade.color.a > 0.99f)
			{
				Glow_Speed.enabled = false;
				Glow_Speed_Border.enabled = false;
				Dot_Out();
				Ending_Cam.target_Size = 5.5f;
				Ending_Cam_Control ending_Cam = Ending_Cam;
				global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3(4f, 0f, -10f);
				Ending_Cam.transform.position = vector;
				ending_Cam.target_Pos = vector;
				Ending_Cam.Pos_Speed = 0.3f;
				Ending_Cam.transform.position = new global::UnityEngine.Vector3(-11f, 0f, -10f);
				Gun_Ship.GetComponent<global::UnityEngine.Animator>().SetTrigger("onOut");
				EndingShip.transform.position = new global::UnityEngine.Vector3(0f, 0.2f, 0f);
				EndingShip.Reset_Pos();
				State++;
			}
			else
			{
				Ending_Cam_Control ending_Cam2 = Ending_Cam;
				global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3(0f, 0f, -10f);
				Ending_Cam.transform.position = vector;
				ending_Cam2.target_Pos = vector;
			}
		}
		else if (State == 3)
		{
			BlackFade.color = global::UnityEngine.Color.Lerp(BlackFade.color, new global::UnityEngine.Color(0f, 0f, 0f, 0f), global::UnityEngine.Time.deltaTime * 2f);
			if (Life_Timer > 25f)
			{
				EndingShip.State = 1;
			}
			if (Life_Timer > 30f)
			{
				Ellen_1.transform.position = new global::UnityEngine.Vector3(0f, -32f, 0f);
				Ellen_Scroll_Speed = 0f;
				Ellen_BG.transform.position = new global::UnityEngine.Vector3(0f, 0f, 0f);
				State++;
			}
		}
		else if (State == 4)
		{
			Ellen_BG.color = global::UnityEngine.Color.Lerp(Ellen_BG.color, new global::UnityEngine.Color(0f, 0f, 0f, 0.7f), global::UnityEngine.Time.deltaTime * 10f);
			Ellen_Scroll_Speed = global::UnityEngine.Mathf.Lerp(Ellen_Scroll_Speed, 0.3f, global::UnityEngine.Time.deltaTime * 0.5f);
			if (Ellen_Num == 1)
			{
				Ellen_1.transform.position = global::UnityEngine.Vector3.Lerp(Ellen_1.transform.position, new global::UnityEngine.Vector3(4f, -2f, 0f), global::UnityEngine.Time.deltaTime * Ellen_Scroll_Speed);
				if (Ellen_1.transform.position.y > -8f)
				{
					Ellen_1.color = global::UnityEngine.Color.Lerp(Ellen_1.color, new global::UnityEngine.Color(1f, 1f, 1f, 0f), global::UnityEngine.Time.deltaTime * 10f);
					if (Ellen_1.color.a < 0.02f)
					{
						Ellen_1.transform.position = new global::UnityEngine.Vector3(100f, -32f, 0f);
						Ellen_2.transform.position = new global::UnityEngine.Vector3(4f, -31.5f, 0f);
						Ellen_Scroll_Speed = 0f;
						Ellen_Num++;
					}
				}
				else
				{
					Ellen_1.color = global::UnityEngine.Color.Lerp(Ellen_1.color, new global::UnityEngine.Color(1f, 1f, 1f, 1f), global::UnityEngine.Time.deltaTime * 10f);
				}
			}
			else if (Ellen_Num == 2)
			{
				Ellen_2.transform.position = global::UnityEngine.Vector3.Lerp(Ellen_2.transform.position, new global::UnityEngine.Vector3(4f, -4f, 0f), global::UnityEngine.Time.deltaTime * Ellen_Scroll_Speed);
				if (Ellen_2.transform.position.y > -9.5f)
				{
					Ellen_2.color = global::UnityEngine.Color.Lerp(Ellen_2.color, new global::UnityEngine.Color(1f, 1f, 1f, 0f), global::UnityEngine.Time.deltaTime * 10f);
					if (Ellen_2.color.a < 0.02f)
					{
						Ellen_2.transform.position = new global::UnityEngine.Vector3(100f, -32f, 0f);
						Ellen_3.transform.position = new global::UnityEngine.Vector3(4f, -6f, 0f);
						Ellen_Scroll_Speed = 0f;
						EndingShip.State = 2;
						Ellen_Num++;
					}
				}
				else
				{
					Ellen_2.color = global::UnityEngine.Color.Lerp(Ellen_2.color, new global::UnityEngine.Color(1f, 1f, 1f, 1f), global::UnityEngine.Time.deltaTime * 10f);
				}
			}
			else
			{
				Ellen_3.transform.position = global::UnityEngine.Vector3.Lerp(Ellen_3.transform.position, new global::UnityEngine.Vector3(4f, -35f, 0f), global::UnityEngine.Time.deltaTime * Ellen_Scroll_Speed);
				if (Ellen_3.transform.position.y < -32.5f)
				{
					Ellen_3.color = global::UnityEngine.Color.Lerp(Ellen_3.color, new global::UnityEngine.Color(1f, 1f, 1f, 0f), global::UnityEngine.Time.deltaTime * 10f);
					Ellen_BG.color = global::UnityEngine.Color.Lerp(Ellen_BG.color, new global::UnityEngine.Color(0f, 0f, 0f, 0f), global::UnityEngine.Time.deltaTime * 10f);
					if (Ellen_3.color.a < 0.02f)
					{
						Ellen_3.transform.position = new global::UnityEngine.Vector3(100f, -32f, 0f);
						EndingShip.State = 3;
						State++;
					}
				}
				else
				{
					Ellen_3.color = global::UnityEngine.Color.Lerp(Ellen_3.color, new global::UnityEngine.Color(1f, 1f, 1f, 1f), global::UnityEngine.Time.deltaTime * 10f);
				}
			}
		}
		else if (State == 5)
		{
			Ellen_BG.color = global::UnityEngine.Color.Lerp(Ellen_BG.color, new global::UnityEngine.Color(0f, 0f, 0f, 0f), global::UnityEngine.Time.deltaTime * 10f);
			if (Life_Timer > 61f)
			{
				EndingShip.State = 4;
				Ending_Cam.target_Size = 9f;
				Ending_Cam.Size_Speed = 0f;
			}
			if (Life_Timer > 66f)
			{
				Ending_Cam.target_Pos = new global::UnityEngine.Vector3(-1f, 0.2f, -10f);
				Ending_Cam.Pos_Speed = 0f;
				State++;
			}
		}
		else if (State == 6)
		{
			if (Life_Timer > 85.5f && EndingShip.State != 6)
			{
				EndingShip.State = 6;
			}
			else if (Life_Timer > 82f && EndingShip.State < 5)
			{
				EndingShip.State = 5;
			}
		}
		if (Life_Timer > 62f)
		{
			Ending_Cam.Size_Speed = global::UnityEngine.Mathf.Lerp(Ending_Cam.Size_Speed, 0.1f, global::UnityEngine.Time.deltaTime * 0.5f);
		}
		if (Life_Timer > 66f)
		{
			Ending_Cam.Pos_Speed = global::UnityEngine.Mathf.Lerp(Ending_Cam.Pos_Speed, 0.1f, global::UnityEngine.Time.deltaTime * 0.5f);
		}
		if (Life_Timer > 21f && !Bgm_Played)
		{
			Bgm_Played = true;
			BGM_Ending.Play();
		}
		if (Life_Timer < 68.5f)
		{
			AlienQuet_Text.Life_Timer = -1f;
		}
		if (Life_Timer > 74.2f)
		{
			Ani_Text();
		}
		if (Life_Timer > 91f && !isFadeOut)
		{
			if (Slot_Num > -1 && DataLoaded)
			{
				if (Slot_Num == 0)
				{
					GetComponent<Save_Control>().SaveData.E_scene_1[19] = 100;
				}
				else if (Slot_Num == 1)
				{
					GetComponent<Save_Control>().SaveData.E_scene_2[19] = 100;
				}
				else if (Slot_Num == 2)
				{
					GetComponent<Save_Control>().SaveData.E_scene_3[19] = 100;
				}
				GetComponent<Save_Control>().Save_Game();
			}
			Set_FadeOut(string.Empty);
		}
		if (isFadeIn)
		{
			FadeOpacity -= global::UnityEngine.Time.deltaTime * 0.4f;
			if (FadeOpacity <= 0.01f)
			{
				isFadeIn = false;
				FadeOpacity = 0f;
				BlackFade.enabled = false;
			}
			BlackFade.color = new global::UnityEngine.Color(0f, 0f, 0f, FadeOpacity);
		}
		else
		{
			if (!isFadeOut)
			{
				return;
			}
			FadeOpacity += global::UnityEngine.Time.deltaTime * 1f;
			if (FadeOpacity >= 1f)
			{
				isFadeOut = false;
				FadeOpacity = 1f;
				if (Escaped)
				{
					global::UnityEngine.Application.LoadLevel("Credits");
				}
				else
				{
					global::UnityEngine.Application.LoadLevel("Title");
				}
			}
			BlackFade.color = new global::UnityEngine.Color(0f, 0f, 0f, FadeOpacity);
			base.GetComponent<UnityEngine.AudioSource>().volume = global::UnityEngine.Mathf.Lerp(base.GetComponent<UnityEngine.AudioSource>().volume, 0f, global::UnityEngine.Time.deltaTime * 0.5f);
			BGM_Ending.volume = global::UnityEngine.Mathf.Lerp(BGM_Ending.volume, 0f, global::UnityEngine.Time.deltaTime * 5f);
		}
	}

	private void Dot_Make()
	{
		Dot_List = new global::UnityEngine.GameObject[90];
		for (int i = 0; i < Dot_List.Length; i++)
		{
			Dot_List[i] = global::UnityEngine.Object.Instantiate(_Dot_Speed, base.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, (float)i * global::UnityEngine.Random.Range(3.5f, 4.5f))) as global::UnityEngine.GameObject;
			Dot_List[i].transform.parent = base.transform;
			Dot_List[i].transform.localScale = new global::UnityEngine.Vector3(-0.1f, 0.1f, 1f);
			Dot_List[i].transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Random.Range(7f, 12f));
		}
	}

	private void Dot_Move()
	{
		float num = 1f;
		for (int i = 0; i < Dot_List.Length; i++)
		{
			Dot_List[i].transform.localScale = global::UnityEngine.Vector3.Lerp(Dot_List[i].transform.localScale, new global::UnityEngine.Vector3(-2f, 0.05f, 1f), global::UnityEngine.Time.deltaTime * 10f);
			Dot_List[i].transform.Translate(global::UnityEngine.Vector3.left * global::UnityEngine.Time.deltaTime * 20f);
			num = global::UnityEngine.Vector3.Distance(Dot_List[i].transform.position, base.transform.position);
			if (num < 3f || num > 100f)
			{
				Dot_List[i].transform.localScale = new global::UnityEngine.Vector3(-0.1f, 0.1f, 1f);
				Dot_List[i].transform.position = base.transform.position;
				Dot_List[i].transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Random.Range(7f, 12f));
			}
		}
	}

	private void Dot_Out()
	{
		for (int i = 0; i < Dot_List.Length; i++)
		{
			Dot_List[i].transform.position = new global::UnityEngine.Vector3(100f, 0f, 0f);
		}
	}

	private void Set_FadeOut(string fadeoutaction)
	{
		isFadeOut = true;
		isFadeIn = false;
		if (!BlackFade.enabled)
		{
			FadeOpacity = 0f;
			BlackFade.enabled = true;
			BlackFade.color = new global::UnityEngine.Color(0f, 0f, 0f, 0f);
		}
		FadeOutAction = fadeoutaction;
	}
}
