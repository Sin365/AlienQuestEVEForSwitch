using UnityEngine;

public class LV_0_DistGlow : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.Transform pos_Y;

	public float Target_Distance;

	public global::UnityEngine.SpriteRenderer[] SR_List;

	public float[] Target_Opacity;

	private float[] Orig_Opacity;

	private float distance = 50f;

    GameManager GM => GameManager.instance;
    Player_Control PC => GameManager.instance?.PC;
    GameObject Player => GameManager.instance?.gobj_Player;

    private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		//Player = global::UnityEngine.GameObject.Find("Player");
		Orig_Opacity = new float[SR_List.Length];
		for (int i = 0; i < SR_List.Length; i++)
		{
			Orig_Opacity[i] = SR_List[i].color.a;
		}
		if (pos_Y != null && Player.transform.position.y < pos_Y.position.y)
		{
			for (int j = 0; j < SR_List.Length; j++)
			{
				SR_List[j].color = new global::UnityEngine.Color(SR_List[j].color.r, SR_List[j].color.g, SR_List[j].color.b, Target_Opacity[j]);
			}
		}
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		if (pos_Y != null)
		{
			if (Player.transform.position.y < pos_Y.position.y)
			{
				for (int i = 0; i < SR_List.Length; i++)
				{
					SR_List[i].color = global::UnityEngine.Color.Lerp(SR_List[i].color, new global::UnityEngine.Color(SR_List[i].color.r, SR_List[i].color.g, SR_List[i].color.b, Target_Opacity[i]), global::UnityEngine.Time.deltaTime * 1f);
				}
			}
			else
			{
				for (int j = 0; j < SR_List.Length; j++)
				{
					SR_List[j].color = global::UnityEngine.Color.Lerp(SR_List[j].color, new global::UnityEngine.Color(SR_List[j].color.r, SR_List[j].color.g, SR_List[j].color.b, Orig_Opacity[j]), global::UnityEngine.Time.deltaTime * 1f);
				}
			}
			return;
		}
		distance = global::UnityEngine.Vector3.Distance(Player.transform.position, base.transform.position);
		if (distance < Target_Distance)
		{
			for (int k = 0; k < SR_List.Length; k++)
			{
				SR_List[k].color = global::UnityEngine.Color.Lerp(SR_List[k].color, new global::UnityEngine.Color(SR_List[k].color.r, SR_List[k].color.g, SR_List[k].color.b, Target_Opacity[k]), global::UnityEngine.Time.deltaTime * 1.5f);
			}
		}
		else
		{
			for (int l = 0; l < SR_List.Length; l++)
			{
				SR_List[l].color = global::UnityEngine.Color.Lerp(SR_List[l].color, new global::UnityEngine.Color(SR_List[l].color.r, SR_List[l].color.g, SR_List[l].color.b, Orig_Opacity[l]), global::UnityEngine.Time.deltaTime * 1f);
			}
		}
	}
}
