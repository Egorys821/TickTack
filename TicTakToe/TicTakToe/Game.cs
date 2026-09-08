using System.Runtime.InteropServices.JavaScript;
using System.Text;

namespace TicTakToe;

/// <summary>
/// Holds the state of a tic-tac-toe match and the rules for playing it: making moves,
/// growing the field, switching turns, and detecting wins or draws. Build one with
/// <see cref="GameBuilder"/> rather than constructing it directly.
/// </summary>
public class Game : ISubject
{
    /// <summary>Whether the game has ended, either by a win or a draw. No further moves are accepted once <c>true</c>.</summary>
    public bool IsGameDone { get; private set; } = false;
    private FieldType[,] field;

    /// <summary>The current width of the field, in cells.</summary>
    public int FieldWidth => this.field.GetLength(0);

    /// <summary>The current height of the field, in cells.</summary>
    public int FieldHeight => this.field.GetLength(1);

    /// <summary>The coordinate of the most recently made move.</summary>
    public Coordinate lastPlayed { get; private set; }
    private int winLength { get; set; }

    private bool fixedFieldSize;

    /// <summary>The player whose turn it currently is.</summary>
    public Player CurrentPlayer;
    private Player playerOne;
    private Player playerTwo;

    private List<IObserver> Observers = new List<IObserver>();

    

    private static List<Coordinate> _WinDirections;
    static Game()
    {
        _WinDirections = new();
        _WinDirections.Add(new Coordinate(1, 0));
        _WinDirections.Add(new Coordinate(0, 1));
        // arrow
        _WinDirections.Add(new Coordinate(1, 1));
        _WinDirections.Add(new Coordinate(-1, 1));

    }

    private bool IsValidCoordinate(Coordinate c)
    {
        return !(c.X >= FieldWidth || c.Y >= FieldHeight || c.X < 0 || c.Y < 0);
    }

    /// <summary>
    /// Checks whether the move at <paramref name="coordinate"/> won the game for
    /// <see cref="CurrentPlayer"/>, or whether the field is now full, and sets
    /// <see cref="IsGameDone"/> accordingly. Call this right after <see cref="TryMakeMove"/>
    /// succeeds, before <see cref="ChangePlayers"/>, since it evaluates lines through
    /// <paramref name="coordinate"/> using <see cref="CurrentPlayer"/>'s mark.
    /// </summary>
    /// <param name="coordinate">The coordinate of the move that was just made.</param>
    /// <returns>
    /// A <see cref="GameReport"/> describing the outcome: a win for <see cref="CurrentPlayer"/>,
    /// a draw if the field is full, or <see cref="WinCondition.UNKNOWN"/> if the game continues.
    /// </returns>
    public GameReport GetGameReport(Coordinate coordinate)
    {
        foreach (Coordinate direction in _WinDirections)
        {
            var LeftSide = new Coordinate(coordinate.X, coordinate.Y);
            var RightSide = new Coordinate(coordinate.X, coordinate.Y);
            bool canGoLeft = true;
            bool canGoRight = true;
            int count = 1;
            while (canGoLeft || canGoRight)
            {
                // move in directions
                if (canGoRight)
                    RightSide += direction;

                if (canGoLeft)
                    LeftSide -= direction;


                // right
                if (!IsValidCoordinate(RightSide) ||
                    field[RightSide.X, RightSide.Y] != CurrentPlayer.Type)
                {
                    canGoRight = false;
                    
                }
                else
                {
                    count++;
                }
                // left
                if (!IsValidCoordinate(LeftSide) ||
                    field[LeftSide.X, LeftSide.Y] != CurrentPlayer.Type)
                {
                    canGoLeft = false;
                   
                }
                else
                {
                    count++;
                }

                //if (count >= winLength) break;
            }

            if (count >= winLength)
            {

                IsGameDone = true;

                if (CurrentPlayer.Type == FieldType.X)
                {
                    //RightSide -= direction;
                    LeftSide += direction;

                    List<Coordinate> coords = new();
                    while (LeftSide != RightSide)
                    {
                        coords.Add(LeftSide);
                        LeftSide += direction;
                    }

                    return new GameReport() { WinCondition = WinCondition.X_WON, coordinates = coords.ToArray() };
                }
                if (CurrentPlayer.Type == FieldType.O)
                {
                    //RightSide -= direction;
                    LeftSide += direction;

                    List<Coordinate> coords = new();
                    while (LeftSide != RightSide)
                    {
                        coords.Add(LeftSide);
                        LeftSide += direction;
                    }

                    return new GameReport() { WinCondition = WinCondition.O_WON, coordinates = coords.ToArray() };
                }
            }

        }

        foreach (var c in field)
        {
            if (c == FieldType.Empty)
            {
                return new GameReport() { WinCondition = WinCondition.UNKNOWN, coordinates = null };
            }
        }

        IsGameDone = true;
        return new GameReport() { WinCondition = WinCondition.DRAW, coordinates = null };
    }
    /// <summary>
    /// Creates a new game. Prefer <see cref="GameBuilder"/> over calling this directly.
    /// </summary>
    /// <param name="playerA">The first player to move.</param>
    /// <param name="playerB">The second player.</param>
    /// <param name="initialFieldSize">The width and height of the initially square field, in cells. Defaults to 5.</param>
    /// <param name="winLength">The number of connected marks needed to win. Defaults to 3.</param>
    /// <param name="fixedFieldSize">
    /// If <c>true</c>, the field stays this size; if <c>false</c>, it grows whenever a move is
    /// made on its edge. Defaults to <c>true</c>.
    /// </param>
    internal Game(Player playerA, Player playerB, int initialFieldSize = 5, int winLength = 3, bool fixedFieldSize = true)
    {
        field = new FieldType[initialFieldSize, initialFieldSize];

        this.winLength = winLength;
        this.fixedFieldSize = fixedFieldSize;

        this.playerOne = playerA;
        this.playerTwo = playerB;

        this.CurrentPlayer = playerA;
    }

    /// <summary>
    /// Switches <see cref="CurrentPlayer"/> to the other player and notifies attached
    /// observers via <see cref="Notify"/>. Call this after a move that did not end the game.
    /// </summary>
    public void ChangePlayers()
    {

        if (CurrentPlayer == playerOne)
        {
            CurrentPlayer = playerTwo;
        }
        else
        {
            CurrentPlayer = playerOne;
        }

        Notify();
    }

    /// <summary>
    /// Renders the field as a text grid for display in a console, with row/column indices
    /// and <c>■</c>/<c>X</c>/<c>O</c> markers for empty, X, and O cells respectively.
    /// </summary>
    /// <returns>A multi-line string representation of the current field.</returns>
    public string GetGameState()
    {
        /*
         * ■■■■■
         * ■X■OO
         * ■X■■■
         * 
         */

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("width: " + FieldWidth + " Height: " + FieldHeight);

        sb.Append("  ");
        for (int i = 0; i < FieldWidth; i++)
        {
            sb.Append(i);
        }
        sb.Append('\n');

        for (int i = 0; i < FieldWidth; i++)
        {
            for (int j = 0; j < FieldHeight; j++)
            {
                if (j == 0)
                {
                    sb.Append(i.ToString("00") + " ");
                }

                switch (field[i, j])
                {
                    case FieldType.Empty:
                        sb.Append('■');
                        break;
                    case FieldType.X:
                        sb.Append('X');
                        break;
                    case FieldType.O:
                        sb.Append('O');
                        break;
                }

            }
            sb.Append('\n');
        }
        return sb.ToString();

    }

    private bool IsValidMove(Coordinate c, FieldType type)
    {
        return !(c.X < 0 ||
            c.Y < 0 ||
            c.X >= FieldWidth ||
            c.Y >= FieldHeight ||
            field[c.X, c.Y] != FieldType.Empty);
    }

    private void ExpandField(Direction direction, int count = 1)
    {
        FieldType[,] newField;
        Coordinate offset;

        switch (direction)
        {
            case Direction.UP:
                newField = new FieldType[FieldWidth, FieldHeight + count];
                offset = new Coordinate(0, count);
                break;
            case Direction.DOWN:
                newField = new FieldType[FieldWidth, FieldHeight + count];
                offset = new Coordinate(0, 0);
                break;
            case Direction.LEFT:
                newField = new FieldType[FieldWidth + count, FieldHeight];
                offset = new Coordinate(count, 0);
                break;
            case Direction.RIGHT:
                newField = new FieldType[FieldWidth + count, FieldHeight];
                offset = new Coordinate(0, 0);
                break;
            case Direction.ALL:
                newField = new FieldType[FieldWidth + count * 2, FieldHeight + count * 2];
                offset = new Coordinate(count, count);
                break;
            default:
                throw new ArgumentException();
        }

        for (int i = 0; i < FieldWidth; i++)
        {
            for (int j = 0; j < FieldHeight; j++)
            {
                newField[i + offset.X, j + offset.Y] = field[i, j];
            }

        }

        field = newField;

    }

    /// <summary>
    /// Attempts to place <see cref="CurrentPlayer"/>'s mark at <paramref name="c"/>. If the field
    /// is not fixed-size and the move lands on an edge, the field expands by one cell in that
    /// direction. This does not switch turns or check for a win; call <see cref="GetGameReport"/>
    /// and then <see cref="ChangePlayers"/> afterwards.
    /// </summary>
    /// <param name="c">The coordinate to play at.</param>
    /// <returns>
    /// <c>true</c> if the move was made; <c>false</c> if the game is already done, the
    /// coordinate is out of bounds, or the cell is already occupied.
    /// </returns>
    public bool TryMakeMove(Coordinate c)
    {
        if (IsGameDone)
            return false;

        // Validation
        if (!IsValidMove(c, CurrentPlayer.Type))
            return false;

        // Set tile
        SetTile(c, CurrentPlayer.Type);

        lastPlayed = c;

        if (fixedFieldSize)
            return true;

        // ExpandLogic

        if (c.X == 0)
            ExpandField(Direction.LEFT, 1);

        if (c.Y == 0)
            ExpandField(Direction.UP, 1);

        if (c.X == FieldWidth - 1)
            ExpandField(Direction.RIGHT, 1);

        if (c.Y == FieldHeight - 1)
            ExpandField(Direction.DOWN, 1);

        // TODO Say to mediator

        return true;
    }

    private void SetTile(Coordinate coordinate, FieldType type)
    {
        field[coordinate.X, coordinate.Y] = type;
    }

    /// <inheritdoc/>
    public void Attach(IObserver observer)
    {
        Observers.Add(observer);
    }

    /// <inheritdoc/>
    public void Detach(IObserver observer)
    {
        Observers.Remove(observer);
    }

    /// <inheritdoc/>
    public void Notify()
    {
        foreach (var observer in Observers)
        {
            observer.Update(this);
        }
    }
}
