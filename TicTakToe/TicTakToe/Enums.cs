namespace TicTakToe;

/// <summary>
/// The content of a single cell on the game field.
/// </summary>
public enum FieldType
{
    /// <summary>The cell has not been played yet.</summary>
    Empty,
    /// <summary>The cell has been claimed by the player using <c>X</c>.</summary>
    X,
    /// <summary>The cell has been claimed by the player using <c>O</c>.</summary>
    O
}

/// <summary>
/// The outcome of a <see cref="Game"/> as reported by <see cref="Game.GetGameReport"/>
/// after a move has been made.
/// </summary>
public enum WinCondition
{
    /// <summary>The field is full and neither player reached the required win length.</summary>
    DRAW,
    /// <summary>The player using <c>X</c> connected enough cells in a row to win.</summary>
    X_WON,
    /// <summary>The player using <c>O</c> connected enough cells in a row to win.</summary>
    O_WON,
    /// <summary>The game has not been decided yet; play should continue.</summary>
    UNKNOWN
}

enum Direction
{
    LEFT, RIGHT, UP, DOWN, ALL
}
