using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCricket.Domain.Cards;

/// <summary>
/// Specifies the face values for playing cards, excluding numbered cards.
/// </summary>
public enum CardFace
{
    /// <summary>
    /// Represents the absence of a value or option.
    /// </summary>
    None,
    /// <summary>
    /// Represents the jack face value.
    /// </summary>
    Jack,
    /// <summary>
    /// Represents the queen face value.
    /// </summary>
    Queen,
    /// <summary>
    /// Represents the king face value.
    /// </summary>
    King
}
