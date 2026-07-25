using Godot;
using System;

public partial class PersonAndDog : Customer
{
	// Called when the node enters the scene tree for the first time.
	[Export] int max;
	[Export] private Timer shopTimer;
	private bool isPaused = false;

   // Called when the node enters the scene tree for the first time.
   public override void _Ready()
   {
      GoToRandomNavNode();
   }

   public override void _PhysicsProcess(double delta)
   {
      if (isPaused)
      {
         Velocity = Vector2.Zero;
         MoveAndSlide();
         return;
      }
      base._PhysicsProcess(delta);
      if (navi.IsNavigationFinished())
      {
         Velocity = Vector2.Zero;
         MoveAndSlide();

         if (state == ShoppingState.Roaming)
         {
            GD.Print("Reached: ", currentNavTarget.Name);
            isPaused = true;
            shopTimer.Start();

            //pause here for a bit
         }
         // else: AtRegister (or transitioning) — just stand still until state changes
      }
      else
      {
         Vector2 nextPathPosition = navi.GetNextPathPosition();
         Vector2 direction = (nextPathPosition - GlobalPosition).Normalized();
         Velocity = direction * Speed;
         MoveAndSlide();
      }
   }

   private void on_timer_timeout()
   {
      isPaused = false;
      if (state == ShoppingState.Roaming) GoToRandomNavNode();
   }

   private void _on_buy_zone_entered(Node2D body)
   {
      if (navi.IsNavigationFinished())
      {
         Random rnd = new Random();
         int buyNum = rnd.Next(1, max);
         float valueBought = 0;
         if (body is Aisle)
         {
            ((Aisle)body).Interact(buyNum);
            valueBought = ((Aisle)body).productValue;
         }
         if (body is Fridge)
         {
            ((Fridge)body).Interact(buyNum);
            valueBought = ((Fridge)body).productValue;
         }
         itemsBought += buyNum;
         totalValue += valueBought;
         numVisited += 1;
      }
   }
}