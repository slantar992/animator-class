using UnityEngine;

public struct BoxFace
{
	public Vector3 a;
	public Vector3 b;
	public Vector3 c;
	public Vector3 d;
	public Vector3 direction;

	public BoxFace(Vector3 a, Vector3 b, Vector3 c, Vector3 d, Vector3 direction)
	{
		this.a = a;
		this.b = b;
		this.c = c;
		this.d = d;
		this.direction = direction.normalized;
	}

	public Vector3 Center => new Vector3
	(
		(a.x + b.x + c.x + d.x) / 4,
		(a.y + b.y + c.y + d.y) / 4,
		(a.z + b.z + c.z + d.z) / 4
	);

	public Vector3 PointDirection => Vector3.Scale(Center, direction);
}
