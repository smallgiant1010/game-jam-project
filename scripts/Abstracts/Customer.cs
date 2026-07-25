using Godot;
using System;

public partial class Customer : CharacterBody2D
{

   [Export] protected Godot.Collections.Array<Node2D> navNodes = new Godot.Collections.Array<Node2D>();

   [Export] protected NavigationAgent2D navi;
   protected RayCast2D raycast;
   protected Node2D currentNavTarget;
   public const float Speed = 300.0f;

   public override void _Ready()
   {
      raycast = GetNode<RayCast2D>("RayCast2D");

   }

   protected void GoToRandomNavNode()  //used to pick shelf for customer to walk to
   {
      Random rnd = new Random();
      currentNavTarget = navNodes[rnd.Next(0, navNodes.Count)];
      navi.TargetPosition = currentNavTarget.GlobalPosition;
   }
}