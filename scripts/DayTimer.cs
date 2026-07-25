using Godot;
using System;

public partial class DayTimer : Timer
{
	public static DayTimer Instance { private set; get; }
	public override void _EnterTree()
	{
		Instance = this;
	}
}
