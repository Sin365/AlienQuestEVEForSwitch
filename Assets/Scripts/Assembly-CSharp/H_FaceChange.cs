public class H_FaceChange : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.SkinnedMeshRenderer Face_Mouth_Open;

	public global::UnityEngine.SkinnedMeshRenderer Face_Mouth_Close;

	private bool onMouth = true;

	private void Mouth_Open()
	{
		if (!onMouth)
		{
			onMouth = true;
			Face_Mouth_Open.enabled = true;
			Face_Mouth_Close.enabled = false;
		}
	}

	private void Mouth_Close()
	{
		if (onMouth)
		{
			onMouth = false;
			Face_Mouth_Open.enabled = false;
			Face_Mouth_Close.enabled = true;
		}
	}
}
