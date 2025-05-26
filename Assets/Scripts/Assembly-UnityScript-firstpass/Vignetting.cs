[global::System.Serializable]
[global::UnityEngine.ExecuteInEditMode]
[global::UnityEngine.AddComponentMenu("Image Effects/Camera/Vignette and Chromatic Aberration")]
[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Camera))]
public class Vignetting : PostEffectsBase
{
	[global::System.Serializable]
	public enum AberrationMode
	{
		Simple = 0,
		Advanced = 1
	}

	public Vignetting.AberrationMode mode;

	public float intensity;

	public float chromaticAberration;

	public float axialAberration;

	public float blur;

	public float blurSpread;

	public float luminanceDependency;

	public float blurDistance;

	public global::UnityEngine.Shader vignetteShader;

	private global::UnityEngine.Material vignetteMaterial;

	public global::UnityEngine.Shader separableBlurShader;

	private global::UnityEngine.Material separableBlurMaterial;

	public global::UnityEngine.Shader chromAberrationShader;

	private global::UnityEngine.Material chromAberrationMaterial;

	public Vignetting()
	{
		mode = Vignetting.AberrationMode.Simple;
		intensity = 0.375f;
		chromaticAberration = 0.2f;
		axialAberration = 0.5f;
		blurSpread = 0.75f;
		luminanceDependency = 0.25f;
		blurDistance = 2.5f;
	}

	public override bool CheckResources()
	{
		CheckSupport(false);
		vignetteMaterial = CheckShaderAndCreateMaterial(vignetteShader, vignetteMaterial);
		separableBlurMaterial = CheckShaderAndCreateMaterial(separableBlurShader, separableBlurMaterial);
		chromAberrationMaterial = CheckShaderAndCreateMaterial(chromAberrationShader, chromAberrationMaterial);
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
		int width = source.width;
		int height = source.height;
		bool num = global::UnityEngine.Mathf.Abs(blur) > 0f;
		if (!num)
		{
			num = global::UnityEngine.Mathf.Abs(intensity) > 0f;
		}
		bool flag = num;
		float num2 = 1f * (float)width / (1f * (float)height);
		float num3 = 0.001953125f;
		global::UnityEngine.RenderTexture renderTexture = null;
		global::UnityEngine.RenderTexture renderTexture2 = null;
		global::UnityEngine.RenderTexture renderTexture3 = null;
		if (flag)
		{
			renderTexture = global::UnityEngine.RenderTexture.GetTemporary(width, height, 0, source.format);
			if (!(global::UnityEngine.Mathf.Abs(blur) <= 0f))
			{
				renderTexture2 = global::UnityEngine.RenderTexture.GetTemporary(width / 2, height / 2, 0, source.format);
				global::UnityEngine.Graphics.Blit(source, renderTexture2, chromAberrationMaterial, 0);
				for (int i = 0; i < 2; i++)
				{
					separableBlurMaterial.SetVector("offsets", new global::UnityEngine.Vector4(0f, blurSpread * num3, 0f, 0f));
					renderTexture3 = global::UnityEngine.RenderTexture.GetTemporary(width / 2, height / 2, 0, source.format);
					global::UnityEngine.Graphics.Blit(renderTexture2, renderTexture3, separableBlurMaterial);
					global::UnityEngine.RenderTexture.ReleaseTemporary(renderTexture2);
					separableBlurMaterial.SetVector("offsets", new global::UnityEngine.Vector4(blurSpread * num3 / num2, 0f, 0f, 0f));
					renderTexture2 = global::UnityEngine.RenderTexture.GetTemporary(width / 2, height / 2, 0, source.format);
					global::UnityEngine.Graphics.Blit(renderTexture3, renderTexture2, separableBlurMaterial);
					global::UnityEngine.RenderTexture.ReleaseTemporary(renderTexture3);
				}
			}
			vignetteMaterial.SetFloat("_Intensity", intensity);
			vignetteMaterial.SetFloat("_Blur", blur);
			vignetteMaterial.SetTexture("_VignetteTex", renderTexture2);
			global::UnityEngine.Graphics.Blit(source, renderTexture, vignetteMaterial, 0);
		}
		chromAberrationMaterial.SetFloat("_ChromaticAberration", chromaticAberration);
		chromAberrationMaterial.SetFloat("_AxialAberration", axialAberration);
		chromAberrationMaterial.SetVector("_BlurDistance", new global::UnityEngine.Vector2(0f - blurDistance, blurDistance));
		chromAberrationMaterial.SetFloat("_Luminance", 1f / global::UnityEngine.Mathf.Max(float.Epsilon, luminanceDependency));
		if (flag)
		{
			renderTexture.wrapMode = global::UnityEngine.TextureWrapMode.Clamp;
		}
		else
		{
			source.wrapMode = global::UnityEngine.TextureWrapMode.Clamp;
		}
		global::UnityEngine.Graphics.Blit((!flag) ? source : renderTexture, destination, chromAberrationMaterial, (mode != Vignetting.AberrationMode.Advanced) ? 1 : 2);
		global::UnityEngine.RenderTexture.ReleaseTemporary(renderTexture);
		global::UnityEngine.RenderTexture.ReleaseTemporary(renderTexture2);
	}

	public override void Main()
	{
	}
}
