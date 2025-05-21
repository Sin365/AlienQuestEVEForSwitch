using UnityEngine;

public class Magic_Fire_4 : MonoBehaviour
{
	public enum Shield_Type
	{
		Player = 0,
		Moster = 1,
	}

	public Shield_Type Type;
	public GameObject Circle_Inner;
	public GameObject Circle_Outer;
	public GameObject[] Inner_Q;
	public GameObject[] Outer_Q;
	public GameObject Head;
	public GameObject soundShield;
}
