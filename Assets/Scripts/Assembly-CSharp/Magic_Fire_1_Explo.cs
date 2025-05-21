public class Magic_Fire_1_Explo : global::UnityEngine.MonoBehaviour
{
	private float Life_Timer;

	private float Size = 0.6f;

	private float Target_Size = 1.8f;

	private float Opacity = 1f;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		Size = (float)global::UnityEngine.Random.Range(60, 95) * 0.01f;
		Target_Size = (float)global::UnityEngine.Random.Range(120, 180) * 0.01f;
		base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Random.Range(-30, 30) * 0.01f);
		base.transform.Translate(global::UnityEngine.Vector3.up * global::UnityEngine.Random.Range(-30, 30) * 0.01f);
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			Life_Timer += global::UnityEngine.Time.deltaTime;
			Size = global::UnityEngine.Mathf.Lerp(Size, Target_Size, global::UnityEngine.Time.deltaTime * 12f);
			base.transform.localScale = new global::UnityEngine.Vector3(Size, Size, 1f);
			Opacity -= global::UnityEngine.Time.deltaTime * 4f;
			if (Opacity < 0f)
			{
				Opacity = 0f;
			}
			GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, Opacity);
			if (Opacity == 0f || Life_Timer > 1.5f)
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
