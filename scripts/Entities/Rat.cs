using Godot;
using System;

public partial class Rat : Enemy
{
   // Called when the node enters the scene tree for the first time.
   public override void _Ready()
   {
      base._Ready();
      SpawnManager.Instance.ChangeSpawnProbability(0.5);
   }

   // Called every frame. 'delta' is the elapsed time since the previous frame.
   public override void _Process(double delta)
   {
   }

   public override void _PhysicsProcess(double delta)
   {
      base._PhysicsProcess(delta);
   }
}
