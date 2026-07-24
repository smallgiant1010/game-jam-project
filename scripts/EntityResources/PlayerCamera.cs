using Godot;
public partial class PlayerCamera : Camera2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Control control = GetNode<Control>("/root/MainScene/Control");
		ApplyLimits(GetViewport().GetWindow().Size, control.Size);
	}

	private void ApplyLimits(Vector2 viewport, Vector2 mapDimensions)
	{
		LimitTop = LimitLeft = 0;
		LimitBottom = (int)(mapDimensions.Y - (viewport.Y / 2));
		LimitRight = (int)(mapDimensions.X - (viewport.X / 2));
	}
}
