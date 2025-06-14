using UnityEngine;

public class Mon_Fire : global::UnityEngine.MonoBehaviour
{
	public int Type;

	public int Damage = 30;

	public float DmgForce = 10f;

	public global::UnityEngine.SpriteRenderer[] SR_List;

	public global::UnityEngine.GameObject _Dust;

	public global::UnityEngine.GameObject _Explo;

	private global::UnityEngine.Color color_Explo = new global::UnityEngine.Color(1f, 1f, 1f);

	private bool onExplo;

	private float Life_Timer;

	private float onCam_Timer;

	private float Life_Max = 2f;

	private float Dust_Timer;

	private float diatance;

	private float Rot_Speed = 5f;

	private float Ring_Size = 1f;

	private float Glow_Timer;

	private float Glow_Opacity;

	private global::UnityEngine.Vector3 posOrig;

    Player_Control PC => GameManager.instance?.PC;
    GameObject Player => GameManager.instance?.gobj_Player;

    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//Player = global::UnityEngine.GameObject.Find("Player");
		//PC = Player.GetComponent<Player_Control>();
		Ring_Size = SR_List[0].transform.localScale.x;
		posOrig = base.transform.position;
		if (Type == 1)
		{
			Life_Max = 3f;
		}
		else if (Type == 3)
		{
			Life_Max = 1.5f;
		}
		else if (Type == 6)
		{
			Life_Max = 3f;
		}
		else if (Type == 7)
		{
			Life_Max = 1.6f;
			Rot_Speed = 0f;
		}
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
		if (!onExplo)
		{
			if (Glow_Timer > 0.01f)
			{
				Glow_Timer = 0f;
				Glow_Opacity = (float)global::UnityEngine.Random.Range(5, 11) * 0.1f;
				SR_List[2].color = new global::UnityEngine.Color(SR_List[2].color.r, SR_List[2].color.g, SR_List[2].color.b, Glow_Opacity);
			}
			if (Type == 1)
			{
				base.transform.Translate(global::UnityEngine.Vector3.left * global::UnityEngine.Time.deltaTime * 12f);
				Dust_Timer += global::UnityEngine.Time.deltaTime;
				if (onCam_Timer > 0f && Dust_Timer > 0.01f)
				{
					Dust_Timer = 0f;
					global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Dust, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
					if (base.transform.localScale.y < 1f)
					{
						gameObject.transform.localScale = new global::UnityEngine.Vector3(gameObject.transform.localScale.x * base.transform.localScale.x, gameObject.transform.localScale.y * base.transform.localScale.y, 1f);
					}
					gameObject.transform.Translate(global::UnityEngine.Vector3.up * global::UnityEngine.Random.Range(-0.1f, 0.1f));
				}
			}
			else if (Type == 2)
			{
				base.transform.Translate(global::UnityEngine.Vector3.left * global::UnityEngine.Time.deltaTime * 15f);
				Dust_Timer += global::UnityEngine.Time.deltaTime;
				if (onCam_Timer > 0f && Dust_Timer > 0.01f)
				{
					Dust_Timer = 0f;
					global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(_Dust, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
					if (base.transform.localScale.y < 1f)
					{
						gameObject2.transform.localScale = new global::UnityEngine.Vector3(gameObject2.transform.localScale.x * base.transform.localScale.x, gameObject2.transform.localScale.y * base.transform.localScale.y, 1f);
					}
					gameObject2.transform.Translate(global::UnityEngine.Vector3.up * global::UnityEngine.Random.Range(-0.1f, 0.1f));
				}
				Rot_Speed = global::UnityEngine.Mathf.Lerp(Rot_Speed, 0f, global::UnityEngine.Time.deltaTime * 1f);
				if (Life_Timer > 0.2f && Life_Timer < 0.8f)
				{
					float angle = Get_Angle();
					base.transform.rotation = global::UnityEngine.Quaternion.Lerp(base.transform.rotation, global::UnityEngine.Quaternion.Euler(0f, 0f, angle), global::UnityEngine.Time.deltaTime * Rot_Speed);
				}
			}
			else if (Type == 3)
			{
				base.transform.Translate(global::UnityEngine.Vector3.left * global::UnityEngine.Time.deltaTime * 18f);
				Dust_Timer += global::UnityEngine.Time.deltaTime;
				if (onCam_Timer > 0f && Dust_Timer > 0.01f)
				{
					Dust_Timer = 0f;
					global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(_Dust, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
					if (base.transform.localScale.y < 1f)
					{
						gameObject3.transform.localScale = new global::UnityEngine.Vector3(gameObject3.transform.localScale.x * base.transform.localScale.x, gameObject3.transform.localScale.y * base.transform.localScale.y, 1f);
					}
					global::UnityEngine.GameObject gameObject4 = global::UnityEngine.Object.Instantiate(_Dust, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
					gameObject4.transform.localScale = gameObject3.transform.localScale;
				}
			}
			else if (Type == 5 || Type == 10)
			{
				base.transform.Translate(global::UnityEngine.Vector3.left * global::UnityEngine.Time.deltaTime * 20f);
				Dust_Timer += global::UnityEngine.Time.deltaTime;
				if (onCam_Timer > 0f && Dust_Timer > 0.01f)
				{
					Dust_Timer = 0f;
					global::UnityEngine.GameObject gameObject5 = global::UnityEngine.Object.Instantiate(_Dust, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
					if (base.transform.localScale.y < 1f)
					{
						gameObject5.transform.localScale = new global::UnityEngine.Vector3(gameObject5.transform.localScale.x * base.transform.localScale.x, gameObject5.transform.localScale.y * base.transform.localScale.y, 1f);
					}
					gameObject5.transform.Translate(global::UnityEngine.Vector3.up * global::UnityEngine.Random.Range(-0.1f, 0.1f));
					gameObject5.transform.Translate(global::UnityEngine.Vector3.right * -0.5f);
				}
				if (Life_Timer > 0.2f && Life_Timer < 0.8f)
				{
					float angle2 = Get_Angle();
					base.transform.rotation = global::UnityEngine.Quaternion.Lerp(base.transform.rotation, global::UnityEngine.Quaternion.Euler(0f, 0f, angle2), global::UnityEngine.Time.deltaTime * 2f);
				}
			}
			else if (Type == 6)
			{
				base.transform.Translate(global::UnityEngine.Vector3.left * global::UnityEngine.Time.deltaTime * 20f);
				Dust_Timer += global::UnityEngine.Time.deltaTime;
				if (onCam_Timer > 0f && Dust_Timer > 0.01f)
				{
					Dust_Timer = 0f;
					global::UnityEngine.GameObject gameObject6 = global::UnityEngine.Object.Instantiate(_Dust, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
					if (base.transform.localScale.y < 1f)
					{
						gameObject6.transform.localScale = new global::UnityEngine.Vector3(gameObject6.transform.localScale.x * base.transform.localScale.x, gameObject6.transform.localScale.y * base.transform.localScale.y, 1f);
					}
					gameObject6.transform.Translate(global::UnityEngine.Vector3.up * global::UnityEngine.Random.Range(-0.1f, 0.1f));
					gameObject6.transform.Translate(global::UnityEngine.Vector3.right * -0.5f);
				}
				if (Life_Timer > 0.4f)
				{
					Rot_Speed = global::UnityEngine.Mathf.Lerp(Rot_Speed, 0f, global::UnityEngine.Time.deltaTime * 0.75f);
					float angle3 = Get_Angle();
					base.transform.rotation = global::UnityEngine.Quaternion.Lerp(base.transform.rotation, global::UnityEngine.Quaternion.Euler(0f, 0f, angle3), global::UnityEngine.Time.deltaTime * Rot_Speed);
				}
			}
			else if (Type == 4)
			{
				base.transform.Translate(global::UnityEngine.Vector3.left * global::UnityEngine.Time.deltaTime * 17f);
				Dust_Timer += global::UnityEngine.Time.deltaTime;
				if (onCam_Timer > 0f && Dust_Timer > 0.01f)
				{
					Dust_Timer = 0f;
					global::UnityEngine.GameObject gameObject7 = global::UnityEngine.Object.Instantiate(_Dust, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
					if (base.transform.localScale.y < 1f)
					{
						gameObject7.transform.localScale = new global::UnityEngine.Vector3(gameObject7.transform.localScale.x * base.transform.localScale.x, gameObject7.transform.localScale.y * base.transform.localScale.y, 1f);
					}
					gameObject7.transform.Translate(global::UnityEngine.Vector3.up * global::UnityEngine.Random.Range(-0.1f, 0.1f));
					global::UnityEngine.GameObject gameObject8 = global::UnityEngine.Object.Instantiate(_Dust, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
					gameObject8.transform.localScale = gameObject7.transform.localScale;
					gameObject8.transform.Translate(global::UnityEngine.Vector3.up * global::UnityEngine.Random.Range(-0.16f, 0.16f));
				}
				if (Life_Timer > 0.3f && Life_Timer < 1.5f)
				{
					float angle4 = Get_Angle();
					base.transform.rotation = global::UnityEngine.Quaternion.Lerp(base.transform.rotation, global::UnityEngine.Quaternion.Euler(0f, 0f, angle4), global::UnityEngine.Time.deltaTime * 0.5f);
				}
			}
			else if (Type == 7)
			{
				base.transform.Translate(global::UnityEngine.Vector3.left * global::UnityEngine.Time.deltaTime * 15f);
				Dust_Timer += global::UnityEngine.Time.deltaTime;
				if (onCam_Timer > 0f && Dust_Timer > 0.01f)
				{
					Dust_Timer = 0f;
					global::UnityEngine.GameObject gameObject9 = global::UnityEngine.Object.Instantiate(_Dust, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
					if (base.transform.localScale.y < 1f)
					{
						gameObject9.transform.localScale = new global::UnityEngine.Vector3(gameObject9.transform.localScale.x * base.transform.localScale.x, gameObject9.transform.localScale.y * base.transform.localScale.y, 1f);
					}
					gameObject9.transform.Translate(global::UnityEngine.Vector3.up * global::UnityEngine.Random.Range(-0.1f, 0.1f));
					global::UnityEngine.GameObject gameObject10 = global::UnityEngine.Object.Instantiate(_Dust, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
					gameObject10.transform.localScale = gameObject9.transform.localScale;
					gameObject10.transform.Translate(global::UnityEngine.Vector3.up * global::UnityEngine.Random.Range(-0.3f, 0.3f));
				}
				if (Life_Timer < 1f)
				{
					Rot_Speed = global::UnityEngine.Mathf.Lerp(Rot_Speed, 5f, global::UnityEngine.Time.deltaTime);
				}
				else
				{
					Rot_Speed = global::UnityEngine.Mathf.Lerp(Rot_Speed, 0f, global::UnityEngine.Time.deltaTime * 3f);
				}
				if (Life_Timer > 0.3f)
				{
					float angle5 = Get_Angle();
					base.transform.rotation = global::UnityEngine.Quaternion.Lerp(base.transform.rotation, global::UnityEngine.Quaternion.Euler(0f, 0f, angle5), global::UnityEngine.Time.deltaTime * Rot_Speed);
				}
			}
			else if (Type == 8)
			{
				base.transform.Translate(global::UnityEngine.Vector3.left * global::UnityEngine.Time.deltaTime * 20f);
				Dust_Timer += global::UnityEngine.Time.deltaTime;
				if (onCam_Timer > 0f && Dust_Timer > 0.01f)
				{
					Dust_Timer = 0f;
					global::UnityEngine.GameObject gameObject11 = global::UnityEngine.Object.Instantiate(_Dust, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
					if (base.transform.localScale.y < 1f)
					{
						gameObject11.transform.localScale = new global::UnityEngine.Vector3(gameObject11.transform.localScale.x * base.transform.localScale.x, gameObject11.transform.localScale.y * base.transform.localScale.y, 1f);
					}
					gameObject11.transform.Translate(global::UnityEngine.Vector3.up * global::UnityEngine.Random.Range(-0.2f, 0.2f));
					gameObject11.transform.Translate(global::UnityEngine.Vector3.right * 0.2f);
				}
			}
			else if (Type == 15)
			{
				base.transform.Translate(global::UnityEngine.Vector3.left * global::UnityEngine.Time.deltaTime * 22f);
				Dust_Timer += global::UnityEngine.Time.deltaTime;
				if (onCam_Timer > 0f && Dust_Timer > 0.02f)
				{
					Dust_Timer = 0f;
					global::UnityEngine.GameObject gameObject12 = global::UnityEngine.Object.Instantiate(_Dust, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
					if (base.transform.localScale.y < 1f)
					{
						gameObject12.transform.localScale = new global::UnityEngine.Vector3(gameObject12.transform.localScale.x * base.transform.localScale.x, gameObject12.transform.localScale.y * base.transform.localScale.y, 1f);
					}
					gameObject12.transform.Translate(global::UnityEngine.Vector3.right * 0.6f);
				}
			}
			else if (Type == 16)
			{
				base.transform.Translate(global::UnityEngine.Vector3.left * global::UnityEngine.Time.deltaTime * 22f);
				Dust_Timer += global::UnityEngine.Time.deltaTime;
				if (onCam_Timer > 0f && Dust_Timer > 0.02f)
				{
					Dust_Timer = 0f;
					global::UnityEngine.GameObject gameObject13 = global::UnityEngine.Object.Instantiate(_Dust, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
					if (base.transform.localScale.y < 1f)
					{
						gameObject13.transform.localScale = new global::UnityEngine.Vector3(gameObject13.transform.localScale.x * base.transform.localScale.x, gameObject13.transform.localScale.y * base.transform.localScale.y, 1f);
					}
					gameObject13.transform.Translate(global::UnityEngine.Vector3.right * 0.6f);
				}
			}
			diatance = global::UnityEngine.Vector3.Distance(base.transform.position, posOrig);
			if (diatance > 30f || Life_Timer > Life_Max)
			{
				base.transform.localScale = global::UnityEngine.Vector3.Lerp(base.transform.localScale, new global::UnityEngine.Vector3(0.5f, 0f, 1f), global::UnityEngine.Time.deltaTime * 10f);
				if (onCam_Timer <= 0f)
				{
					global::UnityEngine.Object.Destroy(base.gameObject);
				}
			}
			if (diatance > 40f || Life_Timer > 10f || base.transform.localScale.y < 0.05f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
		else
		{
			SR_List[0].color = global::UnityEngine.Color.Lerp(SR_List[0].color, new global::UnityEngine.Color(SR_List[0].color.r, SR_List[0].color.g, SR_List[0].color.b, 0f), global::UnityEngine.Time.deltaTime * 10f);
			if (Type == 15 || Type == 16)
			{
				SR_List[0].transform.localScale = global::UnityEngine.Vector3.Lerp(SR_List[0].transform.localScale, new global::UnityEngine.Vector3(Ring_Size * 7f, Ring_Size * 7f, 1f), global::UnityEngine.Time.deltaTime * 10f);
			}
			else
			{
				SR_List[0].transform.localScale = global::UnityEngine.Vector3.Lerp(SR_List[0].transform.localScale, new global::UnityEngine.Vector3(Ring_Size * 2f, Ring_Size * 2f, 1f), global::UnityEngine.Time.deltaTime * 8f);
			}
			SR_List[2].color = global::UnityEngine.Color.Lerp(SR_List[2].color, new global::UnityEngine.Color(SR_List[2].color.r, SR_List[2].color.g, SR_List[2].color.b, 0f), global::UnityEngine.Time.deltaTime * 10f);
			if (Life_Timer > 3f || SR_List[0].color.a < 0.02f)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	private float Get_Angle()
	{
		global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3(Player.transform.position.x, Player.transform.position.y + 2.4f);
		global::UnityEngine.Vector3 position = base.transform.position;
		vector.x -= position.x;
		vector.y -= position.y;
		return global::UnityEngine.Mathf.Atan2(vector.y, vector.x) * 57.29578f + 180f;
	}

	private void Explo(bool onGround)
	{
		Life_Timer = 0f;
		SR_List[1].enabled = false;
		if (SR_List.Length > 3)
		{
			SR_List[3].enabled = false;
		}
		SR_List[0].color = new global::UnityEngine.Color(SR_List[0].color.r, SR_List[0].color.g, SR_List[0].color.b, 1f);
		SR_List[0].transform.localScale = new global::UnityEngine.Vector3(Ring_Size * 1.2f, Ring_Size * 1.2f, 1f);
		base.transform.localScale = new global::UnityEngine.Vector3(1f, 1f, 1f);
		if (onCam_Timer > 0.4f)
		{
			GameManager.instance.sc_Sound_List.Mon_Hit_2(base.transform.position);
		}
		if (_Explo != null)
		{
			if (Type == 5)
			{
				color_Explo = new global::UnityEngine.Color(0.75f, 1f, 0f);
			}
			else if (Type == 7)
			{
				color_Explo = new global::UnityEngine.Color(1f, 0f, 1f);
			}
			else
			{
				color_Explo = new global::UnityEngine.Color(1f, 1f, 0f);
			}
			if (onGround)
			{
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(_Explo, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
				gameObject.transform.localScale = new global::UnityEngine.Vector3(-2.2f, 2.2f, 1f);
				gameObject.GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Explo;
				return;
			}
			float num = global::UnityEngine.Random.Range(-0.2f, 0.2f);
			global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(_Explo, new global::UnityEngine.Vector3(base.transform.position.x + num, base.transform.position.y + num, 0f), global::UnityEngine.Quaternion.Euler(0f, 0f, global::UnityEngine.Random.Range(0f, 60f))) as global::UnityEngine.GameObject;
			num = global::UnityEngine.Random.Range(-0.2f, 0.2f);
			global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(_Explo, new global::UnityEngine.Vector3(base.transform.position.x + num, base.transform.position.y + num, 0f), global::UnityEngine.Quaternion.Euler(0f, 0f, global::UnityEngine.Random.Range(90f, 150f))) as global::UnityEngine.GameObject;
			num = global::UnityEngine.Random.Range(-0.2f, 0.2f);
			global::UnityEngine.GameObject gameObject4 = global::UnityEngine.Object.Instantiate(_Explo, new global::UnityEngine.Vector3(base.transform.position.x + num, base.transform.position.y + num, 0f), global::UnityEngine.Quaternion.Euler(0f, 0f, global::UnityEngine.Random.Range(180f, 240f))) as global::UnityEngine.GameObject;
			num = global::UnityEngine.Random.Range(-0.2f, 0.2f);
			global::UnityEngine.GameObject gameObject5 = global::UnityEngine.Object.Instantiate(_Explo, new global::UnityEngine.Vector3(base.transform.position.x + num, base.transform.position.y + num, 0f), global::UnityEngine.Quaternion.Euler(0f, 0f, global::UnityEngine.Random.Range(240f, 330f))) as global::UnityEngine.GameObject;
			gameObject2.GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Explo;
			gameObject3.GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Explo;
			gameObject4.GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Explo;
			gameObject5.GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Explo;
		}
	}

	private void OnTriggerStay2D(global::UnityEngine.Collider2D col)
	{
		if (col.tag == "Col_Camera")
		{
			onCam_Timer = 0.5f;
		}
		if (GM.Paused || onExplo)
		{
			return;
		}
		if (col.name == "Ani" && !GM.onShield && GM.Damage_Timer <= 0f && !GM.GameOver && !GM.onEvent && !GM.onHscene && !GM.onDown)
		{
			onExplo = true;
			int num = ((!(base.transform.position.x > col.transform.position.x)) ? 1 : (-1));
			if (Type == 10)
			{
				GM.Damage(Damage, DmgForce * (float)num, true, 10);
			}
			else if (Type == 7)
			{
				GM.Poison_Timer = 3f;
				if (GM.Poison_DMG < 10)
				{
					GM.Poison_DMG = 10;
				}
				GM.Damage(Damage, DmgForce * (float)num, false, 10);
			}
			else
			{
				GM.Damage(Damage, DmgForce * (float)num, false, 10);
			}
			Explo(false);
		}
		else if (col.tag == "Magic_Shield" || col.tag == "Ground" || col.tag == "Gate" || col.tag == "Breakable")
		{
			onExplo = true;
			Explo(true);
		}
		else if (col.tag == "Magic_Fire")
		{
			string text = col.name.Substring(0, 11);
			if (text == "MagicFire_5")
			{
				onExplo = true;
				Explo(true);
			}
		}
	}
}
