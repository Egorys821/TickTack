using System;
using System.Collections.Generic;
using System.Text;

namespace TicTakToe;

public class Player
{
    public string? Name { get; init; } = null;
    public FieldType Type { get; init; }

    public string WinCelebration { get; init; } = "";

    public Player(string name, FieldType type, string WC="")
    {
        this.Name = name;
        this.Type = type;
        this.WinCelebration = WC;
    }
    public override string ToString()
    {
        return $" {Name}({Type})";
    }

    public string GetGameName()
    {
        return "";
    }

}
