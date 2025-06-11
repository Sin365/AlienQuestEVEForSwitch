public class Info_Gate : global::UnityEngine.MonoBehaviour
{
	public bool on_Info;

	public global::UnityEngine.SpriteRenderer Spr_Item;

	public global::UnityEngine.SpriteRenderer Spr_Dir;

	private global::UnityEngine.Color color_On = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

	private global::UnityEngine.Color color_Off = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

    GameManager GM => GameManager.instance;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Off;
		Spr_Item.color = color_Off;
		Spr_Dir.color = color_Off;
	}

	private void Update()
	{
		if (!GM.Paused && !GM.onGatePass)
		{
			if (on_Info)
			{
				Spr_Item.color = global::UnityEngine.Color.Lerp(Spr_Item.color, color_On, global::UnityEngine.Time.deltaTime * 5f);
				GetComponent<global::UnityEngine.SpriteRenderer>().color = Spr_Item.color;
				Spr_Dir.color = Spr_Item.color;
			}
			else
			{
				Spr_Item.color = global::UnityEngine.Color.Lerp(Spr_Item.color, color_Off, global::UnityEngine.Time.deltaTime * 3f);
				GetComponent<global::UnityEngine.SpriteRenderer>().color = Spr_Item.color;
				Spr_Dir.color = Spr_Item.color;
			}
		}
	}
}
