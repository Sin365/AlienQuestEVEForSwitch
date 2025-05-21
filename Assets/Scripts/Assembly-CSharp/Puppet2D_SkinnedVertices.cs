[global::UnityEngine.ExecuteInEditMode]
public class Puppet2D_SkinnedVertices : global::UnityEngine.MonoBehaviour
{
	private class Bone
	{
		internal global::UnityEngine.Transform bone;

		internal float weight;

		internal global::UnityEngine.Vector3 delta;
	}

	private global::UnityEngine.Mesh mesh;

	private global::System.Collections.Generic.List<global::System.Collections.Generic.List<Puppet2D_SkinnedVertices.Bone>> allBones = new global::System.Collections.Generic.List<global::System.Collections.Generic.List<Puppet2D_SkinnedVertices.Bone>>();

	private void Start()
	{
		global::UnityEngine.SkinnedMeshRenderer skinnedMeshRenderer = GetComponent(typeof(global::UnityEngine.SkinnedMeshRenderer)) as global::UnityEngine.SkinnedMeshRenderer;
		mesh = skinnedMeshRenderer.sharedMesh;
		for (int i = 0; i < mesh.vertexCount; i++)
		{
			global::UnityEngine.Vector3 position = mesh.vertices[i];
			position = base.transform.TransformPoint(position);
			global::UnityEngine.BoneWeight boneWeight = mesh.boneWeights[i];
			int[] array = new int[4] { boneWeight.boneIndex0, boneWeight.boneIndex1, boneWeight.boneIndex2, boneWeight.boneIndex3 };
			float[] array2 = new float[4] { boneWeight.weight0, boneWeight.weight1, boneWeight.weight2, boneWeight.weight3 };
			global::System.Collections.Generic.List<Puppet2D_SkinnedVertices.Bone> list = new global::System.Collections.Generic.List<Puppet2D_SkinnedVertices.Bone>();
			allBones.Add(list);
			for (int j = 0; j < 4; j++)
			{
				if (array2[j] > 0f)
				{
					Puppet2D_SkinnedVertices.Bone bone = new Puppet2D_SkinnedVertices.Bone();
					list.Add(bone);
					bone.bone = skinnedMeshRenderer.bones[array[j]];
					bone.weight = array2[j];
					bone.delta = bone.bone.InverseTransformPoint(position);
				}
			}
		}
	}

	private void OnDrawGizmos()
	{
		if (!global::UnityEngine.Application.isPlaying || !base.enabled)
		{
			return;
		}
		for (int i = 0; i < mesh.vertexCount; i++)
		{
			global::System.Collections.Generic.List<Puppet2D_SkinnedVertices.Bone> list = allBones[i];
			global::UnityEngine.Vector3 zero = global::UnityEngine.Vector3.zero;
			foreach (Puppet2D_SkinnedVertices.Bone item in list)
			{
				zero += item.bone.TransformPoint(item.delta) * item.weight;
			}
			int count = list.Count;
			global::UnityEngine.Color color;
			switch (count)
			{
			case 4:
				color = global::UnityEngine.Color.red;
				break;
			case 3:
				color = global::UnityEngine.Color.blue;
				break;
			case 2:
				color = global::UnityEngine.Color.green;
				break;
			default:
				color = global::UnityEngine.Color.black;
				break;
			}
			global::UnityEngine.Gizmos.color = color;
			global::UnityEngine.Gizmos.DrawWireCube(zero, (float)count * 0.05f * global::UnityEngine.Vector3.one);
			global::UnityEngine.Vector3 zero2 = global::UnityEngine.Vector3.zero;
			foreach (Puppet2D_SkinnedVertices.Bone item2 in list)
			{
				zero2 += item2.bone.TransformDirection(mesh.normals[i]) * item2.weight;
			}
		}
	}
}
