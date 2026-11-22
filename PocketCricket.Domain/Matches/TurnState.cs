using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCricket.Domain.Matches;

/// <summary>
/// Represents the lifecycle state of a turn.
/// </summary>
public enum TurnState
{
    /// <summary>
    /// The turn has been created but not started.
    /// </summary>
    NotStarted,

    /// <summary>
    /// The turn is currently active.
    /// </summary>
    InProgress,

    /// <summary>
    /// The turn has completed.
    /// </summary>
    Completed
}
