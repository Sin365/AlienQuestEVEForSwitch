[global::System.Serializable]
[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Camera))]
[global::UnityEngine.ExecuteInEditMode]
public class PostEffectsBase : global::UnityEngine.MonoBehaviour
{
	protected bool supportHDRTextures;

	protected bool supportDX11;

	protected bool isSupported;

	public PostEffectsBase()
	{
		supportHDRTextures = true;
		isSupported = true;
	}

	public virtual global::UnityEngine.Material CheckShaderAndCreateMaterial(global::UnityEngine.Shader s, global::UnityEngine.Material m2Create)
	{
		object result;
		if (!s)
		{
			global::UnityEngine.Debug.Log("Missing shader in " + ToString());
			enabled = false;
			result = null;
		}
		else if (s.isSupported && (bool)m2Create && m2Create.shader == s)
		{
			result = m2Create;
		}
		else if (!s.isSupported)
		{
			NotSupported();
			global::UnityEngine.Debug.Log("The shader " + s.ToString() + " on effect " + ToString() + " is not supported on this platform!");
			result = null;
		}
		else
		{
			m2Create = new global::UnityEngine.Material(s);
			m2Create.hideFlags = global::UnityEngine.HideFlags.DontSave;
			result = ((!m2Create) ? null : m2Create);
		}
		return (global::UnityEngine.Material)result;
	}

	public virtual global::UnityEngine.Material CreateMaterial(global::UnityEngine.Shader s, global::UnityEngine.Material m2Create)
	{
		object result;
		if (!s)
		{
			global::UnityEngine.Debug.Log("Missing shader in " + ToString());
			result = null;
		}
		else if ((bool)m2Create && m2Create.shader == s && s.isSupported)
		{
			result = m2Create;
		}
		else if (!s.isSupported)
		{
			result = null;
		}
		else
		{
			m2Create = new global::UnityEngine.Material(s);
			m2Create.hideFlags = global::UnityEngine.HideFlags.DontSave;
			result = ((!m2Create) ? null : m2Create);
		}
		return (global::UnityEngine.Material)result;
	}

	public virtual void OnEnable()
	{
		isSupported = true;
	}

	public virtual bool CheckSupport()
	{
		return CheckSupport(false);
	}

	public virtual bool CheckResources()
	{
		global::UnityEngine.Debug.LogWarning("CheckResources () for " + ToString() + " should be overwritten.");
		return isSupported;
	}

	public virtual void Start()
	{
		CheckResources();
	}

	public virtual bool CheckSupport(bool needDepth)
	{
		isSupported = true;
		supportHDRTextures = global::UnityEngine.SystemInfo.SupportsRenderTextureFormat(global::UnityEngine.RenderTextureFormat.ARGBHalf);
		bool num = global::UnityEngine.SystemInfo.graphicsShaderLevel >= 50;
		if (num)
		{
			num = global::UnityEngine.SystemInfo.supportsComputeShaders;
		}
		supportDX11 = num;
		int result;
		if (!global::UnityEngine.SystemInfo.supportsImageEffects || !global::UnityEngine.SystemInfo.supportsRenderTextures)
		{
			NotSupported();
			result = 0;
		}
		else if (needDepth && !global::UnityEngine.SystemInfo.SupportsRenderTextureFormat(global::UnityEngine.RenderTextureFormat.Depth))
		{
			NotSupported();
			result = 0;
		}
		else
		{
			if (needDepth)
			{
				GetComponent<UnityEngine.Camera>().depthTextureMode |= global::UnityEngine.DepthTextureMode.Depth;
			}
			result = 1;
		}
		return (byte)result != 0;
	}

	public virtual bool CheckSupport(bool needDepth, bool needHdr)
	{
		int result;
		if (!CheckSupport(needDepth))
		{
			result = 0;
		}
		else if (needHdr && !supportHDRTextures)
		{
			NotSupported();
			result = 0;
		}
		else
		{
			result = 1;
		}
		return (byte)result != 0;
	}

	public virtual bool Dx11Support()
	{
		return supportDX11;
	}

	public virtual void ReportAutoDisable()
	{
		global::UnityEngine.Debug.LogWarning("The image effect " + ToString() + " has been disabled as it's not supported on the current platform.");
	}

	public virtual bool CheckShader(global::UnityEngine.Shader s)
	{
		global::UnityEngine.Debug.Log("The shader " + s.ToString() + " on effect " + ToString() + " is not part of the Unity 3.2+ effects suite anymore. For best performance and quality, please ensure you are using the latest Standard Assets Image Effects (Pro only) package.");
		int result;
		if (!s.isSupported)
		{
			NotSupported();
			result = 0;
		}
		else
		{
			result = 0;
		}
		return (byte)result != 0;
	}

	public virtual void NotSupported()
	{
		enabled = false;
		isSupported = false;
	}

	public virtual void DrawBorder(global::UnityEngine.RenderTexture dest, global::UnityEngine.Material material)
	{
		float num = default(float);
		float num2 = default(float);
		float num3 = default(float);
		float num4 = default(float);
		global::UnityEngine.RenderTexture.active = dest;
		bool flag = true;
		global::UnityEngine.GL.PushMatrix();
		global::UnityEngine.GL.LoadOrtho();
		for (int i = 0; i < material.passCount; i++)
		{
			material.SetPass(i);
			float num5 = default(float);
			float num6 = default(float);
			if (flag)
			{
				num5 = 1f;
				num6 = 0f;
			}
			else
			{
				num5 = 0f;
				num6 = 1f;
			}
			num = 0f;
			num2 = 0f + 1f / ((float)dest.width * 1f);
			num3 = 0f;
			num4 = 1f;
			global::UnityEngine.GL.Begin(7);
			global::UnityEngine.GL.TexCoord2(0f, num5);
			global::UnityEngine.GL.Vertex3(num, num3, 0.1f);
			global::UnityEngine.GL.TexCoord2(1f, num5);
			global::UnityEngine.GL.Vertex3(num2, num3, 0.1f);
			global::UnityEngine.GL.TexCoord2(1f, num6);
			global::UnityEngine.GL.Vertex3(num2, num4, 0.1f);
			global::UnityEngine.GL.TexCoord2(0f, num6);
			global::UnityEngine.GL.Vertex3(num, num4, 0.1f);
			num = 1f - 1f / ((float)dest.width * 1f);
			num2 = 1f;
			num3 = 0f;
			num4 = 1f;
			global::UnityEngine.GL.TexCoord2(0f, num5);
			global::UnityEngine.GL.Vertex3(num, num3, 0.1f);
			global::UnityEngine.GL.TexCoord2(1f, num5);
			global::UnityEngine.GL.Vertex3(num2, num3, 0.1f);
			global::UnityEngine.GL.TexCoord2(1f, num6);
			global::UnityEngine.GL.Vertex3(num2, num4, 0.1f);
			global::UnityEngine.GL.TexCoord2(0f, num6);
			global::UnityEngine.GL.Vertex3(num, num4, 0.1f);
			num = 0f;
			num2 = 1f;
			num3 = 0f;
			num4 = 0f + 1f / ((float)dest.height * 1f);
			global::UnityEngine.GL.TexCoord2(0f, num5);
			global::UnityEngine.GL.Vertex3(num, num3, 0.1f);
			global::UnityEngine.GL.TexCoord2(1f, num5);
			global::UnityEngine.GL.Vertex3(num2, num3, 0.1f);
			global::UnityEngine.GL.TexCoord2(1f, num6);
			global::UnityEngine.GL.Vertex3(num2, num4, 0.1f);
			global::UnityEngine.GL.TexCoord2(0f, num6);
			global::UnityEngine.GL.Vertex3(num, num4, 0.1f);
			num = 0f;
			num2 = 1f;
			num3 = 1f - 1f / ((float)dest.height * 1f);
			num4 = 1f;
			global::UnityEngine.GL.TexCoord2(0f, num5);
			global::UnityEngine.GL.Vertex3(num, num3, 0.1f);
			global::UnityEngine.GL.TexCoord2(1f, num5);
			global::UnityEngine.GL.Vertex3(num2, num3, 0.1f);
			global::UnityEngine.GL.TexCoord2(1f, num6);
			global::UnityEngine.GL.Vertex3(num2, num4, 0.1f);
			global::UnityEngine.GL.TexCoord2(0f, num6);
			global::UnityEngine.GL.Vertex3(num, num4, 0.1f);
			global::UnityEngine.GL.End();
		}
		global::UnityEngine.GL.PopMatrix();
	}

	public virtual void Main()
	{
	}
}
