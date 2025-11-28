using Godot;
using System;

public partial class OptionsMenu : Control
{
    [Export] public AudioStream Music1;
    [Export] public AudioStream Music2;
    [Export] public AudioStream Music3;

    private int selectedIndex = -1; // No selection yet

    private AudioStreamPlayer previewPlayer;

    public override void _Ready()
    {
        previewPlayer = GetNode<AudioStreamPlayer>("PreviewPlayer");

        GetNode<Button>("MusicOption1").Pressed += () => OnMusicOptionPressed(0);
        GetNode<Button>("MusicOption2").Pressed += () => OnMusicOptionPressed(1);
        GetNode<Button>("MusicOption3").Pressed += () => OnMusicOptionPressed(2);

        GetNode<Button>("ConfirmButton").Pressed += OnConfirmPressed;
    }

    private void OnMusicOptionPressed(int index)
    {
        selectedIndex = index;

        switch (index)
        {
            case 0:
                previewPlayer.Stream = Music1;
                break;
            case 1:
                previewPlayer.Stream = Music2;
                break;
            case 2:
                previewPlayer.Stream = Music3;
                break;
        }

        previewPlayer.Play();
    }

    private void OnConfirmPressed()
    {
        if (selectedIndex == -1)
        {
            GD.Print("No music selected!");
            return;
        }

        // Save the selected music so the Main Menu or game can load it
        MusicManager.SelectedMusic = selectedIndex;

        // Return to the Main Menu scene
        GetTree().ChangeSceneToFile("res://MainMenu.tscn");
    }
    
}