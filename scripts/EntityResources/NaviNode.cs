using Godot;
using System;

public partial class NaviNode : Node2D
{
	// Called when the node enters the scene tree for the first time.

	public override void _Draw()
	{
		DrawCircle(Vector2.Zero, 10, Colors.Red);
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.

}
