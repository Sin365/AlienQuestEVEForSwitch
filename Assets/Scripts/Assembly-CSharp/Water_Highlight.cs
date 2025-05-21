public class Water_Highlight : global::UnityEngine.MonoBehaviour
{
	public int FlowRight = -1;

	private float Life_Timer;

	private global::UnityEngine.Color color_ON = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_OFF = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	private float Size = 3f;

	private global::UnityEngine.SpriteRenderer SR;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		SR = GetComponent<global::UnityEngine.SpriteRenderer>();
		SR.color = color_OFF;
		Size = global::UnityEngine.Random.Range(2f, 3f);
		base.transform.localScale = new global::UnityEngine.Vector3(Size, 1f, 1f);
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			Life_Timer += global::UnityEngine.Time.deltaTime;
			if (Life_Timer < 0.3f)
			{
				SR.color = global::UnityEngine.Color.Lerp(SR.color, color_ON, global::UnityEngine.Time.deltaTime * 5f);
				Size = global::UnityEngine.Mathf.Lerp(Size, 10f, global::UnityEngine.Time.deltaTime * 1f);
				base.transform.localScale = new global::UnityEngine.Vector3(Size, 1f, 1f);
			}
			else if (Life_Timer > 0.5f)
			{
				SR.color = global::UnityEngine.Color.Lerp(SR.color, color_OFF, global::UnityEngine.Time.deltaTime * 5f);
				Size = global::UnityEngine.Mathf.Lerp(Size, 0f, global::UnityEngine.Time.deltaTime * 1f);
				base.transform.localScale = new global::UnityEngine.Vector3(Size, 1f, 1f);
			}
			base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * 0.3f * FlowRight);
			if (Life_Timer > 1.2f || SR.color.a < 0.02f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}
}
