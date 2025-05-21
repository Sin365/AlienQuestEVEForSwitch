public class H_CumDown : global::UnityEngine.MonoBehaviour
{
	private enum AniState
	{
		DownDirect = 0,
		DownDrool = 1,
		Pee = 2,
		Upper = 3,
		UpperSlow = 4
	}

	private H_CumDown.AniState State;

	public float SpreadPower = 1f;

	public global::UnityEngine.GameObject CumShot_Geo;

	public global::UnityEngine.GameObject[] Cum_Ctrl;

	public global::UnityEngine.Transform pos_Target;

	private bool onHold = true;

	private float Hold_Timer = 0.5f;

	private float Life_Timer;

	private float Opacity_Timer;

	private float Dist = 0.38f;

	private float Size = 1f;

	private float Opacity = 0.7f;

	private global::UnityEngine.Vector3 pos_Start;

	private global::UnityEngine.Color color_OFF = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	private void Start()
	{
		CumShot_Geo.GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material.color = new global::UnityEngine.Color(1f, 1f, 1f, Opacity);
	}

	public void Set_SortingOrder(int num)
	{
		CumShot_Geo.GetComponent<Puppet2D_SortingLayer>().renderer.sortingOrder = num;
	}

	public void Set_DownDirect(float size)
	{
		State = H_CumDown.AniState.DownDirect;
		Size = size;
		Hold_Timer = 0.5f * Size;
		if (size != 1f)
		{
			Set_Dist_Short(size);
		}
	}

	public void Set_DownDrool(float size)
	{
		State = H_CumDown.AniState.DownDrool;
		Size = size;
		Hold_Timer = 1.2f * Size;
		if (size == 1f)
		{
			Set_Dist_Random();
		}
		else
		{
			Set_Dist_Short(size);
		}
	}

	public void Set_Pee()
	{
		State = H_CumDown.AniState.Pee;
		Opacity = 1f;
		CumShot_Geo.GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material.color = new global::UnityEngine.Color(1f, 1f, 0.5f, Opacity);
		Spread();
	}

	private void Set_Dist_Random()
	{
		for (int i = 1; i < 6; i++)
		{
			Cum_Ctrl[i].GetComponent<global::UnityEngine.DistanceJoint2D>().distance = global::UnityEngine.Random.Range(0.1f, 0.35f);
		}
	}

	private void Set_Dist_Short(float size)
	{
		for (int i = 1; i < 6; i++)
		{
			Cum_Ctrl[i].GetComponent<global::UnityEngine.DistanceJoint2D>().distance = 0.38f * size;
		}
	}

	private void Update()
	{
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (onHold && pos_Target != null)
		{
			Cum_Ctrl[6].transform.position = pos_Target.position;
		}
		if (State == H_CumDown.AniState.DownDirect)
		{
			if (onHold && Life_Timer > Hold_Timer)
			{
				onHold = false;
			}
			Cum_Ctrl[6].rigidbody2D.mass = global::UnityEngine.Mathf.Lerp(Cum_Ctrl[6].rigidbody2D.mass, 0.9f, global::UnityEngine.Time.deltaTime * 10f);
			Cum_Ctrl[6].rigidbody2D.gravityScale = global::UnityEngine.Mathf.Lerp(Cum_Ctrl[6].rigidbody2D.gravityScale, 1f, global::UnityEngine.Time.deltaTime * 0.1f);
		}
		else if (State == H_CumDown.AniState.DownDrool)
		{
			if (onHold && (Life_Timer > Hold_Timer || pos_Target == null))
			{
				onHold = false;
			}
			if (!onHold)
			{
				Cum_Ctrl[6].rigidbody2D.mass = global::UnityEngine.Mathf.Lerp(Cum_Ctrl[6].rigidbody2D.mass, 0.9f, global::UnityEngine.Time.deltaTime * 50f);
				Cum_Ctrl[6].rigidbody2D.gravityScale = global::UnityEngine.Mathf.Lerp(Cum_Ctrl[6].rigidbody2D.gravityScale, 1f, global::UnityEngine.Time.deltaTime * 2f);
			}
		}
		else if (State == H_CumDown.AniState.Pee)
		{
			if (onHold && Life_Timer > 0.1f)
			{
				onHold = false;
			}
			Cum_Ctrl[6].rigidbody2D.mass = global::UnityEngine.Mathf.Lerp(Cum_Ctrl[6].rigidbody2D.mass, 0.9f, global::UnityEngine.Time.deltaTime * 10f);
			Cum_Ctrl[6].rigidbody2D.gravityScale = global::UnityEngine.Mathf.Lerp(Cum_Ctrl[6].rigidbody2D.gravityScale, 1f, global::UnityEngine.Time.deltaTime * 0.1f);
		}
		else
		{
			onHold = false;
		}
		if (State == H_CumDown.AniState.Pee)
		{
			Opacity -= global::UnityEngine.Time.deltaTime * 1.2f;
			CumShot_Geo.GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material.color = new global::UnityEngine.Color(1f, 1f, 1f, Opacity);
		}
		else if (!onHold)
		{
			Opacity_Timer += global::UnityEngine.Time.deltaTime;
			if (Opacity_Timer > 0.8f * Size)
			{
				Opacity -= global::UnityEngine.Time.deltaTime * 1.2f;
				CumShot_Geo.GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material.color = new global::UnityEngine.Color(1f, 1f, 1f, Opacity);
			}
		}
		if (Opacity < 0.03f || Life_Timer > 3.5f)
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void Spread()
	{
		global::UnityEngine.Vector2 relativeForce = new global::UnityEngine.Vector2(global::UnityEngine.Random.Range(100, 400), global::UnityEngine.Random.Range(-100, 100));
		for (int i = 0; i < 6; i++)
		{
			Cum_Ctrl[i].rigidbody2D.AddRelativeForce(relativeForce);
		}
	}
}
