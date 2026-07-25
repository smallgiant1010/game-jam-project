using Godot;
using System.Collections.Generic;
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
	public static TaskList Instance { private set; get; }
	[Export] private VBoxContainer vBoxContainer_;
	private SortedDictionary<Task, int> tasks;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
		InitializeTasks();
		ConnectSignals();
	}

	private void InitializeTasks()
	{
		tasks.Add(Task.CleanPoop, 0);
		tasks.Add(Task.CleanSpill, 0);
		tasks.Add(Task.CatchRat, 0);
		tasks.Add(Task.FixRegister, 0);
		tasks.Add(Task.FixVendingMachine, 0);
		tasks.Add(Task.RestockAisle, 0);
		tasks.Add(Task.FixFridge, 0);
		tasks.Add(Task.RestockFridge, 0);
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

	// Create UI For Each Subscriber Method
	private void OnEvent(Task task)
	{
		tasks[task]++;
	}

	private void OnCompletion(Task task)
	{
		tasks[task]--;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
