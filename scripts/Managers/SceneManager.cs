using Godot;
using System;

public static class Scenes
{
    public static class DayScene
    {
        public const string DAY_UID = "";
    }
}

public partial class SceneManager : Node
{
    public static SceneManager Instance { get; private set; }
    public override void _Ready()
    {
        Instance = this;
    }

    public void LoadLevel(string uid)
    {
        GetTree().ChangeSceneToFile(uid);
    }

    public void ReloadCurrentLevel()
    {
        GetTree().ReloadCurrentScene();
    }
}
