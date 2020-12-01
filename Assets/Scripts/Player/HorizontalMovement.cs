
using System;
using UnityEngine;

[Serializable]
public class HorizontalMovement
{
	public Vector2 Direction { get; set; }
	public Vector2 Velocity { get; private set; }
	public float Speed => Velocity.magnitude;
	public float MaxSpeed => maxSpeed;
	public float AbsoluteSpeed => Speed / maxSpeed;

	[SerializeField] private float maxSpeed = 10;

	public void Update() => Velocity = Direction.normalized * Mathf.Min(Direction.magnitude, 1) * maxSpeed;
}