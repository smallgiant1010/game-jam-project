using Godot;
using System;
using System.Collections.Generic;

public partial class Register : BreakableInteractive
{
    public Queue<Customer> customers;
    [Signal]
    public delegate void TransactionCompleteEventHandler();
    private bool canBeInteractedWith = true;
    [Export] private PackedScene cashRegisterUI;
    private CashRegister cashRegister;
    public override void _Ready()
    {
        base._Ready();
        cashRegister = cashRegisterUI.Instantiate<CashRegister>();
        cashRegister.register = this;
        assignedTask = Task.FixRegister;
        customers = new();
    }

    public void OnReachedRegister(Customer customer)
    {
        customers.Enqueue(customer);
        TransactionComplete += customer.OnTransactionComplete;
        if(Math.Abs(cashRegister.totalAmount) < .00001f && Math.Abs(cashRegister.changeRequired) < .00001f)
        {
            cashRegister.totalAmount = customer.totalValue;
            cashRegister.changeRequired = customer.totalValue - customer.payAmount;
        }
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (isBroken) canBeInteractedWith = false;
        else canBeInteractedWith = true;
    }

}
