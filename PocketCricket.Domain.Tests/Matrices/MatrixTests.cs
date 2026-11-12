using FluentAssertions;
using PocketCricket.Domain.Cards;
using PocketCricket.Domain.Matrices;

namespace PocketCricket.Domain.Tests.Matrices;

/// <summary>
/// 
/// </summary>
public sealed class MatrixTests
{
    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public void Constructor_ShouldCreateMatrixWithNineEmptyCells()
    {
        // Arrange
        Matrix matrix = new();

        // Assert
        matrix.Cells.Should().HaveCount(9);
        matrix.Cells
            .Should()
            .OnlyContain(cell => cell.IsEmpty);
    }

    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public void PlaceCard_WhenMatrixIsEmpty_ShouldPlaceCardInFirstCell()
    {
        // Arrange
        Matrix matrix = new();

        Card card = new(
            CardSuit.Hearts,
            CardRank.Seven);

        // Act
        matrix.PlaceCard(card);

        // Assert
        matrix.Cells[0].Card.Should().Be(card);

        matrix.Cells
            .Skip(1)
            .Should()
            .OnlyContain(cell => cell.IsEmpty);
    }

    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public void PlaceCard_WhenCalledMultipleTimes_ShouldPlaceCardsInSequentialOrder()
    {
        // Arrange
        Matrix matrix = new();

        Card firstCard = new(CardSuit.Hearts, CardRank.Seven);
        Card secondCard = new(CardSuit.Spades, CardRank.Ace);
        Card thirdCard = new(CardSuit.Clubs, CardRank.King);

        // Act
        matrix.PlaceCard(firstCard);
        matrix.PlaceCard(secondCard);
        matrix.PlaceCard(thirdCard);

        // Assert
        matrix.Cells[0].Card.Should().Be(firstCard);
        matrix.Cells[1].Card.Should().Be(secondCard);
        matrix.Cells[2].Card.Should().Be(thirdCard);

        matrix.Cells
            .Skip(3)
            .Should()
            .OnlyContain(cell => cell.IsEmpty);
    }

    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public void ReplaceCard_ShouldReplaceTheExistingCard()
    {
        // Arrange
        Matrix matrix = new();

        Card originalCard = new(CardSuit.Hearts, CardRank.Seven);
        Card coveringCard = new(CardSuit.Spades, CardRank.Ace);

        matrix.PlaceCard(originalCard);

        // Act
        matrix.CoverCard(0, coveringCard);

        // Assert
        matrix.Cells[0].Card.Should().Be(coveringCard);
    }

    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public void ReplaceCard_ShouldRecordCoverHistory()
    {
        // Arrange
        Matrix matrix = new();

        Card coveredCard = new(CardSuit.Hearts, CardRank.Seven);
        Card coveringCard = new(CardSuit.Spades, CardRank.Ace);

        matrix.PlaceCard(coveredCard);

        // Act
        matrix.CoverCard(0, coveringCard);

        // Assert
        matrix.CoverHistory.Should().ContainSingle();

        CardCoverRecord history = matrix.CoverHistory.Single();

        history.CoveredCard.Should().Be(coveredCard);
        history.CoveringCard.Should().Be(coveringCard);
        history.CellIndex.Should().Be(0);
    }
}
