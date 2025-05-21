public class Title_AQText : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.Sprite[] AQ_Spr;

	public global::UnityEngine.UI.Image[] AQ_Font;

	public float Life_Timer;

	private float State_Timer;

	private int[] State;

	private int[] Spr_Num;

	private float[] Spr_Timer;

	private float[] Opacity;

	private bool isEnd;

	private void Start()
	{
		State = new int[10];
		Spr_Num = new int[10] { 0, 1, 2, 3, 4, 5, 6, 3, 7, 8 };
		Spr_Timer = new float[10];
		for (int i = 0; i < 10; i++)
		{
			AQ_Font[i].color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
		}
		Opacity = new float[10];
	}

	private void Update()
	{
		Life_Timer += global::UnityEngine.Time.deltaTime;
		if (isEnd || !(Life_Timer > 4f))
		{
			return;
		}
		State_Timer += global::UnityEngine.Time.deltaTime;
		bool flag = true;
		if (State_Timer > 0.5f)
		{
			int num = global::UnityEngine.Random.Range(0, 10);
			if (State[num] < 2)
			{
				State[num] = 1;
			}
		}
		for (int i = 0; i < 10; i++)
		{
			if (State[i] == 0)
			{
				flag = false;
			}
			else
			{
				if (State[i] != 1)
				{
					continue;
				}
				flag = false;
				Spr_Timer[i] += global::UnityEngine.Time.deltaTime;
				if (Spr_Timer[i] > 0.03f)
				{
					Spr_Timer[i] = 0f;
					if (Spr_Num[i] < 9)
					{
						Spr_Num[i]++;
					}
					else
					{
						Spr_Num[i] = 0;
					}
					AQ_Font[i].sprite = AQ_Spr[Spr_Num[i]];
				}
				Opacity[i] += global::UnityEngine.Time.deltaTime * (float)(10 - i) * 0.2f;
				AQ_Font[i].color = new global::UnityEngine.Color(1f, 1f, 1f, Opacity[i]);
				if (Opacity[i] >= 1f)
				{
					State[i] = 2;
					AQ_Font[i].sprite = AQ_Spr[i];
				}
			}
		}
		isEnd = flag;
	}
}
