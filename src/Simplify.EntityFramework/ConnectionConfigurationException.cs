using System;

namespace Simplify.EntityFramework
{
	/// <summary>
	/// The exception class using for Database connection configuration exceptions
	/// </summary>
	[Serializable]
	public sealed class DatabaseConnectionConfigurationException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DatabaseConnectionConfigurationException"/> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public DatabaseConnectionConfigurationException(string message) : base(message) { }
	}
}