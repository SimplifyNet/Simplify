using MailKit.Security;

namespace Simplify.Mail.Settings;

/// <summary>
/// Represent MailSender settings
/// </summary>
public interface IMailSenderSettings
{
	/// <summary>
	/// The SMTP server address
	/// </summary>
	string SmtpServerAddress { get; }

	/// <summary>
	/// The SMTP server port number
	/// </summary>
	int SmtpServerPortNumber { get; }

	/// <summary>
	/// The mail sender SMTP user name
	/// </summary>
	string SmtpUserName { get; }

	/// <summary>
	/// The mail sender SMTP user password
	/// </summary>
	string SmtpUserPassword { get; }

	/// <summary>
	/// Anti-spam pool message life time (min.)
	/// </summary>
	int AntiSpamPoolMessageLifeTime { get; }

	/// <summary>
	/// Anti-spam messages pool on
	/// </summary>
	bool AntiSpamMessagesPoolOn { get; }

	/// <summary>
	/// Gets a value indicating whether SSL is enabled for connection.
	/// </summary>
	/// <value>
	/// <c>true</c> if SSL is enabled for connection; otherwise, <c>false</c>.
	/// </value>
	bool EnableSsl { get; }

	/// <summary>
	/// Gets the secure socket options for the SMTP connection.
	/// When set, this overrides <see cref="EnableSsl"/>.
	/// When <c>null</c>, the connection behavior is determined by <see cref="EnableSsl"/>.
	/// </summary>
	SecureSocketOptions? SecureSocketOptions { get; }
}