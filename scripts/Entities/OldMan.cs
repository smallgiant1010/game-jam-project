using Godot;
using System;
using System.Threading.Tasks;

public partial class OldMan : Enemy
{
	// Called when the node enters the scene tree for the first time.
	[Export] public Timer stunTimer;
	[Export] public Timer cdTimer;
	private bool activeThreat = true; // controls if it is monitorable
	private bool stunned = false;
	public override void _Ready()
	{
		Speed = 150; // meant to move slow
		base._Ready();
	}

	public override void _PhysicsProcess(double delta)
	{
		if (raycast.IsColliding())
		{
			var collider = raycast.GetCollider(0);
			GD.Print(collider);
			if (collider is Area2D area && area.GetParent() is Player player)
			{
				aggro.Start();
				if (activeThreat) aoe.SetDeferred("monitoring", true);
				goal = player;
				navi.TargetPosition = goal.GlobalPosition;
			}
		}

		if (!stunned) base._PhysicsProcess(delta);
		else
		{
			Velocity = Vector2.Zero;
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
			// stun player and take HUGE patience, then area2d goes on cd, also plays a long animation that pauses everything
			player.health -= (float)(.2 * player.maxHealth); // player takes 20% max health damage
			player.isStunned = true;
			player.stunTimer.Start(3); // stun player for 3 seconds

			aoe.SetDeferred("monitoring", false);
			activeThreat = false;
			stunned = true;

			stunTimer.Start();
			cdTimer.Start();

			// stun itself for 3 seconds, turn monitoring off for 6 seconds
			GD.Print("Health now: ", player.health);
		}
	}

	private void _on_self_stun_timeout()
	{
		stunned = false;

	}

	private void _on_cd_timer_timeout()
	{
		aoe.SetDeferred("monitoring", true);
		activeThreat = true; // order is intentional, helps make it so that 6 seconds will have passed before old man can act again
	}
}
