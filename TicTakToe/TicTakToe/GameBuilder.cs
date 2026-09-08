using System;
using System.Collections.Generic;
using System.Text;

namespace TicTakToe;

/// <summary>
/// Fluent builder for <see cref="Game"/>. Configure players and field settings with the
/// <c>With...</c> methods, then call <see cref="Build"/> to construct the game.
/// </summary>
public class GameBuilder
{
    // (Player playerA, Player playerB, int initialFieldSize, int winLength, bool fixedFieldSize)
    private Player[] _players = new Player[2];
    private int _initialFieldSize = 3;
    private int _winLength = 3;
    private bool _fixedFieldSize = false;

    /// <summary>Creates a builder with default settings: a 3x3 fixed-size field and a win length of 3.</summary>
    public GameBuilder() { }

    /// <summary>
    /// Registers a player for the game. Call this exactly twice before <see cref="Build"/>;
    /// the first call sets player one, the second sets player two.
    /// </summary>
    /// <param name="player">The player to add.</param>
    /// <returns>This builder, for chaining.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="player"/> is <c>null</c>.</exception>
    /// <exception cref="Exception">A player has already been added twice.</exception>
    public GameBuilder WithPlayer(Player player)
    {

        ArgumentNullException.ThrowIfNull(player);

        if (_players[0] == null)
        {
            _players[0] = player;
        }
        else if (_players[1] == null)
        {
            _players[1] = player;
        }
        else
        {
            throw new Exception("Only 2 players are supported");
        }
        return this;
    }

    /// <summary>Sets the width and height of the (initially square) field. Defaults to 3.</summary>
    /// <param name="size">The number of cells per side. Must be at least 2.</param>
    /// <returns>This builder, for chaining.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="size"/> is less than 2.</exception>
    public GameBuilder WithInitialFieldSize(int size)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(size, 2);

        _initialFieldSize = size;
        return this;
    }

    /// <summary>Sets how many marks in a row are needed to win. Defaults to 3.</summary>
    /// <param name="length">The number of connected cells required to win. Must be at least 1.</param>
    /// <returns>This builder, for chaining.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="length"/> is less than 1.</exception>
    public GameBuilder WithWinLength(int length)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(length, 1);

        _winLength = length;
        return this;
    }

    /// <summary>
    /// Sets whether the field stays a fixed size or grows automatically when a move is made
    /// on its edge. Defaults to <c>false</c> (growing field).
    /// </summary>
    /// <param name="fixedFs"><c>true</c> to keep the field size fixed; <c>false</c> to let it expand.</param>
    /// <returns>This builder, for chaining.</returns>
    public GameBuilder WithFixedFieldSize(bool fixedFs = true)
    {
        _fixedFieldSize = fixedFs;
        return this;
    }

    /// <summary>Constructs the <see cref="Game"/> from the settings configured so far.</summary>
    /// <returns>A new <see cref="Game"/> ready to be played.</returns>
    /// <exception cref="Exception">Fewer than two players have been added via <see cref="WithPlayer"/>.</exception>
    public Game Build()
    {
        if (_players[0] == null || _players[1] == null)
            throw new Exception("Not enough players to play");

        return new Game(_players[0],
            _players[1],
            _initialFieldSize,
            _winLength,
            _fixedFieldSize);
    }


}
