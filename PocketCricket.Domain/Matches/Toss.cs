using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCricket.Domain.Matches;

/// <summary>
/// 
/// </summary>
public sealed class Toss
{
    /// <summary>
    /// Resolves the toss outcome using the number of cards placed by each player.
    /// </summary>
    /// <param name="playerOneResult">Player one's toss segment result.</param>
    /// <param name="playerTwoResult">Player two's toss segment result.</param>
    /// <returns>The toss outcome.</returns>
    public TossResult Resolve(
        SegmentResult playerOneResult,
        SegmentResult playerTwoResult)
    {
        ArgumentNullException.ThrowIfNull(playerOneResult);
        ArgumentNullException.ThrowIfNull(playerTwoResult);

        if (playerOneResult.CardsPlaced == playerTwoResult.CardsPlaced)
        {
            return new TossResult(
                playerOneResult.CardsPlaced,
                playerTwoResult.CardsPlaced,
                null,
                TossStatus.ReplayRequired);
        }

        if (playerOneResult.CardsPlaced > playerTwoResult.CardsPlaced)
        {
            return new TossResult(
                playerOneResult.CardsPlaced,
                playerTwoResult.CardsPlaced,
                PlayerSide.PlayerOne,
                TossStatus.Completed);
        }

        return new TossResult(
            playerOneResult.CardsPlaced,
            playerTwoResult.CardsPlaced,
            PlayerSide.PlayerTwo,
            TossStatus.Completed);
    }
}
