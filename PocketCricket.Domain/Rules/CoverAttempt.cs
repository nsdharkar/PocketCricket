using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCricket.Domain.Rules;

/// <summary>
/// Represents a player's attempt to cover cards.
/// </summary>
/// <param name="CellIndexes">
/// The matrix cell indexes selected by the player.
/// </param>
public sealed record CoverAttempt(
    IReadOnlyList<int> CellIndexes);
