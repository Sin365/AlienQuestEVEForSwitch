public class LV_4_Cam : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.Transform Orig_Left;

	public global::UnityEngine.Transform Bottom_Left;

	public global::UnityEngine.Transform Hidden_Left;

	public global::UnityEngine.SpriteRenderer SR_Tile;

	public float Orig_Speed = 5f;

	public float Target_Speed = 5f;

	private Camera_Control CamCon;

	private global::UnityEngine.GameObject Player;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		Player = global::UnityEngine.GameObject.Find("Player");
		CamCon = global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>();
		if (Player.transform.position.y > base.transform.position.y)
		{
			CamCon.Cam_Left = Orig_Left.position.x;
		}
		else if (Player.transform.position.x < base.transform.position.x)
		{
			CamCon.Cam_Left = Hidden_Left.position.x;
		}
		else
		{
			CamCon.Cam_Left = Bottom_Left.position.x;
		}
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		if (Player.transform.position.y > base.transform.position.y)
		{
			CamCon.Cam_Left = global::UnityEngine.Mathf.Lerp(CamCon.Cam_Left, Orig_Left.position.x, global::UnityEngine.Time.deltaTime * Orig_Speed);
		}
		else if (Player.transform.position.y < base.transform.position.y)
		{
			if (SR_Tile.color.a < 1f)
			{
				CamCon.Cam_Left = global::UnityEngine.Mathf.Lerp(CamCon.Cam_Left, Hidden_Left.position.x, global::UnityEngine.Time.deltaTime * Target_Speed);
			}
			else
			{
				CamCon.Cam_Left = global::UnityEngine.Mathf.Lerp(CamCon.Cam_Left, Bottom_Left.position.x, global::UnityEngine.Time.deltaTime * Target_Speed);
			}
		}
	}
}
