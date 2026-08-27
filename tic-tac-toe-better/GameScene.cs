using Godot;
using System;
using System.Text;
using TicTacToeBetter;
using TicTakToe;

public sealed partial class GameScene : Node
{
    PackedScene GameCellScene;
    public GridContainer Container = null;
    Mediator GameMediator = null;


    public Hud Hud;

    public override void _Ready()
    {

        Container = GetNode<GridContainer>("GridContainer");
        GameCellScene = GD.Load<PackedScene>(GamePaths.GameCellPath);
        Hud = GetNode<Hud>("HUD");
        Hud.gameScene = this;


        CreateNewGame();
    }

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
