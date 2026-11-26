using PocketCricket.Domain.Rules;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;

namespace PocketCricket.Domain.Tests.Rules;

/// <summary>
/// 
/// </summary>
public sealed class PendingRuleCollectionTests
{
    /// <summary>
    /// Tests that a new pending rule collection is empty.
    /// </summary>
    [Fact]
    public void Constructor_ShouldCreateEmptyCollection()
    {
        // Arrange
        PendingRuleCollection collection = new();

        // Assert
        collection.Count.Should().Be(0);
    }

    /// <summary>
    /// Tests that a rule can be added to the pending rule collection.
    /// </summary>
    [Fact]
    public void Add_WhenRuleIsProvided_ShouldIncreaseCount()
    {
        // Arrange
        PendingRuleCollection collection = new();

        RuleCandidate candidate = new(
            RuleType.Kqj,
            []);

        // Act
        collection.Add(candidate);

        // Assert
        collection.Count.Should().Be(1);
    }

    /// <summary>
    /// Tests that the first added rule is returned.
    /// </summary>
    [Fact]
    public void Peek_WhenCollectionContainsRule_ShouldReturnFirstRule()
    {
        // Arrange
        PendingRuleCollection collection = new();

        RuleCandidate candidate = new(
            RuleType.Kqj,
            []);

        collection.Add(candidate);

        // Act
        RuleCandidate result = collection.Peek();

        // Assert
        result.Should().Be(candidate);
    }

    /// <summary>
    /// Tests that Peek throws when the collection is empty.
    /// </summary>
    [Fact]
    public void Peek_WhenCollectionIsEmpty_ShouldThrowInvalidOperationException()
    {
        // Arrange
        PendingRuleCollection collection = new();

        // Act
        Action action = () => collection.Peek();

        // Assert
        action.Should()
            .Throw<InvalidOperationException>()
            .WithMessage("There are no pending rules.");
    }

    /// <summary>
    /// Tests that removing a pending rule decreases the collection count.
    /// </summary>
    [Fact]
    public void Remove_WhenRuleExists_ShouldDecreaseCount()
    {
        // Arrange
        PendingRuleCollection collection = new();

        RuleCandidate candidate = new(
            RuleType.Kqj,
            []);

        collection.Add(candidate);

        // Act
        collection.Remove(candidate);

        // Assert
        collection.Count.Should().Be(0);
    }

    /// <summary>
    /// Tests that removing a rule not in the collection does not throw.
    /// </summary>
    [Fact]
    public void Remove_WhenRuleDoesNotExist_ShouldNotThrow()
    {
        // Arrange
        PendingRuleCollection collection = new();

        RuleCandidate candidate = new(
            RuleType.Kqj,
            []);

        // Act
        Action action = () => collection.Remove(candidate);

        // Assert
        action.Should().NotThrow();

        collection.Count.Should().Be(0);
    }

    /// <summary>
    /// Tests that clearing the collection removes all rules.
    /// </summary>
    [Fact]
    public void Clear_WhenCollectionContainsRules_ShouldRemoveAllRules()
    {
        // Arrange
        PendingRuleCollection collection = new();

        collection.Add(new RuleCandidate(RuleType.Kqj, []));
        collection.Add(new RuleCandidate(RuleType.Trio, []));

        // Act
        collection.Clear();

        // Assert
        collection.Count.Should().Be(0);
    }
}
