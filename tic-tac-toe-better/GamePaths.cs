using Godot;
using System;

/// <summary>Centralizes the <c>res://</c> paths to the project's scene files.</summary>
public partial class GamePaths : Node
{
    /// <summary>Path to the main menu scene.</summary>
    public static string MainPath => "res://main.tscn";

    /// <summary>Path to the game field scene.</summary>
    public static string GameScenePath => "res://GameScene.tscn";

    /// <summary>Path to the individual game cell scene, instantiated once per field position.</summary>
    public static string GameCellPath => "res://game_cell.tscn";
}
