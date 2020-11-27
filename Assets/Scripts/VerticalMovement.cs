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
	private float glidingGravity;
	private State state;

	public void RefreshParameters()
	{
		upwardsGravity = CalculateGravity(timeToReachHeight);
		fallingGravity = CalculateGravity(timeToReachFloor);
		glidingGravity = CalculateGravity(glidingTimeToReachFloor);
		jumpInitialSpeed = upwardsGravity * timeToReachHeight;
	}

	private float CalculateGravity(float time)
	{
		return 2 * jumpHeight / Mathf.Pow(time, 2);
	}

	public void Jump() => state = State.JumpStart;
	public void Fall() => state = State.Falling;
	public void Stop() => state = State.Stop;

	public void Glide(bool active = true)
	{
		if (active && state == State.Falling)
		{
			state = State.Gliding;
		}
		else if (!active && state == State.Gliding)
		{
			state = State.Falling;
		}
	}

	public void Update()
	{
		if (!active) Stop();
		UpdateState();
	}

	private void UpdateState()
	{
		switch (state)
		{
			case State.Stop:
				Speed = 0;
				break;
			case State.JumpStart:
				Speed = jumpInitialSpeed;
				state = State.Rising;
				break;
			case State.Rising:
				UpdateSpeed(upwardsGravity);
				if (Speed < 0)
				{
					state = State.Falling;
				}
				break;
			case State.Falling:
				UpdateSpeed(fallingGravity);
				break;
			case State.Gliding:
				UpdateSpeed(glidingGravity);
				break;
		}
	}

	private void UpdateSpeed(float gravity) => Speed -= gravity * Time.deltaTime;

	public enum State { Stop, JumpStart, Rising, Falling, Gliding }
}