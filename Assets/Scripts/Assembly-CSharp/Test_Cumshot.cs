public class Test_Cumshot : global::UnityEngine.MonoBehaviour
{
	private float Life_Timer;

	private bool isFired;

	private float Rot;

	private float Size_Tail;

	private global::UnityEngine.Vector3 Pos_Orig;

	public global::UnityEngine.GameObject Tail;

	private void Start()
	{
		Pos_Orig = base.transform.position;
		base.GetComponent<UnityEngine.Rigidbody2D>().Sleep();
		Tail.transform.localScale = new global::UnityEngine.Vector3(0f, 1f, 1f);
	}

	private void Update()
	{
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (!isFired && global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.Space))
		{
			Shot();
		}
		if (isFired)
		{
			Size_Tail = global::UnityEngine.Mathf.Lerp(Size_Tail, 1f, global::UnityEngine.Time.deltaTime * 2f);
			Tail.transform.localScale = new global::UnityEngine.Vector3(Size_Tail, 1f, 1f);
			Rot = global::UnityEngine.Mathf.Lerp(Rot, 70f, global::UnityEngine.Time.deltaTime * 8f);
			base.transform.rotation = global::UnityEngine.Quaternion.Euler(0f, 0f, Rot);
			if (base.transform.position.y < -50f)
			{
				Reset();
			}
		}
	}

	private void Shot()
	{
		base.GetComponent<UnityEngine.Rigidbody2D>().WakeUp();
		base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(new global::UnityEngine.Vector2(-600f, 0f));
		isFired = true;
	}

	private void Reset()
	{
		isFired = false;
		base.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(0f, 0f);
		base.GetComponent<UnityEngine.Rigidbody2D>().Sleep();
		Tail.transform.localScale = new global::UnityEngine.Vector3(0f, 1f, 1f);
		base.transform.rotation = global::UnityEngine.Quaternion.Euler(0f, 0f, 0f);
		base.transform.position = Pos_Orig;
		Rot = 0f;
		Size_Tail = 0f;
	}
}
