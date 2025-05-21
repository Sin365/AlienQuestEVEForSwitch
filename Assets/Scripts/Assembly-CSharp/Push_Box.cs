public class Push_Box : global::UnityEngine.MonoBehaviour
{
	public bool onLift_V;

	public bool onLift_H;

	public float Speed_for_PC;

	public global::UnityEngine.Transform pos_C;

	public global::UnityEngine.Transform pos_C_2;

	private void Start()
	{
	}

	private void FixedUpdate()
	{
		global::UnityEngine.Debug.DrawLine(pos_C.position, pos_C_2.position, global::UnityEngine.Color.green);
		global::UnityEngine.RaycastHit2D raycastHit2D = global::UnityEngine.Physics2D.Linecast(pos_C.position, pos_C_2.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
		if (raycastHit2D.collider != null)
		{
			if (raycastHit2D.collider.gameObject.GetComponent<Tile_Lift>() != null)
			{
				if (raycastHit2D.collider.gameObject.GetComponent<Tile_Lift>().Type == 0)
				{
					base.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(base.GetComponent<UnityEngine.Rigidbody2D>().velocity.x, 0f);
					base.transform.position = new global::UnityEngine.Vector3(base.transform.position.x, raycastHit2D.collider.transform.position.y + 2.16f, 0f);
					onLift_V = true;
					onLift_H = false;
				}
				else
				{
					base.transform.position = new global::UnityEngine.Vector3(base.transform.position.x + raycastHit2D.collider.GetComponent<Tile_Lift>().Speed_for_PC * global::UnityEngine.Time.deltaTime, base.transform.position.y, 0f);
					onLift_V = false;
					onLift_H = true;
				}
			}
			else if (raycastHit2D.collider.gameObject.GetComponent<Push_Box>() != null)
			{
				if (raycastHit2D.collider.gameObject.GetComponent<Push_Box>().onLift_V)
				{
					base.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(base.GetComponent<UnityEngine.Rigidbody2D>().velocity.x, 0f);
					base.transform.position = new global::UnityEngine.Vector3(base.transform.position.x, raycastHit2D.collider.transform.position.y + 3.5f, 0f);
				}
				else
				{
					base.transform.position = new global::UnityEngine.Vector3(base.transform.position.x + raycastHit2D.collider.GetComponent<Push_Box>().Speed_for_PC * global::UnityEngine.Time.deltaTime, base.transform.position.y, 0f);
				}
				onLift_V = false;
				onLift_H = false;
			}
			else
			{
				onLift_V = false;
				onLift_H = false;
			}
		}
		else
		{
			onLift_V = false;
			onLift_H = false;
		}
	}
}
