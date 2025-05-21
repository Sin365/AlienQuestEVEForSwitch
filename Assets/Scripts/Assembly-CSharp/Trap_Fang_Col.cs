public class Trap_Fang_Col : global::UnityEngine.MonoBehaviour
{
	public Trap_Fang_Group TrapBody;

	private float Damage_Delay;

	private Player_Control PC;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		PC = global::UnityEngine.GameObject.Find("Player").GetComponent<Player_Control>();
	}

	private void Update()
	{
		if (!GM.Paused && Damage_Delay > 0f)
		{
			Damage_Delay -= global::UnityEngine.Time.deltaTime;
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
			if (PC.rigidbody2D.velocity.y < 1f)
			{
				PC.rigidbody2D.AddForce(global::UnityEngine.Vector3.up * 10f, global::UnityEngine.ForceMode2D.Impulse);
			}
		}
	}
}
