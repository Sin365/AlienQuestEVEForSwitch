[global::System.Serializable]
[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Camera))]
[global::UnityEngine.ExecuteInEditMode]
[global::UnityEngine.AddComponentMenu("Image Effects/Camera/Depth of Field (deprecated)")]
public class DepthOfField34 : PostEffectsBase
{
	[global::System.NonSerialized]
	private static int SMOOTH_DOWNSAMPLE_PASS = 6;

	[global::System.NonSerialized]
	private static float BOKEH_EXTRA_BLUR = 2f;

	public Dof34QualitySetting quality;

	public DofResolution resolution;

	public bool simpleTweakMode;

	public float focalPoint;

	public float smoothness;

	public float focalZDistance;

	public float focalZStartCurve;

	public float focalZEndCurve;

	private float focalStartCurve;

	private float focalEndCurve;

	private float focalDistance01;

	public global::UnityEngine.Transform objectFocus;

	public float focalSize;

	public DofBlurriness bluriness;

	public float maxBlurSpread;

	public float foregroundBlurExtrude;

	public global::UnityEngine.Shader dofBlurShader;

	private global::UnityEngine.Material dofBlurMaterial;

	public global::UnityEngine.Shader dofShader;

	private global::UnityEngine.Material dofMaterial;

	public bool visualize;

	public BokehDestination bokehDestination;

	private float widthOverHeight;

	private float oneOverBaseSize;

	public bool bokeh;

	public bool bokehSupport;

	public global::UnityEngine.Shader bokehShader;

	public global::UnityEngine.Texture2D bokehTexture;

	public float bokehScale;

	public float bokehIntensity;

	public float bokehThreshholdContrast;

	public float bokehThreshholdLuminance;

	public int bokehDownsample;

	private global::UnityEngine.Material bokehMaterial;

	private global::UnityEngine.RenderTexture foregroundTexture;

	private global::UnityEngine.RenderTexture mediumRezWorkTexture;

	private global::UnityEngine.RenderTexture finalDefocus;

	private global::UnityEngine.RenderTexture lowRezWorkTexture;

	private global::UnityEngine.RenderTexture bokehSource;

	private global::UnityEngine.RenderTexture bokehSource2;

	public DepthOfField34()
	{
		quality = Dof34QualitySetting.OnlyBackground;
		resolution = DofResolution.Low;
		simpleTweakMode = true;
		focalPoint = 1f;
		smoothness = 0.5f;
		focalZStartCurve = 1f;
		focalZEndCurve = 1f;
		focalStartCurve = 2f;
		focalEndCurve = 2f;
		focalDistance01 = 0.1f;
		bluriness = DofBlurriness.High;
		maxBlurSpread = 1.75f;
		foregroundBlurExtrude = 1.15f;
		bokehDestination = BokehDestination.Background;
		widthOverHeight = 1.25f;
		oneOverBaseSize = 0.001953125f;
		bokehSupport = true;
		bokehScale = 2.4f;
		bokehIntensity = 0.15f;
		bokehThreshholdContrast = 0.1f;
		bokehThreshholdLuminance = 0.55f;
		bokehDownsample = 1;
	}

	public virtual void CreateMaterials()
	{
		dofBlurMaterial = CheckShaderAndCreateMaterial(dofBlurShader, dofBlurMaterial);
		dofMaterial = CheckShaderAndCreateMaterial(dofShader, dofMaterial);
		bokehSupport = bokehShader.isSupported;
		if (bokeh && bokehSupport && (bool)bokehShader)
		{
			bokehMaterial = CheckShaderAndCreateMaterial(bokehShader, bokehMaterial);
		}
	}

	public override bool CheckResources()
	{
		CheckSupport(true);
		dofBlurMaterial = CheckShaderAndCreateMaterial(dofBlurShader, dofBlurMaterial);
		dofMaterial = CheckShaderAndCreateMaterial(dofShader, dofMaterial);
		bokehSupport = bokehShader.isSupported;
		if (bokeh && bokehSupport && (bool)bokehShader)
		{
			bokehMaterial = CheckShaderAndCreateMaterial(bokehShader, bokehMaterial);
		}
		if (!isSupported)
		{
			ReportAutoDisable();
		}
		return isSupported;
	}

	public virtual void OnDisable()
	{
		Quads.Cleanup();
	}

	public override void OnEnable()
	{
		GetComponent<UnityEngine.Camera>().depthTextureMode |= global::UnityEngine.DepthTextureMode.Depth;
	}

	public virtual float FocalDistance01(float worldDist)
	{
		return GetComponent<UnityEngine.Camera>().WorldToViewportPoint((worldDist - GetComponent<UnityEngine.Camera>().nearClipPlane) * GetComponent<UnityEngine.Camera>().transform.forward + GetComponent<UnityEngine.Camera>().transform.position).z / (GetComponent<UnityEngine.Camera>().farClipPlane - GetComponent<UnityEngine.Camera>().nearClipPlane);
	}

	public virtual int GetDividerBasedOnQuality()
	{
		int result = 1;
		if (resolution == DofResolution.Medium)
		{
			result = 2;
		}
		else if (resolution == DofResolution.Low)
		{
			result = 2;
		}
		return result;
	}

	public virtual int GetLowResolutionDividerBasedOnQuality(int baseDivider)
	{
		int num = baseDivider;
		if (resolution == DofResolution.High)
		{
			num *= 2;
		}
		if (resolution == DofResolution.Low)
		{
			num *= 2;
		}
		return num;
	}

	public virtual void OnRenderImage(global::UnityEngine.RenderTexture source, global::UnityEngine.RenderTexture destination)
	{
		if (!CheckResources())
		{
			global::UnityEngine.Graphics.Blit(source, destination);
			return;
		}
		if (!(smoothness >= 0.1f))
		{
			smoothness = 0.1f;
		}
		bool num = bokeh;
		if (num)
		{
			num = bokehSupport;
		}
		bokeh = num;
		float num2 = ((!bokeh) ? 1f : BOKEH_EXTRA_BLUR);
		bool flag = quality > Dof34QualitySetting.OnlyBackground;
		float num3 = focalSize / (GetComponent<UnityEngine.Camera>().farClipPlane - GetComponent<UnityEngine.Camera>().nearClipPlane);
		if (simpleTweakMode)
		{
			focalDistance01 = ((!objectFocus) ? FocalDistance01(focalPoint) : (GetComponent<UnityEngine.Camera>().WorldToViewportPoint(objectFocus.position).z / GetComponent<UnityEngine.Camera>().farClipPlane));
			focalStartCurve = focalDistance01 * smoothness;
			focalEndCurve = focalStartCurve;
			bool num4 = flag;
			if (num4)
			{
				num4 = focalPoint > GetComponent<UnityEngine.Camera>().nearClipPlane + float.Epsilon;
			}
			flag = num4;
		}
		else
		{
			if ((bool)objectFocus)
			{
				global::UnityEngine.Vector3 vector = GetComponent<UnityEngine.Camera>().WorldToViewportPoint(objectFocus.position);
				vector.z /= GetComponent<UnityEngine.Camera>().farClipPlane;
				focalDistance01 = vector.z;
			}
			else
			{
				focalDistance01 = FocalDistance01(focalZDistance);
			}
			focalStartCurve = focalZStartCurve;
			focalEndCurve = focalZEndCurve;
			bool num5 = flag;
			if (num5)
			{
				num5 = focalPoint > GetComponent<UnityEngine.Camera>().nearClipPlane + float.Epsilon;
			}
			flag = num5;
		}
		widthOverHeight = 1f * (float)source.width / (1f * (float)source.height);
		oneOverBaseSize = 0.001953125f;
		dofMaterial.SetFloat("_ForegroundBlurExtrude", foregroundBlurExtrude);
		dofMaterial.SetVector("_CurveParams", new global::UnityEngine.Vector4((!simpleTweakMode) ? focalStartCurve : (1f / focalStartCurve), (!simpleTweakMode) ? focalEndCurve : (1f / focalEndCurve), num3 * 0.5f, focalDistance01));
		dofMaterial.SetVector("_InvRenderTargetSize", new global::UnityEngine.Vector4(1f / (1f * (float)source.width), 1f / (1f * (float)source.height), 0f, 0f));
		int dividerBasedOnQuality = GetDividerBasedOnQuality();
		int lowResolutionDividerBasedOnQuality = GetLowResolutionDividerBasedOnQuality(dividerBasedOnQuality);
		AllocateTextures(flag, source, dividerBasedOnQuality, lowResolutionDividerBasedOnQuality);
		global::UnityEngine.Graphics.Blit(source, source, dofMaterial, 3);
		Downsample(source, mediumRezWorkTexture);
		Blur(mediumRezWorkTexture, mediumRezWorkTexture, DofBlurriness.Low, 4, maxBlurSpread);
		if (bokeh && (bokehDestination & BokehDestination.Background) != 0)
		{
			dofMaterial.SetVector("_Threshhold", new global::UnityEngine.Vector4(bokehThreshholdContrast, bokehThreshholdLuminance, 0.95f, 0f));
			global::UnityEngine.Graphics.Blit(mediumRezWorkTexture, bokehSource2, dofMaterial, 11);
			global::UnityEngine.Graphics.Blit(mediumRezWorkTexture, lowRezWorkTexture);
			Blur(lowRezWorkTexture, lowRezWorkTexture, bluriness, 0, maxBlurSpread * num2);
		}
		else
		{
			Downsample(mediumRezWorkTexture, lowRezWorkTexture);
			Blur(lowRezWorkTexture, lowRezWorkTexture, bluriness, 0, maxBlurSpread);
		}
		dofBlurMaterial.SetTexture("_TapLow", lowRezWorkTexture);
		dofBlurMaterial.SetTexture("_TapMedium", mediumRezWorkTexture);
		global::UnityEngine.Graphics.Blit(null, finalDefocus, dofBlurMaterial, 3);
		if (bokeh && (bokehDestination & BokehDestination.Background) != 0)
		{
			AddBokeh(bokehSource2, bokehSource, finalDefocus);
		}
		dofMaterial.SetTexture("_TapLowBackground", finalDefocus);
		dofMaterial.SetTexture("_TapMedium", mediumRezWorkTexture);
		global::UnityEngine.Graphics.Blit(source, (!flag) ? destination : foregroundTexture, dofMaterial, visualize ? 2 : 0);
		if (flag)
		{
			global::UnityEngine.Graphics.Blit(foregroundTexture, source, dofMaterial, 5);
			Downsample(source, mediumRezWorkTexture);
			BlurFg(mediumRezWorkTexture, mediumRezWorkTexture, DofBlurriness.Low, 2, maxBlurSpread);
			if (bokeh && (bokehDestination & BokehDestination.Foreground) != 0)
			{
				dofMaterial.SetVector("_Threshhold", new global::UnityEngine.Vector4(bokehThreshholdContrast * 0.5f, bokehThreshholdLuminance, 0f, 0f));
				global::UnityEngine.Graphics.Blit(mediumRezWorkTexture, bokehSource2, dofMaterial, 11);
				global::UnityEngine.Graphics.Blit(mediumRezWorkTexture, lowRezWorkTexture);
				BlurFg(lowRezWorkTexture, lowRezWorkTexture, bluriness, 1, maxBlurSpread * num2);
			}
			else
			{
				BlurFg(mediumRezWorkTexture, lowRezWorkTexture, bluriness, 1, maxBlurSpread);
			}
			global::UnityEngine.Graphics.Blit(lowRezWorkTexture, finalDefocus);
			dofMaterial.SetTexture("_TapLowForeground", finalDefocus);
			global::UnityEngine.Graphics.Blit(source, destination, dofMaterial, visualize ? 1 : 4);
			if (bokeh && (bokehDestination & BokehDestination.Foreground) != 0)
			{
				AddBokeh(bokehSource2, bokehSource, destination);
			}
		}
		ReleaseTextures();
	}

	public virtual void Blur(global::UnityEngine.RenderTexture from, global::UnityEngine.RenderTexture to, DofBlurriness iterations, int blurPass, float spread)
	{
		global::UnityEngine.RenderTexture temporary = global::UnityEngine.RenderTexture.GetTemporary(to.width, to.height);
		if (iterations > DofBlurriness.Low)
		{
			BlurHex(from, to, blurPass, spread, temporary);
			if (iterations > DofBlurriness.High)
			{
				dofBlurMaterial.SetVector("offsets", new global::UnityEngine.Vector4(0f, spread * oneOverBaseSize, 0f, 0f));
				global::UnityEngine.Graphics.Blit(to, temporary, dofBlurMaterial, blurPass);
				dofBlurMaterial.SetVector("offsets", new global::UnityEngine.Vector4(spread / widthOverHeight * oneOverBaseSize, 0f, 0f, 0f));
				global::UnityEngine.Graphics.Blit(temporary, to, dofBlurMaterial, blurPass);
			}
		}
		else
		{
			dofBlurMaterial.SetVector("offsets", new global::UnityEngine.Vector4(0f, spread * oneOverBaseSize, 0f, 0f));
			global::UnityEngine.Graphics.Blit(from, temporary, dofBlurMaterial, blurPass);
			dofBlurMaterial.SetVector("offsets", new global::UnityEngine.Vector4(spread / widthOverHeight * oneOverBaseSize, 0f, 0f, 0f));
			global::UnityEngine.Graphics.Blit(temporary, to, dofBlurMaterial, blurPass);
		}
		global::UnityEngine.RenderTexture.ReleaseTemporary(temporary);
	}

	public virtual void BlurFg(global::UnityEngine.RenderTexture from, global::UnityEngine.RenderTexture to, DofBlurriness iterations, int blurPass, float spread)
	{
		dofBlurMaterial.SetTexture("_TapHigh", from);
		global::UnityEngine.RenderTexture temporary = global::UnityEngine.RenderTexture.GetTemporary(to.width, to.height);
		if (iterations > DofBlurriness.Low)
		{
			BlurHex(from, to, blurPass, spread, temporary);
			if (iterations > DofBlurriness.High)
			{
				dofBlurMaterial.SetVector("offsets", new global::UnityEngine.Vector4(0f, spread * oneOverBaseSize, 0f, 0f));
				global::UnityEngine.Graphics.Blit(to, temporary, dofBlurMaterial, blurPass);
				dofBlurMaterial.SetVector("offsets", new global::UnityEngine.Vector4(spread / widthOverHeight * oneOverBaseSize, 0f, 0f, 0f));
				global::UnityEngine.Graphics.Blit(temporary, to, dofBlurMaterial, blurPass);
			}
		}
		else
		{
			dofBlurMaterial.SetVector("offsets", new global::UnityEngine.Vector4(0f, spread * oneOverBaseSize, 0f, 0f));
			global::UnityEngine.Graphics.Blit(from, temporary, dofBlurMaterial, blurPass);
			dofBlurMaterial.SetVector("offsets", new global::UnityEngine.Vector4(spread / widthOverHeight * oneOverBaseSize, 0f, 0f, 0f));
			global::UnityEngine.Graphics.Blit(temporary, to, dofBlurMaterial, blurPass);
		}
		global::UnityEngine.RenderTexture.ReleaseTemporary(temporary);
	}

	public virtual void BlurHex(global::UnityEngine.RenderTexture from, global::UnityEngine.RenderTexture to, int blurPass, float spread, global::UnityEngine.RenderTexture tmp)
	{
		dofBlurMaterial.SetVector("offsets", new global::UnityEngine.Vector4(0f, spread * oneOverBaseSize, 0f, 0f));
		global::UnityEngine.Graphics.Blit(from, tmp, dofBlurMaterial, blurPass);
		dofBlurMaterial.SetVector("offsets", new global::UnityEngine.Vector4(spread / widthOverHeight * oneOverBaseSize, 0f, 0f, 0f));
		global::UnityEngine.Graphics.Blit(tmp, to, dofBlurMaterial, blurPass);
		dofBlurMaterial.SetVector("offsets", new global::UnityEngine.Vector4(spread / widthOverHeight * oneOverBaseSize, spread * oneOverBaseSize, 0f, 0f));
		global::UnityEngine.Graphics.Blit(to, tmp, dofBlurMaterial, blurPass);
		dofBlurMaterial.SetVector("offsets", new global::UnityEngine.Vector4(spread / widthOverHeight * oneOverBaseSize, (0f - spread) * oneOverBaseSize, 0f, 0f));
		global::UnityEngine.Graphics.Blit(tmp, to, dofBlurMaterial, blurPass);
	}

	public virtual void Downsample(global::UnityEngine.RenderTexture from, global::UnityEngine.RenderTexture to)
	{
		dofMaterial.SetVector("_InvRenderTargetSize", new global::UnityEngine.Vector4(1f / (1f * (float)to.width), 1f / (1f * (float)to.height), 0f, 0f));
		global::UnityEngine.Graphics.Blit(from, to, dofMaterial, SMOOTH_DOWNSAMPLE_PASS);
	}

	public virtual void AddBokeh(global::UnityEngine.RenderTexture bokehInfo, global::UnityEngine.RenderTexture tempTex, global::UnityEngine.RenderTexture finalTarget)
	{
		if (!bokehMaterial)
		{
			return;
		}
		global::UnityEngine.Mesh[] meshes = Quads.GetMeshes(tempTex.width, tempTex.height);
		global::UnityEngine.RenderTexture.active = tempTex;
		global::UnityEngine.GL.Clear(false, true, new global::UnityEngine.Color(0f, 0f, 0f, 0f));
		global::UnityEngine.GL.PushMatrix();
		global::UnityEngine.GL.LoadIdentity();
		bokehInfo.filterMode = global::UnityEngine.FilterMode.Point;
		float num = (float)bokehInfo.width * 1f / ((float)bokehInfo.height * 1f);
		float num2 = 2f / (1f * (float)bokehInfo.width);
		num2 += bokehScale * maxBlurSpread * BOKEH_EXTRA_BLUR * oneOverBaseSize;
		bokehMaterial.SetTexture("_Source", bokehInfo);
		bokehMaterial.SetTexture("_MainTex", bokehTexture);
		bokehMaterial.SetVector("_ArScale", new global::UnityEngine.Vector4(num2, num2 * num, 0.5f, 0.5f * num));
		bokehMaterial.SetFloat("_Intensity", bokehIntensity);
		bokehMaterial.SetPass(0);
		int i = 0;
		global::UnityEngine.Mesh[] array = meshes;
		for (int length = array.Length; i < length; i++)
		{
			if ((bool)array[i])
			{
				global::UnityEngine.Graphics.DrawMeshNow(array[i], global::UnityEngine.Matrix4x4.identity);
			}
		}
		global::UnityEngine.GL.PopMatrix();
		global::UnityEngine.Graphics.Blit(tempTex, finalTarget, dofMaterial, 8);
		bokehInfo.filterMode = global::UnityEngine.FilterMode.Bilinear;
	}

	public virtual void ReleaseTextures()
	{
		if ((bool)foregroundTexture)
		{
			global::UnityEngine.RenderTexture.ReleaseTemporary(foregroundTexture);
		}
		if ((bool)finalDefocus)
		{
			global::UnityEngine.RenderTexture.ReleaseTemporary(finalDefocus);
		}
		if ((bool)mediumRezWorkTexture)
		{
			global::UnityEngine.RenderTexture.ReleaseTemporary(mediumRezWorkTexture);
		}
		if ((bool)lowRezWorkTexture)
		{
			global::UnityEngine.RenderTexture.ReleaseTemporary(lowRezWorkTexture);
		}
		if ((bool)bokehSource)
		{
			global::UnityEngine.RenderTexture.ReleaseTemporary(bokehSource);
		}
		if ((bool)bokehSource2)
		{
			global::UnityEngine.RenderTexture.ReleaseTemporary(bokehSource2);
		}
	}

	public virtual void AllocateTextures(bool blurForeground, global::UnityEngine.RenderTexture source, int divider, int lowTexDivider)
	{
		foregroundTexture = null;
		if (blurForeground)
		{
			foregroundTexture = global::UnityEngine.RenderTexture.GetTemporary(source.width, source.height, 0);
		}
		mediumRezWorkTexture = global::UnityEngine.RenderTexture.GetTemporary(source.width / divider, source.height / divider, 0);
		finalDefocus = global::UnityEngine.RenderTexture.GetTemporary(source.width / divider, source.height / divider, 0);
		lowRezWorkTexture = global::UnityEngine.RenderTexture.GetTemporary(source.width / lowTexDivider, source.height / lowTexDivider, 0);
		bokehSource = null;
		bokehSource2 = null;
		if (bokeh)
		{
			bokehSource = global::UnityEngine.RenderTexture.GetTemporary(source.width / (lowTexDivider * bokehDownsample), source.height / (lowTexDivider * bokehDownsample), 0, global::UnityEngine.RenderTextureFormat.ARGBHalf);
			bokehSource2 = global::UnityEngine.RenderTexture.GetTemporary(source.width / (lowTexDivider * bokehDownsample), source.height / (lowTexDivider * bokehDownsample), 0, global::UnityEngine.RenderTextureFormat.ARGBHalf);
			bokehSource.filterMode = global::UnityEngine.FilterMode.Bilinear;
			bokehSource2.filterMode = global::UnityEngine.FilterMode.Bilinear;
			global::UnityEngine.RenderTexture.active = bokehSource2;
			global::UnityEngine.GL.Clear(false, true, new global::UnityEngine.Color(0f, 0f, 0f, 0f));
		}
		source.filterMode = global::UnityEngine.FilterMode.Bilinear;
		finalDefocus.filterMode = global::UnityEngine.FilterMode.Bilinear;
		mediumRezWorkTexture.filterMode = global::UnityEngine.FilterMode.Bilinear;
		lowRezWorkTexture.filterMode = global::UnityEngine.FilterMode.Bilinear;
		if ((bool)foregroundTexture)
		{
			foregroundTexture.filterMode = global::UnityEngine.FilterMode.Bilinear;
		}
	}

	public override void Main()
	{
	}
}
