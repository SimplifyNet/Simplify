using System;

namespace Simplify.System;

/// <summary>
/// Provides configuration related errors
/// </summary>
/// <seealso cref="Exception" />
public class ConfigurationException : Exception
{
	/// <summary>
	/// Initializes a new instance of the <see cref="ConfigurationException" /> class.
	/// </summary>
	/// <param name="message">The message that describes the error.</param>
	public ConfigurationException(string message) : base(message)
	{
	}
}