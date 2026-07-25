using Godot;
using System;

public partial class Rat : Enemy
{
   // Called when the node enters the scene tree for the first time.
   public bool stunned = false;
   public override void _Ready()
   {
      base._Ready();
      SpawnManager.Instance.ChangeSpawnProbability(0.5);
      GD.Print(goal);
   }

   public void Caught()
   {
      // SpawnManager.Instance.ChangeSpawnProbability(2);
      Market.Instance.RemoveEnemy(id);
      QueueFree();
   }

   // Called every frame. 'delta' is the elapsed time since the previous frame.

   public override void _PhysicsProcess(double delta)
   {
      if(!stunned) base._PhysicsProcess(delta);
   }
}
