
using UnityEngine;

public class Field : MonoBehaviour
{
	[SerializeField] private Vector2 limits = new Vector2(10, 10);
	[SerializeField] private float height = 10;

	public Vector2 Limits => limits;

	private void OnValidate()
	{
		transform.eulerAngles = new Vector3(90, 0, 0);
		transform.position = new Vector3(limits.x * .5f, -height * .5f - 0.05f, limits.y * .5f);
		transform.localScale = new Vector3(limits.x, limits.y, height);
	}

	public Vector3 Clamp(Vector3 position)
	{
		return new Vector3
		(
			Mathf.Clamp(position.x, 0, limits.x),
			position.y,
			Mathf.Clamp(position.z, 0, limits.y)
		);
	}

	public Vector2 GetAbsoluteCoodinate(Vector3 worldPosition)
	{
		return new Vector2
		(
			Mathf.InverseLerp(0, limits.x, worldPosition.x),
			Mathf.InverseLerp(0, limits.y, worldPosition.y)
		);
	}
}