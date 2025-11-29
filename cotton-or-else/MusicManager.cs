using Godot;

public partial class MusicManager : Node
{
    [Export] public AudioStream Music1;
    [Export] public AudioStream Music2;
    [Export] public AudioStream Music3;

    private AudioStreamPlayer _player;

    public int SelectedMusic = 0;

    public override void _Ready()
    {
        _player = new AudioStreamPlayer();
        AddChild(_player);
        _player.VolumeDb = 0;
        // Looping is set in Import tab
    }

    public void PlaySelectedMusic()
    {
        if (SelectedMusic == 0)
            _player.Stream = Music1;
        else if (SelectedMusic == 1)
            _player.Stream = Music2;
        else if (SelectedMusic == 2)
            _player.Stream = Music3;

        if (_player.Stream != null)
            _player.Play();
        else
            GD.Print("No stream assigned for SelectedMusic=" + SelectedMusic);
    }

    public void StopMusic()
    {
        _player.Stop();
    }
}