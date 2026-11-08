using FluentAssertions;
using PocketCricket.Domain.Cards;
using PocketCricket.Domain.Decks;
using PocketCricket.Domain.Interfaces;
using PocketCricket.Domain.Tests.Random.Helpers;
using PocketCricket.Domain.Exceptions;

namespace PocketCricket.Domain.Tests;

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
}
