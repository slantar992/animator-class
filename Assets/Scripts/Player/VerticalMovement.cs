using System;
using UnityEngine;

[Serializable]
public class VerticalMovement
{
	public float Speed { get; private set; }

	[SerializeField] private bool active = true;
	[SerializeField, Min(0)] private float jumpHeight;
	[SerializeField, Min(0.0001f)] private float timeToReachHeight;
	[SerializeField, Min(0.0001f)] private float timeToReachFloor;
	[SerializeField, Min(0.0001f)] private float glidingTimeToReachFloor;

	private float jumpInitialSpeed;
	private float upwardsGravity;
	private float fallingGravity;
	private float glidingSpeed;
	public VerticalState State { get; private set; }

	public void RefreshParameters()
	{
		upwardsGravity = CalculateGravity(timeToReachHeight);
		fallingGravity = CalculateGravity(timeToReachFloor);
		glidingSpeed = jumpHeight / glidingTimeToReachFloor;
		jumpInitialSpeed = upwardsGravity * timeToReachHeight;
	}

	private float CalculateGravity(float time)
	{
		return 2 * jumpHeight / Mathf.Pow(time, 2);
	}

	public void Jump()
	{
		if (State == VerticalState.Stop)
		{
			State = VerticalState.JumpStart;
		}
	}

	public void Fall() => State = VerticalState.Falling;
	public void Stop() => State = VerticalState.Stop;

	public void Glide(bool active = true)
	{
		if (active && State == VerticalState.Falling)
		{
			State = VerticalState.Gliding;
		}
		else if (!active && State == VerticalState.Gliding)
		{
			State = VerticalState.Falling;
		}
	}

	public void Update()
	{
		if (!active) Stop();
		UpdateState();
	}

	private void UpdateState()
	{
		switch (State)
		{
			case VerticalState.Stop:
				Speed = 0;
				break;
			case VerticalState.JumpStart:
				Speed = jumpInitialSpeed;
				State = VerticalState.Rising;
				break;
			case VerticalState.Rising:
				UpdateSpeed(upwardsGravity);
				if (Speed < 0)
				{
					State = VerticalState.Falling;
				}
				break;
			case VerticalState.Falling:
				UpdateSpeed(fallingGravity);
				break;
			case VerticalState.Gliding:
				Speed = -glidingSpeed;
				break;
		}
	}

	private void UpdateSpeed(float gravity) => Speed -= gravity * Time.deltaTime;

	public enum VerticalState { Stop, JumpStart, Rising, Falling, Gliding }
}