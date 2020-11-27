
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private const string HORIZONTAL = "Horizontal";
	private const string VERTICAL = "Vertical";
	private const string JUMP = "Jump";

	private float HorizontalAxis => Input.GetAxisRaw(HORIZONTAL);
	private float VerticalAxis => Input.GetAxisRaw(VERTICAL);
	private bool JumpButtonDown => Input.GetButtonDown(JUMP);
	private bool JumpButton => Input.GetButton(JUMP);

	public VerticalMovement verticalMovement;

	public float moveSpeed;
	public LayerMask floorLayerMask;
	public Transform field;
	public Vector2 fieldLimits = new Vector2(5, 5);

	private bool onAir;
	private Vector3 velocity;
	private Vector3 newPosition;
	private Vector2 axes;

	private void Start()
	{
		verticalMovement.RefreshParameters();
		transform.position = new Vector3(1, 0, 1);
	}

	private void OnValidate()
	{
		verticalMovement.RefreshParameters();
		if (field != null)
		{
			field.eulerAngles = new Vector3(90, 0, 0);
			field.position = new Vector3(fieldLimits.x * .5f, -0.005f, fieldLimits.y * .5f);
			field.localScale = new Vector3(fieldLimits.x, fieldLimits.y, 1);
		}
	}

	private void Update()
	{
		CalculateHorizontalMovement();
		TryJump();
		TryGlide();
		CalculateNewPosition();
		TryStopJump();
		ClampPositionInArea();

		transform.position = newPosition;
	}

	private void TryGlide()
	{
		Debug.Log(JumpButton);
		verticalMovement.Glide(onAir && JumpButton);
	}

	private void CalculateHorizontalMovement() => axes = new Vector2(HorizontalAxis, VerticalAxis).normalized;

	private void TryStopJump()
	{
		if (onAir && newPosition.y < 0)
		{
			verticalMovement.Stop();
			onAir = false;
		}
	}

	private void TryJump()
	{
		if (!onAir && JumpButtonDown)
		{
			verticalMovement.Jump();
			onAir = true;
		}
	}

	private void CalculateNewPosition()
	{
		verticalMovement.Update();
		velocity = new Vector3(axes.x * moveSpeed, verticalMovement.Speed, axes.y * moveSpeed);
		newPosition = transform.position + velocity * Time.deltaTime;
	}

	private void ClampPositionInArea()
	{
		newPosition = new Vector3(
					Mathf.Clamp(newPosition.x, 0, fieldLimits.x),
					newPosition.y < 0 ? 0 : newPosition.y,
					Mathf.Clamp(newPosition.z, 0, fieldLimits.y));
	}
}
