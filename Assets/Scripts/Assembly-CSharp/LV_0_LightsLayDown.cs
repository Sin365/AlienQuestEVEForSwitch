public class LV_0_LightsLayDown : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.Transform pos_Y;

	public global::UnityEngine.SpriteRenderer[] SR_List;

	public float[] Target_Opacity;

	private float[] Orig_Opacity;

	private float distance = 50f;

	private GameManager GM;

	private global::UnityEngine.GameObject Player;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		Player = global::UnityEngine.GameObject.Find("Player");
		Orig_Opacity = new float[SR_List.Length];
		for (int i = 0; i < SR_List.Length; i++)
		{
			Orig_Opacity[i] = SR_List[i].color.a;
		}
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		if (global::UnityEngine.GameObject.Find("Main Camera").transform.position.y < pos_Y.position.y)
		{
			for (int i = 0; i < SR_List.Length; i++)
			{
				SR_List[i].color = global::UnityEngine.Color.Lerp(SR_List[i].color, new global::UnityEngine.Color(SR_List[i].color.r, SR_List[i].color.g, SR_List[i].color.b, Orig_Opacity[i]), global::UnityEngine.Time.deltaTime * 1.5f);
			}
		}
		else
		{
			for (int j = 0; j < SR_List.Length; j++)
			{
				SR_List[j].color = global::UnityEngine.Color.Lerp(SR_List[j].color, new global::UnityEngine.Color(SR_List[j].color.r, SR_List[j].color.g, SR_List[j].color.b, Target_Opacity[j]), global::UnityEngine.Time.deltaTime * 3f);
			}
		}
	}
}
