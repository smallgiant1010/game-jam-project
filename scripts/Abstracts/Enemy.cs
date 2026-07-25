using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Enemy : CharacterBody2D
{
	public float Speed = 300.0f;
	[Export] public Node2D goal = null;
	[Export] public int totalNodes = 7;
	[Export] protected NavigationAgent2D navi;
	[Export] protected Area2D aoe;
	public LinkedListNode<Enemy> id;
	protected ShapeCast2D raycast;
	protected Timer aggro; // can also be used to trigger hazard, i.e aggro timer runs out, kid spills drink on floor
	protected List<Node2D> navNodes = new List<Node2D>();
	protected int nodesVisited = 0;
	protected Random rnd = new Random();

	public override void _Ready()
	{
		// GetNode<NavigationAgent2D>("NavigationAgent2D").TargetPosition = goal.GlobalPosition;
		raycast = GetNode<ShapeCast2D>("RayCast2D");
		aggro = GetNode<Timer>("aggro");
		Node naviNodesContainer = GetNode<Node>("../Navi Nodes"); // adjust path as needed
		foreach (Node child in naviNodesContainer.GetChildren())
		{
			if (child is Node2D navPoint)
			{
				GD.Print("Nav point: ", navPoint.Name, " at ", navPoint.GlobalPosition);
				navNodes.Add(navPoint); // can prob be optimized by hard setting it idk
			}
		}
		getRandNode();

		// market.instance.RemoveEnemy(id) when enemy leaves
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


	public virtual void getRandNode()
	{
		nodesVisited++;
		if (nodesVisited != totalNodes)
		{
			goal = navNodes[rnd.Next(0, navNodes.Count)]; // can leave early, which is intentional
		}
		else // visited alloted nodes, reached its "max" lifetime
		{
			goal = navNodes[navNodes.Count - 1];
		}
		navi.TargetPosition = goal.GlobalPosition;
		GD.Print(goal);
	}
}
