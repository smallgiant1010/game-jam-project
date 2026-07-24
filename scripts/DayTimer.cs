using Godot;
using System;

public partial class DayTimer : Timer
{
	public static DayTimer Instance;
	public override void _EnterTree()
	{
		if(Instance == null)
		{
			Instance = this;
		} else
		{
			QueueFree();
		}
	}
}
