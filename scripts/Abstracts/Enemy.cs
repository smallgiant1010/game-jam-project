using Godot;
using System;
using System.Collections.Generic;

public partial class Enemy : CharacterBody2D
{
	public const float Speed = 300.0f;
	[Export] public Node2D goal = null;
	[Export] protected NavigationAgent2D navi;
	[Export] protected Area2D aoe;
	protected ShapeCast2D shapecast;
	protected Timer aggro; // can also be used to trigger hazard, i.e aggro timer runs out, kid spills drink on floor

	[Export] protected Godot.Collections.Array<Node2D> navNodes = new Godot.Collections.Array<Node2D>();

	public override void _Ready()
	{
		// GetNode<NavigationAgent2D>("NavigationAgent2D").TargetPosition = goal.GlobalPosition;
		shapecast = GetNodeOrNull<ShapeCast2D>("ShapeCast2D");
		aggro = GetNodeOrNull<Timer>("aggro");
		getRandNode();
	}
   
   public override void _PhysicsProcess(double delta)
	{
		if (navi.IsNavigationFinished())
		{
			Velocity = Vector2.Zero;  // keep this so it settles/resolves collisions properly, doesn't just freeze mid-air
		}
		else
		{
			Vector2 nextPathPosition = navi.GetNextPathPosition();
			Vector2 direction = (nextPathPosition - GlobalPosition).Normalized();
			Velocity = direction * Speed;
		}
		MoveAndSlide();
	}

	public void getRandNode()
   {
      Random rnd = new Random();
      goal = navNodes[rnd.Next(0, navNodes.Count - 1)]; // exclude last node (reserved as exit)
      navi.TargetPosition = goal.GlobalPosition;
      GD.Print(goal);
   }
}
