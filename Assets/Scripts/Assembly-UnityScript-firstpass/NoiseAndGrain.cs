[global::System.Serializable]
[global::UnityEngine.ExecuteInEditMode]
[global::UnityEngine.AddComponentMenu("Image Effects/Noise/Noise And Grain (Filmic)")]
[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Camera))]
public class NoiseAndGrain : PostEffectsBase
{
	public float intensityMultiplier;

	public float generalIntensity;

	public float blackIntensity;

	public float whiteIntensity;

	public float midGrey;

	public bool dx11Grain;

	public float softness;

	public bool monochrome;

	public global::UnityEngine.Vector3 intensities;

	public global::UnityEngine.Vector3 tiling;

	public float monochromeTiling;

	public global::UnityEngine.FilterMode filterMode;

	public global::UnityEngine.Texture2D noiseTexture;

	public global::UnityEngine.Shader noiseShader;

	private global::UnityEngine.Material noiseMaterial;

	public global::UnityEngine.Shader dx11NoiseShader;

	private global::UnityEngine.Material dx11NoiseMaterial;

	[global::System.NonSerialized]
	private static float TILE_AMOUNT = 64f;

	public NoiseAndGrain()
	{
		intensityMultiplier = 0.25f;
		generalIntensity = 0.5f;
		blackIntensity = 1f;
		whiteIntensity = 1f;
		midGrey = 0.2f;
		intensities = new global::UnityEngine.Vector3(1f, 1f, 1f);
		tiling = new global::UnityEngine.Vector3(64f, 64f, 64f);
		monochromeTiling = 64f;
		filterMode = global::UnityEngine.FilterMode.Bilinear;
	}

	public override bool CheckResources()
	{
		CheckSupport(false);
		noiseMaterial = CheckShaderAndCreateMaterial(noiseShader, noiseMaterial);
		if (dx11Grain && supportDX11)
		{
			dx11NoiseMaterial = CheckShaderAndCreateMaterial(dx11NoiseShader, dx11NoiseMaterial);
		}
		if (!isSupported)
		{
			ReportAutoDisable();
		}
		return isSupported;
	}

	public virtual void OnRenderImage(global::UnityEngine.RenderTexture source, global::UnityEngine.RenderTexture destination)
	{
		if (!CheckResources() || null == noiseTexture)
		{
			global::UnityEngine.Graphics.Blit(source, destination);
			if (null == noiseTexture)
			{
				global::UnityEngine.Debug.LogWarning("Noise & Grain effect failing as noise texture is not assigned. please assign.", transform);
			}
			return;
		}
		softness = global::UnityEngine.Mathf.Clamp(softness, 0f, 0.99f);
		if (dx11Grain && supportDX11)
		{
			dx11NoiseMaterial.SetFloat("_DX11NoiseTime", global::UnityEngine.Time.frameCount);
			dx11NoiseMaterial.SetTexture("_NoiseTex", noiseTexture);
			dx11NoiseMaterial.SetVector("_NoisePerChannel", (!monochrome) ? intensities : global::UnityEngine.Vector3.one);
			dx11NoiseMaterial.SetVector("_MidGrey", new global::UnityEngine.Vector3(midGrey, 1f / (1f - midGrey), -1f / midGrey));
			dx11NoiseMaterial.SetVector("_NoiseAmount", new global::UnityEngine.Vector3(generalIntensity, blackIntensity, whiteIntensity) * intensityMultiplier);
			if (!(softness <= float.Epsilon))
			{
				global::UnityEngine.RenderTexture temporary = global::UnityEngine.RenderTexture.GetTemporary((int)((float)source.width * (1f - softness)), (int)((float)source.height * (1f - softness)));
				DrawNoiseQuadGrid(source, temporary, dx11NoiseMaterial, noiseTexture, (!monochrome) ? 2 : 3);
				dx11NoiseMaterial.SetTexture("_NoiseTex", temporary);
				global::UnityEngine.Graphics.Blit(source, destination, dx11NoiseMaterial, 4);
				global::UnityEngine.RenderTexture.ReleaseTemporary(temporary);
			}
			else
			{
				DrawNoiseQuadGrid(source, destination, dx11NoiseMaterial, noiseTexture, monochrome ? 1 : 0);
			}
			return;
		}
		if ((bool)noiseTexture)
		{
			noiseTexture.wrapMode = global::UnityEngine.TextureWrapMode.Repeat;
			noiseTexture.filterMode = filterMode;
		}
		noiseMaterial.SetTexture("_NoiseTex", noiseTexture);
		noiseMaterial.SetVector("_NoisePerChannel", (!monochrome) ? intensities : global::UnityEngine.Vector3.one);
		noiseMaterial.SetVector("_NoiseTilingPerChannel", (!monochrome) ? tiling : (global::UnityEngine.Vector3.one * monochromeTiling));
		noiseMaterial.SetVector("_MidGrey", new global::UnityEngine.Vector3(midGrey, 1f / (1f - midGrey), -1f / midGrey));
		noiseMaterial.SetVector("_NoiseAmount", new global::UnityEngine.Vector3(generalIntensity, blackIntensity, whiteIntensity) * intensityMultiplier);
		if (!(softness <= float.Epsilon))
		{
			global::UnityEngine.RenderTexture temporary2 = global::UnityEngine.RenderTexture.GetTemporary((int)((float)source.width * (1f - softness)), (int)((float)source.height * (1f - softness)));
			DrawNoiseQuadGrid(source, temporary2, noiseMaterial, noiseTexture, 2);
			noiseMaterial.SetTexture("_NoiseTex", temporary2);
			global::UnityEngine.Graphics.Blit(source, destination, noiseMaterial, 1);
			global::UnityEngine.RenderTexture.ReleaseTemporary(temporary2);
		}
		else
		{
			DrawNoiseQuadGrid(source, destination, noiseMaterial, noiseTexture, 0);
		}
	}

	public static void DrawNoiseQuadGrid(global::UnityEngine.RenderTexture source, global::UnityEngine.RenderTexture dest, global::UnityEngine.Material fxMaterial, global::UnityEngine.Texture2D noise, int passNr)
	{
		global::UnityEngine.RenderTexture.active = dest;
		float num = (float)noise.width * 1f;
		float num2 = 1f * (float)source.width / TILE_AMOUNT;
		fxMaterial.SetTexture("_MainTex", source);
		global::UnityEngine.GL.PushMatrix();
		global::UnityEngine.GL.LoadOrtho();
		float num3 = 1f * (float)source.width / (1f * (float)source.height);
		float num4 = 1f / num2;
		float num5 = num4 * num3;
		float num6 = num / ((float)noise.width * 1f);
		fxMaterial.SetPass(passNr);
		global::UnityEngine.GL.Begin(7);
		for (float num7 = 0f; num7 < 1f; num7 += num4)
		{
			for (float num8 = 0f; num8 < 1f; num8 += num5)
			{
				float num9 = global::UnityEngine.Random.Range(0f, 1f);
				float num10 = global::UnityEngine.Random.Range(0f, 1f);
				num9 = global::UnityEngine.Mathf.Floor(num9 * num) / num;
				num10 = global::UnityEngine.Mathf.Floor(num10 * num) / num;
				float num11 = 1f / num;
				global::UnityEngine.GL.MultiTexCoord2(0, num9, num10);
				global::UnityEngine.GL.MultiTexCoord2(1, 0f, 0f);
				global::UnityEngine.GL.Vertex3(num7, num8, 0.1f);
				global::UnityEngine.GL.MultiTexCoord2(0, num9 + num6 * num11, num10);
				global::UnityEngine.GL.MultiTexCoord2(1, 1f, 0f);
				global::UnityEngine.GL.Vertex3(num7 + num4, num8, 0.1f);
				global::UnityEngine.GL.MultiTexCoord2(0, num9 + num6 * num11, num10 + num6 * num11);
				global::UnityEngine.GL.MultiTexCoord2(1, 1f, 1f);
				global::UnityEngine.GL.Vertex3(num7 + num4, num8 + num5, 0.1f);
				global::UnityEngine.GL.MultiTexCoord2(0, num9, num10 + num6 * num11);
				global::UnityEngine.GL.MultiTexCoord2(1, 0f, 1f);
				global::UnityEngine.GL.Vertex3(num7, num8 + num5, 0.1f);
			}
		}
		global::UnityEngine.GL.End();
		global::UnityEngine.GL.PopMatrix();
	}

	public override void Main()
	{
	}
}
