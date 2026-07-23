using Godot;
using System;

public partial class Player : CharacterBody2D
{
	private int speed = 250;
	private RayCast2D raycast;
	[Export] public int health = 100; // patience
	public override void _Ready()
	{
		raycast = GetNode<RayCast2D>("RayCast2D");
		GD.Print(raycast);
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 direction = Vector2.Zero;

		if (Input.IsActionPressed("move_right"))
		{
			direction.X += 1;
		}

		if (Input.IsActionPressed("move_left"))
		{
			direction.X -= 1;
		}

		if (Input.IsActionPressed("move_down"))
		{
			direction.Y += 1;
		}

		if (Input.IsActionPressed("move_up"))
		{
			direction.Y -= 1;
		}

		if (Input.IsActionJustPressed("interact"))
		{
			GD.Print(raycast.IsColliding());
			if (raycast.IsColliding())
			{
				var collider = raycast.GetCollider();
				GD.Print(collider);
			}
			GD.Print("interacting");
		}
		// GD.Print(Position);
		Velocity = direction.Normalized() * speed;
		MoveAndSlide();
	}
}
