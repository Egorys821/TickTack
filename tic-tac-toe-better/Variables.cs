using Godot;
using System;
using TicTakToe;

public partial class Variables : Node
{
	public static Player Player1;
	public static Player Player2;

	public static Game Game;

	public static int GameSize;
    public static int WinLen;
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
