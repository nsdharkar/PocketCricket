using System;
using System.Collections.Generic;
using System.Text;
using PocketCricket.Domain.Interfaces;

namespace PocketCricket.Domain.Tests.Random;

/// <summary>
/// A fake implementation of IRandomProvider for testing purposes.
/// </summary>
public sealed class FakeRandomProvider : IRandomProvider
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    public int Next(int maxValue)
    {
        return 0; // Always return 0 for testing purposes
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    public int Next(int minValue, int maxValue)
    {
        return minValue; // Always return the minimum value for testing purposes
    }
}
