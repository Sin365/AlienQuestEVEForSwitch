public class UI_Girl_Front : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.SpriteRenderer SR_Cursor;

	public global::UnityEngine.Transform pos_Top;

	public global::UnityEngine.Transform pos_Bot;

	public global::UnityEngine.Sprite spr_Eye_1_L;

	public global::UnityEngine.Sprite spr_Eye_1_R;

	public global::UnityEngine.Sprite spr_Eye_2_L;

	public global::UnityEngine.Sprite spr_Eye_2_R;

	public global::UnityEngine.Sprite spr_EyeBrow;

	public global::UnityEngine.Sprite spr_EyeBrow_0;

	public global::UnityEngine.Sprite spr_Close_1;

	public global::UnityEngine.Sprite spr_Close_2;

	public global::UnityEngine.Sprite spr_Close_3;

	public global::UnityEngine.Sprite spr_Mouth_1;

	public global::UnityEngine.Sprite spr_Mouth_2;

	public global::UnityEngine.SpriteRenderer SR_Pink;

	public global::UnityEngine.SpriteRenderer SR_EyeClose;

	public global::UnityEngine.SpriteRenderer SR_EyeHL_L;

	public global::UnityEngine.SpriteRenderer SR_EyeHL_R;

	public global::UnityEngine.SpriteRenderer SR_Eye_L;

	public global::UnityEngine.SpriteRenderer SR_Eye_R;

	public global::UnityEngine.SpriteRenderer SR_Mouth;

	public global::UnityEngine.SpriteRenderer SR_Mouth_AAH;

	public global::UnityEngine.SpriteRenderer SR_Face;

	public global::UnityEngine.SpriteRenderer SR_EyeBack;

	public global::UnityEngine.SpriteRenderer SR_HairBack_2;

	public global::UnityEngine.SpriteRenderer SR_Neck;

	public global::UnityEngine.SpriteRenderer SR_Leg;

	public global::UnityEngine.SpriteRenderer SR_Hand_R;

	public global::UnityEngine.SpriteRenderer SR_Hand_L;

	public global::UnityEngine.SpriteRenderer SR_Wand;

	public global::UnityEngine.SpriteRenderer UI_Hand_R;

	public global::UnityEngine.SpriteRenderer UI_Hand_L;

	public global::UnityEngine.SkinnedMeshRenderer Smr_Body;

	public global::UnityEngine.SkinnedMeshRenderer Smr_Arm_L;

	public global::UnityEngine.SkinnedMeshRenderer Smr_Arm_R;

	public global::UnityEngine.SkinnedMeshRenderer Smr_Breast_L;

	public global::UnityEngine.SkinnedMeshRenderer Smr_Breast_R;

	public global::UnityEngine.SkinnedMeshRenderer Smr_Hair;

	public global::UnityEngine.SkinnedMeshRenderer Smr_Hair_Front;

	public global::UnityEngine.SkinnedMeshRenderer Smr_Hair_Back;

	public global::UnityEngine.Material Mtl_Cloth;

	private bool onCloth = true;

	public global::UnityEngine.Material Mtl_Naked;

	public global::UnityEngine.SpriteRenderer BlueGlow;

	public global::UnityEngine.Transform pos_MosaicTarget;

	public global::UnityEngine.Transform pos_Mosaic;

	public global::UnityEngine.SpriteRenderer TopCover_Text;

	public global::UnityEngine.SpriteRenderer TopCover_Box;

	public global::UnityEngine.SpriteRenderer BotCover_Text;

	public global::UnityEngine.SpriteRenderer BotCover_Box;

	public global::UnityEngine.SpriteRenderer BotMosaic_1;

	public global::UnityEngine.SpriteRenderer BotMosaic_2;

	private bool onTop;

	private bool onTopHold;

	private bool onBot;

	private bool onTop_Start;

	private bool onBot_Start;

	private bool onHold_Start;

	private bool onMouseDown;

	private bool onTrigger_L;

	private bool onTrigger_R;

	private float Life_Timer;

	private float End_Delay;

	private bool onPause;

	private float Pause_Timer;

	private bool onMosaic = true;

	private int Idle_Num;

	private float Hold_Timer;

	private float PadHold_Timer;

	private bool onPadHold;

	private float Pink_Timer;

	private float Push_Timer;

	private int Ani_Index = -1;

	private float Ani_Timer;

	private float EyeClose_Timer;

	private float EyePos_Timer;

	private global::UnityEngine.Vector3 pos_Target;

	private global::UnityEngine.Vector3 pos_EyeHL_L;

	private global::UnityEngine.Vector3 pos_EyeHL_R;

	private global::UnityEngine.Vector3 pos_Eye_L;

	private global::UnityEngine.Vector3 pos_Eye_R;

	private global::UnityEngine.Vector3 pos_EyeHL_Down_L = new global::UnityEngine.Vector3(-0.334f, 0.033f, 0f);

	private global::UnityEngine.Vector3 pos_EyeHL_Down_R = new global::UnityEngine.Vector3(0.233f, 0.039f, 0f);

	private global::UnityEngine.Vector3 pos_Eye_Down_L = new global::UnityEngine.Vector3(-0.307f, -0.03f, 0f);

	private global::UnityEngine.Vector3 pos_Eye_Down_R = new global::UnityEngine.Vector3(0.258f, -0.022f, 0f);

	private global::UnityEngine.Color color_ON = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_OFF = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	private global::UnityEngine.Color colorGlow_ON = new global::UnityEngine.Color(0f, 0.1f, 1f, 0.5f);

	private global::UnityEngine.Color colorGlow_OFF = new global::UnityEngine.Color(0f, 0.1f, 1f, 0f);

	private global::UnityEngine.Color colorMosaic_ON = new global::UnityEngine.Color(1f, 1f, 1f, 0.2f);

	private int Moan_Num;

	private bool On_Moan_Hold;

	private bool On_Moan_Exit;

	private float Moan_Timer;

	private float Moan_End_Timer;

	private float Moan_Hold_Timer;

	private H_SoundControl H_Sound;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		if (global::UnityEngine.GameObject.Find("Sound_List_H") != null)
		{
			H_Sound = global::UnityEngine.GameObject.Find("Sound_List_H").GetComponent<H_SoundControl>();
		}
		pos_EyeHL_L = SR_EyeHL_L.transform.localPosition;
		pos_EyeHL_R = SR_EyeHL_R.transform.localPosition;
		pos_Eye_L = SR_Eye_L.transform.localPosition;
		pos_Eye_R = SR_Eye_R.transform.localPosition;
		SR_Pink.color = color_OFF;
		Set_Color(color_OFF);
		global::UnityEngine.SpriteRenderer uI_Hand_R = UI_Hand_R;
		global::UnityEngine.Color color = color_OFF;
		UI_Hand_L.color = color;
		uI_Hand_R.color = color;
		pos_Target = pos_Top.position;
		BlueGlow.color = colorGlow_OFF;
		GetComponent<global::UnityEngine.Animator>().speed = 0f;
		global::UnityEngine.SpriteRenderer botMosaic_ = BotMosaic_1;
		color = color_OFF;
		BotMosaic_2.color = color;
		botMosaic_.color = color;
		if (global::UnityEngine.PlayerPrefs.GetInt("UncensoredPatch") == 1)
		{
			onMosaic = false;
			BotMosaic_1.enabled = false;
			BotMosaic_2.enabled = false;
		}
	}

	private void Set_Color(global::UnityEngine.Color color)
	{
		SR_Face.color = color;
		SR_EyeClose.color = color;
		SR_EyeHL_L.color = color;
		SR_EyeHL_R.color = color;
		SR_Eye_L.color = color;
		SR_Eye_R.color = color;
		SR_Mouth.color = color;
		SR_Mouth_AAH.color = color;
		SR_EyeBack.color = color;
		SR_HairBack_2.color = color;
		SR_Neck.color = color;
		SR_Leg.color = color;
		SR_Hand_R.color = color;
		SR_Hand_L.color = color;
		SR_Wand.color = color;
		Smr_Body.material.color = color;
		Smr_Arm_L.material.color = color;
		Smr_Arm_R.material.color = color;
		Smr_Breast_L.material.color = color;
		Smr_Breast_R.material.color = color;
		Smr_Hair.material.color = color;
		Smr_Hair_Front.material.color = color;
		Smr_Hair_Back.material.color = color;
		if (!onCloth && global::UnityEngine.PlayerPrefs.GetInt("Censorship") == 1)
		{
			global::UnityEngine.SpriteRenderer topCover_Text = TopCover_Text;
			global::UnityEngine.Color color2 = color;
			TopCover_Box.color = color2;
			topCover_Text.color = color2;
			global::UnityEngine.SpriteRenderer botCover_Text = BotCover_Text;
			color2 = color;
			BotCover_Box.color = color2;
			botCover_Text.color = color2;
		}
		else
		{
			global::UnityEngine.SpriteRenderer topCover_Text2 = TopCover_Text;
			global::UnityEngine.Color color2 = color_OFF;
			TopCover_Box.color = color2;
			topCover_Text2.color = color2;
			global::UnityEngine.SpriteRenderer botCover_Text2 = BotCover_Text;
			color2 = color_OFF;
			BotCover_Box.color = color2;
			botCover_Text2.color = color2;
		}
	}

	private void Set_Material(global::UnityEngine.Material mtl)
	{
		Smr_Body.material = mtl;
		Smr_Breast_L.material = mtl;
		Smr_Breast_R.material = mtl;
	}

	private void End_Hit()
	{
		onTop = false;
		onTopHold = false;
		onBot = false;
		onPadHold = false;
		onTop_Start = false;
		GetComponent<global::UnityEngine.Animator>().SetBool("onTop", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onHold", false);
		GetComponent<global::UnityEngine.Animator>().SetBool("onBot", false);
		GetComponent<global::UnityEngine.Animator>().SetInteger("Num_Top", 0);
		GetComponent<global::UnityEngine.Animator>().SetInteger("Num_Bot", 0);
		End_Delay = 0f;
		Pink_Timer = 0f;
		On_Moan_Hold = false;
		On_Moan_Exit = false;
	}

	private void IdleNum_Add()
	{
		Idle_Num++;
	}

	private void Check_Hold_Start()
	{
		onHold_Start = true;
	}

	private void Check_Top_Start()
	{
		onTop_Start = true;
	}

	private void Check_Bot_Start()
	{
		onBot_Start = true;
	}

	private void Update()
	{
		if (GM.onCloth && !onCloth)
		{
			onCloth = true;
			Set_Material(Mtl_Cloth);
		}
		else if (!GM.onCloth && onCloth)
		{
			onCloth = false;
			Set_Material(Mtl_Naked);
		}
		if (Moan_Timer > 0f)
		{
			Moan_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Moan_End_Timer > 0f)
		{
			Moan_End_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (onTopHold || onPadHold)
		{
			Moan_Hold_Timer += global::UnityEngine.Time.deltaTime;
		}
		if (!GM.Paused || GM.onMap)
		{
			global::UnityEngine.SpriteRenderer uI_Hand_R = UI_Hand_R;
			global::UnityEngine.Color color = global::UnityEngine.Color.Lerp(UI_Hand_L.color, color_OFF, global::UnityEngine.Time.deltaTime * 12f);
			UI_Hand_L.color = color;
			uI_Hand_R.color = color;
			SR_Cursor.color = global::UnityEngine.Color.Lerp(SR_Cursor.color, color_OFF, global::UnityEngine.Time.deltaTime * 12f);
			SR_Pink.color = global::UnityEngine.Color.Lerp(SR_Pink.color, color_OFF, global::UnityEngine.Time.deltaTime * 12f);
			Set_Color(global::UnityEngine.Color.Lerp(SR_Face.color, color_OFF, global::UnityEngine.Time.deltaTime * 12f));
			base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, new global::UnityEngine.Vector3(-60f, 49f, 0f), global::UnityEngine.Time.deltaTime * 5f);
			BlueGlow.color = global::UnityEngine.Color.Lerp(BlueGlow.color, colorGlow_OFF, global::UnityEngine.Time.deltaTime * 1f);
			BotMosaic_1.color = global::UnityEngine.Color.Lerp(BotMosaic_1.color, color_OFF, global::UnityEngine.Time.deltaTime * 12f);
			BotMosaic_2.color = SR_Face.color;
			pos_Mosaic.position = pos_MosaicTarget.position;
			Pause_Timer += global::UnityEngine.Time.deltaTime;
			if (onPause && Pause_Timer > 1f)
			{
				onPause = false;
				Pause_Timer = 0f;
				GetComponent<global::UnityEngine.Animator>().speed = 0f;
			}
			if (Idle_Num > 0)
			{
				Idle_Num = 0;
			}
			return;
		}
		if (!onPause)
		{
			onPause = true;
			Pause_Timer = 0f;
			GetComponent<global::UnityEngine.Animator>().speed = 1f;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
		End_Delay += global::UnityEngine.Time.deltaTime;
		SR_Pink.color = global::UnityEngine.Color.Lerp(SR_Pink.color, new global::UnityEngine.Color(1f, 1f, 1f, Pink_Timer), global::UnityEngine.Time.deltaTime * 1f);
		Set_Color(global::UnityEngine.Color.Lerp(SR_Face.color, color_ON, global::UnityEngine.Time.deltaTime * 10f));
		base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, new global::UnityEngine.Vector3(-54.7f, 49f, 0f), global::UnityEngine.Time.deltaTime * 5f);
		BlueGlow.color = global::UnityEngine.Color.Lerp(BlueGlow.color, colorGlow_ON, global::UnityEngine.Time.deltaTime * 1f);
		if (onMosaic && !onCloth)
		{
			BotMosaic_1.color = global::UnityEngine.Color.Lerp(BotMosaic_1.color, new global::UnityEngine.Color(1f, 1f, 1f, 0.3f), global::UnityEngine.Time.deltaTime * 2f);
			BotMosaic_2.color = SR_Face.color;
			pos_Mosaic.position = pos_MosaicTarget.position;
		}
		Check_Input();
		Check_Mouse();
		if (global::UnityEngine.Input.GetMouseButtonUp(0))
		{
			if (onTopHold)
			{
				onTopHold = false;
				onHold_Start = false;
				On_Moan_Exit = true;
				GetComponent<global::UnityEngine.Animator>().SetBool("onTop", false);
				GetComponent<global::UnityEngine.Animator>().SetBool("onHold", false);
				Idle_Num = 0;
				Pink_Timer = 0f;
				GetComponent<global::UnityEngine.Animator>().SetBool("onBot", false);
				SR_Eye_L.sprite = spr_Eye_1_L;
				SR_Eye_R.sprite = spr_Eye_1_R;
			}
			else if (Hold_Timer > 0f)
			{
				if (onTop)
				{
					if (GetComponent<global::UnityEngine.Animator>().GetInteger("Num_Top") == 0)
					{
						GetComponent<global::UnityEngine.Animator>().SetInteger("Num_Top", 1);
					}
					else
					{
						GetComponent<global::UnityEngine.Animator>().SetInteger("Num_Top", 0);
					}
				}
				else if (End_Delay < 0.9f)
				{
					if (GetComponent<global::UnityEngine.Animator>().GetInteger("Num_Top") == 0)
					{
						GetComponent<global::UnityEngine.Animator>().Play("Top", 0, 0f);
					}
					else
					{
						GetComponent<global::UnityEngine.Animator>().Play("Top_2", 0, 0f);
					}
				}
				onTop = true;
				On_Moan_Exit = true;
				GetComponent<global::UnityEngine.Animator>().SetBool("onTop", true);
				GetComponent<global::UnityEngine.Animator>().SetBool("onHold", false);
				GetComponent<global::UnityEngine.Animator>().SetBool("onBot", false);
				Pink_Timer = 1f;
				SR_Pink.color = color_ON;
				Idle_Num = 0;
				EyeClose_Timer = -3f;
				SR_EyeClose.sprite = spr_Close_3;
				Ani_Index = 2;
				SR_EyeHL_L.sortingOrder = 210;
				SR_EyeHL_R.sortingOrder = 210;
				GetComponent<global::UnityEngine.Animator>().SetInteger("Num_Bot", 0);
			}
			Hold_Timer = 0f;
		}
		else if (global::UnityEngine.Input.GetMouseButtonDown(0))
		{
			global::UnityEngine.Ray ray = global::UnityEngine.GameObject.Find("UI Camera").camera.ScreenPointToRay(global::UnityEngine.Input.mousePosition);
			global::UnityEngine.RaycastHit2D rayIntersection = global::UnityEngine.Physics2D.GetRayIntersection(ray, float.PositiveInfinity);
			if (rayIntersection.collider != null)
			{
				if (rayIntersection.collider.name == "COL_Girl_Top")
				{
					Hold_Timer += global::UnityEngine.Time.deltaTime;
					On_Moan_Hold = true;
				}
				else if (rayIntersection.collider.name == "COL_Girl_Bot")
				{
					if (onBot)
					{
						if (GetComponent<global::UnityEngine.Animator>().GetInteger("Num_Bot") == 0)
						{
							GetComponent<global::UnityEngine.Animator>().SetInteger("Num_Bot", 1);
						}
						else
						{
							GetComponent<global::UnityEngine.Animator>().SetInteger("Num_Bot", 0);
						}
					}
					else if (End_Delay < 0.9f)
					{
						if (GetComponent<global::UnityEngine.Animator>().GetInteger("Num_Bot") == 0)
						{
							GetComponent<global::UnityEngine.Animator>().Play("Bottom", 0, 0f);
						}
						else
						{
							GetComponent<global::UnityEngine.Animator>().Play("Bottom_2", 0, 0f);
						}
					}
					onBot = true;
					On_Moan_Exit = true;
					GetComponent<global::UnityEngine.Animator>().SetBool("onTop", false);
					GetComponent<global::UnityEngine.Animator>().SetBool("onHold", false);
					GetComponent<global::UnityEngine.Animator>().SetBool("onBot", true);
					Pink_Timer = 1f;
					SR_Pink.color = color_ON;
					Idle_Num = 0;
					EyeClose_Timer = -3f;
					SR_EyeClose.sprite = spr_Close_3;
					Ani_Index = 2;
					SR_EyeHL_L.sortingOrder = 210;
					SR_EyeHL_R.sortingOrder = 210;
					GetComponent<global::UnityEngine.Animator>().SetInteger("Num_Top", 0);
				}
			}
		}
		else if (global::UnityEngine.Input.GetMouseButton(0))
		{
			global::UnityEngine.Ray ray2 = global::UnityEngine.GameObject.Find("UI Camera").camera.ScreenPointToRay(global::UnityEngine.Input.mousePosition);
			global::UnityEngine.RaycastHit2D rayIntersection2 = global::UnityEngine.Physics2D.GetRayIntersection(ray2, float.PositiveInfinity);
			if (rayIntersection2.collider != null)
			{
				if (rayIntersection2.collider.name == "COL_Girl_Top")
				{
					Hold_Timer += global::UnityEngine.Time.deltaTime;
					Pink_Timer = 1f;
					if (!onTopHold && Hold_Timer > 0.1f)
					{
						onTopHold = true;
						GetComponent<global::UnityEngine.Animator>().SetBool("onTop", false);
						GetComponent<global::UnityEngine.Animator>().SetBool("onHold", true);
						GetComponent<global::UnityEngine.Animator>().SetBool("onBot", false);
						GetComponent<global::UnityEngine.Animator>().SetInteger("Num_Top", 0);
						GetComponent<global::UnityEngine.Animator>().SetInteger("Num_Bot", 0);
						Idle_Num = 0;
						SR_Eye_L.sprite = spr_Eye_2_L;
						SR_Eye_R.sprite = spr_Eye_2_R;
					}
				}
			}
			else
			{
				Hold_Timer = 0f;
			}
		}
		else
		{
			Hold_Timer = 0f;
			if (onTopHold)
			{
				onTopHold = false;
				GetComponent<global::UnityEngine.Animator>().SetBool("onHold", false);
			}
		}
		EyePos_Timer += global::UnityEngine.Time.deltaTime;
		if (EyePos_Timer > 0.08f)
		{
			EyePos_Timer = 0f;
			float num = (float)global::UnityEngine.Random.Range(0, 50) * 0.0001f;
			float num2 = (float)global::UnityEngine.Random.Range(0, 50) * 0.0001f;
			if (onTopHold || onPadHold)
			{
				SR_Eye_L.transform.localPosition = new global::UnityEngine.Vector3(pos_Eye_Down_L.x + num, pos_Eye_Down_L.y + num2, 0f);
				SR_Eye_R.transform.localPosition = new global::UnityEngine.Vector3(pos_Eye_Down_R.x + num, pos_Eye_Down_R.y + num2, 0f);
			}
			else
			{
				SR_Eye_L.transform.localPosition = new global::UnityEngine.Vector3(pos_Eye_L.x + num, pos_Eye_L.y + num2, 0f);
				SR_Eye_R.transform.localPosition = new global::UnityEngine.Vector3(pos_Eye_R.x + num, pos_Eye_R.y + num2, 0f);
			}
			num = (float)global::UnityEngine.Random.Range(0, 100) * 0.0001f;
			num2 = (float)global::UnityEngine.Random.Range(0, 100) * 0.0001f;
			if (onTopHold || onPadHold)
			{
				SR_EyeHL_L.transform.localPosition = new global::UnityEngine.Vector3(pos_EyeHL_Down_L.x + num * 2f, pos_EyeHL_Down_L.y + num2 * 2f, 0f);
				SR_EyeHL_R.transform.localPosition = new global::UnityEngine.Vector3(pos_EyeHL_Down_R.x + num * 2f, pos_EyeHL_Down_R.y + num2 * 2f, 0f);
			}
			else
			{
				SR_EyeHL_L.transform.localPosition = new global::UnityEngine.Vector3(pos_EyeHL_L.x + num, pos_EyeHL_L.y + num2, 0f);
				SR_EyeHL_R.transform.localPosition = new global::UnityEngine.Vector3(pos_EyeHL_R.x + num, pos_EyeHL_R.y + num2, 0f);
			}
		}
		SR_Mouth.transform.localScale = new global::UnityEngine.Vector3(1f, 1f + global::UnityEngine.Mathf.Sin(Life_Timer * 3f) * 0.1f, 1f);
		if (onTopHold || onPadHold)
		{
			global::UnityEngine.SpriteRenderer uI_Hand_R2 = UI_Hand_R;
			global::UnityEngine.Color color = global::UnityEngine.Color.Lerp(UI_Hand_L.color, new global::UnityEngine.Color(1f, 1f, 1f, 0.2f), global::UnityEngine.Time.deltaTime * 8f);
			UI_Hand_L.color = color;
			uI_Hand_R2.color = color;
		}
		else
		{
			global::UnityEngine.SpriteRenderer uI_Hand_R3 = UI_Hand_R;
			global::UnityEngine.Color color = global::UnityEngine.Color.Lerp(UI_Hand_L.color, color_OFF, global::UnityEngine.Time.deltaTime * 8f);
			UI_Hand_L.color = color;
			uI_Hand_R3.color = color;
		}
		if (Ani_Index > -1)
		{
			if (Ani_Timer > 0.02f)
			{
				Ani_Timer = 0f;
				Ani_Index++;
				switch (Ani_Index)
				{
				case 1:
					SR_EyeClose.sprite = spr_Close_1;
					break;
				case 2:
					SR_EyeClose.sprite = spr_Close_2;
					break;
				case 3:
					SR_EyeClose.sprite = spr_Close_3;
					break;
				case 5:
					SR_EyeClose.sprite = spr_Close_2;
					break;
				case 6:
					SR_EyeClose.sprite = spr_Close_1;
					break;
				case 7:
					SR_EyeClose.sprite = spr_EyeBrow_0;
					break;
				case 8:
					SR_EyeClose.sprite = spr_EyeBrow;
					Ani_Index = -1;
					EyeClose_Timer = 0f;
					SR_EyeHL_L.sortingOrder = 218;
					SR_EyeHL_R.sortingOrder = 218;
					break;
				case 4:
					break;
				}
			}
			else
			{
				Ani_Timer += global::UnityEngine.Time.deltaTime;
			}
			return;
		}
		EyeClose_Timer += global::UnityEngine.Time.deltaTime;
		if (EyeClose_Timer > 2f)
		{
			EyeClose_Timer = 0f;
			if (global::UnityEngine.Random.Range(0, 20) > 5)
			{
				Ani_Index = 0;
				SR_EyeHL_L.sortingOrder = 210;
				SR_EyeHL_R.sortingOrder = 210;
			}
		}
	}

	private void Check_Input()
	{
		if (!onTrigger_R && global::UnityEngine.Input.GetAxis("L_Trigger") > 0f)
		{
			onTrigger_R = true;
			PadHold_Timer = 0f;
			if (onBot)
			{
				if (GetComponent<global::UnityEngine.Animator>().GetInteger("Num_Bot") == 0)
				{
					GetComponent<global::UnityEngine.Animator>().SetInteger("Num_Bot", 1);
				}
				else
				{
					GetComponent<global::UnityEngine.Animator>().SetInteger("Num_Bot", 0);
				}
			}
			else if (End_Delay < 0.9f)
			{
				if (GetComponent<global::UnityEngine.Animator>().GetInteger("Num_Bot") == 0)
				{
					GetComponent<global::UnityEngine.Animator>().Play("Bottom", 0, 0f);
				}
				else
				{
					GetComponent<global::UnityEngine.Animator>().Play("Bottom_2", 0, 0f);
				}
			}
			onBot = true;
			GetComponent<global::UnityEngine.Animator>().SetBool("onTop", false);
			GetComponent<global::UnityEngine.Animator>().SetBool("onHold", false);
			GetComponent<global::UnityEngine.Animator>().SetBool("onBot", true);
			On_Moan_Exit = true;
			Pink_Timer = 1f;
			SR_Pink.color = color_ON;
			Idle_Num = 0;
			EyeClose_Timer = -3f;
			SR_EyeClose.sprite = spr_Close_3;
			Ani_Index = 2;
			SR_EyeHL_L.sortingOrder = 210;
			SR_EyeHL_R.sortingOrder = 210;
			GetComponent<global::UnityEngine.Animator>().SetInteger("Num_Top", 0);
		}
		else if (global::UnityEngine.Input.GetAxis("L_Trigger") < 0f)
		{
			onTrigger_L = true;
			PadHold_Timer += global::UnityEngine.Time.deltaTime;
			Pink_Timer = 1f;
			if (!onPadHold && PadHold_Timer > 0.12f)
			{
				onPadHold = true;
				GetComponent<global::UnityEngine.Animator>().SetBool("onTop", false);
				GetComponent<global::UnityEngine.Animator>().SetBool("onHold", true);
				On_Moan_Hold = true;
				GetComponent<global::UnityEngine.Animator>().SetBool("onBot", false);
				GetComponent<global::UnityEngine.Animator>().SetInteger("Num_Top", 0);
				GetComponent<global::UnityEngine.Animator>().SetInteger("Num_Bot", 0);
				Idle_Num = 0;
				SR_Eye_L.sprite = spr_Eye_2_L;
				SR_Eye_R.sprite = spr_Eye_2_R;
			}
		}
		else if (onTrigger_R && global::UnityEngine.Input.GetAxis("L_Trigger") < 0.1f)
		{
			onTrigger_R = false;
		}
		else if (onTrigger_L && global::UnityEngine.Input.GetAxis("L_Trigger") > -0.1f)
		{
			onTrigger_L = false;
			if (onPadHold)
			{
				onPadHold = false;
				onHold_Start = false;
				GetComponent<global::UnityEngine.Animator>().SetBool("onTop", false);
				On_Moan_Exit = true;
				GetComponent<global::UnityEngine.Animator>().SetBool("onHold", false);
				Idle_Num = 0;
				Pink_Timer = 0f;
				GetComponent<global::UnityEngine.Animator>().SetBool("onBot", false);
				SR_Eye_L.sprite = spr_Eye_1_L;
				SR_Eye_R.sprite = spr_Eye_1_R;
			}
			else if (PadHold_Timer > 0f)
			{
				if (onTop)
				{
					if (GetComponent<global::UnityEngine.Animator>().GetInteger("Num_Top") == 0)
					{
						GetComponent<global::UnityEngine.Animator>().SetInteger("Num_Top", 1);
					}
					else
					{
						GetComponent<global::UnityEngine.Animator>().SetInteger("Num_Top", 0);
					}
				}
				else if (End_Delay < 0.9f)
				{
					if (GetComponent<global::UnityEngine.Animator>().GetInteger("Num_Top") == 0)
					{
						GetComponent<global::UnityEngine.Animator>().Play("Top", 0, 0f);
					}
					else
					{
						GetComponent<global::UnityEngine.Animator>().Play("Top_2", 0, 0f);
					}
				}
				onTop = true;
				GetComponent<global::UnityEngine.Animator>().SetBool("onTop", true);
				On_Moan_Exit = true;
				GetComponent<global::UnityEngine.Animator>().SetBool("onHold", false);
				GetComponent<global::UnityEngine.Animator>().SetBool("onBot", false);
				Pink_Timer = 1f;
				SR_Pink.color = color_ON;
				Idle_Num = 0;
				EyeClose_Timer = -3f;
				SR_EyeClose.sprite = spr_Close_3;
				Ani_Index = 2;
				SR_EyeHL_L.sortingOrder = 210;
				SR_EyeHL_R.sortingOrder = 210;
				GetComponent<global::UnityEngine.Animator>().SetInteger("Num_Bot", 0);
			}
			PadHold_Timer = 0f;
		}
		else
		{
			onPadHold = false;
			PadHold_Timer = 0f;
		}
	}

	private void Check_Mouse()
	{
		global::UnityEngine.Ray ray = global::UnityEngine.GameObject.Find("UI Camera").camera.ScreenPointToRay(global::UnityEngine.Input.mousePosition);
		global::UnityEngine.RaycastHit2D rayIntersection = global::UnityEngine.Physics2D.GetRayIntersection(ray, float.PositiveInfinity);
		if (rayIntersection.collider != null)
		{
			if (rayIntersection.collider.name == "COL_Girl_Top" || rayIntersection.collider.name == "COL_Girl_Bot")
			{
				SR_Cursor.color = global::UnityEngine.Color.Lerp(SR_Cursor.color, color_ON, global::UnityEngine.Time.deltaTime * 5f);
			}
			else
			{
				SR_Cursor.color = global::UnityEngine.Color.Lerp(SR_Cursor.color, color_OFF, global::UnityEngine.Time.deltaTime * 20f);
			}
		}
		else
		{
			SR_Cursor.color = global::UnityEngine.Color.Lerp(SR_Cursor.color, color_OFF, global::UnityEngine.Time.deltaTime * 20f);
		}
		if (SR_Cursor.color.a > 0.98f)
		{
			float num = global::UnityEngine.Mathf.Lerp(SR_Cursor.GetComponent<global::UnityEngine.RectTransform>().localScale.x, 50f + global::UnityEngine.Mathf.Sin(Life_Timer * 8f) * 10f, global::UnityEngine.Time.deltaTime * 12f);
			SR_Cursor.GetComponent<global::UnityEngine.RectTransform>().localScale = new global::UnityEngine.Vector3(num, num, 1f);
		}
		else
		{
			SR_Cursor.GetComponent<global::UnityEngine.RectTransform>().localScale = global::UnityEngine.Vector3.Lerp(SR_Cursor.GetComponent<global::UnityEngine.RectTransform>().localScale, new global::UnityEngine.Vector3(50f, 50f, 1f), global::UnityEngine.Time.deltaTime * 5f);
		}
		float num2 = 1920f / (float)global::UnityEngine.Screen.width;
		pos_Target = new global::UnityEngine.Vector3(global::UnityEngine.Input.mousePosition.x * num2 - 960f, global::UnityEngine.Input.mousePosition.y * num2 - 540f, 0f);
		SR_Cursor.GetComponent<global::UnityEngine.RectTransform>().localPosition = global::UnityEngine.Vector3.Lerp(SR_Cursor.GetComponent<global::UnityEngine.RectTransform>().localPosition, pos_Target, global::UnityEngine.Time.deltaTime * 12f);
	}

	private void Sound_Moan_Start()
	{
		if (H_Sound != null && On_Moan_Hold)
		{
			H_Sound.Sound_Moan(14, 1);
			On_Moan_Hold = false;
		}
	}

	private void Sound_Moan_End()
	{
		if (!(H_Sound != null) || !On_Moan_Exit)
		{
			return;
		}
		if (Moan_Hold_Timer > 0.3f)
		{
			H_Sound.Sound_Moan(11, 1);
			Moan_Timer = 0.4f;
		}
		else if (Moan_Timer <= 0f)
		{
			int num = global::UnityEngine.Random.Range(1, 5);
			if (Moan_Num == num)
			{
				num = ((num >= 4) ? 1 : (num + 1));
			}
			switch (num)
			{
			case 1:
				H_Sound.Sound_Moan(12, 1);
				break;
			case 2:
				H_Sound.Sound_Moan(3, 1);
				break;
			case 3:
				H_Sound.Sound_Moan(13, 1);
				break;
			default:
				H_Sound.Sound_Moan(8, 1);
				break;
			}
			Moan_Num = num;
			Moan_Timer = 0.4f;
		}
		On_Moan_Hold = false;
		Moan_Hold_Timer = 0f;
		On_Moan_Exit = false;
	}
}
