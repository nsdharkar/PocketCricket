using PocketCricket.Domain.Interfaces;
using PocketCricket.Domain.Matrices;
using PocketCricket.Domain.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCricket.Domain.Tests.Rules;

/// <summary>
/// A fake implementation of the IRuleEngine interface for testing purposes.
/// </summary>
internal sealed class FakeRuleEngine : IRuleEngine
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="matrix"></param>
    /// <returns></returns>
    public IReadOnlyList<RuleCandidate> FindRules(Matrix matrix)
    {
        ArgumentNullException.ThrowIfNull(matrix);

        FindRulesCalled = true;

        return RulesToReturn;
    }

    /// <summary>
    /// 
    /// </summary>
    public bool FindRulesCalled { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public IReadOnlyList<RuleCandidate> RulesToReturn { get; set; } = [];
}
