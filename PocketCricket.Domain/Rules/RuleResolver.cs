using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCricket.Domain.Rules;

/// <summary>
/// Resolves rules for the PocketCricket domain.
/// </summary>
public sealed class RuleResolver
{
    /// <summary>
    /// 
    /// </summary>
    private readonly PendingRuleCollection _pendingRules;

    /// <summary>
    /// Initializes a new instance of the <see cref="RuleResolver"/> class.
    /// </summary>
    /// <param name="pendingRules">
    /// The collection of pending rules.
    /// </param>
    public RuleResolver(
        PendingRuleCollection pendingRules)
    {
        ArgumentNullException.ThrowIfNull(pendingRules);

        _pendingRules = pendingRules;
    }

    /// <summary>
    /// Gets the rule currently being resolved.
    /// </summary>
    public RuleResolution? CurrentResolution { get; private set; }

    /// <summary>
    /// Begins resolving the next pending rule.
    /// </summary>
    /// The collection containing pending rules.
    public void BeginNextResolution()
    {
        if (CurrentResolution is not null)
        {
            throw new InvalidOperationException(
                "A rule resolution is already in progress.");
        }

        CurrentResolution = new RuleResolution(
            _pendingRules.Peek());
    }

    /// <summary>
    /// Covers a rule cell.
    /// </summary>
    /// <param name="cell">The rule cell to cover.</param>
    public RuleCoverResult Cover(RuleCell cell)
    {
        ArgumentNullException.ThrowIfNull(cell);

        if (CurrentResolution is null)
        {
            throw new InvalidOperationException(
                "There is no active rule resolution.");
        }

        if (CurrentResolution.IsCompleted)
        {
            throw new InvalidOperationException(
                "The current rule resolution is already completed.");
        }

        if (!CurrentResolution.Rule.Cells.Contains(cell))
        {
            return RuleCoverResult.Miss;
        }

        CurrentResolution.AddCoveredCell(cell);
        
        if (CurrentResolution.IsCompleted)
        {
            _pendingRules.Remove(CurrentResolution.Rule);

            return RuleCoverResult.Completed;
        }

        return RuleCoverResult.Success;
    }

    /// <summary>
    /// Ends the current completed rule resolution.
    /// </summary>
    public void EndCurrentResolution()
    {
        if (CurrentResolution is null)
        {
            throw new InvalidOperationException(
                "There is no active rule resolution.");
        }

        if (!CurrentResolution.IsCompleted)
        {
            throw new InvalidOperationException(
                "The current rule resolution has not been completed.");
        }

        CurrentResolution = null;
    }
}
