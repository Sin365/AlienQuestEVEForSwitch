public class Magic_Fire_5 : global::UnityEngine.MonoBehaviour
{
	private bool onActive = true;

	private float Life_Timer;

	private global::UnityEngine.Vector3 target;

	private float Black_Timer;

	private float Blue_Timer;

	private float Size = 1f;

	private float Size_Glow = 1f;

	public global::UnityEngine.GameObject Glow;

	public global::UnityEngine.GameObject BlackBar;

	public global::UnityEngine.GameObject InnerBlue;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		Size = base.transform.localScale.y;
		target = new global::UnityEngine.Vector3(base.transform.position.x + 12f * base.transform.localScale.x, base.transform.position.y, 0f);
		Make_Black();
		global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().Set_Shake();
	}

	private void Make_Black()
	{
		int num = global::UnityEngine.Random.Range(0, 24);
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(BlackBar, base.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 15f * (float)num + (float)global::UnityEngine.Random.Range(-50, 50) * 0.1f)) as global::UnityEngine.GameObject;
		gameObject.transform.parent = base.transform;
		num = ((num == global::UnityEngine.Random.Range(0, 24)) ? num++ : num);
		global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(BlackBar, base.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 15f * (float)num + (float)global::UnityEngine.Random.Range(-50, 50) * 0.1f)) as global::UnityEngine.GameObject;
		gameObject2.transform.parent = base.transform;
		num = ((num == global::UnityEngine.Random.Range(0, 24)) ? num++ : num);
		global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(BlackBar, base.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 15f * (float)num + (float)global::UnityEngine.Random.Range(-50, 50) * 0.1f)) as global::UnityEngine.GameObject;
		gameObject3.transform.parent = base.transform;
		num = ((num == global::UnityEngine.Random.Range(0, 24)) ? num++ : num);
		global::UnityEngine.GameObject gameObject4 = global::UnityEngine.Object.Instantiate(BlackBar, base.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 15f * (float)num + (float)global::UnityEngine.Random.Range(-50, 50) * 0.1f)) as global::UnityEngine.GameObject;
		gameObject4.transform.parent = base.transform;
		num = ((num == global::UnityEngine.Random.Range(0, 24)) ? num++ : num);
		global::UnityEngine.GameObject gameObject5 = global::UnityEngine.Object.Instantiate(BlackBar, base.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 15f * (float)num + (float)global::UnityEngine.Random.Range(-50, 50) * 0.1f)) as global::UnityEngine.GameObject;
		gameObject5.transform.parent = base.transform;
	}

	private void Make_Blue()
	{
		int num = global::UnityEngine.Random.Range(0, 36);
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(InnerBlue, base.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 10f * (float)num + (float)global::UnityEngine.Random.Range(-50, 50) * 0.1f)) as global::UnityEngine.GameObject;
		gameObject.transform.parent = base.transform;
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
		base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, target, global::UnityEngine.Time.deltaTime * 3f);
		if (Life_Timer > 3.45f)
		{
			Size = global::UnityEngine.Mathf.Lerp(Size, 0f, global::UnityEngine.Time.deltaTime * 20f);
			Glow.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 0.8f, 0.5f);
		}
		else
		{
			Size = 1f + (1f + global::UnityEngine.Mathf.Sin(Life_Timer * 40f)) * 0.1f;
		}
		base.transform.localScale = new global::UnityEngine.Vector3(Size, Size, 1f);
		if (onActive)
		{
			Black_Timer += global::UnityEngine.Time.deltaTime;
			if (Black_Timer > 0.01f && Life_Timer < 3.5f)
			{
				Make_Black();
				Make_Black();
				Black_Timer = 0f;
			}
			if (Life_Timer < 3.1f)
			{
				Blue_Timer += global::UnityEngine.Time.deltaTime;
				if (Blue_Timer > 0.05f && Life_Timer < 3.5f)
				{
					Make_Blue();
					Blue_Timer = 0f;
				}
			}
		}
		if (Life_Timer > 3.5f && onActive)
		{
			onActive = false;
			GetComponent<global::UnityEngine.CircleCollider2D>().enabled = false;
		}
		if (Life_Timer > 4.5f && Size < 0.02f)
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
