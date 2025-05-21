public class H_Control : global::UnityEngine.MonoBehaviour
{
	public int facingRight = -1;

	public global::UnityEngine.GameObject H_Object;

	public global::UnityEngine.GameObject Mon_1;

	public global::UnityEngine.GameObject Mon_2;

	public void Reset()
	{
		facingRight = -1;
		if (H_Object != null)
		{
			H_Object = null;
		}
		if (Mon_1 != null)
		{
			Mon_1 = null;
		}
		if (Mon_2 != null)
		{
			Mon_2 = null;
		}
	}
}
