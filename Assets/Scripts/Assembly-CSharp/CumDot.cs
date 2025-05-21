public class CumDot : global::UnityEngine.MonoBehaviour
{
	private float Opacity = 1f;

	private void Start()
	{
		base.rigidbody2D.AddRelativeForce(new global::UnityEngine.Vector2(global::UnityEngine.Random.Range(500, 1000), 0f));
	}

	private void Update()
	{
		if (Opacity > 0f)
		{
			Opacity -= global::UnityEngine.Time.deltaTime * 0.3f;
		}
		GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, Opacity);
		if (base.transform.position.y < -12f || Opacity <= 0f)
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
