using PocketCricket.Domain.Cards;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCricket.Domain.Matrices;

/// <summary>
/// Represents the 3x3 cricket card placement matrix.
/// </summary>
public sealed class Matrix
{
    private readonly List<MatrixCell> _cells;

    /// <summary>
    /// 
    /// </summary>
    private readonly List<CardCoverRecord> _coverHistory = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="Matrix"/> class.
    /// </summary>
    public Matrix()
    {
        _cells = Enumerable
            .Range(0, 9)
            .Select(_ => new MatrixCell())
            .ToList();
    }

    /// <summary>
    /// Gets the matrix cells in placement order.
    /// </summary>
    public IReadOnlyList<MatrixCell> Cells => _cells;

    /// <summary>
    /// 
    /// </summary>
    public IReadOnlyList<CardCoverRecord> CoverHistory => _coverHistory;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="card"></param>
    public void PlaceCard(Card card)
    {
        ArgumentNullException.ThrowIfNull(card);

        MatrixCell? emptyCell = _cells.FirstOrDefault(cell => cell.IsEmpty);

        if (emptyCell is null)
        {
            throw new InvalidOperationException("No empty cells are available");
        }

        emptyCell.Place(card);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <param name="card"></param>
    public void CoverCard(int index, Card card)
    {
        ArgumentNullException.ThrowIfNull(card);

        Card coveredCard = _cells[index].Card!;

        _coverHistory.Add(
            new CardCoverRecord(
                index,
                coveredCard,
                card));

        _cells[index].Place(card);
    }
}
