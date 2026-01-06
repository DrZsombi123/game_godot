using Godot;

public partial class DeathScreen : Control
{
    public override void _Ready()
    {
        var timeLabel = GetNode<Label>("TimeLabel");
        var cottonLabel = GetNode<Label>("CottonLabel");

        timeLabel.Text = $"Time Survived: {FormatTime(GameEvents.Instance.TimeSurvived)}";
        cottonLabel.Text = $"Cottons Picked: {GameEvents.Instance.TotalCottonPicked}";
    }

    private string FormatTime(float seconds)
    {
        int minutes = (int)(seconds / 60);
        int secs = (int)(seconds % 60);
        int ms = (int)((seconds - Mathf.Floor(seconds)) * 100);

        return $"{minutes:00}:{secs:00}.{ms:00}";
    }

    private void OnExitPressed()
    {
        GameEvents.Instance.ResetRun();
        
        GetTree().Quit();
    }
}