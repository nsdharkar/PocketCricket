using System;
using System.Collections.Generic;
using System.Text;
using PocketCricket.Domain.Interfaces;

namespace PocketCricket.Domain.Tests.Random.Helpers;

/// <summary>
/// A fake implementation of IRandomProvider for testing purposes.
/// </summary>
public sealed class FakeRandomProvider : IRandomProvider
{
    private readonly Queue<int> _values;

    /// <summary>
    /// Initializes a new instance of the <see cref="FakeRandomProvider"/> class with the specified sequence of values.
    /// </summary>
    /// <param name="values"></param>
    public FakeRandomProvider(params int[] values)
    {
        ArgumentNullException.ThrowIfNull(values);

        _values = new Queue<int>(values);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public int Next(int maxValue)
    {
        if (_values.Count == 0)
        {
            throw new InvalidOperationException(
                "No fake random values remain.");
        }

        return _values.Dequeue();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public int Next(int minValue, int maxValue)
    {
        if (_values.Count == 0)
        {
            throw new InvalidOperationException(
                "No fake random values remain.");
        }

        int value = _values.Dequeue();

        if (value < minValue || value >= maxValue)
        {
            throw new InvalidOperationException(
                $"Fake random value '{value}' is outside the requested range [{minValue}, {maxValue}).");
        }

        return value;
    }
}
