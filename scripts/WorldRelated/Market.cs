using Godot;
using System;
using System.Collections.Generic;

public partial class Market : Node
{
    public static Market Instance { private set; get; }
    public List<Aisle> aisles;
    public List<BreakableInteractive> machines;
    public List<Register> registers;
    public List<StorageRack> storageRacks;
    private LinkedList<Enemy> enemies;
    private LinkedList<Customer> customers;
    public override void _Ready()
    {
        Instance = this;
        registers = [];
        machines = [];
        aisles = [];
        storageRacks = [];
        enemies = [];
        customers = [];
        GameManager.Instance.EndGame += OnEndGame;
        GameManager.Instance.StartGame += InitializeInteractives;
    }

    public override void _ExitTree()
    {
        GameManager.Instance.EndGame -= OnEndGame;
        GameManager.Instance.StartGame -= InitializeInteractives;
    }


    private void OnEndGame()
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.QueueFree();
        }

        foreach (Customer customer in customers)
        {
            customer.QueueFree();
        }

        enemies.Clear();
        customers.Clear();
    }


    private void InitializeInteractives()
    {
        var childrenOfRoot = GetTree().CurrentScene.GetNode<TileMapLayer>("InteractiveLayer");
        foreach (Aisle aisle in childrenOfRoot.GetNode<Node>("Aisles").GetChildren())
        {
            aisles.Add(aisle);
        }

        foreach (Fridge fridge in childrenOfRoot.GetNode<Node>("Fridges").GetChildren())
        {
            machines.Add(fridge);
        }

        foreach (VendingMachine vendingMachine in childrenOfRoot.GetNode<Node>("VendingMachines").GetChildren())
        {
            machines.Add(vendingMachine);
        }

        foreach (Register register in childrenOfRoot.GetNode<Node>("Registers").GetChildren())
        {
            machines.Add(register);
            registers.Add(register);
        }

        foreach (StorageRack storageRack in childrenOfRoot.GetNode<Node>("StorageRacks").GetChildren())
        {
            storageRacks.Add(storageRack);
        }
    }

    public LinkedListNode<Enemy> AddEnemy(ref Enemy enemy)
    {
        return enemies.AddLast(enemy);
    }

    public LinkedListNode<Customer> AddCustomer(ref Customer customer)
    {
        return customers.AddLast(customer);
    }

    public void RemoveEnemy(LinkedListNode<Enemy> node)
    {
        enemies.Remove(node);
    }

    public void RemoveCustomer(LinkedListNode<Customer> node)
    {
        customers.Remove(node);
    }
}
