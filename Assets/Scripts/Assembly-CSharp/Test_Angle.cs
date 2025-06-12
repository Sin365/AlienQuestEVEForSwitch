using UnityEngine;

public class Test_Angle : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject _Fire_1;

	private float Fire_1_Timer;

	public float Fire_Delay = 1f;

	public bool on_Gravity;

	public bool on_Laser;

	private global::UnityEngine.GameObject Laser;

	private global::UnityEngine.Transform target;

	private global::UnityEngine.Vector3 targetPos;

	private global::UnityEngine.Vector3 thisPos;

	private float angle;

	private float distance;

    Player_Control PC => GameManager.instance.PC;
    GameObject Player => GameManager.instance.gobj_Player;

    GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//Player = global::UnityEngine.GameObject.Find("Player");
		target = Player.transform;
		Fire_1_Timer = global::UnityEngine.Random.Range(-1.5f, 0.5f);
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		targetPos = new global::UnityEngine.Vector3(target.position.x, target.position.y + 2.8f);
		thisPos = base.transform.position;
		targetPos.x -= thisPos.x;
		targetPos.y -= thisPos.y;
		angle = global::UnityEngine.Mathf.Atan2(targetPos.y, targetPos.x) * 57.29578f;
		base.transform.rotation = global::UnityEngine.Quaternion.Euler(new global::UnityEngine.Vector3(0f, 0f, angle + 180f));
		distance = global::UnityEngine.Vector3.Distance(base.transform.position, target.position);
		if (!(distance < 30f) || GM.GameOver || GM.onDown || GM.onHscene || GM.onEvent)
		{
			return;
		}
		Fire_1_Timer += global::UnityEngine.Time.deltaTime;
		if (!(Fire_1_Timer > Fire_Delay))
		{
			return;
		}
		Fire_1_Timer = 0f;
		if (on_Gravity)
		{
			float num = base.transform.position.x - Player.transform.position.x;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Fire_1, thisPos, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
			gameObject.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * (0f - num), global::UnityEngine.ForceMode2D.Impulse);
			gameObject.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.up * 20f, global::UnityEngine.ForceMode2D.Impulse);
		}
		else if (on_Laser)
		{
			Laser = global::UnityEngine.Object.Instantiate(_Fire_1, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
			if (Laser.GetComponent<Mon_GateLaser>() != null)
			{
				Laser.GetComponent<Mon_GateLaser>().MonObject = base.gameObject;
			}
			else if (Laser.GetComponent<Mon_GateLaser_2>() != null)
			{
				Laser.GetComponent<Mon_GateLaser_2>().MonObject = base.gameObject;
			}
		}
		else
		{
			global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(_Fire_1, thisPos, global::UnityEngine.Quaternion.Euler(new global::UnityEngine.Vector3(0f, 0f, angle + 180f))) as global::UnityEngine.GameObject;
		}
		global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Boss_4_Fire(thisPos);
	}
}
