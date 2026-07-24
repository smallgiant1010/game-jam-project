using System;
using Godot;
public partial class Aisle : Node2D
{
	[Export] private float minimumProductValue = 5f;
	[Export] private float maximumProductValue = 50f;
	[Export] private int productCount;
	private float productValue;
	public override void _Ready()
	{
		productValue = minimumProductValue + (float)Random.Shared.NextDouble() * (maximumProductValue - minimumProductValue);
	}

	private void OnChangeProductCount(int amount)
	{
		productCount += amount;
	}

	public void interact()
	{
		GD.Print("restocking");
	}
}
