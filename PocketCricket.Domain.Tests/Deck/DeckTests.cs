using FluentAssertions;
using PocketCricket.Domain.Deck;
using PocketCricket.Domain.Interfaces;
using PocketCricket.Domain.Tests.Random;

namespace PocketCricket.Domain.Tests.Deck;

/// <summary>
/// Tests for the Deck class.
/// </summary>
public sealed class DeckTests
{
    /// <summary>
    /// Test case to verify that the constructor creates a deck with 52 cards.
    /// </summary>
    [Fact]
    public void Constructor_ShouldCreateDeckWith52Cards()
    {
        // Arrange
        IRandomProvider randomProvider = new FakeRandomProvider();

        // Act
        var deck = new PocketCricket.Domain.Deck.Deck(randomProvider);

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
        IRandomProvider randomProvider = new FakeRandomProvider();

        // Act
        var deck = new PocketCricket.Domain.Deck.Deck(randomProvider);

        // Assert
        deck.Cards
            .Should()
            .OnlyHaveUniqueItems();
    }
}
