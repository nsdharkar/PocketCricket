using PocketCricket.Domain.Matches;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;

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
        MatchSettings settings = new(MatchFormat.T20);

        // Act
        Match match = new(settings);

        // Assert
        match.Settings.Should().Be(settings);
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
        Match match = new(settings);

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
        Match match = new(new MatchSettings(MatchFormat.T20));

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
        Match match = new(new MatchSettings(MatchFormat.T20));

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
        Match match = new(new MatchSettings(MatchFormat.T20));

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
        Match match = new(new MatchSettings(MatchFormat.T20));

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
        Match match = new(new MatchSettings(MatchFormat.T20));

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
        Match match = new(new MatchSettings(MatchFormat.T20));

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
}
