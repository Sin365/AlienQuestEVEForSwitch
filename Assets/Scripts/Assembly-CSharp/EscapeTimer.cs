public class EscapeTimer : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.UI.Text escapeTimer;

	private bool onTimer;

	private bool onGameOver;

	private global::UnityEngine.Vector3 posOrig;

	private global::UnityEngine.Vector3 posTarget;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		posOrig = new global::UnityEngine.Vector3(490f, 740f, 0f);
		posTarget = new global::UnityEngine.Vector3(490f, 420f, 0f);
		GetComponent<global::UnityEngine.RectTransform>().localPosition = posOrig;
		escapeTimer.text = string.Empty;
	}

	private void Set_Timer()
	{
		onTimer = true;
	}

	private void Pause_Timer()
	{
		onTimer = false;
		if (!GM.onMap && !GM.onSave)
		{
			GetComponent<global::UnityEngine.RectTransform>().localPosition = posOrig;
		}
	}

	private void Update()
	{
		if (GM.EventState != 200)
		{
			return;
		}
		if (onTimer && !GM.onGatePass)
		{
			GM.escapeTimer -= global::UnityEngine.Time.deltaTime;
			if (GM.escapeTimer >= 0f)
			{
				Check_Time();
			}
			else
			{
				escapeTimer.text = "00:00:00";
			}
			GetComponent<global::UnityEngine.RectTransform>().localPosition = global::UnityEngine.Vector3.Lerp(GetComponent<global::UnityEngine.RectTransform>().localPosition, posTarget, global::UnityEngine.Time.deltaTime * 10f);
		}
		else if (!GM.Paused && !GM.onSave)
		{
			onTimer = true;
		}
		if (!onGameOver && GM.onEvent && GM.onGameClear)
		{
			onGameOver = true;
			posTarget = posOrig;
		}
		else if (!onGameOver && GM.escapeTimer <= 0.5f && !GM.onGameClear)
		{
			onGameOver = true;
			posTarget = posOrig;
			GM.onEvent = true;
			GM.onGameClear = true;
			GM.Set_FadeOut("GameEnding");
			AxiPlayerPrefs.SetInt("Escaped", 0);
		}
		if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.KeypadPlus))
		{
			GM.escapeTimer = 5f;
		}
	}

	private void Check_Time()
	{
		string empty = string.Empty;
		int num = (int)(GM.escapeTimer / 3600f);
		int num2 = (int)((GM.escapeTimer - (float)(3600 * num)) / 60f);
		int num3 = (int)(GM.escapeTimer % 60f);
		empty = ((num2 <= 9) ? (empty + "0" + num2 + ":") : (empty + num2 + ":"));
		empty = ((num3 <= 9) ? (empty + "0" + num3 + ":") : (empty + num3 + ":"));
		string text = (GM.escapeTimer % 1f).ToString();
		empty = ((text.Length < 4) ? (empty + "00") : (empty + (GM.escapeTimer % 1f).ToString().Substring(2, 2)));
		escapeTimer.text = empty;
	}
}
