using Godot;
using System;

public partial class Karen : Enemy
{
	// Called when the node enters the scene tree for the first time.
	private bool inSight = false;
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Ready()
	{
		base._Ready();
	}

	public override void _PhysicsProcess(double delta)
	{
		if (raycast.IsColliding())
		{
			var collider = raycast.GetCollider();
			if (collider is Player player)
			{
				aggro.Start();
				inSight = true;
				goal = player;
				// begin chase on player
			}
		}

		if (inSight)
		{
			Vector2 nextPathPosition = navi.GetNextPathPosition();
			Vector2 direction = (nextPathPosition - GlobalPosition).Normalized();
			Velocity = direction * Speed;
			MoveAndSlide();
		}
		else
		{
			Velocity = Vector2.Zero; // roam, so this is a placeholder for now
			MoveAndSlide(); // still call this so friction/collision resolution stays consistent
		}
	}

	private void _on_aggro_timeout()
	{
		GD.Print("Enemy deaggroed");
		inSight = false;
	}

	private void _on_nav_timeout()
	{
		if (navi.TargetPosition != goal.GlobalPosition) navi.TargetPosition = goal.GlobalPosition;
	}

	private void _on_area_2d_area_entered(Area2D area)
	{
		GD.Print("Entered by: ", area.Name, " | Parent: ", area.GetParent().Name);
		if (area.GetParent() is Player player)
		{
			player.decay += 1;
			GD.Print("Decay now: ", player.decay);
		}
	}

	private void _on_area_2d_area_exited(Area2D area)
	{
		if (area.GetParent() is Player player)
		{
			player.decay -= 1;
			GD.Print("Decay now: ", player.decay);
		}
	}
}
