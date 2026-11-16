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
        SegmentResult playerOneResult =
            new(SegmentType.Toss, 45);

        SegmentResult playerTwoResult =
            new(SegmentType.Toss, 32);

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
        SegmentResult playerOneResult =
            new(SegmentType.Toss, 25);

        SegmentResult playerTwoResult =
            new(SegmentType.Toss, 41);

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
        SegmentResult playerOneResult =
            new(SegmentType.Toss, 35);

        SegmentResult playerTwoResult =
            new(SegmentType.Toss, 35);

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
