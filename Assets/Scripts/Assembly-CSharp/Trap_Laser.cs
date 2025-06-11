public class Trap_Laser : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.SpriteRenderer Alert;

	public global::UnityEngine.SpriteRenderer Glow_1;

	public global::UnityEngine.SpriteRenderer Glow_2;

	public global::UnityEngine.SpriteRenderer Glow_3;

	public global::UnityEngine.BoxCollider2D Col_Laser;

	private int State = -1;

	private float State_Timer;

	private float Life_Timer;

	private float alert_Opacity;

	private float glow_Opacity_1;

	private float glow_Opacity_2;

	private float glow_Opacity_3;

	private float glow_Timer_1;

	private float glow_Timer_2;

	private float glow_Timer_3;

    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		global::UnityEngine.SpriteRenderer alert = Alert;
		global::UnityEngine.Color color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
		Glow_3.color = color;
		color = color;
		Glow_2.color = color;
		color = color;
		Glow_1.color = color;
		alert.color = color;
		Col_Laser.enabled = false;
		State_Timer = global::UnityEngine.Random.Range(-1f, 1.5f);
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		if (global::UnityEngine.Mathf.Abs(global::UnityEngine.GameObject.Find("Player").transform.position.x - base.transform.position.x) > 15f || global::UnityEngine.Mathf.Abs(global::UnityEngine.GameObject.Find("Player").transform.position.y - base.transform.position.y) > 15f)
		{
			State = -1;
			glow_Timer_1 = (glow_Timer_2 = (glow_Timer_3 = 0f));
			alert_Opacity = (glow_Opacity_1 = (glow_Opacity_2 = (glow_Opacity_3 = 0f)));
			global::UnityEngine.SpriteRenderer alert = Alert;
			global::UnityEngine.Color color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
			Glow_3.color = color;
			color = color;
			Glow_2.color = color;
			color = color;
			Glow_1.color = color;
			alert.color = color;
			Col_Laser.enabled = false;
			return;
		}
		State_Timer += global::UnityEngine.Time.deltaTime;
		if (State <= 0)
		{
			if (State == -1)
			{
				State = 0;
				State_Timer = global::UnityEngine.Random.Range(1.2f, 1.8f);
			}
			if (State_Timer > 2f)
			{
				State = 1;
				State_Timer = 0f;
				glow_Timer_1 = (glow_Timer_2 = (glow_Timer_3 = 0f));
				glow_Opacity_1 = (glow_Opacity_2 = (glow_Opacity_3 = 1f));
				global::UnityEngine.SpriteRenderer glow_ = Glow_1;
				global::UnityEngine.Color color = new global::UnityEngine.Color(1f, 1f, 1f, 1f);
				Glow_3.color = color;
				color = color;
				Glow_2.color = color;
				glow_.color = color;
				global::UnityEngine.GameObject.Find("Menu").SendMessage("Sound_Laser");
				Col_Laser.enabled = true;
			}
			else if (State_Timer > 1f)
			{
				alert_Opacity = global::UnityEngine.Mathf.Lerp(alert_Opacity, 1f, global::UnityEngine.Time.deltaTime * 5f);
				Alert.color = new global::UnityEngine.Color(1f, 1f, 1f, alert_Opacity);
			}
			return;
		}
		alert_Opacity = global::UnityEngine.Mathf.Lerp(alert_Opacity, 0f, global::UnityEngine.Time.deltaTime * 5f);
		Alert.color = new global::UnityEngine.Color(1f, 1f, 1f, alert_Opacity);
		glow_Timer_1 += global::UnityEngine.Time.deltaTime;
		glow_Timer_2 += global::UnityEngine.Time.deltaTime;
		glow_Timer_3 += global::UnityEngine.Time.deltaTime;
		if (glow_Timer_1 > 0.12f)
		{
			glow_Opacity_1 = global::UnityEngine.Random.Range(0.2f, 1f);
		}
		if (glow_Timer_2 > 0.08f)
		{
			glow_Opacity_2 = global::UnityEngine.Random.Range(0.2f, 1f);
		}
		if (glow_Timer_3 > 0.1f)
		{
			glow_Opacity_3 = global::UnityEngine.Random.Range(0f, 1f);
		}
		Glow_1.color = new global::UnityEngine.Color(1f, 1f, 1f, glow_Opacity_1);
		Glow_2.color = new global::UnityEngine.Color(1f, 1f, 1f, glow_Opacity_2);
		Glow_3.color = new global::UnityEngine.Color(1f, 1f, 1f, glow_Opacity_3);
		if (State_Timer > 1f)
		{
			State = 0;
			State_Timer = 0f;
			glow_Timer_1 = (glow_Timer_2 = (glow_Timer_3 = 0f));
			glow_Opacity_1 = (glow_Opacity_2 = (glow_Opacity_3 = 0f));
			global::UnityEngine.SpriteRenderer glow_2 = Glow_1;
			global::UnityEngine.Color color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
			Glow_3.color = color;
			color = color;
			Glow_2.color = color;
			glow_2.color = color;
			Col_Laser.enabled = false;
		}
	}
}
