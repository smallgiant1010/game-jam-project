using Godot;
using System;
using System.Collections.Generic;

public partial class Enemy : CharacterBody2D
{
	public const float Speed = 300.0f;
	[Export] public Node2D goal = null;
	[Export] protected NavigationAgent2D navi;
	[Export] protected Area2D aoe;
	public LinkedListNode<Enemy> id;
	protected RayCast2D raycast;
	protected Timer aggro; // can also be used to trigger hazard, i.e aggro timer runs out, kid spills drink on floor
	protected List<Node2D> navNodes = new List<Node2D>();

	public override void _Ready()
	{
		// GetNode<NavigationAgent2D>("NavigationAgent2D").TargetPosition = goal.GlobalPosition;
		raycast = GetNode<RayCast2D>("RayCast2D");
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

	public void getRandNode()
	{
		Random rnd = new Random();
		goal = navNodes[rnd.Next(0, navNodes.Count)];
		navi.TargetPosition = goal.GlobalPosition; 
		GD.Print(goal);
	}
}
