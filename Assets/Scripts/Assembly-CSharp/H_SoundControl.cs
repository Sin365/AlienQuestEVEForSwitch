using UnityEditor;
using UnityEngine;

public class H_SoundControl : global::UnityEngine.MonoBehaviour
{
	//public global::UnityEngine.GameObject sound_Piston_4;

	//public global::UnityEngine.GameObject sound_Piston_5;

	//public global::UnityEngine.GameObject sound_Piston_9;

	//public global::UnityEngine.GameObject sound_Piston_10;

	//public global::UnityEngine.GameObject sound_Piston_11;

	//public global::UnityEngine.GameObject sound_Piston_12;

	//public global::UnityEngine.GameObject sound_FaceHugger;

	//public global::UnityEngine.GameObject[] CumShot;

	//public global::UnityEngine.GameObject[] CumDot;

	//public global::UnityEngine.GameObject[] Moan;

	string sound_Piston_4 = "prefabs/sound/Sound_Piston_04";
	string sound_Piston_5 = "prefabs/sound/Sound_Piston_05";
	string sound_Piston_9 = "prefabs/sound/Sound_Piston_09";
	string sound_Piston_10 = "prefabs/sound/Sound_Piston_10";
	string sound_Piston_11 = "prefabs/sound/Sound_Piston_11";
	string sound_Piston_12 = "prefabs/sound/Sound_Piston_12";
	string sound_FaceHugger = "prefabs/sound/Sound_PistonFaceHugger";
	string[] CumShot = new string[6] { "prefabs/h_scene/H_CumDown_1", "prefabs/h_scene/H_CumDown_2", "prefabs/h_scene/H_CumDown_3", "prefabs/h_scene/H_CumDown_4", "prefabs/h_scene/H_CumDown_5", "prefabs/h_scene/H_CumDown_6" };
	string[] CumDot = new string[3] { "prefabs/h_scene/H_CumDot_1", "prefabs/h_scene/H_CumDot_2", "prefabs/h_scene/H_CumDot_3" };
	string[] Moan = new string[28] { "prefabs/sound/h_sound/Sound_Moan_2_1", "prefabs/sound/h_sound/Sound_Moan_2_1", "prefabs/sound/h_sound/Sound_Moan_2_2", "prefabs/sound/h_sound/Sound_Moan_2_3", "prefabs/sound/h_sound/Sound_Moan_2_4", "prefabs/sound/h_sound/Sound_Moan_2_5", "prefabs/sound/h_sound/Sound_Moan_2_6", "prefabs/sound/h_sound/Sound_Moan_2_7", "prefabs/sound/h_sound/Sound_Moan_2_8", "prefabs/sound/h_sound/Sound_Moan_2_9", "prefabs/sound/h_sound/Sound_Moan_2_10", "prefabs/sound/h_sound/Sound_Moan_2_11", "prefabs/sound/h_sound/Sound_Moan_2_12", "prefabs/sound/h_sound/Sound_Moan_2_13_Dmg", "prefabs/sound/h_sound/Sound_Moan_2_14_Dmg", "prefabs/sound/h_sound/Sound_Moan_2_15", "prefabs/sound/h_sound/Sound_Moan_2_16", "prefabs/sound/h_sound/Sound_Moan_2_17", "prefabs/sound/h_sound/Sound_Moan_2_18", "prefabs/sound/h_sound/Sound_Moan_2_19", "prefabs/sound/h_sound/Sound_Moan_2_20", "prefabs/sound/h_sound/Sound_Moan_2_21", "prefabs/sound/h_sound/Sound_Moan_1_1", "prefabs/sound/h_sound/Sound_Moan_1_2", "prefabs/sound/h_sound/Sound_Moan_1_3", "prefabs/sound/h_sound/Sound_Moan_1_4", "prefabs/sound/h_sound/Sound_Moan_1_5", "prefabs/sound/h_sound/Sound_Moan_1_End" };


	private float piston_Timer_4;

	private float piston_Timer_5;

	private float piston_Timer_9;

	private float piston_Timer_10;

	private float piston_Timer_11;

	private float piston_Timer_12;

	private float piston_Timer_FH;

	private float[] Moan_Timer;

	AxiSoundBase[] Sound_Moan_InGame;
	//string GetPrefabPath(GameObject obj)
	//{
	//	return AssetDatabase.GetAssetPath(obj);
	//}
	private void Start()
	{
		//string str = string.Empty;
		//str += $"string 	sound_Piston_4	 = \"{GetPrefabPath(sound_Piston_4)}\";\r\n";
		//str += $"string 	sound_Piston_5	 = \"{GetPrefabPath(sound_Piston_5)}\";\r\n";
		//str += $"string 	sound_Piston_9	 = \"{GetPrefabPath(sound_Piston_9)}\";\r\n";
		//str += $"string 	sound_Piston_10	 = \"{GetPrefabPath(sound_Piston_10)}\";\r\n";
		//str += $"string 	sound_Piston_11	 = \"{GetPrefabPath(sound_Piston_11)}\";\r\n";
		//str += $"string 	sound_Piston_12	 = \"{GetPrefabPath(sound_Piston_12)}\";\r\n";
		//str += $"string 	sound_FaceHugger	 = \"{GetPrefabPath(sound_FaceHugger)}\";\r\n";

		//str += "string[] CumShot = new string[6] { \"" + GetPrefabPath(CumShot[0])
		//	+ "\", \"" + GetPrefabPath(CumShot[1])
		//	+ "\", \"" + GetPrefabPath(CumShot[2])
		//	+ "\", \"" + GetPrefabPath(CumShot[3])
		//	+ "\", \"" + GetPrefabPath(CumShot[4])
		//	+ "\", \"" + GetPrefabPath(CumShot[5])
		//	+ "\" };\r\n";
		//str += "string[] CumDot = new string[3] { \"" + GetPrefabPath(CumDot[0])
		//	+ "\", \"" + GetPrefabPath(CumDot[1])
		//	+ "\", \"" + GetPrefabPath(CumDot[2])
		//	+ "\" };\r\n";
		//str += "string[] Moan = new string[28] { \"" + GetPrefabPath(Moan[0])
		//	+ "\", \"" + GetPrefabPath(Moan[1])
		//	+ "\", \"" + GetPrefabPath(Moan[2])
		//	+ "\", \"" + GetPrefabPath(Moan[3])
		//	+ "\", \"" + GetPrefabPath(Moan[4])
		//	+ "\", \"" + GetPrefabPath(Moan[5])
		//	+ "\", \"" + GetPrefabPath(Moan[6])
		//	+ "\", \"" + GetPrefabPath(Moan[7])
		//	+ "\", \"" + GetPrefabPath(Moan[8])
		//	+ "\", \"" + GetPrefabPath(Moan[9])
		//	+ "\", \"" + GetPrefabPath(Moan[10])
		//	+ "\", \"" + GetPrefabPath(Moan[11])
		//	+ "\", \"" + GetPrefabPath(Moan[12])
		//	+ "\", \"" + GetPrefabPath(Moan[13])
		//	+ "\", \"" + GetPrefabPath(Moan[14])
		//	+ "\", \"" + GetPrefabPath(Moan[15])
		//	+ "\", \"" + GetPrefabPath(Moan[16])
		//	+ "\", \"" + GetPrefabPath(Moan[17])
		//	+ "\", \"" + GetPrefabPath(Moan[18])
		//	+ "\", \"" + GetPrefabPath(Moan[19])
		//	+ "\", \"" + GetPrefabPath(Moan[20])
		//	+ "\", \"" + GetPrefabPath(Moan[21])
		//	+ "\", \"" + GetPrefabPath(Moan[22])
		//	+ "\", \"" + GetPrefabPath(Moan[23])
		//	+ "\", \"" + GetPrefabPath(Moan[24])
		//	+ "\", \"" + GetPrefabPath(Moan[25])
		//	+ "\", \"" + GetPrefabPath(Moan[26])
		//	+ "\", \"" + GetPrefabPath(Moan[27])
		//	+ "\" };\r\n";
		//Debug.Log("H_SoundControl=>\r\n" + str);

		Moan_Timer = new float[Moan.Length];
		for (int i = 0; i < Moan_Timer.Length; i++)
		{
			Moan_Timer[i] = 0f;
		}
		Sound_Moan_InGame = new AxiSoundBase[6];
	}

	private void Update()
	{
		if (piston_Timer_4 > 0f)
		{
			piston_Timer_4 -= global::UnityEngine.Time.deltaTime;
		}
		if (piston_Timer_5 > 0f)
		{
			piston_Timer_5 -= global::UnityEngine.Time.deltaTime;
		}
		if (piston_Timer_9 > 0f)
		{
			piston_Timer_9 -= global::UnityEngine.Time.deltaTime;
		}
		if (piston_Timer_10 > 0f)
		{
			piston_Timer_10 -= global::UnityEngine.Time.deltaTime;
		}
		if (piston_Timer_11 > 0f)
		{
			piston_Timer_11 -= global::UnityEngine.Time.deltaTime;
		}
		if (piston_Timer_12 > 0f)
		{
			piston_Timer_12 -= global::UnityEngine.Time.deltaTime;
		}
		if (piston_Timer_FH > 0f)
		{
			piston_Timer_FH -= global::UnityEngine.Time.deltaTime;
		}
		for (int i = 0; i < Moan_Timer.Length; i++)
		{
			if (Moan_Timer[i] > 0f)
			{
				Moan_Timer[i] -= global::UnityEngine.Time.deltaTime;
			}
		}
	}

	private void Sound_Piston_4()
	{
		if (piston_Timer_4 <= 0f)
		{
			piston_Timer_4 = 0.06f;
			AxiSoundPool.AddSoundForPosRot(sound_Piston_4, base.transform.position, base.transform.rotation);
		}
	}

	private void Sound_Piston_5()
	{
		if (piston_Timer_5 <= 0f)
		{
			piston_Timer_5 = 0.06f;
			AxiSoundPool.AddSoundForPosRot(sound_Piston_5, base.transform.position, base.transform.rotation);
		}
	}

	private void Sound_Piston_9()
	{
		if (piston_Timer_9 <= 0f)
		{
			piston_Timer_9 = 0.06f;
			AxiSoundPool.AddSoundForPosRot(sound_Piston_9, base.transform.position, base.transform.rotation);
		}
	}

	private void Sound_Piston_10()
	{
		if (piston_Timer_10 <= 0f)
		{
			piston_Timer_10 = 0.06f;
			AxiSoundPool.AddSoundForPosRot(sound_Piston_10, base.transform.position, base.transform.rotation);
		}
	}

	private void Sound_Piston_11()
	{
		if (piston_Timer_11 <= 0f)
		{
			piston_Timer_11 = 0.06f;
			AxiSoundPool.AddSoundForPosRot(sound_Piston_11, base.transform.position, base.transform.rotation);
		}
	}

	private void Sound_Piston_12()
	{
		if (piston_Timer_12 <= 0f)
		{
			piston_Timer_12 = 0.06f;
			AxiSoundPool.AddSoundForPosRot(sound_Piston_12, base.transform.position, base.transform.rotation);
		}
	}

	private void Sound_FaceHugger()
	{
		if (piston_Timer_FH <= 0f)
		{
			piston_Timer_FH = 0.06f;
			AxiSoundPool.AddSoundForPosRot(sound_FaceHugger, base.transform.position, base.transform.rotation);
		}
	}

	public void Cum_DownDirect(global::UnityEngine.Transform Cum_Pos, int Cum_Index, float Cum_Size)
	{
		global::UnityEngine.GameObject gameObject = AxiObject.Instantiate(CumShot[global::UnityEngine.Random.Range(0, 6)], Cum_Pos.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 90f));
		gameObject.GetComponent<H_CumDown>().pos_Target = Cum_Pos;
		gameObject.GetComponent<H_CumDown>().Set_SortingOrder(Cum_Index);
		gameObject.GetComponent<H_CumDown>().Set_DownDirect(Cum_Size);
	}

	public void Cum_DownDrool(global::UnityEngine.Transform Cum_Pos, int Cum_Index, float Cum_Size)
	{
		global::UnityEngine.GameObject gameObject = AxiObject.Instantiate(CumShot[global::UnityEngine.Random.Range(0, 6)], Cum_Pos.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 90f));
		gameObject.GetComponent<H_CumDown>().pos_Target = Cum_Pos;
		gameObject.GetComponent<H_CumDown>().Set_SortingOrder(Cum_Index);
		gameObject.GetComponent<H_CumDown>().Set_DownDrool(Cum_Size);
	}

	public void Cum_Pee(global::UnityEngine.Transform Cum_Pos, int Cum_Index, float Cum_Size)
	{
		global::UnityEngine.GameObject gameObject = AxiObject.Instantiate(CumShot[global::UnityEngine.Random.Range(0, 6)], Cum_Pos.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 90f));
		gameObject.GetComponent<H_CumDown>().pos_Target = Cum_Pos;
		gameObject.GetComponent<H_CumDown>().Set_SortingOrder(Cum_Index);
		gameObject.GetComponent<H_CumDown>().Set_Pee();
		for (int i = 0; i < 8; i++)
		{
			global::UnityEngine.GameObject gameObject2 = AxiObject.Instantiate(CumDot[global::UnityEngine.Random.Range(1, 3)], Cum_Pos.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f));
		}
	}

	public void Cum_DownDirect_GO(global::UnityEngine.Transform Cum_Pos, int Cum_Index, float Cum_Size)
	{
		global::UnityEngine.GameObject gameObject = AxiObject.Instantiate(CumShot[global::UnityEngine.Random.Range(0, 6)], Cum_Pos.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 90f));
		gameObject.GetComponent<H_CumDown>().pos_Target = Cum_Pos;
		gameObject.GetComponent<H_CumDown>().Set_SortingOrder(Cum_Index);
		gameObject.GetComponent<H_CumDown>().Set_DownDirect(Cum_Size);
		gameObject.transform.localScale = new global::UnityEngine.Vector3(1f, 1f, 1f);
	}

	public void Cum_DownDrool_GO(global::UnityEngine.Transform Cum_Pos, int Cum_Index, float Cum_Size)
	{
		global::UnityEngine.GameObject gameObject = AxiObject.Instantiate(CumShot[global::UnityEngine.Random.Range(3, 6)], Cum_Pos.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 90f));
		gameObject.GetComponent<H_CumDown>().pos_Target = Cum_Pos;
		gameObject.GetComponent<H_CumDown>().Set_SortingOrder(Cum_Index);
		gameObject.GetComponent<H_CumDown>().Set_DownDrool(Cum_Size);
		gameObject.transform.localScale = new global::UnityEngine.Vector3(1f, 1f, 1f);
	}

	public void Cum_Pee_GO(global::UnityEngine.Transform Cum_Pos, int Cum_Index, float Cum_Size)
	{
		global::UnityEngine.GameObject gameObject = AxiObject.Instantiate(CumShot[global::UnityEngine.Random.Range(0, 6)], Cum_Pos.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 90f));
		gameObject.GetComponent<H_CumDown>().pos_Target = Cum_Pos;
		gameObject.GetComponent<H_CumDown>().Set_SortingOrder(Cum_Index);
		gameObject.GetComponent<H_CumDown>().Set_Pee();
		gameObject.transform.localScale = new global::UnityEngine.Vector3(1f, 1f, 1f);
		for (int i = 0; i < 8; i++)
		{
			global::UnityEngine.GameObject gameObject2 = AxiObject.Instantiate(CumDot[global::UnityEngine.Random.Range(1, 3)], Cum_Pos.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f));
		}
	}

	public void Sound_Moan(int num, int slot_num)
	{
		if (Moan_Timer[num] <= 0f)
		{
			Moan_Timer[num] = 0.1f;
			AxiSoundBase gameObject = AxiSoundPool.AddSoundForPosRot(Moan[num], base.transform.position, base.transform.rotation);
			if (Sound_Moan_InGame[slot_num] != null)
			{
				AxiSoundPool.CheckNeedRemoveFormPool(Sound_Moan_InGame[slot_num]);
				//global::UnityEngine.Object.Destroy(Sound_Moan_InGame[slot_num].gameObject);
			}
			Sound_Moan_InGame[slot_num] = gameObject;
		}
	}
}
