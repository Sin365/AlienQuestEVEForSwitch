public class CumShot_N : global::UnityEngine.MonoBehaviour
{
	private enum AniState
	{
		DownDirect = 0,
		DownDrool = 1,
		Pee = 2
	}

	private CumShot_N.AniState State;

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

	private float Opacity = 1f;

	private global::UnityEngine.Vector3 pos_Start;

	private global::UnityEngine.Color color_OFF = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

	private void Start()
	{
		CumShot_Geo.GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material.color = new global::UnityEngine.Color(1f, 1f, 1f, Opacity);
		for (int i = 0; i < 6; i++)
		{
			Cum_Ctrl[i].transform.localPosition = new global::UnityEngine.Vector3(Cum_Ctrl[i].transform.localPosition.x + 0.65f * (float)(6 - i), 0f, 0f);
		}
	}

	public void Set_SortingOrder(int num)
	{
		CumShot_Geo.GetComponent<Puppet2D_SortingLayer>().GetComponent<UnityEngine.Renderer>().sortingOrder = num;
	}

	public void Set_DownDirect(float size)
	{
		State = CumShot_N.AniState.DownDirect;
		Size = size;
		Hold_Timer = 0.8f;
	}

	public void Set_DownDrool(float size)
	{
		State = CumShot_N.AniState.DownDrool;
		Size = size;
		Hold_Timer = 1.6f;
	}

	private void Set_Dist_Random()
	{
		for (int i = 1; i < 6; i++)
		{
			Cum_Ctrl[i].GetComponent<global::UnityEngine.DistanceJoint2D>().distance = global::UnityEngine.Random.Range(0.1f, 0.35f);
		}
	}

	private void Update()
	{
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (onHold && pos_Target != null)
		{
			Cum_Ctrl[6].transform.position = pos_Target.position;
		}
		if (State == CumShot_N.AniState.DownDirect)
		{
			if (onHold && Life_Timer > Hold_Timer)
			{
				onHold = false;
			}
			Cum_Ctrl[6].GetComponent<UnityEngine.Rigidbody2D>().mass = global::UnityEngine.Mathf.Lerp(Cum_Ctrl[6].GetComponent<UnityEngine.Rigidbody2D>().mass, 0.9f, global::UnityEngine.Time.deltaTime * 10f);
			Cum_Ctrl[6].GetComponent<UnityEngine.Rigidbody2D>().gravityScale = global::UnityEngine.Mathf.Lerp(Cum_Ctrl[6].GetComponent<UnityEngine.Rigidbody2D>().gravityScale, 1f, global::UnityEngine.Time.deltaTime * 0.1f);
		}
		else if (State == CumShot_N.AniState.DownDrool)
		{
			if (onHold && Life_Timer > Hold_Timer)
			{
				onHold = false;
			}
			if (!onHold)
			{
				Cum_Ctrl[6].GetComponent<UnityEngine.Rigidbody2D>().mass = global::UnityEngine.Mathf.Lerp(Cum_Ctrl[6].GetComponent<UnityEngine.Rigidbody2D>().mass, 0.9f, global::UnityEngine.Time.deltaTime * 50f);
				Cum_Ctrl[6].GetComponent<UnityEngine.Rigidbody2D>().gravityScale = global::UnityEngine.Mathf.Lerp(Cum_Ctrl[6].GetComponent<UnityEngine.Rigidbody2D>().gravityScale, 1f, global::UnityEngine.Time.deltaTime * 2f);
			}
		}
		else
		{
			onHold = false;
		}
		if (!onHold)
		{
			Opacity_Timer += global::UnityEngine.Time.deltaTime;
			if (Opacity_Timer > 0.3f)
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
			Cum_Ctrl[i].GetComponent<UnityEngine.Rigidbody2D>().AddRelativeForce(relativeForce);
		}
	}
}
