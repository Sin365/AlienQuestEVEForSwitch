[global::System.Serializable]
[global::UnityEngine.AddComponentMenu("Image Effects/Color Adjustments/Contrast Enhance (Unsharp Mask)")]
[global::UnityEngine.ExecuteInEditMode]
[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Camera))]
public class ContrastEnhance : PostEffectsBase
{
	public float intensity;

	public float threshhold;

	private global::UnityEngine.Material separableBlurMaterial;

	private global::UnityEngine.Material contrastCompositeMaterial;

	public float blurSpread;

	public global::UnityEngine.Shader separableBlurShader;

	public global::UnityEngine.Shader contrastCompositeShader;

	public ContrastEnhance()
	{
		intensity = 0.5f;
		blurSpread = 1f;
	}

	public override bool CheckResources()
	{
		CheckSupport(false);
		contrastCompositeMaterial = CheckShaderAndCreateMaterial(contrastCompositeShader, contrastCompositeMaterial);
		separableBlurMaterial = CheckShaderAndCreateMaterial(separableBlurShader, separableBlurMaterial);
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
		global::UnityEngine.RenderTexture temporary = global::UnityEngine.RenderTexture.GetTemporary(width / 2, height / 2, 0);
		global::UnityEngine.Graphics.Blit(source, temporary);
		global::UnityEngine.RenderTexture temporary2 = global::UnityEngine.RenderTexture.GetTemporary(width / 4, height / 4, 0);
		global::UnityEngine.Graphics.Blit(temporary, temporary2);
		global::UnityEngine.RenderTexture.ReleaseTemporary(temporary);
		separableBlurMaterial.SetVector("offsets", new global::UnityEngine.Vector4(0f, blurSpread * 1f / (float)temporary2.height, 0f, 0f));
		global::UnityEngine.RenderTexture temporary3 = global::UnityEngine.RenderTexture.GetTemporary(width / 4, height / 4, 0);
		global::UnityEngine.Graphics.Blit(temporary2, temporary3, separableBlurMaterial);
		global::UnityEngine.RenderTexture.ReleaseTemporary(temporary2);
		separableBlurMaterial.SetVector("offsets", new global::UnityEngine.Vector4(blurSpread * 1f / (float)temporary2.width, 0f, 0f, 0f));
		temporary2 = global::UnityEngine.RenderTexture.GetTemporary(width / 4, height / 4, 0);
		global::UnityEngine.Graphics.Blit(temporary3, temporary2, separableBlurMaterial);
		global::UnityEngine.RenderTexture.ReleaseTemporary(temporary3);
		contrastCompositeMaterial.SetTexture("_MainTexBlurred", temporary2);
		contrastCompositeMaterial.SetFloat("intensity", intensity);
		contrastCompositeMaterial.SetFloat("threshhold", threshhold);
		global::UnityEngine.Graphics.Blit(source, destination, contrastCompositeMaterial);
		global::UnityEngine.RenderTexture.ReleaseTemporary(temporary2);
	}

	public override void Main()
	{
	}
}
