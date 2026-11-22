using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using PocketCricket.Domain.Matches;

namespace PocketCricket.Domain.Tests.Matches;

/// <summary>
/// 
/// </summary>
public sealed class SegmentResultTests
{
    /// <summary>
    /// Tests the behavior of the <see cref="TurnResult"/> constructor
    /// when valid values are supplied.
    /// </summary>
    [Fact]
    public void Constructor_WhenValidValuesProvided_ShouldCreateSegmentResult()
    {
        // Arrange & Act
        TurnResult playerOneResult = new(43);
        TurnResult playerTwoResult = new(32);

        // Assert
        playerOneResult.CardsPlaced.Should().Be(43);
        playerTwoResult.CardsPlaced.Should().Be(32);
    }


    /// <summary>
    /// Tests the behavior of the <see cref="TurnResult"/> constructor
    /// when fewer than nine cards are supplied.
    /// </summary>
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(8)]
    public void Constructor_WhenCardsPlacedIsLessThanMinimum_ShouldThrowArgumentOutOfRangeException(int cardsPlaced)
    {
        // Arrange & Act
        Action action = () => new TurnResult(cardsPlaced);

        // Assert
        action.Should()
            .Throw<ArgumentOutOfRangeException>();
    }
}
