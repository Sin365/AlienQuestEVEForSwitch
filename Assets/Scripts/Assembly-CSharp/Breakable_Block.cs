public class Breakable_Block : global::UnityEngine.MonoBehaviour
{
	public bool isBroken;

	private float Broken_Timer;

	private bool onCol = true;

	private int block_num;

	public global::UnityEngine.GameObject[] Blocks;

	public global::UnityEngine.SpriteRenderer BackBlack;

	public global::UnityEngine.SpriteRenderer GlowBlack;

	private float[] brk_Speed;

	private global::UnityEngine.Vector3 size_On = new global::UnityEngine.Vector3(2f, 2f, 1f);

	private global::UnityEngine.Vector3 size_Off = new global::UnityEngine.Vector3(0f, 0f, 1f);

	private global::UnityEngine.Color color_On = new global::UnityEngine.Color(0f, 0f, 0f, 1f);

	private global::UnityEngine.Color color_Off = new global::UnityEngine.Color(0f, 0f, 0f, 0f);

	GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		block_num = Blocks.Length;
		brk_Speed = new float[block_num];
		for (int i = 0; i < block_num; i++)
		{
			brk_Speed[i] = global::UnityEngine.Random.Range(4f, 8f);
		}
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		if (isBroken)
		{
			for (int i = 0; i < block_num; i++)
			{
				Blocks[i].transform.localScale = global::UnityEngine.Vector3.Lerp(Blocks[i].transform.localScale, size_Off, brk_Speed[i] * global::UnityEngine.Time.deltaTime);
			}
			if (BackBlack != null)
			{
				BackBlack.color = global::UnityEngine.Color.Lerp(BackBlack.color, color_Off, global::UnityEngine.Time.deltaTime * 4f);
			}
			if (GlowBlack != null)
			{
				GlowBlack.color = global::UnityEngine.Color.Lerp(GlowBlack.color, color_Off, global::UnityEngine.Time.deltaTime * 4f);
			}
			Broken_Timer += global::UnityEngine.Time.deltaTime;
			if (Broken_Timer > 4f)
			{
				isBroken = false;
				Broken_Timer = 0f;
			}
			return;
		}
		for (int j = 0; j < block_num; j++)
		{
			Blocks[j].transform.localScale = global::UnityEngine.Vector3.Lerp(Blocks[j].transform.localScale, size_On, brk_Speed[j] * global::UnityEngine.Time.deltaTime * 1.5f);
		}
		if (BackBlack != null)
		{
			BackBlack.color = global::UnityEngine.Color.Lerp(BackBlack.color, color_On, global::UnityEngine.Time.deltaTime * 2f);
		}
		if (GlowBlack != null)
		{
			GlowBlack.color = global::UnityEngine.Color.Lerp(GlowBlack.color, color_On, global::UnityEngine.Time.deltaTime * 2f);
		}
		if (!onCol && Blocks[0].transform.localScale.x > 1.9f)
		{
			onCol = true;
			global::UnityEngine.BoxCollider2D[] components = GetComponents<global::UnityEngine.BoxCollider2D>();
			global::UnityEngine.BoxCollider2D[] array = components;
			foreach (global::UnityEngine.BoxCollider2D boxCollider2D in array)
			{
				boxCollider2D.enabled = true;
			}
		}
	}

	private void Break()
	{
		if (!isBroken)
		{
			isBroken = true;
			onCol = false;
			global::UnityEngine.BoxCollider2D[] components = GetComponents<global::UnityEngine.BoxCollider2D>();
			global::UnityEngine.BoxCollider2D[] array = components;
			foreach (global::UnityEngine.BoxCollider2D boxCollider2D in array)
			{
				boxCollider2D.enabled = false;
			}
		}
	}
}
