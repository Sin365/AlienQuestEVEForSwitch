public class Trap_Fang_N : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject[] Head;

	private float[] Life_Timer;

	private float pos_Y;

	private float Damage_Delay;

	private Player_Control PC;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		PC = global::UnityEngine.GameObject.Find("Player").GetComponent<Player_Control>();
		pos_Y = Head[0].transform.position.y;
		Life_Timer = new float[Head.Length];
		for (int i = 0; i < Head.Length; i++)
		{
			Life_Timer[i] = global::UnityEngine.Random.Range(-1.57f, 1.57f);
		}
	}

	private void Update()
	{
		if (!GM.Paused)
		{
			if (Damage_Delay > 0f)
			{
				Damage_Delay -= global::UnityEngine.Time.deltaTime;
			}
			for (int i = 0; i < Head.Length; i++)
			{
				Life_Timer[i] += global::UnityEngine.Time.deltaTime;
				Head[i].transform.position = new global::UnityEngine.Vector3(Head[i].transform.position.x, pos_Y + global::UnityEngine.Mathf.Sin(Life_Timer[i] * 5f) * 0.16f, 0f);
			}
		}
	}

	private void OnTriggerStay2D(global::UnityEngine.Collider2D col)
	{
		if (!(PC.State.ToString() != "Down") || GM.Paused || GM.onGameClear || GM.GameOver)
		{
			return;
		}
		if (col.tag == "Magic_Shield")
		{
			GM.Break_Shield();
		}
		else if (!GM.onShield && col.name == "Ani" && GM.Damage_Timer <= 0f && Damage_Delay <= 0f)
		{
			int dmg = (int)((float)GM.HP_Max * 0.1f);
			if (base.transform.position.x > col.transform.parent.position.x)
			{
				GM.Damage(dmg, -15f, false, 0);
			}
			else
			{
				GM.Damage(dmg, 15f, false, 0);
			}
			Damage_Delay = 1f;
			global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Player_Damage(14, false, PC.transform.position);
			if (PC.GetComponent<UnityEngine.Rigidbody2D>().velocity.y < 1f)
			{
				PC.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.up * 10f, global::UnityEngine.ForceMode2D.Impulse);
			}
		}
	}
}
