using PocketCricket.Domain.Cards;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;

namespace PocketCricket.Domain.Tests.Cards;

/// <summary>
/// Tests for the <see cref="Card"/> value object.
/// </summary>
public sealed class CardTests
{
    /// <summary>
    /// Verifies that a Card correctly stores its suit and rank.
    /// </summary>
    [Fact]
    public void Card_ShouldStore_SuitAndRank()
    {
        // Arrange
        var card = new Card(CardSuit.Hearts, CardRank.Seven);

        // Assert
        card.Suit.Should().Be(CardSuit.Hearts);
        card.Rank.Should().Be(CardRank.Seven);
    }

    /// <summary>
    /// Verifies that two cards with the same suit and rank are considered equal.
    /// </summary>
    [Fact]
    public void Cards_WithSameSuitAndRank_ShouldBeEqual()
    {
        // Arrange
        var firstCard = new Card(CardSuit.Spades, CardRank.King);
        var secondCard = new Card(CardSuit.Spades, CardRank.King);

        // Assert
        firstCard.Should().Be(secondCard);
    }

    /// <summary>
    /// Verifies that two cards with the same rank but different suits are not considered equal.
    /// </summary>
    [Fact]
    public void Cards_WithDifferentSuit_ShouldNotBeEqual()
    {
        // Arrange
        var firstCard = new Card(CardSuit.Hearts, CardRank.Ace);
        var secondCard = new Card(CardSuit.Spades, CardRank.Ace);

        // Assert
        firstCard.Should().NotBe(secondCard);
    }

    /// <summary>
    /// Verifies that two cards with the same suit but different ranks are not considered equal.
    /// </summary>
    [Fact]
    public void Cards_WithDifferentRank_ShouldNotBeEqual()
    {
        // Arrange
        var firstCard = new Card(CardSuit.Hearts, CardRank.Seven);
        var secondCard = new Card(CardSuit.Hearts, CardRank.Eight);

        // Assert
        firstCard.Should().NotBe(secondCard);
    }

    /// <summary>
    /// Verifies that the GameValue property of a Card returns the expected value for a given rank.
    /// </summary>
    /// <param name="rank">The rank of the card to test.</param>
    /// <param name="expectedValue">The expected game value for the specified card rank.</param>
    [Theory]
    [InlineData(CardRank.Ace, 1)]
    [InlineData(CardRank.Seven, 7)]
    [InlineData(CardRank.Ten, 10)]
    [InlineData(CardRank.Jack, 0)]
    [InlineData(CardRank.Queen, 0)]
    [InlineData(CardRank.King, 0)]
    public void Card_ShouldReturn_CorrectGameValue(CardRank rank, int expectedValue)
    {
        // Arrange
        var card = new Card(CardSuit.Hearts, rank);

        // Assert
        card.GameValue.Should().Be(expectedValue);
    }

    /// <summary>
    /// Verifies that the Face property of a Card returns the expected CardFace for a given CardRank.
    /// </summary>
    /// <param name="rank">The rank of the card to test.</param>
    /// <param name="expectedFace">The expected face value for the specified card rank.</param>
    [Theory]
    [InlineData(CardRank.Jack, CardFace.Jack)]
    [InlineData(CardRank.Queen, CardFace.Queen)]
    [InlineData(CardRank.King, CardFace.King)]
    [InlineData(CardRank.Ace, CardFace.None)]
    [InlineData(CardRank.Seven, CardFace.None)]
    public void Card_ShouldReturn_CorrectFace(CardRank rank, CardFace expectedFace)
    {
        // Arrange
        var card = new Card(CardSuit.Spades, rank);

        // Assert
        card.Face.Should().Be(expectedFace);
    }
}
