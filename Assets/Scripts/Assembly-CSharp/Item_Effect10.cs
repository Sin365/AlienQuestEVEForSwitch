public class Item_Effect10 : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject Glow;

	private float Glow_Timer;

	private float Opacity;

	private float Size = 1f;

	private void Start()
	{
	}

	private void Update()
	{
		Glow_Timer += global::UnityEngine.Time.deltaTime;
		if ((double)Glow_Timer > 0.05)
		{
			Size = 1f + (float)global::UnityEngine.Random.Range(-100, 100) * 0.001f;
			Glow.transform.localScale = new global::UnityEngine.Vector3(Size, Size, 1f);
			Opacity = (float)global::UnityEngine.Random.Range(0, 600) * 0.001f;
			Glow.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, Opacity);
		}
	}
}
