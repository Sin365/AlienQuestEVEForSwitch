[global::System.Serializable]
[global::UnityEngine.ExecuteInEditMode]
[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Camera))]
[global::UnityEngine.AddComponentMenu("Image Effects/Bloom and Glow/Bloom")]
public class Bloom : PostEffectsBase
{
	[global::System.Serializable]
	public enum LensFlareStyle
	{
		Ghosting = 0,
		Anamorphic = 1,
		Combined = 2
	}

	[global::System.Serializable]
	public enum TweakMode
	{
		Basic = 0,
		Complex = 1
	}

	[global::System.Serializable]
	public enum HDRBloomMode
	{
		Auto = 0,
		On = 1,
		Off = 2
	}

	[global::System.Serializable]
	public enum BloomScreenBlendMode
	{
		Screen = 0,
		Add = 1
	}

	[global::System.Serializable]
	public enum BloomQuality
	{
		Cheap = 0,
		High = 1
	}

	public Bloom.TweakMode tweakMode;

	public Bloom.BloomScreenBlendMode screenBlendMode;

	public Bloom.HDRBloomMode hdr;

	private bool doHdr;

	public float sepBlurSpread;

	public Bloom.BloomQuality quality;

	public float bloomIntensity;

	public float bloomThreshhold;

	public global::UnityEngine.Color bloomThreshholdColor;

	public int bloomBlurIterations;

	public int hollywoodFlareBlurIterations;

	public float flareRotation;

	public Bloom.LensFlareStyle lensflareMode;

	public float hollyStretchWidth;

	public float lensflareIntensity;

	public float lensflareThreshhold;

	public float lensFlareSaturation;

	public global::UnityEngine.Color flareColorA;

	public global::UnityEngine.Color flareColorB;

	public global::UnityEngine.Color flareColorC;

	public global::UnityEngine.Color flareColorD;

	public float blurWidth;

	public global::UnityEngine.Texture2D lensFlareVignetteMask;

	public global::UnityEngine.Shader lensFlareShader;

	private global::UnityEngine.Material lensFlareMaterial;

	public global::UnityEngine.Shader screenBlendShader;

	private global::UnityEngine.Material screenBlend;

	public global::UnityEngine.Shader blurAndFlaresShader;

	private global::UnityEngine.Material blurAndFlaresMaterial;

	public global::UnityEngine.Shader brightPassFilterShader;

	private global::UnityEngine.Material brightPassFilterMaterial;

	public Bloom()
	{
		screenBlendMode = Bloom.BloomScreenBlendMode.Add;
		hdr = Bloom.HDRBloomMode.Auto;
		sepBlurSpread = 2.5f;
		quality = Bloom.BloomQuality.High;
		bloomIntensity = 0.5f;
		bloomThreshhold = 0.5f;
		bloomThreshholdColor = global::UnityEngine.Color.white;
		bloomBlurIterations = 2;
		hollywoodFlareBlurIterations = 2;
		lensflareMode = Bloom.LensFlareStyle.Anamorphic;
		hollyStretchWidth = 2.5f;
		lensflareThreshhold = 0.3f;
		lensFlareSaturation = 0.75f;
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
		blurAndFlaresMaterial = CheckShaderAndCreateMaterial(blurAndFlaresShader, blurAndFlaresMaterial);
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
		if (hdr == Bloom.HDRBloomMode.Auto)
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
			doHdr = hdr == Bloom.HDRBloomMode.On;
		}
		bool num2 = doHdr;
		if (num2)
		{
			num2 = supportHDRTextures;
		}
		doHdr = num2;
		Bloom.BloomScreenBlendMode bloomScreenBlendMode = screenBlendMode;
		if (doHdr)
		{
			bloomScreenBlendMode = Bloom.BloomScreenBlendMode.Add;
		}
		global::UnityEngine.RenderTextureFormat format = ((!doHdr) ? global::UnityEngine.RenderTextureFormat.Default : global::UnityEngine.RenderTextureFormat.ARGBHalf);
		int width = source.width / 2;
		int height = source.height / 2;
		int width2 = source.width / 4;
		int height2 = source.height / 4;
		float num3 = 1f * (float)source.width / (1f * (float)source.height);
		float num4 = 0.001953125f;
		global::UnityEngine.RenderTexture temporary = global::UnityEngine.RenderTexture.GetTemporary(width2, height2, 0, format);
		global::UnityEngine.RenderTexture temporary2 = global::UnityEngine.RenderTexture.GetTemporary(width, height, 0, format);
		if (quality > Bloom.BloomQuality.Cheap)
		{
			global::UnityEngine.Graphics.Blit(source, temporary2, screenBlend, 2);
			global::UnityEngine.RenderTexture temporary3 = global::UnityEngine.RenderTexture.GetTemporary(width2, height2, 0, format);
			global::UnityEngine.Graphics.Blit(temporary2, temporary3, screenBlend, 2);
			global::UnityEngine.Graphics.Blit(temporary3, temporary, screenBlend, 6);
			global::UnityEngine.RenderTexture.ReleaseTemporary(temporary3);
		}
		else
		{
			global::UnityEngine.Graphics.Blit(source, temporary2);
			global::UnityEngine.Graphics.Blit(temporary2, temporary, screenBlend, 6);
		}
		global::UnityEngine.RenderTexture.ReleaseTemporary(temporary2);
		global::UnityEngine.RenderTexture renderTexture = global::UnityEngine.RenderTexture.GetTemporary(width2, height2, 0, format);
		BrightFilter(bloomThreshhold * bloomThreshholdColor, temporary, renderTexture);
		if (bloomBlurIterations < 1)
		{
			bloomBlurIterations = 1;
		}
		else if (bloomBlurIterations > 10)
		{
			bloomBlurIterations = 10;
		}
		for (int i = 0; i < bloomBlurIterations; i++)
		{
			float num5 = (1f + (float)i * 0.25f) * sepBlurSpread;
			global::UnityEngine.RenderTexture temporary4 = global::UnityEngine.RenderTexture.GetTemporary(width2, height2, 0, format);
			blurAndFlaresMaterial.SetVector("_Offsets", new global::UnityEngine.Vector4(0f, num5 * num4, 0f, 0f));
			global::UnityEngine.Graphics.Blit(renderTexture, temporary4, blurAndFlaresMaterial, 4);
			global::UnityEngine.RenderTexture.ReleaseTemporary(renderTexture);
			renderTexture = temporary4;
			temporary4 = global::UnityEngine.RenderTexture.GetTemporary(width2, height2, 0, format);
			blurAndFlaresMaterial.SetVector("_Offsets", new global::UnityEngine.Vector4(num5 / num3 * num4, 0f, 0f, 0f));
			global::UnityEngine.Graphics.Blit(renderTexture, temporary4, blurAndFlaresMaterial, 4);
			global::UnityEngine.RenderTexture.ReleaseTemporary(renderTexture);
			renderTexture = temporary4;
			if (quality > Bloom.BloomQuality.Cheap)
			{
				if (i == 0)
				{
					global::UnityEngine.Graphics.SetRenderTarget(temporary);
					global::UnityEngine.GL.Clear(false, true, global::UnityEngine.Color.black);
					global::UnityEngine.Graphics.Blit(renderTexture, temporary);
				}
				else
				{
					temporary.MarkRestoreExpected();
					global::UnityEngine.Graphics.Blit(renderTexture, temporary, screenBlend, 10);
				}
			}
		}
		if (quality > Bloom.BloomQuality.Cheap)
		{
			global::UnityEngine.Graphics.SetRenderTarget(renderTexture);
			global::UnityEngine.GL.Clear(false, true, global::UnityEngine.Color.black);
			global::UnityEngine.Graphics.Blit(temporary, renderTexture, screenBlend, 6);
		}
		if (!(lensflareIntensity <= float.Epsilon))
		{
			global::UnityEngine.RenderTexture temporary5 = global::UnityEngine.RenderTexture.GetTemporary(width2, height2, 0, format);
			if (lensflareMode == Bloom.LensFlareStyle.Ghosting)
			{
				BrightFilter(lensflareThreshhold, renderTexture, temporary5);
				if (quality > Bloom.BloomQuality.Cheap)
				{
					blurAndFlaresMaterial.SetVector("_Offsets", new global::UnityEngine.Vector4(0f, 1.5f / (1f * (float)temporary.height), 0f, 0f));
					global::UnityEngine.Graphics.SetRenderTarget(temporary);
					global::UnityEngine.GL.Clear(false, true, global::UnityEngine.Color.black);
					global::UnityEngine.Graphics.Blit(temporary5, temporary, blurAndFlaresMaterial, 4);
					blurAndFlaresMaterial.SetVector("_Offsets", new global::UnityEngine.Vector4(1.5f / (1f * (float)temporary.width), 0f, 0f, 0f));
					global::UnityEngine.Graphics.SetRenderTarget(temporary5);
					global::UnityEngine.GL.Clear(false, true, global::UnityEngine.Color.black);
					global::UnityEngine.Graphics.Blit(temporary, temporary5, blurAndFlaresMaterial, 4);
				}
				Vignette(0.975f, temporary5, temporary5);
				BlendFlares(temporary5, renderTexture);
			}
			else
			{
				float num6 = 1f * global::UnityEngine.Mathf.Cos(flareRotation);
				float num7 = 1f * global::UnityEngine.Mathf.Sin(flareRotation);
				float num8 = hollyStretchWidth * 1f / num3 * num4;
				float num9 = hollyStretchWidth * num4;
				blurAndFlaresMaterial.SetVector("_Offsets", new global::UnityEngine.Vector4(num6, num7, 0f, 0f));
				blurAndFlaresMaterial.SetVector("_Threshhold", new global::UnityEngine.Vector4(lensflareThreshhold, 1f, 0f, 0f));
				blurAndFlaresMaterial.SetVector("_TintColor", new global::UnityEngine.Vector4(flareColorA.r, flareColorA.g, flareColorA.b, flareColorA.a) * flareColorA.a * lensflareIntensity);
				blurAndFlaresMaterial.SetFloat("_Saturation", lensFlareSaturation);
				temporary.DiscardContents();
				global::UnityEngine.Graphics.Blit(temporary5, temporary, blurAndFlaresMaterial, 2);
				temporary5.DiscardContents();
				global::UnityEngine.Graphics.Blit(temporary, temporary5, blurAndFlaresMaterial, 3);
				blurAndFlaresMaterial.SetVector("_Offsets", new global::UnityEngine.Vector4(num6 * num8, num7 * num8, 0f, 0f));
				blurAndFlaresMaterial.SetFloat("_StretchWidth", hollyStretchWidth);
				temporary.DiscardContents();
				global::UnityEngine.Graphics.Blit(temporary5, temporary, blurAndFlaresMaterial, 1);
				blurAndFlaresMaterial.SetFloat("_StretchWidth", hollyStretchWidth * 2f);
				temporary5.DiscardContents();
				global::UnityEngine.Graphics.Blit(temporary, temporary5, blurAndFlaresMaterial, 1);
				blurAndFlaresMaterial.SetFloat("_StretchWidth", hollyStretchWidth * 4f);
				temporary.DiscardContents();
				global::UnityEngine.Graphics.Blit(temporary5, temporary, blurAndFlaresMaterial, 1);
				for (int i = 0; i < hollywoodFlareBlurIterations; i++)
				{
					num8 = hollyStretchWidth * 2f / num3 * num4;
					blurAndFlaresMaterial.SetVector("_Offsets", new global::UnityEngine.Vector4(num8 * num6, num8 * num7, 0f, 0f));
					temporary5.DiscardContents();
					global::UnityEngine.Graphics.Blit(temporary, temporary5, blurAndFlaresMaterial, 4);
					blurAndFlaresMaterial.SetVector("_Offsets", new global::UnityEngine.Vector4(num8 * num6, num8 * num7, 0f, 0f));
					temporary.DiscardContents();
					global::UnityEngine.Graphics.Blit(temporary5, temporary, blurAndFlaresMaterial, 4);
				}
				if (lensflareMode == Bloom.LensFlareStyle.Anamorphic)
				{
					AddTo(1f, temporary, renderTexture);
				}
				else
				{
					Vignette(1f, temporary, temporary5);
					BlendFlares(temporary5, temporary);
					AddTo(1f, temporary, renderTexture);
				}
			}
			global::UnityEngine.RenderTexture.ReleaseTemporary(temporary5);
		}
		int pass = (int)bloomScreenBlendMode;
		screenBlend.SetFloat("_Intensity", bloomIntensity);
		screenBlend.SetTexture("_ColorBuffer", source);
		if (quality > Bloom.BloomQuality.Cheap)
		{
			global::UnityEngine.RenderTexture temporary6 = global::UnityEngine.RenderTexture.GetTemporary(width, height, 0, format);
			global::UnityEngine.Graphics.Blit(renderTexture, temporary6);
			global::UnityEngine.Graphics.Blit(temporary6, destination, screenBlend, pass);
			global::UnityEngine.RenderTexture.ReleaseTemporary(temporary6);
		}
		else
		{
			global::UnityEngine.Graphics.Blit(renderTexture, destination, screenBlend, pass);
		}
		global::UnityEngine.RenderTexture.ReleaseTemporary(temporary);
		global::UnityEngine.RenderTexture.ReleaseTemporary(renderTexture);
	}

	private void AddTo(float intensity_, global::UnityEngine.RenderTexture from, global::UnityEngine.RenderTexture to)
	{
		screenBlend.SetFloat("_Intensity", intensity_);
		to.MarkRestoreExpected();
		global::UnityEngine.Graphics.Blit(from, to, screenBlend, 9);
	}

	private void BlendFlares(global::UnityEngine.RenderTexture from, global::UnityEngine.RenderTexture to)
	{
		lensFlareMaterial.SetVector("colorA", new global::UnityEngine.Vector4(flareColorA.r, flareColorA.g, flareColorA.b, flareColorA.a) * lensflareIntensity);
		lensFlareMaterial.SetVector("colorB", new global::UnityEngine.Vector4(flareColorB.r, flareColorB.g, flareColorB.b, flareColorB.a) * lensflareIntensity);
		lensFlareMaterial.SetVector("colorC", new global::UnityEngine.Vector4(flareColorC.r, flareColorC.g, flareColorC.b, flareColorC.a) * lensflareIntensity);
		lensFlareMaterial.SetVector("colorD", new global::UnityEngine.Vector4(flareColorD.r, flareColorD.g, flareColorD.b, flareColorD.a) * lensflareIntensity);
		to.MarkRestoreExpected();
		global::UnityEngine.Graphics.Blit(from, to, lensFlareMaterial);
	}

	private void BrightFilter(float thresh, global::UnityEngine.RenderTexture from, global::UnityEngine.RenderTexture to)
	{
		brightPassFilterMaterial.SetVector("_Threshhold", new global::UnityEngine.Vector4(thresh, thresh, thresh, thresh));
		global::UnityEngine.Graphics.Blit(from, to, brightPassFilterMaterial, 0);
	}

	private void BrightFilter(global::UnityEngine.Color threshColor, global::UnityEngine.RenderTexture from, global::UnityEngine.RenderTexture to)
	{
		brightPassFilterMaterial.SetVector("_Threshhold", threshColor);
		global::UnityEngine.Graphics.Blit(from, to, brightPassFilterMaterial, 1);
	}

	private void Vignette(float amount, global::UnityEngine.RenderTexture from, global::UnityEngine.RenderTexture to)
	{
		if ((bool)lensFlareVignetteMask)
		{
			screenBlend.SetTexture("_ColorBuffer", lensFlareVignetteMask);
			to.MarkRestoreExpected();
			global::UnityEngine.Graphics.Blit((!(from == to)) ? from : null, to, screenBlend, (!(from == to)) ? 3 : 7);
		}
		else if (from != to)
		{
			global::UnityEngine.Graphics.SetRenderTarget(to);
			global::UnityEngine.GL.Clear(false, true, global::UnityEngine.Color.black);
			global::UnityEngine.Graphics.Blit(from, to);
		}
	}

	public override void Main()
	{
	}
}
