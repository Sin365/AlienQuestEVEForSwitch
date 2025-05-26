[global::System.Serializable]
[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Camera))]
[global::UnityEngine.AddComponentMenu("Image Effects/Bloom and Glow/BloomAndFlares (3.5, Deprecated)")]
[global::UnityEngine.ExecuteInEditMode]
public class BloomAndLensFlares : PostEffectsBase
{
	public TweakMode34 tweakMode;

	public BloomScreenBlendMode screenBlendMode;

	public HDRBloomMode hdr;

	private bool doHdr;

	public float sepBlurSpread;

	public float useSrcAlphaAsMask;

	public float bloomIntensity;

	public float bloomThreshhold;

	public int bloomBlurIterations;

	public bool lensflares;

	public int hollywoodFlareBlurIterations;

	public LensflareStyle34 lensflareMode;

	public float hollyStretchWidth;

	public float lensflareIntensity;

	public float lensflareThreshhold;

	public global::UnityEngine.Color flareColorA;

	public global::UnityEngine.Color flareColorB;

	public global::UnityEngine.Color flareColorC;

	public global::UnityEngine.Color flareColorD;

	public float blurWidth;

	public global::UnityEngine.Texture2D lensFlareVignetteMask;

	public global::UnityEngine.Shader lensFlareShader;

	private global::UnityEngine.Material lensFlareMaterial;

	public global::UnityEngine.Shader vignetteShader;

	private global::UnityEngine.Material vignetteMaterial;

	public global::UnityEngine.Shader separableBlurShader;

	private global::UnityEngine.Material separableBlurMaterial;

	public global::UnityEngine.Shader addBrightStuffOneOneShader;

	private global::UnityEngine.Material addBrightStuffBlendOneOneMaterial;

	public global::UnityEngine.Shader screenBlendShader;

	private global::UnityEngine.Material screenBlend;

	public global::UnityEngine.Shader hollywoodFlaresShader;

	private global::UnityEngine.Material hollywoodFlaresMaterial;

	public global::UnityEngine.Shader brightPassFilterShader;

	private global::UnityEngine.Material brightPassFilterMaterial;

	public BloomAndLensFlares()
	{
		screenBlendMode = BloomScreenBlendMode.Add;
		hdr = HDRBloomMode.Auto;
		sepBlurSpread = 1.5f;
		useSrcAlphaAsMask = 0.5f;
		bloomIntensity = 1f;
		bloomThreshhold = 0.5f;
		bloomBlurIterations = 2;
		hollywoodFlareBlurIterations = 2;
		lensflareMode = LensflareStyle34.Anamorphic;
		hollyStretchWidth = 3.5f;
		lensflareIntensity = 1f;
		lensflareThreshhold = 0.3f;
		flareColorA = new global::UnityEngine.Color(0.4f, 0.4f, 0.8f, 0.75f);
		flareColorB = new global::UnityEngine.Color(0.4f, 0.8f, 0.8f, 0.75f);
		flareColorC = new global::UnityEngine.Color(0.8f, 0.4f, 0.8f, 0.75f);
		flareColorD = new global::UnityEngine.Color(0.8f, 0.4f, 0f, 0.75f);
		blurWidth = 1f;
	}

	public override bool CheckResources()
	{
		CheckSupport(false);
		screenBlend = CheckShaderAndCreateMaterial(screenBlendShader, screenBlend);
		lensFlareMaterial = CheckShaderAndCreateMaterial(lensFlareShader, lensFlareMaterial);
		vignetteMaterial = CheckShaderAndCreateMaterial(vignetteShader, vignetteMaterial);
		separableBlurMaterial = CheckShaderAndCreateMaterial(separableBlurShader, separableBlurMaterial);
		addBrightStuffBlendOneOneMaterial = CheckShaderAndCreateMaterial(addBrightStuffOneOneShader, addBrightStuffBlendOneOneMaterial);
		hollywoodFlaresMaterial = CheckShaderAndCreateMaterial(hollywoodFlaresShader, hollywoodFlaresMaterial);
		brightPassFilterMaterial = CheckShaderAndCreateMaterial(brightPassFilterShader, brightPassFilterMaterial);
		if (!isSupported)
		{
			ReportAutoDisable();
		}
		return isSupported;
	}

	public virtual void OnRenderImage(global::UnityEngine.RenderTexture source, global::UnityEngine.RenderTexture destination)
	{
		if (!CheckResources())
		{
			global::UnityEngine.Graphics.Blit(source, destination);
			return;
		}
		doHdr = false;
		if (hdr == HDRBloomMode.Auto)
		{
			bool num = source.format == global::UnityEngine.RenderTextureFormat.ARGBHalf;
			if (num)
			{
				num = GetComponent<UnityEngine.Camera>().allowHDR;
			}
			doHdr = num;
		}
		else
		{
			doHdr = hdr == HDRBloomMode.On;
		}
		bool num2 = doHdr;
		if (num2)
		{
			num2 = supportHDRTextures;
		}
		doHdr = num2;
		BloomScreenBlendMode pass = screenBlendMode;
		if (doHdr)
		{
			pass = BloomScreenBlendMode.Add;
		}
		global::UnityEngine.RenderTextureFormat format = ((!doHdr) ? global::UnityEngine.RenderTextureFormat.Default : global::UnityEngine.RenderTextureFormat.ARGBHalf);
		global::UnityEngine.RenderTexture temporary = global::UnityEngine.RenderTexture.GetTemporary(source.width / 2, source.height / 2, 0, format);
		global::UnityEngine.RenderTexture temporary2 = global::UnityEngine.RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0, format);
		global::UnityEngine.RenderTexture temporary3 = global::UnityEngine.RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0, format);
		global::UnityEngine.RenderTexture temporary4 = global::UnityEngine.RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0, format);
		float num3 = 1f * (float)source.width / (1f * (float)source.height);
		float num4 = 0.001953125f;
		global::UnityEngine.Graphics.Blit(source, temporary, screenBlend, 2);
		global::UnityEngine.Graphics.Blit(temporary, temporary2, screenBlend, 2);
		global::UnityEngine.RenderTexture.ReleaseTemporary(temporary);
		BrightFilter(bloomThreshhold, useSrcAlphaAsMask, temporary2, temporary3);
		temporary2.DiscardContents();
		if (bloomBlurIterations < 1)
		{
			bloomBlurIterations = 1;
		}
		for (int i = 0; i < bloomBlurIterations; i++)
		{
			float num5 = (1f + (float)i * 0.5f) * sepBlurSpread;
			separableBlurMaterial.SetVector("offsets", new global::UnityEngine.Vector4(0f, num5 * num4, 0f, 0f));
			global::UnityEngine.RenderTexture renderTexture = ((i != 0) ? temporary2 : temporary3);
			global::UnityEngine.Graphics.Blit(renderTexture, temporary4, separableBlurMaterial);
			renderTexture.DiscardContents();
			separableBlurMaterial.SetVector("offsets", new global::UnityEngine.Vector4(num5 / num3 * num4, 0f, 0f, 0f));
			global::UnityEngine.Graphics.Blit(temporary4, temporary2, separableBlurMaterial);
			temporary4.DiscardContents();
		}
		if (lensflares)
		{
			if (lensflareMode == LensflareStyle34.Ghosting)
			{
				BrightFilter(lensflareThreshhold, 0f, temporary2, temporary4);
				temporary2.DiscardContents();
				Vignette(0.975f, temporary4, temporary3);
				temporary4.DiscardContents();
				BlendFlares(temporary3, temporary2);
				temporary3.DiscardContents();
			}
			else
			{
				hollywoodFlaresMaterial.SetVector("_Threshhold", new global::UnityEngine.Vector4(lensflareThreshhold, 1f / (1f - lensflareThreshhold), 0f, 0f));
				hollywoodFlaresMaterial.SetVector("tintColor", new global::UnityEngine.Vector4(flareColorA.r, flareColorA.g, flareColorA.b, flareColorA.a) * flareColorA.a * lensflareIntensity);
				global::UnityEngine.Graphics.Blit(temporary4, temporary3, hollywoodFlaresMaterial, 2);
				temporary4.DiscardContents();
				global::UnityEngine.Graphics.Blit(temporary3, temporary4, hollywoodFlaresMaterial, 3);
				temporary3.DiscardContents();
				hollywoodFlaresMaterial.SetVector("offsets", new global::UnityEngine.Vector4(sepBlurSpread * 1f / num3 * num4, 0f, 0f, 0f));
				hollywoodFlaresMaterial.SetFloat("stretchWidth", hollyStretchWidth);
				global::UnityEngine.Graphics.Blit(temporary4, temporary3, hollywoodFlaresMaterial, 1);
				temporary4.DiscardContents();
				hollywoodFlaresMaterial.SetFloat("stretchWidth", hollyStretchWidth * 2f);
				global::UnityEngine.Graphics.Blit(temporary3, temporary4, hollywoodFlaresMaterial, 1);
				temporary3.DiscardContents();
				hollywoodFlaresMaterial.SetFloat("stretchWidth", hollyStretchWidth * 4f);
				global::UnityEngine.Graphics.Blit(temporary4, temporary3, hollywoodFlaresMaterial, 1);
				temporary4.DiscardContents();
				if (lensflareMode == LensflareStyle34.Anamorphic)
				{
					for (int j = 0; j < hollywoodFlareBlurIterations; j++)
					{
						separableBlurMaterial.SetVector("offsets", new global::UnityEngine.Vector4(hollyStretchWidth * 2f / num3 * num4, 0f, 0f, 0f));
						global::UnityEngine.Graphics.Blit(temporary3, temporary4, separableBlurMaterial);
						temporary3.DiscardContents();
						separableBlurMaterial.SetVector("offsets", new global::UnityEngine.Vector4(hollyStretchWidth * 2f / num3 * num4, 0f, 0f, 0f));
						global::UnityEngine.Graphics.Blit(temporary4, temporary3, separableBlurMaterial);
						temporary4.DiscardContents();
					}
					AddTo(1f, temporary3, temporary2);
					temporary3.DiscardContents();
				}
				else
				{
					for (int k = 0; k < hollywoodFlareBlurIterations; k++)
					{
						separableBlurMaterial.SetVector("offsets", new global::UnityEngine.Vector4(hollyStretchWidth * 2f / num3 * num4, 0f, 0f, 0f));
						global::UnityEngine.Graphics.Blit(temporary3, temporary4, separableBlurMaterial);
						temporary3.DiscardContents();
						separableBlurMaterial.SetVector("offsets", new global::UnityEngine.Vector4(hollyStretchWidth * 2f / num3 * num4, 0f, 0f, 0f));
						global::UnityEngine.Graphics.Blit(temporary4, temporary3, separableBlurMaterial);
						temporary4.DiscardContents();
					}
					Vignette(1f, temporary3, temporary4);
					temporary3.DiscardContents();
					BlendFlares(temporary4, temporary3);
					temporary4.DiscardContents();
					AddTo(1f, temporary3, temporary2);
					temporary3.DiscardContents();
				}
			}
		}
		screenBlend.SetFloat("_Intensity", bloomIntensity);
		screenBlend.SetTexture("_ColorBuffer", source);
		global::UnityEngine.Graphics.Blit(temporary2, destination, screenBlend, (int)pass);
		global::UnityEngine.RenderTexture.ReleaseTemporary(temporary2);
		global::UnityEngine.RenderTexture.ReleaseTemporary(temporary3);
		global::UnityEngine.RenderTexture.ReleaseTemporary(temporary4);
	}

	private void AddTo(float intensity_, global::UnityEngine.RenderTexture from, global::UnityEngine.RenderTexture to)
	{
		addBrightStuffBlendOneOneMaterial.SetFloat("_Intensity", intensity_);
		global::UnityEngine.Graphics.Blit(from, to, addBrightStuffBlendOneOneMaterial);
	}

	private void BlendFlares(global::UnityEngine.RenderTexture from, global::UnityEngine.RenderTexture to)
	{
		lensFlareMaterial.SetVector("colorA", new global::UnityEngine.Vector4(flareColorA.r, flareColorA.g, flareColorA.b, flareColorA.a) * lensflareIntensity);
		lensFlareMaterial.SetVector("colorB", new global::UnityEngine.Vector4(flareColorB.r, flareColorB.g, flareColorB.b, flareColorB.a) * lensflareIntensity);
		lensFlareMaterial.SetVector("colorC", new global::UnityEngine.Vector4(flareColorC.r, flareColorC.g, flareColorC.b, flareColorC.a) * lensflareIntensity);
		lensFlareMaterial.SetVector("colorD", new global::UnityEngine.Vector4(flareColorD.r, flareColorD.g, flareColorD.b, flareColorD.a) * lensflareIntensity);
		global::UnityEngine.Graphics.Blit(from, to, lensFlareMaterial);
	}

	private void BrightFilter(float thresh, float useAlphaAsMask, global::UnityEngine.RenderTexture from, global::UnityEngine.RenderTexture to)
	{
		if (doHdr)
		{
			brightPassFilterMaterial.SetVector("threshhold", new global::UnityEngine.Vector4(thresh, 1f, 0f, 0f));
		}
		else
		{
			brightPassFilterMaterial.SetVector("threshhold", new global::UnityEngine.Vector4(thresh, 1f / (1f - thresh), 0f, 0f));
		}
		brightPassFilterMaterial.SetFloat("useSrcAlphaAsMask", useAlphaAsMask);
		global::UnityEngine.Graphics.Blit(from, to, brightPassFilterMaterial);
	}

	private void Vignette(float amount, global::UnityEngine.RenderTexture from, global::UnityEngine.RenderTexture to)
	{
		if ((bool)lensFlareVignetteMask)
		{
			screenBlend.SetTexture("_ColorBuffer", lensFlareVignetteMask);
			global::UnityEngine.Graphics.Blit(from, to, screenBlend, 3);
		}
		else
		{
			vignetteMaterial.SetFloat("vignetteIntensity", amount);
			global::UnityEngine.Graphics.Blit(from, to, vignetteMaterial);
		}
	}

	public override void Main()
	{
	}
}
