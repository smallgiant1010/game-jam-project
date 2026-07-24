using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
	public const float Speed = 300.0f;
	[Export] public Node2D goal = null;
	[Export] protected NavigationAgent2D navi;
	[Export] protected Area2D aoe;
	protected RayCast2D raycast;
	protected Timer aggro; // can also be used to trigger hazard, i.e aggro timer runs out, kid spills drink on floor

  public override void _Ready()
  {
    // GetNode<NavigationAgent2D>("NavigationAgent2D").TargetPosition = goal.GlobalPosition;
		raycast = GetNode<RayCast2D>("RayCast2D");
		aggro = GetNode<Timer>("aggro");
  }
}
