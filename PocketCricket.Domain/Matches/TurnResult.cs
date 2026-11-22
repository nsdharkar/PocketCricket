using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCricket.Domain.Matches;

/// <summary>
/// 
/// </summary>
public sealed class TurnResult
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cardsPlaced"></param>
    public TurnResult(int cardsPlaced)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(cardsPlaced, 9);

        CardsPlaced = cardsPlaced;
    }

    /// <summary>
    /// 
    /// </summary>
    public int CardsPlaced { get; }
}
