
using System;
using UnityEngine;

[Serializable]
public class PlayerAnimation
{
	private const string PARAM_GLIDING = "Gliding";
	private const string PARAM_JUMP = "Jump";
	private const string PARAM_HEIGHT = "Height";
	private const string PARAM_RUN_SPEED = "RunSpeed";

	[SerializeField] private Transform model;
	private Animator animator;

	public bool Gliding { get; set; }
	public float Height { get; set; }
	public float RunSpeed { get; set; }
	public Vector2 Direction { get; set; }

	public void Init() => animator = model.GetComponent<Animator>();

	public void TriggerJump() => animator.SetTrigger(PARAM_JUMP);

	public void Update()
	{
		if (animator == null || model == null) return;
		SetAnimatorParams();
		RotateModel();
	}

	private void SetAnimatorParams()
	{
		animator.SetBool(PARAM_GLIDING, Gliding);
		animator.SetFloat(PARAM_HEIGHT, Height);
		animator.SetFloat(PARAM_RUN_SPEED, RunSpeed);
	}

	private void RotateModel()
	{
		if (!Gliding && Direction.magnitude > 0)
		{
			model.rotation = Quaternion.LookRotation(new Vector3(Direction.x, 0, Direction.y));
		}
	}
}