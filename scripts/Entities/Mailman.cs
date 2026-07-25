using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class Mailman : CharacterBody2D
{
   [Export] private Timer spawnTimer;
   [Export] private Timer stayTimer;
   [Export] private Node2D spawnPoint;
   // Called when the node enters the scene tree for the first time.
   public override void _Ready()
   {
      GlobalPosition = spawnPoint.GlobalPosition;
      spawnTimer.Timeout += OnSpawnTimerTimeout;
      stayTimer.Timeout += OnStayTimerTimeout;
      spawnTimer.Start();
      Visible = false;
   }

   private void OnSpawnTimerTimeout()
   {
      Visible = true;
      SetPhysicsProcess(true);
      stayTimer.Start();
   }

   private void OnStayTimerTimeout()
   {
      Visible = false;
      SetPhysicsProcess(false);
      spawnTimer.Start();
   }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
   public override void _Process(double delta)
   {
   }
}
