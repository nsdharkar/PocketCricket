using FluentAssertions;
using PocketCricket.Domain.Interfaces;
using PocketCricket.Domain.Tests.Random.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCricket.Domain.Tests.Random;

/// <summary>
/// 
/// </summary>
public sealed class FakeRandomProviderTests
{
    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public void Next_WhenSequenceContainsValues_ShouldReturnValuesInOrder()
    {
        // Arrange
        IRandomProvider randomProvider = new FakeRandomProvider(4, 7, 2);

        // Act
        int first = randomProvider.Next(10);
        int second = randomProvider.Next(10);
        int third = randomProvider.Next(10);

        // Assert
        first.Should().Be(4);
        second.Should().Be(7);
        third.Should().Be(2);
    }

    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public void Next_WhenSequenceIsExhausted_ShouldThrowInvalidOperationException()
    {
        // Arrange
        IRandomProvider randomProvider = new FakeRandomProvider(5);

        randomProvider.Next(10);

        // Act
        Action action = () => randomProvider.Next(10);

        // Assert
        action.Should()
              .Throw<InvalidOperationException>();
    }
}
