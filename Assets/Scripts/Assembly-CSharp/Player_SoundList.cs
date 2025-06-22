using UnityEngine;

public class Player_SoundList : global::UnityEngine.MonoBehaviour
{
	//public global::UnityEngine.GameObject Attack_1;

	//public global::UnityEngine.GameObject Attack_2;

	//public global::UnityEngine.GameObject Attack_3;

	//public global::UnityEngine.GameObject Jump;

	//public global::UnityEngine.GameObject Slide;

	//public global::UnityEngine.GameObject Spin;

	//public global::UnityEngine.GameObject Down;

	//public global::UnityEngine.GameObject FootStep;

	//public global::UnityEngine.GameObject voiceDamage_1;

	//public global::UnityEngine.GameObject voiceDamage_2;

	//public global::UnityEngine.GameObject voiceDamage_3;

	//public global::UnityEngine.GameObject voiceDamage_4;

	//public global::UnityEngine.GameObject voiceDeath_1;

	string Attack_1 = "prefabs/sound/Sound_Arm Whoosh 01";
	string Attack_2 = "prefabs/sound/Sound_Arm Whoosh 06";
	string Attack_3 = "prefabs/sound/Sound_Arm Whoosh 07";
	string Jump = "prefabs/sound/Sound_Jump_1";
	string Slide = "prefabs/sound/Sound_Slide";
	string Spin = "prefabs/sound/Sound_Hit_6_Punch 02";
	string Down = "prefabs/sound/Player_Down";
	string FootStep = "prefabs/sound/Footstep_1";
	string voiceDamage_1 = "prefabs/sound/voice/voice_Dmg_1";
	string voiceDamage_2 = "prefabs/sound/voice/voice_Dmg_2";
	string voiceDamage_3 = "prefabs/sound/voice/voice_Dmg_3";
	string voiceDamage_4 = "prefabs/sound/voice/voice_Dmg_4_Long";
	string voiceDeath_1 = "prefabs/sound/voice/voice_Death";



	private float Spin_Timer;

	private float Atk_Timer;

	private float Test_Timer;

	//string GetPrefabPath(UnityEngine.GameObject obj)
	//{
	//	return UnityEditor.AssetDatabase.GetAssetPath(obj);
	//}
	private void Start()
	{
		//string str = string.Empty;
		//str += $"string 	Attack_1	 = \"{GetPrefabPath(Attack_1)}\"\r\n";
		//str += $"string 	Attack_2	 = \"{GetPrefabPath(Attack_2)}\"\r\n";
		//str += $"string 	Attack_3	 = \"{GetPrefabPath(Attack_3)}\"\r\n";
		//str += $"string 	Jump	 = \"{GetPrefabPath(Jump)}\"\r\n";
		//str += $"string 	Slide	 = \"{GetPrefabPath(Slide)}\"\r\n";
		//str += $"string 	Spin	 = \"{GetPrefabPath(Spin)}\"\r\n";
		//str += $"string 	Down	 = \"{GetPrefabPath(Down)}\"\r\n";
		//str += $"string 	FootStep	 = \"{GetPrefabPath(FootStep)}\"\r\n";
		//str += $"string 	voiceDamage_1	 = \"{GetPrefabPath(voiceDamage_1)}\"\r\n";
		//str += $"string 	voiceDamage_2	 = \"{GetPrefabPath(voiceDamage_2)}\"\r\n";
		//str += $"string 	voiceDamage_3	 = \"{GetPrefabPath(voiceDamage_3)}\"\r\n";
		//str += $"string 	voiceDamage_4	 = \"{GetPrefabPath(voiceDamage_4)}\"\r\n";
		//str += $"string 	voiceDeath_1	 = \"{GetPrefabPath(voiceDeath_1)}\"\r\n";
		//Debug.Log("Player_SoundList=>\r\n" + str);
	}
	private void Update()
	{
		if (Spin_Timer > 0f)
		{
			Spin_Timer -= global::UnityEngine.Time.deltaTime;
		}
		if (Atk_Timer > 0f)
		{
			Atk_Timer -= global::UnityEngine.Time.deltaTime;
		}
	}

	private void Sound_Attack()
	{
		if (Atk_Timer <= 0f)
		{
			Atk_Timer = 0.05f;
			switch (global::UnityEngine.Random.Range(1, 4))
			{
				case 1:
					{
						//global::UnityEngine.GameObject gameObject3 = AxiObject.Instantiate(Attack_1, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
						AxiSoundPool.AddSoundForPosRot(Attack_1, base.transform.position, base.transform.rotation);
						break;
					}
				case 2:
					{
						//global::UnityEngine.GameObject gameObject2 = AxiObject.Instantiate(Attack_2, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
						AxiSoundPool.AddSoundForPosRot(Attack_2, base.transform.position, base.transform.rotation);
						break;
					}
				default:
					{
						//global::UnityEngine.GameObject gameObject = AxiObject.Instantiate(Attack_3, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
						AxiSoundPool.AddSoundForPosRot(Attack_3, base.transform.position, base.transform.rotation);

						break;
					}
			}
		}
	}

	private void Sound_Jump()
	{
		//global::UnityEngine.GameObject gameObject = AxiObject.Instantiate(Jump, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
		AxiSoundPool.AddSoundForPosRot(Jump, base.transform.position, base.transform.rotation);
	}

	private void Sound_Slide()
	{
		//global::UnityEngine.GameObject gameObject = AxiObject.Instantiate(Slide, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
		AxiSoundPool.AddSoundForPosRot(Slide, base.transform.position, base.transform.rotation);

	}

	private void Sound_Spin()
	{
		if (Spin_Timer <= 0f)
		{
			Spin_Timer = 0.05f;
			//global::UnityEngine.GameObject gameObject = AxiObject.Instantiate(Attack_3, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
			AxiSoundPool.AddSoundForPosRot(Attack_3, base.transform.position, base.transform.rotation);

		}
	}

	private void Sound_Down()
	{
		//global::UnityEngine.GameObject gameObject = AxiObject.Instantiate(Down, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
		AxiSoundPool.AddSoundForPosRot(Down, base.transform.position, base.transform.rotation);

	}

	private void Sound_FootStep()
	{
		//global::UnityEngine.GameObject gameObject = AxiObject.Instantiate(FootStep, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
		AxiSoundPool.AddSoundForPosRot(FootStep, base.transform.position, base.transform.rotation);
	}

	private void Voice_Damage()
	{
		switch (global::UnityEngine.Random.Range(1, 4))
		{
			case 1:
				{
					//global::UnityEngine.GameObject gameObject3 = AxiObject.Instantiate(voiceDamage_1, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
					AxiSoundPool.AddSoundForPosRot(voiceDamage_1, base.transform.position, base.transform.rotation);
					break;
				}
			case 2:
				{
					//global::UnityEngine.GameObject gameObject2 = AxiObject.Instantiate(voiceDamage_2, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
					AxiSoundPool.AddSoundForPosRot(voiceDamage_2, base.transform.position, base.transform.rotation);
					break;
				}
			default:
				{
					//global::UnityEngine.GameObject gameObject = AxiObject.Instantiate(voiceDamage_4, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
					AxiSoundPool.AddSoundForPosRot(voiceDamage_4, base.transform.position, base.transform.rotation);
					break;
				}
		}
	}

	private void Voice_Death()
	{
		//global::UnityEngine.GameObject gameObject = AxiObject.Instantiate(voiceDeath_1, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
		AxiSoundPool.AddSoundForPosRot(voiceDeath_1, base.transform.position, base.transform.rotation);
	}
}
