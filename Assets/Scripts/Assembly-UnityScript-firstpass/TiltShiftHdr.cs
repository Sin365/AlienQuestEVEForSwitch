[global::System.Serializable]
[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Camera))]
[global::UnityEngine.AddComponentMenu("Image Effects/Camera/Tilt Shift (Lens Blur)")]
[global::UnityEngine.ExecuteInEditMode]
public class TiltShiftHdr : PostEffectsBase
{
	[global::System.Serializable]
	public enum TiltShiftMode
	{
		TiltShiftMode = 0,
		IrisMode = 1
	}

	[global::System.Serializable]
	public enum TiltShiftQuality
	{
		Preview = 0,
		Normal = 1,
		High = 2
	}

	public TiltShiftHdr.TiltShiftMode mode;

	public TiltShiftHdr.TiltShiftQuality quality;

	[global::UnityEngine.Range(0f, 15f)]
	public float blurArea;

	[global::UnityEngine.Range(0f, 25f)]
	public float maxBlurSize;

	[global::UnityEngine.Range(0f, 1f)]
	public int downsample;

	public global::UnityEngine.Shader tiltShiftShader;

	private global::UnityEngine.Material tiltShiftMaterial;

	public TiltShiftHdr()
	{
		mode = TiltShiftHdr.TiltShiftMode.TiltShiftMode;
		quality = TiltShiftHdr.TiltShiftQuality.Normal;
		blurArea = 1f;
		maxBlurSize = 5f;
	}

	public override bool CheckResources()
	{
		CheckSupport(true);
		tiltShiftMaterial = CheckShaderAndCreateMaterial(tiltShiftShader, tiltShiftMaterial);
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
		tiltShiftMaterial.SetFloat("_BlurSize", (maxBlurSize >= 0f) ? maxBlurSize : 0f);
		tiltShiftMaterial.SetFloat("_BlurArea", blurArea);
		source.filterMode = global::UnityEngine.FilterMode.Bilinear;
		global::UnityEngine.RenderTexture renderTexture = destination;
		if (downsample != 0)
		{
			renderTexture = global::UnityEngine.RenderTexture.GetTemporary(source.width >> downsample, source.height >> downsample, 0, source.format);
			renderTexture.filterMode = global::UnityEngine.FilterMode.Bilinear;
		}
		int num = (int)quality;
		num *= 2;
		global::UnityEngine.Graphics.Blit(source, renderTexture, tiltShiftMaterial, (mode != TiltShiftHdr.TiltShiftMode.TiltShiftMode) ? (num + 1) : num);
		if (downsample != 0)
		{
			tiltShiftMaterial.SetTexture("_Blurred", renderTexture);
			global::UnityEngine.Graphics.Blit(source, destination, tiltShiftMaterial, 6);
		}
		if (renderTexture != destination)
		{
			global::UnityEngine.RenderTexture.ReleaseTemporary(renderTexture);
		}
	}

	public override void Main()
	{
	}
}
