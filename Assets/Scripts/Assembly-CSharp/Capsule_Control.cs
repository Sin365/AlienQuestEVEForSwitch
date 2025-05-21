public class Capsule_Control : global::UnityEngine.MonoBehaviour
{
	private int State;

	private bool isChange;

	private float Life_Timer;

	private float Size = 1f;

	private float Opacity_Timer;

	private float Start_Timer;

	private float Body_L_Timer;

	private float Flicker_Timer;

	private float Black_Opacity = 1f;

	private float Glow_2_Opacity;

	private float Glow_1_Opacity;

	private float Glow_Head_Opacity;

	private float Glow_Body_Opacity;

	private float Body_Opacity;

	private float Body_L_Opacity;

	private float CapOp_Opacity = 0.5f;

	private float Blue_Opacity;

	private float Capsule_Opacity;

	private float CamPos_Timer;

	private global::UnityEngine.Vector3 Pos_Orig;

	public global::UnityEngine.GameObject Black_Gra;

	public global::UnityEngine.GameObject Glow_2;

	public global::UnityEngine.GameObject Glow_1;

	public global::UnityEngine.GameObject Glow_0;

	public global::UnityEngine.GameObject Glow_Head;

	public global::UnityEngine.GameObject Glow_Body;

	public global::UnityEngine.GameObject Opacity;

	public global::UnityEngine.GameObject Blue;

	public global::UnityEngine.GameObject Hair;

	public global::UnityEngine.GameObject Body;

	public global::UnityEngine.GameObject Hair_L;

	public global::UnityEngine.GameObject Body_L;

	public global::UnityEngine.GameObject Capsule_Top;

	public global::UnityEngine.GameObject Capsule_Mid;

	public global::UnityEngine.GameObject Capsule_Bot;

	private void Start()
	{
		Pos_Orig = base.transform.position;
		Reset();
		Set_Opacity();
	}

	private void Reset_Index()
	{
	}

	private void Update()
	{
		Life_Timer += global::UnityEngine.Time.deltaTime;
		Size = 1f + global::UnityEngine.Mathf.Sin(Life_Timer) * 0.01f;
		base.transform.localScale = new global::UnityEngine.Vector3(Size, Size, 1f);
		if (CamPos_Timer > 0.02f)
		{
			CamPos_Timer = 0f;
			base.transform.position = new global::UnityEngine.Vector3(Pos_Orig.x + (float)global::UnityEngine.Random.Range(-20, 20) * 0.001f, Pos_Orig.y + (float)global::UnityEngine.Random.Range(-20, 20) * 0.001f, 0f);
		}
		else
		{
			CamPos_Timer += global::UnityEngine.Time.deltaTime;
		}
		if (Flicker_Timer > 0.05f)
		{
			Flicker_Timer = 0f;
			float a = 0.6f + (float)global::UnityEngine.Random.Range(0, 40) * 0.01f;
			Glow_0.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, a);
		}
		else
		{
			Flicker_Timer += global::UnityEngine.Time.deltaTime;
		}
		if (Black_Opacity != 0f)
		{
			Black_Opacity = global::UnityEngine.Mathf.Lerp(Black_Opacity, 0f, global::UnityEngine.Time.deltaTime * 0.2f);
			Black_Gra.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, Black_Opacity);
		}
		if (State == 0)
		{
			Start_Timer += global::UnityEngine.Time.deltaTime * 2f;
			Opacity_Timer += global::UnityEngine.Time.deltaTime * 2f;
			Glow_2_Opacity = 1f - (1f + global::UnityEngine.Mathf.Cos(Opacity_Timer)) * 0.5f;
			Glow_1_Opacity = 0.2f + Glow_2_Opacity * 0.8f;
			if (Glow_Head_Opacity > 0f)
			{
				Glow_Head_Opacity = global::UnityEngine.Mathf.Lerp(Glow_Head_Opacity, 0f, global::UnityEngine.Time.deltaTime * 2f);
			}
			if (Glow_Body_Opacity > 0f)
			{
				Glow_Body_Opacity = global::UnityEngine.Mathf.Lerp(Glow_Body_Opacity, 0f, global::UnityEngine.Time.deltaTime * 2f);
			}
			if (Body_Opacity > 0f)
			{
				Body_Opacity = global::UnityEngine.Mathf.Lerp(Body_Opacity, 0f, global::UnityEngine.Time.deltaTime * 2f);
			}
			if (Body_L_Opacity > 0f)
			{
				Body_L_Opacity = global::UnityEngine.Mathf.Lerp(Body_L_Opacity, 0f, global::UnityEngine.Time.deltaTime * 2f);
			}
			if (Blue_Opacity < 1f)
			{
				Blue_Opacity = global::UnityEngine.Mathf.Lerp(Blue_Opacity, 1f, global::UnityEngine.Time.deltaTime * 1f);
			}
			if (Capsule_Opacity < 1f)
			{
				Capsule_Opacity = global::UnityEngine.Mathf.Lerp(Capsule_Opacity, 1f, global::UnityEngine.Time.deltaTime * 1f);
			}
			if (Start_Timer > (float)global::System.Math.PI * 4f)
			{
				isChange = false;
				State = 1;
				Body_L_Timer = 0f;
				Start_Timer = 0f;
			}
		}
		else if (State == 1)
		{
			Start_Timer += global::UnityEngine.Time.deltaTime * 3f;
			Opacity_Timer += global::UnityEngine.Time.deltaTime * 3f;
			Body_L_Timer += global::UnityEngine.Time.deltaTime * 2f;
			if (Body_L_Opacity < 0.4f)
			{
				Glow_2_Opacity = 1f - (1f + global::UnityEngine.Mathf.Cos(Opacity_Timer)) * 0.5f;
				Glow_1_Opacity = Glow_2_Opacity;
				Glow_Head_Opacity = Glow_1_Opacity;
				Glow_Body_Opacity = Glow_2_Opacity;
				Body_L_Opacity = 1f - (1f + global::UnityEngine.Mathf.Cos(Body_L_Timer)) * 0.5f;
			}
			else
			{
				Glow_2_Opacity = 1f - (1f + global::UnityEngine.Mathf.Cos(Opacity_Timer)) * 0.5f;
				Glow_1_Opacity = 0.2f + Glow_2_Opacity * 0.8f;
				Glow_Head_Opacity = Glow_1_Opacity;
				Glow_Body_Opacity = Glow_2_Opacity;
				Body_L_Opacity = 1f - (1f + global::UnityEngine.Mathf.Cos(Body_L_Timer)) * 0.5f;
				Body_L_Opacity = 0.4f + Body_L_Opacity * 0.3f;
			}
			if (Blue_Opacity < 1f)
			{
				Blue_Opacity = global::UnityEngine.Mathf.Lerp(Blue_Opacity, 1f, global::UnityEngine.Time.deltaTime * 3f);
			}
			if (Capsule_Opacity < 1f)
			{
				Capsule_Opacity = global::UnityEngine.Mathf.Lerp(Capsule_Opacity, 1f, global::UnityEngine.Time.deltaTime * 3f);
			}
			if (Start_Timer > (float)global::System.Math.PI * 16f)
			{
				isChange = false;
				State = 2;
				Body_L_Timer = 0f;
				Start_Timer = 0f;
			}
		}
		else
		{
			Start_Timer += global::UnityEngine.Time.deltaTime * 5f;
			Opacity_Timer += global::UnityEngine.Time.deltaTime * 5f;
			Glow_2_Opacity = 1f - (1f + global::UnityEngine.Mathf.Cos(Opacity_Timer)) * 0.5f;
			Glow_1_Opacity = 0.2f + Glow_2_Opacity * 0.8f;
			Glow_Head_Opacity = Glow_1_Opacity;
			Glow_Body_Opacity = Glow_2_Opacity;
			if (Blue_Opacity != 0.4f)
			{
				Blue_Opacity = global::UnityEngine.Mathf.Lerp(Blue_Opacity, 0.35f, global::UnityEngine.Time.deltaTime * 3f);
			}
			if (Body_Opacity < 1f)
			{
				Body_Opacity = global::UnityEngine.Mathf.Lerp(Body_Opacity, 1f, global::UnityEngine.Time.deltaTime * 3f);
			}
			if (Body_L_Opacity > 0f)
			{
				Body_L_Opacity = global::UnityEngine.Mathf.Lerp(Body_L_Opacity, 0f, global::UnityEngine.Time.deltaTime * 3f);
			}
			if (Capsule_Opacity > 0f)
			{
				Capsule_Opacity = global::UnityEngine.Mathf.Lerp(Capsule_Opacity, 0f, global::UnityEngine.Time.deltaTime * 3f);
			}
			if (isChange)
			{
				isChange = false;
				State = 0;
				Opacity_Timer = 0f;
				Start_Timer = 0f;
			}
		}
		Set_Opacity();
		global::UnityEngine.Vector3 localScale = new global::UnityEngine.Vector3(1f + (1f + global::UnityEngine.Mathf.Sin(Life_Timer * 3.2f)) * 0.008f, 1f, 1f);
		Glow_Head.transform.localScale = localScale;
		Hair.transform.localScale = localScale;
		Hair_L.transform.localScale = localScale;
		if (!isChange && global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.Space))
		{
			isChange = true;
		}
	}

	private void Reset()
	{
		State = 0;
		Glow_2_Opacity = 0f;
		Glow_1_Opacity = 0f;
		Glow_Head_Opacity = 0f;
		Glow_Body_Opacity = 0f;
		Body_Opacity = 0f;
		Body_L_Opacity = 0f;
		CapOp_Opacity = 0.5f;
		Blue_Opacity = 1f;
		Capsule_Opacity = 1f;
	}

	private void Set_Opacity()
	{
		Glow_2.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, Glow_2_Opacity);
		Glow_1.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, Glow_1_Opacity);
		Glow_Head.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, Glow_Head_Opacity);
		Glow_Body.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, Glow_Body_Opacity);
		Hair.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, Body_Opacity);
		Body.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, Body_Opacity);
		Hair_L.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, Body_L_Opacity);
		Body_L.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, Body_L_Opacity);
		Opacity.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, CapOp_Opacity);
		Blue.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, Blue_Opacity);
		Capsule_Top.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, Capsule_Opacity);
		Capsule_Bot.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, Capsule_Opacity);
		Capsule_Mid.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, Capsule_Opacity);
	}
}
