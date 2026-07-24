using Godot;
using System;

public partial class BreakableInteractive : Node2D
{
    [Export] private double breakdownDelay = 20f;
    [Export] private double breakdownProbability = 20f;
    public bool isBroken { get; private set; } = false;
    private double currentTime;
    public override void _Ready()
	{
		currentTime = 0f;
	}

    private void OnFix()
    {
        isBroken = false;
    }
    
    public override void _Process(double delta)
	{
		currentTime += delta;
		if(currentTime >= breakdownDelay)
		{
			double gacha = Random.Shared.NextDouble() * 100f;
			if(gacha <= breakdownProbability)
			{
				isBroken = true;
			}
			currentTime = 0;
		}
	}
}
