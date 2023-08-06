using System;

namespace Simplify.Templates;

/// <summary>
/// provides `Template` related exceptions
/// </summary>
[Serializable]
public sealed class TemplateException : Exception
{
	/// <summary>
	/// Initializes a new instance of the <see cref="TemplateException"/> class.
	/// </summary>
	/// <param name="message">The message that describes the error.</param>
	public TemplateException(string message) : base(message) { }
}