using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCricket.Domain.Matches;

/// <summary>
/// 
/// </summary>
public sealed class SegmentResult
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="segmentType"></param>
    /// <param name="cardsPlaced"></param>
    public SegmentResult(
        SegmentType segmentType,
        int cardsPlaced)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(cardsPlaced, 9);

        SegmentType = segmentType;
        CardsPlaced = cardsPlaced;
    }

    /// <summary>
    /// 
    /// </summary>
    public SegmentType SegmentType { get; }

    /// <summary>
    /// 
    /// </summary>
    public int CardsPlaced { get; }
}
