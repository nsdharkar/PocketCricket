using PocketCricket.Domain.Cards;
using PocketCricket.Domain.Exceptions;
using PocketCricket.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCricket.Domain.Decks;

/// <summary>
/// Represents a deck of cards.
/// </summary>
public sealed class Deck
{
    private readonly Stack<Card> _cards;

    /// <summary>
    /// Gets the collection of cards in the deck.
    /// </summary>
    public IReadOnlyCollection<Card> Cards => _cards;

    /// <summary>
    /// Initializes a new instance of the <see cref="Deck"/> class.
    /// </summary>
    /// <param name="randomProvider"></param>
    public Deck(IRandomProvider randomProvider)
    {
        ArgumentNullException.ThrowIfNull(randomProvider);

        _cards = CreateOrderedDeck();
    }

    /// <summary>
    /// Gets the number of cards currently in the deck.
    /// </summary>
    public int Count => _cards.Count;

    private static Stack<Card> CreateOrderedDeck()
    {
        Stack<Card> cards = new();

        foreach (CardSuit suit in Enum.GetValues<CardSuit>())
        {
            foreach (CardRank rank in Enum.GetValues<CardRank>())
            {
                cards.Push(new Card(suit, rank));
            }
        }

        return cards;
    }

    /// <summary>
    /// Draws a card from the deck.
    /// </summary>
    /// <returns>The drawn </returns>
    public Card Draw()
    {

        if (_cards.Count == 0)
        {
            throw new DeckEmptyException();
        }

        return _cards.Pop();
    }
}
