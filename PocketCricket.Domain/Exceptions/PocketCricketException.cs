using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCricket.Domain.Exceptions;

/// <summary>
/// Represents a custom exception for the PocketCricket application.
/// </summary>
public abstract class PocketCricketException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PocketCricketException"/> class.
    /// </summary>
    /// <param name="message"></param>
    protected PocketCricketException(string message) : base(message)
    {
    }
}
