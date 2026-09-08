using System;
using System.Collections.Generic;
using System.Text;

namespace TicTakToe;

/// <summary>
/// A participant in a <see cref="Game"/>, identified by a display name and the
/// <see cref="FieldType"/> (<c>X</c> or <c>O</c>) they place on the field.
/// </summary>
public class Player
{
    /// <summary>The player's display name, or <c>null</c> if none was given.</summary>
    public string? Name { get; init; } = null;

    /// <summary>The mark (<see cref="FieldType.X"/> or <see cref="FieldType.O"/>) this player places on the field.</summary>
    public FieldType Type { get; init; }

    /// <summary>Text shown when this player wins a game. Empty if none was configured.</summary>
    public string WinCelebration { get; init; } = "";

    /// <summary>
    /// Creates a new player.
    /// </summary>
    /// <param name="name">The player's display name.</param>
    /// <param name="type">The mark this player places on the field.</param>
    /// <param name="WC">Text to show when this player wins. Defaults to an empty string.</param>
    public Player(string name, FieldType type, string WC="")
    {
        this.Name = name;
        this.Type = type;
        this.WinCelebration = WC;
    }

    /// <summary>Returns the player's name followed by their <see cref="Type"/> in parentheses, e.g. <c>" Adam(X)"</c>.</summary>
    /// <returns>A short human-readable description of the player.</returns>
    public override string ToString()
    {
        return $" {Name}({Type})";
    }

    /// <summary>
    /// Placeholder for a future game name; currently always returns an empty string.
    /// </summary>
    /// <returns>An empty string.</returns>
    public string GetGameName()
    {
        return "";
    }

}
