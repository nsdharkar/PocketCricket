using PocketCricket.Domain.Cards;
using PocketCricket.Domain.Matrices;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCricket.Domain.Rules;

/// <summary>
/// 
/// </summary>
public sealed class RuleEngine
{
    /// <summary>
    /// Finds all valid rules on the supplied matrix.
    /// </summary>
    public IReadOnlyList<RuleCandidate> FindRules(Matrix matrix)
    {
        ArgumentNullException.ThrowIfNull(matrix);
        List<RuleCandidate> rules = [];

        FindElevenPairs(matrix, rules);
        FindTrios(matrix, rules);
        FindKqjSequences(matrix, rules);



        return rules;
    }

    private static void FindElevenPairs(Matrix matrix, ICollection<RuleCandidate> rules)
    {
        for (int firstIndex = 0; firstIndex < matrix.Cells.Count - 1; firstIndex++)
        {
            MatrixCell firstCell = matrix.Cells[firstIndex];

            if (firstCell.IsEmpty)
            {
                continue;
            }

            for (int secondIndex = firstIndex + 1; secondIndex < matrix.Cells.Count; secondIndex++)
            {
                MatrixCell secondCell = matrix.Cells[secondIndex];

                if (secondCell.IsEmpty)
                {
                    continue;
                }

                Card firstCard = firstCell.Card!;
                Card secondCard = secondCell.Card!;

                if (firstCard.GameValue + secondCard.GameValue == 11)
                {
                    rules.Add(
                        new RuleCandidate(
                            RuleType.ElevenPair,
                            [
                                new RuleCell(firstIndex, firstCard),
                                new RuleCell(secondIndex, secondCard)
                            ]));
                }
            }
        }
    }

    private static void FindTrios(Matrix matrix, ICollection<RuleCandidate> rules)
    {
        for (int firstIndex = 0; firstIndex < matrix.Cells.Count - 2; firstIndex++)
        {
            MatrixCell firstCell = matrix.Cells[firstIndex];

            if (firstCell.IsEmpty)
            {
                continue;
            }

            for (int secondIndex = firstIndex + 1; secondIndex < matrix.Cells.Count - 1; secondIndex++)
            {
                MatrixCell secondCell = matrix.Cells[secondIndex];

                if (secondCell.IsEmpty)
                {
                    continue;
                }

                for (int thirdIndex = secondIndex + 1; thirdIndex < matrix.Cells.Count; thirdIndex++)
                {
                    MatrixCell thirdCell = matrix.Cells[thirdIndex];

                    if (thirdCell.IsEmpty)
                    {
                        continue;
                    }

                    Card firstCard = firstCell.Card!;
                    Card secondCard = secondCell.Card!;
                    Card thirdCard = thirdCell.Card!;

                    if (firstCard.Rank == secondCard.Rank && secondCard.Rank == thirdCard.Rank)
                    {
                        rules.Add(
                            new RuleCandidate(
                                RuleType.Trio,
                                [
                                    new RuleCell(firstIndex, firstCard),
                                    new RuleCell(secondIndex, secondCard),
                                    new RuleCell(thirdIndex, thirdCard)
                                ]));
                    }
                }
            }
        }
    }

    private static void FindKqjSequences(Matrix matrix, ICollection<RuleCandidate> rules)
    {
        for (int firstIndex = 0; firstIndex < matrix.Cells.Count - 2; firstIndex++)
        {
            MatrixCell firstCell = matrix.Cells[firstIndex];

            if (firstCell.IsEmpty)
            {
                continue;
            }

            for (int secondIndex = firstIndex + 1; secondIndex < matrix.Cells.Count - 1; secondIndex++)
            {
                MatrixCell secondCell = matrix.Cells[secondIndex];

                if (secondCell.IsEmpty)
                {
                    continue;
                }

                for (int thirdIndex = secondIndex + 1; thirdIndex < matrix.Cells.Count; thirdIndex++)
                {
                    MatrixCell thirdCell = matrix.Cells[thirdIndex];

                    if (thirdCell.IsEmpty)
                    {
                        continue;
                    }

                    Card firstCard = firstCell.Card!;
                    Card secondCard = secondCell.Card!;
                    Card thirdCard = thirdCell.Card!;

                    RuleCell? king = null;
                    RuleCell? queen = null;
                    RuleCell? jack = null;

                    AssignRuleCell(firstIndex, firstCard, ref king, ref queen, ref jack);
                    AssignRuleCell(secondIndex, secondCard, ref king, ref queen, ref jack);
                    AssignRuleCell(thirdIndex, thirdCard, ref king, ref queen, ref jack);

                    if (king is null || queen is null || jack is null)
                    {
                        continue;
                    }

                    rules.Add(
                        new RuleCandidate(
                            RuleType.Kqj,
                            [
                                king,
                            queen,
                            jack
                            ]));
                }
            }
        }
    }

    private static void AssignRuleCell(
    int cellIndex,
    Card card,
    ref RuleCell? king,
    ref RuleCell? queen,
    ref RuleCell? jack)
    {
        switch (card.Rank)
        {
            case CardRank.King:
                king = new RuleCell(cellIndex, card);
                break;

            case CardRank.Queen:
                queen = new RuleCell(cellIndex, card);
                break;

            case CardRank.Jack:
                jack = new RuleCell(cellIndex, card);
                break;
        }
    }
}
