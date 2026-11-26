using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCricket.Domain.Rules;

/// <summary>
/// Represents the resolution of a rule in the PocketCricket domain.
/// </summary>
public sealed class RuleResolution
{
    /// <summary>
    /// Initializes a new instance of the RuleResolution class with the specified RuleCandidate.
    /// </summary>  
    /// <param name="rule">rule: The RuleCandidate instance to use for this resolution. Cannot be null.</param>
    public RuleResolution(RuleCandidate rule)
    {
        ArgumentNullException.ThrowIfNull(rule);

        Rule = rule;
    }

    /// <summary>
    /// Gets the rule being resolved.
    /// </summary>
    public RuleCandidate Rule { get; }

    private readonly List<RuleCell> _coveredCells = new();

    /// <summary>
    /// Gets the matrix cells covered during this resolution.
    /// </summary>
    public IReadOnlyList<RuleCell> CoveredCells => _coveredCells;

    /// <summary>
    /// Adds a covered matrix cell to the resolution.
    /// </summary>
    /// <param name="cell">The rule cell to add.</param>
    public void Cover(RuleCell cell)
    {
        ArgumentNullException.ThrowIfNull(cell);

        if (IsCompleted)
        {
            
        }

        if (cell != ExpectedCell)
        {
            
        }

        _coveredCells.Add(cell);
    }

    /// <summary>
    /// Gets whether all cells required by the rule have been covered.
    /// </summary>
    public bool IsCompleted =>
        Rule.Cells.All(cell => _coveredCells.Contains(cell));

    /// <summary>
    /// Gets the next rule cell expected to be covered.
    /// </summary>
    public RuleCell? ExpectedCell
    {
        get
        {
            if (IsCompleted)
            {
                return null;
            }

            return Rule.Cells[CoveredCells.Count];
        }
    }

    /// <summary>
    /// Adds a covered cell to the current rule resolution.
    /// </summary>
    /// <param name="cell">The covered rule cell.</param>
    public void AddCoveredCell(RuleCell cell)
    {
        ArgumentNullException.ThrowIfNull(cell);

        _coveredCells.Add(cell);
    }
}
