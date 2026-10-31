using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using PocketCricket.Domain.Cards;

namespace PocketCricket.Domain.Tests.Cards
{
    /// <summary>
    /// Contains unit tests for the CardSuit enumeration.
    /// </summary>
    public sealed class CardSuitTests
    {
        /// <summary>
        /// Verifies that the CardSuit enumeration contains exactly four standard suits: Clubs, Diamonds, Hearts, and
        /// Spades.
        /// </summary>
        [Fact]
        public void CardSuit_ShouldContain_FourStandardSuits()
        {
            var suits = Enum.GetValues<CardSuit>();

            suits.Should().HaveCount(4);

            suits.Should().Contain(CardSuit.Clubs);
            suits.Should().Contain(CardSuit.Diamonds);
            suits.Should().Contain(CardSuit.Hearts);
            suits.Should().Contain(CardSuit.Spades);
        }
    }
}
