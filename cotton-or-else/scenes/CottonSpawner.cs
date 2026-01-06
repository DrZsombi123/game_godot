using Godot;
using System;
using System.Collections.Generic;

public partial class CottonSpawner : Node2D
{
    [Export] public PackedScene CottonScene;
    [Export] public NodePath MarkersPath;

    private Node2D _markersParent;
    private RandomNumberGenerator _rng = new RandomNumberGenerator();

    public override void _Ready()
    {
        _markersParent = GetNode<Node2D>(MarkersPath);
        _rng.Randomize();

        StartSpawnLoop();
    }

    private async void StartSpawnLoop()
    {
        while (true)
        {
            float waitTime = _rng.RandfRange(5f, 10f);
            await ToSignal(GetTree().CreateTimer(waitTime), "timeout");

            TrySpawnCotton();
        }
    }

    private void TrySpawnCotton()
    {
        List<Marker2D> emptyMarkers = new();

        foreach (Node child in _markersParent.GetChildren())
        {
            if (child is Marker2D marker && marker.GetChildCount() == 0)
            {
                emptyMarkers.Add(marker);
            }
        }

        if (emptyMarkers.Count == 0)
            return;

        Marker2D chosenMarker = emptyMarkers[_rng.RandiRange(0, emptyMarkers.Count - 1)];

        Node2D cottonInstance = CottonScene.Instantiate<Node2D>();
        chosenMarker.AddChild(cottonInstance);
        cottonInstance.GlobalPosition = chosenMarker.GlobalPosition;
    }
}