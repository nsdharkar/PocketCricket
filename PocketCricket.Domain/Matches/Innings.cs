using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCricket.Domain.Matches;

/// <summary>
/// Represents an innings in a Pocket Cricket match.
/// </summary>
public sealed class Innings
{
    /// <summary>
    /// Initializes a new instance of the Innings class with the specified batting side.
    /// </summary>
    /// <param name="battingSide">The side of the team that is batting in this innings.</param>
    /// <param name="bowlingSide">The side of the team that is bowling in this innings.</param>
    public Innings(PlayerSide battingSide, PlayerSide bowlingSide)
    {
        BattingSide = battingSide;
        BowlingSide = bowlingSide;

        CurrentTurn = new Turn(TurnType.Bowling);
    }

    /// <summary>
    /// Gets the side of the team that is batting in this innings.
    /// </summary>
    public PlayerSide BattingSide { get; }

    /// <summary>
    /// Gets the side of the team that is bowling in this innings.
    /// </summary>
    public PlayerSide BowlingSide { get; }

    /// <summary>
    /// Gets the current segment of the innings.
    /// </summary>
    public Turn CurrentTurn { get; private set; }
}
