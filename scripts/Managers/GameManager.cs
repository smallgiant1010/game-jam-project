using Godot;
using System;

public enum Day
{
	SUNDAY,
	MONDAY,
	TUESDAY,
	WEDNESDAY,
	THURSDAY,
	FRIDAY,
	SATURDAY,
}

public partial class GameManager : Node2D
{
	[Export] private float requiredMoney = 1000.0f;
	[Export] private float TimeTilLevelEndsInMinutes = 5f;
	public static GameManager Instance;
	private Day currentDay;
	private float currentMoney = 0f;
	public bool GameStarted = false;
	public override void _EnterTree()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			QueueFree();
		}
	}

	public override void _Ready()
	{
		currentDay = Day.SUNDAY;
		DayTimer.Instance.Timeout += OnTimerTimeout;
	}

	private void OnTimerTimeout()
	{
		GameStarted = false;
		switch(currentDay)
		{
			case Day.SATURDAY:
				break;
			case Day.FRIDAY:
				break;
			default:
				currentDay += 1;
				break;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("pause"))
		{
			if (DayTimer.Instance.Paused)
			{
				Engine.TimeScale = 1f;
				DayTimer.Instance.Paused = false;
			}
			else
			{
				Engine.TimeScale = 0f;
				DayTimer.Instance.Paused = true;
			}
		}
	}

	public void ChangeMoney(float money)
	{
		currentMoney += money;
	}

	public float GetTime()
	{
		return TimeTilLevelEndsInMinutes;
	}
}
