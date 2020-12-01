
using UnityEngine;

[ExecuteInEditMode]
public class CameraController : MonoBehaviour
{
	[SerializeField] private Field field;
	[SerializeField] private Transform target;
	[SerializeField] private float truckOffset = 1;
	[SerializeField] private float targetDistance = 1;
	[SerializeField] private float height = 10;
	[SerializeField, Range(0.01f, 0.1f)] private float smoothing = 0.1f;

	private Vector3 finalPosition;

	private void LateUpdate()
	{
		if (field == null || target == null) return;

		var absolutePositionInField = field.GetAbsoluteCoodinate(target.position);
		finalPosition =
			new Vector3(
				Mathf.Lerp(-truckOffset, field.Limits.x + truckOffset, absolutePositionInField.x),
				height,
				target.position.z - targetDistance);
		transform.LookAt(target);
		transform.position = Vector3.Lerp(transform.position, finalPosition, smoothing);

	}
}
