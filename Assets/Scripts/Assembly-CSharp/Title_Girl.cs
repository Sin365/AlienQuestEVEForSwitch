public class Title_Girl : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.Sprite spr_EyeBrow;

	public global::UnityEngine.Sprite spr_EyeBrow_0;

	public global::UnityEngine.Sprite spr_Close_1;

	public global::UnityEngine.Sprite spr_Close_2;

	public global::UnityEngine.Sprite spr_Close_3;

	public global::UnityEngine.SpriteRenderer SR_Eye_L;

	public global::UnityEngine.SpriteRenderer SR_Eye_R;

	public global::UnityEngine.SpriteRenderer SR_EyeHL_L_1;

	public global::UnityEngine.SpriteRenderer SR_EyeHL_L_2;

	public global::UnityEngine.SpriteRenderer SR_EyeHL_L_3;

	public global::UnityEngine.SpriteRenderer SR_EyeHL_R_1;

	public global::UnityEngine.SpriteRenderer SR_EyeHL_R_2;

	public global::UnityEngine.SpriteRenderer SR_EyeHL_R_3;

	public global::UnityEngine.SpriteRenderer SR_EyeClose;

	public global::UnityEngine.SkinnedMeshRenderer SR_Mouth_1;

	public global::UnityEngine.SkinnedMeshRenderer SR_Mouth_2;

	public global::UnityEngine.SkinnedMeshRenderer Smr_Body;

	public global::UnityEngine.SkinnedMeshRenderer Smr_Leg;

	public global::UnityEngine.SkinnedMeshRenderer Smr_Arm_R1;

	public global::UnityEngine.SkinnedMeshRenderer Smr_Arm_R2;

	public global::UnityEngine.SkinnedMeshRenderer Smr_Hand;

	public global::UnityEngine.SkinnedMeshRenderer Smr_Breast;

	private bool onCloth = false;

	public global::UnityEngine.Material Mtl_Body_Cloth;

	public global::UnityEngine.Material Mtl_Body_Naked;

	public global::UnityEngine.Material Mtl_Arm_Cloth;

	public global::UnityEngine.Material Mtl_Arm_Naked;

	public global::UnityEngine.SpriteRenderer CensoredBox_1;

	public global::UnityEngine.SpriteRenderer CensoredBox_2;

	public global::UnityEngine.SpriteRenderer CensoredText_1;

	public global::UnityEngine.SpriteRenderer CensoredText_2;

	private int Ani_Index = -1;

	private float Ani_Timer;

	private float EyeClose_Timer;

	private float EyePos_Timer;

	private global::UnityEngine.Vector3 pos_Eye_L;

	private global::UnityEngine.Vector3 pos_Eye_R;

	private global::UnityEngine.Vector3 pos_EyeHL_L_1;

	private global::UnityEngine.Vector3 pos_EyeHL_L_2;

	private global::UnityEngine.Vector3 pos_EyeHL_L_3;

	private global::UnityEngine.Vector3 pos_EyeHL_R_1;

	private global::UnityEngine.Vector3 pos_EyeHL_R_2;

	private global::UnityEngine.Vector3 pos_EyeHL_R_3;

	private void Start()
	{
		pos_Eye_L = SR_Eye_L.transform.localPosition;
		pos_Eye_R = SR_Eye_R.transform.localPosition;
		pos_EyeHL_L_1 = SR_EyeHL_L_1.transform.localPosition;
		pos_EyeHL_L_2 = SR_EyeHL_L_2.transform.localPosition;
		pos_EyeHL_L_3 = SR_EyeHL_L_3.transform.localPosition;
		pos_EyeHL_R_1 = SR_EyeHL_R_1.transform.localPosition;
		pos_EyeHL_R_2 = SR_EyeHL_R_2.transform.localPosition;
		pos_EyeHL_R_3 = SR_EyeHL_R_3.transform.localPosition;
		if (global::UnityEngine.PlayerPrefs.GetInt("Game_Saved") > 0)
		{
			if (global::UnityEngine.Random.Range(0, 10) > 1)
			{
				SR_Mouth_1.enabled = true;
				SR_Mouth_2.enabled = false;
			}
			else
			{
				SR_Mouth_1.enabled = false;
				SR_Mouth_2.enabled = true;
			}
			if (global::UnityEngine.PlayerPrefs.GetInt("TitleCloth_Off") == 1)
			{
				onCloth = false;
			}
		}
		if (!onCloth)
		{
			Smr_Body.material = Mtl_Body_Naked;
			Smr_Leg.material = Mtl_Body_Naked;
			Smr_Breast.material = Mtl_Body_Naked;
			Smr_Arm_R1.material = Mtl_Arm_Naked;
			Smr_Arm_R2.material = Mtl_Arm_Naked;
			Smr_Hand.material = Mtl_Arm_Naked;
			if (global::UnityEngine.PlayerPrefs.GetInt("Censorship") == 1)
			{
				CensoredText_1.enabled = true;
				CensoredText_2.enabled = true;
				CensoredBox_1.enabled = true;
				CensoredBox_2.enabled = true;
			}
		}
	}

	private void OnOff_Cloth()
	{
		if (!onCloth)
		{
			onCloth = true;
			global::UnityEngine.PlayerPrefs.SetInt("TitleCloth_Off", 0);
			global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_MenuOff");
			Smr_Body.material = Mtl_Body_Cloth;
			Smr_Leg.material = Mtl_Body_Cloth;
			Smr_Breast.material = Mtl_Body_Cloth;
			Smr_Arm_R1.material = Mtl_Arm_Cloth;
			Smr_Arm_R2.material = Mtl_Arm_Cloth;
			Smr_Hand.material = Mtl_Arm_Cloth;
			CensoredText_1.enabled = false;
			CensoredText_2.enabled = false;
			CensoredBox_1.enabled = false;
			CensoredBox_2.enabled = false;
			return;
		}
		onCloth = false;
		global::UnityEngine.PlayerPrefs.SetInt("TitleCloth_Off", 1);
		global::UnityEngine.GameObject.Find("UI_SoundList").SendMessage("Sound_MenuOn");
		Smr_Body.material = Mtl_Body_Naked;
		Smr_Leg.material = Mtl_Body_Naked;
		Smr_Breast.material = Mtl_Body_Naked;
		Smr_Arm_R1.material = Mtl_Arm_Naked;
		Smr_Arm_R2.material = Mtl_Arm_Naked;
		Smr_Hand.material = Mtl_Arm_Naked;
		if (global::UnityEngine.PlayerPrefs.GetInt("Censorship") == 1)
		{
			CensoredText_1.enabled = true;
			CensoredText_2.enabled = true;
			CensoredBox_1.enabled = true;
			CensoredBox_2.enabled = true;
		}
	}

	private void Update()
	{
		EyePos_Timer += global::UnityEngine.Time.deltaTime;
		if (EyePos_Timer > 0.08f)
		{
			EyePos_Timer = 0f;
			float num = (float)global::UnityEngine.Random.Range(0, 50) * 0.0001f;
			float num2 = (float)global::UnityEngine.Random.Range(0, 50) * 0.0001f;
			SR_Eye_L.transform.localPosition = new global::UnityEngine.Vector3(pos_Eye_L.x + num, pos_Eye_L.y + num2, 0f);
			SR_Eye_R.transform.localPosition = new global::UnityEngine.Vector3(pos_Eye_R.x + num, pos_Eye_R.y + num2, 0f);
			num = (float)global::UnityEngine.Random.Range(0, 100) * 0.0001f;
			num2 = (float)global::UnityEngine.Random.Range(0, 100) * 0.0001f;
			SR_EyeHL_L_1.transform.localPosition = new global::UnityEngine.Vector3(pos_EyeHL_L_1.x + num, pos_EyeHL_L_1.y + num2, 0f);
			SR_EyeHL_R_1.transform.localPosition = new global::UnityEngine.Vector3(pos_EyeHL_R_1.x + num, pos_EyeHL_R_1.y + num2, 0f);
			num = (float)global::UnityEngine.Random.Range(0, 100) * 0.0001f;
			num2 = (float)global::UnityEngine.Random.Range(0, 100) * 0.0001f;
			SR_EyeHL_L_2.transform.localPosition = new global::UnityEngine.Vector3(pos_EyeHL_L_2.x + num, pos_EyeHL_L_2.y + num2, 0f);
			SR_EyeHL_R_2.transform.localPosition = new global::UnityEngine.Vector3(pos_EyeHL_R_2.x + num, pos_EyeHL_R_2.y + num2, 0f);
			num = (float)global::UnityEngine.Random.Range(0, 100) * 0.0001f;
			num2 = (float)global::UnityEngine.Random.Range(0, 100) * 0.0001f;
			SR_EyeHL_L_3.transform.localPosition = new global::UnityEngine.Vector3(pos_EyeHL_L_3.x + num, pos_EyeHL_L_3.y + num2, 0f);
			SR_EyeHL_R_3.transform.localPosition = new global::UnityEngine.Vector3(pos_EyeHL_R_3.x + num, pos_EyeHL_R_3.y + num2, 0f);
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
				{
					SR_EyeClose.sprite = spr_EyeBrow;
					Ani_Index = -1;
					EyeClose_Timer = 0f;
					global::UnityEngine.SpriteRenderer sR_EyeHL_L_ = SR_EyeHL_L_1;
					int num3 = 55;
					SR_EyeHL_L_3.sortingOrder = num3;
					num3 = num3;
					SR_EyeHL_L_2.sortingOrder = num3;
					sR_EyeHL_L_.sortingOrder = num3;
					global::UnityEngine.SpriteRenderer sR_EyeHL_R_ = SR_EyeHL_R_1;
					num3 = 55;
					SR_EyeHL_R_3.sortingOrder = num3;
					num3 = num3;
					SR_EyeHL_R_2.sortingOrder = num3;
					sR_EyeHL_R_.sortingOrder = num3;
					break;
				}
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
				global::UnityEngine.SpriteRenderer sR_EyeHL_L_2 = SR_EyeHL_L_1;
				int num4 = 53;
				SR_EyeHL_L_3.sortingOrder = num4;
				num4 = num4;
				SR_EyeHL_L_2.sortingOrder = num4;
				sR_EyeHL_L_2.sortingOrder = num4;
				global::UnityEngine.SpriteRenderer sR_EyeHL_R_2 = SR_EyeHL_R_1;
				num4 = 53;
				SR_EyeHL_R_3.sortingOrder = num4;
				num4 = num4;
				SR_EyeHL_R_2.sortingOrder = num4;
				sR_EyeHL_R_2.sortingOrder = num4;
			}
		}
	}
}
