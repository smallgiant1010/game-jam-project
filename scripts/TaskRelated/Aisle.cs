using System;
using Godot;
public partial class Aisle : Node2D
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
	public override void _Ready()
	{
		productValue = minimumProductValue + (float)Random.Shared.NextDouble() * (maximumProductValue - minimumProductValue);
		currentProductCount = maxProductCount;
	}

	public void Interact(int amount)
	{
		if(currentProductCount == 0 && currentProductCount + amount > 0)
		{
			EmitSignal("RestockCompleted", (int)Task.RestockAisle);
			hasSignaled = false;
		}
		currentProductCount = Math.Max(0, currentProductCount + amount);
	}

    public override void _Process(double delta)
    {
        if(currentProductCount == 0)
		{
			if(!hasSignaled)
			{
				EmitSignal("RestockRequired", (int)Task.RestockAisle);
				hasSignaled = true;
			}
		}
    }
}
