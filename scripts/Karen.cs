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
		aoe.AreaEntered += _on_area_2d_area_entered;
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
		GD.Print("Duelist Dash > Rapier m2 > Auto Reload > Bullseye > Duelist Dash > Bullseye > Needles eye");
		if (area.GetParent() is Player player)
		{
			player.decay += 1;
		}
		
	}
}
