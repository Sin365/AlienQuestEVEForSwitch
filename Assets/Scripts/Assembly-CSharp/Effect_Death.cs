public class Effect_Death : global::UnityEngine.MonoBehaviour
{
	private float Life_Timer;

	private float Size = 0.5f;

	private float Opacity = 1f;

	private float Speed = 20f;

	private global::UnityEngine.SpriteRenderer SR;

	private global::UnityEngine.SpriteRenderer SR_Border;

	private global::UnityEngine.SpriteRenderer SR_Glow;

	public global::UnityEngine.GameObject Ring;

	public global::UnityEngine.GameObject Dust;

	public global::UnityEngine.GameObject Border;

	public global::UnityEngine.GameObject Glow;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		SR = GetComponent<global::UnityEngine.SpriteRenderer>();
		SR_Border = Border.GetComponent<global::UnityEngine.SpriteRenderer>();
		SR_Glow = Glow.GetComponent<global::UnityEngine.SpriteRenderer>();
		global::UnityEngine.Vector3 position = new global::UnityEngine.Vector3(base.transform.position.x, base.transform.position.y, 0f);
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Ring, position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(Ring, position, global::UnityEngine.Quaternion.Euler(0f, 0f, 90f)) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(Ring, position, global::UnityEngine.Quaternion.Euler(0f, 0f, 180f)) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject4 = global::UnityEngine.Object.Instantiate(Ring, position, global::UnityEngine.Quaternion.Euler(0f, 0f, 270f)) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject5 = global::UnityEngine.Object.Instantiate(Ring, position, global::UnityEngine.Quaternion.Euler(0f, 0f, 45f)) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject6 = global::UnityEngine.Object.Instantiate(Ring, position, global::UnityEngine.Quaternion.Euler(0f, 0f, 135f)) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject7 = global::UnityEngine.Object.Instantiate(Ring, position, global::UnityEngine.Quaternion.Euler(0f, 0f, 225f)) as global::UnityEngine.GameObject;
		global::UnityEngine.GameObject gameObject8 = global::UnityEngine.Object.Instantiate(Ring, position, global::UnityEngine.Quaternion.Euler(0f, 0f, 315f)) as global::UnityEngine.GameObject;
		for (int i = 0; i < 20; i++)
		{
			Create_Dust(new global::UnityEngine.Vector3(base.transform.position.x, base.transform.position.y, 0f), 0.5f, 18 * i);
		}
		StartCoroutine("Spread_Dust");
	}

	private void Create_Dust(global::UnityEngine.Vector3 pos, float size, float rot)
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Dust, pos, global::UnityEngine.Quaternion.Euler(0f, 0f, rot)) as global::UnityEngine.GameObject;
		gameObject.transform.localScale = new global::UnityEngine.Vector3(4f, 4f, 1f);
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			Life_Timer += global::UnityEngine.Time.deltaTime;
			Size = global::UnityEngine.Mathf.Lerp(Size, 2.8f, global::UnityEngine.Time.deltaTime * 8f);
			base.transform.localScale = new global::UnityEngine.Vector3(Size, Size, 1f);
			if (Life_Timer > 0.2f)
			{
				Opacity = global::UnityEngine.Mathf.Lerp(Opacity, 0f, global::UnityEngine.Time.deltaTime * 6f);
				SR.color = new global::UnityEngine.Color(SR.color.r, SR.color.g, SR.color.b, Opacity);
				SR_Border.color = new global::UnityEngine.Color(SR_Border.color.r, SR_Border.color.g, SR_Border.color.b, Opacity);
				SR_Glow.color = new global::UnityEngine.Color(SR_Glow.color.r, SR_Glow.color.g, SR_Glow.color.b, Opacity);
			}
			if (Life_Timer > 3f)
			{
				Destroy_Self();
			}
		}
	}

	public global::System.Collections.IEnumerator Spread_Dust()
	{
		yield return new global::UnityEngine.WaitForSeconds(0.05f);
		for (int i = 0; i < 36; i++)
		{
			Create_Dust(new global::UnityEngine.Vector3(base.transform.position.x, base.transform.position.y, 0f), 0.5f, 10 * i);
		}
		yield return new global::UnityEngine.WaitForSeconds(0.1f);
		for (int j = 0; j < 20; j++)
		{
			Create_Dust(new global::UnityEngine.Vector3(base.transform.position.x, base.transform.position.y, 0f), 0.5f, 18 * j);
		}
		yield return new global::UnityEngine.WaitForSeconds(0.05f);
		for (int k = 0; k < 36; k++)
		{
			Create_Dust(new global::UnityEngine.Vector3(base.transform.position.x, base.transform.position.y, 0f), 0.5f, 10 * k);
		}
	}

	private void Destroy_Self()
	{
		global::UnityEngine.Object.Destroy(base.gameObject);
	}
}
