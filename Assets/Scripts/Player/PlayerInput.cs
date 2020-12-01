
using System;
using UnityEngine;

[Serializable]
public class PlayerInput
{
	[Header("Axes")]
	[SerializeField] private string Horizontal = "Horizontal";
	[SerializeField] private string Vertical = "Vertical";
	[SerializeField] private string Jump = "Jump";

	public float HorizontalAxis => GetAxisValue(Horizontal);
	public float VerticalAxis => GetAxisValue(Vertical);
	public Vector2 AxesValue => new Vector2(HorizontalAxis, VerticalAxis);
	public bool JumpButtonDown => Input.GetButtonDown(Jump);
	public bool JumpButton => Input.GetButton(Jump);

	private float GetAxisValue(string axis)
	{
		if (Input.anyKey)
		{
			Debug.Log("Any Key");
			return Input.GetAxisRaw(axis);
		}
		else
		{
			return Input.GetAxis(axis);
		}
	}
}