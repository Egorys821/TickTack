using Godot;
using System.Linq;
using TicTakToe;
namespace TicTacToeBetter;

/// <summary>
/// Connects <see cref="GameCell"/> clicks to the underlying <see cref="Game"/>: makes the move,
/// colors in a winning line, shows the end-game window, or advances to the next player.
/// </summary>
public class Mediator : IMediator
{
    Game game;
    GridContainer container;
    GameScene gameScene;

    /// <summary>Creates a mediator that drives <paramref name="game"/> for the cells hosted in <paramref name="gameScene"/>.</summary>
    /// <param name="game">The game whose moves this mediator applies.</param>
    /// <param name="gameScene">The scene whose <see cref="GameScene.Container"/> holds the cell buttons and whose <see cref="GameScene.Hud"/> shows results.</param>
    public Mediator(Game game, GameScene gameScene)
    {
        this.game = game;
        this.gameScene = gameScene;
        this.container = gameScene.Container;
    }

    /// <summary>
    /// Handles a <c>"click"</c> event from a <see cref="GameCell"/>: attempts the move, colors
    /// the winning line if one formed, shows the end-game window if the game is over, and
    /// otherwise switches to the next player. Events from other components or with other names are ignored.
    /// </summary>
    /// <param name="Component">The component that raised the event.</param>
    /// <param name="Event">The event name; only <c>"click"</c> from a <see cref="GameCell"/> is handled.</param>
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
