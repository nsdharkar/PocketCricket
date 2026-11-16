using PocketCricket.Domain.Matches;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;

namespace PocketCricket.Domain.Tests.Matches;

/// <summary>
/// 
/// </summary>
public sealed class TossResultTests
{
    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public void Constructor_WhenValidValuesProvided_ShouldCreateTossResult()
    {
        TossResult result = new(
            42,
            35,
            PlayerSide.PlayerOne,
            TossStatus.Completed);

        result.PlayerOneCardsPlaced.Should().Be(42);
        result.PlayerTwoCardsPlaced.Should().Be(35);
        result.Winner.Should().Be(PlayerSide.PlayerOne);
        result.Status.Should().Be(TossStatus.Completed);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cardsPlaced"></param>
    [Theory]
    [InlineData(0)]
    [InlineData(8)]
    public void Constructor_WhenCardsPlacedIsLessThanMinimum_ShouldThrowArgumentOutOfRangeException(
    int cardsPlaced)
    {
        Action action = () => new TossResult(
            cardsPlaced,
            10,
            PlayerSide.PlayerTwo,
            TossStatus.Completed);

        action.Should()
            .Throw<ArgumentOutOfRangeException>();
    }
}
