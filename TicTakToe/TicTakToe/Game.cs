using System.Runtime.InteropServices.JavaScript;
using System.Text;

namespace TicTakToe;

public class Game : ISubject
{
    public bool IsGameDone { get; private set; } = false;
    public FieldType[,] field;
    public int FieldWidth => this.field.GetLength(0);
    public int FieldHeight => this.field.GetLength(1);
    public Coordinate lastPlayed { get; private set; }
    private int winLength { get; set; }

    private bool fixedFieldSize;
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

    public GameReport GetGameReport()
    {
        Coordinate coordinate = lastPlayed;

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
    internal Game(Player playerA, Player playerB, int initialFieldSize = 5, int winLength = 3, bool fixedFieldSize = true)
    {
        field = new FieldType[initialFieldSize, initialFieldSize];

        this.winLength = winLength;
        this.fixedFieldSize = fixedFieldSize;

        this.playerOne = playerA;
        this.playerTwo = playerB;

        this.CurrentPlayer = playerA;
    }

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
        {
            ExpandField(Direction.LEFT, 1);
            lastPlayed = new Coordinate(lastPlayed.X + 1, lastPlayed.Y);
        }

        if (c.Y == 0)
        {
            ExpandField(Direction.UP, 1);
            lastPlayed = new Coordinate(lastPlayed.X , lastPlayed.Y + 1);
        }

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

    public void Attach(IObserver observer)
    {
        Observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        Observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (var observer in Observers)
        {
            observer.Update(this);
        }
    }
}
