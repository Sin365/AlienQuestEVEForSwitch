using UnityEngine;
using AxiIO;
using System.Runtime.Serialization.Formatters.Binary;

public class Save_Control : global::UnityEngine.MonoBehaviour
{

	[global::System.Serializable]
	public class PlayerData
	{
		public float version = 1f;

		public bool test_data = true;

		public bool[] isSaved = new bool[3];

		public int[] Room_Num = new int[3];

		public int[] Level = new int[3] { 1, 1, 1 };

		public int[] HP = new int[3];

		public int[] HP_Max = new int[3] { 10, 10, 10 };

		public int[] MP = new int[3];

		public int[] MP_Max = new int[3] { 10, 10, 10 };

		public bool[] onCloth = new bool[3];

		public int[] ATK = new int[3];

		public int[] DEF = new int[3];

		public int[] STR = new int[3];

		public int[] CON = new int[3];

		public int[] INT = new int[3];

		public int[] LCK = new int[3];

		public int[] ExpNow = new int[3];

		public int[] Kills = new int[3];

		public float[] Rate = new float[3];

		public float[] PlayTime = new float[3];

		public int[] StatPoints = new int[3];

		public int[] Weapon_Num = new int[3];

		public int[] Skill_Num = new int[3];

		public bool[] onWeapon_1 = new bool[3];

		public bool[] onWeapon_2 = new bool[3];

		public bool[] onWeapon_3 = new bool[3];

		public bool[] onWeapon_4 = new bool[3];

		public bool[] onWeapon_5 = new bool[3];

		public bool[] onSkill_2 = new bool[3];

		public bool[] onSkill_3 = new bool[3];

		public bool[] onSkill_4 = new bool[3];

		public bool[] onSkill_5 = new bool[3];

		public bool[] onScrew = new bool[3];

		public bool[] onHighJump = new bool[3];

		public bool[] onSpeedUp = new bool[3];

		public bool[] onDBJump = new bool[3];

		public bool[] onBackDash = new bool[3];

		public bool[] onCard_1 = new bool[3];

		public bool[] onCard_2 = new bool[3];

		public bool[] onCard_3 = new bool[3];

		public bool[] onCard_4 = new bool[3];

		public bool[] onCard_5 = new bool[3];

		public int[] Bonus_HP = new int[3];

		public int[] Bonus_MP = new int[3];

		public int[] Bonus_ATK = new int[3];

		public int[] Bonus_Regen = new int[3];

		public int[] Bonus_Blood = new int[3];

		public int[] E_scene_1;

		public int[] E_scene_2;

		public int[] E_scene_3;

		public int[] Map_1;

		public int[] Map_2;

		public int[] Map_3;

		public int[] BonusItems_1;

		public int[] BonusItems_2;

		public int[] BonusItems_3;

		public int[] Bonus_Life = new int[3];

		public int[] ExtraInt_A = new int[3];

		public int[] ExtraInt_B = new int[3];

		public int[] ExtraInt_C = new int[3];

		public float[] ExtraFlo_A = new float[3];

		public float[] ExtraFlo_B = new float[3];

		public float[] ExtraFlo_C = new float[3];

		public int[] H_scene;

		public int[] H_Over;
	}

	private float Version = 1f;

#if UNITY_SWITCH && !UNITY_EDITOR
	string SaveDataRootDirPath = "save:/";
#else
	string SaveDataRootDirPath = Application.persistentDataPath;
#endif
	string UncensoredFilePath => SaveDataRootDirPath + "/Uncensored.dat";
	string SaveDataFilePath => SaveDataRootDirPath + "/SaveData.dat";

	public Save_Control.PlayerData SaveData = new Save_Control.PlayerData();

	private void Start()
	{
		Make_Data();
	}

	private void Check_Uncensored()
	{
		//if (global::System.IO.File.Exists("Uncensored.dat"))
		if (AxiIO.AxiIO.io.file_Exists(UncensoredFilePath))
		{
			AxiPlayerPrefs.SetInt("UncensoredPatch", 1);
		}
		else
		{
			AxiPlayerPrefs.SetInt("UncensoredPatch", 0);
		}
	}

	private void Test_MakeFile_Uncensored()
	{
		global::System.Runtime.Serialization.Formatters.Binary.BinaryFormatter binaryFormatter = new global::System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
		//global::System.IO.FileStream fileStream = global::System.IO.File.Create("Uncensored.dat");
		//fileStream.Close();
		AxiIO.AxiIO.io.file_WriteAllBytes(UncensoredFilePath, new byte[] { 0 });
	}

	public void Load_Game()
	{
		bool flag = false;
		//if (global::System.IO.File.Exists("SaveData.dat"))
		if (AxiIO.AxiIO.io.file_Exists(SaveDataFilePath))
		{
			global::System.Runtime.Serialization.Formatters.Binary.BinaryFormatter binaryFormatter = new global::System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			//global::System.IO.FileStream fileStream = global::System.IO.File.Open("SaveData.dat", global::System.IO.FileMode.Open);
			byte[] saveBytes = AxiIO.AxiIO.io.file_ReadAllBytes(SaveDataFilePath);
			//if (fileStream != null && fileStream.Length > 0)
			if (saveBytes != null && saveBytes.Length > 0)
			{
				//SaveData = (Save_Control.PlayerData)binaryFormatter.Deserialize(fileStream);

				using (System.IO.MemoryStream ms = new System.IO.MemoryStream(saveBytes))
				{
					SaveData = (Save_Control.PlayerData)binaryFormatter.Deserialize(ms);
				}
				if (Version != SaveData.version)
				{
				}
				if (SaveData.version < 0.12f)
				{
					flag = true;
					AxiPlayerPrefs.SetInt("H_1", 0);
					AxiPlayerPrefs.SetInt("H_2", 0);
					AxiPlayerPrefs.SetInt("H_3", 0);
					AxiPlayerPrefs.SetInt("H_4", 0);
					AxiPlayerPrefs.SetInt("H_8", 0);
					AxiPlayerPrefs.SetInt("H_9", 0);
					AxiPlayerPrefs.SetInt("H_13", 0);
					SaveData.H_scene = new int[70];
				}
				if (SaveData.version <= 0.12f)
				{
					SaveData.E_scene_1[16] = (SaveData.E_scene_1[17] = (SaveData.E_scene_1[18] = (SaveData.E_scene_1[19] = 0)));
					SaveData.E_scene_2[16] = (SaveData.E_scene_2[17] = (SaveData.E_scene_2[18] = (SaveData.E_scene_2[19] = 0)));
					SaveData.E_scene_3[16] = (SaveData.E_scene_3[17] = (SaveData.E_scene_3[18] = (SaveData.E_scene_3[19] = 0)));
					SaveData.Room_Num[0] = (SaveData.Room_Num[1] = (SaveData.Room_Num[2] = 0));
					global::UnityEngine.Debug.Log("v0.12 Loaded.  Escene[16] -> 0,  Room_Num -> 0");
				}
			}
			else
			{
				global::UnityEngine.GameObject.Find("Info_Text").GetComponent<global::UnityEngine.UI.Text>().text = "Save Data Error!!!";
			}
			//fileStream.Close();
		}
		Check_Uncensored();
		if (flag)
		{
			global::UnityEngine.Debug.Log("Save Data Version : " + SaveData.version);
			Save_Game();
		}
	}

	public void Save_Game()
	{
		AxiPlayerPrefs.SetInt("Game_Saved", 1);
		SaveData.version = Version;
		global::System.Runtime.Serialization.Formatters.Binary.BinaryFormatter binaryFormatter = new global::System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
		//global::System.IO.FileStream fileStream = global::System.IO.File.Create("SaveData.dat");
		//binaryFormatter.Serialize(fileStream, SaveData);
		//fileStream.Close();

		using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
		{
			binaryFormatter.Serialize(ms, SaveData);
			AxiIO.AxiIO.io.file_WriteAllBytes(SaveDataFilePath, ms.ToArray());
		}
	}

	public void Delete_Data(int num, bool isSaved)
	{
		if (num >= 0 && num < 3)
		{
			SaveData.isSaved[num] = isSaved;
			SaveData.Room_Num[num] = 0;
			SaveData.Level[num] = 1;
			SaveData.HP[num] = 92;
			SaveData.HP_Max[num] = 92;
			SaveData.MP[num] = 60;
			SaveData.MP_Max[num] = 60;
			SaveData.onCloth[num] = true;
			SaveData.ATK[num] = 9;
			SaveData.DEF[num] = 8;
			SaveData.STR[num] = 12;
			SaveData.CON[num] = 18;
			SaveData.INT[num] = 16;
			SaveData.LCK[num] = 7;
			SaveData.ExpNow[num] = 0;
			SaveData.Kills[num] = 0;
			SaveData.Rate[num] = 0f;
			SaveData.PlayTime[num] = 0f;
			SaveData.StatPoints[num] = 0;
			SaveData.Weapon_Num[num] = 0;
			SaveData.Skill_Num[num] = 1;
			SaveData.onWeapon_1[num] = false;
			SaveData.onWeapon_2[num] = false;
			SaveData.onWeapon_3[num] = false;
			SaveData.onWeapon_4[num] = false;
			SaveData.onWeapon_5[num] = false;
			SaveData.onSkill_2[num] = false;
			SaveData.onSkill_3[num] = false;
			SaveData.onSkill_4[num] = false;
			SaveData.onSkill_5[num] = false;
			SaveData.onScrew[num] = false;
			SaveData.onHighJump[num] = false;
			SaveData.onSpeedUp[num] = false;
			SaveData.onDBJump[num] = false;
			SaveData.onBackDash[num] = false;
			SaveData.onCard_1[num] = false;
			SaveData.onCard_2[num] = false;
			SaveData.onCard_3[num] = false;
			SaveData.onCard_4[num] = false;
			SaveData.onCard_5[num] = false;
			SaveData.Bonus_HP[num] = 0;
			SaveData.Bonus_MP[num] = 0;
			SaveData.Bonus_ATK[num] = 0;
			SaveData.Bonus_Regen[num] = 0;
			SaveData.Bonus_Blood[num] = 0;
			SaveData.Bonus_Life[num] = 0;
			SaveData.ExtraInt_A[num] = 0;
			SaveData.ExtraInt_B[num] = 0;
			SaveData.ExtraInt_C[num] = 0;
			SaveData.ExtraFlo_A[num] = 0f;
			SaveData.ExtraFlo_B[num] = 0f;
			SaveData.ExtraFlo_C[num] = 0f;
			Reset_EventMap(num);
			Save_Game();
		}
	}

	private void Reset_EventMap(int num)
	{
		switch (num)
		{
			case 0:
				{
					for (int l = 0; l < 20; l++)
					{
						SaveData.E_scene_1[l] = 0;
					}
					for (int m = 0; m < 200; m++)
					{
						SaveData.Map_1[m] = 0;
					}
					for (int n = 0; n < 50; n++)
					{
						SaveData.BonusItems_1[n] = 0;
					}
					break;
				}
			case 1:
				{
					for (int num2 = 0; num2 < 20; num2++)
					{
						SaveData.E_scene_2[num2] = 0;
					}
					for (int num3 = 0; num3 < 200; num3++)
					{
						SaveData.Map_2[num3] = 0;
					}
					for (int num4 = 0; num4 < 50; num4++)
					{
						SaveData.BonusItems_2[num4] = 0;
					}
					break;
				}
			case 2:
				{
					for (int i = 0; i < 20; i++)
					{
						SaveData.E_scene_3[i] = 0;
					}
					for (int j = 0; j < 200; j++)
					{
						SaveData.Map_3[j] = 0;
					}
					for (int k = 0; k < 50; k++)
					{
						SaveData.BonusItems_3[k] = 0;
					}
					break;
				}
		}
	}

	public void Copy_Data(int copy_num, int paste_num)
	{
		if (copy_num < 0 || copy_num >= 3 || paste_num < 0 || paste_num >= 3 || !SaveData.isSaved[copy_num])
		{
			return;
		}
		SaveData.isSaved[paste_num] = SaveData.isSaved[copy_num];
		SaveData.Room_Num[paste_num] = SaveData.Room_Num[copy_num];
		SaveData.Level[paste_num] = SaveData.Level[copy_num];
		SaveData.HP[paste_num] = SaveData.HP[copy_num];
		SaveData.HP_Max[paste_num] = SaveData.HP_Max[copy_num];
		SaveData.MP[paste_num] = SaveData.MP[copy_num];
		SaveData.MP_Max[paste_num] = SaveData.MP_Max[copy_num];
		SaveData.onCloth[paste_num] = SaveData.onCloth[copy_num];
		SaveData.ATK[paste_num] = SaveData.ATK[copy_num];
		SaveData.DEF[paste_num] = SaveData.DEF[copy_num];
		SaveData.STR[paste_num] = SaveData.STR[copy_num];
		SaveData.CON[paste_num] = SaveData.CON[copy_num];
		SaveData.INT[paste_num] = SaveData.INT[copy_num];
		SaveData.LCK[paste_num] = SaveData.LCK[copy_num];
		SaveData.ExpNow[paste_num] = SaveData.ExpNow[copy_num];
		SaveData.Kills[paste_num] = SaveData.Kills[copy_num];
		SaveData.Rate[paste_num] = SaveData.Rate[copy_num];
		SaveData.PlayTime[paste_num] = SaveData.PlayTime[copy_num];
		SaveData.StatPoints[paste_num] = SaveData.StatPoints[copy_num];
		SaveData.Weapon_Num[paste_num] = SaveData.Weapon_Num[copy_num];
		SaveData.Skill_Num[paste_num] = SaveData.Skill_Num[copy_num];
		SaveData.onWeapon_1[paste_num] = SaveData.onWeapon_1[copy_num];
		SaveData.onWeapon_2[paste_num] = SaveData.onWeapon_2[copy_num];
		SaveData.onWeapon_3[paste_num] = SaveData.onWeapon_3[copy_num];
		SaveData.onWeapon_4[paste_num] = SaveData.onWeapon_4[copy_num];
		SaveData.onWeapon_5[paste_num] = SaveData.onWeapon_5[copy_num];
		SaveData.onSkill_2[paste_num] = SaveData.onSkill_2[copy_num];
		SaveData.onSkill_3[paste_num] = SaveData.onSkill_3[copy_num];
		SaveData.onSkill_4[paste_num] = SaveData.onSkill_4[copy_num];
		SaveData.onSkill_5[paste_num] = SaveData.onSkill_5[copy_num];
		SaveData.onScrew[paste_num] = SaveData.onScrew[copy_num];
		SaveData.onHighJump[paste_num] = SaveData.onHighJump[copy_num];
		SaveData.onSpeedUp[paste_num] = SaveData.onSpeedUp[copy_num];
		SaveData.onDBJump[paste_num] = SaveData.onDBJump[copy_num];
		SaveData.onBackDash[paste_num] = SaveData.onBackDash[copy_num];
		SaveData.onCard_1[paste_num] = SaveData.onCard_1[copy_num];
		SaveData.onCard_2[paste_num] = SaveData.onCard_2[copy_num];
		SaveData.onCard_3[paste_num] = SaveData.onCard_3[copy_num];
		SaveData.onCard_4[paste_num] = SaveData.onCard_4[copy_num];
		SaveData.onCard_5[paste_num] = SaveData.onCard_5[copy_num];
		SaveData.Bonus_HP[paste_num] = SaveData.Bonus_HP[copy_num];
		SaveData.Bonus_MP[paste_num] = SaveData.Bonus_MP[copy_num];
		SaveData.Bonus_ATK[paste_num] = SaveData.Bonus_ATK[copy_num];
		SaveData.Bonus_Regen[paste_num] = SaveData.Bonus_Regen[copy_num];
		SaveData.Bonus_Blood[paste_num] = SaveData.Bonus_Blood[copy_num];
		SaveData.Bonus_Life[paste_num] = SaveData.Bonus_Life[copy_num];
		SaveData.ExtraInt_A[paste_num] = SaveData.ExtraInt_A[copy_num];
		SaveData.ExtraInt_B[paste_num] = SaveData.ExtraInt_B[copy_num];
		SaveData.ExtraInt_C[paste_num] = SaveData.ExtraInt_C[copy_num];
		SaveData.ExtraFlo_A[paste_num] = SaveData.ExtraFlo_A[copy_num];
		SaveData.ExtraFlo_B[paste_num] = SaveData.ExtraFlo_B[copy_num];
		SaveData.ExtraFlo_C[paste_num] = SaveData.ExtraFlo_C[copy_num];
		switch (copy_num)
		{
			case 0:
				switch (paste_num)
				{
					case 1:
						global::System.Array.Copy(SaveData.E_scene_1, SaveData.E_scene_2, SaveData.E_scene_1.Length);
						global::System.Array.Copy(SaveData.Map_1, SaveData.Map_2, SaveData.Map_1.Length);
						global::System.Array.Copy(SaveData.BonusItems_1, SaveData.BonusItems_2, SaveData.BonusItems_1.Length);
						break;
					case 2:
						global::System.Array.Copy(SaveData.E_scene_1, SaveData.E_scene_3, SaveData.E_scene_1.Length);
						global::System.Array.Copy(SaveData.Map_1, SaveData.Map_3, SaveData.Map_1.Length);
						global::System.Array.Copy(SaveData.BonusItems_1, SaveData.BonusItems_3, SaveData.BonusItems_1.Length);
						break;
				}
				break;
			case 1:
				switch (paste_num)
				{
					case 0:
						global::System.Array.Copy(SaveData.E_scene_2, SaveData.E_scene_1, SaveData.E_scene_2.Length);
						global::System.Array.Copy(SaveData.Map_2, SaveData.Map_1, SaveData.Map_2.Length);
						global::System.Array.Copy(SaveData.BonusItems_2, SaveData.BonusItems_1, SaveData.BonusItems_2.Length);
						break;
					case 2:
						global::System.Array.Copy(SaveData.E_scene_2, SaveData.E_scene_3, SaveData.E_scene_2.Length);
						global::System.Array.Copy(SaveData.Map_2, SaveData.Map_3, SaveData.Map_2.Length);
						global::System.Array.Copy(SaveData.BonusItems_2, SaveData.BonusItems_3, SaveData.BonusItems_2.Length);
						break;
				}
				break;
			case 2:
				switch (paste_num)
				{
					case 0:
						global::System.Array.Copy(SaveData.E_scene_3, SaveData.E_scene_1, SaveData.E_scene_3.Length);
						global::System.Array.Copy(SaveData.Map_3, SaveData.Map_1, SaveData.Map_3.Length);
						global::System.Array.Copy(SaveData.BonusItems_3, SaveData.BonusItems_1, SaveData.BonusItems_3.Length);
						break;
					case 1:
						global::System.Array.Copy(SaveData.E_scene_3, SaveData.E_scene_2, SaveData.E_scene_3.Length);
						global::System.Array.Copy(SaveData.Map_3, SaveData.Map_2, SaveData.Map_3.Length);
						global::System.Array.Copy(SaveData.BonusItems_3, SaveData.BonusItems_2, SaveData.BonusItems_3.Length);
						break;
				}
				break;
		}
		Save_Game();
	}

	private void Make_Data()
	{
		SaveData.isSaved = new bool[3];
		SaveData.Room_Num = new int[3];
		SaveData.Level = new int[3] { 1, 1, 1 };
		SaveData.HP = new int[3] { 92, 92, 92 };
		SaveData.HP_Max = new int[3] { 92, 92, 92 };
		SaveData.MP = new int[3] { 60, 60, 60 };
		SaveData.MP_Max = new int[3] { 60, 60, 60 };
		SaveData.onCloth = new bool[3] { true, true, true };
		SaveData.ATK = new int[3] { 9, 9, 9 };
		SaveData.DEF = new int[3] { 8, 8, 8 };
		SaveData.STR = new int[3] { 12, 12, 12 };
		SaveData.CON = new int[3] { 18, 18, 18 };
		SaveData.INT = new int[3] { 16, 16, 16 };
		SaveData.LCK = new int[3] { 7, 7, 7 };
		SaveData.ExpNow = new int[3];
		SaveData.Kills = new int[3];
		SaveData.Rate = new float[3];
		SaveData.PlayTime = new float[3];
		SaveData.StatPoints = new int[3];
		SaveData.Weapon_Num = new int[3];
		SaveData.Skill_Num = new int[3] { 1, 1, 1 };
		SaveData.onWeapon_1 = new bool[3];
		SaveData.onWeapon_2 = new bool[3];
		SaveData.onWeapon_3 = new bool[3];
		SaveData.onWeapon_4 = new bool[3];
		SaveData.onWeapon_5 = new bool[3];
		SaveData.onSkill_2 = new bool[3];
		SaveData.onSkill_3 = new bool[3];
		SaveData.onSkill_4 = new bool[3];
		SaveData.onSkill_5 = new bool[3];
		SaveData.onScrew = new bool[3];
		SaveData.onHighJump = new bool[3];
		SaveData.onSpeedUp = new bool[3];
		SaveData.onDBJump = new bool[3];
		SaveData.onBackDash = new bool[3];
		SaveData.onCard_1 = new bool[3];
		SaveData.onCard_2 = new bool[3];
		SaveData.onCard_3 = new bool[3];
		SaveData.onCard_4 = new bool[3];
		SaveData.onCard_5 = new bool[3];
		SaveData.Bonus_HP = new int[3];
		SaveData.Bonus_MP = new int[3];
		SaveData.Bonus_ATK = new int[3];
		SaveData.Bonus_Regen = new int[3];
		SaveData.Bonus_Blood = new int[3];
		SaveData.Bonus_Life = new int[3];
		SaveData.ExtraInt_A = new int[3];
		SaveData.ExtraInt_B = new int[3];
		SaveData.ExtraInt_C = new int[3];
		SaveData.ExtraFlo_A = new float[3];
		SaveData.ExtraFlo_B = new float[3];
		SaveData.ExtraFlo_C = new float[3];
		SaveData.E_scene_1 = new int[20];
		SaveData.E_scene_2 = new int[20];
		SaveData.E_scene_3 = new int[20];
		Save_Control.PlayerData saveData = SaveData;
		int[] array = new int[200];
		array[0] = 1;
		saveData.Map_1 = array;
		Save_Control.PlayerData saveData2 = SaveData;
		int[] array2 = new int[200];
		array2[0] = 1;
		saveData2.Map_2 = array2;
		Save_Control.PlayerData saveData3 = SaveData;
		int[] array3 = new int[200];
		array3[0] = 1;
		saveData3.Map_3 = array3;
		Save_Control.PlayerData saveData4 = SaveData;
		int[] array4 = new int[50];
		array4[0] = 1;
		saveData4.BonusItems_1 = array4;
		Save_Control.PlayerData saveData5 = SaveData;
		int[] array5 = new int[50];
		array5[0] = 1;
		saveData5.BonusItems_2 = array5;
		Save_Control.PlayerData saveData6 = SaveData;
		int[] array6 = new int[50];
		array6[0] = 1;
		saveData6.BonusItems_3 = array6;
		SaveData.H_scene = new int[70];
		SaveData.H_Over = new int[20];
	}

	private void Text_Bonus_Item(Save_Control.PlayerData data)
	{
		string text = "\n";
		if (!(global::UnityEngine.GameObject.Find("Info_Text") != null))
		{
			return;
		}
		for (int i = 0; i < SaveData.H_scene.Length; i++)
		{
			string text2 = text;
			text = text2 + " " + data.H_scene[i] + ", ";
			if (i % 10 == 0)
			{
				text += "\n";
			}
		}
		global::UnityEngine.GameObject.Find("Info_Text").GetComponent<global::UnityEngine.UI.Text>().text += text;
	}

	private void Text_Event_Map(Save_Control.PlayerData data)
	{
		string empty = string.Empty;
		global::UnityEngine.GameObject.Find("Info_Text").GetComponent<global::UnityEngine.UI.Text>().text = string.Empty;
		if (global::UnityEngine.GameObject.Find("Info_Text") != null)
		{
			empty += " Event  Slot 1: ------------------- \n";
			for (int i = 0; i < 20; i++)
			{
				empty = empty + " " + data.E_scene_1[i];
			}
			empty += "\n\n\n";
			empty += " Event  Slot 2: ------------------- \n";
			for (int j = 0; j < 20; j++)
			{
				empty = empty + " " + data.E_scene_2[j];
			}
			empty += "\n\n\n";
			empty += " Event  Slot 3: ------------------- \n";
			for (int k = 0; k < 20; k++)
			{
				empty = empty + " " + data.E_scene_3[k];
			}
			empty += "\n\n\n\n\n";
			empty += " Map  Slot 1: ------------------- \n";
			for (int l = 0; l < 50; l++)
			{
				empty = empty + " " + data.Map_1[l];
			}
			empty += "\n";
			for (int m = 50; m < 100; m++)
			{
				empty = empty + " " + data.Map_1[m];
			}
			empty += "\n";
			for (int n = 100; n < 150; n++)
			{
				empty = empty + " " + data.Map_1[n];
			}
			empty += "\n";
			for (int num = 150; num < 200; num++)
			{
				empty = empty + " " + data.Map_1[num];
			}
			empty += "\n\n\n";
			empty += " Map  Slot 2: ------------------- \n";
			for (int num2 = 0; num2 < 50; num2++)
			{
				empty = empty + " " + data.Map_2[num2];
			}
			empty += "\n";
			for (int num3 = 50; num3 < 100; num3++)
			{
				empty = empty + " " + data.Map_2[num3];
			}
			empty += "\n";
			for (int num4 = 100; num4 < 150; num4++)
			{
				empty = empty + " " + data.Map_2[num4];
			}
			empty += "\n";
			for (int num5 = 150; num5 < 200; num5++)
			{
				empty = empty + " " + data.Map_2[num5];
			}
			empty += "\n\n\n";
			empty += " Map  Slot 3: ------------------- \n";
			for (int num6 = 0; num6 < 50; num6++)
			{
				empty = empty + " " + data.Map_3[num6];
			}
			empty += "\n";
			for (int num7 = 50; num7 < 100; num7++)
			{
				empty = empty + " " + data.Map_3[num7];
			}
			empty += "\n";
			for (int num8 = 100; num8 < 150; num8++)
			{
				empty = empty + " " + data.Map_3[num8];
			}
			empty += "\n";
			for (int num9 = 150; num9 < 200; num9++)
			{
				empty = empty + " " + data.Map_3[num9];
			}
			empty += "\n\n\n\n";
			empty += " H_scene: ------------------- \n";
			for (int num10 = 0; num10 < 40; num10++)
			{
				empty = empty + " " + data.H_scene[num10];
			}
			empty += "\n\n\n";
			empty += " H_Over: ------------------- \n";
			for (int num11 = 0; num11 < 20; num11++)
			{
				empty = empty + " " + data.H_Over[num11];
			}
			global::UnityEngine.GameObject.Find("Info_Text").GetComponent<global::UnityEngine.UI.Text>().text = empty;
		}
	}

	private void Text_Data(Save_Control.PlayerData data)
	{
		string empty = string.Empty;
		global::UnityEngine.GameObject.Find("Info_Text").GetComponent<global::UnityEngine.UI.Text>().text = string.Empty;
		if (global::UnityEngine.GameObject.Find("Info_Text") != null)
		{
			string text = empty;
			empty = text + " IsSaved \t:  [  " + data.isSaved[0] + ",  " + data.isSaved[1] + ",  " + data.isSaved[2] + "  ] \n";
			text = empty;
			empty = text + " Room_Num \t:  [  " + data.Room_Num[0] + ",  " + data.Room_Num[1] + ",  " + data.Room_Num[2] + "  ] \n";
			text = empty;
			empty = text + " Level \t:  [  " + data.Level[0] + ",  " + data.Level[1] + ",  " + data.Level[2] + "  ] \n";
			text = empty;
			empty = text + " HP \t:  [  " + data.HP[0] + ",  " + data.HP[1] + ",  " + data.HP[2] + "  ] \n";
			text = empty;
			empty = text + " HP_Max \t:  [  " + data.HP_Max[0] + ",  " + data.HP_Max[1] + ",  " + data.HP_Max[2] + "  ] \n";
			text = empty;
			empty = text + " MP \t:  [  " + data.MP[0] + ",  " + data.MP[1] + ",  " + data.MP[2] + "  ] \n";
			text = empty;
			empty = text + " MP_Max \t:  [  " + data.MP_Max[0] + ",  " + data.MP_Max[1] + ",  " + data.MP_Max[2] + "  ] \n";
			empty += "\n";
			text = empty;
			empty = text + " onCloth  :  [  " + data.onCloth[0] + ",  " + data.onCloth[1] + ",  " + data.onCloth[2] + "  ] \n";
			text = empty;
			empty = text + " ATK  :  [  " + data.ATK[0] + ",  " + data.ATK[1] + ",  " + data.ATK[2] + "  ] \n";
			text = empty;
			empty = text + " DEF  :  [  " + data.DEF[0] + ",  " + data.DEF[1] + ",  " + data.DEF[2] + "  ] \n";
			text = empty;
			empty = text + " STR  :  [  " + data.STR[0] + ",  " + data.STR[1] + ",  " + data.STR[2] + "  ] \n";
			text = empty;
			empty = text + " CON  :  [  " + data.CON[0] + ",  " + data.CON[1] + ",  " + data.CON[2] + "  ] \n";
			text = empty;
			empty = text + " INT  :  [  " + data.INT[0] + ",  " + data.INT[1] + ",  " + data.INT[2] + "  ] \n";
			text = empty;
			empty = text + " LCK  :  [  " + data.LCK[0] + ",  " + data.LCK[1] + ",  " + data.LCK[2] + "  ] \n";
			text = empty;
			empty = text + " ExpNow  :  [  " + data.ExpNow[0] + ",  " + data.ExpNow[1] + ",  " + data.ExpNow[2] + "  ] \n";
			text = empty;
			empty = text + " Kills  :  [  " + data.Kills[0] + ",  " + data.Kills[1] + ",  " + data.Kills[2] + "  ] \n";
			text = empty;
			empty = text + " Rate  :  [  " + data.Rate[0] + ",  " + data.Rate[1] + ",  " + data.Rate[2] + "  ] \n";
			text = empty;
			empty = text + " PlayTime  :  [  " + (int)data.PlayTime[0] + "sec,  " + (int)data.PlayTime[1] + "sec,  " + (int)data.PlayTime[2] + "sec  ] \n";
			empty += "\n";
			text = empty;
			empty = text + " StatPoints  :  [  " + data.StatPoints[0] + ",  " + data.StatPoints[1] + ",  " + data.StatPoints[2] + "  ] \n";
			text = empty;
			empty = text + " Weapon_Num  :  [  " + data.Weapon_Num[0] + ",  " + data.Weapon_Num[1] + ",  " + data.Weapon_Num[2] + "  ] \n";
			text = empty;
			empty = text + " Skill_Num  :  [  " + data.Skill_Num[0] + ",  " + data.Skill_Num[1] + ",  " + data.Skill_Num[2] + "  ] \n";
			text = empty;
			empty = text + " onWeapon_1  :  [  " + data.onWeapon_1[0] + ",  " + data.onWeapon_1[1] + ",  " + data.onWeapon_1[2] + "  ] \n";
			text = empty;
			empty = text + " onWeapon_2  :  [  " + data.onWeapon_2[0] + ",  " + data.onWeapon_2[1] + ",  " + data.onWeapon_2[2] + "  ] \n";
			text = empty;
			empty = text + " onWeapon_3  :  [  " + data.onWeapon_3[0] + ",  " + data.onWeapon_3[1] + ",  " + data.onWeapon_3[2] + "  ] \n";
			text = empty;
			empty = text + " onWeapon_4  :  [  " + data.onWeapon_4[0] + ",  " + data.onWeapon_4[1] + ",  " + data.onWeapon_4[2] + "  ] \n";
			text = empty;
			empty = text + " onWeapon_5  :  [  " + data.onWeapon_5[0] + ",  " + data.onWeapon_5[1] + ",  " + data.onWeapon_5[2] + "  ] \n";
			text = empty;
			empty = text + " onSkill_2  :  [  " + data.onSkill_2[0] + ",  " + data.onSkill_2[1] + ",  " + data.onSkill_2[2] + "  ] \n";
			text = empty;
			empty = text + " onSkill_3  :  [  " + data.onSkill_3[0] + ",  " + data.onSkill_3[1] + ",  " + data.onSkill_3[2] + "  ] \n";
			text = empty;
			empty = text + " onSkill_4  :  [  " + data.onSkill_4[0] + ",  " + data.onSkill_4[1] + ",  " + data.onSkill_4[2] + "  ] \n";
			text = empty;
			empty = text + " onSkill_5  :  [  " + data.onSkill_5[0] + ",  " + data.onSkill_5[1] + ",  " + data.onSkill_5[2] + "  ] \n";
			text = empty;
			empty = text + " onBackDash  :  [  " + data.onBackDash[0] + ",  " + data.onBackDash[1] + ",  " + data.onBackDash[2] + "  ] \n";
			text = empty;
			empty = text + " onDBJump  :  [  " + data.onDBJump[0] + ",  " + data.onDBJump[1] + ",  " + data.onDBJump[2] + "  ] \n";
			text = empty;
			empty = text + " onSpeedUp  :  [  " + data.onSpeedUp[0] + ",  " + data.onSpeedUp[1] + ",  " + data.onSpeedUp[2] + "  ] \n";
			text = empty;
			empty = text + " onHighJump  :  [  " + data.onHighJump[0] + ",  " + data.onHighJump[1] + ",  " + data.onHighJump[2] + "  ] \n";
			text = empty;
			empty = text + " onScrew  :  [  " + data.onScrew[0] + ",  " + data.onScrew[1] + ",  " + data.onScrew[2] + "  ] \n";
			text = empty;
			empty = text + " onCard_1  :  [  " + data.onCard_1[0] + ",  " + data.onCard_1[1] + ",  " + data.onCard_1[2] + "  ] \n";
			text = empty;
			empty = text + " onCard_2  :  [  " + data.onCard_2[0] + ",  " + data.onCard_2[1] + ",  " + data.onCard_2[2] + "  ] \n";
			text = empty;
			empty = text + " onCard_3  :  [  " + data.onCard_3[0] + ",  " + data.onCard_3[1] + ",  " + data.onCard_3[2] + "  ] \n";
			text = empty;
			empty = text + " onCard_4  :  [  " + data.onCard_4[0] + ",  " + data.onCard_4[1] + ",  " + data.onCard_4[2] + "  ] \n";
			text = empty;
			empty = text + " onCard_5  :  [  " + data.onCard_5[0] + ",  " + data.onCard_5[1] + ",  " + data.onCard_5[2] + "  ] \n";
			text = empty;
			empty = text + " Bonus_HP  :  [  " + data.Bonus_HP[0] + ",  " + data.Bonus_HP[1] + ",  " + data.Bonus_HP[2] + "  ] \n";
			text = empty;
			empty = text + " Bonus_MP  :  [  " + data.Bonus_MP[0] + ",  " + data.Bonus_MP[1] + ",  " + data.Bonus_MP[2] + "  ] \n";
			text = empty;
			empty = text + " Bonus_ATK  :  [  " + data.Bonus_ATK[0] + ",  " + data.Bonus_ATK[1] + ",  " + data.Bonus_ATK[2] + "  ] \n";
			text = empty;
			empty = text + " Bonus_Regen  :  [  " + data.Bonus_Regen[0] + ",  " + data.Bonus_Regen[1] + ",  " + data.Bonus_Regen[2] + "  ] \n";
			text = empty;
			empty = text + " Bonus_Blood  :  [  " + data.Bonus_Blood[0] + ",  " + data.Bonus_Blood[1] + ",  " + data.Bonus_Blood[2] + "  ] \n";
			global::UnityEngine.GameObject.Find("Info_Text").GetComponent<global::UnityEngine.UI.Text>().text = empty;
		}
	}
}
