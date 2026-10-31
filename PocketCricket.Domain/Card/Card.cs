using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCricket.Domain.Cards;

/// <summary>
/// Represents a standard playing card.
/// </summary>
public sealed record Card
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Card"/> record.
    /// </summary>
    /// <param name="suit">The card suit.</param>
    /// <param name="rank">The card rank.</param>
    public Card(CardSuit suit, CardRank rank)
    {

        if (!Enum.IsDefined(suit))
        {
            throw new ArgumentOutOfRangeException(
                nameof(suit),
                suit,
                "The specified card suit is invalid.");
        }

        if (!Enum.IsDefined(rank))
        {
            throw new ArgumentOutOfRangeException(
                nameof(rank),
                rank,
                "The specified card rank is invalid.");
        }

        Suit = suit;
        Rank = rank;
    }

    /// <summary>
    /// Gets the suit of the card.
    /// </summary>
    public CardSuit Suit { get; }

    /// <summary>
    /// Gets the rank of the card.
    /// </summary>
    public CardRank Rank { get; }

    /// <summary>
    /// Gets the face value of the card.
    /// </summary>
    public CardFace Face =>
        Rank switch
        {
            CardRank.Jack => CardFace.Jack,
            CardRank.Queen => CardFace.Queen,
            CardRank.King => CardFace.King,
            _ => CardFace.None
        };

    /// <summary>
    /// Gets the PocketCricket game value of the card.
    /// </summary>
    public int GameValue =>
        Rank switch
        {
            CardRank.Ace => 1,
            CardRank.Two => 2,
            CardRank.Three => 3,
            CardRank.Four => 4,
            CardRank.Five => 5,
            CardRank.Six => 6,
            CardRank.Seven => 7,
            CardRank.Eight => 8,
            CardRank.Nine => 9,
            CardRank.Ten => 10,
            _ => 0
        };
}
