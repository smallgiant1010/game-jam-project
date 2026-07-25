using Godot;
using System;
using System.Collections.Generic;

public partial class Customer : CharacterBody2D
{
   [Signal]
   public delegate void ReachedRegisterEventHandler(Customer customer);

   protected enum ShoppingState { Roaming, HeadingToRegister, AtRegister, HeadingToExit }
   protected ShoppingState state = ShoppingState.Roaming;

   [Export] protected Godot.Collections.Array<Node2D> navNodes = new Godot.Collections.Array<Node2D>();

   [Export] protected NavigationAgent2D navi;
   [Export] protected Node2D register;
   protected RayCast2D raycast;
   protected Node2D currentNavTarget;
   public float Speed = 300.0f;
   public int numVisited = 0;

   public int itemsBought;
   public float totalValue;

   public LinkedListNode<Customer> id;

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

   private void GoToRegister()
   {
      state = ShoppingState.HeadingToRegister;
      currentNavTarget = register;
      navi.TargetPosition = currentNavTarget.GlobalPosition;
   }

   private void GoToExit()
   {
      state = ShoppingState.HeadingToExit;
      currentNavTarget = navNodes[navNodes.Count - 1];
      navi.TargetPosition = currentNavTarget.GlobalPosition;
   }

   public override void _PhysicsProcess(double delta)
   {
      if (state == ShoppingState.Roaming && numVisited >= 3)
      {
         GoToRegister();
      }
      else if (state == ShoppingState.HeadingToRegister && navi.IsNavigationFinished())
      {
         state = ShoppingState.AtRegister;
         EmitSignal(SignalName.ReachedRegister, this);
      }
      else if (state == ShoppingState.HeadingToExit && navi.IsNavigationFinished())
      {
         QueueFree();
      }
   }

   public void OnTransactionComplete()
   {
      if (state == ShoppingState.AtRegister) GoToExit();
   }

   public void setRegister(Node2D reg)
   {
      register = reg;
   }
}