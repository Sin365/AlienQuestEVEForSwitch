public class Item_Effect5 : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject Glow;

	private float Glow_Timer;

	private float Opacity;

	private void Start()
	{
	}

	private void Update()
	{
		Glow_Timer += global::UnityEngine.Time.deltaTime;
		Opacity = (1f + global::UnityEngine.Mathf.Sin(Glow_Timer * 2f)) * 0.5f * 0.1f;
		Glow.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, Opacity);
	}
}
