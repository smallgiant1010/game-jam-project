using Godot;
using System;

public partial class VendingMachine : BreakableInteractive
{
	private float itemUpcharge = 0.5f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
	}

	private void OnPlayerInteraction()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		base._Process(delta);
	}
}
