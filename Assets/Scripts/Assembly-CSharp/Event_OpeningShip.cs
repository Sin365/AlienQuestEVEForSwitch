public class Event_OpeningShip : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.SpriteRenderer[] RedLights;

	public global::UnityEngine.Sprite spr_Ship_Off;

	private float Life_Timer;

	private void Start()
	{
	}

	private void Update()
	{
		Life_Timer += global::UnityEngine.Time.deltaTime;
		RedLights[0].color = new global::UnityEngine.Color(1f, 1f, 1f, (1f + global::UnityEngine.Mathf.Sin(Life_Timer * 3f)) * 0.5f);
		for (int i = 1; i < RedLights.Length; i++)
		{
			RedLights[i].color = RedLights[0].color;
		}
	}

	private void Ship_Off()
	{
		GetComponent<global::UnityEngine.SpriteRenderer>().sprite = spr_Ship_Off;
	}
}
