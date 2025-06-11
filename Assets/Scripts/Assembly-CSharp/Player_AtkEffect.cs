public class Player_AtkEffect : global::UnityEngine.MonoBehaviour
{
	private int Ani_Index;

	private float Ani_Timer = 1f;

	public global::UnityEngine.Sprite[] Spr_Effect_1;

	public global::UnityEngine.PolygonCollider2D Col_Weapon;

	private global::UnityEngine.SpriteRenderer SR;

    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		SR = GetComponent<global::UnityEngine.SpriteRenderer>();
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		if (Ani_Timer > 0.02f)
		{
			Ani_Timer = 0f;
			Ani_Index++;
			switch (Ani_Index)
			{
			case 1:
				SR.sprite = Spr_Effect_1[1];
				break;
			case 2:
				SR.sprite = Spr_Effect_1[2];
				break;
			case 3:
				SR.sprite = Spr_Effect_1[3];
				break;
			case 4:
				SR.sprite = Spr_Effect_1[4];
				Col_Weapon.enabled = false;
				break;
			case 5:
				SR.sprite = Spr_Effect_1[0];
				break;
			}
		}
		else
		{
			Ani_Timer += global::UnityEngine.Time.deltaTime;
		}
		if (Ani_Index > 4)
		{
			global::UnityEngine.Object.Destroy(base.transform.parent.gameObject);
		}
	}
}
