[global::System.Serializable]
[global::UnityEngine.ExecuteInEditMode]
[global::UnityEngine.AddComponentMenu("Image Effects/Color Adjustments/Color Correction (3D Lookup Texture)")]
public class ColorCorrectionLut : PostEffectsBase
{
	public global::UnityEngine.Shader shader;

	private global::UnityEngine.Material material;

	public global::UnityEngine.Texture3D converted3DLut;

	public string basedOnTempTex;

	public ColorCorrectionLut()
	{
		basedOnTempTex = string.Empty;
	}

	public override bool CheckResources()
	{
		CheckSupport(false);
		material = CheckShaderAndCreateMaterial(shader, material);
		if (!isSupported || !global::UnityEngine.SystemInfo.supports3DTextures)
		{
			ReportAutoDisable();
		}
		return isSupported;
	}

	public virtual void OnDisable()
	{
		if ((bool)material)
		{
			global::UnityEngine.Object.DestroyImmediate(material);
			material = null;
		}
	}

	public virtual void OnDestroy()
	{
		if ((bool)converted3DLut)
		{
			global::UnityEngine.Object.DestroyImmediate(converted3DLut);
		}
		converted3DLut = null;
	}

	public virtual void SetIdentityLut()
	{
		int num = 16;
		global::UnityEngine.Color[] array = new global::UnityEngine.Color[num * num * num];
		float num2 = 1f / (1f * (float)num - 1f);
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num; j++)
			{
				for (int k = 0; k < num; k++)
				{
					array[i + j * num + k * num * num] = new global::UnityEngine.Color((float)i * 1f * num2, (float)j * 1f * num2, (float)k * 1f * num2, 1f);
				}
			}
		}
		if ((bool)converted3DLut)
		{
			global::UnityEngine.Object.DestroyImmediate(converted3DLut);
		}
		converted3DLut = new global::UnityEngine.Texture3D(num, num, num, global::UnityEngine.TextureFormat.ARGB32, false);
		converted3DLut.SetPixels(array);
		converted3DLut.Apply();
		basedOnTempTex = string.Empty;
	}

	public virtual bool ValidDimensions(global::UnityEngine.Texture2D tex2d)
	{
		int result;
		if (!tex2d)
		{
			result = 0;
		}
		else
		{
			int height = tex2d.height;
			result = ((height == global::UnityEngine.Mathf.FloorToInt(global::UnityEngine.Mathf.Sqrt(tex2d.width))) ? 1 : 0);
		}
		return (byte)result != 0;
	}

	public virtual void Convert(global::UnityEngine.Texture2D temp2DTex, string path)
	{
		if ((bool)temp2DTex)
		{
			int num = temp2DTex.width * temp2DTex.height;
			num = temp2DTex.height;
			if (!ValidDimensions(temp2DTex))
			{
				global::UnityEngine.Debug.LogWarning("The given 2D texture " + temp2DTex.name + " cannot be used as a 3D LUT.");
				basedOnTempTex = string.Empty;
				return;
			}
			global::UnityEngine.Color[] pixels = temp2DTex.GetPixels();
			global::UnityEngine.Color[] array = new global::UnityEngine.Color[pixels.Length];
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < num; j++)
				{
					for (int k = 0; k < num; k++)
					{
						int num2 = num - j - 1;
						array[i + j * num + k * num * num] = pixels[k * num + i + num2 * num * num];
					}
				}
			}
			if ((bool)converted3DLut)
			{
				global::UnityEngine.Object.DestroyImmediate(converted3DLut);
			}
			converted3DLut = new global::UnityEngine.Texture3D(num, num, num, global::UnityEngine.TextureFormat.ARGB32, false);
			converted3DLut.SetPixels(array);
			converted3DLut.Apply();
			basedOnTempTex = path;
		}
		else
		{
			global::UnityEngine.Debug.LogError("Couldn't color correct with 3D LUT texture. Image Effect will be disabled.");
		}
	}

	public virtual void OnRenderImage(global::UnityEngine.RenderTexture source, global::UnityEngine.RenderTexture destination)
	{
		if (!CheckResources() || !global::UnityEngine.SystemInfo.supports3DTextures)
		{
			global::UnityEngine.Graphics.Blit(source, destination);
			return;
		}
		if (converted3DLut == null)
		{
			SetIdentityLut();
		}
		int width = converted3DLut.width;
		converted3DLut.wrapMode = global::UnityEngine.TextureWrapMode.Clamp;
		material.SetFloat("_Scale", (float)(width - 1) / (1f * (float)width));
		material.SetFloat("_Offset", 1f / (2f * (float)width));
		material.SetTexture("_ClutTex", converted3DLut);
		global::UnityEngine.Graphics.Blit(source, destination, material, (global::UnityEngine.QualitySettings.activeColorSpace == global::UnityEngine.ColorSpace.Linear) ? 1 : 0);
	}

	public override void Main()
	{
	}
}
