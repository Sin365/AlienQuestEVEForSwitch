[global::System.Serializable]
[global::UnityEngine.ExecuteInEditMode]
[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Camera))]
public class PostEffectsHelper : global::UnityEngine.MonoBehaviour
{
	public virtual void Start()
	{
	}

	public virtual void OnRenderImage(global::UnityEngine.RenderTexture source, global::UnityEngine.RenderTexture destination)
	{
		global::UnityEngine.Debug.Log("OnRenderImage in Helper called ...");
	}

	public static void DrawLowLevelPlaneAlignedWithCamera(float dist, global::UnityEngine.RenderTexture source, global::UnityEngine.RenderTexture dest, global::UnityEngine.Material material, global::UnityEngine.Camera cameraForProjectionMatrix)
	{
		global::UnityEngine.RenderTexture.active = dest;
		material.SetTexture("_MainTex", source);
		bool flag = true;
		global::UnityEngine.GL.PushMatrix();
		global::UnityEngine.GL.LoadIdentity();
		global::UnityEngine.GL.LoadProjectionMatrix(cameraForProjectionMatrix.projectionMatrix);
		float f = cameraForProjectionMatrix.fieldOfView * 0.5f * ((float)global::System.Math.PI / 180f);
		float num = global::UnityEngine.Mathf.Cos(f) / global::UnityEngine.Mathf.Sin(f);
		float aspect = cameraForProjectionMatrix.aspect;
		float num2 = aspect / (0f - num);
		float num3 = aspect / num;
		float num4 = 1f / (0f - num);
		float num5 = 1f / num;
		float num6 = 1f;
		num2 *= dist * num6;
		num3 *= dist * num6;
		num4 *= dist * num6;
		num5 *= dist * num6;
		float z = 0f - dist;
		for (int i = 0; i < material.passCount; i++)
		{
			material.SetPass(i);
			global::UnityEngine.GL.Begin(7);
			float num7 = default(float);
			float num8 = default(float);
			if (flag)
			{
				num7 = 1f;
				num8 = 0f;
			}
			else
			{
				num7 = 0f;
				num8 = 1f;
			}
			global::UnityEngine.GL.TexCoord2(0f, num7);
			global::UnityEngine.GL.Vertex3(num2, num4, z);
			global::UnityEngine.GL.TexCoord2(1f, num7);
			global::UnityEngine.GL.Vertex3(num3, num4, z);
			global::UnityEngine.GL.TexCoord2(1f, num8);
			global::UnityEngine.GL.Vertex3(num3, num5, z);
			global::UnityEngine.GL.TexCoord2(0f, num8);
			global::UnityEngine.GL.Vertex3(num2, num5, z);
			global::UnityEngine.GL.End();
		}
		global::UnityEngine.GL.PopMatrix();
	}

	public static void DrawBorder(global::UnityEngine.RenderTexture dest, global::UnityEngine.Material material)
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

	public static void DrawLowLevelQuad(float x1, float x2, float y1, float y2, global::UnityEngine.RenderTexture source, global::UnityEngine.RenderTexture dest, global::UnityEngine.Material material)
	{
		global::UnityEngine.RenderTexture.active = dest;
		material.SetTexture("_MainTex", source);
		bool flag = true;
		global::UnityEngine.GL.PushMatrix();
		global::UnityEngine.GL.LoadOrtho();
		for (int i = 0; i < material.passCount; i++)
		{
			material.SetPass(i);
			global::UnityEngine.GL.Begin(7);
			float num = default(float);
			float num2 = default(float);
			if (flag)
			{
				num = 1f;
				num2 = 0f;
			}
			else
			{
				num = 0f;
				num2 = 1f;
			}
			global::UnityEngine.GL.TexCoord2(0f, num);
			global::UnityEngine.GL.Vertex3(x1, y1, 0.1f);
			global::UnityEngine.GL.TexCoord2(1f, num);
			global::UnityEngine.GL.Vertex3(x2, y1, 0.1f);
			global::UnityEngine.GL.TexCoord2(1f, num2);
			global::UnityEngine.GL.Vertex3(x2, y2, 0.1f);
			global::UnityEngine.GL.TexCoord2(0f, num2);
			global::UnityEngine.GL.Vertex3(x1, y2, 0.1f);
			global::UnityEngine.GL.End();
		}
		global::UnityEngine.GL.PopMatrix();
	}

	public virtual void Main()
	{
	}
}
