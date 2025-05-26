[global::System.Serializable]
[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Camera))]
[global::UnityEngine.ExecuteInEditMode]
[global::UnityEngine.AddComponentMenu("Image Effects/Edge Detection/Edge Detection")]
public class EdgeDetectEffectNormals : PostEffectsBase
{
	public EdgeDetectMode mode;

	public float sensitivityDepth;

	public float sensitivityNormals;

	public float lumThreshhold;

	public float edgeExp;

	public float sampleDist;

	public float edgesOnly;

	public global::UnityEngine.Color edgesOnlyBgColor;

	public global::UnityEngine.Shader edgeDetectShader;

	private global::UnityEngine.Material edgeDetectMaterial;

	private EdgeDetectMode oldMode;

	public EdgeDetectEffectNormals()
	{
		mode = EdgeDetectMode.SobelDepthThin;
		sensitivityDepth = 1f;
		sensitivityNormals = 1f;
		lumThreshhold = 0.2f;
		edgeExp = 1f;
		sampleDist = 1f;
		edgesOnlyBgColor = global::UnityEngine.Color.white;
		oldMode = EdgeDetectMode.SobelDepthThin;
	}

	public override bool CheckResources()
	{
		CheckSupport(true);
		edgeDetectMaterial = CheckShaderAndCreateMaterial(edgeDetectShader, edgeDetectMaterial);
		if (mode != oldMode)
		{
			SetCameraFlag();
		}
		oldMode = mode;
		if (!isSupported)
		{
			ReportAutoDisable();
		}
		return isSupported;
	}

	public override void Start()
	{
		oldMode = mode;
	}

	public virtual void SetCameraFlag()
	{
		if (mode == EdgeDetectMode.SobelDepth || mode == EdgeDetectMode.SobelDepthThin)
		{
			GetComponent<UnityEngine.Camera>().depthTextureMode |= global::UnityEngine.DepthTextureMode.Depth;
		}
		else if (mode == EdgeDetectMode.TriangleDepthNormals || mode == EdgeDetectMode.RobertsCrossDepthNormals)
		{
			GetComponent<UnityEngine.Camera>().depthTextureMode |= global::UnityEngine.DepthTextureMode.DepthNormals;
		}
	}

	public override void OnEnable()
	{
		SetCameraFlag();
	}

	[global::UnityEngine.ImageEffectOpaque]
	public virtual void OnRenderImage(global::UnityEngine.RenderTexture source, global::UnityEngine.RenderTexture destination)
	{
		if (!CheckResources())
		{
			global::UnityEngine.Graphics.Blit(source, destination);
			return;
		}
		global::UnityEngine.Vector2 vector = new global::UnityEngine.Vector2(sensitivityDepth, sensitivityNormals);
		edgeDetectMaterial.SetVector("_Sensitivity", new global::UnityEngine.Vector4(vector.x, vector.y, 1f, vector.y));
		edgeDetectMaterial.SetFloat("_BgFade", edgesOnly);
		edgeDetectMaterial.SetFloat("_SampleDistance", sampleDist);
		edgeDetectMaterial.SetVector("_BgColor", edgesOnlyBgColor);
		edgeDetectMaterial.SetFloat("_Exponent", edgeExp);
		edgeDetectMaterial.SetFloat("_Threshold", lumThreshhold);
		global::UnityEngine.Graphics.Blit(source, destination, edgeDetectMaterial, (int)mode);
	}

	public override void Main()
	{
	}
}
