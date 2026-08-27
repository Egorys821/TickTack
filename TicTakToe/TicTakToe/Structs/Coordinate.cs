global using System.Collections.Generic;
global using System;
namespace TicTakToe;

public struct Coordinate
{
    public int X;
    public int Y;

    public override string ToString()
    {
        return "X= " + X + " Y= " + Y;
    }
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
    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }
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

    public static Coordinate operator +(Coordinate left, Coordinate right)
    {
        return new Coordinate(left.X + right.X, left.Y + right.Y);
    }
    public static Coordinate operator -(Coordinate left, Coordinate right)
    {
        return new Coordinate(left.X - right.X, left.Y - right.Y);
    }
    public static bool operator ==(Coordinate left, Coordinate right)
    {
        return (left.X==right.X && left.Y == right.Y);
    }
    public static bool operator !=(Coordinate left, Coordinate right)
    {
        return !(left == right);
    }
}

