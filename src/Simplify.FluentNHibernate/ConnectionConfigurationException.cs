using System;

namespace Simplify.FluentNHibernate;

/// <summary>
/// The exception class using for Database connection configuration exceptions
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DatabaseConnectionConfigurationException"/> class.
/// </remarks>
/// <param name="message">The message that describes the error.</param>
[Serializable]
public sealed class DatabaseConnectionConfigurationException(string message) : Exception(message)
{
}