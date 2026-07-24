using Godot;
using System;

public partial class VendingMachine : BreakableInteractive
{
	private float itemUpcharge = 0.5f;
	public override void _Ready()
	{
		base._Ready();
	}

	private void OnPlayerInteraction()
	{
		
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
	}
}
