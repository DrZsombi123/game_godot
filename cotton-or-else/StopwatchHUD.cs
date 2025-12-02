using Godot;

public partial class StopwatchHUD : CanvasLayer
{
    private float _timeElapsed = 0f;
    private bool _running = true;
    private Label _label;

    public override void _Ready()
    {
        _label = GetNode<Label>("StopwatchLabel");
    }

    public override void _Process(double delta)
    {
        if (_running)
        {
            _timeElapsed += (float)delta;
            _label.Text = FormatTime(_timeElapsed);
        }
    }

    private string FormatTime(float seconds)
    {
        int minutes = (int)(seconds / 60);
        int secs = (int)(seconds % 60);
        int ms = (int)((seconds - Mathf.Floor(seconds)) * 100);

        return $"{minutes:00}:{secs:00}.{ms:00}";
    }

    public void Stop()
    {
        _running = false;
    }
}