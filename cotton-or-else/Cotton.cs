using Godot;
using System;

public partial class Cotton : Area2D
{
    public int cottonPicked = 0;
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
        if(playerNearby && Input.IsActionPressed("ui_accept"))
        {
           cottonPicked +=1;
            GD.Print(cottonPicked);
            QueueFree();
        }
    }
}
