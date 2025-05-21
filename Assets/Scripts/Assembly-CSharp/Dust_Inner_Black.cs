public class Dust_Inner_Black : global::UnityEngine.MonoBehaviour
{
	private float Opacity = 0.25f;

	private float Size = 1.5f;

	private global::UnityEngine.SpriteRenderer SR;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		SR = GetComponent<global::UnityEngine.SpriteRenderer>();
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			Opacity = global::UnityEngine.Mathf.Lerp(Opacity, 0.1f, global::UnityEngine.Time.deltaTime * 3f);
			SR.color = new global::UnityEngine.Color(0f, 0f, 0f, Opacity);
			Size -= global::UnityEngine.Time.deltaTime * 5f;
			base.transform.localScale = new global::UnityEngine.Vector3(Size, Size - 0.5f, 1f);
			if (Size < 0.22f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}
}
