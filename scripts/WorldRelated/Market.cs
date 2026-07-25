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
        foreach(Enemy enemy in enemies)
        {
            enemy.QueueFree();
        }

        foreach(Customer customer in customers)
        {
            customer.QueueFree();
        }

        enemies.Clear();
        customers.Clear();
    }


    private void InitializeInteractives()
    {
        machines = [];
        aisles = [];
        storageRacks = [];
        enemies = [];
        customers = [];
        var childrenOfRoot = GetTree().CurrentScene.GetChildren(true);
        foreach (Node node in childrenOfRoot)
        {
            if (node is Aisle aisle)
            {
                aisles.Add(aisle);
            }
            else if (node is BreakableInteractive breakableInteractive)
            {
                machines.Add(breakableInteractive);
                if(breakableInteractive is Register register)
                {
                    registers.Add(register);
                }
            } else if(node is StorageRack storageRack)
            {
                storageRacks.Add(storageRack);
            }
        }
    }

    public LinkedListNode<Enemy> AddEnemy(Enemy enemy)
    {
        return enemies.AddLast(enemy);
    }

    public LinkedListNode<Customer> AddCustomer(Customer customer)
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
