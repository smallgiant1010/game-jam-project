using Godot;
using System;

public partial class QuickTimeSlider : Control
{
	[Export] private ColorRect wholeZone_;
	[Export] private ColorRect validZone_;
	[Export] private ColorRect cursor_;
	private float minWidth, maxWidth;
	private float maxDist;
	private float angle = 0;
	private bool isStopped = false;
	public override void _Ready()
	{
		maxWidth = wholeZone_.Size.X / 8;
		minWidth = wholeZone_.Size.X / 40;
		maxDist = wholeZone_.Size.X / 2;
		GenerateValidZone();
	}

	private void GenerateValidZone()
	{
		float width = minWidth + (float)(Random.Shared.NextDouble() * (maxWidth - minWidth));
		Vector2 validZoneSize = new Vector2(width, wholeZone_.Size.Y);

		validZone_.Size = validZoneSize;

		float randomX = width - maxDist + (float)(Random.Shared.NextDouble() * (wholeZone_.Size.X - width));
		Vector2 validZonePos = new Vector2(randomX, 0f);

		validZone_.Position = validZonePos;
	}

	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("interactive_stop"))
		{
			isStopped = true;
		}
		if (!isStopped)
		{
			angle += (float)delta;
			float newX = Mathf.Sin(angle) * maxDist;
			Vector2 newPos = new Vector2(newX, 0f);
			cursor_.Position = newPos;
		}
		else
		{
			float cursorX = cursor_.Position.X;
			float validZoneX = validZone_.Position.X;
			if (cursorX >= validZoneX && cursorX <= validZoneX + validZone_.Size.X)
			{
				GD.Print("CONGRATS");
			}
			else
			{
				GD.Print("YOU SUCK");
			}
			QueueFree();
		}
	}
}
