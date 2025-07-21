using System;
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
		Report();
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
		for (int i = 0; i < count; i++)
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
		string[] elist;
		bool result = AxiNS.instance.io.GetDirectoryEntrysFullRecursion("save:/", out elist);
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
		AxiIO.AxiIO.io.dir_Delete("save:/test", true);
	}

	private void Test9()
	{
		Debug.Log("T9");
		AxiIO.AxiIO.io.file_Delete("save:/test/1.txt");
	}
	void InitGame()
    {
#if UNITY_PSP2 && !UNITY_EDITOR
        //PSVita最好手动创建目录
        if (!AxiIO.Directory.Exists(AxiPlayerPrefs.SaveDataRootDirPath))
            AxiIO.Directory.CreateDirectory(AxiPlayerPrefs.SaveDataRootDirPath);
		UnityEngine.PSVita.PSVitaVideoPlayer.TransferMemToMonoHeap();
#endif
		GameObject.DontDestroyOnLoad(gameObject);
		SceneManager.LoadScene("Title");
		//global::UnityEngine.Application.LoadLevel("Title");
	}

	bool bStart = false;
	private void Update()
	{
		if (!bStart && Input.anyKeyDown)
		{
			mDebugger.enabled = !mDebugger.enabled;
			bStart = true;
			InitGame();
		}
	}
	void Report()
	{
		string platform = Application.platform.ToString();
		string username = "";
		string uuid = "";
		int GameID = 3;
		string Note = "";
#if UNITY_EDITOR
		platform = "UnityEditor"; 
		uuid = "UnityEditor";
#endif

#if UNITY_SWITCH
		if (AxiNS.instance.user.GetNickName(out string _username))
			username = _username;
		else
			username = "获取失败";
		if (AxiNS.instance.user.GetUserID(out nn.account.Uid uid))
			uuid = uid.ToString();
		else
			uuid = "获取失败";
#endif
		// 核心修改：对所有字符串参数进行UTF-8 URL编码
		string encodedPlatform = Uri.EscapeDataString(platform);
		string encodedGamename = Uri.EscapeDataString(Application.productName);
		string encodedUser = Uri.EscapeDataString(username);
		string encodedUuid = Uri.EscapeDataString(uuid);
		string encodedNote = Uri.EscapeDataString(Note);

		// 构建安全URL（使用编码后参数）
		string url = $"http://yizhi.axibug.com/api/reporting/?" +
					 $"gameid={GameID}&" +
					 $"platform={encodedPlatform}&" +
					 $"gamename={encodedGamename}&" +
					 $"user={encodedUser}&" +
					 $"uuid={encodedUuid}&" +
					 $"note={encodedNote}";
		AxiHttp.AxiRequestAsync(url);
	}
}