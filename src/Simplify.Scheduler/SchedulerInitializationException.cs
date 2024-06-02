using System;

namespace Simplify.Scheduler;

/// <summary>
/// The exception class using for scheduler initialization exceptions
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="SchedulerInitializationException"/> class.
/// </remarks>
/// <param name="message">The message that describes the error.</param>
[Serializable]
public sealed class SchedulerInitializationException(string message) : Exception(message);