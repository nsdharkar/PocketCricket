using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCricket.Domain.Matches;

/// <summary>
/// Represents the immutable configuration for a Pocket Cricket match.
/// </summary>
public sealed class MatchSettings
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MatchSettings"/> class.
    /// </summary>
    /// <param name="format">The match format.</param>
    public MatchSettings(MatchFormat format)
    {
        Format = format;

        SegmentsPerInnings = format switch
        {
            MatchFormat.Practice => 0,
            MatchFormat.T10 => 10,
            MatchFormat.T20 => 20,
            MatchFormat.ODI => 50,
            MatchFormat.Test => 60,
            _ => throw new ArgumentOutOfRangeException(nameof(format))
        };
    }

    /// <summary>
    /// Gets the selected match format.
    /// </summary>
    public MatchFormat Format { get; }

    /// <summary>
    /// Gets the number of segments played in each innings.
    /// A value of 0 represents an unlimited number of segments.
    /// </summary>
    public int SegmentsPerInnings { get; }
}
