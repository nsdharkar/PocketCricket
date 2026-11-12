using PocketCricket.Domain.Cards;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCricket.Domain.Matrices;

/// <summary>
/// 
/// </summary>
/// <param name="CellIndex"></param>
/// <param name="CoveredCard"></param>
/// <param name="CoveringCard"></param>
public sealed record CardCoverRecord(
    int CellIndex,
    Card CoveredCard,
    Card CoveringCard);
