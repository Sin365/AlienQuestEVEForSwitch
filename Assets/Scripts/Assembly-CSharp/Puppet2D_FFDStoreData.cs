[global::UnityEngine.ExecuteInEditMode]
public class Puppet2D_FFDStoreData : global::UnityEngine.MonoBehaviour
{
	public global::System.Collections.Generic.List<global::UnityEngine.Transform> FFDCtrls = new global::System.Collections.Generic.List<global::UnityEngine.Transform>();

	public global::System.Collections.Generic.List<int> FFDPathNumber = new global::System.Collections.Generic.List<int>();

	[global::UnityEngine.HideInInspector]
	public bool Editable = true;

	private void Update()
	{
		if (!Editable)
		{
			return;
		}
		for (int num = FFDCtrls.Count - 1; num >= 0; num--)
		{
			if (FFDCtrls[num] == null)
			{
				FFDCtrls.RemoveAt(num);
			}
		}
	}
}
