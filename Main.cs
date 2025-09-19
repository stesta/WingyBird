using Godot;
using System;

public partial class Main : Node2D
{
    public int Score = 0;

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
        GD.Print($"Score: {Score}");
    }

    public void BirdCollidedSignalHandler()
    {
        GetTree().Paused = true;
        GD.Print("Game Over!");
    }
}
