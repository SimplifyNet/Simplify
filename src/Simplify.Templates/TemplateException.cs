using System;

namespace Simplify.Templates;

/// <summary>
/// Provides the template related exceptions
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="TemplateException"/> class.
/// </remarks>
/// <param name="message">The message that describes the error.</param>
public sealed class TemplateException(string message) : Exception(message)
{
}