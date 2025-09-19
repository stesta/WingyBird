using Godot;
using System;

public partial class Main : Node2D
{
    public int Score = 0;
    private Label _scoreDisplay;

    public override void _Ready()
    {
        _scoreDisplay = GetNode<Label>("%ScoreDisplay");
    }

    private void Restart()
    {
        var currentScene = GetTree().CurrentScene;
        if (currentScene != null)
        {
            GetTree().ReloadCurrentScene();
        }

        GetTree().Paused = false;
    }

    public void GateClearedSignalHandler()
    {
        Score++;
        _scoreDisplay.Text = $"Score: {Score}";
    }

    public void BirdCollidedSignalHandler()
    {
        GetTree().Paused = true;
        GD.Print("Game Over!");
    }
}
