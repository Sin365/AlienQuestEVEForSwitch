public class Puppet2D_IKHandle : global::UnityEngine.MonoBehaviour
{
	public bool Flip;

	public bool SquashAndStretch;

	public bool Scale;

	[global::UnityEngine.HideInInspector]
	public global::UnityEngine.Vector3 AimDirection;

	[global::UnityEngine.HideInInspector]
	public global::UnityEngine.Transform poleVector;

	[global::UnityEngine.HideInInspector]
	public global::UnityEngine.Vector3 UpDirection;

	[global::UnityEngine.HideInInspector]
	public global::UnityEngine.Vector3[] scaleStart = new global::UnityEngine.Vector3[2];

	[global::UnityEngine.HideInInspector]
	public global::UnityEngine.Transform topJointTransform;

	[global::UnityEngine.HideInInspector]
	public global::UnityEngine.Transform middleJointTransform;

	[global::UnityEngine.HideInInspector]
	public global::UnityEngine.Transform bottomJointTransform;

	[global::UnityEngine.HideInInspector]
	public global::UnityEngine.Vector3 OffsetScale = new global::UnityEngine.Vector3(1f, 1f, 1f);

	private global::UnityEngine.Transform IK_CTRL;

	private global::UnityEngine.Vector3 root2IK;

	private global::UnityEngine.Vector3 root2IK2MiddleJoint;

	private bool LargerMiddleJoint;

	[global::UnityEngine.HideInInspector]
	public int numberIkBonesIndex;

	public int numberOfBones = 4;

	public int iterations = 10;

	public float damping = 1f;

	public global::UnityEngine.Transform IKControl;

	public global::UnityEngine.Transform endTransform;

	public global::UnityEngine.Transform startTransform;

	public global::System.Collections.Generic.List<global::UnityEngine.Vector3> bindPose;

	public global::System.Collections.Generic.List<global::UnityEngine.Transform> bindBones;

	public bool limitBones = true;

	public global::System.Collections.Generic.List<global::UnityEngine.Transform> angleLimitTransform = new global::System.Collections.Generic.List<global::UnityEngine.Transform>();

	public global::System.Collections.Generic.List<global::UnityEngine.Vector2> angleLimits = new global::System.Collections.Generic.List<global::UnityEngine.Vector2>();

	public void CalculateIK()
	{
		if (numberIkBonesIndex == 1)
		{
			CalculateMultiIK();
			return;
		}
		int num = (Flip ? 1 : (-1));
		IK_CTRL = base.transform;
		root2IK = (topJointTransform.position + IK_CTRL.position) / 2f;
		global::UnityEngine.Vector3 vector = IK_CTRL.position - topJointTransform.position;
		global::UnityEngine.Quaternion quaternion = global::UnityEngine.Quaternion.AngleAxis(num * 90, global::UnityEngine.Vector3.forward);
		root2IK2MiddleJoint = quaternion * vector;
		poleVector.position = root2IK - root2IK2MiddleJoint;
		float angle = GetAngle();
		global::UnityEngine.Quaternion quaternion2 = global::UnityEngine.Quaternion.AngleAxis(angle * (float)num, global::UnityEngine.Vector3.forward);
		if (!IsNaN(quaternion2))
		{
			topJointTransform.rotation = global::UnityEngine.Quaternion.LookRotation(IK_CTRL.position - topJointTransform.position, AimDirection) * global::UnityEngine.Quaternion.AngleAxis(90f, UpDirection) * quaternion2;
		}
		else if (LargerMiddleJoint)
		{
			topJointTransform.rotation = global::UnityEngine.Quaternion.LookRotation(IK_CTRL.position - topJointTransform.position, AimDirection) * global::UnityEngine.Quaternion.AngleAxis(-90f, UpDirection);
		}
		else
		{
			topJointTransform.rotation = global::UnityEngine.Quaternion.LookRotation(IK_CTRL.position - topJointTransform.position, AimDirection) * global::UnityEngine.Quaternion.AngleAxis(90f, UpDirection);
		}
		middleJointTransform.rotation = global::UnityEngine.Quaternion.LookRotation(IK_CTRL.position - middleJointTransform.position, AimDirection) * global::UnityEngine.Quaternion.AngleAxis(90f, UpDirection);
		bottomJointTransform.rotation = IK_CTRL.rotation;
		if (Scale)
		{
			bottomJointTransform.localScale = new global::UnityEngine.Vector3(IK_CTRL.localScale.x * OffsetScale.x, IK_CTRL.localScale.y * OffsetScale.y, IK_CTRL.localScale.z * OffsetScale.z);
		}
	}

	private bool IsNaN(global::UnityEngine.Quaternion q)
	{
		return float.IsNaN(q.x) || float.IsNaN(q.y) || float.IsNaN(q.z) || float.IsNaN(q.w);
	}

	private float GetAngle()
	{
		if (SquashAndStretch)
		{
			topJointTransform.localScale = scaleStart[0];
			middleJointTransform.localScale = scaleStart[1];
		}
		float num = global::UnityEngine.Vector3.Distance(topJointTransform.position, middleJointTransform.position);
		float num2 = global::UnityEngine.Vector3.Distance(middleJointTransform.position, bottomJointTransform.position);
		float num3 = num + num2;
		float num4 = global::UnityEngine.Vector3.Distance(topJointTransform.position, IK_CTRL.position);
		if (num2 > num)
		{
			LargerMiddleJoint = true;
		}
		else
		{
			LargerMiddleJoint = false;
		}
		if (SquashAndStretch && num4 > num3)
		{
			topJointTransform.localScale = new global::UnityEngine.Vector3(scaleStart[0].x, num4 / num3 * scaleStart[0].y, scaleStart[0].z);
		}
		num4 = global::UnityEngine.Mathf.Min(num4, num3 - 0.0001f);
		float num5 = (num * num - num2 * num2 + num4 * num4) / (2f * num4);
		return global::UnityEngine.Mathf.Acos(num5 / num) * 57.29578f;
	}

	private void OnValidate()
	{
		for (int i = 0; i < angleLimits.Count; i++)
		{
			angleLimits[i] = new global::UnityEngine.Vector2(global::UnityEngine.Mathf.Clamp(angleLimits[i].x, -360f, 360f), global::UnityEngine.Mathf.Clamp(angleLimits[i].y, -360f, 360f));
		}
	}

	public void CalculateMultiIK()
	{
		if (!(base.transform == null) && !(endTransform == null))
		{
			for (int i = 0; i < iterations; i++)
			{
				CalculateMultiIK_run();
			}
			endTransform.rotation = base.transform.rotation;
		}
	}

	private void CalculateMultiIK_run()
	{
		global::UnityEngine.Transform parent = endTransform.parent;
		while (true)
		{
			RotateTowardsTarget(parent);
			if (parent == startTransform)
			{
				break;
			}
			parent = parent.parent;
		}
	}

	private void RotateTowardsTarget(global::UnityEngine.Transform startTransform)
	{
		if (!(startTransform == null))
		{
			global::UnityEngine.Vector2 vector = base.transform.position - startTransform.position;
			global::UnityEngine.Vector2 vector2 = endTransform.position - startTransform.position;
			float num = SignedAngle(vector2, vector);
			if (startTransform.eulerAngles.y % 360f > 90f && startTransform.eulerAngles.y % 360f < 275f)
			{
				num *= -1f;
			}
			num *= damping;
			num = 0f - (num - startTransform.localEulerAngles.z);
			if (angleLimits != null && angleLimitTransform.Contains(startTransform))
			{
				global::UnityEngine.Vector2 vector3 = angleLimits[angleLimitTransform.IndexOf(startTransform)];
				num = ClampAngle(num, vector3.x, vector3.y);
			}
			startTransform.localRotation = global::UnityEngine.Quaternion.Euler(0f, 0f, num);
		}
	}

	public static float SignedAngle(global::UnityEngine.Vector3 a, global::UnityEngine.Vector3 b)
	{
		float num = global::UnityEngine.Vector3.Angle(a, b);
		float num2 = global::UnityEngine.Mathf.Sign(global::UnityEngine.Vector3.Dot(global::UnityEngine.Vector3.back, global::UnityEngine.Vector3.Cross(a, b)));
		return num * num2;
	}

	private float ClampAngle(float angle, float min, float max)
	{
		return global::UnityEngine.Mathf.Clamp(angle, min, max);
	}
}
