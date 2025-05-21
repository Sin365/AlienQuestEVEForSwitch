public class H8_Dummy : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject Ctrl_1;

	private void Flip()
	{
		if (Ctrl_1.GetComponent<Puppet2D_GlobalControl>().flip)
		{
			Ctrl_1.GetComponent<Puppet2D_GlobalControl>().flip = false;
			base.transform.position = new global::UnityEngine.Vector3(base.transform.position.x + 5.9f, base.transform.position.y, 0f);
		}
		else
		{
			Ctrl_1.GetComponent<Puppet2D_GlobalControl>().flip = true;
			base.transform.position = new global::UnityEngine.Vector3(base.transform.position.x - 5.9f, base.transform.position.y, 0f);
		}
	}
}
