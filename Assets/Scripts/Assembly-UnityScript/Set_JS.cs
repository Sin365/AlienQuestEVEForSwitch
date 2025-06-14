using System;
using UnityEngine;

[Serializable]
public class Set_JS : MonoBehaviour
{
	public virtual void Set_Vignetting_16()
	{
		((Vignetting)GetComponent(typeof(Vignetting))).blur = 1.6f;
	}

	public virtual void Set_Vignetting_13()
	{
		((Vignetting)GetComponent(typeof(Vignetting))).blur = 1.3f;
	}

	public virtual void Set_Vignetting_10()
	{
		((Vignetting)GetComponent(typeof(Vignetting))).blur = 1f;
	}

	public virtual void Set_Vignetting_8()
	{
		((Vignetting)GetComponent(typeof(Vignetting))).blur = 0.8f;
	}

	public virtual void Set_Vignetting_Main()
	{
		((Vignetting)GetComponent(typeof(Vignetting))).blur = 0.65f;
	}

	public virtual void Set_Bloom_ON()
	{
		((Bloom)GetComponent(typeof(Bloom))).enabled = true;
		((Bloom)GetComponent(typeof(Bloom))).bloomThreshhold = 0.6f;
	}

	public virtual void Set_Bloom_OnOff()
	{
		if (((Bloom)GetComponent(typeof(Bloom))).enabled)
		{
			((Bloom)GetComponent(typeof(Bloom))).enabled = false;
		}
		else
		{
			((Bloom)GetComponent(typeof(Bloom))).enabled = true;
		}
	}

	public virtual void Set_Bloom_OFF()
	{
		((Bloom)GetComponent(typeof(Bloom))).enabled = false;
	}

	public virtual void Set_Bloom_ON_10()
	{
		((Bloom)GetComponent(typeof(Bloom))).enabled = true;
		((Bloom)GetComponent(typeof(Bloom))).bloomThreshhold = 0.1f;
	}

	public virtual void Set_Bloom_ON_20()
	{
		((Bloom)GetComponent(typeof(Bloom))).enabled = true;
		((Bloom)GetComponent(typeof(Bloom))).bloomThreshhold = 0.2f;
	}

	public virtual void Set_Bloom_ON_30()
	{
		((Bloom)GetComponent(typeof(Bloom))).enabled = true;
		((Bloom)GetComponent(typeof(Bloom))).bloomThreshhold = 0.3f;
	}

	public virtual void Set_Bloom_ON_40()
	{
		((Bloom)GetComponent(typeof(Bloom))).enabled = true;
		((Bloom)GetComponent(typeof(Bloom))).bloomThreshhold = 0.4f;
	}

	public virtual void Set_Bloom_ON_45()
	{
		((Bloom)GetComponent(typeof(Bloom))).enabled = true;
		((Bloom)GetComponent(typeof(Bloom))).bloomThreshhold = 0.45f;
	}

	public virtual void Set_Bloom_ON_50()
	{
		((Bloom)GetComponent(typeof(Bloom))).enabled = true;
		((Bloom)GetComponent(typeof(Bloom))).bloomThreshhold = 0.5f;
	}

	public virtual void Set_Bloom_ON_60()
	{
		((Bloom)GetComponent(typeof(Bloom))).enabled = true;
		((Bloom)GetComponent(typeof(Bloom))).bloomThreshhold = 0.6f;
	}

	public virtual void Main()
	{
	}
}
