using Godot;
using System;

public partial class CashRegister : Control
{
	[Export] private PanelContainer halfDollars;
	[Export] private PanelContainer quarters;
	[Export] private PanelContainer dimes;
	[Export] private PanelContainer nickels;
	[Export] private PanelContainer pennies;
	[Export] private Label totalCost;
	[Export] private Label amountGiven;
	[Export] private Button closeButton_;
	[Export] private Button transactionButton_;
	public Register register;
	public float totalAmount;
	public float changeRequired;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Hide();
		closeButton_.Pressed += Hide;
		transactionButton_.Pressed += OnCompletedTransaction;
	}

    private void OnCompletedTransaction()
    {
        if(MathF.Abs(changeRequired) < 0.0001f)
		{
			register.EmitSignal("TransactionComplete");
			if(register.customers.Count > 0)
			{
				Customer customer = register.customers.Peek();
				totalAmount = customer.totalValue;
				changeRequired = totalAmount - customer.totalValue; 
			} else
			{
				totalAmount = 0f;
				changeRequired = 0f;
			}
		}
    }

	private void updateUI()
	{
		
	}

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		if (MathF.Abs(totalAmount) < 0.0001f && MathF.Abs(changeRequired) < 0.0001f)
		{
			transactionButton_.Disabled = true;
		}
		else
		{
			transactionButton_.Disabled = false;
		}
	}
}
