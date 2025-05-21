public class DemoText : global::UnityEngine.MonoBehaviour
{
	private bool onEnabled;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		GetComponent<global::UnityEngine.UI.Text>().color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
		GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(0f, -1000f, 0f);
	}

	private void Update()
	{
		if (GM.Room_Num == 92 && !GM.Paused)
		{
			if (!onEnabled)
			{
				onEnabled = true;
			}
			GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(0f, 0f, 0f);
			GetComponent<global::UnityEngine.UI.Text>().color = global::UnityEngine.Color.Lerp(GetComponent<global::UnityEngine.UI.Text>().color, new global::UnityEngine.Color(1f, 1f, 1f, 1f), global::UnityEngine.Time.deltaTime * 1f);
		}
		else if (onEnabled)
		{
			GetComponent<global::UnityEngine.UI.Text>().color = global::UnityEngine.Color.Lerp(GetComponent<global::UnityEngine.UI.Text>().color, new global::UnityEngine.Color(1f, 1f, 1f, 0f), global::UnityEngine.Time.deltaTime * 5f);
			if (GetComponent<global::UnityEngine.UI.Text>().color.a < 0.005f)
			{
				onEnabled = false;
				GetComponent<global::UnityEngine.UI.Text>().color = new global::UnityEngine.Color(1f, 1f, 1f, 0f);
				GetComponent<global::UnityEngine.RectTransform>().localPosition = new global::UnityEngine.Vector3(0f, -1000f, 0f);
			}
		}
	}
}
