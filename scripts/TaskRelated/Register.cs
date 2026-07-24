using Godot;
using System;

public partial class Register : BreakableInteractive
{
    public override void _Ready()
    {
        base._Ready();
        assignedTask = Task.FixRegister;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
    }

}
