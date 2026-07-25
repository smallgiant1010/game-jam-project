using Godot;
using System;

public partial class Thief : Enemy
{
   private enum State{ Stolen, Fleeing, Roaming, Caught }
   private State currentState = State.Roaming;
   [Export] int max;

   private int itemsStolen = 0;
   // Called when the node enters the scene tree for the first time.
   public override void _Ready()
   {
      base._Ready();
   }

   public override void _PhysicsProcess(double delta)
   {
      base._PhysicsProcess(delta);

      if (shapecast.IsColliding())
      {
         for (int i = 0; i < shapecast.GetCollisionCount(); i++)
         {
            if (shapecast.GetCollider(i) is Person person)
            {
               currentState = State.Caught;
               //add box dropping animation
            }
         }
      }

      if (currentState == State.Caught) return; //cant move when caught

   }

   private void _on_nav_timeout()
   {
      if (currentState == State.Fleeing)
      {
         if (navi.TargetPosition != goal.GlobalPosition) navi.TargetPosition = goal.GlobalPosition;
         if (goal is not Player && navi.TargetPosition == goal.GlobalPosition) getRandNode();
      }
   }

   private void _on_panic_zone_body_entered(Node2D body)
   {
      if (body is Person person)
      {
         currentState = State.Fleeing;
         navi.TargetPosition = navNodes[navNodes.Count - 1].GlobalPosition;
         Vector2 direction = (navi.TargetPosition - GlobalPosition).Normalized();
         Velocity = direction * Speed;
         MoveAndSlide();
      }
   }

   private void _on_steal_zone_body_entered(Node2D body)
   {
      if (navi.IsNavigationFinished())
      {
         Random rnd = new Random();
         int stealNum = rnd.Next(1, max);
         if (body is Aisle)
         {
            ((Aisle)body).Interact(stealNum);
         }
         if (body is Fridge)
         {
            ((Fridge)body).Interact(stealNum);
         }
         itemsStolen += stealNum;
      }
   }
   // Called every frame. 'delta' is the elapsed time since the previous frame.
   public override void _Process(double delta)
   {
      if (itemsStolen > 0) currentState = State.Stolen;

      if (navi.IsNavigationFinished())
      {

      }
   }
}
