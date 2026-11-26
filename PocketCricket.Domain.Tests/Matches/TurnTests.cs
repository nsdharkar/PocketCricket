using FluentAssertions;
using PocketCricket.Domain.Cards;
using PocketCricket.Domain.Interfaces;
using PocketCricket.Domain.Matches;
using PocketCricket.Domain.Rules;
using PocketCricket.Domain.Tests.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCricket.Domain.Tests.Matches;

/// <summary>
/// 
/// </summary>
public sealed class TurnTests
{
    /// <summary>
    /// Tests that a new turn creates an empty matrix.
    /// </summary>
    [Fact]
    public void Constructor_WhenTurnCreated_ShouldCreateEmptyMatrix()
    {
        // Arrange
        Turn turn = new(TurnType.Bowling, new FakeRuleEngine());

        // Assert
        turn.Matrix.Should().NotBeNull();

        turn.Matrix.Cells
            .Count(cell => !cell.IsEmpty)
            .Should()
            .Be(0);
    }

    /// <summary>
    /// Tests that a drawn card can be placed into the turn matrix.
    /// </summary>
    [Fact]
    public void PlaceDrawnCard_WhenCardIsAvailable_ShouldAddCardToMatrix()
    {
        // Arrange
        Turn turn = new(TurnType.Bowling, new FakeRuleEngine());

        Card card = new(
            CardSuit.Spades,
            CardRank.Five);

        // Act
        turn.SetDrawnCard(card);
        turn.PlaceDrawnCard();

        // Assert
        turn.Matrix.Cells
            .Count(cell => !cell.IsEmpty)
            .Should()
            .Be(1);

        turn.CurrentCard.Should()
            .BeNull();
    }

    /// <summary>
    /// Tests that a turn cannot place a card when no card has been drawn.
    /// </summary>
    [Fact]
    public void PlaceDrawnCard_WhenNoCardIsDrawn_ShouldThrowInvalidOperationException()
    {
        // Arrange
        Turn turn = new(TurnType.Bowling, new FakeRuleEngine());

        // Act
        Action action = () => turn.PlaceDrawnCard();

        // Assert
        action.Should()
            .Throw<InvalidOperationException>()
            .WithMessage("No drawn card is available to place.");
    }

    /// <summary>
    /// Tests that a turn starts with no pending rules.
    /// </summary>
    [Fact]
    public void Constructor_ShouldCreateEmptyPendingRuleCollection()
    {
        // Arrange
        Turn turn = CreateTurn();

        // Assert
        turn.PendingRules.Count.Should().Be(0);
    }

    private static Turn CreateTurn()
    {
        return new Turn(
            TurnType.Bowling,
            new FakeRuleEngine());
    }

    /// <summary>
    /// Tests that placing a card evaluates the matrix for rules.
    /// </summary>
    [Fact]
    public void PlaceDrawnCard_ShouldEvaluateRules()
    {
        // Arrange
        FakeRuleEngine ruleEngine = new();

        Turn turn = new(
            TurnType.Bowling,
            ruleEngine);

        Card card = new(
            CardSuit.Spades,
            CardRank.Five);

        turn.SetDrawnCard(card);

        // Act
        turn.PlaceDrawnCard();

        // Assert
        ruleEngine.FindRulesCalled.Should().BeTrue();
    }

    /// <summary>
    /// Tests that detected rules are stored as pending rules.
    /// </summary>
    [Fact]
    public void PlaceDrawnCard_WhenRulesAreDetected_ShouldPopulatePendingRules()
    {
        // Arrange
        RuleCandidate candidate = new(
            RuleType.Kqj,
            []);

        FakeRuleEngine ruleEngine = new()
        {
            RulesToReturn = [candidate]
        };

        Turn turn = new(
            TurnType.Bowling,
            ruleEngine);

        Card card = new(
            CardSuit.Spades,
            CardRank.Five);

        turn.SetDrawnCard(card);

        // Act
        turn.PlaceDrawnCard();

        // Assert
        turn.PendingRules.Count.Should().Be(1);

        turn.PendingRules.Peek().Should().Be(candidate);
    }
}
