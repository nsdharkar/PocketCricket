using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCricket.Domain.Matches;

/// <summary>
/// 
/// </summary>
public enum MatchState
{
    /// <summary>
    /// WaitingForToss
    /// </summary>
    WaitingForToss,
    /// <summary>
    /// WaitingForTossDecision
    /// </summary>
    WaitingForTossDecision,
    /// <summary>
    /// InProgress
    /// </summary>
    InProgress,
    /// <summary>
    /// Completed
    /// </summary>
    Completed
}
