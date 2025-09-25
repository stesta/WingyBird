using Godot;
using System;

public partial class Main : Node2D
{
    public int Score = 0;
    private Label _scoreDisplay;
    private CanvasLayer _gameOverDisplay;
    private Button _reset;

    public override void _Ready()
    {
        _scoreDisplay = GetNode<Label>("%ScoreDisplay");
        _gameOverDisplay = GetNode<CanvasLayer>("%GameOver");
        _reset = GetNode<Button>("%Reset");

        _reset.Pressed += () =>
        {
            GetTree().Paused = false;
            _gameOverDisplay.Visible = false;

            var currentScene = GetTree().CurrentScene;
            if (currentScene != null)
            {
                GetTree().ReloadCurrentScene();
            }
        };
    }

    public void GateClearedSignalHandler()
    {
        Score++;
        _scoreDisplay.Text = $"Score: {Score}";
    }

    public void BirdCollidedSignalHandler()
    {
        _gameOverDisplay.Visible = true;
        GetTree().Paused = true;
        GD.Print("Game Over!");
    }
}
