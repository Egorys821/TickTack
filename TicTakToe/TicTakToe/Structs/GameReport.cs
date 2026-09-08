using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTakToe;

/// <summary>
/// The result of evaluating the field after a move, as produced by <see cref="Game.GetGameReport"/>.
/// </summary>
public struct GameReport
{
    /// <summary>The outcome of the game after the move that was evaluated.</summary>
    public WinCondition WinCondition;

    /// <summary>
    /// The coordinates that form the winning line, or <c>null</c> if <see cref="WinCondition"/>
    /// is <see cref="WinCondition.DRAW"/> or <see cref="WinCondition.UNKNOWN"/>.
    /// </summary>
    public Coordinate[] coordinates;
}