public class Water_Dust_2 : global::UnityEngine.MonoBehaviour
{
	private float Life_Timer;

	private global::UnityEngine.Color color_Off = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	private global::UnityEngine.SpriteRenderer SR;

    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		SR = GetComponent<global::UnityEngine.SpriteRenderer>();
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			Life_Timer += global::UnityEngine.Time.deltaTime;
			SR.color = global::UnityEngine.Color.Lerp(SR.color, color_Off, global::UnityEngine.Time.deltaTime * 5f);
			base.transform.localScale = new global::UnityEngine.Vector3(base.transform.localScale.x + global::UnityEngine.Time.deltaTime * 20f, base.transform.localScale.y - global::UnityEngine.Time.deltaTime, 1f);
			if (Life_Timer > 1f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}
}
