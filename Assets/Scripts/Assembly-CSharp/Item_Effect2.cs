public class Item_Effect2 : global::UnityEngine.MonoBehaviour
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
		if ((double)Glow_Timer > 0.05)
		{
			Glow_Timer = 0f;
			Opacity = (float)global::UnityEngine.Random.Range(0, 600) * 0.001f;
			Glow.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, Opacity);
		}
	}
}
