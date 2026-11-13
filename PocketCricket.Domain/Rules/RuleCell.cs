using PocketCricket.Domain.Cards;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCricket.Domain.Rules;

/// <summary>
/// Represents a cell in the rule evaluation grid.
/// </summary>
/// <param name="CellIndex"></param>
/// <param name="Card"></param>
public sealed record RuleCell(int CellIndex, Card Card);
