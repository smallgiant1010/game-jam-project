using Godot;
using System;

public partial class Kid : Enemy
{
	// Called when the node enters the scene tree for the first time.
	[Export] public Timer spillTime;
	private PackedScene scene;
	private Random rnd = new Random();

	public override void _Ready()
	{
		Speed = 400; // idk placeholder for now
		base._Ready();

		scene = GD.Load<PackedScene>("res://scenes/dog_poop.tscn");
		if (scene == null)
		{
			GD.PrintErr("Failed to load dog_poop.tscn!");
		}
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
		if (rnd.Next(0, 2) == 0)
		{
			var instance = scene.Instantiate<Node2D>();
			instance.GlobalPosition = GlobalPosition; // spawn at Kid's current position
			GetTree().CurrentScene.AddChild(instance); // add to scene root, not Kid
			GD.Print("oops all spills");
		}
	}
}
