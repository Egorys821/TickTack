using Godot;
using System.Linq;
using TicTakToe;
namespace TicTacToeBetter;

public class Mediator : IMediator
{
    Game game;
    GridContainer container;
    GameScene gameScene;

    public Mediator(Game game, GameScene gameScene)
    {
        this.game = game;
        this.gameScene = gameScene;
        this.container = gameScene.Container;
    }

    public void Notify(IComponent Component, string Event)
    {
        if (Component is GameCell && Event == "click")
        {
            GameCell cell = (GameCell)Component;

            if (game.TryMakeMove(cell.Location))
            {

                var report = game.GetGameReport(cell.Location);
                
                switch (report.WinCondition)
                {
                    case WinCondition.DRAW:
                        GD.Print("draw");
                        
                        break;
                    case WinCondition.X_WON:
                        ColorWiningCells(report.coordinates);
                        
                        break;
                    case WinCondition.O_WON:
                        ColorWiningCells(report.coordinates);

                        break;
                    case WinCondition.UNKNOWN:
                        break;
                    default:
                        break;
                }

                cell.FieldType = game.CurrentPlayer.Type;
                cell.UpdateUI();
                if (game.IsGameDone)
                {
                    DisableAllContainerButtons();
                    string EndWindowText;

                    if (report.WinCondition == WinCondition.DRAW)
                    {
                        EndWindowText = "Draw";
                    }
                    else
                    {
                        EndWindowText = game.CurrentPlayer + " win.\n" + game.CurrentPlayer.WinCelebration;
                    }
                    
                    gameScene.Hud.ShowEndWindow(EndWindowText);
                }
                else
                {
                    game.ChangePlayers();
                }

            }

        }
    }

    private void DisableAllContainerButtons()
    {
        foreach (var item in container.GetChildren().OfType<GameCell>())
        {
            item.Disabled = true;
        }
    }

    private void ColorWiningCells(Coordinate[] coordinates)
    {
        foreach (Node item in container.GetChildren())
        {
            if (item is GameCell gameCell)
            {
                if (coordinates.Contains(gameCell.Location))
                {
                    gameCell.ChangeColor(GameCell.GreenColor);
                }
            }
        }
    }

}
