using System;
using System.Collections.Generic;
using System.Text;
using PocketCricket.Domain.Matrices;
using PocketCricket.Domain.Rules;

namespace PocketCricket.Domain.Interfaces;

/// <summary>
/// 
/// </summary>
public interface IRuleEngine
{
    /// <summary>
    /// Finds rule candidates based on the provided matrix.
    /// </summary>
    /// <param name="matrix">The matrix to analyze for rule candidates.</param>
    /// <returns>An immutable list of rule candidate objects.</returns>
    public IReadOnlyList<RuleCandidate> FindRules(Matrix matrix);
}
