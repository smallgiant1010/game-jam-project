using Godot;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
public enum Task
{
	CleanPoop,
	CleanSpill,
	RestockAisle,
	FixVendingMachine,
	FixRegister,
	FixFridge,
	CatchRat,
}

public partial class TaskList : Control
{
	[Export] private VBoxContainer vBoxContainer_;
	private SortedDictionary<Task, int> tasks;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
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
	}

	private void ConnectSignals()
	{

	}

	private void OnEvent(Task task)
	{

	}

	private void OnCompletion(Task task)
	{

	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
