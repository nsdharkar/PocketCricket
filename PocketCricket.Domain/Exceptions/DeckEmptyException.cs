using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCricket.Domain.Exceptions;

/// <summary>
/// Represents an exception that is thrown when an attempt is made to draw a card from an empty deck.
/// </summary>
public sealed class DeckEmptyException : PocketCricketException
{
    /// <summary>
    /// The deck is empty.
    /// </summary>
    public DeckEmptyException() : base("The deck is empty.")
    {
    }
}
