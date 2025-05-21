public class Trap_Laser_Col : global::UnityEngine.MonoBehaviour
{
	public Trap_Laser TrapBody;

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
		if (PC.State.ToString() != "Down" && !GM.Paused && !GM.onGameClear && !GM.GameOver && col.name == "Ani" && !GM.onShield && Damage_Delay <= 0f)
		{
			int dmg = (int)((float)GM.HP_Max * 0.25f);
			if (base.transform.position.x > col.transform.parent.position.x)
			{
				GM.Damage(dmg, -20f, false, 0);
			}
			else
			{
				GM.Damage(dmg, 20f, false, 0);
			}
			Damage_Delay = 1f;
		}
	}
}
