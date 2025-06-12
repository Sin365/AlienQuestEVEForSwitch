using UnityEngine;

public class LV_0_Fog : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.Transform pos_Y;

	public global::UnityEngine.SpriteRenderer[] Fog_List;

	public global::UnityEngine.SpriteRenderer Fog_Light;

	private bool on_Fog = true;

	private global::UnityEngine.Color colorFog_OFF = new global::UnityEngine.Color(0f, 0f, 0f, 0f);

	private global::UnityEngine.Color colorFogLight_ON = new global::UnityEngine.Color(0f, 0f, 0f, 0f);
    Player_Control PC => GameManager.instance.PC;
    GameObject Player => GameManager.instance.gobj_Player;

    GameManager GM => GameManager.instance;

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//PC = global::UnityEngine.GameObject.Find("Player").GetComponent<Player_Control>();
		colorFog_OFF = new global::UnityEngine.Color(Fog_List[0].color.r, Fog_List[0].color.g, Fog_List[0].color.b, 0f);
		if (Fog_Light != null)
		{
			colorFogLight_ON = Fog_Light.color;
		}
		if (PC.transform.position.y < pos_Y.position.y)
		{
			Fog_Off();
		}
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		if (on_Fog && PC.transform.position.y < pos_Y.position.y)
		{
			on_Fog = false;
		}
		if (!on_Fog)
		{
			for (int i = 0; i < Fog_List.Length; i++)
			{
				Fog_List[i].color = global::UnityEngine.Color.Lerp(Fog_List[i].color, colorFog_OFF, global::UnityEngine.Time.deltaTime * 2f);
			}
			if (Fog_Light != null)
			{
				Fog_Light.color = global::UnityEngine.Color.Lerp(Fog_Light.color, new global::UnityEngine.Color(colorFogLight_ON.r, colorFogLight_ON.g, colorFogLight_ON.b, 0.19f), global::UnityEngine.Time.deltaTime * 0.5f);
			}
		}
	}

	private void Fog_Off()
	{
		for (int i = 0; i < Fog_List.Length; i++)
		{
			Fog_List[i].color = colorFog_OFF;
		}
		Fog_Light.color = new global::UnityEngine.Color(colorFogLight_ON.r, colorFogLight_ON.g, colorFogLight_ON.b, 0.19f);
	}
}
