using System;

namespace Simplify.Mail;

/// <summary>
/// The exception class using for MailSender exceptions
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="MailSenderException"/> class.
/// </remarks>
/// <param name="message">The message that describes the error.</param>
public sealed class MailSenderException(string message) : Exception(message)
{
}