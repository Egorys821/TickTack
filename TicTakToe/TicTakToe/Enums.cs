namespace TicTakToe;

public enum FieldType
{
    Empty,
    X,
    O
}

public enum WinCondition
{
    DRAW,
    X_WON,
    O_WON,
    UNKNOWN
}

enum Direction
{
    LEFT, RIGHT, UP, DOWN, ALL
}
