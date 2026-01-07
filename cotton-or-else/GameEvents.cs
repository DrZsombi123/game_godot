using Godot;
using System;
using DialogueManagerRuntime;


public partial class GameEvents : Node
{
    
    public static GameEvents Instance { get; private set; }
    public int TotalCottonPicked = 0;
    public float TimeSurvived = 0f;
    [Signal]
    public delegate void CottonPickedEventHandler();

    public override void _Ready()
    {
        Instance = this;
    }

    public void EmitCottonPicked()
    {
        TotalCottonPicked++;
        EmitSignal(SignalName.CottonPicked);
        
        GD.Print(TotalCottonPicked);
    }

    public void ResetRun()
    {
        TotalCottonPicked = 0;
        TimeSurvived = 0f;
    }
}