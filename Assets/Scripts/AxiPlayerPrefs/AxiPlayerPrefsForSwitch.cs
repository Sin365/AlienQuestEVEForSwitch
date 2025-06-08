using System.Collections.Generic;
using UnityEngine;

public class AxiPlayerPrefsForSwitch : AxiPlayerPrefsFileBase
{
	static string AxiPlayerPrefsFilePath => AxiPlayerPrefs.SaveDataRootDirPath + "/AxiPlayerPrefs.dat";

	public AxiPlayerPrefsForSwitch():base(NSLoadData, NSSaveData)
    {
		Debug.Log($"AxiPlayerPrefsForSwitch Init");
	}

    public static Dictionary<string, AxiPlayerPrefsKeyValye> NSLoadData()
	{
#if UNITY_SWITCH && !UNITY_EDITOR
        if (!AxiIO.AxiIO.io.file_Exists(AxiPlayerPrefsFilePath))
            return new Dictionary<string, AxiPlayerPrefsKeyValye>();
        else
        {
			string outputData = string.Empty;
			byte[] loadedData = AxiIO.AxiIO.io.file_ReadAllBytes(AxiPlayerPrefsFilePath);
			if (loadedData != null && loadedData.Length != 0)
			{
				using (System.IO.MemoryStream stream = new System.IO.MemoryStream(loadedData))
				{
					using (System.IO.BinaryReader reader = new System.IO.BinaryReader(stream))
					{
						outputData = reader.ReadString();
					}
				}
			}

			if (string.IsNullOrEmpty(outputData))
				return new Dictionary<string, AxiPlayerPrefsKeyValye>();

			return AxiPlayerPrefsFileBase.JsonStrToData(outputData);
		}
#else
        return new Dictionary<string, AxiPlayerPrefsKeyValye>();
#endif
    }

    public static void NSSaveData(Dictionary<string, AxiPlayerPrefsKeyValye> data)
	{
#if UNITY_SWITCH && !UNITY_EDITOR
		Debug.Log($"NSSaveData   Start!");
		Debug.Log($"NSSaveData   data.Keys.Count=> {data.Keys.Count}");
		string jsonStr = AxiPlayerPrefsFileBase.DataToJsonStr(data);
		Debug.Log($"NSSaveData   jsonStr=> {jsonStr}");
		byte[] dataByteArray;
		using (System.IO.MemoryStream stream = new System.IO.MemoryStream(jsonStr.Length * sizeof(char)))
		{
			System.IO.BinaryWriter binaryWriter = new System.IO.BinaryWriter(stream);
			binaryWriter.Write(jsonStr);
			dataByteArray = stream.GetBuffer();
			stream.Close();
		}
		AxiIO.AxiIO.io.file_WriteAllBytes(AxiPlayerPrefsFilePath, dataByteArray, false);
		Debug.Log($"NSSaveData   end!");
#else

#endif
	}

}