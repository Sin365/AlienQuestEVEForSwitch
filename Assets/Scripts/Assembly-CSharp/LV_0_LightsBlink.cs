public class LV_0_LightsBlink : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.Transform[] pos_Start;

	public global::UnityEngine.SpriteRenderer[] SR_List;

	public float[] Target_Opacity;

	private float[] Orig_Opacity;

	private bool onBlink;

	private float State_Timer;

	private float Blink_Timer;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		if (pos_Start.Length > 0)
		{
			int num = global::UnityEngine.Random.Range(0, pos_Start.Length);
			base.transform.position = pos_Start[num].position;
		}
		Orig_Opacity = new float[SR_List.Length];
		for (int i = 0; i < SR_List.Length; i++)
		{
			Orig_Opacity[i] = SR_List[i].color.a;
		}
		State_Timer = global::UnityEngine.Random.Range(2f, 5.5f);
		Blink_Timer = -1f;
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		if (!onBlink)
		{
			for (int i = 0; i < SR_List.Length; i++)
			{
				SR_List[i].color = global::UnityEngine.Color.Lerp(SR_List[i].color, new global::UnityEngine.Color(SR_List[i].color.r, SR_List[i].color.g, SR_List[i].color.b, Orig_Opacity[i]), global::UnityEngine.Time.deltaTime * 2f);
			}
			if (State_Timer > 0f)
			{
				State_Timer -= global::UnityEngine.Time.deltaTime;
				return;
			}
			onBlink = true;
			State_Timer = 1f;
			return;
		}
		if (Blink_Timer > 0f)
		{
			Blink_Timer -= global::UnityEngine.Time.deltaTime;
			for (int j = 0; j < SR_List.Length; j++)
			{
				SR_List[j].color = global::UnityEngine.Color.Lerp(SR_List[j].color, new global::UnityEngine.Color(SR_List[j].color.r, SR_List[j].color.g, SR_List[j].color.b, Orig_Opacity[j]), global::UnityEngine.Time.deltaTime * 8f);
			}
		}
		else
		{
			Blink_Timer = global::UnityEngine.Random.Range(0.06f, 0.2f);
			for (int k = 0; k < SR_List.Length; k++)
			{
				SR_List[k].color = new global::UnityEngine.Color(SR_List[k].color.r, SR_List[k].color.g, SR_List[k].color.b, Target_Opacity[k]);
			}
		}
		if (State_Timer > 0f)
		{
			State_Timer -= global::UnityEngine.Time.deltaTime;
			return;
		}
		onBlink = false;
		State_Timer = global::UnityEngine.Random.Range(2f, 5.5f);
	}
}
