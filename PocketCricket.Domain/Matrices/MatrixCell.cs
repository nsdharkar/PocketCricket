using PocketCricket.Domain.Cards;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCricket.Domain.Matrices;

/// <summary>
/// Represents a single cell in the cricket card matrix.
/// </summary>
public sealed class MatrixCell
{
    /// <summary>
    /// Gets the card currently occupying this cell.
    /// </summary>
    public Card? Card { get; private set; }

    /// <summary>
    /// Gets a value indicating whether this cell is empty.
    /// </summary>
    public bool IsEmpty => Card is null;

    internal void Place(Card card)
    {
        ArgumentNullException.ThrowIfNull(card);
        Card = card;
    }
}
