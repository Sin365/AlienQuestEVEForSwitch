public class Ending_Ship : global::UnityEngine.MonoBehaviour
{
	public int State;

	private float Life_Timer;

	private global::UnityEngine.Color color_Glow_C;

	private global::UnityEngine.Color color_Glow_S;

	private global::UnityEngine.Color color_Cap;

	private global::UnityEngine.Color color_Cap_Blue;

	private global::UnityEngine.Color color_Cap_Glow_1;

	private global::UnityEngine.Color color_Cap_Glow_2;

	private global::UnityEngine.Color color_Cap_Glow_3;

	private float Glow_S_Timer;

	private float Glow_S_Opacity;

	private float Cap_Glow_1_Timer;

	private float Cap_Glow_2_Timer;

	private float Cap_Glow_1_Opacity;

	private float Cap_Glow_2_Opacity;

	private global::UnityEngine.Vector3 pos_Capsule_C;

	private global::UnityEngine.Vector3 pos_Capsule_L;

	private global::UnityEngine.Vector3 pos_Capsule_R;

	private global::UnityEngine.Vector3 target_Capsule_C;

	private global::UnityEngine.Vector3 target_Capsule_L;

	private global::UnityEngine.Vector3 target_Capsule_R;

	private global::UnityEngine.Vector3 target_EggBox_Top;

	private global::UnityEngine.Vector3 target_EggBox_Bot;

	public global::UnityEngine.SpriteRenderer Glow_Circle;

	public global::UnityEngine.SpriteRenderer Glow_Straight;

	public global::UnityEngine.SpriteRenderer Capsule_C;

	public global::UnityEngine.SpriteRenderer Capsule_L;

	public global::UnityEngine.SpriteRenderer Capsule_R;

	public global::UnityEngine.SpriteRenderer Capsule_Blue;

	public global::UnityEngine.SpriteRenderer Capsule_Glow_1;

	public global::UnityEngine.SpriteRenderer Capsule_Glow_2;

	public global::UnityEngine.SpriteRenderer Capsule_Glow_3;

	public global::UnityEngine.SpriteRenderer Ellen_Back;

	public global::UnityEngine.SpriteRenderer Ellen_Sleep;

	public global::UnityEngine.SpriteRenderer EggBox_Glow;

	public global::UnityEngine.SpriteRenderer Monitor_1;

	public global::UnityEngine.SpriteRenderer Monitor_2;

	public global::UnityEngine.Transform EggBox_Top;

	public global::UnityEngine.Transform EggBox_Bot;

	private void Start()
	{
		color_Glow_C = Glow_Circle.color;
		color_Glow_S = Glow_Straight.color;
		Glow_S_Opacity = color_Glow_S.a;
		color_Cap = Capsule_C.color;
		color_Cap_Blue = Capsule_Blue.color;
		color_Cap_Glow_1 = Capsule_Glow_1.color;
		color_Cap_Glow_2 = Capsule_Glow_2.color;
		color_Cap_Glow_3 = Capsule_Glow_3.color;
		Cap_Glow_1_Opacity = color_Cap_Glow_1.a;
		Cap_Glow_2_Opacity = color_Cap_Glow_2.a;
		Capsule_Blue.color = new global::UnityEngine.Color(Capsule_Blue.color.r, Capsule_Blue.color.g, Capsule_Blue.color.b, 0f);
		Capsule_Glow_1.color = new global::UnityEngine.Color(Capsule_Glow_1.color.r, Capsule_Glow_1.color.g, Capsule_Glow_1.color.b, 0f);
		Capsule_Glow_2.color = new global::UnityEngine.Color(Capsule_Glow_2.color.r, Capsule_Glow_2.color.g, Capsule_Glow_2.color.b, 0f);
		Capsule_Glow_3.color = new global::UnityEngine.Color(Capsule_Glow_3.color.r, Capsule_Glow_3.color.g, Capsule_Glow_3.color.b, 0f);
		EggBox_Glow.color = new global::UnityEngine.Color(1f, 0f, 0f, 0f);
		Monitor_1.color = new global::UnityEngine.Color(1f, 0f, 0f, 0f);
		Monitor_2.color = new global::UnityEngine.Color(1f, 0f, 0f, 0f);
		Ellen_Back.enabled = true;
		Ellen_Sleep.enabled = false;
	}

	private void Update()
	{
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (Glow_S_Timer < 0f)
		{
			Glow_S_Opacity = color_Glow_S.a + global::UnityEngine.Random.Range(-0.1f, 0.1f);
			Glow_S_Timer = global::UnityEngine.Random.Range(0.1f, 1f);
		}
		else
		{
			Glow_S_Timer -= global::UnityEngine.Time.deltaTime;
		}
		Glow_Straight.color = global::UnityEngine.Color.Lerp(Glow_Straight.color, new global::UnityEngine.Color(Glow_Straight.color.r, Glow_Straight.color.g, Glow_Straight.color.b, Glow_S_Opacity), global::UnityEngine.Time.deltaTime * 1f);
		if (State == 1)
		{
			Capsule_C.transform.position = global::UnityEngine.Vector3.Lerp(Capsule_C.transform.position, target_Capsule_C, global::UnityEngine.Time.deltaTime * 0.3f);
			Capsule_L.transform.position = global::UnityEngine.Vector3.Lerp(Capsule_L.transform.position, target_Capsule_L, global::UnityEngine.Time.deltaTime * 0.3f);
			Capsule_R.transform.position = global::UnityEngine.Vector3.Lerp(Capsule_R.transform.position, target_Capsule_R, global::UnityEngine.Time.deltaTime * 0.3f);
		}
		else if (State == 2)
		{
			Ellen_Back.enabled = false;
			Ellen_Sleep.enabled = true;
		}
		else if (State == 3)
		{
			Capsule_C.transform.position = global::UnityEngine.Vector3.Lerp(Capsule_C.transform.position, pos_Capsule_C, global::UnityEngine.Time.deltaTime * 1f);
			Capsule_L.transform.position = global::UnityEngine.Vector3.Lerp(Capsule_L.transform.position, pos_Capsule_L, global::UnityEngine.Time.deltaTime * 1f);
			Capsule_R.transform.position = global::UnityEngine.Vector3.Lerp(Capsule_R.transform.position, pos_Capsule_R, global::UnityEngine.Time.deltaTime * 1f);
		}
		else if (State == 4)
		{
			Capsule_Blue.color = global::UnityEngine.Color.Lerp(Capsule_Blue.color, color_Cap_Blue, global::UnityEngine.Time.deltaTime * 1f);
			Capsule_Glow_3.color = global::UnityEngine.Color.Lerp(Capsule_Glow_3.color, color_Cap_Glow_3, global::UnityEngine.Time.deltaTime * 1f);
			Capsule_C.color = global::UnityEngine.Color.Lerp(Capsule_C.color, new global::UnityEngine.Color(color_Cap.r, color_Cap.g, color_Cap.b, 0f), global::UnityEngine.Time.deltaTime * 1f);
			global::UnityEngine.SpriteRenderer capsule_L = Capsule_L;
			global::UnityEngine.Color color = Capsule_C.color;
			Capsule_R.color = color;
			capsule_L.color = color;
		}
		else if (State == 5)
		{
			EggBox_Glow.color = global::UnityEngine.Color.Lerp(EggBox_Glow.color, new global::UnityEngine.Color(EggBox_Glow.color.r, EggBox_Glow.color.g, EggBox_Glow.color.b, 1f), global::UnityEngine.Time.deltaTime * 2f);
		}
		else if (State == 6)
		{
			EggBox_Top.position = global::UnityEngine.Vector3.Lerp(EggBox_Top.position, target_EggBox_Top, global::UnityEngine.Time.deltaTime * 0.8f);
			EggBox_Bot.position = global::UnityEngine.Vector3.Lerp(EggBox_Bot.position, target_EggBox_Bot, global::UnityEngine.Time.deltaTime * 0.8f);
			EggBox_Glow.color = global::UnityEngine.Color.Lerp(EggBox_Glow.color, new global::UnityEngine.Color(EggBox_Glow.color.r, EggBox_Glow.color.g, EggBox_Glow.color.b, 0f), global::UnityEngine.Time.deltaTime * 10f);
		}
		if (State > 3)
		{
			Capsule_Glow_1.color = global::UnityEngine.Color.Lerp(Capsule_Glow_1.color, new global::UnityEngine.Color(color_Cap_Glow_1.r, color_Cap_Glow_1.g, color_Cap_Glow_1.b, 0.8f + global::UnityEngine.Mathf.Sin(Life_Timer * 3f) * 0.2f), global::UnityEngine.Time.deltaTime * 20f);
			Capsule_Glow_2.color = global::UnityEngine.Color.Lerp(Capsule_Glow_2.color, new global::UnityEngine.Color(color_Cap_Glow_2.r, color_Cap_Glow_2.g, color_Cap_Glow_2.b, 0.75f + global::UnityEngine.Mathf.Sin(Life_Timer * 4f) * 0.3f), global::UnityEngine.Time.deltaTime * 20f);
		}
	}

	public void Reset_Pos()
	{
		pos_Capsule_C = Capsule_C.transform.position;
		pos_Capsule_L = Capsule_L.transform.position;
		pos_Capsule_R = Capsule_R.transform.position;
		target_Capsule_C = new global::UnityEngine.Vector3(pos_Capsule_C.x, pos_Capsule_C.y + 2.2f, 0f);
		target_Capsule_L = new global::UnityEngine.Vector3(pos_Capsule_L.x - 1.2f, pos_Capsule_L.y, 0f);
		target_Capsule_R = new global::UnityEngine.Vector3(pos_Capsule_R.x + 1.2f, pos_Capsule_R.y, 0f);
		target_EggBox_Top = new global::UnityEngine.Vector3(EggBox_Top.transform.position.x, EggBox_Top.transform.position.y + 1f, 0f);
		target_EggBox_Bot = new global::UnityEngine.Vector3(EggBox_Bot.transform.position.x, EggBox_Bot.transform.position.y - 1f, 0f);
	}
}
