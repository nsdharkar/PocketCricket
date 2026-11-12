using FluentAssertions;
using PocketCricket.Domain.Cards;
using PocketCricket.Domain.Decks;
using PocketCricket.Domain.Interfaces;
using PocketCricket.Domain.Tests.Random.Helpers;
using PocketCricket.Domain.Exceptions;

namespace PocketCricket.Domain.Tests.DeckTests;

/// <summary>
/// Tests for the Deck class.
/// </summary>
public sealed class DeckTests
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private static IRandomProvider CreateDeterministicRandomProvider()
    {
        return new FakeRandomProvider(Enumerable.Repeat(0, 51).ToArray());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private static Deck CreateDeck()
    {
        return new Deck(CreateDeterministicRandomProvider());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private static IRandomProvider CreateShuffleTestRandomProvider()
    {
        return new FakeRandomProvider(
        17, 3, 28, 7, 41, 12, 5, 33, 19, 1,
        24, 8, 36, 14, 2, 45, 10, 31, 6, 22,
        13, 39, 4, 18, 26, 9, 15, 20, 11, 16,
        7, 12, 3, 8, 5, 4, 2, 6, 1, 0,
        3, 1, 2, 0, 1, 1, 0, 0, 0, 0,
        0);
    }

    /// <summary>
    /// Test case to verify that the constructor creates a deck with 52 cards.
    /// </summary>
    [Fact]
    public void Constructor_ShouldCreateDeckWith52Cards()
    {
        // Arrange
        Deck deck = CreateDeck();

        // Assert
        deck.Count.Should().Be(52);
    }

    /// <summary>
    /// Test case to verify that the constructor creates a deck with unique cards.
    /// </summary>
    [Fact]
    public void Constructor_ShouldCreateDeckWithUniqueCards()
    {
        // Arrange
        Deck deck = CreateDeck();
        
        // Assert
        deck.Cards
            .Should()
            .OnlyHaveUniqueItems();
    }

    /// <summary>
    /// Test case to verify that the Draw method returns a card.
    /// </summary>
    [Fact]
    public void Draw_ShouldReturnCard()
    {
        // Arrange
        Deck deck = CreateDeck();

        // Act
        Card card = deck.Draw();

        // Assert
        card.Should().NotBeNull();
    }

    /// <summary>
    /// Test case to verify that the Draw method reduces the count by one.
    /// </summary>
    [Fact]
    public void Draw_ShouldReduceCountByOne()
    {
        // Arrange
        Deck deck = CreateDeck();

        // Act
        deck.Draw();

        // Assert
        deck.Count.Should().Be(51);
    }

    /// <summary>
    /// Test case to verify that the Draw method returns different cards on consecutive draws.
    /// </summary>
    [Fact]
    public void Draw_ShouldReturnDifferentCardsOnConsecutiveDraws()
    {
        // Arrange
        Deck deck = CreateDeck();

        // Act
        Card firstCard = deck.Draw();
        Card secondCard = deck.Draw();

        // Assert
        firstCard.Should().NotBe(secondCard);
    }

    /// <summary>
    /// Test case to verify that the Draw method throws a DeckEmptyException when the deck is empty.
    /// </summary>
    [Fact]
    public void Draw_WhenDeckIsEmpty_ShouldThrowDeckEmptyException()
    {
        // Arrange
        Deck deck = CreateDeck();

        while (deck.Count > 0)
        {
            deck.Draw();
        }

        // Act
        Action action = () => deck.Draw();

        // Assert
        action.Should()
            .Throw<DeckEmptyException>();
    }

    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public void Constructor_WhenUsingDeterministicRandomProvider_ShouldShuffleDeckDeterministically()
    {
        // Arrange
        Deck deck = new(CreateShuffleTestRandomProvider()); //CreateDeck();

        // Act
        Card first = deck.Draw();
        Card second = deck.Draw();
        Card third = deck.Draw();
        Card fourth = deck.Draw();
        Card fifth = deck.Draw();

        // Assert
        first.Should().Be(new Card(CardSuit.Diamonds, CardRank.Five));
        second.Should().Be(new Card(CardSuit.Clubs, CardRank.Four));
        third.Should().Be(new Card(CardSuit.Hearts, CardRank.Three));
        fourth.Should().Be(new Card(CardSuit.Clubs, CardRank.Eight));
        fifth.Should().Be(new Card(CardSuit.Spades, CardRank.Three));
    }
}
