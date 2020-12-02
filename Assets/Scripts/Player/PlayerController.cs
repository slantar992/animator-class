
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private PlayerLogic logic;
	//[SerializeField] private new PlayerAnimation animation;

	//private void Awake() => logic.OnJumpTriggeredEvent += animation.TriggerJump;
	private void Start()
	{
		logic.Init();
		//animation.Init();
	}
	private void OnValidate() => logic.RefreshInternalParams();

	private void Update()
	{
		UpdateLogic();
		//UpdateAnimation();
	}

	//private void OnDestroy() => logic.OnJumpTriggeredEvent -= animation.TriggerJump;


	private void UpdateLogic()
	{
		logic.Update();
		transform.position = logic.Position;
	}

	/*private void UpdateAnimation()
	{
		animation.Gliding = logic.IsGliding;
		animation.RunSpeed = logic.AbsoluteSpeed;
		animation.Direction = logic.Direction;
		animation.Height = transform.position.y;
		animation.Update();
	}*/
}
