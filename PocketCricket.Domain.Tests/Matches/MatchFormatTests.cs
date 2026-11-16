using FluentAssertions;
using PocketCricket.Domain.Matches;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCricket.Domain.Tests.Matches;

/// <summary>
/// 
/// </summary>
public sealed class MatchFormatTests
{
    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public void MatchFormat_ShouldContainSupportedFormats()
    {
        MatchFormat[] formats = Enum.GetValues<MatchFormat>();

        formats.Should()
            .Contain([
                MatchFormat.Practice,
            MatchFormat.T10,
            MatchFormat.T20,
            MatchFormat.ODI,
            MatchFormat.Test
            ]);
    }

    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public void Constructor_WhenFormatProvided_ShouldCreateSettings()
    {
        MatchSettings settings = new(MatchFormat.T20);

        settings.Format.Should()
            .Be(MatchFormat.T20);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="format"></param>
    /// <param name="expectedSegments"></param>
    [Theory]
    [InlineData(MatchFormat.Practice, 0)]
    [InlineData(MatchFormat.T10, 10)]
    [InlineData(MatchFormat.T20, 20)]
    [InlineData(MatchFormat.ODI, 50)]
    [InlineData(MatchFormat.Test, 60)]
    public void SegmentsPerInnings_ShouldReturnCorrectValue(
    MatchFormat format,
    int expectedSegments)
    {
        MatchSettings settings = new(format);

        settings.SegmentsPerInnings
            .Should()
            .Be(expectedSegments);
    }
}
