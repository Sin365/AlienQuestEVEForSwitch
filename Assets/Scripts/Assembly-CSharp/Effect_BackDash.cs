public class Effect_BackDash : global::UnityEngine.MonoBehaviour
{
	private int Room_Num;

	private float Opacity = 1f;

	private float R = 1f;

	private float G = 1f;

	private float B = 1f;

	private float player_Scale;

	private global::UnityEngine.SpriteRenderer SR;

	private GameManager GM;

	private void Start()
	{
		Set_Start();
		if (GM.Weapon_Num == 5 && Opacity < 0.4f)
		{
			player_Scale = global::UnityEngine.GameObject.Find("Player").transform.localScale.x;
		}
	}

	public void Set_Opacity(float opacity)
	{
		Set_Start();
		Opacity = opacity;
		SR.color = new global::UnityEngine.Color(R, G, B, Opacity);
	}

	private void Set_Color_5()
	{
		Set_Start();
		R = 1f;
		G = 0.2f;
		B = 0f;
		SR.color = new global::UnityEngine.Color(R, G, B, Opacity);
	}

	private void Set_Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		SR = GetComponent<global::UnityEngine.SpriteRenderer>();
		R = SR.color.r;
		G = SR.color.g;
		B = SR.color.b;
		Opacity = SR.color.a;
		Room_Num = GM.Room_Num;
	}

	private void Update()
	{
		if (Room_Num != GM.Room_Num)
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
		if (!GM.Paused && !GM.onGatePass)
		{
			Opacity -= global::UnityEngine.Time.deltaTime * 2f;
			if (player_Scale != 0f && player_Scale != global::UnityEngine.GameObject.Find("Player").transform.localScale.x)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			if (Opacity > 0f)
			{
				SR.color = new global::UnityEngine.Color(R, G, B, Opacity);
			}
			else
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}
}
