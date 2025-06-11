public class Atk_Queen_Tail : global::UnityEngine.MonoBehaviour
{
	private bool onDamage;

	private float Damage_Timer;

	private bool onPower;

	private bool onUpper;

	public global::UnityEngine.GameObject MonObject;

	public global::UnityEngine.GameObject PosObject;

	GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	private void Update()
	{
		if (MonObject == null)
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
		if (GM.Paused || !(PosObject != null))
		{
			return;
		}
		if (onDamage)
		{
			Damage_Timer -= global::UnityEngine.Time.deltaTime;
			if (Damage_Timer <= 0f)
			{
				onDamage = false;
			}
		}
		base.transform.position = PosObject.transform.position;
		base.transform.rotation = PosObject.transform.rotation;
	}

	public void Set_Power()
	{
		onPower = true;
		onUpper = false;
	}

	public void Set_Upper()
	{
		onPower = false;
		onUpper = true;
	}

	public void Reset()
	{
		onPower = false;
		onUpper = false;
		onDamage = false;
	}

	private void OnTriggerStay2D(global::UnityEngine.Collider2D col)
	{
		if (col.name == "Ani" && !GM.Paused && !GM.GameOver && !GM.onGameClear && !onDamage)
		{
			onDamage = true;
			Damage_Timer = 0.5f;
			if (GM.onShield)
			{
				GM.Break_Shield();
			}
		}
	}
}
