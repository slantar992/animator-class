
using System;
using UnityEngine;

[Serializable]
public class PlayerLogic
{
	public event Action OnJumpTriggeredEvent;

	[SerializeField] private Field field;
	[SerializeField] private PlayerInput input;
	[SerializeField] private VerticalMovement verticalMovement;
	[SerializeField] private HorizontalMovement horizontalMovement;

	public Vector3 Velocity { get; set; }
	public Vector3 Position { get; set; }
	public bool IsGliding => verticalMovement.State == VerticalMovement.VerticalState.Gliding;
	public float AbsoluteSpeed => horizontalMovement.AbsoluteSpeed;
	public Vector2 Direction => horizontalMovement.Direction;

	public void Init()
	{
		RefreshInternalParams();
		Position = new Vector3(field.Limits.x * .5f, 0, 1);
	}

	public void RefreshInternalParams()
	{
		verticalMovement?.RefreshParameters();
	}

	public void Update()
	{
		SetHorizontalDirection();
		TryJump();
		TryGlide();

		CalculateNewPosition();

		TryStopJump();
		ClampPositionInArea();
	}

	private void SetHorizontalDirection() => horizontalMovement.Direction = new Vector2(input.HorizontalAxis, input.VerticalAxis);

	private void TryJump()
	{
		if (input.JumpButtonDown)
		{
			verticalMovement.Jump();
			if (verticalMovement.State == VerticalMovement.VerticalState.JumpStart)
			{
				OnJumpTriggeredEvent?.Invoke();
			}
		}
	}

	private void TryGlide() => verticalMovement.Glide(input.JumpButton);

	private void CalculateNewPosition()
	{
		horizontalMovement.Update();
		verticalMovement.Update();

		Velocity = new Vector3(horizontalMovement.Velocity.x, verticalMovement.Speed, horizontalMovement.Velocity.y);
		Position += Velocity * Time.deltaTime;
	}

	private void TryStopJump()
	{
		if (Position.y < 0)
		{
			verticalMovement.Stop();
		}
	}

	private void ClampPositionInArea()
	{
		if (field != null)
		{
			Position = field.Clamp(Position);
		}
		Position = new Vector3(
					Position.x,
					Position.y < 0 ? 0 : Position.y,
					Position.z);
	}
}