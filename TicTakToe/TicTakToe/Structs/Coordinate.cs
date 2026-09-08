global using System.Collections.Generic;
global using System;
namespace TicTakToe;

/// <summary>
/// A zero-based (column, row) position on a <see cref="Game"/>'s field.
/// </summary>
public struct Coordinate
{
    /// <summary>The column index.</summary>
    public int X;

    /// <summary>The row index.</summary>
    public int Y;

    /// <summary>Returns a human-readable representation, e.g. <c>"X= 2 Y= 3"</c>.</summary>
    /// <returns>The coordinate formatted as <c>"X= {X} Y= {Y}"</c>.</returns>
    public override string ToString()
    {
        return "X= " + X + " Y= " + Y;
    }

    /// <summary>
    /// Checks whether <paramref name="s"/> can be parsed by <see cref="Coordinate(string)"/>,
    /// i.e. it is a non-null string of the form <c>"x,y"</c> with two parseable integers.
    /// </summary>
    /// <param name="s">The string to validate, or <c>null</c>.</param>
    /// <returns><c>true</c> if <paramref name="s"/> is a valid <c>"x,y"</c> coordinate string; otherwise <c>false</c>.</returns>
    public static bool IsValidStringForConvertionToCoordinate(string s)
    {
        if (s is null) return false;

        var parts = s.Split(',');
        if (parts.Length != 2)
        {
            return false;
        }
        try
        {
            int.Parse(parts[0]);
            int.Parse(parts[1]);
        }
        catch (Exception ex)
        {
            if (ex is ArgumentNullException || ex is OverflowException || ex is FormatException)
            {
                return false;
            }
            else
            {
                throw;
            }

        }


        return true;


    }
    /// <summary>Creates a coordinate from an explicit column and row.</summary>
    /// <param name="x">The column index.</param>
    /// <param name="y">The row index.</param>
    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }

    /// <summary>Parses a coordinate from a string of the form <c>"x,y"</c>, e.g. <c>"5,3"</c>.</summary>
    /// <param name="s">The string to parse.</param>
    /// <exception cref="ArgumentException">
    /// <paramref name="s"/> is <c>null</c> or not in the form <c>"x,y"</c> with two parseable integers;
    /// see <see cref="IsValidStringForConvertionToCoordinate"/>.
    /// </exception>
    public Coordinate(string s)
    {
        // check for valid string aa,bb
        if (!IsValidStringForConvertionToCoordinate(s))
        {
            throw new ArgumentException();
        }
        string[] parts = s.Split(',');
        this.X = int.Parse(parts[0]);
        this.Y = int.Parse(parts[1]);


    }

    /// <summary>Adds two coordinates component-wise.</summary>
    /// <param name="left">The first coordinate.</param>
    /// <param name="right">The second coordinate.</param>
    /// <returns>A coordinate whose <see cref="X"/> and <see cref="Y"/> are the sums of the operands'.</returns>
    public static Coordinate operator +(Coordinate left, Coordinate right)
    {
        return new Coordinate(left.X + right.X, left.Y + right.Y);
    }

    /// <summary>Subtracts one coordinate from another component-wise.</summary>
    /// <param name="left">The coordinate to subtract from.</param>
    /// <param name="right">The coordinate to subtract.</param>
    /// <returns>A coordinate whose <see cref="X"/> and <see cref="Y"/> are the differences of the operands'.</returns>
    public static Coordinate operator -(Coordinate left, Coordinate right)
    {
        return new Coordinate(left.X - right.X, left.Y - right.Y);
    }

    /// <summary>Checks whether two coordinates refer to the same (X, Y) position.</summary>
    /// <param name="left">The first coordinate.</param>
    /// <param name="right">The second coordinate.</param>
    /// <returns><c>true</c> if both coordinates have equal <see cref="X"/> and <see cref="Y"/>; otherwise <c>false</c>.</returns>
    public static bool operator ==(Coordinate left, Coordinate right)
    {
        return (left.X==right.X && left.Y == right.Y);
    }

    /// <summary>Checks whether two coordinates refer to different (X, Y) positions.</summary>
    /// <param name="left">The first coordinate.</param>
    /// <param name="right">The second coordinate.</param>
    /// <returns><c>true</c> if the coordinates differ in <see cref="X"/> or <see cref="Y"/>; otherwise <c>false</c>.</returns>
    public static bool operator !=(Coordinate left, Coordinate right)
    {
        return !(left == right);
    }
}

