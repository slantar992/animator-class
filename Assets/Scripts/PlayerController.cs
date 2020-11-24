
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private const string HORIZONTAL = "Horizontal";
	private const string VERTICAL = "Vertical";
	private const string JUMP = "Jump";

	private float HorizontalAxis => Input.GetAxisRaw(HORIZONTAL);
	private float VerticalAxis => Input.GetAxisRaw(VERTICAL);
	private bool JumpButton => Input.GetButtonDown(JUMP);

	public float moveSpeed;
	public float jumpHeight;
	public float timeToReachHeight;
	public float timeToReachFloor;
	public Transform bottom;
	public LayerMask floorLayerMask;

	private bool onAir;
	private Collider[] jumpColliders;
	private Vector3 velocity;
	private RaycastHit hit;
	private float jumpSpeed;
	private float upwardsGravity;
	private float downwardsGravity;
	private Vector2 axes;

	private void Start() => CalculateJumpParameters();

	public void CalculateJumpParameters()
	{
		upwardsGravity = 2 * jumpHeight / Mathf.Pow(timeToReachHeight, 2);
		downwardsGravity = 2 * jumpHeight / Mathf.Pow(timeToReachFloor, 2);
		jumpSpeed = upwardsGravity * timeToReachHeight;
	}

	private void Update()
	{
		axes = new Vector2(HorizontalAxis, VerticalAxis).normalized;
		velocity = new Vector3(axes.x * moveSpeed, velocity.y, axes.y * moveSpeed);

		if (!onAir && JumpButton)
		{
			velocity = new Vector3(velocity.x, jumpSpeed, velocity.z);
			onAir = true;
		}

		if (onAir)
		{
			if (velocity.y > 0)
			{
				velocity = new Vector3(velocity.x, velocity.y - upwardsGravity * Time.deltaTime, velocity.z);
			}
			else
			{
				velocity = new Vector3(velocity.x, velocity.y - downwardsGravity * Time.deltaTime, velocity.z);
			}
		}

		transform.position = transform.position + velocity * Time.deltaTime;

		if (onAir)
		{
			if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f, floorLayerMask))
			{

				velocity = new Vector3(velocity.x, 0, velocity.z);
				transform.position = new Vector3(transform.position.x, hit.point.y + 1, transform.position.z);
				onAir = false;
			}
		}
		else if (!Physics.Raycast(transform.position, Vector3.down, 1.1f, floorLayerMask))
		{
			onAir = true;
		}
	}
}
