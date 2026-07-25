using Godot;
using System;

public partial class ManagerProjectile : Area2D
{
   [Export] public float Speed = 600f;
   public Vector2 Direction = Vector2.Right;

   public override void _Ready()
   {
      BodyEntered += OnBodyEntered;
   }

   public override void _PhysicsProcess(double delta)
   {
      Position += Direction * Speed * (float)delta;
   }

   private void OnBodyEntered(Node2D body)
   {
      if (body is Player player)
      {
         player.health -= 10;
      }
      QueueFree();
   }
}