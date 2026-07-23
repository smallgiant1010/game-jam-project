using Godot;
using System;

public partial class Player : Node2D
{
	public static Player Instance;
	[Export] private float speed = 10.0f;
	[Export] private RemoteTransform2D remoteTransform2D_;
	private Vector2 direction;
	public override void _Ready()
	{
		Instance = this;
		direction = Vector2.Right;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 InputMovement = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		direction = InputMovement;

		Vector2 CurrentPosition = Position;
		CurrentPosition += InputMovement * speed;
		Position = CurrentPosition;
	}
}
