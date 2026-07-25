using Godot;
using System;

public partial class Item : Node
{
	// Called when the node enters the scene tree for the first time.
	[Export] public string name = "item";
	[Export] public int cost = 1;
	[Export] public int buffVal = 5; // increase stat by x amount
	[Export] protected Timer buff;
	[Export] protected float buffDuration = (float).05; // if its .05, that means this is ainstant buff
	protected Player player;
	public virtual void useItem()
	{
		buff.Start(buffDuration);
	}
	private void _on_buff_duration_timeout()
	{
		// apply buff here
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
}
