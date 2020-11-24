using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerController))]
public class PlayerControllerEditor : Editor
{

	private PlayerController controller;

	private void Awake()
	{
		controller = (PlayerController)target;
	}

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		if (GUILayout.Button("Recalculate Jump Params"))
		{
			controller.CalculateJumpParameters();
		}
	}
}