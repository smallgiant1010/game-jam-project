using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
	public const float Speed = 300.0f;
	[Export] public Node2D goal;

  public override void _Ready()
  {
    GetNode<NavigationAgent2D>("NavigationAgent2D").TargetPosition = goal.GlobalPosition;
  }
  public override void _PhysicsProcess(double delta)
  {
    base._PhysicsProcess(delta);
  }
}
