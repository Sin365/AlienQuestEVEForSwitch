public class Magic_Fire_3_Explo : global::UnityEngine.MonoBehaviour
{
	public bool isMonsters;

	public global::UnityEngine.GameObject Smog;

	public global::UnityEngine.GameObject[] Glow;

	public global::UnityEngine.GameObject bar_Obj;

	private global::UnityEngine.GameObject Bar_C;

	private global::UnityEngine.GameObject Bar_CL;

	private global::UnityEngine.GameObject Bar_CR;

	private float Life_Timer;

	private int Smog_Num;

	private global::UnityEngine.Color color_TrCenter = new global::UnityEngine.Color(1f, 1f, 1f, 2f);

	private global::UnityEngine.Color color_TrCenter_L = new global::UnityEngine.Color(1f, 1f, 1f, 2f);

	private float Center_Size_X = 1f;

	private float Col_Size_X;

	public float Glow_Opacity;

	private float Glow_Size = 1.1f;

	private global::UnityEngine.Color color_Glow = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		Bar_C = global::UnityEngine.Object.Instantiate(bar_Obj, new global::UnityEngine.Vector3(base.transform.position.x, base.transform.position.y, 0f), base.transform.rotation) as global::UnityEngine.GameObject;
		Bar_C.transform.localScale = new global::UnityEngine.Vector3(1f, 0.3f, 1f);
		Bar_C.transform.parent = base.transform;
		Bar_CL = global::UnityEngine.Object.Instantiate(bar_Obj, new global::UnityEngine.Vector3(base.transform.position.x - 0.25f, base.transform.position.y, 0f), base.transform.rotation) as global::UnityEngine.GameObject;
		Bar_CL.transform.localScale = new global::UnityEngine.Vector3(1f, 0.06f, 1f);
		Bar_CL.transform.parent = base.transform;
		Bar_CR = global::UnityEngine.Object.Instantiate(bar_Obj, new global::UnityEngine.Vector3(base.transform.position.x + 0.25f, base.transform.position.y, 0f), base.transform.rotation) as global::UnityEngine.GameObject;
		Bar_CR.transform.localScale = new global::UnityEngine.Vector3(1f, 0.06f, 1f);
		Bar_CR.transform.parent = base.transform;
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Smog, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
		Smog_Num++;
		Glow[0].transform.localScale = new global::UnityEngine.Vector3(0.6f, 1f, 1f);
		Glow[1].transform.localScale = new global::UnityEngine.Vector3(-0.6f, 1f, 1f);
		Glow[2].transform.localScale = new global::UnityEngine.Vector3(0.6f, -1f, 1f);
		Glow[3].transform.localScale = new global::UnityEngine.Vector3(-0.6f, -1f, 1f);
		color_Glow = Glow[0].GetComponent<global::UnityEngine.SpriteRenderer>().color;
		UnityEngine.Camera.main.GetComponent<Camera_Control>().Set_Shake_Timer(0.1f, base.transform.position);
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (Smog_Num == 1 && Life_Timer > 1.3f)
		{
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Smog, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
			Smog_Num++;
		}
		Bar_CL.transform.position = global::UnityEngine.Vector3.Lerp(Bar_CL.transform.position, Bar_C.transform.position, global::UnityEngine.Time.deltaTime * 1.5f);
		Bar_CR.transform.position = global::UnityEngine.Vector3.Lerp(Bar_CR.transform.position, Bar_C.transform.position, global::UnityEngine.Time.deltaTime * 1.5f);
		if (color_TrCenter.a > 0f)
		{
			Bar_C.GetComponent<global::UnityEngine.SpriteRenderer>().color = global::UnityEngine.Color.Lerp(Bar_C.GetComponent<global::UnityEngine.SpriteRenderer>().color, color_TrCenter, global::UnityEngine.Time.deltaTime * 2f);
		}
		else
		{
			Bar_C.GetComponent<global::UnityEngine.SpriteRenderer>().color = global::UnityEngine.Color.Lerp(Bar_C.GetComponent<global::UnityEngine.SpriteRenderer>().color, color_TrCenter, global::UnityEngine.Time.deltaTime * 3f);
		}
		if (Bar_C.GetComponent<global::UnityEngine.SpriteRenderer>().color.a > 0.95f)
		{
			color_TrCenter = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
		}
		Bar_C.transform.localScale = new global::UnityEngine.Vector3(1f, global::UnityEngine.Mathf.Lerp(Bar_C.transform.localScale.y, 10.2f, global::UnityEngine.Time.deltaTime * 5.8f), 1f);
		if (color_TrCenter_L.a > 0f)
		{
			Bar_CL.GetComponent<global::UnityEngine.SpriteRenderer>().color = global::UnityEngine.Color.Lerp(Bar_C.GetComponent<global::UnityEngine.SpriteRenderer>().color, color_TrCenter_L, global::UnityEngine.Time.deltaTime * 2f);
		}
		else
		{
			Bar_CL.GetComponent<global::UnityEngine.SpriteRenderer>().color = global::UnityEngine.Color.Lerp(Bar_C.GetComponent<global::UnityEngine.SpriteRenderer>().color, color_TrCenter_L, global::UnityEngine.Time.deltaTime * 3f);
		}
		Bar_CR.GetComponent<global::UnityEngine.SpriteRenderer>().color = Bar_CL.GetComponent<global::UnityEngine.SpriteRenderer>().color;
		if (Bar_C.GetComponent<global::UnityEngine.SpriteRenderer>().color.a > 0.95f)
		{
			color_TrCenter_L = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
		}
		Bar_CL.transform.localScale = new global::UnityEngine.Vector3(1f, global::UnityEngine.Mathf.Lerp(Bar_CL.transform.localScale.y, 10.2f, global::UnityEngine.Time.deltaTime * 3.8f), 1f);
		Bar_CR.transform.localScale = Bar_CL.transform.localScale;
		Glow_Size = global::UnityEngine.Mathf.Lerp(Glow_Size, 1.3f, global::UnityEngine.Time.deltaTime * 1f);
		Glow[0].transform.localScale = new global::UnityEngine.Vector3(Glow_Size, 1f, 1f);
		Glow[1].transform.localScale = new global::UnityEngine.Vector3(0f - Glow_Size, 1f, 1f);
		Glow[2].transform.localScale = new global::UnityEngine.Vector3(Glow_Size, -1f, 1f);
		Glow[3].transform.localScale = new global::UnityEngine.Vector3(0f - Glow_Size, -1f, 1f);
		if (Life_Timer < 3f)
		{
			Glow_Opacity = 0.5f + (1f + global::UnityEngine.Mathf.Sin(Life_Timer * 3f)) * 0.25f;
		}
		else if (Life_Timer < 4.2f)
		{
			Glow_Opacity = global::UnityEngine.Mathf.Lerp(Glow_Opacity, 0f, global::UnityEngine.Time.deltaTime * 2.5f);
		}
		else
		{
			Glow_Opacity = global::UnityEngine.Mathf.Lerp(Glow_Opacity, 0f, global::UnityEngine.Time.deltaTime * 5f);
		}
		if (Glow.Length > 0)
		{
			for (int i = 0; i < Glow.Length; i++)
			{
				Glow[i].GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(color_Glow.r, color_Glow.g, color_Glow.b, Glow_Opacity);
			}
		}
		if (Life_Timer > 3.5f && Glow_Opacity < 0.01f)
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
		if ((double)Col_Size_X < 24.3)
		{
			Col_Size_X += global::UnityEngine.Time.deltaTime * 18.5f;
			GetComponent<global::UnityEngine.BoxCollider2D>().size = new global::UnityEngine.Vector2(Col_Size_X, 4.8f);
		}
	}

	private void OnTriggerStay2D(global::UnityEngine.Collider2D col)
	{
		if (isMonsters && Glow_Opacity > 0.4f && col.name == "Ani" && !GM.Paused && !GM.GameOver && !GM.onEvent)
		{
			GM.PoisonSmog_Timer = 0.2f;
			if (GM.Poison_DMG < 10)
			{
				GM.Poison_DMG = 10;
			}
		}
	}
}
