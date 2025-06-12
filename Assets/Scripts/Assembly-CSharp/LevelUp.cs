using UnityEngine;

public class LevelUp : global::UnityEngine.MonoBehaviour
{
	private bool onEnabled;

	public float Life_Timer;

	public global::UnityEngine.SpriteRenderer Magic_Bar;

	public global::UnityEngine.SpriteRenderer Magic_Bar_Glow;

	public global::UnityEngine.SpriteRenderer Glow_Circle;

	GameManager GM => GameManager.instance;

    Player_Control PC => GameManager.instance.PC;
    GameObject Player => GameManager.instance.gobj_Player;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//Player = global::UnityEngine.GameObject.Find("Player");
		Magic_Bar.transform.localScale = new global::UnityEngine.Vector3(0f, 0f, 1f);
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (Life_Timer > 0f)
		{
			if (!onEnabled)
			{
				onEnabled = true;
				Magic_Bar.transform.localScale = new global::UnityEngine.Vector3(12f, 1f, 1f);
			}
			Magic_Bar.transform.localScale = global::UnityEngine.Vector3.Lerp(Magic_Bar.transform.localScale, new global::UnityEngine.Vector3(0f, 25f, 1f), global::UnityEngine.Time.deltaTime * 10f);
			Glow_Circle.transform.localScale = global::UnityEngine.Vector3.Lerp(Glow_Circle.transform.localScale, new global::UnityEngine.Vector3(2f, 3f, 1f), global::UnityEngine.Time.deltaTime * 5f);
			Glow_Circle.color = global::UnityEngine.Color.Lerp(Glow_Circle.color, new global::UnityEngine.Color(Glow_Circle.color.r, Glow_Circle.color.g, Glow_Circle.color.b, 0f), global::UnityEngine.Time.deltaTime * 5f);
			if (Magic_Bar.transform.localScale.x < 2f)
			{
				Magic_Bar.color = global::UnityEngine.Color.Lerp(Magic_Bar.color, new global::UnityEngine.Color(Magic_Bar.color.r, Magic_Bar.color.g, Magic_Bar.color.b, 0f), global::UnityEngine.Time.deltaTime * 5f);
				Magic_Bar_Glow.color = global::UnityEngine.Color.Lerp(Magic_Bar_Glow.color, new global::UnityEngine.Color(Magic_Bar_Glow.color.r, Magic_Bar_Glow.color.g, Magic_Bar_Glow.color.b, 0f), global::UnityEngine.Time.deltaTime * 5f);
			}
			if (Magic_Bar.color.a < 0.01f || Magic_Bar.transform.localScale.x < 0.01f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}
}
