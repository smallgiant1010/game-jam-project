using Godot;
using System;
using System.Collections.Generic;

public partial class Market : Node
{
    public static Market Instance { private set; get; }
    [Export] private Area2D floor_;
    public List<Aisle> aisles;
    public List<BreakableInteractive> machines;
    private LinkedList<Enemy> enemies;
    public override void _Ready()
    {
        Instance = this;
        InitializeInteractives();
    }

    private void InitializeInteractives()
    {
        var allAisles = GetChildren();
        foreach (Node node in allAisles)
        {
            if (node is Aisle aisle)
            {
                aisles.Add(aisle);
            }
            else if (node is BreakableInteractive breakableInteractive)
            {
                machines.Add(breakableInteractive);
            }
        }
    }

    public LinkedListNode<Enemy> AddEnemy(Enemy enemy)
    {
        return enemies.AddLast(enemy);
    }
    
    public void RemoveEnemy(LinkedListNode<Enemy> node)
    {
        enemies.Remove(node);
    }
}
