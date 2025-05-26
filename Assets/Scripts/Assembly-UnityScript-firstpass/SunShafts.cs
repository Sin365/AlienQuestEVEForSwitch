[global::System.Serializable]
[global::UnityEngine.ExecuteInEditMode]
[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Camera))]
[global::UnityEngine.AddComponentMenu("Image Effects/Rendering/Sun Shafts")]
public class SunShafts : PostEffectsBase
{
	public SunShaftsResolution resolution;

	public ShaftsScreenBlendMode screenBlendMode;

	public global::UnityEngine.Transform sunTransform;

	public int radialBlurIterations;

	public global::UnityEngine.Color sunColor;

	public float sunShaftBlurRadius;

	public float sunShaftIntensity;

	public float useSkyBoxAlpha;

	public float maxRadius;

	public bool useDepthTexture;

	public global::UnityEngine.Shader sunShaftsShader;

	private global::UnityEngine.Material sunShaftsMaterial;

	public global::UnityEngine.Shader simpleClearShader;

	private global::UnityEngine.Material simpleClearMaterial;

	public SunShafts()
	{
		resolution = SunShaftsResolution.Normal;
		screenBlendMode = ShaftsScreenBlendMode.Screen;
		radialBlurIterations = 2;
		sunColor = global::UnityEngine.Color.white;
		sunShaftBlurRadius = 2.5f;
		sunShaftIntensity = 1.15f;
		useSkyBoxAlpha = 0.75f;
		maxRadius = 0.75f;
		useDepthTexture = true;
	}

	public override bool CheckResources()
	{
		CheckSupport(useDepthTexture);
		sunShaftsMaterial = CheckShaderAndCreateMaterial(sunShaftsShader, sunShaftsMaterial);
		simpleClearMaterial = CheckShaderAndCreateMaterial(simpleClearShader, simpleClearMaterial);
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
		if (useDepthTexture)
		{
			GetComponent<UnityEngine.Camera>().depthTextureMode |= global::UnityEngine.DepthTextureMode.Depth;
		}
		int num = 4;
		if (resolution == SunShaftsResolution.Normal)
		{
			num = 2;
		}
		else if (resolution == SunShaftsResolution.High)
		{
			num = 1;
		}
		global::UnityEngine.Vector3 vector = global::UnityEngine.Vector3.one * 0.5f;
		vector = ((!sunTransform) ? new global::UnityEngine.Vector3(0.5f, 0.5f, 0f) : GetComponent<UnityEngine.Camera>().WorldToViewportPoint(sunTransform.position));
		int width = source.width / num;
		int height = source.height / num;
		global::UnityEngine.RenderTexture renderTexture = null;
		global::UnityEngine.RenderTexture temporary = global::UnityEngine.RenderTexture.GetTemporary(width, height, 0);
		sunShaftsMaterial.SetVector("_BlurRadius4", new global::UnityEngine.Vector4(1f, 1f, 0f, 0f) * sunShaftBlurRadius);
		sunShaftsMaterial.SetVector("_SunPosition", new global::UnityEngine.Vector4(vector.x, vector.y, vector.z, maxRadius));
		sunShaftsMaterial.SetFloat("_NoSkyBoxMask", 1f - useSkyBoxAlpha);
		if (!useDepthTexture)
		{
			global::UnityEngine.RenderTexture renderTexture2 = (global::UnityEngine.RenderTexture.active = global::UnityEngine.RenderTexture.GetTemporary(source.width, source.height, 0));
			global::UnityEngine.GL.ClearWithSkybox(false, GetComponent<UnityEngine.Camera>());
			sunShaftsMaterial.SetTexture("_Skybox", renderTexture2);
			global::UnityEngine.Graphics.Blit(source, temporary, sunShaftsMaterial, 3);
			global::UnityEngine.RenderTexture.ReleaseTemporary(renderTexture2);
		}
		else
		{
			global::UnityEngine.Graphics.Blit(source, temporary, sunShaftsMaterial, 2);
		}
		DrawBorder(temporary, simpleClearMaterial);
		radialBlurIterations = global::UnityEngine.Mathf.Clamp(radialBlurIterations, 1, 4);
		float num2 = sunShaftBlurRadius * 0.0013020834f;
		sunShaftsMaterial.SetVector("_BlurRadius4", new global::UnityEngine.Vector4(num2, num2, 0f, 0f));
		sunShaftsMaterial.SetVector("_SunPosition", new global::UnityEngine.Vector4(vector.x, vector.y, vector.z, maxRadius));
		for (int i = 0; i < radialBlurIterations; i++)
		{
			renderTexture = global::UnityEngine.RenderTexture.GetTemporary(width, height, 0);
			global::UnityEngine.Graphics.Blit(temporary, renderTexture, sunShaftsMaterial, 1);
			global::UnityEngine.RenderTexture.ReleaseTemporary(temporary);
			num2 = sunShaftBlurRadius * (((float)i * 2f + 1f) * 6f) / 768f;
			sunShaftsMaterial.SetVector("_BlurRadius4", new global::UnityEngine.Vector4(num2, num2, 0f, 0f));
			temporary = global::UnityEngine.RenderTexture.GetTemporary(width, height, 0);
			global::UnityEngine.Graphics.Blit(renderTexture, temporary, sunShaftsMaterial, 1);
			global::UnityEngine.RenderTexture.ReleaseTemporary(renderTexture);
			num2 = sunShaftBlurRadius * (((float)i * 2f + 2f) * 6f) / 768f;
			sunShaftsMaterial.SetVector("_BlurRadius4", new global::UnityEngine.Vector4(num2, num2, 0f, 0f));
		}
		if (!(vector.z < 0f))
		{
			sunShaftsMaterial.SetVector("_SunColor", new global::UnityEngine.Vector4(sunColor.r, sunColor.g, sunColor.b, sunColor.a) * sunShaftIntensity);
		}
		else
		{
			sunShaftsMaterial.SetVector("_SunColor", global::UnityEngine.Vector4.zero);
		}
		sunShaftsMaterial.SetTexture("_ColorBuffer", temporary);
		global::UnityEngine.Graphics.Blit(source, destination, sunShaftsMaterial, (screenBlendMode != ShaftsScreenBlendMode.Screen) ? 4 : 0);
		global::UnityEngine.RenderTexture.ReleaseTemporary(temporary);
	}

	public override void Main()
	{
	}
}
