public class Save_ActiveGlow : global::UnityEngine.MonoBehaviour
{
	private global::UnityEngine.SpriteRenderer Spr;

	private global::UnityEngine.Vector3 pos_Start;

    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		Spr = GetComponent<global::UnityEngine.SpriteRenderer>();
		pos_Start = base.transform.position;
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			base.transform.Translate(global::UnityEngine.Vector3.up * global::UnityEngine.Time.deltaTime * 20f);
			if (base.transform.position.y > pos_Start.y + 1f)
			{
				Spr.color = global::UnityEngine.Color.Lerp(Spr.color, new global::UnityEngine.Color(1f, 1f, 1f, 0f), global::UnityEngine.Time.deltaTime * 7f);
			}
			if (base.transform.position.y > pos_Start.y + 12f || Spr.color.a < 0.005f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}
}
