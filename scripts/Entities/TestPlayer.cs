using Godot;
using System;

public partial class TestPlayer : CharacterBody2D
{
	[Export] private float SPEED = 1200.0f;

	public override void _PhysicsProcess(double delta)
	{
		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		Velocity = SPEED * direction;
		MoveAndSlide();
	}
}
