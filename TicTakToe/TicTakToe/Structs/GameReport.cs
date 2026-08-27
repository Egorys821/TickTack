using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTakToe;

public struct GameReport
{
    public WinCondition WinCondition;
    public Coordinate[] coordinates;
}