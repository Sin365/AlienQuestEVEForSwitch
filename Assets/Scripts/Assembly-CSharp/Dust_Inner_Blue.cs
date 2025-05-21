public class Dust_Inner_Blue : global::UnityEngine.MonoBehaviour
{
	private float Opacity = 0.1f;

	private float Size = 1.8f;

	private global::UnityEngine.SpriteRenderer SR;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		SR = GetComponent<global::UnityEngine.SpriteRenderer>();
		SR.color = new global::UnityEngine.Color(1f, 1f, 1f, 1f);
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			Opacity = global::UnityEngine.Mathf.Lerp(Opacity, 1f, global::UnityEngine.Time.deltaTime * 3f);
			SR.color = new global::UnityEngine.Color(1f, 1f, 1f, Opacity);
			Size = global::UnityEngine.Mathf.Lerp(Size, 0f, global::UnityEngine.Time.deltaTime * 15f);
			base.transform.localScale = new global::UnityEngine.Vector3(Size, 1f, 1f);
			if (Size < 0.1f)
			{
				Destroy_Self();
			}
		}
	}

	private void Destroy_Self()
	{
		global::UnityEngine.Object.Destroy(base.gameObject);
	}
}
