using System;
using System.Collections.Generic;
using System.Text;
using PocketCricket.Domain.Rules;
using FluentAssertions;

namespace PocketCricket.Domain.Tests.Rules;

/// <summary>
/// 
/// </summary>
public sealed class CoverAttemptTests
{
    /// <summary>
    /// Tests that a cover attempt stores the selected cell indexes.
    /// </summary>
    [Fact]
    public void Constructor_WhenIndexesProvided_ShouldStoreIndexes()
    {
        // Arrange
        IReadOnlyList<int> indexes = [1, 4, 7];

        // Act
        CoverAttempt attempt = new(indexes);

        // Assert
        attempt.CellIndexes.Should().BeEquivalentTo(indexes);
    }
}
