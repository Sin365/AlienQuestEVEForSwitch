public class Item_Blood_Absorb : global::UnityEngine.MonoBehaviour
{
	public Monster Mon;

	private int Blood_Num;

	private GameManager GM;

	private void Start()
	{
		GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	private void Update()
	{
		if (!GM.Paused && Mon == null)
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public void Get_Blood(int Num)
	{
		if (Mon.HP < Mon.HP_Max)
		{
			Mon.HP = ((Mon.HP + Num >= Mon.HP_Max) ? Mon.HP_Max : (Mon.HP += Num));
		}
		Blood_Num++;
	}

	private void OnTriggerStay2D(global::UnityEngine.Collider2D col)
	{
		if (!(Mon != null) || GM.Paused || GM.GameOver || !(col.tag == "Blood"))
		{
			return;
		}
		if (Mon == null)
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
		if (col.GetComponent<Item_Blood>().onChase_Absorb)
		{
			if (!(col.GetComponent<Item_Blood>().Absorb_Target == null) && col.GetComponent<Item_Blood>().Absorb_Target != base.gameObject)
			{
				float num = global::UnityEngine.Vector3.Distance(col.transform.position, base.transform.position);
				if (num < col.GetComponent<Item_Blood>().distance)
				{
					col.GetComponent<Item_Blood>().Absorb_Target = base.gameObject;
				}
			}
		}
		else if (col.GetComponent<Item_Blood>().onChase)
		{
			col.GetComponent<Item_Blood>().onChase_Absorb = true;
			col.GetComponent<Item_Blood>().Absorb_Target = base.gameObject;
		}
		else
		{
			col.GetComponent<Item_Blood>().onChase_Absorb = true;
			col.GetComponent<Item_Blood>().Absorb_Target = base.gameObject;
		}
	}
}
