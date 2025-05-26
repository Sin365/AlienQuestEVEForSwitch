[global::System.Serializable]
[global::UnityEngine.AddComponentMenu("Image Effects/Color Adjustments/Color Correction (Curves, Saturation)")]
[global::UnityEngine.ExecuteInEditMode]
public class ColorCorrectionCurves : PostEffectsBase
{
	public global::UnityEngine.AnimationCurve redChannel;

	public global::UnityEngine.AnimationCurve greenChannel;

	public global::UnityEngine.AnimationCurve blueChannel;

	public bool useDepthCorrection;

	public global::UnityEngine.AnimationCurve zCurve;

	public global::UnityEngine.AnimationCurve depthRedChannel;

	public global::UnityEngine.AnimationCurve depthGreenChannel;

	public global::UnityEngine.AnimationCurve depthBlueChannel;

	private global::UnityEngine.Material ccMaterial;

	private global::UnityEngine.Material ccDepthMaterial;

	private global::UnityEngine.Material selectiveCcMaterial;

	private global::UnityEngine.Texture2D rgbChannelTex;

	private global::UnityEngine.Texture2D rgbDepthChannelTex;

	private global::UnityEngine.Texture2D zCurveTex;

	public float saturation;

	public bool selectiveCc;

	public global::UnityEngine.Color selectiveFromColor;

	public global::UnityEngine.Color selectiveToColor;

	public ColorCorrectionMode mode;

	public bool updateTextures;

	public global::UnityEngine.Shader colorCorrectionCurvesShader;

	public global::UnityEngine.Shader simpleColorCorrectionCurvesShader;

	public global::UnityEngine.Shader colorCorrectionSelectiveShader;

	private bool updateTexturesOnStartup;

	public ColorCorrectionCurves()
	{
		saturation = 1f;
		selectiveFromColor = global::UnityEngine.Color.white;
		selectiveToColor = global::UnityEngine.Color.white;
		updateTextures = true;
		updateTexturesOnStartup = true;
	}

	public override void Start()
	{
		base.Start();
		updateTexturesOnStartup = true;
	}

	public virtual void Awake()
	{
	}

	public override bool CheckResources()
	{
		CheckSupport(mode == ColorCorrectionMode.Advanced);
		ccMaterial = CheckShaderAndCreateMaterial(simpleColorCorrectionCurvesShader, ccMaterial);
		ccDepthMaterial = CheckShaderAndCreateMaterial(colorCorrectionCurvesShader, ccDepthMaterial);
		selectiveCcMaterial = CheckShaderAndCreateMaterial(colorCorrectionSelectiveShader, selectiveCcMaterial);
		if (!rgbChannelTex)
		{
			rgbChannelTex = new global::UnityEngine.Texture2D(256, 4, global::UnityEngine.TextureFormat.ARGB32, false, true);
		}
		if (!rgbDepthChannelTex)
		{
			rgbDepthChannelTex = new global::UnityEngine.Texture2D(256, 4, global::UnityEngine.TextureFormat.ARGB32, false, true);
		}
		if (!zCurveTex)
		{
			zCurveTex = new global::UnityEngine.Texture2D(256, 1, global::UnityEngine.TextureFormat.ARGB32, false, true);
		}
		rgbChannelTex.hideFlags = global::UnityEngine.HideFlags.DontSave;
		rgbDepthChannelTex.hideFlags = global::UnityEngine.HideFlags.DontSave;
		zCurveTex.hideFlags = global::UnityEngine.HideFlags.DontSave;
		rgbChannelTex.wrapMode = global::UnityEngine.TextureWrapMode.Clamp;
		rgbDepthChannelTex.wrapMode = global::UnityEngine.TextureWrapMode.Clamp;
		zCurveTex.wrapMode = global::UnityEngine.TextureWrapMode.Clamp;
		if (!isSupported)
		{
			ReportAutoDisable();
		}
		return isSupported;
	}

	public virtual void UpdateParameters()
	{
		CheckResources();
		if (redChannel != null && greenChannel != null && blueChannel != null)
		{
			for (float num = 0f; num <= 1f; num += 0.003921569f)
			{
				float num2 = global::UnityEngine.Mathf.Clamp(redChannel.Evaluate(num), 0f, 1f);
				float num3 = global::UnityEngine.Mathf.Clamp(greenChannel.Evaluate(num), 0f, 1f);
				float num4 = global::UnityEngine.Mathf.Clamp(blueChannel.Evaluate(num), 0f, 1f);
				rgbChannelTex.SetPixel((int)global::UnityEngine.Mathf.Floor(num * 255f), 0, new global::UnityEngine.Color(num2, num2, num2));
				rgbChannelTex.SetPixel((int)global::UnityEngine.Mathf.Floor(num * 255f), 1, new global::UnityEngine.Color(num3, num3, num3));
				rgbChannelTex.SetPixel((int)global::UnityEngine.Mathf.Floor(num * 255f), 2, new global::UnityEngine.Color(num4, num4, num4));
				float num5 = global::UnityEngine.Mathf.Clamp(zCurve.Evaluate(num), 0f, 1f);
				zCurveTex.SetPixel((int)global::UnityEngine.Mathf.Floor(num * 255f), 0, new global::UnityEngine.Color(num5, num5, num5));
				num2 = global::UnityEngine.Mathf.Clamp(depthRedChannel.Evaluate(num), 0f, 1f);
				num3 = global::UnityEngine.Mathf.Clamp(depthGreenChannel.Evaluate(num), 0f, 1f);
				num4 = global::UnityEngine.Mathf.Clamp(depthBlueChannel.Evaluate(num), 0f, 1f);
				rgbDepthChannelTex.SetPixel((int)global::UnityEngine.Mathf.Floor(num * 255f), 0, new global::UnityEngine.Color(num2, num2, num2));
				rgbDepthChannelTex.SetPixel((int)global::UnityEngine.Mathf.Floor(num * 255f), 1, new global::UnityEngine.Color(num3, num3, num3));
				rgbDepthChannelTex.SetPixel((int)global::UnityEngine.Mathf.Floor(num * 255f), 2, new global::UnityEngine.Color(num4, num4, num4));
			}
			rgbChannelTex.Apply();
			rgbDepthChannelTex.Apply();
			zCurveTex.Apply();
		}
	}

	public virtual void UpdateTextures()
	{
		UpdateParameters();
	}

	public virtual void OnRenderImage(global::UnityEngine.RenderTexture source, global::UnityEngine.RenderTexture destination)
	{
		if (!CheckResources())
		{
			global::UnityEngine.Graphics.Blit(source, destination);
			return;
		}
		if (updateTexturesOnStartup)
		{
			UpdateParameters();
			updateTexturesOnStartup = false;
		}
		if (useDepthCorrection)
		{
			GetComponent<UnityEngine.Camera>().depthTextureMode |= global::UnityEngine.DepthTextureMode.Depth;
		}
		global::UnityEngine.RenderTexture renderTexture = destination;
		if (selectiveCc)
		{
			renderTexture = global::UnityEngine.RenderTexture.GetTemporary(source.width, source.height);
		}
		if (useDepthCorrection)
		{
			ccDepthMaterial.SetTexture("_RgbTex", rgbChannelTex);
			ccDepthMaterial.SetTexture("_ZCurve", zCurveTex);
			ccDepthMaterial.SetTexture("_RgbDepthTex", rgbDepthChannelTex);
			ccDepthMaterial.SetFloat("_Saturation", saturation);
			global::UnityEngine.Graphics.Blit(source, renderTexture, ccDepthMaterial);
		}
		else
		{
			ccMaterial.SetTexture("_RgbTex", rgbChannelTex);
			ccMaterial.SetFloat("_Saturation", saturation);
			global::UnityEngine.Graphics.Blit(source, renderTexture, ccMaterial);
		}
		if (selectiveCc)
		{
			selectiveCcMaterial.SetColor("selColor", selectiveFromColor);
			selectiveCcMaterial.SetColor("targetColor", selectiveToColor);
			global::UnityEngine.Graphics.Blit(renderTexture, destination, selectiveCcMaterial);
			global::UnityEngine.RenderTexture.ReleaseTemporary(renderTexture);
		}
	}

	public override void Main()
	{
	}
}
