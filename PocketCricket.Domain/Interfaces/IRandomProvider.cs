using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCricket.Domain.Interfaces;

/// <summary>
/// Provides a way to generate random numbers.
/// </summary>
public interface IRandomProvider
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    public int Next(int maxValue);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns> 
    public int Next(int minValue, int maxValue);
}
