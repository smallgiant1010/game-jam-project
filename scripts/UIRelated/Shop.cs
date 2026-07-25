using Godot;
using System;

public partial class Shop : Control
{
	[Export] private Button nextDayButton_;
    public override void _Ready()
    {
		nextDayButton_.Pressed += OnPressed;
    }

	public void OnPressed()
	{
		SceneManager.Instance.LoadLevel(Scenes.DayScene.DAY_UID);
		GameManager.Instance.EmitSignal("StartGame");
		DayTimer.Instance.Start(GameManager.Instance.GetTime() * 60.0);
	}
}
