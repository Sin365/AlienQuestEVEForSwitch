public class H_MosaicMove : global::UnityEngine.MonoBehaviour
{
	private bool onEnabled;

	public global::UnityEngine.Transform posTarget;

	private global::UnityEngine.Vector2 dist;

	public bool H_Mon_7_Flip;

	private void Start()
	{
		if (AxiPlayerPrefs.GetInt("UncensoredPatch") != 1)
		{
			onEnabled = true;
			GetComponent<global::UnityEngine.SpriteRenderer>().enabled = true;
		}
		else
		{
			onEnabled = false;
			GetComponent<global::UnityEngine.SpriteRenderer>().enabled = false;
		}
		if (!H_Mon_7_Flip)
		{
			dist = new global::UnityEngine.Vector2(base.transform.position.x - posTarget.position.x, base.transform.position.y - posTarget.position.y);
		}
		else
		{
			dist = new global::UnityEngine.Vector2(posTarget.position.x - base.transform.position.x, base.transform.position.y - posTarget.position.y);
		}
	}

	private void Update()
	{
		if (onEnabled)
		{
			if (base.transform.parent.transform.localScale.z > 0f)
			{
				base.transform.position = new global::UnityEngine.Vector3(posTarget.position.x + dist.x, posTarget.position.y + dist.y, 0f);
			}
			else
			{
				base.transform.position = new global::UnityEngine.Vector3(posTarget.position.x - dist.x, posTarget.position.y + dist.y, 0f);
			}
		}
	}
}
