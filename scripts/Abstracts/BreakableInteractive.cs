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
	private Timer timer_;
	public bool isBroken { get; protected set; } = false;
	private bool hasSignaled = false;
	protected Task assignedTask;
	public override void _Ready()
	{
		timer_ = GetNode<Timer>("Timer");
		timer_.Timeout += OnTimeout;
		timer_.Start(breakdownDelay);
	}
	
	private void OnTimeout()
	{
		if(!isBroken)
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
			timer_.Start(breakdownDelay);
		}
	}

    public void Interact()
    {
		isBroken = false;
		hasSignaled = false;
		EmitSignal("Fixed", (int)assignedTask);
    }
    
    public override void _Process(double delta)
	{
	}
}
