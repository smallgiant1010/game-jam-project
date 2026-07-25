using Godot;
using System;

public partial class Kid : Enemy
{
	// Called when the node enters the scene tree for the first time.
	[Export] public Timer spillTime;
	PackedScene scene = GD.Load<PackedScene>("res://dog_poop.tscn");

	public override void _Ready()
	{
		Speed = 400; // idk placeholder for now
		base._Ready();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
	}
	private void _on_aggro_timeout() // functonally identical to nav timer
	{
		if (navi.IsNavigationFinished())
		{
			getRandNode(); // only pick a new wander point once actually arrived
		}
	}

	private void _on_spill_timeout()
	{
		if (rnd.Next(0, 1) == 0)
		{
			var instance = scene.Instantiate();
			AddChild(instance);
			GD.Print("oops all spills");
			// produce spill
		}
		// do nothing
	}
}
