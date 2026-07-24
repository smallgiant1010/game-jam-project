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

		base._PhysicsProcess(delta);
	}

	private void _on_aggro_timeout()
	{
		GD.Print("Enemy deaggroed");
		aoe.SetDeferred("monitoring", false);
		getRandNode();
	}

	private void _on_nav_timeout()
	{
		if (goal is Player)
		{
			navi.TargetPosition = goal.GlobalPosition; // keep tracking player position
		}
		else if (navi.IsNavigationFinished())
		{
			getRandNode(); // only pick a new wander point once actually arrived
		}
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
