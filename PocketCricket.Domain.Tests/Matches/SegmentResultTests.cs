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
    /// Tests the behavior of the <see cref="SegmentResult"/> constructor
    /// when valid values are supplied.
    /// </summary>
    [Fact]
    public void Constructor_WhenValidValuesProvided_ShouldCreateSegmentResult()
    {
        // Arrange & Act
        SegmentResult result = new(SegmentType.Bowling, 43);

        // Assert
        result.SegmentType.Should().Be(SegmentType.Bowling);
        result.CardsPlaced.Should().Be(43);
    }


    /// <summary>
    /// Tests the behavior of the <see cref="SegmentResult"/> constructor
    /// when fewer than nine cards are supplied.
    /// </summary>
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(8)]
    public void Constructor_WhenCardsPlacedIsLessThanMinimum_ShouldThrowArgumentOutOfRangeException(
        int cardsPlaced)
    {
        // Arrange & Act
        Action action = () => new SegmentResult(SegmentType.Batting, cardsPlaced);

        // Assert
        action.Should()
            .Throw<ArgumentOutOfRangeException>();
    }
}
