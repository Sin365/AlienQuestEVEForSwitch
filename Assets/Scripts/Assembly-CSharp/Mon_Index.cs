public class Mon_Index : global::UnityEngine.MonoBehaviour
{
	public int Index;

	public int Step;

	public global::UnityEngine.GameObject[] Object_List;

	private global::UnityEngine.Color[] Color_List;

	private global::UnityEngine.Color color_Orig = new global::UnityEngine.Color(1f, 1f, 1f);

	private global::UnityEngine.Color color_Poison = new global::UnityEngine.Color(0.75f, 1f, 0f);

	private global::UnityEngine.Color color_Slow = new global::UnityEngine.Color(0f, 0.92f, 1f);

	private void Start()
	{
		if (Object_List.Length > 0)
		{
			Color_List = new global::UnityEngine.Color[Object_List.Length];
			for (int i = 0; i < Object_List.Length; i++)
			{
				Check_Skin(i);
			}
		}
	}

	private void Check_Skin(int num)
	{
		if (Object_List[num].GetComponent<global::UnityEngine.SpriteRenderer>() != null)
		{
			if (Step > 0)
			{
				Object_List[num].GetComponent<global::UnityEngine.SpriteRenderer>().sortingOrder -= 20 * Step;
			}
			Object_List[num].GetComponent<global::UnityEngine.SpriteRenderer>().sortingOrder += 20 * Index;
			Color_List[num] = Object_List[num].GetComponent<global::UnityEngine.SpriteRenderer>().color;
			return;
		}
		global::UnityEngine.SkinnedMeshRenderer component = Object_List[num].GetComponent<global::UnityEngine.SkinnedMeshRenderer>();
		if (Step > 0)
		{
			component.sortingOrder -= 20 * Step;
		}
		component.sortingOrder += 20 * Index;
		Color_List[num] = component.material.color;
	}

	public void On_MagicSlow()
	{
		if (Object_List.Length <= 0)
		{
			return;
		}
		for (int i = 0; i < Object_List.Length; i++)
		{
			if (Object_List[i].GetComponent<global::UnityEngine.SpriteRenderer>() != null)
			{
				if (Object_List[i].tag != "CensoredTag")
				{
					Object_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Slow;
				}
			}
			else
			{
				Object_List[i].GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material.color = color_Slow;
			}
		}
	}

	public void On_MagicPoison()
	{
		if (Object_List.Length <= 0)
		{
			return;
		}
		for (int i = 0; i < Object_List.Length; i++)
		{
			if (Object_List[i].GetComponent<global::UnityEngine.SpriteRenderer>() != null)
			{
				if (Object_List[i].tag != "CensoredTag")
				{
					Object_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Poison;
				}
			}
			else
			{
				Object_List[i].GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material.color = color_Poison;
			}
		}
	}

	public void Off_Magic()
	{
		if (Object_List.Length <= 0)
		{
			return;
		}
		for (int i = 0; i < Object_List.Length; i++)
		{
			if (Object_List[i].GetComponent<global::UnityEngine.SpriteRenderer>() != null)
			{
				if (Object_List[i].tag != "CensoredTag")
				{
					Object_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().color = Color_List[i];
				}
			}
			else
			{
				Object_List[i].GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material.color = Color_List[i];
			}
		}
	}

	public void OnOff_Object(bool isOnOff)
	{
		if (Object_List.Length <= 0)
		{
			return;
		}
		for (int i = 0; i < Object_List.Length; i++)
		{
			if (Object_List[i].GetComponent<global::UnityEngine.SpriteRenderer>() != null)
			{
				if (Object_List[i].tag == "CensoredTag")
				{
					if (global::UnityEngine.PlayerPrefs.GetInt("Censorship") == 1 && isOnOff)
					{
						Object_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().enabled = true;
					}
					Object_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().enabled = false;
				}
				else
				{
					Object_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().enabled = isOnOff;
				}
			}
			else
			{
				Object_List[i].GetComponent<global::UnityEngine.SkinnedMeshRenderer>().enabled = isOnOff;
			}
		}
	}

	public void Set_UserColor(global::UnityEngine.Color user_color)
	{
		if (Object_List.Length <= 0)
		{
			return;
		}
		for (int i = 0; i < Object_List.Length; i++)
		{
			if (Object_List[i].GetComponent<global::UnityEngine.SpriteRenderer>() != null)
			{
				if (Object_List[i].tag != "CensoredTag")
				{
					Object_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().color = user_color;
				}
			}
			else
			{
				Object_List[i].GetComponent<global::UnityEngine.SkinnedMeshRenderer>().material.color = user_color;
			}
		}
	}

	public void Xenomorph_Layer(int num)
	{
		if (Object_List.Length <= 0)
		{
			return;
		}
		for (int i = 0; i < Object_List.Length; i++)
		{
			if (Object_List[i].GetComponent<global::UnityEngine.SkinnedMeshRenderer>() != null)
			{
				Object_List[i].GetComponent<global::UnityEngine.SkinnedMeshRenderer>().sortingLayerID = num;
			}
			else
			{
				Object_List[i].GetComponent<global::UnityEngine.SpriteRenderer>().sortingLayerID = num;
			}
		}
	}
}
