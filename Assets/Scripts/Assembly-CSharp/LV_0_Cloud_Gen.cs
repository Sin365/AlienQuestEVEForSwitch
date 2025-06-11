public class LV_0_Cloud_Gen : global::UnityEngine.MonoBehaviour
{
	public float dist_X;

	public float _distY;

	public global::UnityEngine.SpriteRenderer[] Cloud_List;

	private bool[] onCloud;

	private float[] Speed;

	private float[] Regen_Timer;

	private float Life_Timer;

	private global::UnityEngine.Color color_ON = new global::UnityEngine.Color(0f, 0.5f, 1f, 0.5f);

	private global::UnityEngine.Color color_OFF = new global::UnityEngine.Color(0f, 0.5f, 1f, 0f);

	private global::UnityEngine.SpriteRenderer SR;

	GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		SR = GetComponent<global::UnityEngine.SpriteRenderer>();
		onCloud = new bool[Cloud_List.Length];
		Speed = new float[Cloud_List.Length];
		Regen_Timer = new float[Cloud_List.Length];
		for (int i = 0; i < Cloud_List.Length; i++)
		{
			onCloud[i] = true;
			Speed[i] = global::UnityEngine.Random.Range(1f, 3f);
			Regen_Timer[i] = global::UnityEngine.Random.Range(0.5f, 3f);
			Cloud_List[i].transform.position = new global::UnityEngine.Vector3(base.transform.position.x + global::UnityEngine.Random.Range(0f - dist_X, dist_X), base.transform.position.y + global::UnityEngine.Random.Range(0f - _distY, _distY), 0f);
		}
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
		for (int i = 0; i < Cloud_List.Length; i++)
		{
			if (onCloud[i])
			{
				Cloud_List[i].color = global::UnityEngine.Color.Lerp(Cloud_List[i].color, color_ON, global::UnityEngine.Time.deltaTime * Speed[i]);
				if (Cloud_List[i].color.a > 0.4999f)
				{
					onCloud[i] = false;
				}
			}
			else
			{
				Cloud_List[i].color = global::UnityEngine.Color.Lerp(Cloud_List[i].color, color_OFF, global::UnityEngine.Time.deltaTime * Speed[i]);
				if (Cloud_List[i].color.a < 0.0001f)
				{
					onCloud[i] = true;
					Speed[i] = global::UnityEngine.Random.Range(1f, 3f);
					Cloud_List[i].transform.localScale = new global::UnityEngine.Vector3(1f, 1f, 1f);
					Cloud_List[i].transform.position = new global::UnityEngine.Vector3(base.transform.position.x + global::UnityEngine.Random.Range(0f - dist_X, dist_X), base.transform.position.y + global::UnityEngine.Random.Range(0f - _distY, _distY), 0f);
				}
			}
			Cloud_List[i].transform.localScale = new global::UnityEngine.Vector3(Cloud_List[i].transform.localScale.x + global::UnityEngine.Time.deltaTime * 1.5f * Speed[i], Cloud_List[i].transform.localScale.y - global::UnityEngine.Time.deltaTime * 0.1f, 1f);
		}
	}
}
