using PocketCricket.Domain.Decks;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCricket.Domain.Matches;

/// <summary>
/// Represents a Pocket Cricket match.
/// </summary>
public sealed class Match
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Match"/> class.
    /// </summary>
    /// <param name="settings">The match settings.</param>
    /// <param name="deck">The deck used throughout the match.</param>
    public Match(MatchSettings settings, Deck deck)
    {
        ArgumentNullException.ThrowIfNull(settings);
        ArgumentNullException.ThrowIfNull(deck);

        Settings = settings;
        Deck = deck;
        State = MatchState.WaitingForToss;
    }

    /// <summary>
    /// Gets the immutable settings for this match.
    /// </summary>
    public MatchSettings Settings { get; }

    /// <summary>
    /// The current innings object in the game state.
    /// </summary>
    public Innings? CurrentInnings { get; private set; }

    /// <summary>
    /// Gets the current state of the match.
    /// </summary>
    public MatchState State { get; private set; }

    /// <summary>
    /// Gets the result of the toss, if it has been completed.
    /// </summary>
    public TossResult? TossResult { get; private set; }

    /// <summary>
    /// Gets the decision of the toss, if it has been made.
    /// </summary>
    public TossDecision? Decision { get; private set; }

    /// <summary>
    /// Gets the deck used throughout the match.
    /// </summary>
    public Deck Deck { get; }

    /// <summary>
    /// Applies the result of the toss to the match.
    /// </summary>
    /// <param name="tossResult"></param>
    public void ApplyTossResult(TossResult tossResult)
    {
        ArgumentNullException.ThrowIfNull(tossResult);

        if (TossResult is not null)
        {
            throw new InvalidOperationException(
                "The toss result has already been applied.");
        }

        TossResult = tossResult;

        State = MatchState.WaitingForTossDecision;
    }

    /// <summary>
    /// Applies the decision of the toss to the match.
    /// </summary>
    /// <param name="player"></param>
    /// <param name="decision"></param>
    public void ApplyTossDecision(PlayerSide player, TossDecision decision)
    {
        PlayerSide battingSide;
        PlayerSide bowlingSide;

        if (TossResult is null)
        {
            throw new InvalidOperationException(
                "The toss result must be applied before the toss decision.");
        }

        if (TossResult.Winner != player)
        {
            throw new InvalidOperationException(
                "Only the toss winner can make the toss decision.");
        }

        if (Decision is not null)
        {
            throw new InvalidOperationException(
                "The toss decision has already been made.");
        }

        Decision = decision;

        if (decision == TossDecision.Bat)
        {
            battingSide = player;

            bowlingSide = GetOpponent(player);
        }
        else
        {
            battingSide = GetOpponent(player);
            
            bowlingSide = player;
        }


        CurrentInnings = new Innings(battingSide, bowlingSide);

        State = MatchState.InProgress;
    }

    private static PlayerSide GetOpponent(PlayerSide player)
    {
        return player == PlayerSide.PlayerOne
            ? PlayerSide.PlayerTwo
            : PlayerSide.PlayerOne;
    }


}
