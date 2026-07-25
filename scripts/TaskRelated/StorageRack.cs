using System;
using Godot;

public partial class StorageRack : Node2D
{
	[Export] private int maxProductCount;
	public int currentProductCount { get; private set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		currentProductCount = maxProductCount;
	}
	
	public void Interact(int amount)
	{
		currentProductCount = Math.Max(0, currentProductCount + amount);
		currentProductCount = Math.Min(maxProductCount, currentProductCount);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
