public class CumShot_1 : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject CumShot_Geo;

	public global::UnityEngine.GameObject Ctrl_1;

	public global::UnityEngine.GameObject Ctrl_2;

	public global::UnityEngine.GameObject Ctrl_3;

	public global::UnityEngine.GameObject Ctrl_4;

	public global::UnityEngine.GameObject Ctrl_5;

	public global::UnityEngine.GameObject Ctrl_6;

	public global::UnityEngine.GameObject Ctrl_7;

	public global::UnityEngine.Transform pos_Target;

	private float Life_Timer;

	private float Opacity = 1f;

	private float Scale;

	private float Hold_Timer;

	private void Start()
	{
	}

	private void Update()
	{
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (Life_Timer > 0.7f)
		{
			Opacity = global::UnityEngine.Mathf.Lerp(Opacity, 0f, global::UnityEngine.Time.deltaTime * 3f);
			CumShot_Geo.GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material.color = new global::UnityEngine.Color(1f, 1f, 1f, Opacity);
			if (Life_Timer > 4f || Opacity < 0.02f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
			if (Scale < 0.5f)
			{
				Set_GravityScale();
			}
		}
		if (Ctrl_1.transform.position.y < -12f && Ctrl_7.transform.position.y < -12f)
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
		if (pos_Target != null)
		{
			Hold_Timer -= global::UnityEngine.Time.deltaTime;
			Ctrl_7.transform.position = pos_Target.position;
			if (Hold_Timer <= 0f)
			{
				pos_Target = null;
				Ctrl_4.GetComponent<global::UnityEngine.Rigidbody2D>().gravityScale = 2.4f;
				Ctrl_5.GetComponent<global::UnityEngine.Rigidbody2D>().gravityScale = 2.2f;
				Ctrl_6.GetComponent<global::UnityEngine.Rigidbody2D>().gravityScale = 2f;
				Ctrl_7.GetComponent<global::UnityEngine.Rigidbody2D>().gravityScale = 1.6f;
			}
		}
	}

	public void Set_SortingOrder(int num, global::UnityEngine.Transform pos, float holdTimer)
	{
		CumShot_Geo.GetComponent<Puppet2D_SortingLayer>().renderer.sortingOrder = num;
		if (pos != null)
		{
			pos_Target = pos;
			Hold_Timer = holdTimer;
			Ctrl_4.GetComponent<global::UnityEngine.Rigidbody2D>().gravityScale = 2f;
			Ctrl_5.GetComponent<global::UnityEngine.Rigidbody2D>().gravityScale = 1.4f;
			Ctrl_6.GetComponent<global::UnityEngine.Rigidbody2D>().gravityScale = 0.7f;
			Ctrl_7.GetComponent<global::UnityEngine.Rigidbody2D>().gravityScale = 0f;
			Ctrl_6.GetComponent<global::UnityEngine.DistanceJoint2D>().distance = 1.5f;
			Ctrl_7.GetComponent<global::UnityEngine.DistanceJoint2D>().distance = 1f;
		}
	}

	private void Slow_Down()
	{
		Ctrl_5.GetComponent<global::UnityEngine.Rigidbody2D>().gravityScale = 2f;
		Ctrl_6.GetComponent<global::UnityEngine.Rigidbody2D>().gravityScale = 1.5f;
		Ctrl_7.GetComponent<global::UnityEngine.Rigidbody2D>().gravityScale = 0.7f;
	}

	private void Start_Long()
	{
		CumShot_Geo.GetComponent<Puppet2D_SortingLayer>().renderer.sortingOrder = 200;
		Ctrl_1.rigidbody2D.AddRelativeForce(new global::UnityEngine.Vector2(global::UnityEngine.Random.Range(500, 1000), 0f));
		Ctrl_2.rigidbody2D.AddRelativeForce(new global::UnityEngine.Vector2(global::UnityEngine.Random.Range(500, 1000), 0f));
		Ctrl_3.rigidbody2D.AddRelativeForce(new global::UnityEngine.Vector2(global::UnityEngine.Random.Range(500, 1000), 0f));
		Ctrl_4.rigidbody2D.AddRelativeForce(new global::UnityEngine.Vector2(global::UnityEngine.Random.Range(500, 1000), 0f));
		Ctrl_5.rigidbody2D.AddRelativeForce(new global::UnityEngine.Vector2(global::UnityEngine.Random.Range(400, 900), 0f));
		Ctrl_6.rigidbody2D.AddRelativeForce(new global::UnityEngine.Vector2(global::UnityEngine.Random.Range(300, 700), 0f));
		Ctrl_7.rigidbody2D.AddRelativeForce(new global::UnityEngine.Vector2(global::UnityEngine.Random.Range(200, 500), 0f));
	}

	private void Set_GravityScale()
	{
		Scale = 0.5f;
		Ctrl_1.GetComponent<global::UnityEngine.Rigidbody2D>().gravityScale = Scale;
		Ctrl_2.GetComponent<global::UnityEngine.Rigidbody2D>().gravityScale = Scale;
		Ctrl_3.GetComponent<global::UnityEngine.Rigidbody2D>().gravityScale = Scale;
		Ctrl_4.GetComponent<global::UnityEngine.Rigidbody2D>().gravityScale = Scale;
		Ctrl_5.GetComponent<global::UnityEngine.Rigidbody2D>().gravityScale = Scale;
		Ctrl_6.GetComponent<global::UnityEngine.Rigidbody2D>().gravityScale = Scale;
		Ctrl_7.GetComponent<global::UnityEngine.Rigidbody2D>().gravityScale = Scale;
	}
}
