using Godot;
using System;

public partial class Lounge : Area2D
{
	// Called when the node enters the scene tree for the first time.
	private Timer regenTimer;
	private Player player;
	public override void _Ready()
	{
		GD.Print("Detector ready");
		BodyEntered += _on_lounge_body_entered; // cannot detect without it
		BodyExited += _on_lounge_body_exited; // cannot detect without it

		regenTimer = GetNode<Timer>("Timer");
	}
	private void _on_lounge_body_entered(Node2D body)
	{
		if (body is Player p)
		{
			regenTimer.Autostart = true;
			player = p;
			regenTimer.Start();
			GD.Print("neko nyaaa");
		}
	}

	private void _on_lounge_body_exited(Node2D body)
	{
		if (player != null)
		{
			GD.Print("GET BACK TO WORK WAGECUCK");
			player.decay = 1;
			regenTimer.Stop();
			regenTimer.Autostart = false;
		}
	}
	private void _on_timer_timeout()
	{
		player.decay = 0;
		player.health += 1;
	}
	public override void _PhysicsProcess(double delta)
	{
		// var bodies = GetOverlappingBodies();
		// GD.Print("Bodies overlapping: ", bodies.Count);
		// foreach (Node2D b in bodies)
		//     GD.Print(" - ", b.Name);
	}
}
