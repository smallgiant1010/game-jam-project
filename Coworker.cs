using Godot;
using System;

public partial class Coworker : Enemy
{
	// Called when the node enters the scene tree for the first time.
	private bool brokeStuff = false;
	[Export] public Node2D lounge;
	[Export] public Node2D register;

	public override void _Ready()
	{
		// only ever move between lounge and cash reg
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.

	public override void _PhysicsProcess(double delta)
	{
		// do nothing
	}

	public override void getRandNode()
	{
		// do nothing
	}


}
