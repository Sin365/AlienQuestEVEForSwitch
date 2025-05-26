[global::System.Serializable]
[global::UnityEngine.AddComponentMenu("Image Effects/Bloom and Glow/Bloom (Optimized)")]
[global::UnityEngine.ExecuteInEditMode]
[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Camera))]
public class FastBloom : PostEffectsBase
{
	[global::System.Serializable]
	public enum Resolution
	{
		Low = 0,
		High = 1
	}

	[global::System.Serializable]
	public enum BlurType
	{
		Standard = 0,
		Sgx = 1
	}

	[global::UnityEngine.Range(0f, 1.5f)]
	public float threshhold;

	[global::UnityEngine.Range(0f, 2.5f)]
	public float intensity;

	[global::UnityEngine.Range(0.25f, 5.5f)]
	public float blurSize;

	public FastBloom.Resolution resolution;

	[global::UnityEngine.Range(1f, 4f)]
	public int blurIterations;

	public FastBloom.BlurType blurType;

	public global::UnityEngine.Shader fastBloomShader;

	private global::UnityEngine.Material fastBloomMaterial;

	public FastBloom()
	{
		threshhold = 0.25f;
		intensity = 0.75f;
		blurSize = 1f;
		resolution = FastBloom.Resolution.Low;
		blurIterations = 1;
		blurType = FastBloom.BlurType.Standard;
	}

	public override bool CheckResources()
	{
		CheckSupport(false);
		fastBloomMaterial = CheckShaderAndCreateMaterial(fastBloomShader, fastBloomMaterial);
		if (!isSupported)
		{
			ReportAutoDisable();
		}
		return isSupported;
	}

	public virtual void OnDisable()
	{
		if ((bool)fastBloomMaterial)
		{
			global::UnityEngine.Object.DestroyImmediate(fastBloomMaterial);
		}
	}

	public virtual void OnRenderImage(global::UnityEngine.RenderTexture source, global::UnityEngine.RenderTexture destination)
	{
		if (!CheckResources())
		{
			global::UnityEngine.Graphics.Blit(source, destination);
			return;
		}
		int num = ((resolution != FastBloom.Resolution.Low) ? 2 : 4);
		float num2 = ((resolution != FastBloom.Resolution.Low) ? 1f : 0.5f);
		fastBloomMaterial.SetVector("_Parameter", new global::UnityEngine.Vector4(blurSize * num2, 0f, threshhold, intensity));
		source.filterMode = global::UnityEngine.FilterMode.Bilinear;
		int width = source.width / num;
		int height = source.height / num;
		global::UnityEngine.RenderTexture renderTexture = global::UnityEngine.RenderTexture.GetTemporary(width, height, 0, source.format);
		renderTexture.filterMode = global::UnityEngine.FilterMode.Bilinear;
		global::UnityEngine.Graphics.Blit(source, renderTexture, fastBloomMaterial, 1);
		int num3 = ((blurType != FastBloom.BlurType.Standard) ? 2 : 0);
		for (int i = 0; i < blurIterations; i++)
		{
			fastBloomMaterial.SetVector("_Parameter", new global::UnityEngine.Vector4(blurSize * num2 + (float)i * 1f, 0f, threshhold, intensity));
			global::UnityEngine.RenderTexture temporary = global::UnityEngine.RenderTexture.GetTemporary(width, height, 0, source.format);
			temporary.filterMode = global::UnityEngine.FilterMode.Bilinear;
			global::UnityEngine.Graphics.Blit(renderTexture, temporary, fastBloomMaterial, 2 + num3);
			global::UnityEngine.RenderTexture.ReleaseTemporary(renderTexture);
			renderTexture = temporary;
			temporary = global::UnityEngine.RenderTexture.GetTemporary(width, height, 0, source.format);
			temporary.filterMode = global::UnityEngine.FilterMode.Bilinear;
			global::UnityEngine.Graphics.Blit(renderTexture, temporary, fastBloomMaterial, 3 + num3);
			global::UnityEngine.RenderTexture.ReleaseTemporary(renderTexture);
			renderTexture = temporary;
		}
		fastBloomMaterial.SetTexture("_Bloom", renderTexture);
		global::UnityEngine.Graphics.Blit(source, destination, fastBloomMaterial, 0);
		global::UnityEngine.RenderTexture.ReleaseTemporary(renderTexture);
	}

	public override void Main()
	{
	}
}
