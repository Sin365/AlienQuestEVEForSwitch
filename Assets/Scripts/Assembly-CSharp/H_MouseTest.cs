public class H_MouseTest : global::UnityEngine.MonoBehaviour
{
	private float ratio = 1920f / (float)global::UnityEngine.Screen.width;

	private global::UnityEngine.Vector3 pos_Target;

	private void Start()
	{
		pos_Target = base.transform.position;
	}

	private void Update()
	{
		if (global::UnityEngine.Input.GetMouseButton(0))
		{
			pos_Target = global::UnityEngine.GameObject.Find("Main Camera").GetComponent<UnityEngine.Camera>().ScreenToWorldPoint(global::UnityEngine.Input.mousePosition);
		}
		pos_Target.z = 0f;
		base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, new global::UnityEngine.Vector3(pos_Target.x, pos_Target.y, 0f), global::UnityEngine.Time.deltaTime * 2f);
	}
}
