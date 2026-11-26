using FluentAssertions;
using PocketCricket.Domain.Cards;
using PocketCricket.Domain.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCricket.Domain.Tests.Rules;

/// <summary>
/// 
/// </summary>
public sealed class RuleResolverTests
{
    /// <summary>
    /// Tests that a new rule resolver starts without an active rule resolution.
    /// </summary>
    [Fact]
    public void Constructor_ShouldNotHaveCurrentResolution()
    {
        // Arrange
        PendingRuleCollection pendingRules = new();

        RuleResolver resolver = new(pendingRules);

        // Assert
        resolver.CurrentResolution.Should().BeNull();
    }


    /// <summary>
    /// Tests that the next pending rule becomes the current resolution.
    /// </summary>
    [Fact]
    public void BeginNextResolution_WhenPendingRuleExists_ShouldCreateCurrentResolution()
    {
        // Arrange
        PendingRuleCollection pendingRules = new();

        RuleCandidate candidate = new(
            RuleType.Kqj,
            []);

        pendingRules.Add(candidate);

        RuleResolver resolver = new(pendingRules);

        // Act
        resolver.BeginNextResolution();

        // Assert
        resolver.CurrentResolution.Should().NotBeNull();

        resolver.CurrentResolution!.Rule.Should().Be(candidate);
    }


    /// <summary>
    /// Tests that a new resolution cannot begin while another resolution is active.
    /// </summary>
    [Fact]
    public void BeginNextResolution_WhenResolutionIsAlreadyActive_ShouldThrowInvalidOperationException()
    {
        // Arrange
        PendingRuleCollection pendingRules = new();

        RuleCandidate firstRule = new(
            RuleType.Kqj,
            []);

        RuleCandidate secondRule = new(
            RuleType.Trio,
            []);

        pendingRules.Add(firstRule);
        pendingRules.Add(secondRule);

        RuleResolver resolver = new(pendingRules);

        resolver.BeginNextResolution();

        // Act
        Action action = () => resolver.BeginNextResolution();

        // Assert
        action.Should()
            .Throw<InvalidOperationException>()
            .WithMessage("A rule resolution is already in progress.");
    }


    /// <summary>
    /// Tests that covering a valid rule cell stores the covered cell.
    /// </summary>
    [Fact]
    public void Cover_WhenRuleCellBelongsToCurrentResolution_ShouldAddCoveredCell()
    {
        // Arrange
        Card king = new(CardSuit.Spades, CardRank.King);

        RuleCell cell = new(
            2,
            king);

        RuleCandidate candidate = new(
            RuleType.Kqj,
            [cell]);

        PendingRuleCollection pendingRules = new();

        pendingRules.Add(candidate);

        RuleResolver resolver = new(pendingRules);

        resolver.BeginNextResolution();

        // Act
        resolver.Cover(cell);

        // Assert
        resolver.CurrentResolution!
            .CoveredCells
            .Should()
            .ContainSingle()
            .Which.Should()
            .Be(cell);
    }


    /// <summary>
    /// Tests that covering a cell outside the current rule results in a miss.
    /// </summary>
    [Fact]
    public void Cover_WhenCellDoesNotBelongToCurrentResolution_ShouldReturnMiss()
    {
        // Arrange
        Card king = new(CardSuit.Spades, CardRank.King);
        Card queen = new(CardSuit.Hearts, CardRank.Queen);
        Card jack = new(CardSuit.Diamonds, CardRank.Jack);

        RuleCell kingCell = new(2, king);
        RuleCell queenCell = new(5, queen);
        RuleCell jackCell = new(6, jack);

        RuleCandidate candidate = new(
            RuleType.Kqj,
            [
                kingCell,
            queenCell,
            jackCell
            ]);

        PendingRuleCollection pendingRules = new();
        pendingRules.Add(candidate);

        RuleResolver resolver = new(pendingRules);

        resolver.BeginNextResolution();

        RuleCell invalidCell = new(
            8,
            new Card(CardSuit.Clubs, CardRank.Seven));

        // Act
        RuleCoverResult result = resolver.Cover(invalidCell);

        // Assert
        result.Should().Be(RuleCoverResult.Miss);

        resolver.CurrentResolution!
            .CoveredCells
            .Should()
            .BeEmpty();
    }

    /// <summary>
    /// Tests that covering the final required cell completes the current resolution.
    /// </summary>
    [Fact]
    public void Cover_WhenFinalRequiredCellIsCovered_ShouldReturnCompleted()
    {
        // Arrange
        Card king = new(CardSuit.Spades, CardRank.King);
        Card queen = new(CardSuit.Hearts, CardRank.Queen);
        Card jack = new(CardSuit.Diamonds, CardRank.Jack);

        RuleCell kingCell = new(2, king);
        RuleCell queenCell = new(5, queen);
        RuleCell jackCell = new(6, jack);

        RuleCandidate candidate = new(
            RuleType.Kqj,
            [
                kingCell,
            queenCell,
            jackCell
            ]);

        PendingRuleCollection pendingRules = new();
        pendingRules.Add(candidate);

        RuleResolver resolver = new(pendingRules);

        resolver.BeginNextResolution();

        resolver.Cover(kingCell);
        resolver.Cover(queenCell);

        // Act
        RuleCoverResult result = resolver.Cover(jackCell);

        // Assert
        result.Should().Be(RuleCoverResult.Completed);

        resolver.CurrentResolution!
            .IsCompleted
            .Should()
            .BeTrue();
    }


    /// <summary>
    /// Tests that covering is not allowed after the current resolution is completed.
    /// </summary>
    [Fact]
    public void Cover_WhenCurrentResolutionIsAlreadyCompleted_ShouldThrowInvalidOperationException()
    {
        // Arrange
        Card king = new(CardSuit.Spades, CardRank.King);

        RuleCell cell = new(2, king);

        RuleCandidate candidate = new(
            RuleType.Kqj,
            [cell]);

        PendingRuleCollection pendingRules = new();
        pendingRules.Add(candidate);

        RuleResolver resolver = new(pendingRules);

        resolver.BeginNextResolution();

        resolver.Cover(cell);

        // Act
        Action action = () => resolver.Cover(cell);

        // Assert
        action.Should()
            .Throw<InvalidOperationException>()
            .WithMessage("The current rule resolution is already completed.");
    }

    /// <summary>
    /// Tests that a completed rule is removed from the pending collection.
    /// </summary>
    [Fact]
    public void Cover_WhenCurrentRuleIsCompleted_ShouldRemoveRuleFromPendingCollection()
    {
        // Arrange
        Card king = new(CardSuit.Spades, CardRank.King);

        RuleCell cell = new(
            2,
            king);

        RuleCandidate candidate = new(
            RuleType.Kqj,
            [cell]);

        PendingRuleCollection pendingRules = new();

        pendingRules.Add(candidate);

        RuleResolver resolver =
            new(pendingRules);

        resolver.BeginNextResolution();

        // Act
        RuleCoverResult result =
            resolver.Cover(cell);

        // Assert
        result.Should().Be(
            RuleCoverResult.Completed);

        pendingRules.Count.Should().Be(0);
    }

    /// <summary>
    /// Tests that ending the current resolution clears the active resolution.
    /// </summary>
    [Fact]
    public void EndCurrentResolution_WhenResolutionIsCompleted_ShouldClearCurrentResolution()
    {
        // Arrange
        Card king = new(CardSuit.Spades, CardRank.King);

        RuleCell cell = new(2, king);

        RuleCandidate candidate = new(
            RuleType.Kqj,
            [cell]);

        PendingRuleCollection pendingRules = new();

        pendingRules.Add(candidate);

        RuleResolver resolver =
            new(pendingRules);

        resolver.BeginNextResolution();

        resolver.Cover(cell);

        // Act
        resolver.EndCurrentResolution();

        // Assert
        resolver.CurrentResolution
            .Should()
            .BeNull();
    }
}
