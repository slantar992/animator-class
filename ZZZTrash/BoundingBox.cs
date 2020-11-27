
using System.Collections.Generic;
using UnityEngine;

public struct BoundingBox
{
	public Vector3 min;
	public Vector3 max;

	public BoundingBox(Vector3 min, Vector3 max)
	{
		this.max = max;
		this.min = min;
	}

	public Vector3 Center => Vector3.Lerp(min, max, .5f);

	public Vector3 a => min;
	public Vector3 b => new Vector3(max.x, min.y, min.z);
	public Vector3 c => new Vector3(max.x, min.y, max.z);
	public Vector3 d => new Vector3(min.x, min.y, max.z);
	public Vector3 e => new Vector3(min.x, max.y, min.z);
	public Vector3 f => new Vector3(max.x, max.y, min.z);
	public Vector3 g => max;
	public Vector3 h => new Vector3(min.x, max.y, max.z);

	public BoxFace A => new BoxFace(a, b, c, d, Vector3.down);
	public BoxFace B => new BoxFace(e, f, g, h, Vector3.up);
	public BoxFace C => new BoxFace(c, d, h, g, Vector3.forward);
	public BoxFace D => new BoxFace(b, c, g, f, Vector3.right);
	public BoxFace E => new BoxFace(a, b, f, e, Vector3.back);
	public BoxFace F => new BoxFace(d, a, e, h, Vector3.left);

	public Dictionary<BoxSide, BoxFace> Faces => new Dictionary<BoxSide, BoxFace>
	{
		{BoxSide.Bottom, A},
		{BoxSide.Top, B},
		{BoxSide.Forward, C},
		{BoxSide.Right, D},
		{BoxSide.Back, E},
		{BoxSide.Left, F},
	};
}