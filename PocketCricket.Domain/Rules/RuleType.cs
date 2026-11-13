using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCricket.Domain.Rules;

/// <summary>
/// Represents the type of a rule in the game.
/// </summary>
public enum RuleType
{
    /// <summary>
    /// Represents the KQJ rule.
    /// </summary>
    Kqj = 1,
    /// <summary>
    /// Represents the Trio rule.
    /// </summary>
    Trio = 2,
    /// <summary>
    /// Represents the Eleven Pair rule.
    /// </summary>
    ElevenPair = 3
}
