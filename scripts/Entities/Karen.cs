using Godot;
using System;

public partial class Karen : Enemy
{
	// Called when the node enters the scene tree for the first time.
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
				aoe.SetDeferred("monitoring", true);
				goal = player;
				navi.TargetPosition = goal.GlobalPosition;
			}
		}

		if (navi.IsNavigationFinished())
		{
			Velocity = Vector2.Zero;
			MoveAndSlide(); // keep this so it settles/resolves collisions properly, doesn't just freeze mid-air
		}
		else
		{
			Vector2 nextPathPosition = navi.GetNextPathPosition();
			Vector2 direction = (nextPathPosition - GlobalPosition).Normalized();
			Velocity = direction * Speed;
			MoveAndSlide();
		}
	}

	private void _on_aggro_timeout()
	{
		GD.Print("Enemy deaggroed");
		aoe.SetDeferred("monitoring", false);
		getRandNode();
	}

	private void _on_nav_timeout()
	{
		if (navi.TargetPosition != goal.GlobalPosition) navi.TargetPosition = goal.GlobalPosition;
		if (goal is not Player && navi.TargetPosition == goal.GlobalPosition) getRandNode(); // move somewhere else, lingering is intentional if the enemy draws the same node again
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
