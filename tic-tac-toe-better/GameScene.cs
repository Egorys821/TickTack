using Godot;
using System;
using System.Text;
using TicTacToeBetter;
using TicTakToe;

/// <summary>
/// Root node of the game field scene. Builds the <see cref="Game"/>, the grid of
/// <see cref="GameCell"/> buttons, and the <see cref="Mediator"/> that connects them.
/// </summary>
public sealed partial class GameScene : Node
{
    PackedScene GameCellScene;

    /// <summary>The grid container that hosts one <see cref="GameCell"/> button per field position.</summary>
    public GridContainer Container = null;
    Mediator GameMediator = null;


    /// <summary>The heads-up display showing whose turn it is and the end-of-game window.</summary>
    public Hud Hud;

    public override void _Ready()
    {

        Container = GetNode<GridContainer>("GridContainer");
        GameCellScene = GD.Load<PackedScene>(GamePaths.GameCellPath);
        Hud = GetNode<Hud>("HUD");
        Hud.gameScene = this;


        CreateNewGame();
    }

    /// <summary>
    /// Builds a fresh <see cref="Game"/> and <see cref="Mediator"/> from <see cref="Variables"/>,
    /// clears any existing cell buttons from <see cref="Container"/>, and repopulates it.
    /// Called on scene start and again when the player chooses "play again".
    /// </summary>
    public void CreateNewGame()
    {
        Variables.Game = new GameBuilder()
            .WithPlayer(Variables.Player1)
            .WithPlayer(Variables.Player2)
            .WithInitialFieldSize(Variables.GameSize)
            .WithWinLength(Variables.WinLen)
            .WithFixedFieldSize(Variables.FixedGameSize)
            .Build();


        GameMediator = new Mediator(Variables.Game, this);

        // Clear previous buttons
        foreach (var item in Container.GetChildren())
        {
            item.QueueFree();
        }

        Container.Columns = Variables.Game.FieldWidth;

        AddCells();
    }

    // Adding cells to container
    void AddCells()
    {
        for (int y = 0; y < Variables.Game.FieldHeight; y++)
        {
            for (int x = 0; x < Variables.Game.FieldWidth; x++)
            {
                GameCell cellInstance = GameCellScene.Instantiate<GameCell>();
                cellInstance.Init(new Coordinate(x, y), FieldType.Empty, GameMediator);
                Container.AddChild(cellInstance);
            }
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
