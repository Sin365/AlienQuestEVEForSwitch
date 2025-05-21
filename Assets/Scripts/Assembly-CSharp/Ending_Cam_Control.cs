public class Ending_Cam_Control : global::UnityEngine.MonoBehaviour
{
	public int State;

	public float target_Size = 7f;

	public float Size_Speed = 1f;

	public global::UnityEngine.Vector3 target_Pos;

	public float Pos_Speed = 1f;

	public bool onShake;

	private float shakeTimer = -100f;

	private float shakeDeg = 0.01f;

	private float shakeDelay;

	private global::UnityEngine.Vector2 shake_R = new global::UnityEngine.Vector2(0f, 0f);

	private void Start()
	{
		base.GetComponent<UnityEngine.Camera>().orthographicSize = 5f;
		target_Pos = base.transform.position;
	}

	public void Set_Shake()
	{
		onShake = true;
		shakeTimer = 12f;
		shakeDeg = 0.004f;
	}

	private void Update()
	{
		base.GetComponent<UnityEngine.Camera>().orthographicSize = global::UnityEngine.Mathf.Lerp(base.GetComponent<UnityEngine.Camera>().orthographicSize, target_Size, global::UnityEngine.Time.deltaTime * Size_Speed);
		if (onShake)
		{
			shakeTimer -= global::UnityEngine.Time.deltaTime;
			if (shakeTimer <= 0f)
			{
				onShake = false;
			}
			else if (shakeTimer < 2f)
			{
				shakeDeg = global::UnityEngine.Mathf.Lerp(shakeDeg, 0f, global::UnityEngine.Time.deltaTime * 3f);
			}
			shake_R.x = (float)global::UnityEngine.Random.Range(-50, 50) * shakeDeg;
			shake_R.y = (float)global::UnityEngine.Random.Range(-50, 50) * shakeDeg;
			target_Pos = global::UnityEngine.Vector3.Lerp(base.transform.position, target_Pos, global::UnityEngine.Time.deltaTime * 5f);
			base.transform.position = new global::UnityEngine.Vector3(target_Pos.x + shake_R.x, target_Pos.y + shake_R.y, -10f);
		}
		else
		{
			base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, target_Pos, global::UnityEngine.Time.deltaTime * Pos_Speed);
		}
	}
}
