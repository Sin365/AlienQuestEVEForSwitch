[global::System.Serializable]
[global::UnityEngine.AddComponentMenu("Image Effects/Edge Detection/Crease Shading")]
[global::UnityEngine.ExecuteInEditMode]
[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Camera))]
public class Crease : PostEffectsBase
{
	public float intensity;

	public int softness;

	public float spread;

	public global::UnityEngine.Shader blurShader;

	private global::UnityEngine.Material blurMaterial;

	public global::UnityEngine.Shader depthFetchShader;

	private global::UnityEngine.Material depthFetchMaterial;

	public global::UnityEngine.Shader creaseApplyShader;

	private global::UnityEngine.Material creaseApplyMaterial;

	public Crease()
	{
		intensity = 0.5f;
		softness = 1;
		spread = 1f;
	}

	public override bool CheckResources()
	{
		CheckSupport(true);
		blurMaterial = CheckShaderAndCreateMaterial(blurShader, blurMaterial);
		depthFetchMaterial = CheckShaderAndCreateMaterial(depthFetchShader, depthFetchMaterial);
		creaseApplyMaterial = CheckShaderAndCreateMaterial(creaseApplyShader, creaseApplyMaterial);
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
		float num = 1f * (float)width / (1f * (float)height);
		float num2 = 0.001953125f;
		global::UnityEngine.RenderTexture temporary = global::UnityEngine.RenderTexture.GetTemporary(width, height, 0);
		global::UnityEngine.RenderTexture renderTexture = global::UnityEngine.RenderTexture.GetTemporary(width / 2, height / 2, 0);
		global::UnityEngine.Graphics.Blit(source, temporary, depthFetchMaterial);
		global::UnityEngine.Graphics.Blit(temporary, renderTexture);
		for (int i = 0; i < softness; i++)
		{
			global::UnityEngine.RenderTexture temporary2 = global::UnityEngine.RenderTexture.GetTemporary(width / 2, height / 2, 0);
			blurMaterial.SetVector("offsets", new global::UnityEngine.Vector4(0f, spread * num2, 0f, 0f));
			global::UnityEngine.Graphics.Blit(renderTexture, temporary2, blurMaterial);
			global::UnityEngine.RenderTexture.ReleaseTemporary(renderTexture);
			renderTexture = temporary2;
			temporary2 = global::UnityEngine.RenderTexture.GetTemporary(width / 2, height / 2, 0);
			blurMaterial.SetVector("offsets", new global::UnityEngine.Vector4(spread * num2 / num, 0f, 0f, 0f));
			global::UnityEngine.Graphics.Blit(renderTexture, temporary2, blurMaterial);
			global::UnityEngine.RenderTexture.ReleaseTemporary(renderTexture);
			renderTexture = temporary2;
		}
		creaseApplyMaterial.SetTexture("_HrDepthTex", temporary);
		creaseApplyMaterial.SetTexture("_LrDepthTex", renderTexture);
		creaseApplyMaterial.SetFloat("intensity", intensity);
		global::UnityEngine.Graphics.Blit(source, destination, creaseApplyMaterial);
		global::UnityEngine.RenderTexture.ReleaseTemporary(temporary);
		global::UnityEngine.RenderTexture.ReleaseTemporary(renderTexture);
	}

	public override void Main()
	{
	}
}
