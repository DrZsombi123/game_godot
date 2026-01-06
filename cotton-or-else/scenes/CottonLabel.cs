using Godot;

public partial class CottonLabel : Label
{
    public override void _Ready()
    {
        // Set initial value
        Text = "Cotton: " + GameEvents.Instance.TotalCottonPicked;

        // Connect to the signal
        GameEvents.Instance.CottonPicked += OnCottonPicked;
    }

    private void OnCottonPicked()
    {
        Text = "Cotton: " + GameEvents.Instance.TotalCottonPicked;
    }

    public override void _ExitTree()
    {
        // Clean up signal connection
        if (GameEvents.Instance != null)
            GameEvents.Instance.CottonPicked -= OnCottonPicked;
    }
}