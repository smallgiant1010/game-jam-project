using Godot;
using System;

public partial class PersonAndDog : Customer
{
	// Called when the node enters the scene tree for the first time.
	[Export] int max;

   // Called when the node enters the scene tree for the first time.
   public override void _Ready()
   {
      GoToRandomNavNode();
   }

   // Called every frame. 'delta' is the elapsed time since the previous frame.
   public override void _Process(double delta)
   {
      if (navi.IsNavigationFinished())
      {
         GD.Print("Reached: ", currentNavTarget.Name);
         Velocity = Vector2.Zero;
         MoveAndSlide();

         //call interact function from Aisle.cs
         GoToRandomNavNode();
      }
      else
      {
         Vector2 nextPathPosition = navi.GetNextPathPosition();
         Vector2 direction = (nextPathPosition - GlobalPosition).Normalized();
         Velocity = direction * Speed;
         MoveAndSlide();
      }
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
      }
   }
}
