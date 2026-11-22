using PocketCricket.Domain.Matches;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;

namespace PocketCricket.Domain.Tests.Matches;

/// <summary>
/// 
/// </summary>
public sealed class TossTests
{
    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public void Resolve_WhenPlayerOnePlacesMoreCards_ShouldDeclarePlayerOneWinner()
    {
        // Arrange
        TurnResult playerOneResult =
            new(45);

        TurnResult playerTwoResult =
            new(32);

        Toss toss = new();

        // Act
        TossResult result = toss.Resolve(
            playerOneResult,
            playerTwoResult);

        // Assert
        result.Winner.Should()
            .Be(PlayerSide.PlayerOne);

        result.Status.Should()
            .Be(TossStatus.Completed);
    }

    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public void Resolve_WhenPlayerTwoPlacesMoreCards_ShouldDeclarePlayerTwoWinner()
    {
        // Arrange
        TurnResult playerOneResult =
            new(25);

        TurnResult playerTwoResult =
            new(41);

        Toss toss = new();

        // Act
        TossResult result = toss.Resolve(
            playerOneResult,
            playerTwoResult);

        // Assert
        result.Winner.Should()
            .Be(PlayerSide.PlayerTwo);

        result.Status.Should()
            .Be(TossStatus.Completed);
    }

    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public void Resolve_WhenBothPlayersPlaceSameNumberOfCards_ShouldRequireReplay()
    {
        // Arrange
        TurnResult playerOneResult =
            new(35);

        TurnResult playerTwoResult =
            new(35);

        Toss toss = new();

        // Act
        TossResult result = toss.Resolve(
            playerOneResult,
            playerTwoResult);

        // Assert
        result.Status.Should()
            .Be(TossStatus.ReplayRequired);

        result.Winner.Should()
            .BeNull();
    }
}
