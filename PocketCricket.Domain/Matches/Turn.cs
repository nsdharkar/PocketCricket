using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCricket.Domain.Matches;

/// <summary>
/// Represents a segment of an innings in a Pocket Cricket match.
/// </summary>
public sealed class Turn
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    public Turn(TurnType type)
    {
        Type = type;
        State = TurnState.InProgress;
    }

    /// <summary>
    /// 
    /// </summary>
    public TurnType Type { get; }

    /// <summary>
    /// Gets the lifecycle state of the turn.
    /// </summary>
    public TurnState State { get; private set; }
}
