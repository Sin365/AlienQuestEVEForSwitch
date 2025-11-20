#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public static class EditorTools
{

    /// <summary>
    /// 创建临时场景
    /// </summary>
    public static void CreateTempScene()
    {
        EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects);
    }



    /// <summary>
    /// 打开搜索面板
    /// </summary>
    /// <param name="title">标题名称</param>
    /// <param name="defaultPath">默认搜索路径</param>
    /// <returns>返回选择的文件夹绝对路径，如果无效返回NULL</returns>
    public static string OpenFolderPanel(string title, string defaultPath, string defaultName = "")
    {
        string openPath = EditorUtility.OpenFolderPanel(title, defaultPath, defaultName);
        if (string.IsNullOrEmpty(openPath))
            return null;

        if (openPath.Contains("/Assets") == false)
        {
            Debug.LogWarning("Please select unity assets folder.");
            return null;
        }
        return openPath;
    }

    /// <summary>
    /// 打开搜索面板
    /// </summary>
    /// <param name="title">标题名称</param>
    /// <param name="defaultPath">默认搜索路径</param>
    /// <returns>返回选择的文件绝对路径，如果无效返回NULL</returns>
    public static string OpenFilePath(string title, string defaultPath, string extension = "")
    {
        string openPath = EditorUtility.OpenFilePanel(title, defaultPath, extension);
        if (string.IsNullOrEmpty(openPath))
            return null;

        if (openPath.Contains("/Assets") == false)
        {
            Debug.LogWarning("Please select unity assets file.");
            return null;
        }
        return openPath;
    }

    /// <summary>
    /// 显示进度框
    /// </summary>
    public static void DisplayProgressBar(string tips, int progressValue, int totalValue)
    {
        EditorUtility.DisplayProgressBar("进度", $"{tips} : {progressValue}/{totalValue}", (float)progressValue / totalValue);
    }

    /// <summary>
    /// 隐藏进度框
    /// </summary>
    public static void ClearProgressBar()
    {
        EditorUtility.ClearProgressBar();
    }
    #region 文件
        /// <summary>
        /// 创建文件所在的目录
        /// </summary>
        /// <param name="filePath">文件路径</param>
    public static void CreateFileDirectory(string filePath)
    {
        string destDirectory = Path.GetDirectoryName(filePath);
        CreateDirectory(destDirectory);
    }

    /// <summary>
    /// 创建文件夹
    /// </summary>
    public static bool CreateDirectory(string directory)
    {
        if (Directory.Exists(directory) == false)
        {
            Directory.CreateDirectory(directory);
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 拷贝文件
    /// </summary>
    public static void CopyFile(string sourcePath, string destPath, bool overwrite)
    {
        if (File.Exists(sourcePath) == false)
            throw new FileNotFoundException(sourcePath);

        // 创建目录
        CreateFileDirectory(destPath);

        // 复制文件
        File.Copy(sourcePath, destPath, overwrite);
    }

    /// <summary>
    /// 清空文件夹
    /// </summary>
    /// <param name="folderPath">要清理的文件夹路径</param>
    public static void ClearFolder(string directoryPath)
    {
        if (Directory.Exists(directoryPath) == false)
            return;

        // 删除文件
        string[] allFiles = Directory.GetFiles(directoryPath);
        for (int i = 0; i < allFiles.Length; i++)
        {
            File.Delete(allFiles[i]);
        }

        // 删除文件夹
        string[] allFolders = Directory.GetDirectories(directoryPath);
        for (int i = 0; i < allFolders.Length; i++)
        {
            Directory.Delete(allFolders[i], true);
        }
    }

    /// <summary>
    /// 获取文件字节大小
    /// </summary>
    public static long GetFileSize(string filePath)
    {
        FileInfo fileInfo = new FileInfo(filePath);
        return fileInfo.Length;
    }
    /// <summary>
    /// 读取文件的所有文本内容
    /// </summary>
    public static string ReadFileAllText(string filePath)
    {
        if (File.Exists(filePath) == false)
            return string.Empty;

        return File.ReadAllText(filePath, Encoding.UTF8);
    }

    /// <summary>
    /// 读取文本的所有文本内容
    /// </summary>
    public static string[] ReadFileAllLine(string filePath)
    {
        if (File.Exists(filePath) == false)
            return null;

        return File.ReadAllLines(filePath, Encoding.UTF8);
    }

    /// <summary>
    /// 检测AssetBundle文件是否合法
    /// </summary>
    public static bool CheckBundleFileValid(byte[] fileData)
    {
        string signature = ReadStringToNull(fileData, 20);
        if (signature == "UnityFS" || signature == "UnityRaw" || signature == "UnityWeb" || signature == "\xFA\xFA\xFA\xFA\xFA\xFA\xFA\xFA")
            return true;
        else
            return false;
    }
    private static string ReadStringToNull(byte[] data, int maxLength)
    {
        List<byte> bytes = new List<byte>();
        for (int i = 0; i < data.Length; i++)
        {
            if (i >= maxLength)
                break;

            byte bt = data[i];
            if (bt == 0)
                break;

            bytes.Add(bt);
        }

        if (bytes.Count == 0)
            return string.Empty;
        else
            return Encoding.UTF8.GetString(bytes.ToArray());
    }
    #endregion
    #region EditorWindow
    public static void FocusUnitySceneWindow()
    {
        EditorWindow.FocusWindowIfItsOpen<SceneView>();
    }
    public static void CloseUnityGameWindow()
    {
        System.Type T = Assembly.Load("UnityEditor").GetType("UnityEditor.GameView");
        EditorWindow.GetWindow(T, false, "GameView", true).Close();
    }
    public static void FocusUnityGameWindow()
    {
        System.Type T = Assembly.Load("UnityEditor").GetType("UnityEditor.GameView");
        EditorWindow.GetWindow(T, false, "GameView", true);
    }
    public static void FocueUnityProjectWindow()
    {
        System.Type T = Assembly.Load("UnityEditor").GetType("UnityEditor.ProjectBrowser");
        EditorWindow.GetWindow(T, false, "Project", true);
    }
    public static void FocusUnityHierarchyWindow()
    {
        System.Type T = Assembly.Load("UnityEditor").GetType("UnityEditor.SceneHierarchyWindow");
        EditorWindow.GetWindow(T, false, "Hierarchy", true);
    }
    public static void FocusUnityInspectorWindow()
    {
        System.Type T = Assembly.Load("UnityEditor").GetType("UnityEditor.InspectorWindow");
        EditorWindow.GetWindow(T, false, "Inspector", true);
    }
    public static void FocusUnityConsoleWindow()
    {
        System.Type T = Assembly.Load("UnityEditor").GetType("UnityEditor.ConsoleWindow");
        EditorWindow.GetWindow(T, false, "Console", true);
    }
    #endregion
    /// <summary>
    /// 调用私有的静态方法
    /// </summary>
    /// <param name="type">类的类型</param>
    /// <param name="method">类里要调用的方法名</param>
    /// <param name="parameters">调用方法传入的参数</param>
    public static object InvokeNonPublicStaticMethod(System.Type type, string method, params object[] parameters)
    {
        var methodInfo = type.GetMethod(method, BindingFlags.NonPublic | BindingFlags.Static);
        if (methodInfo == null)
        {
            UnityEngine.Debug.LogError($"{type.FullName} not found method : {method}");
            return null;
        }
        return methodInfo.Invoke(null, parameters);
    }

    /// <summary>
    /// 调用公开的静态方法
    /// </summary>
    /// <param name="type">类的类型</param>
    /// <param name="method">类里要调用的方法名</param>
    /// <param name="parameters">调用方法传入的参数</param>
    public static object InvokePublicStaticMethod(System.Type type, string method, params object[] parameters)
    {
        var methodInfo = type.GetMethod(method, BindingFlags.Public | BindingFlags.Static);
        if (methodInfo == null)
        {
            UnityEngine.Debug.LogError($"{type.FullName} not found method : {method}");
            return null;
        }
        return methodInfo.Invoke(null, parameters);
    }
}
#endif