using Godot;
using System;

public partial class GameEvents : Node
{
    public static GameEvents Instance { get; private set; }

    [Signal]
    public delegate void CottonPickedEventHandler();

    public override void _Ready()
    {
        Instance = this;
    }

    public void EmitCottonPicked()
    {
        EmitSignal(SignalName.CottonPicked);
    }
}