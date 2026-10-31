using FluentAssertions;
using PocketCricket.Domain.Cards;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCricket.Domain.Tests.Cards;

/// <summary>
/// Contains unit tests for the CardRank enumeration.
/// </summary>
public sealed class CardRankTests
{
    /// <summary>
    /// Verifies that the CardRank enumeration contains exactly thirteen ranks.
    /// </summary>
    [Fact]
    public void CardRank_ShouldContain_ThirteenRanks()
    {
        var ranks = Enum.GetValues<CardRank>();

        ranks.Should().HaveCount(13);
    }
}
