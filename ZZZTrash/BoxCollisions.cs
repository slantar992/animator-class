
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BoxCollisions
{
	[SerializeField, Range(2, 10)] private int rayCountX = 5;
	[SerializeField, Range(2, 10)] private int rayCountY = 5;
	[SerializeField] private LayerMask mask;
	[SerializeField] private bool debugRays;
	[SerializeField] private bool debugHits;

	private readonly List<(Vector3, Vector3)> raysTriggered = new List<(Vector3, Vector3)>();
	private readonly List<BoxCollisionData> result = new List<BoxCollisionData>();
	private readonly List<Vector3> hits = new List<Vector3>();
	private RaycastHit hit;

	public BoxCollisionResult ProcessCollissions(Bounds bounds)
	{
		var boundingBox = new BoundingBox(bounds.min, bounds.max);
		CalculateCollisisons(boundingBox);
		var correction = CorrectPosition(boundingBox.Center);
		return new BoxCollisionResult
		{
			collisions = result,
			correctedPosition = correction
		};
	}

	private Vector3 CorrectPosition(Vector3 center)
	{
		Vector3 correctedPosition = center;

		foreach (var collision in result)
		{
			correctedPosition += collision.correction;
		}

		return correctedPosition;
	}

	Vector3 horizontalPosition;
	Vector3 verticalPosition;
	Vector3 endPoint;
	Vector3 reference;
	Vector3 direction;
	float distance;
	bool collide;

	private void CalculateCollisisons(BoundingBox boundingBox)
	{

		result.Clear();
		if (debugRays)
			raysTriggered.Clear();
		if (debugHits)
			hits.Clear();

		foreach (var entry in boundingBox.Faces)
		{
			collide = false;
			reference = entry.Value.a;
			for (int x = 0; x < rayCountX; x++)
			{
				horizontalPosition = Vector3.Lerp(reference, entry.Value.b, x / (rayCountX - 1f)) - reference;
				for (int y = 0; y < rayCountY; y++)
				{
					verticalPosition = Vector3.Lerp(reference, entry.Value.d, y / (rayCountY - 1f)) - reference;
					endPoint = horizontalPosition + verticalPosition + reference;
					distance = Vector3.Distance(boundingBox.Center, endPoint);
					direction = (endPoint - boundingBox.Center).normalized;
					if (entry.Key == BoxSide.Right)
					{
						Debug.Log(direction);
					}
					collide = Physics.Raycast(boundingBox.Center, direction, out hit, distance, mask);

					if (debugRays)
					{
						Debug.DrawRay(boundingBox.Center, direction);
						raysTriggered.Add((boundingBox.Center, endPoint));
					}

					if (collide)
					{

						if (debugHits)
							hits.Add(hit.point);

						result.Add(new BoxCollisionData
						{
							side = entry.Key,
							face = entry.Value
						});

						break;
					}
				}

				if (collide) break;
			}
		}
	}

	public void DebugRays()
	{
		if (debugHits)
		{
			Gizmos.color = Color.yellow;
			foreach (var hitElem in hits)
			{
				Gizmos.DrawWireSphere(hitElem, 0.01f);
			}
		}
	}

}