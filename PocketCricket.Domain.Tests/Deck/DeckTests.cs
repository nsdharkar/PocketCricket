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
    /// 
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
}
