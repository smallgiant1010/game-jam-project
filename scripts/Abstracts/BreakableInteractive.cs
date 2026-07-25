using Godot;
using System;

public partial class BreakableInteractive : Node2D
{
    [Export] private double breakdownDelay = 20f;
	[Export] private double breakdownProbability = 20f;
	[Signal]
	public delegate void BreakdownEventHandler(Task task);
	[Signal]
	public delegate void FixedEventHandler(Task task);
	public bool isBroken { get; private set; } = false;
	private bool hasSignaled = false;
	private double currentTime;
	protected Task assignedTask;
    public override void _Ready()
	{
		currentTime = 0f;
	}

    public void Interact()
    {
		isBroken = false;
		hasSignaled = false;
		EmitSignal("Fixed", (int)assignedTask);
    }
    
    public override void _Process(double delta)
	{
		currentTime += delta;
		if(!isBroken && currentTime >= breakdownDelay)
		{
			double gacha = Random.Shared.NextDouble() * 100f;
			if(gacha <= breakdownProbability)
			{
				isBroken = true;
				if(!hasSignaled)
				{
					EmitSignal("Breakdown", (int)assignedTask);
					hasSignaled = true;
				}
			}
			currentTime = 0;
		}
	}
}
