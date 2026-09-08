using Godot;
using System;
using TicTakToe;

/// <summary>
/// Global, scene-independent settings shared between <see cref="Main"/> (where they are set)
/// and <see cref="GameScene"/> (where they are consumed to build the <see cref="Game"/>).
/// </summary>
public partial class Variables : Node
{
	/// <summary>The player using <see cref="FieldType.X"/>.</summary>
	public static Player Player1;

	/// <summary>The player using <see cref="FieldType.O"/>.</summary>
	public static Player Player2;

	/// <summary>The currently active game, or <c>null</c> before the first game scene is created.</summary>
	public static Game Game;

	/// <summary>The width and height of the field to create, in cells.</summary>
	public static int GameSize;

    /// <summary>The number of connected marks needed to win.</summary>
    public static int WinLen;

	/// <summary>Whether the field should stay a fixed size instead of growing as moves are made on its edges.</summary>
	public static bool FixedGameSize;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

}
