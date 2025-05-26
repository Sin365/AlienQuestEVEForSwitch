[global::System.Serializable]
[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Camera))]
[global::UnityEngine.AddComponentMenu("Image Effects/Camera/Depth of Field (Lens Blur, Scatter, DX11)")]
[global::UnityEngine.ExecuteInEditMode]
public class DepthOfFieldScatter : PostEffectsBase
{
	[global::System.Serializable]
	public enum BlurType
	{
		DiscBlur = 0,
		DX11 = 1
	}

	[global::System.Serializable]
	public enum BlurSampleCount
	{
		Low = 0,
		Medium = 1,
		High = 2
	}

	public bool visualizeFocus;

	public float focalLength;

	public float focalSize;

	public float aperture;

	public global::UnityEngine.Transform focalTransform;

	public float maxBlurSize;

	public bool highResolution;

	public DepthOfFieldScatter.BlurType blurType;

	public DepthOfFieldScatter.BlurSampleCount blurSampleCount;

	public bool nearBlur;

	public float foregroundOverlap;

	public global::UnityEngine.Shader dofHdrShader;

	private global::UnityEngine.Material dofHdrMaterial;

	public global::UnityEngine.Shader dx11BokehShader;

	private global::UnityEngine.Material dx11bokehMaterial;

	public float dx11BokehThreshhold;

	public float dx11SpawnHeuristic;

	public global::UnityEngine.Texture2D dx11BokehTexture;

	public float dx11BokehScale;

	public float dx11BokehIntensity;

	private float focalDistance01;

	private global::UnityEngine.ComputeBuffer cbDrawArgs;

	private global::UnityEngine.ComputeBuffer cbPoints;

	private float internalBlurWidth;

	public DepthOfFieldScatter()
	{
		focalLength = 10f;
		focalSize = 0.05f;
		aperture = 11.5f;
		maxBlurSize = 2f;
		blurType = DepthOfFieldScatter.BlurType.DiscBlur;
		blurSampleCount = DepthOfFieldScatter.BlurSampleCount.High;
		foregroundOverlap = 1f;
		dx11BokehThreshhold = 0.5f;
		dx11SpawnHeuristic = 0.0875f;
		dx11BokehScale = 1.2f;
		dx11BokehIntensity = 2.5f;
		focalDistance01 = 10f;
		internalBlurWidth = 1f;
	}

	public override bool CheckResources()
	{
		CheckSupport(true);
		dofHdrMaterial = CheckShaderAndCreateMaterial(dofHdrShader, dofHdrMaterial);
		if (supportDX11 && blurType == DepthOfFieldScatter.BlurType.DX11)
		{
			dx11bokehMaterial = CheckShaderAndCreateMaterial(dx11BokehShader, dx11bokehMaterial);
			CreateComputeResources();
		}
		if (!isSupported)
		{
			ReportAutoDisable();
		}
		return isSupported;
	}

	public override void OnEnable()
	{
		GetComponent<UnityEngine.Camera>().depthTextureMode |= global::UnityEngine.DepthTextureMode.Depth;
	}

	public virtual void OnDisable()
	{
		ReleaseComputeResources();
		if ((bool)dofHdrMaterial)
		{
			global::UnityEngine.Object.DestroyImmediate(dofHdrMaterial);
		}
		dofHdrMaterial = null;
		if ((bool)dx11bokehMaterial)
		{
			global::UnityEngine.Object.DestroyImmediate(dx11bokehMaterial);
		}
		dx11bokehMaterial = null;
	}

	public virtual void ReleaseComputeResources()
	{
		if (cbDrawArgs != null)
		{
			cbDrawArgs.Release();
		}
		cbDrawArgs = null;
		if (cbPoints != null)
		{
			cbPoints.Release();
		}
		cbPoints = null;
	}

	public virtual void CreateComputeResources()
	{
		if (global::Boo.Lang.Runtime.RuntimeServices.EqualityOperator(cbDrawArgs, null))
		{
			cbDrawArgs = new global::UnityEngine.ComputeBuffer(1, 16, global::UnityEngine.ComputeBufferType.IndirectArguments);
			int[] data = new int[4] { 0, 1, 0, 0 };
			cbDrawArgs.SetData(data);
		}
		if (global::Boo.Lang.Runtime.RuntimeServices.EqualityOperator(cbPoints, null))
		{
			cbPoints = new global::UnityEngine.ComputeBuffer(90000, 28, global::UnityEngine.ComputeBufferType.Append);
		}
	}

	public virtual float FocalDistance01(float worldDist)
	{
		return GetComponent<UnityEngine.Camera>().WorldToViewportPoint((worldDist - GetComponent<UnityEngine.Camera>().nearClipPlane) * GetComponent<UnityEngine.Camera>().transform.forward + GetComponent<UnityEngine.Camera>().transform.position).z / (GetComponent<UnityEngine.Camera>().farClipPlane - GetComponent<UnityEngine.Camera>().nearClipPlane);
	}

	private void WriteCoc(global::UnityEngine.RenderTexture fromTo, bool fgDilate)
	{
		dofHdrMaterial.SetTexture("_FgOverlap", null);
		if (nearBlur && fgDilate)
		{
			int width = fromTo.width / 2;
			int height = fromTo.height / 2;
			global::UnityEngine.RenderTexture temporary = global::UnityEngine.RenderTexture.GetTemporary(width, height, 0, fromTo.format);
			global::UnityEngine.Graphics.Blit(fromTo, temporary, dofHdrMaterial, 4);
			float num = internalBlurWidth * foregroundOverlap;
			dofHdrMaterial.SetVector("_Offsets", new global::UnityEngine.Vector4(0f, num, 0f, num));
			global::UnityEngine.RenderTexture temporary2 = global::UnityEngine.RenderTexture.GetTemporary(width, height, 0, fromTo.format);
			global::UnityEngine.Graphics.Blit(temporary, temporary2, dofHdrMaterial, 2);
			global::UnityEngine.RenderTexture.ReleaseTemporary(temporary);
			dofHdrMaterial.SetVector("_Offsets", new global::UnityEngine.Vector4(num, 0f, 0f, num));
			temporary = global::UnityEngine.RenderTexture.GetTemporary(width, height, 0, fromTo.format);
			global::UnityEngine.Graphics.Blit(temporary2, temporary, dofHdrMaterial, 2);
			global::UnityEngine.RenderTexture.ReleaseTemporary(temporary2);
			dofHdrMaterial.SetTexture("_FgOverlap", temporary);
			fromTo.MarkRestoreExpected();
			global::UnityEngine.Graphics.Blit(fromTo, fromTo, dofHdrMaterial, 13);
			global::UnityEngine.RenderTexture.ReleaseTemporary(temporary);
		}
		else
		{
			global::UnityEngine.Graphics.Blit(fromTo, fromTo, dofHdrMaterial, 0);
		}
	}

	public virtual void OnRenderImage(global::UnityEngine.RenderTexture source, global::UnityEngine.RenderTexture destination)
	{
		if (!CheckResources())
		{
			global::UnityEngine.Graphics.Blit(source, destination);
			return;
		}
		if (!(aperture >= 0f))
		{
			aperture = 0f;
		}
		if (!(maxBlurSize >= 0.1f))
		{
			maxBlurSize = 0.1f;
		}
		focalSize = global::UnityEngine.Mathf.Clamp(focalSize, 0f, 2f);
		internalBlurWidth = global::UnityEngine.Mathf.Max(maxBlurSize, 0f);
		focalDistance01 = ((!focalTransform) ? FocalDistance01(focalLength) : (GetComponent<UnityEngine.Camera>().WorldToViewportPoint(focalTransform.position).z / GetComponent<UnityEngine.Camera>().farClipPlane));
		dofHdrMaterial.SetVector("_CurveParams", new global::UnityEngine.Vector4(1f, focalSize, aperture / 10f, focalDistance01));
		global::UnityEngine.RenderTexture renderTexture = null;
		global::UnityEngine.RenderTexture renderTexture2 = null;
		global::UnityEngine.RenderTexture renderTexture3 = null;
		global::UnityEngine.RenderTexture renderTexture4 = null;
		float num = internalBlurWidth * foregroundOverlap;
		if (visualizeFocus)
		{
			WriteCoc(source, true);
			global::UnityEngine.Graphics.Blit(source, destination, dofHdrMaterial, 16);
		}
		else if (blurType == DepthOfFieldScatter.BlurType.DX11 && (bool)dx11bokehMaterial)
		{
			if (highResolution)
			{
				internalBlurWidth = ((internalBlurWidth >= 0.1f) ? internalBlurWidth : 0.1f);
				num = internalBlurWidth * foregroundOverlap;
				renderTexture = global::UnityEngine.RenderTexture.GetTemporary(source.width, source.height, 0, source.format);
				global::UnityEngine.RenderTexture temporary = global::UnityEngine.RenderTexture.GetTemporary(source.width, source.height, 0, source.format);
				WriteCoc(source, false);
				renderTexture3 = global::UnityEngine.RenderTexture.GetTemporary(source.width >> 1, source.height >> 1, 0, source.format);
				renderTexture4 = global::UnityEngine.RenderTexture.GetTemporary(source.width >> 1, source.height >> 1, 0, source.format);
				global::UnityEngine.Graphics.Blit(source, renderTexture3, dofHdrMaterial, 15);
				dofHdrMaterial.SetVector("_Offsets", new global::UnityEngine.Vector4(0f, 1.5f, 0f, 1.5f));
				global::UnityEngine.Graphics.Blit(renderTexture3, renderTexture4, dofHdrMaterial, 19);
				dofHdrMaterial.SetVector("_Offsets", new global::UnityEngine.Vector4(1.5f, 0f, 0f, 1.5f));
				global::UnityEngine.Graphics.Blit(renderTexture4, renderTexture3, dofHdrMaterial, 19);
				if (nearBlur)
				{
					global::UnityEngine.Graphics.Blit(source, renderTexture4, dofHdrMaterial, 4);
				}
				dx11bokehMaterial.SetTexture("_BlurredColor", renderTexture3);
				dx11bokehMaterial.SetFloat("_SpawnHeuristic", dx11SpawnHeuristic);
				dx11bokehMaterial.SetVector("_BokehParams", new global::UnityEngine.Vector4(dx11BokehScale, dx11BokehIntensity, global::UnityEngine.Mathf.Clamp(dx11BokehThreshhold, 0.005f, 4f), internalBlurWidth));
				dx11bokehMaterial.SetTexture("_FgCocMask", (!nearBlur) ? null : renderTexture4);
				global::UnityEngine.Graphics.SetRandomWriteTarget(1, cbPoints);
				global::UnityEngine.Graphics.Blit(source, renderTexture, dx11bokehMaterial, 0);
				global::UnityEngine.Graphics.ClearRandomWriteTargets();
				if (nearBlur)
				{
					dofHdrMaterial.SetVector("_Offsets", new global::UnityEngine.Vector4(0f, num, 0f, num));
					global::UnityEngine.Graphics.Blit(renderTexture4, renderTexture3, dofHdrMaterial, 2);
					dofHdrMaterial.SetVector("_Offsets", new global::UnityEngine.Vector4(num, 0f, 0f, num));
					global::UnityEngine.Graphics.Blit(renderTexture3, renderTexture4, dofHdrMaterial, 2);
					global::UnityEngine.Graphics.Blit(renderTexture4, renderTexture, dofHdrMaterial, 3);
				}
				global::UnityEngine.Graphics.Blit(renderTexture, temporary, dofHdrMaterial, 20);
				dofHdrMaterial.SetVector("_Offsets", new global::UnityEngine.Vector4(internalBlurWidth, 0f, 0f, internalBlurWidth));
				global::UnityEngine.Graphics.Blit(renderTexture, source, dofHdrMaterial, 5);
				dofHdrMaterial.SetVector("_Offsets", new global::UnityEngine.Vector4(0f, internalBlurWidth, 0f, internalBlurWidth));
				global::UnityEngine.Graphics.Blit(source, temporary, dofHdrMaterial, 21);
				global::UnityEngine.Graphics.SetRenderTarget(temporary);
				global::UnityEngine.ComputeBuffer.CopyCount(cbPoints, cbDrawArgs, 0);
				dx11bokehMaterial.SetBuffer("pointBuffer", cbPoints);
				dx11bokehMaterial.SetTexture("_MainTex", dx11BokehTexture);
				dx11bokehMaterial.SetVector("_Screen", new global::UnityEngine.Vector3(1f / (1f * (float)source.width), 1f / (1f * (float)source.height), internalBlurWidth));
				dx11bokehMaterial.SetPass(2);
				global::UnityEngine.Graphics.DrawProceduralIndirectNow(global::UnityEngine.MeshTopology.Points, cbDrawArgs, 0);
				global::UnityEngine.Graphics.Blit(temporary, destination);
				global::UnityEngine.RenderTexture.ReleaseTemporary(temporary);
				global::UnityEngine.RenderTexture.ReleaseTemporary(renderTexture3);
				global::UnityEngine.RenderTexture.ReleaseTemporary(renderTexture4);
			}
			else
			{
				renderTexture = global::UnityEngine.RenderTexture.GetTemporary(source.width >> 1, source.height >> 1, 0, source.format);
				renderTexture2 = global::UnityEngine.RenderTexture.GetTemporary(source.width >> 1, source.height >> 1, 0, source.format);
				num = internalBlurWidth * foregroundOverlap;
				WriteCoc(source, false);
				source.filterMode = global::UnityEngine.FilterMode.Bilinear;
				global::UnityEngine.Graphics.Blit(source, renderTexture, dofHdrMaterial, 6);
				renderTexture3 = global::UnityEngine.RenderTexture.GetTemporary(renderTexture.width >> 1, renderTexture.height >> 1, 0, renderTexture.format);
				renderTexture4 = global::UnityEngine.RenderTexture.GetTemporary(renderTexture.width >> 1, renderTexture.height >> 1, 0, renderTexture.format);
				global::UnityEngine.Graphics.Blit(renderTexture, renderTexture3, dofHdrMaterial, 15);
				dofHdrMaterial.SetVector("_Offsets", new global::UnityEngine.Vector4(0f, 1.5f, 0f, 1.5f));
				global::UnityEngine.Graphics.Blit(renderTexture3, renderTexture4, dofHdrMaterial, 19);
				dofHdrMaterial.SetVector("_Offsets", new global::UnityEngine.Vector4(1.5f, 0f, 0f, 1.5f));
				global::UnityEngine.Graphics.Blit(renderTexture4, renderTexture3, dofHdrMaterial, 19);
				global::UnityEngine.RenderTexture renderTexture5 = null;
				if (nearBlur)
				{
					renderTexture5 = global::UnityEngine.RenderTexture.GetTemporary(source.width >> 1, source.height >> 1, 0, source.format);
					global::UnityEngine.Graphics.Blit(source, renderTexture5, dofHdrMaterial, 4);
				}
				dx11bokehMaterial.SetTexture("_BlurredColor", renderTexture3);
				dx11bokehMaterial.SetFloat("_SpawnHeuristic", dx11SpawnHeuristic);
				dx11bokehMaterial.SetVector("_BokehParams", new global::UnityEngine.Vector4(dx11BokehScale, dx11BokehIntensity, global::UnityEngine.Mathf.Clamp(dx11BokehThreshhold, 0.005f, 4f), internalBlurWidth));
				dx11bokehMaterial.SetTexture("_FgCocMask", renderTexture5);
				global::UnityEngine.Graphics.SetRandomWriteTarget(1, cbPoints);
				global::UnityEngine.Graphics.Blit(renderTexture, renderTexture2, dx11bokehMaterial, 0);
				global::UnityEngine.Graphics.ClearRandomWriteTargets();
				global::UnityEngine.RenderTexture.ReleaseTemporary(renderTexture3);
				global::UnityEngine.RenderTexture.ReleaseTemporary(renderTexture4);
				if (nearBlur)
				{
					dofHdrMaterial.SetVector("_Offsets", new global::UnityEngine.Vector4(0f, num, 0f, num));
					global::UnityEngine.Graphics.Blit(renderTexture5, renderTexture, dofHdrMaterial, 2);
					dofHdrMaterial.SetVector("_Offsets", new global::UnityEngine.Vector4(num, 0f, 0f, num));
					global::UnityEngine.Graphics.Blit(renderTexture, renderTexture5, dofHdrMaterial, 2);
					global::UnityEngine.Graphics.Blit(renderTexture5, renderTexture2, dofHdrMaterial, 3);
				}
				dofHdrMaterial.SetVector("_Offsets", new global::UnityEngine.Vector4(internalBlurWidth, 0f, 0f, internalBlurWidth));
				global::UnityEngine.Graphics.Blit(renderTexture2, renderTexture, dofHdrMaterial, 5);
				dofHdrMaterial.SetVector("_Offsets", new global::UnityEngine.Vector4(0f, internalBlurWidth, 0f, internalBlurWidth));
				global::UnityEngine.Graphics.Blit(renderTexture, renderTexture2, dofHdrMaterial, 5);
				global::UnityEngine.Graphics.SetRenderTarget(renderTexture2);
				global::UnityEngine.ComputeBuffer.CopyCount(cbPoints, cbDrawArgs, 0);
				dx11bokehMaterial.SetBuffer("pointBuffer", cbPoints);
				dx11bokehMaterial.SetTexture("_MainTex", dx11BokehTexture);
				dx11bokehMaterial.SetVector("_Screen", new global::UnityEngine.Vector3(1f / (1f * (float)renderTexture2.width), 1f / (1f * (float)renderTexture2.height), internalBlurWidth));
				dx11bokehMaterial.SetPass(1);
				global::UnityEngine.Graphics.DrawProceduralIndirectNow(global::UnityEngine.MeshTopology.Points, cbDrawArgs, 0);
				dofHdrMaterial.SetTexture("_LowRez", renderTexture2);
				dofHdrMaterial.SetTexture("_FgOverlap", renderTexture5);
				dofHdrMaterial.SetVector("_Offsets", 1f * (float)source.width / (1f * (float)renderTexture2.width) * internalBlurWidth * global::UnityEngine.Vector4.one);
				global::UnityEngine.Graphics.Blit(source, destination, dofHdrMaterial, 9);
				if ((bool)renderTexture5)
				{
					global::UnityEngine.RenderTexture.ReleaseTemporary(renderTexture5);
				}
			}
		}
		else
		{
			source.filterMode = global::UnityEngine.FilterMode.Bilinear;
			if (highResolution)
			{
				internalBlurWidth *= 2f;
			}
			WriteCoc(source, true);
			renderTexture = global::UnityEngine.RenderTexture.GetTemporary(source.width >> 1, source.height >> 1, 0, source.format);
			renderTexture2 = global::UnityEngine.RenderTexture.GetTemporary(source.width >> 1, source.height >> 1, 0, source.format);
			int pass = ((blurSampleCount != DepthOfFieldScatter.BlurSampleCount.High && blurSampleCount != DepthOfFieldScatter.BlurSampleCount.Medium) ? 11 : 17);
			if (highResolution)
			{
				dofHdrMaterial.SetVector("_Offsets", new global::UnityEngine.Vector4(0f, internalBlurWidth, 0.025f, internalBlurWidth));
				global::UnityEngine.Graphics.Blit(source, destination, dofHdrMaterial, pass);
			}
			else
			{
				dofHdrMaterial.SetVector("_Offsets", new global::UnityEngine.Vector4(0f, internalBlurWidth, 0.1f, internalBlurWidth));
				global::UnityEngine.Graphics.Blit(source, renderTexture, dofHdrMaterial, 6);
				global::UnityEngine.Graphics.Blit(renderTexture, renderTexture2, dofHdrMaterial, pass);
				dofHdrMaterial.SetTexture("_LowRez", renderTexture2);
				dofHdrMaterial.SetTexture("_FgOverlap", null);
				dofHdrMaterial.SetVector("_Offsets", global::UnityEngine.Vector4.one * (1f * (float)source.width / (1f * (float)renderTexture2.width)) * internalBlurWidth);
				global::UnityEngine.Graphics.Blit(source, destination, dofHdrMaterial, (blurSampleCount != DepthOfFieldScatter.BlurSampleCount.High) ? 12 : 18);
			}
		}
		if ((bool)renderTexture)
		{
			global::UnityEngine.RenderTexture.ReleaseTemporary(renderTexture);
		}
		if ((bool)renderTexture2)
		{
			global::UnityEngine.RenderTexture.ReleaseTemporary(renderTexture2);
		}
	}

	public override void Main()
	{
	}
}
