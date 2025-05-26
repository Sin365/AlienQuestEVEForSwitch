[global::System.Serializable]
[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Camera))]
[global::UnityEngine.AddComponentMenu("Image Effects/Color Adjustments/Tonemapping")]
[global::UnityEngine.ExecuteInEditMode]
public class Tonemapping : PostEffectsBase
{
	[global::System.Serializable]
	public enum TonemapperType
	{
		SimpleReinhard = 0,
		UserCurve = 1,
		Hable = 2,
		Photographic = 3,
		OptimizedHejiDawson = 4,
		AdaptiveReinhard = 5,
		AdaptiveReinhardAutoWhite = 6
	}

	[global::System.Serializable]
	public enum AdaptiveTexSize
	{
		Square16 = 0x10,
		Square32 = 0x20,
		Square64 = 0x40,
		Square128 = 0x80,
		Square256 = 0x100,
		Square512 = 0x200,
		Square1024 = 0x400
	}

	public Tonemapping.TonemapperType type;

	public Tonemapping.AdaptiveTexSize adaptiveTextureSize;

	public global::UnityEngine.AnimationCurve remapCurve;

	private global::UnityEngine.Texture2D curveTex;

	public float exposureAdjustment;

	public float middleGrey;

	public float white;

	public float adaptionSpeed;

	public global::UnityEngine.Shader tonemapper;

	public bool validRenderTextureFormat;

	private global::UnityEngine.Material tonemapMaterial;

	private global::UnityEngine.RenderTexture rt;

	private global::UnityEngine.RenderTextureFormat rtFormat;

	public Tonemapping()
	{
		type = Tonemapping.TonemapperType.Photographic;
		adaptiveTextureSize = Tonemapping.AdaptiveTexSize.Square256;
		exposureAdjustment = 1.5f;
		middleGrey = 0.4f;
		white = 2f;
		adaptionSpeed = 1.5f;
		validRenderTextureFormat = true;
		rtFormat = global::UnityEngine.RenderTextureFormat.ARGBHalf;
	}

	public override bool CheckResources()
	{
		CheckSupport(false, true);
		tonemapMaterial = CheckShaderAndCreateMaterial(tonemapper, tonemapMaterial);
		if (!curveTex && type == Tonemapping.TonemapperType.UserCurve)
		{
			curveTex = new global::UnityEngine.Texture2D(256, 1, global::UnityEngine.TextureFormat.ARGB32, false, true);
			curveTex.filterMode = global::UnityEngine.FilterMode.Bilinear;
			curveTex.wrapMode = global::UnityEngine.TextureWrapMode.Clamp;
			curveTex.hideFlags = global::UnityEngine.HideFlags.DontSave;
		}
		if (!isSupported)
		{
			ReportAutoDisable();
		}
		return isSupported;
	}

	public virtual float UpdateCurve()
	{
		float num = 1f;
		if (global::UnityScript.Lang.Extensions.get_length((global::System.Array)remapCurve.keys) < 1)
		{
			remapCurve = new global::UnityEngine.AnimationCurve(new global::UnityEngine.Keyframe(0f, 0f), new global::UnityEngine.Keyframe(2f, 1f));
		}
		if (remapCurve != null)
		{
			if (remapCurve.length != 0)
			{
				num = remapCurve[remapCurve.length - 1].time;
			}
			for (float num2 = 0f; num2 <= 1f; num2 += 0.003921569f)
			{
				float num3 = remapCurve.Evaluate(num2 * 1f * num);
				curveTex.SetPixel((int)global::UnityEngine.Mathf.Floor(num2 * 255f), 0, new global::UnityEngine.Color(num3, num3, num3));
			}
			curveTex.Apply();
		}
		return 1f / num;
	}

	public virtual void OnDisable()
	{
		if ((bool)rt)
		{
			global::UnityEngine.Object.DestroyImmediate(rt);
			rt = null;
		}
		if ((bool)tonemapMaterial)
		{
			global::UnityEngine.Object.DestroyImmediate(tonemapMaterial);
			tonemapMaterial = null;
		}
		if ((bool)curveTex)
		{
			global::UnityEngine.Object.DestroyImmediate(curveTex);
			curveTex = null;
		}
	}

	public virtual bool CreateInternalRenderTexture()
	{
		int result;
		if ((bool)rt)
		{
			result = 0;
		}
		else
		{
			rtFormat = ((!global::UnityEngine.SystemInfo.SupportsRenderTextureFormat(global::UnityEngine.RenderTextureFormat.RGHalf)) ? global::UnityEngine.RenderTextureFormat.ARGBHalf : global::UnityEngine.RenderTextureFormat.RGHalf);
			rt = new global::UnityEngine.RenderTexture(1, 1, 0, rtFormat);
			rt.hideFlags = global::UnityEngine.HideFlags.DontSave;
			result = 1;
		}
		return (byte)result != 0;
	}

	[global::UnityEngine.ImageEffectTransformsToLDR]
	public virtual void OnRenderImage(global::UnityEngine.RenderTexture source, global::UnityEngine.RenderTexture destination)
	{
		if (!CheckResources())
		{
			global::UnityEngine.Graphics.Blit(source, destination);
			return;
		}
		exposureAdjustment = ((exposureAdjustment >= 0.001f) ? exposureAdjustment : 0.001f);
		if (type == Tonemapping.TonemapperType.UserCurve)
		{
			float value = UpdateCurve();
			tonemapMaterial.SetFloat("_RangeScale", value);
			tonemapMaterial.SetTexture("_Curve", curveTex);
			global::UnityEngine.Graphics.Blit(source, destination, tonemapMaterial, 4);
			return;
		}
		if (type == Tonemapping.TonemapperType.SimpleReinhard)
		{
			tonemapMaterial.SetFloat("_ExposureAdjustment", exposureAdjustment);
			global::UnityEngine.Graphics.Blit(source, destination, tonemapMaterial, 6);
			return;
		}
		if (type == Tonemapping.TonemapperType.Hable)
		{
			tonemapMaterial.SetFloat("_ExposureAdjustment", exposureAdjustment);
			global::UnityEngine.Graphics.Blit(source, destination, tonemapMaterial, 5);
			return;
		}
		if (type == Tonemapping.TonemapperType.Photographic)
		{
			tonemapMaterial.SetFloat("_ExposureAdjustment", exposureAdjustment);
			global::UnityEngine.Graphics.Blit(source, destination, tonemapMaterial, 8);
			return;
		}
		if (type == Tonemapping.TonemapperType.OptimizedHejiDawson)
		{
			tonemapMaterial.SetFloat("_ExposureAdjustment", 0.5f * exposureAdjustment);
			global::UnityEngine.Graphics.Blit(source, destination, tonemapMaterial, 7);
			return;
		}
		bool flag = CreateInternalRenderTexture();
		global::UnityEngine.RenderTexture temporary = global::UnityEngine.RenderTexture.GetTemporary((int)adaptiveTextureSize, (int)adaptiveTextureSize, 0, rtFormat);
		global::UnityEngine.Graphics.Blit(source, temporary);
		int num = (int)global::UnityEngine.Mathf.Log((float)temporary.width * 1f, 2f);
		int num2 = 2;
		global::UnityEngine.RenderTexture[] array = new global::UnityEngine.RenderTexture[num];
		for (int i = 0; i < num; i++)
		{
			array[i] = global::UnityEngine.RenderTexture.GetTemporary(temporary.width / num2, temporary.width / num2, 0, rtFormat);
			num2 *= 2;
		}
		float num3 = (float)source.width * 1f / ((float)source.height * 1f);
		global::UnityEngine.RenderTexture source2 = array[num - 1];
		global::UnityEngine.Graphics.Blit(temporary, array[0], tonemapMaterial, 1);
		if (type == Tonemapping.TonemapperType.AdaptiveReinhardAutoWhite)
		{
			for (int i = 0; i < num - 1; i++)
			{
				global::UnityEngine.Graphics.Blit(array[i], array[i + 1], tonemapMaterial, 9);
				source2 = array[i + 1];
			}
		}
		else if (type == Tonemapping.TonemapperType.AdaptiveReinhard)
		{
			for (int i = 0; i < num - 1; i++)
			{
				global::UnityEngine.Graphics.Blit(array[i], array[i + 1]);
				source2 = array[i + 1];
			}
		}
		adaptionSpeed = ((adaptionSpeed >= 0.001f) ? adaptionSpeed : 0.001f);
		tonemapMaterial.SetFloat("_AdaptionSpeed", adaptionSpeed);
		rt.MarkRestoreExpected();
		global::UnityEngine.Graphics.Blit(source2, rt, tonemapMaterial, (!flag) ? 2 : 3);
		middleGrey = ((middleGrey >= 0.001f) ? middleGrey : 0.001f);
		tonemapMaterial.SetVector("_HdrParams", new global::UnityEngine.Vector4(middleGrey, middleGrey, middleGrey, white * white));
		tonemapMaterial.SetTexture("_SmallTex", rt);
		if (type == Tonemapping.TonemapperType.AdaptiveReinhard)
		{
			global::UnityEngine.Graphics.Blit(source, destination, tonemapMaterial, 0);
		}
		else if (type == Tonemapping.TonemapperType.AdaptiveReinhardAutoWhite)
		{
			global::UnityEngine.Graphics.Blit(source, destination, tonemapMaterial, 10);
		}
		else
		{
			global::UnityEngine.Debug.LogError("No valid adaptive tonemapper type found!");
			global::UnityEngine.Graphics.Blit(source, destination);
		}
		for (int i = 0; i < num; i++)
		{
			global::UnityEngine.RenderTexture.ReleaseTemporary(array[i]);
		}
		global::UnityEngine.RenderTexture.ReleaseTemporary(temporary);
	}

	public override void Main()
	{
	}
}
