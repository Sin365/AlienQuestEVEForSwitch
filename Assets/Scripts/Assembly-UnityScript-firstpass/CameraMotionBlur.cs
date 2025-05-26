[global::System.Serializable]
[global::UnityEngine.AddComponentMenu("Image Effects/Camera/Camera Motion Blur")]
[global::UnityEngine.ExecuteInEditMode]
[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Camera))]
public class CameraMotionBlur : PostEffectsBase
{
	[global::System.Serializable]
	public enum MotionBlurFilter
	{
		CameraMotion = 0,
		LocalBlur = 1,
		Reconstruction = 2,
		ReconstructionDX11 = 3,
		ReconstructionDisc = 4
	}

	[global::System.NonSerialized]
	public static int MAX_RADIUS = 10;

	public CameraMotionBlur.MotionBlurFilter filterType;

	public bool preview;

	public global::UnityEngine.Vector3 previewScale;

	public float movementScale;

	public float rotationScale;

	public float maxVelocity;

	public float minVelocity;

	public float velocityScale;

	public float softZDistance;

	public int velocityDownsample;

	public global::UnityEngine.LayerMask excludeLayers;

	private global::UnityEngine.GameObject tmpCam;

	public global::UnityEngine.Shader shader;

	public global::UnityEngine.Shader dx11MotionBlurShader;

	public global::UnityEngine.Shader replacementClear;

	private global::UnityEngine.Material motionBlurMaterial;

	private global::UnityEngine.Material dx11MotionBlurMaterial;

	public global::UnityEngine.Texture2D noiseTexture;

	public float jitter;

	public bool showVelocity;

	public float showVelocityScale;

	private global::UnityEngine.Matrix4x4 currentViewProjMat;

	private global::UnityEngine.Matrix4x4 prevViewProjMat;

	private int prevFrameCount;

	private bool wasActive;

	private global::UnityEngine.Vector3 prevFrameForward;

	private global::UnityEngine.Vector3 prevFrameRight;

	private global::UnityEngine.Vector3 prevFrameUp;

	private global::UnityEngine.Vector3 prevFramePos;

	public CameraMotionBlur()
	{
		filterType = CameraMotionBlur.MotionBlurFilter.Reconstruction;
		previewScale = global::UnityEngine.Vector3.one;
		rotationScale = 1f;
		maxVelocity = 8f;
		minVelocity = 0.1f;
		velocityScale = 0.375f;
		softZDistance = 0.005f;
		velocityDownsample = 1;
		jitter = 0.05f;
		showVelocityScale = 1f;
		prevFrameForward = global::UnityEngine.Vector3.forward;
		prevFrameRight = global::UnityEngine.Vector3.right;
		prevFrameUp = global::UnityEngine.Vector3.up;
		prevFramePos = global::UnityEngine.Vector3.zero;
	}

	private void CalculateViewProjection()
	{
		global::UnityEngine.Matrix4x4 worldToCameraMatrix = GetComponent<UnityEngine.Camera>().worldToCameraMatrix;
		global::UnityEngine.Matrix4x4 gPUProjectionMatrix = global::UnityEngine.GL.GetGPUProjectionMatrix(GetComponent<UnityEngine.Camera>().projectionMatrix, true);
		currentViewProjMat = gPUProjectionMatrix * worldToCameraMatrix;
	}

	public override void Start()
	{
		CheckResources();
		wasActive = gameObject.activeInHierarchy;
		CalculateViewProjection();
		Remember();
		wasActive = false;
	}

	public override void OnEnable()
	{
		GetComponent<UnityEngine.Camera>().depthTextureMode |= global::UnityEngine.DepthTextureMode.Depth;
	}

	public virtual void OnDisable()
	{
		if (null != motionBlurMaterial)
		{
			global::UnityEngine.Object.DestroyImmediate(motionBlurMaterial);
			motionBlurMaterial = null;
		}
		if (null != dx11MotionBlurMaterial)
		{
			global::UnityEngine.Object.DestroyImmediate(dx11MotionBlurMaterial);
			dx11MotionBlurMaterial = null;
		}
		if (null != tmpCam)
		{
			global::UnityEngine.Object.DestroyImmediate(tmpCam);
			tmpCam = null;
		}
	}

	public override bool CheckResources()
	{
		CheckSupport(true, true);
		motionBlurMaterial = CheckShaderAndCreateMaterial(shader, motionBlurMaterial);
		if (supportDX11 && filterType == CameraMotionBlur.MotionBlurFilter.ReconstructionDX11)
		{
			dx11MotionBlurMaterial = CheckShaderAndCreateMaterial(dx11MotionBlurShader, dx11MotionBlurMaterial);
		}
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
		if (filterType == CameraMotionBlur.MotionBlurFilter.CameraMotion)
		{
			StartFrame();
		}
		global::UnityEngine.RenderTextureFormat format = ((!global::UnityEngine.SystemInfo.SupportsRenderTextureFormat(global::UnityEngine.RenderTextureFormat.RGHalf)) ? global::UnityEngine.RenderTextureFormat.ARGBHalf : global::UnityEngine.RenderTextureFormat.RGHalf);
		global::UnityEngine.RenderTexture temporary = global::UnityEngine.RenderTexture.GetTemporary(divRoundUp(source.width, velocityDownsample), divRoundUp(source.height, velocityDownsample), 0, format);
		int num = 1;
		int num2 = 1;
		maxVelocity = global::UnityEngine.Mathf.Max(2f, maxVelocity);
		float num3 = maxVelocity;
		bool flag = false;
		if (filterType == CameraMotionBlur.MotionBlurFilter.ReconstructionDX11 && dx11MotionBlurMaterial == null)
		{
			flag = true;
		}
		if (filterType == CameraMotionBlur.MotionBlurFilter.Reconstruction || flag || filterType == CameraMotionBlur.MotionBlurFilter.ReconstructionDisc)
		{
			maxVelocity = global::UnityEngine.Mathf.Min(maxVelocity, MAX_RADIUS);
			num = divRoundUp(temporary.width, (int)maxVelocity);
			num2 = divRoundUp(temporary.height, (int)maxVelocity);
			num3 = temporary.width / num;
		}
		else
		{
			num = divRoundUp(temporary.width, (int)maxVelocity);
			num2 = divRoundUp(temporary.height, (int)maxVelocity);
			num3 = temporary.width / num;
		}
		global::UnityEngine.RenderTexture temporary2 = global::UnityEngine.RenderTexture.GetTemporary(num, num2, 0, format);
		global::UnityEngine.RenderTexture temporary3 = global::UnityEngine.RenderTexture.GetTemporary(num, num2, 0, format);
		temporary.filterMode = global::UnityEngine.FilterMode.Point;
		temporary2.filterMode = global::UnityEngine.FilterMode.Point;
		temporary3.filterMode = global::UnityEngine.FilterMode.Point;
		if ((bool)noiseTexture)
		{
			noiseTexture.filterMode = global::UnityEngine.FilterMode.Point;
		}
		source.wrapMode = global::UnityEngine.TextureWrapMode.Clamp;
		temporary.wrapMode = global::UnityEngine.TextureWrapMode.Clamp;
		temporary3.wrapMode = global::UnityEngine.TextureWrapMode.Clamp;
		temporary2.wrapMode = global::UnityEngine.TextureWrapMode.Clamp;
		CalculateViewProjection();
		if (gameObject.activeInHierarchy && !wasActive)
		{
			Remember();
		}
		wasActive = gameObject.activeInHierarchy;
		global::UnityEngine.Matrix4x4 matrix4x = global::UnityEngine.Matrix4x4.Inverse(currentViewProjMat);
		motionBlurMaterial.SetMatrix("_InvViewProj", matrix4x);
		motionBlurMaterial.SetMatrix("_PrevViewProj", prevViewProjMat);
		motionBlurMaterial.SetMatrix("_ToPrevViewProjCombined", prevViewProjMat * matrix4x);
		motionBlurMaterial.SetFloat("_MaxVelocity", num3);
		motionBlurMaterial.SetFloat("_MaxRadiusOrKInPaper", num3);
		motionBlurMaterial.SetFloat("_MinVelocity", minVelocity);
		motionBlurMaterial.SetFloat("_VelocityScale", velocityScale);
		motionBlurMaterial.SetFloat("_Jitter", jitter);
		motionBlurMaterial.SetTexture("_NoiseTex", noiseTexture);
		motionBlurMaterial.SetTexture("_VelTex", temporary);
		motionBlurMaterial.SetTexture("_NeighbourMaxTex", temporary3);
		motionBlurMaterial.SetTexture("_TileTexDebug", temporary2);
		if (preview)
		{
			global::UnityEngine.Matrix4x4 worldToCameraMatrix = this.GetComponent<UnityEngine.Camera>().worldToCameraMatrix;
			global::UnityEngine.Matrix4x4 identity = global::UnityEngine.Matrix4x4.identity;
			identity.SetTRS(previewScale * 0.3333f, global::UnityEngine.Quaternion.identity, global::UnityEngine.Vector3.one);
			global::UnityEngine.Matrix4x4 gPUProjectionMatrix = global::UnityEngine.GL.GetGPUProjectionMatrix(this.GetComponent<UnityEngine.Camera>().projectionMatrix, true);
			prevViewProjMat = gPUProjectionMatrix * identity * worldToCameraMatrix;
			motionBlurMaterial.SetMatrix("_PrevViewProj", prevViewProjMat);
			motionBlurMaterial.SetMatrix("_ToPrevViewProjCombined", prevViewProjMat * matrix4x);
		}
		if (filterType == CameraMotionBlur.MotionBlurFilter.CameraMotion)
		{
			global::UnityEngine.Vector4 zero = global::UnityEngine.Vector4.zero;
			float num4 = global::UnityEngine.Vector3.Dot(transform.up, global::UnityEngine.Vector3.up);
			global::UnityEngine.Vector3 rhs = prevFramePos - transform.position;
			float magnitude = rhs.magnitude;
			float num5 = 1f;
			num5 = global::UnityEngine.Vector3.Angle(transform.up, prevFrameUp) / this.GetComponent<UnityEngine.Camera>().fieldOfView * ((float)source.width * 0.75f);
			zero.x = rotationScale * num5;
			num5 = global::UnityEngine.Vector3.Angle(transform.forward, prevFrameForward) / this.GetComponent<UnityEngine.Camera>().fieldOfView * ((float)source.width * 0.75f);
			zero.y = rotationScale * num4 * num5;
			num5 = global::UnityEngine.Vector3.Angle(transform.forward, prevFrameForward) / this.GetComponent<UnityEngine.Camera>().fieldOfView * ((float)source.width * 0.75f);
			zero.z = rotationScale * (1f - num4) * num5;
			if (!(magnitude <= float.Epsilon) && !(movementScale <= float.Epsilon))
			{
				zero.w = movementScale * global::UnityEngine.Vector3.Dot(transform.forward, rhs) * ((float)source.width * 0.5f);
				zero.x += movementScale * global::UnityEngine.Vector3.Dot(transform.up, rhs) * ((float)source.width * 0.5f);
				zero.y += movementScale * global::UnityEngine.Vector3.Dot(transform.right, rhs) * ((float)source.width * 0.5f);
			}
			if (preview)
			{
				motionBlurMaterial.SetVector("_BlurDirectionPacked", new global::UnityEngine.Vector4(previewScale.y, previewScale.x, 0f, previewScale.z) * 0.5f * this.GetComponent<UnityEngine.Camera>().fieldOfView);
			}
			else
			{
				motionBlurMaterial.SetVector("_BlurDirectionPacked", zero);
			}
		}
		else
		{
			global::UnityEngine.Graphics.Blit(source, temporary, motionBlurMaterial, 0);
			global::UnityEngine.Camera camera = null;
			if (excludeLayers.value != 0)
			{
				camera = GetTmpCam();
			}
			if ((bool)camera && excludeLayers.value != 0 && (bool)replacementClear && replacementClear.isSupported)
			{
				camera.targetTexture = temporary;
				camera.cullingMask = excludeLayers;
				camera.RenderWithShader(replacementClear, string.Empty);
			}
		}
		if (!preview && global::UnityEngine.Time.frameCount != prevFrameCount)
		{
			prevFrameCount = global::UnityEngine.Time.frameCount;
			Remember();
		}
		source.filterMode = global::UnityEngine.FilterMode.Bilinear;
		if (showVelocity)
		{
			motionBlurMaterial.SetFloat("_DisplayVelocityScale", showVelocityScale);
			global::UnityEngine.Graphics.Blit(temporary, destination, motionBlurMaterial, 1);
		}
		else if (filterType == CameraMotionBlur.MotionBlurFilter.ReconstructionDX11 && !flag)
		{
			dx11MotionBlurMaterial.SetFloat("_MinVelocity", minVelocity);
			dx11MotionBlurMaterial.SetFloat("_VelocityScale", velocityScale);
			dx11MotionBlurMaterial.SetFloat("_Jitter", jitter);
			dx11MotionBlurMaterial.SetTexture("_NoiseTex", noiseTexture);
			dx11MotionBlurMaterial.SetTexture("_VelTex", temporary);
			dx11MotionBlurMaterial.SetTexture("_NeighbourMaxTex", temporary3);
			dx11MotionBlurMaterial.SetFloat("_SoftZDistance", global::UnityEngine.Mathf.Max(0.00025f, softZDistance));
			dx11MotionBlurMaterial.SetFloat("_MaxRadiusOrKInPaper", num3);
			global::UnityEngine.Graphics.Blit(temporary, temporary2, dx11MotionBlurMaterial, 0);
			global::UnityEngine.Graphics.Blit(temporary2, temporary3, dx11MotionBlurMaterial, 1);
			global::UnityEngine.Graphics.Blit(source, destination, dx11MotionBlurMaterial, 2);
		}
		else if (filterType == CameraMotionBlur.MotionBlurFilter.Reconstruction || flag)
		{
			motionBlurMaterial.SetFloat("_SoftZDistance", global::UnityEngine.Mathf.Max(0.00025f, softZDistance));
			global::UnityEngine.Graphics.Blit(temporary, temporary2, motionBlurMaterial, 2);
			global::UnityEngine.Graphics.Blit(temporary2, temporary3, motionBlurMaterial, 3);
			global::UnityEngine.Graphics.Blit(source, destination, motionBlurMaterial, 4);
		}
		else if (filterType == CameraMotionBlur.MotionBlurFilter.CameraMotion)
		{
			global::UnityEngine.Graphics.Blit(source, destination, motionBlurMaterial, 6);
		}
		else if (filterType == CameraMotionBlur.MotionBlurFilter.ReconstructionDisc)
		{
			motionBlurMaterial.SetFloat("_SoftZDistance", global::UnityEngine.Mathf.Max(0.00025f, softZDistance));
			global::UnityEngine.Graphics.Blit(temporary, temporary2, motionBlurMaterial, 2);
			global::UnityEngine.Graphics.Blit(temporary2, temporary3, motionBlurMaterial, 3);
			global::UnityEngine.Graphics.Blit(source, destination, motionBlurMaterial, 7);
		}
		else
		{
			global::UnityEngine.Graphics.Blit(source, destination, motionBlurMaterial, 5);
		}
		global::UnityEngine.RenderTexture.ReleaseTemporary(temporary);
		global::UnityEngine.RenderTexture.ReleaseTemporary(temporary2);
		global::UnityEngine.RenderTexture.ReleaseTemporary(temporary3);
	}

	public virtual void Remember()
	{
		prevViewProjMat = currentViewProjMat;
		prevFrameForward = transform.forward;
		prevFrameRight = transform.right;
		prevFrameUp = transform.up;
		prevFramePos = transform.position;
	}

	public virtual global::UnityEngine.Camera GetTmpCam()
	{
		if (tmpCam == null)
		{
			string text = "_" + GetComponent<UnityEngine.Camera>().name + "_MotionBlurTmpCam";
			global::UnityEngine.GameObject gameObject = global::UnityEngine.GameObject.Find(text);
			if (null == gameObject)
			{
				tmpCam = new global::UnityEngine.GameObject(text, typeof(global::UnityEngine.Camera));
			}
			else
			{
				tmpCam = gameObject;
			}
		}
		tmpCam.hideFlags = global::UnityEngine.HideFlags.DontSave;
		tmpCam.transform.position = GetComponent<UnityEngine.Camera>().transform.position;
		tmpCam.transform.rotation = GetComponent<UnityEngine.Camera>().transform.rotation;
		tmpCam.transform.localScale = GetComponent<UnityEngine.Camera>().transform.localScale;
		tmpCam.GetComponent<UnityEngine.Camera>().CopyFrom(GetComponent<UnityEngine.Camera>());
		tmpCam.GetComponent<UnityEngine.Camera>().enabled = false;
		tmpCam.GetComponent<UnityEngine.Camera>().depthTextureMode = global::UnityEngine.DepthTextureMode.None;
		tmpCam.GetComponent<UnityEngine.Camera>().clearFlags = global::UnityEngine.CameraClearFlags.Nothing;
		return tmpCam.GetComponent<UnityEngine.Camera>();
	}

	public virtual void StartFrame()
	{
		prevFramePos = global::UnityEngine.Vector3.Slerp(prevFramePos, transform.position, 0.75f);
	}

	public virtual int divRoundUp(int x, int d)
	{
		return (x + d - 1) / d;
	}

	public override void Main()
	{
	}
}
