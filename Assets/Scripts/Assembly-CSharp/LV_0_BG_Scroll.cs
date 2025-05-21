public class LV_0_BG_Scroll : global::UnityEngine.MonoBehaviour
{
	public float Vertical_Ratio;

	public float Horizontal_Ratio;

	private global::UnityEngine.Vector3 pos_Orig;

	private void Start()
	{
		pos_Orig = base.transform.position;
	}

	private void FixedUpdate()
	{
		float num = global::UnityEngine.GameObject.Find("Main Camera").transform.position.x - pos_Orig.x;
		float num2 = global::UnityEngine.GameObject.Find("Main Camera").transform.position.y - pos_Orig.y;
		base.transform.position = new global::UnityEngine.Vector3(pos_Orig.x + num * Horizontal_Ratio, pos_Orig.y + num2 * Vertical_Ratio, 0f);
	}
}
