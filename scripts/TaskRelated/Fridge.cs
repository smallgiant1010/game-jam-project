using Godot;
using System;

public partial class Fridge : BreakableInteractive
{
	[Export] private float minimumProductValue = 5f;
	[Export] private float maximumProductValue = 50f;
	[Export] private int productCount;
	private float productValue;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		productValue = minimumProductValue + (float)Random.Shared.NextDouble() * (maximumProductValue - minimumProductValue);
	}

	private void OnChangeProductCount(int amount)
	{
		productCount += amount;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		base._Process(delta);
	}
}
