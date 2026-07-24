using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public int speed = 500;
	[Export] public float health = 100; // patience
	[Export] public float maxHealth = 100; // patience
	[Export] public bool isStunned = false;
	[Export] public float decay = 1;
	[Export] public int live = 3;
	private RayCast2D raycast;
	private Timer stunTimer;
	private Area2D hurtbox;
	public override void _Ready()
	{
		raycast = GetNode<RayCast2D>("RayCast2D");
		stunTimer = GetNode<Timer>("StunTimer");
		hurtbox = GetNode<Area2D>("hurtbox");
	}
	// private void _on_hurtbox_area_entered(Node2D body) // CHECK OBJECT NAME FIRST, FOR NOW IT WILL ALWAYS PERMA STUN
	// {
	// 	isStunned = true;
	// 	hurtbox.SetDeferred("monitoring", false);

	// 	stunTimer.Start();
	// 	GD.Print(body.Name + " entered");
	// }
	private void _on_stun_timer_timeout()
	{
		isStunned = false;
		hurtbox.SetDeferred("monitoring", true);
	}

	private void _on_patience_decay_timeout()
	{
		// pause if ur in lounge

		health -= 1 * decay;
		// GD.Print(decay);
	}
	public override void _Process(double delta)
	{
		if (health > maxHealth) health = maxHealth; // health capped

		if (health <= 0) GD.Print("you lost lmao what a loser"); // transform

		if (Input.IsActionJustPressed("interact"))
		{
			GD.Print(raycast.IsColliding());
			if (raycast.IsColliding())
			{
				var collider = raycast.GetCollider();
				if (collider is Aisle a) a.interact();
				// else if (collider is Fridge b) b.
				GD.Print(collider);
			}
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		if (!isStunned)
		{
			Vector2 direction = Vector2.Zero;

			if (Input.IsActionPressed("move_right"))
			{
				raycast.TargetPosition = new Vector2(64, 0);
				direction.X += 1;
			}

			if (Input.IsActionPressed("move_left"))
			{
				raycast.TargetPosition = new Vector2(-64, 0);

				direction.X -= 1;
			}

			if (Input.IsActionPressed("move_down"))
			{
				raycast.TargetPosition = new Vector2(0, 96); // player is taller than they are wide, raycast extended to compensate

				direction.Y += 1;
			}

			if (Input.IsActionPressed("move_up"))
			{
				raycast.TargetPosition = new Vector2(0, -96);

				direction.Y -= 1;
			}
			// GD.Print(Position);
			Velocity = direction.Normalized() * speed;
			MoveAndSlide();
		}
	}
}
