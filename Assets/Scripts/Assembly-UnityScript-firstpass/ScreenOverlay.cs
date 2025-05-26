[global::System.Serializable]
[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Camera))]
[global::UnityEngine.AddComponentMenu("Image Effects/Other/Screen Overlay")]
[global::UnityEngine.ExecuteInEditMode]
public class ScreenOverlay : PostEffectsBase
{
	[global::System.Serializable]
	public enum OverlayBlendMode
	{
		Additive = 0,
		ScreenBlend = 1,
		Multiply = 2,
		Overlay = 3,
		AlphaBlend = 4
	}

	public ScreenOverlay.OverlayBlendMode blendMode;

	public float intensity;

	public global::UnityEngine.Texture2D texture;

	public global::UnityEngine.Shader overlayShader;

	private global::UnityEngine.Material overlayMaterial;

	public ScreenOverlay()
	{
		blendMode = ScreenOverlay.OverlayBlendMode.Overlay;
		intensity = 1f;
	}

	public override bool CheckResources()
	{
		CheckSupport(false);
		overlayMaterial = CheckShaderAndCreateMaterial(overlayShader, overlayMaterial);
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
		global::UnityEngine.Vector4 vector = new global::UnityEngine.Vector4(1f, 0f, 0f, 1f);
		overlayMaterial.SetVector("_UV_Transform", vector);
		overlayMaterial.SetFloat("_Intensity", intensity);
		overlayMaterial.SetTexture("_Overlay", texture);
		global::UnityEngine.Graphics.Blit(source, destination, overlayMaterial, (int)blendMode);
	}

	public override void Main()
	{
	}
}
