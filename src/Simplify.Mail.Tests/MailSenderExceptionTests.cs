using NUnit.Framework;

namespace Simplify.Mail.Tests;

[TestFixture]
public class MailSenderExceptionTests
{
	[Test]
	public void Constructor_SetsMessage()
	{
		// Arrange
		const string message = "test message";

		// Act
		var ex = new MailSenderException(message);

		// Assert
		Assert.That(ex.Message, Is.EqualTo(message));
	}

	[Test]
	public void Constructor_IsException()
	{
		// Arrange
		var ex = new MailSenderException("test");

		// Act & Assert
		Assert.That(ex, Is.InstanceOf<System.Exception>());
	}
}
