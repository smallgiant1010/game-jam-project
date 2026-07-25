using Godot;
using System;

public enum Day
{
	SUNDAY = 0,
	MONDAY = 1,
	TUESDAY = 2,
	WEDNESDAY = 3,
	THURSDAY = 4,
	FRIDAY = 5,
	SATURDAY = 6,
}

public partial class GameManager : Node2D
{
	[Export] private float requiredMoney = 1000.0f;
	[Export] private float TimeTilLevelEndsInMinutes = 5f;
	[Signal]
	public delegate void StartGameEventHandler();
	[Signal]
	public delegate void EndGameEventHandler();
	public static GameManager Instance { private set; get; }
	public Day currentDay { private set; get; }
	private float currentMoney = 0f;

	public override void _Ready()
	{
		Instance = this;
		currentDay = Day.SUNDAY;
		DayTimer.Instance.Timeout += OnTimerTimeout;
	}

    public override void _ExitTree()
    {
        DayTimer.Instance.Timeout -= OnTimerTimeout;
    }


	private void OnTimerTimeout()
	{
		EmitSignal("EndGame");
		switch(currentDay)
		{
			case Day.SATURDAY:
				if(requiredMoney <= currentMoney)
				{
					
				} else
				{
					
				}
				break;
			case Day.FRIDAY:
				break;
			default:
				currentDay++;
				SpawnManager.Instance.customerSpawnProbability -= 10;
				SceneManager.Instance.LoadLevel(Scenes.ShopScene.SHOP_UID);
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
