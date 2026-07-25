using Godot;
using System;
using System.Collections.Generic;

public partial class CashRegister : Control
{
	[Export] private Godot.Collections.Array<PanelContainer> denominations;
	[Export] private Label totalCost;
	[Export] private Label amountGiven;
	[Export] private Button closeButton_;
	[Export] private Button transactionButton_;
	public Register register;
	public float totalAmount;
	public float changeRequired;
	private readonly Dictionary<string, float> values = new()
	{
		{ "HalfDollars", 0.5f },
		{ "Quarters", 0.25f },
		{ "Dimes", 0.10f },
		{ "Nickels", 0.05f },
		{ "Pennies", 0.01f }
	};
	private List<(Button button, Action handler)> connections;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Hide();
		closeButton_.Pressed += Hide;
		transactionButton_.Pressed += OnCompletedTransaction;
		InitializeDenominations();
	}

    public override void _ExitTree()
    {
		foreach ((Button button, Action action) in connections)
		{
			button.Pressed -= action;
		}

		connections.Clear();
    }

	private void InitializeDenominations()
	{
		connections = [];
		foreach (PanelContainer panelContainer in denominations)
		{
			Button remove = panelContainer.GetNode<Button>("Remove");
			Button add = panelContainer.GetNode<Button>("Add");

			Action removal = () =>
			{
				changeRequired += values[panelContainer.Name];
				UpdateUI();
			};

			remove.Pressed += removal;

			Action inclusion = () =>
			{
				changeRequired -= values[panelContainer.Name];
				UpdateUI();
			};

			add.Pressed += inclusion;

			connections.Add((remove, removal));
			connections.Add((add, inclusion));
		}
	}

    private void OnCompletedTransaction()
    {
		if (MathF.Abs(changeRequired) < 0.0001f)
		{
			GameManager.Instance.ChangeMoney(totalAmount);
			register.EmitSignal("TransactionComplete");
			if (register.customers.Count > 0)
			{
				Customer customer = register.customers.Peek();
				totalAmount = customer.totalValue;
				changeRequired = totalAmount - customer.payAmount;
			}
			else
			{
				totalAmount = 0f;
				changeRequired = 0f;
			}
		}
		UpdateUI();
    }

	private void UpdateUI()
	{
		totalCost.Text = $"Total Amount: ${Math.Round(totalAmount, 2)}";
		amountGiven.Text = $"Change Required: ${Math.Round(changeRequired, 2)}";
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
