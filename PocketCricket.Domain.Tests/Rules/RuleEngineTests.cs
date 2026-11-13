using FluentAssertions;
using PocketCricket.Domain.Cards;
using PocketCricket.Domain.Matrices;
using PocketCricket.Domain.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCricket.Domain.Tests.Rules;

/// <summary>
/// 
/// </summary>
public sealed class RuleEngineTests
{
    /// <summary>
    /// Tests the behavior of the <see cref="RuleEngine.FindRules"/> method
    /// when no rules exist in the provided matrix.
    /// </summary>
    [Fact]
    public void FindRules_WhenNoRulesExist_ShouldReturnEmptyCollection()
    {
        // Arrange
        Matrix matrix = new();

        matrix.PlaceCard(new Card(CardSuit.Hearts, CardRank.Two));
        matrix.PlaceCard(new Card(CardSuit.Spades, CardRank.Five));
        matrix.PlaceCard(new Card(CardSuit.Clubs, CardRank.Eight));

        RuleEngine ruleEngine = new();

        // Act
        IReadOnlyList<RuleCandidate> rules =
            ruleEngine.FindRules(matrix);

        // Assert
        rules.Should().BeEmpty();
    }

    /// <summary>
    /// Tests the behavior of the <see cref="RuleEngine.FindRules"/> method
    /// when a single eleven pair exists in the provided matrix.
    /// </summary>
    [Fact]
    public void FindRules_WhenSingleElevenPairExists_ShouldReturnOneElevenPair()
    {
        // Arrange
        Matrix matrix = new();

        matrix.PlaceCard(new Card(CardSuit.Hearts, CardRank.Seven));
        matrix.PlaceCard(new Card(CardSuit.Clubs, CardRank.Four));

        RuleEngine ruleEngine = new();

        // Act
        IReadOnlyList<RuleCandidate> rules =
            ruleEngine.FindRules(matrix);

        // Assert
        rules.Should().ContainSingle();

        RuleCandidate rule = rules.Single();

        rule.RuleType.Should().Be(RuleType.ElevenPair);
    }

    /// <summary>
    /// Tests the behavior of the <see cref="RuleEngine.FindRules"/> method
    /// when the board contains two independent eleven pairs.
    /// </summary>
    [Fact]
    public void FindRules_WhenBoardContainsTwoIndependentElevenPairs_ShouldReturnTwoCandidates()
    {
        // Arrange
        Matrix matrix = new();

        matrix.PlaceCard(new Card(CardSuit.Hearts, CardRank.Seven));
        matrix.PlaceCard(new Card(CardSuit.Clubs, CardRank.Four));
        matrix.PlaceCard(new Card(CardSuit.Diamonds, CardRank.Nine));
        matrix.PlaceCard(new Card(CardSuit.Spades, CardRank.Two));

        RuleEngine ruleEngine = new();

        // Act
        IReadOnlyList<RuleCandidate> rules =
            ruleEngine.FindRules(matrix);

        // Assert
        rules.Should().HaveCount(2);

        rules.Should().OnlyContain(rule =>
            rule.RuleType == RuleType.ElevenPair);
    }

    /// <summary>
    /// Tests the behavior of the <see cref="RuleEngine.FindRules"/> method
    /// when the board contains two independent eleven pairs.
    /// </summary>
    [Fact]
    public void FindRules_WhenBoardContainsDuplicateLowerCards_ShouldReturnTwoCandidates()
    {
        // Arrange
        Matrix matrix = new();

        matrix.PlaceCard(new Card(CardSuit.Hearts, CardRank.Seven));
        matrix.PlaceCard(new Card(CardSuit.Clubs, CardRank.Four));
        matrix.PlaceCard(new Card(CardSuit.Diamonds, CardRank.Four));

        RuleEngine ruleEngine = new();

        // Act
        IReadOnlyList<RuleCandidate> rules =
            ruleEngine.FindRules(matrix);

        // Assert
        rules.Should().HaveCount(2);
    }

    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public void FindRules_WhenSingleElevenPairExists_ShouldReturnParticipatingCards()
    {
        // Arrange
        Matrix matrix = new();

        Card seven = new(CardSuit.Hearts, CardRank.Seven);
        Card four = new(CardSuit.Clubs, CardRank.Four);

        matrix.PlaceCard(seven);
        matrix.PlaceCard(four);

        RuleEngine ruleEngine = new();

        // Act
        IReadOnlyList<RuleCandidate> rules =
            ruleEngine.FindRules(matrix);

        // Assert
        RuleCandidate rule = rules.Should().ContainSingle().Subject;

        rule.RuleType.Should().Be(RuleType.ElevenPair);

        rule.Cells[0].Card.Should().Be(seven);
        rule.Cells[1].Card.Should().Be(four);

        rule.Cells.Should().HaveCount(2);

        rule.Cells[0].CellIndex.Should().Be(0);
        rule.Cells[0].Card.Should().Be(seven);

        rule.Cells[1].CellIndex.Should().Be(1);
        rule.Cells[1].Card.Should().Be(four);
    }

    /// <summary>
    /// Tests the behavior of the <see cref="RuleEngine.FindRules"/> method
    /// when a single eleven pair exists on the board.
    /// </summary>
    [Fact]
    public void FindRules_WhenSingleElevenPairExists_ShouldReturnCandidateWithParticipatingCells()
    {
        // Arrange
        Matrix matrix = new();

        Card seven = new(CardSuit.Hearts, CardRank.Seven);
        Card four = new(CardSuit.Clubs, CardRank.Four);

        matrix.PlaceCard(seven);
        matrix.PlaceCard(four);

        RuleEngine ruleEngine = new();

        // Act
        IReadOnlyList<RuleCandidate> rules = ruleEngine.FindRules(matrix);

        // Assert
        RuleCandidate candidate = rules.Should().ContainSingle().Subject;

        candidate.RuleType.Should().Be(RuleType.ElevenPair);

        candidate.Cells.Should().HaveCount(2);

        candidate.Cells[0].CellIndex.Should().Be(0);
        candidate.Cells[0].Card.Should().Be(seven);

        candidate.Cells[1].CellIndex.Should().Be(1);
        candidate.Cells[1].Card.Should().Be(four);
    }

    /// <summary>
    /// Tests the behavior of the <see cref="RuleEngine.FindRules"/> method
    /// when a single trio exists on the board.
    /// </summary>
    [Fact]
    public void FindRules_WhenBoardContainsSingleTrio_ShouldReturnOneCandidate()
    {
        // Arrange
        Matrix matrix = new();

        matrix.PlaceCard(new Card(CardSuit.Hearts, CardRank.Five));
        matrix.PlaceCard(new Card(CardSuit.Clubs, CardRank.Five));
        matrix.PlaceCard(new Card(CardSuit.Spades, CardRank.Five));

        RuleEngine ruleEngine = new();

        // Act
        IReadOnlyList<RuleCandidate> rules = ruleEngine.FindRules(matrix);

        // Assert
        RuleCandidate candidate = rules.Should().ContainSingle().Subject;

        candidate.RuleType.Should().Be(RuleType.Trio);

        candidate.Cells.Should().HaveCount(3);
    }

    /// <summary>
    /// Tests the behavior of the <see cref="RuleEngine.FindRules"/> method
    /// when a single KQJ sequence exists in a different placement order.
    /// </summary>
    [Fact]
    public void FindRules_WhenBoardContainsSingleKqjInDifferentOrder_ShouldReturnOneCandidate()
    {
        // Arrange
        Matrix matrix = new();

        matrix.PlaceCard(new Card(CardSuit.Hearts, CardRank.Queen));
        matrix.PlaceCard(new Card(CardSuit.Clubs, CardRank.Jack));
        matrix.PlaceCard(new Card(CardSuit.Spades, CardRank.King));

        RuleEngine ruleEngine = new();

        // Act
        IReadOnlyList<RuleCandidate> rules = ruleEngine.FindRules(matrix);

        // Assert
        RuleCandidate candidate = rules.Should().ContainSingle().Subject;

        candidate.RuleType.Should().Be(RuleType.Kqj);

        candidate.Cells.Should().HaveCount(3);

        candidate.Cells[0].CellIndex.Should().Be(2);
        candidate.Cells[0].Card.Rank.Should().Be(CardRank.King);

        candidate.Cells[1].CellIndex.Should().Be(0);
        candidate.Cells[1].Card.Rank.Should().Be(CardRank.Queen);

        candidate.Cells[2].CellIndex.Should().Be(1);
        candidate.Cells[2].Card.Rank.Should().Be(CardRank.Jack);
    }

    /// <summary>
    /// Tests the behavior of the <see cref="RuleEngine.FindRules"/> method
    /// when both an Eleven Pair and a Trio exist on the board.
    /// </summary>
    [Fact]
    public void FindRules_WhenElevenPairAndTrioExist_ShouldReturnBothCandidates()
    {
        // Arrange
        Matrix matrix = new();

        matrix.PlaceCard(new Card(CardSuit.Hearts, CardRank.Seven));
        matrix.PlaceCard(new Card(CardSuit.Clubs, CardRank.Four));

        matrix.PlaceCard(new Card(CardSuit.Hearts, CardRank.Five));
        matrix.PlaceCard(new Card(CardSuit.Clubs, CardRank.Five));
        matrix.PlaceCard(new Card(CardSuit.Spades, CardRank.Five));

        RuleEngine ruleEngine = new();

        // Act
        IReadOnlyList<RuleCandidate> rules = ruleEngine.FindRules(matrix);

        // Assert
        rules.Should().HaveCount(2);

        rules.Should().Contain(rule => rule.RuleType == RuleType.ElevenPair);
        rules.Should().Contain(rule => rule.RuleType == RuleType.Trio);
    }

    /// <summary>
    /// Tests the behavior of the <see cref="RuleEngine.FindRules"/> method
    /// when both a KQJ sequence and an Eleven Pair exist on the board.
    /// </summary>
    [Fact]
    public void FindRules_WhenKqjAndElevenPairExist_ShouldReturnBothCandidates()
    {
        // Arrange
        Matrix matrix = new();

        matrix.PlaceCard(new Card(CardSuit.Hearts, CardRank.King));
        matrix.PlaceCard(new Card(CardSuit.Clubs, CardRank.Queen));
        matrix.PlaceCard(new Card(CardSuit.Spades, CardRank.Jack));

        matrix.PlaceCard(new Card(CardSuit.Diamonds, CardRank.Seven));
        matrix.PlaceCard(new Card(CardSuit.Hearts, CardRank.Four));

        RuleEngine ruleEngine = new();

        // Act
        IReadOnlyList<RuleCandidate> rules = ruleEngine.FindRules(matrix);

        // Assert
        rules.Should().HaveCount(2);

        rules.Should().Contain(rule => rule.RuleType == RuleType.Kqj);
        rules.Should().Contain(rule => rule.RuleType == RuleType.ElevenPair);
    }

    /// <summary>
    /// Tests the behavior of the <see cref="RuleEngine.FindRules"/> method
    /// when all supported rule types exist on the board.
    /// </summary>
    [Fact]
    public void FindRules_WhenAllRuleTypesExist_ShouldReturnAllCandidates()
    {
        // Arrange
        Matrix matrix = new();

        matrix.PlaceCard(new Card(CardSuit.Hearts, CardRank.King));
        matrix.PlaceCard(new Card(CardSuit.Clubs, CardRank.Queen));
        matrix.PlaceCard(new Card(CardSuit.Spades, CardRank.Jack));

        matrix.PlaceCard(new Card(CardSuit.Hearts, CardRank.Five));
        matrix.PlaceCard(new Card(CardSuit.Clubs, CardRank.Five));
        matrix.PlaceCard(new Card(CardSuit.Diamonds, CardRank.Five));

        matrix.PlaceCard(new Card(CardSuit.Hearts, CardRank.Seven));
        matrix.PlaceCard(new Card(CardSuit.Clubs, CardRank.Four));

        RuleEngine ruleEngine = new();

        // Act
        IReadOnlyList<RuleCandidate> rules = ruleEngine.FindRules(matrix);

        // Assert
        rules.Should().HaveCount(3);

        rules.Should().Contain(rule => rule.RuleType == RuleType.Kqj);
        rules.Should().Contain(rule => rule.RuleType == RuleType.Trio);
        rules.Should().Contain(rule => rule.RuleType == RuleType.ElevenPair);
    }
}
