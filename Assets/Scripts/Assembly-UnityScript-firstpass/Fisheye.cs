[global::System.Serializable]
[global::UnityEngine.ExecuteInEditMode]
[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Camera))]
[global::UnityEngine.AddComponentMenu("Image Effects/Displacement/Fisheye")]
public class Fisheye : PostEffectsBase
{
	public float strengthX;

	public float strengthY;

	public global::UnityEngine.Shader fishEyeShader;

	private global::UnityEngine.Material fisheyeMaterial;

	public Fisheye()
	{
		strengthX = 0.05f;
		strengthY = 0.05f;
	}

	public override bool CheckResources()
	{
		CheckSupport(false);
		fisheyeMaterial = CheckShaderAndCreateMaterial(fishEyeShader, fisheyeMaterial);
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
		float num = 5f / 32f;
		float num2 = (float)source.width * 1f / ((float)source.height * 1f);
		fisheyeMaterial.SetVector("intensity", new global::UnityEngine.Vector4(strengthX * num2 * num, strengthY * num, strengthX * num2 * num, strengthY * num));
		global::UnityEngine.Graphics.Blit(source, destination, fisheyeMaterial);
	}

	public override void Main()
	{
	}
}
