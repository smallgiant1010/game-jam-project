using Godot;
using System;

public partial class ItemCard : PanelContainer
{
	// Add Item that this references so it can be passed via signals
	[Signal]
	public delegate void BuyItemEventHandler();
	[Signal]
	public delegate void SellItemEventHandler();
	private int itemCount = 0;
	private Button buyButton_;
	private Button sellButton_;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		buyButton_ = GetNode<Button>("Buy");
		sellButton_ = GetNode<Button>("Sell");

		buyButton_.Pressed += OnBuyPressed;
		sellButton_.Pressed += OnSellPressed;
	}

    private void OnSellPressed()
    {
		--itemCount;
		buyButton_.Text = $"Buy:{itemCount}";
    }


    private void OnBuyPressed()
    {
		++itemCount;
		buyButton_.Text = $"Buy:{itemCount}";
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
