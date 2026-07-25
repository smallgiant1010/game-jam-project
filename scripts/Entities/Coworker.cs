using Godot;
using System;

public partial class Coworker : Enemy
{
	// Called when the node enters the scene tree for the first time.
	private bool brokeStuff = false;
	[Export] public Node2D lounge;
	[Export] public Node2D register;
	[Export] public Timer checktimer;
	private Random rnd = new Random();

	public override void _Ready()
	{
		// only ever move between lounge and cash reg
		goal = register; // exclude last node (reserved as exit)
		navi.TargetPosition = goal.GlobalPosition;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.

	public override void _PhysicsProcess(double delta)
	{
		if (navi.IsNavigationFinished() && !brokeStuff) // remain at register
		{
			Velocity = Vector2.Zero;  // keep this so it settles/resolves collisions properly, doesn't just freeze mid-air
		}
		else // move to lounge
		{
			Vector2 nextPathPosition = navi.GetNextPathPosition();
			Vector2 direction = (nextPathPosition - GlobalPosition).Normalized();
			Velocity = direction * Speed;
		}
		MoveAndSlide();
		// do nothing
	}

	private void _on_aggro_timeout()
	{
		if (rnd.Next(0, 6) == 0 && !brokeStuff)
		{
			brokeStuff = true;
			goal = lounge;
			checktimer.Autostart = true;
			checktimer.Start();

			GD.Print("reg broke");
		}
	}
	private void _on_check_fix_timeout()
	{
		// check if reg fixed every second, which will be done in the register.cs via signal
		if (brokeStuff)
		{
			checktimer.Autostart = false;
			brokeStuff = false;
			goal = register;
		}
	}
}
