public class H_CumDot : global::UnityEngine.MonoBehaviour
{
	private float Opacity = 1f;

	private void Start()
	{
		base.GetComponent<UnityEngine.Rigidbody2D>().AddRelativeForce(new global::UnityEngine.Vector2(global::UnityEngine.Random.Range(-100, 100), global::UnityEngine.Random.Range(100, 200)));
	}

	private void Update()
	{
		if (Opacity > 0f)
		{
			Opacity -= global::UnityEngine.Time.deltaTime * 1f;
		}
		GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, Opacity);
		if (Opacity <= 0f)
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
