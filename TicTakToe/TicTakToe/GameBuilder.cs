using System;
using System.Collections.Generic;
using System.Text;

namespace TicTakToe;

public class GameBuilder
{
    // (Player playerA, Player playerB, int initialFieldSize, int winLength, bool fixedFieldSize)
    private Player[] _players = new Player[2];
    private int _initialFieldSize = 3;
    private int _winLength = 3;
    private bool _fixedFieldSize = false;

    public GameBuilder() { }
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
    public GameBuilder WithInitialFieldSize(int size)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(size, 2);

        _initialFieldSize = size;
        return this;
    }
    public GameBuilder WithWinLength(int length)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(length, 1);

        _winLength = length;
        return this;
    }
    public GameBuilder WithFixedFieldSize(bool fixedFs = true)
    {
        _fixedFieldSize = fixedFs;
        return this;
    }

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
