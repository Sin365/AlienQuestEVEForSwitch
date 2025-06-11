public class Water_Dust : global::UnityEngine.MonoBehaviour
{
	private float Life_Timer;

	private global::UnityEngine.Color color_Off = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	private float Size = 3f;

	private global::UnityEngine.SpriteRenderer SR;

    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		SR = GetComponent<global::UnityEngine.SpriteRenderer>();
		Size = global::UnityEngine.Random.Range(0.2f, 0.25f);
		base.transform.localScale = new global::UnityEngine.Vector3(Size, Size, 1f);
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			Life_Timer += global::UnityEngine.Time.deltaTime;
			if (Life_Timer > 0.6f)
			{
				SR.color = global::UnityEngine.Color.Lerp(SR.color, color_Off, global::UnityEngine.Time.deltaTime * 5f);
			}
			Size = global::UnityEngine.Mathf.Lerp(Size, 0.1f, global::UnityEngine.Time.deltaTime * 3f);
			base.transform.localScale = new global::UnityEngine.Vector3(Size, Size, 1f);
			if (Life_Timer > 1f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}
}
