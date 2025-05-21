using UnityEngine;

public class AI_Mon_Egg : MonoBehaviour
{
	public enum AniState
	{
		Normal = 0,
		Open = 1,
	}

	public AniState State;
	public int HP;
	public int HP_Max;
	public float HP_Ratio;
	public GameObject FaceHugger;
	public GameObject Explo;
	public Transform pos_Text;
	public Transform explo_Pos_1;
	public Transform explo_Pos_2;
	public Transform explo_Pos_3;
	public Transform pos_FaceHugger;
	public GameObject Blood_Obj;
	public int Index;
	public SkinnedMeshRenderer Egg_Body;
	public SkinnedMeshRenderer Egg_Wing_L;
	public SkinnedMeshRenderer Egg_Wing_R;
	public SpriteRenderer Egg_Cover;
}
