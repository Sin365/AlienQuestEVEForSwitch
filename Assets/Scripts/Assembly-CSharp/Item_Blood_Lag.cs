public class Item_Blood_Lag : global::UnityEngine.MonoBehaviour
{
	private float Opacity = 1f;

	private float Size = 1f;

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
			Opacity -= global::UnityEngine.Time.deltaTime * 6f;
			Size -= global::UnityEngine.Time.deltaTime * 5f;
			if (Opacity > 0f)
			{
				SR.color = new global::UnityEngine.Color(1f, 1f, 1f, Opacity);
				base.transform.localScale = new global::UnityEngine.Vector3(Size, Size, 1f);
			}
			else
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}
}
