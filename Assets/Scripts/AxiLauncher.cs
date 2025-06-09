using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AxiLauncher : MonoBehaviour
{
	public Debugger mDebugger;
	public Button btnStart;
	public Button btnTest1;
	public Button btnTest2;
	public Button btnTest3;
	public Button btnTest4;
	public Button btnTest5;
	public Button btnTest6;
	public Button btnTest7;
	public Button btnTest8;
	public Button btnTest9;

#if UNITY_2020_1_OR_NEWER

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
	private static void BeforeSplashScreen()
	{
		UnityEngine.Rendering.SplashScreen.Stop(UnityEngine.Rendering.SplashScreen.StopBehavior.StopImmediate);
		Debug.Log("Unity Logo播放完成，已停止");
	}
#endif
	void OnEnable()
	{
		btnStart.onClick.AddListener(InitGame);
		btnTest1.onClick.AddListener(Test1);
		btnTest2.onClick.AddListener(Test2);
		btnTest3.onClick.AddListener(Test3);
		btnTest4.onClick.AddListener(Test4);
		btnTest5.onClick.AddListener(Test5);
		btnTest6.onClick.AddListener(Test6);
		btnTest7.onClick.AddListener(Test7);
		btnTest8.onClick.AddListener(Test8);
		btnTest9.onClick.AddListener(Test9);
	}

	byte[] GetTestBytes(int count, byte val)
	{
		byte[] bytes = new byte[count];
		for(int i = 0; i < count; i++)
		{
			bytes[i] = val;
		}
		return bytes;
	}

	private void Test1()
	{
		Debug.Log("T1");
		mDebugger.enabled = !mDebugger.enabled;
		//AxiIO.AxiIO.io.file_ReadAllBytes(Save_Control.SaveDataFilePath);
	}
	private void Test2()
	{
		Debug.Log("T2");
		//AxiIO.AxiIO.io.file_Exists(Save_Control.UncensoredFilePath);
		AxiPlayerPrefs.SetInt("UncensoredPatch", 1);
	}
	private void Test3()
	{
		Debug.Log("T3");
		AxiPlayerPrefs.GetInt("Game_Setting");
		AxiIO.AxiIO.io.file_Exists(Save_Control.SaveDataFilePath);
		byte[] saveBytes = AxiIO.AxiIO.io.file_ReadAllBytes(Save_Control.SaveDataFilePath);
		AxiIO.AxiIO.io.file_Exists(Save_Control.UncensoredFilePath);
	}

	private void Test4()
	{
		Debug.Log("T4"); 
		bool result = AxiNS.instance.io.GetDirectoryEntrysFullRecursion("save:/", out var elist);
		if (!result)
			UnityEngine.Debug.Log($"result =>{result}");
		else
		{
			UnityEngine.Debug.Log($"==== FullRecursion Entrys List====");
			foreach (var e in elist)
				UnityEngine.Debug.Log(e);
		}
	}

	private void Test5()
	{
		Debug.Log("T5");
		AxiIO.AxiIO.io.file_WriteAllBytes("save:/test/1.txt", GetTestBytes(50, 1));
	}

	private void Test6()
	{
		Debug.Log("T6");
		AxiIO.AxiIO.io.file_WriteAllBytes("save:/test/1.txt", GetTestBytes(2048, 1));
	}

	private void Test7()
	{
		Debug.Log("T7");
		AxiIO.AxiIO.io.file_WriteAllBytes("save:/test/1.txt", GetTestBytes(1024, 1));
	}

	private void Test8()
	{
		Debug.Log("T8");
		AxiIO.AxiIO.io.dir_Delete("save:/test",true);
	}

	private void Test9()
	{
		Debug.Log("T9");
		AxiIO.AxiIO.io.file_Delete("save:/test/1.txt");
	}
	void InitGame()
	{
		GameObject.DontDestroyOnLoad(gameObject);
		SceneManager.LoadScene("Title");
		//global::UnityEngine.Application.LoadLevel("Title");
	}
}