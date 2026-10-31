using FluentAssertions;
using PocketCricket.Domain.Cards;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCricket.Domain.Tests.Cards;

/// <summary>
/// Tests for the <see cref="CardFace"/> enumeration.
/// </summary>
public sealed class CardFaceTests
{
    /// <summary>
    /// Verifies that the CardFace enumeration contains exactly four expected values: None, Jack, Queen, and King.
    /// </summary>
    [Fact]
    public void CardFace_ShouldContain_FourExpectedValues()
    {
        // Arrange & Act
        var faces = Enum.GetValues<CardFace>();

        // Assert
        faces.Should().HaveCount(4);

        faces.Should().Contain(CardFace.None);
        faces.Should().Contain(CardFace.Jack);
        faces.Should().Contain(CardFace.Queen);
        faces.Should().Contain(CardFace.King);
    }

    /// <summary>
    /// Verifies that each CardFace enum member has the expected integer value.
    /// </summary>
    /// <param name="face">The CardFace enum member to test.</param>
    /// <param name="expectedValue">The expected integer value of the CardFace enum member.</param>
    [Theory]
    [InlineData(CardFace.None, 0)]
    [InlineData(CardFace.Jack, 1)]
    [InlineData(CardFace.Queen, 2)]
    [InlineData(CardFace.King, 3)]
    public void CardFace_ShouldHave_ExpectedEnumValues(CardFace face, int expectedValue)
    {
        // Assert
        ((int)face).Should().Be(expectedValue);
    }

    /// <summary>
    /// Verifies that the CardFace enumeration contains only unique values.
    /// </summary>
    [Fact]
    public void CardFace_ShouldContain_OnlyUniqueValues()
    {
        // Arrange & Act
        var values = Enum.GetValues<CardFace>().Cast<int>();

        // Assert
        values.Should().OnlyHaveUniqueItems();
    }
}
