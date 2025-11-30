using Godot;
using System;

public partial class OptionsMenu : Control
{
    [Export] public AudioStream Music1;
    [Export] public AudioStream Music2;
    [Export] public AudioStream Music3;

    private int selectedIndex = -1;

    private AudioStreamPlayer previewPlayer;

    public override void _Ready()
    {
        previewPlayer = GetNode<AudioStreamPlayer>("PreviewPlayer");

        GetNode<Button>("HBoxContainer/MusicOption1").Pressed += () => OnMusicOptionPressed(0);
        GetNode<Button>("HBoxContainer/MusicOption2").Pressed += () => OnMusicOptionPressed(1);
        GetNode<Button>("HBoxContainer/MusicOption3").Pressed += () => OnMusicOptionPressed(2);

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

    // Access the autoload singleton MusicManager
    MusicManager mm = (MusicManager)GetNode("/root/MusicManager");

    if (mm != null)
    {
        mm.SelectedMusic = selectedIndex;
        GD.Print("SelectedMusic set to: " + mm.SelectedMusic);
    }
    else
    {
        GD.Print("MusicManager singleton not found!");
    }
            GetTree().ChangeSceneToFile("res://scenes/MainMenu.tscn");
}
}
