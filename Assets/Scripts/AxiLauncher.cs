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
		SceneManager.LoadScene("Title");
		//global::UnityEngine.Application.LoadLevel("Title");
	}
}