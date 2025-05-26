[global::System.Serializable]
[global::UnityEngine.AddComponentMenu("Image Effects/Rendering/Screen Space Ambient Obscurance")]
[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Camera))]
[global::UnityEngine.ExecuteInEditMode]
public class AmbientObscurance : PostEffectsBase
{
	[global::UnityEngine.Range(0f, 3f)]
	public float intensity;

	[global::UnityEngine.Range(0.1f, 3f)]
	public float radius;

	[global::UnityEngine.Range(0f, 3f)]
	public int blurIterations;

	[global::UnityEngine.Range(0f, 5f)]
	public float blurFilterDistance;

	[global::UnityEngine.Range(0f, 1f)]
	public int downsample;

	public global::UnityEngine.Texture2D rand;

	public global::UnityEngine.Shader aoShader;

	private global::UnityEngine.Material aoMaterial;

	public AmbientObscurance()
	{
		intensity = 0.5f;
		radius = 0.2f;
		blurIterations = 1;
		blurFilterDistance = 1.25f;
	}

	public override bool CheckResources()
	{
		CheckSupport(true);
		aoMaterial = CheckShaderAndCreateMaterial(aoShader, aoMaterial);
		if (!isSupported)
		{
			ReportAutoDisable();
		}
		return isSupported;
	}

	public virtual void OnDisable()
	{
		if ((bool)aoMaterial)
		{
			global::UnityEngine.Object.DestroyImmediate(aoMaterial);
		}
		aoMaterial = null;
	}

	[global::UnityEngine.ImageEffectOpaque]
	public virtual void OnRenderImage(global::UnityEngine.RenderTexture source, global::UnityEngine.RenderTexture destination)
	{
		if (!CheckResources())
		{
			global::UnityEngine.Graphics.Blit(source, destination);
			return;
		}
		global::UnityEngine.Matrix4x4 projectionMatrix = GetComponent<UnityEngine.Camera>().projectionMatrix;
		global::UnityEngine.Matrix4x4 inverse = projectionMatrix.inverse;
		global::UnityEngine.Vector4 vector = new global::UnityEngine.Vector4(-2f / ((float)global::UnityEngine.Screen.width * projectionMatrix[0]), -2f / ((float)global::UnityEngine.Screen.height * projectionMatrix[5]), (1f - projectionMatrix[2]) / projectionMatrix[0], (1f + projectionMatrix[6]) / projectionMatrix[5]);
		aoMaterial.SetVector("_ProjInfo", vector);
		aoMaterial.SetMatrix("_ProjectionInv", inverse);
		aoMaterial.SetTexture("_Rand", rand);
		aoMaterial.SetFloat("_Radius", radius);
		aoMaterial.SetFloat("_Radius2", radius * radius);
		aoMaterial.SetFloat("_Intensity", intensity);
		aoMaterial.SetFloat("_BlurFilterDistance", blurFilterDistance);
		int width = source.width;
		int height = source.height;
		global::UnityEngine.RenderTexture renderTexture = global::UnityEngine.RenderTexture.GetTemporary(width >> downsample, height >> downsample);
		global::UnityEngine.RenderTexture renderTexture2 = null;
		global::UnityEngine.Graphics.Blit(source, renderTexture, aoMaterial, 0);
		if (downsample > 0)
		{
			renderTexture2 = global::UnityEngine.RenderTexture.GetTemporary(width, height);
			global::UnityEngine.Graphics.Blit(renderTexture, renderTexture2, aoMaterial, 4);
			global::UnityEngine.RenderTexture.ReleaseTemporary(renderTexture);
			renderTexture = renderTexture2;
		}
		for (int i = 0; i < blurIterations; i++)
		{
			aoMaterial.SetVector("_Axis", new global::UnityEngine.Vector2(1f, 0f));
			renderTexture2 = global::UnityEngine.RenderTexture.GetTemporary(width, height);
			global::UnityEngine.Graphics.Blit(renderTexture, renderTexture2, aoMaterial, 1);
			global::UnityEngine.RenderTexture.ReleaseTemporary(renderTexture);
			aoMaterial.SetVector("_Axis", new global::UnityEngine.Vector2(0f, 1f));
			renderTexture = global::UnityEngine.RenderTexture.GetTemporary(width, height);
			global::UnityEngine.Graphics.Blit(renderTexture2, renderTexture, aoMaterial, 1);
			global::UnityEngine.RenderTexture.ReleaseTemporary(renderTexture2);
		}
		aoMaterial.SetTexture("_AOTex", renderTexture);
		global::UnityEngine.Graphics.Blit(source, destination, aoMaterial, 2);
		global::UnityEngine.RenderTexture.ReleaseTemporary(renderTexture);
	}

	public override void Main()
	{
	}
}
