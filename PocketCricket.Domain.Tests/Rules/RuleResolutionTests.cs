using FluentAssertions;
using PocketCricket.Domain.Cards;
using PocketCricket.Domain.Rules;


namespace PocketCricket.Domain.Tests.Rules;

/// <summary>
/// 
/// </summary>
public sealed class RuleResolutionTests
{
    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public void Constructor_ShouldStoreRuleCandidate()
    {
        // Arrange
        RuleCandidate candidate = new(
            RuleType.Kqj,
            []);

        // Act
        RuleResolution resolution = new(candidate);

        // Assert
        resolution.Rule.Should().Be(candidate);
    }

    /// <summary>
    /// Tests that a new rule resolution starts with no covered cells.
    /// </summary>
    [Fact]
    public void Constructor_ShouldStartWithNoCoveredCells()
    {
        // Arrange
        RuleCandidate candidate = new(
            RuleType.Kqj,
            []);

        // Act
        RuleResolution resolution = new(candidate);

        // Assert
        resolution.CoveredCells.Should().BeEmpty();
    }

    /// <summary>
    /// Tests that a covered cell can be added to a rule resolution.
    /// </summary>
    [Fact]
    public void AddCoveredCell_WhenCellIsProvided_ShouldStoreCell()
    {
        // Arrange

        Card king = new(CardSuit.Spades, CardRank.King);

        RuleCandidate candidate = new(
            RuleType.Kqj,
            []);

        RuleResolution resolution = new(candidate);

        // Act
        resolution.AddCoveredCell(new RuleCell(2, king));

        // Assert
        resolution.CoveredCells.Should().ContainSingle(x => x.CellIndex == 2);
    }

    /// <summary>
    /// Tests that a resolution is incomplete until all rule cells are covered.
    /// </summary>
    [Fact]
    public void IsCompleted_WhenAllCellsAreCovered_ShouldReturnTrue()
    {
        // Arrange

        Card king = new(CardSuit.Spades, CardRank.King);
        Card queen = new(CardSuit.Hearts, CardRank.Queen);
        Card jack = new(CardSuit.Diamonds, CardRank.Jack);

        RuleCandidate candidate = new(
            RuleType.Kqj,
            [new RuleCell(2, king),
        new RuleCell(5, queen),
        new RuleCell(6, jack)]);

        RuleResolution resolution = new(candidate);

        resolution.AddCoveredCell(new RuleCell(2, king));
        resolution.AddCoveredCell(new RuleCell(5, queen));
        resolution.AddCoveredCell(new RuleCell(6, jack));

        // Act
        bool result = resolution.IsCompleted;

        // Assert
        result.Should().BeTrue();
    }

    /// <summary>
    /// Tests that a resolution remains incomplete when some cells are not covered.
    /// </summary>
    [Fact]
    public void IsCompleted_WhenSomeCellsAreMissing_ShouldReturnFalse()
    {
        // Arrange

        Card king = new(CardSuit.Spades, CardRank.King);
        Card queen = new(CardSuit.Hearts, CardRank.Queen);
        Card jack = new(CardSuit.Diamonds, CardRank.Jack);

        RuleCandidate candidate = new(
            RuleType.Kqj,
            [new RuleCell(2, king),
        new RuleCell(5, queen),
        new RuleCell(6, jack)]);

        RuleResolution resolution = new(candidate);

        resolution.AddCoveredCell(new RuleCell(2, king));
        resolution.AddCoveredCell(new RuleCell(5, queen));

        // Act
        bool result = resolution.IsCompleted;

        // Assert
        result.Should().BeFalse();
    }
}
