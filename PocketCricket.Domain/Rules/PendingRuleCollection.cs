using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCricket.Domain.Rules;

/// <summary>
/// 
/// </summary>
public sealed class PendingRuleCollection
{
    private readonly List<RuleCandidate> _rules = new();

    /// <summary>
    /// 
    /// </summary>
    public int Count => _rules.Count;

    /// <summary>
    /// Adds a rule to the pending collection.
    /// </summary>
    /// <param name="candidate">The rule to add.</param>
    public void Add(RuleCandidate candidate)
    {
        ArgumentNullException.ThrowIfNull(candidate);

        _rules.Add(candidate);
    }

    /// <summary>
    /// Returns the next pending rule.
    /// </summary>
    /// <returns>The first pending rule.</returns>
    public RuleCandidate Peek()
    {
        if (_rules.Count == 0)
        {
            throw new InvalidOperationException(
                "There are no pending rules.");
        }

        return _rules[0];
    }

    /// <summary>
    /// Removes a resolved rule from the collection.
    /// </summary>
    /// <param name="candidate">The rule to remove.</param>
    public void Remove(RuleCandidate candidate)
    {
        ArgumentNullException.ThrowIfNull(candidate);

        _rules.Remove(candidate);
    }

    /// <summary>
    /// Clears all pending rules from the collection.
    /// </summary>
    public void Clear()
    {
        _rules.Clear();
    }
}
