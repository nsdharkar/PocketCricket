using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCricket.Domain.Rules;

/// <summary>
/// Represents a candidate rule in the game.
/// </summary>
/// <param name="RuleType"></param>
/// <param name="Cells"></param>
public sealed record RuleCandidate(RuleType RuleType, IReadOnlyList<RuleCell> Cells);

