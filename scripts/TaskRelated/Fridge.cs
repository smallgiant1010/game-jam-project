using Godot;
using System;

public partial class Fridge : BreakableInteractive
{
	[Export] private float minimumProductValue = 5f;
	[Export] private float maximumProductValue = 50f;
	[Export] private int maxProductCount;
	[Signal]
	public delegate void RestockRequiredEventHandler(Task task);
	[Signal]
	public delegate void RestockCompletedEventHandler(Task task);
	private int currentProductCount;
	public bool hasSignaled { private get; set; }
	public float productValue { private set; get; }
	// Fix This Class
	public override void _Ready()
	{
		base._Ready();
		assignedTask = Task.FixFridge;
		productValue = minimumProductValue + (float)Random.Shared.NextDouble() * (maximumProductValue - minimumProductValue);
	}

	public void Interact(int amount)
	{
		if(currentProductCount == 0 && currentProductCount + amount > 0)
		{
			EmitSignal("RestockCompleted", (int)Task.RestockFridge);
			hasSignaled = false;
		}
		currentProductCount = Math.Max(0, currentProductCount + amount);
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		if(currentProductCount == 0)
		{
			if(!hasSignaled)
			{
				EmitSignal("RestockRequired", (int)Task.RestockFridge);
				hasSignaled = true;
			}
		}
	}
}
