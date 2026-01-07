using DialogueManagerRuntime;
using Godot;
using System;
using DialogueManagerRuntime;

public partial class Game : Node2D
{
	[Export] Resource Dialogue;
	[Export] PackedScene endscene;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
{;
	DialogueManager.DialogueEnded += OnDialogueEnded;
	DialogueManager.ShowDialogueBalloon(Dialogue,"start");
    
}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void OnDialogueEnded(Resource dialogue)
	{
		DialogueManager.DialogueEnded -= OnDialogueEnded;
		MusicManager mm = (MusicManager)GetNode("/root/MusicManager");
    	mm.PlaySelectedMusic();
		Nigakiller.dialogueend = true;
		GetTree().CallGroup("hud", "StartTimer");
	}

}
