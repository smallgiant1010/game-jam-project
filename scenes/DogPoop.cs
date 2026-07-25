using Godot;
using System;

public partial class DogPoop : Area2D
{
	// Called when the node enters the scene tree for the first time.
	private int health = 5; // cleaning damages this
	private Timer timer;
	public override void _Ready()
	{
		timer = GetNode<Timer>("Tiemer");
	}

	public void interact()
	{
		health -= 1;
		if (health <= 0) QueueFree();
	}
	private void _on_timer_timeout()
	{
		SetDeferred("monitoring", true);
	}

	private void _on_hurtbox_area_entered(Node2D body) // CHECK OBJECT NAME FIRST, FOR NOW IT WILL ALWAYS PERMA STUN
	{
		if (body.GetParent() is Player player)
		{
			timer.Start();
			player.isStunned = true;
			player.health -= 5;
			SetDeferred("monitoring", false);

			player.stunTimer.Start(2);
			GD.Print(body.Name + " entered");
		}
	}
}
