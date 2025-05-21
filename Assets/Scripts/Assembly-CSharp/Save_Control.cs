using UnityEngine;
using System;

public class Save_Control : MonoBehaviour
{
	[Serializable]
	public class PlayerData
	{
		public float version;
		public bool test_data;
		public bool[] isSaved;
		public int[] Room_Num;
		public int[] Level;
		public int[] HP;
		public int[] HP_Max;
		public int[] MP;
		public int[] MP_Max;
		public bool[] onCloth;
		public int[] ATK;
		public int[] DEF;
		public int[] STR;
		public int[] CON;
		public int[] INT;
		public int[] LCK;
		public int[] ExpNow;
		public int[] Kills;
		public float[] Rate;
		public float[] PlayTime;
		public int[] StatPoints;
		public int[] Weapon_Num;
		public int[] Skill_Num;
		public bool[] onWeapon_1;
		public bool[] onWeapon_2;
		public bool[] onWeapon_3;
		public bool[] onWeapon_4;
		public bool[] onWeapon_5;
		public bool[] onSkill_2;
		public bool[] onSkill_3;
		public bool[] onSkill_4;
		public bool[] onSkill_5;
		public bool[] onScrew;
		public bool[] onHighJump;
		public bool[] onSpeedUp;
		public bool[] onDBJump;
		public bool[] onBackDash;
		public bool[] onCard_1;
		public bool[] onCard_2;
		public bool[] onCard_3;
		public bool[] onCard_4;
		public bool[] onCard_5;
		public int[] Bonus_HP;
		public int[] Bonus_MP;
		public int[] Bonus_ATK;
		public int[] Bonus_Regen;
		public int[] Bonus_Blood;
		public int[] E_scene_1;
		public int[] E_scene_2;
		public int[] E_scene_3;
		public int[] Map_1;
		public int[] Map_2;
		public int[] Map_3;
		public int[] BonusItems_1;
		public int[] BonusItems_2;
		public int[] BonusItems_3;
		public int[] Bonus_Life;
		public int[] ExtraInt_A;
		public int[] ExtraInt_B;
		public int[] ExtraInt_C;
		public float[] ExtraFlo_A;
		public float[] ExtraFlo_B;
		public float[] ExtraFlo_C;
		public int[] H_scene;
		public int[] H_Over;
	}

	public PlayerData SaveData;
}
