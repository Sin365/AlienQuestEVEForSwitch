[global::System.Serializable]
[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Camera))]
[global::UnityEngine.AddComponentMenu("Image Effects/Rendering/Global Fog")]
[global::UnityEngine.ExecuteInEditMode]
public class GlobalFog : PostEffectsBase
{
	[global::System.Serializable]
	public enum FogMode
	{
		AbsoluteYAndDistance = 0,
		AbsoluteY = 1,
		Distance = 2,
		RelativeYAndDistance = 3
	}

	public GlobalFog.FogMode fogMode;

	private float CAMERA_NEAR;

	private float CAMERA_FAR;

	private float CAMERA_FOV;

	private float CAMERA_ASPECT_RATIO;

	public float startDistance;

	public float globalDensity;

	public float heightScale;

	public float height;

	public global::UnityEngine.Color globalFogColor;

	public global::UnityEngine.Shader fogShader;

	private global::UnityEngine.Material fogMaterial;

	public GlobalFog()
	{
		fogMode = GlobalFog.FogMode.AbsoluteYAndDistance;
		CAMERA_NEAR = 0.5f;
		CAMERA_FAR = 50f;
		CAMERA_FOV = 60f;
		CAMERA_ASPECT_RATIO = 1.333333f;
		startDistance = 200f;
		globalDensity = 1f;
		heightScale = 100f;
		globalFogColor = global::UnityEngine.Color.grey;
	}

	public override bool CheckResources()
	{
		CheckSupport(true);
		fogMaterial = CheckShaderAndCreateMaterial(fogShader, fogMaterial);
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
		CAMERA_NEAR = GetComponent<UnityEngine.Camera>().nearClipPlane;
		CAMERA_FAR = GetComponent<UnityEngine.Camera>().farClipPlane;
		CAMERA_FOV = GetComponent<UnityEngine.Camera>().fieldOfView;
		CAMERA_ASPECT_RATIO = GetComponent<UnityEngine.Camera>().aspect;
		global::UnityEngine.Matrix4x4 identity = global::UnityEngine.Matrix4x4.identity;
		global::UnityEngine.Vector4 vector = default(global::UnityEngine.Vector4);
		global::UnityEngine.Vector3 vector2 = default(global::UnityEngine.Vector3);
		float num = CAMERA_FOV * 0.5f;
		global::UnityEngine.Vector3 vector3 = GetComponent<UnityEngine.Camera>().transform.right * CAMERA_NEAR * global::UnityEngine.Mathf.Tan(num * ((float)global::System.Math.PI / 180f)) * CAMERA_ASPECT_RATIO;
		global::UnityEngine.Vector3 vector4 = GetComponent<UnityEngine.Camera>().transform.up * CAMERA_NEAR * global::UnityEngine.Mathf.Tan(num * ((float)global::System.Math.PI / 180f));
		global::UnityEngine.Vector3 vector5 = GetComponent<UnityEngine.Camera>().transform.forward * CAMERA_NEAR - vector3 + vector4;
		float num2 = vector5.magnitude * CAMERA_FAR / CAMERA_NEAR;
		vector5.Normalize();
		vector5 *= num2;
		global::UnityEngine.Vector3 vector6 = GetComponent<UnityEngine.Camera>().transform.forward * CAMERA_NEAR + vector3 + vector4;
		vector6.Normalize();
		vector6 *= num2;
		global::UnityEngine.Vector3 vector7 = GetComponent<UnityEngine.Camera>().transform.forward * CAMERA_NEAR + vector3 - vector4;
		vector7.Normalize();
		vector7 *= num2;
		global::UnityEngine.Vector3 vector8 = GetComponent<UnityEngine.Camera>().transform.forward * CAMERA_NEAR - vector3 - vector4;
		vector8.Normalize();
		vector8 *= num2;
		identity.SetRow(0, vector5);
		identity.SetRow(1, vector6);
		identity.SetRow(2, vector7);
		identity.SetRow(3, vector8);
		fogMaterial.SetMatrix("_FrustumCornersWS", identity);
		fogMaterial.SetVector("_CameraWS", GetComponent<UnityEngine.Camera>().transform.position);
		fogMaterial.SetVector("_StartDistance", new global::UnityEngine.Vector4(1f / startDistance, num2 - startDistance));
		fogMaterial.SetVector("_Y", new global::UnityEngine.Vector4(height, 1f / heightScale));
		fogMaterial.SetFloat("_GlobalDensity", globalDensity * 0.01f);
		fogMaterial.SetColor("_FogColor", globalFogColor);
		CustomGraphicsBlit(source, destination, fogMaterial, (int)fogMode);
	}

	public static void CustomGraphicsBlit(global::UnityEngine.RenderTexture source, global::UnityEngine.RenderTexture dest, global::UnityEngine.Material fxMaterial, int passNr)
	{
		global::UnityEngine.RenderTexture.active = dest;
		fxMaterial.SetTexture("_MainTex", source);
		global::UnityEngine.GL.PushMatrix();
		global::UnityEngine.GL.LoadOrtho();
		fxMaterial.SetPass(passNr);
		global::UnityEngine.GL.Begin(7);
		global::UnityEngine.GL.MultiTexCoord2(0, 0f, 0f);
		global::UnityEngine.GL.Vertex3(0f, 0f, 3f);
		global::UnityEngine.GL.MultiTexCoord2(0, 1f, 0f);
		global::UnityEngine.GL.Vertex3(1f, 0f, 2f);
		global::UnityEngine.GL.MultiTexCoord2(0, 1f, 1f);
		global::UnityEngine.GL.Vertex3(1f, 1f, 1f);
		global::UnityEngine.GL.MultiTexCoord2(0, 0f, 1f);
		global::UnityEngine.GL.Vertex3(0f, 1f, 0f);
		global::UnityEngine.GL.End();
		global::UnityEngine.GL.PopMatrix();
	}

	public override void Main()
	{
	}
}
