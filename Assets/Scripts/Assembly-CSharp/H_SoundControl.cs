public class H_SoundControl : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject sound_Piston_4;

	public global::UnityEngine.GameObject sound_Piston_5;

	public global::UnityEngine.GameObject sound_Piston_9;

	public global::UnityEngine.GameObject sound_Piston_10;

	public global::UnityEngine.GameObject sound_Piston_11;

	public global::UnityEngine.GameObject sound_Piston_12;

	public global::UnityEngine.GameObject sound_FaceHugger;

	public global::UnityEngine.GameObject[] CumShot;

	public global::UnityEngine.GameObject[] CumDot;

	public global::UnityEngine.GameObject[] Moan;

	private float piston_Timer_4;

	private float piston_Timer_5;

	private float piston_Timer_9;

	private float piston_Timer_10;

	private float piston_Timer_11;

	private float piston_Timer_12;

	private float piston_Timer_FH;

	private float[] Moan_Timer;

	private global::UnityEngine.GameObject[] Sound_Moan_InGame;

	private void Start()
	{
		Moan_Timer = new float[Moan.Length];
		for (int i = 0; i < Moan_Timer.Length; i++)
		{
			Moan_Timer[i] = 0f;
		}
		Sound_Moan_InGame = new global::UnityEngine.GameObject[6];
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
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(CumShot[global::UnityEngine.Random.Range(0, 6)], Cum_Pos.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 90f));
		gameObject.GetComponent<H_CumDown>().pos_Target = Cum_Pos;
		gameObject.GetComponent<H_CumDown>().Set_SortingOrder(Cum_Index);
		gameObject.GetComponent<H_CumDown>().Set_DownDirect(Cum_Size);
	}

	public void Cum_DownDrool(global::UnityEngine.Transform Cum_Pos, int Cum_Index, float Cum_Size)
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(CumShot[global::UnityEngine.Random.Range(0, 6)], Cum_Pos.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 90f));
		gameObject.GetComponent<H_CumDown>().pos_Target = Cum_Pos;
		gameObject.GetComponent<H_CumDown>().Set_SortingOrder(Cum_Index);
		gameObject.GetComponent<H_CumDown>().Set_DownDrool(Cum_Size);
	}

	public void Cum_Pee(global::UnityEngine.Transform Cum_Pos, int Cum_Index, float Cum_Size)
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(CumShot[global::UnityEngine.Random.Range(0, 6)], Cum_Pos.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 90f));
		gameObject.GetComponent<H_CumDown>().pos_Target = Cum_Pos;
		gameObject.GetComponent<H_CumDown>().Set_SortingOrder(Cum_Index);
		gameObject.GetComponent<H_CumDown>().Set_Pee();
		for (int i = 0; i < 8; i++)
		{
			global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(CumDot[global::UnityEngine.Random.Range(1, 3)], Cum_Pos.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f));
		}
	}

	public void Cum_DownDirect_GO(global::UnityEngine.Transform Cum_Pos, int Cum_Index, float Cum_Size)
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(CumShot[global::UnityEngine.Random.Range(0, 6)], Cum_Pos.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 90f));
		gameObject.GetComponent<H_CumDown>().pos_Target = Cum_Pos;
		gameObject.GetComponent<H_CumDown>().Set_SortingOrder(Cum_Index);
		gameObject.GetComponent<H_CumDown>().Set_DownDirect(Cum_Size);
		gameObject.transform.localScale = new global::UnityEngine.Vector3(1f, 1f, 1f);
	}

	public void Cum_DownDrool_GO(global::UnityEngine.Transform Cum_Pos, int Cum_Index, float Cum_Size)
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(CumShot[global::UnityEngine.Random.Range(3, 6)], Cum_Pos.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 90f));
		gameObject.GetComponent<H_CumDown>().pos_Target = Cum_Pos;
		gameObject.GetComponent<H_CumDown>().Set_SortingOrder(Cum_Index);
		gameObject.GetComponent<H_CumDown>().Set_DownDrool(Cum_Size);
		gameObject.transform.localScale = new global::UnityEngine.Vector3(1f, 1f, 1f);
	}

	public void Cum_Pee_GO(global::UnityEngine.Transform Cum_Pos, int Cum_Index, float Cum_Size)
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(CumShot[global::UnityEngine.Random.Range(0, 6)], Cum_Pos.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 90f));
		gameObject.GetComponent<H_CumDown>().pos_Target = Cum_Pos;
		gameObject.GetComponent<H_CumDown>().Set_SortingOrder(Cum_Index);
		gameObject.GetComponent<H_CumDown>().Set_Pee();
		gameObject.transform.localScale = new global::UnityEngine.Vector3(1f, 1f, 1f);
		for (int i = 0; i < 8; i++)
		{
			global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(CumDot[global::UnityEngine.Random.Range(1, 3)], Cum_Pos.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f));
		}
	}

	public void Sound_Moan(int num, int slot_num)
	{
		if (Moan_Timer[num] <= 0f)
		{
			Moan_Timer[num] = 0.1f;
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Moan[num], base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
			if (Sound_Moan_InGame[slot_num] != null)
			{
				global::UnityEngine.Object.Destroy(Sound_Moan_InGame[slot_num].gameObject);
			}
			Sound_Moan_InGame[slot_num] = gameObject;
		}
	}
}
