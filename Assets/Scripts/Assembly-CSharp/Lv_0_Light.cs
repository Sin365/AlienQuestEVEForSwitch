public class Lv_0_Light : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.SpriteRenderer Glow_1;

	public global::UnityEngine.SpriteRenderer Glow_2;

	private float Life_Timer;

	GameManager GM => GameManager.instance;

	private void Start()
	{
		//if (global::UnityEngine.GameObject.Find("GameManager") != null)
		//{
		//	GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//}
		Life_Timer = global::UnityEngine.Random.Range(-1.57f, 1.57f);
	}

	private void Update()
	{
		if (GM == null)
		{
			Life_Timer += global::UnityEngine.Time.deltaTime;
			Glow_1.color = new global::UnityEngine.Color(1f, 1f, 1f, 0.4f + global::UnityEngine.Mathf.Sin(Life_Timer * 3f) * 0.2f);
			Glow_2.color = new global::UnityEngine.Color(1f, 1f, 1f, 0.7f + global::UnityEngine.Mathf.Sin(Life_Timer * 3f) * 0.2f);
		}
		else if (!GM.Paused)
		{
			Life_Timer += global::UnityEngine.Time.deltaTime;
			Glow_1.color = new global::UnityEngine.Color(1f, 1f, 1f, 0.4f + global::UnityEngine.Mathf.Sin(Life_Timer * 3f) * 0.2f);
			Glow_2.color = new global::UnityEngine.Color(1f, 1f, 1f, 0.7f + global::UnityEngine.Mathf.Sin(Life_Timer * 3f) * 0.2f);
		}
	}
}
