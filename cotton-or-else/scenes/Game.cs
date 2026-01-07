using Godot;
using System;
using DialogueManagerRuntime;

public partial class Game : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
{
    MusicManager mm = (MusicManager)GetNode("/root/MusicManager");
    mm.PlaySelectedMusic();

	var dialogue = GD.Load<Resource>("res://kezdes.dialogue");
	DialogueManager.ShowDialogueBalloon(dialogue, "start");
}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
