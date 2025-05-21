public class Item_Glow : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject ItemObject;

	public global::UnityEngine.Transform GlowBox;

	public global::UnityEngine.Transform GlowCircle;

	public global::UnityEngine.Transform GlowInner;

	public global::UnityEngine.SpriteRenderer SR_Box_1;

	public global::UnityEngine.SpriteRenderer SR_Box_2;

	public global::UnityEngine.SpriteRenderer SR_Circle;

	public global::UnityEngine.SpriteRenderer SR_Inner_1;

	public global::UnityEngine.SpriteRenderer SR_Inner_2;

	public global::UnityEngine.SpriteRenderer SR_Over;

	private global::UnityEngine.Vector3 sizeBox_Orig;

	private global::UnityEngine.Vector3 sizeBox_Target;

	private global::UnityEngine.Vector3 sizeCircle_Orig;

	private global::UnityEngine.Vector3 sizeCircle_Target;

	private global::UnityEngine.Color colorBox_Orig;

	private global::UnityEngine.Color colorBox_Target;

	private global::UnityEngine.Color colorCircle_Orig;

	private global::UnityEngine.Color colorCircle_Target;

	private global::UnityEngine.Vector3 sizeInner_Orig;

	private global::UnityEngine.Vector3 sizeInner_Target;

	private global::UnityEngine.Color colorInner_Orig;

	private global::UnityEngine.Color colorInner_Target;

	private global::UnityEngine.Color colorOver_Orig;

	private global::UnityEngine.Color colorOver_Target;

	private bool onEnd;

	private float End_Timer;

	private float State_Size = 0.6f;

	private float Life_Timer;

	private float color_Timer;

	private float size_Timer;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		sizeBox_Orig = (sizeBox_Target = GlowBox.localScale);
		sizeCircle_Orig = (sizeCircle_Target = GlowCircle.localScale);
		colorBox_Orig = (colorBox_Target = SR_Box_1.color);
		colorCircle_Orig = (colorCircle_Target = SR_Circle.color);
		sizeInner_Orig = (sizeInner_Target = GlowInner.localScale);
		colorInner_Orig = (colorInner_Target = SR_Inner_1.color);
		colorOver_Orig = (colorOver_Target = SR_Over.color);
		global::UnityEngine.SpriteRenderer sR_Box_ = SR_Box_2;
		global::UnityEngine.Color color = new global::UnityEngine.Color(colorBox_Orig.r, colorBox_Orig.g, colorBox_Orig.b, 0f);
		SR_Box_1.color = color;
		sR_Box_.color = color;
		SR_Circle.color = new global::UnityEngine.Color(colorCircle_Orig.r, colorCircle_Orig.g, colorCircle_Orig.b, 0f);
		global::UnityEngine.SpriteRenderer sR_Inner_ = SR_Inner_2;
		color = new global::UnityEngine.Color(colorInner_Orig.r, colorInner_Orig.g, colorInner_Orig.b, 0f);
		SR_Inner_1.color = color;
		sR_Inner_.color = color;
		SR_Over.color = new global::UnityEngine.Color(colorOver_Orig.r, colorOver_Orig.g, colorOver_Orig.b, 0f);
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (!onEnd)
		{
			State_Size = 0.6f;
			if (size_Timer > 0f)
			{
				size_Timer -= global::UnityEngine.Time.deltaTime;
			}
			else
			{
				size_Timer = 0.1f;
				sizeBox_Target = new global::UnityEngine.Vector3(sizeBox_Orig.x * global::UnityEngine.Random.Range(0.1f, 1f) * State_Size, sizeBox_Orig.y * global::UnityEngine.Random.Range(0.8f, 1.2f), 1f);
				sizeCircle_Target = new global::UnityEngine.Vector3(sizeCircle_Orig.x * global::UnityEngine.Random.Range(0.75f, 1.1f) * State_Size, sizeCircle_Orig.y * global::UnityEngine.Random.Range(9f, 1.1f), 1f);
				sizeInner_Target = new global::UnityEngine.Vector3(global::UnityEngine.Random.Range(0.02f, 0.1f) * State_Size, sizeInner_Orig.y * global::UnityEngine.Random.Range(9f, 1.05f), 1f);
			}
			GlowBox.localScale = global::UnityEngine.Vector3.Lerp(GlowBox.localScale, sizeBox_Target, global::UnityEngine.Time.deltaTime * 5f);
			GlowCircle.localScale = global::UnityEngine.Vector3.Lerp(GlowCircle.localScale, sizeCircle_Target, global::UnityEngine.Time.deltaTime * 5f);
			GlowInner.localScale = global::UnityEngine.Vector3.Lerp(GlowInner.localScale, sizeInner_Target, global::UnityEngine.Time.deltaTime * 5f);
			if (color_Timer > 0f)
			{
				color_Timer -= global::UnityEngine.Time.deltaTime;
			}
			else
			{
				color_Timer = 0.2f;
				colorBox_Target = new global::UnityEngine.Color(colorBox_Orig.r, colorBox_Orig.g, colorBox_Orig.b, global::UnityEngine.Random.Range(0.1f, 1f));
				colorCircle_Target = new global::UnityEngine.Color(colorCircle_Orig.r, colorCircle_Orig.g, colorCircle_Orig.b, global::UnityEngine.Random.Range(0.05f, 0.25f));
				colorInner_Target = new global::UnityEngine.Color(colorInner_Orig.r, colorInner_Orig.g, colorInner_Orig.b, global::UnityEngine.Random.Range(0.1f, 0.8f));
				colorOver_Target = new global::UnityEngine.Color(colorOver_Orig.r, colorOver_Orig.g, colorOver_Orig.b, global::UnityEngine.Random.Range(0.2f, 0.25f));
			}
			global::UnityEngine.SpriteRenderer sR_Box_ = SR_Box_2;
			global::UnityEngine.Color color = global::UnityEngine.Color.Lerp(SR_Box_1.color, colorBox_Target, global::UnityEngine.Time.deltaTime * 5f);
			SR_Box_1.color = color;
			sR_Box_.color = color;
			SR_Circle.color = global::UnityEngine.Color.Lerp(SR_Circle.color, colorCircle_Target, global::UnityEngine.Time.deltaTime * 5f);
			global::UnityEngine.SpriteRenderer sR_Inner_ = SR_Inner_2;
			color = global::UnityEngine.Color.Lerp(SR_Inner_1.color, colorInner_Target, global::UnityEngine.Time.deltaTime * 5f);
			SR_Inner_1.color = color;
			sR_Inner_.color = color;
			SR_Over.color = global::UnityEngine.Color.Lerp(SR_Over.color, colorOver_Target, global::UnityEngine.Time.deltaTime * 5f);
			if (ItemObject == null)
			{
				EndGlow();
			}
		}
		else
		{
			GlowBox.localScale = global::UnityEngine.Vector3.Lerp(GlowBox.localScale, sizeBox_Target, global::UnityEngine.Time.deltaTime * 5f);
			GlowCircle.localScale = global::UnityEngine.Vector3.Lerp(GlowCircle.localScale, sizeCircle_Target, global::UnityEngine.Time.deltaTime * 5f);
			global::UnityEngine.SpriteRenderer sR_Box_2 = SR_Box_2;
			global::UnityEngine.Color color = global::UnityEngine.Color.Lerp(SR_Box_1.color, colorBox_Target, global::UnityEngine.Time.deltaTime * 5f);
			SR_Box_1.color = color;
			sR_Box_2.color = color;
			SR_Circle.color = global::UnityEngine.Color.Lerp(SR_Circle.color, colorCircle_Target, global::UnityEngine.Time.deltaTime * 5f);
			SR_Over.color = global::UnityEngine.Color.Lerp(SR_Over.color, colorOver_Target, global::UnityEngine.Time.deltaTime * 4.5f);
			End_Timer += global::UnityEngine.Time.deltaTime;
			if (End_Timer > 2f || SR_Box_1.color.a < 0.01f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	private void EndGlow()
	{
		onEnd = true;
		sizeBox_Target = new global::UnityEngine.Vector3(sizeBox_Orig.x * 6f, sizeBox_Orig.y * 2f, 1f);
		sizeCircle_Target = new global::UnityEngine.Vector3(sizeCircle_Orig.x * 7f, sizeCircle_Orig.y * 2f, 1f);
		colorBox_Target = new global::UnityEngine.Color(colorBox_Orig.r, colorBox_Orig.g, colorBox_Orig.b, 0f);
		colorCircle_Target = new global::UnityEngine.Color(colorCircle_Orig.r, colorCircle_Orig.g, colorCircle_Orig.b, 0f);
		SR_Inner_1.enabled = false;
		SR_Inner_2.enabled = false;
		colorOver_Target = new global::UnityEngine.Color(colorOver_Orig.r, colorOver_Orig.g, colorOver_Orig.b, 0f);
	}
}
