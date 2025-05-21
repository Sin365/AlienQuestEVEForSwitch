public class Gallery_Mosaic : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.Transform targePos_Mosaic;

	private global::UnityEngine.Camera mainCamera;

	private new global::UnityEngine.Camera camera;

	private float ratio = 1f;

	private float dist_X;

	private float dist_Y;

	private float RX;

	private float RY;

	private global::UnityEngine.Vector2 pos = new global::UnityEngine.Vector2(0.45f, 0.35f);

	private global::UnityEngine.Vector2 size = new global::UnityEngine.Vector2(0.1f, 0.3f);

	private void Start()
	{
		mainCamera = global::UnityEngine.GameObject.Find("Main Camera").GetComponent<global::UnityEngine.Camera>();
		camera = GetComponent<global::UnityEngine.Camera>();
		camera.rect = new global::UnityEngine.Rect(0.5f, 1.5f, 0.1f, 0.3f);
		if (global::UnityEngine.PlayerPrefs.GetInt("UncensoredPatch") == 1)
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void Update()
	{
		ratio = mainCamera.orthographicSize / 7f;
		if (global::UnityEngine.GameObject.Find("pos_Mosaic") != null)
		{
			dist_Y = global::UnityEngine.GameObject.Find("pos_Mosaic").transform.position.y - mainCamera.transform.position.y;
			dist_X = global::UnityEngine.GameObject.Find("pos_Mosaic").transform.position.x - mainCamera.transform.position.x;
			RY = 0.5f - size.y * 0.5f / ratio + dist_Y / mainCamera.orthographicSize / 2f;
			RX = 0.5f - size.x * 0.5f / ratio + dist_X * 0.5625f / mainCamera.orthographicSize / 2f;
			camera.rect = new global::UnityEngine.Rect(RX, RY, size.x / ratio, size.y / ratio);
		}
		else
		{
			camera.rect = new global::UnityEngine.Rect(0.5f, 1.5f, 0.1f, 0.3f);
		}
	}

	public void Set_Mosaic(int num)
	{
		switch (num)
		{
		case 1:
			pos = new global::UnityEngine.Vector2(0.25f, 0.2f);
			size = new global::UnityEngine.Vector2(0.1f, 0.2f);
			break;
		case 2:
			pos = new global::UnityEngine.Vector2(0.52f, 0.02f);
			size = new global::UnityEngine.Vector2(0.1f, 0.2f);
			break;
		case 3:
			pos = new global::UnityEngine.Vector2(0.6f, 0.63f);
			size = new global::UnityEngine.Vector2(0.08f, 0.2f);
			break;
		case 4:
			pos = new global::UnityEngine.Vector2(0.63f, 0.13f);
			size = new global::UnityEngine.Vector2(0.08f, 0.2f);
			break;
		case 5:
			pos = new global::UnityEngine.Vector2(0.45f, 0.67f);
			size = new global::UnityEngine.Vector2(0.08f, 0.16f);
			break;
		default:
			pos = new global::UnityEngine.Vector2(0.45f, 0.35f);
			size = new global::UnityEngine.Vector2(0.1f, 0.3f);
			break;
		}
	}
}
