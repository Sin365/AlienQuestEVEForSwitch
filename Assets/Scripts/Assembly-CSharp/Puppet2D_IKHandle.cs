using UnityEngine;
using System.Collections.Generic;

public class Puppet2D_IKHandle : MonoBehaviour
{
	public bool Flip;
	public bool SquashAndStretch;
	public bool Scale;
	public Vector3 AimDirection;
	public Transform poleVector;
	public Vector3 UpDirection;
	public Vector3[] scaleStart;
	public Transform topJointTransform;
	public Transform middleJointTransform;
	public Transform bottomJointTransform;
	public Vector3 OffsetScale;
	public int numberIkBonesIndex;
	public int numberOfBones;
	public int iterations;
	public float damping;
	public Transform IKControl;
	public Transform endTransform;
	public Transform startTransform;
	public List<Vector3> bindPose;
	public List<Transform> bindBones;
	public bool limitBones;
	public List<Transform> angleLimitTransform;
	public List<Vector2> angleLimits;
}
