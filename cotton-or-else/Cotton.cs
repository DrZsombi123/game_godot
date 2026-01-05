using Godot;
using System;

public partial class Cotton : Area2D
{
    [Signal]
    public delegate void CottonPickedEventHandler();
    
    private bool playerNearby = false;

    private Player playerRef;

    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
        BodyExited += OnBodyExited;
    }

    private void OnBodyEntered(Node body){
        if(body is Player player){
            playerNearby = true;
            playerRef = player;
        }   
    }

    private void OnBodyExited(Node body){
        if(body is Player player){
            playerNearby = false;
            playerRef = null;
        }   
    }

    public override void _Process(double delta){
        if(playerNearby && Input.IsActionJustPressed("ui_accept"))
        {
           
            
            GameEvents.Instance.EmitCottonPicked();
            QueueFree();
        }
    }
}
