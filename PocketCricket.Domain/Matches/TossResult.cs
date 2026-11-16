using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCricket.Domain.Matches;

/// <summary>
/// 
/// </summary>
public sealed class TossResult
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="playerOneCardsPlaced"></param>
    /// <param name="playerTwoCardsPlaced"></param>
    /// <param name="winner"></param>
    /// <param name="status"></param>
    public TossResult(
        int playerOneCardsPlaced,
        int playerTwoCardsPlaced,
        PlayerSide? winner,
        TossStatus status)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(
            playerOneCardsPlaced,
            9);

        ArgumentOutOfRangeException.ThrowIfLessThan(
            playerTwoCardsPlaced,
            9);

        PlayerOneCardsPlaced = playerOneCardsPlaced;
        PlayerTwoCardsPlaced = playerTwoCardsPlaced;
        Winner = winner;
        Status = status;
    }

    /// <summary>
    /// 
    /// </summary>
    public int PlayerOneCardsPlaced { get; }

    /// <summary>
    /// 
    /// </summary>
    public int PlayerTwoCardsPlaced { get; }
    
    /// <summary>
    /// 
    /// </summary>
    public PlayerSide? Winner { get; }

    /// <summary>
    /// 
    /// </summary>
    public TossStatus Status { get; }
}
