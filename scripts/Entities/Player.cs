using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public partial class Player : CharacterBody2D
{
	[Export] public int speed = 500;
	[Export] public float health = 100; // patience
	[Export] public float maxHealth = 100; // patience
	[Export] public bool isStunned = false;
	[Export] public float decay = 1;
	[Export] public int live = 3;
	private RayCast2D raycast;
	private Control tasklist;
	public Timer stunTimer;
	private Area2D hurtbox;
	private PackedScene qteScene;
	private Mop mop;

	private Wrench wrench;

	private enum toolType { Mop, Wrench, None }
	private toolType currentTool;
	private List<Tool> tools = new List<Tool>();
	private Rat tempRat;
	public override void _Ready()
	{
		raycast = GetNode<RayCast2D>("RayCast2D");
		stunTimer = GetNode<Timer>("StunTimer");
		hurtbox = GetNode<Area2D>("hurtbox");
		mop = GetNode<Mop>("Mop");
		wrench = GetNode<Wrench>("Wrench");
		tasklist = GetNode<Control>("../tasklist/TaskList");
		qteScene = GD.Load<PackedScene>("res://scenes/QTE/quick_time_slider.tscn");
		tools.Add(mop);
		tools.Add(wrench);
		currentTool = toolType.None; //add press key event for equipping tool

		GD.Print(tasklist);
	}

	private void _on_stun_timer_timeout()
	{
		isStunned = false;
		hurtbox.SetDeferred("monitoring", true);
	}

	private void _on_patience_decay_timeout()
	{
		// pause if ur in lounge
		health -= 1 * decay;
		GD.Print(health);
	}

	private void qteEnd(bool isSuccessful)
	{
		isStunned = false;

		if (isSuccessful)
		{
			tempRat.Caught();
			tempRat = null;
			return;
		}
		tempRat.stunned = false;
	}
	public override void _Process(double delta)
	{
		if (health > maxHealth) health = maxHealth; // health capped

		if (health <= 0) GD.Print("you lost lmao what a loser"); // transform, ray grows in size and rotates rapidly

		if (Input.IsActionJustPressed("interact"))
		{
			GD.Print(raycast.IsColliding());
			if (raycast.IsColliding())
			{
				var collider = raycast.GetCollider();
				if (collider is Aisle a) a.Interact(1);
				if (collider is Rat r)
				{
					// load the scene lmao
					var instance = qteScene.Instantiate<Control>();
					tasklist.GetParent().AddChild(instance);
					QuickTimeSlider qte = GetNode<QuickTimeSlider>("../tasklist/Quick Time Slider");
					qte.Finish += qteEnd;

					isStunned = true; // stun player until they finish qte
					tempRat = r;
					r.stunned = true;
				}
				// else if (collider is Fridge b) b.
				GD.Print(collider);
			}
		}

		if (Input.IsActionJustPressed("tasklist"))
		{
			tasklist.Visible = !tasklist.Visible;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		if (!isStunned)
		{
			Vector2 direction = Vector2.Zero;

			if (Input.IsActionPressed("move_right"))
			{
				raycast.TargetPosition = new Vector2(28, 0);
				direction.X += 1;
			}

			if (Input.IsActionPressed("move_left"))
			{
				raycast.TargetPosition = new Vector2(-28, 0);

				direction.X -= 1;
			}

			if (Input.IsActionPressed("move_down"))
			{
				raycast.TargetPosition = new Vector2(0, 28); // player is taller than they are wide, raycast extended to compensate

				direction.Y += 1;
			}

			if (Input.IsActionPressed("move_up"))
			{
				raycast.TargetPosition = new Vector2(0, -28);

				direction.Y -= 1;
			}
			Velocity = direction.Normalized() * speed;
			MoveAndSlide();
		}
	}
}
