public class Magic_Fire_1B : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.SpriteRenderer[] SR_List;

	public global::UnityEngine.GameObject _Dust;

	private bool onExplo;

	private bool onStop;

	private bool onSlow;

	private bool onGround;

	private int facingRight = 1;

	private float Life_Timer;

	private float onCam_Timer;

	private float Dust_Timer;

	private float diatance;

	private float Speed = 40f;

	private float Rot_Speed = 5f;

	private float Ring_Size = 1f;

	private float Glow_Timer;

	private float Glow_Opacity;

	private float Target_Rot;

	private global::UnityEngine.Vector3 posOrig;

	private Player_Control PC;

	private global::UnityEngine.GameObject Player;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		Player = global::UnityEngine.GameObject.Find("Player");
		PC = Player.GetComponent<Player_Control>();
		if (base.transform.localScale.x < 0f)
		{
			facingRight = -1;
		}
		Target_Rot = base.transform.rotation.eulerAngles.z + global::UnityEngine.Random.Range(-80f, 80f);
		Ring_Size = SR_List[0].transform.localScale.x;
		posOrig = base.transform.position;
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		Life_Timer += global::UnityEngine.Time.deltaTime;
		Dust_Timer += global::UnityEngine.Time.deltaTime;
		Glow_Timer += global::UnityEngine.Time.deltaTime;
		if (onCam_Timer > 0f)
		{
			onCam_Timer -= global::UnityEngine.Time.deltaTime;
		}
		diatance = global::UnityEngine.Vector3.Distance(base.transform.position, posOrig);
		if (!onStop)
		{
			if (onSlow)
			{
				Speed = global::UnityEngine.Mathf.Lerp(Speed, 0f, global::UnityEngine.Time.deltaTime * 7f);
			}
			base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * Speed * facingRight);
			Dust_Timer += global::UnityEngine.Time.deltaTime;
			if (Dust_Timer > 0.02f)
			{
				Dust_Timer = 0f;
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Dust, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
				gameObject.transform.localScale = new global::UnityEngine.Vector3(gameObject.transform.localScale.x * (float)facingRight, gameObject.transform.localScale.y, 1f);
				gameObject.transform.Translate(global::UnityEngine.Vector3.up * global::UnityEngine.Random.Range(-0.1f, 0.1f));
			}
			if (!onSlow && Life_Timer > 0.15f)
			{
				base.transform.rotation = global::UnityEngine.Quaternion.Lerp(base.transform.rotation, global::UnityEngine.Quaternion.Euler(new global::UnityEngine.Vector3(0f, 0f, Target_Rot)), global::UnityEngine.Time.deltaTime * Rot_Speed);
			}
		}
		if (!onExplo)
		{
			if (Glow_Timer > 0.01f)
			{
				Glow_Timer = 0f;
				Glow_Opacity = (float)global::UnityEngine.Random.Range(5, 11) * 0.1f;
				SR_List[2].color = new global::UnityEngine.Color(SR_List[2].color.r, SR_List[2].color.g, SR_List[2].color.b, Glow_Opacity);
			}
			if (diatance > 30f || Life_Timer > 1.5f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
		else
		{
			SR_List[0].color = global::UnityEngine.Color.Lerp(SR_List[0].color, new global::UnityEngine.Color(SR_List[0].color.r, SR_List[0].color.g, SR_List[0].color.b, 0f), global::UnityEngine.Time.deltaTime * 10f);
			SR_List[0].transform.localScale = global::UnityEngine.Vector3.Lerp(SR_List[0].transform.localScale, new global::UnityEngine.Vector3(Ring_Size * 2f, Ring_Size * 2f, 1f), global::UnityEngine.Time.deltaTime * 8f);
			SR_List[2].color = global::UnityEngine.Color.Lerp(SR_List[2].color, new global::UnityEngine.Color(SR_List[2].color.r, SR_List[2].color.g, SR_List[2].color.b, 0f), global::UnityEngine.Time.deltaTime * 10f);
			if (Life_Timer > 1f || SR_List[0].color.a < 0.02f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	private void Make_Explo()
	{
		if (!onExplo)
		{
			onExplo = true;
			onSlow = true;
			Life_Timer = 0f;
			SR_List[1].enabled = false;
			SR_List[3].enabled = false;
			SR_List[0].enabled = false;
			SR_List[2].enabled = false;
			if (onCam_Timer > 0.4f)
			{
				global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Hit_2(base.transform.position);
			}
		}
	}

	private void Explo()
	{
		Life_Timer = 0f;
		SR_List[1].enabled = false;
		SR_List[3].enabled = false;
		SR_List[0].color = new global::UnityEngine.Color(SR_List[0].color.r, SR_List[0].color.g, SR_List[0].color.b, 1f);
		SR_List[0].transform.localScale = new global::UnityEngine.Vector3(Ring_Size * 1.2f, Ring_Size * 1.2f, 1f);
		if (onCam_Timer > 0.4f)
		{
			global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Mon_Hit_2(base.transform.position);
		}
	}

	private void OnTriggerExit2D(global::UnityEngine.Collider2D col)
	{
		if (!GM.Paused && col.tag == "Col_Camera")
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void OnTriggerStay2D(global::UnityEngine.Collider2D col)
	{
		if (col.tag == "Col_Camera")
		{
			onCam_Timer = 0.5f;
		}
		if (!GM.Paused && !onGround && (col.tag == "Ground" || col.tag == "Gate" || col.tag == "Breakable" || col.tag == "Mon_Shield"))
		{
			if (!onExplo)
			{
				onExplo = true;
				Explo();
			}
			onStop = true;
			onGround = true;
			GetComponent<global::UnityEngine.CircleCollider2D>().enabled = false;
		}
	}
}
