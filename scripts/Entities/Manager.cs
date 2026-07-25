using Godot;
using System;

public partial class Manager : CharacterBody2D
{

   [Export] private PackedScene projectileScene;
   // Called when the node enters the scene tree for the first time.
   public override void _Ready()
   {
   }

   // Called every frame. 'delta' is the elapsed time since the previous frame.
   public override void _Process(double delta)
   {
   }

   private void on_attack_zone_body_entered(Node2D body)
   {
      if (body is Player player)
      {

      }
   }

   private void ShootProjectile(Vector2 fromPosition, Vector2 toPosition)
   {
      var projectile = projectileScene.Instantiate<ManagerProjectile>();
      projectile.GlobalPosition = fromPosition;
      projectile.Direction = (toPosition - fromPosition).Normalized();
      GetTree().Root.AddChild(projectile);
   }
}
