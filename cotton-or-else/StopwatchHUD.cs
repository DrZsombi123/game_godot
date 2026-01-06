using Godot;

public partial class StopwatchHUD : CanvasLayer
{
    private float _timeElapsed = 0f;
    private bool _running = true;

    private Label _stopwatchLabel;
    private Label _hpLabel;

    public override void _Ready()
    {
        _stopwatchLabel = GetNode<Label>("StopwatchLabel");
        _hpLabel = GetNode<Label>("HpLabel");

        UpdateHp(3); // optional default value
    }

    public override void _Process(double delta)
    {
        if (_running)
        {
            _timeElapsed += (float)delta;
            _stopwatchLabel.Text = FormatTime(_timeElapsed);
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
        GameEvents.Instance.TimeSurvived = _timeElapsed;
    }

    // 👇 HP update function
    public void UpdateHp(int currentHp)
    {
        _hpLabel.Text = $"HP: {currentHp}";
    }
}