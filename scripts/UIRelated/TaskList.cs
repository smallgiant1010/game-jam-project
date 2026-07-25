using Godot;
using System.Collections.Generic;
using System.Numerics;
public enum Task
{
	CleanPoop,
	CleanSpill,
	RestockAisle,
	RestockFridge,
	FixVendingMachine,
	FixRegister,
	FixFridge,
	CatchRat,
}

public partial class TaskList : Control
{
	[Export] private VBoxContainer vBoxContainer_;
	[Export] private PackedScene taskScene;
	private SortedDictionary<Task, Stack<TaskItem>> tasks;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Hide();
		InitializeTasks();
		ConnectSignals();
	}

	private void InitializeTasks()
	{
		tasks = new()
        {
            { Task.CleanPoop, new() },
            { Task.CleanSpill, new() },
            { Task.CatchRat, new() },
            { Task.FixRegister, new() },
            { Task.FixVendingMachine, new() },
            { Task.RestockAisle, new() },
            { Task.FixFridge, new() },
            { Task.RestockFridge, new() }
        };
	}

	private void ConnectSignals()
	{
		foreach (Aisle aisle in Market.Instance.aisles)
		{
			aisle.RestockRequired += OnEvent;
			aisle.RestockCompleted += OnCompletion;
		}

		foreach (BreakableInteractive breakableInteractive in Market.Instance.machines)
		{
			breakableInteractive.Breakdown += OnEvent;
			breakableInteractive.Fixed += OnCompletion;
		}
	}

    public override void _ExitTree()
    {
        foreach (Aisle aisle in Market.Instance.aisles)
		{
			aisle.RestockRequired -= OnEvent;
			aisle.RestockCompleted -= OnCompletion;
		}

		foreach (BreakableInteractive breakableInteractive in Market.Instance.machines)
		{
			breakableInteractive.Breakdown -= OnEvent;
			breakableInteractive.Fixed -= OnCompletion;
		}
    }

	// Create UI For Each Subscriber Method
	private void OnEvent(Task task)
	{
		TaskItem node = taskScene.Instantiate<TaskItem>();
		switch (task)
		{
			case Task.CatchRat:
				node.label_.Text = "Catch Rat";
				break;
			case Task.FixFridge:
				node.label_.Text = "Fix Fridge";
				break;
			case Task.FixRegister:
				node.label_.Text = "Fix Register";
				break;
			case Task.RestockAisle:
				node.label_.Text = "Restock Aisle";
				break;
			case Task.RestockFridge:
				node.label_.Text = "Restock Fridge";
				break;
			case Task.FixVendingMachine:
				node.label_.Text = "Fix Vending Machine";
				break;
			case Task.CleanSpill:
				node.label_.Text = "Clean Spill";
				break;
			case Task.CleanPoop:
				node.label_.Text = "Clean Poop";
				break;
		}

		tasks[task].Push(node);
		vBoxContainer_.AddChild(node);
	}

	private void OnCompletion(Task task)
	{
		TaskItem node = tasks[task].Pop();
		node.QueueFree();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
