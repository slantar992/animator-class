using UnityEngine;

public class Field : MonoBehaviour
{
	[SerializeField] private Vector2 fieldLimits = new Vector2(5, 5);

	private void OnValidate()
	{
		transform.eulerAngles = new Vector3(90, 0, 0);
		transform.position = new Vector3(fieldLimits.x * .5f, -0.005f, fieldLimits.y * .5f);
		transform.localScale = new Vector3(fieldLimits.x, fieldLimits.y, 1);
	}

	public Vector3 Clamp(Vector3 position)
	{
		return new Vector3
		(
			Mathf.Clamp(position.x, 0, fieldLimits.x),
			position.y,
			Mathf.Clamp(position.z, 0, fieldLimits.y)
		);
	}
}