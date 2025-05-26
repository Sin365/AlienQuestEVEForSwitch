[global::System.Serializable]
[global::UnityEngine.ExecuteInEditMode]
[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Camera))]
[global::UnityEngine.AddComponentMenu("Image Effects/Blur/Blur (Optimized)")]
public class Blur : PostEffectsBase
{
	[global::System.Serializable]
	public enum BlurType
	{
		StandardGauss = 0,
		SgxGauss = 1
	}

	[global::UnityEngine.Range(0f, 2f)]
	public int downsample;

	[global::UnityEngine.Range(0f, 10f)]
	public float blurSize;

	[global::UnityEngine.Range(1f, 4f)]
	public int blurIterations;

	public Blur.BlurType blurType;

	public global::UnityEngine.Shader blurShader;

	private global::UnityEngine.Material blurMaterial;

	public Blur()
	{
		downsample = 1;
		blurSize = 3f;
		blurIterations = 2;
		blurType = Blur.BlurType.StandardGauss;
	}

	public override bool CheckResources()
	{
		CheckSupport(false);
		blurMaterial = CheckShaderAndCreateMaterial(blurShader, blurMaterial);
		if (!isSupported)
		{
			ReportAutoDisable();
		}
		return isSupported;
	}

	public virtual void OnDisable()
	{
		if ((bool)blurMaterial)
		{
			global::UnityEngine.Object.DestroyImmediate(blurMaterial);
		}
	}

	public virtual void OnRenderImage(global::UnityEngine.RenderTexture source, global::UnityEngine.RenderTexture destination)
	{
		if (!CheckResources())
		{
			global::UnityEngine.Graphics.Blit(source, destination);
			return;
		}
		float num = 1f / (1f * (float)(1 << downsample));
		blurMaterial.SetVector("_Parameter", new global::UnityEngine.Vector4(blurSize * num, (0f - blurSize) * num, 0f, 0f));
		source.filterMode = global::UnityEngine.FilterMode.Bilinear;
		int width = source.width >> downsample;
		int height = source.height >> downsample;
		global::UnityEngine.RenderTexture renderTexture = global::UnityEngine.RenderTexture.GetTemporary(width, height, 0, source.format);
		renderTexture.filterMode = global::UnityEngine.FilterMode.Bilinear;
		global::UnityEngine.Graphics.Blit(source, renderTexture, blurMaterial, 0);
		int num2 = ((blurType != Blur.BlurType.StandardGauss) ? 2 : 0);
		for (int i = 0; i < blurIterations; i++)
		{
			float num3 = (float)i * 1f;
			blurMaterial.SetVector("_Parameter", new global::UnityEngine.Vector4(blurSize * num + num3, (0f - blurSize) * num - num3, 0f, 0f));
			global::UnityEngine.RenderTexture temporary = global::UnityEngine.RenderTexture.GetTemporary(width, height, 0, source.format);
			temporary.filterMode = global::UnityEngine.FilterMode.Bilinear;
			global::UnityEngine.Graphics.Blit(renderTexture, temporary, blurMaterial, 1 + num2);
			global::UnityEngine.RenderTexture.ReleaseTemporary(renderTexture);
			renderTexture = temporary;
			temporary = global::UnityEngine.RenderTexture.GetTemporary(width, height, 0, source.format);
			temporary.filterMode = global::UnityEngine.FilterMode.Bilinear;
			global::UnityEngine.Graphics.Blit(renderTexture, temporary, blurMaterial, 2 + num2);
			global::UnityEngine.RenderTexture.ReleaseTemporary(renderTexture);
			renderTexture = temporary;
		}
		global::UnityEngine.Graphics.Blit(renderTexture, destination);
		global::UnityEngine.RenderTexture.ReleaseTemporary(renderTexture);
	}

	public override void Main()
	{
	}
}
