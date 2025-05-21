[global::UnityEngine.ExecuteInEditMode]
public class Puppet2D_GlobalControl : global::UnityEngine.MonoBehaviour
{
	public float startRotationY;

	public global::System.Collections.Generic.List<Puppet2D_SplineControl> _SplineControls = new global::System.Collections.Generic.List<Puppet2D_SplineControl>();

	public global::System.Collections.Generic.List<Puppet2D_IKHandle> _Ikhandles = new global::System.Collections.Generic.List<Puppet2D_IKHandle>();

	public global::System.Collections.Generic.List<Puppet2D_ParentControl> _ParentControls = new global::System.Collections.Generic.List<Puppet2D_ParentControl>();

	public global::System.Collections.Generic.List<Puppet2D_FFDLineDisplay> _ffdControls = new global::System.Collections.Generic.List<Puppet2D_FFDLineDisplay>();

	[global::UnityEngine.HideInInspector]
	public global::System.Collections.Generic.List<global::UnityEngine.SpriteRenderer> _Controls = new global::System.Collections.Generic.List<global::UnityEngine.SpriteRenderer>();

	[global::UnityEngine.HideInInspector]
	public global::System.Collections.Generic.List<global::UnityEngine.SpriteRenderer> _Bones = new global::System.Collections.Generic.List<global::UnityEngine.SpriteRenderer>();

	[global::UnityEngine.HideInInspector]
	public global::System.Collections.Generic.List<global::UnityEngine.SpriteRenderer> _FFDControls = new global::System.Collections.Generic.List<global::UnityEngine.SpriteRenderer>();

	public bool ControlsVisiblity = true;

	public bool BonesVisiblity = true;

	public bool FFD_Visiblity = true;

	public bool CombineMeshes;

	public bool flip;

	private bool internalFlip;

	public bool AutoRefresh = true;

	public bool ControlsEnabled = true;

	public bool lateUpdate = true;

	[global::UnityEngine.HideInInspector]
	public int _flipCorrection = 1;

	private void OnEnable()
	{
		if (AutoRefresh)
		{
			_Ikhandles.Clear();
			_SplineControls.Clear();
			_ParentControls.Clear();
			_Controls.Clear();
			_Bones.Clear();
			_FFDControls.Clear();
			_ffdControls.Clear();
			TraverseHierarchy(base.transform);
		}
	}

	public void Refresh()
	{
		_Ikhandles.Clear();
		_SplineControls.Clear();
		_ParentControls.Clear();
		_Controls.Clear();
		_Bones.Clear();
		_FFDControls.Clear();
		_ffdControls.Clear();
		TraverseHierarchy(base.transform);
	}

	private void Awake()
	{
		internalFlip = flip;
		if (global::UnityEngine.Application.isPlaying && CombineMeshes)
		{
			CombineAllMeshes();
		}
	}

	public void Init()
	{
		_Ikhandles.Clear();
		_SplineControls.Clear();
		_ParentControls.Clear();
		_Controls.Clear();
		_Bones.Clear();
		_FFDControls.Clear();
		_ffdControls.Clear();
		TraverseHierarchy(base.transform);
	}

	private void OnValidate()
	{
		if (AutoRefresh)
		{
			_Ikhandles.Clear();
			_SplineControls.Clear();
			_ParentControls.Clear();
			_Controls.Clear();
			_Bones.Clear();
			_FFDControls.Clear();
			_ffdControls.Clear();
			TraverseHierarchy(base.transform);
		}
		foreach (global::UnityEngine.SpriteRenderer control in _Controls)
		{
			if ((bool)control && control.enabled != ControlsVisiblity)
			{
				control.enabled = ControlsVisiblity;
			}
		}
		foreach (global::UnityEngine.SpriteRenderer bone in _Bones)
		{
			if ((bool)bone && bone.enabled != BonesVisiblity)
			{
				bone.enabled = BonesVisiblity;
			}
		}
		foreach (global::UnityEngine.SpriteRenderer fFDControl in _FFDControls)
		{
			if ((bool)fFDControl && (bool)fFDControl.transform.parent && (bool)fFDControl.transform.parent.gameObject && fFDControl.transform.parent.gameObject.activeSelf != FFD_Visiblity)
			{
				fFDControl.transform.parent.gameObject.SetActive(FFD_Visiblity);
			}
		}
	}

	private void Update()
	{
		if (lateUpdate)
		{
			return;
		}
		if (ControlsEnabled)
		{
			Run();
		}
		if (internalFlip != flip)
		{
			if (flip)
			{
				base.transform.localScale = new global::UnityEngine.Vector3(base.transform.localScale.x, base.transform.localScale.y, 0f - base.transform.localScale.z);
				base.transform.localEulerAngles = new global::UnityEngine.Vector3(base.transform.rotation.eulerAngles.x, startRotationY + 180f, base.transform.rotation.eulerAngles.z);
			}
			else
			{
				base.transform.localScale = new global::UnityEngine.Vector3(global::UnityEngine.Mathf.Abs(base.transform.localScale.x), global::UnityEngine.Mathf.Abs(base.transform.localScale.y), global::UnityEngine.Mathf.Abs(base.transform.localScale.z));
				base.transform.localEulerAngles = new global::UnityEngine.Vector3(base.transform.rotation.eulerAngles.x, startRotationY, base.transform.rotation.eulerAngles.z);
			}
			internalFlip = flip;
			Run();
		}
	}

	private void LateUpdate()
	{
		if (!lateUpdate)
		{
			return;
		}
		if (ControlsEnabled)
		{
			Run();
		}
		if (internalFlip != flip)
		{
			if (flip)
			{
				base.transform.localScale = new global::UnityEngine.Vector3(base.transform.localScale.x, base.transform.localScale.y, 0f - base.transform.localScale.z);
				base.transform.localEulerAngles = new global::UnityEngine.Vector3(base.transform.rotation.eulerAngles.x, startRotationY + 180f, base.transform.rotation.eulerAngles.z);
			}
			else
			{
				base.transform.localScale = new global::UnityEngine.Vector3(global::UnityEngine.Mathf.Abs(base.transform.localScale.x), global::UnityEngine.Mathf.Abs(base.transform.localScale.y), global::UnityEngine.Mathf.Abs(base.transform.localScale.z));
				base.transform.localEulerAngles = new global::UnityEngine.Vector3(base.transform.rotation.eulerAngles.x, startRotationY, base.transform.rotation.eulerAngles.z);
			}
			internalFlip = flip;
			Run();
		}
	}

	public void Run()
	{
		foreach (Puppet2D_SplineControl splineControl in _SplineControls)
		{
			if ((bool)splineControl)
			{
				splineControl.Run();
			}
		}
		foreach (Puppet2D_ParentControl parentControl in _ParentControls)
		{
			if ((bool)parentControl)
			{
				parentControl.ParentControlRun();
			}
		}
		FaceCamera();
		foreach (Puppet2D_IKHandle ikhandle in _Ikhandles)
		{
			if ((bool)ikhandle)
			{
				ikhandle.CalculateIK();
			}
		}
		foreach (Puppet2D_FFDLineDisplay ffdControl in _ffdControls)
		{
			if ((bool)ffdControl)
			{
				ffdControl.Run();
			}
		}
	}

	public void TraverseHierarchy(global::UnityEngine.Transform root)
	{
		foreach (global::UnityEngine.Transform item in root)
		{
			global::UnityEngine.GameObject gameObject = item.gameObject;
			global::UnityEngine.SpriteRenderer component = gameObject.transform.GetComponent<global::UnityEngine.SpriteRenderer>();
			if ((bool)component && (bool)component.sprite)
			{
				if (component.sprite.name.Contains("Control"))
				{
					_Controls.Add(component);
				}
				else if (component.sprite.name.Contains("ffd"))
				{
					_FFDControls.Add(component);
				}
				else if (component.sprite.name.Contains("Bone"))
				{
					_Bones.Add(component);
				}
			}
			Puppet2D_ParentControl component2 = gameObject.transform.GetComponent<Puppet2D_ParentControl>();
			if ((bool)component2)
			{
				_ParentControls.Add(component2);
			}
			Puppet2D_IKHandle component3 = gameObject.transform.GetComponent<Puppet2D_IKHandle>();
			if ((bool)component3)
			{
				_Ikhandles.Add(component3);
			}
			Puppet2D_FFDLineDisplay component4 = gameObject.transform.GetComponent<Puppet2D_FFDLineDisplay>();
			if ((bool)component4)
			{
				_ffdControls.Add(component4);
			}
			Puppet2D_SplineControl component5 = gameObject.transform.GetComponent<Puppet2D_SplineControl>();
			if ((bool)component5)
			{
				_SplineControls.Add(component5);
			}
			TraverseHierarchy(item);
		}
	}

	private void CombineAllMeshes()
	{
		global::UnityEngine.Vector3 localScale = base.transform.localScale;
		global::UnityEngine.Quaternion rotation = base.transform.rotation;
		global::UnityEngine.Vector3 position = base.transform.position;
		base.transform.localScale = global::UnityEngine.Vector3.one;
		base.transform.rotation = global::UnityEngine.Quaternion.identity;
		base.transform.position = global::UnityEngine.Vector3.zero;
		global::UnityEngine.SkinnedMeshRenderer[] componentsInChildren = GetComponentsInChildren<global::UnityEngine.SkinnedMeshRenderer>();
		global::System.Collections.Generic.List<global::UnityEngine.Transform> list = new global::System.Collections.Generic.List<global::UnityEngine.Transform>();
		global::System.Collections.Generic.List<global::UnityEngine.BoneWeight> list2 = new global::System.Collections.Generic.List<global::UnityEngine.BoneWeight>();
		global::System.Collections.Generic.List<global::UnityEngine.CombineInstance> list3 = new global::System.Collections.Generic.List<global::UnityEngine.CombineInstance>();
		global::System.Collections.Generic.List<global::UnityEngine.Texture2D> list4 = new global::System.Collections.Generic.List<global::UnityEngine.Texture2D>();
		global::UnityEngine.Material material = null;
		int num = 0;
		global::System.Collections.Generic.Dictionary<global::UnityEngine.SkinnedMeshRenderer, float> dictionary = new global::System.Collections.Generic.Dictionary<global::UnityEngine.SkinnedMeshRenderer, float>(componentsInChildren.Length);
		bool updateWhenOffscreen = false;
		global::UnityEngine.SkinnedMeshRenderer[] array = componentsInChildren;
		foreach (global::UnityEngine.SkinnedMeshRenderer skinnedMeshRenderer in array)
		{
			dictionary.Add(skinnedMeshRenderer, skinnedMeshRenderer.transform.position.z);
			updateWhenOffscreen = skinnedMeshRenderer.updateWhenOffscreen;
		}
		global::System.Linq.IOrderedEnumerable<global::System.Collections.Generic.KeyValuePair<global::UnityEngine.SkinnedMeshRenderer, float>> source = global::System.Linq.Enumerable.OrderBy(dictionary, (global::System.Collections.Generic.KeyValuePair<global::UnityEngine.SkinnedMeshRenderer, float> pair) => pair.Key.sortingOrder);
		source = global::System.Linq.Enumerable.OrderByDescending(source, (global::System.Collections.Generic.KeyValuePair<global::UnityEngine.SkinnedMeshRenderer, float> pair) => pair.Value);
		foreach (global::System.Collections.Generic.KeyValuePair<global::UnityEngine.SkinnedMeshRenderer, float> item4 in source)
		{
			num += item4.Key.sharedMesh.subMeshCount;
		}
		int[] array2 = new int[num];
		int num2 = 0;
		int num3 = 0;
		foreach (global::System.Collections.Generic.KeyValuePair<global::UnityEngine.SkinnedMeshRenderer, float> item5 in source)
		{
			global::UnityEngine.SkinnedMeshRenderer key = item5.Key;
			if (material == null)
			{
				material = key.sharedMaterial;
			}
			else if ((bool)material.mainTexture && (bool)key.sharedMaterial.mainTexture && material.mainTexture != key.sharedMaterial.mainTexture)
			{
				continue;
			}
			bool flag = false;
			global::UnityEngine.Transform[] bones = key.bones;
			foreach (global::UnityEngine.Transform transform in bones)
			{
				Puppet2D_FFDLineDisplay component = transform.GetComponent<Puppet2D_FFDLineDisplay>();
				if ((bool)component && component.outputSkinnedMesh != key)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				global::UnityEngine.BoneWeight[] boneWeights = key.sharedMesh.boneWeights;
				global::UnityEngine.BoneWeight[] array3 = boneWeights;
				foreach (global::UnityEngine.BoneWeight boneWeight in array3)
				{
					global::UnityEngine.BoneWeight item = boneWeight;
					item.boneIndex0 += num2;
					item.boneIndex1 += num2;
					item.boneIndex2 += num2;
					item.boneIndex3 += num2;
					list2.Add(item);
				}
				num2 += key.bones.Length;
				global::UnityEngine.Transform[] bones2 = key.bones;
				global::UnityEngine.Transform[] array4 = bones2;
				foreach (global::UnityEngine.Transform item2 in array4)
				{
					list.Add(item2);
				}
				if (key.material.mainTexture != null)
				{
					list4.Add(key.GetComponent<global::UnityEngine.Renderer>().material.mainTexture as global::UnityEngine.Texture2D);
				}
				global::UnityEngine.CombineInstance item3 = new global::UnityEngine.CombineInstance
				{
					mesh = key.sharedMesh
				};
				array2[num3] = item3.mesh.vertexCount;
				item3.transform = key.transform.localToWorldMatrix;
				list3.Add(item3);
				global::UnityEngine.Object.Destroy(key.gameObject);
				num3++;
			}
		}
		global::System.Collections.Generic.List<global::UnityEngine.Matrix4x4> list5 = new global::System.Collections.Generic.List<global::UnityEngine.Matrix4x4>();
		for (int num7 = 0; num7 < list.Count; num7++)
		{
			if ((bool)list[num7].GetComponent<Puppet2D_FFDLineDisplay>())
			{
				global::UnityEngine.Vector3 position2 = list[num7].transform.parent.parent.position;
				global::UnityEngine.Quaternion rotation2 = list[num7].transform.parent.parent.rotation;
				list[num7].transform.parent.parent.position = global::UnityEngine.Vector3.zero;
				list[num7].transform.parent.parent.rotation = global::UnityEngine.Quaternion.identity;
				list5.Add(list[num7].worldToLocalMatrix * base.transform.worldToLocalMatrix);
				list[num7].transform.parent.parent.position = position2;
				list[num7].transform.parent.parent.rotation = rotation2;
			}
			else
			{
				list5.Add(list[num7].worldToLocalMatrix * base.transform.worldToLocalMatrix);
			}
		}
		global::UnityEngine.SkinnedMeshRenderer skinnedMeshRenderer2 = base.gameObject.AddComponent<global::UnityEngine.SkinnedMeshRenderer>();
		skinnedMeshRenderer2.updateWhenOffscreen = updateWhenOffscreen;
		skinnedMeshRenderer2.sharedMesh = new global::UnityEngine.Mesh();
		skinnedMeshRenderer2.sharedMesh.CombineMeshes(list3.ToArray(), true, true);
		global::UnityEngine.Material material2 = ((!(material != null)) ? new global::UnityEngine.Material(global::UnityEngine.Shader.Find("Unlit/Transparent")) : material);
		material2.mainTexture = list4[0];
		skinnedMeshRenderer2.sharedMesh.uv = skinnedMeshRenderer2.sharedMesh.uv;
		skinnedMeshRenderer2.sharedMaterial = material2;
		skinnedMeshRenderer2.bones = list.ToArray();
		skinnedMeshRenderer2.sharedMesh.boneWeights = list2.ToArray();
		skinnedMeshRenderer2.sharedMesh.bindposes = list5.ToArray();
		skinnedMeshRenderer2.sharedMesh.RecalculateBounds();
		base.transform.localScale = localScale;
		base.transform.rotation = rotation;
		base.transform.position = position;
	}

	private void FaceCamera()
	{
		foreach (Puppet2D_IKHandle ikhandle in _Ikhandles)
		{
			ikhandle.AimDirection = base.transform.forward.normalized * _flipCorrection;
		}
	}
}
