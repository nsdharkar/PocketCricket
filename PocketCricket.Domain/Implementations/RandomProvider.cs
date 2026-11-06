using System;
using System.Collections.Generic;
using System.Text;
using PocketCricket.Domain.Interfaces;

namespace PocketCricket.Domain.Implementations;

/// <summary>
/// A real implementation of IRandomProvider using the System.Random class.
/// </summary>
public sealed class RandomProvider : IRandomProvider
{
    private readonly Random _random;
    /// <summary>
    /// Initializes a new instance of the <see cref="RandomProvider"/> class.
    /// </summary>
    public RandomProvider()
    {
        _random = new Random();
    }
    /// <summary>
    /// Returns a non-negative random integer that is less than the specified maximum.
    /// </summary>
    /// <param name="maxValue">The exclusive upper bound of the random number to be generated. Must be greater than or equal to 0.</param>
    /// <returns>A 32-bit signed integer that is greater than or equal to 0, and less than maxValue; that is, the range of return values includes 0 but not maxValue. If maxValue equals 0, maxValue is returned.</returns>
    public int Next(int maxValue)
    {
        return _random.Next(maxValue);
    }
    /// <summary>
    /// Returns a random integer that is within a specified range.
    /// </summary>
    /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
    /// <param name="maxValue">The exclusive upper bound of the random number returned. Must be greater than or equal to minValue.</param>
    /// <returns>A 32-bit signed integer greater than or equal to minValue and less than maxValue; that is, the range of return values includes minValue but not maxValue. If minValue equals maxValue, minValue is returned.</returns>
    public int Next(int minValue, int maxValue)
    {
        return _random.Next(minValue, maxValue);
    }
}
