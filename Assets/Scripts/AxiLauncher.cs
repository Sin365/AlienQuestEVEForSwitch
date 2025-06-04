using UnityEngine;
using UnityEngine.SceneManagement;

public class AxiLauncher : MonoBehaviour
{

	private void OnEnable()
	{
		InitGame();
	}

	void InitGame()
	{
		GameObject.DontDestroyOnLoad(gameObject);
#if UNITY_SWITCH
		AxiNS.instance.Init();
#endif
		SceneManager.LoadScene("Title");
		//global::UnityEngine.Application.LoadLevel("Title");
	}
}