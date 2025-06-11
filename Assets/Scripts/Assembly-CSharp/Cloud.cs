public class Cloud : global::UnityEngine.MonoBehaviour
{
	public int Room_Num = 1;

	public bool isFront = true;

	private float cloud_Timer;

	private float size;

	private float opacity;

	private float posX;

	private global::UnityEngine.Vector3 PosOrig;

	private global::UnityEngine.Color ColorOrig;

    GameManager GM => GameManager.instance;

    private global::UnityEngine.SpriteRenderer SR;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		SR = GetComponent<global::UnityEngine.SpriteRenderer>();
		PosOrig = base.transform.position;
		ColorOrig = SR.color;
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			cloud_Timer += global::UnityEngine.Time.deltaTime;
			if (isFront)
			{
				size = global::UnityEngine.Mathf.Sin(cloud_Timer * 1.5f) * 0.1f;
				opacity = global::UnityEngine.Mathf.Sin(cloud_Timer * 1.5f) * 0.1f;
				base.transform.localScale = new global::UnityEngine.Vector3(4f + size, 4f - size, 1f);
				SR.color = new global::UnityEngine.Color(ColorOrig.r, ColorOrig.g, ColorOrig.b, 0.9f + opacity);
			}
			else
			{
				size = global::UnityEngine.Mathf.Sin(cloud_Timer * 0.9f) * 0.1f;
				posX = global::UnityEngine.Mathf.Sin(cloud_Timer * 0.7f) * 0.1f;
				base.transform.localScale = new global::UnityEngine.Vector3(4f + size, 4f - size, 1f);
				base.transform.position = new global::UnityEngine.Vector3(PosOrig.x, PosOrig.y + posX, 0f);
			}
		}
	}
}
