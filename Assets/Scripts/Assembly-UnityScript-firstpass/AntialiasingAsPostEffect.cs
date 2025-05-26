[global::System.Serializable]
[global::UnityEngine.ExecuteInEditMode]
[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Camera))]
[global::UnityEngine.AddComponentMenu("Image Effects/Other/Antialiasing")]
public class AntialiasingAsPostEffect : PostEffectsBase
{
	public AAMode mode;

	public bool showGeneratedNormals;

	public float offsetScale;

	public float blurRadius;

	public float edgeThresholdMin;

	public float edgeThreshold;

	public float edgeSharpness;

	public bool dlaaSharp;

	public global::UnityEngine.Shader ssaaShader;

	private global::UnityEngine.Material ssaa;

	public global::UnityEngine.Shader dlaaShader;

	private global::UnityEngine.Material dlaa;

	public global::UnityEngine.Shader nfaaShader;

	private global::UnityEngine.Material nfaa;

	public global::UnityEngine.Shader shaderFXAAPreset2;

	private global::UnityEngine.Material materialFXAAPreset2;

	public global::UnityEngine.Shader shaderFXAAPreset3;

	private global::UnityEngine.Material materialFXAAPreset3;

	public global::UnityEngine.Shader shaderFXAAII;

	private global::UnityEngine.Material materialFXAAII;

	public global::UnityEngine.Shader shaderFXAAIII;

	private global::UnityEngine.Material materialFXAAIII;

	public AntialiasingAsPostEffect()
	{
		mode = AAMode.FXAA3Console;
		offsetScale = 0.2f;
		blurRadius = 18f;
		edgeThresholdMin = 0.05f;
		edgeThreshold = 0.2f;
		edgeSharpness = 4f;
	}

	public virtual global::UnityEngine.Material CurrentAAMaterial()
	{
		global::UnityEngine.Material material = null;
		switch (mode)
		{
		case AAMode.FXAA3Console:
			return materialFXAAIII;
		case AAMode.FXAA2:
			return materialFXAAII;
		case AAMode.FXAA1PresetA:
			return materialFXAAPreset2;
		case AAMode.FXAA1PresetB:
			return materialFXAAPreset3;
		case AAMode.NFAA:
			return nfaa;
		case AAMode.SSAA:
			return ssaa;
		case AAMode.DLAA:
			return dlaa;
		default:
			return null;
		}
	}

	public override bool CheckResources()
	{
		CheckSupport(false);
		materialFXAAPreset2 = CreateMaterial(shaderFXAAPreset2, materialFXAAPreset2);
		materialFXAAPreset3 = CreateMaterial(shaderFXAAPreset3, materialFXAAPreset3);
		materialFXAAII = CreateMaterial(shaderFXAAII, materialFXAAII);
		materialFXAAIII = CreateMaterial(shaderFXAAIII, materialFXAAIII);
		nfaa = CreateMaterial(nfaaShader, nfaa);
		ssaa = CreateMaterial(ssaaShader, ssaa);
		dlaa = CreateMaterial(dlaaShader, dlaa);
		if (!ssaaShader.isSupported)
		{
			NotSupported();
			ReportAutoDisable();
		}
		return isSupported;
	}

	public virtual void OnRenderImage(global::UnityEngine.RenderTexture source, global::UnityEngine.RenderTexture destination)
	{
		if (!CheckResources())
		{
			global::UnityEngine.Graphics.Blit(source, destination);
		}
		else if (mode == AAMode.FXAA3Console && materialFXAAIII != null)
		{
			materialFXAAIII.SetFloat("_EdgeThresholdMin", edgeThresholdMin);
			materialFXAAIII.SetFloat("_EdgeThreshold", edgeThreshold);
			materialFXAAIII.SetFloat("_EdgeSharpness", edgeSharpness);
			global::UnityEngine.Graphics.Blit(source, destination, materialFXAAIII);
		}
		else if (mode == AAMode.FXAA1PresetB && materialFXAAPreset3 != null)
		{
			global::UnityEngine.Graphics.Blit(source, destination, materialFXAAPreset3);
		}
		else if (mode == AAMode.FXAA1PresetA && materialFXAAPreset2 != null)
		{
			source.anisoLevel = 4;
			global::UnityEngine.Graphics.Blit(source, destination, materialFXAAPreset2);
			source.anisoLevel = 0;
		}
		else if (mode == AAMode.FXAA2 && materialFXAAII != null)
		{
			global::UnityEngine.Graphics.Blit(source, destination, materialFXAAII);
		}
		else if (mode == AAMode.SSAA && ssaa != null)
		{
			global::UnityEngine.Graphics.Blit(source, destination, ssaa);
		}
		else if (mode == AAMode.DLAA && dlaa != null)
		{
			source.anisoLevel = 0;
			global::UnityEngine.RenderTexture temporary = global::UnityEngine.RenderTexture.GetTemporary(source.width, source.height);
			global::UnityEngine.Graphics.Blit(source, temporary, dlaa, 0);
			global::UnityEngine.Graphics.Blit(temporary, destination, dlaa, (!dlaaSharp) ? 1 : 2);
			global::UnityEngine.RenderTexture.ReleaseTemporary(temporary);
		}
		else if (mode == AAMode.NFAA && nfaa != null)
		{
			source.anisoLevel = 0;
			nfaa.SetFloat("_OffsetScale", offsetScale);
			nfaa.SetFloat("_BlurRadius", blurRadius);
			global::UnityEngine.Graphics.Blit(source, destination, nfaa, showGeneratedNormals ? 1 : 0);
		}
		else
		{
			global::UnityEngine.Graphics.Blit(source, destination);
		}
	}

	public override void Main()
	{
	}
}
