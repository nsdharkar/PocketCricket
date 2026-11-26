using PocketCricket.Domain.Cards;
using PocketCricket.Domain.Matrices;
using PocketCricket.Domain.Rules;
using System;
using System.Collections.Generic;
using System.Text;
using PocketCricket.Domain.Interfaces;

namespace PocketCricket.Domain.Matches;

/// <summary>
/// Represents a segment of an innings in a Pocket Cricket match.
/// </summary>
public sealed class Turn
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="ruleEngine"></param>
    public Turn(TurnType type, IRuleEngine ruleEngine)
    {
        ArgumentNullException.ThrowIfNull(ruleEngine);

        Type = type;
        State = TurnState.InProgress;
        Matrix = new Matrix();
        _ruleEngine = ruleEngine;
        PendingRules = new PendingRuleCollection();
    }

    /// <summary>
    /// 
    /// </summary>
    public TurnType Type { get; }

    /// <summary>
    /// Gets the lifecycle state of the turn.
    /// </summary>
    public TurnState State { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public Matrix Matrix { get; }

    /// <summary>
    /// Gets the card that was drawn for this turn.
    /// </summary>
    public Card? CurrentCard { get; private set; }

    /// <summary>
    /// Gets the pending rules for the current turn.
    /// </summary>
    public PendingRuleCollection PendingRules { get; }

    private readonly IRuleEngine _ruleEngine;

    /// <summary>
    /// Sets the card that was drawn for this turn.
    /// </summary>
    /// <param name="card"></param>
    public void SetDrawnCard(Card card)
    {
        ArgumentNullException.ThrowIfNull(card);

        CurrentCard = card;
    }

    /// <summary>
    /// Places the currently drawn card into the matrix.
    /// </summary>
    public void PlaceDrawnCard()
    {
        if (CurrentCard is null)
        {
            throw new InvalidOperationException(
                "No drawn card is available to place.");
        }

        Matrix.PlaceCard(CurrentCard);

        IReadOnlyList<RuleCandidate> rules = _ruleEngine.FindRules(Matrix);

        PendingRules.Clear();

        foreach (RuleCandidate rule in rules)
        {
            PendingRules.Add(rule);
        }

        CurrentCard = null;
    }
}
