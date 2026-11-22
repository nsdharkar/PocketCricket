using FluentAssertions;
using PocketCricket.Domain.Decks;
using PocketCricket.Domain.Implementations;
using PocketCricket.Domain.Matches;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCricket.Domain.Tests.Matches;

/// <summary>
/// 
/// </summary>
public sealed class MatchTests
{
    /// <summary>
    /// Tests the behavior of the <see cref="Match"/> constructor
    /// when valid match settings are provided.
    /// </summary>
    [Fact]
    public void Constructor_WhenSettingsProvided_ShouldCreateMatch()
    {
        // Arrange
        Deck deck = new(new RandomProvider());

        MatchSettings settings = new(MatchFormat.T20);

        // Act
        Match match = new(settings, deck);

        // Assert
        match.Settings.Should().Be(settings);
        match.Deck.Should().BeSameAs(deck);
    }

    /// <summary>
    /// Tests the behavior of the <see cref="Match"/> constructor
    /// when a valid match is created.
    /// </summary>
    [Fact]
    public void Constructor_WhenMatchIsCreated_ShouldBeWaitingForToss()
    {
        // Arrange
        MatchSettings settings = new(MatchFormat.T20);

        // Act
        Deck deck = new(new RandomProvider());

        Match match = new(
            new MatchSettings(MatchFormat.T20),
            deck);

        // Assert
        match.State.Should()
             .Be(MatchState.WaitingForToss);
    }

    /// <summary>
    /// Tests the behavior of the <see cref="Match"/> when the toss decision is applied.
    /// </summary>
    [Fact]
    public void ApplyTossDecision_WhenDecisionIsBat_ShouldStartMatch()
    {
        // Arrange
        Deck deck = new(new RandomProvider());

        Match match = new(
            new MatchSettings(MatchFormat.T20),
            deck);

        TossResult tossResult = new(
        42,
        35,
        PlayerSide.PlayerOne,
        TossStatus.Completed);

        match.ApplyTossResult(tossResult);

        // Act
        match.ApplyTossDecision(PlayerSide.PlayerOne, TossDecision.Bat);

        // Assert
        match.State.Should().Be(MatchState.InProgress);
    }

    /// <summary>
    /// Tests the behavior of the <see cref="Match"/> when the toss result is applied.
    /// </summary>
    [Fact]
    public void ApplyTossResult_WhenTossIsCompleted_ShouldWaitForTossDecision()
    {
        // Arrange
        Deck deck = new(new RandomProvider());

        Match match = new(
            new MatchSettings(MatchFormat.T20),
            deck);

        TossResult tossResult = new(
            playerOneCardsPlaced: 42,
            playerTwoCardsPlaced: 35,
            winner: PlayerSide.PlayerOne,
            status: TossStatus.Completed);

        // Act
        match.ApplyTossResult(tossResult);

        // Assert
        match.State.Should().Be(MatchState.WaitingForTossDecision);
    }

    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public void ApplyTossDecision_WhenNonWinnerChooses_ShouldThrowInvalidOperationException()
    {
        // Arrange
        Deck deck = new(new RandomProvider());

        Match match = new(
            new MatchSettings(MatchFormat.T20),
            deck);

        TossResult tossResult = new(
            42,
            35,
            PlayerSide.PlayerOne,
            TossStatus.Completed);

        match.ApplyTossResult(tossResult);

        // Act
        Action action = () => match.ApplyTossDecision(
            PlayerSide.PlayerTwo,
            TossDecision.Bat);

        // Assert
        action.Should()
            .Throw<InvalidOperationException>();
    }

    /// <summary>
    /// Tests the behavior of the <see cref="Match"/> when the toss result is applied multiple times.
    /// </summary>
    [Fact]
    public void ApplyTossResult_WhenAlreadyApplied_ShouldThrowInvalidOperationException()
    {
        // Arrange
        Deck deck = new(new RandomProvider());

        Match match = new(
            new MatchSettings(MatchFormat.T20),
            deck);

        TossResult tossResult = new(
            42,
            35,
            PlayerSide.PlayerOne,
            TossStatus.Completed);

        match.ApplyTossResult(tossResult);

        // Act
        Action action = () => match.ApplyTossResult(tossResult);

        // Assert
        action.Should()
            .Throw<InvalidOperationException>();
    }

    /// <summary>
    /// Tests the behavior of the <see cref="Match"/> when the toss decision is applied without a prior toss result.
    /// </summary>
    [Fact]
    public void ApplyTossDecision_WhenTossHasNotBeenApplied_ShouldThrowInvalidOperationException()
    {
        // Arrange
        Deck deck = new(new RandomProvider());

        Match match = new(
            new MatchSettings(MatchFormat.T20),
            deck);

        // Act
        Action action = () => match.ApplyTossDecision(
            PlayerSide.PlayerOne,
            TossDecision.Bat);

        // Assert
        action.Should()
            .Throw<InvalidOperationException>();
    }

    /// <summary>
    /// Tests the behavior of the <see cref="Match"/> when the toss decision is applied after the decision has already been made.
    /// </summary>
    [Fact]
    public void ApplyTossDecision_WhenDecisionAlreadyMade_ShouldThrowInvalidOperationException()
    {
        // Arrange
        Deck deck = new(new RandomProvider());

        Match match = new(
            new MatchSettings(MatchFormat.T20),
            deck);

        TossResult tossResult = new(
            42,
            35,
            PlayerSide.PlayerOne,
            TossStatus.Completed);

        match.ApplyTossResult(tossResult);

        match.ApplyTossDecision(
            PlayerSide.PlayerOne,
            TossDecision.Bat);

        // Act
        Action action = () => match.ApplyTossDecision(
            PlayerSide.PlayerOne,
            TossDecision.Bowl);

        // Assert
        action.Should()
            .Throw<InvalidOperationException>();
    }

    /// <summary>
    /// Tests the behavior of the <see cref="Match"/> when the toss winner chooses to bat.
    /// </summary>
    [Fact]
    public void ApplyTossDecision_WhenWinnerChoosesBat_ShouldCreateCurrentInnings()
    {
        // Arrange
        Deck deck = new(new RandomProvider());

        Match match = new(
            new MatchSettings(MatchFormat.T20),
            deck);

        TossResult tossResult = new(
            42,
            35,
            PlayerSide.PlayerOne,
            TossStatus.Completed);

        match.ApplyTossResult(tossResult);

        // Act
        match.ApplyTossDecision(
            PlayerSide.PlayerOne,
            TossDecision.Bat);

        // Assert
        match.CurrentInnings.Should().NotBeNull();
    }

    /// <summary>
    /// Tests the behavior of the <see cref="Match"/> when the toss winner chooses to bat.
    /// </summary>
    [Fact]
    public void ApplyTossDecision_WhenWinnerChoosesBat_ShouldSetWinnerAsBattingSide()
    {
        // Arrange
        Deck deck = new(new RandomProvider());

        Match match = new(
            new MatchSettings(MatchFormat.T20),
            deck);

        TossResult tossResult = new(
            42,
            35,
            PlayerSide.PlayerOne,
            TossStatus.Completed);

        match.ApplyTossResult(tossResult);

        // Act
        match.ApplyTossDecision(
            PlayerSide.PlayerOne,
            TossDecision.Bat);

        // Assert
        match.CurrentInnings!
            .BattingSide
            .Should()
            .Be(PlayerSide.PlayerOne);
    }

    /// <summary>
    /// Tests the behavior of the <see cref="Match"/> when the toss winner chooses to bat.
    /// </summary>
    [Fact]
    public void ApplyTossDecision_WhenWinnerChoosesBat_ShouldSetOpponentAsBowlingSide()
    {
        // Arrange
        Deck deck = new(new RandomProvider());

        Match match = new(
            new MatchSettings(MatchFormat.T20),
            deck);

        TossResult tossResult = new(
            42,
            35,
            PlayerSide.PlayerOne,
            TossStatus.Completed);

        match.ApplyTossResult(tossResult);

        // Act
        match.ApplyTossDecision(
            PlayerSide.PlayerOne,
            TossDecision.Bat);

        // Assert
        match.CurrentInnings!
            .BowlingSide
            .Should()
            .Be(PlayerSide.PlayerTwo);
    }

    /// <summary>
    /// Tests the behavior of the <see cref="Match"/> when the toss winner chooses to bat.
    /// </summary>
    [Fact]
    public void ApplyTossDecision_WhenWinnerChoosesBat_ShouldCreateCurrentSegment()
    {
        // Arrange
        Deck deck = new(new RandomProvider());

        Match match = new(
            new MatchSettings(MatchFormat.T20),
            deck);

        TossResult tossResult = new(
            42,
            35,
            PlayerSide.PlayerOne,
            TossStatus.Completed);

        match.ApplyTossResult(tossResult);

        // Act
        match.ApplyTossDecision(
            PlayerSide.PlayerOne,
            TossDecision.Bat);

        // Assert
        match.CurrentInnings!
            .CurrentTurn
            .Should()
            .NotBeNull();
    }

    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public void ApplyTossDecision_WhenWinnerChoosesBat_ShouldCreateBowlingTurn()
    {
        // Arrange
        Deck deck = new(new RandomProvider());

        Match match = new(
            new MatchSettings(MatchFormat.T20),
            deck);

        TossResult tossResult = new(
            42,
            35,
            PlayerSide.PlayerOne,
            TossStatus.Completed);

        match.ApplyTossResult(tossResult);

        // Act
        match.ApplyTossDecision(
            PlayerSide.PlayerOne,
            TossDecision.Bat);

        // Assert
        match.CurrentInnings!
            .CurrentTurn
            .Type
            .Should()
            .Be(TurnType.Bowling);
    }

    /// <summary>
    /// Tests the behavior of the <see cref="Match"/> constructor when a valid deck is provided.
    /// </summary>
    [Fact]
    public void Constructor_WhenValidDeckIsProvided_ShouldStoreDeck()
    {
        // Arrange
        Deck deck = new(new RandomProvider());

        // Act
        Match match = new(new MatchSettings(MatchFormat.T20), deck);

        // Assert
        match.Deck.Should().BeSameAs(deck);
    }

    /// <summary>
    /// Tests that the first turn after toss decision starts as a bowling turn.
    /// </summary>
    [Fact]
    public void ApplyTossDecision_WhenMatchStarts_ShouldCreateBowlingTurn()
    {
        // Arrange
        Deck deck = new(new RandomProvider());

        Match match = new(new MatchSettings(MatchFormat.T20), deck);

        TossResult tossResult = new(42, 35, PlayerSide.PlayerOne, TossStatus.Completed);

        match.ApplyTossResult(tossResult);

        // Act
        match.ApplyTossDecision(PlayerSide.PlayerOne, TossDecision.Bat);

        // Assert
        match.CurrentInnings!.CurrentTurn.Type.Should().Be(TurnType.Bowling);
    }

    /// <summary>
    /// Tests that the first turn starts immediately after toss decision.
    /// </summary>
    [Fact]
    public void ApplyTossDecision_WhenMatchStarts_ShouldCreateActiveTurn()
    {
        // Arrange
        Deck deck = new(new RandomProvider());

        Match match = new(
            new MatchSettings(MatchFormat.T20),
            deck);

        TossResult tossResult = new(
            42,
            35,
            PlayerSide.PlayerOne,
            TossStatus.Completed);

        match.ApplyTossResult(tossResult);

        // Act
        match.ApplyTossDecision(
            PlayerSide.PlayerOne,
            TossDecision.Bat);

        // Assert
        match.CurrentInnings!
            .CurrentTurn
            .State
            .Should()
            .Be(TurnState.InProgress);
    }
}
